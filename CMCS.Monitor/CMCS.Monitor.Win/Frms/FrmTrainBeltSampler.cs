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
using CMCS.Common.Enums;
using CMCS.Monitor.DAO;
using CMCS.Monitor.Win.CefGlue;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using Xilium.CefGlue;
//
using Xilium.CefGlue.WindowsForms;
using CMCS.Common.Entities.Inf;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmTrainBeltSampler : MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmTrainBeltSampler";

        CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

        Dictionary<string, string> SampleCodes = new Dictionary<string, string>();
        bool tempBool = true;

        public FrmTrainBeltSampler()
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

            cefWebBrowser.StartUrl = SelfVars.Url_BeltSampler;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;
            timer2.Enabled = true;
            //页面初始化完成
            //ReadConfig();
        }

        private void FrmTrainBeltSampler_Load(object sender, EventArgs e)
        {
            FormInit();
        }

        private void superGridControl_CancelEdit_BeginEdit(object sender, GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        /// <summary>
        /// 打开卸样界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenUnload_Click(object sender, EventArgs e)
        {

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

            //ReadConfig();
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        void RequestData()
        {
            CommonDAO commonDAO = CommonDAO.GetInstance();

            string value = string.Empty, machineCode = string.Empty, equInfSamplerSystemStatus = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();

            #region 皮带采样机 #1

            datas.Clear();
            machineCode = GlobalVars.MachineCode_PDCYJ_1;
            //equInfSamplerSystemStatus = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString());
            //datas.Add(new HtmlDataItem("#1皮采_采样编码", ConvertSignalValue(!SysStatus(equInfSamplerSystemStatus) ? "" : commonDAO.GetSignalDataValue(machineCode, "采样编码")), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("#1皮采_矿发量", ConvertSignalValue(!SysStatus(equInfSamplerSystemStatus) ? "" : commonDAO.GetSignalDataValue(machineCode, "矿发量")), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("#1皮采_开始时间", ConvertSignalValue(!SysStatus(equInfSamplerSystemStatus) ? "" : commonDAO.GetSignalDataValue(machineCode, "截取开始时间")), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("#1皮采_来煤车数", ConvertSignalValue(!SysStatus(equInfSamplerSystemStatus) ? "" : commonDAO.GetSignalDataValue(machineCode, "来煤车数")), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("#1皮采_采样次数", ConvertSignalValue(!SysStatus(equInfSamplerSystemStatus) ? "" : commonDAO.GetSignalDataValue(machineCode, "次采次数")), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("#1皮采_系统", ConvertMachineStatusToColor(equInfSamplerSystemStatus), eHtmlDataItemType.svg_color));

            //// 集样罐   
            //List<InfEquInfSampleBarrel> barrels1 = MonitorDAO.GetInstance().GetEquInfSampleBarrels(machineCode);
            //datas.Add(new HtmlDataItem("#1皮采_集样罐", Newtonsoft.Json.JsonConvert.SerializeObject(barrels1.Select(a => new { SampleCode = a.SampleCode, BarrelNumber = a.BarrelNumber, IsCurrent = a.IsCurrent, BarrelStatus = a.BarrelStatus, SampleCount = a.SampleCount })), eHtmlDataItemType.json_data));

          
            datas.Add(new HtmlDataItem("初级给料皮带正转a", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带正转") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("初级给料皮带反转a", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带反转") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("初级给料皮带清扫a", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带清扫") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("破碎机a", commonDAO.GetSignalDataValue(machineCode, "破碎机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("破碎清扫电机a", commonDAO.GetSignalDataValue(machineCode, "破碎清扫电机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("次级给料皮带a", commonDAO.GetSignalDataValue(machineCode, "次级给料皮带") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("次级给料皮带清扫a", commonDAO.GetSignalDataValue(machineCode, "次级给料皮带清扫") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("煤流信号a", commonDAO.GetSignalDataValue(machineCode, "煤流信号") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("缩分器a", commonDAO.GetSignalDataValue(machineCode, "缩分器") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("斗提机a", commonDAO.GetSignalDataValue(machineCode, "斗提机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("返料输送机a", commonDAO.GetSignalDataValue(machineCode, "返料输送机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("封装机运行状态a", commonDAO.GetSignalDataValue(machineCode, "封装机运行状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机准备状态a", commonDAO.GetSignalDataValue(machineCode, "封装机准备状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机连接状态a", commonDAO.GetSignalDataValue(machineCode, "封装机连接状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机报警状态a", commonDAO.GetSignalDataValue(machineCode, "封装机报警状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("采样机PLC连接状态a", commonDAO.GetSignalDataValue(machineCode, "采样机PLC连接状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机无报警状态a", commonDAO.GetSignalDataValue(machineCode, "采样机无报警状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("远程自动状态a", commonDAO.GetSignalDataValue(machineCode, "远程自动状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("远程手动状态a", commonDAO.GetSignalDataValue(machineCode, "远程手动状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("就地自动状态a", commonDAO.GetSignalDataValue(machineCode, "就地自动状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("就地手动状态a", commonDAO.GetSignalDataValue(machineCode, "就地手动状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("下次采样时间a", commonDAO.GetSignalDataValue(machineCode, "下次采样时间"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带称a", commonDAO.GetSignalDataValue(machineCode, "皮带称"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("当前样重a", commonDAO.GetSignalDataValue(machineCode, "当前样重"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样次数a", commonDAO.GetSignalDataValue(machineCode, "采样次数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("当前采样码a", commonDAO.GetSignalDataValue(machineCode, "当前采样码"), eHtmlDataItemType.svg_text));
           
            datas.Add(new HtmlDataItem("采样机编码a", commonDAO.GetSignalDataValue(machineCode, "采样机编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("翻车机编码a", commonDAO.GetSignalDataValue(machineCode, "翻车机编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("翻车机车数a", commonDAO.GetSignalDataValue(machineCode, "翻车机车数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("已翻车数a", commonDAO.GetSignalDataValue(machineCode, "已翻车数"), eHtmlDataItemType.svg_text));

            // 添加更多...

            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

            #endregion

            #region 皮带采样机 #2

            datas.Clear();
            machineCode = GlobalVars.MachineCode_PDCYJ_2;
            //equInfSamplerSystemStatus = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString());
            //datas.Add(new HtmlDataItem("#2皮采_采样编码", ConvertSignalValue(!SysStatus(equInfSamplerSystemStatus) ? "" : commonDAO.GetSignalDataValue(machineCode, "采样编码")), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("#2皮采_矿发量", ConvertSignalValue(!SysStatus(equInfSamplerSystemStatus) ? "" : commonDAO.GetSignalDataValue(machineCode, "矿发量")), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("#2皮采_开始时间", ConvertSignalValue(!SysStatus(equInfSamplerSystemStatus) ? "" : commonDAO.GetSignalDataValue(machineCode, "截取开始时间")), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("#2皮采_来煤车数", ConvertSignalValue(!SysStatus(equInfSamplerSystemStatus) ? "" : commonDAO.GetSignalDataValue(machineCode, "来煤车数")), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("#2皮采_采样次数", ConvertSignalValue(!SysStatus(equInfSamplerSystemStatus) ? "" : commonDAO.GetSignalDataValue(machineCode, "次采次数")), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("#2皮采_系统", ConvertMachineStatusToColor(equInfSamplerSystemStatus), eHtmlDataItemType.svg_color));

            //// 集样罐   
            //List<InfEquInfSampleBarrel> barrels2 = MonitorDAO.GetInstance().GetEquInfSampleBarrels(machineCode);
            //datas.Add(new HtmlDataItem("#2皮采_集样罐", Newtonsoft.Json.JsonConvert.SerializeObject(barrels2.Select(a => new { SampleCode = a.SampleCode, BarrelNumber = a.BarrelNumber, IsCurrent = a.IsCurrent, BarrelStatus = a.BarrelStatus, SampleCount = a.SampleCount })), eHtmlDataItemType.json_data));

            datas.Add(new HtmlDataItem("初级给料皮带正转b", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带正转") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("初级给料皮带反转b", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带反转") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("初级给料皮带清扫b", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带清扫") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("破碎机b", commonDAO.GetSignalDataValue(machineCode, "破碎机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("破碎清扫电机b", commonDAO.GetSignalDataValue(machineCode, "破碎清扫电机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("次级给料皮带b", commonDAO.GetSignalDataValue(machineCode, "次级给料皮带") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("次级给料皮带清扫b", commonDAO.GetSignalDataValue(machineCode, "次级给料皮带清扫") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("煤流信号b", commonDAO.GetSignalDataValue(machineCode, "煤流信号") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("缩分器b", commonDAO.GetSignalDataValue(machineCode, "缩分器") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("斗提机b", commonDAO.GetSignalDataValue(machineCode, "斗提机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("返料输送机b", commonDAO.GetSignalDataValue(machineCode, "返料输送机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("封装机运行状态b", commonDAO.GetSignalDataValue(machineCode, "封装机运行状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机准备状态b", commonDAO.GetSignalDataValue(machineCode, "封装机准备状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机连接状态b", commonDAO.GetSignalDataValue(machineCode, "封装机连接状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机报警状态b", commonDAO.GetSignalDataValue(machineCode, "封装机报警状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("采样机PLC连接状态b", commonDAO.GetSignalDataValue(machineCode, "采样机PLC连接状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机无报警状态b", commonDAO.GetSignalDataValue(machineCode, "采样机无报警状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("远程自动状态b", commonDAO.GetSignalDataValue(machineCode, "远程自动状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("远程手动状态b", commonDAO.GetSignalDataValue(machineCode, "远程手动状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("就地自动状态b", commonDAO.GetSignalDataValue(machineCode, "就地自动状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("就地手动状态b", commonDAO.GetSignalDataValue(machineCode, "就地手动状态") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("下次采样时间b", commonDAO.GetSignalDataValue(machineCode, "下次采样时间"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带称b", commonDAO.GetSignalDataValue(machineCode, "皮带称"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("当前样重b", commonDAO.GetSignalDataValue(machineCode, "当前样重"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样次数b", commonDAO.GetSignalDataValue(machineCode, "采样次数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("当前采样码b", commonDAO.GetSignalDataValue(machineCode, "当前采样码"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("采样机编码b", commonDAO.GetSignalDataValue(machineCode, "采样机编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("翻车机编码b", commonDAO.GetSignalDataValue(machineCode, "翻车机编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("翻车机车数b", commonDAO.GetSignalDataValue(machineCode, "翻车机车数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("已翻车数b", commonDAO.GetSignalDataValue(machineCode, "已翻车数"), eHtmlDataItemType.svg_text));


            // 添加更多...

            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

            tempBool = tempBool ? false : true;

            #endregion
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
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("testColor();", "", 0);
        }

        /// <summary>
        /// 转换信号点值
        /// </summary>
        /// <param name="signalValue">信号点值</param>
        /// <param name="unit">单位</param>
        /// <returns></returns>
        private string ConvertSignalValue(string signalValue, string unit = "")
        {
            return !string.IsNullOrEmpty(signalValue) ? signalValue + unit : "--";
        }

        private bool SysStatus(string systemStatus)
        {
            if ("|就绪待机|离线|".Contains("|" + systemStatus + "|"))
                return false;
            else if ("|正在运行|正在卸样|".Contains("|" + systemStatus + "|"))
                return true;
            else if ("|发生故障|".Contains("|" + systemStatus + "|"))
                return false;
            else if ("|离线状态|".Contains("|" + systemStatus + "|"))
                return false;
            else
                return false;
        }

        /// <summary>
        /// 转换设备系统状态为颜色值
        /// </summary>
        /// <param name="systemStatus">系统状态</param>
        /// <returns></returns>
        private string ConvertMachineStatusToColor(string systemStatus)
        {
            if ("|就绪待机|离线|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.BeReady);
            else if ("|正在运行|正在卸样|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Working);
            else if ("|发生故障|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);
            else
                return ColorTranslator.ToHtml(EquipmentStatusColors.Forbidden);
        }
    }
}
