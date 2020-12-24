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
using CMCS.DumblyConcealer.Tasks.AutoMt;
using CMCS.DumblyConcealer.Win.Core;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
	public partial class FrmAutoMt : TaskForm
	{
		RTxtOutputer rTxtOutputer;

		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

		public FrmAutoMt()
		{
			InitializeComponent();
		}

		private void FrmAutoMaker_NCGM_Load(object sender, EventArgs e)
		{
			this.Text = "在线全水接口业务";

			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

			ExecuteAllTask();
		}

		/// <summary>
		/// 执行所有任务
		/// </summary>
		void ExecuteAllTask()
		{
			#region #1全自动制样机

			EquAutoMtDAO autoMakerDAO1 = new EquAutoMtDAO(GlobalVars.MachineCode_ZXQS_1, new DapperDber.Dbs.SqlServerDb.SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#1在线全水分析接口连接字符串")));
			taskSimpleScheduler.StartNewTask("在线全水测试-快速同步", () =>
			{
				autoMakerDAO1.SyncMtResult(this.rTxtOutputer.Output);
				autoMakerDAO1.SyncError(this.rTxtOutputer.Output);
				autoMakerDAO1.SyncSignal(this.rTxtOutputer.Output);
			}, 2000, OutputError);
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
