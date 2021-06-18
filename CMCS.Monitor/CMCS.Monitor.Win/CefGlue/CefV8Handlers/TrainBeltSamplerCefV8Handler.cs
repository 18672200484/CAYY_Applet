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
//
using Xilium.CefGlue;

namespace CMCS.Monitor.Win.CefGlue
{
	/// <summary>
	/// 火车采样监控 CefV8Handler
	/// </summary>
	public class TrainBeltSamplerCefV8Handler : CefV8Handler
	{
		List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();
		CommonDAO commonDAO = CommonDAO.GetInstance();

		protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
		{
			exception = null;
			returnValue = null;
			//string paramSampler = arguments[0].GetStringValue();

			switch (name)
			{
				//case "ChangeSelected":
				//	CefProcessMessage cefMsg = CefProcessMessage.Create("TrainSamplerChangeSelected");
				//	cefMsg.Arguments.SetSize(0);
				//	cefMsg.Arguments.SetString(0, paramSampler);
				//	CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg);
				//	break;
				case "LeadCar1":
					CefProcessMessage cefMsg1 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg1.Arguments.SetSize(0);
					cefMsg1.Arguments.SetString(0, "LeadCar1");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg1);
					break;
				case "MovingBelt1":
					CefProcessMessage cefMsg2 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg2.Arguments.SetSize(0);
					cefMsg2.Arguments.SetString(0, "MovingBelt1");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg2);
					break;
				case "LeadCar2":
					CefProcessMessage cefMsg3 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg3.Arguments.SetSize(0);
					cefMsg3.Arguments.SetString(0, "LeadCar2");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg3);
					break;
				case "MovingBelt2":
					CefProcessMessage cefMsg4 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg4.Arguments.SetSize(0);
					cefMsg4.Arguments.SetString(0, "MovingBelt2");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg4);
					break;
				case "StopSampler1":
					CefProcessMessage cefMsg5 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg5.Arguments.SetSize(0);
					cefMsg5.Arguments.SetString(0, "StopSampler1");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg5);
					break;
				case "StopSampler2":
					CefProcessMessage cefMsg6 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg6.Arguments.SetSize(0);
					cefMsg6.Arguments.SetString(0, "StopSampler2");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg6);
					break;
				case "SendSampler1":
					CefProcessMessage cefMsg7 = CefProcessMessage.Create("TrainBeltSamplerPlan");
					cefMsg7.Arguments.SetSize(0);
					cefMsg7.Arguments.SetString(0, "SendSampler1");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg7);
					break;
				case "SendSampler2":
					CefProcessMessage cefMsg8 = CefProcessMessage.Create("TrainBeltSamplerPlan");
					cefMsg8.Arguments.SetSize(0);
					cefMsg8.Arguments.SetString(0, "SendSampler2");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg8);
					break;
				case "AlarmReset1":
					CefProcessMessage cefMsg9 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg9.Arguments.SetSize(0);
					cefMsg9.Arguments.SetString(0, "AlarmReset1");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg9);
					break;
				case "AlarmReset2":
					CefProcessMessage cefMsg10 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg10.Arguments.SetSize(0);
					cefMsg10.Arguments.SetString(0, "AlarmReset2");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg10);
					break;
				case "FZJAlarmReset1":
					CefProcessMessage cefMsg11 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg11.Arguments.SetSize(0);
					cefMsg11.Arguments.SetString(0, "FZJAlarmReset1");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg11);
					break;
				case "FZJAlarmReset2":
					CefProcessMessage cefMsg12 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg12.Arguments.SetSize(0);
					cefMsg12.Arguments.SetString(0, "FZJAlarmReset2");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg12);
					break;
				case "StopS1":
					CefProcessMessage cefMsg13 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg13.Arguments.SetSize(0);
					cefMsg13.Arguments.SetString(0, "StopS1");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg13);
					break;
				case "StopS2":
					CefProcessMessage cefMsg14 = CefProcessMessage.Create("TrainBeltSamplerCmd");
					cefMsg14.Arguments.SetSize(0);
					cefMsg14.Arguments.SetString(0, "StopS2");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg14);
					break;
				case "FaultRecord1":
					CefProcessMessage cefMsg15 = CefProcessMessage.Create("FaultRecord");
					cefMsg15.Arguments.SetSize(0);
					cefMsg15.Arguments.SetString(0, "2PA皮采");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg15);
					break;
				case "FaultRecord2":
					CefProcessMessage cefMsg16 = CefProcessMessage.Create("FaultRecord");
					cefMsg16.Arguments.SetSize(0);
					cefMsg16.Arguments.SetString(0, "2PB皮采");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg16);
					break;
				//  打开采样机报警信息
				case "AlarmInfo":
					CefProcessMessage cefMsg = CefProcessMessage.Create("TrainBeltSampleAlarmInfo");
					cefMsg.Arguments.SetSize(0);
					cefMsg.Arguments.SetString(0, arguments[0].GetStringValue());
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg);
					break;
				case "DataSelect":
					CefProcessMessage cefMsg17 = CefProcessMessage.Create("TrainBeltSampleDataSelect");
					cefMsg17.Arguments.SetSize(0);
					cefMsg17.Arguments.SetString(0, "DataSelect");
					CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, cefMsg17);
					break;
				default:
					returnValue = null;
					break;
			}

			return true;
		}
	}
}
