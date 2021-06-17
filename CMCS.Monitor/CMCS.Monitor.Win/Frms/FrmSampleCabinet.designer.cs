namespace CMCS.Monitor.Win.Frms
{
    partial class FrmSampleCabinet
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
            this.gboxTest = new System.Windows.Forms.GroupBox();
            this.btnRequestData = new DevComponents.DotNetBar.ButtonX();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.panWebBrower = new System.Windows.Forms.Panel();
            this.btnShow_rl3bc = new System.Windows.Forms.Button();
            this.btnShow_rl02cc = new System.Windows.Forms.Button();
            this.btnShow_rl02fx = new System.Windows.Forms.Button();
            this.btnShow_rl6qs = new System.Windows.Forms.Button();
            this.btnShow_rc3bc = new System.Windows.Forms.Button();
            this.btnShow_rc02cc = new System.Windows.Forms.Button();
            this.btnShow_rc02fx = new System.Windows.Forms.Button();
            this.btnShow_rc6qs = new System.Windows.Forms.Button();
            this.gboxTest.SuspendLayout();
            this.panWebBrower.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gboxTest
            // 
            this.gboxTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.gboxTest.Controls.Add(this.btnRefresh);
            this.gboxTest.Controls.Add(this.btnRequestData);
            this.gboxTest.ForeColor = System.Drawing.Color.White;
            this.gboxTest.Location = new System.Drawing.Point(1793, 3);
            this.gboxTest.Name = "gboxTest";
            this.gboxTest.Size = new System.Drawing.Size(111, 86);
            this.gboxTest.TabIndex = 9;
            this.gboxTest.TabStop = false;
            this.gboxTest.Text = " 测 试 ";
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
            // panWebBrower
            // 
            this.panWebBrower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
            this.panWebBrower.Controls.Add(this.btnShow_rc6qs);
            this.panWebBrower.Controls.Add(this.btnShow_rc02fx);
            this.panWebBrower.Controls.Add(this.btnShow_rc02cc);
            this.panWebBrower.Controls.Add(this.btnShow_rc3bc);
            this.panWebBrower.Controls.Add(this.btnShow_rl6qs);
            this.panWebBrower.Controls.Add(this.btnShow_rl02fx);
            this.panWebBrower.Controls.Add(this.btnShow_rl02cc);
            this.panWebBrower.Controls.Add(this.btnShow_rl3bc);
            this.panWebBrower.Controls.Add(this.gboxTest);
            this.panWebBrower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panWebBrower.ForeColor = System.Drawing.Color.White;
            this.panWebBrower.Location = new System.Drawing.Point(0, 0);
            this.panWebBrower.Margin = new System.Windows.Forms.Padding(0);
            this.panWebBrower.Name = "panWebBrower";
            this.panWebBrower.Size = new System.Drawing.Size(1910, 920);
            this.panWebBrower.TabIndex = 2;
            // 
            // btnShow_rl3bc
            // 
            this.btnShow_rl3bc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnShow_rl3bc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShow_rl3bc.FlatAppearance.BorderSize = 0;
            this.btnShow_rl3bc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShow_rl3bc.Font = new System.Drawing.Font("宋体", 10F);
            this.btnShow_rl3bc.ForeColor = System.Drawing.Color.White;
            this.btnShow_rl3bc.Location = new System.Drawing.Point(1546, 853);
            this.btnShow_rl3bc.Name = "btnShow_rl3bc";
            this.btnShow_rl3bc.Size = new System.Drawing.Size(56, 18);
            this.btnShow_rl3bc.TabIndex = 35;
            this.btnShow_rl3bc.Text = "显示";
            this.btnShow_rl3bc.UseVisualStyleBackColor = false;
            this.btnShow_rl3bc.Click += new System.EventHandler(this.btnShow_rl3bc_Click);
            // 
            // btnShow_rl02cc
            // 
            this.btnShow_rl02cc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnShow_rl02cc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShow_rl02cc.FlatAppearance.BorderSize = 0;
            this.btnShow_rl02cc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShow_rl02cc.Font = new System.Drawing.Font("宋体", 10F);
            this.btnShow_rl02cc.ForeColor = System.Drawing.Color.White;
            this.btnShow_rl02cc.Location = new System.Drawing.Point(1546, 830);
            this.btnShow_rl02cc.Name = "btnShow_rl02cc";
            this.btnShow_rl02cc.Size = new System.Drawing.Size(56, 18);
            this.btnShow_rl02cc.TabIndex = 36;
            this.btnShow_rl02cc.Text = "显示";
            this.btnShow_rl02cc.UseVisualStyleBackColor = false;
            this.btnShow_rl02cc.Click += new System.EventHandler(this.btnShow_rl02cc_Click);
            // 
            // btnShow_rl02fx
            // 
            this.btnShow_rl02fx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnShow_rl02fx.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShow_rl02fx.FlatAppearance.BorderSize = 0;
            this.btnShow_rl02fx.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShow_rl02fx.Font = new System.Drawing.Font("宋体", 10F);
            this.btnShow_rl02fx.ForeColor = System.Drawing.Color.White;
            this.btnShow_rl02fx.Location = new System.Drawing.Point(1546, 807);
            this.btnShow_rl02fx.Name = "btnShow_rl02fx";
            this.btnShow_rl02fx.Size = new System.Drawing.Size(56, 18);
            this.btnShow_rl02fx.TabIndex = 37;
            this.btnShow_rl02fx.Text = "显示";
            this.btnShow_rl02fx.UseVisualStyleBackColor = false;
            this.btnShow_rl02fx.Click += new System.EventHandler(this.btnShow_rl02fx_Click);
            // 
            // btnShow_rl6qs
            // 
            this.btnShow_rl6qs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnShow_rl6qs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShow_rl6qs.FlatAppearance.BorderSize = 0;
            this.btnShow_rl6qs.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShow_rl6qs.Font = new System.Drawing.Font("宋体", 10F);
            this.btnShow_rl6qs.ForeColor = System.Drawing.Color.White;
            this.btnShow_rl6qs.Location = new System.Drawing.Point(1546, 784);
            this.btnShow_rl6qs.Name = "btnShow_rl6qs";
            this.btnShow_rl6qs.Size = new System.Drawing.Size(56, 18);
            this.btnShow_rl6qs.TabIndex = 38;
            this.btnShow_rl6qs.Text = "显示";
            this.btnShow_rl6qs.UseVisualStyleBackColor = false;
            this.btnShow_rl6qs.Click += new System.EventHandler(this.btnShow_rl6qs_Click);
            // 
            // btnShow_rc3bc
            // 
            this.btnShow_rc3bc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnShow_rc3bc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShow_rc3bc.FlatAppearance.BorderSize = 0;
            this.btnShow_rc3bc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShow_rc3bc.Font = new System.Drawing.Font("宋体", 10F);
            this.btnShow_rc3bc.ForeColor = System.Drawing.Color.White;
            this.btnShow_rc3bc.Location = new System.Drawing.Point(1546, 760);
            this.btnShow_rc3bc.Name = "btnShow_rc3bc";
            this.btnShow_rc3bc.Size = new System.Drawing.Size(56, 18);
            this.btnShow_rc3bc.TabIndex = 39;
            this.btnShow_rc3bc.Text = "显示";
            this.btnShow_rc3bc.UseVisualStyleBackColor = false;
            this.btnShow_rc3bc.Click += new System.EventHandler(this.btnShow_rc3bc_Click);
            // 
            // btnShow_rc02cc
            // 
            this.btnShow_rc02cc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnShow_rc02cc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShow_rc02cc.FlatAppearance.BorderSize = 0;
            this.btnShow_rc02cc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShow_rc02cc.Font = new System.Drawing.Font("宋体", 10F);
            this.btnShow_rc02cc.ForeColor = System.Drawing.Color.White;
            this.btnShow_rc02cc.Location = new System.Drawing.Point(1546, 736);
            this.btnShow_rc02cc.Name = "btnShow_rc02cc";
            this.btnShow_rc02cc.Size = new System.Drawing.Size(56, 18);
            this.btnShow_rc02cc.TabIndex = 40;
            this.btnShow_rc02cc.Text = "显示";
            this.btnShow_rc02cc.UseVisualStyleBackColor = false;
            this.btnShow_rc02cc.Click += new System.EventHandler(this.btnShow_rc02cc_Click);
            // 
            // btnShow_rc02fx
            // 
            this.btnShow_rc02fx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnShow_rc02fx.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShow_rc02fx.FlatAppearance.BorderSize = 0;
            this.btnShow_rc02fx.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShow_rc02fx.Font = new System.Drawing.Font("宋体", 10F);
            this.btnShow_rc02fx.ForeColor = System.Drawing.Color.White;
            this.btnShow_rc02fx.Location = new System.Drawing.Point(1546, 712);
            this.btnShow_rc02fx.Name = "btnShow_rc02fx";
            this.btnShow_rc02fx.Size = new System.Drawing.Size(56, 18);
            this.btnShow_rc02fx.TabIndex = 41;
            this.btnShow_rc02fx.Text = "显示";
            this.btnShow_rc02fx.UseVisualStyleBackColor = false;
            this.btnShow_rc02fx.Click += new System.EventHandler(this.btnShow_rc02fx_Click);
            // 
            // btnShow_rc6qs
            // 
            this.btnShow_rc6qs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnShow_rc6qs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShow_rc6qs.FlatAppearance.BorderSize = 0;
            this.btnShow_rc6qs.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShow_rc6qs.Font = new System.Drawing.Font("宋体", 10F);
            this.btnShow_rc6qs.ForeColor = System.Drawing.Color.White;
            this.btnShow_rc6qs.Location = new System.Drawing.Point(1546, 688);
            this.btnShow_rc6qs.Name = "btnShow_rc6qs";
            this.btnShow_rc6qs.Size = new System.Drawing.Size(56, 18);
            this.btnShow_rc6qs.TabIndex = 42;
            this.btnShow_rc6qs.Text = "显示";
            this.btnShow_rc6qs.UseVisualStyleBackColor = false;
            this.btnShow_rc6qs.Click += new System.EventHandler(this.btnShow_rc6qs_Click);
            // 
            // FrmSampleCabinet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1910, 920);
            this.Controls.Add(this.panWebBrower);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmSampleCabinet";
            this.Text = "全自动气动传输";
            this.Load += new System.EventHandler(this.FrmAutoCupboardPneumaticTransfer_Load);
            this.gboxTest.ResumeLayout(false);
            this.panWebBrower.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox gboxTest;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.ButtonX btnRequestData;
        private System.Windows.Forms.Panel panWebBrower;
        private System.Windows.Forms.Button btnShow_rc6qs;
        private System.Windows.Forms.Button btnShow_rc02fx;
        private System.Windows.Forms.Button btnShow_rc02cc;
        private System.Windows.Forms.Button btnShow_rc3bc;
        private System.Windows.Forms.Button btnShow_rl6qs;
        private System.Windows.Forms.Button btnShow_rl02fx;
        private System.Windows.Forms.Button btnShow_rl02cc;
        private System.Windows.Forms.Button btnShow_rl3bc;
    }
}