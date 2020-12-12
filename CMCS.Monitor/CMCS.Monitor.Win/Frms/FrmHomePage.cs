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
			datas.Add(new HtmlDataItem("火车入厂车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.火车入厂车数.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("火车翻车车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.火车翻车车数.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("火车出厂车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.火车出厂车数.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#1翻车机已翻车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.已翻车数.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#2翻车机已翻车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.已翻车数.ToString()), eHtmlDataItemType.svg_text));
			string CarNumber_1 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.当前车号.ToString());
			string CarNumber_2 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.当前车号.ToString());
			datas.Add(new HtmlDataItem("#1翻车机当前车号", CarNumber_1, eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#2翻车机当前车号", CarNumber_2, eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#1翻车机", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.当前车号.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2翻车机", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.当前车号.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#4车号识别", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.当前车号.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#5车号识别", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.当前车号.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("来船量", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.来船量.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("汽车转运车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车转运车数.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("汽车入厂车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车入厂车数.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("汽车采样称重车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车采样称重车数.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("汽车回皮车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车回皮车数.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("汽车出厂车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车出厂车数.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("制样合批数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.制样合批数.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("存样数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.存样数.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("待化验数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.待化验数.ToString()), eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("制样机_制样数量", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "制样机_制样数量"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("制样机_制样编码", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "制样机_制样编码"), eHtmlDataItemType.svg_text));

			#region 批次 采制化
			if (!string.IsNullOrEmpty(CarNumber_1))
			{
				BindBatch(CarNumber_1, "#2", datas);
			}
			if (!string.IsNullOrEmpty(CarNumber_1))
			{
				BindBatch(CarNumber_2, "#4", datas);
			}
			#endregion

			#region 汽车采样机

			machineCode = GlobalVars.MachineCode_QC_JxSampler_1;
			datas.Add(new HtmlDataItem("汽车_1号采样_系统", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("汽车_1号采样_车号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("汽车_1号采样_道闸1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸1升杆.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("汽车_1号采样_道闸2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸2升杆.ToString())), eHtmlDataItemType.svg_color));

			machineCode = GlobalVars.MachineCode_QC_JxSampler_2;
			datas.Add(new HtmlDataItem("汽车_2号采样_系统", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("汽车_2号采样_车号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("汽车_2号采样_道闸1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸1升杆.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("汽车_2号采样_道闸2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸2升杆.ToString())), eHtmlDataItemType.svg_color));

			#endregion

			#region 汽车衡

			machineCode = GlobalVars.MachineCode_QC_Weighter_1;
			datas.Add(new HtmlDataItem("#1磅系统", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#1磅当前车号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#1磅当前重量", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.地磅仪表_实时重量.ToString() + "t"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("汽车_1号衡_道闸1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸1升杆.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("汽车_1号衡_道闸2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸2升杆.ToString())), eHtmlDataItemType.svg_color));
			AddDataItemBySignal(datas, machineCode, "汽车_1号衡_红绿灯");

			machineCode = GlobalVars.MachineCode_QC_Weighter_2;
			datas.Add(new HtmlDataItem("#2磅系统", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2磅当前车号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#2磅当前重量", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.地磅仪表_实时重量.ToString() + "t"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("汽车_2号衡_道闸1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸1升杆.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("汽车_2号衡_道闸2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸2升杆.ToString())), eHtmlDataItemType.svg_color));
			AddDataItemBySignal(datas, machineCode, "汽车_2号衡_红绿灯");

			machineCode = GlobalVars.MachineCode_QC_Weighter_3;
			datas.Add(new HtmlDataItem("#3磅系统", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#3磅当前车号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#3磅当前重量", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.地磅仪表_实时重量.ToString() + "t"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("汽车_3号衡_道闸1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸1升杆.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("汽车_3号衡_道闸2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸2升杆.ToString())), eHtmlDataItemType.svg_color));
			AddDataItemBySignal(datas, machineCode, "汽车_3号衡_红绿灯");

			#endregion

			datas.Add(new HtmlDataItem("门禁_制样室进", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "门禁_制样室进"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("门禁_化验室进", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "门禁_化验室进"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("门禁_集控室进", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "门禁_集控室进"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("门禁_办公楼进", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "门禁_办公楼进"), eHtmlDataItemType.svg_text));

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

	}

	public class HomePageCefWebClient : CefWebClient
	{
		CefWebBrowser cefWebBrowser;

		public HomePageCefWebClient(CefWebBrowser cefWebBrowser)
			: base(cefWebBrowser)
		{
			this.cefWebBrowser = cefWebBrowser;
		}

		protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
		{
			if (message.Name == "OpenTruckWeighter")
				SelfVars.MainFrameForm.OpenTruckWeighter();
			else if (message.Name == "TruckWeighterChangeSelected")
				SelfVars.TruckWeighterForm.CurrentMachineCode = MonitorCommon.GetInstance().GetTruckWeighterMachineCodeBySelected(message.Arguments.GetString(0));
			else if (message.Name == "CarSamplerChangeSelected")
				SelfVars.CarSamplerForm.CurrentMachineCode = MonitorCommon.GetInstance().GetCarSamplerMachineCodeBySelected(message.Arguments.GetString(0));
			else if (message.Name == "TrainSamplerChangeSelected")
				SelfVars.TrainSamplerForm.CurrentMachineCode = message.Arguments.GetString(0);

			return true;
		}

		protected override CefContextMenuHandler GetContextMenuHandler()
		{
			return new CefMenuHandler();
		}
	}
}