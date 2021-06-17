using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Xilium.CefGlue;
using System.Windows.Forms;
using CMCS.Monitor.Win.Frms;
using CMCS.Monitor.Win.Frms.Sys;
using CMCS.Monitor.Win.Core;
using CMCS.Common.DAO;
using CMCS.Common;
using CMCS.Monitor.Win.Utilities;

namespace CMCS.Monitor.Win.CefGlue
{
    /// <summary>
    /// 汽车衡监控 CefV8Handler
    /// </summary>
    public class TruckWeighterCefV8Handler : CefV8Handler
    {
        CommonDAO commonDAO = CommonDAO.GetInstance();

        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            returnValue = null;

            switch (name)
            {
                // 道闸1升杆
                case "Gate1Up":
                    commonDAO.SendAppRemoteControlCmd(arguments[0].GetStringValue(), "控制道闸", "Gate1Up");

                    CefProcessMessage cefMsg3 = CefProcessMessage.Create("SaveOperationLog");
                    cefMsg3.Arguments.SetSize(0);
                    cefMsg3.Arguments.SetString(0, "设置" + arguments[0].GetStringValue() + "道闸1升杆");
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg3);
                    break;
                // 道闸1降杆
                case "Gate1Down":
                    commonDAO.SendAppRemoteControlCmd(arguments[0].GetStringValue(), "控制道闸", "Gate1Down");

                    CefProcessMessage cefMsg4 = CefProcessMessage.Create("SaveOperationLog");
                    cefMsg4.Arguments.SetSize(0);
                    cefMsg4.Arguments.SetString(0, "设置" + arguments[0].GetStringValue() + "道闸1降杆");
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg4);
                    break;
                // 道闸2升杆
                case "Gate2Up":
                    commonDAO.SendAppRemoteControlCmd(arguments[0].GetStringValue(), "控制道闸", "Gate2Up");

                    CefProcessMessage cefMsg5 = CefProcessMessage.Create("SaveOperationLog");
                    cefMsg5.Arguments.SetSize(0);
                    cefMsg5.Arguments.SetString(0, "设置" + arguments[0].GetStringValue() + "道闸2升杆");
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg5);
                    break;
                // 道闸2降杆
                case "Gate2Down":
                    commonDAO.SendAppRemoteControlCmd(arguments[0].GetStringValue(), "控制道闸", "Gate2Down");

                    CefProcessMessage cefMsg6 = CefProcessMessage.Create("SaveOperationLog");
                    cefMsg6.Arguments.SetSize(0);
                    cefMsg6.Arguments.SetString(0, "设置" + arguments[0].GetStringValue() + "道闸2降杆");
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg6);
                    break;
                case "ChangeSelected":
                    CefProcessMessage cefMsg = CefProcessMessage.Create("TruckWeighterChangeSelected");
                    cefMsg.Arguments.SetSize(0);
                    cefMsg.Arguments.SetString(0, arguments[0].GetStringValue());
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg);
                    break;
                case "ChangeGH":
                    string stateGH = arguments[1].GetStringValue();
                    commonDAO.SetAppletConfig(arguments[0].GetStringValue(), "启动过衡", stateGH);

                    CefProcessMessage cefMsg1 = CefProcessMessage.Create("SaveOperationLog");
                    cefMsg1.Arguments.SetSize(0);
                    cefMsg1.Arguments.SetString(0, "设置" + arguments[0].GetStringValue() + stateGH == "1" ? "启动过衡" : "停止过衡");
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg1);

                    break;
                case "ChangeCY":
                    string stateCY = arguments[1].GetStringValue();
                    commonDAO.SetAppletConfig(arguments[0].GetStringValue(), "启动采样", stateCY);

                    CefProcessMessage cefMsg2 = CefProcessMessage.Create("SaveOperationLog");
                    cefMsg2.Arguments.SetSize(0);
                    cefMsg2.Arguments.SetString(0, "设置" + arguments[0].GetStringValue() + stateCY == "1" ? "启动采样" : "停止采样");
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg2);
                    break;
                default:
                    returnValue = null;
                    break;
            }

            return true;
        }
    }
}
