﻿using System;
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
using CMCS.Common.Entities.Fuel;

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
				res += commonDAO.SetSignalDataValue(KYToMachineCode(state.CYJ_Machine), eSignalDataName.卸料机状态.ToString(), systemXL.ToString()) ? 1 : 0;
			}

			//foreach (EquSignalData item in DcDbers.GetInstance().BeltSampler_Dber.Entities<EquSignalData>())
			//{

			//}
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
				KY_CYJ_P_OUTRUN samplecmdEqu = DcDbers.GetInstance().BeltSampler_Dber.Entity<KY_CYJ_P_OUTRUN>("where CY_Code=@CY_Code and CYJ_Machine=@CYJ_Machine", new { CYJ_Machine = MachineCodeToKY(this.MachineCode), CY_Code = entity.SampleCode });
				if (samplecmdEqu == null)
				{
					//	isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Insert(new KY_CYJ_P_OUTRUN
					//	{
					//		CYJ_Machine = MachineCodeToKY(this.MachineCode),
					//		CY_Code = entity.SampleCode,
					//		Send_Time = DateTime.Now,
					//		CY_Flag = 0,
					//		Stop_Flag = 0,
					//		TurnCode = this.MachineCode.Contains("A") ? "#1" : "#2",
					//		Car_Count= commonDAO.GetGDHCarCountBySampleCode(this.MachineCode.Contains("A") ? "#1" : "#2", entity.SampleCode)

					//}) > 0;


					KY_CYJ_P_OUTRUN outrun = new KY_CYJ_P_OUTRUN();
					outrun.CYJ_Machine = MachineCodeToKY(this.MachineCode);
					outrun.CY_Code = entity.SampleCode;
					outrun.Send_Time = DateTime.Now;
					outrun.CY_Flag = 0;
					outrun.Stop_Flag = 0;
					outrun.TurnCode = this.MachineCode.Contains("A") ? "#1" : "#2";
					outrun.Car_Count = commonDAO.GetGDHCarCountBySampleCode(this.MachineCode.Contains("A") ? "#1" : "#2", entity.SampleCode);
					isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Insert(outrun) > 0;
					commonDAO.SetSignalDataValue(this.MachineCode, "轨道车数", outrun.Car_Count.ToString());

					KY_CYJ_P_TurnOver turn = DcDbers.GetInstance().BeltSampler_Dber.Entity<KY_CYJ_P_TurnOver>("where CY_Code=@CY_Code", new { CY_Code = entity.SampleCode });
					if (turn == null)
					{
						turn = new KY_CYJ_P_TurnOver();
						turn.Send_Time = DateTime.Now;
						turn.CY_Code = entity.SampleCode;
						turn.DataFlag = 0;
						turn.Car_Count = commonDAO.GetCarCountBySampleCode(entity.SampleCode);
						turn.Ready_Count = commonDAO.GetRealyCarCountBySampleCode(entity.SampleCode);
						turn.IsDone = 0;
						turn.TurnCode = this.MachineCode.Contains("A") ? "#1" : "#2";
						DcDbers.GetInstance().BeltSampler_Dber.Insert(turn);
						commonDAO.SetSignalDataValue(this.MachineCode, turn.TurnCode == "#1" ? "#1翻车机车数" : "#2翻车机车数", turn.Car_Count.ToString());
					}
				}
				else
				{
					samplecmdEqu.CYJ_Machine = MachineCodeToKY(this.MachineCode);
					samplecmdEqu.CY_Code = entity.SampleCode;
					samplecmdEqu.Send_Time = DateTime.Now;
					samplecmdEqu.CY_Flag = 0;
					samplecmdEqu.Stop_Flag = 0;
					samplecmdEqu.TurnCode = this.MachineCode.Contains("A") ? "#1" : "#2";
					samplecmdEqu.Car_Count = commonDAO.GetGDHCarCountBySampleCode(this.MachineCode.Contains("A") ? "#1" : "#2", entity.SampleCode);
					isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Update(samplecmdEqu) > 0;
					commonDAO.SetSignalDataValue(this.MachineCode, "轨道车数", samplecmdEqu.Car_Count.ToString());
				}

				if (isSuccess)
				{
					entity.SyncFlag = 1;
					Dbers.GetInstance().SelfDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步采样计划 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);

			//// 集中管控 > 第三方 
			//foreach (InfBeltSamplePlan entity in BeltSamplerDAO.GetInstance().GetWaitForSyncBeltSamplePlan(this.MachineCode))
			//{
			//	bool isSuccess = false;
			//	// 需调整：命令中的水分等信息视接口而定
			//	KY_CYJ_P_OUTRUN samplecmdEqu = DcDbers.GetInstance().BeltSampler_Dber.Entity<KY_CYJ_P_OUTRUN>("where CY_Code=@CY_Code and CYJ_Machine=@CYJ_Machine", new { CYJ_Machine = MachineCodeToKY(this.MachineCode), CY_Code = entity.SampleCode });
			//	if (samplecmdEqu == null)
			//	{
			//	//	isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Insert(new KY_CYJ_P_OUTRUN
			//	//	{
			//	//		CYJ_Machine = MachineCodeToKY(this.MachineCode),
			//	//		CY_Code = entity.SampleCode,
			//	//		Send_Time = DateTime.Now,
			//	//		CY_Flag = 0,
			//	//		Stop_Flag = 0,
			//	//		TurnCode = this.MachineCode.Contains("A") ? "#1" : "#2",
			//	//		Car_Count= commonDAO.GetGDHCarCountBySampleCode(this.MachineCode.Contains("A") ? "#1" : "#2", entity.SampleCode)

			//	//}) > 0;


			//		KY_CYJ_P_OUTRUN outrun = new KY_CYJ_P_OUTRUN(); 
			//		outrun.CYJ_Machine = MachineCodeToKY(this.MachineCode);
			//		outrun.CY_Code = entity.SampleCode;
			//		outrun.Send_Time = DateTime.Now;
			//		outrun.CY_Flag = 0;
			//		outrun.Stop_Flag = 0;
			//		outrun.TurnCode = this.MachineCode.Contains("A") ? "#1" : "#2";
			//		outrun.Car_Count = commonDAO.GetGDHCarCountBySampleCode(this.MachineCode.Contains("A") ? "#1" : "#2", entity.SampleCode);
			//		isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Insert(outrun)>0;
			//		commonDAO.SetSignalDataValue(this.MachineCode,"轨道车数", outrun.Car_Count.ToString());

			//		KY_CYJ_P_TurnOver turn = DcDbers.GetInstance().BeltSampler_Dber.Entity<KY_CYJ_P_TurnOver>("where CY_Code=@CY_Code", new { CY_Code = entity.SampleCode });
			//		if (turn == null)
			//		{
			//			turn = new KY_CYJ_P_TurnOver();
			//			turn.Send_Time = DateTime.Now;
			//			turn.CY_Code = entity.SampleCode;
			//			turn.DataFlag = 0;
			//			turn.Car_Count = commonDAO.GetCarCountBySampleCode(entity.SampleCode);
			//			turn.Ready_Count = commonDAO.GetRealyCarCountBySampleCode(entity.SampleCode);
			//			turn.IsDone = 0;
			//			turn.TurnCode = this.MachineCode.Contains("A") ? "#1" : "#2";
			//			DcDbers.GetInstance().BeltSampler_Dber.Insert(turn);
			//			commonDAO.SetSignalDataValue(this.MachineCode, turn.TurnCode == "#1" ? "#1翻车机车数" : "#2翻车机车数", turn.Car_Count.ToString());
			//		}
			//	}
			//	else
			//	{
			//		samplecmdEqu.CYJ_Machine = MachineCodeToKY(this.MachineCode);
			//		samplecmdEqu.CY_Code = entity.SampleCode;
			//		samplecmdEqu.Send_Time = DateTime.Now;
			//		samplecmdEqu.CY_Flag = 0;
			//		samplecmdEqu.Stop_Flag = 0;
			//		samplecmdEqu.TurnCode = this.MachineCode.Contains("A") ? "#1" : "#2";
			//		samplecmdEqu.Car_Count = commonDAO.GetGDHCarCountBySampleCode(this.MachineCode.Contains("A") ? "#1" : "#2", entity.SampleCode);
			//		isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Update(samplecmdEqu) > 0;
			//		commonDAO.SetSignalDataValue(this.MachineCode, "轨道车数", samplecmdEqu.Car_Count.ToString());
			//	}

			//	if (isSuccess)
			//	{
			//		entity.SyncFlag = 1;
			//		Dbers.GetInstance().SelfDber.Update(entity);

			//		res++;
			//	}
			//}
			//output(string.Format("同步采样计划 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);


		}

		/// <summary>
		/// 同步采样计划_KY
		/// </summary>
		/// <param name="output"></param>
		/// <param name="MachineCode">设备编码</param>
		public void SyncSamplePlan_KY(Action<string, eOutputType> output)
		{
			int res = 0;

			// 集中管控 > 第三方 
			foreach (InfBeltSamplePlan_KY entity in Dbers.GetInstance().SelfDber.Entities<InfBeltSamplePlan_KY>("where SyncFlag=0"))
			{
				bool isSuccess = false;

				KY_CYJ_P_TurnOver turn = DcDbers.GetInstance().BeltSampler_Dber.Entity<KY_CYJ_P_TurnOver>("where CY_Code=@CY_Code and TurnCode=@TurnCode", new { CY_Code = entity.SampleCode, TurnCode = entity.MachineCode });
				if (turn == null)
				{
					turn = new KY_CYJ_P_TurnOver();
					turn.Send_Time = DateTime.Now;
					turn.CY_Code = entity.SampleCode;
					turn.DataFlag = 0;
					turn.Car_Count = entity.CarCount;
					turn.Ready_Count = 0;
					turn.IsDone = 0;
					turn.TurnCode = entity.MachineCode;
					isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Insert(turn) > 0;
				}
				else
				{
					turn.Send_Time = DateTime.Now;
					turn.CY_Code = entity.SampleCode;
					turn.DataFlag = 0;
					turn.Car_Count = entity.CarCount;
					turn.Ready_Count = 0;
					turn.IsDone = 0;
					turn.TurnCode = entity.MachineCode;
					isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Update(turn) > 0;
				}


				if (isSuccess)
				{
					entity.SyncFlag = 1;
					Dbers.GetInstance().SelfDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步采样计划 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步翻车信息
		/// </summary>
		/// <param name="output"></param>
		public void SyncTurn(Action<string, eOutputType> output)
		{
			int res = 0;
			foreach (KY_CYJ_P_TurnOver entity in DcDbers.GetInstance().BeltSampler_Dber.Entities<KY_CYJ_P_TurnOver>("where IsDone=0 order by Send_Time"))
			{
				entity.Ready_Count = commonDAO.GetRealyCarCountBySampleCode(entity.CY_Code);
				if (entity.Ready_Count == entity.Car_Count)
					entity.IsDone = 1;
				entity.DataFlag = 0;
				entity.Send_Time = DateTime.Now;
				res += DcDbers.GetInstance().BeltSampler_Dber.Update(entity);
				commonDAO.SetSignalDataValue(this.MachineCode, entity.TurnCode == "#1" ? "#1翻车机已翻车数" : "#2翻车机已翻车数", entity.Ready_Count.ToString());
			}
			output(string.Format("同步翻车信息{0}条", res), eOutputType.Normal);
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

		/// <summary>
		/// 同步采样机其他命令
		/// </summary>
		/// <param name="output"></param>
		/// <param name="MachineCode">设备编码</param>
		public void SyncSampleCmd(Action<string, eOutputType> output)
		{
			int res = 0;

			// 集中管控 > 第三方 
			foreach (InfBeltSampleCmd_KY entity in commonDAO.SelfDber.Entities<InfBeltSampleCmd_KY>(" Where SyncFlag='0'"))
			{
				bool isSuccess = false;

				KY_CYJ_P_CMD samplecmdEqu = DcDbers.GetInstance().BeltSampler_Dber.Entity<KY_CYJ_P_CMD>("where CMDId=@CMDId", new { CMDId = entity.Id });
				if (samplecmdEqu == null)
				{
					isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Insert(new KY_CYJ_P_CMD
					{
						CMDId = entity.Id,
						MachineCode = MachineCodeToKY(entity.MachineCode),
						CmdCode = int.Parse(entity.CmdCode),
						ResultCode = 0,
						OperatorName = entity.OperatorName,
						SendDateTime = entity.SendDateTime,
						DataFlag = 0,
					}) > 0;

				}
				else
				{
					samplecmdEqu.MachineCode = MachineCodeToKY(entity.MachineCode);
					samplecmdEqu.CmdCode = int.Parse(entity.CmdCode);
					samplecmdEqu.ResultCode = 0;
					samplecmdEqu.OperatorName = entity.OperatorName;
					samplecmdEqu.SendDateTime = entity.SendDateTime;
					samplecmdEqu.DataFlag = 0;
					isSuccess = DcDbers.GetInstance().BeltSampler_Dber.Update(samplecmdEqu) > 0;
				}

				if (isSuccess)
				{
					entity.SyncFlag = 1;
					Dbers.GetInstance().SelfDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步其他采样命令 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);


		}

		/// <summary>
		/// 同步故障信息到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncError(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (KY_CYJ_P_Alarm entity in DcDbers.GetInstance().BeltSampler_Dber.Entities<KY_CYJ_P_Alarm>("where DateDiff(dd,AlarmDateTime,getdate())<=7"))
			{
				InfEquInfHitch infEquInfHitch = Dbers.GetInstance().SelfDber.Entity<InfEquInfHitch>("where HitchTime=:HitchTime and HitchDescribe=:HitchDescribe", new { HitchTime = entity.AlarmDateTime, HitchDescribe = entity.VarComment });
				if (infEquInfHitch == null)
				{
					if (commonDAO.SaveEquInfHitch(entity.VarName.Contains("A侧") ? GlobalVars.MachineCode_PDCYJ_1 : GlobalVars.MachineCode_PDCYJ_2, entity.AlarmDateTime, entity.VarComment))
					{
						//entity.DataFlag = 1;
						//this.EquDber.Update(entity);

						res++;
					}
				}
			}

			output(string.Format("同步故障信息记录 {0} 条", res), eOutputType.Normal);
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
			foreach (KY_CYJ_P_BARREL entity in DcDbers.GetInstance().BeltSampler_Dber.Entities<KY_CYJ_P_BARREL>("where DateDiff(dd,EditDate,getdate())<=7"))
			{
				// 查找采样命令
				CmcsRCSampling sampling = commonDAO.SelfDber.Entity<CmcsRCSampling>("where SampleCode=:SampleCode", new { SampleCode = entity.Barrel_Name });
				if (sampling != null)
				{
					CmcsRCSampleBarrel cmcsRCSampleBarrel = commonDAO.SelfDber.Entity<CmcsRCSampleBarrel>("where BarrelNumber='" + entity.Barrel_Name + "' and to_char(BarrellingTime,'yyyy-mm-dd hh24:mi:ss')= '" +entity.EditDate.ToString("yyyy-MM-dd HH:mm:ss") +"' and SamplerName = '"+ KYToMachineCode(entity.CYJ_Machine) + "'");
					if (cmcsRCSampleBarrel == null)
					{
						// 生成采样桶记录
						CmcsRCSampleBarrel rCSampleBarrel = new CmcsRCSampleBarrel()
						{
							SamplingId = sampling.Id,
							BarrellingTime = entity.EditDate,
							BarrelNumber = entity.Barrel_Name,
							SamplerName = KYToMachineCode(entity.CYJ_Machine),
							SampleType = eSamplingType.机械采样.ToString(),
							//SampleCount = barrel.SampleCount,
							SampleWeight = entity.Barrel_Weight,
							BarrelCode = entity.Barrel_Code
						};
						commonDAO.SelfDber.Insert(rCSampleBarrel);
					}
					else
					{
						cmcsRCSampleBarrel.SampleWeight = entity.Barrel_Weight;
						commonDAO.SelfDber.Update(cmcsRCSampleBarrel);
					}
				}


				 InfBeltSamplerUnloadResult oldUnloadResult = commonDAO.SelfDber.Entity<InfBeltSamplerUnloadResult>("where samplecode='" + entity.Barrel_Name + "' and to_char(UnloadTime,'yyyy-mm-dd hh24:mi:ss')= '" + entity.EditDate.ToString("yyyy-MM-dd HH:mm:ss") + "'");
				if (oldUnloadResult == null)
				{

					if (commonDAO.SelfDber.Insert(new InfBeltSamplerUnloadResult
					{
						MachineCode = KYToMachineCode(entity.CYJ_Machine),
						SampleCode = entity.Barrel_Name,
						BarrelCode = entity.Barrel_Code,
						UnloadTime = entity.EditDate
					}) > 0)
					{
						//entity.DataFlag = 1;
						//this.EquDber.Update(entity);

						res++;
					}
				}
				else
				{
					oldUnloadResult.MachineCode = KYToMachineCode(entity.CYJ_Machine);
					oldUnloadResult.SampleCode = entity.Barrel_Name;
					oldUnloadResult.BarrelCode = entity.Barrel_Code;
					oldUnloadResult.UnloadTime = entity.EditDate;

					if (commonDAO.SelfDber.Update(oldUnloadResult) > 0)
					{
						//entity.DataFlag = 1;
						//this.EquDber.Update(entity);

						res++;
					}
				}
			}
			output(string.Format("同步卸样结果 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
		}

	}
}
