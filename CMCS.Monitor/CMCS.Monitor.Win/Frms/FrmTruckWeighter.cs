using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.CarTransport.DAO;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using CMCS.Monitor.Win.Utilities;
using DevComponents.DotNetBar;
using Xilium.CefGlue.WindowsForms;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmTruckWeighter : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmTruckWeighter";

        CommonDAO commonDAO = CommonDAO.GetInstance();
        WeighterDAO weighterDAO = WeighterDAO.GetInstance();
        MonitorCommon monitorCommon = MonitorCommon.GetInstance();
        /// <summary>
        /// 语音播报
        /// </summary>
        VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

        CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

        string currentMachineCode = GlobalVars.MachineCode_QC_Weighter_1;
        /// <summary>
        /// 当前选中的衡器
        /// </summary>
        public string CurrentMachineCode
        {
            get { return currentMachineCode; }
            set { currentMachineCode = value; }
        }

        public FrmTruckWeighter()
        {
            InitializeComponent();
            //currentMachineCode = machineCode;
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

            cefWebBrowser.StartUrl = SelfVars.Url_TruckWeighter;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);

            //语音设置
            voiceSpeaker.SetVoice(0, 100, "Girl XiaoKun");

            //初次加载需要清除语音提示内容
            commonDAO.SetAppletConfig(GlobalVars.MachineCode_QC_Weighter_1, "语音播放信息", "");
            commonDAO.SetAppletConfig(GlobalVars.MachineCode_QC_Weighter_2, "语音播放信息", "");
            commonDAO.SetAppletConfig(GlobalVars.MachineCode_QC_Weighter_3, "语音播放信息", "");
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;

            RequestData();

            //首次加载刷新列表
            btnRefreshBuyTransport_Click(null, null);
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

            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("changeSelected('" + machineCode + "');", "", 0);

            string machine = "";
            if (machineCode.Contains("1"))
                machine = "#1汽车衡";
            else if (machineCode.Contains("空车"))
                machine = "空车衡";
            else if (machineCode.Contains("3"))
                machine = "#3汽车衡";

            datas.Add(new HtmlDataItem("汽车衡_当前衡器", machine, eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("汽车衡_1号衡系统", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_2号衡系统", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_3号衡系统", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_3, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_系统", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_IO控制器", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.IO控制器_连接状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_地磅仪表", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.地磅仪表_连接状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_LED屏", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.LED屏1_连接状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_读卡器1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.读卡器1_连接状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_读卡器2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.读卡器2_连接状态.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("汽车衡_仪表重量", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.地磅仪表_实时重量.ToString()) + " 吨", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("汽车衡_仪表重量", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.地磅仪表_稳定.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.BeReady) : ColorTranslator.ToHtml(EquipmentStatusColors.Working), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_当前车号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("汽车衡_LED屏显示", commonDAO.GetSignalDataValue(machineCode, "LED屏显示信息"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("汽车衡_卡车", (!string.IsNullOrEmpty(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()))).ToString(), eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("汽车衡_地感1", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.地感1信号.ToString()).ToLower() == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_地感2", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.地感2信号.ToString()).ToLower() == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_对射1", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.对射1信号.ToString()).ToLower() == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_对射2", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.对射2信号.ToString()).ToLower() == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("汽车衡_道闸1", (commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸1升杆.ToString()) == "1").ToString(), eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("汽车衡_道闸2", (commonDAO.GetSignalDataValue(machineCode, eSignalDataName.道闸2升杆.ToString()) == "1").ToString(), eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("汽车衡_卡车", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.上磅方向.ToString()), eHtmlDataItemType.svg_scare));

            datas.Add(new HtmlDataItem("汽车衡_过衡启用状态", commonDAO.GetAppletConfigString(machineCode, "启动过衡") == "1" ? "过衡已开启" : "过衡已停止", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("汽车衡_过衡启用状态", commonDAO.GetAppletConfigString(machineCode, "启动过衡") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_磅体", commonDAO.GetAppletConfigString(machineCode, "启动过衡") == "1" ? "#00ff00" : "#c0c0c0", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("汽车衡_当前衡器", commonDAO.GetAppletConfigString(machineCode, "启动过衡") == "1" ? "#00ff00" : "#fbfbff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("汽车衡_采样启用状态", commonDAO.GetAppletConfigString(machineCode, "启动采样") == "1" ? "采样已开启" : "采样已停止", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("汽车衡_采样启用状态", commonDAO.GetAppletConfigString(machineCode, "启动采样") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            // 添加更多...

            if (machine == "空车衡")
            {
                datas.Add(new HtmlDataItem("QGCY", "0", eHtmlDataItemType.btn_visible));
                datas.Add(new HtmlDataItem("TZCY", "0", eHtmlDataItemType.btn_visible));
                datas.Add(new HtmlDataItem("汽车衡_采样启用状态", "false", eHtmlDataItemType.svg_visible));
            }
            else
            {
                datas.Add(new HtmlDataItem("QGCY", "1", eHtmlDataItemType.btn_visible));
                datas.Add(new HtmlDataItem("TZCY", "1", eHtmlDataItemType.btn_visible));
                datas.Add(new HtmlDataItem("汽车衡_采样启用状态", "true", eHtmlDataItemType.svg_visible));
            }

            string carnumber = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString());
            if (!string.IsNullOrEmpty(carnumber))
            {
                CmcsBuyFuelTransport transport = commonDAO.SelfDber.Entity<CmcsBuyFuelTransport>(" where CarNumber=:CarNumber order by creationtime desc", new { CarNumber = carnumber });
                if (transport != null)
                {
                    datas.Add(new HtmlDataItem("汽车衡_毛重", transport.GrossWeight.ToString() + " 吨", eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("汽车衡_皮重", transport.TareWeight.ToString() + " 吨", eHtmlDataItemType.svg_text));
                }
            }

            //播放语音
            string speakerValue = commonDAO.GetSignalDataValue(machineCode, "语音播放信息");
            if (!string.IsNullOrWhiteSpace(speakerValue))
                voiceSpeaker.Speak(speakerValue, 1, false);

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

        #region 运输记录

        private void btnRefreshBuyTransport_Click(object sender, EventArgs e)
        {
            // 入厂煤
            LoadTodayUnFinishBuyFuelTransport();
            LoadTodayFinishBuyFuelTransport();
        }

        /// <summary>
        /// 获取未完成的入厂煤记录
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetUnFinishBuyFuelTransport(this.CurrentMachineCode.Replace("汽车智能化-", ""));
        }

        /// <summary>
        /// 获取指定日期已完成的入厂煤记录
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), this.CurrentMachineCode.Replace("汽车智能化-", ""));
        }

        #endregion

    }
}