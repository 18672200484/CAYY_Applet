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
	/// 制样机OPC接口
	/// </summary>
	public class EquAutoMakerOPC
	{
		private static EquAutoMakerOPC instance;

		public static EquAutoMakerOPC GetInstance()
		{
			if (instance == null)
			{
				instance = new EquAutoMakerOPC();
			}
			return instance;
		}
		/// <summary>
		/// EquCarJXSamplerDAO
		/// </summary>
		public EquAutoMakerOPC()
		{
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		#region OPC变量
		static OPCClientDAO opcServere = null;
		string[] tags = new string[] { "左侧干燥机转速", "右侧干燥机转速", "左侧烘干倒计时", "右侧烘干倒计时", "左侧干燥箱温度","右侧干燥箱温度",
		"粉碎电机电流","主气路正压值","弃料收集仓负压值","粉碎单元正压值","粉碎单元真空上料机负压值","粉碎总计时","3mm制样倒计时",
		"6mm制样倒计时","在线测水连接状态","6mm瓶装机灌装口有瓶信号","3mm瓶装机灌装口有瓶信号3","3mm瓶装机灌装口有瓶信号1","3mm瓶装机灌装口有瓶信号2","机采给料步","故障复位"};
		string[] tags_weight = new string[] { "弃料称实时重量", "6mm瓶装机秤重量", "3mm瓶装机称实时重量", "3mm分析样称实时重量", "3mm干燥样称实时重量",
		"原煤称实时重量","原煤称实时重量","6mm瓶装机称净重","3mm分析样净重","3mm干燥后留样净重","3mm瓶装机称净重_3存查样","3mm瓶装机称净重_0_2分析样","3mm瓶装机称净重_0_2存查样",
		"原煤重量"};
		string[] tags_code = new string[] {"3mm瓶装机煤样编码","6mm煤样编码","3mm煤样编码","3mm一级提升机料斗煤样编码","3mm弃料一级皮带煤样编码","3mm弃料二级皮带煤样编码",
		"干燥煤样编码","干燥箱1煤样编码","干燥箱2煤样编码","原煤煤样编码","3mm二级提升机料斗煤样编码","粉碎煤样编码","3mm弃料桶煤样编码",
		"3mm煤样弃料旋转给料机煤样编码","弃料单元煤样编码","6mm瓶装机煤样编码" };
		string[] tags_flow = new string[] { "S_在线测水缩分准备步","S_在线测水缩分步","S_在线测水设备煤样传送步","S_6mm瓶装机进瓶步","S_原煤上料皮带上升步","S_原煤上料皮带前进出料步",
		"S_6mm煤样制备步","S_链式缩分单元小清洗步","S_原煤上料皮带后退步","S_原煤上料皮带下降步","S_机采伸缩皮带伸出步","S_机采伸缩皮带缩回步","ST_6mm制样无流程标记",
		"S_3mm煤样制备步","S_3mm制样称重步","ST_3mm制样无流程标记","S_3mm缩分称重步","S_3mm样接斗步","S_3mm样电机推出步","S_一级提升机上升步","S_一级提升机下降步",
		"S_3mm样电机缩回步","S_3mm样卸斗步","S_3mm煤样缩分步","S_圆盘缩分1单元小清洗步","S_3mm缩分弃料称重步","ST_3mm缩分无流程标记","S_干燥机入料准备步","S_干燥布料步",
		"S_烘干数据读取步","S_干燥单元小清洗步","ST_干燥布料无流程标记","S_干燥出料预备步","S_干燥气缸闸板出料步","S_干燥筛网出料步","S_干燥筛网摆动步","S_干燥毛重记录步","S_干燥称重步",
		"S_干燥筛网回原位步","ST_干燥出料无流程标记",
		"S_粉碎称重步","S_3mm干燥样卸斗步","S_3mm干燥样接斗步","S_3mm二级提升机直线行走电机后退步","S_圆盘缩分2准备步","S_3mm二级提升机上升步","S_3mm二级提升机倒料步","S_3mm二级提升机下降步",
		"S_3mm二级提升机直线行走电机前进步","S_3mm干燥制粉样输送步","S_3mm存查样传送步","S_粉碎弃料推出步","S_粉碎小清洗步","S_粉碎制样步","S_粉碎大清洗步","S_粉碎出料步",
        "S_粉碎弃料缩回步","S_粉碎正式样进料步","ST_粉碎制样无流程标记",
		"S_6mm瓶装机落瓶步","S_6mm瓶装机落瓶推瓶步","S_6mm瓶装机落瓶推瓶后退步","S_6mm瓶装机抱瓶翻转步","S_6mm瓶装机去翻转位步","S_6mm瓶装机去写卡步","S_6mm瓶装机写卡步",
        "S_6mm瓶装机去暂存皮带步_空瓶_","S_6mm瓶装机去下料口步","S_6mm瓶装机回推瓶步_从下料口_","S_6mm瓶装机回推瓶步_从暂存皮带_空瓶",
		"S_3mm瓶装机落瓶步","S_3mm瓶装机落瓶推瓶步","S_3mm瓶装机落瓶推瓶后退步","S_3mm瓶装机抱瓶翻转步","S_3mm瓶装机去翻转位步","S_3mm瓶装机去写卡步","S_3mm瓶装机写卡步","S_3mm瓶装机去暂存皮带步_空瓶_",
		"S_3mm瓶装机去下料口步","S_3mm瓶装机回推瓶步_从下料口_","S_3mm瓶装机回推瓶步_从暂存皮带_空瓶",
		"S_在线测水弃料步","S_6mm弃料收集步","S_弃料双向皮带排空步"
				};

		string[] tags_dj = new string[] { "链式缩分器",
										"3mm一级圆盘缩分器",
										"干燥设备左边风扇运行信号",
										"干燥设备右边风扇运行信号",
										"弃料真空上料机",
										"筛分破碎",
										"3mm二级圆盘缩分器",
										"粉碎机",
										"湿煤破碎机",
										"对辊破碎机",
										"粉碎单元真空上料机"
										};

		string[] tags_other = new string[] { "3mm弃样暂存数", "3mm弃料一级皮带有煤标志", "3mm弃料二级皮带有煤标志", "左侧干燥机有煤标志", "右侧干燥机有煤标志", "3mm一级提升机料斗有煤标志", "3mm二级提升机料斗有煤标志" , "干燥机入料皮带有煤标志",
			"轴流风机1速度","轴流风机2速度"};

		string[] tags_al = new string[] { "AL_链式缩分器空开跳闸",
						"AL_6mm瓶装机行走伺服空开跳闸",
						"AL_原煤样提升机空开跳闸",
						"AL_原煤样输送皮带行走电机空开跳闸",
						"AL_原煤样输送皮带空开跳闸",
						"AL_湿煤破碎机空开跳闸",
						"AL_筛板振动电机空开跳闸",
						"AL_6mm缩分给料皮带空开跳闸",
						"AL_6mm缩分给料皮带整流电机空开跳闸",
						"AL_6mm转运皮带_分析样_空开跳闸",
						"AL_6mm转运皮带_全水样_空开跳闸",
						"AL_原煤弃料暂存皮带空开跳闸",
						"AL_6mm弃料真空上料机空开跳闸",
						"AL_对辊破碎出料皮带空开跳闸",
						"AL_在线测水灌装样皮带给料机空开跳闸",
						"AL_弃料风机空开跳闸",
						"AL_6mm瓶装机煤样瓶暂存电机空开跳闸",
						"AL_紧急停止",
						"AL_6mm空瓶仓1缺瓶信号",
						"AL_6mm空瓶仓2缺瓶信号",
						"AL_6mm空瓶仓3缺瓶信号",
						"AL_6mm缺盖信号",
						"AL_6mm气送准备好信号",
						"AL_3mm一级提升机空开跳闸",
						"AL_6mm输送皮带空开跳闸",
						"AL_6mm皮带整流空开跳闸",
						"AL_对辊破碎机空开跳闸",
						"AL_3mm输送皮带空开跳闸",
						"AL_分析样中间给料皮带整流空开跳闸",
						"AL_3mm弃料一级皮带空开跳闸",
						"AL_3mm弃料二级皮带空开跳闸",
						"AL_6mm煤样弃料旋转给料机空开跳闸",
						"AL_3mm筛分破碎机空开跳闸",
						"AL_3mm二级提升机空开跳闸",
						"AL_3mm螺旋给料机空开跳闸",
						"AL_3mm二级缩分出料皮带空开跳闸",
						"AL_0_2mm制粉机空开跳闸",
						"AL_粉碎入料螺旋给料机空开跳闸",
						"AL_3mm粉碎出料螺旋机空开跳闸",
						"AL_3mm空瓶仓1缺瓶信号",
						"AL_3mm空瓶仓2缺瓶信号",
						"AL_3mm空瓶仓3缺瓶信号",
						"AL_3mm缺盖信号",
						"AL_3mm气送准备好信号",
						"AL_3mm空瓶仓4缺瓶信号",
						"AL_3mm空瓶仓5缺瓶信号",
						"AL_3mm空瓶仓6缺瓶信号",
						"AL_上料提升机动作超时",
						"AL_上料皮带行走动作超时",
						"AL_链式缩分驱动器报警",
						"AL_上料皮带变频器报警",
						"AL_给料皮带变频器报警",
						"AL_6mm瓶装机行走驱动器报警",
						"AL_6mm抓手旋转驱动器报警",
						"AL_在线测水缩分驱动器报警",
						"AL_3mm一级提升机动作超时",
						"AL_分析样中间给料皮带变频器报警",
						"AL_3mm二级提升机动作超时",
						"AL_6mm提升机变频器报警",
						"AL_粉碎变频器报警",
						"AL_3mm瓶装机行走驱动器报警",
						"AL_3mm抓手旋转驱动器报警",
						"AL_圆盘缩分1驱动器报警",
						"AL_圆盘缩分2驱动器报警",
						"AL_上位机急停",
						"AL_左侧PTC加热器空开跳闸",
						"AL_右侧PTC加热器空开跳闸",
						"AL_干燥机布料皮带空开跳闸",
						"AL_干燥机布料皮带整流电机1空开跳闸",
						"AL_干燥机布料皮带整流电机2空开跳闸",
						"AL_3mm干燥出料皮带空开跳闸",
						"AL_干燥机行走机构空开跳闸",
						"AL_左侧轴流风机空开跳闸",
						"AL_右侧轴流风机空开跳闸",
						"AL_制粉入料螺旋给料机变频器报警",
						"AL_轴流风机2变频器报警",
						"AL_6mm制样部分电机报警",
						"AL_3mm缩分部分电机报警",
						"AL_干燥进料部分电机报警",
						"AL_干燥出料部分电机报警",
						"AL_粉碎制样部分电机报警",
						"AL_弃料部分电机报警",
						"AL_总故障报警",
						"AL_链式缩分回零超时",
						"AL_圆盘缩分1原位感应超时",
						"AL_干燥行走机构动作超时",
						"AL_圆盘缩分2原位感应超时",
						"AL_上料提升机动作欠时",
						"AL_上料皮带行走动作欠时",
						"AL_3mm一级提升机动作欠时",
						"AL_干燥行走机构动作欠时",
						"AL_3mm二级提升机动作欠时",
						"AL_3mm一级提升机称重行走电机动作超时",
						"AL_3mm一级提升机称重行走电机动作欠时",
						"AL_3mm二级提升机称重行走电机动作超时",
						"AL_3mm二级提升机称重行走电机动作欠时",
						"AL_0_2mm弃料气缸伸出动作超时",
						"AL_0_2mm弃料气缸缩回动作超时",
						"AL_粉碎机灌装压紧气缸动作超时",
						"AL_全水样灌装压紧气缸动作超时",
						"AL_3mm一级缩分器留样气缸动作超时",
						"AL_存查样灌装压紧气缸动作超时",
						"AL_机采伸缩皮带气缸动作超时",
						"AL_机采伸缩皮带气缸动作欠时",
						"AL_3mm料斗没有到位",
						"AL_在线测水缩分器伺服空开跳闸",
						"AL_机采进料皮带电机空开跳闸",
						"AL_在线测水皮带给料机空开跳闸",
						"AL_在线测水整流电机空开跳闸",
						"AL_机采进料直线行走电机空开跳闸",
						"AL_门禁系统电机空开跳闸",
						"AL_3mm一级提升机称重行走电机空开跳闸",
						"AL_弃料收集罐旋转给料机空开跳闸",
						"AL_6mm瓶装机气送模块升降电机空开跳闸",
						"AL_对辊破碎机给料皮带空开跳闸",
						"AL_3mm一级缩分器空开跳闸",
						"AL_3mm二级缩分器空开跳闸",
						"AL_3mm行走伺服空开跳闸",
						"AL_3mm瓶装机煤样瓶暂存电机空开跳闸",
						"AL_弃料单元紧急停止",
						"AL_3mm煤样旋转给料机空开跳闸",
						"AL_3mm二级提升机直线行走电机空开跳闸",
						"AL_3mm气送升降电机空开跳闸",
						"AL_粉碎单元备用电机3空开跳闸",
						"AL_粉碎单元备用电机4空开跳闸",
						"AL_分析样中间给料皮带空开跳闸",
						"AL_6mm瓶装机煤样瓶暂存电机堵转",
						"AL_对辊破碎出料皮带堵转",
						"AL_在线测水灌装样皮带给料机堵转",
						"AL_机采进料皮带堵转",
						"AL_原煤样输送皮带堵转",
						"AL_6mm缩分给料皮带堵转",
						"AL_6mm转运皮带_分析样_堵转",
						"AL_6mm转运皮带_全水样_堵转",
						"AL_对辊破碎机堵转",
						"AL_6mm煤样旋转给料机堵转",
						"AL_弃料收集罐旋转给料机堵转",
						"AL_原煤弃料暂存皮带堵转",
						"AL_在线测水皮带给料机堵转",
						"AL_3mm瓶装机煤样瓶暂存电机堵转",
						"AL_3mm弃料一级皮带堵转",
						"AL_3mm干燥出料皮带堵转",
						"AL_3mm螺旋给料机堵转",
						"AL_粉碎入料螺旋给料机堵转",
						"AL_3mm弃料二级皮带堵转",
						"AL_3mm煤样旋转给料机堵转",
						"AL_干燥机布料皮带堵转",
						"AL_3mm筛分破碎机堵转",
						"AL_3mm二级缩分出料皮带堵转",
						"AL_分析样中间给料皮带堵转",
						"AL_弃料单元旋转堵转",
						"AL_对辊破碎机给料皮带堵转",
						"AL_3mm制样部分电机报警",
						"AL_链式缩分器不动作报警",
						"AL_3mm一级圆盘缩分器不动作报警",
						"AL_3mm二级圆盘缩分器不动作报警",
						"AL_3mm瓶装机瓶码错误请弃瓶",
						"AL_6mm瓶装机缺瓶",
						"AL_6mm瓶装机缺盖",
						"AL_3mm瓶装机缺瓶",
						"AL_3mm瓶装机缺盖",
						"AL_粉碎单元正压值过低",
						"AL_主气路正压值过低",
						"AL_3mm一级提升机料斗分析样净重偏低",
						"AL_3mm二级提升机料斗干燥后留样净重异常",
						"AL_6mm瓶装机盖未旋好",
						"AL_6mm瓶装机瓶口缺盖",
						"AL_3mm瓶装机盖未旋好",
						"AL_3mm瓶装机瓶口缺盖",
						"AL_6mm瓶装机工位没瓶报警",
						"AL_3mm瓶装机工位没瓶报警",
						"AL_在线测水缩分器不动作报警",
						"AL_6mm弃料重量异常",
						"AL_3mm弃料重量异常",
						"AL_6mm瓶装机未抓到盖报警",
						"AL_3mm瓶装机未抓到盖报警"
						};

		string[] tags_dottedline = new string[] { "I_原煤样输送皮带变频器运行信号",
												"I_6mm缩分给料皮带变频器运行信号",
												"Q_6mm转运皮带_全水样_",
												"Q_6mm转运皮带_分析样_",
												"Q_对辊破碎出料皮带",
												"I_分析样中间给料皮带变频器运行信号",
												"I_3mm一级缩分器驱动器运行信号",
												"Q_3mm弃料一级皮带",
												"Q_3mm弃料二级皮带正转",
												"Q_干燥机布料皮带正转",
												"Q_干燥机布料皮带反转",
												"ST_干燥出料流程",
												"Q_3mm筛分破碎机正转",
												"Q_3mm二级缩分出料皮带反转",
												"Q_3mm二级缩分出料皮带正转",
												"ST_粉碎流程"
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
			for (int i = 0; i < tags.Length; i++)
			{
				lists.Add("全自动制样机.#1全自动制样机." + tags[i]);
			}
			for (int i = 0; i < tags_weight.Length; i++)
			{
				lists.Add("全自动制样机.#1全自动制样机." + tags_weight[i]);
			}
			for (int i = 0; i < tags_code.Length; i++)
			{
				lists.Add("全自动制样机.#1全自动制样机." + tags_code[i]);
			}
			for (int i = 0; i < tags_flow.Length; i++)
			{
				lists.Add("全自动制样机.#1全自动制样机." + tags_flow[i]);
			}
			for (int i = 0; i < tags_other.Length; i++)
			{
				lists.Add("全自动制样机.#1全自动制样机." + tags_other[i]);
			}
			for (int i = 0; i < tags_dj.Length; i++)
			{
				lists.Add("全自动制样机.#1全自动制样机." + tags_dj[i]);
			}
			for (int i = 0; i < tags_al.Length; i++)
			{
				lists.Add("全自动制样机.#1全自动制样机." + tags_al[i]);
			}
			for (int i = 0; i < tags_dottedline.Length; i++)
			{
				lists.Add("全自动制样机.#1全自动制样机." + tags_dottedline[i]);
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
			CmcsAppRemoteControlCmd appRemoteControlCmd = commonDAO.GetNewestAppRemoteControlCmd(GlobalVars.MachineCode_QZDZYJ_1);
			if (appRemoteControlCmd != null)
			{
				if (appRemoteControlCmd.CmdCode == "故障复位")
				{
					output("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param, eOutputType.Normal);
					Dictionary<string, object> cmd = new Dictionary<string, object>();
					cmd.Add("全自动制样机.#1全自动制样机." + "故障复位",  true );
					if (opcServere.WriteOPCItemValue(cmd))
					{
						// 更新执行结果
						commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
						commonDAO.SaveSysMessage(appRemoteControlCmd.AppIdentifier + "故障复位", appRemoteControlCmd.AppIdentifier + "故障复位执行成功");
					}
				}
			}
		}
	}
}
