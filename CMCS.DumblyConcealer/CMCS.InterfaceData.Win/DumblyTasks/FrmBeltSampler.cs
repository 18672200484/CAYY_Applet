using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;
using CMCS.DumblyConcealer.Tasks.BeltSampler.Entities;
using System;
using System.Collections.Generic;
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
            this.Text = "皮带采样机接口业务";
            equDber = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("皮带采样机接口连接字符串"));
            Bind_KY_CYJ_P_STATE();
            Bind_KY_CYJ_P_OUTRUN();
            Bind_KY_CYJ_P_TurnOver();
            Bind_KY_CYJ_P_BARREL();
           
        }

        private void Bind_KY_CYJ_P_STATE()
        {
            List<KY_CYJ_P_STATE> list = equDber.Entities<KY_CYJ_P_STATE>();
            SGC_KY_CYJ_P_STATE.PrimaryGrid.DataSource = list;
        }

        private void Bind_KY_CYJ_P_OUTRUN()
        {
            List<KY_CYJ_P_OUTRUN> list = equDber.TopEntities<KY_CYJ_P_OUTRUN>(100, " order by Send_Time desc");
            SGC_KY_CYJ_P_OUTRUN.PrimaryGrid.DataSource = list;
        }
        

        private void Bind_KY_CYJ_P_TurnOver()
        {
            List<KY_CYJ_P_TurnOver> list = equDber.TopEntities<KY_CYJ_P_TurnOver>(100, " order by Send_Time desc");
            SGC_KY_CYJ_P_TurnOver.PrimaryGrid.DataSource = list;
        }

        private void Bind_KY_CYJ_P_BARREL()
        {
            List<KY_CYJ_P_BARREL> list = equDber.Entities<KY_CYJ_P_BARREL>();
            SGC_KY_CYJ_P_BARREL.PrimaryGrid.DataSource = list;
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

    }
}
