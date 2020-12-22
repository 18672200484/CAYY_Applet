using System;
using System.Windows.Forms;
using CMCS.DumblyConcealer.Win.Core;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.AutoCupboard;
using CMCS.Common;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.Common.DAO;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    public partial class FrmAutoCupboard : TaskForm
    {
        RTxtOutputer rTxtOutputer;
        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

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

            this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

            ExecuteAllTask();
        }

        /// <summary>
        /// 执行所有任务
        /// </summary>
        void ExecuteAllTask()
        {
            this.taskSimpleScheduler.StartNewTask("同步上位机运行状态", () =>
            {
                EquAutoCupboardDAO autoCupboard_DAO = new EquAutoCupboardDAO(GlobalVars.MachineCode_CYG1, new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("智能存样柜接口连接字符串")));
                //if (CommonDAO.GetInstance().TestPing(autoCupboard_DAO.EquDber.Connection.DataSource))
                //{
                    //#1存样柜
                    autoCupboard_DAO.SyncCYGToBSInfo(this.rTxtOutputer.Output);
                    //autoCupboard_DAO.SyncCYGHistoryToBSInfo(this.rTxtOutputer.Output);
                    //autoCupboard_DAO.SyncClearCmdToCS(this.rTxtOutputer.Output);
                    //autoCupboard_DAO.SyncTakeCmdToCS(this.rTxtOutputer.Output);
                    
                    //autoCupboard_DAO.SyncCYGError(this.rTxtOutputer.Output);
                    ///autoCupboard_DAO.SyncCYGInfo(this.rTxtOutputer.Output);
                    //autoCupboard_DAO.SyncCYGCmd(this.rTxtOutputer.Output);
                    //autoCupboard_DAO.SyncCYGResult(this.rTxtOutputer.Output);
                    //autoCupboard_DAO.SyncSignalDatal(this.rTxtOutputer.Output);
                    //autoCupboard_DAO.SyncCYGRecord(this.rTxtOutputer.Output);
                //}
                //else
                //    rTxtOutputer.Output(autoCupboard_DAO.MachineCode + "网络不通", eOutputType.Warn);
            }, 5000, OutputError);

            //this.taskSimpleScheduler.StartNewTask("同步上位机2运行状态", () =>
            //{
            //    EquAutoCupboardDAO autoCupboard_DAO2 = new EquAutoCupboardDAO(GlobalVars.MachineCode_CYG2, new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("智能存样柜2接口连接字符串")));
            //    if (CommonDAO.GetInstance().TestPing(autoCupboard_DAO2.EquDber.Connection.DataSource))
            //    { //#2存样柜
            //        autoCupboard_DAO2.SyncCYGError(this.rTxtOutputer.Output);
            //        autoCupboard_DAO2.SyncCYGInfo(this.rTxtOutputer.Output);
            //        autoCupboard_DAO2.SyncCYGCmd(this.rTxtOutputer.Output);
            //        autoCupboard_DAO2.SyncCYGResult(this.rTxtOutputer.Output);
            //        autoCupboard_DAO2.SyncSignalDatal(this.rTxtOutputer.Output);
            //        autoCupboard_DAO2.SyncCYGRecord(this.rTxtOutputer.Output);
            //    }
            //    else
            //        rTxtOutputer.Output(autoCupboard_DAO2.MachineCode + "网络不通", eOutputType.Warn);
            //}, 5000, OutputError);
        }

        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ex"></param>
        void OutputError(string text, Exception ex)
        {
            this.rTxtOutputer.Output(text + Environment.NewLine + ex.Message, eOutputType.Error);

            Log4Neter.Error(text, ex);
        }
        /// <summary>
        /// 窗体关闭后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAutoCupboard_NCGM_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 注意：必须取消任务
            this.taskSimpleScheduler.Cancal();
        }

    }
}
