using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;
using CMCS.DumblyConcealer.Tasks.TrainJxSampler.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CMCS.InterfaceData.Win.DumblyTasks
{
    public partial class FrmTrainSampler : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmTrainSampler";

        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();
        SqlServerDapperDber equDber = null;

        /// <summary>
        /// 最后一次心跳值
        /// </summary>
        bool lastHeartbeat;

        public FrmTrainSampler()
        {
            InitializeComponent();
        }

        private void FrmTrainSampler_Load(object sender, EventArgs e)
        {
            this.Text = "全自动制样机接口业务";
            equDber = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#1火车机械采样机接口连接字符串"));
            Bind_EquTbHCQSCYJBarrel();
            Bind_EquTbHCQSCYJCmd();
            Bind_EquTbHCQSCYJPlan();
            Bind_EquTbHCQSCYJPlanDetail();
            Bind_EquTbHCQSCYJUnloadResult();
            Bind_EquTbHCQSCYJError();
            Bind_EquTbHCQSCYJSignal();
        }

        private void Bind_EquTbHCQSCYJBarrel()
        {
            List<EquHCQSCYJBarrel> list = equDber.Entities<EquHCQSCYJBarrel>();
            SGC_EquTbHCQSCYJBarrel.PrimaryGrid.DataSource = list;
        }

        private void Bind_EquTbHCQSCYJCmd()
        {
            List<EquHCQSCYJSampleCmd> list = equDber.TopEntities<EquHCQSCYJSampleCmd>(100, " order by CreateDate desc");
            SGC_EquTbHCQSCYJCmd.PrimaryGrid.DataSource = list;
        }
        

        private void Bind_EquTbHCQSCYJPlan()
        {
            List<EquHCQSCYJPlan> list = equDber.TopEntities<EquHCQSCYJPlan>(100, " order by CreateDate desc");
            SGC_EquTbHCQSCYJPlan.PrimaryGrid.DataSource = list;
        }

        private void Bind_EquTbHCQSCYJPlanDetail()
        {
            List<EquHCQSCYJPlanDetail> list = equDber.TopEntities<EquHCQSCYJPlanDetail>(100, " order by CreateDate desc");
            SGC_EquTbHCQSCYJPlanDetail.PrimaryGrid.DataSource = list;
        }

        private void Bind_EquTbHCQSCYJUnloadResult()
        {
            List<EquHCQSCYJUnloadResult> list = equDber.TopEntities<EquHCQSCYJUnloadResult>(100, " order by CreateDate desc");
            SGC_EquTbHCQSCYJUnloadResult.PrimaryGrid.DataSource = list;
        }

        private void Bind_EquTbHCQSCYJError()
        {
            List<EquHCQSCYJError> list = equDber.TopEntities<EquHCQSCYJError>(100, " order by CreateDate desc");
            SGC_EquTbHCQSCYJError.PrimaryGrid.DataSource = list;
        }

        private void Bind_EquTbHCQSCYJSignal()
        {
            List<EquHCQSCYJSignal> list = equDber.TopEntities<EquHCQSCYJSignal>(100, " order by CreateDate desc");
            SGC_EquTbHCQSCYJSignal.PrimaryGrid.DataSource = list;
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
