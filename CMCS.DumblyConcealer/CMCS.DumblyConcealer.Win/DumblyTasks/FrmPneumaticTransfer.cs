using System;
using System.Windows.Forms;
using CMCS.DumblyConcealer.Win.Core;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.AutoCupboard;
using CMCS.Common;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.Common.DAO;
using CMCS.DumblyConcealer.Tasks.PneumaticTransfer;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
	public partial class FrmPneumaticTransfer : TaskForm
	{
		RTxtOutputer rTxtOutputer;
		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

		/// <summary>
		/// 最后一次心跳值
		/// </summary>
		bool lastHeartbeat;

		public FrmPneumaticTransfer()
		{
			InitializeComponent();
		}

		private void FrmAutoCupboard_NCGM_Load(object sender, EventArgs e)
		{
			this.Text = "气动传输接口业务";

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
				EquPneumaticTransferOPC.GetInstance().SyncOPCTags(this.rTxtOutputer.Output);
			}, 0, OutputError);

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
