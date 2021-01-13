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

namespace CMCS.DumblyConcealer.Tasks.PneumaticTransfer
{
	/// <summary>
	/// 气动传输OPC接口
	/// </summary>
	public class EquPneumaticTransferOPC
	{
		private static EquPneumaticTransferOPC instance;

		public static EquPneumaticTransferOPC GetInstance()
		{
			if (instance == null)
			{
				instance = new EquPneumaticTransferOPC();
			}
			return instance;
		}
		/// <summary>
		/// EquCarJXSamplerDAO
		/// </summary>
		public EquPneumaticTransferOPC()
		{
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		#region OPC变量
		static OPCClientDAO opcServere = null;
		string[] tags = new string[] { "换向器1.管道1检测", "换向器1.管道2检测", "换向器1.管道3检测", "换向器1.管道4检测", "换向器1.管道1定位",
		"换向器1.管道2定位","换向器1.管道3定位","换向器1.管道4定位","换向器2.管道1检测", "换向器2.管道2检测", "换向器2.管道3检测", "换向器2.管道4检测", "换向器2.管道1定位",
		"换向器2.管道2定位","换向器2.管道3定位","换向器2.管道4定位","风机.风机运行","风机.吹气位置","风机.吸气位置","存样柜收发站.准备就绪","存样柜收发站.样品到位","存样柜收发站.大瓶准备好","存样柜收发站.中瓶准备好",
		"存样柜收发站.小瓶准备好","大瓶发送站.准备就绪","大瓶发送站.物料检测","大瓶发送站.管道检测","小瓶发送站.准备就绪","小瓶发送站.物料检测","小瓶发送站.管道检测",
		"人工发送站.准备就绪","人工发送站.物料检测","弃样接收站.准备就绪","弃样接收站.样品到位检测","化验室接收站.准备就绪","化验室接收站.样品检测","化验室发送站.准备就绪","化验室发送站.管道检测",
		"化验室发送站.物料检测"};
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
				lists.Add("气动传输." + tags[i]);
			}

			opcServere = new OPCClientDAO(lists, "Kepware.KEPServerEX.V6", "127.0.0.1");
			opcServere.InitOPC(output);
		}
	}
}
