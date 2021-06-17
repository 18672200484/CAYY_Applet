using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace CMCS.InterfaceData.Win.DumblyTasks
{
    public partial class FrmAutoMaker : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmAutoMaker";

        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();
        SqlServerDapperDber equDber = null;

        public FrmAutoMaker()
        {
            InitializeComponent();
        }

        private void FrmAutoMaker_Load(object sender, EventArgs e)
        {
            dtInputStart.Value = DateTime.Now.Date;
            dtInputEnd.Value = dtInputStart.Value.AddDays(1);
            dtInputStart1.Value = DateTime.Now.Date;
            dtInputEnd1.Value = dtInputStart1.Value.AddDays(1);
            dtInputStart2.Value = DateTime.Now.Date;
            dtInputEnd2.Value = dtInputStart2.Value.AddDays(1);
            dtInputStart3.Value = DateTime.Now.Date;
            dtInputEnd3.Value = dtInputStart3.Value.AddDays(1);

            this.Text = "全自动制样机接口业务";
            equDber = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#1全自动制样机接口连接字符串"));

            Bind_ZY_Record_Tb();
            Bind_tb_AlarmDataRecord();
            Bind_tb_ZY_OperateRecord();
            Bind_tb_DataRecord();
        }

        /// <summary>
        /// 窗体关闭后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAutoCupboard_NCGM_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 注意：必须取消任务
            //this.taskSimpleScheduler.Cancal();
        }

        /// <summary>
        /// 制样报警
        /// </summary>
        private void Bind_ZY_Record_Tb()
        {
            DataTable dt = equDber.ExecuteDataTable("select * from ZY_Record_Tb where [StartTime]>='" + dtInputStart.Value + "' and [StartTime]<='" + dtInputEnd.Value + "' order by [StartTime] desc");
            SGC_ZY_Record_Tb.PrimaryGrid.DataSource = dt;
        }

        /// <summary>
        /// 历史报警
        /// </summary>
        private void Bind_tb_AlarmDataRecord()
        {
            DataTable dt = equDber.ExecuteDataTable("select * from tb_AlarmDataRecord where [DateTime]>='" + dtInputStart1.Value + "' and [DateTime]<='" + dtInputEnd1.Value + "' order by [DateTime] desc");
            SGC_tb_AlarmDataRecord.PrimaryGrid.DataSource = dt;
        }

        /// <summary>
        /// 操作记录
        /// </summary>
        private void Bind_tb_ZY_OperateRecord()
        {
            DataTable dt = equDber.ExecuteDataTable("select * from tb_ZY_OperateRecord where [DateTime]>='" + dtInputStart2.Value + "' and [DateTime]<='" + dtInputEnd2.Value + "' order by [DateTime] desc");
            SGC_tb_ZY_OperateRecord.PrimaryGrid.DataSource = dt;
        }

        /// <summary>
        /// 运行记录
        /// </summary>
        private void Bind_tb_DataRecord()
        {
            DataTable dt = equDber.ExecuteDataTable("select * from tb_DataRecord where [Date]>='" + dtInputStart3.Value + "' and [Date]<='" + dtInputEnd3.Value + "' order by [Date] desc");
            SGC_tb_DataRecord.PrimaryGrid.DataSource = dt;
        }

        private void Bind_ZY_Cmd_Tb()
        {
            List<ZY_Cmd_Tb> list = equDber.TopEntities<ZY_Cmd_Tb>(100, " order by SendCommandTime desc");
            SGC_ZY_Cmd_Tb.PrimaryGrid.DataSource = list;
        }

        private void Bind_ZY_Interface_Tb()
        {
            List<ZY_Interface_Tb> list = equDber.TopEntities<ZY_Interface_Tb>(100, " order by SendCommandTime desc");
            SGC_ZY_Interface_Tb.PrimaryGrid.DataSource = list;
        }

        private void Bind_ZY_Status_Tb()
        {
            List<ZY_Status_Tb> list = equDber.Entities<ZY_Status_Tb>();
            SGC_ZY_Status_Tb.PrimaryGrid.DataSource = list;
        }

        private void Bind_ZY_State_Tb()
        {
            List<ZY_State_Tb> list = equDber.Entities<ZY_State_Tb>();
            SGC_ZY_State_Tb.PrimaryGrid.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Bind_ZY_Record_Tb();
        }

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            Bind_tb_AlarmDataRecord();
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            Bind_tb_ZY_OperateRecord();
        }

        private void btnSearch3_Click(object sender, EventArgs e)
        {
            Bind_tb_DataRecord();
        }

        #region DataGridView

        private void superGridControl_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        #endregion
    }
}
