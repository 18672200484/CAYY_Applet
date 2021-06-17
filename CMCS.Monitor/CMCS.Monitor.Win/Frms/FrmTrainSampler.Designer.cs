namespace CMCS.Monitor.Win.Frms
{
    partial class FrmTrainSampler
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panWebBrower = new DevComponents.DotNetBar.PanelEx();
            this.btnHeadTailSection = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.btnChangeTrain = new DevComponents.DotNetBar.ButtonX();
            this.btnErrorReset = new DevComponents.DotNetBar.ButtonX();
            this.btnSystemReset = new DevComponents.DotNetBar.ButtonX();
            this.btnEndSampler = new DevComponents.DotNetBar.ButtonX();
            this.btnStartSampler = new DevComponents.DotNetBar.ButtonX();
            this.gboxTest = new System.Windows.Forms.GroupBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.btnRequestData = new DevComponents.DotNetBar.ButtonX();
            this.btnFaultRecord = new DevComponents.DotNetBar.ButtonX();
            this.panWebBrower.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.panWebBrower.CanvasColor = System.Drawing.SystemColors.Control;
            this.panWebBrower.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panWebBrower.Controls.Add(this.btnFaultRecord);
            this.panWebBrower.Controls.Add(this.btnHeadTailSection);
            this.panWebBrower.Controls.Add(this.panel1);
            this.panWebBrower.Controls.Add(this.btnChangeTrain);
            this.panWebBrower.Controls.Add(this.btnErrorReset);
            this.panWebBrower.Controls.Add(this.btnSystemReset);
            this.panWebBrower.Controls.Add(this.btnEndSampler);
            this.panWebBrower.Controls.Add(this.btnStartSampler);
            this.panWebBrower.Controls.Add(this.gboxTest);
            this.panWebBrower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panWebBrower.Location = new System.Drawing.Point(0, 0);
            this.panWebBrower.Margin = new System.Windows.Forms.Padding(0);
            this.panWebBrower.Name = "panWebBrower";
            this.panWebBrower.Size = new System.Drawing.Size(1910, 920);
            this.panWebBrower.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panWebBrower.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panWebBrower.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panWebBrower.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panWebBrower.Style.GradientAngle = 90;
            this.panWebBrower.TabIndex = 2;
            // 
            // btnHeadTailSection
            // 
            this.btnHeadTailSection.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnHeadTailSection.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnHeadTailSection.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeadTailSection.Location = new System.Drawing.Point(1543, 87);
            this.btnHeadTailSection.Name = "btnHeadTailSection";
            this.btnHeadTailSection.Size = new System.Drawing.Size(120, 30);
            this.btnHeadTailSection.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnHeadTailSection.TabIndex = 15;
            this.btnHeadTailSection.Text = "首/尾车";
            this.btnHeadTailSection.Click += new System.EventHandler(this.btnHeadTailSection_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.superGridControl1);
            this.panel1.Location = new System.Drawing.Point(1296, 382);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 309);
            this.panel1.TabIndex = 14;
            // 
            // superGridControl1
            // 
            this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl1.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl1.ForeColor = System.Drawing.Color.White;
            this.superGridControl1.Location = new System.Drawing.Point(0, 0);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn1.DataPropertyName = "SAMPLECODE";
            gridColumn1.HeaderText = "采样编码";
            gridColumn1.Name = "";
            gridColumn2.DataPropertyName = "CARNUMBER";
            gridColumn2.HeaderText = "车号";
            gridColumn2.Name = "";
            gridColumn3.DataPropertyName = "CARMODEL";
            gridColumn3.HeaderText = "车型";
            gridColumn3.Name = "";
            gridColumn3.Width = 75;
            gridColumn4.DataPropertyName = "ORDERNUMBER";
            gridColumn4.HeaderText = "序号";
            gridColumn4.Name = "";
            gridColumn4.Width = 60;
            gridColumn5.DataPropertyName = "CYCOUNT";
            gridColumn5.HeaderText = "采样点数";
            gridColumn5.Name = "";
            gridColumn5.Width = 80;
            gridColumn6.DataPropertyName = "TRAINCODE";
            gridColumn6.HeaderText = "轨道";
            gridColumn6.Name = "";
            gridColumn6.Width = 60;
            gridColumn7.DataPropertyName = "STATUS";
            gridColumn7.HeaderText = "采样状态";
            gridColumn7.Name = "";
            gridColumn7.Width = 80;
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn6);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn7);
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            this.superGridControl1.Size = new System.Drawing.Size(594, 309);
            this.superGridControl1.TabIndex = 3;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // btnChangeTrain
            // 
            this.btnChangeTrain.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChangeTrain.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChangeTrain.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeTrain.Location = new System.Drawing.Point(1402, 87);
            this.btnChangeTrain.Name = "btnChangeTrain";
            this.btnChangeTrain.Size = new System.Drawing.Size(120, 30);
            this.btnChangeTrain.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChangeTrain.TabIndex = 13;
            this.btnChangeTrain.Text = "切换轨道";
            this.btnChangeTrain.Click += new System.EventHandler(this.btnChangeTrain_Click);
            // 
            // btnErrorReset
            // 
            this.btnErrorReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnErrorReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnErrorReset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnErrorReset.Location = new System.Drawing.Point(1543, 51);
            this.btnErrorReset.Name = "btnErrorReset";
            this.btnErrorReset.Size = new System.Drawing.Size(120, 30);
            this.btnErrorReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnErrorReset.TabIndex = 12;
            this.btnErrorReset.Text = "故障复位";
            this.btnErrorReset.Click += new System.EventHandler(this.btnErrorReset_Click);
            // 
            // btnSystemReset
            // 
            this.btnSystemReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSystemReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSystemReset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSystemReset.Location = new System.Drawing.Point(1402, 51);
            this.btnSystemReset.Name = "btnSystemReset";
            this.btnSystemReset.Size = new System.Drawing.Size(120, 30);
            this.btnSystemReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSystemReset.TabIndex = 11;
            this.btnSystemReset.Text = "系统复位";
            this.btnSystemReset.Click += new System.EventHandler(this.btnSystemReset_Click);
            // 
            // btnEndSampler
            // 
            this.btnEndSampler.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEndSampler.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEndSampler.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndSampler.Location = new System.Drawing.Point(1543, 12);
            this.btnEndSampler.Name = "btnEndSampler";
            this.btnEndSampler.Size = new System.Drawing.Size(120, 30);
            this.btnEndSampler.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEndSampler.TabIndex = 10;
            this.btnEndSampler.Text = "停止采样";
            this.btnEndSampler.Click += new System.EventHandler(this.btnEndSampler_Click);
            // 
            // btnStartSampler
            // 
            this.btnStartSampler.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStartSampler.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStartSampler.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartSampler.Location = new System.Drawing.Point(1402, 12);
            this.btnStartSampler.Name = "btnStartSampler";
            this.btnStartSampler.Size = new System.Drawing.Size(120, 30);
            this.btnStartSampler.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnStartSampler.TabIndex = 9;
            this.btnStartSampler.Text = "开始采样";
            this.btnStartSampler.Click += new System.EventHandler(this.btnStartSampler_Click);
            // 
            // gboxTest
            // 
            this.gboxTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.gboxTest.Controls.Add(this.buttonX1);
            this.gboxTest.Controls.Add(this.btnRefresh);
            this.gboxTest.Controls.Add(this.btnRequestData);
            this.gboxTest.ForeColor = System.Drawing.Color.White;
            this.gboxTest.Location = new System.Drawing.Point(1796, 0);
            this.gboxTest.Name = "gboxTest";
            this.gboxTest.Size = new System.Drawing.Size(111, 113);
            this.gboxTest.TabIndex = 8;
            this.gboxTest.TabStop = false;
            this.gboxTest.Text = " 测 试 ";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(6, 80);
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
            // btnFaultRecord
            // 
            this.btnFaultRecord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFaultRecord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFaultRecord.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaultRecord.Location = new System.Drawing.Point(1402, 123);
            this.btnFaultRecord.Name = "btnFaultRecord";
            this.btnFaultRecord.Size = new System.Drawing.Size(120, 30);
            this.btnFaultRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFaultRecord.TabIndex = 16;
            this.btnFaultRecord.Text = "历史故障";
            this.btnFaultRecord.Click += new System.EventHandler(this.btnFaultRecord_Click);
            // 
            // FrmTrainSampler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1910, 920);
            this.Controls.Add(this.panWebBrower);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1910, 920);
            this.Name = "FrmTrainSampler";
            this.Text = "MetroForm";
            this.Load += new System.EventHandler(this.FrmTruckWeighter_Load);
            this.panWebBrower.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gboxTest.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private DevComponents.DotNetBar.PanelEx panWebBrower;
        private System.Windows.Forms.GroupBox gboxTest;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.ButtonX btnRequestData;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX btnStartSampler;
        private DevComponents.DotNetBar.ButtonX btnChangeTrain;
        private DevComponents.DotNetBar.ButtonX btnErrorReset;
        private DevComponents.DotNetBar.ButtonX btnSystemReset;
        private DevComponents.DotNetBar.ButtonX btnEndSampler;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.ButtonX btnHeadTailSection;
        private DevComponents.DotNetBar.ButtonX btnFaultRecord;
    }
}