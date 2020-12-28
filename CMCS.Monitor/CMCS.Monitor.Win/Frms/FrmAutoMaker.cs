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

		CefWebBrowser cefWebBrowser = new CefWebBrowser();

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
			cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
			panWebBrower.Controls.Add(cefWebBrowser);
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
			CommonDAO commonDAO = CommonDAO.GetInstance();
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

			value = commonDAO.GetSignalDataValue(machineCode, "湿煤破碎机");
			datas.Add(new HtmlDataItem("湿煤破碎机1", value == "1" ? "Red" : "url(#SVGID_120_)", eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("湿煤破碎机2", value == "1" ? "Red" : "url(#SVGID_120_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("湿煤破碎机3", value == "1" ? "Red" : "url(#SVGID_121_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("湿煤破碎机4", value == "1" ? "Red" : "#606060", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("湿煤破碎机5", value == "1" ? "Red" : "#FD0B08", eHtmlDataItemType.svg_color));
			
			value = commonDAO.GetSignalDataValue(machineCode, "链式缩分驱动器运行信号");
			datas.Add(new HtmlDataItem("链式缩分器1", value == "1" ? "Red" : "url(#SVGID_122_)", eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("链式缩分器2", value == "1" ? "Red" : "url(#SVGID_122_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("链式缩分器3", value == "1" ? "Red" : "#CEC9C6", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("链式缩分器4", value == "1" ? "Red" : "#BCB296", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("链式缩分器5", value == "1" ? "Red" : "#F6E1C6", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("链式缩分器6", value == "1" ? "Red" : "#FA090D", eHtmlDataItemType.svg_color));

			value = commonDAO.GetSignalDataValue(machineCode, "对辊破碎机");
			datas.Add(new HtmlDataItem("对辊破碎机1", value == "1" ? "Red" : "url(#SVGID_113_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("对辊破碎机2", value == "1" ? "Red" : "url(#SVGID_114_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("对辊破碎机3", value == "1" ? "Red" : "#FA090D", eHtmlDataItemType.svg_color));

			value = commonDAO.GetSignalDataValue(machineCode, "3mm一级缩分器驱动器运行信号");
			datas.Add(new HtmlDataItem("3mm一级缩分器1", value == "1" ? "Red" : "url(#SVGID_129_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm一级缩分器2", value == "1" ? "Red" : "#000000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm一级缩分器3", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm一级缩分器4", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm一级缩分器5", value == "1" ? "Red" : "#FA090D", eHtmlDataItemType.svg_color));

			value = commonDAO.GetSignalDataValue(machineCode, "3mm弃料一级皮带");
			datas.Add(new HtmlDataItem("3mm弃料皮带", value == "1" ? "Red" : "url(#SVGID_116_)", eHtmlDataItemType.svg_color));

			//value = commonDAO.GetSignalDataValue(machineCode, "粉碎单元真空上料机");
			//datas.Add(new HtmlDataItem("弃料真空上料机", value == "1" ? "Red" : "url(#g1256)", eHtmlDataItemType.svg_color));

			value = commonDAO.GetSignalDataValue(machineCode, "3mm筛分破碎机正转");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("筛分破碎1", value == "1" ? "Red" : "url(#SVGID_130_)", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("筛分破碎2", value == "1" ? "Red" : "url(#SVGID_132_)", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("筛分破碎3", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
				//datas.Add(new HtmlDataItem("筛分破碎4", value == "1" ? "Red" : "url(#SVGID_130_)", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("筛分破碎5", value == "1" ? "Red" : "#FA090D", eHtmlDataItemType.svg_color));
			}
			else
			{
				datas.Add(new HtmlDataItem("筛分破碎1", value == "1" ? "Red" : "url(#SVGID_130_)", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("筛分破碎2", value == "1" ? "Red" : "url(#SVGID_132_)", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("筛分破碎3", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
				//datas.Add(new HtmlDataItem("筛分破碎4", value == "1" ? "Red" : "url(#SVGID_130_)", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("筛分破碎5", value == "1" ? "Red" : "#FA090D", eHtmlDataItemType.svg_color));
			}

			value = commonDAO.GetSignalDataValue(machineCode, "3mm二级缩分器驱动器运行信号");
			datas.Add(new HtmlDataItem("3mm二级圆盘缩分器1", value == "1" ? "Red" : "url(#SVGID_104_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm二级圆盘缩分器2", value == "1" ? "Red" : "#000000", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm二级圆盘缩分器3", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm二级圆盘缩分器4", value == "1" ? "Red" : "#FA090D", eHtmlDataItemType.svg_color));

			value = commonDAO.GetSignalDataValue(machineCode, "0_2mm制粉机变频器运行信号");
			datas.Add(new HtmlDataItem("粉碎机1", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("粉碎机2", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("粉碎机3", value == "1" ? "Red" : "url(#SVGID_150_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("粉碎机4", value == "1" ? "Red" : "url(#SVGID_148_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("粉碎机5", value == "1" ? "Red" : "url(#SVGID_149_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("粉碎机6", value == "1" ? "Red" : "#545452", eHtmlDataItemType.svg_color));

			value = commonDAO.GetSignalDataValue(machineCode, "粉碎单元真空上料机");
			datas.Add(new HtmlDataItem("真空上料机1", value == "1" ? "Red" : "url(#SVGID_35_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("真空上料机2", value == "1" ? "Red" : "url(#SVGID_37_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("真空上料机3", value == "1" ? "Red" : "url(#SVGID_36_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("真空上料机4", value == "1" ? "Red" : "url(#SVGID_39_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("真空上料机5", value == "1" ? "Red" : "url(#SVGID_40_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("真空上料机6", value == "1" ? "Red" : "url(#SVGID_38_)", eHtmlDataItemType.svg_color));

			value = commonDAO.GetSignalDataValue(machineCode, "左侧PTC加热器");
			datas.Add(new HtmlDataItem("左侧PTC加热器1", value == "1" ? "Red" : "url(#SVGID_137_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("左侧PTC加热器2", value == "1" ? "Red" : "url(#SVGID_133_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("左侧PTC加热器3", value == "1" ? "Red" : "url(#SVGID_138_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("左侧PTC加热器4", value == "1" ? "Red" : "url(#SVGID_134_)", eHtmlDataItemType.svg_color));

			value = commonDAO.GetSignalDataValue(machineCode, "右侧PTC加热器");
			datas.Add(new HtmlDataItem("右侧PTC加热器1", value == "1" ? "Red" : "url(#SVGID_140_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("右侧PTC加热器2", value == "1" ? "Red" : "url(#SVGID_139_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("右侧PTC加热器3", value == "1" ? "Red" : "url(#SVGID_144_)", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("右侧PTC加热器4", value == "1" ? "Red" : "url(#SVGID_143_)", eHtmlDataItemType.svg_color));
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
			if (string.IsNullOrEmpty(txtMakeCode.Text))
			{
				MessageBox.Show("请输入制样码", "提示");
				return;
			}

			CmcsRCMake rcMake = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode", new { MakeCode = txtMakeCode.Text });
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
			makerControlCmd.CmdCode = eEquInfMakerCmd.开始制样.ToString();
			makerControlCmd.SyncFlag = 0;
			if (Dbers.GetInstance().SelfDber.Insert<InfMakerControlCmd>(makerControlCmd) > 0)
			{
				MessageBox.Show("命令发送成功", "提示");
				return;
			}
		}

		private void btnDownMake_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtMakeCode.Text))
			{
				MessageBox.Show("请输入制样码", "提示");
				return;
			}

			CmcsRCMake rcMake = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode", new { MakeCode = txtMakeCode.Text });
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
				MessageBox.Show("命令发送成功", "提示");
				return;
			}
		}

		private void btnKeepMake_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtMakeCode.Text))
			{
				MessageBox.Show("请输入制样码", "提示");
				return;
			}

			CmcsRCMake rcMake = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode", new { MakeCode = txtMakeCode.Text });
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
				MessageBox.Show("命令发送成功", "提示");
				return;
			}
		}
	}
}
