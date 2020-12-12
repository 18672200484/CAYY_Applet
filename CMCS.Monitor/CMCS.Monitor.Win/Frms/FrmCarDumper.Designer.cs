namespace CMCS.CarTransport.BeltSampler.Frms
{
    partial class FrmCarDumper
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn18 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.superGridControl2 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.lblG2WFC = new System.Windows.Forms.Label();
            this.lblG4WFC = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(1345, 682);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 1;
            this.buttonX2.Text = "buttonX2";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 2000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.panel1.Controls.Add(this.lblG4WFC);
            this.panel1.Controls.Add(this.lblG2WFC);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1900, 1000);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1262, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(325, 62);
            this.label2.TabIndex = 3;
            this.label2.Text = "#4轨翻车信息";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(291, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 62);
            this.label1.TabIndex = 2;
            this.label1.Text = "#2轨翻车信息";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.superGridControl2);
            this.panel3.Location = new System.Drawing.Point(977, 166);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(900, 700);
            this.panel3.TabIndex = 1;
            // 
            // superGridControl2
            // 
            this.superGridControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.superGridControl2.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl2.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl2.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl2.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl2.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl2.ForeColor = System.Drawing.Color.White;
            this.superGridControl2.Location = new System.Drawing.Point(0, 0);
            this.superGridControl2.Name = "superGridControl2";
            this.superGridControl2.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn1.DataPropertyName = "BATCH";
            gridColumn1.HeaderText = "批次";
            gridColumn1.Name = "";
            gridColumn1.Width = 120;
            gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn2.DataPropertyName = "ORDERNUM";
            gridColumn2.HeaderText = "队列顺序";
            gridColumn2.Name = "";
            gridColumn3.DataPropertyName = "TRAINNUMBER";
            gridColumn3.HeaderText = "车号";
            gridColumn3.Name = "";
            gridColumn4.DataPropertyName = "CARMODEL";
            gridColumn4.HeaderText = "车型";
            gridColumn4.Name = "";
            gridColumn4.Width = 90;
            gridColumn5.DataPropertyName = "PASSTIME";
            gridColumn5.HeaderText = "入厂时间";
            gridColumn5.Name = "";
            gridColumn5.Width = 150;
            gridColumn6.DataPropertyName = "GROSSQTY";
            gridColumn6.HeaderText = "毛重";
            gridColumn6.Name = "";
            gridColumn6.Width = 80;
            gridColumn7.DataPropertyName = "SKINQTY";
            gridColumn7.HeaderText = "皮重";
            gridColumn7.Name = "";
            gridColumn7.Width = 80;
            gridColumn8.DataPropertyName = "SUTTLEQTY";
            gridColumn8.HeaderText = "净重";
            gridColumn8.Name = "";
            gridColumn8.Width = 80;
            gridColumn9.DataPropertyName = "ISFC";
            gridColumn9.HeaderText = "翻车状态";
            gridColumn9.Name = "";
            gridColumn9.Width = 80;
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn4);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn5);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn6);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn7);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn8);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn9);
            this.superGridControl2.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl2.PrimaryGrid.MultiSelect = false;
            this.superGridControl2.PrimaryGrid.ShowRowGridIndex = true;
            this.superGridControl2.Size = new System.Drawing.Size(900, 700);
            this.superGridControl2.TabIndex = 3;
            this.superGridControl2.Text = "superGridControl2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.superGridControl1);
            this.panel2.Location = new System.Drawing.Point(25, 166);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 700);
            this.panel2.TabIndex = 0;
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
            gridColumn10.DataPropertyName = "BATCH";
            gridColumn10.HeaderText = "批次";
            gridColumn10.Name = "";
            gridColumn10.Width = 120;
            gridColumn11.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn11.DataPropertyName = "ORDERNUM";
            gridColumn11.HeaderText = "队列顺序";
            gridColumn11.Name = "";
            gridColumn12.DataPropertyName = "TRAINNUMBER";
            gridColumn12.HeaderText = "车号";
            gridColumn12.Name = "";
            gridColumn13.DataPropertyName = "CARMODEL";
            gridColumn13.HeaderText = "车型";
            gridColumn13.Name = "";
            gridColumn13.Width = 90;
            gridColumn14.DataPropertyName = "PASSTIME";
            gridColumn14.HeaderText = "入厂时间";
            gridColumn14.Name = "";
            gridColumn14.Width = 150;
            gridColumn15.DataPropertyName = "GROSSQTY";
            gridColumn15.HeaderText = "毛重";
            gridColumn15.Name = "";
            gridColumn15.Width = 80;
            gridColumn16.DataPropertyName = "SKINQTY";
            gridColumn16.HeaderText = "皮重";
            gridColumn16.Name = "";
            gridColumn16.Width = 80;
            gridColumn17.DataPropertyName = "SUTTLEQTY";
            gridColumn17.HeaderText = "净重";
            gridColumn17.Name = "";
            gridColumn17.Width = 80;
            gridColumn18.DataPropertyName = "ISFC";
            gridColumn18.HeaderText = "翻车状态";
            gridColumn18.Name = "";
            gridColumn18.Width = 80;
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn10);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn11);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn12);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn13);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn14);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn15);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn16);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn17);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn18);
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            this.superGridControl1.Size = new System.Drawing.Size(900, 700);
            this.superGridControl1.TabIndex = 2;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // lblG2WFC
            // 
            this.lblG2WFC.AutoSize = true;
            this.lblG2WFC.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblG2WFC.Location = new System.Drawing.Point(20, 123);
            this.lblG2WFC.Name = "lblG2WFC";
            this.lblG2WFC.Size = new System.Drawing.Size(128, 28);
            this.lblG2WFC.TabIndex = 4;
            this.lblG2WFC.Text = "未翻车：0 车";
            // 
            // lblG4WFC
            // 
            this.lblG4WFC.AutoSize = true;
            this.lblG4WFC.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblG4WFC.Location = new System.Drawing.Point(972, 123);
            this.lblG4WFC.Name = "lblG4WFC";
            this.lblG4WFC.Size = new System.Drawing.Size(128, 28);
            this.lblG4WFC.TabIndex = 5;
            this.lblG4WFC.Text = "未翻车：0 车";
            // 
            // FrmCarDumper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1900, 1000);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonX2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCarDumper";
            this.Text = " 入 厂 煤 采 样 ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCarSampler_FormClosing);
            this.Load += new System.EventHandler(this.FrmCarSampler_Load);
            this.Shown += new System.EventHandler(this.FrmCarSampler_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl2;
        private System.Windows.Forms.Label lblG4WFC;
        private System.Windows.Forms.Label lblG2WFC;
    }
}