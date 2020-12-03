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

namespace CMCS.DumblyConcealer.Tasks.CarJXSampler
{
	/// <summary>
	/// 汽车机械采样机接口业务
	/// </summary>
	public class EquCarJXSamplerDAO
	{
		/// <summary>
		/// EquCarJXSamplerDAO
		/// </summary>
		/// <param name="machineCode">设备编码</param>
		public EquCarJXSamplerDAO(string machineCode)
		{
			this.MachineCode = machineCode;
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		/// <summary>
		/// 设备编码
		/// </summary>
		string MachineCode;
		/// <summary>
		/// 是否处于故障状态
		/// </summary>
		bool IsHitch = false;
		/// <summary>
		/// 上一次上位机心跳值
		/// </summary>
		string PrevHeartbeat = string.Empty;

		#region 数据转换方法（此处有点麻烦，后期调整接口方案）

		#endregion

		/// <summary>
		/// 同步实时信号到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <param name="MachineCode">设备编码</param>
		/// <returns></returns>
		public int SyncSignal(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (EquQCJXCYJSignal entity in DcDbers.GetInstance().CarJXSampler_Dber.Entities<EquQCJXCYJSignal>())
			{
				if (entity.TagName == GlobalVars.EquHeartbeatName) continue;

				// 当心跳检测为故障时，则不更新系统状态，保持 eSampleSystemStatus.发生故障
				if (entity.TagName == eSignalDataName.系统.ToString() && IsHitch) continue;

				res += commonDAO.SetSignalDataValue(this.MachineCode, entity.TagName, entity.TagValue) ? 1 : 0;
			}
			output(string.Format("同步实时信号 {0} 条", res), eOutputType.Normal);

			return res;
		}

		/// <summary>
		/// 获取上位机运行状态表 - 心跳值
		/// 每隔30s读取该值，如果数值不变化则表示设备上位机出现故障
		/// </summary>
		/// <param name="MachineCode">设备编码</param>
		public void SyncHeartbeatSignal()
		{
			EquQCJXCYJSignal pDCYSignal = DcDbers.GetInstance().CarJXSampler_Dber.Entity<EquQCJXCYJSignal>("where TagName=:TagName", new { TagName = GlobalVars.EquHeartbeatName });
			ChangeSystemHitchStatus((pDCYSignal != null && pDCYSignal.TagValue == this.PrevHeartbeat));

			this.PrevHeartbeat = pDCYSignal != null ? pDCYSignal.TagValue : string.Empty;
		}

		/// <summary>
		/// 改变系统状态值
		/// </summary>
		/// <param name="isHitch">是否故障</param> 
		public void ChangeSystemHitchStatus(bool isHitch)
		{
			IsHitch = isHitch;

			if (IsHitch) commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.系统.ToString(), eEquInfSamplerSystemStatus.发生故障.ToString());
		}

		/// <summary>
		/// 同步集样罐信息到集中管控
		/// </summary>
		/// <param name="output"></param> 
		/// <returns></returns>
		public void SyncBarrel(Action<string, eOutputType> output)
		{
			int res = 0;

			List<EquQCJXCYJBarrel> infpdcybarrels = DcDbers.GetInstance().CarJXSampler_Dber.Entities<EquQCJXCYJBarrel>();
			foreach (EquQCJXCYJBarrel entity in infpdcybarrels)
			{
				if (commonDAO.SaveEquInfSampleBarrel(new InfEquInfSampleBarrel
				{
					BarrelNumber = entity.BarrelNumber,
					BarrelStatus = entity.BarrelStatus.ToString(),
					MachineCode = this.MachineCode,
					InFactoryBatchId = entity.InFactoryBatchId,
					InterfaceType = commonDAO.GetMachineInterfaceTypeByCode(this.MachineCode),
					IsCurrent = entity.IsCurrent,
					SampleCode = entity.SampleCode,
					SampleCount = entity.SampleCount,
					UpdateTime = entity.UpdateTime,
					BarrelType = entity.BarrelType,
				}))
				{

					entity.DataFlag = 1;
					DcDbers.GetInstance().CarJXSampler_Dber.Update(entity);

					res++;
				}
			}

			output(string.Format("同步集样罐记录 {0} 条", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步故障信息到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncQCJXCYJError(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (EquQCJXCYJError entity in DcDbers.GetInstance().CarJXSampler_Dber.Entities<EquQCJXCYJError>("where DataFlag=0"))
			{
				if (commonDAO.SaveEquInfHitch(this.MachineCode, entity.ErrorTime, "故障代码 " + entity.ErrorCode + "，" + entity.ErrorDescribe))
				{
					entity.DataFlag = 1;
					DcDbers.GetInstance().CarJXSampler_Dber.Update(entity);

					res++;
				}
			}

			output(string.Format("同步故障信息记录 {0} 条", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步采样命令
		/// </summary>
		/// <param name="output"></param>
		/// <param name="MachineCode">设备编码</param>
		public void SyncSampleCmd(Action<string, eOutputType> output)
		{
			int res = 0;

			// 集中管控 > 第三方 
			foreach (InfQCJXCYSampleCMD entity in CarSamplerDAO.GetInstance().GetWaitForSyncSampleCMD(this.MachineCode))
			{
				bool isSuccess = false;

				Interface_Data samplecmdEqu = DcDbers.GetInstance().CarJXSampler_Dber.Get<Interface_Data>(entity.Id);
				if (samplecmdEqu == null)
				{
					isSuccess = DcDbers.GetInstance().CarJXSampler_Dber.Insert(new Interface_Data
					{
						// 保持相同的Id
						Interface_Id = entity.Id,
						Sampler_No = this.MachineCode == GlobalVars.MachineCode_QCJXCYJ_1 ? "CY01" : "CY02",
						Weighing_Id = entity.SerialNumber,
						Car_Mark = entity.CarNumber,
						Mine_Name = entity.SampleCode,
						Point_Count = entity.PointCount,
						Car_Length = entity.CarriageLength,
						Car_Width = entity.CarriageWidth,
						Car_Height = entity.CarriageHeight,
						Chassis_Height = entity.CarriageBottomToFloor,
						Tie_Rod_Place1 = entity.Obstacle1,
						Tie_Rod_Place2 = entity.Obstacle2,
						Tie_Rod_Place3 = entity.Obstacle3,
						Tie_Rod_Place4 = entity.Obstacle4,
						Tie_Rod_Place5 = entity.Obstacle5,
						Tie_Rod_Place6 = entity.Obstacle6,
						Data_Status = 1
					}) > 0;
				}
				else
				{
					samplecmdEqu.Car_Mark = entity.CarNumber;
					samplecmdEqu.Weighing_Id = entity.SerialNumber;
					samplecmdEqu.Mine_Name = entity.SampleCode;
					samplecmdEqu.Point_Count = entity.PointCount;
					samplecmdEqu.Car_Length = entity.CarriageLength;
					samplecmdEqu.Car_Width = entity.CarriageWidth;
					samplecmdEqu.Car_Height = entity.CarriageHeight;
					samplecmdEqu.Chassis_Height = entity.CarriageBottomToFloor;
					samplecmdEqu.Tie_Rod_Place1 = entity.Obstacle1;
					samplecmdEqu.Tie_Rod_Place2 = entity.Obstacle2;
					samplecmdEqu.Tie_Rod_Place3 = entity.Obstacle3;
					samplecmdEqu.Tie_Rod_Place4 = entity.Obstacle4;
					samplecmdEqu.Tie_Rod_Place5 = entity.Obstacle5;
					samplecmdEqu.Tie_Rod_Place6 = entity.Obstacle6;
					samplecmdEqu.Data_Status = 1;
					isSuccess = DcDbers.GetInstance().CarJXSampler_Dber.Update(samplecmdEqu) > 0;
				}

				if (isSuccess)
				{
					entity.SyncFlag = 1;
					Dbers.GetInstance().SelfDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步采样计划 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);


			res = 0;
			// 第三方 > 集中管控
			foreach (Interface_Data entity in DcDbers.GetInstance().CarJXSampler_Dber.Entities<Interface_Data>("where Data_Status=2 order by Sample_Time desc"))
			{
				InfQCJXCYSampleCMD samplecmdInf = Dbers.GetInstance().SelfDber.Get<InfQCJXCYSampleCMD>(entity.Interface_Id);
				if (samplecmdInf == null) continue;

				//samplecmdInf.Point1 = entity.Point1;
				//samplecmdInf.Point2 = entity.Point2;
				//samplecmdInf.Point3 = entity.Point3;
				//samplecmdInf.Point4 = entity.Point4;
				//samplecmdInf.Point5 = entity.Point5;
				//samplecmdInf.Point6 = entity.Point6;
				samplecmdInf.StartTime = entity.Sample_Time;
				//samplecmdInf.EndTime = entity.EndTime;
				//samplecmdInf.SampleUser = entity.SampleUser;
				samplecmdInf.ResultCode = eEquInfCmdResultCode.成功.ToString();

				//if (Dbers.GetInstance().SelfDber.Update(samplecmdInf) > 0)
				//{
				//	// 我方已读
				//	entity.DataFlag = 3;
				//	this.EquDber.Update(entity);
				res++;
				//}
			}
			output(string.Format("同步采样计划 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步历史卸样结果
		/// </summary>
		/// <param name="output"></param>
		/// <param name="MachineCode"></param>
		public void SyncUnloadResult(Action<string, eOutputType> output)
		{
			int res = 0;

			res = 0;
			// 第三方 > 集中管控
			foreach (EquQCJXCYJUnloadResult entity in DcDbers.GetInstance().CarJXSampler_Dber.Entities<EquQCJXCYJUnloadResult>("where DataFlag=0"))
			{
				InfQCJXCYJUnloadResult oldUnloadResult = commonDAO.SelfDber.Get<InfQCJXCYJUnloadResult>(entity.Id);
				if (oldUnloadResult == null)
				{
					// 查找采样命令
					EquQCJXCYJSampleCmd qCJXCYJSampleCmd = DcDbers.GetInstance().CarJXSampler_Dber.Entity<EquQCJXCYJSampleCmd>("where SampleCode=:SampleCode", new { SampleCode = entity.SampleCode });
					if (qCJXCYJSampleCmd != null)
					{
						// 生成采样桶记录
						CmcsRCSampleBarrel rCSampleBarrel = new CmcsRCSampleBarrel()
						{
							BarrelCode = entity.BarrelCode,
							BarrellingTime = entity.UnloadTime,
							BarrelNumber = entity.BarrelNumber,
							InFactoryBatchId = qCJXCYJSampleCmd.InFactoryBatchId,
							SamplerName = commonDAO.GetMachineNameByCode(this.MachineCode),
							SampleType = eSamplingType.机械采样.ToString(),
							SamplingId = entity.SamplingId
						};

						if (commonDAO.SelfDber.Insert(rCSampleBarrel) > 0)
						{
							if (commonDAO.SelfDber.Insert(new InfQCJXCYJUnloadResult
							{
								SampleCode = entity.SampleCode,
								BarrelCode = entity.BarrelCode,
								UnloadTime = entity.UnloadTime,
								DataFlag = entity.DataFlag
							}) > 0)
							{
								entity.DataFlag = 1;
								DcDbers.GetInstance().CarJXSampler_Dber.Update(entity);

								res++;
							}
						}
					}
				}
			}
			output(string.Format("同步卸样结果 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
		}
	}
}
