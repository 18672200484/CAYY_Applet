using CMCS.CarTransport.BeltSampler.Core;
using CMCS.CarTransport.BeltSampler.Enums;
using CMCS.CarTransport.BeltSampler.Frms.Sys;
using CMCS.CarTransport.DAO;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Common.Views;
using CMCS.Forms.UserControls;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CMCS.CarTransport.BeltSampler.Frms
{
	public partial class FrmBeltSampler : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmBeltSampler";

		public FrmBeltSampler()
		{
			InitializeComponent();
		}

		#region Vars

		CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
		BeltSamplerDAO beltSamplerDAO = BeltSamplerDAO.GetInstance();
		CommonDAO commonDAO = CommonDAO.GetInstance();
		RTxtOutputer rTxtOutputer;
		/// <summary>
		/// 语音播报
		/// </summary>
		//VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

		eFlowFlag currentFlowFlag = eFlowFlag.发送计划;
		/// <summary>
		/// 当前业务流程标识
		/// </summary>
		public eFlowFlag CurrentFlowFlag
		{
			get { return currentFlowFlag; }
			set
			{
				currentFlowFlag = value;
				panCurrentCarNumber.Text = value.ToString();
			}
		}

		/// <summary>
		/// 采样机编码 默认#1采样机
		/// </summary>
		string[] sampleMachineCodes = new string[] { GlobalVars.MachineCode_RCPDCYJ_1 };
		string[] jYGMachineCodes = new string[] { "#1入厂皮带采样机集样器", "#2入厂皮带采样机集样器" };
		/// <summary>
		/// 当前选中的皮带采样机设备
		/// </summary>
		CmcsCMEquipment currentSampleMachine;
		/// <summary>
		/// 当前选择的采样机设备
		/// </summary>
		public CmcsCMEquipment CurrentSampleMachine
		{
			get { return currentSampleMachine; }
			set
			{
				currentSampleMachine = value;
				if (value != null)
				{
					lblCurrSamplerName.Text = value.EquipmentName.Contains("#1") ? "A皮带采样机" : "B皮带采样机";
				}
			}
		}

		/// <summary>
		/// 集样器设备编码
		/// </summary>
		public string JYGMachineCode = "#1入厂皮带采样机集样器";

		View_RCSampling currentRCSampling;
		/// <summary>
		/// 当前采样单
		/// </summary>
		public View_RCSampling CurrentRCSampling
		{
			get { return currentRCSampling; }
			set
			{
				currentRCSampling = value;
				if (value != null)
				{
					lblBatch.Text = value.Batch;
					lblFactarriveDate.Text = value.FactarriveDate.ToString("yyyy-MM-dd");
					lblSupplierName.Text = value.SupplierName;
					lblMineName.Text = value.MineName;
				}
				else
				{
					lblBatch.Text = "####";
					lblFactarriveDate.Text = "####";
					lblSupplierName.Text = "####";
					lblMineName.Text = "####";
				}
			}
		}

		InfBeltSampleCmd currentSampleCMD;
		/// <summary>
		/// 当前采样命令
		/// </summary>
		public InfBeltSampleCmd CurrentSampleCMD
		{
			get { return currentSampleCMD; }
			set { currentSampleCMD = value; }
		}

		eEquInfGatherType currentGatherType = eEquInfGatherType.底卸式;
		/// <summary>
		/// 当前卸样方式 默认底卸式
		/// </summary>
		public eEquInfGatherType CurrentGatherType
		{
			get { return currentGatherType; }
			set
			{
				currentGatherType = value;
				lblGatherType.Text = value.ToString();
			}
		}

		//eEquInfCmdResultCode currentCmdResultCode = eEquInfCmdResultCode.默认;
		///// <summary>
		///// 当前命令执行结果 
		///// </summary>
		//public eEquInfCmdResultCode CurrentCmdResultCode
		//{
		//	get { return currentCmdResultCode; }
		//	set
		//	{
		//		currentCmdResultCode = value;

		//		lblResult.Text = currentCmdResultCode.ToString();
		//	}
		//}

		eEquInfCmdResultCode currentJYGCmdResultCode = eEquInfCmdResultCode.默认;
		/// <summary>
		/// 集样罐当前命令执行结果 
		/// </summary>
		public eEquInfCmdResultCode CurrentJYGCmdResultCode
		{
			get { return currentJYGCmdResultCode; }
			set
			{
				currentJYGCmdResultCode = value;

				lblResultJYG.Text = currentJYGCmdResultCode.ToString();
			}
		}

		eEquInfSamplerSystemStatus currentSystemStatus = eEquInfSamplerSystemStatus.就绪待机;
		/// <summary>
		/// 当前采样机系统状态
		/// </summary>
		public eEquInfSamplerSystemStatus CurrentSystemStatus
		{
			get { return currentSystemStatus; }
			set
			{
				currentSystemStatus = value;
				lblSampleState.Text = value.ToString();
			}
		}

		/// <summary>
		/// 采样机结果是否执行
		/// </summary>
		bool IsResultSample = false;

		/// <summary>
		/// 集样器结果是否执行
		/// </summary>
		bool IsResultJYG = false;
		#endregion

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void InitForm()
		{
			superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			superGridControl2.PrimaryGrid.AutoGenerateColumns = false;
			//绑定SuperGridControl事件 gclmSetSampler
			GridButtonXEditControl btnSetSampler = superGridControl2.PrimaryGrid.Columns["gclmSetSampler"].EditControl as GridButtonXEditControl;
			btnSetSampler.Click += btnSetSampler_Click;

			// 采样机设备编码，跟采样程序一一对应
			sampleMachineCodes = CommonDAO.GetInstance().GetCommonAppletConfigString("入厂采样机设备编码").Split('|');

			// 重置程序远程控制命令
			commonDAO.ResetAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);
		}

		private void FrmCarSampler_Load(object sender, EventArgs e)
		{
			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);
		}

		private void FrmCarSampler_Shown(object sender, EventArgs e)
		{
			InitForm();

			CreateSamplerButton();
			CreateEquStatus();
			BindRCSampling(superGridControl2);
			// 触发第一个按钮
			if (lypanSamplerButton.Controls.Count > 0) (lypanSamplerButton.Controls[0] as RadioButton).Checked = true;
			SetGatherType();
		}

		private void FrmCarSampler_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		#region 公共业务
		/// <summary>
		/// 读卡、车号识别任务
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			timer1.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag)
				{
					case eFlowFlag.等待执行:

						if (!IsResultSample)
						{
							CurrentCmdResultCode = beltSamplerDAO.GetSampleCmdResult(CurrentSampleCMD.Id);
							if (CurrentCmdResultCode == eEquInfCmdResultCode.成功)
							{
								this.rTxtOutputer.Output("采样命令执行成功", eOutputType.Success);
							}
							else if (CurrentCmdResultCode == eEquInfCmdResultCode.失败)
							{
								this.rTxtOutputer.Output("采样命令执行失败", eOutputType.Warn);
								List<InfEquInfHitch> list_Hitch = commonDAO.GetWarnEquInfHitch(DateTime.Now.AddMinutes(-1), DateTime.Now.AddMinutes(1), this.CurrentSampleMachine.EquipmentCode);
								foreach (InfEquInfHitch item in list_Hitch)
								{
									this.rTxtOutputer.Output("采样机:" + item.HitchDescribe, eOutputType.Error);
								}
							}
							IsResultSample = CurrentCmdResultCode != eEquInfCmdResultCode.默认;
						}

						break;
					case eFlowFlag.执行完毕:
						ResetBuyFuel();
						break;
				}
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer1_Tick", ex);
			}
			finally
			{
				timer1.Start();
			}

			timer1.Start();
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			timer2.Stop();
			// 2秒执行一次
			timer2.Interval = 2000;

			try
			{
				RefreshEquStatus();
				BindBeltSampleBarrel(superGridControl1, this.JYGMachineCode);
				BindRCSampling(superGridControl2);

			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer2_Tick", ex);
			}
			finally
			{
				timer2.Start();
			}
		}

		#endregion

		#region 操作

		/// <summary>
		/// 开始采样
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStartSampler_Click(object sender, EventArgs e)
		{
			if (this.CurrentFlowFlag == eFlowFlag.等待执行)
			{ MessageBoxEx.Show("等待当前命令执行完成"); return; }
			if (CurrentRCSampling == null) { MessageBoxEx.Show("请先设置当前采样单"); return; }

			string lczt = CommonDAO.GetInstance().GetSignalDataValue("入厂转运线", eSignalDataName.转运线大流程.ToString());
			if (lczt != "采样")
			{
				{ MessageBoxEx.Show("设备转运线大流程未设置为采样！"); return; }
			}

			string hgfs = CommonDAO.GetInstance().GetSignalDataValue(this.JYGMachineCode, eSignalDataName.换罐方式.ToString());
			if (hgfs != "计算机")
			{
				{ MessageBoxEx.Show("设备换罐方式未设置为计算机！"); return; }
			}

			//string systemStatus = CommonDAO.GetInstance().GetSignalDataValue(this.JYGMachineCode, eSignalDataName.设备状态.ToString());
			//if (systemStatus != eEquInfSamplerSystemStatus.就绪待机.ToString())
			//{
			//	{ MessageBoxEx.Show("集样器未就绪"); return; }
			//}

			if (!SendSamplingCMD(true)) { MessageBoxEx.Show("集样命令发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("命令发送成功，等待执行");
			timer1.Enabled = true;
			this.CurrentFlowFlag = eFlowFlag.等待执行;
		}

		/// <summary>
		/// 设置当前采样单
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSetSampler_Click(object sender, EventArgs e)
		{
			GridRow gridRow = (superGridControl2.PrimaryGrid.ActiveRow as GridRow);
			if (gridRow == null) return;

			if (MessageBoxEx.Show("是否设置该记录为当前采样单", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
				CurrentRCSampling = gridRow.DataItem as View_RCSampling;
		}
		#endregion

		#region 入厂煤采样业务
		/// <summary>
		/// 保存入厂煤运输记录
		/// </summary>
		/// <returns></returns>
		bool SendSamplingPlan()
		{
			InfBeltSamplePlan oldBeltSamplePlan = Dbers.GetInstance().SelfDber.Entity<InfBeltSamplePlan>("where InFactoryBatchId=:InFactoryBatchId and SampleCode=:SampleCode", new { InFactoryBatchId = this.CurrentRCSampling.BatchId, SampleCode = this.CurrentRCSampling.SampleCode });
			if (oldBeltSamplePlan == null)
			{
				return Dbers.GetInstance().SelfDber.Insert<InfBeltSamplePlan>(new InfBeltSamplePlan
				{
					DataFlag = 0,
					InterfaceType = this.CurrentSampleMachine.InterfaceType,
					InFactoryBatchId = this.CurrentRCSampling.BatchId,
					SampleCode = this.CurrentRCSampling.SampleCode,
					FuelKindName = this.CurrentRCSampling.FuelName,
					CarCount = 0,
					Mt = 0,
					TicketWeight = 0,
					GatherType = CurrentGatherType.ToString(),
					SampleType = "皮带采样"
				}) > 0;
			}
			else
			{
				oldBeltSamplePlan.DataFlag = 0;
				oldBeltSamplePlan.FuelKindName = this.CurrentRCSampling.FuelName;
				oldBeltSamplePlan.CarCount = 0;
				oldBeltSamplePlan.Mt = 0;
				oldBeltSamplePlan.TicketWeight = 0;
				oldBeltSamplePlan.GatherType = CurrentGatherType.ToString();

				return Dbers.GetInstance().SelfDber.Update(oldBeltSamplePlan) > 0;
			}
		}

		/// <summary>
		/// 发送开始采样命令
		/// </summary>
		/// <returns></returns>
		bool SendSamplingCMD(bool isStart)
		{
			string barrelNumber = string.Empty;
			if (beltSamplerDAO.CheckIsUnFinishCmd(this.JYGMachineCode, this.CurrentRCSampling.SampleCode, 5))
			{
				this.rTxtOutputer.Output("该采样码最近5分钟内有未执行的命令，请稍后再发送命令", eOutputType.Warn);
				return false;
			}
			InfCYSampleCMD SampleCmd_Old = Dbers.GetInstance().SelfDber.Entity<InfCYSampleCMD>("where MachineCode=:MachineCode and SampleCode=:SampleCode order by CreationTime", new { MachineCode = this.JYGMachineCode, SampleCode = this.CurrentRCSampling.SampleCode });
			if (SampleCmd_Old != null)
			{
				if (!beltSamplerDAO.CheckBarrelNumberIsFull(SampleCmd_Old.BarrelNumber.ToString(), SampleCmd_Old.MachineCode))
				{
					this.rTxtOutputer.Output("样桶已满，请清样", eOutputType.Warn);
					return false;
				}
				else
					barrelNumber = SampleCmd_Old.BarrelNumber.ToString();
			}
			else
				barrelNumber = beltSamplerDAO.GetBarrelNumberBySampelCode(this.CurrentRCSampling.SampleCode, this.JYGMachineCode);
			if (string.IsNullOrEmpty(barrelNumber))
			{
				this.rTxtOutputer.Output("无样桶可分配", eOutputType.Warn);
				return false;
			}
			this.CurrentJYGCMD = new InfCYSampleCMD();
			this.CurrentJYGCMD.MachineCode = this.JYGMachineCode;
			this.CurrentJYGCMD.SampleCode = this.CurrentRCSampling.SampleCode;
			this.CurrentJYGCMD.BarrelNumber = Convert.ToInt32(barrelNumber);
			this.CurrentJYGCMD.ResultCode = eEquInfCmdResultCode.默认.ToString();
			this.CurrentJYGCMD.DataFlag = 0;
			this.CurrentJYGCMD.CmdCode = "1";
			// 发送集样命令
			if (commonDAO.SelfDber.Insert<InfCYSampleCMD>(CurrentJYGCMD) > 0)
			{
				//this.rTxtOutputer.Output("集样命令发送成功,样罐号:" + barrelNumber);
				this.rTxtOutputer.Output(MachineCodeToShow(this.JYGMachineCode) + "集样命令发送成功");
				//写入实时信号
				commonDAO.SetSignalDataValue(this.JYGMachineCode, "采样编码", this.CurrentRCSampling.SampleCode);
				commonDAO.SetSignalDataValue(this.JYGMachineCode, "截取开始时间", DateTime.Now.ToShortTimeString());
				CmcsInFactoryBatch batch = Dbers.GetInstance().SelfDber.Entity<CmcsInFactoryBatch>("where Id=:Id", new { Id = this.CurrentRCSampling.BatchId });
				if (batch != null)
				{
					commonDAO.SetSignalDataValue(this.JYGMachineCode, "矿发量", batch.TicketQty.ToString());
					commonDAO.SetSignalDataValue(this.JYGMachineCode, "来煤车数", batch.TransportNumber.ToString());
				}

				return true;
			}
			else
			{
				//this.rTxtOutputer.Output("集样命令发送失败,样罐号:" + barrelNumber);
				this.rTxtOutputer.Output(MachineCodeToShow(this.JYGMachineCode) + "集样命令发送失败");
				return false;
			}

			//CurrentSampleCMD = new InfBeltSampleCmd
			//{
			//	DataFlag = 0,
			//	InterfaceType = this.CurrentSampleMachine.InterfaceType,
			//	MachineCode = this.CurrentSampleMachine.EquipmentCode,
			//	ResultCode = eEquInfCmdResultCode.默认.ToString(),
			//	SampleCode = this.CurrentRCSampling.SampleCode,
			//	//BarrelNumber =Convert.ToInt32(barrelNumber),
			//	CmdCode = (isStart == true ? eEquInfSamplerCmd.开始采样.ToString() : eEquInfSamplerCmd.结束采样.ToString())
			//};
			//if (Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd>(CurrentSampleCMD) > 0)
			//{
			//	this.rTxtOutputer.Output("采样命令发送成功");
			//	return true;
			//}
			//return false;
		}

		/// <summary>
		/// 重置入厂煤运输记录
		/// </summary>
		void ResetBuyFuel()
		{
			this.CurrentFlowFlag = eFlowFlag.选择计划;
			//this.CurrentCmdResultCode = eEquInfCmdResultCode.默认;
			this.CurrentJYGCmdResultCode = eEquInfCmdResultCode.默认;
			this.CurrentSampleCMD = null;
			this.CurrentRCSampling = null;
			IsResultJYG = false;
			IsResultSample = false;
		}

		/// <summary>
		/// 重置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReset_Click(object sender, EventArgs e)
		{
			ResetBuyFuel();
		}

		#endregion

		#region 信号业务
		/// <summary>
		/// 创建皮带采样机、集样器状态
		/// </summary>
		private void CreateEquStatus()
		{
			flpanEquState.SuspendLayout();

			foreach (string cMEquipmentCode in sampleMachineCodes)
			{
				LabelX lblMachineName = new LabelX()
				{
					Text = cMEquipmentCode.Contains("#1") ? "A皮带采样机" : "B皮带采样机",
					AutoSize = true,
					Anchor = AnchorStyles.Left,
					Font = new Font("Segoe UI", 14.25f, FontStyle.Bold)
				};

				flpanEquState.Controls.Add(lblMachineName);

				LabelX uCtrlSignalLight = new LabelX()
				{
					Tag = cMEquipmentCode,
					AutoSize = true,
					Anchor = AnchorStyles.Left,
					Font = new Font("Segoe UI", 14.25f, FontStyle.Bold),
					Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
				};
				SetSystemStatusToolTip(uCtrlSignalLight);
				flpanEquState.Controls.Add(uCtrlSignalLight);
			}

			foreach (string cMEquipmentCode in jYGMachineCodes)
			{
				LabelX lblMachineName = new LabelX()
				{
					Text = cMEquipmentCode.Contains("#1") ? "A入厂皮带采样机集样器" : "B入厂皮带采样机集样器",
					AutoSize = true,
					Anchor = AnchorStyles.Left,
					Font = new Font("Segoe UI", 14.25f, FontStyle.Bold)
				};

				flpanEquState.Controls.Add(lblMachineName);

				LabelX uCtrlSignalLight = new LabelX()
				{
					Tag = cMEquipmentCode,
					AutoSize = true,
					Anchor = AnchorStyles.Left,
					Font = new Font("Segoe UI", 14.25f, FontStyle.Bold),
					Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
				};
				SetSystemStatusToolTip(uCtrlSignalLight);
				flpanEquState.Controls.Add(uCtrlSignalLight);
			}

			flpanEquState.ResumeLayout();
			if (this.flpanEquState.Controls.Count == 0)
				MessageBoxEx.Show("皮带采样机或制样机参数未设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// 更新皮带采样机状态
		/// </summary>
		private void RefreshEquStatus()
		{
			foreach (LabelX uCtrlSignalLight in flpanEquState.Controls.OfType<LabelX>())
			{
				if (uCtrlSignalLight.Tag == null) continue;

				string machineCode = uCtrlSignalLight.Tag.ToString();
				if (string.IsNullOrEmpty(machineCode)) continue;
				if (machineCode.Contains("集样器"))
				{
					string systemStatus = CommonDAO.GetInstance().GetSignalDataValue(machineCode, eSignalDataName.系统故障.ToString());
					uCtrlSignalLight.Text = systemStatus;
					if (systemStatus == "正常")
					{
						uCtrlSignalLight.BackColor = EquipmentStatusColors.BeReady;
					}
					else
					{
						uCtrlSignalLight.BackColor = EquipmentStatusColors.Breakdown;
					}
				}
				else
				{
					string systemStatus = CommonDAO.GetInstance().GetSignalDataValue(machineCode, eSignalDataName.设备状态.ToString());
					uCtrlSignalLight.Text = systemStatus;
					if (systemStatus == eEquInfSamplerSystemStatus.就绪待机.ToString())
						uCtrlSignalLight.BackColor = EquipmentStatusColors.BeReady;
					else if (systemStatus == eEquInfSamplerSystemStatus.正在运行.ToString() || systemStatus == eEquInfSamplerSystemStatus.正在卸样.ToString())
						uCtrlSignalLight.BackColor = EquipmentStatusColors.Working;
					else if (systemStatus == eEquInfSamplerSystemStatus.发生故障.ToString())
						uCtrlSignalLight.BackColor = EquipmentStatusColors.Breakdown;
					else if (systemStatus == eEquInfSamplerSystemStatus.样桶已满.ToString())
						uCtrlSignalLight.BackColor = EquipmentStatusColors.Full;
					else if (systemStatus == eEquInfSamplerSystemStatus.系统停止.ToString())
						uCtrlSignalLight.BackColor = EquipmentStatusColors.Forbidden;

					eEquInfSamplerSystemStatus status;
					//当前选择的采样机状态
					if (machineCode == CurrentSampleMachine.EquipmentCode)
						if (Enum.TryParse(systemStatus, out status))
							CurrentSystemStatus = status;
				}

			}
		}

		/// <summary>
		/// 设置ToolTip提示
		/// </summary>
		private void SetSystemStatusToolTip(Control control)
		{
			this.toolTip1.SetToolTip(control, "<绿色> 就绪待机\r\n<红色> 正在运行\r\n<黄色> 发生故障\r\n<灰色> 系统停止");
		}

		private void SetGatherType()
		{
			eEquInfGatherType GatherType;
			if (currentSampleMachine.EquipmentCode == "#1皮带采样机")
			{
				if (Enum.TryParse(CommonDAO.GetInstance().GetAppletConfigString("公共配置", "#1皮带采样机集样方式"), out GatherType))
					CurrentGatherType = GatherType;
			}
			else
				if (Enum.TryParse(CommonDAO.GetInstance().GetAppletConfigString("公共配置", "#2皮带采样机集样方式"), out GatherType))
				CurrentGatherType = GatherType;
		}
		#endregion

		#region 其他

		private void superGridControl_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
		{
			// 取消进入编辑
			e.Cancel = true;
		}

		/// <summary>
		/// 设置行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
		{
			e.Text = (e.GridRow.RowIndex + 1).ToString();
		}

		/// <summary>
		/// Invoke封装
		/// </summary>
		/// <param name="action"></param>
		public void InvokeEx(Action action)
		{
			if (this.IsDisposed || !this.IsHandleCreated) return;

			this.Invoke(action);
		}

		/// <summary>
		/// 生成采样机选项
		/// </summary>
		private void CreateSamplerButton()
		{
			foreach (string machineCode in sampleMachineCodes)
			{
				CmcsCMEquipment Equipment = CommonDAO.GetInstance().GetCMEquipmentByMachineCode(machineCode);
				RadioButton rbtnSampler = new RadioButton();
				rbtnSampler.Font = new Font("Segoe UI", 15f, FontStyle.Bold);
				rbtnSampler.Text = Equipment.EquipmentName.Contains("#1") ? "A皮带采样机" : "B皮带采样机";
				rbtnSampler.Tag = Equipment;
				rbtnSampler.AutoSize = true;
				rbtnSampler.Padding = new System.Windows.Forms.Padding(10, 0, 0, 10);
				rbtnSampler.CheckedChanged += new EventHandler(rbtnSampler_CheckedChanged);

				lypanSamplerButton.Controls.Add(rbtnSampler);
			}
		}

		void rbtnSampler_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rbtnSampler = sender as RadioButton;
			this.CurrentSampleMachine = rbtnSampler.Tag as CmcsCMEquipment;
			JYGMachineCode = this.CurrentSampleMachine.EquipmentCode.Contains("1") ? "#1入厂皮带采样机集样器" : "#2入厂皮带采样机集样器";
			BindBeltSampleBarrel(superGridControl1, this.JYGMachineCode);
		}

		#endregion

		/// <summary>
		/// 绑定集样罐信息
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="machineCode">设备编码</param>
		private void BindBeltSampleBarrel(SuperGridControl superGridControl, string machineCode)
		{
			IList<InfEquInfSampleBarrel> list = CommonDAO.GetInstance().GetEquInfSampleBarrels(machineCode);
			superGridControl.PrimaryGrid.DataSource = list;
		}

		private void BindRCSampling(SuperGridControl superGridControl)
		{
			List<View_RCSampling> list = beltSamplerDAO.GetViewRCSampling(string.Format("where BatchType='火车' and SamplingWay!='人工采样' and to_char(SamplingDate,'yyyy-MM-dd hh24:mm:ss')>='{0}' order by SamplingDate desc", DateTime.Now.AddDays(-3).Date.ToString("yyyy-MM-dd")));
			superGridControl.PrimaryGrid.DataSource = list;
		}

		/// <summary>
		/// 转换为显示名称
		/// </summary>
		/// <param name="machineCode"></param>
		/// <returns></returns>
		public string MachineCodeToShow(string machineCode)
		{

			if (machineCode.Contains("#1"))
			{
				return "A入厂皮带采样机集样器";
			}
			else
			{
				return "B入厂皮带采样机集样器";
			}

		}
	}
}
