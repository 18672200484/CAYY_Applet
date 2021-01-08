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
//using CMCS.Common.Entities.AutoMaker;
//using CMCS.Common.Entities.Inf;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using DevComponents.DotNetBar.Metro;
using Xilium.CefGlue.WindowsForms;
using CMCS.Monitor.Win.UserControls;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Entities.AutoCupboard;
using CMCS.Common.Enums;
//using CMCS.Common.Entities.AutoCupboard;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmSampleCabinet : MetroForm
    {
        private string green = "#00C000", red = "#FF0000", white = "#FFFFFF", gray = "#D0D2D3";
        public string[] strSignal = new string[] { "6mm破碎", "6mm缩分", "3mm破碎", "3mm缩分", "0.2mm破碎", "红外干燥", "6mm全水分样瓶", "3mm存查样样瓶", "0.2mm分析样样瓶", "0.2mm备查样样瓶", "总经理备查样瓶一", "总经理备查样瓶二", "总经理备查样瓶三", "弃料输送机", "上盖一体机自动" };
        public string[] strWaySignal = new string[] { "路劲探测器36", "路劲探测器37", "路劲探测器38", "路劲探测器39", "路劲探测器3A", "路劲探测器3B", "路劲探测器3C", "路劲探测器3D", "路劲探测器3E" };

        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmSampleCabinet";

        CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

        public FrmSampleCabinet()
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
            cefWebBrowser.StartUrl = SelfVars.Url_SampleCabinet;
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

            //#region 智能存样柜 #

            //datas.Clear();
            //machineCode = GlobalVars.MachineCode_CYG1;
            //datas.Add(new HtmlDataItem("总仓位", commonDAO.GetSignalDataValue(machineCode, "全柜全部"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("未存仓位", commonDAO.GetSignalDataValue(machineCode, "全柜无样"), eHtmlDataItemType.svg_text));
            //try
            //{
            //    int allhas = Convert.ToInt32(commonDAO.GetSignalDataValue(machineCode, "全柜全部")) - Convert.ToInt32(commonDAO.GetSignalDataValue(machineCode, "全柜无样"));
            //    datas.Add(new HtmlDataItem("已存仓位", allhas.ToString(), eHtmlDataItemType.svg_text));
            //}
            //catch (Exception)
            //{
            //}
            //datas.Add(new HtmlDataItem("存样率", commonDAO.GetSignalDataValue(machineCode, "全柜有样百分比") + "%", eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("6mm全水样", commonDAO.GetSignalDataValue(machineCode, "全柜6mm样瓶"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("6mm总经理备查样", commonDAO.GetSignalDataValue(machineCode, "全柜总经理备查样瓶"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("3mm备查样", commonDAO.GetSignalDataValue(machineCode, "全柜3mm样瓶"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("02mm备查样", commonDAO.GetSignalDataValue(machineCode, "全柜0.2mm样瓶"), eHtmlDataItemType.svg_text));
            ////datas.Add(new HtmlDataItem("样品扭转", commonDAO.GetSignalDataValue(machineCode, "样品扭转"), eHtmlDataItemType.svg_custom));
            //datas.Add(new HtmlDataItem("全自动存样柜_状态", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString()), eHtmlDataItemType.svg_color));

            //machineCode = GlobalVars.MachineCode_QD;
            //datas.Add(new HtmlDataItem("气动传输_状态", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString()), eHtmlDataItemType.svg_color));

            ////气动传输路径信号
            //for (int i = 0; i < strWaySignal.Length; i++)
            //{
            //    datas.Add(new HtmlDataItem(strWaySignal[i], commonDAO.GetSignalDataValue(machineCode, strWaySignal[i]) == "1" ? red : gray, eHtmlDataItemType.svg_blinkcolor));
            //}

            //// 发送到页面
            //cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
            ////

            List<InfCYGSam> lists = Dbers.GetInstance().SelfDber.Entities<InfCYGSam>().ToList();
            List<Tempsam> tempsams = new List<Tempsam>();
           foreach(var item in lists)
            {
                Tempsam tempsam = new Tempsam();
                tempsam.Name = item.AreaNumber + "-" + item.ColumnIndex + "-" + item.CellIndex;
                tempsam.Type = item.SamType;
                tempsam.Code = item.Code;

                tempsams.Add(tempsam);
            }


            //拼Html
            //string html = "";
            //List<InfCYGSam> list = Dbers.GetInstance().SelfDber.Entities<InfCYGSam>().ToList();

            //html += "<div><div>";
            //var list1 = list.Where(a => a.AreaNumber == 2).OrderByDescending(a=>a.ColumnIndex);
            //foreach(var item in list1.GroupBy(a => a.ColumnIndex))
            //{
            //    var item1 = item.OrderByDescending(a=>a.CellIndex).ToList();
            //    foreach (var it in item1)
            //    {
            //        if (!string.IsNullOrEmpty(it.Code))
            //        {
            //            html += "<div class=\'guad_wc\' onclick=\'showBoxDetail(" + it.Code + ",this)\'>A0101</div>";
            //        }
            //        else
            //        {
            //            html += "<div class=\'guad_wc\'>A0101</div>";
            //        }
            //    }
            //}
            //html += "</div><div class=\'guad_yg\'>";
            //var list2 = list.Where(a => a.AreaNumber == 1).OrderBy(a => a.ColumnIndex);
            //foreach (var item in list2.GroupBy(a => a.ColumnIndex))
            //{
            //    var item1 = item.OrderByDescending(a => a.CellIndex).ToList();
            //    foreach (var it in item1)
            //    {
            //        if (!string.IsNullOrEmpty(it.Code))
            //        {
            //            html += "<div class=\'guad_wc\' onclick=\'showBoxDetail(" + it.Code + ",this)\'>A0101</div>";
            //        }
            //        else
            //        {
            //            html += "<div class=\'guad_wc\'>A0101</div>";
            //        }
            //    }
            //}
            //html += "</div></div>";


            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadSamInfo(" +Newtonsoft.Json.JsonConvert.SerializeObject(tempsams) + ");", "", 0);

       
        }


        /// <summary>
        /// 第三方样品类型 转换成 集中管控样品类型
        /// </summary>
        /// <param name="ypType">第三方样品类型</param>
        /// <returns></returns>
        public string ConvertToCmcsYpType(string ypType)
        {
            if (ypType == "1")
                return "6mm全水样";
            if (ypType == "2")
                return "3mm备查样";
            if (ypType == "3")
                return "0.2mm分析样";
            if (ypType == "4")
                return "0.2mm备查样";
            if (ypType == "5")
                return "6mm总经理备查样1";
            if (ypType == "6")
                return "6mm总经理备查样2";
            if (ypType == "7")
                return "6mm总经理备查样3";
            else
                return "未知";
        }

        [Serializable()]
        public class Tempsam
        {
            public string Name { get; set; }
            public string Type { get; set; }

            public string Code { get; set; }
        }


        [Serializable()]
        public class TempGS
        {
            public string Type { get; set; }
            public int TmDay { get; set; }
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

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRequestData_Click(object sender, EventArgs e)
        {
            RequestData();
        }

    }
}
