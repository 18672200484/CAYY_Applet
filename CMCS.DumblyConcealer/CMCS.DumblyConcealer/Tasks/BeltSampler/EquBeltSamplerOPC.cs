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
		"缩分间隔","封装机连接状态","封装机运行状态","#1翻车机","#2翻车机","主皮带","采样机报警","返料运输机"};
		string[] tags2 = new string[] { "封装机报警", "封装机连接状态", "封装机运行状态", "封装机准备好" };
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

			opcServere = new OPCClientDAO(lists, "Kepware.KEPServerEX.V6", "127.0.0.1");
			opcServere.InitOPC(output);
		}
	}
}
