using System;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.AssayDevices;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;
using CMCS.DumblyConcealer.Tasks.AutoMt.Entities;

namespace CMCS.DumblyConcealer.Tasks.AutoMt
{
	/// <summary>
	/// 自动水分仪接口业务
	/// </summary>
	public class EquAutoMtDAO
	{
		/// <summary>
		/// EquAutoMakerDAO
		/// </summary>
		/// <param name="machineCode">设备编码</param>
		/// <param name="equDber">第三方数据库访问对象</param>
		public EquAutoMtDAO(string machineCode, SqlServerDapperDber equDber)
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
			if (machine == GlobalVars.MachineCode_QZDZYJ_KY_1)
				return GlobalVars.MachineCode_QZDZYJ_1;
			return string.Empty;
		}

		/// <summary>
		/// 标准设备编码转换为开元编码
		/// </summary>
		/// <param name="machine"></param>
		/// <returns></returns>
		public string DataToKYMachine(string machine)
		{
			if (machine == GlobalVars.MachineCode_QZDZYJ_1)
				return GlobalVars.MachineCode_QZDZYJ_KY_1;
			return string.Empty;
		}
		#endregion

		/// <summary>
		/// 同步实时信号到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SyncSignal(Action<string, eOutputType> output)
		{
			int res = 0;

			//制样设备状态
			foreach (YQ_Status entity in this.EquDber.Entities<YQ_Status>())
			{
				eEquInfMtSystemStatus status = eEquInfMtSystemStatus.就绪待机;
				Enum.TryParse(entity.Status.ToString(), out status);
				res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), status.ToString()) ? 1 : 0;

				output(string.Format("同步实时信号 {0} 条", res), eOutputType.Normal);
			}
			return res;
		}

		/// <summary>
		/// 同步制样 故障信息到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncError(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (var entity in this.EquDber.Entities<YQ_Status>())
			{
				TB_YQ_ERRORCODE error = this.EquDber.Entity<TB_YQ_ERRORCODE>("where ErrorCode=@ErrorCode", new { ErrorCode = entity.ErroeCode });
				if (error != null)
				{
					if (CommonDAO.GetInstance().SaveEquInfHitch(this.MachineCode, entity.St_Time, error.ErrorDes))
					{
						res++;
					}
				}
			}

			output(string.Format("同步故障信息记录 {0} 条", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步水分结果
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncMtResult(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (Tb_TestResult entity in this.EquDber.Entities<Tb_TestResult>("where SampleName is not null and EndingTime is not null and Moisture!=0 order by EndingTime asc"))
			{
				CmcsMoistureAssay moisture = commonDAO.SelfDber.Entity<CmcsMoistureAssay>("where PKID=:PKID", new
				{
					PKID = this.MachineCode + "-" + entity.Id
				});
				if (moisture == null)
				{
					moisture = new CmcsMoistureAssay();
					moisture.PKID = this.MachineCode + "-" + entity.Id;
					moisture.FacilityNumber = this.MachineCode;
					moisture.SampleNumber = entity.SampleNo;
					moisture.SampleWeight = entity.SampleWeight;
					moisture.Mar = entity.Moisture;
					moisture.ContainerNumber = entity.PositionNo.ToString();
					moisture.ContainerWeight = entity.TrayWeight;
					moisture.DryWeight = entity.LeftWeight;
					moisture.AssayTime = entity.EndingTime;
					moisture.WaterType = entity.SampleName == "全水样" ? "mt" : "mar";
					moisture.DataFrom = "在线全水仪";
					moisture.AssayUser = entity.Operator;
					res += Dbers.GetInstance().SelfDber.Insert(moisture);
				}
				else
				{
					moisture.FacilityNumber = this.MachineCode;
					moisture.SampleNumber = entity.SampleNo;
					moisture.SampleWeight = entity.SampleWeight;
					moisture.Mar = entity.Moisture;
					moisture.ContainerNumber = entity.PositionNo.ToString();
					moisture.ContainerWeight = entity.TrayWeight;
					moisture.DryWeight = entity.LeftWeight;
					moisture.AssayTime = entity.EndingTime;
					moisture.WaterType = entity.SampleName == "全水样" ? "mt" : "mar";
					moisture.DataFrom = "在线全水仪";
					moisture.AssayUser = entity.Operator;
					res += Dbers.GetInstance().SelfDber.Update(moisture);
				}
				output(string.Format("同步全水测试记录 {0} 条", res), eOutputType.Normal);
			}
		}

	}
}
