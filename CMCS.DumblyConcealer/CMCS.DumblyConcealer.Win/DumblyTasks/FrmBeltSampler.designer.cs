namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    partial class FrmBeltSampler
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
			this.rtxtOutput = new System.Windows.Forms.RichTextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// rtxtOutput
			// 
			this.rtxtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.rtxtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtxtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtOutput.ForeColor = System.Drawing.Color.White;
			this.rtxtOutput.Location = new System.Drawing.Point(3, 3);
			this.rtxtOutput.Name = "rtxtOutput";
			this.rtxtOutput.Size = new System.Drawing.Size(558, 326);
			this.rtxtOutput.TabIndex = 0;
			this.rtxtOutput.Text = "";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.rtxtOutput, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(564, 332);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// FrmBeltSampler
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(564, 332);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "FrmBeltSampler";
			this.Text = "FrmTask1";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCarSampler_CSKY_FormClosed);
			this.Load += new System.EventHandler(this.FrmCarSampler_CSKY_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.RichTextBox rtxtOutput;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}