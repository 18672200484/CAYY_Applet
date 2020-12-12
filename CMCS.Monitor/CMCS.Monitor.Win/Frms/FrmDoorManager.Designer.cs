namespace CMCS.Monitor.Win.Frms
{
    partial class FrmDoorManager
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
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.btnAll = new DevComponents.DotNetBar.ButtonX();
			this.btnSearch = new DevComponents.DotNetBar.ButtonX();
			this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// superGridControl1
			// 
			this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl1.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 14.25F);
			this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
			this.superGridControl1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl1.ForeColor = System.Drawing.Color.White;
			this.superGridControl1.Location = new System.Drawing.Point(3, 43);
			this.superGridControl1.Name = "superGridControl1";
			this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			gridColumn1.DataPropertyName = "Name";
			gridColumn1.HeaderText = "设备名称";
			gridColumn1.Name = "Name";
			gridColumn1.Width = 200;
			gridColumn2.DataPropertyName = "Ip";
			gridColumn2.HeaderText = "设备IP";
			gridColumn2.Name = "IP";
			gridColumn2.Width = 200;
			gridColumn3.DataPropertyName = "";
			gridColumn3.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn3.HeaderText = "";
			gridColumn3.Name = "gcmlOpen";
			gridColumn3.NullString = "开 门";
			gridColumn3.Width = 80;
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
			this.superGridControl1.PrimaryGrid.DefaultRowHeight = 25;
			this.superGridControl1.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 14.25F);
			this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
			this.superGridControl1.PrimaryGrid.MultiSelect = false;
			this.superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
			this.superGridControl1.Size = new System.Drawing.Size(1023, 450);
			this.superGridControl1.TabIndex = 1;
			this.superGridControl1.Text = "superGridControl1";
			this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
			this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.superGridControl1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1029, 496);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.btnAll);
			this.panelEx1.Controls.Add(this.btnSearch);
			this.panelEx1.Controls.Add(this.txtSearch);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(3, 3);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(1023, 34);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 19;
			// 
			// btnAll
			// 
			this.btnAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAll.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnAll.Location = new System.Drawing.Point(950, 5);
			this.btnAll.Name = "btnAll";
			this.btnAll.Size = new System.Drawing.Size(75, 23);
			this.btnAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnAll.TabIndex = 20;
			this.btnAll.Text = "全  部";
			this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnSearch.Location = new System.Drawing.Point(869, 6);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnSearch.TabIndex = 19;
			this.btnSearch.Text = "搜  索";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtSearch.Border.Class = "TextBoxBorder";
			this.txtSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.ForeColor = System.Drawing.Color.White;
			this.txtSearch.Location = new System.Drawing.Point(683, 4);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(180, 27);
			this.txtSearch.TabIndex = 18;
			this.txtSearch.WatermarkText = "请输入设备名称";
			// 
			// FrmDoorManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1029, 496);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmDoorManager";
			this.Text = "设备监控";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDoorManager_FormClosing);
			this.Load += new System.EventHandler(this.FrmCarMonitor_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panelEx1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

		#endregion

		private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private DevComponents.DotNetBar.ButtonX btnSearch;
		private DevComponents.DotNetBar.ButtonX btnAll;
	}
}