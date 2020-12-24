namespace CMCS.Monitor.Win.Frms
{
    partial class FrmAutoMaker
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnKeepMake = new DevComponents.DotNetBar.ButtonX();
			this.btnStartMake = new DevComponents.DotNetBar.ButtonX();
			this.btnDownMake = new DevComponents.DotNetBar.ButtonX();
			this.txtMakeCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.panWebBrower.SuspendLayout();
			this.gboxTest.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Interval = 3000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// panWebBrower
			// 
			this.panWebBrower.Controls.Add(this.groupBox1);
			this.panWebBrower.Controls.Add(this.gboxTest);
			this.panWebBrower.Dock = System.Windows.Forms.DockStyle.Fill;
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
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.groupBox1.Controls.Add(this.labelX1);
			this.groupBox1.Controls.Add(this.txtMakeCode);
			this.groupBox1.Controls.Add(this.btnKeepMake);
			this.groupBox1.Controls.Add(this.btnStartMake);
			this.groupBox1.Controls.Add(this.btnDownMake);
			this.groupBox1.ForeColor = System.Drawing.Color.White;
			this.groupBox1.Location = new System.Drawing.Point(1539, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(250, 114);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "制样测试 ";
			// 
			// btnKeepMake
			// 
			this.btnKeepMake.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnKeepMake.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnKeepMake.Location = new System.Drawing.Point(164, 62);
			this.btnKeepMake.Name = "btnKeepMake";
			this.btnKeepMake.Size = new System.Drawing.Size(71, 23);
			this.btnKeepMake.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnKeepMake.TabIndex = 6;
			this.btnKeepMake.Text = "继续制样";
			this.btnKeepMake.Click += new System.EventHandler(this.btnKeepMake_Click);
			// 
			// btnStartMake
			// 
			this.btnStartMake.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnStartMake.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnStartMake.Location = new System.Drawing.Point(6, 62);
			this.btnStartMake.Name = "btnStartMake";
			this.btnStartMake.Size = new System.Drawing.Size(71, 23);
			this.btnStartMake.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnStartMake.TabIndex = 4;
			this.btnStartMake.Text = "开始制样";
			this.btnStartMake.Click += new System.EventHandler(this.btnStartMake_Click);
			// 
			// btnDownMake
			// 
			this.btnDownMake.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnDownMake.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnDownMake.Location = new System.Drawing.Point(83, 62);
			this.btnDownMake.Name = "btnDownMake";
			this.btnDownMake.Size = new System.Drawing.Size(70, 23);
			this.btnDownMake.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnDownMake.TabIndex = 5;
			this.btnDownMake.Text = "暂停制样";
			this.btnDownMake.Click += new System.EventHandler(this.btnDownMake_Click);
			// 
			// txtMakeCode
			// 
			this.txtMakeCode.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.txtMakeCode.Border.Class = "TextBoxBorder";
			this.txtMakeCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtMakeCode.Font = new System.Drawing.Font("宋体", 11.25F);
			this.txtMakeCode.ForeColor = System.Drawing.Color.Black;
			this.txtMakeCode.Location = new System.Drawing.Point(91, 31);
			this.txtMakeCode.Name = "txtMakeCode";
			this.txtMakeCode.Size = new System.Drawing.Size(144, 25);
			this.txtMakeCode.TabIndex = 7;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("宋体", 11.25F);
			this.labelX1.Location = new System.Drawing.Point(20, 33);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(57, 23);
			this.labelX1.TabIndex = 8;
			this.labelX1.Text = "制样码";
			// 
			// FrmAutoMaker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1910, 920);
			this.Controls.Add(this.panWebBrower);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MinimumSize = new System.Drawing.Size(1910, 920);
			this.Name = "FrmAutoMaker";
			this.Text = "全自动制样机";
			this.Load += new System.EventHandler(this.FrmAutoMakerCSKY_Load);
			this.panWebBrower.ResumeLayout(false);
			this.gboxTest.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panWebBrower;
        private System.Windows.Forms.GroupBox gboxTest;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.ButtonX btnRequestData;
        private DevComponents.DotNetBar.ButtonX buttonX1;
		private System.Windows.Forms.GroupBox groupBox1;
		private DevComponents.DotNetBar.ButtonX btnKeepMake;
		private DevComponents.DotNetBar.ButtonX btnStartMake;
		private DevComponents.DotNetBar.ButtonX btnDownMake;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.Controls.TextBoxX txtMakeCode;
	}
}