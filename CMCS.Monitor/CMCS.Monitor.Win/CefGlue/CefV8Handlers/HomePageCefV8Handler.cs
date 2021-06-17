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

namespace CMCS.Monitor.Win.CefGlue
{
    /// <summary>
    /// 集中管控首页 CefV8Handler
    /// </summary>
    public class HomePageCefV8Handler : CefV8Handler
    {
        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            returnValue = null;
            //string paramSampler = arguments[0].GetStringValue();

            switch (name)
            {
                //  打开火车机械采样机监控界面
                case "OpenTrainSampler":
                    CefProcessMessage cefMsg5 = CefProcessMessage.Create("OpenTrainSampler");
                    cefMsg5.Arguments.SetSize(0);
                    cefMsg5.Arguments.SetString(0, arguments[0].GetStringValue());
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg5);
                    break;
                //  打开汽车入厂重车衡监控
                case "OpenTruckWeighter":
                    CefProcessMessage cefMsg6 = CefProcessMessage.Create("OpenTruckWeighter");
                    cefMsg6.Arguments.SetSize(0);
                    cefMsg6.Arguments.SetString(0, arguments[0].GetStringValue());
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg6);
                    break;
                //  打开汽车机械采样机监控
                case "OpenCarSampler1":
                    CefProcessMessage cefMsg4 = CefProcessMessage.Create("OpenCarSampler1");
                    cefMsg4.Arguments.SetSize(0);
                    cefMsg4.Arguments.SetString(0, arguments[0].GetStringValue());
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg4);
                    break;
                //  打开化验室监控
                case "OpenLaboratory":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenAssayManage"));
                    break;
                //  打开合样归批
                case "OpenBatchMachine":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenBatchMachine"));
                    break;
                //  打开气动传输监控
                case "OpenAutoCupboard":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenAutoCupboard"));
                    break;
                //  打开智能存样柜
                case "OpenSampleCabinet":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenSampleCabinet"));
                    break;
                //  打开全自动制样机监控界面
                case "OpenAutoMaker":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenAutoMaker"));
                    break;
                //  打开皮带采样机监控界面
                case "OpenTrainBeltSampler":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenTrainBeltSampler"));
                    break;
                default:
                    returnValue = null;
                    break;
            }

            return true;
        }
    }
}
