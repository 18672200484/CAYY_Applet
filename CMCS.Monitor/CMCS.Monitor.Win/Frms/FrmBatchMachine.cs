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

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmBatchMachine : MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmBatchMachine";

		CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

		CommonDAO commonDAO = CommonDAO.GetInstance();
		public FrmBatchMachine()
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
			cefWebBrowser.StartUrl = SelfVars.Url_BatchMachine;
			cefWebBrowser.Dock = DockStyle.Fill;
			cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
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
			machineCode = GlobalVars.MachineCode_HYGPJ_1;

			///信号接入
			//datas.Add(new HtmlDataItem("故障提示", commonDAO.GetSignalDataValue(machineCode, "设备状态") == "发生故障" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("湿煤破碎电机", commonDAO.GetSignalDataValue(machineCode, "湿煤破碎机") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));



			datas.Add(new HtmlDataItem("X轴位置", commonDAO.GetSignalDataValue(machineCode, "当前轴位置"), eHtmlDataItemType.svg_text));
			value = commonDAO.GetSignalDataValue(machineCode, "制样机卸料准备好信号");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("制样机卸料准备好信号", "制样机卸料准备好", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("制样机卸料准备好信号", "#00ff00", eHtmlDataItemType.svg_color));
			}
			else
			{
				datas.Add(new HtmlDataItem("制样机卸料准备好信号", "制样机卸料未准备好", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("制样机卸料准备好信号", "#ff0000", eHtmlDataItemType.svg_color));
			}
			value = commonDAO.GetSignalDataValue(machineCode, "系统自动1手动0");
			if(value == "1")
			{
				datas.Add(new HtmlDataItem("本地", "#000000", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("自动", "#ff0000", eHtmlDataItemType.svg_color));
			}
			else
			{
				datas.Add(new HtmlDataItem("本地", "#ff0000", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("自动", "#000000", eHtmlDataItemType.svg_color));
			}

			value = commonDAO.GetSignalDataValue(machineCode, "归批流程运行中");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("归批流程", "运行中", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("归批流程", "#00ff00", eHtmlDataItemType.svg_color));
			}
			else
			{
				datas.Add(new HtmlDataItem("归批流程", "未运行", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("归批流程", "#ff0000", eHtmlDataItemType.svg_color));
			}

			datas.Add(new HtmlDataItem("存桶选择工位", commonDAO.GetSignalDataValue(machineCode, "存桶选择工位"), eHtmlDataItemType.svg_text));

			//datas.Add(new HtmlDataItem("归批流程步", commonDAO.GetSignalDataValue(machineCode, "归批流程步"), eHtmlDataItemType.svg_text));
			value = commonDAO.GetSignalDataValue(machineCode, "归批流程状态");
			string gplc = "空闲";
			if (value == "3")
			{
				gplc = "进桶到读卡位步";
			}
			else if(value == "4")
			{
				gplc = "读卡与判断步";
			}
			else if (value == "5")
			{
				gplc = "进桶选桶步";
			}
			else if (value == "6")
			{
				gplc = "进桶到存桶工位步";
			}
			else if(value == "7")
			{
				gplc = "故障桶排空步";
			}
			else if (value == "8")
			{
				gplc = "完成步";
			}
			datas.Add(new HtmlDataItem("归批流程步", gplc, eHtmlDataItemType.svg_text));


			datas.Add(new HtmlDataItem("归批读卡编码", commonDAO.GetSignalDataValue(machineCode, "归批读卡编码"), eHtmlDataItemType.svg_text));

			value = commonDAO.GetSignalDataValue(machineCode, "倒料流程运行中");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("倒料流程", "运行中", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("倒料流程", "#00ff00", eHtmlDataItemType.svg_color));
			}
			else
			{
				datas.Add(new HtmlDataItem("倒料流程", "未运行", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("倒料流程", "#ff0000", eHtmlDataItemType.svg_color));
			}
			datas.Add(new HtmlDataItem("倒料选择工位", commonDAO.GetSignalDataValue(machineCode, "倒料选择工位"), eHtmlDataItemType.svg_text));

			//datas.Add(new HtmlDataItem("倒料流程步", commonDAO.GetSignalDataValue(machineCode, "倒料流程步"), eHtmlDataItemType.svg_text));
			value = commonDAO.GetSignalDataValue(machineCode, "倒料流程运行");
			string dllc = "空闲";
			if (value == "3")
			{
				dllc = "进桶到读卡位步";
			}
			else if (value == "4")
			{

			}
			else if (value == "5")
			{
				dllc = "进桶选桶步";
			}
			else if (value == "6")
			{
				dllc = "进桶到存桶工位步";
			}
			else if (value == "7")
			{
				dllc = "故障桶排空步";
			}
			else if (value == "8")
			{
				dllc = "完成步";
			}
			datas.Add(new HtmlDataItem("倒料流程步", dllc, eHtmlDataItemType.svg_text));


			datas.Add(new HtmlDataItem("倒料读卡编码", commonDAO.GetSignalDataValue(machineCode, "倒料读卡编码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("倒料编码", commonDAO.GetSignalDataValue(machineCode, "倒料编码给定"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("倒料模式", commonDAO.GetSignalDataValue(machineCode, "倒料模式"), eHtmlDataItemType.svg_text));

			//样桶信息
			List<InfBatchMachineBarrel> barrel = Dbers.GetInstance().SelfDber.Entities<InfBatchMachineBarrel>();
			foreach(InfBatchMachineBarrel item in barrel)
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
			#endregion

			

			//异常信息
			string sql = string.Format(@"select
										a.signalprefix,a.signalname,a.updatetime
										from cmcstbsignaldata a where a.signalprefix like '%合样归批机%' and a.signalname like 'M_%'and a.signalvalue='1' order by updatetime desc
										");
			DataTable dt = commonDAO.SelfDber.ExecuteDataTable(sql);
			List<WarningTemp> list = new List<WarningTemp>();

			if (dt.Rows.Count > 6)
			{
				for (int i = 0; i < 6; i++)
				{
					WarningTemp entity = new WarningTemp();
					entity.tDate = Convert.ToDateTime(dt.Rows[i]["updatetime"]).ToShortDateString();
					entity.tTime = Convert.ToDateTime(dt.Rows[i]["updatetime"]).ToShortTimeString();
					entity.MachineName = dt.Rows[i]["signalprefix"].ToString();
					entity.Remark = dt.Rows[i]["signalname"].ToString().TrimStart('M').TrimStart('_');
					list.Add(entity);
				}
			}
			else
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					WarningTemp entity = new WarningTemp();
					entity.tDate = Convert.ToDateTime(dt.Rows[i]["updatetime"]).ToShortDateString();
					entity.tTime = Convert.ToDateTime(dt.Rows[i]["updatetime"]).ToShortTimeString();
					entity.MachineName = dt.Rows[i]["signalprefix"].ToString();
					entity.Remark = dt.Rows[i]["signalname"].ToString().TrimStart('M').TrimStart('_');
					list.Add(entity);
				}
				for (int i = dt.Rows.Count; i < 6; i++)
				{
					WarningTemp entity = new WarningTemp();
					entity.tDate = "";
					entity.tTime = "";
					entity.MachineName = "";
					entity.Remark = "";
					list.Add(entity);
				}
			}
			datas.Add(new HtmlDataItem("报警", dt.Rows.Count>0 ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("报警数目", dt.Rows.Count.ToString(), eHtmlDataItemType.svg_text)) ;

			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadHitchs(" + Newtonsoft.Json.JsonConvert.SerializeObject(list.Select(a => new { tDate = a.tDate, tTime = a.tTime, MachineName = a.MachineName, Remark = a.Remark })) + ");", "", 0);

			// 发送到页面
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

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
			//CommonDAO commonDAO = CommonDAO.GetInstance();
			//commonDAO.SaveSysMessage("合样归批倒料", "合样归批倒料执行成功");
		}

		/// <summary>
		/// 归批流程暂停
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnZT_GPLC_Click(object sender, EventArgs e)
		{
			if (MessageBoxEx.Show("确定归批流程暂停！", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
			{
				commonDAO.SendAppRemoteControlCmd(GlobalVars.MachineCode_HYGPJ_1, "归批流程暂停", "1");
				commonDAO.SaveOperationLog("设置"+ GlobalVars.MachineCode_HYGPJ_1 + "归批流程暂停", GlobalVars.LoginUser.Name);
			}
		}

		/// <summary>
		/// 倒料流程暂停
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnZT_DLLC_Click(object sender, EventArgs e)
		{
			if (MessageBoxEx.Show("确定倒料流程暂停！", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
			{
				commonDAO.SendAppRemoteControlCmd(GlobalVars.MachineCode_HYGPJ_1, "倒料流程暂停", "1");
				commonDAO.SaveOperationLog("设置" + GlobalVars.MachineCode_HYGPJ_1 + "倒料流程暂停", GlobalVars.LoginUser.Name);
			}
		}

		private void btnFW_DLLC_Click(object sender, EventArgs e)
		{
			if (MessageBoxEx.Show("确定倒料流程复位！", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
			{
				commonDAO.SendAppRemoteControlCmd(GlobalVars.MachineCode_HYGPJ_1, "倒料流程复位", "1");
				commonDAO.SaveOperationLog("设置" + GlobalVars.MachineCode_HYGPJ_1 + "倒料流程复位", GlobalVars.LoginUser.Name);
			}
		}

		private void btnFW_GPLC_Click(object sender, EventArgs e)
		{
			if (MessageBoxEx.Show("确定归批流程复位！", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
			{
				commonDAO.SendAppRemoteControlCmd(GlobalVars.MachineCode_HYGPJ_1, "归批流程复位", "1");
				commonDAO.SaveOperationLog("设置" + GlobalVars.MachineCode_HYGPJ_1 + "归批流程复位", GlobalVars.LoginUser.Name);
			}
		}
	}
}
