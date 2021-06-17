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
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panWebBrower = new System.Windows.Forms.Panel();
			this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnRead = new DevComponents.DotNetBar.ButtonX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.txtMakeCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.btnKeepMake = new DevComponents.DotNetBar.ButtonX();
			this.btnStartMake = new DevComponents.DotNetBar.ButtonX();
			this.btnDownMake = new DevComponents.DotNetBar.ButtonX();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.dtInputSampleDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
			this.btnSearch = new DevComponents.DotNetBar.ButtonX();
			this.SGC_MakeCodeInfo = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.txtMakeCode_RG = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.btnKeepMake_RG = new DevComponents.DotNetBar.ButtonX();
			this.btnStartMake_RG = new DevComponents.DotNetBar.ButtonX();
			this.btnDownMake_RG = new DevComponents.DotNetBar.ButtonX();
			this.btnFaultReset = new System.Windows.Forms.Button();
			this.btnDeviceInfo = new System.Windows.Forms.Button();
			this.btnQlCxwfw = new System.Windows.Forms.Button();
			this.btnDataSelect = new System.Windows.Forms.Button();
			this.gboxTest = new System.Windows.Forms.GroupBox();
			this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
			this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
			this.btnRequestData = new DevComponents.DotNetBar.ButtonX();
			this.panWebBrower.SuspendLayout();
			this.expandablePanel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtInputSampleDate)).BeginInit();
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
			this.panWebBrower.Controls.Add(this.expandablePanel1);
			this.panWebBrower.Controls.Add(this.btnFaultReset);
			this.panWebBrower.Controls.Add(this.btnDeviceInfo);
			this.panWebBrower.Controls.Add(this.btnQlCxwfw);
			this.panWebBrower.Controls.Add(this.btnDataSelect);
			this.panWebBrower.Controls.Add(this.gboxTest);
			this.panWebBrower.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panWebBrower.ForeColor = System.Drawing.Color.White;
			this.panWebBrower.Location = new System.Drawing.Point(0, 0);
			this.panWebBrower.Margin = new System.Windows.Forms.Padding(0);
			this.panWebBrower.Name = "panWebBrower";
			this.panWebBrower.Size = new System.Drawing.Size(1910, 920);
			this.panWebBrower.TabIndex = 2;
			// 
			// expandablePanel1
			// 
			this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
			this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.expandablePanel1.Controls.Add(this.groupBox2);
			this.expandablePanel1.Controls.Add(this.groupBox1);
			this.expandablePanel1.ExpandOnTitleClick = true;
			this.expandablePanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.expandablePanel1.Location = new System.Drawing.Point(1379, 3);
			this.expandablePanel1.Name = "expandablePanel1";
			this.expandablePanel1.Size = new System.Drawing.Size(404, 521);
			this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandablePanel1.Style.GradientAngle = 90;
			this.expandablePanel1.TabIndex = 16;
			this.expandablePanel1.TitleHeight = 30;
			this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
			this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.expandablePanel1.TitleStyle.GradientAngle = 90;
			this.expandablePanel1.TitleText = "制样操作";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.groupBox2.Controls.Add(this.btnRead);
			this.groupBox2.Controls.Add(this.labelX1);
			this.groupBox2.Controls.Add(this.txtMakeCode);
			this.groupBox2.Controls.Add(this.btnKeepMake);
			this.groupBox2.Controls.Add(this.btnStartMake);
			this.groupBox2.Controls.Add(this.btnDownMake);
			this.groupBox2.ForeColor = System.Drawing.Color.White;
			this.groupBox2.Location = new System.Drawing.Point(29, 31);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(346, 118);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "合样归批煤样";
			// 
			// btnRead
			// 
			this.btnRead.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnRead.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnRead.Location = new System.Drawing.Point(241, 31);
			this.btnRead.Name = "btnRead";
			this.btnRead.Size = new System.Drawing.Size(64, 23);
			this.btnRead.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnRead.TabIndex = 9;
			this.btnRead.Text = "读取";
			this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
			// 
			// labelX1
			// 
			this.labelX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("宋体", 11.25F);
			this.labelX1.ForeColor = System.Drawing.Color.White;
			this.labelX1.Location = new System.Drawing.Point(19, 31);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(57, 23);
			this.labelX1.TabIndex = 8;
			this.labelX1.Text = "制样码";
			// 
			// txtMakeCode
			// 
			this.txtMakeCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtMakeCode.Border.Class = "TextBoxBorder";
			this.txtMakeCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtMakeCode.Font = new System.Drawing.Font("宋体", 11.25F);
			this.txtMakeCode.ForeColor = System.Drawing.Color.White;
			this.txtMakeCode.Location = new System.Drawing.Point(91, 29);
			this.txtMakeCode.Name = "txtMakeCode";
			this.txtMakeCode.Size = new System.Drawing.Size(140, 25);
			this.txtMakeCode.TabIndex = 7;
			// 
			// btnKeepMake
			// 
			this.btnKeepMake.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnKeepMake.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnKeepMake.Location = new System.Drawing.Point(211, 72);
			this.btnKeepMake.Name = "btnKeepMake";
			this.btnKeepMake.Size = new System.Drawing.Size(94, 23);
			this.btnKeepMake.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnKeepMake.TabIndex = 6;
			this.btnKeepMake.Text = "继续制样";
			this.btnKeepMake.Click += new System.EventHandler(this.btnKeepMake_Click);
			// 
			// btnStartMake
			// 
			this.btnStartMake.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnStartMake.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnStartMake.Location = new System.Drawing.Point(20, 72);
			this.btnStartMake.Name = "btnStartMake";
			this.btnStartMake.Size = new System.Drawing.Size(90, 23);
			this.btnStartMake.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnStartMake.TabIndex = 4;
			this.btnStartMake.Text = "开始制样";
			this.btnStartMake.Click += new System.EventHandler(this.btnStartMake_Click);
			// 
			// btnDownMake
			// 
			this.btnDownMake.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnDownMake.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnDownMake.Location = new System.Drawing.Point(123, 72);
			this.btnDownMake.Name = "btnDownMake";
			this.btnDownMake.Size = new System.Drawing.Size(79, 23);
			this.btnDownMake.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnDownMake.TabIndex = 5;
			this.btnDownMake.Text = "暂停制样";
			this.btnDownMake.Click += new System.EventHandler(this.btnDownMake_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.groupBox1.Controls.Add(this.labelX3);
			this.groupBox1.Controls.Add(this.dtInputSampleDate);
			this.groupBox1.Controls.Add(this.btnSearch);
			this.groupBox1.Controls.Add(this.SGC_MakeCodeInfo);
			this.groupBox1.Controls.Add(this.labelX2);
			this.groupBox1.Controls.Add(this.txtMakeCode_RG);
			this.groupBox1.Controls.Add(this.btnKeepMake_RG);
			this.groupBox1.Controls.Add(this.btnStartMake_RG);
			this.groupBox1.Controls.Add(this.btnDownMake_RG);
			this.groupBox1.ForeColor = System.Drawing.Color.White;
			this.groupBox1.Location = new System.Drawing.Point(29, 168);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(346, 321);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "人工投料煤样";
			// 
			// labelX3
			// 
			this.labelX3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.Font = new System.Drawing.Font("宋体", 11.25F);
			this.labelX3.ForeColor = System.Drawing.Color.White;
			this.labelX3.Location = new System.Drawing.Point(19, 112);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(70, 23);
			this.labelX3.TabIndex = 266;
			this.labelX3.Text = "采样日期";
			// 
			// dtInputSampleDate
			// 
			this.dtInputSampleDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.dtInputSampleDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.dtInputSampleDate.BackgroundStyle.Class = "DateTimeInputBackground";
			this.dtInputSampleDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtInputSampleDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
			this.dtInputSampleDate.ButtonDropDown.Visible = true;
			this.dtInputSampleDate.CustomFormat = "yyyy-MM-dd";
			this.dtInputSampleDate.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.dtInputSampleDate.ForeColor = System.Drawing.Color.White;
			this.dtInputSampleDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
			this.dtInputSampleDate.IsPopupCalendarOpen = false;
			this.dtInputSampleDate.Location = new System.Drawing.Point(91, 109);
			// 
			// 
			// 
			this.dtInputSampleDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dtInputSampleDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtInputSampleDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
			this.dtInputSampleDate.MonthCalendar.ClearButtonVisible = true;
			// 
			// 
			// 
			this.dtInputSampleDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
			this.dtInputSampleDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
			this.dtInputSampleDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.dtInputSampleDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.dtInputSampleDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.dtInputSampleDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
			this.dtInputSampleDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtInputSampleDate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 3, 1, 0, 0, 0, 0);
			this.dtInputSampleDate.MonthCalendar.MarkedDates = new System.DateTime[0];
			this.dtInputSampleDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dtInputSampleDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.dtInputSampleDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
			this.dtInputSampleDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.dtInputSampleDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dtInputSampleDate.MonthCalendar.TodayButtonVisible = true;
			this.dtInputSampleDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
			this.dtInputSampleDate.Name = "dtInputSampleDate";
			this.dtInputSampleDate.Size = new System.Drawing.Size(121, 27);
			this.dtInputSampleDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.dtInputSampleDate.TabIndex = 265;
			this.dtInputSampleDate.TimeSelectorTimeFormat = DevComponents.Editors.DateTimeAdv.eTimeSelectorFormat.Time24H;
			// 
			// btnSearch
			// 
			this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnSearch.Location = new System.Drawing.Point(232, 112);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(69, 23);
			this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnSearch.TabIndex = 12;
			this.btnSearch.Text = "查询";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// SGC_MakeCodeInfo
			// 
			this.SGC_MakeCodeInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.SGC_MakeCodeInfo.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			this.SGC_MakeCodeInfo.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.SGC_MakeCodeInfo.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.SGC_MakeCodeInfo.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
			this.SGC_MakeCodeInfo.ForeColor = System.Drawing.Color.White;
			this.SGC_MakeCodeInfo.Location = new System.Drawing.Point(20, 145);
			this.SGC_MakeCodeInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.SGC_MakeCodeInfo.Name = "SGC_MakeCodeInfo";
			this.SGC_MakeCodeInfo.PrimaryGrid.AutoGenerateColumns = false;
			this.SGC_MakeCodeInfo.PrimaryGrid.Caption.Text = "";
			gridColumn1.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
			gridColumn1.DataPropertyName = "";
			gridColumn1.HeaderText = "制样码";
			gridColumn1.Name = "MAKECODE";
			gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
			gridColumn2.HeaderText = "桶数";
			gridColumn2.Name = "SAMPLECOUNT";
			this.SGC_MakeCodeInfo.PrimaryGrid.Columns.Add(gridColumn1);
			this.SGC_MakeCodeInfo.PrimaryGrid.Columns.Add(gridColumn2);
			this.SGC_MakeCodeInfo.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
			this.SGC_MakeCodeInfo.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
			this.SGC_MakeCodeInfo.Size = new System.Drawing.Size(285, 158);
			this.SGC_MakeCodeInfo.TabIndex = 11;
			this.SGC_MakeCodeInfo.Text = "superGridControl1";
			this.SGC_MakeCodeInfo.CellDoubleClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs>(this.superGridControl_BuyFuel_CellDoubleClick);
			this.SGC_MakeCodeInfo.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl_BeginEdit);
			this.SGC_MakeCodeInfo.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
			// 
			// labelX2
			// 
			this.labelX2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Font = new System.Drawing.Font("宋体", 11.25F);
			this.labelX2.ForeColor = System.Drawing.Color.White;
			this.labelX2.Location = new System.Drawing.Point(19, 31);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(57, 23);
			this.labelX2.TabIndex = 8;
			this.labelX2.Text = "制样码";
			// 
			// txtMakeCode_RG
			// 
			this.txtMakeCode_RG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtMakeCode_RG.Border.Class = "TextBoxBorder";
			this.txtMakeCode_RG.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtMakeCode_RG.Font = new System.Drawing.Font("宋体", 11.25F);
			this.txtMakeCode_RG.ForeColor = System.Drawing.Color.White;
			this.txtMakeCode_RG.Location = new System.Drawing.Point(91, 29);
			this.txtMakeCode_RG.Name = "txtMakeCode_RG";
			this.txtMakeCode_RG.Size = new System.Drawing.Size(200, 25);
			this.txtMakeCode_RG.TabIndex = 7;
			// 
			// btnKeepMake_RG
			// 
			this.btnKeepMake_RG.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnKeepMake_RG.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnKeepMake_RG.Location = new System.Drawing.Point(202, 72);
			this.btnKeepMake_RG.Name = "btnKeepMake_RG";
			this.btnKeepMake_RG.Size = new System.Drawing.Size(77, 23);
			this.btnKeepMake_RG.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnKeepMake_RG.TabIndex = 6;
			this.btnKeepMake_RG.Text = "继续制样";
			this.btnKeepMake_RG.Click += new System.EventHandler(this.btnKeepMake_Click);
			// 
			// btnStartMake_RG
			// 
			this.btnStartMake_RG.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnStartMake_RG.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnStartMake_RG.Location = new System.Drawing.Point(20, 72);
			this.btnStartMake_RG.Name = "btnStartMake_RG";
			this.btnStartMake_RG.Size = new System.Drawing.Size(90, 23);
			this.btnStartMake_RG.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnStartMake_RG.TabIndex = 4;
			this.btnStartMake_RG.Text = "开始制样";
			this.btnStartMake_RG.Click += new System.EventHandler(this.btnStartMake_Click);
			// 
			// btnDownMake_RG
			// 
			this.btnDownMake_RG.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnDownMake_RG.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnDownMake_RG.Location = new System.Drawing.Point(116, 72);
			this.btnDownMake_RG.Name = "btnDownMake_RG";
			this.btnDownMake_RG.Size = new System.Drawing.Size(79, 23);
			this.btnDownMake_RG.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnDownMake_RG.TabIndex = 5;
			this.btnDownMake_RG.Text = "暂停制样";
			this.btnDownMake_RG.Click += new System.EventHandler(this.btnDownMake_Click);
			// 
			// btnFaultReset
			// 
			this.btnFaultReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.btnFaultReset.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnFaultReset.FlatAppearance.BorderSize = 0;
			this.btnFaultReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFaultReset.Font = new System.Drawing.Font("宋体", 16F);
			this.btnFaultReset.ForeColor = System.Drawing.Color.White;
			this.btnFaultReset.Location = new System.Drawing.Point(1700, 832);
			this.btnFaultReset.Name = "btnFaultReset";
			this.btnFaultReset.Size = new System.Drawing.Size(140, 48);
			this.btnFaultReset.TabIndex = 14;
			this.btnFaultReset.Text = "故障复位";
			this.btnFaultReset.UseVisualStyleBackColor = false;
			this.btnFaultReset.Click += new System.EventHandler(this.btnFaultReset_Click);
			// 
			// btnDeviceInfo
			// 
			this.btnDeviceInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.btnDeviceInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnDeviceInfo.FlatAppearance.BorderSize = 0;
			this.btnDeviceInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDeviceInfo.Font = new System.Drawing.Font("宋体", 16F);
			this.btnDeviceInfo.ForeColor = System.Drawing.Color.White;
			this.btnDeviceInfo.Location = new System.Drawing.Point(1700, 750);
			this.btnDeviceInfo.Name = "btnDeviceInfo";
			this.btnDeviceInfo.Size = new System.Drawing.Size(140, 48);
			this.btnDeviceInfo.TabIndex = 13;
			this.btnDeviceInfo.Text = "设备信息";
			this.btnDeviceInfo.UseVisualStyleBackColor = false;
			this.btnDeviceInfo.Click += new System.EventHandler(this.btnDeviceInfo_Click);
			// 
			// btnQlCxwfw
			// 
			this.btnQlCxwfw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.btnQlCxwfw.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnQlCxwfw.FlatAppearance.BorderSize = 0;
			this.btnQlCxwfw.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnQlCxwfw.Font = new System.Drawing.Font("宋体", 15F);
			this.btnQlCxwfw.ForeColor = System.Drawing.Color.White;
			this.btnQlCxwfw.Location = new System.Drawing.Point(1527, 832);
			this.btnQlCxwfw.Name = "btnQlCxwfw";
			this.btnQlCxwfw.Size = new System.Drawing.Size(140, 48);
			this.btnQlCxwfw.TabIndex = 12;
			this.btnQlCxwfw.Text = "弃料超限复位";
			this.btnQlCxwfw.UseVisualStyleBackColor = false;
			this.btnQlCxwfw.Click += new System.EventHandler(this.btnQlCxwfw_Click);
			// 
			// btnDataSelect
			// 
			this.btnDataSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.btnDataSelect.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnDataSelect.FlatAppearance.BorderSize = 0;
			this.btnDataSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDataSelect.Font = new System.Drawing.Font("宋体", 16F);
			this.btnDataSelect.ForeColor = System.Drawing.Color.White;
			this.btnDataSelect.Location = new System.Drawing.Point(1527, 750);
			this.btnDataSelect.Name = "btnDataSelect";
			this.btnDataSelect.Size = new System.Drawing.Size(140, 48);
			this.btnDataSelect.TabIndex = 11;
			this.btnDataSelect.Text = "数据查询";
			this.btnDataSelect.UseVisualStyleBackColor = false;
			this.btnDataSelect.Click += new System.EventHandler(this.btnDataSelect_Click);
			// 
			// gboxTest
			// 
			this.gboxTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gboxTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.gboxTest.Controls.Add(this.buttonX1);
			this.gboxTest.Controls.Add(this.btnRefresh);
			this.gboxTest.Controls.Add(this.btnRequestData);
			this.gboxTest.ForeColor = System.Drawing.Color.White;
			this.gboxTest.Location = new System.Drawing.Point(1798, 4);
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
			// FrmAutoMaker
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
			this.Name = "FrmAutoMaker";
			this.Text = "全自动制样机";
			this.Load += new System.EventHandler(this.FrmAutoMakerCSKY_Load);
			this.panWebBrower.ResumeLayout(false);
			this.expandablePanel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtInputSampleDate)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.ButtonX btnRead;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMakeCode;
        private DevComponents.DotNetBar.ButtonX btnKeepMake;
        private DevComponents.DotNetBar.ButtonX btnStartMake;
        private DevComponents.DotNetBar.ButtonX btnDownMake;
        private System.Windows.Forms.Button btnFaultReset;
        private System.Windows.Forms.Button btnDeviceInfo;
        private System.Windows.Forms.Button btnQlCxwfw;
        private System.Windows.Forms.Button btnDataSelect;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMakeCode_RG;
        private DevComponents.DotNetBar.ButtonX btnKeepMake_RG;
        private DevComponents.DotNetBar.ButtonX btnStartMake_RG;
        private DevComponents.DotNetBar.ButtonX btnDownMake_RG;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SGC_MakeCodeInfo;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtInputSampleDate;
    }
}