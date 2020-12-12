namespace CMCS.Monitor.Win.Frms
{
    partial class FrmDoor
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
			this.panWebBrower = new DevComponents.DotNetBar.PanelEx();
			this.panel1 = new DevComponents.DotNetBar.PanelEx();
			this.appBox = new SmileWei.EmbeddedApp.AppContainer(this.components);
			this.panWebBrower.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panWebBrower
			// 
			this.panWebBrower.CanvasColor = System.Drawing.SystemColors.Control;
			this.panWebBrower.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panWebBrower.Controls.Add(this.panel1);
			this.panWebBrower.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panWebBrower.Location = new System.Drawing.Point(0, 0);
			this.panWebBrower.Name = "panWebBrower";
			this.panWebBrower.Size = new System.Drawing.Size(1910, 920);
			this.panWebBrower.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panWebBrower.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panWebBrower.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panWebBrower.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panWebBrower.Style.GradientAngle = 90;
			this.panWebBrower.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panel1.Controls.Add(this.appBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1910, 920);
			this.panel1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panel1.Style.GradientAngle = 90;
			this.panel1.TabIndex = 3;
			// 
			// appBox
			// 
			this.appBox.AppFilename = "";
			this.appBox.AppProcess = null;
			this.appBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.appBox.Location = new System.Drawing.Point(0, 0);
			this.appBox.Name = "appBox";
			this.appBox.Size = new System.Drawing.Size(1910, 920);
			this.appBox.TabIndex = 1;
			// 
			// FrmDoor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1910, 920);
			this.Controls.Add(this.panWebBrower);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmDoor";
			this.Text = "设备监控";
			this.Load += new System.EventHandler(this.FrmCarMonitor_Load);
			this.panWebBrower.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.PanelEx panWebBrower;
		private DevComponents.DotNetBar.PanelEx panel1;
		private SmileWei.EmbeddedApp.AppContainer appBox;
	}
}