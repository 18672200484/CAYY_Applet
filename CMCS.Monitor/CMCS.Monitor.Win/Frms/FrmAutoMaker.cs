using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Inf;
using CMCS.Monitor.DAO;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using DevComponents.DotNetBar.Metro;
using Xilium.CefGlue.WindowsForms;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Fuel;
using CMCS.Monitor.Win.UserControls;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Monitor.Win.Utilities;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmAutoMaker : MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmAutoMakerCSKY";
		public string[] strSignal = new string[] { "湿煤破碎机", "链式缩分驱动器运行信号", "对辊破碎机", "3mm一级缩分器驱动器运行信号", "制样机_3mm缩分1",
			"制样机_3mm缩分2","制样机_干燥","制样机_3mm缩分3","制样机_02mm破碎","制样机_02mm缩分","制样机_6mm缩分3","制样机_6mm弃料","制样机_弃料清洗样",
			"制样机_鼓风机","制样机_一体机"};

		CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();
		MonitorCommon monitorCommon = MonitorCommon.GetInstance();
		CommonDAO commonDAO = CommonDAO.GetInstance();
		public FrmAutoMaker()
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
			cefWebBrowser.StartUrl = SelfVars.Url_AutoMaker;
			cefWebBrowser.Dock = DockStyle.Fill;
			cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
			cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
			panWebBrower.Controls.Add(cefWebBrowser);

			dtInputSampleDate.Value = DateTime.Now;
		}

		void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
		{
			timer1.Enabled = true;
		}

		private void FrmAutoMakerCSKY_Load(object sender, EventArgs e)
		{
			FormInit();
		}
		/// <summary>
		/// 请求数据
		/// </summary>
		void RequestData()
		{

			AutoMakerDAO automakerDAO = AutoMakerDAO.GetInstance();

			string value = string.Empty, machineCode = string.Empty;
			List<HtmlDataItem> datas = new List<HtmlDataItem>();
			List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();

			#region 全自动制样机

			datas.Clear();
			machineCode = GlobalVars.MachineCode_QZDZYJ_1;

			//制样信息
			string makeCode = commonDAO.GetSignalDataValue(machineCode, "制样编码");
			datas.Add(new HtmlDataItem("制样机_制样编码", makeCode, eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("制样机_开始时间", commonDAO.GetSignalDataValue(machineCode, "开始时间"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("制样机_煤种", commonDAO.GetSignalDataValue(machineCode, "煤种"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("制样机_水分", commonDAO.GetSignalDataValue(machineCode, "水分"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("制样机_粒度", commonDAO.GetSignalDataValue(machineCode, "粒度"), eHtmlDataItemType.svg_text));

			value = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString());
			if ("|就绪待机|".Contains("|" + value + "|"))
				datas.Add(new HtmlDataItem("制样机_系统", "#00c000", eHtmlDataItemType.svg_color));
			else if ("|正在运行|正在卸样|".Contains("|" + value + "|"))
				datas.Add(new HtmlDataItem("制样机_系统", "#ff0000", eHtmlDataItemType.svg_color));
			else if ("|发生故障|".Contains("|" + value + "|"))
				datas.Add(new HtmlDataItem("制样机_系统", "#ffff00", eHtmlDataItemType.svg_color));
			else
				datas.Add(new HtmlDataItem("制样机_系统", "#c0c0c0", eHtmlDataItemType.svg_color));

			//信号状态
			//string keys = string.Empty;
			//foreach (string item in strSignal)
			//{
			//	if (commonDAO.GetSignalDataValue(machineCode, item) == "1")
			//	{
			//		keys += item;
			//		datas.Add(new HtmlDataItem(item, "Red", eHtmlDataItemType.svg_color));
			//	}
			//	else
			//		datas.Add(new HtmlDataItem(item + "_Line", "#6d6e71", eHtmlDataItemType.svg_color));
			//}

			//datas.Add(new HtmlDataItem("Keys", keys, eHtmlDataItemType.svg_scroll));

			///信号接入
			datas.Add(new HtmlDataItem("故障提示", commonDAO.GetSignalDataValue(machineCode, "设备状态") == "发生故障" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("湿煤破碎电机", commonDAO.GetSignalDataValue(machineCode, "湿煤破碎机") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("链式缩分器", commonDAO.GetSignalDataValue(machineCode, "链式缩分器") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("对辊破碎", commonDAO.GetSignalDataValue(machineCode, "对辊破碎机") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm一级圆盘缩分器", commonDAO.GetSignalDataValue(machineCode, "3mm一级圆盘缩分器") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm一级圆盘缩分器管道", commonDAO.GetSignalDataValue(machineCode, "3mm一级圆盘缩分器"), eHtmlDataItemType.svg_dyncolor));
			datas.Add(new HtmlDataItem("弃料真空上料机", commonDAO.GetSignalDataValue(machineCode, "弃料真空上料机") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm弃料真空上料机管道", commonDAO.GetSignalDataValue(machineCode, "弃料真空上料机"), eHtmlDataItemType.svg_dyncolor));

			datas.Add(new HtmlDataItem("筛分破碎", commonDAO.GetSignalDataValue(machineCode, "筛分破碎") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("筛分破碎管道", commonDAO.GetSignalDataValue(machineCode, "筛分破碎"), eHtmlDataItemType.svg_dyncolor));
			datas.Add(new HtmlDataItem("3mm二级圆盘缩分器", commonDAO.GetSignalDataValue(machineCode, "3mm二级圆盘缩分器") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm存查样管道", commonDAO.GetSignalDataValue(machineCode, "3mm二级圆盘缩分器"), eHtmlDataItemType.svg_dyncolor));
			datas.Add(new HtmlDataItem("粉碎机", commonDAO.GetSignalDataValue(machineCode, "粉碎机") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("粉碎机管道", commonDAO.GetSignalDataValue(machineCode, "粉碎机"), eHtmlDataItemType.svg_dyncolor));
			datas.Add(new HtmlDataItem("真空上料机", commonDAO.GetSignalDataValue(machineCode, "粉碎单元真空上料机") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("2mm存查样管道", commonDAO.GetSignalDataValue(machineCode, "粉碎单元真空上料机"), eHtmlDataItemType.svg_dyncolor));
			datas.Add(new HtmlDataItem("左风扇", commonDAO.GetSignalDataValue(machineCode, "干燥设备左边风扇运行信号") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("右风扇", commonDAO.GetSignalDataValue(machineCode, "干燥设备右边风扇运行信号") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("左风扇管道", commonDAO.GetSignalDataValue(machineCode, "干燥设备左边风扇运行信号"), eHtmlDataItemType.svg_dyncolor));
			datas.Add(new HtmlDataItem("右风扇管道", commonDAO.GetSignalDataValue(machineCode, "干燥设备右边风扇运行信号"), eHtmlDataItemType.svg_dyncolor));

			datas.Add(new HtmlDataItem("I_原煤样输送皮带变频器运行信号", commonDAO.GetSignalDataValue(machineCode, "I_原煤样输送皮带变频器运行信号"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("I_6mm缩分给料皮带变频器运行信号", commonDAO.GetSignalDataValue(machineCode, "I_6mm缩分给料皮带变频器运行信号"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("Q_6mm转运皮带_全水样_", commonDAO.GetSignalDataValue(machineCode, "Q_6mm转运皮带_全水样_"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("Q_6mm转运皮带_分析样_", commonDAO.GetSignalDataValue(machineCode, "Q_6mm转运皮带_分析样_"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("Q_对辊破碎出料皮带", commonDAO.GetSignalDataValue(machineCode, "Q_对辊破碎出料皮带"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("I_分析样中间给料皮带变频器运行信号", commonDAO.GetSignalDataValue(machineCode, "I_分析样中间给料皮带变频器运行信号"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("I_3mm一级缩分器驱动器运行信号", commonDAO.GetSignalDataValue(machineCode, "I_3mm一级缩分器驱动器运行信号"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("Q_3mm弃料一级皮带", commonDAO.GetSignalDataValue(machineCode, "Q_3mm弃料一级皮带"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("Q_3mm弃料二级皮带正转", commonDAO.GetSignalDataValue(machineCode, "Q_3mm弃料二级皮带正转"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("Q_干燥机布料皮带正转", commonDAO.GetSignalDataValue(machineCode, "Q_干燥机布料皮带正转"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("Q_干燥机布料皮带反转", commonDAO.GetSignalDataValue(machineCode, "Q_干燥机布料皮带反转"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("ST_干燥出料流程", commonDAO.GetSignalDataValue(machineCode, "ST_干燥出料流程"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("Q_3mm筛分破碎机正转", commonDAO.GetSignalDataValue(machineCode, "Q_3mm筛分破碎机正转"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("Q_3mm二级缩分出料皮带反转", commonDAO.GetSignalDataValue(machineCode, "Q_3mm二级缩分出料皮带反转"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("Q_3mm二级缩分出料皮带正转", commonDAO.GetSignalDataValue(machineCode, "Q_3mm二级缩分出料皮带正转"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("ST_粉碎流程", commonDAO.GetSignalDataValue(machineCode, "ST_粉碎流程"), eHtmlDataItemType.svg_visible));


			//datas.Add(new HtmlDataItem("煤样编码", commonDAO.GetSignalDataValue(machineCode, "煤样编码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("原煤制样重量", commonDAO.GetSignalDataValue(machineCode, "原煤重量") + " Kg", eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("在线测水状态", commonDAO.GetSignalDataValue(machineCode, "在线测水连接状态") == "1" ? "不在线" : "在线", eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("左侧干燥机转速", commonDAO.GetSignalDataValue(machineCode, "轴流风机1速度") + " r/min", eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("右侧干燥机转速", commonDAO.GetSignalDataValue(machineCode, "轴流风机2速度") + " r/min", eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("全水样有瓶", commonDAO.GetSignalDataValue(machineCode, "6mm瓶装机灌装口有瓶信号") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("存查样有瓶", commonDAO.GetSignalDataValue(machineCode, "mm瓶装机灌装口有瓶信号3") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("分析样有瓶", commonDAO.GetSignalDataValue(machineCode, "mm瓶装机灌装口有瓶信号1") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("存查样有瓶2", commonDAO.GetSignalDataValue(machineCode, "mm瓶装机灌装口有瓶信号2") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("全水样重", commonDAO.GetSignalDataValue(machineCode, "6mm瓶装机称净重") + " g", eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("3mm煤样重", commonDAO.GetSignalDataValue(machineCode, "3mm分析样净重") + " g", eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("干燥煤样重", commonDAO.GetSignalDataValue(machineCode, "3mm干燥后留样净重") + " g", eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("存查样重", commonDAO.GetSignalDataValue(machineCode, "3mm瓶装机称净重_3存查样") + " g", eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("分析样重", commonDAO.GetSignalDataValue(machineCode, "3mm瓶装机称净重_0_2分析样") + " g", eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("存查样重2", commonDAO.GetSignalDataValue(machineCode, "3mm瓶装机称净重_0_2存查样") + " g", eHtmlDataItemType.svg_text));

			value = commonDAO.GetSignalDataValue(machineCode, "6mm制样倒计时");
			datas.Add(new HtmlDataItem("6mm制样倒计时", value + " 秒", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "3mm制样倒计时");
			datas.Add(new HtmlDataItem("3mm制样倒计时", value + " 秒", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "左侧烘干倒计时");
			datas.Add(new HtmlDataItem("左侧烘干倒计时", value + " 秒", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "右侧烘干倒计时");
			datas.Add(new HtmlDataItem("右侧烘干倒计时", value + " 秒", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "粉碎总计时");
			datas.Add(new HtmlDataItem("粉碎制样总计时", value + " 秒", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "粉碎单元真空上料机负压值");
			datas.Add(new HtmlDataItem("粉碎负压", value + " kpa", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "弃料收集仓负压值");
			datas.Add(new HtmlDataItem("弃料负压", value + " kpa", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "主气路正压值");
			datas.Add(new HtmlDataItem("主气路正压", value + " kpa", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "粉碎单元正压值");
			datas.Add(new HtmlDataItem("粉碎单元正压", value + " kpa", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "原煤称实时重量");
			datas.Add(new HtmlDataItem("原煤样称（实时）", value + " Kg", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "3mm分析样称实时重量");
			datas.Add(new HtmlDataItem("3mm分析样称（实时）", value + "g", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "3mm干燥样称实时重量");
			datas.Add(new HtmlDataItem("3mm干燥样称（实时）", value + " g", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "6mm瓶装机秤重量");
			datas.Add(new HtmlDataItem("6mm瓶装机称（实时）", value + "g", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "3mm瓶装机称实时重量");
			datas.Add(new HtmlDataItem("3mm瓶装机称（实时）", value + "g", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "弃料称实时重量");
			datas.Add(new HtmlDataItem("弃料样称（实时）", value + " Kg", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "左侧干燥箱温度");
			datas.Add(new HtmlDataItem("左侧干燥箱温度", value + " ℃", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "右侧干燥箱温度");
			datas.Add(new HtmlDataItem("右侧干燥箱温度", value + " ℃", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));
			value = commonDAO.GetSignalDataValue(machineCode, "粉碎电机电流");
			datas.Add(new HtmlDataItem("粉碎机电流", value + " A", monitorCommon.ConvertRunToColor(value != "0"), eHtmlDataItemType.svg_textcolor));


			datas.Add(new HtmlDataItem("煤样编码", commonDAO.GetSignalDataValue(machineCode, "原煤煤样编码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("6mm瓶装机煤样编码", commonDAO.GetSignalDataValue(machineCode, "6mm瓶装机煤样编码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("3mm弃料一级皮带煤样编码", commonDAO.GetSignalDataValue(machineCode, "3mm弃料一级皮带煤样编码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("干燥箱1煤样编码", commonDAO.GetSignalDataValue(machineCode, "干燥箱1煤样编码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("3mm煤样编码", commonDAO.GetSignalDataValue(machineCode, "3mm煤样编码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("干燥箱2煤样编码", commonDAO.GetSignalDataValue(machineCode, "干燥箱2煤样编码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("3mm瓶装机煤样编码", commonDAO.GetSignalDataValue(machineCode, "3mm瓶装机煤样编码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("粉碎煤样编码", commonDAO.GetSignalDataValue(machineCode, "粉碎煤样编码"), eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("3mm弃料一级皮带有煤标志", commonDAO.GetSignalDataValue(machineCode, "3mm弃料一级皮带有煤标志"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("3mm弃料管道", commonDAO.GetSignalDataValue(machineCode, "3mm弃料一级皮带有煤标志"), eHtmlDataItemType.svg_dyncolor));

			datas.Add(new HtmlDataItem("3mm弃料一级皮带煤样编码", commonDAO.GetSignalDataValue(machineCode, "3mm弃料一级皮带有煤标志"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("3mm弃料二级皮带有煤标志", commonDAO.GetSignalDataValue(machineCode, "3mm弃料二级皮带有煤标志"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("左侧干燥机有煤标志", commonDAO.GetSignalDataValue(machineCode, "左侧干燥机有煤标志"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("干燥箱1煤样编码", commonDAO.GetSignalDataValue(machineCode, "左侧干燥机有煤标志"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("右侧干燥机有煤标志", commonDAO.GetSignalDataValue(machineCode, "右侧干燥机有煤标志"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("干燥箱2煤样编码", commonDAO.GetSignalDataValue(machineCode, "右侧干燥机有煤标志"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("3mm一级提升机料斗有煤标志", commonDAO.GetSignalDataValue(machineCode, "3mm一级提升机料斗有煤标志"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("3mm二级提升机料斗有煤标志", commonDAO.GetSignalDataValue(machineCode, "3mm二级提升机料斗有煤标志"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("2mm制样管道", commonDAO.GetSignalDataValue(machineCode, "3mm二级提升机料斗有煤标志"), eHtmlDataItemType.svg_dyncolor));
			datas.Add(new HtmlDataItem("粉碎煤样编码", commonDAO.GetSignalDataValue(machineCode, "0_2mm制粉机变频器运行信号"), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("3mm煤样编码", commonDAO.GetSignalDataValue(machineCode, "干燥机入料皮带有煤标志"), eHtmlDataItemType.svg_visible));
			//制样流程判断

			//6mm制样流程：
			//S_在线测水缩分准备步
			//S_在线测水缩分步
			//S_在线测水设备煤样传送步
			//S_6mm瓶装机进瓶步
			//S_原煤上料皮带上升步
			//S_原煤上料皮带前进出料步
			//S_6mm煤样制备步
			//S_链式缩分单元小清洗步
			//S_原煤上料皮带后退步
			//S_原煤上料皮带下降步
			//S_机采伸缩皮带伸出步
			//S_机采伸缩皮带缩回步
			//ST_6mm制样无流程标记
			string lc_6mm制样流程 = "";
			if (commonDAO.GetSignalDataValue(machineCode, "S_在线测水缩分准备步") == "1")
			{
				lc_6mm制样流程 = "在线测水缩分准备步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_在线测水缩分步") == "1")
			{
				lc_6mm制样流程 = "在线测水缩分步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_在线测水设备煤样传送步") == "1")
			{
				lc_6mm制样流程 = "在线测水设备煤样传送步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机进瓶步") == "1")
			{
				lc_6mm制样流程 = "6mm瓶装机进瓶步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_原煤上料皮带上升步") == "1")
			{
				lc_6mm制样流程 = "原煤上料皮带上升步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_原煤上料皮带前进出料步") == "1")
			{
				lc_6mm制样流程 = "原煤上料皮带前进出料步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm煤样制备步") == "1")
			{
				lc_6mm制样流程 = "6mm煤样制备步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_链式缩分单元小清洗步") == "1")
			{
				lc_6mm制样流程 = "链式缩分单元小清洗步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_原煤上料皮带后退步") == "1")
			{
				lc_6mm制样流程 = "原煤上料皮带后退步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_原煤上料皮带下降步") == "1")
			{
				lc_6mm制样流程 = "原煤上料皮带下降步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_机采伸缩皮带伸出步") == "1")
			{
				lc_6mm制样流程 = "机采伸缩皮带伸出步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_机采伸缩皮带缩回步") == "1")
			{
				lc_6mm制样流程 = "机采伸缩皮带缩回步";
			}
			else
			{
				lc_6mm制样流程 = "无流程动作运行";
			}
			datas.Add(new HtmlDataItem("6mm制样", lc_6mm制样流程, monitorCommon.ConvertRunToColor(lc_6mm制样流程 != "无流程动作运行"), eHtmlDataItemType.svg_textcolor));
			datas.Add(new HtmlDataItem("6mm瓶装机煤样编码", monitorCommon.Desalt(lc_6mm制样流程 == "无流程动作运行"), eHtmlDataItemType.svg_color));
			//3mm制样流程：
			//S_3mm煤样制备步
			//S_3mm制样称重步
			//ST_3mm制样无流程标记
			string lc_3mm制样流程 = "";
			if (commonDAO.GetSignalDataValue(machineCode, "S_3mm煤样制备步") == "1")
			{
				lc_3mm制样流程 = "3mm煤样制备步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm制样称重步") == "1")
			{
				lc_3mm制样流程 = "3mm制样称重步";
			}
			else
			{
				lc_3mm制样流程 = "无流程动作运行";
			}
			datas.Add(new HtmlDataItem("3mm制样", lc_3mm制样流程, monitorCommon.ConvertRunToColor(lc_3mm制样流程 != "无流程动作运行"), eHtmlDataItemType.svg_textcolor));
			datas.Add(new HtmlDataItem("3mm煤样编码", monitorCommon.Desalt(lc_3mm制样流程 == "无流程动作运行"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm制样管道", lc_3mm制样流程 != "无流程动作运行" ? "1" : "0", eHtmlDataItemType.svg_dyncolor));
			//3mm缩分：
			//S_3mm缩分称重步
			//S_3mm样接斗步
			//S_3mm样电机推出步
			//S_一级提升机上升步
			//S_一级提升机下降步
			//S_3mm样电机缩回步
			//S_3mm样卸斗步
			//S_3mm煤样缩分步
			//S_圆盘缩分1单元小清洗步
			//S_3mm缩分弃料称重步
			//ST_3mm缩分无流程标记

			string lc_3mm缩分 = "";
			if (commonDAO.GetSignalDataValue(machineCode, "S_3mm缩分称重步") == "1")
			{
				lc_3mm缩分 = "3mm缩分称重步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm样接斗步") == "1")
			{
				lc_3mm缩分 = "3mm样接斗步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm样电机推出步") == "1")
			{
				lc_3mm缩分 = "3mm样电机推出步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_一级提升机上升步") == "1")
			{
				lc_3mm缩分 = "一级提升机上升步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_一级提升机下降步") == "1")
			{
				lc_3mm缩分 = "一级提升机下降步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm样电机缩回步") == "1")
			{
				lc_3mm缩分 = "3mm样电机缩回步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm样卸斗步") == "1")
			{
				lc_3mm缩分 = "3mm样卸斗步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm煤样缩分步") == "1")
			{
				lc_3mm缩分 = "3mm煤样缩分步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_圆盘缩分1单元小清洗步") == "1")
			{
				lc_3mm缩分 = "圆盘缩分1单元小清洗步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm缩分弃料称重步") == "1")
			{
				lc_3mm缩分 = "3mm缩分弃料称重步";
			}
			else
			{
				lc_3mm缩分 = "无流程动作运行";
			}
			datas.Add(new HtmlDataItem("3mm缩分", lc_3mm缩分, monitorCommon.ConvertRunToColor(lc_3mm缩分 != "无流程动作运行"), eHtmlDataItemType.svg_textcolor));
			datas.Add(new HtmlDataItem("3mm弃料一级皮带煤样编码", monitorCommon.Desalt(lc_3mm制样流程 == "无流程动作运行"), eHtmlDataItemType.svg_color));
			//干燥布料：
			//S_干燥机入料准备步
			//S_干燥布料步
			//S_烘干数据读取步
			//S_干燥单元小清洗步
			//ST_干燥布料无流程标记
			string lc_干燥布料 = "";
			if (commonDAO.GetSignalDataValue(machineCode, "S_干燥机入料准备步") == "1")
			{
				lc_干燥布料 = "干燥机入料准备步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_干燥布料步") == "1")
			{
				lc_干燥布料 = "干燥布料步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_烘干数据读取步") == "1")
			{
				lc_干燥布料 = "烘干数据读取步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_干燥单元小清洗步") == "1")
			{
				lc_干燥布料 = "干燥单元小清洗步";
			}
			else
			{
				lc_干燥布料 = "无流程动作运行";
			}
			datas.Add(new HtmlDataItem("干燥布料", lc_干燥布料, monitorCommon.ConvertRunToColor(lc_干燥布料 != "无流程动作运行"), eHtmlDataItemType.svg_textcolor));

			//干燥出料：
			//S_干燥出料预备步
			//S_干燥气缸闸板出料步
			//S_干燥筛网出料步
			//S_干燥筛网摆动步
			//S_干燥毛重记录步
			//S_干燥称重步
			//S_干燥筛网回原位步
			//ST_干燥出料无流程标记
			string lc_干燥出料 = "";
			if (commonDAO.GetSignalDataValue(machineCode, "S_干燥出料预备步") == "1")
			{
				lc_干燥出料 = "干燥出料预备步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_干燥气缸闸板出料步") == "1")
			{
				lc_干燥出料 = "干燥气缸闸板出料步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_干燥筛网出料步") == "1")
			{
				lc_干燥出料 = "干燥筛网出料步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_干燥筛网摆动步") == "1")
			{
				lc_干燥出料 = "干燥筛网摆动步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_干燥毛重记录步") == "1")
			{
				lc_干燥出料 = "干燥毛重记录步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_干燥称重步") == "1")
			{
				lc_干燥出料 = "干燥称重步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_干燥筛网回原位步") == "1")
			{
				lc_干燥出料 = "干燥筛网回原位步";
			}
			else
			{
				lc_干燥出料 = "无流程动作运行";
			}
			datas.Add(new HtmlDataItem("干燥出料", lc_干燥出料, monitorCommon.ConvertRunToColor(lc_干燥出料 != "无流程动作运行"), eHtmlDataItemType.svg_textcolor));
			datas.Add(new HtmlDataItem("干燥箱1煤样编码", monitorCommon.Desalt(lc_干燥出料 == "无流程动作运行"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("干燥箱2煤样编码", monitorCommon.Desalt(lc_干燥出料 == "无流程动作运行"), eHtmlDataItemType.svg_color));
			//粉碎制样：
			//S_粉碎称重步
			//S_3mm干燥样卸斗步
			//S_3mm干燥样接斗步
			//S_3mm二级提升机直线行走电机后退步
			//S_圆盘缩分2准备步
			//S_3mm二级提升机上升步
			//S_3mm二级提升机倒料步
			//S_3mm二级提升机下降步
			//S_3mm二级提升机直线行走电机前进步
			//S_3mm干燥制粉样输送步
			//S_3mm存查样传送步
			//S_粉碎弃料推出步
			//S_粉碎小清洗步
			//S_粉碎制样步
			//S_粉碎大清洗步
			//S_粉碎出料步
			//S_粉碎弃料缩回步
			//S_粉碎正式样进料步
			//ST_粉碎制样无流程标记
			string lc_粉碎制样 = "";
			if (commonDAO.GetSignalDataValue(machineCode, "S_粉碎称重步") == "1")
			{
				lc_粉碎制样 = "粉碎称重步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm干燥样卸斗步") == "1")
			{
				lc_粉碎制样 = "3mm干燥样卸斗步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm干燥样接斗步") == "1")
			{
				lc_粉碎制样 = "3mm干燥样接斗步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm二级提升机直线行走电机后退步") == "1")
			{
				lc_粉碎制样 = "3mm二级提升机直线行走电机后退步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_圆盘缩分2准备步") == "1")
			{
				lc_粉碎制样 = "圆盘缩分2准备步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm二级提升机上升步") == "1")
			{
				lc_粉碎制样 = "3mm二级提升机上升步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm二级提升机倒料步") == "1")
			{
				lc_粉碎制样 = "3mm二级提升机倒料步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm二级提升机下降步") == "1")
			{
				lc_粉碎制样 = "3mm二级提升机下降步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm二级提升机直线行走电机前进步") == "1")
			{
				lc_粉碎制样 = "3mm二级提升机直线行走电机前进步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm干燥制粉样输送步") == "1")
			{
				lc_粉碎制样 = "3mm干燥制粉样输送步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm存查样传送步") == "1")
			{
				lc_粉碎制样 = "3mm存查样传送步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_粉碎弃料推出步") == "1")
			{
				lc_粉碎制样 = "粉碎弃料推出步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_粉碎小清洗步") == "1")
			{
				lc_粉碎制样 = "粉碎小清洗步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_粉碎制样步") == "1")
			{
				lc_粉碎制样 = "粉碎制样步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_粉碎大清洗步") == "1")
			{
				lc_粉碎制样 = "粉碎大清洗步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_粉碎出料步") == "1")
			{
				lc_粉碎制样 = "粉碎出料步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_粉碎弃料缩回步") == "1")
			{
				lc_粉碎制样 = "粉碎弃料缩回步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_粉碎正式样进料步") == "1")
			{
				lc_粉碎制样 = "粉碎正式样进料步";
			}
			else
			{
				lc_粉碎制样 = "无流程动作运行";
			}
			datas.Add(new HtmlDataItem("粉碎制样", lc_粉碎制样, monitorCommon.ConvertRunToColor(lc_粉碎制样 != "无流程动作运行"), eHtmlDataItemType.svg_textcolor));

			datas.Add(new HtmlDataItem("粉碎煤样编码", monitorCommon.Desalt(lc_粉碎制样 == "无流程动作运行"), eHtmlDataItemType.svg_color));
			//6mm瓶装机流程：
			//S_6mm瓶装机落瓶步
			//S_6mm瓶装机落瓶推瓶步
			//S_6mm瓶装机落瓶推瓶后退步
			//S_6mm瓶装机抱瓶翻转步
			//S_6mm瓶装机去翻转位步
			//S_6mm瓶装机去写卡步
			//S_6mm瓶装机写卡步
			//S_6mm瓶装机去暂存皮带步_空瓶_
			//S_6mm瓶装机去下料口步
			//S_6mm瓶装机回推瓶步_从下料口_
			//S_6mm瓶装机回推瓶步_从暂存皮带_空瓶
			//瓶装机进瓶无流程标记（没信号）

			string lc_6mm瓶装机流程 = "";
			//新增机采给料步判断
			if (commonDAO.GetSignalDataValue(machineCode, "机采给料步") == "1")
			{
				lc_6mm瓶装机流程 = "机采给料步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机落瓶步") == "1")
			{
				lc_6mm瓶装机流程 = "6mm瓶装机落瓶步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机落瓶推瓶步") == "1")
			{
				lc_6mm瓶装机流程 = "6mm瓶装机落瓶推瓶步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机落瓶推瓶后退步") == "1")
			{
				lc_6mm瓶装机流程 = "6mm瓶装机落瓶推瓶后退步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机抱瓶翻转步") == "1")
			{
				lc_6mm瓶装机流程 = "6mm瓶装机抱瓶翻转步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机去翻转位步") == "1")
			{
				lc_6mm瓶装机流程 = "6mm瓶装机去翻转位步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机去写卡步") == "1")
			{
				lc_6mm瓶装机流程 = "6mm瓶装机去写卡步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机写卡步") == "1")
			{
				lc_6mm瓶装机流程 = "6mm瓶装机写卡步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机去暂存皮带步_空瓶_") == "1")
			{
				lc_6mm瓶装机流程 = "6mm瓶装机去暂存皮带步_空瓶_";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机去下料口步") == "1")
			{
				lc_6mm瓶装机流程 = "6mm瓶装机去下料口步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机回推瓶步_从下料口_") == "1")
			{
				lc_6mm瓶装机流程 = "6mm瓶装机回推瓶步_从下料口_";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm瓶装机回推瓶步_从暂存皮带_空瓶") == "1")
			{
				lc_6mm瓶装机流程 = "6mm瓶装机回推瓶步_从暂存皮带_空瓶";
			}
			else
			{
				lc_6mm瓶装机流程 = "无流程动作运行";
			}
			datas.Add(new HtmlDataItem("6mm瓶装机", lc_6mm瓶装机流程, monitorCommon.ConvertRunToColor(lc_6mm瓶装机流程 != "无流程动作运行"), eHtmlDataItemType.svg_textcolor));
			//3mm瓶装机流程：
			//S_3mm瓶装机落瓶步
			//S_3mm瓶装机落瓶推瓶步
			//S_3mm瓶装机落瓶推瓶后退步
			//S_3mm瓶装机抱瓶翻转步
			//S_3mm瓶装机去翻转位步
			//S_3mm瓶装机去写卡步
			//S_3mm瓶装机写卡步
			//S_3mm瓶装机去暂存皮带步_空瓶_
			//S_3mm瓶装机去下料口步
			//S_3mm瓶装机回推瓶步_从下料口_
			//S_3mm瓶装机回推瓶步_从暂存皮带_空瓶
			//瓶装机进瓶无流程标记（没信号）

			string lc_3mm瓶装机流程 = "";
			if (commonDAO.GetSignalDataValue(machineCode, "S_3mm瓶装机落瓶步") == "1")
			{
				lc_3mm瓶装机流程 = "3mm瓶装机落瓶步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm瓶装机落瓶推瓶步") == "1")
			{
				lc_3mm瓶装机流程 = "3mm瓶装机落瓶推瓶步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm瓶装机落瓶推瓶后退步") == "1")
			{
				lc_3mm瓶装机流程 = "3mm瓶装机落瓶推瓶后退步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm瓶装机抱瓶翻转步") == "1")
			{
				lc_3mm瓶装机流程 = "3mm瓶装机抱瓶翻转步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm瓶装机去翻转位步") == "1")
			{
				lc_3mm瓶装机流程 = "3mm瓶装机去翻转位步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm瓶装机去写卡步") == "1")
			{
				lc_3mm瓶装机流程 = "3mm瓶装机去写卡步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm瓶装机写卡步") == "1")
			{
				lc_3mm瓶装机流程 = "3mm瓶装机写卡步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm瓶装机去暂存皮带步_空瓶_") == "1")
			{
				lc_3mm瓶装机流程 = "3mm瓶装机去暂存皮带步_空瓶_";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm瓶装机去下料口步") == "1")
			{
				lc_3mm瓶装机流程 = "3mm瓶装机去下料口步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm瓶装机回推瓶步_从下料口_") == "1")
			{
				lc_3mm瓶装机流程 = "3mm瓶装机回推瓶步_从下料口_";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_3mm瓶装机回推瓶步_从暂存皮带_空瓶") == "1")
			{
				lc_3mm瓶装机流程 = "3mm瓶装机回推瓶步_从暂存皮带_空瓶";
			}
			else
			{
				lc_3mm瓶装机流程 = "无流程动作运行";
			}
			datas.Add(new HtmlDataItem("3mm瓶装机", lc_3mm瓶装机流程, monitorCommon.ConvertRunToColor(lc_3mm瓶装机流程 != "无流程动作运行"), eHtmlDataItemType.svg_textcolor));
			datas.Add(new HtmlDataItem("3mm瓶装机煤样编码", monitorCommon.Desalt(lc_3mm瓶装机流程 == "无流程动作运行"), eHtmlDataItemType.svg_color));
			//弃料流程：
			//S_在线测水弃料步
			//S_6mm弃料收集步
			//S_弃料双向皮带排空步

			string lc_弃料流程 = "";
			if (commonDAO.GetSignalDataValue(machineCode, "S_在线测水弃料步") == "1")
			{
				lc_弃料流程 = "在线测水弃料步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_6mm弃料收集步") == "1")
			{
				lc_弃料流程 = "6mm弃料收集步";
			}
			else if (commonDAO.GetSignalDataValue(machineCode, "S_弃料双向皮带排空步") == "1")
			{
				lc_弃料流程 = "弃料双向皮带排空步";
			}
			else
			{
				lc_弃料流程 = "无流程动作运行";
			}
			datas.Add(new HtmlDataItem("弃料流程", lc_弃料流程, monitorCommon.ConvertRunToColor(lc_弃料流程 != "无流程动作运行"), eHtmlDataItemType.svg_textcolor));
			#endregion

			// 发送到页面
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);


			//出样信息
			List<InfMakerRecord> listMakerRecord = automakerDAO.GetMakerRecordByMakeCode(makeCode);
			List<object> listRes = new List<object>();
			foreach (InfMakerRecord item in listMakerRecord)
			{
				//获取样瓶传输状态
				string Status = automakerDAO.GetMakerRecordStatusByBarrelCode(item.BarrelCode);
				var makerRecord = new
				{
					EndTime = item.EndTime.ToString("yyyy-MM-dd HH:mm"),
					YPType = item.YPType,
					BarrelCode = item.BarrelCode,
					YPWeight = item.YPWeight,
					Status = Status
				};
				listRes.Add(makerRecord);
			}
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadSampleInfo(" + Newtonsoft.Json.JsonConvert.SerializeObject(listRes) + ");", "", 0);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			// 界面不可见时，停止发送数据
			if (!this.Visible) return;

			RequestData();
		}

		/// <summary>
		/// 刷新页面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			cefWebBrowser.Browser.Reload();
		}

		private void btnRequestData_Click(object sender, EventArgs e)
		{
			RequestData();
		}

		private void buttonX1_Click(object sender, EventArgs e)
		{
			// 发送到页面
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("testColor();", "", 0);
		}

		/// <summary>
		/// 开始制样
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStartMake_Click(object sender, EventArgs e)
		{
			if (MessageBoxEx.Show("确定开始制样！", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel) return;

			string MakeCode = string.Empty;
			ButtonX buttonX = sender as ButtonX;
			if (buttonX == null) return;

			if (buttonX.Name == "btnStartMake")
				MakeCode = txtMakeCode.Text;
			else if (buttonX.Name == "btnStartMake_RG")
				MakeCode = txtMakeCode_RG.Text;

			if (string.IsNullOrEmpty(MakeCode))
			{
				MessageBox.Show("请输入制样码", "提示");
				return;
			}
			string value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, eSignalDataName.设备状态.ToString());
			if (value != "允许制样")
			{
				MessageBox.Show("制样机未准备好！", "提示");
				return;
			}

			CmcsRCMake rcMake = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode", new { MakeCode = MakeCode.Trim() });
			if (rcMake == null)
			{
				MessageBox.Show("未找到制样记录", "提示");
				return;
			}
			IList<CmcsRCSampleBarrel> samplebarrels = commonDAO.SelfDber.Entities<CmcsRCSampleBarrel>("where SamplingId=:SamplingId", new { SamplingId = rcMake.SamplingId });
			if (samplebarrels != null && samplebarrels.Count > 0)
			{
				double weight = commonDAO.GetSignalDataValueDouble(GlobalVars.MachineCode_QZDZYJ_1, "原煤称实时重量");
				double mathweight = commonDAO.GetCommonAppletConfigDouble("制样超差重量");
				if (Math.Abs(samplebarrels.Sum(a => a.SampleWeight) - weight) > mathweight)
				{
					MessageBox.Show("制样重量与原始样重超差！", "提示");
					return;
				}
			}

			//if (weight > 0)
			//{
			//	MessageBox.Show("制样料斗有料，不允许发送指令！", "提示");
			//	return;
			//}
			string currentMessage = string.Empty;

			InfMakerControlCmd makerControlCmd = new InfMakerControlCmd();
			makerControlCmd.InterfaceType = GlobalVars.InterfaceType_QZDZYJ;
			makerControlCmd.MachineCode = GlobalVars.MachineCode_QZDZYJ_1;
			makerControlCmd.MakeCode = rcMake.MakeCode;
			makerControlCmd.ResultCode = eEquInfCmdResultCode.默认.ToString();
			makerControlCmd.CmdCode = eEquInfMakerCmd.开始制样.ToString();
			makerControlCmd.SyncFlag = 0;
			if (Dbers.GetInstance().SelfDber.Insert<InfMakerControlCmd>(makerControlCmd) > 0)
			{
				commonDAO.SaveOperationLog("给制样机发送开始制样命令，制样码：" + rcMake.MakeCode, GlobalVars.LoginUser.Name);

				MessageBox.Show("命令发送成功", "提示");
				return;
			}


			//FrmBatchMachineBarrel_Select frm = new FrmBatchMachineBarrel_Select("");
			//if (frm.ShowDialog() == DialogResult.OK)
			//{
			//	BatchMachineBarrel_Select content = frm.Output;

			//	CmcsRCSampling rcSampling = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCSampling>("where SampleCode=:SampleCode", new { SampleCode = content.SampleID });
			//	if (rcSampling == null)
			//	{
			//		MessageBox.Show("未找到采样记录", "提示");
			//		return;
			//	}

			//	CmcsRCMake rcMake = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCMake>("where SamplingId=:SamplingId", new { SamplingId = rcSampling.Id });
			//	if (rcMake == null)
			//	{
			//		MessageBox.Show("未找到制样记录", "提示");
			//		return;
			//	}

			//	string currentMessage = string.Empty;
			//	InfBatchMachineCmd batchMachineCmd = new InfBatchMachineCmd();
			//	batchMachineCmd.InterfaceType= GlobalVars.MachineCode_HYGPJ_1;
			//	batchMachineCmd.MachineCode= GlobalVars.MachineCode_HYGPJ_1;
			//	batchMachineCmd.CmdCode = eEquInfBatchMachineCmd.倒料.ToString();
			//	batchMachineCmd.SampleCode = rcSampling.SampleCode;
			//	batchMachineCmd.ResultCode = eEquInfCmdResultCode.默认.ToString();
			//	batchMachineCmd.SyncFlag = 0;

			//	if (Dbers.GetInstance().SelfDber.Insert<InfBatchMachineCmd>(batchMachineCmd) > 0)
			//	{
			//		InfMakerControlCmd makerControlCmd = new InfMakerControlCmd();
			//		makerControlCmd.InterfaceType = GlobalVars.InterfaceType_QZDZYJ;
			//		makerControlCmd.MachineCode = GlobalVars.MachineCode_QZDZYJ_1;
			//		makerControlCmd.MakeCode = rcMake.MakeCode;
			//		makerControlCmd.ResultCode = eEquInfCmdResultCode.默认.ToString();
			//		makerControlCmd.CmdCode = eEquInfMakerCmd.开始制样.ToString();
			//		makerControlCmd.SyncFlag = 0;
			//		if (Dbers.GetInstance().SelfDber.Insert<InfMakerControlCmd>(makerControlCmd) > 0)
			//		{
			//			commonDAO.SaveOperationLog("给制样机发送开始制样命令，制样码：" + rcMake.MakeCode, GlobalVars.LoginUser.Name);

			//			MessageBox.Show("命令发送成功", "提示");
			//			return;
			//		}
			//	}
			//}


		}

		/// <summary>
		/// 暂停制样
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDownMake_Click(object sender, EventArgs e)
		{
			if (MessageBoxEx.Show("确定暂停制样！", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel) return;

			CmcsRCMake rcMake = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode", new { MakeCode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "原煤煤样编码") });
			if (rcMake == null)
			{
				MessageBox.Show("未找到制样记录", "提示");
				return;
			}

			string currentMessage = string.Empty;
			InfMakerControlCmd makerControlCmd = new InfMakerControlCmd();
			makerControlCmd.InterfaceType = GlobalVars.InterfaceType_QZDZYJ;
			makerControlCmd.MachineCode = GlobalVars.MachineCode_QZDZYJ_1;
			makerControlCmd.MakeCode = rcMake.MakeCode;
			makerControlCmd.ResultCode = eEquInfCmdResultCode.默认.ToString();
			makerControlCmd.CmdCode = eEquInfMakerCmd.停止制样.ToString();
			makerControlCmd.SyncFlag = 0;
			if (Dbers.GetInstance().SelfDber.Insert<InfMakerControlCmd>(makerControlCmd) > 0)
			{
				commonDAO.SaveOperationLog("给制样机发送停止制样命令", GlobalVars.LoginUser.Name);

				MessageBox.Show("命令发送成功", "提示");
				return;
			}
		}

		/// <summary>
		/// 继续制样
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnKeepMake_Click(object sender, EventArgs e)
		{
			if (MessageBoxEx.Show("确定继续制样！", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel) return;

			CmcsRCMake rcMake = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode", new { MakeCode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "原煤煤样编码") });
			if (rcMake == null)
			{
				MessageBox.Show("未找到制样记录", "提示");
				return;
			}

			string currentMessage = string.Empty;
			InfMakerControlCmd makerControlCmd = new InfMakerControlCmd();
			makerControlCmd.InterfaceType = GlobalVars.InterfaceType_QZDZYJ;
			makerControlCmd.MachineCode = GlobalVars.MachineCode_QZDZYJ_1;
			makerControlCmd.MakeCode = rcMake.MakeCode;
			makerControlCmd.ResultCode = eEquInfCmdResultCode.默认.ToString();
			makerControlCmd.CmdCode = eEquInfMakerCmd.继续制样.ToString();
			makerControlCmd.SyncFlag = 0;
			if (Dbers.GetInstance().SelfDber.Insert<InfMakerControlCmd>(makerControlCmd) > 0)
			{
				commonDAO.SaveOperationLog("给制样机发送继续制样命令", GlobalVars.LoginUser.Name);

				MessageBox.Show("命令发送成功", "提示");
				return;
			}
		}

		private void btnRead_Click(object sender, EventArgs e)
		{
			InfBatchMachineCmd cmd = CommonDAO.GetInstance().SelfDber.Entity<InfBatchMachineCmd>("where ResultCode='成功' order by CreationTime desc");
			if (cmd == null)
			{
				MessageBox.Show("未找到倒样成功记录", "提示");
				return;
			}


			CmcsRCSampling rcSampling = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCSampling>("where SampleCode=:SampleCode", new { SampleCode = cmd.SampleCode });
			CmcsRLSampling rlSampling = CommonDAO.GetInstance().SelfDber.Entity<CmcsRLSampling>("where SampleCode=:SampleCode", new { SampleCode = cmd.SampleCode });
			if (rcSampling == null && rlSampling == null)
			{
				MessageBox.Show("未找到采样记录", "提示");
				return;
			}

			string SamplingId = "";
			CmcsRCMake rlMake = null;
			CmcsRCMake rcMake = null;

			if (rlSampling != null)
			{
				SamplingId = rlSampling.Id;
				rlMake = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCMake>("where RlSamplingId=:SamplingId", new { SamplingId = SamplingId });
			}
			if (rcSampling != null)
			{
				SamplingId = rcSampling.Id;
				rcMake = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCMake>("where SamplingId=:SamplingId", new { SamplingId = SamplingId });
			}

			if (rlMake == null && rcMake == null)
			{
				MessageBox.Show("未找到制样记录", "提示");
				return;
			}

			if (rlMake != null)
			{
				InfMakerRecord makeRecord = CommonDAO.GetInstance().SelfDber.Entity<InfMakerRecord>("where MakeCode=:MakeCode", new { MakeCode = rlMake.MakeCode });
				if (makeRecord != null)
				{
					MessageBox.Show("未找到没有制样的编码", "提示");
					return;
				}
				decimal ymzl = Convert.ToDecimal(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "原煤重量"));
				string ymbm = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "原煤煤样编码");
				if (ymbm == rlMake.MakeCode && ymzl > 0)
				{
					MessageBox.Show("未找到没有制样的编码", "提示");
					return;
				}

				txtMakeCode.Text = rlMake.MakeCode;
			}
			if (rcMake != null)
			{
				InfMakerRecord makeRecord = CommonDAO.GetInstance().SelfDber.Entity<InfMakerRecord>("where MakeCode=:MakeCode", new { MakeCode = rcMake.MakeCode });
				if (makeRecord != null)
				{
					MessageBox.Show("未找到没有制样的编码", "提示");
					return;
				}
				decimal ymzl = Convert.ToDecimal(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "原煤重量"));
				string ymbm = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "原煤煤样编码");
				if (ymbm == rcMake.MakeCode && ymzl > 0)
				{
					MessageBox.Show("未找到没有制样的编码", "提示");
					return;
				}

				txtMakeCode.Text = rcMake.MakeCode;
			}
		}

		/// <summary>
		/// 数据查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDataSelect_Click(object sender, EventArgs e)
		{
			CMCS.InterfaceData.Win.DumblyTasks.FrmAutoMaker frm = new CMCS.InterfaceData.Win.DumblyTasks.FrmAutoMaker();
			frm.ShowDialog();
		}

		/// <summary>
		/// 设备信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeviceInfo_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 弃料超限复位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnQlCxwfw_Click(object sender, EventArgs e)
		{
			if (MessageBoxEx.Show("确定弃料超限复位！", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
			{
				commonDAO.SendAppRemoteControlCmd("#1全自动制样机", "弃料超限复位", "1");
				commonDAO.SaveOperationLog("设置#1全自动制样机弃料超限复位", GlobalVars.LoginUser.Name);
			}
		}

		/// <summary>
		/// 故障复位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFaultReset_Click(object sender, EventArgs e)
		{
			if (MessageBoxEx.Show("确定故障复位！", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
			{
				commonDAO.SendAppRemoteControlCmd("#1全自动制样机", "故障复位", "1");
				commonDAO.SaveOperationLog("设置#1全自动制样机故障复位", GlobalVars.LoginUser.Name);
			}
		}

		/// <summary>
		/// 绑定制样编码信息
		/// </summary>
		private void BindSGC_MakeCodeInfo()
		{
			DateTime dtStart = dtInputSampleDate.Value;
			DateTime dtEnd = dtStart.AddDays(1);
			string sql = @"select t.makecode, count(b.id) as sampleCount
                         from cmcstbmake t
                         left join cmcstbrcsampling a
                           on t.samplingid = a.id
                         left join cmcstbrcsampingdetail b
                           on a.id = b.samplingid
                        where a.isdeleted=0 and b.isdeleted=0 and t.isdeleted=0 and a.samplingdate >= to_date('" + dtStart + "', 'yyyy-MM-dd HH24:MI:SS') and a.samplingdate < to_date('" + dtEnd + "', 'yyyy-MM-dd HH24:MI:SS') group by t.makecode ";

			DataTable dataTable = Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
			SGC_MakeCodeInfo.PrimaryGrid.DataSource = dataTable;
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			BindSGC_MakeCodeInfo();
		}

		#region superGridControl

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
		/// 双击行时，自动填充供煤单位、矿点等信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl_BuyFuel_CellDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs e)
		{
			GridRow gridRow = (sender as SuperGridControl).PrimaryGrid.ActiveRow as GridRow;
			if (gridRow == null) return;

			DataRowView drv = gridRow.DataItem as DataRowView;
			if (drv == null) return;

			txtMakeCode_RG.Text = drv.Row.ItemArray[0].ToString();
		}
		#endregion
	}
}
