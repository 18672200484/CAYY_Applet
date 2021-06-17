using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;
using CMCS.DumblyConcealer.Tasks.BeltSampler.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace CMCS.InterfaceData.Win.DumblyTasks
{
    public partial class FrmBeltSampler : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmBeltSampler";

        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();
        SqlServerDapperDber equDber = null;

        /// <summary>
        /// 最后一次心跳值
        /// </summary>
        bool lastHeartbeat;

        public FrmBeltSampler()
        {
            InitializeComponent();
        }

        private void FrmBeltSampler_Load(object sender, EventArgs e)
        {
            dtInputStart.Value = DateTime.Now.Date;
            dtInputEnd.Value = dtInputStart.Value.AddDays(1);
            dtInputStart1.Value = DateTime.Now.Date;
            dtInputEnd1.Value = dtInputStart1.Value.AddDays(1);
            dtInputStart2.Value = DateTime.Now.Date;
            dtInputEnd2.Value = dtInputStart2.Value.AddDays(1);

            this.Text = "皮带采样机接口业务";
            equDber = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("皮带采样机接口连接字符串"));

            Bind_KY_CYJ_P_RECORD();
            Bind_KY_CYJ_P_BARREL();
            Bind_KY_CYJ_P_Alarm();
        }

        /// <summary>
        /// 采样记录
        /// </summary>
        private void Bind_KY_CYJ_P_RECORD()
        {
            DataTable dt = equDber.ExecuteDataTable("select * from KY_CYJ_P_RECORD where [BEGIN_DATE]>='" + dtInputStart.Value + "' and [BEGIN_DATE]<='" + dtInputEnd.Value + "' order by [BEGIN_DATE] desc");
            SGC_KY_CYJ_P_RECORD.PrimaryGrid.DataSource = dt;
        }

        /// <summary>
        /// 装桶记录
        /// </summary>
        private void Bind_KY_CYJ_P_BARREL()
        {
            DataTable dt = equDber.ExecuteDataTable("select * from KY_CYJ_P_BARREL where [EDITDATE]>='" + dtInputStart1.Value + "' and [EDITDATE]<='" + dtInputEnd1.Value + "' order by [EDITDATE] desc");
            SGC_KY_CYJ_P_BARREL.PrimaryGrid.DataSource = dt;
        }

        /// <summary>
        /// 报警信息
        /// </summary>
        private void Bind_KY_CYJ_P_Alarm()
        {
            DataTable dt = equDber.ExecuteDataTable("select * from KY_CYJ_P_Alarm where [AlarmDateTime]>='" + dtInputStart2.Value + "' and [AlarmDateTime]<='" + dtInputEnd2.Value + "' order by [AlarmDateTime] desc");
            SGC_KY_CYJ_P_Alarm.PrimaryGrid.DataSource = dt;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Bind_KY_CYJ_P_RECORD();
        }

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            Bind_KY_CYJ_P_BARREL();
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            Bind_KY_CYJ_P_Alarm();
        }
    }
}
