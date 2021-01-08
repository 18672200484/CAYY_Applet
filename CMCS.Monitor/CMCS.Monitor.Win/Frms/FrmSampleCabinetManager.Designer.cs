namespace CMCS.Monitor.Win.Frms
{
    partial class FrmSampleCabinetManager
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn28 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn29 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn30 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn31 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn32 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn33 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn34 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn35 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn36 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dtInputEnd = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnAll = new DevComponents.DotNetBar.ButtonX();
            this.lblto = new DevComponents.DotNetBar.LabelX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.dtInputStart = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.rtxtOutput = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtInputEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInputStart)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtxtOutput);
            this.splitContainer1.Size = new System.Drawing.Size(1900, 1000);
            this.splitContainer1.SplitterDistance = 1408;
            this.splitContainer1.TabIndex = 0;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1408, 1000);
            this.tableLayoutPanel1.TabIndex = 3;
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
            gridColumn28.DataPropertyName = "MACHINENAME";
            gridColumn28.HeaderText = "存放样柜";
            gridColumn28.Name = "MACHINENAME";
            gridColumn28.Width = 200;
            gridColumn29.DataPropertyName = "SAMPLECODE";
            gridColumn29.HeaderText = "样品编号";
            gridColumn29.Name = "SAMPLECODE";
            gridColumn29.Width = 200;
            gridColumn30.DataPropertyName = "SAMPLETYPE";
            gridColumn30.HeaderText = "样品类型";
            gridColumn30.Name = "SAMPLETYPE";
            gridColumn31.DataPropertyName = "INPUTTIME";
            gridColumn31.HeaderText = "存样时间";
            gridColumn31.Name = "INPUTTIME";
            gridColumn31.Width = 200;
            gridColumn32.DataPropertyName = "INPUTPLE";
            gridColumn32.HeaderText = "存样人";
            gridColumn32.Name = "INPUTPLE";
            gridColumn33.DataPropertyName = "BATCH";
            gridColumn33.HeaderText = "批次";
            gridColumn33.Name = "BATCH";
            gridColumn33.Width = 150;
            gridColumn34.DataPropertyName = "SUPPLIERNAME";
            gridColumn34.HeaderText = "供煤单位";
            gridColumn34.Name = "SUPPLIERNAME";
            gridColumn34.Width = 250;
            gridColumn35.DataPropertyName = "";
            gridColumn35.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn35.HeaderText = "";
            gridColumn35.Name = "gcmlTake";
            gridColumn35.NullString = "取 样";
            gridColumn35.Width = 80;
            gridColumn36.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn36.HeaderText = " ";
            gridColumn36.Name = "gcmlClear";
            gridColumn36.NullString = "弃 样";
            gridColumn36.Width = 80;
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn28);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn29);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn30);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn31);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn32);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn33);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn34);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn35);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn36);
            this.superGridControl1.PrimaryGrid.DefaultRowHeight = 25;
            this.superGridControl1.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            this.superGridControl1.Size = new System.Drawing.Size(1402, 954);
            this.superGridControl1.TabIndex = 1;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.dtInputEnd);
            this.panelEx1.Controls.Add(this.btnAll);
            this.panelEx1.Controls.Add(this.lblto);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.dtInputStart);
            this.panelEx1.Controls.Add(this.txtSearch);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1402, 34);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 19;
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(680, 5);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(71, 23);
            this.labelX1.TabIndex = 268;
            this.labelX1.Text = "存样时间";
            // 
            // dtInputEnd
            // 
            this.dtInputEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtInputEnd.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.dtInputEnd.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtInputEnd.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputEnd.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtInputEnd.ButtonDropDown.Visible = true;
            this.dtInputEnd.CustomFormat = "yyyy-MM-dd";
            this.dtInputEnd.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.dtInputEnd.ForeColor = System.Drawing.Color.Black;
            this.dtInputEnd.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtInputEnd.IsPopupCalendarOpen = false;
            this.dtInputEnd.Location = new System.Drawing.Point(923, 4);
            // 
            // 
            // 
            this.dtInputEnd.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtInputEnd.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputEnd.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtInputEnd.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputEnd.MonthCalendar.DisplayMonth = new System.DateTime(2016, 3, 1, 0, 0, 0, 0);
            this.dtInputEnd.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtInputEnd.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtInputEnd.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtInputEnd.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtInputEnd.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtInputEnd.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputEnd.MonthCalendar.TodayButtonVisible = true;
            this.dtInputEnd.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtInputEnd.Name = "dtInputEnd";
            this.dtInputEnd.Size = new System.Drawing.Size(133, 27);
            this.dtInputEnd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtInputEnd.TabIndex = 267;
            this.dtInputEnd.TimeSelectorTimeFormat = DevComponents.Editors.DateTimeAdv.eTimeSelectorFormat.Time24H;
            // 
            // btnAll
            // 
            this.btnAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAll.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnAll.Location = new System.Drawing.Point(1329, 5);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAll.TabIndex = 20;
            this.btnAll.Text = "全  部";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // lblto
            // 
            this.lblto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblto.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblto.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.lblto.ForeColor = System.Drawing.Color.Black;
            this.lblto.Location = new System.Drawing.Point(896, 6);
            this.lblto.Name = "lblto";
            this.lblto.Size = new System.Drawing.Size(21, 23);
            this.lblto.TabIndex = 266;
            this.lblto.Text = "至";
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnSearch.Location = new System.Drawing.Point(1248, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "搜  索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtInputStart
            // 
            this.dtInputStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtInputStart.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.dtInputStart.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtInputStart.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputStart.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtInputStart.ButtonDropDown.Visible = true;
            this.dtInputStart.CustomFormat = "yyyy-MM-dd";
            this.dtInputStart.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.dtInputStart.ForeColor = System.Drawing.Color.Black;
            this.dtInputStart.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtInputStart.IsPopupCalendarOpen = false;
            this.dtInputStart.Location = new System.Drawing.Point(757, 4);
            // 
            // 
            // 
            this.dtInputStart.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtInputStart.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputStart.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtInputStart.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputStart.MonthCalendar.DisplayMonth = new System.DateTime(2016, 3, 1, 0, 0, 0, 0);
            this.dtInputStart.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtInputStart.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtInputStart.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtInputStart.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtInputStart.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtInputStart.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputStart.MonthCalendar.TodayButtonVisible = true;
            this.dtInputStart.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtInputStart.Name = "dtInputStart";
            this.dtInputStart.Size = new System.Drawing.Size(133, 27);
            this.dtInputStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtInputStart.TabIndex = 265;
            this.dtInputStart.TimeSelectorTimeFormat = DevComponents.Editors.DateTimeAdv.eTimeSelectorFormat.Time24H;
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
            this.txtSearch.Location = new System.Drawing.Point(1062, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(180, 27);
            this.txtSearch.TabIndex = 18;
            this.txtSearch.WatermarkText = "请输入样品编码";
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.rtxtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtOutput.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.rtxtOutput.ForeColor = System.Drawing.Color.White;
            this.rtxtOutput.Location = new System.Drawing.Point(0, 0);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Size = new System.Drawing.Size(488, 1000);
            this.rtxtOutput.TabIndex = 1;
            this.rtxtOutput.Text = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 2000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // FrmSampleCabinetManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1900, 1000);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSampleCabinetManager";
            this.Text = "存样柜存取";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDoorManager_FormClosing);
            this.Load += new System.EventHandler(this.FrmCarMonitor_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtInputEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInputStart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnAll;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        private System.Windows.Forms.RichTextBox rtxtOutput;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtInputEnd;
        private DevComponents.DotNetBar.LabelX lblto;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtInputStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}