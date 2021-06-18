using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.AutoMaker;
using CMCS.DumblyConcealer.Tasks.BeltSampler;
using CMCS.DumblyConcealer.Win.Core;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
	public partial class FrmBatchMachine : TaskForm
	{
		RTxtOutputer rTxtOutputer;

		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

		public FrmBatchMachine()
		{
			InitializeComponent();
		}

		private void FrmAutoMaker_NCGM_Load(object sender, EventArgs e)
		{
			this.Text = "合样归批机接口业务";

			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

			ExecuteAllTask();

		}

		/// <summary>
		/// 执行所有任务
		/// </summary>
		void ExecuteAllTask()
		{
			#region #1合样归批机

			EquBatchMachineDAO batchMachineDAO1 = new EquBatchMachineDAO(GlobalVars.MachineCode_HYGPJ_1, new DapperDber.Dbs.SqlServerDb.SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("合样归批连接字符串")));
			taskSimpleScheduler.StartNewTask("#1合样归批机-快速同步", () =>
			{
				batchMachineDAO1.SyncCmd(this.rTxtOutputer.Output);
				batchMachineDAO1.SyncError(this.rTxtOutputer.Output);
				batchMachineDAO1.SyncBarrel(this.rTxtOutputer.Output);

			}, 2000, OutputError);

			taskSimpleScheduler.StartNewTask("#1合样归批机-OPC同步", () =>
			{
				EquBatchMachineOPC.GetInstance().SyncOPCTags(this.rTxtOutputer.Output);
			}, 0, OutputError);

			taskSimpleScheduler.StartNewTask("#1合样归批机执行命令", () =>
			{
				EquBatchMachineOPC.GetInstance().RunCmd(this.rTxtOutputer.Output);
			}, 500, OutputError);

			taskSimpleScheduler.StartNewTask("送样小车-OPC同步", () =>
			{
				EquSYCarOPC.GetInstance().SyncOPCTags(this.rTxtOutputer.Output);
			}, 0, OutputError);

			taskSimpleScheduler.StartNewTask("送样小车执行命令", () =>
			{
				EquSYCarOPC.GetInstance().RunCmd(this.rTxtOutputer.Output);
			}, 500, OutputError);
			#endregion
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
		private void FrmAutoMaker_NCGM_FormClosed(object sender, FormClosedEventArgs e)
		{
			// 注意：必须取消任务
			this.taskSimpleScheduler.Cancal();
		}
	}
}
