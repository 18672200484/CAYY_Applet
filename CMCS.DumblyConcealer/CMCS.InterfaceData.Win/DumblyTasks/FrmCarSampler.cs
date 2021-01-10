using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;
using CMCS.DumblyConcealer.Tasks.CarJXSampler.Entities;
using CMCS.DumblyConcealer.Tasks.TrainJxSampler.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CMCS.InterfaceData.Win.DumblyTasks
{
    public partial class FrmCarSampler : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmCarSampler";

        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();
        SqlServerDapperDber equDber = null;

        /// <summary>
        /// 最后一次心跳值
        /// </summary>
        bool lastHeartbeat;

        public FrmCarSampler()
        {
            InitializeComponent();
        }

        private void FrmCarSampler_Load(object sender, EventArgs e)
        {
            this.Text = "全自动制样机接口业务";
            equDber = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("汽车机械采样机接口连接字符串"));
            Bind_SAMPLE_INTERFACE_DATA();
        }

        private void Bind_SAMPLE_INTERFACE_DATA()
        {
            List<Interface_Data> list = equDber.Entities<Interface_Data>();
            SGC_SAMPLE_INTERFACE_DATA.PrimaryGrid.DataSource = list;
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
