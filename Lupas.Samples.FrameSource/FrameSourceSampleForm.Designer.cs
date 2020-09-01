namespace Lupas.Samples.FrameSource
{
    partial class FrameSourceSampleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrameSourceSampleForm));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSetup = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelFPS = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelUsage = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFreeze = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLive = new System.Windows.Forms.ToolStripButton();
            this.imageControl = new DX2D1ImageControl();
            this.perfomanceComponent1 = new PerfomanceComponent();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSetup,
            this.toolStripLabelFPS,
            this.toolStripSeparator1,
            this.toolStripLabelUsage,
            this.toolStripButtonSave,
            this.toolStripButtonFreeze,
            this.toolStripButtonLive});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(585, 25);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonSetup
            // 
            this.toolStripButtonSetup.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSetup.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSetup.Image")));
            this.toolStripButtonSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSetup.Name = "toolStripButtonSetup";
            this.toolStripButtonSetup.Size = new System.Drawing.Size(41, 22);
            this.toolStripButtonSetup.Text = "Setup";
            this.toolStripButtonSetup.Click += new System.EventHandler(this.toolStripButtonSetup_Click);
            // 
            // toolStripLabelFPS
            // 
            this.toolStripLabelFPS.Name = "toolStripLabelFPS";
            this.toolStripLabelFPS.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabelFPS.Text = "<FPS>";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabelUsage
            // 
            this.toolStripLabelUsage.Name = "toolStripLabelUsage";
            this.toolStripLabelUsage.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabelUsage.Text = "<Usage>";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(71, 22);
            this.toolStripButtonSave.Text = "Save Frame";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonFreeze
            // 
            this.toolStripButtonFreeze.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonFreeze.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonFreeze.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFreeze.Image")));
            this.toolStripButtonFreeze.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFreeze.Name = "toolStripButtonFreeze";
            this.toolStripButtonFreeze.Size = new System.Drawing.Size(44, 22);
            this.toolStripButtonFreeze.Text = "Freeze";
            this.toolStripButtonFreeze.Click += new System.EventHandler(this.toolStripButtonFreeze_Click);
            // 
            // toolStripButtonLive
            // 
            this.toolStripButtonLive.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonLive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonLive.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLive.Image")));
            this.toolStripButtonLive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLive.Name = "toolStripButtonLive";
            this.toolStripButtonLive.Size = new System.Drawing.Size(32, 22);
            this.toolStripButtonLive.Text = "Live";
            this.toolStripButtonLive.Click += new System.EventHandler(this.toolStripButtonLive_Click);
            // 
            // imageControl
            // 
            this.imageControl.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.imageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageControl.ImagePadding = 5;
            this.imageControl.Location = new System.Drawing.Point(0, 25);
            this.imageControl.Name = "imageControl";
            this.imageControl.Size = new System.Drawing.Size(585, 442);
            this.imageControl.TabIndex = 2;
            this.imageControl.DoubleClick += new System.EventHandler(this.imageControl_DoubleClick);
            // 
            // perfomanceComponent1
            // 
            this.perfomanceComponent1.Interval = 2000;
            this.perfomanceComponent1.OnPerfomanceMeasured += new System.EventHandler(this.perfomanceComponent1_OnPerfomanceMeasured);
            // 
            // FrameSourceSampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(585, 467);
            this.Controls.Add(this.imageControl);
            this.Controls.Add(this.toolStripMain);
            this.Name = "FrameSourceSampleForm";
            this.ShowIcon = false;
            this.Text = "Frame Source Sample";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFrameSourceTest_FormClosing);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonSetup;
        private System.Windows.Forms.ToolStripButton toolStripButtonFreeze;
        private System.Windows.Forms.ToolStripButton toolStripButtonLive;
        private DX2D1ImageControl imageControl;
        private System.Windows.Forms.ToolStripLabel toolStripLabelFPS;
        private PerfomanceComponent perfomanceComponent1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelUsage;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
    }
}

