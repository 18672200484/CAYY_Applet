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

namespace CMCS.DumblyConcealer.Tasks.CarJXSampler
{
	/// <summary>
	/// 汽车机械采样机OPC接口
	/// </summary>
	public class EquCarJXSamplerOPC
	{
		private static EquCarJXSamplerOPC instance;

		public static EquCarJXSamplerOPC GetInstance()
		{
			if (instance == null)
			{
				instance = new EquCarJXSamplerOPC();
			}
			return instance;
		}
		/// <summary>
		/// EquCarJXSamplerDAO
		/// </summary>
		public EquCarJXSamplerOPC()
		{
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		#region OPC变量
		static OPCClientDAO opcServere = null;
		string[] tags = new string[] { "运行状态", "当前采样点数", "当前桶号", "当前X坐标", "当前Y坐标", "当前Z坐标", "大车故障", "小车故障", "升降故障",
			"动力头故障","匀料器故障","给料皮带故障","环锤故障","反击板故障","缩分故障","样料故障","弃料故障","桶故障","道闸故障","大车运行","小车运行","升降运行",
		"动力头运行","匀料器运行","给料皮带运行","环锤运行","反击板运行","缩分运行","样料运行","弃料运行","样煤仓运行","道闸运行","前边界","后边界","左边界","右边界",
		"上边界","下边界","道闸抬到位","道闸落到位","采样自动","采样急停","制样自动","制样急停","远程/就地","启动/停止","急停/复位","远程手动/自动"
			};
		#endregion

		/// <summary>
		/// 同步OPC点位信息
		/// </summary>
		/// <param name="output"></param> 
		/// <returns></returns>
		public void SyncOPCTags(Action<string, eOutputType> output)
		{
			List<string> lists = new List<string>();
			//lists.Add("汽车机械采样机.#1采样机._System._AutoDemoted");
			//lists.Add("汽车机械采样机.#1采样机._System._ConnectTimeout");
			//lists.Add("汽车机械采样机.#1采样机._System._DemanPoll");
			for (int i = 0; i < tags.Length; i++)
			{
				lists.Add("汽车机械采样机.#1采样机." + tags[i]);
			}
			for (int i = 0; i < tags.Length; i++)
			{
				lists.Add("汽车机械采样机.#2采样机." + tags[i]);
			}
			opcServere = new OPCClientDAO(lists, "Kepware.KEPServerEX.V6", "127.0.0.1");
			opcServere.InitOPC(output);
		}

		/// <summary>
		/// 采样机1执行控制命令
		/// </summary>
		/// <param name="output"></param>
		public void RunCmd1(Action<string, eOutputType> output)
		{
			CmcsAppRemoteControlCmd appRemoteControlCmd = commonDAO.GetNewestAppRemoteControlCmd(GlobalVars.MachineCode_QCJXCYJ_1);
			if (appRemoteControlCmd != null)
			{
				if (appRemoteControlCmd.CmdCode == "急停")
				{
					output("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param, eOutputType.Normal);
					Dictionary<string, object> cmd = new Dictionary<string, object>();
					cmd.Add("汽车机械采样机.#1采样机." + "急停/复位", appRemoteControlCmd.CmdCode);
					if (opcServere.WriteOPCItemValue(cmd))
					{
						// 更新执行结果
						commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
						commonDAO.SaveSysMessage(appRemoteControlCmd.AppIdentifier + "急停", appRemoteControlCmd.AppIdentifier + "急停执行成功");
					}
				}
			}
		}

		/// <summary>
		/// 采样机2执行控制命令
		/// </summary>
		/// <param name="output"></param>
		public void RunCmd2(Action<string, eOutputType> output)
		{
			CmcsAppRemoteControlCmd appRemoteControlCmd = commonDAO.GetNewestAppRemoteControlCmd(GlobalVars.MachineCode_QCJXCYJ_2);
			if (appRemoteControlCmd != null)
			{
				if (appRemoteControlCmd.CmdCode == "急停")
				{
					output("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param, eOutputType.Normal);
					Dictionary<string, object> cmd = new Dictionary<string, object>();
					cmd.Add("汽车机械采样机.#2采样机." + "急停/复位", appRemoteControlCmd.CmdCode);
					if (opcServere.WriteOPCItemValue(cmd))
					{
						// 更新执行结果
						commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
						commonDAO.SaveSysMessage(appRemoteControlCmd.AppIdentifier + "急停", appRemoteControlCmd.AppIdentifier + "急停执行成功");
					}
				}
			}
		}
	}
}
