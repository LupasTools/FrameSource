namespace Lupas.FrameSource.MediaFoundation
{
    partial class MF_CameraPropertiesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxMediaTypes = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanelVidProc = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.tabControlProperyPages = new System.Windows.Forms.TabControl();
            this.tabPageSource = new System.Windows.Forms.TabPage();
            this.checkBoxDXDevice = new System.Windows.Forms.CheckBox();
            this.checkBoxAdvancedProcessing = new System.Windows.Forms.CheckBox();
            this.groupBoxTransform = new System.Windows.Forms.GroupBox();
            this.radioButtonRGBA = new System.Windows.Forms.RadioButton();
            this.radioButtonNV12 = new System.Windows.Forms.RadioButton();
            this.radioButtonRaw = new System.Windows.Forms.RadioButton();
            this.tabPageVideoAmp = new System.Windows.Forms.TabPage();
            this.tabPageCam = new System.Windows.Forms.TabPage();
            this.flowLayoutPanelCamControl = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControlProperyPages.SuspendLayout();
            this.tabPageSource.SuspendLayout();
            this.groupBoxTransform.SuspendLayout();
            this.tabPageVideoAmp.SuspendLayout();
            this.tabPageCam.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxMediaTypes
            // 
            this.comboBoxMediaTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMediaTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxMediaTypes.FormattingEnabled = true;
            this.comboBoxMediaTypes.Location = new System.Drawing.Point(6, 12);
            this.comboBoxMediaTypes.Name = "comboBoxMediaTypes";
            this.comboBoxMediaTypes.Size = new System.Drawing.Size(182, 21);
            this.comboBoxMediaTypes.TabIndex = 2;
            this.comboBoxMediaTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxMediaTypes_SelectedIndexChanged);
            // 
            // flowLayoutPanelVidProc
            // 
            this.flowLayoutPanelVidProc.AutoScroll = true;
            this.flowLayoutPanelVidProc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelVidProc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelVidProc.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelVidProc.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelVidProc.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelVidProc.Name = "flowLayoutPanelVidProc";
            this.flowLayoutPanelVidProc.Size = new System.Drawing.Size(328, 190);
            this.flowLayoutPanelVidProc.TabIndex = 7;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(269, 230);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonDefault
            // 
            this.buttonDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDefault.Location = new System.Drawing.Point(5, 230);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(75, 23);
            this.buttonDefault.TabIndex = 9;
            this.buttonDefault.Text = "Default";
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.buttonDefault_Click);
            // 
            // tabControlProperyPages
            // 
            this.tabControlProperyPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperyPages.Controls.Add(this.tabPageSource);
            this.tabControlProperyPages.Controls.Add(this.tabPageVideoAmp);
            this.tabControlProperyPages.Controls.Add(this.tabPageCam);
            this.tabControlProperyPages.Location = new System.Drawing.Point(5, 5);
            this.tabControlProperyPages.Name = "tabControlProperyPages";
            this.tabControlProperyPages.SelectedIndex = 0;
            this.tabControlProperyPages.Size = new System.Drawing.Size(342, 222);
            this.tabControlProperyPages.TabIndex = 10;
            this.tabControlProperyPages.TabStop = false;
            // 
            // tabPageSource
            // 
            this.tabPageSource.Controls.Add(this.checkBoxDXDevice);
            this.tabPageSource.Controls.Add(this.checkBoxAdvancedProcessing);
            this.tabPageSource.Controls.Add(this.groupBoxTransform);
            this.tabPageSource.Controls.Add(this.comboBoxMediaTypes);
            this.tabPageSource.Location = new System.Drawing.Point(4, 22);
            this.tabPageSource.Name = "tabPageSource";
            this.tabPageSource.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSource.Size = new System.Drawing.Size(334, 196);
            this.tabPageSource.TabIndex = 2;
            this.tabPageSource.Text = "Media Source";
            this.tabPageSource.UseVisualStyleBackColor = true;
            // 
            // checkBoxDXDevice
            // 
            this.checkBoxDXDevice.AutoSize = true;
            this.checkBoxDXDevice.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxDXDevice.Location = new System.Drawing.Point(168, 171);
            this.checkBoxDXDevice.Name = "checkBoxDXDevice";
            this.checkBoxDXDevice.Size = new System.Drawing.Size(160, 17);
            this.checkBoxDXDevice.TabIndex = 7;
            this.checkBoxDXDevice.Text = "Hardware DirectX Transform";
            this.checkBoxDXDevice.UseVisualStyleBackColor = true;
            // 
            // checkBoxAdvancedProcessing
            // 
            this.checkBoxAdvancedProcessing.AutoSize = true;
            this.checkBoxAdvancedProcessing.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxAdvancedProcessing.Location = new System.Drawing.Point(168, 148);
            this.checkBoxAdvancedProcessing.Name = "checkBoxAdvancedProcessing";
            this.checkBoxAdvancedProcessing.Size = new System.Drawing.Size(160, 17);
            this.checkBoxAdvancedProcessing.TabIndex = 6;
            this.checkBoxAdvancedProcessing.Text = "Advanced Video Processing";
            this.checkBoxAdvancedProcessing.UseVisualStyleBackColor = true;
            // 
            // groupBoxTransform
            // 
            this.groupBoxTransform.Controls.Add(this.radioButtonRGBA);
            this.groupBoxTransform.Controls.Add(this.radioButtonNV12);
            this.groupBoxTransform.Controls.Add(this.radioButtonRaw);
            this.groupBoxTransform.Location = new System.Drawing.Point(203, 6);
            this.groupBoxTransform.Name = "groupBoxTransform";
            this.groupBoxTransform.Size = new System.Drawing.Size(125, 102);
            this.groupBoxTransform.TabIndex = 5;
            this.groupBoxTransform.TabStop = false;
            this.groupBoxTransform.Text = "Transform";
            // 
            // radioButtonRGBA
            // 
            this.radioButtonRGBA.AutoSize = true;
            this.radioButtonRGBA.Location = new System.Drawing.Point(15, 66);
            this.radioButtonRGBA.Name = "radioButtonRGBA";
            this.radioButtonRGBA.Size = new System.Drawing.Size(90, 17);
            this.radioButtonRGBA.TabIndex = 2;
            this.radioButtonRGBA.Text = "Force RGB32";
            this.radioButtonRGBA.UseVisualStyleBackColor = true;
            // 
            // radioButtonNV12
            // 
            this.radioButtonNV12.AutoSize = true;
            this.radioButtonNV12.Location = new System.Drawing.Point(15, 43);
            this.radioButtonNV12.Name = "radioButtonNV12";
            this.radioButtonNV12.Size = new System.Drawing.Size(82, 17);
            this.radioButtonNV12.TabIndex = 1;
            this.radioButtonNV12.Text = "Force NV12";
            this.radioButtonNV12.UseVisualStyleBackColor = true;
            // 
            // radioButtonRaw
            // 
            this.radioButtonRaw.AutoSize = true;
            this.radioButtonRaw.Checked = true;
            this.radioButtonRaw.Location = new System.Drawing.Point(15, 20);
            this.radioButtonRaw.Name = "radioButtonRaw";
            this.radioButtonRaw.Size = new System.Drawing.Size(85, 17);
            this.radioButtonRaw.TabIndex = 0;
            this.radioButtonRaw.TabStop = true;
            this.radioButtonRaw.Text = "No transform";
            this.radioButtonRaw.UseVisualStyleBackColor = true;
            // 
            // tabPageVideoAmp
            // 
            this.tabPageVideoAmp.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageVideoAmp.Controls.Add(this.flowLayoutPanelVidProc);
            this.tabPageVideoAmp.Location = new System.Drawing.Point(4, 22);
            this.tabPageVideoAmp.Name = "tabPageVideoAmp";
            this.tabPageVideoAmp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVideoAmp.Size = new System.Drawing.Size(334, 196);
            this.tabPageVideoAmp.TabIndex = 0;
            this.tabPageVideoAmp.Text = "Video Processor";
            // 
            // tabPageCam
            // 
            this.tabPageCam.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageCam.Controls.Add(this.flowLayoutPanelCamControl);
            this.tabPageCam.Location = new System.Drawing.Point(4, 22);
            this.tabPageCam.Name = "tabPageCam";
            this.tabPageCam.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCam.Size = new System.Drawing.Size(334, 196);
            this.tabPageCam.TabIndex = 1;
            this.tabPageCam.Text = "Camera Control";
            // 
            // flowLayoutPanelCamControl
            // 
            this.flowLayoutPanelCamControl.AutoScroll = true;
            this.flowLayoutPanelCamControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelCamControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelCamControl.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelCamControl.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelCamControl.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelCamControl.Name = "flowLayoutPanelCamControl";
            this.flowLayoutPanelCamControl.Size = new System.Drawing.Size(328, 190);
            this.flowLayoutPanelCamControl.TabIndex = 8;
            // 
            // MF_CameraPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(350, 260);
            this.Controls.Add(this.tabControlProperyPages);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MF_CameraPropertiesForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Camera Properties";
            this.TopMost = true;
            this.tabControlProperyPages.ResumeLayout(false);
            this.tabPageSource.ResumeLayout(false);
            this.tabPageSource.PerformLayout();
            this.groupBoxTransform.ResumeLayout(false);
            this.groupBoxTransform.PerformLayout();
            this.tabPageVideoAmp.ResumeLayout(false);
            this.tabPageCam.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxMediaTypes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelVidProc;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.TabControl tabControlProperyPages;
        private System.Windows.Forms.TabPage tabPageVideoAmp;
        private System.Windows.Forms.TabPage tabPageCam;
        private System.Windows.Forms.TabPage tabPageSource;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCamControl;
        private System.Windows.Forms.GroupBox groupBoxTransform;
        private System.Windows.Forms.RadioButton radioButtonRGBA;
        private System.Windows.Forms.RadioButton radioButtonNV12;
        private System.Windows.Forms.RadioButton radioButtonRaw;
        private System.Windows.Forms.CheckBox checkBoxAdvancedProcessing;
        private System.Windows.Forms.CheckBox checkBoxDXDevice;
    }
}