using System;
using System.Collections.Generic;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.TrainJxSampler.Entities;

namespace CMCS.DumblyConcealer.Tasks.TrainJxSampler
{
	/// <summary>
	/// 火车机械采样机接口业务
	/// </summary>
	public class EquTrainJXSamplerDAO
	{
		/// <summary>
		/// EquCarJXSamplerDAO
		/// </summary>
		/// <param name="machineCode">设备编码</param>
		/// <param name="equDber">第三方数据库访问对象</param>
		public EquTrainJXSamplerDAO(string machineCode, SqlServerDapperDber equDber)
		{
			this.MachineCode = machineCode;
			this.EquDber = equDber;
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		/// <summary>
		/// 第三方数据库访问对象
		/// </summary>
		SqlServerDapperDber EquDber;
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

		/// <summary>
		/// 同步实时信号到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <param name="MachineCode">设备编码</param>
		/// <returns></returns>
		public int SyncSignal(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (EquHCQSCYJSignal entity in this.EquDber.Entities<EquHCQSCYJSignal>())
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
			EquHCQSCYJSignal pDCYSignal = this.EquDber.Entity<EquHCQSCYJSignal>("where TagName=@TagName", new { TagName = GlobalVars.EquHeartbeatName });
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

			List<EquHCQSCYJBarrel> infpdcybarrels = this.EquDber.Entities<EquHCQSCYJBarrel>();
			foreach (EquHCQSCYJBarrel entity in infpdcybarrels)
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
					this.EquDber.Update(entity);

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

			foreach (EquHCQSCYJError entity in this.EquDber.Entities<EquHCQSCYJError>("where DataFlag=0"))
			{
				if (commonDAO.SaveEquInfHitch(this.MachineCode, entity.ErrorTime, "故障代码 " + entity.ErrorCode + "，" + entity.ErrorDescribe))
				{
					entity.DataFlag = 1;
					this.EquDber.Update(entity);

					res++;
				}
			}

			output(string.Format("同步故障信息记录 {0} 条", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步采样计划
		/// </summary>
		/// <param name="output"></param>
		/// <param name="MachineCode">设备编码</param>
		public void SyncSamplePlan(Action<string, eOutputType> output)
		{
			int res = 0;

			// 集中管控 > 第三方 
			foreach (InfBeltSamplePlan entity in BeltSamplerDAO.GetInstance().GetWaitForSyncBeltSamplePlan(this.MachineCode))
			{
				bool isSuccess = false;
				// 需调整：命令中的水分等信息视接口而定
				EquHCQSCYJPlan samplecmdEqu = this.EquDber.Get<EquHCQSCYJPlan>(entity.Id);
				if (samplecmdEqu == null)
				{
					isSuccess = this.EquDber.Insert(new EquHCQSCYJPlan
					{
						// 保持相同的Id
						Id = entity.Id,
						SampleCode = entity.SampleCode,
						MachineCode = this.MachineCode,
						TrainCode = entity.TrainCode,
						InFactoryBatchId = entity.InFactoryBatchId,
						TicketWeight = entity.TicketWeight,
						CarCount = entity.CarCount,
						Mt = entity.Mt,
						DataFlag = 0
					}) > 0;
				}
				else
				{
					samplecmdEqu.SampleCode = entity.SampleCode;
					samplecmdEqu.MachineCode = this.MachineCode;
					samplecmdEqu.TrainCode = entity.TrainCode;
					samplecmdEqu.InFactoryBatchId = entity.InFactoryBatchId;
					samplecmdEqu.TicketWeight = entity.TicketWeight;
					samplecmdEqu.CarCount = entity.CarCount;
					samplecmdEqu.Mt = entity.Mt;
					samplecmdEqu.DataFlag = 0;
					isSuccess = this.EquDber.Update(samplecmdEqu) > 0;
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
			foreach (EquHCQSCYJPlan entity in this.EquDber.Entities<EquHCQSCYJPlan>("where DataFlag=2 and datediff(dd,CreateDate,getdate())=0"))
			{
				InfBeltSamplePlan samplecmdInf = Dbers.GetInstance().SelfDber.Get<InfBeltSamplePlan>(entity.Id);
				if (samplecmdInf == null) continue;

				//samplecmdInf.Point1 = entity.Point1;
				//samplecmdInf.Point2 = entity.Point2;
				//samplecmdInf.Point3 = entity.Point3;
				//samplecmdInf.Point4 = entity.Point4;
				//samplecmdInf.Point5 = entity.Point5;
				//samplecmdInf.Point6 = entity.Point6;
				samplecmdInf.StartTime = entity.StartTime;
				samplecmdInf.EndTime = entity.EndTime;
				samplecmdInf.SampleUser = entity.SampleUser;

				if (Dbers.GetInstance().SelfDber.Update(samplecmdInf) > 0)
				{
					// 我方已读
					entity.DataFlag = 3;
					this.EquDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步采样计划 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步采样计划明细
		/// </summary>
		/// <param name="output"></param>
		/// <param name="MachineCode">设备编码</param>
		public void SyncSamplePlanDetail(Action<string, eOutputType> output)
		{
			int res = 0;

			// 集中管控 > 第三方 
			foreach (InfBeltSamplePlanDetail entity in BeltSamplerDAO.GetInstance().GetWaitForSyncBeltSamplePlanDetail(this.MachineCode))
			{
				bool isSuccess = false;
				EquHCQSCYJPlanDetail samplecmdEqu = this.EquDber.Get<EquHCQSCYJPlanDetail>(entity.Id);
				if (samplecmdEqu == null)
				{
					isSuccess = this.EquDber.Insert(new EquHCQSCYJPlanDetail
					{
						// 保持相同的Id
						Id = entity.Id,
						PlanId = entity.PlanId,
						MachineCode = this.MachineCode,
						CarNumber = entity.CarNumber,
						CarModel = entity.CarModel,
						CyCount = entity.CyCount,
						OrderNumber = entity.OrderNumber,
						DataFlag = 0
					}) > 0;
				}
				else
				{
					samplecmdEqu.PlanId = entity.PlanId;
					samplecmdEqu.MachineCode = this.MachineCode;
					samplecmdEqu.CarNumber = entity.CarNumber;
					samplecmdEqu.CarModel = entity.CarModel;
					samplecmdEqu.CyCount = entity.CyCount;
					samplecmdEqu.OrderNumber = entity.OrderNumber;
					samplecmdEqu.DataFlag = 0;
					isSuccess = this.EquDber.Update(samplecmdEqu) > 0;
				}

				if (isSuccess)
				{
					entity.SyncFlag = 1;
					Dbers.GetInstance().SelfDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步采样计划明细 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);


			res = 0;
			// 第三方 > 集中管控
			foreach (EquHCQSCYJPlanDetail entity in this.EquDber.Entities<EquHCQSCYJPlanDetail>("where DataFlag=2 and datediff(dd,CreateDate,getdate())=0"))
			{
				InfBeltSamplePlanDetail samplecmdInf = Dbers.GetInstance().SelfDber.Get<InfBeltSamplePlanDetail>(entity.Id);
				if (samplecmdInf == null) continue;

				//samplecmdInf.Point1 = entity.Point1;
				//samplecmdInf.Point2 = entity.Point2;
				//samplecmdInf.Point3 = entity.Point3;
				//samplecmdInf.Point4 = entity.Point4;
				//samplecmdInf.Point5 = entity.Point5;
				//samplecmdInf.Point6 = entity.Point6;
				samplecmdInf.StartTime = entity.StartTime;
				samplecmdInf.EndTime = entity.EndTime;
				samplecmdInf.SampleUser = entity.SampleUser;

				if (Dbers.GetInstance().SelfDber.Update(samplecmdInf) > 0)
				{
					// 我方已读
					entity.DataFlag = 3;
					this.EquDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步采样计划明细 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
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
			foreach (InfBeltSampleCmd entity in BeltSamplerDAO.GetInstance().GetWaitForSyncBeltSampleCmd(this.MachineCode))
			{
				bool isSuccess = false;
				// 需调整：命令中的水分等信息视接口而定
				EquHCQSCYJSampleCmd samplecmdEqu = this.EquDber.Get<EquHCQSCYJSampleCmd>(entity.Id);
				if (samplecmdEqu == null)
				{
					isSuccess = this.EquDber.Insert(new EquHCQSCYJSampleCmd
					{
						// 保持相同的Id
						Id = entity.Id,
						SampleCode = entity.SampleCode,
						CmdCode = entity.CmdCode,
						ResultCode = entity.ResultCode,
						DataFlag = 0
					}) > 0;
				}
				else
				{
					samplecmdEqu.SampleCode = entity.SampleCode;
					samplecmdEqu.CmdCode = entity.CmdCode;
					samplecmdEqu.ResultCode = entity.ResultCode;
					samplecmdEqu.DataFlag = 0;
					isSuccess = this.EquDber.Update(samplecmdEqu) > 0;
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
			foreach (EquHCQSCYJSampleCmd entity in this.EquDber.Entities<EquHCQSCYJSampleCmd>("where DataFlag=2 and datediff(dd,CreateDate,getdate())=0"))
			{
				InfBeltSampleCmd samplecmdInf = Dbers.GetInstance().SelfDber.Get<InfBeltSampleCmd>(entity.Id);
				if (samplecmdInf == null) continue;

				//samplecmdInf.Point1 = entity.Point1;
				//samplecmdInf.Point2 = entity.Point2;
				//samplecmdInf.Point3 = entity.Point3;
				//samplecmdInf.Point4 = entity.Point4;
				//samplecmdInf.Point5 = entity.Point5;
				//samplecmdInf.Point6 = entity.Point6;
				samplecmdInf.ResultCode = entity.ResultCode;

				if (Dbers.GetInstance().SelfDber.Update(samplecmdInf) > 0)
				{
					// 我方已读
					entity.DataFlag = 3;
					this.EquDber.Update(entity);

					res++;
				}
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
			foreach (EquHCQSCYJUnloadResult entity in this.EquDber.Entities<EquHCQSCYJUnloadResult>("where DataFlag=0"))
			{
				InfBeltSamplerUnloadResult oldUnloadResult = commonDAO.SelfDber.Get<InfBeltSamplerUnloadResult>(entity.Id);
				if (oldUnloadResult == null)
				{
					// 查找采样命令
					EquHCQSCYJPlan qCJXCYJSampleCmd = this.EquDber.Entity<EquHCQSCYJPlan>("where SampleCode=@SampleCode", new { SampleCode = entity.SampleCode });
					if (qCJXCYJSampleCmd != null)
					{
						// 生成采样桶记录
						CmcsRCSampleBarrel rCSampleBarrel = new CmcsRCSampleBarrel()
						{
							BarrelCode = entity.BarrelCode,
							BarrellingTime = entity.UnloadTime,
							BarrelNumber = entity.BarrelNumber,
							InFactoryBatchId = qCJXCYJSampleCmd.InFactoryBatchId,
							SamplerName = this.MachineCode,
							SampleType = eSamplingType.机械采样.ToString(),
							SamplingId = entity.SamplingId
						};

						if (commonDAO.SelfDber.Insert(rCSampleBarrel) > 0)
						{
							if (commonDAO.SelfDber.Insert(new InfQCJXCYJUnloadResult
							{
								MachineCode = this.MachineCode,
								SampleCode = entity.SampleCode,
								BarrelCode = entity.BarrelCode,
								UnloadTime = entity.UnloadTime,
								DataFlag = entity.DataFlag
							}) > 0)
							{
								entity.DataFlag = 1;
								this.EquDber.Update(entity);

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
