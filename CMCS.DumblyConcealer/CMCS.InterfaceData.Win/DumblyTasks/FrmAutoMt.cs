using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;
using CMCS.DumblyConcealer.Tasks.AutoMt.Entities;
using CMCS.DumblyConcealer.Tasks.BeltSampler.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CMCS.InterfaceData.Win.DumblyTasks
{
    public partial class FrmAutoMt : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmAutoMt";

        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();
        SqlServerDapperDber equDber = null;

        /// <summary>
        /// 最后一次心跳值
        /// </summary>
        bool lastHeartbeat;

        public FrmAutoMt()
        {
            InitializeComponent();
        }

        private void FrmAutoMt_Load(object sender, EventArgs e)
        {
            this.Text = "在线全水接口业务";
            equDber = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#1在线全水分析接口连接字符串"));
            Bind_tb_testresult_6550();
            Bind_YQ_Status();
            Bind_TB_YQ_ERRORCODE();
           
        }

        private void Bind_tb_testresult_6550()
        {
            List<Tb_TestResult> list = equDber.TopEntities<Tb_TestResult>(100, " order by StartingTime desc"); 
            SGC_tb_testresult_6550.PrimaryGrid.DataSource = list;
        }

        private void Bind_YQ_Status()
        {
            List<YQ_Status> list = equDber.TopEntities<YQ_Status>(100, " order by St_Time desc"); 
            SGC_YQ_Status.PrimaryGrid.DataSource = list;
        }
        

        private void Bind_TB_YQ_ERRORCODE()
        {
            List<TB_YQ_ERRORCODE> list = equDber.Entities<TB_YQ_ERRORCODE>();
            SGC_TB_YQ_ERRORCODE.PrimaryGrid.DataSource = list;
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
