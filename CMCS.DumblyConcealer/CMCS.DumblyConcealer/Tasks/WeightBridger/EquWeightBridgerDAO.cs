using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Enums;
using CMCS.DapperDber.Dbs.AccessDb;
using CMCS.DumblyConcealer.Enums;
using CMCS.Common.Entities.TrainInFactory;
using CMCS.DumblyConcealer.WeightBridger.Entities;
using CMCS.Common.Entities.Fuel;

namespace CMCS.DumblyConcealer.Tasks.WeightBridger
{
	public class EquWeightBridgerDAO
	{

		private static EquWeightBridgerDAO instance;

		public static EquWeightBridgerDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new EquWeightBridgerDAO();
			}
			return instance;
		}

		private EquWeightBridgerDAO()
		{

		}

		/// <summary>
		/// 同步翻车衡过衡数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SyncLwCarsInfo(Action<string, eOutputType> output)
		{
			int res = 0;

			IList<CarInfoMutual> carInfos = DcDbers.GetInstance().TurnCarWeighterMutualDber.Entities<CarInfoMutual>(" where DataFlag=1 and SuttleWeight>0 and CreateDate>=to_date('" + DateTime.Now.Date.AddDays(-2) + "','yyyy/mm/dd HH24:MI:SS')");
			foreach (var item in carInfos)
			{
				//同步到批次明细
				CmcsTransport transport = Dbers.GetInstance().SelfDber.Entity<CmcsTransport>(" where TransportNo=:TransportNo and InfactoryTime>=:InfactoryTime ", new { TransportNo = item.CarNumber, InfactoryTime = DateTime.Now.Date.AddDays(-2) });
				if (transport != null && !string.IsNullOrEmpty(transport.InFactoryBatchId))
				{
					if (item.GrossWeight != 0 && transport.GrossQty == 0)
						transport.GrossQty = (decimal)item.GrossWeight;

					if (item.TareWeight != 0 && transport.SkinQty == 0)
						transport.SkinQty = (decimal)item.TareWeight;

					if (item.SuttleWeight != 0 && transport.SuttleQty == 0)
					{
						transport.SuttleQty = (decimal)item.SuttleWeight;
						transport.MarginQty = (decimal)(item.SuttleWeight - item.TicketWeight);
					}
					transport.MeasureMan = "自动";
					transport.IsDeleted = item.CancelSign;
					transport.ArriveDate = item.WeightDate;
					transport.TareDate = item.WeightDate.AddMinutes(3).AddSeconds(1.2);
					transport.TrackCode = item.TurnCarNumber == "#1" ? "#4" : "#2";
					if (item.TurnCarNumber == "#1")
						CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.当前车号.ToString(), string.Empty);
					else
						CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.当前车号.ToString(), string.Empty);

					res += Dbers.GetInstance().SelfDber.Update(transport);

					//同步到轨道衡数据表
					CmcsTrainWeightRecord trainRecord = Dbers.GetInstance().SelfDber.Entity<CmcsTrainWeightRecord>("where TrainNumber=:TrainNumber and ArriveTime>=:ArriveTime", new { TrainNumber = item.CarNumber, ArriveTime = DateTime.Now.Date.AddDays(-2) });
					CmcsRCSampling sampling = Dbers.GetInstance().SelfDber.Entity<CmcsRCSampling>("where InFactoryBatchId=:InFactoryBatchId order by SamplingDate", new { InFactoryBatchId = transport.InFactoryBatchId });
					if (trainRecord != null)
					{
						trainRecord.TrainTipperMachineCode = item.TurnCarNumber;
						trainRecord.FuelKind = transport.TheBatch.FuelKindName;
						trainRecord.MineName = sampling != null ? sampling.SampleCode : "";
						trainRecord.SupplierName = "";
						trainRecord.StationName = transport.TheBatch.TheStation.Name;

						trainRecord.SerialNumber = item.RecordId;
						trainRecord.TicketWeight = (decimal)item.TicketWeight;
						trainRecord.GrossTime = item.WeightDate;
						trainRecord.GrossWeight = (decimal)item.GrossWeight;
						trainRecord.SkinTime = item.WeightDate.AddMinutes(3).AddSeconds(1.2);
						trainRecord.SkinWeight = (decimal)item.TareWeight;
						trainRecord.StandardWeight = (decimal)item.SuttleWeight;
						trainRecord.MarginWeight = trainRecord.StandardWeight - trainRecord.TicketWeight - trainRecord.DeductWeight;
						trainRecord.MesureMan = "自动";
						trainRecord.TrainTipperMachineCode = item.TurnCarNumber;
						trainRecord.MachineCode = item.TurnCarNumber == "#1" ? "#4" : "#2";
						trainRecord.IsTurnover = "已翻";
						trainRecord.UnloadTime = item.WeightDate;

						Dbers.GetInstance().SelfDber.Update(trainRecord);
					}
				}

				item.DataFlag = 2;
				DcDbers.GetInstance().TurnCarWeighterMutualDber.Update(item);
			}
			output(string.Format("同步翻车衡数据 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
			return res;
		}

	}
}
