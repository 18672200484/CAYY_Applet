using System;
using System.Collections.Generic;
using System.Linq;
//
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.AssayDevices;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.AssayDevice.Entities;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice
{
	public class EquAssayDeviceDAO
	{
		List<OriginalData> originaldata_rc = null;//入厂化验结果集
		private static EquAssayDeviceDAO instance;

		public static EquAssayDeviceDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new EquAssayDeviceDAO();
			}
			return instance;
		}

		private EquAssayDeviceDAO()
		{

		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		#region 生成标准测硫仪数据
		private int SulfurCount = 0;
		/// <summary>
		/// 生成标准测硫仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SaveToSulfurAssay(Action<string, eOutputType> output, Int32 days)
		{
			int res = 0;
			// .测硫仪 型号：5E-8SAII
			foreach (CLY_WS_S500 entity in Dbers.GetInstance().SelfDber.Entities<CLY_WS_S500>("where Date_ex>= :TestTime and ID is not null", new { TestTime = DateTime.Now.AddDays(-days).Date }))
			{
				CmcsSulfurAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsSulfurAssay>("where PKID=:PKID", new { PKID = entity.PKID });
				if (item == null)
				{
					item = new CmcsSulfurAssay();
					item.SampleNumber = entity.NAME.ToUpper();
					item.FacilityNumber = entity.MACHINECODE;
					item.ContainerNumber = entity.ID;
					item.ContainerWeight = 0;
					item.SampleWeight = entity.WEIGHT;
					item.Stad = Math.Round(entity.STAD, 2, MidpointRounding.AwayFromZero);
					item.AssayUser = entity.ASSAYER;
					item.AssayTime = entity.DATE1;
					item.OrderNumber = 0;
					item.ISEFFECTIVE = 0;
					item.PKID = entity.PKID;

					res += Dbers.GetInstance().SelfDber.Insert<CmcsSulfurAssay>(item);
					commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "1");
				}
				else
				{
					item.SampleNumber = entity.NAME.ToUpper();
					item.FacilityNumber = entity.MACHINECODE;
					item.ContainerNumber = entity.ID;
					item.ContainerWeight = 0;
					item.SampleWeight = entity.WEIGHT;
					item.Stad = Math.Round(entity.STAD, 2, MidpointRounding.AwayFromZero); ;
					item.AssayUser = entity.ASSAYER;
					item.AssayTime = entity.DATE1;
					item.OrderNumber = 0;

					res += Dbers.GetInstance().SelfDber.Update<CmcsSulfurAssay>(item);
					if (SulfurCount > 1000)
					{
						SulfurCount = 0;
						commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "0");
					}
					SulfurCount++;
				}
			}

			output(string.Format("生成标准测硫仪数据 {0} 条", res), eOutputType.Normal);

			return res;
		}
		#endregion

		#region 生成标准量热仪数据
		private int HeatCount = 0;
		/// <summary>
		/// 生成标准量热仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SaveToHeatAssay(Action<string, eOutputType> output, Int32 days)
		{
			int res = 0;

			// .量热仪 型号：5E_KCⅢ
			foreach (LRY_5E_KCⅢ entity in Dbers.GetInstance().SelfDber.Entities<LRY_5E_KCⅢ>("where Date_ex>= :TestTime and number_ex is not null", new { TestTime = DateTime.Now.AddDays(-days).Date }))
			{
				CmcsHeatAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsHeatAssay>("where PKID=:PKID", new { PKID = entity.PKID });
				if (item == null)
				{
					item = new CmcsHeatAssay();
					item.SampleNumber = entity.MINGCHEN.ToUpper();
					item.FacilityNumber = entity.MACHINECODE;
					item.ContainerNumber = entity.NUMBER_EX;
					item.ContainerWeight = 0;
					item.SampleWeight = Convert.ToDecimal(entity.WEIGHT);
					item.Qbad = Convert.ToDecimal(entity.QB);
					item.AssayUser = entity.TESTMAN;
					item.AssayTime = entity.TESTTIME;
					item.IsEffective = 0;
					item.PKID = entity.PKID;

					res += Dbers.GetInstance().SelfDber.Insert<CmcsHeatAssay>(item);
					commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "1");
				}
				else
				{
					item.SampleNumber = entity.MINGCHEN.ToUpper();
					item.FacilityNumber = entity.MACHINECODE;
					item.ContainerNumber = entity.NUMBER_EX;
					item.ContainerWeight = 0;
					item.SampleWeight = Convert.ToDecimal(entity.WEIGHT);
					item.Qbad = Convert.ToDecimal(entity.QB);
					item.AssayUser = entity.TESTMAN;
					item.AssayTime = entity.TESTTIME;

					res += Dbers.GetInstance().SelfDber.Update<CmcsHeatAssay>(item);
					if (HeatCount > 1000)
					{
						HeatCount = 0;
						commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "0");
					}
					HeatCount++;
				}

			}

			// .量热仪 型号：WS_C800
			foreach (LRY_WS_C800 entity in Dbers.GetInstance().SelfDber.Entities<LRY_WS_C800>("where csrq>= :TestTime", new { TestTime = DateTime.Now.AddDays(-days).Date }))
			{
				CmcsHeatAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsHeatAssay>("where PKID=:PKID", new { PKID = entity.PKID });
				if (item == null)
				{
					item = new CmcsHeatAssay();
					item.SampleNumber = entity.SYMC.ToUpper();
					item.FacilityNumber = entity.MACHINECODE;
					item.ContainerNumber = entity.YQBH;
					item.ContainerWeight = 0;
					item.SampleWeight = Convert.ToDecimal(entity.SSZL);
					item.Qbad = Convert.ToDecimal(entity.DTFRL);
					item.AssayUser = entity.HYY;
					item.AssayTime = entity.CSRQ;
					item.IsEffective = 0;
					item.PKID = entity.PKID;

					res += Dbers.GetInstance().SelfDber.Insert<CmcsHeatAssay>(item);
					commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "1");
				}
				else
				{
					item.SampleNumber = entity.SYMC.ToUpper();
					item.FacilityNumber = entity.MACHINECODE;
					item.ContainerNumber = entity.YQBH;
					item.ContainerWeight = 0;
					item.SampleWeight = Convert.ToDecimal(entity.SSZL);
					item.Qbad = Convert.ToDecimal(entity.DTFRL);
					item.AssayUser = entity.HYY;
					item.AssayTime = entity.CSRQ;

					res += Dbers.GetInstance().SelfDber.Update<CmcsHeatAssay>(item);
					if (HeatCount > 1000)
					{
						HeatCount = 0;
						commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "0");
					}
					HeatCount++;
				}
			}

			output(string.Format("生成标准量热仪数据 {0} 条", res), eOutputType.Normal);

			return res;
		}
		#endregion

		#region 生成标准工分仪数据
		private int ProximateCount = 0;
		public int SaveTOProximateAssay(Action<string, eOutputType> output, Int32 days)
		{
			int res = 0;
			//工分仪 型号 5E_MAG
			foreach (GFY_5E_MAG6600 entity in Dbers.GetInstance().SelfDber.Entities<GFY_5E_MAG6600>(" where Date_ex>= :TestTime", new { TestTime = DateTime.Now.AddDays(-days).Date }))
			{
				CmcsProximateAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsProximateAssay>("where PKID=:PKID", new { PKID = entity.PKID });
				if (item == null)
				{
					item = new CmcsProximateAssay();
					item.SampleNumber = entity.SAMPLENAME.ToUpper();
					item.FacilityNumber = entity.MACHINECODE;
					item.MadContainerNumber = entity.OBJCODE.ToString();
					item.MadContainerWeight = entity.EMPTYGGWEIGHT;
					item.MadSampleWeight = entity.COLEWEIGHT;
					item.MadDryWeight = entity.ADDCOLEWEIGHT;
					item.Mad = entity.MAD;
					item.VadContainerNumber = entity.OBJCODEV.ToString();
					item.VadContainerWeight = entity.VEMPTYGGWEIGHT;
					item.VadSampleWeight = entity.VWEIGHT;
					item.VadDryWeight = entity.VADDCOLEWEIGHT;
					item.Vad = entity.VAD;
					item.AssayUser = entity.OPERATOR;
					item.AssayTime = entity.DATE_EX;
					item.Aad = entity.AAD;
					item.IsEffective = 0;
					item.PKID = entity.PKID;
					res += Dbers.GetInstance().SelfDber.Insert<CmcsProximateAssay>(item);
					commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "1");
				}
				else
				{
					item.SampleNumber = entity.SAMPLENAME.ToUpper();
					item.FacilityNumber = entity.MACHINECODE;
					item.MadContainerNumber = entity.OBJCODE.ToString();
					item.MadContainerWeight = entity.EMPTYGGWEIGHT;
					item.MadSampleWeight = entity.COLEWEIGHT;
					item.MadDryWeight = entity.ADDCOLEWEIGHT;
					item.Mad = entity.MAD;
					item.VadContainerNumber = entity.OBJCODEV.ToString();
					item.VadContainerWeight = entity.VEMPTYGGWEIGHT;
					item.VadSampleWeight = entity.VWEIGHT;
					item.VadDryWeight = entity.VADDCOLEWEIGHT;
					item.Vad = entity.VAD;
					item.AssayUser = entity.OPERATOR;
					item.AssayTime = entity.DATE_EX;
					item.Aad = entity.AAD;

					res += Dbers.GetInstance().SelfDber.Update<CmcsProximateAssay>(item);
					if (ProximateCount > 1000)
					{
						ProximateCount = 0;
						commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "0");
					}
					ProximateCount++;
				}

			}

			//工分仪 型号 WS_G800
			foreach (GFY_WS_G800 entity in Dbers.GetInstance().SelfDber.Entities<GFY_WS_G800>(" where csrq>= :TestTime", new { TestTime = DateTime.Now.AddDays(-days).Date }))
			{
				CmcsProximateAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsProximateAssay>("where PKID=:PKID", new { PKID = entity.PKID });
				if (item == null)
				{
					item = new CmcsProximateAssay();
					item.SampleNumber = entity.SYMC.ToUpper();
					item.FacilityNumber = entity.MACHINECODE;
					item.MadContainerNumber = "0";
					item.MadContainerWeight = entity.SFKGGZ;
					item.MadSampleWeight = entity.SFSYZL;
					item.MadDryWeight = 0;
					item.Mad = 0;
					item.VadContainerNumber = "0";
					item.VadContainerWeight = entity.HFFKGGZ;
					item.VadSampleWeight = entity.HFFSYZL;
					item.VadDryWeight = 0;
					item.Vad = 0;
					item.AssayUser = entity.HYY;
					item.AssayTime = entity.CSRQ;
					item.IsEffective = 0;
					item.PKID = entity.PKID;
					res += Dbers.GetInstance().SelfDber.Insert<CmcsProximateAssay>(item);
					commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "1");
				}
				else
				{
					item.SampleNumber = entity.SYMC.ToUpper();
					item.FacilityNumber = entity.MACHINECODE;
					item.MadContainerNumber = "0";
					item.MadContainerWeight = entity.SFKGGZ;
					item.MadSampleWeight = entity.SFSYZL;
					item.MadDryWeight = 0;
					item.Mad = 0;
					item.VadContainerNumber = "0";
					item.VadContainerWeight = entity.HFFKGGZ;
					item.VadSampleWeight = entity.HFFSYZL;
					item.VadDryWeight = 0;
					item.Vad = 0;
					item.AssayUser = entity.HYY;
					item.AssayTime = entity.CSRQ;
					res += Dbers.GetInstance().SelfDber.Update<CmcsProximateAssay>(item);
					if (ProximateCount > 1000)
					{
						ProximateCount = 0;
						commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "0");
					}
					ProximateCount++;
				}
			}

			output(string.Format("生成标准工分仪数据 {0} 条", res), eOutputType.Normal);

			return res;
		}
		#endregion

		#region 保存标准水分仪数据
		private int MoistureCount = 0;
		/// <summary>
		/// 保存标准水分仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SaveToMoistureAssay(Action<string, eOutputType> output, Int32 days)
		{
			int res = 0;

			// .水分仪 型号：5E-MW6510
			foreach (SFY_WS_M700 entity in Dbers.GetInstance().SelfDber.Entities<SFY_WS_M700>("where KSSJ>=:KSSJ and SYMC is not null order by KSSJ", new { KSSJ = DateTime.Now.AddDays(-days).Date }))
			{
				string pkid = entity.PKID;

				CmcsMoistureAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsMoistureAssay>("where PKID=:PKID", new { PKID = pkid });

				if (item == null)
				{
					item = new CmcsMoistureAssay();
					item.SampleNumber = entity.SYMC.ToUpper();
					item.FacilityNumber = entity.MachineCode;
					item.ContainerNumber = entity.CZPWZ;
					item.ContainerWeight = entity.CZPCZ;
					item.SampleWeight = entity.SYZL;
					item.Mar = entity.SF;
					item.IsEffective = 0;
					item.PKID = pkid;
					item.WaterType = entity.CSLB;
					item.AssayTime = entity.JSSJ;
					item.AssayUser = entity.HYY;
					res += Dbers.GetInstance().SelfDber.Insert<CmcsMoistureAssay>(item);
					commonDAO.SetSignalDataValue("化验室网络管理", entity.MachineCode + "_运行状态", "1");
				}
				else
				{
					item.SampleNumber = entity.SYMC.ToUpper();
					item.FacilityNumber = entity.MachineCode;
					item.ContainerNumber = entity.CZPWZ;
					item.ContainerWeight = entity.CZPCZ;
					item.SampleWeight = entity.SYZL;
					item.Mar = entity.SF;
					item.WaterType = entity.CSLB;
					item.AssayTime = entity.JSSJ;
					item.AssayUser = entity.HYY;
					res += Dbers.GetInstance().SelfDber.Update<CmcsMoistureAssay>(item);
					if (MoistureCount > 1000)
					{
						MoistureCount = 0;
						commonDAO.SetSignalDataValue("化验室网络管理", entity.MachineCode + "_运行状态", "0");
					}
					MoistureCount++;
				}
			}
			output(string.Format("生成标准水分仪数据 {0} 条", res), eOutputType.Normal);
			return res;
		}
		#endregion

		#region 生成标准碳氢仪数据
		private int HadCount = 0;
		public int SaveTOHydrocarbonAssay(Action<string, eOutputType> output, Int32 days)
		{
			int res = 0;
			foreach (TQY_SDCHN435 entity in Dbers.GetInstance().SelfDber.Entities<TQY_SDCHN435>(" where csrq>= :TestTime", new { TestTime = DateTime.Now.AddDays(-days).Date }))
			{
				CmcsHydrocarbonAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsHydrocarbonAssay>("where PKID=:PKID", new { PKID = entity.PKID });
				if (item == null)
				{
					item = new CmcsHydrocarbonAssay();
					item.SAMPLENUMBER = entity.SYBH.ToUpper();
					item.INDICATED = entity.YZ;
					item.MAD = entity.MAD;
					item.CAD = entity.CAD;
					item.CD = entity.CD;
					item.CONCENTRATIONC = entity.NDC;
					item.INITIALSC = entity.CSC;
					item.EXPERIMENTC = entity.SYC;
					item.ADAMOUNTC = entity.ADLC;
					item.ORIGINALC = entity.YSZC;
					item.BLACKC = Convert.ToDecimal(entity.KBC);
					item.DRIFTINGC = entity.PYC;
					item.HAD = entity.HAD;
					item.HD = entity.HD;
					item.CONCENTRATIONC = entity.NDH;
					item.INITIALSH = entity.CSH;
					item.EXPERIMENTH = entity.SYH;
					item.ADAMOUNTH = entity.ADLH;
					item.ORIGINALH = entity.YSZH;
					item.BLACKH = Convert.ToDecimal(entity.KBH);
					item.DRIFTINGH = entity.PYH;
					item.NAD = entity.NAD;
					item.ND = entity.ND;
					item.STARTBASIC = entity.KSJZ;
					item.OVERBASIC = entity.JFZ;
					item.AUTONUMBER = entity.JFCS.ToString();
					item.CONCENTRATIONN = entity.NDN;
					item.BLACKN = entity.KBN;
					item.DRIFTINGN = entity.PYN;
					item.REVISE = entity.JXXZ;
					item.FORECAST = entity.YCP;
					item.ENVIRONMENT = entity.HJP;
					item.CARBON = entity.TBDP;
					item.HYDROGEN = entity.QBDP;
					item.GAS = entity.JQP;
					item.LABORATORY = entity.HYY;
					item.TESTDATE = entity.CSRQ;
					item.STARTDATE = entity.KSSJ;
					item.ENDDATE = entity.JSSJ;
					item.IDC = Convert.ToDecimal(entity.ZDBH);
					item.AAD = entity.AAD;
					item.CDAF = entity.CDAF;
					item.HDAF = entity.HDAF;
					item.NDAF = entity.NDAF;
					item.ORIGINALN = entity.YSZN;
					item.DRIFWAY = entity.DRIFWAY;
					item.HT_AD = entity.HT_AD;
					item.AUTODIRF_H = entity.AUTODIRF_H;
					item.ISEFFECTIVE = 0;
					item.PKID = entity.PKID;
					res += Dbers.GetInstance().SelfDber.Insert<CmcsHydrocarbonAssay>(item);
					commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "1");
				}
				else
				{
					item.SAMPLENUMBER = entity.SYBH.ToUpper();
					item.INDICATED = entity.YZ;
					item.MAD = entity.MAD;
					item.CAD = entity.CAD;
					item.CD = entity.CD;
					item.CONCENTRATIONC = entity.NDC;
					item.INITIALSC = entity.CSC;
					item.EXPERIMENTC = entity.SYC;
					item.ADAMOUNTC = entity.ADLC;
					item.ORIGINALC = entity.YSZC;
					item.BLACKC = Convert.ToDecimal(entity.KBC);
					item.DRIFTINGC = entity.PYC;
					item.HAD = entity.HAD;
					item.HD = entity.HD;
					item.CONCENTRATIONC = entity.NDH;
					item.INITIALSH = entity.CSH;
					item.EXPERIMENTH = entity.SYH;
					item.ADAMOUNTH = entity.ADLH;
					item.ORIGINALH = entity.YSZH;
					item.BLACKH = Convert.ToDecimal(entity.KBH);
					item.DRIFTINGH = entity.PYH;
					item.NAD = entity.NAD;
					item.ND = entity.ND;
					item.STARTBASIC = entity.KSJZ;
					item.OVERBASIC = entity.JFZ;
					item.AUTONUMBER = entity.JFCS.ToString();
					item.CONCENTRATIONN = entity.NDN;
					item.BLACKN = entity.KBN;
					item.DRIFTINGN = entity.PYN;
					item.REVISE = entity.JXXZ;
					item.FORECAST = entity.YCP;
					item.ENVIRONMENT = entity.HJP;
					item.CARBON = entity.TBDP;
					item.HYDROGEN = entity.QBDP;
					item.GAS = entity.JQP;
					item.LABORATORY = entity.HYY;
					item.TESTDATE = entity.CSRQ;
					item.STARTDATE = entity.KSSJ;
					item.ENDDATE = entity.JSSJ;
					item.IDC = Convert.ToDecimal(entity.ZDBH);
					item.AAD = entity.AAD;
					item.CDAF = entity.CDAF;
					item.HDAF = entity.HDAF;
					item.NDAF = entity.NDAF;
					item.ORIGINALN = entity.YSZN;
					item.DRIFWAY = entity.DRIFWAY;
					item.HT_AD = entity.HT_AD;
					item.AUTODIRF_H = entity.AUTODIRF_H;
					res += Dbers.GetInstance().SelfDber.Update<CmcsHydrocarbonAssay>(item);
					if (HadCount > 1000)
					{
						HadCount = 0;
						commonDAO.SetSignalDataValue("化验室网络管理", entity.MACHINECODE + "_运行状态", "0");
					}
					HadCount++;
				}
			}

			output(string.Format("生成标准碳氢仪数据 {0} 条", res), eOutputType.Normal);

			return res;
		}
		#endregion

		#region 自动提取入厂化验记录
		/// <summary>
		/// 自动提取入厂化验记录煤质
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int AutoRCAssay(Action<string, eOutputType> output)
		{
			List<CmcsRCAssay> Assaylist = Dbers.GetInstance().SelfDber.Entities<CmcsRCAssay>("where CreationTime>:assayDate and WfName is null order by CreationTime asc", new { assayDate = DateTime.Now.AddDays(-2).Date });
			int res = 0;
			foreach (var item in Assaylist)
			{
				originaldata_rc = new List<OriginalData>();

				CmcsFuelQuality Quality2 = getHyCyMz(item, item.TheFuelQuality);

				Dbers.GetInstance().SelfDber.Update<CmcsFuelQuality>(Quality2);
				res++;
			}
			output(string.Format("生成化验数据 {0} 条（集中管控）", res), eOutputType.Normal);

			return res;
		}

		/// <summary>
		/// 提取化验煤质
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="Quality"></param>
		/// <returns></returns>
		private CmcsFuelQuality getHyCyMz(CmcsRCAssay entity, CmcsFuelQuality Quality)
		{
			SumQbad(entity.AssayCode);
			SumStad(entity.AssayCode);
			//SumMt(entity.AssayCode);
			SumMt_New(entity.AssayCode);
			SumMad(entity.AssayCode);
			//SumAad(entity.AssayCode);
			//SumVad(entity.AssayCode);

			if (originaldata_rc.Where(a => a.AssayTarget == "Qbad").ToList().Count != 0)
				Quality.QbAd = originaldata_rc.Where(a => a.AssayTarget == "Qbad").FirstOrDefault().OAssayCalValue;

			if (originaldata_rc.Where(a => a.AssayTarget == "Stad").ToList().Count != 0)
				Quality.Stad = originaldata_rc.Where(a => a.AssayTarget == "Stad").FirstOrDefault().OAssayCalValue;

			if (originaldata_rc.Where(a => a.AssayTarget == "Mt").ToList().Count != 0)
				Quality.Mt = originaldata_rc.Where(a => a.AssayTarget == "Mt").FirstOrDefault().OAssayCalValue;

			if (originaldata_rc.Where(a => a.AssayTarget == "Mad").ToList().Count != 0)
				Quality.Mad = originaldata_rc.Where(a => a.AssayTarget == "Mad").FirstOrDefault().OAssayCalValue;

			if (originaldata_rc.Where(a => a.AssayTarget == "Vad").ToList().Count != 0)
				Quality.Vad = originaldata_rc.Where(a => a.AssayTarget == "Vad").FirstOrDefault().OAssayCalValue;

			if (originaldata_rc.Where(a => a.AssayTarget == "Aad").ToList().Count != 0)
				Quality.Aad = originaldata_rc.Where(a => a.AssayTarget == "Aad").FirstOrDefault().OAssayCalValue;

			if (originaldata_rc.Where(a => a.AssayTarget == "Had").ToList().Count != 0)
				Quality.Had = originaldata_rc.Where(a => a.AssayTarget == "Had").FirstOrDefault().OAssayCalValue;

			Quality = getQuality(Quality);

			return Quality;

		}


		#region 汇总指标值
		void SumQbad(string assayBillnumber)
		{
			if (commonDAO.SelfDber.Count<CmcsHeatAssay>("where SampleNumber=:assayBillnumber and IsEffective=1", new { assayBillnumber = assayBillnumber }) > 0)
				return;
			IList<CmcsHeatAssay> heatAssay = Dbers.GetInstance().SelfDber.TopEntities<CmcsHeatAssay>(4, "where SampleNumber=:assayBillnumber and IsEffective=0 order by AssayTime", new { assayBillnumber = assayBillnumber }).ToList();

			if (heatAssay == null || heatAssay.Count > 4) return;
			bool isvalid = false;
			if (heatAssay.Count > 1)
			{
				if (AssayCalcUtil.CheckIsInArea(heatAssay.Select(a => a.Qbad * 1000).ToList(), "Qbad"))
					isvalid = true;
				if (heatAssay.Count == 4 && !isvalid)
				{
					decimal t = 120m;
					IList<Decimal> temp1 = new List<Decimal>() { heatAssay[0].Qbad * 1000, heatAssay[1].Qbad * 1000, heatAssay[2].Qbad * 1000 };
					IList<Decimal> temp2 = new List<Decimal>() { heatAssay[0].Qbad * 1000, heatAssay[1].Qbad * 1000, heatAssay[3].Qbad * 1000 };
					IList<Decimal> temp3 = new List<Decimal>() { heatAssay[0].Qbad * 1000, heatAssay[2].Qbad * 1000, heatAssay[3].Qbad * 1000 };
					IList<Decimal> temp4 = new List<Decimal>() { heatAssay[1].Qbad * 1000, heatAssay[2].Qbad * 1000, heatAssay[3].Qbad * 1000 };
					if (Math.Abs(temp1.Max() - temp1.Min()) <= (1.2m * t))
						heatAssay.RemoveAt(3);
					else if (Math.Abs(temp2.Max() - temp2.Min()) <= (1.2m * t))
						heatAssay.RemoveAt(2);
					else if (Math.Abs(temp3.Max() - temp3.Min()) <= (1.2m * t))
						heatAssay.RemoveAt(1);
					else if (Math.Abs(temp4.Max() - temp4.Min()) <= (1.2m * t))
						heatAssay.RemoveAt(0);
				}
			}
			else if (heatAssay.Count == 1 && heatAssay[0].SampleNumber.Contains("CYCC"))
				isvalid = true;
			foreach (CmcsHeatAssay item in heatAssay)
			{
				if (isvalid)
				{
					originaldata_rc.Add(new OriginalData()
					{
						AssayNum = assayBillnumber,
						AssayTarget = "Qbad",
						AssayFromDevice = item.FacilityNumber,
						OQbad = heatAssay[0].Qbad,
						OAssayCalValue = AssayCalcUtil.CalcAvgValue(heatAssay.Select(a => a.Qbad).ToList(), 3),
						OAssayUser = item.AssayUser,
						OAssayTime = item.AssayTime,
						Isvalid = isvalid
					});
					item.IsEffective = 1;
				}
				else if (heatAssay.Count == 4)
					item.IsEffective = 2;
				Dbers.GetInstance().SelfDber.Update(item);
			}
		}

		void SumStad(string assayBillnumber)
		{
			if (commonDAO.SelfDber.Count<CmcsSulfurAssay>("where SampleNumber=:assayBillnumber and IsEffective=1", new { assayBillnumber = assayBillnumber }) > 0)
				return;
			IList<CmcsSulfurAssay> stadAssay = Dbers.GetInstance().SelfDber.TopEntities<CmcsSulfurAssay>(4, "where SAMPLENUMBER=:assayBillnumber and IsEffective=0 order by AssayTime", new { assayBillnumber = assayBillnumber }).ToList();
			if (stadAssay == null || stadAssay.Count > 4) return;

			bool isvalid = false;
			if (stadAssay.Count > 1)
			{
				if (AssayCalcUtil.CheckIsInArea(stadAssay.Select(a => a.Stad).ToList(), "Stad"))
					isvalid = true;
				if (stadAssay.Count == 4 && !isvalid)
				{
					decimal t = 120m;
					decimal minVal = stadAssay.Min(a => a.Stad);
					decimal maxVal = stadAssay.Max(a => a.Stad);
					if (minVal <= 1.5m)
						t = 0.05m;
					else if (minVal > 1.5m && minVal <= 4m)
						t = 0.1m;
					else if (minVal > 4m)
						t = 0.2m;

					IList<Decimal> temp1 = new List<Decimal>() { stadAssay[0].Stad, stadAssay[1].Stad, stadAssay[2].Stad };
					IList<Decimal> temp2 = new List<Decimal>() { stadAssay[0].Stad, stadAssay[1].Stad, stadAssay[3].Stad };
					IList<Decimal> temp3 = new List<Decimal>() { stadAssay[0].Stad, stadAssay[2].Stad, stadAssay[3].Stad };
					IList<Decimal> temp4 = new List<Decimal>() { stadAssay[1].Stad, stadAssay[2].Stad, stadAssay[3].Stad };
					if (Math.Abs(temp1.Max() - temp1.Min()) <= (1.2m * t))
						stadAssay.RemoveAt(3);
					else if (Math.Abs(temp2.Max() - temp2.Min()) <= (1.2m * t))
						stadAssay.RemoveAt(2);
					else if (Math.Abs(temp3.Max() - temp3.Min()) <= (1.2m * t))
						stadAssay.RemoveAt(1);
					else if (Math.Abs(temp4.Max() - temp4.Min()) <= (1.2m * t))
						stadAssay.RemoveAt(0);
				}
			}
			else if (stadAssay.Count == 1 && stadAssay[0].SampleNumber.Contains("CYCC"))
				isvalid = true;
			foreach (CmcsSulfurAssay item in stadAssay)
			{
				if (isvalid)
				{
					originaldata_rc.Add(new OriginalData()
					{
						AssayNum = assayBillnumber,
						AssayTarget = "Stad",
						AssayFromDevice = stadAssay[0].FacilityNumber,
						OStad = item.Stad,
						OAssayCalValue = AssayCalcUtil.CalcAvgValue(stadAssay.Select(a => a.Stad).ToList(), 3),
						OAssayUser = item.AssayUser,
						OAssayTime = item.AssayTime,
						Isvalid = isvalid
					});
					item.ISEFFECTIVE = 1;
				}
				else if (stadAssay.Count == 4)
					item.ISEFFECTIVE = 2;
				Dbers.GetInstance().SelfDber.Update(item);
			}
		}

		void SumMt(string assayBillnumber)
		{
			CmcsRCAssay assay = commonDAO.SelfDber.Entity<CmcsRCAssay>("where AssayCode=:AssayCode order by CreationTime desc", new { AssayCode = assayBillnumber });
			if (assay == null) return;
			CmcsRCMake make = commonDAO.SelfDber.Get<CmcsRCMake>(assay.MakeId);
			if (make == null) return;

			if (commonDAO.SelfDber.Count<CmcsMoistureAssay>("where SampleNumber=:assayBillnumber and IsEffective=1", new { assayBillnumber = make.MakeCode }) > 0)
				return;
			IList<CmcsMoistureAssay> mtAssay = Dbers.GetInstance().SelfDber.TopEntities<CmcsMoistureAssay>(4, "where SAMPLENUMBER=:assayBillnumber and IsEffective=0 order by AssayTime", new { assayBillnumber = make.MakeCode }).ToList();
			if (mtAssay == null || mtAssay.Count > 4) return;

			bool isvalid = false;
			if (mtAssay.Count > 1)
			{
				isvalid = AssayCalcUtil.CheckIsInArea(mtAssay.Select(a => a.Mar).ToList(), "Mt");
				if (mtAssay.Count == 4 && !isvalid)
				{
					decimal t = 120m;
					decimal minVal = mtAssay.Min(a => a.Mar);
					decimal maxVal = mtAssay.Max(a => a.Mar);
					if (minVal <= 10m)
						t = 0.4m;
					else
						t = 0.5m;
					IList<Decimal> temp1 = new List<Decimal>() { mtAssay[0].Mar, mtAssay[1].Mar, mtAssay[2].Mar };
					IList<Decimal> temp2 = new List<Decimal>() { mtAssay[0].Mar, mtAssay[1].Mar, mtAssay[3].Mar };
					IList<Decimal> temp3 = new List<Decimal>() { mtAssay[0].Mar, mtAssay[2].Mar, mtAssay[3].Mar };
					IList<Decimal> temp4 = new List<Decimal>() { mtAssay[1].Mar, mtAssay[2].Mar, mtAssay[3].Mar };
					if (Math.Abs(temp1.Max() - temp1.Min()) <= (1.2m * t))
						mtAssay.RemoveAt(3);
					else if (Math.Abs(temp2.Max() - temp2.Min()) <= (1.2m * t))
						mtAssay.RemoveAt(2);
					else if (Math.Abs(temp3.Max() - temp3.Min()) <= (1.2m * t))
						mtAssay.RemoveAt(1);
					else if (Math.Abs(temp4.Max() - temp4.Min()) <= (1.2m * t))
						mtAssay.RemoveAt(0);
				}
			}
			else if (mtAssay.Count == 1 && mtAssay[0].SampleNumber.Contains("CYCC"))
				isvalid = true;
			foreach (CmcsMoistureAssay item in mtAssay)
			{
				if (isvalid)
				{
					originaldata_rc.Add(new OriginalData()
					{
						AssayNum = assayBillnumber,
						AssayTarget = "Mt",
						AssayFromDevice = item.FacilityNumber,
						OMt = item.Mar,
						OAssayCalValue = AssayCalcUtil.CalcAvgValue(mtAssay.Select(a => a.Mar).ToList(), 1),
						OAssayUser = item.AssayUser,
						OAssayTime = item.AssayTime,
						Isvalid = isvalid
					});
					item.IsEffective = 1;
				}
				else if (mtAssay.Count == 4)
					item.IsEffective = 2;
				Dbers.GetInstance().SelfDber.Update(item);
			}
		}

		void SumMt_New(string assayBillnumber)
		{
			CmcsRCAssay assay = commonDAO.SelfDber.Entity<CmcsRCAssay>("where AssayCode=:AssayCode order by CreationTime desc", new { AssayCode = assayBillnumber });
			if (assay == null) return;
			CmcsRCMake make = commonDAO.SelfDber.Get<CmcsRCMake>(assay.MakeId);
			if (make == null) return;
			if (commonDAO.SelfDber.Count<CmcsMoistureAssay>("where SampleNumber=:assayBillnumber and IsEffective=1", new { assayBillnumber = make.MakeCode }) > 0)
				return;
			IList<CmcsMoistureAssay> mtAssay = Dbers.GetInstance().SelfDber.TopEntities<CmcsMoistureAssay>(4, "where SAMPLENUMBER=:assayBillnumber and IsEffective=0 order by AssayTime", new { assayBillnumber = make.MakeCode }).ToList();
			if (mtAssay == null || mtAssay.Count != 2) return;

			bool isvalid = false;
			decimal mt = 0m;
			if (mtAssay.Count == 2 && !isvalid)
			{
				decimal t = 120m;
				decimal minVal = mtAssay.Min(a => a.Mar);
				decimal maxVal = mtAssay.Max(a => a.Mar);
				if (minVal <= 10m)
					t = 0.4m;
				else
					t = 0.5m;
				IList<Decimal> temp1 = new List<Decimal>() { mtAssay[0].Mar, mtAssay[1].Mar };
				IList<Decimal> temp2 = new List<Decimal>() { mtAssay[0].Mar, mtAssay[1].Mar };

				if (Math.Abs(temp1.Max() - temp1.Min()) <= t)
				{
					isvalid = true;
					mt = temp1.Max();
					mtAssay = mtAssay.OrderBy(a => a.Mar).ToList();
					mtAssay.Remove(mtAssay[0]);
				}
			}
			else if (mtAssay.Count == 1 && mtAssay[0].SampleNumber.Contains("CYCC"))
				isvalid = true;
			foreach (CmcsMoistureAssay item in mtAssay)
			{
				if (isvalid)
				{
					originaldata_rc.Add(new OriginalData()
					{
						AssayNum = assayBillnumber,
						AssayTarget = "Mt",
						AssayFromDevice = item.FacilityNumber,
						OMt = item.Mar,
						OAssayCalValue = AssayCalcUtil.CalcAvgValue(mt, 1),
						OAssayUser = item.AssayUser,
						OAssayTime = item.AssayTime,
						Isvalid = isvalid
					});
					item.IsEffective = 1;
				}
				else if (mtAssay.Count == 4)
					item.IsEffective = 2;
				Dbers.GetInstance().SelfDber.Update(item);
			}
		}

		void SumMad(string assayBillnumber)
		{
			if (commonDAO.SelfDber.Count<CmcsProximateAssay>("where SampleNumber=:assayBillnumber and IsEffective=1", new { assayBillnumber = assayBillnumber }) > 0)
				return;
			IList<CmcsProximateAssay> madAssay = Dbers.GetInstance().SelfDber.Entities<CmcsProximateAssay>("where SAMPLENUMBER =:assayBillnumber and IsEffective=0", new { assayBillnumber = assayBillnumber });
			if (madAssay == null || madAssay.Count > 4) return;

			bool isvalid_Mad = false, isvalid_Vad = false, isvalid_Aad = false;
			if (madAssay.Count > 1)
			{
				isvalid_Mad = AssayCalcUtil.CheckIsInArea(madAssay.Select(a => a.Mad).ToList(), "Mad");
				isvalid_Vad = AssayCalcUtil.CheckIsInArea(madAssay.Select(a => a.Vad).ToList(), "Vad");
				isvalid_Aad = AssayCalcUtil.CheckIsInArea(madAssay.Select(a => a.Aad).ToList(), "Aad");

				if (madAssay.Count == 4 && (!isvalid_Mad || !isvalid_Vad || !isvalid_Aad))
				{
					decimal t = 120m;
					decimal minVal = madAssay.Min(a => a.Mad);
					decimal maxVal = madAssay.Max(a => a.Mad);
					if (minVal < 5m)
						t = 0.2m;
					else if (minVal >= 5m && minVal <= 10m)
						t = 0.3m;
					else if (minVal > 10m)
						t = 0.4m;
					IList<Decimal> temp1 = new List<Decimal>() { madAssay[0].Mad, madAssay[1].Mad, madAssay[2].Mad };
					IList<Decimal> temp2 = new List<Decimal>() { madAssay[0].Mad, madAssay[1].Mad, madAssay[3].Mad };
					IList<Decimal> temp3 = new List<Decimal>() { madAssay[0].Mad, madAssay[2].Mad, madAssay[3].Mad };
					IList<Decimal> temp4 = new List<Decimal>() { madAssay[1].Mad, madAssay[2].Mad, madAssay[3].Mad };
					if (Math.Abs(temp1.Max() - temp1.Min()) <= (1.2m * t))
						madAssay.RemoveAt(3);
					else if (Math.Abs(temp2.Max() - temp2.Min()) <= (1.2m * t))
						madAssay.RemoveAt(2);
					else if (Math.Abs(temp4.Max() - temp4.Min()) <= (1.2m * t))
						madAssay.RemoveAt(0);
					else if (Math.Abs(temp3.Max() - temp3.Min()) <= (1.2m * t))
						madAssay.RemoveAt(1);
					madAssay.RemoveAt(0);
				}
			}
			else if (madAssay.Count == 1 && madAssay[0].SampleNumber.Contains("CYCC"))
			{
				isvalid_Mad = true;
				isvalid_Vad = true;
				isvalid_Aad = true;
			}
			foreach (CmcsProximateAssay item in madAssay)
			{
				if (isvalid_Mad && isvalid_Vad && isvalid_Aad)
				{
					originaldata_rc.Add(new OriginalData()
					{
						AssayNum = assayBillnumber,
						AssayTarget = "Mad",
						AssayFromDevice = item.FacilityNumber,
						OMad = item.Mad,
						OAssayCalValue = AssayCalcUtil.CalcAvgValue(madAssay.Select(a => a.Mad).ToList(), 3),
						OAssayUser = item.AssayUser,
						OAssayTime = item.AssayTime,
						Isvalid = isvalid_Mad
					});

					originaldata_rc.Add(new OriginalData()
					{
						AssayNum = assayBillnumber,
						AssayTarget = "Vad",
						AssayFromDevice = item.FacilityNumber,
						OVad = item.Vad,
						OAssayCalValue = AssayCalcUtil.CalcAvgValue(madAssay.Select(a => a.Vad).ToList(), 3),
						OAssayUser = item.AssayUser,
						OAssayTime = item.AssayTime,
						Isvalid = isvalid_Vad
					});

					originaldata_rc.Add(new OriginalData()
					{
						AssayNum = assayBillnumber,
						AssayTarget = "Aad",
						AssayFromDevice = item.FacilityNumber,
						OAad = item.Aad,
						OAssayCalValue = AssayCalcUtil.CalcAvgValue(madAssay.Select(a => a.Aad).ToList(), 3),
						OAssayUser = item.AssayUser,
						OAssayTime = item.AssayTime,
						Isvalid = isvalid_Aad
					});
					item.IsEffective = 1;
				}
				else if (madAssay.Count == 4)
					item.IsEffective = 2;
				Dbers.GetInstance().SelfDber.Update(item);
			}
		}

		[Obsolete]
		void SumVad(string assayBillnumber)
		{
			if (commonDAO.SelfDber.Count<CmcsProximateAssay>("where SampleNumber=:assayBillnumber and IsEffective=1", new { assayBillnumber = assayBillnumber }) > 0)
				return;
			IList<CmcsProximateAssay> vadAssay = Dbers.GetInstance().SelfDber.Entities<CmcsProximateAssay>("where SAMPLENUMBER like'%'||:assayBillnumber||'%' and IsEffective=1  and GGZV>0 and YZV>0 and CMZV>0 and CLPZM>0 and YZM>0 and CMZM>0 and  to_char(assaydate,'yyyy')=to_char(sysdate,'yyyy') Order By MachineCode,SampleNumber", new { assayBillnumber = assayBillnumber }).ToList();
			if (vadAssay.Count != 0 && vadAssay.Count >= 2)
			{
				decimal vad1 = vadAssay[0].Vad;
				decimal vad2 = vadAssay[1].Vad;
				string isvalid = "0";
				int isShowRed = vadAssay.Count > 2 ? 1 : 0;
				if (AssayCalcUtil.IsEffectiveVad(vad1, vad2))
					isvalid = "1";
				originaldata_rc.Add(new OriginalData()
				{
					AssayNum = assayBillnumber,
					AssayTarget = "Vad",
					AssayFromDevice = vadAssay[0].FacilityNumber,
					OVad = vadAssay[0].Vad,
					OAssayCalValue = AssayCalcUtil.CalcAvgValue(vadAssay[0].Vad, vadAssay[1].Vad, 3),
					OAssayUser = vadAssay[0].AssayUser,
					OAssayTime = vadAssay[0].AssayTime,
					IsShowRed = isShowRed,
				});
				originaldata_rc.Add(new OriginalData()
				{
					AssayNum = assayBillnumber,
					AssayTarget = "Vad",
					AssayFromDevice = vadAssay[1].FacilityNumber,
					OVad = vadAssay[1].Vad,
					OAssayCalValue = AssayCalcUtil.CalcAvgValue(vadAssay[0].Vad, vadAssay[1].Vad, 3),
					OAssayUser = vadAssay[1].AssayUser,
					OAssayTime = vadAssay[1].AssayTime,
					IsShowRed = isShowRed,
				});
			}
		}

		[Obsolete]
		void SumAad(string assayBillnumber)
		{
			IList<CmcsProximateAssay> aadAssay = Dbers.GetInstance().SelfDber.Entities<CmcsProximateAssay>("where SAMPLENUMBER like'%'||:assayBillnumber||'%' and IsEffective=1  and CMZA!=0 and CZZA!=0 and YZA!=0 and  to_char(assaydate,'yyyy')=to_char(sysdate,'yyyy') Order By MachineCode,SampleNumber", new { assayBillnumber = assayBillnumber }).ToList();
			if (aadAssay.Count != 0 && aadAssay.Count >= 2)
			{
				decimal aad1 = aadAssay[0].Aad;
				decimal aad2 = aadAssay[1].Aad;
				string isvalid = "0";
				int isShowRed = aadAssay.Count > 2 ? 1 : 0;
				if (AssayCalcUtil.IsEffectiveAad(aad1, aad2))
					isvalid = "1";
				originaldata_rc.Add(new OriginalData()
				{
					AssayNum = assayBillnumber,
					AssayTarget = "Aad",
					AssayFromDevice = aadAssay[0].FacilityNumber,
					OAad = aadAssay[0].Aad,
					OAssayCalValue = AssayCalcUtil.CalcAvgValue(aadAssay[0].Aad, aadAssay[1].Aad, 3),
					OAssayUser = aadAssay[0].AssayUser,
					OAssayTime = aadAssay[0].AssayTime,
					IsShowRed = isShowRed,
				});
				originaldata_rc.Add(new OriginalData()
				{
					AssayNum = assayBillnumber,
					AssayTarget = "Aad",
					AssayFromDevice = aadAssay[1].FacilityNumber,
					OAad = aadAssay[1].Aad,
					OAssayCalValue = AssayCalcUtil.CalcAvgValue(aadAssay[0].Aad, aadAssay[1].Aad, 3),
					OAssayUser = aadAssay[1].AssayUser,
					OAssayTime = aadAssay[1].AssayTime,
					IsShowRed = isShowRed,
				});
			}
		}

		#endregion

		//自动计算值
		public CmcsFuelQuality getQuality(CmcsFuelQuality Quality)
		{
			if (100 - Quality.Mad == 0)
			{
				Quality.Ad = 0;
			}
			else
			{
				Quality.Ad = AssayCalcUtil.mathRount((100 / (100 - Quality.Mad) * Quality.Aad), 2);
			}
			if (100 - Quality.Mad == 0)
			{
				Quality.Var = 0;
			}
			else
			{
				// 收到基会发份(Var)%   Vad(%)*((100-Mar)/(100-Mad))
				Quality.Var = AssayCalcUtil.mathRount((Quality.Vad) * (100 - Quality.Mt) / (100 - Quality.Mad), 2);

			}
			if (100 - Quality.Mad - Quality.Aad == 0)
			{
				Quality.Vdaf = 0;
			}
			else
			{
				//干燥无灰基挥发份(Vdaf)%  Vdaf=100/(100-Mad-Aad)*Vad
				Quality.Vdaf = AssayCalcUtil.mathRount((100 / (100 - Quality.Mad - Quality.Aad) * Quality.Vad), 2);

			}
			if (100 - Quality.Mad == 0)
			{
				Quality.Std = 0;
			}
			else
			{

				// 干燥基全硫(St,d)%   St,d=100/(100-Mad)*St,ad
				Quality.Std = AssayCalcUtil.mathRount((100 / (100 - Quality.Mad) * Quality.Stad), 2);


			}
			if (100 - Quality.Mad == 0)
			{
				Quality.Star = 0;
			}
			else
			{
				//收到基全硫(St,ar)%   St,ar=(100-Mar)/(100-M)*ST
				Quality.Star = AssayCalcUtil.mathRount(((100 - Quality.Mt) / (100 - Quality.Mad) * Quality.Stad), 2);
			}
			decimal a = 0.001m;
			if (Quality.QbAd <= 16.70m)
				a = 0.001m;
			else
			{
				if (Quality.QbAd > 25.10m)
					a = 0.0016m;
				else
					a = 0.0012m;
			}

			//if (Qgrad.Attributes["type"] == "balance")
			Quality.Qgrad = AssayCalcUtil.mathRount(((Quality.QbAd * 1000 - (94.1m * Quality.Stad + a * Quality.QbAd * 1000m)) / 1000), 3);
			//else
			//    Qgrad.Text = CalcAvgValue(((decimal.Parse(QbAd.Text) * 1000 - (94.1m * decimal.Parse(Stad.Text) + a * decimal.Parse(QbAd.Text) * 1000m)) / 1000), 2).ToString();

			decimal qbj = ((Quality.QbAd * 1000 - (94.1m * Quality.Stad + a * Quality.QbAd * 1000m)) / 1000);
			if (100 - Quality.Mad == 0)
			{
				Quality.Qj = 0;
			}
			else
			{
				//Qnet,ar  MJ/kg

				Quality.Qj = AssayCalcUtil.mathRount((((qbj * 1000 - 206 * Quality.Had) * ((100 - Quality.Mt) / (100 - Quality.Mad)) - 23 * Quality.Mt) / 1000), 3);

			}
			//(Qnet,ar)Kcal/kg

			Quality.Qcal = decimal.Parse((Quality.Qj * 1000m / 4.1816m).ToString("0"));


			if (100 - Quality.Mad == 0)
			{
				Quality.Qgrd = 0;
			}
			else
			{

				// MJ/kg
				Quality.Qgrd = AssayCalcUtil.mathRount((Quality.Qgrad * (100 / (100 - Quality.Mad))), 3);

			}
			if (100 - Quality.Mad == 0)
			{
				Quality.Aar = 0;
			}
			else
			{

				Quality.Aar = AssayCalcUtil.mathRount((Quality.Aad * ((100 - Quality.Mt) / (100 - Quality.Mad))), 2);


			}
			if (100 - Quality.Mad == 0)
			{
				Quality.Vd = 0;
			}
			else
			{
				Quality.Vd = AssayCalcUtil.mathRount(Quality.Vad * (100 / (100 - (Quality.Mad))), 2);
			}
			Quality.FCad = (100 - (Quality.Mad + Quality.Aad + Quality.Vad));

			//Har%=[100Vd/(100-Vdaf)]*[7.35/(Vdaf+10)-0.013]*[(100-Ad)/100]*(100-Mar)/100  "Mar就相当于Mar”
			//Had(空气干燥基氢值)=(100-Mar)Har/(100-Mad)

			decimal vd = Quality.Vd;
			decimal vdaf = Quality.Vdaf;
			decimal ad = Quality.Ad;
			decimal mt = Quality.Mt;
			decimal mad = Quality.Mad;
			decimal vad = Quality.Vad;

			if (100 - Quality.Mad - Quality.Aad == 0)
			{
				Quality.Hdaf = 0;
			}
			else
			{
				//干燥无灰基氢值(H,daf)%
				Quality.Hdaf = AssayCalcUtil.mathRount((100 / (100 - Quality.Mad - Quality.Aad) * Quality.Had), 2);

			}
			if (100 - Quality.Mad == 0)
			{
				Quality.Har = 0;
			}
			else
			{
				//收到基氢值(H,ar)%
				Quality.Har = AssayCalcUtil.mathRount((Quality.Had * (100 - Quality.Mt) / (100 - Quality.Mad)), 2);

			}
			if (100 - Quality.Mad == 0)
			{
				Quality.Hd = 0;
			}
			else
			{
				//干燥基氢值(H,d)%
				Quality.Hd = AssayCalcUtil.mathRount((100 / (100 - Quality.Mad) * Quality.Had), 2);

			}
			//干燥基固定碳(FC,d)%
			if (100 - Quality.Mad == 0)
			{
				Quality.FCd = 0;
			}
			else
			{
				Quality.FCd = AssayCalcUtil.mathRount((100 / (100 - Quality.Mad) * Quality.FCad), 2);
			}
			if (100 - Quality.Mad == 0)
			{
				Quality.FCar = 0;
			}
			else
			{
				//收到基固定碳(FC,ar)%

				Quality.FCar = AssayCalcUtil.mathRount((Quality.FCad * (100 - Quality.Mt) / (100 - Quality.Mad)), 2);


			}
			if (100 - Quality.Mad - Quality.Aad == 0)
			{
				Quality.Qnetdaf = 0;
			}
			else
			{
				//干燥无灰基高位热量(Qnet,daf)MJ/kg

				Quality.Qnetdaf = AssayCalcUtil.mathRount((100 / (100 - Quality.Mad - Quality.Aad) * Quality.Qgrad), 2);
			}

			if (100 - Quality.Mad - Quality.Aad == 0)
			{
				Quality.Stadf = 0;
			}
			else
			{
				// 干燥无灰基硫(St,daf)%

				Quality.Stadf = AssayCalcUtil.mathRount((100 / (100 - Quality.Mad - Quality.Aad) * Quality.Stad), 2);


			}
			if (100 - Quality.Mad == 0)
			{
				Quality.Qgrar = 0;
			}
			else
			{
				//收到基高位热量(Qgr,ar)MJ/kg

				Quality.Qgrar = AssayCalcUtil.mathRount((Quality.Qgrad * (100 - Quality.Mt) / (100 - Quality.Mad)), 2);

			}
			//干燥无灰基氢值（Hadf）= 0.00117*干燥基灰分+0.57*根号干燥无灰基挥发分(V,daf)%  +0.1362*干燥无灰基高位热值/1000-2.806

			Quality.Hdaf = AssayCalcUtil.mathRount(0.00117m * Quality.Ad + 0.57m * Convert.ToDecimal(Math.Sqrt(double.Parse(Quality.Vdaf.ToString()))) + 0.1362m * Quality.Qnetdaf - 2.806m, 2);


			////空干基氢值=（100-空干基水分—空干基灰分）/100*干燥无灰基氢值（Hadf）
			//Had.Text = Math.Round(((100 - decimal.Parse(Mad.Text) - decimal.Parse(Aad.Text)) * decimal.Parse(Hdaf.Text)) / 100, 2).ToString();

			if (100 - Quality.Mad == 0)
			{
				Quality.Har = 0;
			}
			else
			{
				//收到基氢值(H, ar) %
				Quality.Har = AssayCalcUtil.mathRount((Quality.Had * (100 - Quality.Mt) / (100 - Quality.Mad)), 2);

			}
			return Quality;
		}

		#endregion
	}

	[Serializable]
	class OriginalData
	{
		/// <summary>
		/// 是否为有效化验
		/// </summary>
		public bool Isvalid { get; set; }
		/// <summary>
		/// 化验编码
		/// </summary>
		public string AssayNum { get; set; }

		/// <summary>
		/// 指标
		/// </summary>
		public string AssayTarget { get; set; }

		/// <summary>
		/// 来源
		/// </summary>
		public string AssayFromDevice { get; set; }

		/// <summary>
		/// 弹筒热值Qb,ad(MJ/kg)
		/// </summary>
		public decimal OQbad { get; set; }

		/// <summary>
		/// 空干基全硫（St,ad)
		/// </summary>
		public decimal OStad { get; set; }


		/// <summary>
		/// 水分值（Mar)
		/// </summary>
		public decimal OMt { get; set; }

		/// <summary>
		/// 空干基水分（M,ad)
		/// </summary>
		public decimal OMad { get; set; }

		/// <summary>
		/// 空干基灰分（A,ad)
		/// </summary>
		public decimal OAad { get; set; }

		/// <summary>
		/// 空干基挥发分（V,ad)
		/// </summary>
		public decimal OVad { get; set; }


		/// <summary>
		/// 空干基氢值（H,ad)
		/// </summary>
		public decimal OHad { get; set; }

		/// <summary>
		/// 平均值
		/// </summary>
		public decimal OAssayCalValue { get; set; }

		/// <summary>
		/// 化验用户
		/// </summary>
		public string OAssayUser { get; set; }

		/// <summary>
		/// 化验时间
		/// </summary>
		public DateTime OAssayTime { get; set; }

		/// <summary>
		/// 是否显示红色 如果原始数据大于两条就显示红色 默认为0
		/// </summary>
		public int IsShowRed { get; set; }
	}
}
