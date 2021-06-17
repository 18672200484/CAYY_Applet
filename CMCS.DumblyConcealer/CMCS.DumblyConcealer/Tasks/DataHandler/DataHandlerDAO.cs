using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common;
using CMCS.Common.Entities.Sys;
using CMCS.DumblyConcealer.Tasks.CarSynchronous.Enums;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.DumblyConcealer.Enums;
using CMCS.Common.DAO;
using CMCS.DapperDber.Dbs.AccessDb;
using System.Data;
using CMCS.DumblyConcealer.Tasks.DataHandler.Entities;
using CMCS.Common.Entities.TrainInFactory;
using CMCS.Common.Enums;

namespace CMCS.DumblyConcealer.Tasks.CarSynchronous
{
	/// <summary>
	/// 综合事件处理
	/// </summary>
	public class DataHandlerDAO
	{
		private static DataHandlerDAO instance;

		public static DataHandlerDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new DataHandlerDAO();
			}
			return instance;
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		private DataHandlerDAO()
		{ }

		/// <summary>
		/// 将汽车入厂煤运输记录同步到批次明细中
		/// </summary>
		/// <param name="transportId">汽车入厂煤运输记录Id</param>
		/// <returns></returns>
		public void SyncToBatch(Action<string, eOutputType> output)
		{
			int res = 0;
			bool succes = false;

			//已完结的有效数据
			foreach (CmcsBuyFuelTransport transport in commonDAO.SelfDber.Entities<CmcsBuyFuelTransport>("where IsUse=1 and IsFinish=1 and IsSyncBatch=0 "))
			{
				if (transport.TareTime == null) continue;

				if (transport.TareTime.Year < 2000) continue;

				string oldBatchId = transport.InFactoryBatchId;

				// 生成批次以及采制化三级编码数据
				CmcsInFactoryBatch batch = commonDAO.GCQCInFactoryBatchByBuyFuelTransport(transport);
				if (batch == null) continue;

				CmcsTransport truck = commonDAO.SelfDber.Entity<CmcsTransport>("where PKID=:PKID and IsDeleted=0", new { PKID = transport.Id });
				if (truck != null)
				{
					truck.TransportNo = transport.CarNumber;
					truck.LastModificAtionTime = transport.LastModificAtionTime;
					truck.InfactoryTime = transport.InFactoryTime;
					truck.ArriveDate = transport.GrossTime;
					truck.TareDate = transport.TareTime;
					truck.OutfactoryTime = transport.OutFactoryTime;
					truck.TicketQty = transport.TicketWeight;
					truck.GrossQty = transport.GrossWeight;
					truck.SkinQty = transport.TareWeight;
					truck.SuttleQty = transport.SuttleWeight;
					truck.KgQty = transport.DeductWeight;
					truck.CheckQty = transport.SuttleWeight - transport.DeductWeight;
					truck.MarginQty = transport.SuttleWeight - transport.DeductWeight - transport.TicketWeight;
					truck.InFactoryBatchId = batch.Id;
					truck.PKID = transport.Id;
					truck.DataFrom = "汽车智能化";
					succes = commonDAO.SelfDber.Update(truck) > 0;
				}
				else
				{
					truck = new CmcsTransport()
					{
						TransportNo = transport.CarNumber,
						LastModificAtionTime = transport.LastModificAtionTime,
						InfactoryTime = transport.CreationTime,
						ArriveDate = transport.GrossTime,
						TareDate = transport.TareTime,
						OutfactoryTime = transport.OutFactoryTime,
						TicketQty = transport.TicketWeight,
						GrossQty = transport.GrossWeight,
						SkinQty = transport.TareWeight,
						SuttleQty = transport.SuttleWeight,
						KgQty = transport.DeductWeight,
						CheckQty = transport.SuttleWeight - transport.DeductWeight,
						MarginQty = transport.SuttleWeight - transport.DeductWeight - transport.TicketWeight,
						InFactoryBatchId = batch.Id,
						PKID = transport.Id,
						DataFrom = "汽车智能化"
					};

					succes = commonDAO.SelfDber.Insert(truck) > 0;
				}

				if (succes)
				{
					succes = UpdateInFactoryBatch(batch); //更新新批次
					if (succes)
					{
						if (oldBatchId != batch.Id)
						{
							CmcsInFactoryBatch oldInFactoryBatch = Dbers.GetInstance().SelfDber.Get<CmcsInFactoryBatch>(oldBatchId);
							UpdateInFactoryBatch(oldInFactoryBatch); //更新旧批次
						}

						//更新智能化运输记录
						transport.IsSyncBatch = 1;
						transport.InFactoryBatchId = batch.Id;
						Dbers.GetInstance().SelfDber.Update<CmcsBuyFuelTransport>(transport);

						res++;
					}
				}

			}

			output(string.Format("同步批次明细数据 {0} 条", res), eOutputType.Normal);
		}

		private bool UpdateInFactoryBatch(CmcsInFactoryBatch batch)
		{
			// 更新批次的量 
			List<CmcsTransport> listTransport = commonDAO.SelfDber.Entities<CmcsTransport>("where InFactoryBatchId=:InFactoryBatchId and IsDeleted=0", new { InFactoryBatchId = batch.Id });

			batch.SuttleQty = listTransport.Sum(a => a.SuttleQty);
			batch.TicketQty = listTransport.Sum(a => a.TicketQty);
			batch.CheckQty = listTransport.Sum(a => a.CheckQty);
			batch.MarginQty = listTransport.Sum(a => a.MarginQty);
			batch.TransportNumber = listTransport.Count;

			return Dbers.GetInstance().SelfDber.Update<CmcsInFactoryBatch>(batch) > 0; ;
		}

		#region 同步门禁数据

		/// <summary>
		/// 同步门禁数据
		/// </summary>
		/// <param name="output"></param>
		public void SyncDoorData(Action<string, eOutputType> output, AccessDapperDber doorDapperDber)
		{
			int res = 0;

			string sql = " select * from acc_monitor_log where time>#2018-08-08#";
			sql += " order by time";
			DataTable dtNum = doorDapperDber.ExecuteDataTable(sql);

			if (dtNum.Rows.Count > 0)
			{
				for (int i = 0; i < dtNum.Rows.Count; i++)
				{
					string id = dtNum.Rows[i]["id"].ToString();
					string userId = dtNum.Rows[i]["pin"].ToString();
					string deviceId = dtNum.Rows[i]["device_id"].ToString();

					string consumerName = GetConsumer(userId, doorDapperDber);
					string doorName = GetMachine(deviceId, doorDapperDber);

					if (string.IsNullOrEmpty(consumerName)) continue;
					if (string.IsNullOrEmpty(doorName)) continue;

					CmcsGuardInfo entity = Dbers.GetInstance().SelfDber.Entity<CmcsGuardInfo>("where NId=:NId", new { NId = id });
					if (entity == null)
					{
						entity = new CmcsGuardInfo()
						{
							DataFrom = "智能化",
							F_ConsumerId = userId,
							F_ConsumerName = consumerName,
							F_InOut = "1",
							F_ReaderId = deviceId,
							F_ReaderName = doorName,
							NId = id,
							F_ReadDate = DateTime.Parse(dtNum.Rows[i]["time"].ToString())
						};

						res += Dbers.GetInstance().SelfDber.Insert(entity);
					}
				}
			}
			output(string.Format("同步门禁数据{0}条", res), eOutputType.Normal);
		}

		//根据用户id得到用户名
		private string GetConsumer(string UserId, AccessDapperDber doorDapperDber)
		{
			string sql = " select name from userinfo where Badgenumber='" + UserId + "'";
			DataTable dt = doorDapperDber.ExecuteDataTable(sql);
			if (dt != null && dt.Rows.Count > 0)
			{
				return dt.Rows[0][0].ToString();
			}
			return "";
		}

		private string GetMachine(string MachineId, AccessDapperDber doorDapperDber)
		{
			string sql = " select MachineAlias from Machines where id=" + MachineId + "";
			DataTable dt = doorDapperDber.ExecuteDataTable(sql);
			if (dt != null && dt.Rows.Count > 0)
			{
				return dt.Rows[0][0].ToString();
			}
			return "";
		}

		#endregion

		/// <summary>
		/// 处理集控首页信号信息
		/// </summary>
		/// <param name="output"></param>
		public void SyncHomePageSignalData(Action<string, eOutputType> output)
		{
			int count = 0, count1 = 0, count2 = 0;
			count = commonDAO.SelfDber.Count<CmcsTrainCarriagePass>("where Direction='进厂' and（MachineCode='2' or MachineCode='3') and trunc(PassTime)=trunc(sysdate)");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.火车入厂车数.ToString(), count.ToString());

			count = commonDAO.SelfDber.Count<CmcsTrainCarriagePass>("where Direction='出厂' and（MachineCode='2' or MachineCode='3') and trunc(PassTime)=trunc(sysdate)");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.火车出厂车数.ToString(), count.ToString());

			count1 = commonDAO.SelfDber.Count<CmcsTransport>("where TrackCode='#2' and GrossQty>0 and trunc(InFactoryTime)=trunc(sysdate)");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.已翻车数.ToString(), count1.ToString());

			count2 = commonDAO.SelfDber.Count<CmcsTransport>("where TrackCode='#4' and GrossQty>0 and trunc(InFactoryTime)=trunc(sysdate)");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.已翻车数.ToString(), count2.ToString());

			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.火车翻车车数.ToString(), (count1 + count2).ToString());

			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.来船量.ToString(), "无");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车转运车数.ToString(), "无");

			count = commonDAO.SelfDber.Count<CmcsBuyFuelTransport>("where trunc(InFactoryTime)=trunc(sysdate)");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车入厂车数.ToString(), count.ToString());

			count = commonDAO.SelfDber.Count<CmcsBuyFuelTransport>("where trunc(InFactoryTime)=trunc(sysdate) and GrossWeight>0");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车采样称重车数.ToString(), count.ToString());

			count = commonDAO.SelfDber.Count<CmcsBuyFuelTransport>("where trunc(InFactoryTime)=trunc(sysdate) and TareWeight>0");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车回皮车数.ToString(), count.ToString());
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.汽车出厂车数.ToString(), count.ToString());

			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.制样合批数.ToString(), "无");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.存样数.ToString(), "无");

			count = commonDAO.SelfDber.Count<CmcsRCAssay>("where trunc(CreationTime)=trunc(sysdate) and AssayPle is null");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.待化验数.ToString(), count.ToString());

			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_PDCYJ_1, "PLC连接状态", commonDAO.TestPing(commonDAO.GetCommonAppletConfigString("2PA皮带采样机PLCIP")) ? "1" : "0");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_PDCYJ_2, "PLC连接状态", commonDAO.TestPing(commonDAO.GetCommonAppletConfigString("2PB皮带采样机PLCIP")) ? "1" : "0");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HCRCCHSB1, "连接状态", commonDAO.TestPing(commonDAO.GetCommonAppletConfigString("#1车号识别IP")) ? "1" : "0");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HCRCCHSB2, "连接状态", commonDAO.TestPing(commonDAO.GetCommonAppletConfigString("#2车号识别IP")) ? "1" : "0");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HCRCCHSB3, "连接状态", commonDAO.TestPing(commonDAO.GetCommonAppletConfigString("#3车号识别IP")) ? "1" : "0");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HCRCCHSB4, "连接状态", commonDAO.TestPing(commonDAO.GetCommonAppletConfigString("#4车号识别IP")) ? "1" : "0");
			commonDAO.SetSignalDataValue(GlobalVars.MachineCode_HCRCCHSB5, "连接状态", commonDAO.TestPing(commonDAO.GetCommonAppletConfigString("#5车号识别IP")) ? "1" : "0");

		}

	}
}
