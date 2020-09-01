// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// MF_LiveFrameSource.cs : 30.8.2020
// MIT license

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lupas.FrameSource.MediaFoundation
{
    public partial class MF_LiveFrameSource : IFrameSource
    {
        public event NewFrameEventHandler OnNewFrame;
        public event TaskExceptionHandler OnWorkerException;

        public int DeviceID { get; set; } = 0;
        public int MediaTypeID { get; set; } = 0;
        public MediaTransform OutputTransform { get; set; } = MediaTransform.ForceNV12;
        public bool EnableAdvancedVideoProcessing { get; set; } = false;
        public bool EnableHardwareTransform { get; set; } = false;
        public int MaxFreezeTimeout { get; set; } = 2000;

        public List<AMCamProperty> AMproperties = new List<AMCamProperty>();

        public void Live()
        {
            WantToFreezeFlag = false;
            startSamplerTask();
        }

        public void Freeze()
        {
            WantToFreezeFlag = true;
            startSamplerTask();
            if (samplerTask == null || (samplerTask.Status != TaskStatus.Running && samplerTask.Status != TaskStatus.WaitingToRun))
                throw new Exception("Freeze failed: reader task isn't ready...");
            int waitTime = MaxFreezeTimeout;
            if (!freezedEvent.WaitOne(waitTime)) throw new TimeoutException($"Couldn't finish a freeze in {waitTime} ms");
        }

        public void CopyLastFrame(IFrameData dest)
        {
            if (dest == null) throw new NullReferenceException("Destination object is not initialized");
            lock (sampleLocker)
            {
                if (disposingFlag) { dest.ResizeIfRequired(0, 0, 0); return; };
                if (lastSample == null) throw new NullReferenceException("Frame is not ready");
                using (var buff = new BufferHelper(lastSample, lastSampleFrameH))
                {
                    dest.ResizeIfRequired(lastSampleFrameW, lastSampleFrameH, buff.Pitch);
                    dest.CopyFrom(buff.Data, lastSampleFrameH, buff.Pitch);
                }
            }
        }

        public void ShowProperties()
        {
            lock (startLocker)
            {
                if (reader == null || reader.IsDisposed)
                {
                    try
                    {
                        newSourceReader();
                    }
                    catch (Exception ex)
                    {
                        SharpDX.Utilities.Dispose(ref reader);
                        if (OnWorkerException == null) throw ex;
                        OnWorkerException.Invoke(this, new ExceptionEventArgs(ex, ex.Message));
                        return;
                    }
                }

                if (settingsForm == null || settingsForm.IsDisposed)
                {
                    UpdatePropertiesCache();
                    settingsForm = new MF_CameraPropertiesForm(deviceName, getNativeMediaTypesAsStrings(), AMproperties);
                    settingsForm.SelectedMediaType = MediaTypeID;
                    settingsForm.Transform = OutputTransform;
                    settingsForm.OnTransformChanged += SettingsForm_OnTransformChanged;
                    settingsForm.OnPropertyChanged += SettingsForm_OnPropertyChanged;
                    settingsForm.OnSourceReaderChanged += SettingsForm_OnSourceReaderChanged;
                    settingsForm.SetReaderCheckBoxes(EnableAdvancedVideoProcessing, EnableHardwareTransform);
                }
            }

            settingsForm.Show();
            settingsForm.BringToFront();
        }
    }
}