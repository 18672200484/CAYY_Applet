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
//using CMCS.Common.Entities;
//using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.Inf;
using CMCS.Monitor.DAO;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using DevComponents.DotNetBar.Metro;
using Xilium.CefGlue.WindowsForms;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmAssayManage : MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmAssayManage";

        CefWebBrowser cefWebBrowser = new CefWebBrowser();

        public FrmAssayManage()
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
            cefWebBrowser.StartUrl = SelfVars.Url_AssayManage;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmAssayManage_Load(object sender, EventArgs e)
        {
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

            #region 化验室网络管理
            datas.Clear();
            machineCode = GlobalVars.MachineCode_AssayManage;

            datas.Add(new HtmlDataItem("化验完成_Line", commonDAO.GetSignalDataValue(machineCode, "化验完成个数"), eHtmlDataItemType.svg_width));
            datas.Add(new HtmlDataItem("化验完成_Count", commonDAO.GetSignalDataValue(machineCode, "化验完成个数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("化验中_Line", commonDAO.GetSignalDataValue(machineCode, "化验中个数"), eHtmlDataItemType.svg_width));
            datas.Add(new HtmlDataItem("化验中_Count", commonDAO.GetSignalDataValue(machineCode, "化验中个数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("待化验数_Line", commonDAO.GetSignalDataValue(machineCode, "待化验数"), eHtmlDataItemType.svg_width));
            datas.Add(new HtmlDataItem("待化验数_Count", commonDAO.GetSignalDataValue(machineCode, "待化验数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("已审核样品个数", commonDAO.GetSignalDataValue(machineCode, "已审核样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("未审核样品个数", commonDAO.GetSignalDataValue(machineCode, "未审核样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("量热仪1_运行状态", commonDAO.GetSignalDataValue(machineCode, "量热仪1_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("量热仪2_运行状态", commonDAO.GetSignalDataValue(machineCode, "量热仪2_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("量热仪3_运行状态", commonDAO.GetSignalDataValue(machineCode, "量热仪3_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("量热仪4_运行状态", commonDAO.GetSignalDataValue(machineCode, "量热仪4_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("量热仪1_样品个数", commonDAO.GetSignalDataValue(machineCode, "量热仪1_样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("量热仪2_样品个数", commonDAO.GetSignalDataValue(machineCode, "量热仪2_样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("量热仪3_样品个数", commonDAO.GetSignalDataValue(machineCode, "量热仪3_样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("量热仪4_样品个数", commonDAO.GetSignalDataValue(machineCode, "量热仪4_样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("水分仪1_运行状态", commonDAO.GetSignalDataValue(machineCode, "水分仪1_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("水分仪2_运行状态", commonDAO.GetSignalDataValue(machineCode, "水分仪2_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("水分仪1_样品个数", commonDAO.GetSignalDataValue(machineCode, "水分仪1_样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("水分仪2_样品个数", commonDAO.GetSignalDataValue(machineCode, "水分仪2_样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("测硫仪1_运行状态", commonDAO.GetSignalDataValue(machineCode, "测硫仪1_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("测硫仪3_运行状态", commonDAO.GetSignalDataValue(machineCode, "测硫仪3_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("测硫仪1_样品个数", commonDAO.GetSignalDataValue(machineCode, "测硫仪1_样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("测硫仪3_样品个数", commonDAO.GetSignalDataValue(machineCode, "测硫仪3_样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("工分仪1_运行状态", commonDAO.GetSignalDataValue(machineCode, "工分仪1_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("工分仪2_运行状态", commonDAO.GetSignalDataValue(machineCode, "工分仪2_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("工分仪3_运行状态", commonDAO.GetSignalDataValue(machineCode, "工分仪3_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("工分仪4_运行状态", commonDAO.GetSignalDataValue(machineCode, "工分仪4_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("工分仪1_样品个数", commonDAO.GetSignalDataValue(machineCode, "工分仪1_样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("工分仪2_样品个数", commonDAO.GetSignalDataValue(machineCode, "工分仪2_样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("工分仪3_样品个数", commonDAO.GetSignalDataValue(machineCode, "工分仪3_样品个数") + "个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("工分仪4_样品个数", commonDAO.GetSignalDataValue(machineCode, "工分仪4_样品个数") + "个", eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("碳氢仪1_运行状态", commonDAO.GetSignalDataValue(machineCode, "碳氢仪1_运行状态") == "1" ? "Red" : "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("碳氢仪1_样品个数", commonDAO.GetSignalDataValue(machineCode, "碳氢仪1_样品个数") + "个", eHtmlDataItemType.svg_text));

            //datas.Add(new HtmlDataItem("水分室温度", commonDAO.GetSignalDataValue(machineCode, "水分室温度"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("水分室湿度", commonDAO.GetSignalDataValue(machineCode, "水分室湿度"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("工分室温度", commonDAO.GetSignalDataValue(machineCode, "工分室温度"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("工分室湿度", commonDAO.GetSignalDataValue(machineCode, "工分室湿度"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("测硫室温度", commonDAO.GetSignalDataValue(machineCode, "测硫室温度"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("测硫室湿度", commonDAO.GetSignalDataValue(machineCode, "测硫室湿度"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("量热室温度", commonDAO.GetSignalDataValue(machineCode, "量热室温度"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("量热室湿度", commonDAO.GetSignalDataValue(machineCode, "量热室湿度"), eHtmlDataItemType.svg_text));
            #endregion

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
        }
    }
}
