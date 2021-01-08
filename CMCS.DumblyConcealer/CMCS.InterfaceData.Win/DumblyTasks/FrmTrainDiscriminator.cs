using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DumblyConcealer.Win.Core;

using CMCS.DumblyConcealer.Tasks.BeltSampler;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.WeightBridger;
using CMCS.DumblyConcealer.Tasks.TrainDiscriminator;
using System.Net.Sockets;
using CMCS.Common.DAO;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
	public partial class FrmTrainDiscriminator : TaskForm
	{
		RTxtOutputer rTxtOutputer;
		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();


		public FrmTrainDiscriminator()
		{
			InitializeComponent();
		}

		private void FrmWeightBridger_Load(object sender, EventArgs e)
		{
			this.Text = "车号识别报文TCP/IP同步业务";

			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

			ExecuteAllTask();
		}
		/// <summary>
		/// 执行所有任务
		/// </summary>
		void ExecuteAllTask()
		{
			TrainDiscriminatorDAO trainWeight_DAO = TrainDiscriminatorDAO.GetInstance();
			TrainDiscriminatorDBW trainWeight_DBW = TrainDiscriminatorDBW.GetInstance();
			taskSimpleScheduler.StartNewTask("同步车号识别报文数据", () =>
			{
				trainWeight_DBW.SyncDBWInfo(this.rTxtOutputer.Output, CommonDAO.GetInstance().GetAppletConfigString("车号识别1文件储存位置"), "1");
				trainWeight_DBW.SyncDBWInfo(this.rTxtOutputer.Output, CommonDAO.GetInstance().GetAppletConfigString("车号识别2文件储存位置"), "2");
				trainWeight_DBW.SyncDBWInfo(this.rTxtOutputer.Output, CommonDAO.GetInstance().GetAppletConfigString("车号识别3文件储存位置"), "3");
				trainWeight_DBW.SyncDBWInfo(this.rTxtOutputer.Output, CommonDAO.GetInstance().GetAppletConfigString("车号识别4文件储存位置"), "4");
				trainWeight_DBW.SyncDBWInfo(this.rTxtOutputer.Output, CommonDAO.GetInstance().GetAppletConfigString("车号识别5文件储存位置"), "5");
			}, 10000, OutputError);

			taskSimpleScheduler.StartNewTask("处理车号识别数据1", () =>
			{
				trainWeight_DAO.CarSpotsHandle(this.rTxtOutputer.Output, 1);
			}, 20000, OutputError);

			taskSimpleScheduler.StartNewTask("处理车号识别数据2", () =>
			{
				trainWeight_DAO.CarSpotsHandle(this.rTxtOutputer.Output, 2);
			}, 20000, OutputError);

			taskSimpleScheduler.StartNewTask("处理车号识别数据3", () =>
			{
				trainWeight_DAO.CarSpotsHandle(this.rTxtOutputer.Output, 3);
			}, 20000, OutputError);

			taskSimpleScheduler.StartNewTask("处理车号识别数据4", () =>
			{
				trainWeight_DAO.CarSpotsHandle(this.rTxtOutputer.Output, 4);
			}, 2000, OutputError);

			taskSimpleScheduler.StartNewTask("处理车号识别数据5", () =>
			{
				trainWeight_DAO.CarSpotsHandle(this.rTxtOutputer.Output, 5);
			}, 2000, OutputError);
		}

		/// <summary>
		/// 输出异常信息
		/// </summary>
		/// <param name="text"></param>
		/// <param name="ex"></param>
		void OutputError(string text, Exception ex)
		{
			this.rTxtOutputer.Output(text + Environment.NewLine + ex.Message, eOutputType.Error);
		}

		/// <summary>
		/// 窗体关闭后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmTrainWeight_FormClosed(object sender, FormClosedEventArgs e)
		{
			// 注意：必须取消任务
			this.taskSimpleScheduler.Cancal();
		}

		private void FrmTrainWeight_FormClosing(object sender, FormClosingEventArgs e)
		{
			//socket.Dispose();
		}
	}
}
