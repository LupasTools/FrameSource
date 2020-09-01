// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// FrameSourceSampleForm.cs : 30.8.2020
// MIT license

using System;
using System.Drawing.Imaging;
using System.Windows.Forms;

using Lupas.FrameSource;

namespace Lupas.Samples.FrameSource
{
    public partial class FrameSourceSampleForm : Form
    {
        readonly IFrameSource frameSource;
        readonly IFrameData frameData;

        public FrameSourceSampleForm(IFrameSource source, IFrameData data)
        {
            InitializeComponent();
            toolStripLabelUsage.Text = "";

            frameSource = source;
            frameData = data;
            frameSource.OnNewFrame += OnNewFrame;
            frameSource.OnWorkerException += FrameSource_OnWorkerException;
        }

        private void FrameSource_OnWorkerException(object sender, ExceptionEventArgs e)
        {
            MessageBox.Show(e.Message, "Worker Exception");
        }

        private void toolStripButtonLive_Click(object sender, EventArgs e)
        {
            frameSource?.Live();
        }

        private void toolStripButtonFreeze_Click(object sender, EventArgs e)
        {
            frameSource?.Freeze();
        }

        private void toolStripButtonSetup_Click(object sender, EventArgs e)
        {
            frameSource?.ShowProperties();
        }

        private void OnNewFrame(object sender, NewFrameEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                frameSource?.CopyLastFrame(frameData);
                imageControl.LoadImage(frameData.DynamicData, frameData.W, frameData.H, frameData.Stride);
                toolStripLabelFPS.Text = $"FPS: {e.Fps,2:N1}";
            }));
        }

        private void FormFrameSourceTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            frameSource?.Dispose();
            (frameData as IDisposable)?.Dispose();
            JsonFile.Serialize(frameSource);
        }

        private void perfomanceComponent1_OnPerfomanceMeasured(object sender, EventArgs e)
        {
            toolStripLabelUsage.Visible = true;
            toolStripLabelUsage.Text = (sender as PerfomanceComponent).ToString();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            frameSource.CopyLastFrame(frameData);
            using (var bmp = frameData.ToBitmap())
                bmp.Save("tmp.png", ImageFormat.Png);
        }

        bool isFullScreen = false;
        int savedPaddingValue = 0;
        private void imageControl_DoubleClick(object sender, EventArgs e)
        {
            isFullScreen = !isFullScreen;
            if (isFullScreen)
            {
                savedPaddingValue = imageControl.ImagePadding;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                imageControl.ImagePadding = 0;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                imageControl.ImagePadding = savedPaddingValue;
            }
            toolStripMain.Visible = !isFullScreen;
        }
    }
}
