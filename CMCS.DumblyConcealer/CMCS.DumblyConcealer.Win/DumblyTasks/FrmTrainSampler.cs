﻿using System;
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
using CMCS.DumblyConcealer.Tasks.TrainJxSampler;
using CMCS.DumblyConcealer.Win.Core;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
	public partial class FrmTrainSampler : TaskForm
	{
		RTxtOutputer rTxtOutputer;
		RTxtOutputer rTxtOutResultputer;
		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

		public FrmTrainSampler()
		{
			InitializeComponent();
		}

		private void FrmCarSampler_CSKY_Load(object sender, EventArgs e)
		{
			this.Text = "火车机械采样机接口业务";

			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

			ExecuteAllTask();

		}

		/// <summary>
		/// 执行所有任务
		/// </summary>
		void ExecuteAllTask()
		{
			#region #1火车机械采样机

			EquTrainJXSamplerDAO carJXSamplerDAO1 = new EquTrainJXSamplerDAO(GlobalVars.MachineCode_HCJXCYJ_1, new DapperDber.Dbs.SqlServerDb.SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#1火车机械采样机接口连接字符串")));

			taskSimpleScheduler.StartNewTask("#1火车机械采样机-快速同步", () =>
			{
				carJXSamplerDAO1.SyncBarrel(this.rTxtOutputer.Output);
				carJXSamplerDAO1.SyncSampleCmd(this.rTxtOutputer.Output);
				carJXSamplerDAO1.SyncSamplePlan(this.rTxtOutputer.Output);
				carJXSamplerDAO1.SyncSamplePlanDetail(this.rTxtOutputer.Output);
				//carJXSamplerDAO1.SyncUnloadResult(this.rTxtOutputer.Output);
				carJXSamplerDAO1.SyncQCJXCYJError(this.rTxtOutputer.Output);
				carJXSamplerDAO1.SyncSignal(this.rTxtOutputer.Output);

			}, 2000, OutputError);

			//this.taskSimpleScheduler.StartNewTask("#1汽车机械采样机-上位机心跳", () =>
			//{
			//    carJXSamplerDAO1.SyncHeartbeatSignal();
			//}, 30000, OutputError);

			#endregion

			#region #2火车机械采样机

			EquTrainJXSamplerDAO carJXSamplerDAO2 = new EquTrainJXSamplerDAO(GlobalVars.MachineCode_HCJXCYJ_2, new DapperDber.Dbs.SqlServerDb.SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#2火车机械采样机接口连接字符串")));

			taskSimpleScheduler.StartNewTask("#2火车机械采样机-快速同步", () =>
			{
				carJXSamplerDAO2.SyncBarrel(this.rTxtOutputer.Output);
				carJXSamplerDAO2.SyncSampleCmd(this.rTxtOutputer.Output);
				carJXSamplerDAO2.SyncSamplePlan(this.rTxtOutputer.Output);
				carJXSamplerDAO2.SyncSamplePlanDetail(this.rTxtOutputer.Output);
				//carJXSamplerDAO2.SyncUnloadResult(this.rTxtOutputer.Output);
				carJXSamplerDAO2.SyncQCJXCYJError(this.rTxtOutputer.Output);
				carJXSamplerDAO2.SyncSignal(this.rTxtOutputer.Output);

			}, 2000, OutputError);

			//this.taskSimpleScheduler.StartNewTask("#2汽车机械采样机-上位机心跳", () =>
			//{
			//    carJXSamplerDAO2.SyncHeartbeatSignal();
			//}, 30000, OutputError);

			#endregion

			#region #3火车机械采样机

			EquTrainJXSamplerDAO carJXSamplerDAO3 = new EquTrainJXSamplerDAO(GlobalVars.MachineCode_HCJXCYJ_3, new DapperDber.Dbs.SqlServerDb.SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#3火车机械采样机接口连接字符串")));

			taskSimpleScheduler.StartNewTask("#3火车机械采样机-快速同步", () =>
			{
				carJXSamplerDAO3.SyncBarrel(this.rTxtOutputer.Output);
				carJXSamplerDAO3.SyncSampleCmd(this.rTxtOutputer.Output);
				carJXSamplerDAO3.SyncSamplePlan(this.rTxtOutputer.Output);
				carJXSamplerDAO3.SyncSamplePlanDetail(this.rTxtOutputer.Output);
				//carJXSamplerDAO3.SyncUnloadResult(this.rTxtOutputer.Output);
				carJXSamplerDAO3.SyncQCJXCYJError(this.rTxtOutputer.Output);
				carJXSamplerDAO3.SyncSignal(this.rTxtOutputer.Output);

			}, 2000, OutputError);

			//this.taskSimpleScheduler.StartNewTask("#3汽车机械采样机-上位机心跳", () =>
			//{
			//    carJXSamplerDAO1.SyncHeartbeatSignal();
			//}, 30000, OutputError);

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
