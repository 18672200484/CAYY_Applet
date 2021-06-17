using System;
using System.Collections.Generic;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker
{
	/// <summary>
	/// 全自动制样机接口业务
	/// </summary>
	public class EquBatchMachineDAO
	{
		/// <summary>
		/// EquAutoMakerDAO
		/// </summary>
		/// <param name="machineCode">合样归批编码</param>
		/// <param name="equDber">第三方数据库访问对象</param>
		public EquBatchMachineDAO(string machineCode, SqlServerDapperDber equDber)
		{
			this.MachineCode = machineCode;
			this.EquDber = equDber;
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		/// <summary>
		/// 第三方数据库访问对象
		/// </summary>
		public SqlServerDapperDber EquDber;
		/// <summary>
		/// 设备编码
		/// </summary>
		public string MachineCode;
		/// <summary>
		/// 是否处于故障状态
		/// </summary>
		bool IsHitch = false;
		/// <summary>
		/// 上一次上位机心跳值
		/// </summary>
		string PrevHeartbeat = string.Empty;

		#region 数据转换方法

		/// <summary>
		/// 开元编码转换为标准设备编码
		/// </summary>
		/// <param name="machine"></param>
		/// <returns></returns>
		public string KYMachineToData(string machine)
		{
			if (machine == GlobalVars.MachineCode_HYGPJ_KY_1)
				return GlobalVars.MachineCode_HYGPJ_1;
			return string.Empty;
		}

		/// <summary>
		/// 标准设备编码转换为开元编码
		/// </summary>
		/// <param name="machine"></param>
		/// <returns></returns>
		public string DataToKYMachine(string machine)
		{
			if (machine == GlobalVars.MachineCode_HYGPJ_1)
				return GlobalVars.MachineCode_HYGPJ_KY_1;
			return string.Empty;
		}
		#endregion

		/// <summary>
		/// 同步实时信号到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		//public int SyncSignal(Action<string, eOutputType> output)
		//{
		//	int res = 0;

		//	//同步制样机状态
		//	foreach (ZY_Status_Tb entity in this.EquDber.Entities<ZY_Status_Tb>("where MachineCode=@MachineCode", new { MachineCode = DataToKYMachine(this.MachineCode) }))
		//	{
		//		eEquInfAutoMakerSystemStatus systemStatus = eEquInfAutoMakerSystemStatus.发生故障;
		//		Enum.TryParse<eEquInfAutoMakerSystemStatus>(entity.SamReady.ToString(), out systemStatus);
		//		res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), systemStatus.ToString()) ? 1 : 0;
		//	}
		//	//制样设备状态
		//	foreach (ZY_State_Tb entity in this.EquDber.Entities<ZY_State_Tb>())
		//	{
		//		res += commonDAO.SetSignalDataValue(this.MachineCode, entity.DeviceName, entity.DeviceStatus.ToString()) ? 1 : 0;

		//		entity.DataStatus = 1;//我方已读
		//		this.EquDber.Update(entity);
		//	}

		//	output(string.Format("同步实时信号 {0} 条", res), eOutputType.Normal);

		//	return res;
		//}

		/// <summary>
		/// 同步合样归批机 故障信息到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncError(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (var entity in this.EquDber.Entities<CY_HYGP_AL_Tb>("where DATEDIFF(dd, Date_Time, GETDATE())=0"))
			{
				InfEquInfHitch infEquInfHitch = Dbers.GetInstance().SelfDber.Entity<InfEquInfHitch>(" where MachineCode=:MachineCode and HitchTime=:HitchTime and HitchDescribe=:HitchDescribe", new { MachineCode = this.MachineCode, HitchTime=entity.Date_Time, HitchDescribe= entity.Error_Record });
				if (infEquInfHitch == null)
				{
					if (CommonDAO.GetInstance().SaveEquInfHitch(this.MachineCode, entity.Date_Time, entity.Error_Record))
					{
						//entity.ReadStatus = 1;
						//this.EquDber.Update(entity);
						res++;
					}
				}
			}

			output(string.Format("同步故障信息记录 {0} 条", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步制样命令
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncCmd(Action<string, eOutputType> output)
		{
			int res = 0;

			// 集中管控 > 第三方 
			foreach (InfBatchMachineCmd entity in Dbers.GetInstance().SelfDber.Entities<InfBatchMachineCmd>("where SyncFlag=0", new { MachineCode = this.MachineCode }))
			{
				bool isSuccess = false;
				eEquInfBatchMachineCmd cmd = eEquInfBatchMachineCmd.倒料;
				Enum.TryParse<eEquInfBatchMachineCmd>(entity.CmdCode, out cmd);
				CY_HYGP_Cmd_Tb cmdtb = this.EquDber.Entity<CY_HYGP_Cmd_Tb>("where SampleID=@SampleID", new { SampleID= entity .SampleCode});
				if (cmdtb == null)
				{
					isSuccess = this.EquDber.Insert(new CY_HYGP_Cmd_Tb
					{
						CommandCode = (int)cmd,
						SampleID = entity.SampleCode,
						DataStatus = 0,
						SendTime = DateTime.Now,
						DateTime = DateTime.Now
					}) > 0;
				}
				else
				{
					cmdtb.CommandCode = (int)cmd;
					cmdtb.SampleID = entity.SampleCode;
					cmdtb.SendTime = DateTime.Now;
					cmdtb.DateTime = DateTime.Now;
					cmdtb.DataStatus = 0;
					isSuccess = this.EquDber.Update(cmdtb) > 0;
				}

				if (isSuccess)
				{
					entity.SyncFlag = 1;
					commonDAO.SelfDber.Update(entity);
					res++;
				}
			}
			output(string.Format("同步控制命令 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);

			res = 0;
			// 第三方 > 集中管控
			foreach (InfBatchMachineCmd item in commonDAO.SelfDber.Entities<InfBatchMachineCmd>("where ResultCode='默认' order by CreationTime "))
			{
				CY_HYGP_Cmd_Tb entity = this.EquDber.Entity<CY_HYGP_Cmd_Tb>("where SampleID=@SampleID", new { SampleID = item.SampleCode });
				if (entity != null)
				{
					if (entity.DataStatus == 11 || entity.DataStatus == 12)
					{
						item.ResultCode = eEquInfCmdResultCode.成功.ToString();

						if (Dbers.GetInstance().SelfDber.Update(item) > 0)
						{
							commonDAO.SaveSysMessage("合样归批倒料", "合样归批倒料执行成功,编码："+ item.SampleCode);
							res++;
						}
						output(string.Format("同步采样计划 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
					}
				}
			}
		}

		/// <summary>
		/// 同步集样罐信息到集中管控
		/// </summary>
		/// <param name="output"></param> 
		/// <returns></returns>
		public void SyncBarrel(Action<string, eOutputType> output)
		{
			int res = 0;

			List<CY_HYGP_Barrel_Tb> barrels = this.EquDber.Entities<CY_HYGP_Barrel_Tb>();
			foreach (CY_HYGP_Barrel_Tb entity in barrels)
			{
				if (commonDAO.SaveInfBatchMachineBarrel(new InfBatchMachineBarrel
				{
					MachineCode = entity.MachineCode,
					BarrelStation = entity.BarrelStation,
					BarrelCode = entity.BarrelCode,
					SampleID = entity.SampleID,
					SampleWeight = entity.SampleWeight,
					BarrelStatus = entity.BarrelStatus,
					DataStatus = entity.DataStatus,
					StrartTime = entity.StrartTime,
					EndTime = entity.EndTime,
				}))
				{
					//DcDbers.GetInstance().CarJXSampler_Dber.Update(entity);

					res++;
				}
			}

			output(string.Format("同步集样罐记录 {0} 条", res), eOutputType.Normal);
		}
	}
}
