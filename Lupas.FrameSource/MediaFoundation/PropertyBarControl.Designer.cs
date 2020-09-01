namespace Lupas.FrameSource.MediaFoundation
{
    partial class PropertyBarControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelName = new System.Windows.Forms.Label();
            this.trackBarValues = new System.Windows.Forms.TrackBar();
            this.checkBoxAuto = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValues)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelName.AutoEllipsis = true;
            this.labelName.Location = new System.Drawing.Point(0, 3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(142, 23);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Brightness";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // trackBarValues
            // 
            this.trackBarValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarValues.BackColor = System.Drawing.SystemColors.Window;
            this.trackBarValues.Location = new System.Drawing.Point(138, -1);
            this.trackBarValues.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.trackBarValues.Name = "trackBarValues";
            this.trackBarValues.Size = new System.Drawing.Size(135, 45);
            this.trackBarValues.TabIndex = 1;
            this.trackBarValues.ValueChanged += new System.EventHandler(this.trackBarValues_ValueChanged);
            // 
            // checkBoxAuto
            // 
            this.checkBoxAuto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAuto.AutoSize = true;
            this.checkBoxAuto.Location = new System.Drawing.Point(269, 3);
            this.checkBoxAuto.Name = "checkBoxAuto";
            this.checkBoxAuto.Size = new System.Drawing.Size(48, 17);
            this.checkBoxAuto.TabIndex = 2;
            this.checkBoxAuto.Text = "Auto";
            this.checkBoxAuto.UseVisualStyleBackColor = true;
            // 
            // MF_CameraPropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxAuto);
            this.Controls.Add(this.trackBarValues);
            this.Controls.Add(this.labelName);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.Name = "MF_CameraPropertyControl";
            this.Size = new System.Drawing.Size(314, 22);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValues)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TrackBar trackBarValues;
        private System.Windows.Forms.CheckBox checkBoxAuto;
    }
}
