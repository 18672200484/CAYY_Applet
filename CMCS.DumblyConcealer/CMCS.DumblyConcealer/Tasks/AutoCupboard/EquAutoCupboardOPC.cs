using CMCS.Common.DAO;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard
{
	/// <summary>
	/// 存样柜OPC接口
	/// </summary>
	public class EquAutoCupboardOPC
    {
		private static EquAutoCupboardOPC instance;

		public static EquAutoCupboardOPC GetInstance()
		{
			if (instance == null)
			{
				instance = new EquAutoCupboardOPC();
			}
			return instance;
		}
		/// <summary>
		/// EquAutoCupboardOPC
		/// </summary>
		public EquAutoCupboardOPC()
		{
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		#region OPC变量
		static OPCClientDAO opcServere = null;
		string[] tags = new string[] { "人工存瓶信号", "人工取瓶信号", "气送存瓶信号", "气送取瓶信号", "气送系统信号","行走伺服",
		"升降伺服","滑动模组","旋转伺服","电爪状态开到位","电爪状态关到位","气送阀板开到位","气送阀板关到位"};

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
				lists.Add("全自动存样柜.#1全自动存样柜." + tags[i]);
			}
		
			opcServere = new OPCClientDAO(lists, "Kepware.KEPServerEX.V6", "127.0.0.1");
			opcServere.InitOPC(output);
		}
	}
}
