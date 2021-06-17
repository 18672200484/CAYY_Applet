using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Monitor.DAO;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using CMCS.Monitor.Win.Utilities;
using DevComponents.DotNetBar.Metro;
using Xilium.CefGlue;
using Xilium.CefGlue.WindowsForms;
using CMCS.Common.Utilities;
using DevComponents.DotNetBar;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmCarSampler : MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmCarSampler";

        CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

        CommonDAO commonDAO = CommonDAO.GetInstance();
        MonitorCommon monitorCommon = MonitorCommon.GetInstance();
        string LastCarNumber = string.Empty;

        string currentMachineCode = GlobalVars.MachineCode_QCJXCYJ_1;
        /// <summary>
        /// 当前选中的采样机
        /// </summary>
        public string CurrentMachineCode
        {
            get { return currentMachineCode; }
            set { currentMachineCode = value; }
        }

        public FrmCarSampler()
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
            cefWebBrowser.StartUrl = SelfVars.Url_CarSampler;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmCarSampler_Load(object sender, EventArgs e)
        {
            FormInit();
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        void RequestData()
        {
            string value = string.Empty, machineCode = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();
            List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();

            #region 汽车机械采样机

            datas.Clear();
            machineCode = this.CurrentMachineCode;

            datas.Add(new HtmlDataItem("机械采样机_当前采样机", machineCode, eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("机械采样机_当前采样机", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("机械采样机_采样编码", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.采样编码.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("机械采样机_矿发量", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.矿发量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("机械采样机_采样时间", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.采样时间.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("机械采样机_车牌号", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("机械采样机_采样点数", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.采样点数.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("机械采样机_供应商名称", commonDAO.GetSignalDataValue(machineCode, "供应商名称"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("机械采样机_X坐标", commonDAO.GetSignalDataValue(machineCode, "当前X坐标"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("机械采样机_Y坐标", commonDAO.GetSignalDataValue(machineCode, "当前Y坐标"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("机械采样机_Z坐标", commonDAO.GetSignalDataValue(machineCode, "当前Z坐标"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("机械采样机_小车1", commonDAO.GetSignalDataValue(machineCode, "小车运行") == "1" ? "Red" : "url(#rect1770_1_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_小车2", commonDAO.GetSignalDataValue(machineCode, "小车运行") == "1" ? "Red" : "url(#rect1752_1_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_小车3", commonDAO.GetSignalDataValue(machineCode, "小车运行") == "1" ? "Red" : "url(#rect1761_1_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_小车4", commonDAO.GetSignalDataValue(machineCode, "小车运行") == "1" ? "Red" : "url(#rect1716_1_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_小车5", commonDAO.GetSignalDataValue(machineCode, "小车运行") == "1" ? "Red" : "url(#rect1725_1_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_小车6", commonDAO.GetSignalDataValue(machineCode, "小车运行") == "1" ? "Red" : "url(#rect1734_1_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_小车7", commonDAO.GetSignalDataValue(machineCode, "小车运行") == "1" ? "Red" : "url(#polygon1743_1_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_接料斗1", commonDAO.GetSignalDataValue(machineCode, "样料运行") == "1" ? "Red" : "url(#_164344952_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_接料斗2", commonDAO.GetSignalDataValue(machineCode, "样料运行") == "1" ? "Red" : "url(#_130855712_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_接料斗3", commonDAO.GetSignalDataValue(machineCode, "样料运行") == "1" ? "Red" : "url(#_164355560_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_接料斗4", commonDAO.GetSignalDataValue(machineCode, "样料运行") == "1" ? "Red" : "url(#_164351936_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_除铁给料皮带", commonDAO.GetSignalDataValue(machineCode, "给料皮带运行") == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_溜煤管", commonDAO.GetSignalDataValue(machineCode, "匀料器运行") == "1" ? "Red" : "url(#polygon984_1_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_锤式破碎机1", commonDAO.GetSignalDataValue(machineCode, "环锤运行") == "1" ? "Red" : "url(#_125277864-0_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_锤式破碎机2", commonDAO.GetSignalDataValue(machineCode, "环锤运行") == "1" ? "Red" : "url(#_164348960_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_锤式破碎机3", commonDAO.GetSignalDataValue(machineCode, "环锤运行") == "1" ? "Red" : "url(#_130854176_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_锤式破碎机4", commonDAO.GetSignalDataValue(machineCode, "环锤运行") == "1" ? "Red" : "url(#_130859336-4_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_锤式破碎机5", commonDAO.GetSignalDataValue(machineCode, "环锤运行") == "1" ? "Red" : "url(#_164347592_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_锤式破碎机6", commonDAO.GetSignalDataValue(machineCode, "环锤运行") == "1" ? "Red" : "url(#_164343680_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_锤式破碎机7", commonDAO.GetSignalDataValue(machineCode, "环锤运行") == "1" ? "Red" : "url(#_164356088_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_缩分皮带", commonDAO.GetSignalDataValue(machineCode, "缩分运行") == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("机械采样机_弃料斗", commonDAO.GetSignalDataValue(machineCode, "弃料运行") == "1" ? "Red" : "url(#_164344952-8_1_)", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("盛样桶号", commonDAO.GetSignalDataValue(machineCode, "当前桶号"), eHtmlDataItemType.svg_text));
            CmcsAutotruck autotruck1 = CommonDAO.GetInstance().GetAutotruckByCarNumber(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString()));
            if (autotruck1 != null)
            {
                datas.Add(new HtmlDataItem("车厢长", autotruck1.CarriageLength.ToString(), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("车厢高", autotruck1.CarriageHeight.ToString(), eHtmlDataItemType.svg_text)); ;
                datas.Add(new HtmlDataItem("车厢宽", autotruck1.CarriageWidth.ToString(), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("车底高", autotruck1.CarriageBottomToFloor.ToString(), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("拉筋1", autotruck1.LeftObstacle1.ToString(), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("拉筋2", autotruck1.LeftObstacle2.ToString(), eHtmlDataItemType.svg_text)); ;
                datas.Add(new HtmlDataItem("拉筋3", autotruck1.LeftObstacle3.ToString(), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("拉筋4", autotruck1.LeftObstacle4.ToString(), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("拉筋5", autotruck1.LeftObstacle5.ToString(), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("拉筋6", autotruck1.LeftObstacle6.ToString(), eHtmlDataItemType.svg_text));

            }
            else
            {
                datas.Add(new HtmlDataItem("车厢长", "0", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("车厢高", "0", eHtmlDataItemType.svg_text)); ;
                datas.Add(new HtmlDataItem("车厢宽", "0", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("车底高", "0", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("拉筋1", "0", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("拉筋2", "0", eHtmlDataItemType.svg_text)); ;
                datas.Add(new HtmlDataItem("拉筋3", "0", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("拉筋4", "0", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("拉筋5", "0", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("拉筋6", "0", eHtmlDataItemType.svg_text));
            }

            datas.Add(new HtmlDataItem("前边界", commonDAO.GetSignalDataValue(machineCode, "前边界") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("左边界", commonDAO.GetSignalDataValue(machineCode, "左边界") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("上边界", commonDAO.GetSignalDataValue(machineCode, "上边界") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("后边界", commonDAO.GetSignalDataValue(machineCode, "后边界") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("右边界", commonDAO.GetSignalDataValue(machineCode, "右边界") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("下边界", commonDAO.GetSignalDataValue(machineCode, "下边界") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("车尾上", commonDAO.GetSignalDataValue(machineCode, "道闸抬到位") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("车尾下", commonDAO.GetSignalDataValue(machineCode, "道闸落到位") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("大车", commonDAO.GetSignalDataValue(machineCode, "大车故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "大车运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("小车", commonDAO.GetSignalDataValue(machineCode, "小车故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "小车运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("升降", commonDAO.GetSignalDataValue(machineCode, "升降故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "升降运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("旋转", commonDAO.GetSignalDataValue(machineCode, "动力头故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "动力头运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("匀料器", commonDAO.GetSignalDataValue(machineCode, "匀料器故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "匀料器运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("给料", commonDAO.GetSignalDataValue(machineCode, "给料皮带故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "给料皮带运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("环锤", commonDAO.GetSignalDataValue(machineCode, "环锤故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "环锤运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("反击板", commonDAO.GetSignalDataValue(machineCode, "反击板故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "反击板运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("缩分", commonDAO.GetSignalDataValue(machineCode, "缩分故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "缩分运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("样料", commonDAO.GetSignalDataValue(machineCode, "样料故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "样料运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("弃料", commonDAO.GetSignalDataValue(machineCode, "弃料故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "弃料运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("样桶", commonDAO.GetSignalDataValue(machineCode, "桶故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "样煤仓运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("车尾检测", commonDAO.GetSignalDataValue(machineCode, "道闸故障") == "1" ? "#ffff00" : commonDAO.GetSignalDataValue(machineCode, "道闸运行") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("传感器_车尾", commonDAO.GetSignalDataValue(machineCode, "传感器车尾"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("传感器_侧面", commonDAO.GetSignalDataValue(machineCode, "传感器侧面"), eHtmlDataItemType.svg_text));

            string cyfs = "手动";
            string cyys = "#00ff00";
            if (commonDAO.GetSignalDataValue(machineCode, "采样自动") == "1")
            {
                cyfs = "自动";
                cyys = "#00ff00";
            }
            if (commonDAO.GetSignalDataValue(machineCode, "采样急停") == "1")
            {
                cyfs = "急停";
                cyys = "#ff0000";
            }
            datas.Add(new HtmlDataItem("采样方式", cyfs, eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样方式", cyys, eHtmlDataItemType.svg_color));

            string zyfs = "手动";
            string zyys = "#00ff00";
            if (commonDAO.GetSignalDataValue(machineCode, "制样自动") == "1")
            {
                zyfs = "自动";
                zyys = "#00ff00";
            }
            if (commonDAO.GetSignalDataValue(machineCode, "制样急停") == "1")
            {
                zyfs = "急停";
                zyys = "#ff0000";
            }
            datas.Add(new HtmlDataItem("制样方式", zyfs, eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("制样方式", zyys, eHtmlDataItemType.svg_color));

            #region 车厢拉筋信息
            string carNumber = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.当前车号.ToString());
            if (!string.IsNullOrEmpty(carNumber))
            {
                CmcsAutotruck autotruck = CommonDAO.GetInstance().GetAutotruckByCarNumber(carNumber);
                if (autotruck != null)
                {
                    if (PreviewCarCarriage(autotruck))
                        this.pbxAutotruck.Visible = true;
                    else
                        this.pbxAutotruck.Visible = false;
                }
                else
                    this.pbxAutotruck.Visible = false;
            }
            else
                this.pbxAutotruck.Visible = false;

            this.LastCarNumber = carNumber;
            #endregion

            // 集样罐   
            List<InfEquInfSampleBarrel> barrels1 = MonitorDAO.GetInstance().GetEquInfSampleBarrels(machineCode);
            datas.Add(new HtmlDataItem("采样机1_集样罐", Newtonsoft.Json.JsonConvert.SerializeObject(barrels1.Select(a => new { BarrelNumber = a.BarrelNumber, IsCurrent = a.IsCurrent, BarrelStatus = a.BarrelStatus, SampleCount = a.SampleCount })), eHtmlDataItemType.json_data));
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

            //CmcsAutotruck autotruck = CommonDAO.GetInstance().GetAutotruckByCarNumber("鄂ATM168");
            //Bitmap res = new Bitmap(CMCS.Monitor.Win.Properties.Resources.Autotruck);
            //PreviewCarBmp carBmp = new PreviewCarBmp(autotruck);
            //Bitmap bmp = carBmp.GetPreviewBitmap(res, 249, 130);
            //bmp.Save("Autotruck.bmp");
        }

        /// <summary>
        /// 预览车辆拉筋信息图
        /// </summary>
        /// <param name="autotruck"></param>
        private bool PreviewCarCarriage(CmcsAutotruck autotruck)
        {
            if (autotruck != null && autotruck.CarriageLength > 0 && autotruck.CarriageWidth > 0)
            {
                Bitmap res = new Bitmap(CMCS.Monitor.Win.Properties.Resources.Autotruck);
                PreviewCarBmp carBmp = new PreviewCarBmp(autotruck);
                Bitmap bmp = carBmp.GetPreviewBitmap(res, 249, 130);
                bmp.Save("Web/CarSampler/Resources/images/Autotruck.png");
                pbxAutotruck.Image = bmp;

                return true;
            }
            return false;
        }

        /// <summary>
        /// 急停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show(this.btnStop, "确定急停", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                commonDAO.SendAppRemoteControlCmd(this.CurrentMachineCode, "急停", "1");
                commonDAO.SaveOperationLog("设置" + this.CurrentMachineCode + "急停", GlobalVars.LoginUser.Name);
            }
        }

        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show(this.btnStop, "确定复位", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                commonDAO.SendAppRemoteControlCmd(this.CurrentMachineCode, "急停", "0");
                commonDAO.SaveOperationLog("设置" + this.CurrentMachineCode + "急停复位", GlobalVars.LoginUser.Name);
            }
        }

        /// <summary>
        /// 重新采样
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReSample_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show(this.btnStop, "确定重新采样", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                string MachineCode_QC_Weighter = string.Empty;
                if (this.CurrentMachineCode == GlobalVars.MachineCode_QCJXCYJ_1)
                    MachineCode_QC_Weighter = GlobalVars.MachineCode_QC_Weighter_1;
                else
                    MachineCode_QC_Weighter = GlobalVars.MachineCode_QC_Weighter_3;

                commonDAO.SendAppRemoteControlCmd(MachineCode_QC_Weighter, "重新采样", "1");
                commonDAO.SaveOperationLog("设置" + this.CurrentMachineCode + "重新采样", GlobalVars.LoginUser.Name);
            }
        }
    }
}
