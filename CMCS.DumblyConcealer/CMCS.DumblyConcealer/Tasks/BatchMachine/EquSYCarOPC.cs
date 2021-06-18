using System;
using System.Collections.Generic;
//
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Utilities;
using OPCAutomation;

namespace CMCS.DumblyConcealer.Tasks.BeltSampler
{
	/// <summary>
	/// 送样小车OPC接口
	/// </summary>
	public class EquSYCarOPC
	{
		private static EquSYCarOPC instance;

		public static EquSYCarOPC GetInstance()
		{
			if (instance == null)
			{
				instance = new EquSYCarOPC();
			}
			return instance;
		}
		/// <summary>
		/// EquBatchMachineOPC
		/// </summary>
		public EquSYCarOPC()
		{
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		#region OPC变量
		static OPCClientDAO opcServere = null;
		string[] tags = new string[] { "装车端模拟刷卡", "卸车端模拟刷卡" };

		#endregion

		/// <summary>
		/// 同步OPC点位信息
		/// </summary>
		/// <param name="output"></param> 
		/// <returns></returns>
		public void SyncOPCTags(Action<string, eOutputType> output)
		{
			List<string> lists = new List<string>();

			lists.Add("送样小车.装车端." + tags[0]);
			lists.Add("送样小车.卸车端." + tags[1]);

			opcServere = new OPCClientDAO(lists, "Kepware.KEPServerEX.V6", "127.0.0.1");
			opcServere.InitOPC(output);
		}

		/// <summary>
		/// 执行控制命令
		/// </summary>
		/// <param name="output"></param>
		public void RunCmd(Action<string, eOutputType> output)
		{
			CmcsAppRemoteControlCmd appRemoteControlCmd = commonDAO.GetNewestAppRemoteControlCmd(GlobalVars.MachineCode_HYGPJ_1);
			if (appRemoteControlCmd != null)
			{
				if (appRemoteControlCmd.CmdCode == "卸车刷卡")
				{
					output("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param, eOutputType.Normal);
					Dictionary<string, object> cmd = new Dictionary<string, object>();
					cmd.Add("送样小车.卸车端." + "卸车刷卡", true);
					if (opcServere.WriteOPCItemValue(cmd))
					{
						// 更新执行结果
						commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
						commonDAO.SaveSysMessage(appRemoteControlCmd.AppIdentifier + "卸车刷卡", appRemoteControlCmd.AppIdentifier + "卸车刷卡执行成功");
					}
				}
				else if (appRemoteControlCmd.CmdCode == "装车刷卡")
				{
					output("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param, eOutputType.Normal);
					Dictionary<string, object> cmd = new Dictionary<string, object>();
					cmd.Add("送样小车.卸车端." + "装车刷卡", true);
					if (opcServere.WriteOPCItemValue(cmd))
					{
						// 更新执行结果
						commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
						commonDAO.SaveSysMessage(appRemoteControlCmd.AppIdentifier + "装车刷卡", appRemoteControlCmd.AppIdentifier + "装车刷卡执行成功");
					}
				}
			}
		}
	}
}
