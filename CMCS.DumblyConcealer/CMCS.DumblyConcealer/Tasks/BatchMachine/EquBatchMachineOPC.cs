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
	/// 合样归批OPC接口
	/// </summary>
	public class EquBatchMachineOPC
	{
		private static EquBatchMachineOPC instance;

		public static EquBatchMachineOPC GetInstance()
		{
			if (instance == null)
			{
				instance = new EquBatchMachineOPC();
			}
			return instance;
		}
		/// <summary>
		/// EquBatchMachineOPC
		/// </summary>
		public EquBatchMachineOPC()
		{
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		#region OPC变量
		static OPCClientDAO opcServere = null;
		string[] tags = new string[] { "当前轴位置", "制样机卸料准备好信号", "系统自动1手动0", "归批流程运行中", "存桶选择工位","归批读卡编码",
		"倒料流程运行中","倒料选择工位","倒料读卡编码","倒料编码给定","倒料流程运行","归批流程状态"};
		

		string[] tags_other = new string[] { "M_归批流程总报警",
											"M_倒料流程总报警",
											"M_进桶单元总报警",
											"M_归批暂存单元总报警",
											"M_开盖倒料单元总报警",
											"M_系统信息总报警",
											"M_系统总提示",
											"M_进桶滚筒电机保护开关跳闸报警",
											"M_进桶读卡滚筒电机保护开关跳闸报警",
											"M_进桶滚筒挡桶气缸挡桶超时报警",
											"M_进桶滚筒挡桶气缸放桶超时报警",
											"M_进桶读卡滚筒挡桶气缸挡桶超时报警",
											"M_进桶读卡滚筒挡桶气缸放桶超时报警",
											"M_进桶读卡器读卡失败报警",
											"M_归批暂存当前进桶样桶无工位存桶",
											"M_归批暂存读卡成功编码为空",
											"M_归批暂存X轴保护开关跳闸报警",
											"M_归批暂存X轴驱动器报警",
											"M_归批暂存滚筒电机保护开关跳闸报警",
											"M_归批暂存气缸进桶超时报警",
											"M_归批暂存气缸回位超时报警",
											"M_归批暂存门禁气缸开门超时报警",
											"M_归批暂存门禁气缸回位超时报警",
											"M_编码器检测距离误差过大报警",
											"M_归批暂存疑似倒桶报警",
											"M_样桶出桶后检测不到桶",
											"M_归批暂存X轴回零失败",
											"M_开盖倒料滚筒电机保护开关跳闸报警",
											"M_开盖倒料读卡滚筒电机保护开关跳闸报警",
											"M_开盖倒料升降电机保护开关跳闸报警",
											"M_开盖倒料升降电机变频器报警",
											"M_开盖倒料平移电机保护开关跳闸报警",
											"M_开盖倒料平移电机驱动器报警",
											"M_开盖倒料旋转电机保护开关跳闸报警",
											"M_洗桶电机保护开关跳闸报警",
											"M_空桶暂存滚筒电机保护开关跳闸报警",
											"M_开盖倒料滚筒挡桶气缸挡桶超时报警",
											"M_开盖倒料滚筒挡桶气缸放桶超时报警",
											"M_开盖倒料读卡滚筒挡桶气缸挡桶超时报警",
											"M_开盖倒料读卡滚筒挡桶气缸放桶超时报警",
											"M_开盖倒料读卡位挡桶气缸挡桶超时报警",
											"M_开盖倒料读卡位挡桶气缸放桶超时报警",
											"M_开盖倒料夹桶气缸夹桶超时报警",
											"M_开盖倒料夹桶气缸松桶超时报警",
											"M_开盖接盖气缸接盖超时报警",
											"M_开盖接盖气缸回位超时报警",
											"M_开盖倒料升降电机上升超时报警",
											"M_开盖倒料升降电机下降超时报警",
											"M_开盖倒料平移电机前进超时报警",
											"M_开盖倒料平移电机后退超时报警",
											"M_开盖倒料旋转电机正转倒料超时报警",
											"M_开盖倒料旋转电机反转回位超时报警",
											"M_开盖负压不足报警",
											"M_开盖倒料读卡位检测不到样桶报警",
											"M_开盖倒料流程样桶处理数量小于出桶数量",
											"M_开盖倒料读卡失败",
											"M_倒料编码与读卡编码不一致",
											"M_开盖倒料多次开盖失败报警",
											"M_开盖倒料旋转限位故障报警",
											"M_升降模块升降电机保护开关跳闸报警",
											"M_升降模块滚筒电机保护开关跳闸报警",
											"M_制样间滚筒线一电机1保护开关跳闸报警",
											"M_升降模块升降电机上升超时报警",
											"M_升降模块升降电机下降超时报警",
											"M_空桶暂存推桶气缸推桶超时报警",
											"M_空桶暂存推桶气缸回位超时报警",
											"M_空桶暂存挡桶气缸2挡桶超时报警",
											"M_空桶暂存挡桶气缸2回位超时报警",
											"M_开盖平移拿桶位疑似失效",
											"M_开盖平移开始倒料位疑似失效",
											"M_系统气压压力检测报警",
											"M_系统远程按钮急停",
											"M_使用权限到期",
											"M_本地数据库连接失败",
											"M_上位机急停",
											"M_卸车端通信失败报警"
											};

		string[] tags_xcd = new string[] { "卸车端_车辆交互流程运行", "卸车端报警" };

		string[] tags_zcd = new string[] { "装车端_车辆交互流程运行", "装车端_满桶转存流程运行", "装车端报警" };

		string[] tags_cld = new string[] { "车辆报警" };
		#endregion

		/// <summary>
		/// 同步OPC点位信息
		/// </summary>
		/// <param name="output"></param> 
		/// <returns></returns>
		public void SyncOPCTags(Action<string, eOutputType> output)
		{
			List<string> lists = new List<string>();
			for (int i = 0; i < tags.Length; i++)
			{
				lists.Add("合样归批机.#1合样归批机." + tags[i]);
			}
			for (int i = 0; i < tags_other.Length; i++)
			{
				lists.Add("合样归批机.#1合样归批机." + tags_other[i]);
			}
			for (int i = 0; i < tags_xcd.Length; i++)
			{
				lists.Add("合样归批机.卸车端." + tags_xcd[i]);
			}
			for (int i = 0; i < tags_zcd.Length; i++)
			{
				lists.Add("合样归批机.装车端." + tags_zcd[i]);
			}
			for (int i = 0; i < tags_cld.Length; i++)
			{
				lists.Add("合样归批机.小车端." + tags_cld[i]);
			}
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
				if (appRemoteControlCmd.CmdCode == "故障复位")
				{
					output("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param, eOutputType.Normal);
					Dictionary<string, object> cmd = new Dictionary<string, object>();
					cmd.Add("合样归批机.#1合样归批机." + "故障复位", true);
					if (opcServere.WriteOPCItemValue(cmd))
					{
						// 更新执行结果
						commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
						commonDAO.SaveSysMessage(appRemoteControlCmd.AppIdentifier + "故障复位", appRemoteControlCmd.AppIdentifier + "故障复位执行成功");
					}
				}
				else if(appRemoteControlCmd.CmdCode == "归批流程暂停")
				{
					output("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param, eOutputType.Normal);
					Dictionary<string, object> cmd = new Dictionary<string, object>();
					cmd.Add("合样归批机.#1合样归批机." + "归批流程暂停", true);
					if (opcServere.WriteOPCItemValue(cmd))
					{
						// 更新执行结果
						commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
						commonDAO.SaveSysMessage(appRemoteControlCmd.AppIdentifier + "归批流程暂停", appRemoteControlCmd.AppIdentifier + "归批流程暂停执行成功");
					}
				}
				else if (appRemoteControlCmd.CmdCode == "倒料流程暂停")
				{
					output("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param, eOutputType.Normal);
					Dictionary<string, object> cmd = new Dictionary<string, object>();
					cmd.Add("合样归批机.#1合样归批机." + "倒料流程暂停", true);
					if (opcServere.WriteOPCItemValue(cmd))
					{
						// 更新执行结果
						commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
						commonDAO.SaveSysMessage(appRemoteControlCmd.AppIdentifier + "倒料流程暂停", appRemoteControlCmd.AppIdentifier + "倒料流程暂停执行成功");
					}
				}
				else if (appRemoteControlCmd.CmdCode == "归批流程复位")
				{
					output("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param, eOutputType.Normal);
					Dictionary<string, object> cmd = new Dictionary<string, object>();
					cmd.Add("合样归批机.#1合样归批机." + "归批流程复位", true);
					if (opcServere.WriteOPCItemValue(cmd))
					{
						// 更新执行结果
						commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
						commonDAO.SaveSysMessage(appRemoteControlCmd.AppIdentifier + "归批流程复位", appRemoteControlCmd.AppIdentifier + "归批流程复位执行成功");
					}
				}
				else if (appRemoteControlCmd.CmdCode == "倒料流程复位")
				{
					output("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param, eOutputType.Normal);
					Dictionary<string, object> cmd = new Dictionary<string, object>();
					cmd.Add("合样归批机.#1合样归批机." + "倒料流程复位", true);
					if (opcServere.WriteOPCItemValue(cmd))
					{
						// 更新执行结果
						commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
						commonDAO.SaveSysMessage(appRemoteControlCmd.AppIdentifier + "倒料流程复位", appRemoteControlCmd.AppIdentifier + "倒料流程复位执行成功");
					}
				}
			}
		}
	}
}
