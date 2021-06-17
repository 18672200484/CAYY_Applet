using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using CMCS.Monitor.Win.Utilities;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using Xilium.CefGlue.WindowsForms;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmTrainSampler : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmTrainSampler";

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

			cefWebBrowser.StartUrl = SelfVars.Url_TrainSampler;
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

			commonDAO.SaveOperationLog(this.CurrentMachineCode+ "发送开始采样命令", GlobalVars.LoginUser.Name);
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
		private void BindSampling(SuperGridControl superGridControl, string machinecode,string samplecode)
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
				else if(s.Year>2000&&e.Year<2000)
				{
					list.Rows[i]["status"] = "正在采样";
				}
				else if(e.Year > 2000)
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
			if (status== "故障")
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
	}
}