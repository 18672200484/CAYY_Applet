using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Enums;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using CMCS.Monitor.Win.Utilities;
using DevComponents.DotNetBar;
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

			datas.Add(new HtmlDataItem("汽车衡_当前衡器", machineCode, eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#1采样机状态", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2采样机状态", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#3采样机状态", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_3, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("当前设备状态", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("采样码", commonDAO.GetSignalDataValue(machineCode, "采样码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("矿发量", commonDAO.GetSignalDataValue(machineCode, "矿发量"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("开始时间", commonDAO.GetSignalDataValue(machineCode, "开始时间"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("来煤车数", commonDAO.GetSignalDataValue(machineCode, "来煤车数"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("采样点数", commonDAO.GetSignalDataValue(machineCode, "采样点数"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("当前车号", commonDAO.GetSignalDataValue(machineCode, "当前车号"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("2道允许牵车", commonDAO.GetSignalDataValue(machineCode, "2道允许牵车"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("4道允许牵车", commonDAO.GetSignalDataValue(machineCode, "4道允许牵车"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("料斗", monitorCommon.ConvertStatusToColor(commonDAO.GetSignalDataValue(machineCode, "料斗")), eHtmlDataItemType.svg_color));
			string point = commonDAO.GetSignalDataValue(machineCode, "实时坐标");
			if (!string.IsNullOrEmpty(point))
			{
				string[] points = point.Split(',');
				if (points.Length == 3)
				{
					datas.Add(new HtmlDataItem("大车坐标", points[0], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("小车坐标", points[1], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("升降坐标", points[2], eHtmlDataItemType.svg_text));
				}
			}
			// 添加更多...

			// 发送到页面
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
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

	}
}