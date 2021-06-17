namespace CMCS.Monitor.Win.Frms
{
    partial class FrmBatchMachine
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
            this.gboxTest = new System.Windows.Forms.GroupBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.btnRequestData = new DevComponents.DotNetBar.ButtonX();
            this.btnZT_GPLC = new System.Windows.Forms.Button();
            this.btnZT_DLLC = new System.Windows.Forms.Button();
            this.btnFW_GPLC = new System.Windows.Forms.Button();
            this.btnFW_DLLC = new System.Windows.Forms.Button();
            this.panWebBrower.SuspendLayout();
            this.gboxTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panWebBrower
            // 
            this.panWebBrower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
            this.panWebBrower.Controls.Add(this.btnFW_DLLC);
            this.panWebBrower.Controls.Add(this.btnFW_GPLC);
            this.panWebBrower.Controls.Add(this.btnZT_GPLC);
            this.panWebBrower.Controls.Add(this.btnZT_DLLC);
            this.panWebBrower.Controls.Add(this.gboxTest);
            this.panWebBrower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panWebBrower.ForeColor = System.Drawing.Color.White;
            this.panWebBrower.Location = new System.Drawing.Point(0, 0);
            this.panWebBrower.Margin = new System.Windows.Forms.Padding(0);
            this.panWebBrower.Name = "panWebBrower";
            this.panWebBrower.Size = new System.Drawing.Size(1910, 920);
            this.panWebBrower.TabIndex = 2;
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
            // btnZT_GPLC
            // 
            this.btnZT_GPLC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnZT_GPLC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZT_GPLC.FlatAppearance.BorderSize = 0;
            this.btnZT_GPLC.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnZT_GPLC.Font = new System.Drawing.Font("宋体", 16F);
            this.btnZT_GPLC.ForeColor = System.Drawing.Color.White;
            this.btnZT_GPLC.Location = new System.Drawing.Point(1593, 113);
            this.btnZT_GPLC.Name = "btnZT_GPLC";
            this.btnZT_GPLC.Size = new System.Drawing.Size(57, 35);
            this.btnZT_GPLC.TabIndex = 15;
            this.btnZT_GPLC.Text = "暂停";
            this.btnZT_GPLC.UseVisualStyleBackColor = false;
            this.btnZT_GPLC.Click += new System.EventHandler(this.btnZT_GPLC_Click);
            // 
            // btnZT_DLLC
            // 
            this.btnZT_DLLC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnZT_DLLC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZT_DLLC.FlatAppearance.BorderSize = 0;
            this.btnZT_DLLC.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnZT_DLLC.Font = new System.Drawing.Font("宋体", 16F);
            this.btnZT_DLLC.ForeColor = System.Drawing.Color.White;
            this.btnZT_DLLC.Location = new System.Drawing.Point(1593, 320);
            this.btnZT_DLLC.Name = "btnZT_DLLC";
            this.btnZT_DLLC.Size = new System.Drawing.Size(57, 35);
            this.btnZT_DLLC.TabIndex = 16;
            this.btnZT_DLLC.Text = "暂停";
            this.btnZT_DLLC.UseVisualStyleBackColor = false;
            this.btnZT_DLLC.Click += new System.EventHandler(this.btnZT_DLLC_Click);
            // 
            // btnFW_GPLC
            // 
            this.btnFW_GPLC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnFW_GPLC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFW_GPLC.FlatAppearance.BorderSize = 0;
            this.btnFW_GPLC.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFW_GPLC.Font = new System.Drawing.Font("宋体", 16F);
            this.btnFW_GPLC.ForeColor = System.Drawing.Color.White;
            this.btnFW_GPLC.Location = new System.Drawing.Point(1656, 113);
            this.btnFW_GPLC.Name = "btnFW_GPLC";
            this.btnFW_GPLC.Size = new System.Drawing.Size(56, 35);
            this.btnFW_GPLC.TabIndex = 17;
            this.btnFW_GPLC.Text = "复位";
            this.btnFW_GPLC.UseVisualStyleBackColor = false;
            this.btnFW_GPLC.Click += new System.EventHandler(this.btnFW_GPLC_Click);
            // 
            // btnFW_DLLC
            // 
            this.btnFW_DLLC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.btnFW_DLLC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFW_DLLC.FlatAppearance.BorderSize = 0;
            this.btnFW_DLLC.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFW_DLLC.Font = new System.Drawing.Font("宋体", 16F);
            this.btnFW_DLLC.ForeColor = System.Drawing.Color.White;
            this.btnFW_DLLC.Location = new System.Drawing.Point(1656, 320);
            this.btnFW_DLLC.Name = "btnFW_DLLC";
            this.btnFW_DLLC.Size = new System.Drawing.Size(56, 35);
            this.btnFW_DLLC.TabIndex = 18;
            this.btnFW_DLLC.Text = "复位";
            this.btnFW_DLLC.UseVisualStyleBackColor = false;
            this.btnFW_DLLC.Click += new System.EventHandler(this.btnFW_DLLC_Click);
            // 
            // FrmBatchMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1910, 920);
            this.Controls.Add(this.panWebBrower);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1910, 920);
            this.Name = "FrmBatchMachine";
            this.Text = "全自动制样机";
            this.Load += new System.EventHandler(this.FrmAutoMakerCSKY_Load);
            this.panWebBrower.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnZT_DLLC;
        private System.Windows.Forms.Button btnZT_GPLC;
        private System.Windows.Forms.Button btnFW_GPLC;
        private System.Windows.Forms.Button btnFW_DLLC;
    }
}