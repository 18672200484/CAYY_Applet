using System;
using System.Data;
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
	public class EquAutoMakerDAO
	{
		/// <summary>
		/// EquAutoMakerDAO
		/// </summary>
		/// <param name="machineCode">制样机编码</param>
		/// <param name="equDber">第三方数据库访问对象</param>
		public EquAutoMakerDAO(string machineCode, SqlServerDapperDber equDber)
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

			//同步制样机状态
			foreach (ZY_Status_Tb entity in this.EquDber.Entities<ZY_Status_Tb>("where MachineCode=@MachineCode", new { MachineCode = DataToKYMachine(this.MachineCode) }))
			{
				eEquInfAutoMakerSystemStatus systemStatus = eEquInfAutoMakerSystemStatus.发生故障;
				Enum.TryParse<eEquInfAutoMakerSystemStatus>(entity.SamReady.ToString(), out systemStatus);
				res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), systemStatus.ToString()) ? 1 : 0;
			}
			//制样设备状态
			//foreach (ZY_State_Tb entity in this.EquDber.Entities<ZY_State_Tb>())
			//{
			//	res += commonDAO.SetSignalDataValue(this.MachineCode, entity.DeviceName, entity.DeviceStatus.ToString()) ? 1 : 0;

			//	entity.DataStatus = 1;//我方已读
			//	this.EquDber.Update(entity);
			//}

			output(string.Format("同步实时信号 {0} 条", res), eOutputType.Normal);

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

			foreach (var entity in this.EquDber.Entities<ZY_Error_Tb>("where ReadStatus=0"))
			{
				if (CommonDAO.GetInstance().SaveEquInfHitch(this.MachineCode, entity.DateTime, entity.AlarmName))
				{
					entity.ReadStatus = 1;
					this.EquDber.Update(entity);

					res++;
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
			foreach (InfMakerControlCmd entity in AutoMakerDAO.GetInstance().GetWaitForSyncMakerControlCmd(this.MachineCode))
			{
				bool isSuccess = false;
				eEquInfMakerCmd cmd = eEquInfMakerCmd.开始制样;
				Enum.TryParse<eEquInfMakerCmd>(entity.CmdCode, out cmd);
				ZY_Cmd_Tb cmdtb = this.EquDber.Entity<ZY_Cmd_Tb>("where MachineCode=@MachineCode", new { MachineCode = DataToKYMachine(this.MachineCode) });
				if (cmdtb == null)
				{
					isSuccess = this.EquDber.Insert(new ZY_Cmd_Tb
					{
						MachineCode = DataToKYMachine(this.MachineCode),
						CommandCode = (int)cmd,
						SampleCode = entity.MakeCode,
						SendCommandTime = DateTime.Now,
						DataStatus = 0
					}) > 0;
				}
				else
				{
					cmdtb.CommandCode = (int)cmd;
					cmdtb.SampleCode = entity.MakeCode;
					cmdtb.SendCommandTime = DateTime.Now;
					cmdtb.DataStatus = 0;
					isSuccess = this.EquDber.Update(cmdtb) > 0;
				}

				ZY_Interface_Tb interfacetb = this.EquDber.Entity<ZY_Interface_Tb>();
				if (interfacetb == null)
				{
					interfacetb = new ZY_Interface_Tb();
					interfacetb.SampleID = entity.MakeCode;
					interfacetb.Type = 1;
					interfacetb.Size = 1;
					interfacetb.Water = 1;
					interfacetb.SendCommandTime = DateTime.Now;
					interfacetb.DataStatus = 0;
					isSuccess = this.EquDber.Insert(interfacetb) > 0;
				}
				else
				{
					isSuccess = this.EquDber.Execute(string.Format("update {0} set SampleID ='{1}',Type = 4,Size = 1,Water = 4,SendCommandTime=getdate(),DataStatus = 0 ", CMCS.DapperDber.Util.EntityReflectionUtil.GetTableName<ZY_Interface_Tb>(), entity.MakeCode)) > 0;
				}
				if (isSuccess)
				{
					entity.SyncFlag = 1;
					commonDAO.SelfDber.Update(entity);
					res++;
				}
			}
			output(string.Format("同步控制命令 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步制样 出样明细信息到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncMakeDetail(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (ZY_Record_Tb entity in this.EquDber.Entities<ZY_Record_Tb>("where DataStatus=0 order by StartTime asc"))
			{
				if (SyncToRCMakeDetail(entity))
				{
					InfMakerRecord makeRecord = new InfMakerRecord
					{
						InterfaceType = CommonDAO.GetInstance().GetMachineInterfaceTypeByCode(this.MachineCode),
						MachineCode = this.MachineCode,
						MakeCode = entity.SampleID,
						BarrelCode = entity.PackCode,
						YPType = entity.SampleType, //AutoMakerDAO.GetInstance().GetKYMakeType(entity.SampleType.ToString()),
						YPWeight = entity.SamepleWeight,
						StartTime = entity.StartTime,
						EndTime = entity.EndTime,
						MakeUser = entity.UserName,
						DataFlag = 1
					};
					if (AutoMakerDAO.GetInstance().SaveMakerRecord(makeRecord))
					{
						entity.DataStatus = 1;
						this.EquDber.Update(entity);
						res++;
					}
				}
			}

			output(string.Format("同步出样明细记录 {0} 条", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步样品信息到集中管控入厂煤制样明细表
		/// </summary>
		/// <param name="makeDetail"></param>
		private bool SyncToRCMakeDetail(ZY_Record_Tb makeDetail)
		{
			CmcsRCMake rCMake = commonDAO.SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode", new { MakeCode = makeDetail.SampleID });
			if (rCMake != null)
			{
				// 修改制样结束时间
				rCMake.MakeType = eMakeType.机械制样.ToString();
				//if (rCMake.MakeDate < makeDetail.EndTime) rCMake.MakeDate = makeDetail.EndTime;
				//if (rCMake.MakeDate != rCMake.CreationTime && rCMake.MakeDate > makeDetail.StartTime)
				//{
				//	//rCMake.GetDate = makeDetail.StartTime;
				//	rCMake.MakeDate = makeDetail.EndTime;

				//	//取归批时间
				//	string sql = string.Format(@"select a.backbatchdate from 
				//								fultbinfactorybatch a 
				//								left join cmcstbrcsampling b on a.id=b.infactorybatchid
				//								left join cmcstbmake c on b.id=c.samplingid
				//								where c.id='{0}'", rCMake.Id);
				//	DataTable dt = commonDAO.SelfDber.ExecuteDataTable(sql);
				//	if (dt != null && dt.Rows.Count>0)
				//	{
				//		rCMake.GetDate = Convert.ToDateTime(dt.Rows[0]["backbatchdate"]);
				//	}
				//}

				rCMake.MakeDate = makeDetail.StartTime;

				//取归批时间
				string sql = string.Format(@"select a.backbatchdate from 
												fultbinfactorybatch a 
												left join cmcstbrcsampling b on a.id=b.infactorybatchid
												left join cmcstbmake c on b.id=c.samplingid
												where c.id='{0}'", rCMake.Id);
				DataTable dt = commonDAO.SelfDber.ExecuteDataTable(sql);
				if (dt != null && dt.Rows.Count > 0)
				{
					rCMake.GetDate = Convert.ToDateTime(dt.Rows[0]["backbatchdate"]);
				}

				commonDAO.SelfDber.Update(rCMake);

				CmcsRCMakeDetail rCMakeDetail = commonDAO.SelfDber.Entity<CmcsRCMakeDetail>("where MakeId=:MakeId and SampleType=:SampleType", new { MakeId = rCMake.Id, SampleType = MakeTypeChange(makeDetail.SampleType) });
				if (rCMakeDetail != null)
				{
					rCMakeDetail.LastModificAtionTime = DateTime.Now;
					rCMakeDetail.CreationTime = DateTime.Now;
					rCMakeDetail.SampleWeight = makeDetail.SamepleWeight;
					rCMakeDetail.SampleType = MakeTypeChange(makeDetail.SampleType);
					rCMakeDetail.SampleCode = makeDetail.PackCode;
					return commonDAO.SelfDber.Update(rCMakeDetail) > 0;
				}
				else
				{
					rCMakeDetail = new CmcsRCMakeDetail();
					rCMakeDetail.MakeId = rCMake.Id;
					rCMakeDetail.LastModificAtionTime = DateTime.Now;
					rCMakeDetail.CreationTime = DateTime.Now;
					rCMakeDetail.SampleWeight = makeDetail.SamepleWeight;
					rCMakeDetail.SampleType = MakeTypeChange(makeDetail.SampleType);//AutoMakerDAO.GetInstance().GetKYMakeType(makeDetail.SampleType.ToString());
					rCMakeDetail.SampleCode = makeDetail.PackCode;
					return commonDAO.SelfDber.Insert(rCMakeDetail) > 0;
				}
			}
			else
				return true;

		}

		/// <summary>
		/// 制样类型转换
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private string MakeTypeChange(string type)
		{
			string makeType = "6mm全水样";
			switch (type)
			{
				case "1":
					makeType = "6mm全水样";
					break;
				case "2":
					makeType = "3mm备查样";
					break;
				case "3":
					makeType = "0.2mm分析样";
					break;
				case "4":
					makeType = "0.2mm存查样";
					break;
				default:
					break;
			}
			return makeType;
		}
	}
}
