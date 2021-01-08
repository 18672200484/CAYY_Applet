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
using CMCS.DumblyConcealer.Tasks.BeltSampler;
using CMCS.DumblyConcealer.Tasks.CarJXSampler;
using CMCS.DumblyConcealer.Win.Core;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
	public partial class FrmBeltSampler : TaskForm
	{
		RTxtOutputer rTxtOutputer;
		RTxtOutputer rTxtOutResultputer;
		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

		public FrmBeltSampler()
		{
			InitializeComponent();
		}

		private void FrmCarSampler_CSKY_Load(object sender, EventArgs e)
		{
			this.Text = "皮带采样机接口业务";

			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

			ExecuteAllTask();
		}

		/// <summary>
		/// 执行所有任务
		/// </summary>
		void ExecuteAllTask()
		{
			#region #1皮带采样机

			EquBeltSamplerDAO beltSamplerDAO1 = new EquBeltSamplerDAO(GlobalVars.MachineCode_PDCYJ_1);

			taskSimpleScheduler.StartNewTask("#1皮带采样机-快速同步", () =>
			{
				beltSamplerDAO1.SyncSignal(this.rTxtOutputer.Output);
				beltSamplerDAO1.SyncSamplePlan(this.rTxtOutputer.Output);
				beltSamplerDAO1.SyncBarrel(this.rTxtOutputer.Output);
				beltSamplerDAO1.SyncTurn(this.rTxtOutputer.Output);
			}, 2000, OutputError);

			#endregion

			#region #2皮带采样机

			EquBeltSamplerDAO beltSamplerDAO2 = new EquBeltSamplerDAO(GlobalVars.MachineCode_PDCYJ_2);

			taskSimpleScheduler.StartNewTask("#2皮带采样机-快速同步", () =>
			{
				beltSamplerDAO2.SyncSignal(this.rTxtOutputer.Output);
				beltSamplerDAO2.SyncSamplePlan(this.rTxtOutputer.Output);
				beltSamplerDAO2.SyncBarrel(this.rTxtOutputer.Output);
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
		/// 输出异常信息（结果）
		/// </summary>
		/// <param name="text"></param>
		/// <param name="ex"></param>
		void OutputResultError(string text, Exception ex)
		{
			this.rTxtOutResultputer.Output(text + Environment.NewLine + ex.Message, eOutputType.Error);

			Log4Neter.Error(text, ex);
		}

		/// <summary>
		/// 窗体关闭后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmCarSampler_CSKY_FormClosed(object sender, FormClosedEventArgs e)
		{
			// 注意：必须取消任务
			this.taskSimpleScheduler.Cancal();
		}
	}
}
