using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.DAO;
using CMCS.DumblyConcealer.Tasks.BeltSampler.Entities;
using CMCS.Common;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Enums;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;

namespace CMCS.DumblyConcealer.Tasks.BeltSampler
{
	/// <summary>
	/// 皮带采样机接口业务
	/// </summary>
	public class EquBeltSamplerDAO
	{
		/// <summary>
		/// EquCarJXSamplerDAO
		/// </summary>
		/// <param name="machineCode">设备编码</param>
		/// <param name="equDber">第三方数据库访问对象</param>
		public EquBeltSamplerDAO(string machineCode)
		{
			this.MachineCode = machineCode;
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		/// <summary>
		/// 设备编码
		/// </summary>
		string MachineCode;

		#region 设备编号转换
		/// <summary>
		/// 设备编码转换为开元编码
		/// </summary>
		/// <param name="machinecode"></param>
		/// <returns></returns>
		public string MachineCodeToKY(string machinecode)
		{
			if (machinecode == GlobalVars.MachineCode_PDCYJ_1)
				return GlobalVars.MachineCode_PDCYJKY_1;
			else if (machinecode == GlobalVars.MachineCode_PDCYJ_2)
				return GlobalVars.MachineCode_PDCYJKY_2;
			return string.Empty;
		}

		/// <summary>
		/// 开元编码转换为设备编码
		/// </summary>
		/// <param name="machinecode"></param>
		/// <returns></returns>
		public string KYToMachineCode(string machinecode)
		{
			if (machinecode == GlobalVars.MachineCode_PDCYJKY_1)
				return GlobalVars.MachineCode_PDCYJ_1;
			else if (machinecode == GlobalVars.MachineCode_PDCYJKY_2)
				return GlobalVars.MachineCode_PDCYJ_2;
			return string.Empty;
		}
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
			foreach (KY_CYJ_P_STATE state in DcDbers.GetInstance().BeltSampler_Dber.Entities<KY_CYJ_P_STATE>())
			{
				eEquInfBeltSamplerSystemStatus system = eEquInfBeltSamplerSystemStatus.等待采样;
				Enum.TryParse<eEquInfBeltSamplerSystemStatus>(state.CY_State, out system);
				res += commonDAO.SetSignalDataValue(KYToMachineCode(state.CYJ_Machine), eSignalDataName.设备状态.ToString(), system.ToString()) ? 1 : 0;

				eEquInfBeltSamplerUnloadStatus systemXL = eEquInfBeltSamplerUnloadStatus.默认;
				Enum.TryParse<eEquInfBeltSamplerSystemStatus>(state.XL_State, out system);
				res += commonDAO.SetSignalDataValue(KYToMachineCode(state.CYJ_Machine), KYToMachineCode(state.CYJ_Machine) + eSignalDataName.卸料机状态.ToString(), systemXL.ToString()) ? 1 : 0;
			}

			foreach (EquSignalData item in DcDbers.GetInstance().BeltSampler_Dber.Entities<EquSignalData>())
			{

			}
			output(string.Format("同步实时信号 {0} 条", res), eOutputType.Normal);

			return res;
		}

		/// <summary>
		/// 同步集样罐信息到集中管控
		/// </summary>
		/// <param name="output"></param> 
		/// <returns></returns>
		public void SyncBarrel(Action<string, eOutputType> output)
		{
			int res = 0;

			List<KY_CYJ_P_BARREL> infpdcybarrels = DcDbers.GetInstance().BeltSampler_Dber.Entities<KY_CYJ_P_BARREL>();
			foreach (KY_CYJ_P_BARREL entity in infpdcybarrels)
			{
				if (commonDAO.SaveEquInfSampleBarrel(new InfEquInfSampleBarrel
				{
					BarrelNumber = entity.Barrel_Code,
					BarrelStatus = entity.Down_Full == 1 ? "已满" : "未满".ToString(),
					MachineCode = this.MachineCode,
					InterfaceType = GlobalVars.InterfaceType_PDCYJ,
					SampleCode = entity.Batch_Number,
					InFactoryBatchId = commonDAO.GetBatchIdBySampleCode(entity.Barrel_Code),
					SampleCount = entity.Down_Count,
					UpdateTime = entity.End_Time,
					BarrelType = "底卸式",
				}))
				{
					res++;
				}
			}

			output(string.Format("同步集样罐记录 {0} 条", res), eOutputType.Normal);
		}

		///// <summary>
		///// 同步故障信息到集中管控
		///// </summary>
		///// <param name="output"></param>
		///// <returns></returns>
		//public void SyncQCJXCYJError(Action<string, eOutputType> output)
		//{
		//	int res = 0;

		//	foreach (EquHCQSCYJError entity in this.EquDber.Entities<EquHCQSCYJError>("where DataFlag=0"))
		//	{
		//		if (commonDAO.SaveEquInfHitch(this.MachineCode, entity.ErrorTime, "故障代码 " + entity.ErrorCode + "，" + entity.ErrorDescribe))
		//		{
		//			entity.DataFlag = 1;
		//			this.EquDber.Update(entity);

		//			res++;
		//		}
		//	}

		//	output(string.Format("同步故障信息记录 {0} 条", res), eOutputType.Normal);
		//}

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
				KY_CYJ_P_OUTRUN samplecmdEqu = DcDbers.GetInstance().BeltSampler_Dber.Entity<KY_CYJ_P_OUTRUN>("where CY_Code=:CY_Code and CYJ_Machine=:CYJ_Machine", new { CYJ_Machine = MachineCodeToKY(this.MachineCode), CY_Code = entity.SampleCode });
				if (samplecmdEqu == null)
				{
					isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Insert(new KY_CYJ_P_OUTRUN
					{
						CYJ_Machine = MachineCodeToKY(this.MachineCode),
						CY_Code = entity.SampleCode,
						Send_Time = DateTime.Now,
						CY_Flag = 0,
						Stop_Flag = 0
					}) > 0;
				}
				else
				{
					samplecmdEqu.CYJ_Machine = MachineCodeToKY(this.MachineCode);
					samplecmdEqu.CY_Code = entity.SampleCode;
					samplecmdEqu.Send_Time = DateTime.Now;
					samplecmdEqu.CY_Flag = 0;
					samplecmdEqu.Stop_Flag = 0;
					isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Update(samplecmdEqu) > 0;
				}

				if (isSuccess)
				{
					entity.SyncFlag = 1;
					Dbers.GetInstance().SelfDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步采样计划 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);


			//res = 0;
			//// 第三方 > 集中管控
			//foreach (EquHCQSCYJPlan entity in this.EquDber.Entities<EquHCQSCYJPlan>("where DataFlag=2 and datediff(dd,CreateDate,getdate())=0"))
			//{
			//	InfBeltSamplePlan samplecmdInf = Dbers.GetInstance().SelfDber.Get<InfBeltSamplePlan>(entity.Id);
			//	if (samplecmdInf == null) continue;

			//	//samplecmdInf.Point1 = entity.Point1;
			//	//samplecmdInf.Point2 = entity.Point2;
			//	//samplecmdInf.Point3 = entity.Point3;
			//	//samplecmdInf.Point4 = entity.Point4;
			//	//samplecmdInf.Point5 = entity.Point5;
			//	//samplecmdInf.Point6 = entity.Point6;
			//	samplecmdInf.StartTime = entity.StartTime;
			//	samplecmdInf.EndTime = entity.EndTime;
			//	samplecmdInf.SampleUser = entity.SampleUser;

			//	if (Dbers.GetInstance().SelfDber.Update(samplecmdInf) > 0)
			//	{
			//		// 我方已读
			//		entity.DataFlag = 3;
			//		this.EquDber.Update(entity);

			//		res++;
			//	}
			//}
			//output(string.Format("同步采样计划 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
		}

		///// <summary>
		///// 同步采样计划明细
		///// </summary>
		///// <param name="output"></param>
		///// <param name="MachineCode">设备编码</param>
		//public void SyncSamplePlanDetail(Action<string, eOutputType> output)
		//{
		//	int res = 0;

		//	// 集中管控 > 第三方 
		//	foreach (InfBeltSamplePlanDetail entity in BeltSamplerDAO.GetInstance().GetWaitForSyncBeltSamplePlanDetail(this.MachineCode))
		//	{
		//		bool isSuccess = false;
		//		EquHCQSCYJPlanDetail samplecmdEqu = this.EquDber.Get<EquHCQSCYJPlanDetail>(entity.Id);
		//		if (samplecmdEqu == null)
		//		{
		//			isSuccess = this.EquDber.Insert(new EquHCQSCYJPlanDetail
		//			{
		//				// 保持相同的Id
		//				Id = entity.Id,
		//				PlanId = entity.PlanId,
		//				MachineCode = this.MachineCode,
		//				CarNumber = entity.CarNumber,
		//				CarModel = entity.CarModel.Substring(0, 3),
		//				CyCount = entity.CyCount,
		//				OrderNumber = entity.OrderNumber,
		//				DataFlag = 0
		//			}) > 0;
		//		}
		//		else
		//		{
		//			samplecmdEqu.PlanId = entity.PlanId;
		//			samplecmdEqu.MachineCode = this.MachineCode;
		//			samplecmdEqu.CarNumber = entity.CarNumber;
		//			samplecmdEqu.CarModel = entity.CarModel.Substring(0, 3);
		//			samplecmdEqu.CyCount = entity.CyCount;
		//			samplecmdEqu.OrderNumber = entity.OrderNumber;
		//			//samplecmdEqu.DataFlag = 0;
		//			isSuccess = this.EquDber.Update(samplecmdEqu) > 0;
		//		}

		//		if (isSuccess)
		//		{
		//			entity.SyncFlag = 1;
		//			Dbers.GetInstance().SelfDber.Update(entity);

		//			res++;
		//		}
		//	}
		//	output(string.Format("同步采样计划明细 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);


		//	res = 0;
		//	// 第三方 > 集中管控
		//	foreach (EquHCQSCYJPlanDetail entity in this.EquDber.Entities<EquHCQSCYJPlanDetail>("where DataFlag=2 and datediff(dd,CreateDate,getdate())=0"))
		//	{
		//		InfBeltSamplePlanDetail samplecmdInf = Dbers.GetInstance().SelfDber.Get<InfBeltSamplePlanDetail>(entity.Id);
		//		if (samplecmdInf == null) continue;

		//		//samplecmdInf.Point1 = entity.Point1;
		//		//samplecmdInf.Point2 = entity.Point2;
		//		//samplecmdInf.Point3 = entity.Point3;
		//		//samplecmdInf.Point4 = entity.Point4;
		//		//samplecmdInf.Point5 = entity.Point5;
		//		//samplecmdInf.Point6 = entity.Point6;
		//		samplecmdInf.StartTime = entity.StartTime;
		//		samplecmdInf.EndTime = entity.EndTime;
		//		samplecmdInf.SampleUser = entity.SampleUser;

		//		if (Dbers.GetInstance().SelfDber.Update(samplecmdInf) > 0)
		//		{
		//			// 我方已读
		//			entity.DataFlag = 3;
		//			this.EquDber.Update(entity);

		//			res++;
		//		}
		//	}
		//	output(string.Format("同步采样计划明细 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
		//}

		///// <summary>
		///// 同步采样命令
		///// </summary>
		///// <param name="output"></param>
		///// <param name="MachineCode">设备编码</param>
		//public void SyncSampleCmd(Action<string, eOutputType> output)
		//{
		//	int res = 0;

		//	// 集中管控 > 第三方 
		//	foreach (InfBeltSampleCmd entity in BeltSamplerDAO.GetInstance().GetWaitForSyncBeltSampleCmd(this.MachineCode))
		//	{
		//		bool isSuccess = false;
		//		// 需调整：命令中的水分等信息视接口而定
		//		EquHCQSCYJSampleCmd samplecmdEqu = this.EquDber.Get<EquHCQSCYJSampleCmd>(entity.Id);
		//		if (samplecmdEqu == null)
		//		{
		//			isSuccess = this.EquDber.Insert(new EquHCQSCYJSampleCmd
		//			{
		//				// 保持相同的Id
		//				Id = entity.Id,
		//				SampleCode = entity.SampleCode,
		//				CmdCode = entity.CmdCode,
		//				ResultCode = entity.ResultCode,
		//				MachineCode = this.MachineCode,
		//				DataFlag = 0
		//			}) > 0;
		//		}
		//		else
		//		{
		//			samplecmdEqu.SampleCode = entity.SampleCode;
		//			samplecmdEqu.CmdCode = entity.CmdCode;
		//			samplecmdEqu.ResultCode = entity.ResultCode;
		//			samplecmdEqu.DataFlag = 0;
		//			isSuccess = this.EquDber.Update(samplecmdEqu) > 0;
		//		}

		//		if (isSuccess)
		//		{
		//			entity.SyncFlag = 1;
		//			Dbers.GetInstance().SelfDber.Update(entity);

		//			res++;
		//		}
		//	}
		//	output(string.Format("同步采样计划 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);


		//	res = 0;
		//	// 第三方 > 集中管控
		//	foreach (EquHCQSCYJSampleCmd entity in this.EquDber.Entities<EquHCQSCYJSampleCmd>("where DataFlag=2 and datediff(dd,CreateDate,getdate())=0"))
		//	{
		//		InfBeltSampleCmd samplecmdInf = Dbers.GetInstance().SelfDber.Get<InfBeltSampleCmd>(entity.Id);
		//		if (samplecmdInf == null) continue;

		//		if (entity.ResultCode.Contains("失败"))
		//		{
		//			samplecmdInf.ResultCode = eEquInfCmdResultCode.失败.ToString();
		//			commonDAO.SaveEquInfHitch(this.MachineCode, DateTime.Now, entity.ResultCode);
		//		}
		//		else
		//			samplecmdInf.ResultCode = entity.ResultCode;

		//		if (Dbers.GetInstance().SelfDber.Update(samplecmdInf) > 0)
		//		{
		//			// 我方已读
		//			entity.DataFlag = 3;
		//			this.EquDber.Update(entity);

		//			res++;
		//		}
		//	}
		//	output(string.Format("同步采样计划 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
		//}

		///// <summary>
		///// 同步历史卸样结果
		///// </summary>
		///// <param name="output"></param>
		///// <param name="MachineCode"></param>
		//public void SyncUnloadResult(Action<string, eOutputType> output)
		//{
		//	int res = 0;

		//	res = 0;
		//	// 第三方 > 集中管控
		//	foreach (EquHCQSCYJUnloadResult entity in this.EquDber.Entities<EquHCQSCYJUnloadResult>("where DataFlag=0"))
		//	{
		//		InfBeltSamplerUnloadResult oldUnloadResult = commonDAO.SelfDber.Get<InfBeltSamplerUnloadResult>(entity.Id);
		//		if (oldUnloadResult == null)
		//		{
		//			// 查找采样命令
		//			EquHCQSCYJPlan qCJXCYJSampleCmd = this.EquDber.Entity<EquHCQSCYJPlan>("where SampleCode=@SampleCode", new { SampleCode = entity.SampleCode });
		//			if (qCJXCYJSampleCmd != null)
		//			{
		//				// 生成采样桶记录
		//				CmcsRCSampleBarrel rCSampleBarrel = new CmcsRCSampleBarrel()
		//				{
		//					BarrelCode = entity.BarrelCode,
		//					BarrellingTime = entity.UnloadTime,
		//					BarrelNumber = entity.BarrelNumber,
		//					InFactoryBatchId = qCJXCYJSampleCmd.InFactoryBatchId,
		//					SamplerName = this.MachineCode,
		//					SampleType = eSamplingType.机械采样.ToString(),
		//					SamplingId = entity.SamplingId
		//				};

		//				if (commonDAO.SelfDber.Insert(rCSampleBarrel) > 0)
		//				{
		//					if (commonDAO.SelfDber.Insert(new InfQCJXCYJUnloadResult
		//					{
		//						MachineCode = this.MachineCode,
		//						SampleCode = entity.SampleCode,
		//						BarrelCode = entity.BarrelCode,
		//						UnloadTime = entity.UnloadTime,
		//						DataFlag = entity.DataFlag
		//					}) > 0)
		//					{
		//						entity.DataFlag = 1;
		//						this.EquDber.Update(entity);

		//						res++;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	output(string.Format("同步卸样结果 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
		//}
	}
}
