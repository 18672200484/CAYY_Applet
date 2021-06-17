using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common.DAO;
using CMCS.WeighCheck.DAO;
using CMCS.WeighCheck.SampleWeigh.Frms.SampleWeigth;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;

namespace CMCS.WeighCheck.SampleWeigh.Frms
{
    public partial class FrmRLSampleSelect : MetroForm
    {
        #region Var

        /// <summary>
        /// 选中的实体
        /// </summary>
        public SampleInfo Output;

        CZYHandlerDAO cZYHandlerDAO = CZYHandlerDAO.GetInstance();

        List<SampleInfo> listSampleInfo = new List<SampleInfo>();

        /// <summary>
        /// 当前日期
        /// </summary>
        DateTime CurrentDay = DateTime.Now;

        #endregion

        public FrmRLSampleSelect()
        {
            InitializeComponent();
        }

        private void FrmRLSampleSelect_Load(object sender, EventArgs e)
        {
            dtInputStart.Value = dtInputEnd.Value = DateTime.Now;

            BindData();
        }

        #region 业务

        private void BindData()
        {
            listSampleInfo.Clear();

            try
            {

            DataTable dt = cZYHandlerDAO.GetRLSampleInfo(DateTime.Parse(dtInputStart.Text), DateTime.Parse(dtInputEnd.Text).AddDays(1));
            if (dt != null)
            {
                foreach (DataRow drSample in dt.Rows)
                {
                    listSampleInfo.Add(new SampleInfo()
                    {
                        Id = drSample["Id"].ToString(),
                        SampleCode = drSample["SampleCode"].ToString(),
                        SamplingDate = DateTime.Parse(drSample["SamplingDate"].ToString()),
                        SamplingType = drSample["SamplingType"].ToString(),
                        Crew = drSample["Crew"].ToString(),
                        Shifts = drSample["Shifts"].ToString(),
                        SelectType = "入炉"
                    });
                }
            }

            }
            catch (Exception)
            {

                throw;
            }
            superGridControl1.PrimaryGrid.DataSource = listSampleInfo.OrderByDescending(a => a.SamplingDate).ToList();
        }

        private void ShowDateTime()
        {
            dtInputStart.Value = dtInputEnd.Value = DateTime.Parse(CurrentDay.ToString("yyyy-MM-dd"));
        }

        #endregion

        #region 操作

        private void lvwInfactoryBatch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewEx lvw = sender as ListViewEx;
            if (lvw != null)
            {
                ListViewItem item = lvw.GetItemAt(e.X, e.Y);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dtInputStart.Text))
                return;
            if (string.IsNullOrEmpty(dtInputEnd.Text))
                return;

            BindData();
        }

        private void btnPreDay_Click(object sender, EventArgs e)
        {
            CurrentDay = CurrentDay.AddDays(-1);
            ShowDateTime();
            BindData();
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            CurrentDay = CurrentDay.AddDays(1);
            ShowDateTime();
            BindData();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            CurrentDay = DateTime.Now;
            ShowDateTime();
            BindData();
        }

        #endregion

        #region superGridControl

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑模式
            e.Cancel = true;
        }

        private void superGridControl1_CellDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs e)
        {
            GridRow gridRow = (sender as SuperGridControl).PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;

            SampleInfo entity = (gridRow.DataItem as SampleInfo);
            if (entity == null) return;

            this.Output = (gridRow.DataItem as SampleInfo);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion
    }

    
}
