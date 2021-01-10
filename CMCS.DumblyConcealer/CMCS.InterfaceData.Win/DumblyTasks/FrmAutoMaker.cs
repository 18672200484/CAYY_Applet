using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// 最后一次心跳值
        /// </summary>
        bool lastHeartbeat;

        public FrmAutoMaker()
        {
            InitializeComponent();
        }

        private void FrmAutoMaker_Load(object sender, EventArgs e)
        {
            this.Text = "全自动制样机接口业务";
            equDber = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#1全自动制样机接口连接字符串"));
            Bind_ZY_Cmd_Tb();
            Bind_ZY_Interface_Tb();
            Bind_tb_AlarmDataRecord();
            Bind_ZY_Status_Tb();
            Bind_ZY_State_Tb();
            Bind_ZY_Record_Tb();
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
        

        private void Bind_tb_AlarmDataRecord()
        {
            List<ZY_Error_Tb> list = equDber.TopEntities<ZY_Error_Tb>(100, " order by DateTime desc");
            SGC_tb_AlarmDataRecord.PrimaryGrid.DataSource = list;
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

        private void Bind_ZY_Record_Tb()
        {
            List<ZY_Record_Tb> list = equDber.TopEntities<ZY_Record_Tb>(100, " order by StartTime desc");
            SGC_ZY_Record_Tb.PrimaryGrid.DataSource = list;
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
