using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Enums;
using CMCS.DumblyConcealer.Enums;
using OPCClientCore;
using CMCS.DapperDber.Util;
using CMCS.Common;

namespace CMCS.DumblyConcealer.Utilities
{
	public class OPCClientDAO
	{
		public OPCClientDAO(List<string> tags, string serverName, string serverIP)
		{
			Tags = tags;
			this.ServerName = serverName;
			this.ServerIP = serverIP;
		}
		Action<string, eOutputType> OutPut;
		/// <summary>
		/// 服务名
		/// </summary>
		string ServerName;
		/// <summary>
		/// 服务ip
		/// </summary>
		string ServerIP;

		CommonDAO commonDAO = CommonDAO.GetInstance();
		OPCClient opcClient = new OPCClient();
		/// <summary>
		/// 点位集合
		/// </summary>
		public List<string> Tags = new List<string>();

		public bool InitOPC(Action<string, eOutputType> output)
		{
			OutPut = output;
			try
			{
				opcClient.OnDataReveiced += new OPCClient.DataReveiced(opcClient_OnDataReveiced);
				opcClient.OnScanError += new OPCClient.ScanErrorEventHandler(opcClient_OnScanError);

				//连接OPC
				if (opcClient.Connect(this.ServerName, this.ServerIP))
				{
					//创建组
					opcClient.CreateGroup(Tags);

					//开始读数
					opcClient.Read();

					return true;
				}
				return false;
			}
			catch (Exception ex) { output(string.Format("OPC服务" + ServerName + "初始化异常", ex.Message), eOutputType.Error); }

			return false;
		}

		public void CloseOPC()
		{
			if (opcClient == null) return;

			opcClient.Close();
		}

		/// <summary>
		/// 写入点位信息
		/// </summary>
		/// <param name="dicSource">点位键值对集合 点位名 值</param>
		/// <returns></returns>
		public bool WriteOPCItemValue(Dictionary<string, object> dicSource)
		{
			if (dicSource.Count == 0) return false;

			if (!opcClient.Write(dicSource)) return false;

			return true;
		}

		void opcClient_OnDataReveiced(List<ItemResults> itemResults)
		{
			foreach (ItemResults item in itemResults)
			{
				if (item.ItemName.Contains("#1采样机"))
				{
					commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, item.ItemName.Replace("汽车机械采样机.#1采样机.", ""), item.Value != null ? item.Value.ToString().Replace("False", "0").Replace("True", "1") : "");
					OutPut(string.Format("写入实时信号{0}", item.ItemName), eOutputType.Normal);
				}
				else if (item.ItemName.Contains("#2采样机"))
				{
					commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, item.ItemName.Replace("汽车机械采样机.#2采样机.", ""), item.Value != null ? item.Value.ToString().Replace("False", "0").Replace("True", "1") : "");
					OutPut(string.Format("写入实时信号{0}", item.ItemName), eOutputType.Normal);
				}
			}
		}

		void opcClient_OnScanError(Exception ex)
		{
			throw ex;
		}
	}
}
