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
				//IList<CmcsTrainCross> cardata = Dbers.GetInstance().SelfDber.Entities<CmcsTrainCross>(" where MachineCode='" + carSpotsNum + "' and DataFlag=0 and PassTime>=to_date('" + DateTime.Now.Date.AddDays(-1) + "','yyyy/mm/dd HH24:MI:SS') order by PassTime asc,OrderNum asc");
				IList<CmcsTrainRecognition> cardata = Dbers.GetInstance().SelfDber.Entities<CmcsTrainRecognition>(" where MachineCode='" + carSpotsNum + "' and DataFlag=0 and CrossTime>=to_date('" + DateTime.Now.Date.AddDays(-1) + "','yyyy/mm/dd HH24:MI:SS') order by CrossTime asc,OrderNum asc");
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
							flag = CarSpot4Data(output, item);
							break;
						case 5:
							flag = CarSpot5Data(output, item);
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
		public static bool CarSpot1Data(Action<string, eOutputType> output, CmcsTrainRecognition trainPass)
		{
			int res = 0;
			if (trainPass.Direction == "进厂")
			{
				CmcsTrainWeightRecord transportOver = Dbers.GetInstance().SelfDber.Entity<CmcsTrainWeightRecord>(" where TrainNumber='" + trainPass.CarNumber + "' and IsTurnover='已翻' and ArriveTime>=to_date('" + DateTime.Now.Date.AddDays(-2) + "','yyyy/mm/dd HH24:MI:SS')");
				CmcsTrainWeightRecord transport = Dbers.GetInstance().SelfDber.Entity<CmcsTrainWeightRecord>(" where TrainNumber='" + trainPass.CarNumber + "' and IsTurnover='未翻' and ArriveTime>=to_date('" + DateTime.Now.Date.AddDays(-2) + "','yyyy/mm/dd HH24:MI:SS')");
				if (transport == null)
				{
					// 此判断是过滤车辆出厂后立马进厂合并不同轨道车辆的情况，时间相差几分钟
					if (transportOver != null)
						return true;

					res += Dbers.GetInstance().SelfDber.Insert(new CmcsTrainWeightRecord()
					{
						PKID = trainPass.Id,
						TrainNumber = trainPass.CarNumber,
						ArriveTime = trainPass.CrossTime,
						TrainType = trainPass.CarModel,
						IsTurnover = "未翻",
						MachineCode = trainPass.MachineCode,
						DataFlag = 0,
						TicketWeight = (decimal)CommonDAO.GetInstance().GetTrainRateLoadByTrainType(trainPass.CarModel),
						OrderNumber = trainPass.OrderNum,
						GrossTime = trainPass.CrossTime,
						SkinTime = trainPass.CrossTime,
						LeaveTime = trainPass.CrossTime,
						UnloadTime = trainPass.CrossTime,
					});
					return true;
				}
			}
			else if (trainPass.Direction == "出厂")
			{
				CmcsTrainWeightRecord trainRecord = Dbers.GetInstance().SelfDber.Entity<CmcsTrainWeightRecord>("where TrainNumber=:TrainNumber order by ArriveTime desc", new { TrainNumber = trainPass.CarNumber });
				if (trainRecord != null)
				{
					trainRecord.LeaveTime = trainPass.CrossTime;
					Dbers.GetInstance().SelfDber.Update(trainRecord);

					CmcsTransport transport = Dbers.GetInstance().SelfDber.Entity<CmcsTransport>("where PKID=:PKID order by InfactoryTime desc", new { PKID = trainPass.Id });
					if (transport != null)
					{
						transport.OutfactoryTime = trainPass.CrossTime;
						Dbers.GetInstance().SelfDber.Update(transport);
					}
				}
				//移除定位信息
				RemoveTransportPosition(trainPass.CarNumber);
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
		public static bool CarSpot2Data(Action<string, eOutputType> output, CmcsTrainRecognition trainPass)
		{
			if (trainPass == null) return false;

			if (trainPass.Direction == "进厂")
			{
				CmcsTrainCarriagePass transport = Dbers.GetInstance().SelfDber.Entity<CmcsTrainCarriagePass>("where TrainNumber='" + trainPass.CarNumber + "' and PassTime>=to_date('" + DateTime.Now.Date.AddDays(-1) + "','yyyy/mm/dd HH24:MI:SS') order by PassTime desc");
				if (transport == null)
				{
					transport = new CmcsTrainCarriagePass();
					transport.TrainNumber = trainPass.CarNumber;
					transport.CarModel = trainPass.CarModel;
					transport.MachineCode = trainPass.MachineCode;
					transport.PassTime = trainPass.CrossTime;
					transport.Direction = trainPass.Direction;
					transport.OrderNum = trainPass.OrderNum;
					transport.DataFlag = 0;
					transport.PKID = trainPass.Id;
					Dbers.GetInstance().SelfDber.Insert(transport);
				}

				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.当前车号.ToString(), trainPass.CarNumber);

				// 插入定位信息
				if (InsertTransportPosition("#2", trainPass.CarNumber))
				{
					output(string.Format("#2轨道插入定位信息;{0}车号识别 车号:{1}", trainPass.MachineCode, trainPass.CarNumber), eOutputType.Normal);
					return true;
				}

			}
			else if (trainPass.Direction == "出厂")
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.当前车号.ToString(), string.Empty);
				// 移除定位信息
				if (RemoveTransportPosition(trainPass.CarNumber))
				{
					output(string.Format("#2轨道移除定位信息;{0}车号识别 车号:{1}", trainPass.MachineCode, trainPass.CarNumber), eOutputType.Normal);
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
		public static bool CarSpot3Data(Action<string, eOutputType> output, CmcsTrainRecognition trainPass)
		{
			if (trainPass == null) return false;

			if (trainPass.Direction == "进厂")
			{
				CmcsTrainCarriagePass transport = Dbers.GetInstance().SelfDber.Entity<CmcsTrainCarriagePass>("where TrainNumber=:TrainNumber and PassTime>=:PassTime order by PassTime desc", new { TrainNumber = trainPass.CarNumber, PassTime = DateTime.Now.AddDays(-1) });
				if (transport == null)
				{
					transport = new CmcsTrainCarriagePass();
					transport.TrainNumber = trainPass.CarNumber;
					transport.CarModel = trainPass.CarModel;
					transport.MachineCode = trainPass.MachineCode;
					transport.PassTime = trainPass.CrossTime;
					transport.Direction = trainPass.Direction;
					transport.OrderNum = trainPass.OrderNum;
					transport.DataFlag = 0;
					transport.PKID = trainPass.Id;
					Dbers.GetInstance().SelfDber.Insert(transport);
				}
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.当前车号.ToString(), trainPass.CarNumber);
				// 插入定位信息
				if (InsertTransportPosition("#4", trainPass.CarNumber))
				{
					output(string.Format("#4轨道插入定位信息;{0}车号识别 车号:{1}", trainPass.MachineCode, trainPass.CarNumber), eOutputType.Normal);
					return true;
				}

			}
			else if (trainPass.Direction == "出厂")
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.当前车号.ToString(), string.Empty);
				if (RemoveTransportPosition(trainPass.CarNumber))
				{
					output(string.Format("#4轨道移除定位信息;{0}车号识别 车号:{1}", trainPass.MachineCode, trainPass.CarNumber), eOutputType.Normal);
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
		public static bool CarSpot4Data(Action<string, eOutputType> output, CmcsTrainRecognition trainPass)
		{
			CmcsTransport transport = Dbers.GetInstance().SelfDber.Entity<CmcsTransport>("where TransportNo='" + trainPass.CarNumber + "' and InfactoryTime>=to_date('" + DateTime.Now.Date.AddDays(-1) + "','yyyy/mm/dd HH24:MI:SS') order by InfactoryTime desc");
			if (transport == null) return false;

			if (trainPass.Direction == "进厂" && transport.InfactoryTime > DateTime.MinValue)
			{
				if (trainPass.OrderNum == 2)//处理牵车太靠近 两节车都识别到车号 后面一节车顶掉前一节车
				{
					try
					{
						CmcsTrainRecognition lastentity = Dbers.GetInstance().SelfDber.Entity<CmcsTrainRecognition>("where CrossTime=:CrossTime and OrderNum=1 and DataFlag=1 order by CrossTime", new { CrossTime = trainPass.CrossTime });
						if (lastentity != null)
						{
							CarInfoMutual mutual = DcDbers.GetInstance().TurnCarWeighterMutualDber.Entity<CarInfoMutual>("where CarNumber=:CarNumber and CreateDate>=:CreateDate", new { CarNumber = lastentity.CarNumber, CreateDate = DateTime.Now.AddDays(-1) });
							if (mutual == null || mutual.SuttleWeight == 0) return false;
						}
					}
					catch (Exception ex)
					{
						output("处理连续车号:" + ex.Message, eOutputType.Error);
					}
				}
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.当前车号.ToString(), trainPass.CarNumber);

				//插入车辆信息至翻车衡交互数据库
				if (InsertCarToTurnCarWeighter("#1", transport))
					output(string.Format("向{0}插入一条翻车衡数据", GlobalVars.MachineCode_TrunOver_1), eOutputType.Normal);
				//更改翻车状态
				if (TurnTransportPosition(trainPass.CarNumber))
					output(string.Format("{0}更改为已翻车", trainPass.CarNumber), eOutputType.Normal);
				return true;
			}
			else if (trainPass.Direction == "出厂")
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.当前车号.ToString(), string.Empty);
				//更改翻车状态
				if (UnTurnTransportPosition(trainPass.CarNumber))
					output(string.Format("{0}更改为未翻车", trainPass.CarNumber), eOutputType.Normal);
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
		public static bool CarSpot5Data(Action<string, eOutputType> output, CmcsTrainRecognition trainPass)
		{
			CmcsTransport transport = Dbers.GetInstance().SelfDber.Entity<CmcsTransport>("where TransportNo='" + trainPass.CarNumber + "' and InfactoryTime>=to_date('" + DateTime.Now.Date.AddDays(-1) + "','yyyy/mm/dd HH24:MI:SS') order by InfactoryTime desc");
			if (transport == null) return false;

			if (trainPass.Direction == "进厂" && transport.InfactoryTime > DateTime.MinValue)
			{
				if (trainPass.OrderNum == 2)//处理牵车太靠近 两节车都识别到车号 后面一节车顶掉前一节车
				{
					try
					{
						CmcsTrainRecognition lastentity = Dbers.GetInstance().SelfDber.Entity<CmcsTrainRecognition>("where CrossTime=:CrossTime and OrderNum=1 and DataFlag=1 order by CrossTime", new { CrossTime = trainPass.CrossTime });
						if (lastentity != null)
						{
							CarInfoMutual mutual = DcDbers.GetInstance().TurnCarWeighterMutualDber.Entity<CarInfoMutual>("where CarNumber=:CarNumber and CreateDate>=:CreateDate", new { CarNumber = lastentity.CarNumber, CreateDate = DateTime.Now.AddDays(-1) });
							if (mutual == null || mutual.SuttleWeight == 0) return false;
						}
					}
					catch (Exception ex)
					{
						output("处理连续车号:" + ex.Message, eOutputType.Error);
					}
				}
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.当前车号.ToString(), trainPass.CarNumber);

				//插入车辆信息至翻车衡交互数据库
				if (InsertCarToTurnCarWeighter("#2", transport))
					output(string.Format("向{0}插入一条数据", GlobalVars.MachineCode_TrunOver_2), eOutputType.Normal);

				//更改翻车状态
				if (TurnTransportPosition(trainPass.CarNumber))
					output(string.Format("{0}更改为已翻车", trainPass.CarNumber), eOutputType.Normal);
				return true;
			}
			else if (trainPass.Direction == "出厂")
			{
				CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.当前车号.ToString(), string.Empty);
				//更改翻车状态
				if (UnTurnTransportPosition(trainPass.CarNumber))
					output(string.Format("{0}更改为未翻车", trainPass.CarNumber), eOutputType.Normal);
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
		public static bool InsertCarToTurnCarWeighter(string trunNumber, CmcsTransport transport)
		{
			if (transport != null && !string.IsNullOrEmpty(transport.InFactoryBatchId))
			{
				CmcsRCSampling sampling = Dbers.GetInstance().SelfDber.Entity<CmcsRCSampling>("where InFactoryBatchId=:InFactoryBatchId and SamplingType!='抽查采样' order by SamplingDate desc", new { InFactoryBatchId = transport.InFactoryBatchId });
				if (sampling != null)
				{
					if (DcDbers.GetInstance().TurnCarWeighterMutualDber.Entity<CarInfoMutual>(" where  TurnCarNumber='" + trunNumber + "' and  CarNumber='" + transport.TransportNo + "' and DataFlag=0 ") == null)
					{
						DcDbers.GetInstance().TurnCarWeighterMutualDber.Execute(" update CarInfoMutual set DataFlag=1 where TurnCarNumber='" + trunNumber + "'");

						return DcDbers.GetInstance().TurnCarWeighterMutualDber.Insert(new CarInfoMutual()
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
						}) > 0;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// 插入定位信息
		/// </summary>
		/// <param name="trackNumber"></param>
		/// <param name="transportid"></param>
		/// <returns></returns>
		public static bool InsertTransportPosition(string trackNumber, string carNumber)
		{
			CmcsTrainCarriagePass transport = Dbers.GetInstance().SelfDber.Entity<CmcsTrainCarriagePass>("where TrainNumber=:TrainNumber and PassTime>=:PassTime order by CreationTime desc", new { TrainNumber = carNumber, PassTime = DateTime.Now.AddDays(-1) });
			if (transport == null) return false;
			CmcsTransportPosition entity = Dbers.GetInstance().SelfDber.Entity<CmcsTransportPosition>("where TransportId='" + transport.Id + "'");
			if (entity != null) Dbers.GetInstance().SelfDber.Delete<CmcsTransportPosition>(entity.Id);

			return Dbers.GetInstance().SelfDber.Insert(new CmcsTransportPosition()
			{
				OrderNumber = GetTransportPositionMaxOrder(),
				TrackNumber = trackNumber,
				TransportId = transport.Id,
				IsDisCharged = 0
			}) > 0;
		}

		/// <summary>
		/// 移除轨道车辆定位信息
		/// </summary>
		/// <param name="transportId"></param>
		public static bool RemoveTransportPosition(string trainNumber)
		{
			CmcsTrainCarriagePass oldtrain = Dbers.GetInstance().SelfDber.Entity<CmcsTrainCarriagePass>("where TrainNumber=:TrainNumber and PassTime>=:PassTime order by PassTime desc", new { TrainNumber = trainNumber, PassTime = DateTime.Now.AddDays(-1) });
			if (oldtrain == null) return false;
			CmcsTransportPosition entity = Dbers.GetInstance().SelfDber.Entity<CmcsTransportPosition>("where TransportId='" + oldtrain.Id + "' order by CreationTime desc");
			if (entity == null) return false;
			return Dbers.GetInstance().SelfDber.Delete<CmcsTransportPosition>(entity.Id) > 0;
		}

		/// <summary>
		/// 修改轨道车辆定位信息，设定为已翻车
		/// </summary>
		/// <param name="trackNumber"></param>
		/// <param name="transportId"></param>
		public static bool TurnTransportPosition(string trainNumber)
		{
			CmcsTrainCarriagePass oldtrain = Dbers.GetInstance().SelfDber.Entity<CmcsTrainCarriagePass>("where TrainNumber=:TrainNumber and PassTime>=:PassTime order by PassTime desc", new { TrainNumber = trainNumber, PassTime = DateTime.Now.AddDays(-1) });
			if (oldtrain == null) return false;
			CmcsTransportPosition entity = Dbers.GetInstance().SelfDber.Entity<CmcsTransportPosition>("where TransportId='" + oldtrain.Id + "' and IsDisCharged=0 order by CreationTime desc");
			if (entity == null) return false;
			entity.TrackNumber = entity.TrackNumber == "#4" ? "#5" : "#1";
			entity.IsDisCharged = 1;
			entity.TurnCarDate = DateTime.Now;
			return Dbers.GetInstance().SelfDber.Update(entity) > 0;
		}

		/// <summary>
		/// 修改轨道车辆定位信息，设定为未翻车
		/// </summary> 
		/// <param name="transportId"></param>
		public static bool UnTurnTransportPosition(string trainNumber)
		{
			CmcsTrainCarriagePass oldtrain = Dbers.GetInstance().SelfDber.Entity<CmcsTrainCarriagePass>("where TrainNumber=:TrainNumber and PassTime>=:PassTime order by PassTime desc", new { TrainNumber = trainNumber, PassTime = DateTime.Now.AddDays(-1) });
			if (oldtrain == null) return false;
			CmcsTransportPosition entity = Dbers.GetInstance().SelfDber.Entity<CmcsTransportPosition>("where TransportId='" + oldtrain.Id + "' and IsDisCharged=1 order by CreationTime desc");
			if (entity == null) return false;

			entity.IsDisCharged = 0;
			entity.TurnCarDate = DateTime.MinValue;
			return Dbers.GetInstance().SelfDber.Update(entity) > 0;
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
