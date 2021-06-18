﻿using System;
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
        CommonDAO commonDAO = CommonDAO.GetInstance();
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
            

            string value = string.Empty, machineCode = string.Empty, equInfSamplerSystemStatus = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();

            #region 皮带采样机 #1

            datas.Clear();
            machineCode = GlobalVars.MachineCode_PDCYJ_1;

            datas.Add(new HtmlDataItem("#1翻车机1", commonDAO.GetSignalDataValue(machineCode, "#1翻车机") == "1" ? "#FF0000" : "#808080", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1翻车机1a", commonDAO.GetSignalDataValue(machineCode, "#1翻车机") == "1" ? "#FF0000" : "url(#SVGID_83_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2翻车机1", commonDAO.GetSignalDataValue(machineCode, "#2翻车机") == "1" ? "#FF0000" : "#808080", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2翻车机1a", commonDAO.GetSignalDataValue(machineCode, "#2翻车机") == "1" ? "#FF0000" : "url(#SVGID_85_)", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("2PA", commonDAO.GetSignalDataValue(machineCode, "主皮带") == "1" ? "Red" : "url(#SVGID_17_)", eHtmlDataItemType.svg_color));
            //value = commonDAO.GetSignalDataValue(machineCode, "#2翻车机");
            //if (value == "1")
            //{
                datas.Add(new HtmlDataItem("2PA1", commonDAO.GetSignalDataValue(machineCode, "主皮带") == "1" && commonDAO.GetSignalDataValue(machineCode, "#2翻车机") =="1"? "Red" : "url(#SVGID_17_-5)", eHtmlDataItemType.svg_color));
            //}

            datas.Add(new HtmlDataItem("初级给料皮带正转a", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带正转") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("初级给料皮带反转a", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带反转") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("初级给料皮带清扫a", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带清扫") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("破碎机a", commonDAO.GetSignalDataValue(machineCode, "初级破碎机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("破碎清扫电机a", commonDAO.GetSignalDataValue(machineCode, "破碎清扫电机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("次级给料皮带a", commonDAO.GetSignalDataValue(machineCode, "次级给料皮带") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("次级给料皮带清扫a", commonDAO.GetSignalDataValue(machineCode, "次级给料皮带清扫") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("缩分器a", commonDAO.GetSignalDataValue(machineCode, "缩分器") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("斗提机a", commonDAO.GetSignalDataValue(machineCode, "弃料提升斗") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("返料输送机a", commonDAO.GetSignalDataValue(machineCode, "返料皮带") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

            if (commonDAO.GetSignalDataValue(machineCode, "M_给料皮带空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("初级给料皮带正转a", "#ff0000", eHtmlDataItemType.svg_color));
                datas.Add(new HtmlDataItem("初级给料皮带反转a", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_初级清扫故障") == "1")
            {
                datas.Add(new HtmlDataItem("初级给料皮带清扫a", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_破碎电机空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("破碎机a", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_破碎清扫电机空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("破碎清扫电机a", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_缩分皮带空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("次级给料皮带a", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_次级清扫故障") == "1")
            {
                datas.Add(new HtmlDataItem("次级给料皮带清扫a", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_缩分电机空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("缩分器a", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_斗提机空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("斗提机a", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_返料输送机故障") == "1")
            {
                datas.Add(new HtmlDataItem("返料输送机a", "#ff0000", eHtmlDataItemType.svg_color));
            }

            datas.Add(new HtmlDataItem("初级给料皮带正转方向a", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带正转"), tempBool, eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("初级给料皮带反转方向a", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带反转"), tempBool, eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("次级给料皮带方向a", commonDAO.GetSignalDataValue(machineCode, "次级给料皮带"), tempBool, eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("斗提机方向a", commonDAO.GetSignalDataValue(machineCode, "弃料提升斗"), tempBool,eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("返料运输机方向a", commonDAO.GetSignalDataValue(machineCode, "返料皮带"), tempBool,eHtmlDataItemType.svg_visible));

            datas.Add(new HtmlDataItem("封装机运行状态a", commonDAO.GetSignalDataValue(machineCode, "封装机运行状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机准备状态a", commonDAO.GetSignalDataValue(machineCode, "封装机准备好") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机连接状态a", commonDAO.GetSignalDataValue(machineCode, "封装机连接状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机报警状态a", commonDAO.GetSignalDataValue(machineCode, "封装机报警") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("采样机PLC连接状态a", commonDAO.GetSignalDataValue(machineCode, "PLC连接状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机无报警状态a", commonDAO.GetSignalDataValue(machineCode, "采样机报警") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机报警a", commonDAO.GetSignalDataValue(machineCode, "采样机报警") == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("远程自动状态a", commonDAO.GetSignalDataValue(machineCode, "远程自动") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("远程手动状态a", commonDAO.GetSignalDataValue(machineCode, "远程手动") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("就地自动状态a", commonDAO.GetSignalDataValue(machineCode, "就地自动") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("就地手动状态a", commonDAO.GetSignalDataValue(machineCode, "就地手动") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("下次采样时间a", commonDAO.GetSignalDataValue(machineCode, "下次采样时间"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带称a", commonDAO.GetSignalDataValue(machineCode, "皮带秤"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("当前样重a", commonDAO.GetSignalDataValue(machineCode, "封装机装样重量"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样次数a", commonDAO.GetSignalDataValue(machineCode, "封装机装样次数"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("进料桶煤量a", (commonDAO.GetSignalDataValueDouble(machineCode, "封装机装样次数")/25*70).ToString() ,"1", eHtmlDataItemType.svg_height));

            if (commonDAO.GetSignalDataValue(machineCode, "#1翻车机") == "1")
            {
                datas.Add(new HtmlDataItem("当前采样码a", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "采样编码"), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("采样机编码a", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "采样编码"), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("已翻车数a", GetYFCS("1", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "采样编码")).ToString(), eHtmlDataItemType.svg_text));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "#2翻车机") == "1")
            {
                datas.Add(new HtmlDataItem("当前采样码a", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "采样编码"), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("采样机编码a", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "采样编码"), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("已翻车数a", GetYFCS("2", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "采样编码")).ToString(), eHtmlDataItemType.svg_text));
            }

            datas.Add(new HtmlDataItem("翻车机编码a", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "采样编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("翻车机车数a", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "翻车机车数"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("已翻车数a", commonDAO.GetSignalDataValue(machineCode, "已翻车车数"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("允许一号翻车机翻车", commonDAO.GetSignalDataValue(machineCode, "禁止1号翻车机翻车") == "0" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机故障a", commonDAO.GetSignalDataValue(machineCode, "采样机报警") == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("工位切换a", commonDAO.GetSignalDataValue(machineCode, "工位切换") == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            // 添加更多...

            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

            #endregion

            #region 皮带采样机 #2

            datas.Clear();
            machineCode = GlobalVars.MachineCode_PDCYJ_2;

            datas.Add(new HtmlDataItem("#1翻车机2", commonDAO.GetSignalDataValue(machineCode, "#1翻车机") == "1" ? "#FF0000" : "#808080", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1翻车机2a", commonDAO.GetSignalDataValue(machineCode, "#1翻车机") == "1" ? "#FF0000" : "url(#SVGID_84_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2翻车机2", commonDAO.GetSignalDataValue(machineCode, "#2翻车机") == "1" ? "#FF0000" : "#808080", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2翻车机2a", commonDAO.GetSignalDataValue(machineCode, "#2翻车机") == "1" ? "#FF0000" : "url(#SVGID_86_)", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("2PB", commonDAO.GetSignalDataValue(machineCode, "主皮带") == "1" ? "Red" : "url(#SVGID_18_)", eHtmlDataItemType.svg_color));
            //value = commonDAO.GetSignalDataValue(machineCode, "#1翻车机");
            //if (value == "1")
            //{
                datas.Add(new HtmlDataItem("2PB1", commonDAO.GetSignalDataValue(machineCode, "主皮带") == "1" && commonDAO.GetSignalDataValue(machineCode, "#1翻车机") =="1"? "Red" : "url(#SVGID_17_-4-2)", eHtmlDataItemType.svg_color));
            //}


            datas.Add(new HtmlDataItem("初级给料皮带正转b", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带正转") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("初级给料皮带反转b", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带反转") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("初级给料皮带清扫b", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带清扫") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("破碎机b", commonDAO.GetSignalDataValue(machineCode, "初级破碎机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("破碎清扫电机b", commonDAO.GetSignalDataValue(machineCode, "破碎清扫电机") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("次级给料皮带b", commonDAO.GetSignalDataValue(machineCode, "次级给料皮带") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("次级给料皮带清扫b", commonDAO.GetSignalDataValue(machineCode, "次级给料皮带清扫") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("缩分器b", commonDAO.GetSignalDataValue(machineCode, "缩分器") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("斗提机b", commonDAO.GetSignalDataValue(machineCode, "弃料提升斗") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("返料输送机b", commonDAO.GetSignalDataValue(machineCode, "返料皮带") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

            if (commonDAO.GetSignalDataValue(machineCode, "M_给料皮带空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("初级给料皮带正转b", "#ff0000", eHtmlDataItemType.svg_color));
                datas.Add(new HtmlDataItem("初级给料皮带反转b", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_初级清扫故障") == "1")
            {
                datas.Add(new HtmlDataItem("初级给料皮带清扫b", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_破碎电机空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("破碎机b", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_破碎清扫电机空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("破碎清扫电机b", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_缩分皮带空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("次级给料皮带b", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_次级清扫故障") == "1")
            {
                datas.Add(new HtmlDataItem("次级给料皮带清扫b", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_缩分电机空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("缩分器b", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_斗提机空开跳闸") == "1")
            {
                datas.Add(new HtmlDataItem("斗提机b", "#ff0000", eHtmlDataItemType.svg_color));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "M_返料输送机故障") == "1")
            {
                datas.Add(new HtmlDataItem("返料输送机b", "#ff0000", eHtmlDataItemType.svg_color));
            }

            datas.Add(new HtmlDataItem("初级给料皮带正转方向b", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带正转"), tempBool, eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("初级给料皮带反转方向b", commonDAO.GetSignalDataValue(machineCode, "初级给料皮带反转"), tempBool, eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("次级给料皮带方向b", commonDAO.GetSignalDataValue(machineCode, "次级给料皮带"), tempBool, eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("斗提机方向b", commonDAO.GetSignalDataValue(machineCode, "弃料提升斗"), tempBool, eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("返料运输机方向b", commonDAO.GetSignalDataValue(machineCode, "返料皮带"),tempBool, eHtmlDataItemType.svg_visible));

            datas.Add(new HtmlDataItem("封装机运行状态b", commonDAO.GetSignalDataValue(machineCode, "封装机运行状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机准备状态b", commonDAO.GetSignalDataValue(machineCode, "封装机准备好") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机连接状态b", commonDAO.GetSignalDataValue(machineCode, "封装机连接状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("封装机报警状态b", commonDAO.GetSignalDataValue(machineCode, "封装机报警") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("采样机PLC连接状态b", commonDAO.GetSignalDataValue(machineCode, "PLC连接状态") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机无报警状态b", commonDAO.GetSignalDataValue(machineCode, "采样机报警") == "1" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机报警b", commonDAO.GetSignalDataValue(machineCode, "采样机报警") == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("远程自动状态b", commonDAO.GetSignalDataValue(machineCode, "远程自动") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("远程手动状态b", commonDAO.GetSignalDataValue(machineCode, "远程手动") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("就地自动状态b", commonDAO.GetSignalDataValue(machineCode, "就地自动") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("就地手动状态b", commonDAO.GetSignalDataValue(machineCode, "就地手动") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("下次采样时间b", commonDAO.GetSignalDataValue(machineCode, "下次采样时间"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带称b", commonDAO.GetSignalDataValue(machineCode, "皮带秤"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("当前样重b", commonDAO.GetSignalDataValue(machineCode, "封装机装样重量"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样次数b", commonDAO.GetSignalDataValue(machineCode, "封装机装样次数"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("进料桶煤量b", (commonDAO.GetSignalDataValueDouble(machineCode, "封装机装样次数") / 25 * 70).ToString(), "2", eHtmlDataItemType.svg_height));

            if (commonDAO.GetSignalDataValue(machineCode, "#1翻车机") == "1")
            {
                datas.Add(new HtmlDataItem("当前采样码b", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "采样编码"), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("采样机编码b", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "采样编码"), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("已翻车数b", GetYFCS("1", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "采样编码")).ToString(), eHtmlDataItemType.svg_text));
            }
            if (commonDAO.GetSignalDataValue(machineCode, "#2翻车机") == "1")
            {
                datas.Add(new HtmlDataItem("当前采样码b", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "采样编码"), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("采样机编码b", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "采样编码"), eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("已翻车数b", GetYFCS("2", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "采样编码")).ToString(), eHtmlDataItemType.svg_text));
            }

            datas.Add(new HtmlDataItem("翻车机编码b", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "采样编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("翻车机车数b", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "翻车机车数"), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("已翻车数b", commonDAO.GetSignalDataValue(machineCode, "已翻车车数"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("允许二号翻车机翻车", commonDAO.GetSignalDataValue(machineCode, "禁止2号翻车机翻车") == "0" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机故障b", commonDAO.GetSignalDataValue(machineCode, "采样机报警") == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("工位切换b", commonDAO.GetSignalDataValue(machineCode, "工位切换") == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            // 添加更多...

            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

            tempBool = tempBool ? false : true;

            #endregion
        }

        /// <summary>
        /// 获取已翻车数
        /// </summary>
        /// <param name="fcj"></param>
        /// <param name="cym"></param>
        /// <returns></returns>
        public int GetYFCS(string fcj,string cym)
        {
            int re = 0;
            string sql = string.Format(@"select count(*) ct
                    from cmcstbtraincarriagepass t5 
                    left join fultbtransport t1 on t1.pkid=t5.id
                    left join fultbinfactorybatch t2 on t1.infactorybatchid=t2.id
                    inner join cmcstbtransportposition t6 on t5.id=t6.transportid 
                    left join cmcstbrcsampling t7 on t7.infactorybatchid=t2.id
                    where t6.tracknumber='{0}' and t7.samplecode='{1}'",
                    fcj=="1"?"#5":"#1", cym);
            DataTable dt = commonDAO.SelfDber.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                re = Convert.ToInt32(dt.Rows[0][0]);
            }
            return re;
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
