using CMCS.Common.DAO;
using CMCS.Common.Entities.iEAA;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmBatchMachineBarrel_Select : DevComponents.DotNetBar.Metro.MetroForm
    {
        CommonDAO commonDAO = CommonDAO.GetInstance();
        /// <summary>
        /// 选中的实体
        /// </summary>
        public BatchMachineBarrel_Select Output;

        /// <summary>
        /// 条件语句
        /// </summary>
        string sqlWhere;

        public FrmBatchMachineBarrel_Select(string sqlWhere)
        {
            InitializeComponent();

            this.sqlWhere = sqlWhere;

            Search(string.Empty);
        }

        private void FrmBatchMachineBarrel_Select_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Output = null;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (superGridControl1.PrimaryGrid.Rows.Count > 0) superGridControl1.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                Return();
            }
            else
            {
                Search(txtInput.Text.Trim());
            }
        }

        void Search(string input)
        {
            List<BatchMachineBarrel_Select> list = new List<BatchMachineBarrel_Select>();
            string sql = string.Format(@"select a.sampleid from inftbInfBatchMachineBarrel a where a.barrelstatus=1 and a.datastatus=1 group by a.sampleid");
            DataTable dt = commonDAO.SelfDber.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    BatchMachineBarrel_Select entity = new BatchMachineBarrel_Select();
                    entity.SampleID = dt.Rows[i]["sampleid"].ToString();
                    list.Add(entity);
                }
            }
            superGridControl1.PrimaryGrid.DataSource = list;
        }

        void Return()
        {
            GridRow gridRow = superGridControl1.PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;

            this.Output = (gridRow.DataItem as BatchMachineBarrel_Select);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑模式
            e.Cancel = true;
        }

        /// <summary>
        /// 设置行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        private void superGridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Return();
        }

        private void superGridControl1_CellDoubleClick(object sender, GridCellDoubleClickEventArgs e)
        {
            Return();
        }

       
    }
    public class BatchMachineBarrel_Select
    {
        private String _SampleID;
        /// <summary>
        /// 采样编码
        /// </summary>
        public String SampleID
        {
            get { return _SampleID; }
            set { _SampleID = value; }
        }

    }


}

