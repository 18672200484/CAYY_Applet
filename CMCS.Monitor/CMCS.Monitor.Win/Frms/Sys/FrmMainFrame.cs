using CMCS.CarTransport.BeltSampler.Frms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Enums;
using CMCS.Forms.UserControls;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Frm.Sys;
using CMCS.Monitor.Win.Utilities;
//
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace CMCS.Monitor.Win.Frms.Sys
{
	public partial class FrmMainFrame : MetroForm
	{
		CommonDAO commonDAO = CommonDAO.GetInstance();

		public static SuperTabControlManager superTabControlManager;

		#region Vars

		bool inductorCoil1 = false;
		/// <summary>
		/// #1汽车机械采样机状态 true=有信号  false=无信号
		/// </summary>
		public bool InductorCoil1
		{
			get
			{
				return inductorCoil1;
			}
			set
			{
				inductorCoil1 = value;

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "#1汽车机械采样机急停按钮", value ? "1" : "0");
			}
		}

		/// <summary>
		/// 命令是否已发送
		/// </summary>
		public bool InductorCoil1IsSend = false;

		int inductorCoil1Port;
		/// <summary>
		/// #1汽车机械采样机端口
		/// </summary>
		public int InductorCoil1Port
		{
			get { return inductorCoil1Port; }
			set { inductorCoil1Port = value; }
		}

		bool inductorCoil2 = false;
		/// <summary>
		/// #2汽车机械采样机状态 true=有信号  false=无信号
		/// </summary>
		public bool InductorCoil2
		{
			get
			{
				return inductorCoil2;
			}
			set
			{
				inductorCoil2 = value;

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "#2汽车机械采样机急停按钮", value ? "1" : "0");
			}
		}

		/// <summary>
		/// 命令是否已发送
		/// </summary>
		public bool InductorCoil2IsSend = false;

		int inductorCoil2Port;
		/// <summary>
		///#2汽车机械采样机端口
		/// </summary>
		public int InductorCoil2Port
		{
			get { return inductorCoil2Port; }
			set { inductorCoil2Port = value; }
		}

		bool inductorCoil3 = false;
		/// <summary>
		/// #1火车机械采样机状态 true=有信号  false=无信号
		/// </summary>
		public bool InductorCoil3
		{
			get
			{
				return inductorCoil3;
			}
			set
			{
				inductorCoil3 = value;

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "#1火车机械采样机急停按钮", value ? "1" : "0");
			}
		}

		int inductorCoil3Port;
		/// <summary>
		/// #1火车机械采样机端口
		/// </summary>
		public int InductorCoil3Port
		{
			get { return inductorCoil3Port; }
			set { inductorCoil3Port = value; }
		}

		/// <summary>
		/// 命令是否已发送
		/// </summary>
		public bool InductorCoil3IsSend = false;

		bool inductorCoil4 = false;
		/// <summary>
		/// #2火车机械采样机状态 true=有信号  false=无信号
		/// </summary>
		public bool InductorCoil4
		{
			get
			{
				return inductorCoil4;
			}
			set
			{
				inductorCoil4 = value;

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "#2火车机械采样机急停按钮", value ? "1" : "0");
			}
		}

		int inductorCoil4Port;
		/// <summary>
		/// #2火车机械采样机端口
		/// </summary>
		public int InductorCoil4Port
		{
			get { return inductorCoil4Port; }
			set { inductorCoil4Port = value; }
		}

		/// <summary>
		/// 命令是否已发送
		/// </summary>
		public bool InductorCoil4IsSend = false;

		bool inductorCoil5 = false;
		/// <summary>
		/// #3火车机械采样机状态 true=有信号  false=无信号
		/// </summary>
		public bool InductorCoil5
		{
			get
			{
				return inductorCoil5;
			}
			set
			{
				inductorCoil5 = value;

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "#3火车机械采样机急停按钮", value ? "1" : "0");
			}
		}

		/// <summary>
		/// 命令是否已发送
		/// </summary>
		public bool InductorCoil5IsSend = false;

		int inductorCoil5Port;
		/// <summary>
		/// #3火车机械采样机端口
		/// </summary>
		public int InductorCoil5Port
		{
			get { return inductorCoil5Port; }
			set { inductorCoil5Port = value; }
		}

		#endregion

		public FrmMainFrame()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			lblVersion.Text = new AU.Updater().Version;

			FrmMainFrame.superTabControlManager = new SuperTabControlManager(this.superTabControl1);
		}

		#region IO控制器

		void Iocer_StatusChange(bool status)
		{
			// 接收IO控制器状态 
			InvokeEx(() =>
			{
				//slightIOC.LightColor = (status ? Color.Green : Color.Red);

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.IO控制器_连接状态.ToString(), status ? "1" : "0");
			});
		}

		/// <summary>
		/// IO控制器接收数据时触发
		/// </summary>
		/// <param name="receiveValue"></param>
		void Iocer_Received(int[] receiveValue)
		{
			// 接收状态  
			InvokeEx(() =>
			{
				this.InductorCoil1 = (receiveValue[this.InductorCoil1Port - 1] == 0);
				this.InductorCoil2 = (receiveValue[this.InductorCoil2Port - 1] == 0);
				this.InductorCoil3 = (receiveValue[this.InductorCoil3Port - 1] == 0);
				this.InductorCoil4 = (receiveValue[this.InductorCoil4Port - 1] == 0);
				this.InductorCoil5 = (receiveValue[this.InductorCoil5Port - 1] == 0);

				if (this.InductorCoil1 && !InductorCoil1IsSend)
				{
					if (commonDAO.SendAppRemoteControlCmd(GlobalVars.MachineCode_QCJXCYJ_1, "急停", "1"))
					{
						commonDAO.SaveOperationLog(GlobalVars.MachineCode_QCJXCYJ_1 + "急停按钮按下", GlobalVars.LoginUser.Name);
						InductorCoil1IsSend = true;
					}
				}
				else if (!this.InductorCoil1)
					InductorCoil1IsSend = false;

				if (this.InductorCoil2 && !InductorCoil2IsSend)
				{
					if (commonDAO.SendAppRemoteControlCmd(GlobalVars.MachineCode_QCJXCYJ_2, "急停", "1"))
					{
						commonDAO.SaveOperationLog(GlobalVars.MachineCode_QCJXCYJ_2 + "急停按钮按下", GlobalVars.LoginUser.Name);
						InductorCoil2IsSend = true;
					}
				}
				else if (!this.InductorCoil2)
					InductorCoil2IsSend = false;

				if (this.InductorCoil3 && !InductorCoil3IsSend)
				{
					if (SendSamplingCMD(GlobalVars.MachineCode_HCJXCYJ_1))
					{
						commonDAO.SaveOperationLog(GlobalVars.MachineCode_HCJXCYJ_1 + "急停按钮按下", GlobalVars.LoginUser.Name);
						InductorCoil3IsSend = true;
					}
				}
				else if (!this.InductorCoil3)
					InductorCoil3IsSend = false;

				if (this.InductorCoil4 && !InductorCoil4IsSend)
				{
					if (SendSamplingCMD(GlobalVars.MachineCode_HCJXCYJ_2))
					{
						commonDAO.SaveOperationLog(GlobalVars.MachineCode_HCJXCYJ_2 + "急停按钮按下", GlobalVars.LoginUser.Name);
						InductorCoil4IsSend = true;
					}
				}
				else if (!this.InductorCoil4)
					InductorCoil4IsSend = false;

				if (this.InductorCoil5 && !InductorCoil5IsSend)
				{
					if (SendSamplingCMD(GlobalVars.MachineCode_HCJXCYJ_3))
					{
						commonDAO.SaveOperationLog(GlobalVars.MachineCode_HCJXCYJ_3 + "急停按钮按下", GlobalVars.LoginUser.Name);
						InductorCoil5IsSend = true;
					}
				}
				else if (!this.InductorCoil5)
					InductorCoil5IsSend = false;

			});
		}

		/// <summary>
		/// 发送停止采样命令
		/// </summary>
		/// <returns></returns>
		bool SendSamplingCMD(string machineCode)
		{
			InfBeltSampleCmd samplecmd = new InfBeltSampleCmd
			{
				DataFlag = 0,
				InterfaceType = GlobalVars.InterfaceType_HCJXCYJ,
				MachineCode = machineCode,
				ResultCode = eEquInfCmdResultCode.默认.ToString(),
				SampleCode = "",
				CmdCode = eEquInfSamplerCmd.系统暂停.ToString()
			};
			if (Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd>(samplecmd) > 0)
			{
				return true;
			}
			return false;
		}

		#endregion

		private void Form1_Shown(object sender, EventArgs e)
		{
			if (GlobalVars.LoginUser == null)
			{
				GlobalVars.LoginUser = new Common.Entities.iEAA.CmcsUser();
				GlobalVars.LoginUser.UserName = GlobalVars.AdminAccount;
				GlobalVars.LoginUser.Name = "系统管理员";
			}
			if (GlobalVars.LoginUser != null) lblLoginUserName.Text = GlobalVars.LoginUser.UserName;

			//CommonDAO.GetInstance().ResetAllSysMessageStatus();

			this.InductorCoil1Port = commonDAO.GetAppletConfigInt32("#1汽车机械采样机急停端口");
			this.InductorCoil2Port = commonDAO.GetAppletConfigInt32("#2汽车机械采样机急停端口");
			this.InductorCoil3Port = commonDAO.GetAppletConfigInt32("#1火车机械采样机急停端口");
			this.InductorCoil4Port = commonDAO.GetAppletConfigInt32("#2火车机械采样机急停端口");
			this.InductorCoil5Port = commonDAO.GetAppletConfigInt32("#3火车机械采样机急停端口");
			// IO控制器
			Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
			Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
			bool success = Hardwarer.Iocer.OpenCom(1, 9600);
			commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.IO控制器_连接状态.ToString(), success ? "1" : "0");
			// 打开集中管控首页
			btnOpenHomePage_Click(null, null);

			InitEquipmentStatus();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (MessageBoxEx.Show("确认退出系统？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					//Common.DAO.CommonDAO.GetInstance().SaveLoginLog(GlobalVars.LoginUser.UserName, Common.Enums.Sys.eUserLogInattempts.LockedOut);
					//Hardwarer.Iocer.CloseCom();
					CefRuntime.Shutdown();
					Application.Exit();
				}
				else
				{
					e.Cancel = true;
				}
			}
		}

		/// <summary>
		/// 退出系统
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnApplicationExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 显示当前时间
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_CurrentTime_Tick(object sender, EventArgs e)
		{
			lblCurrentTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
		}

		#region 打开/切换可视主界面

		#region 弹出窗体

		/// <summary>
		/// 打开集中管控首页
		/// </summary>
		public void OpenHomePage()
		{
			this.InvokeEx(() =>
			{
				string uniqueKey = FrmHomePage.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					FrmHomePage Frm = new FrmHomePage();
					FrmMainFrame.superTabControlManager.CreateTab(Frm.Text, uniqueKey, Frm, false);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			});
		}

		/// <summary>
		/// 打开汽车过衡监控
		/// </summary>
		public void OpenTruckWeighter()
		{
			this.InvokeEx(() =>
			{
				string uniqueKey = FrmTruckWeighter.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					SelfVars.TruckWeighterForm = new FrmTruckWeighter();
					FrmMainFrame.superTabControlManager.CreateTab(SelfVars.TruckWeighterForm.Text, uniqueKey, SelfVars.TruckWeighterForm, false);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			});
		}

		/// <summary>
		/// 打开火车采样机
		/// </summary>
		public void OpenTrainSampler()
		{
			this.InvokeEx(() =>
			{
				string uniqueKey = FrmTrainSampler.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					SelfVars.TrainSamplerForm = new FrmTrainSampler();
					FrmMainFrame.superTabControlManager.CreateTab(SelfVars.TrainSamplerForm.Text, uniqueKey, SelfVars.TrainSamplerForm, false);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			});
		}

		/// <summary>
		/// 打开门禁管理
		/// </summary>
		public void OpenDoor()
		{
			this.InvokeEx(() =>
			{
				string uniqueKey = FrmDoorManager.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					FrmDoorManager frm = new FrmDoorManager();
					FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			});
		}

		/// <summary>
		/// 打开入厂采样界面
		/// </summary>
		public void OpenCarSampler()
		{
			string uniqueKey = FrmBeltSampler.UniqueKey;

			if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
			{
				FrmBeltSampler frm = new FrmBeltSampler();
				FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
			}
			else
				FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
		}

		/// <summary>
		/// 打开汽车机械采样机监控
		/// </summary>
		public void OpenCarSampler1()
		{
			this.InvokeEx(() =>
			{
				string uniqueKey = FrmCarSampler.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					SelfVars.CarSamplerForm = new FrmCarSampler();
					FrmMainFrame.superTabControlManager.CreateTab(SelfVars.CarSamplerForm.Text, uniqueKey, SelfVars.CarSamplerForm, false);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			});
		}


		/// <summary>
		/// 打开化验室网络管理监控
		/// </summary>
		public void OpenAssayManage()
		{
			this.InvokeEx(() =>
			{
				string uniqueKey = FrmAssayManage.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					FrmAssayManage frm = new FrmAssayManage();
					FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, false);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			});
		}

		/// <summary>
		/// 打开翻车机信息界面
		/// </summary>
		public void OpenCarDumper()
		{
			string uniqueKey = FrmCarDumper.UniqueKey;

			if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
			{
				FrmCarDumper frm = new FrmCarDumper();
				FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
			}
			else
				FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
		}

		/// <summary>
		/// 打开皮带采样机监控
		/// </summary>
		public void OpenTrainBeltSampler()
		{
			this.InvokeEx(() =>
			{
				string uniqueKey = FrmTrainBeltSampler.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					FrmTrainBeltSampler frmTrainBeltSampler = new FrmTrainBeltSampler();
					FrmMainFrame.superTabControlManager.CreateTab(frmTrainBeltSampler.Text, uniqueKey, frmTrainBeltSampler, false);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			});
		}

		/// <summary>
		/// 打开全自动制样机
		/// </summary>
		public void OpenAutoMaker()
		{
			this.InvokeEx(() =>
			{
				string uniqueKey = FrmAutoMaker.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					FrmAutoMaker frmTrainBeltSampler = new FrmAutoMaker();
					FrmMainFrame.superTabControlManager.CreateTab(frmTrainBeltSampler.Text, uniqueKey, frmTrainBeltSampler, false);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			});
		}

		/// <summary>
		/// 打开视频预览
		/// </summary>
		/// <param name="param"></param>
		/// <param name="videoName"></param>
		public void OpenHikVideo(string videoName)
		{
			this.BeginInvoke((Action)(() =>
			{
				if (!string.IsNullOrEmpty(videoName))
				{
					FrmHikVideo frm = new FrmHikVideo(videoName);
					frm.ShowDialog();
				}
				else
					MessageBoxEx.Show("视频名称未配置");
			}));
		}

		/// <summary>
		/// 打开发送采样计划
		/// </summary>
		/// <param name="param"></param>
		/// <param name="videoName"></param>
		public void OpenSendSampleCode(string name)
		{
			this.BeginInvoke((Action)(() =>
			{
				if (!string.IsNullOrEmpty(name))
				{
					FrmSampleCode_Select frm = new FrmSampleCode_Select(name);
					frm.ShowDialog();
				}

			}));
		}

		/// <summary>
		/// 打开制样机报警信息
		/// </summary>
		/// <param name="param"></param>
		/// <param name="videoName"></param>
		public void OpenAutoMakerErrorInfo()
		{
			this.BeginInvoke((Action)(() =>
			{
				FrmAutoMaker_Warning frm = new FrmAutoMaker_Warning();
				frm.ShowDialog();
			}));
		}

		/// <summary>
		/// 打开合样归批报警信息
		/// </summary>
		/// <param name="param"></param>
		/// <param name="videoName"></param>
		public void OpenBatchMachineErrorInfo()
		{
			this.BeginInvoke((Action)(() =>
			{
				FrmBatchMachine_Warning frm = new FrmBatchMachine_Warning();
				frm.ShowDialog();
			}));
		}

		/// <summary>
		/// 打开历史故障
		/// </summary>
		/// <param name="param"></param>
		/// <param name="videoName"></param>
		public void OpenFaultRecordInfo(string machinecode)
		{
			this.BeginInvoke((Action)(() =>
			{
				FrmWarningInfo frm = new FrmWarningInfo(machinecode);
				frm.ShowDialog();
			}));
		}

		/// <summary>
		/// 打开操作日志界面
		/// </summary>
		public void OpenOperationLogs()
		{
			string uniqueKey = FrmOperationLogs.UniqueKey;

			if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
			{
				FrmOperationLogs frm = new FrmOperationLogs();
				FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
			}
			else
				FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
		}

		/// <summary>
		/// 打开气动传输监控
		/// </summary>
		public void OpenAutoCupboard()
		{
			this.Invoke((Action)(() =>
			{
				string uniqueKey = FrmAutoCupboardPneumaticTransfer.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					FrmAutoCupboardPneumaticTransfer frm = new FrmAutoCupboardPneumaticTransfer();
					FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, false);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			}));
		}

		/// <summary>
		/// 打开智能存样柜
		/// </summary>
		public void OpenSampleCabinet()
		{
			this.Invoke((Action)(() =>
			{
				string uniqueKey = FrmSampleCabinet.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					FrmSampleCabinet frm = new FrmSampleCabinet();
					FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, false);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			}));
		}

		/// <summary>
		/// 打开智能存样柜存取
		/// </summary>
		public void OpenSampleCabinetManager()
		{
			this.Invoke((Action)(() =>
			{
				string uniqueKey = FrmSampleCabinetManager.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					SelfVars.FrmSampleCabinetManagerForm = new FrmSampleCabinetManager();
					FrmMainFrame.superTabControlManager.CreateTab(SelfVars.FrmSampleCabinetManagerForm.Text, uniqueKey, SelfVars.FrmSampleCabinetManagerForm, false);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			}));
		}

		/// <summary>
		/// 打开皮带采样机报警
		/// </summary>
		public void OpenTrainBeltSampler_warning()
		{
			//this.Invoke((Action)(() =>
			//{
			//	string uniqueKey = FrmTrainSampler_Warning.UniqueKey;

			//	if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
			//	{
			//		FrmTrainSampler_Warning frm = new FrmTrainSampler_Warning();
			//		FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
			//	}
			//	else
			//		FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			//}));

			this.BeginInvoke((Action)(() =>
			{
				FrmTrainSampler_Warning frm = new FrmTrainSampler_Warning();
				frm.ShowDialog();
			}));
		}

		/// <summary>
		/// 打开合样归批机
		/// </summary>
		public void OpenBatchMachine()
		{
			this.InvokeEx(() =>
			{
				string uniqueKey = FrmBatchMachine.UniqueKey;

				if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
				{
					FrmBatchMachine frmBatchMachine = new FrmBatchMachine();
					FrmMainFrame.superTabControlManager.CreateTab(frmBatchMachine.Text, uniqueKey, frmBatchMachine, false);
				}
				else
					FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			});
		}

		/// <summary>
		/// 打开合样归批机倒料
		/// </summary>
		public void BatchMachineSendDLCMD()
		{
			this.BeginInvoke((Action)(() =>
			{
				FrmBatchMachineBarrel_Select frm = new FrmBatchMachineBarrel_Select("");
				if (frm.ShowDialog() == DialogResult.OK)
				{
					BatchMachineBarrel_Select content = frm.Output;

					//CmcsRCSampling rcSampling = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCSampling>("where SampleCode=:SampleCode", new { SampleCode = content.SampleID });
					//if (rcSampling == null)
					//{
					//	MessageBox.Show("未找到采样记录", "提示");
					//	return;
					//}

					InfBatchMachineCmd rcSampling = CommonDAO.GetInstance().SelfDber.Entity<InfBatchMachineCmd>("where ResultCode=:ResultCode", new { ResultCode = eEquInfCmdResultCode.默认.ToString() });
					if (rcSampling != null)
					{
						MessageBox.Show("存在未完成的倒料命令，不能发送新的命令！", "提示");
						return;
					}
					double weight = commonDAO.GetSignalDataValueDouble(GlobalVars.MachineCode_QZDZYJ_1, "原煤称实时重量");
					if (weight > 0)
					{
						MessageBox.Show("制样料斗有料，不允许发送指令！", "提示");
						return;
					}
					List<InfBatchMachineBarrel> infBatchMachineBarrel = CommonDAO.GetInstance().SelfDber.Entities<InfBatchMachineBarrel>("where SampleID=:SampleID and barrelstatus=1 and datastatus=1", new { SampleID = content.SampleID });
					List<InfBeltSamplerUnloadResult> infBeltSamplerUnloadResult = CommonDAO.GetInstance().SelfDber.Entities<InfBeltSamplerUnloadResult>("where SampleCode=:SampleCode", new { SampleCode = content.SampleID });
					if (infBatchMachineBarrel.Count != infBeltSamplerUnloadResult.Count)
					{
						MessageBox.Show("合样归批里面样桶数不完整，不能倒料！", "提示");
						return;
					}
					//if (!commonDAO.VerifyComplete(content.SampleID))
					//{
					//	MessageBox.Show("当前批次采样未完成，不能倒料！", "提示");
					//	return;
					//}

					string currentMessage = string.Empty;
					InfBatchMachineCmd batchMachineCmd = new InfBatchMachineCmd();
					batchMachineCmd.InterfaceType = GlobalVars.MachineCode_HYGPJ_1;
					batchMachineCmd.MachineCode = GlobalVars.MachineCode_HYGPJ_1;
					batchMachineCmd.CmdCode = eEquInfBatchMachineCmd.倒料.ToString();
					batchMachineCmd.SampleCode = content.SampleID;// rcSampling.SampleCode;
					batchMachineCmd.ResultCode = eEquInfCmdResultCode.默认.ToString();
					batchMachineCmd.SyncFlag = 0;

					if (Dbers.GetInstance().SelfDber.Insert<InfBatchMachineCmd>(batchMachineCmd) > 0)
					{
						commonDAO.SaveOperationLog("给合样归批机发送倒料命令，采样码：" + content.SampleID, GlobalVars.LoginUser.Name);
						MessageBox.Show("命令发送成功", "提示");
					}

				}
			}));
		}
		#endregion

		/// <summary>
		/// FrmCefTester
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCefTester_Click(object sender, EventArgs e)
		{
			//this.InvokeEx(() =>
			//{
			string uniqueKey = FrmCefTester.UniqueKey;

			if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
			{
				FrmCefTester Frm = new FrmCefTester();
				FrmMainFrame.superTabControlManager.CreateTab(Frm.Text, uniqueKey, Frm, false);
			}
			else
				FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
			//});
		}

		public void SetColorTable(string controlName)
		{
			if (string.IsNullOrEmpty(controlName)) return;
			foreach (Control item in panel_Buttons.Controls)
			{
				if (item.GetType().Name != "ButtonX" && item.GetType().Name != "ButtonItem")
					continue;
				ButtonX button = (ButtonX)item;
				if (item.Name == controlName || (button.SubItems.Count > 0 && button.SubItems.Contains(controlName)))
					button.ColorTable = eButtonColor.Magenta;
				else
					button.ColorTable = eButtonColor.BlueWithBackground;
			}
		}

		/// <summary>
		/// 打开集中管控首页
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenHomePage_Click(object sender, EventArgs e)
		{
			ButtonX button = (ButtonX)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenHomePage();
		}

		/// <summary>
		/// 打开汽车过衡监控
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenTruckWeighter_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenTruckWeighter();
			SelfVars.TruckWeighterForm.CurrentMachineCode = GlobalVars.MachineCode_QC_Weighter_1;
		}
		/// <summary>
		/// 打开空车衡
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenTruckWeighter2_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenTruckWeighter();
			SelfVars.TruckWeighterForm.CurrentMachineCode = GlobalVars.MachineCode_QC_Weighter_2;
		}

		/// <summary>
		/// 打开#3汽车衡
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenTruckWeighter3_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenTruckWeighter();
			SelfVars.TruckWeighterForm.CurrentMachineCode = GlobalVars.MachineCode_QC_Weighter_3;
		}

		/// <summary>
		/// 打开火车采样机
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenTrainSampler_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenTrainSampler();
			SelfVars.TrainSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_HCJXCYJ_1;
		}
		private void btnOpenTrainSampler1_Click(object sender, EventArgs e)
		{
			ButtonX button = (ButtonX)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenTrainSampler();
			SelfVars.TrainSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_HCJXCYJ_1;
		}

		private void btnOpenTrainSampler2_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenTrainSampler();
			SelfVars.TrainSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_HCJXCYJ_2;
		}

		private void btnOpenTrainSampler3_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenTrainSampler();
			SelfVars.TrainSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_HCJXCYJ_3;
		}

		/// <summary>
		/// 打开火车采样机控制程序
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenBeltSampler_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenCarSampler();
		}


		/// <summary>
		/// 打开门禁管理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenDoor_Click(object sender, EventArgs e)
		{
			ButtonX button = (ButtonX)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenDoor();
		}
		/// <summary>
		/// 打开汽车采样机
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenCarSampler_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenCarSampler1();
			SelfVars.CarSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_QCJXCYJ_1;

		}

		private void btnOpenCarSampler2_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenCarSampler1();
			SelfVars.CarSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_QCJXCYJ_2;
		}

		/// <summary>
		/// 化验室网络
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenAssayManage_Click(object sender, EventArgs e)
		{
			ButtonX button = (ButtonX)sender;
			SetColorTable(button != null ? button.Name : "");
			OpenAssayManage();
		}

		/// <summary>
		/// 翻车机信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenCarDumper_Click(object sender, EventArgs e)
		{
			ButtonX button = (ButtonX)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenCarDumper();
		}

		/// <summary>
		/// 火车皮带采样机
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenTrainBeltSampler_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenTrainBeltSampler();
		}
		private void btnOpenTrainBeltSampler1_Click(object sender, EventArgs e)
		{
			ButtonX button = (ButtonX)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenTrainBeltSampler();
		}


		/// <summary>
		/// 全自动制样机
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenAutoMaker_Click(object sender, EventArgs e)
		{
			ButtonX button = (ButtonX)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenAutoMaker();
		}
		/// <summary>
		/// 操作日志
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenOperationLogs_Click(object sender, EventArgs e)
		{
			ButtonX button = (ButtonX)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenOperationLogs();
		}

		/// <summary>
		/// 打开气动传输存样柜监控
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenAutoCupboard_Click(object sender, EventArgs e)
		{
			ButtonX button = (ButtonX)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenAutoCupboard();
		}

		/// <summary>
		/// 打开存样柜
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenSampleCabinet_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenSampleCabinet();
		}

		/// <summary>
		///  打开存样柜取弃
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenSampleCabinetManager_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenSampleCabinetManager();
		}

		/// <summary>
		/// 打开皮带采样机报警
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenTrainBeltSampler_warning_Click(object sender, EventArgs e)
		{
			ButtonItem button = (ButtonItem)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenTrainBeltSampler_warning();
		}

		/// <summary>
		/// 打开合样归批机
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenBatchMachine_Click(object sender, EventArgs e)
		{
			ButtonX button = (ButtonX)sender;
			SetColorTable(button != null ? button.Name : "");

			OpenBatchMachine();
		}
		#endregion

		#region 设备状态监控

		/// <summary>
		/// 初始化设备状态任务
		/// </summary>
		private void InitEquipmentStatus()
		{
			timer_EquipmentStatus.Enabled = true;

			List<CmcsCMEquipment> list = commonDAO.GetChildrenMachinesByCode("机械采样机");
			CreateEquipmentStatus(list);

			// 更新设备状态
			RefreshEquipmentStatus();
		}

		/// <summary>
		/// 创建设备状态控件
		/// </summary>
		/// <param name="list"></param>
		private void CreateEquipmentStatus(List<CmcsCMEquipment> list)
		{
			flpanEquipments.SuspendLayout();

			foreach (CmcsCMEquipment cMEquipment in list)
			{
				UCtrlSignalLight uCtrlSignalLight = new UCtrlSignalLight()
				{
					Anchor = AnchorStyles.Left,
					Width = 16,
					Height = 16,
					Tag = cMEquipment.EquipmentCode,
					Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
				};
				SetSystemStatusToolTip(uCtrlSignalLight);

				flpanEquipments.Controls.Add(uCtrlSignalLight);

				LabelX lblMachineName = new LabelX()
				{
					Text = cMEquipment.EquipmentName,
					Tag = cMEquipment.EquipmentCode,
					AutoSize = true,
					Anchor = AnchorStyles.Left,
					Font = new Font("Segoe UI", 10f, FontStyle.Regular)
				};

				flpanEquipments.Controls.Add(lblMachineName);
			}

			flpanEquipments.ResumeLayout();
		}

		/// <summary>
		/// 更新设备状态
		/// </summary>
		private void RefreshEquipmentStatus()
		{
			foreach (UCtrlSignalLight uCtrlSignalLight in flpanEquipments.Controls.OfType<UCtrlSignalLight>())
			{
				if (uCtrlSignalLight.Tag == null) continue;

				string machineCode = uCtrlSignalLight.Tag.ToString();
				if (string.IsNullOrEmpty(machineCode)) continue;

				string systemStatus = CommonDAO.GetInstance().GetSignalDataValue(machineCode, eSignalDataName.系统.ToString());
				if ("|就绪待机|".Contains("|" + systemStatus + "|"))
					uCtrlSignalLight.LightColor = EquipmentStatusColors.BeReady;
				else if ("|正在运行|正在卸样|".Contains("|" + systemStatus + "|"))
					uCtrlSignalLight.LightColor = EquipmentStatusColors.Working;
				else if ("|发生故障|".Contains("|" + systemStatus + "|"))
					uCtrlSignalLight.LightColor = EquipmentStatusColors.Breakdown;
				else
					uCtrlSignalLight.LightColor = EquipmentStatusColors.Forbidden;
			}
		}

		/// <summary>
		/// 设置ToolTip提示
		/// </summary>
		private void SetSystemStatusToolTip(Control control)
		{
			this.toolTip1.SetToolTip(control, "<绿色> 就绪待机\r\n<红色> 正在运行\r\n<黄色> 发生故障");
		}

		private void timer_EquipmentStatus_Tick(object sender, EventArgs e)
		{
			// 更新设备状态
			RefreshEquipmentStatus();
		}

		#endregion

		#region 显示消息框

		/// <summary>
		/// 显示消息框
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_MsgTime_Tick(object sender, EventArgs e)
		{
			timer_MsgTime.Stop();

			if (DateTime.Now.Second % 30 == 0)
				//30秒获取一次异常信息表
				ShowEquInfHitch();
			if (DateTime.Now.Second % 5 == 0)
				ShowSysMessage();

			timer_MsgTime.Start();
		}

		/// <summary>
		/// 显示设备异常消息框
		/// </summary>
		public void ShowEquInfHitch()
		{
			List<InfEquInfHitch> listResult = CommonDAO.GetInstance().GetWarnEquInfHitch();
			StringBuilder sbHitchDescribe = new StringBuilder();
			if (listResult.Count > 0)
			{
				foreach (InfEquInfHitch item in listResult)
				{
					sbHitchDescribe.Append("<font color='red' size='2'>");
					sbHitchDescribe.Append(item.HitchTime.ToString("HH:mm") + "   " + item.HitchDescribe + "<br>");
					sbHitchDescribe.Append("</font>");
					CommonDAO.GetInstance().UpdateReadEquInfHitch(item.Id);
				}
				//右下角显示
				FrmSysMsg frm_sysMsg = new FrmSysMsg(sbHitchDescribe.ToString(), false);
				frm_sysMsg.Show();
			}
		}

		/// <summary>
		/// 显示系统消息
		/// </summary>
		public void ShowSysMessage()
		{
			CmcsSysMessage entity = CommonDAO.GetInstance().GetTodayTopSysMessage();
			if (entity != null)
			{
				//CommonDAO.GetInstance().ChangeSysMessageStatus(entity.Id, eSysMessageStatus.处理中);
				CommonDAO.GetInstance().ChangeSysMessageStatus(entity.Id, eSysMessageStatus.已处理);

				FrmSysMsg frmSysMsg = new FrmSysMsg(entity);
				frmSysMsg.OnMsgHandler += new FrmSysMsg.MsgHandler(frmSysMsg_OnMsgHandler);

			}
		}

		void frmSysMsg_OnMsgHandler(string msgId, string msgCode, string jsonStr, string buttonText, Form frmMsg)
		{
			switch (buttonText)
			{
				case "查看":

					CommonDAO.GetInstance().ChangeSysMessageStatus(msgId, eSysMessageStatus.已处理);

					switch (msgCode)
					{
						case "汽车桥式采样机":
							break;
					}

					frmMsg.Close();
					break;
				case "我知道了":
					frmMsg.Close();
					break;
				default:
					frmMsg.Close();
					break;
			}
		}
		#endregion

		/// <summary>
		/// Invoke封装
		/// </summary>
		/// <param name="action"></param>
		public void InvokeEx(Action action)
		{
			if (this.IsDisposed || !this.IsHandleCreated) return;

			this.Invoke(action);
		}

        private void btnMinimizeBox_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
