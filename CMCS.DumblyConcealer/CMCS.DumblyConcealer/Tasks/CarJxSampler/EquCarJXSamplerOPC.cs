using System;
using System.Collections.Generic;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.CarJXSampler.Entities;
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
		OPCGroups groups;
		OPCGroup group;
		OPCItems items;
		OPCItem item;

		Array strItemIDs;
		Array lClientHandles;
		Array lserverhandles;
		Array lErrors;
		object RequestedDataTypes = null;
		object AccessPaths = null;
		Array lErrors_Wt;
		int lTransID_Wt = 2;
		int lCancelID_Wt;

		string[] tags = new string[] { "运行状态", "当前采样点数", "当前桶号", "当前X坐标", "当前Y坐标", "当前Z坐标", "大车故障", "小车故障", "升降故障",
			"动力头故障","匀料器故障","给料皮带故障","环锤故障","反击板故障","缩分故障","样料故障","弃料故障","桶故障","道闸故障","大车运行","小车运行","升降运行",
		"动力头运行","匀料器运行","给料皮带运行","环锤运行","反击板运行","缩分运行","样料运行","弃料运行","样煤仓运行","道闸运行","前边界","后边界","左边界","右边界",
		"上边界","下边界","道闸抬到位","道闸落到位","采样自动","采样急停","制样自动","制样急停","远程/就地","启动/停止","急停/复位","远程手动/自动"
			};
		string[] tmpIDs = new string[10];
		#endregion
		Action<string, eOutputType> OutPut;
		/// <summary>
		/// 设备编码
		/// </summary>
		string MachineCode;

		/// <summary>
		/// 同步OPC点位信息
		/// </summary>
		/// <param name="output"></param> 
		/// <returns></returns>
		public void SyncOPCTags(Action<string, eOutputType> output)
		{
			OutPut = output;
			int res = 0;
			OPCServer server = new OPCServer();
			try
			{
				server.Connect("Kepware.KEPServerEX.V6", "localhost");
				output("OPC连接成功", eOutputType.Normal);
			}
			catch (Exception ex)
			{
				output(ex.Message, eOutputType.Error);
			}
			groups = server.OPCGroups;  //拿到组jih
			groups.DefaultGroupIsActive = true; //设置组集合默认为激活状态
			groups.DefaultGroupDeadband = 0;    //设置死区
			groups.DefaultGroupUpdateRate = 200;//设置更新频率

			group = server.OPCGroups.Add("OPCDOTNETGROUP");
			group.IsSubscribed = true; //是否为订阅
			group.UpdateRate = 200;    //刷新频率
			group.DataChange += MyOpcGroup_DataChange; ; //组内数据变化的回调函数
			group.AsyncReadComplete += MyOpcGroup_AsyncReadComplete; //异步读取完成回调
			group.AsyncWriteComplete += MyOpcGroup_AsyncWriteComplete; //异步写入完成回调
			group.AsyncCancelComplete += MyOpcGroup_AsyncCancelComplete;//异步取消读取、写入回调
																		//设备号
			int[] tmpCHandles = new int[tags.Length * 2];
			//for (int i = 0; i < tmpCHandles.Length; i++)
			//{
			//	tmpCHandles[i] = i;
			//}
			//tmpIDs[1] = "汽车机械采样机.#1采样机._System._DemandPoll";
			//tmpIDs[2] = "汽车机械采样机.#1采样机._System._AutoDemoted";
			tmpIDs = new string[tags.Length * 2 + 1];
			for (int i = 1; i <= tags.Length; i++)
			{
				tmpCHandles[i] = i;
				tmpIDs[i + 1] = "汽车机械采样机.#1采样机." + tags[i - 1];
			}
			for (int i = 1; i <= tags.Length; i++)
			{
				tmpCHandles[tags.Length + i] = tags.Length + i;
				tmpIDs[tags.Length + i + 1] = "汽车机械采样机.#2采样机." + tags[i - 1];
			}

			strItemIDs = (Array)tmpIDs;//必须转成Array型，否则不能调用AddItems方法
			lClientHandles = (Array)tmpCHandles;
			// 添加opc标签
			group.OPCItems.AddItems(tmpIDs.Length, ref strItemIDs, ref lClientHandles, out lserverhandles, out lErrors, RequestedDataTypes, AccessPaths);

		}

		void MyOpcGroup_AsyncCancelComplete(int CancelID)
		{
			throw new NotImplementedException();
		}

		void MyOpcGroup_AsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
		{
			throw new NotImplementedException();
		}

		void MyOpcGroup_AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
		{
			//throw new NotImplementedException();
		}

		//datachange事件
		void MyOpcGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
		{
			try
			{
				int res = 0;
				for (int i = 1; i < NumItems + 1; i++)
				{
					string ss = ClientHandles.GetValue(i).ToString() + "-" + ItemValues.GetValue(i).ToString();
					string tag = tmpIDs[(int)ClientHandles.GetValue(i)];
					if (tag.Contains("#1"))
					{
						if (commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, tag.Replace("汽车机械采样机.#1采样机.", ""), ItemValues.GetValue(i).ToString()))
							res++;
					}
					else if (tag.Contains("#2"))
					{
						if (commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, tag.Replace("汽车机械采样机.#2采样机.", ""), ItemValues.GetValue(i).ToString()))
							res++;
					}
				}
				OutPut(string.Format("同步集样罐记录 {0} 条", res), eOutputType.Normal);
			}
			catch (Exception error)
			{
				OutPut("Result--同步读" + error.Message, eOutputType.Error);
			}
		}

	}
}
