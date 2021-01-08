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
using CMCS.Common.Enums;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using CMCS.Monitor.Win.Utilities;
using DevComponents.DotNetBar.Metro;
using Xilium.CefGlue.WindowsForms;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmAutoCupboardPneumaticTransfer : MetroForm
    {
        private string green = "#00C000", red = "#FF0000", white = "#FFFFFF", gray = "#D0D2D3";
        public string[] strSignal = new string[] { "6mm破碎", "6mm缩分", "3mm破碎", "3mm缩分", "0.2mm破碎", "红外干燥", "6mm全水分样瓶", "3mm存查样样瓶", "0.2mm分析样样瓶", "0.2mm备查样样瓶", "总经理备查样瓶一", "总经理备查样瓶二", "总经理备查样瓶三", "弃料输送机", "上盖一体机自动" };
        public string[] strWaySignal = new string[] { "路劲探测器36", "路劲探测器37", "路劲探测器38", "路劲探测器39", "路劲探测器3A", "路劲探测器3B", "路劲探测器3C", "路劲探测器3D", "路劲探测器3E" };

        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmAutoCupboardPneumaticTransfer";

        CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();
        MonitorCommon monitorCommon = MonitorCommon.GetInstance();

        public FrmAutoCupboardPneumaticTransfer()
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
            cefWebBrowser.StartUrl = SelfVars.Url_AutoCupboardPneumaticTransfer;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmAutoCupboardPneumaticTransfer_Load(object sender, EventArgs e)
        {
            //monitorCommon.SetFromSize(this);

            FormInit();
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        void RequestData()
        {
            CommonDAO commonDAO = CommonDAO.GetInstance();

            string value = string.Empty, machineCode = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();
            List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();

            #region 智能存样柜 #

            datas.Clear();

            GetBoxDatas(commonDAO, GlobalVars.MachineCode_CYG1, ref datas);


            //GetQdDatas(commonDAO, GlobalVars.MachineCode_QD, ref datas);

            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

            //if (SelfVars.DataBz == 1)
            //{
            //    //传输信息
            //    List<InfCYGRecord> listMakerRecord = Dbers.GetInstance().SelfDber.TopEntities<InfCYGRecord>(6, " where SampleId is not null order by OpTime desc");
            //    cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadSampleInfo(" + Newtonsoft.Json.JsonConvert.SerializeObject(listMakerRecord.Select(a => new { UpdateTime = a.OpTime.Value.Year < 2000 ? "" : a.OpTime.Value.ToString("yyyy-MM-dd HH:mm"), Code = a.SampleId, SamType = a.SampleType, Status = a.IsSuccessed == 1 ? "存取成功" : "存取失败" })) + ");", "", 0);
            //}
            //else
            //{
            //    //异常信息
            //    List<InfEquInfHitch> infHitches = Dbers.GetInstance().SelfDber.TopEntities<InfEquInfHitch>(6, " where InterfaceType is not null order by HitchTime desc");
            //    cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadHitchs(" + Newtonsoft.Json.JsonConvert.SerializeObject(infHitches.Select(a => new { UpdateTime = a.HitchTime.Year < 2000 ? "" : a.HitchTime.ToString("yyyy-MM-dd HH:mm"), Code = a.HitchDescribe, SamType = a.InterfaceType, Status = a.MachineCode })) + ");", "", 0);
            //}
            #endregion
        }

        String nullif(String st)
        {
            if (String.IsNullOrEmpty(st))
                return null;
            else
                return st;
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
        private void btnRefreshPage_Click(object sender, EventArgs e)
        {
            cefWebBrowser.Browser.Reload();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshData_Click(object sender, EventArgs e)
        {
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
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("test();", "", 0);
        }


        private static void GetBoxDatas(CommonDAO commonDAO, string machineCode, ref List<HtmlDataItem> datas)
        {
            string value = "";
            datas.Add(new HtmlDataItem("存查样柜1_共有仓位", commonDAO.GetSignalDataValue(machineCode, "共有仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("存查样柜1_已存仓位", commonDAO.GetSignalDataValue(machineCode, "已存仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("存查样柜1_未存仓位", commonDAO.GetSignalDataValue(machineCode, "未存仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("存查样柜1_存样率", commonDAO.GetSignalDataValue(machineCode, "存样率"), eHtmlDataItemType.svg_text));


            datas.Add(new HtmlDataItem("存查样柜1_大瓶共有", commonDAO.GetSignalDataValue(machineCode, "大瓶仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("存查样柜1_大瓶已存", commonDAO.GetSignalDataValue(machineCode, "大瓶已存仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("存查样柜1_中瓶共有", commonDAO.GetSignalDataValue(machineCode, "中瓶仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("存查样柜1_中瓶已存", commonDAO.GetSignalDataValue(machineCode, "中瓶已存仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("存查样柜1_小瓶共有", commonDAO.GetSignalDataValue(machineCode, "小瓶仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("存查样柜1_小瓶已存", commonDAO.GetSignalDataValue(machineCode, "小瓶已存仓位"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("存查样柜1_大瓶共有", "#00c000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存查样柜1_大瓶已存", "#1b75bb", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存查样柜1_中瓶共有", "#00c000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存查样柜1_中瓶已存", "#1b75bb", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存查样柜1_小瓶共有", "#00c000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存查样柜1_小瓶已存", "#1b75bb", eHtmlDataItemType.svg_color));

            //datas.Add(new HtmlDataItem(machineCode + "_B已存", "#00c000", eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem(machineCode + "_B未存", "#1b75bb", eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem(machineCode + "_B过期", "#ff0000", eHtmlDataItemType.svg_color));



            //value = commonDAO.GetSignalDataValue(machineCode, "设备状态");
            //if ("|就绪待机|自动|正常|".Contains("|" + value + "|"))
            //    datas.Add(new HtmlDataItem(machineCode + "_系统", "#00c000", eHtmlDataItemType.svg_color));
            //else if ("|正在运行|正在卸样|手动|正在传输|".Contains("|" + value + "|"))
            //    datas.Add(new HtmlDataItem(machineCode + "_系统", "#ff0000", eHtmlDataItemType.svg_color));
            //else if ("|发生故障|错误|".Contains("|" + value + "|"))
            //    datas.Add(new HtmlDataItem(machineCode + "_系统", "#ffff00", eHtmlDataItemType.svg_color));
            //else if ("|离线|".Contains("|" + value + "|"))
            //    datas.Add(new HtmlDataItem(machineCode + "_系统", "#c0c0c0", eHtmlDataItemType.svg_color));
            //else
            //    datas.Add(new HtmlDataItem(machineCode + "_系统", "#c0c0c0", eHtmlDataItemType.svg_color));
        }


        private static void GetQdDatas(CommonDAO commonDAO, string machineCode, ref List<HtmlDataItem> datas)
        {
            //string value = "";
            //value = commonDAO.GetSignalDataValue(machineCode, "系统");
            //if ("|就绪待机|自动|正常|".Contains("|" + value + "|"))
            //    datas.Add(new HtmlDataItem("气动传输_系统", "#00c000", eHtmlDataItemType.svg_color));
            //else if ("|正在运行|正在卸样|手动|正在传输|".Contains("|" + value + "|"))
            //    datas.Add(new HtmlDataItem("气动传输_系统", "#ff0000", eHtmlDataItemType.svg_color));
            //else if ("|发生故障|错误|".Contains("|" + value + "|"))
            //    datas.Add(new HtmlDataItem("气动传输_系统", "#ffff00", eHtmlDataItemType.svg_color));
            //else if ("|离线|".Contains("|" + value + "|"))
            //    datas.Add(new HtmlDataItem("气动传输_系统", "#c0c0c0", eHtmlDataItemType.svg_color));
            //else
            //    datas.Add(new HtmlDataItem("气动传输_系统", "#c0c0c0", eHtmlDataItemType.svg_color));

            //string keys = "";

            //value = commonDAO.GetSignalDataValue(machineCode, "动力风机");
            //if (value == "144")
            //{

            //    datas.Add(new HtmlDataItem("风机", "00c000", eHtmlDataItemType.svg_color));

            //    datas.Add(new HtmlDataItem("气动传输_风机", "风机正转", eHtmlDataItemType.svg_scroll3));
            //}
            //else if(value == "160")
            //{
            //    datas.Add(new HtmlDataItem("气动传输_风机", "风机反转", eHtmlDataItemType.svg_scroll3));
            //}

            //value = commonDAO.GetSignalDataValue(machineCode, "四向转换器1");
            //if (value == "16"|| value == "17")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器1", "1", eHtmlDataItemType.svg_scroll));
            //}
            //else if (value == "32" || value == "33")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器1", "2", eHtmlDataItemType.svg_scroll));
            //}
            //else if (value == "64" || value == "65")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器1", "3", eHtmlDataItemType.svg_scroll));
            //}
            //else if (value == "128" || value == "129")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器1", "4", eHtmlDataItemType.svg_scroll));
            //}

            //value = commonDAO.GetSignalDataValue(machineCode, "四向转换器2");
            //if (value == "16" || value == "17")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器2", "1", eHtmlDataItemType.svg_scroll));
            //}
            //else if (value == "32" || value == "33")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器2", "2", eHtmlDataItemType.svg_scroll));
            //}
            //else if (value == "64" || value == "65")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器2", "3", eHtmlDataItemType.svg_scroll));
            //}
            //else if (value == "128" || value == "129")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器2", "4", eHtmlDataItemType.svg_scroll));
            //}

            //value = commonDAO.GetSignalDataValue(machineCode, "四向转换器3");
            //if (value == "16" || value == "17")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器3", "1", eHtmlDataItemType.svg_scroll));
            //}
            //else if (value == "32" || value == "33")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器3", "2", eHtmlDataItemType.svg_scroll));
            //}
            //else if (value == "64" || value == "65")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器3", "3", eHtmlDataItemType.svg_scroll));
            //}
            //else if (value == "128" || value == "129")
            //{
            //    datas.Add(new HtmlDataItem("四向转换器3", "4", eHtmlDataItemType.svg_scroll));
            //}
        }




    }
}
