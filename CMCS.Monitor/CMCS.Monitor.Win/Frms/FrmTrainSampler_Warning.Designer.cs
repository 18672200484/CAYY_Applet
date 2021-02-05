namespace CMCS.CarTransport.BeltSampler.Frms
{
    partial class FrmTrainSampler_Warning
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
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAlarmReset2 = new DevComponents.DotNetBar.ButtonX();
            this.btnAlarmReset = new DevComponents.DotNetBar.ButtonX();
            this.lblG4WFC = new System.Windows.Forms.Label();
            this.lblG2WFC = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.panel1.SuspendLayout();
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
            this.panel1.Controls.Add(this.btnAlarmReset2);
            this.panel1.Controls.Add(this.btnAlarmReset);
            this.panel1.Controls.Add(this.lblG4WFC);
            this.panel1.Controls.Add(this.lblG2WFC);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1900, 1000);
            this.panel1.TabIndex = 2;
            // 
            // btnAlarmReset2
            // 
            this.btnAlarmReset2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAlarmReset2.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnAlarmReset2.Location = new System.Drawing.Point(1139, 844);
            this.btnAlarmReset2.Name = "btnAlarmReset2";
            this.btnAlarmReset2.Size = new System.Drawing.Size(182, 38);
            this.btnAlarmReset2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAlarmReset2.TabIndex = 46;
            this.btnAlarmReset2.Text = "封装机报警复位";
            this.btnAlarmReset2.Click += new System.EventHandler(this.btnAlarmReset2_Click);
            // 
            // btnAlarmReset
            // 
            this.btnAlarmReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAlarmReset.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnAlarmReset.Location = new System.Drawing.Point(987, 844);
            this.btnAlarmReset.Name = "btnAlarmReset";
            this.btnAlarmReset.Size = new System.Drawing.Size(122, 38);
            this.btnAlarmReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAlarmReset.TabIndex = 45;
            this.btnAlarmReset.Text = "报警复位";
            this.btnAlarmReset.Click += new System.EventHandler(this.btnAlarmReset_Click);
            // 
            // lblG4WFC
            // 
            this.lblG4WFC.AutoSize = true;
            this.lblG4WFC.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblG4WFC.Location = new System.Drawing.Point(205, 9);
            this.lblG4WFC.Name = "lblG4WFC";
            this.lblG4WFC.Size = new System.Drawing.Size(0, 28);
            this.lblG4WFC.TabIndex = 5;
            // 
            // lblG2WFC
            // 
            this.lblG2WFC.AutoSize = true;
            this.lblG2WFC.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblG2WFC.Location = new System.Drawing.Point(34, 9);
            this.lblG2WFC.Name = "lblG2WFC";
            this.lblG2WFC.Size = new System.Drawing.Size(0, 28);
            this.lblG2WFC.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(794, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "皮带采样机报警信息";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.superGridControl1);
            this.panel2.Location = new System.Drawing.Point(112, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1679, 685);
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
            gridColumn1.DataPropertyName = "tDate";
            gridColumn1.HeaderText = "报警日期";
            gridColumn1.Name = "";
            gridColumn1.Width = 150;
            gridColumn2.DataPropertyName = "tTime";
            gridColumn2.HeaderText = "报警时间";
            gridColumn2.Name = "";
            gridColumn2.Width = 150;
            gridColumn3.DataPropertyName = "Name";
            gridColumn3.HeaderText = "变量名";
            gridColumn3.Name = "";
            gridColumn3.Width = 300;
            gridColumn4.DataPropertyName = "Priority";
            gridColumn4.HeaderText = "优先级";
            gridColumn4.Name = "";
            gridColumn4.Width = 120;
            gridColumn5.DataPropertyName = "Type";
            gridColumn5.HeaderText = "事件类型";
            gridColumn5.Name = "";
            gridColumn5.Width = 150;
            gridColumn6.DataPropertyName = "User";
            gridColumn6.HeaderText = "操作员";
            gridColumn6.Name = "";
            gridColumn6.Width = 120;
            gridColumn7.DataPropertyName = "Remark";
            gridColumn7.HeaderText = "变量描述";
            gridColumn7.Name = "";
            gridColumn7.Width = 300;
            gridColumn8.DataPropertyName = "MachineName";
            gridColumn8.HeaderText = "机器名";
            gridColumn8.Name = "";
            gridColumn8.Width = 150;
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn6);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn7);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn8);
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            this.superGridControl1.Size = new System.Drawing.Size(1679, 685);
            this.superGridControl1.TabIndex = 2;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // FrmTrainSampler_Warning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1900, 1000);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonX2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTrainSampler_Warning";
            this.Text = "皮带采样机报警";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTrainSampler_Warning_FormClosing);
            this.Load += new System.EventHandler(this.FrmTrainSampler_Warning_Load);
            this.Shown += new System.EventHandler(this.FrmTrainSampler_Warning_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private System.Windows.Forms.Label lblG4WFC;
        private System.Windows.Forms.Label lblG2WFC;
        private DevComponents.DotNetBar.ButtonX btnAlarmReset;
        private DevComponents.DotNetBar.ButtonX btnAlarmReset2;
    }
}