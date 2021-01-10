using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CMCS.InterfaceData.Win.DumblyTasks
{
    public partial class FrmAutoCupboard : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmAutoCupboard";

        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();
        SqlServerDapperDber equDber = null;

        /// <summary>
        /// 最后一次心跳值
        /// </summary>
        bool lastHeartbeat;

        public FrmAutoCupboard()
        {
            InitializeComponent();
        }

        private void FrmAutoCupboard_NCGM_Load(object sender, EventArgs e)
        {
            this.Text = "智能存样柜接口业务";
            equDber = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("智能存样柜接口连接字符串"));
            Bind_Tb_AlarmDataRecord();
            Bind_tb_bolt();
            Bind_tb_preaction();
            Bind_tb_status();
            Bind_tb_state();
            Bind_tb_action();
        }

        private void Bind_Tb_AlarmDataRecord()
        {
            List<Tb_AlarmDataRecord> list = equDber.TopEntities<Tb_AlarmDataRecord>(100," order by DateTime desc");
            SGC_tb_AlarmDataRecord.PrimaryGrid.DataSource = list;
        }

        private void Bind_tb_bolt()
        {
            List<Tb_Bolt> list = equDber.Entities<Tb_Bolt>();
            SGC_tb_bolt.PrimaryGrid.DataSource = list;
        }
        

        private void Bind_tb_preaction()
        {
            List<Tb_PreAction> list = equDber.TopEntities<Tb_PreAction>(100, " order by I_Time desc");
            SGC_tb_preaction.PrimaryGrid.DataSource = list;
        }

        private void Bind_tb_status()
        {
            List<Tb_Status> list = equDber.Entities<Tb_Status>();
            SGC_tb_status.PrimaryGrid.DataSource = list;
        }

        private void Bind_tb_state()
        {
            List<Tb_State> list = equDber.Entities<Tb_State>();
            SGC_tb_state.PrimaryGrid.DataSource = list;
        }

        private void Bind_tb_action()
        {
            List<Tb_Action> list = equDber.TopEntities<Tb_Action>(100, " order by Operate_Time desc");
            SGC_tb_action.PrimaryGrid.DataSource = list;
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
