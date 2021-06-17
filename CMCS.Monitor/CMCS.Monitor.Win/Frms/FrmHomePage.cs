using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Enums;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using DevComponents.DotNetBar;
using Xilium.CefGlue;
using Xilium.CefGlue.WindowsForms;
using CMCS.Monitor.Win.Utilities;
using CMCS.Monitor.Win.CefGlue;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Utilities;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmHomePage : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmHomePage";

		CommonDAO commonDAO = CommonDAO.GetInstance();
		MonitorCommon monitorCommon = MonitorCommon.GetInstance();

		CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

		public FrmHomePage()
		{
			InitializeComponent();
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
			cefWebBrowser.StartUrl = SelfVars.Url_HomePage;
			cefWebBrowser.Dock = DockStyle.Fill;
			cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
			cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
			panWebBrower.Controls.Add(cefWebBrowser);
		}

		void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
		{
			timer1.Enabled = true;
			timer1.Interval = 3000;

            RequestData();
        }

		private void FrmHomePage_Load(object sender, EventArgs e)
		{
			FormInit();
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
			//datas.Add(new HtmlDataItem("火车入厂车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.火车入厂车数.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("火车翻车车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.火车翻车车数.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("火车出厂车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.火车出厂车数.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("#1翻车机已翻车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.已翻车数.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("#2翻车机已翻车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.已翻车数.ToString()), eHtmlDataItemType.svg_text));
			//string CarNumber_1 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.当前车号.ToString());
			//string CarNumber_2 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.当前车号.ToString());
			//datas.Add(new HtmlDataItem("#1翻车机当前车号", CarNumber_1, eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("#2翻车机当前车号", CarNumber_2, eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("#1翻车机", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.当前车号.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#2翻车机", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.当前车号.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#4车号识别", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.当前车号.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#5车号识别", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.当前车号.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));

			//datas.Add(new HtmlDataItem("来船量", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.来船量.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("汽车转运车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车转运车数.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("汽车入厂车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车入厂车数.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("汽车采样称重车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车采样称重车数.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("汽车回皮车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车回皮车数.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("汽车出厂车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车出厂车数.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("制样合批数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.制样合批数.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("存样数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.存样数.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("待化验数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.待化验数.ToString()), eHtmlDataItemType.svg_text));

			//datas.Add(new HtmlDataItem("制样机_制样数量", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "制样机_制样数量"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("制样机_制样编码", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "制样机_制样编码"), eHtmlDataItemType.svg_text));

			#region 批次 采制化
			//if (!string.IsNullOrEmpty(CarNumber_1))
			//{
			//	BindBatch(CarNumber_1, "#2", datas);
			//}
			//if (!string.IsNullOrEmpty(CarNumber_1))
			//{
			//	BindBatch(CarNumber_2, "#4", datas);
			//}
			#endregion

			#region 汽车采样机

			machineCode = GlobalVars.MachineCode_QC_JxSampler_1;
			//datas.Add(new HtmlDataItem("汽车_1号采样_系统", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("汽车_1号采样_车号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("汽车_1号采样_道闸1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸1升杆.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("汽车_1号采样_道闸2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸2升杆.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#1汽采", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#1汽采a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color1));

			machineCode = GlobalVars.MachineCode_QC_JxSampler_2;
			//datas.Add(new HtmlDataItem("汽车_2号采样_系统", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("汽车_2号采样_车号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("汽车_2号采样_道闸1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸1升杆.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("汽车_2号采样_道闸2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸2升杆.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2汽采", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#2汽采a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color1));

			#endregion

			#region 汽车衡

			machineCode = GlobalVars.MachineCode_QC_Weighter_1;
			////datas.Add(new HtmlDataItem("#1磅系统", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("#1磅当前车号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("#1磅当前重量", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.地磅仪表_实时重量.ToString() + "t"), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("汽车_1号衡_道闸1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸1升杆.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("汽车_1号衡_道闸2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸2升杆.ToString())), eHtmlDataItemType.svg_color));
			////AddDataItemBySignal(datas, machineCode, "汽车_1号衡_红绿灯");
			//datas.Add(new HtmlDataItem("#1地磅", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));


			machineCode = GlobalVars.MachineCode_QC_Weighter_2;
			////datas.Add(new HtmlDataItem("#2磅系统", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("#2磅当前车号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("#2磅当前重量", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.地磅仪表_实时重量.ToString() + "t"), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("汽车_2号衡_道闸1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸1升杆.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("汽车_2号衡_道闸2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸2升杆.ToString())), eHtmlDataItemType.svg_color));
			////AddDataItemBySignal(datas, machineCode, "汽车_2号衡_红绿灯");
			//datas.Add(new HtmlDataItem("重车磅", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

			machineCode = GlobalVars.MachineCode_QC_Weighter_3;
			////datas.Add(new HtmlDataItem("#3磅系统", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("#3磅当前车号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("#3磅当前重量", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.地磅仪表_实时重量.ToString() + "t"), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("汽车_3号衡_道闸1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸1升杆.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("汽车_3号衡_道闸2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸2升杆.ToString())), eHtmlDataItemType.svg_color));
			////AddDataItemBySignal(datas, machineCode, "汽车_3号衡_红绿灯");
			//datas.Add(new HtmlDataItem("#3地磅", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			#endregion

			//datas.Add(new HtmlDataItem("门禁_制样室进", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "门禁_制样室进"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("门禁_化验室进", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "门禁_化验室进"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("门禁_集控室进", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "门禁_集控室进"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("门禁_办公楼进", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "门禁_办公楼进"), eHtmlDataItemType.svg_text));

			//车号识别
			
			datas.Add(new HtmlDataItem("#1车号识别", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest(CommonDAO.GetInstance().GetCommonAppletConfigString("#1车号识别IP")) ? "1" : "0"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2车号识别", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest(CommonDAO.GetInstance().GetCommonAppletConfigString("#2车号识别IP")) ? "1" : "0"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#3车号识别", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest(CommonDAO.GetInstance().GetCommonAppletConfigString("#3车号识别IP")) ? "1" : "0"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#4车号识别", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest(CommonDAO.GetInstance().GetCommonAppletConfigString("#4车号识别IP")) ? "1" : "0"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#5车号识别", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest(CommonDAO.GetInstance().GetCommonAppletConfigString("#5车号识别IP")) ? "1" : "0"), eHtmlDataItemType.svg_color));

			//火车采样
			datas.Add(new HtmlDataItem("#1火采", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#2火采", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color1));
			//datas.Add(new HtmlDataItem("#3火采", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_3, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#3火采", monitorCommon.ConvertMachineStatusToColor("就绪待机"), eHtmlDataItemType.svg_color1));

			datas.Add(new HtmlDataItem("#1火采a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#2火采a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color1));
			//datas.Add(new HtmlDataItem("#3火采a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_3, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#3火采a", monitorCommon.ConvertMachineStatusToColor("就绪待机"), eHtmlDataItemType.svg_color1));

			//皮采
			datas.Add(new HtmlDataItem("2PA皮采", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("2PA皮采a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("2PB皮采", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_2, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("2PB皮采a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_2, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color1));

			if (commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_1, "采样机报警") == "1")
			{
				datas.Add(new HtmlDataItem("2PA皮采", "#ff0000" , eHtmlDataItemType.svg_color1));
				datas.Add(new HtmlDataItem("2PA皮采a", "#ff0000", eHtmlDataItemType.svg_color1));
			}
			if (commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_2, "采样机报警") == "1")
			{
				datas.Add(new HtmlDataItem("2PB皮采", "#ff0000", eHtmlDataItemType.svg_color1));
				datas.Add(new HtmlDataItem("2PB皮采a", "#ff0000", eHtmlDataItemType.svg_color1));
			}

				//轨道衡、
				datas.Add(new HtmlDataItem("#1轨道衡", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest("192.168.70.21") ? "1" : "0"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2轨道衡", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest("192.168.70.22") ? "1" : "0"), eHtmlDataItemType.svg_color));

			//合样归批
			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "小车端.车辆报警");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("车辆状态", "#ff0000", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("车辆状态", "报警", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("车辆状态", "#00ff00", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("车辆状态", "正常", eHtmlDataItemType.svg_text));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "装车端.装车端报警");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("装车端状态", "#ff0000", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("装车端状态", "报警", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("装车端状态", "#00ff00", eHtmlDataItemType.svg_color));

				string zcd1 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "装车端.装车端_车辆交互流程运行");
				string zcd2 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "装车端.装车端_满桶转存流程运行");
				if (zcd1 == "1" || zcd2 == "1")
				{
					datas.Add(new HtmlDataItem("装车端状态", "运行", eHtmlDataItemType.svg_text));
				}
				else
				{
					datas.Add(new HtmlDataItem("装车端状态", "未运行", eHtmlDataItemType.svg_text));
				}

				//atas.Add(new HtmlDataItem("装车端状态", "正常", eHtmlDataItemType.svg_text));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "卸车端.卸车端报警");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("卸车端状态", "#ff0000", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("卸车端状态", "报警", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("卸车端状态", "#00ff00", eHtmlDataItemType.svg_color));

				value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "卸车端.卸车端_车辆交互流程运行");
				if (value == "1")
				{
					datas.Add(new HtmlDataItem("卸车端状态", "运行", eHtmlDataItemType.svg_text));
				}
				else
				{
					datas.Add(new HtmlDataItem("卸车端状态", "未运行", eHtmlDataItemType.svg_text));
				}

				//datas.Add(new HtmlDataItem("卸车端状态", "正常", eHtmlDataItemType.svg_text));
			}


			//样桶信息
			List<InfBatchMachineBarrel> barrel = Dbers.GetInstance().SelfDber.Entities<InfBatchMachineBarrel>();
			foreach (InfBatchMachineBarrel item in barrel)
			{
				if (item.BarrelStatus == 1 && item.DataStatus == 1)
				{
					datas.Add(new HtmlDataItem(item.BarrelStation + "_" + item.BarrelCode, "1", eHtmlDataItemType.svg_visible));
				}
				else
				{
					datas.Add(new HtmlDataItem(item.BarrelStation + "_" + item.BarrelCode, "0", eHtmlDataItemType.svg_visible));
				}
			}

			#region 新首页取数
			DateTime dtime = DateTime.Now;
			//异常信息
			List<InfEquInfHitch> infHitches = Dbers.GetInstance().SelfDber.TopEntities<InfEquInfHitch>(3, " order by HitchTime desc");
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadHitchs(" + Newtonsoft.Json.JsonConvert.SerializeObject(infHitches.Select(a => new { machineCode = a.MachineCode, abnormalTime = a.HitchTime.Year < 2000 ? "" : a.HitchTime.ToString("yyyy-MM-dd HH:mm"), abnormalInfo = a.HitchDescribe })) + ");", "", 0);

			//当日火车信息
			string sql = string.Format(@"select a.transportno,a.grossqty,a.skinqty,a.suttleqty,a.marginqty from fultbtransport a left join fultbinfactorybatch b on a.infactorybatchid=b.id 
									 where b.transporttypename='火车' and to_char(a.infactorytime,'yyyy-MM-dd')='{0}' and a.taredate is not null
									 order by a.taredate desc", dtime.ToString("yyyy-MM-dd"));
			DataTable dt = Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
			List<HomePageTemp> list = new List<HomePageTemp>();
			if (dt.Rows.Count > 7)
			{
				for (int i = 0; i < 7; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = dt.Rows[i]["transportno"].ToString();
					item.grossqty = Convert.ToDecimal(dt.Rows[i]["grossqty"]).ToString("F2");
					item.skinqty = Convert.ToDecimal(dt.Rows[i]["skinqty"]).ToString("F2");
					item.suttleqty = Convert.ToDecimal(dt.Rows[i]["suttleqty"]).ToString("F2");
					item.marginqty = Convert.ToDecimal(dt.Rows[i]["marginqty"]).ToString("F2");
					list.Add(item);
				}
			}
			else
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = dt.Rows[i]["transportno"].ToString();
					item.grossqty = Convert.ToDecimal(dt.Rows[i]["grossqty"]).ToString("F2");
					item.skinqty = Convert.ToDecimal(dt.Rows[i]["skinqty"]).ToString("F2");
					item.suttleqty = Convert.ToDecimal(dt.Rows[i]["suttleqty"]).ToString("F2");
					item.marginqty = Convert.ToDecimal(dt.Rows[i]["marginqty"]).ToString("F2");
					list.Add(item);
				}
				for (int i = dt.Rows.Count; i < 7; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = "";
					item.grossqty = "";
					item.skinqty = "";
					item.suttleqty = "";
					item.marginqty = "";
					list.Add(item);
				}
			}
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadHCInfo(" + Newtonsoft.Json.JsonConvert.SerializeObject(list) + ");", "", 0);

			//当日汽车信息
			string sql1 = string.Format(@"select a.transportno,a.grossqty,a.skinqty,a.suttleqty,c.GROSSPLACE from fultbtransport a left join fultbinfactorybatch b on a.infactorybatchid=b.id 
										left join cmcstbbuyfueltransport c on a.pkid=c.id
										 where b.transporttypename='汽车'and to_char(a.infactorytime,'yyyy-MM-dd')='{0}' and a.taredate is not null
										 order by a.taredate desc
										 ", dtime.ToString("yyyy-MM-dd"));
			DataTable dt1 = Dbers.GetInstance().SelfDber.ExecuteDataTable(sql1);
			List<HomePageTemp> list1 = new List<HomePageTemp>();
			if (dt1.Rows.Count > 7)
			{
				for (int i = 0; i < 7; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = dt1.Rows[i]["transportno"].ToString();
					item.grossqty = Convert.ToDecimal(dt1.Rows[i]["grossqty"]).ToString("F2");
					item.skinqty = Convert.ToDecimal(dt1.Rows[i]["skinqty"].ToString()).ToString("F2");
					item.suttleqty = Convert.ToDecimal(dt1.Rows[i]["suttleqty"]).ToString("F2");
					if (dt1.Rows[i]["GROSSPLACE"] != DBNull.Value)
					{
						item.grossplace = dt1.Rows[i]["GROSSPLACE"].ToString().Contains("#1") ? "#1磅" : "#2磅";
					}

					list1.Add(item);
				}
			}
			else
			{
				for (int i = 0; i < dt1.Rows.Count; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = dt1.Rows[i]["transportno"].ToString();
					item.grossqty = Convert.ToDecimal(dt1.Rows[i]["grossqty"]).ToString("F2");
					item.skinqty = Convert.ToDecimal(dt1.Rows[i]["skinqty"]).ToString("F2");
					item.suttleqty = Convert.ToDecimal(dt1.Rows[i]["suttleqty"]).ToString("F2");
					if (dt1.Rows[i]["GROSSPLACE"] != DBNull.Value)
					{
						item.grossplace = dt1.Rows[i]["GROSSPLACE"].ToString().Contains("#1") ? "#1磅" : "#2磅";
					}
					list1.Add(item);
				}
				for (int i = dt1.Rows.Count; i < 7; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = "";
					item.grossqty = "";
					item.skinqty = "";
					item.suttleqty = "";
					item.grossplace = "";
					list1.Add(item);
				}
			}
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadQCInfo(" + Newtonsoft.Json.JsonConvert.SerializeObject(list1) + ");", "", 0);

			//存样柜信息
			datas.Add(new HtmlDataItem("存查样柜1_共有仓位", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "共有仓位"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("存查样柜1_已存仓位", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "已存仓位"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("存查样柜1_未存仓位", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "未存仓位"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("存查样柜1_存样率", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "存样率"), eHtmlDataItemType.svg_text));


			datas.Add(new HtmlDataItem("存查样柜1_大瓶共有", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "大瓶仓位"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("存查样柜1_大瓶已存", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "大瓶已存仓位"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("存查样柜1_中瓶共有", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "中瓶仓位"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("存查样柜1_中瓶已存", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "中瓶已存仓位"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("存查样柜1_小瓶共有", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "小瓶仓位"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("存查样柜1_小瓶已存", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "小瓶已存仓位"), eHtmlDataItemType.svg_text));

			//采样信息
			string beltsamplecode1 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "采样编码");
			if(VerifyComplete(GlobalVars.MachineCode_TrunOver_1, beltsamplecode1))
			{
				datas.Add(new HtmlDataItem("2PA皮采_采样编码", "", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PA皮采_计划数", "0", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PA皮采_已采数", "0", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("2PA皮采_采样编码", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "采样编码"), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PA皮采_计划数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "翻车机车数"), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PA皮采_已采数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_1, "已翻车车数"), eHtmlDataItemType.svg_text));
			}

			string beltsamplecode2 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "采样编码");
			if (VerifyComplete(GlobalVars.MachineCode_TrunOver_2, beltsamplecode2))
			{

				datas.Add(new HtmlDataItem("2PB皮采_采样编码", "", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PB皮采_计划数", "0", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PB皮采_已采数", "0", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("2PB皮采_采样编码", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "采样编码"), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PB皮采_计划数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "翻车机车数"), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PB皮采_已采数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_2, "已翻车车数"), eHtmlDataItemType.svg_text));
			}

			//全自动制样机
			datas.Add(new HtmlDataItem("湿煤破碎电机", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "湿煤破碎机") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("链式缩分器", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "链式缩分器") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("对辊破碎", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "对辊破碎机") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm一级圆盘缩分器", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "3mm一级圆盘缩分器") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("弃料真空上料机", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "弃料真空上料机") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("筛分破碎", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "筛分破碎") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm二级圆盘缩分器", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "3mm二级圆盘缩分器") == "1" ? "#00ff00" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("粉碎机", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "粉碎机") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("真空上料机", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "粉碎单元真空上料机") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("左风扇", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "干燥设备左边风扇运行信号") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("右风扇", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "干燥设备右边风扇运行信号") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("全水样", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "6mm瓶装机灌装口有瓶信号") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("存查样", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "mm瓶装机灌装口有瓶信号3") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("分析样", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "mm瓶装机灌装口有瓶信号1") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("存查样2", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "mm瓶装机灌装口有瓶信号2") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("3mm煤样", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "3mm一级提升机料斗有煤标志") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("干燥煤样", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "3mm二级提升机料斗有煤标志") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));

			//全水
			datas.Add(new HtmlDataItem("在测样品个数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_ZXQS_1, "在测样品个数"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("当前温度", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_ZXQS_1, "当前温度"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("加热状态", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_ZXQS_1, "加热状态"), eHtmlDataItemType.svg_text));

			//气动
			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "换向器1.管道1定位");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("转换器1_1b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("转换器1_1b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "换向器1.管道2定位");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("转换器1_2b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("转换器1_2b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "换向器1.管道3定位");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("转换器1_3b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("转换器1_3b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}


			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "换向器1.管道4定位");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("转换器1_4b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("转换器1_4b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "换向器2.管道1定位");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("转换器2_1b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("转换器2_1b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "换向器2.管道2定位");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("转换器2_2b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("转换器2_2b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "换向器2.管道3定位");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("转换器2_3b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("转换器2_3b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "换向器2.管道4定位");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("转换器2_4b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("转换器2_4b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			//汽采
			string samplecode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.采样编码.ToString());
			datas.Add(new HtmlDataItem("#1汽采_采样编码", samplecode, eHtmlDataItemType.svg_text));

			string sqls = string.Format(@" select count(*) from cmcstbbuyfueltransport a left join cmcstbrcsampling b on a.samplingid=b.id
										where b.samplecode='{0}'", samplecode);
			DataTable dts= Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			string qc1djs = "0";
			if (dts.Rows.Count > 0)
			{
				qc1djs = dts.Rows[0][0].ToString();
			}
			datas.Add(new HtmlDataItem("#1汽采_登记数", qc1djs, eHtmlDataItemType.svg_text));

		     sqls = string.Format(@" select count(*) from cmcstbbuyfueltransport a left join cmcstbrcsampling b on a.samplingid=b.id
										where b.samplecode='{0}' and a.SamplePlace is not null", samplecode);
		     dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			string qc1ycs = "0";
			if (dts.Rows.Count > 0)
			{
				qc1djs = dts.Rows[0][0].ToString();
			}
			datas.Add(new HtmlDataItem("#1汽采_已采数", qc1ycs, eHtmlDataItemType.svg_text));

			//汽采
			samplecode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.采样编码.ToString());
			datas.Add(new HtmlDataItem("#2汽采_采样编码", samplecode, eHtmlDataItemType.svg_text));

			sqls = string.Format(@" select count(*) from cmcstbbuyfueltransport a left join cmcstbrcsampling b on a.samplingid=b.id
										where b.samplecode='{0}'", samplecode);
		    dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			string qc2djs = "0";
			if (dts.Rows.Count > 0)
			{
				qc2djs = dts.Rows[0][0].ToString();
			}
			datas.Add(new HtmlDataItem("#2汽采_登记数", qc2djs, eHtmlDataItemType.svg_text));

			sqls = string.Format(@" select count(*) from cmcstbbuyfueltransport a left join cmcstbrcsampling b on a.samplingid=b.id
										where b.samplecode='{0}' and a.SamplePlace is not null", samplecode);
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			string qc2ycs = "0";
			if (dts.Rows.Count > 0)
			{
				qc2ycs = dts.Rows[0][0].ToString();
			}
			datas.Add(new HtmlDataItem("#2汽采_已采数", qc2ycs, eHtmlDataItemType.svg_text));

			//火采
			samplecode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.采样编码.ToString());
			if (VerifyCompleteHC(GlobalVars.MachineCode_HCJXCYJ_1, samplecode))
			{
				datas.Add(new HtmlDataItem("#1火采_采样编码", "", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#1火采_计划数", "0", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#1火采_已采数", "0", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("#1火采_采样编码", samplecode, eHtmlDataItemType.svg_text));
				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#1火车机械采样机' and t1.samplecode='{0}'", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hcjhs1 = "0";
				if (dts.Rows.Count > 0)
				{
					hcjhs1 = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#1火采_计划数", hcjhs1, eHtmlDataItemType.svg_text));

				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#1火车机械采样机' and t1.samplecode='{0}' and t.Sampleuser is not null", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hc1jhs = "0";
				if (dts.Rows.Count > 0)
				{
					hc1jhs = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#1火采_已采数", hc1jhs, eHtmlDataItemType.svg_text));
			}

			samplecode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.采样编码.ToString());
			if (VerifyCompleteHC(GlobalVars.MachineCode_HCJXCYJ_2, samplecode))
			{
				datas.Add(new HtmlDataItem("#2火采_采样编码", "", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#2火采_计划数", "0", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#2火采_已采数", "0", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("#2火采_采样编码", samplecode, eHtmlDataItemType.svg_text));

				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#2火车机械采样机' and t1.samplecode='{0}'", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hcjhs2 = "0";
				if (dts.Rows.Count > 0)
				{
					hcjhs2 = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#2火采_计划数", hcjhs2, eHtmlDataItemType.svg_text));

				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#2火车机械采样机' and t1.samplecode='{0}' and t.Sampleuser is not null", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hc2jhs = "0";
				if (dts.Rows.Count > 0)
				{
					hc2jhs = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#2火采_已采数", hc2jhs, eHtmlDataItemType.svg_text));
			}

			samplecode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_3, eSignalDataName.采样编码.ToString());
			if (VerifyCompleteHC(GlobalVars.MachineCode_HCJXCYJ_3, samplecode))
			{
				datas.Add(new HtmlDataItem("#3火采_采样编码", "", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#3火采_计划数", "0", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#3火采_已采数", "0", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("#3火采_采样编码", samplecode, eHtmlDataItemType.svg_text));

				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#3火车机械采样机' and t1.samplecode='{0}'", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hcjhs3 = "0";
				if (dts.Rows.Count > 0)
				{
					hcjhs3 = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#3火采_计划数", hcjhs3, eHtmlDataItemType.svg_text));

				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#3火车机械采样机' and t1.samplecode='{0}' and t.Sampleuser is not null", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hc3jhs = "0";
				if (dts.Rows.Count > 0)
				{
					hc3jhs = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#3火采_已采数", hc3jhs, eHtmlDataItemType.svg_text));
			}

			//其他信息
			int zc1=0,zc2 = 0, zc4 = 0,zc5=0,zc6=0;
			sqls = "select count(*) from cmcstbtransportposition a where a.tracknumber='#1'";
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				zc1 = Convert.ToInt32(dts.Rows[0][0]);
			}
			datas.Add(new HtmlDataItem("1轨车数", "车数 " + zc1.ToString() + "节", eHtmlDataItemType.svg_text));

			sqls = "select count(*) from cmcstbtransportposition a where a.tracknumber='#2'";
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				zc2 = Convert.ToInt32(dts.Rows[0][0]);
			}
			datas.Add(new HtmlDataItem("2轨重车数", "重车数 "+ zc2 .ToString()+ "节", eHtmlDataItemType.svg_text));

			sqls = "select count(*) from cmcstbtransportposition a where a.tracknumber='#4'";
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				zc4 = Convert.ToInt32(dts.Rows[0][0]);
			}
			datas.Add(new HtmlDataItem("4轨重车数", "重车数 " + zc4.ToString() + "节", eHtmlDataItemType.svg_text));

			sqls = "select count(*) from cmcstbtransportposition a where a.tracknumber='#5'";
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				zc5 = Convert.ToInt32(dts.Rows[0][0]);
			}
			datas.Add(new HtmlDataItem("5轨车数", "车数 " + zc5.ToString() + "节", eHtmlDataItemType.svg_text));

			sqls = "select count(*) from cmcstbtransportposition a where a.tracknumber='入厂'";
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				zc6 = Convert.ToInt32(dts.Rows[0][0]);
			}
			datas.Add(new HtmlDataItem("6轨车数", "车数 " + zc6.ToString() + "节", eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("厂内重车数", (zc2+zc4).ToString(), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("厂内空车数", (zc1 + zc5).ToString(), eHtmlDataItemType.svg_text));

			sqls = string.Format("select count(*) from cmcstbbuyfueltransport a where to_char(a.infactorytime,'yyyy-MM-dd')='{0}'",dtime.ToString("yyyy-MM-dd"));
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			string ydjcs = "0", wcccs = "0";
			if (dts.Rows.Count > 0)
			{
				ydjcs = dts.Rows[0][0].ToString() ;
			}
			datas.Add(new HtmlDataItem("已登记车数", ydjcs, eHtmlDataItemType.svg_text));

			sqls = string.Format("select count(*) from cmcstbbuyfueltransport a where to_char(a.outfactorytime,'yyyy-MM-dd')='2000-01-01'");
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				wcccs = dts.Rows[0][0].ToString();
			}
			datas.Add(new HtmlDataItem("未出厂车数", wcccs, eHtmlDataItemType.svg_text));
			#endregion


			// 添加更多...

			// 发送到页面
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
		}

		public void BindBatch(string carnumber, string flag, List<HtmlDataItem> datas)
		{
			DataTable data = commonDAO.SelfDber.ExecuteDataTable(string.Format(@"select t.infactorybatchid,a.batch,b.samplecode,c.makecode,d.assaycode,a.fueltype,a.transportnumber,a.fuelkindname,a.minename,a.ticketqty,a.suttleqty 
																					from fultbtransport t inner join fultbinfactorybatch a on t.infactorybatchid=a.id inner join cmcstbrcsampling b on a.id=b.infactorybatchid inner join 
																					cmcstbmake c on c.samplingid=b.id inner join cmcstbassay d on d.makeid=c.id where trunc(a.factarrivedate)=trunc(sysdate) and t.transportno='{0}' and b.samplingtype!='人工采样'", carnumber));
			if (data != null && data.Rows.Count > 0)
			{
				datas.Add(new HtmlDataItem(flag + "批次编号", data.Rows[0]["batch"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "采样编码", data.Rows[0]["samplecode"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "制样编码", data.Rows[0]["makecode"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "化验编码", data.Rows[0]["assaycode"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "来煤方式", data.Rows[0]["fueltype"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "来煤车数", data.Rows[0]["transportnumber"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "煤种", data.Rows[0]["fuelkindname"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "矿点", data.Rows[0]["minename"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "矿发量", data.Rows[0]["ticketqty"].ToString(), eHtmlDataItemType.svg_text));
				//datas.Add(new HtmlDataItem(flag + "净重", data.Rows[0]["suttleqty"].ToString(), eHtmlDataItemType.svg_text));
				IList<CmcsTransport> list = commonDAO.SelfDber.Entities<CmcsTransport>("where InFactoryBatchId=:InFactoryBatchId", new { InFactoryBatchId = data.Rows[0]["infactorybatchid"] });
				if (list != null)
				{
					datas.Add(new HtmlDataItem(flag + "毛重", list.Sum(a => a.GrossQty).ToString(), eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem(flag + "皮重", list.Sum(a => a.SkinQty).ToString(), eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem(flag + "净重", list.Sum(a => a.SuttleQty).ToString(), eHtmlDataItemType.svg_text));
				}
			}

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			// 界面不可见时，停止发送数据
			if (!this.Visible) return;

            RequestData();
        }

		/// <summary>
		/// 添加红绿灯控制信号
		/// </summary>
		/// <param name="datas"></param>
		/// <param name="machineCode"></param>
		/// <param name="signalValue"></param>
		private void AddDataItemBySignal(List<HtmlDataItem> datas, string machineCode, string signalValue)
		{
			if (commonDAO.GetSignalDataValue(machineCode, eSignalDataName.信号灯1.ToString()) == "1")
			{
				//红灯
				datas.Add(new HtmlDataItem(signalValue + "_红", "#FF0000", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem(signalValue + "_绿", "#CCCCCC", eHtmlDataItemType.svg_color));
			}
			else
			{
				//绿灯
				datas.Add(new HtmlDataItem(signalValue + "_红", "#CCCCCC", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem(signalValue + "_绿", "#00FF00", eHtmlDataItemType.svg_color));
			}
		}

		/// <summary>
		/// 判断皮采当前编码是否完成
		/// </summary>
		/// <param name="fcj"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		public bool VerifyComplete(string fcj, string code)
		{
			string sql = string.Format(@"select t6.*
                    from cmcstbtraincarriagepass t5 
                    left join fultbtransport t1 on t1.pkid=t5.id
                    left join fultbinfactorybatch t2 on t1.infactorybatchid=t2.id
                    left join cmcstbrcsampling t3 on t3.infactorybatchid=t2.id
                    inner join cmcstbtransportposition t6 on t5.id=t6.transportid 
                    where t6.tracknumber='{0}' and t3.samplecode='{1}'", fcj == GlobalVars.MachineCode_TrunOver_1 ? "#4" : "#2", code);
			DataTable dt = Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
			if (dt.Rows.Count > 0)
			{
				return false;
			}
			else
			{
				return true;
			}

		}

		/// <summary>
		/// 判断火采当前编码是否完成
		/// </summary>
		/// <param name="fcj"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		public bool VerifyCompleteHC(string cyj, string code)
		{
			string sql = string.Format(@"select * from inftbbeltsampleplan a
										left join  inftbbeltsampleplandetail b on a.id=b.PLANID
										where a.machinecode='{0}' and a.samplecode='{1}' and b.endtime< to_date('2000-01-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS')", cyj, code);
			DataTable dt = Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
			if (dt.Rows.Count > 0)
			{
				return false;
			}
			else
			{
				return true;
			}

		}
	}

	public class HomePageCefWebClient : CefWebClient
	{
		CefWebBrowser cefWebBrowser;
		CommonDAO commonDAO = CommonDAO.GetInstance();

		public HomePageCefWebClient(CefWebBrowser cefWebBrowser)
			: base(cefWebBrowser)
		{
			this.cefWebBrowser = cefWebBrowser;
		}

		protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
		{
			if (message.Name == "OpenTruckWeighter")
			{
				SelfVars.MainFrameForm.OpenTruckWeighter();
				string b=message.Arguments.GetString(0);
				if (b.Contains("1"))
				{
					SelfVars.TruckWeighterForm.CurrentMachineCode = GlobalVars.MachineCode_QC_Weighter_1;
				}
				else if (b.Contains("空")) 
				{
					SelfVars.TruckWeighterForm.CurrentMachineCode = GlobalVars.MachineCode_QC_Weighter_2;
				}
				else if (b.Contains("3"))
				{
					SelfVars.TruckWeighterForm.CurrentMachineCode = GlobalVars.MachineCode_QC_Weighter_3;
				}
			}
			else if (message.Name == "TruckWeighterChangeSelected")
				SelfVars.TruckWeighterForm.CurrentMachineCode = MonitorCommon.GetInstance().GetTruckWeighterMachineCodeBySelected(message.Arguments.GetString(0));
			else if (message.Name == "CarSamplerChangeSelected")
				SelfVars.CarSamplerForm.CurrentMachineCode = MonitorCommon.GetInstance().GetCarSamplerMachineCodeBySelected(message.Arguments.GetString(0));
			else if (message.Name == "TrainSamplerChangeSelected")
				SelfVars.TrainSamplerForm.CurrentMachineCode = message.Arguments.GetString(0);
			else if (message.Name == "OpenHikVideo")
			{
				//视频预览
				SelfVars.MainFrameForm.OpenHikVideo(MonitorCommon.GetInstance().GetVideoBySelected(message.Arguments.GetString(0)));
			}
			else if (message.Name == "SaveOperationLog")
			{
				commonDAO.SaveOperationLog(message.Arguments.GetString(0), GlobalVars.LoginUser.Name);
			}
			else if (message.Name == "TrainBeltSamplerCmd")
			{
				string cmdtype = message.Arguments.GetString(0);
				string log = "";
				InfBeltSampleCmd_KY cmd = new InfBeltSampleCmd_KY();
				cmd.DataFlag = 0;
				if (cmdtype == "LeadCar1")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.解锁牵车机).ToString();
					log = "给2PA皮带采样机发送允许牵车命令";
				}
				else if (cmdtype == "LeadCar2")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.解锁牵车机).ToString();
					log = "给2PB皮带采样机发送允许牵车命令";
				}
				else if (cmdtype == "MovingBelt1")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.解锁皮带).ToString();
					log = "给2PA皮带采样机发送允许起皮带命令";
				}
				else if (cmdtype == "MovingBelt2")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.解锁皮带).ToString();
					log = "给2PB皮带采样机发送允许起皮带命令";
				}
				else if (cmdtype == "StopSampler1")
				{

					//cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					//cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.停止采样).ToString();
					//log = "给2PA皮带采样机发送停止采样命令";

					//改为远程出桶用
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.远程出桶).ToString();
					log = "给2PA皮带采样机发送远程出桶命令";
				}
				else if (cmdtype == "StopSampler2")
				{
					//cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					//cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.停止采样).ToString();
					//log = "给2PB皮带采样机发送停止采样命令";

					//改为远程出桶用
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.远程出桶).ToString();
					log = "给2PB皮带采样机发送远程出桶命令";
				}
				else if (cmdtype == "AlarmReset1")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.报警复位).ToString();
					log = "给2PA皮带采样机发送报警复位命令";
				}
				else if (cmdtype == "AlarmReset2")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.报警复位).ToString();
					log = "给2PB皮带采样机发送报警复位命令";
				}
				else if (cmdtype == "FZJAlarmReset1")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.封装机报警复位).ToString();
					log = "给2PA皮带采样机发送封装机报警复位命令";
				}
				else if (cmdtype == "FZJAlarmReset2")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.封装机报警复位).ToString();
					log = "给2PB皮带采样机发送封装机报警复位命令";
				}
				else if (cmdtype == "StopS1")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.停止采样).ToString();
					log = "给2PA皮带采样机发送停止采样命令";
				}
				else if (cmdtype == "StopS2")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.停止采样).ToString();
					log = "给2PB皮带采样机发送停止采样命令";
				}

				cmd.ResultCode = eEquInfCmdResultCode.默认.ToString();
				cmd.OperatorName = GlobalVars.LoginUser.Name;
				cmd.SendDateTime = DateTime.Now;
				cmd.SyncFlag = 0;
				if (Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd_KY>(cmd) > 0)
				{
					commonDAO.SaveOperationLog(log, GlobalVars.LoginUser.Name);
				}

			}
			else if (message.Name == "TrainBeltSamplerPlan")
			{
				string gd = message.Arguments.GetString(0) == "SendSampler1" ? "#4" : "#2";

				SelfVars.MainFrameForm.OpenSendSampleCode(gd);
			}
			else if (message.Name == "AutoMakerErrorInfo")
			{
				SelfVars.MainFrameForm.OpenAutoMakerErrorInfo();
			}
			else if (message.Name == "BatchMachineErrorInfo")
			{
				SelfVars.MainFrameForm.OpenBatchMachineErrorInfo();
			}
			else if (message.Name == "BatchMachineSendDLCMD")
			{
				SelfVars.MainFrameForm.BatchMachineSendDLCMD();

			}
			else if (message.Name == "BatchMachineSendGPCMD")
			{
				string currentMessage = string.Empty;
				InfBatchMachineCmd batchMachineCmd = new InfBatchMachineCmd();
				batchMachineCmd.InterfaceType = GlobalVars.MachineCode_HYGPJ_1;
				batchMachineCmd.MachineCode = GlobalVars.MachineCode_HYGPJ_1;
				batchMachineCmd.CmdCode = eEquInfBatchMachineCmd.归批命令.ToString();
				batchMachineCmd.SampleCode = "0";
				batchMachineCmd.ResultCode = eEquInfCmdResultCode.默认.ToString();
				batchMachineCmd.SyncFlag = 0;

				if (Dbers.GetInstance().SelfDber.Insert<InfBatchMachineCmd>(batchMachineCmd) > 0)
				{
					commonDAO.SaveOperationLog("给合样归批机发送归批命令", GlobalVars.LoginUser.Name);
					MessageBox.Show("命令发送成功", "提示");
				}

			}
			else if (message.Name == "BatchMachineUnloadSwip")
			{
				string currentMessage = string.Empty;
				InfBatchMachineCmd batchMachineCmd = new InfBatchMachineCmd();
				batchMachineCmd.InterfaceType = GlobalVars.MachineCode_HYGPJ_1;
				batchMachineCmd.MachineCode = GlobalVars.MachineCode_HYGPJ_1;
				batchMachineCmd.CmdCode = eEquInfBatchMachineCmd.归批命令.ToString();
				batchMachineCmd.SampleCode = "0";
				batchMachineCmd.ResultCode = eEquInfCmdResultCode.默认.ToString();
				batchMachineCmd.SyncFlag = 0;

				if (Dbers.GetInstance().SelfDber.Insert<InfBatchMachineCmd>(batchMachineCmd) > 0)
				{
					commonDAO.SaveOperationLog("给合样归批机发送归批命令", GlobalVars.LoginUser.Name);
					MessageBox.Show("命令发送成功", "提示");
				}

			}
			else if (message.Name == "OpenAssayManage")
			{
				SelfVars.MainFrameForm.OpenAssayManage();
			}
			else if (message.Name == "OpenBatchMachine")
			{
				SelfVars.MainFrameForm.OpenBatchMachine();
			}
			else if (message.Name == "OpenAutoCupboard")
			{
				SelfVars.MainFrameForm.OpenAutoCupboard();
			}
			else if (message.Name == "OpenSampleCabinet")
			{
				SelfVars.MainFrameForm.OpenSampleCabinet();
			}
			else if (message.Name == "OpenAutoMaker")
			{
				SelfVars.MainFrameForm.OpenAutoMaker();
			}
			else if (message.Name == "OpenTrainBeltSampler")
			{
				SelfVars.MainFrameForm.OpenTrainBeltSampler();
			}
			else if (message.Name == "OpenTrainSampler")
			{
				SelfVars.MainFrameForm.OpenTrainSampler();
				string b = message.Arguments.GetString(0);
				if (b.Contains("1"))
				{
					SelfVars.TrainSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_HCJXCYJ_1;
				}
				else if (b.Contains("2"))
				{
					SelfVars.TrainSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_HCJXCYJ_2;
				}
				else if (b.Contains("3"))
				{
					SelfVars.TrainSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_HCJXCYJ_3;
				}
			}
			else if (message.Name == "OpenCarSampler1")
			{
				SelfVars.MainFrameForm.OpenCarSampler1();
				string b = message.Arguments.GetString(0);
				if (b.Contains("1"))
				{
					SelfVars.CarSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_QCJXCYJ_1;
				}
				else if (b.Contains("2"))
				{
					SelfVars.CarSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_QCJXCYJ_2;
				}
				
			}
			//else if (message.Name == "OpenTruckWeighter")
			//{
			//	SelfVars.MainFrameForm.OpenTruckWeighter();
			//}
			else if (message.Name == "FaultRecord")
			{
				string m = message.Arguments.GetString(0);
				if (m == "合样归批")
				{
					SelfVars.MainFrameForm.OpenFaultRecordInfo(GlobalVars.MachineCode_HYGPJ_1);
				}
				else if (m == "全自动制样机")
				{
					SelfVars.MainFrameForm.OpenFaultRecordInfo(GlobalVars.MachineCode_QZDZYJ_1);
				}
				else if (m == "2PA皮采")
				{
					SelfVars.MainFrameForm.OpenFaultRecordInfo(GlobalVars.MachineCode_PDCYJ_1);
				}
				else if (m == "2PB皮采")
				{
					SelfVars.MainFrameForm.OpenFaultRecordInfo(GlobalVars.MachineCode_PDCYJ_2);
				}
			}
			else if(message.Name == "TrainBeltSampleAlarmInfo")
			{
				SelfVars.MainFrameForm.OpenTrainBeltSampler_warning();
			}
			return true;
		}

		protected override CefContextMenuHandler GetContextMenuHandler()
		{
			return new CefMenuHandler();
		}

		
	}

	/// <summary>
	/// 首页临时实体
	/// </summary>
	public class HomePageTemp
	{
		public virtual String transportno { get; set; }
		public virtual String grossqty { get; set; }
		public virtual string skinqty { get; set; }

		public virtual string suttleqty { get; set; }
		public virtual string marginqty { get; set; }

		public virtual string grossplace { get; set; }

	}
}