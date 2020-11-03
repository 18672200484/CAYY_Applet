using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.TrainInFactory;
using CMCS.Common.Enums;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.WeightBridger.Entities;

namespace CMCS.DumblyConcealer.Tasks.TrainDiscriminator
{
	/// <summary>
	/// 火车车号识别业务
	/// </summary>
	public class TrainDiscriminatorDAO
	{
		private static object LockObject = new object();

		private static TrainDiscriminatorDAO instance;

		private static String MachineCode_CHSB = GlobalVars.MachineCode_HCRCCHSB;

		public static TrainDiscriminatorDAO GetInstance()
		{
			if (instance == null)
			{

				instance = new TrainDiscriminatorDAO();
			}
			return instance;
		}

		private TrainDiscriminatorDAO()
		{

		}

		#region 车号识别数据处理

		/// <summary>
		/// 车辆数据处理
		/// </summary>
		/// <param name="carSpotsNum">车辆识别器编号</param>
		/// <returns></returns>
		public int CarSpotsHandle(Action<string, eOutputType> output, int carSpotsNum)
		{
			lock (LockObject)
			{
				int res = 0;
				IList<CmcsTrainCarriagePass> cardata = Dbers.GetInstance().SelfDber.Entities<CmcsTrainCarriagePass>(" where MachineCode='" + carSpotsNum + "' and DataFlag=0 and PassTime>=to_date('" + DateTime.Now.Date.AddDays(-1) + "','yyyy/mm/dd HH24:MI:SS') order by PassTime asc,OrderNum asc");
				bool flag = false;
				foreach (var item in cardata)
				{
					switch (carSpotsNum)
					{
						case 1:
							flag = CarSpot1Data(output, item);
							break;
						case 2:
							flag = CarSpot2Data(output, item);
							break;
						case 3:
							flag = CarSpot3Data(output, item);
							break;
						case 4:
							flag = CarSpot4Data(output, item.TrainNumber, item.PassTime, item.Direction, item.CarModel);
							break;
						case 5:
							flag = CarSpot5Data(output, item.TrainNumber, item.PassTime, item.Direction, item.CarModel);
							break;
					}

					if (flag)
					{
						item.DataFlag = 1;
						Dbers.GetInstance().SelfDber.Update(item);
						res++;
					}
				}
				return res;
			}
		}

		/// <summary>
		/// 1号车号识别
		/// </summary>
		/// <param name="carNumber">车号</param>
		/// <param name="carDate">时间</param>
		/// <param name="direction">方向</param>
		/// <param name="infactoryordernumber">顺序号</param>
		/// <param name="carmodel">车型</param>
		/// <returns></returns>
		public static bool CarSpot1Data(Action<string, eOutputType> output, CmcsTrainCarriagePass trainPass)
		{
			int res = 0;
			if (trainPass.Direction == "进厂")
			{
				CmcsTrainWeightRecord transportOver = Dbers.GetInstance().SelfDber.Entity<CmcsTrainWeightRecord>(" where TrainNumber='" + trainPass.TrainNumber + "' and IsTurnover='已翻' and ArriveTime>=to_date('" + DateTime.Now.Date.AddDays(-2) + "','yyyy/mm/dd HH24:MI:SS')");
				CmcsTrainWeightRecord transport = Dbers.GetInstance().SelfDber.Entity<CmcsTrainWeightRecord>(" where TrainNumber='" + trainPass.TrainNumber + "' and IsTurnover='未翻' and ArriveTime>=to_date('" + DateTime.Now.Date.AddDays(-2) + "','yyyy/mm/dd HH24:MI:SS')");
				if (transport == null)
				{
					// 此判断是过滤车辆出厂后立马进厂合并不同轨道车辆的情况，时间相差几分钟
					if (transportOver != null)
						return true;

					res += Dbers.GetInstance().SelfDber.Insert(new CmcsTrainWeightRecord()
					{
						PKID = trainPass.PKID,
						TrainNumber = trainPass.TrainNumber,
						ArriveTime = trainPass.PassTime,
						TrainType = trainPass.CarModel,
						IsTurnover = "未翻",
						MachineCode = trainPass.MachineCode,
						DataFlag = 0,
						TicketWeight = (decimal)CommonDAO.GetInstance().GetTrainRateLoadByTrainType(trainPass.CarModel),
						OrderNumber = trainPass.OrderNum,
						GrossTime = trainPass.PassTime,
						SkinTime = trainPass.PassTime,
						LeaveTime = trainPass.PassTime,
						UnloadTime = trainPass.PassTime,
					});
					return true;
				}
			}
			else if (trainPass.Direction == "出厂")
			{
				CmcsTrainWeightRecord trainRecord = Dbers.GetInstance().SelfDber.Entity<CmcsTrainWeightRecord>("where TrainNumber=:TrainNumber order by ArriveTime desc", new { TrainNumber = trainPass.TrainNumber });
				if (trainRecord != null)
				{
					trainRecord.LeaveTime = trainPass.PassTime;
					Dbers.GetInstance().SelfDber.Update(trainRecord);

					CmcsTransport transport = Dbers.GetInstance().SelfDber.Entity<CmcsTransport>("where PKID=:PKID order by InfactoryTime desc", new { PKID = trainRecord.PKID });
					if (transport != null)
					{
						transport.OutfactoryTime = trainPass.PassTime;
						Dbers.GetInstance().SelfDber.Update(transport);
					}
				}
				return true;
			}
			output(string.Format("向{0}翻车衡发送数据{1}条", trainPass.MachineCode, res), eOutputType.Normal);
			return false;
		}

		/// <summary>
		/// 2号车号识别
		/// </summary>
		/// <param name="carNumber">车号</param>
		/// <param name="carDate">时间</param>
		/// <param name="direction">方向</param>
		/// <param name="infactoryordernumber">顺序号</param>
		/// <param name="carmodel">车型</param>
		/// <returns></returns>
		public static bool CarSpot2Data(Action<string, eOutputType> output, CmcsTrainCarriagePass trainPass)
		{
			CmcsTransport transport = Dbers.GetInstance().SelfDber.Entity<CmcsTransport>("where TransportNo='" + trainPass.TrainNumber + "' and InfactoryTime>=to_date('" + DateTime.Now.Date.AddDays(-1) + "','yyyy/mm/dd HH24:MI:SS') order by InfactoryTime desc");
			if (transport == null) return false;

			if (trainPass.Direction == "进厂")
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.当前车号.ToString(), trainPass.TrainNumber);

				// 插入定位信息
				if (InsertTransportPosition("#2", transport.Id))
				{
					output(string.Format("插入一条定位信息;{0}车号识别 车号:{1}", trainPass.MachineCode, trainPass.TrainNumber), eOutputType.Normal);
					return true;
				}

				//if (transport.DispatchTime1.Year < 2000)
				//{
				//	transport.DispatchTime1 = carDate;
				//	transport.TrackNumber = "#2";
				//	transport.CarModel = carmodel;
				//	transport.OrderNumber = ordernumber;
				//	return Dbers.SelfOracleDber.Update(transport) > 0;
				//}
				//else if (transport.DispatchTime2.Year < 2000)
				//{
				//	transport.DispatchTime2 = carDate;
				//	transport.TrackNumber = "#2";
				//	transport.OrderNumber = ordernumber;
				//	return Dbers.SelfOracleDber.Update(transport) > 0;
				//}
				//else
				//{
				//	transport.DispatchTime2 = carDate;
				//	transport.TrackNumber = "#2";
				//	transport.OrderNumber = ordernumber;
				//	return Dbers.SelfOracleDber.Update(transport) > 0;
				//}
			}
			else if (trainPass.Direction == "出厂")
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.当前车号.ToString(), string.Empty);
				// 移除定位信息
				if (RemoveTransportPosition(transport.Id))
				{
					output(string.Format("移除一条定位信息;{0}车号识别 车号:{1}", trainPass.MachineCode, trainPass.TrainNumber), eOutputType.Normal);
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 3号车号识别
		/// </summary>
		/// <param name="carNumber">车号</param>
		/// <param name="carDate">时间</param>
		/// <param name="direction">方向</param>
		/// <param name="infactoryordernumber">顺序号</param>
		/// <param name="carmodel">车型</param>
		/// <returns></returns>
		public static bool CarSpot3Data(Action<string, eOutputType> output, CmcsTrainCarriagePass trainPass)
		{
			CmcsTransport transport = Dbers.GetInstance().SelfDber.Entity<CmcsTransport>("where TransportNo='" + trainPass.TrainNumber + "' and InfactoryTime>=to_date('" + DateTime.Now.Date.AddDays(-1) + "','yyyy/mm/dd HH24:MI:SS') order by InfactoryTime desc");
			if (transport == null) return false;

			if (trainPass.Direction == "进厂")
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.当前车号.ToString(), trainPass.TrainNumber);
				// 插入定位信息
				if (InsertTransportPosition("#4", transport.Id))
				{
					output(string.Format("插入一条定位信息;{0}车号识别 车号:{1}", trainPass.MachineCode, trainPass.TrainNumber), eOutputType.Normal);
					return true;
				}

				//if (transport.DispatchTime1.Year < 2000)
				//{
				//	transport.DispatchTime1 = carDate;
				//	transport.TrackNumber = "#4";
				//	transport.CarModel = carmodel;
				//	transport.OrderNumber = ordernumber;
				//	return Dbers.SelfOracleDber.Update(transport) > 0;
				//}
				//else if (transport.DispatchTime2.Year < 2000)
				//{
				//	transport.DispatchTime2 = carDate;
				//	transport.TrackNumber = "#4";
				//	transport.OrderNumber = ordernumber;
				//	return Dbers.SelfOracleDber.Update(transport) > 0;
				//}
				//else
				//{
				//	transport.DispatchTime2 = carDate;
				//	transport.TrackNumber = "#4";
				//	transport.OrderNumber = ordernumber;
				//	return Dbers.SelfOracleDber.Update(transport) > 0;
				//}
			}
			else if (trainPass.TrainNumber == "出厂")
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.当前车号.ToString(), string.Empty);
				if (RemoveTransportPosition(transport.Id))
				{
					output(string.Format("移除一条定位信息;{0}车号识别 车号:{1}", trainPass.MachineCode, trainPass.TrainNumber), eOutputType.Normal);
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 4号车号识别
		/// </summary>
		/// <param name="carNumber">车号</param>
		/// <param name="carDate">时间</param>
		/// <param name="direction">方向</param>
		/// <param name="carmodel">车型</param>
		/// <returns></returns>
		public static bool CarSpot4Data(Action<string, eOutputType> output, string carNumber, DateTime carDate, string direction, string carmodel = "")
		{
			CmcsTransport transport = Dbers.GetInstance().SelfDber.Entity<CmcsTransport>("where TransportNo='" + carNumber + "' and InfactoryTime>=to_date('" + DateTime.Now.Date.AddDays(-1) + "','yyyy/mm/dd HH24:MI:SS') order by InfactoryTime desc");
			if (transport == null) return false;

			if (direction == "进厂" && transport.InfactoryTime > DateTime.MinValue)
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.当前车号.ToString(), carNumber);

				//插入车辆信息至翻车衡交互数据库
				InsertCarToTurnCarWeighter("#1", transport);
				output(string.Format("向{0}插入一条数据", GlobalVars.MachineCode_TrunOver_1), eOutputType.Normal);
				return true;
			}
			else if (direction == "出厂")
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.当前车号.ToString(), string.Empty);

				return true;
			}
			return false;
		}

		/// <summary>
		/// 5号车号识别
		/// </summary>
		/// <param name="carNumber">车号</param>
		/// <param name="carDate">时间</param>
		/// <param name="direction">方向</param>
		/// <param name="carmodel">车型</param>
		/// <returns></returns>
		public static bool CarSpot5Data(Action<string, eOutputType> output, string carNumber, DateTime carDate, string direction, string carmodel = "")
		{
			CmcsTransport transport = Dbers.GetInstance().SelfDber.Entity<CmcsTransport>("where TransportNo='" + carNumber + "' and InfactoryTime>=to_date('" + DateTime.Now.Date.AddDays(-1) + "','yyyy/mm/dd HH24:MI:SS') order by InfactoryTime desc");
			if (transport == null) return false;

			if (direction == "进厂" && transport.InfactoryTime > DateTime.MinValue)
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.当前车号.ToString(), carNumber);

				//插入车辆信息至翻车衡交互数据库
				InsertCarToTurnCarWeighter("#2", transport);
				output(string.Format("向{0}插入一条数据", GlobalVars.MachineCode_TrunOver_2), eOutputType.Normal);
				return true;
			}
			else if (direction == "出厂")
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.当前车号.ToString(), string.Empty);

				return true;
			}
			return false;
		}
		#endregion


		/// <summary>
		/// 插入车辆信息至翻车衡交互数据库
		/// </summary>
		/// <param name="trunNumber"></param>
		/// <param name="transport"></param>
		public static void InsertCarToTurnCarWeighter(string trunNumber, CmcsTransport transport)
		{
			if (transport != null && !string.IsNullOrEmpty(transport.InFactoryBatchId))
			{
				CmcsRCSampling sampling = Dbers.GetInstance().SelfDber.Entity<CmcsRCSampling>("where InFactoryBatchId=:InFactoryBatchId order by SamplingDate desc", new { InFactoryBatchId = transport.InFactoryBatchId });
				if (sampling != null)
				{
					if (DcDbers.GetInstance().TurnCarWeighterMutualDber.Entity<CarInfoMutual>(" where  TurnCarNumber='" + trunNumber + "' and  CarNumber='" + transport.TransportNo + "' and DataFlag=0 ") == null)
					{
						DcDbers.GetInstance().TurnCarWeighterMutualDber.Execute(" update CarInfoMutual set DataFlag=1 where TurnCarNumber='" + trunNumber + "'");

						DcDbers.GetInstance().TurnCarWeighterMutualDber.Insert(new CarInfoMutual()
						{
							TurnCarNumber = trunNumber,
							CarNumber = transport.TransportNo,
							SampleBillNumber = sampling.SampleCode,
							InFactoryDate = transport.InfactoryTime,
							TicketWeight = (double)transport.TicketQty,
							CreateDate = System.DateTime.Now,
							WeightDate = DateTime.Now,
							DataFlag = 0,
							CancelSign = 0
						});
					}
				}
			}
		}

		/// <summary>
		/// 插入定位信息
		/// </summary>
		/// <param name="trackNumber"></param>
		/// <param name="transportid"></param>
		/// <returns></returns>
		public static bool InsertTransportPosition(string trackNumber, string transportId)
		{
			CmcsTransportPosition entity = Dbers.GetInstance().SelfDber.Entity<CmcsTransportPosition>("where TransportId='" + transportId + "'");
			if (entity != null) Dbers.GetInstance().SelfDber.Delete<CmcsTransportPosition>(entity.Id);

			return Dbers.GetInstance().SelfDber.Insert(new CmcsTransportPosition()
			{
				OrderNumber = GetTransportPositionMaxOrder(),
				TrackNumber = trackNumber,
				TransportId = transportId,
				IsDisCharged = 0
			}) > 0;
		}

		/// <summary>
		/// 移除轨道车辆定位信息
		/// </summary>
		/// <param name="transportId"></param>
		public static bool RemoveTransportPosition(string transportId)
		{
			CmcsTransportPosition entity = Dbers.GetInstance().SelfDber.Entity<CmcsTransportPosition>("where TransportId='" + transportId + "'");
			if (entity == null) return false;

			return Dbers.GetInstance().SelfDber.Delete<CmcsTransportPosition>(entity.Id) > 0;
		}


		/// <summary>
		/// 获取定位最大排序号
		/// </summary>
		/// <returns></returns>
		public static int GetTransportPositionMaxOrder()
		{
			CmcsTransportPosition entity = Dbers.GetInstance().SelfDber.Entity<CmcsTransportPosition>("order by OrderNumber desc");
			if (entity == null) return 1;

			return Convert.ToInt32(entity.OrderNumber) + 1;
		}

	}
}
