// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// MF_LiveFrameSource_Engine.cs : 30.8.2020
// MIT license

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.MediaFoundation;
using SharpDX.Direct3D11;
using DXDevice = SharpDX.Direct3D11.Device;

namespace Lupas.FrameSource.MediaFoundation
{
    public partial class MF_LiveFrameSource : IFrameSource
    {
        MediaSource dev = null;
        SourceReader reader = null;
        DXGIDeviceManager dxman = null;

        private CancellationTokenSource cancelTokenSource;
        private object sampleLocker = new object();
        private object startLocker = new object();
        MF_CameraPropertiesForm settingsForm = null;

        Task samplerTask = null;

        string deviceName;

        Sample lastSample = null;
        int lastSampleFrameH = 0;
        int lastSampleFrameW = 0;
        bool disposingFlag = false;

        public void Dispose()
        {
            stopSamplerTask();

            freezedEvent?.Dispose();
            kickstartEvent?.Dispose();

            SharpDX.Utilities.Dispose(ref settingsForm);
            SharpDX.Utilities.Dispose(ref reader);
            SharpDX.Utilities.Dispose(ref dev);
            SharpDX.Utilities.Dispose(ref dxman);

            lock (sampleLocker)
                SharpDX.Utilities.Dispose(ref lastSample);

            //MediaManager.Shutdown();
        }

        private void stopSamplerTask()
        {
            if (samplerTask?.Status != TaskStatus.Running) return;

            disposingFlag = true;
            cancelTokenSource.Cancel();
            kickstartEvent.Set();

            if (!samplerTask.Wait(5000)) throw new Exception("Couldn't exit reader task gracefully...");

            cancelTokenSource?.Dispose();
            samplerTask?.Dispose();
            SharpDX.Utilities.Dispose(ref reader);
        }

        private async void startSamplerTask()
        {
            kickstartEvent.Set();
            try
            {
                lock (startLocker)
                {
                    if (samplerTask?.Status == TaskStatus.Running) return;

                    if (WantToConfigureReader || reader == null || reader.IsDisposed)
                    {
                        newSourceReader();
                        WantToConfigureReader = false;
                        WantToChangeMediaType = true;
                    }

                    samplerTask?.Dispose();
                    cancelTokenSource?.Dispose();
                    cancelTokenSource = new CancellationTokenSource();
                    samplerTask = new Task(samplerProc, cancelTokenSource.Token);
                    samplerTask.Start();
                }
                //Sampler Task should eventually be awaited so that exceptions that may have been thrown can be handled
                await samplerTask;
            }
            catch (Exception ex)
            {
                if (OnWorkerException == null) throw ex;
                OnWorkerException?.Invoke(this, new ExceptionEventArgs(ex, ex.Message));
            }
        }

        private void samplerProc()
        {
            try
            {
                long currentSampleTime = 0;
                int streamRef = 0;
                SourceReaderFlags sourceReaderFlags = 0;

                int fpsStoreLen = 25;
                var fpsStore = new Queue<long>();

                Stopwatch sw = new Stopwatch();
                long lastSampleTime = -1;
                bool justUnfreezed = false;
                double fps = double.NaN;

                while (!cancelTokenSource.Token.IsCancellationRequested)
                {
                    bool gonnaFreeze = WantToFreezeFlag;
                    bool skipBufferedFrames = gonnaFreeze || justUnfreezed;

                    lock (startLocker)
                        if (WantToConfigureReader || reader == null || reader.IsDisposed)
                        {
                            newSourceReader();
                            WantToConfigureReader = false;
                            WantToChangeMediaType = true;
                            fpsStore.Clear();
                        }

                    if (WantToChangeMediaType)
                    {
                        setMediaType();
                        WantToChangeMediaType = false;
                        fpsStore.Clear();
                    }

                    if (skipBufferedFrames) sw.Restart();
                    Sample sample = reader.ReadSample(SourceReaderIndex.FirstVideoStream, 0, out streamRef, out sourceReaderFlags, out currentSampleTime);
                    if (skipBufferedFrames) sw.Stop();

                    if (sample == null && sourceReaderFlags.HasFlag(SourceReaderFlags.StreamTick)) continue;

                    if (sourceReaderFlags != SourceReaderFlags.None)
                    {
                        if (sample != null) sample.Dispose();
                        throw new Exception("Something went really wrong");
                    }

                    if (skipBufferedFrames)
                        if (sw.ElapsedMilliseconds <= 5) { SharpDX.Utilities.Dispose(ref sample); lastSampleTime = currentSampleTime; continue; }

                    justUnfreezed = false;

                    if (lastSampleTime != -1)
                    {
                        fpsStore.Enqueue(currentSampleTime - lastSampleTime);
                        if (fpsStore.Count() > fpsStoreLen) fpsStore.Dequeue();
                        fps = 10000000.0 / fpsStore.Average();
                    }
                    lastSampleTime = currentSampleTime;

                    if (cancelTokenSource.Token.IsCancellationRequested) break;

                    lock (sampleLocker)
                    {
                        if (lastSample == null || lastSample.TotalLength != sample.TotalLength)
                        {
                            var tp = reader.GetCurrentMediaType(SourceReaderIndex.FirstVideoStream);
                            var fsize = tp.Get(MediaTypeAttributeKeys.FrameSize);
                            tp.Dispose();
                            long w = fsize >> 32;
                            long h = fsize & 0xFFFF;

                            lastSampleFrameH = (int)h;
                            lastSampleFrameW = (int)w;
                        }

                        SharpDX.Utilities.Dispose(ref lastSample);
                        lastSample = sample;
                    }

                    OnNewFrame?.Invoke(this, new NewFrameEventArgs(gonnaFreeze ? double.NaN : Math.Round(fps, 1)));

                    if (!gonnaFreeze) continue;

                    WantToFreezeFlag = false;
                    freezedEvent.Set();
                    kickstartEvent.Reset();
                    kickstartEvent.Wait();
                    justUnfreezed = true;
                    fpsStore.Clear();
                }
            }
            catch (SharpDXException ex)
            {
                if ((ex.ResultCode == SharpDX.MediaFoundation.ResultCode.TopoCodecNotFound ||
                    ex.ResultCode == SharpDX.MediaFoundation.ResultCode.InvalidMediaType))
                {
                    String msg = $"Uncompatible MediaType format for current Source Reader configuration.\nSampler stopped\n\n{ex.Message}";
                    MessageBox.Show(msg, "Oops");
                }
                if (ex.ResultCode == SharpDX.MediaFoundation.ResultCode.VideoRecordingDeviceInvalidated)
                {
                    SharpDX.Utilities.Dispose(ref reader);
                    if (settingsForm != null) settingsForm.BeginInvoke(new Action(() => settingsForm.Close()));
                    WantToConfigureReader = true;   //  Recreate the device & reader on possible new connection attempt
                }
                throw;
            }
        }

        private void newSourceReader()
        {
            SharpDX.Utilities.Dispose(ref reader);
            Activate[] devices = null;
            try
            {
                using (MediaAttributes devAttr = new MediaAttributes())
                {
                    devAttr.Set(CaptureDeviceAttributeKeys.SourceType.Guid, CaptureDeviceAttributeKeys.SourceTypeVideoCapture.Guid);
                    devices = MediaFactory.EnumDeviceSources(devAttr);
                    if (devices.Count() <= DeviceID)
                    {
                        StringBuilder strException = new StringBuilder();
                        strException.Append($"Device(s) found:\n");
                        for (int n = 0; n < devices.Count(); n++)
                            strException.AppendLine($"{n} : {devices[n].Get(CaptureDeviceAttributeKeys.FriendlyName)}");
                        if (devices.Count() == 0) strException.AppendLine("None");
                        throw new ArgumentOutOfRangeException("DeviceID", DeviceID, strException.ToString());
                    }
                    dev = devices[DeviceID].ActivateObject<MediaSource>();
                    deviceName = devices[DeviceID].Get(CaptureDeviceAttributeKeys.FriendlyName);
                    applyDevProperties();
                }

                using (MediaAttributes readerAttr = new MediaAttributes())
                {
                    readerAttr.Set(SourceReaderAttributeKeys.EnableAdvancedVideoProcessing, EnableAdvancedVideoProcessing);
                    if (EnableHardwareTransform)
                    {
                        if (dxman == null || dxman.IsDisposed) newDXDeviceForVideo();
                        readerAttr.Set(SourceReaderAttributeKeys.D3DManager, dxman);
                    }
                    reader = new SourceReader(dev, readerAttr);
                }
            }
            finally
            {
                foreach (var d in devices) d.Dispose();
            }
        }

        private void newDXDeviceForVideo()
        {
            SharpDX.Utilities.Dispose(ref dxman);
            dxman = new DXGIDeviceManager();
            using (var dxdevice = new DXDevice(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.BgraSupport | DeviceCreationFlags.VideoSupport))
            {
                using (var mt = dxdevice.QueryInterface<DeviceMultithread>())
                    mt.SetMultithreadProtected(true);
                dxman.ResetDevice(dxdevice);
            }
        }

        private void setMediaType()
        {
            MediaType newType = null;
            try
            {
                newType = reader.GetNativeMediaType(SourceReaderIndex.FirstVideoStream, MediaTypeID);

                switch (OutputTransform)
                {
                    case MediaTransform.ForceNV12:
                        newType.Set(MediaTypeAttributeKeys.Subtype, VideoFormatGuids.NV12);
                        break;
                    case MediaTransform.ForceRGB32:
                        newType.Set(MediaTypeAttributeKeys.Subtype, VideoFormatGuids.Rgb32);
                        break;
                    default:
                        break;
                }
                reader.SetCurrentMediaType(SourceReaderIndex.FirstVideoStream, newType);
            }
            catch (SharpDXException ex) when (ex.ResultCode == SharpDX.MediaFoundation.ResultCode.TopoCodecNotFound ||
                                              ex.ResultCode == SharpDX.MediaFoundation.ResultCode.InvalidMediaType)
            {
                throw new NotSupportedException($"Couldn't set MediaType [{MediaTypeID}] with Transform [{OutputTransform}]");
            }
            catch (SharpDXException ex) when (ex.ResultCode == SharpDX.MediaFoundation.ResultCode.NoMoreTypes && MediaTypeID != 0)
            {
                MediaTypeID = 0;
                setMediaType();
            }
            finally
            {
                SharpDX.Utilities.Dispose(ref newType);
            }
        }

        volatile bool WantToFreezeFlag = false;
        volatile bool WantToConfigureReader = true;
        volatile bool WantToChangeMediaType = true;

        AutoResetEvent freezedEvent = new AutoResetEvent(false);
        ManualResetEventSlim kickstartEvent = new ManualResetEventSlim(false);

        private void SettingsForm_OnSourceReaderChanged(object sender, EventArgs e)
        {
            var form = sender as MF_CameraPropertiesForm;

            var (adv, dxdev) = form.GetReaderCheckBoxes();
            bool oldAdv = EnableAdvancedVideoProcessing;
            bool oldDxDev = EnableHardwareTransform;
            EnableAdvancedVideoProcessing = adv;
            EnableHardwareTransform = dxdev;

            WantToConfigureReader = true;
        }

        private void SettingsForm_OnPropertyChanged(object sender, PropertyBarEventArgs e)
        {
            SetAMProperty(e.prop as AMCamProperty, e.val, e.flag);
        }

        private void SettingsForm_OnTransformChanged(object sender, EventArgs e)
        {
            var form = sender as MF_CameraPropertiesForm;

            MediaTypeID = form.SelectedMediaType;
            OutputTransform = form.Transform;
            WantToChangeMediaType = true;
        }

        private void UpdatePropertiesCache()
        {
            AMproperties.Clear();

            IAMVideoProcAmp vpa = null;
            IAMCameraControl cc = null;

            try
            {
                vpa = (IAMVideoProcAmp)Marshal.GetObjectForIUnknown(dev.NativePointer);
                foreach (var pVal in Enum.GetValues(typeof(VideoProcAmpProperty)))
                {
                    AMVideoProcAmpProperty p = new AMVideoProcAmpProperty();
                    p.PropertyName = Enum.GetName(typeof(VideoProcAmpProperty), pVal);
                    p.ProperyId = (int)pVal;
                    VideoProcAmpFlags flags;
                    int hr = vpa.GetRange((VideoProcAmpProperty)pVal, out p.Min, out p.Max, out p.Delta, out p.DefaultValue, out flags);
                    if (hr != 0) continue;
                    p.PossibleFlags = (int)flags;
                    hr = vpa.Get((VideoProcAmpProperty)pVal, out p.Value, out flags);
                    if (hr != 0) continue;
                    p.Flag = (int)flags;
                    AMproperties.Add(p);
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult != SharpDX.Result.NoInterface.Code) throw;
            }
            finally
            {
                if (vpa != null) Marshal.ReleaseComObject(vpa);
            }

            try
            {
                cc = (IAMCameraControl)Marshal.GetObjectForIUnknown(dev.NativePointer);
                foreach (var pVal in Enum.GetValues(typeof(CameraControlProperty)))
                {
                    AMCameraControlProperty p = new AMCameraControlProperty();
                    p.PropertyName = Enum.GetName(typeof(CameraControlProperty), pVal);
                    p.ProperyId = (int)pVal;
                    CameraControlFlags flags;
                    int hr = cc.GetRange((CameraControlProperty)pVal, out p.Min, out p.Max, out p.Delta, out p.DefaultValue, out flags);
                    if (hr != 0) continue;
                    p.PossibleFlags = (int)flags;
                    hr = cc.Get((CameraControlProperty)pVal, out p.Value, out flags);
                    if (hr != 0) continue;
                    p.Flag = (int)flags;
                    AMproperties.Add(p);
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult != SharpDX.Result.NoInterface.Code) throw;
            }
            finally
            {
                if (cc != null) Marshal.ReleaseComObject(cc);
            }
        }

        internal bool SetAMProperty(AMCamProperty prop, int newValue, int flags)
        {
            int hr = 0;
            object setter = Marshal.GetObjectForIUnknown(dev.NativePointer);
            if (prop is AMVideoProcAmpProperty)
                hr = (setter as IAMVideoProcAmp).Set((VideoProcAmpProperty)prop.ProperyId, newValue, (VideoProcAmpFlags)flags);
            else
            if (prop is AMCameraControlProperty)
                hr = (setter as IAMCameraControl).Set((CameraControlProperty)prop.ProperyId, newValue, (CameraControlFlags)flags);
            Marshal.ReleaseComObject(setter);
            if (hr != 0) throw new ArgumentException($"Property [{prop.PropertyName}] Get/Set Exception: {new SharpDX.Result(hr)}");
            prop.Value = newValue; prop.Flag = flags;
            return true;
        }

        private void applyDevProperties()
        {
            foreach (var p in AMproperties) SetAMProperty(p, p.Value, p.Flag);
        }

        private IEnumerable<string> getNativeMediaTypesAsStrings()
        {
            int n = 0;
            var mediaFormats = typeof(VideoFormatGuids).GetFields();

            var mediatypesOut = new List<string>();

            while (true)
            {
                try
                {
                    var tp = reader.GetNativeMediaType(SourceReaderIndex.FirstVideoStream, n);
                    var s = tp.Get(MediaTypeAttributeKeys.Subtype);

                    long w = 0, h = 0;
                    double fps = double.NaN;
                    var format = mediaFormats.First((t) => (Guid)t.GetValue(t) == s).Name.ToUpper();

                    Guid res;
                    for (int a = 0; a < tp.Count; a++)
                    {
                        tp.GetByIndex(a, out res);

                        if (res.Equals(MediaTypeAttributeKeys.FrameRate.Guid))
                        {
                            var tmp = tp.Get(MediaTypeAttributeKeys.FrameRate);
                            double d1 = tmp >> 32;
                            double d2 = tmp & 0xFFFF;
                            fps = Math.Round((d1 / (d2 == 0 ? double.NaN : d2)), 0);
                            continue;
                        }
                        if (res.Equals(MediaTypeAttributeKeys.FrameSize.Guid))
                        {
                            var fsize = tp.Get(MediaTypeAttributeKeys.FrameSize);
                            w = fsize >> 32;
                            h = fsize & 0xFFFF;
                            continue;
                        }
                    }

                    string WxH = $"{w}x{h}";

                    mediatypesOut.Add($"{n++,3}: {WxH,10} {fps,3} FPS [{format}]");

                    tp.Dispose();
                }
                catch (SharpDX.SharpDXException ex)
                {
                    if (ex.ResultCode == SharpDX.MediaFoundation.ResultCode.NoMoreTypes) break;
                    throw;
                }
            }
            return mediatypesOut;
        }

        struct BufferHelper : IDisposable
        {
            Buffer2D buffer2D;
            MediaBuffer buffer;
            public IntPtr Data { get; private set; }
            public int Pitch { get; private set; }
            public int Length { get; private set; }
            public BufferHelper(Sample sample, int sampleHeight)
            {
                buffer2D = null;
                buffer = sample.ConvertToContiguousBuffer();
                buffer2D = buffer.QueryInterfaceOrNull<SharpDX.MediaFoundation.Buffer2D>();
                int length = 0, pitch = 0;

                if (buffer2D != null)
                {
                    byte[] arr = new byte[IntPtr.Size];
                    buffer2D.Lock2D(arr, out pitch);
                    length = buffer2D.ContiguousLength;
                    Data = new IntPtr(BitConverter.ToInt32(arr, 0));
                }
                else
                {
                    int curlen;
                    Data = buffer.Lock(out length, out curlen);
                    pitch = length / sampleHeight;
                }
                Length = length;
                Pitch = pitch;
            }

            private void Unlock()
            {
                if (buffer2D != null) buffer2D.Unlock2D();
                else buffer.Unlock();
                Data = IntPtr.Zero;
            }

            public void Dispose()
            {
                Unlock();
                if (buffer2D != null) buffer2D.Dispose();
                buffer.Dispose();
            }
        }
    }
}