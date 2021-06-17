namespace CMCS.Monitor.Win.Frms
{
    partial class FrmCarSampler
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panWebBrower = new System.Windows.Forms.Panel();
            this.btnReSample = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pbxAutotruck = new System.Windows.Forms.PictureBox();
            this.gboxTest = new System.Windows.Forms.GroupBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.btnRequestData = new DevComponents.DotNetBar.ButtonX();
            this.panWebBrower.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAutotruck)).BeginInit();
            this.gboxTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panWebBrower
            // 
            this.panWebBrower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
            this.panWebBrower.Controls.Add(this.btnReSample);
            this.panWebBrower.Controls.Add(this.btnReset);
            this.panWebBrower.Controls.Add(this.btnStop);
            this.panWebBrower.Controls.Add(this.pbxAutotruck);
            this.panWebBrower.Controls.Add(this.gboxTest);
            this.panWebBrower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panWebBrower.ForeColor = System.Drawing.Color.White;
            this.panWebBrower.Location = new System.Drawing.Point(0, 0);
            this.panWebBrower.Margin = new System.Windows.Forms.Padding(0);
            this.panWebBrower.Name = "panWebBrower";
            this.panWebBrower.Size = new System.Drawing.Size(1910, 920);
            this.panWebBrower.TabIndex = 2;
            // 
            // btnReSample
            // 
            this.btnReSample.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnReSample.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReSample.FlatAppearance.BorderSize = 0;
            this.btnReSample.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReSample.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.btnReSample.ForeColor = System.Drawing.Color.White;
            this.btnReSample.Location = new System.Drawing.Point(1535, 786);
            this.btnReSample.Name = "btnReSample";
            this.btnReSample.Size = new System.Drawing.Size(220, 74);
            this.btnReSample.TabIndex = 14;
            this.btnReSample.Text = "重新采样";
            this.btnReSample.UseVisualStyleBackColor = false;
            this.btnReSample.Click += new System.EventHandler(this.btnReSample_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReset.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(1535, 675);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(220, 74);
            this.btnReset.TabIndex = 13;
            this.btnReset.Text = "复    位";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStop.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(1535, 563);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(220, 74);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "急    停";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pbxAutotruck
            // 
            this.pbxAutotruck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.pbxAutotruck.ForeColor = System.Drawing.Color.White;
            this.pbxAutotruck.Location = new System.Drawing.Point(270, 700);
            this.pbxAutotruck.Name = "pbxAutotruck";
            this.pbxAutotruck.Size = new System.Drawing.Size(249, 130);
            this.pbxAutotruck.TabIndex = 9;
            this.pbxAutotruck.TabStop = false;
            this.pbxAutotruck.Visible = false;
            // 
            // gboxTest
            // 
            this.gboxTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.gboxTest.Controls.Add(this.buttonX1);
            this.gboxTest.Controls.Add(this.btnRefresh);
            this.gboxTest.Controls.Add(this.btnRequestData);
            this.gboxTest.ForeColor = System.Drawing.Color.White;
            this.gboxTest.Location = new System.Drawing.Point(1795, 4);
            this.gboxTest.Name = "gboxTest";
            this.gboxTest.Size = new System.Drawing.Size(111, 114);
            this.gboxTest.TabIndex = 8;
            this.gboxTest.TabStop = false;
            this.gboxTest.Text = " 测 试 ";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(6, 82);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(98, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 6;
            this.buttonX1.Text = "测试";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Location = new System.Drawing.Point(6, 23);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(98, 23);
            this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "刷新页面";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRequestData
            // 
            this.btnRequestData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRequestData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRequestData.Location = new System.Drawing.Point(6, 51);
            this.btnRequestData.Name = "btnRequestData";
            this.btnRequestData.Size = new System.Drawing.Size(98, 23);
            this.btnRequestData.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRequestData.TabIndex = 5;
            this.btnRequestData.Text = "数据更新";
            this.btnRequestData.Click += new System.EventHandler(this.btnRequestData_Click);
            // 
            // FrmCarSampler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1910, 920);
            this.Controls.Add(this.panWebBrower);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1910, 920);
            this.Name = "FrmCarSampler";
            this.Text = "全自动制样机";
            this.Load += new System.EventHandler(this.FrmCarSampler_Load);
            this.panWebBrower.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxAutotruck)).EndInit();
            this.gboxTest.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panWebBrower;
        private System.Windows.Forms.GroupBox gboxTest;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.ButtonX btnRequestData;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.PictureBox pbxAutotruck;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnReSample;
        private System.Windows.Forms.Button btnReset;
    }
}