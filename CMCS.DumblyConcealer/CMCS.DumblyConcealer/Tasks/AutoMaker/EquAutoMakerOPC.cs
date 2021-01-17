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
		"6mm制样倒计时","在线测水连接状态"};
		string[] tags_weight = new string[] { "弃料称实时重量", "6mm瓶装机称重量", "3mm瓶装机实时重量", "3mm分析样称实时重量", "3mm干燥样称实时重量",
		"原煤称实时重量","原煤称实时重量",};
		string[] tags_code = new string[] {"3mm瓶装机煤样编码","6mm煤样编码","3mm煤样编码","3mm一级提升机料斗煤样编码","3mm弃料一级皮带煤样编码","3mm弃料二级皮带煤样编码",
		"干燥煤样编码","干燥箱1煤样编码","干燥箱2煤样编码","原煤煤样编码","3mm二级提升机料斗煤样编码","粉碎煤样编码","3mm弃料桶煤样编码",
		"3mm煤样弃料旋转给料机煤样编码","弃料单元煤样编码","6mm瓶装机煤样编码", };
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

			opcServere = new OPCClientDAO(lists, "Kepware.KEPServerEX.V6", "127.0.0.1");
			opcServere.InitOPC(output);
		}
	}
}
