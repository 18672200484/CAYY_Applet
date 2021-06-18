using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.CarTransport.BeltSampler.Core;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using CMCS.Monitor.Win.Utilities;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.Editors;
using Xilium.CefGlue.WindowsForms;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmTrainSampler : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmTrainSampler";
		RTxtOutputer rTxtOutputer;
		CommonDAO commonDAO = CommonDAO.GetInstance();
		MonitorCommon monitorCommon = MonitorCommon.GetInstance();

		CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

		string currentMachineCode = GlobalVars.MachineCode_HCJXCYJ_1;
		/// <summary>
		/// 当前选中的设备
		/// </summary>
		public string CurrentMachineCode
		{
			get { return currentMachineCode; }
			set { currentMachineCode = value; }
		}

		public FrmTrainSampler()
		{
			InitializeComponent();
		}

		private void FrmTruckWeighter_Load(object sender, EventArgs e)
		{
			FormInit();
			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);
			timer2.Enabled = true;
			timer3.Enabled = true;

			superGridControl2.PrimaryGrid.AutoGenerateColumns = false;
			superGridControl3.PrimaryGrid.AutoGenerateColumns = false;

		}

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void FormInit()
		{
#if DEBUG
			gboxTest.Visible = true;
#else
            gboxTest.Visible = false; 
#endif

			cefWebBrowser.StartUrl = Core.SelfVars.Url_TrainSampler;
			cefWebBrowser.Dock = DockStyle.Fill;
			cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
			cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
			panWebBrower.Controls.Add(cefWebBrowser);
		}

		void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
		{
			timer1.Enabled = true;
			RequestData();
		}

		/// <summary>
		/// 测试 - 刷新页面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			cefWebBrowser.Browser.Reload();
		}

		/// <summary>
		/// 测试 - 数据刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRequestData_Click(object sender, EventArgs e)
		{
			RequestData();
		}

		/// <summary>
		/// 请求数据
		/// </summary>
		void RequestData()
		{
			string value = string.Empty, machineCode = string.Empty;
			List<HtmlDataItem> datas = new List<HtmlDataItem>();

			datas.Clear();

			machineCode = this.CurrentMachineCode;

			datas.Add(new HtmlDataItem("当前采样机", machineCode, eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("#1采样机状态", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#2采样机状态", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#3采样机状态", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_3, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("当前设备状态", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("采样码", commonDAO.GetSignalDataValue(machineCode, "采样编码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("矿发量", commonDAO.GetSignalDataValue(machineCode, "矿发量"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("开始时间", commonDAO.GetSignalDataValue(machineCode, "开始时间"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("来煤车数", commonDAO.GetSignalDataValue(machineCode, "来煤车数"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("采样点数", commonDAO.GetSignalDataValue(machineCode, "采样点数"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("当前车号", commonDAO.GetSignalDataValue(machineCode, "当前车号"), eHtmlDataItemType.svg_text));

			//datas.Add(new HtmlDataItem("2道允许牵车", commonDAO.GetSignalDataValue(machineCode, "2道允许牵车"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("4道允许牵车", commonDAO.GetSignalDataValue(machineCode, "4道允许牵车"), eHtmlDataItemType.svg_text));

			//datas.Add(new HtmlDataItem("2道允许采样", commonDAO.GetSignalDataValue(machineCode, "2道允许采样"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("4道允许采样", commonDAO.GetSignalDataValue(machineCode, "4道允许采样"), eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("料斗", monitorCommon.ConvertStatusToColor(commonDAO.GetSignalDataValue(machineCode, "料斗")), eHtmlDataItemType.svg_color));
			string point = commonDAO.GetSignalDataValue(machineCode, "实时坐标");
			if (!string.IsNullOrEmpty(point))
			{
				string[] points = point.Split(',');
				if (points.Length == 4)
				{
					datas.Add(new HtmlDataItem("大车坐标", points[0], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("小车坐标", points[1], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("升降坐标", points[2], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("当前轨道", "", eHtmlDataItemType.svg_text));
				}
				else if (points.Length == 5)
				{
					datas.Add(new HtmlDataItem("大车坐标", points[0], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("小车坐标", points[1], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("升降坐标", points[2], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("当前轨道", points[4], eHtmlDataItemType.svg_text));

				}
			}

			datas.Add(new HtmlDataItem("前边界", commonDAO.GetSignalDataValue(machineCode, "大车前限位") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("左边界", commonDAO.GetSignalDataValue(machineCode, "小车左限位") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("上边界", commonDAO.GetSignalDataValue(machineCode, "升降上限位") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("后边界", commonDAO.GetSignalDataValue(machineCode, "大车后限位") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("右边界", commonDAO.GetSignalDataValue(machineCode, "小车右限位") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("下边界", commonDAO.GetSignalDataValue(machineCode, "升降下限位") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("打开位置", commonDAO.GetSignalDataValue(machineCode, "集料斗开到位") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("关闭位置", commonDAO.GetSignalDataValue(machineCode, "集料斗关到位") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("大车", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "大车")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("小车", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "小车")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("升降", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "升降")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("采样螺旋", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "采样头")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("集料斗", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "料斗")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("集样器", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "分矿机")), eHtmlDataItemType.svg_color));


			if (machineCode.Contains("3"))
			{
				datas.Add(new HtmlDataItem("2道允许采样", commonDAO.GetSignalDataValue(machineCode, "允许采样") == "允许采样" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("允许下降左", commonDAO.GetSignalDataValue(machineCode, "允许下降") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("检车开关左", commonDAO.GetSignalDataValue(machineCode, "车厢检测") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("2道允许牵车", commonDAO.GetSignalDataValue(machineCode, "允许牵车") == "禁止牵车" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));


				datas.Add(new HtmlDataItem("2道允许采样l", "允许采样", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("允许下降左l", "允许下降", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("检车开关左l", "检车开关", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2道允许牵车l", "禁止牵车", eHtmlDataItemType.svg_text));

				datas.Add(new HtmlDataItem("4道允许采样", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4道允许采样l", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("允许下降右", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("允许下降右l", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("检车开关右", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("检车开关右l", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4道允许牵车", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4道允许牵车l", "false", eHtmlDataItemType.svg_visible));
			}
			else
			{
				datas.Add(new HtmlDataItem("2道允许采样", commonDAO.GetSignalDataValue(machineCode, "2道允许采样") == "允许采样" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("4道允许采样", commonDAO.GetSignalDataValue(machineCode, "4道允许采样") == "允许采样" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

				datas.Add(new HtmlDataItem("允许下降左", commonDAO.GetSignalDataValue(machineCode, "左道允许下降") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("允许下降右", commonDAO.GetSignalDataValue(machineCode, "右道允许下降") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

				datas.Add(new HtmlDataItem("检车开关左", commonDAO.GetSignalDataValue(machineCode, "左道检测车厢") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("检车开关右", commonDAO.GetSignalDataValue(machineCode, "右道检测车厢") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

				datas.Add(new HtmlDataItem("2道允许牵车", commonDAO.GetSignalDataValue(machineCode, "2道允许牵车") == "禁止牵车" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("4道允许牵车", commonDAO.GetSignalDataValue(machineCode, "4道允许牵车") == "禁止牵车" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));

				datas.Add(new HtmlDataItem("2道允许采样l", "2道允许采样", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("允许下降左l", "允许下降左", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("检车开关左l", "检车开关左", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2道允许牵车l", "2道禁止牵车", eHtmlDataItemType.svg_text));

				datas.Add(new HtmlDataItem("4道允许采样", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4道允许采样l", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("允许下降右", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("允许下降右l", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("检车开关右", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("检车开关右l", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4道允许牵车", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4道允许牵车l", "true", eHtmlDataItemType.svg_visible));


			}

			// 添加更多...

			// 发送到页面
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

			BindSampling(superGridControl1, machineCode, commonDAO.GetSignalDataValue(machineCode, "采样编码"));
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			// 界面不可见时，停止发送数据
			if (!this.Visible) return;

			RequestData();
		}

		private void buttonX1_Click(object sender, EventArgs e)
		{
			// 发送到页面
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("test1();", "", 0);
		}

		private void btnStartSampler_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.开始采样)) { MessageBoxEx.Show("开始采样命令发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("开始采样命令发送成功，等待执行");

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "发送开始采样命令", GlobalVars.LoginUser.Name);
		}

		private void btnEndSampler_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.系统暂停)) { MessageBoxEx.Show("系统暂停命令发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("系统暂停命令发送成功，等待执行");

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "发送系统暂停命令", GlobalVars.LoginUser.Name);
		}

		private void btnSystemReset_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.系统复位)) { MessageBoxEx.Show("系统复位命令发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("系统复位命令发送成功，等待执行");

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "发送系统复位命令", GlobalVars.LoginUser.Name);
		}

		private void btnErrorReset_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.故障复位)) { MessageBoxEx.Show("故障复位命令发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("故障复位命令发送成功，等待执行");

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "发送故障复位命令", GlobalVars.LoginUser.Name);
		}

		private void btnChangeTrain_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.切换轨道)) { MessageBoxEx.Show("切换轨道命令发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("切换轨道命令发送成功，等待执行");

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "发送切换轨道命令", GlobalVars.LoginUser.Name);
		}

		private void btnHeadTailSection_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.首尾车)) { MessageBoxEx.Show("首/尾车命令发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("首/尾车命令发送成功，等待执行");

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "发送首/尾车命令", GlobalVars.LoginUser.Name);
		}

		/// <summary>
		/// 发送采样命令
		/// </summary>
		/// <returns></returns>
		bool SendSamplingCMD(eEquInfSamplerCmd cmd)
		{
			CmcsCMEquipment Equipment = CommonDAO.GetInstance().GetCMEquipmentByMachineCode(this.CurrentMachineCode);
			InfBeltSampleCmd CurrentSampleCMD = new InfBeltSampleCmd
			{
				DataFlag = 0,
				InterfaceType = Equipment.InterfaceType,
				MachineCode = Equipment.EquipmentCode,
				ResultCode = eEquInfCmdResultCode.默认.ToString(),
				SampleCode = commonDAO.GetSignalDataValue(this.CurrentMachineCode, "采样编码"),
				CmdCode = cmd.ToString()
			};
			if (Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd>(CurrentSampleCMD) > 0)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 绑定采样信息
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="machineCode">设备编码</param>
		private void BindSampling(SuperGridControl superGridControl, string machinecode, string samplecode)
		{
			string sql = string.Format(@"SELECT T1.SAMPLECODE,T2.CARNUMBER,T2.CARMODEL,T2.ORDERNUMBER,T2.CYCOUNT,T1.TRAINCODE,T2.STARTTIME,T2.ENDTIME,'' STATUS FROM INFTBBELTSAMPLEPLANDETAIL T2 LEFT JOIN INFTBBELTSAMPLEPLAN T1 ON T2.PLANID=T1.ID
									WHERE T1.MACHINECODE='{0}'  AND T1.SAMPLECODE='{1}' ORDER BY T2.ORDERNUMBER
										", machinecode, samplecode);
			DataTable list = commonDAO.SelfDber.ExecuteDataTable(sql);
			for (int i = 0; i < list.Rows.Count; i++)
			{
				DateTime s = Convert.ToDateTime(list.Rows[i]["starttime"]);
				DateTime e = Convert.ToDateTime(list.Rows[i]["endtime"]);
				if (s.Year < 2000)
				{
					list.Rows[i]["status"] = "待采样";
				}
				else if (s.Year > 2000 && e.Year < 2000)
				{
					list.Rows[i]["status"] = "正在采样";
				}
				else if (e.Year > 2000)
				{
					list.Rows[i]["status"] = "已采样";
				}
			}
			superGridControl.PrimaryGrid.DataSource = list;
		}

		/// <summary>
		/// 转换设备状态为颜色值
		/// </summary>
		/// <param name="systemStatus">系统状态</param>
		/// <returns></returns>
		public string ConvertMachineToColor(string status)
		{
			if (status == "故障")
				return "#ffff00";
			else if (status == "运行")
				return "#ff0000";
			else
				return "#c8c8c8";
		}

		/// <summary>
		/// 显示历史故障
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFaultRecord_Click(object sender, EventArgs e)
		{
			FrmWarningInfo frm = new FrmWarningInfo(this.CurrentMachineCode);
			frm.ShowDialog();
		}

		#region 采样远程操作

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
					lblFuelKindName.Text = value.FuelKindName;
				}
				else
				{
					lblBatch.Text = "####";
					lblFactarriveDate.Text = "####";
					lblSupplierName.Text = "####";
					lblFuelKindName.Text = "####";
				}
			}
		}

		eTrainSampleFlowFlag currentFlowFlag = eTrainSampleFlowFlag.发送计划;
		/// <summary>
		/// 当前业务流程标识
		/// </summary>
		public eTrainSampleFlowFlag CurrentFlowFlag
		{
			get { return currentFlowFlag; }
			set
			{
				currentFlowFlag = value;
				panCurrentCarNumber.Text = value.ToString();
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

		eEquInfCmdResultCode currentCmdResultCode = eEquInfCmdResultCode.默认;
		/// <summary>
		/// 当前命令执行结果 
		/// </summary>
		public eEquInfCmdResultCode CurrentCmdResultCode
		{
			get { return currentCmdResultCode; }
			set
			{
				currentCmdResultCode = value;

				lblResult.Text = currentCmdResultCode.ToString();
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

		#region 公共业务
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer2_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			timer1.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag)
				{
					case eTrainSampleFlowFlag.等待执行:

						if (!IsResultSample)
						{
							CurrentCmdResultCode = BeltSamplerDAO.GetInstance().GetSampleCmdResult(CurrentSampleCMD.Id);
							if (CurrentCmdResultCode == eEquInfCmdResultCode.成功)
							{
								this.rTxtOutputer.Output("采样命令执行成功", eOutputType.Success);
								this.CurrentFlowFlag = eTrainSampleFlowFlag.执行完毕;
							}
							else if (CurrentCmdResultCode == eEquInfCmdResultCode.失败)
							{
								this.rTxtOutputer.Output("采样命令执行失败", eOutputType.Warn);
								List<InfEquInfHitch> list_Hitch = commonDAO.GetEquInfHitch(DateTime.Now.AddMinutes(-1), DateTime.Now.AddMinutes(1), this.CurrentMachineCode);
								foreach (InfEquInfHitch item in list_Hitch)
								{
									this.rTxtOutputer.Output("采样机:" + item.HitchDescribe, eOutputType.Error);
								}
							}
							IsResultSample = CurrentCmdResultCode != eEquInfCmdResultCode.默认;
						}

						break;
					case eTrainSampleFlowFlag.执行完毕:
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

		private void timer3_Tick(object sender, EventArgs e)
		{
			timer2.Stop();
			// 2秒执行一次
			timer2.Interval = 2000;

			try
			{
				CreateEquStatus();
				RefreshEquStatus();
				BindBeltSampleBarrel(superGridControl3, this.CurrentMachineCode);
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
		/// 发送采样计划
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSendSamplePlan_Click(object sender, EventArgs e)
		{
			if (CurrentRCSampling == null) { MessageBoxEx.Show("请先设置当前采样单"); return; }
			if (this.CurrentMachineCode != GlobalVars.MachineCode_HCJXCYJ_3 && cmbTrainCode.SelectedItem == null) { MessageBoxEx.Show("请先选择轨道编号"); return; }
			//if((int)cmbCyCount.Value == 0){ MessageBoxEx.Show("采样点数必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
			if (!SendSamplingPlan()) { MessageBoxEx.Show("采样计划发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("采样计划发送成功");
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
		/// 发送采样计划
		/// </summary>
		/// <returns></returns>
		bool SendSamplingPlan()
		{
			InfBeltSamplePlan oldBeltSamplePlan = Dbers.GetInstance().SelfDber.Entity<InfBeltSamplePlan>("where InFactoryBatchId=:InFactoryBatchId and SampleCode=:SampleCode and MachineCode=:MachineCode", new { InFactoryBatchId = this.CurrentRCSampling.BatchId, SampleCode = this.CurrentRCSampling.SampleCode, MachineCode = this.CurrentMachineCode });
			if (oldBeltSamplePlan == null)
			{
				oldBeltSamplePlan = new InfBeltSamplePlan();
				oldBeltSamplePlan.DataFlag = 0;
				oldBeltSamplePlan.InterfaceType = GlobalVars.InterfaceType_HCJXCYJ;
				oldBeltSamplePlan.InFactoryBatchId = this.CurrentRCSampling.BatchId;
				oldBeltSamplePlan.SampleCode = this.CurrentRCSampling.SampleCode;
				oldBeltSamplePlan.FuelKindName = this.CurrentRCSampling.FuelKindName;
				oldBeltSamplePlan.Mt = 0;
				oldBeltSamplePlan.TicketWeight = 0;
				oldBeltSamplePlan.GatherType = "样桶";
				oldBeltSamplePlan.TrainCode = ((ComboItem)cmbTrainCode.SelectedItem) != null ? ((ComboItem)cmbTrainCode.SelectedItem).Text : "";
				oldBeltSamplePlan.SampleType = CurrentRCSampling.SamplingType;
				oldBeltSamplePlan.MachineCode = this.CurrentMachineCode;
				oldBeltSamplePlan.CarCount = this.CurrentRCSampling.TransportNumber;
				//oldBeltSamplePlan.TrainCode = "#2";
				if (oldBeltSamplePlan.SampleType == eSamplingType.机械采样.ToString() || oldBeltSamplePlan.SampleType == eSamplingType.皮带采样.ToString())
				{
					IList<CmcsTransport> transports = commonDAO.SelfDber.Entities<CmcsTransport>("where InFactoryBatchId=:InFactoryBatchId order by OrderNumber", new { InFactoryBatchId = CurrentRCSampling.BatchId });
					foreach (CmcsTransport item in transports)
					{
						InfBeltSamplePlanDetail samplePlanDetail = new InfBeltSamplePlanDetail();
						samplePlanDetail.PlanId = oldBeltSamplePlan.Id;
						samplePlanDetail.MchineCode = this.CurrentMachineCode;
						samplePlanDetail.CarNumber = item.TransportNo;
						samplePlanDetail.OrderNumber = item.OrderNumber;
						samplePlanDetail.SyncFlag = 0;
						samplePlanDetail.CarModel = item.TrainType;
						samplePlanDetail.CyCount = Convert.ToInt32(this.cmbCYCount.Text);//(int)dbi_CyCount.Value;
																						 //samplePlanDetail.TrainCode = "#2";
						Dbers.GetInstance().SelfDber.Insert<InfBeltSamplePlanDetail>(samplePlanDetail);
					}
					commonDAO.SetSignalDataValue(this.CurrentMachineCode, eSignalDataName.矿发量.ToString(), transports.Sum(a => a.TicketQty).ToString());
				}
				commonDAO.SetSignalDataValue(this.CurrentMachineCode, eSignalDataName.采样编码.ToString(), this.CurrentRCSampling.SampleCode);

				return Dbers.GetInstance().SelfDber.Insert<InfBeltSamplePlan>(oldBeltSamplePlan) > 0;
			}
			else
			{
				oldBeltSamplePlan.DataFlag = 0;
				oldBeltSamplePlan.FuelKindName = this.CurrentRCSampling.FuelKindName;
				oldBeltSamplePlan.Mt = 0;
				oldBeltSamplePlan.TicketWeight = 0;
				oldBeltSamplePlan.SampleType = CurrentRCSampling.SamplingType;
				oldBeltSamplePlan.MachineCode = this.CurrentMachineCode;
				oldBeltSamplePlan.CarCount = this.CurrentRCSampling.TransportNumber;
				oldBeltSamplePlan.TrainCode = ((ComboItem)cmbTrainCode.SelectedItem) != null ? ((ComboItem)cmbTrainCode.SelectedItem).Text : "";
				oldBeltSamplePlan.SyncFlag = 0;
				if (oldBeltSamplePlan.SampleType == eSamplingType.机械采样.ToString() || oldBeltSamplePlan.SampleType == eSamplingType.皮带采样.ToString())
				{
					IList<CmcsTransport> transports = commonDAO.SelfDber.Entities<CmcsTransport>("where InFactoryBatchId=:InFactoryBatchId order by OrderNumber", new { InFactoryBatchId = CurrentRCSampling.BatchId });
					foreach (CmcsTransport item in transports)
					{
						InfBeltSamplePlanDetail samplePlanDetail = commonDAO.SelfDber.Entity<InfBeltSamplePlanDetail>("where PlanId=:PlanId and CarNumber=:CarNumber order by OrderNumber", new { PlanId = oldBeltSamplePlan.Id, CarNumber = item.TransportNo });
						if (samplePlanDetail == null)
						{
							samplePlanDetail = new InfBeltSamplePlanDetail();
							samplePlanDetail.PlanId = oldBeltSamplePlan.Id;
							samplePlanDetail.MchineCode = this.CurrentMachineCode;
							samplePlanDetail.CarNumber = item.TransportNo;
							samplePlanDetail.OrderNumber = item.OrderNumber;
							samplePlanDetail.SyncFlag = 0;
							samplePlanDetail.CarModel = item.TrainType;
							samplePlanDetail.CyCount = Convert.ToInt32(this.cmbCYCount.Text);// (int)dbi_CyCount.Value;
																							 //samplePlanDetail.TrainCode = "#2";
							Dbers.GetInstance().SelfDber.Insert<InfBeltSamplePlanDetail>(samplePlanDetail);
						}
					}
				}
				return Dbers.GetInstance().SelfDber.Update(oldBeltSamplePlan) > 0;
			}
		}

		/// <summary>
		/// 重置入厂煤运输记录
		/// </summary>
		void ResetBuyFuel()
		{
			this.CurrentFlowFlag = eTrainSampleFlowFlag.选择计划;
			this.CurrentCmdResultCode = eEquInfCmdResultCode.默认;
			this.CurrentSampleCMD = null;
			//this.CurrentRCSampling = null;
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
		/// 创建皮带采样机
		/// </summary>
		private void CreateEquStatus()
		{
			flpanEquState.SuspendLayout();
			flpanEquState.Controls.Clear();
			LabelX lblMachineName = new LabelX()
			{
				Text = this.CurrentMachineCode,
				AutoSize = true,
				Anchor = AnchorStyles.Left,
				Font = new Font("Segoe UI", 14.25f, FontStyle.Bold)
			};

			flpanEquState.Controls.Add(lblMachineName);

			LabelX uCtrlSignalLight = new LabelX()
			{
				Tag = this.CurrentMachineCode,
				AutoSize = true,
				Anchor = AnchorStyles.Left,
				Font = new Font("Segoe UI", 14.25f, FontStyle.Bold),
				Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
			};
			SetSystemStatusToolTip(uCtrlSignalLight);
			flpanEquState.Controls.Add(uCtrlSignalLight);

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
				string systemStatus = CommonDAO.GetInstance().GetSignalDataValue(machineCode, eSignalDataName.设备状态.ToString());
				uCtrlSignalLight.Text = systemStatus;
				if (systemStatus == eEquInfSamplerSystemStatus.就绪待机.ToString())
					uCtrlSignalLight.BackColor = EquipmentStatusColors.BeReady;
				else if (systemStatus == eEquInfSamplerSystemStatus.正在运行.ToString() || systemStatus == eEquInfSamplerSystemStatus.正在卸样.ToString())
					uCtrlSignalLight.BackColor = EquipmentStatusColors.Working;
				else if (systemStatus == eEquInfSamplerSystemStatus.发生故障.ToString())
					uCtrlSignalLight.BackColor = EquipmentStatusColors.Breakdown;
				else if (systemStatus == eEquInfSamplerSystemStatus.系统停止.ToString())
					uCtrlSignalLight.BackColor = EquipmentStatusColors.Forbidden;

				eEquInfSamplerSystemStatus status;

				if (Enum.TryParse(systemStatus, out status))
					CurrentSystemStatus = status;
			}
		}

		/// <summary>
		/// 设置ToolTip提示
		/// </summary>
		private void SetSystemStatusToolTip(Control control)
		{
			this.toolTip1.SetToolTip(control, "<绿色> 就绪待机\r\n<红色> 正在运行\r\n<黄色> 发生故障\r\n<灰色> 系统停止");
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
			List<View_RCSampling> list = commonDAO.SelfDber.Entities<View_RCSampling>(string.Format("where BatchType='火车' and SamplingType!='人工采样' and to_char(SamplingDate,'yyyy-MM-dd hh24:mm:ss')>='{0}' order by SamplingDate desc", DateTime.Now.AddDays(-3).Date.ToString("yyyy-MM-dd")));
			superGridControl.PrimaryGrid.DataSource = list;
		}

		#endregion
	}
}

/// <summary>
/// 流程标识
/// </summary>
public enum eTrainSampleFlowFlag
{
	选择计划,
	发送计划,
	等待执行,
	执行完毕
}
