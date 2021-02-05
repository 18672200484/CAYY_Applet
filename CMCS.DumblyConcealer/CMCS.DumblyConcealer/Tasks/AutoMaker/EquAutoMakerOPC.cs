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
		"6mm制样倒计时","在线测水连接状态","6mm瓶装机灌装口有瓶信号","3mm瓶装机灌装口有瓶信号3","3mm瓶装机灌装口有瓶信号1","3mm瓶装机灌装口有瓶信号2"};
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

		string[] tags_other = new string[] { "3mm弃样暂存数", "3mm弃料一级皮带有煤标志", "3mm弃料二级皮带有煤标志", "左侧干燥机有煤标志", "右侧干燥机有煤标志", "3mm一级提升机料斗有煤标志", "3mm二级提升机料斗有煤标志" , "干燥机入料皮带有煤标志",
			"轴流风机1速度","轴流风机2速度"};
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
			opcServere = new OPCClientDAO(lists, "Kepware.KEPServerEX.V6", "127.0.0.1");
			opcServere.InitOPC(output);
		}
	}
}
