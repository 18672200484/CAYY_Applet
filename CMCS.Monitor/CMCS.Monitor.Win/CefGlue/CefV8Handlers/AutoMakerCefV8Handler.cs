using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.Inf;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Frms;
using CMCS.Monitor.Win.Frms.Sys;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.Utilities;
//
using Xilium.CefGlue;

namespace CMCS.Monitor.Win.CefGlue
{
    /// <summary>
    /// 汽车采样监控 CefV8Handler
    /// </summary>
    public class AutoMakerCefV8Handler : CefV8Handler
    {
        List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();
        CommonDAO commonDAO = CommonDAO.GetInstance();

        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            returnValue = null;
            string paramSampler = arguments[0].GetStringValue();

            switch (name)
            {
                // 急停
                case "Stop":
                    commonDAO.SendAppRemoteControlCmd(MonitorCommon.GetInstance().GetCarSamplerMachineCodeBySelected(arguments[0].GetStringValue()), "急停", "1");

                    CefProcessMessage cefMsg1 = CefProcessMessage.Create("SaveOperationLog");
                    cefMsg1.Arguments.SetSize(0);
                    cefMsg1.Arguments.SetString(0, "设置" + MonitorCommon.GetInstance().GetCarSamplerMachineCodeBySelected(arguments[0].GetStringValue()) + "急停");
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg1);
                    break;
                // 车辆信息
                case "CarInfo":
                    if (paramSampler == "#1")
                        MessageBox.Show("#1 CarInfo");
                    else if (paramSampler == "#2")
                        MessageBox.Show("#2 CarInfo");
                    break;
                // 故障复位
                case "ErrorReset":
                    if (paramSampler == "#1")
                        MessageBox.Show("#1 ErrorReset");
                    else if (paramSampler == "#2")
                        MessageBox.Show("#2 ErrorReset");
                    break;
                // 采样历史记录
                case "SampleHistory":
                    if (paramSampler == "#1")
                        MessageBox.Show("#1 SampleHistory");
                    else if (paramSampler == "#2")
                        MessageBox.Show("#2 SampleHistory");
                    break;
                //  打开制样机报警信息
                case "ErrorInfo":
                    CefProcessMessage cefMsg = CefProcessMessage.Create("AutoMakerErrorInfo");
                    cefMsg.Arguments.SetSize(0);
                    cefMsg.Arguments.SetString(0, arguments[0].GetStringValue());
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg);
                    break;
                //  历史故障
                case "FaultRecord":
                    cefMsg = CefProcessMessage.Create("FaultRecord");
                    cefMsg.Arguments.SetSize(0);
                    cefMsg.Arguments.SetString(0, arguments[0].GetStringValue());
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg);
                    break;
                // 故障复位
                case "FaultReset":
                    commonDAO.SendAppRemoteControlCmd("#1全自动制样机", "故障复位", "1");

                    CefProcessMessage cefMsg2 = CefProcessMessage.Create("SaveOperationLog");
                    cefMsg2.Arguments.SetSize(0);
                    cefMsg2.Arguments.SetString(0, "设置#1全自动制样机故障复位");
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
