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
	/// 皮带采样机OPC接口
	/// </summary>
	public class EquBeltSamplerOPC
	{
		private static EquBeltSamplerOPC instance;

		public static EquBeltSamplerOPC GetInstance()
		{
			if (instance == null)
			{
				instance = new EquBeltSamplerOPC();
			}
			return instance;
		}
		/// <summary>
		/// EquCarJXSamplerDAO
		/// </summary>
		public EquBeltSamplerOPC()
		{
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		#region OPC变量
		static OPCClientDAO opcServere = null;
		string[] tags = new string[] { "采样头", "初级给料皮带正转", "初级给料皮带反转", "初级给料皮带清扫", "初级破碎机",
		"次级给料皮带","次级给料皮带清扫","破碎清扫电机","弃料提升斗", "输煤皮带","缩分器","远程自动","远程手动","就地自动","就地手动",
		"缩分间隔","#1翻车机","#2翻车机","主皮带","采样机报警","返料运输机","返料皮带","禁止1号翻车机翻车","禁止2号翻车机翻车",
		"皮带秤","下次采样时间","系统故障","工位切换","已翻车车数"};
		string[] tags2 = new string[] { "封装机报警", "封装机连接状态", "封装机运行状态", "封装机准备好", "封装机装样次数", "封装机装样重量" };

		string[] tags3 = new string[] { "M_采样头空开跳闸","M_给料皮带空开跳闸","M_破碎电机空开跳闸","M_破碎清扫电机空开跳闸","M_相序报警","M_缩分皮带空开跳闸","M_缩分电机空开跳闸",
										"M_螺旋输送机空开跳闸","M_斗提机空开跳闸","M_弃料仓门电机空开跳闸","M_转运皮带空开跳闸","M_转运出料斗空开跳闸","M_给料皮带堵转报警","M_破碎电机堵转报警","M_缩分皮带堵转报警",
										"M_斗提机堵转报警","M_转运皮带堵转报警","M_急停报警","M_采样头重故障报警","M_采样头紧急停止","M_控制柜急停","M_远程急停","M_上位机急停","M_制样部分报警","M_采样部分报警","M_采样机总报警",
										"M_采样头接近开关报警","M_缩分器接近开关报警","M_弃料皮带空开跳闸","M_电动三通开超时报警","M_电动三通关超时报警","M_弃料皮带堵转报警","M_采样头制动器空开跳闸","M_仓门推杆电机空开跳闸",
										"M_桶满报警","M_选通失败报警","M_封装机总报警","M_返料输送机故障","M_初级清扫故障","M_次级清扫故障","M_次级给料机变频故障","M_初级给料机变频故障"
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
				lists.Add("皮带采样机.2PA皮带采样机." + tags[i]);
				lists.Add("皮带采样机.2PB皮带采样机." + tags[i]);
			}
			for (int i = 0; i < tags2.Length; i++)
			{
				lists.Add("皮带采样机.2PA封装机." + tags2[i]);
				lists.Add("皮带采样机.2PB封装机." + tags2[i]);
			}
			for (int i = 0; i < tags3.Length; i++)
			{
				lists.Add("皮带采样机.2PA皮带采样机." + tags3[i]);
				lists.Add("皮带采样机.2PB皮带采样机." + tags3[i]);
			}

			opcServere = new OPCClientDAO(lists, "Kepware.KEPServerEX.V6", "127.0.0.1");
			opcServere.InitOPC(output);
		}
	}
}
