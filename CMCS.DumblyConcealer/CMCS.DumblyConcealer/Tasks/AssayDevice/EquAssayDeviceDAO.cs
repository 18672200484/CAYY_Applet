using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.AssayDevices;
using CMCS.Common.Entities.Fuel;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.AssayDevice.Entities;
using System;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice
{
	public class EquAssayDeviceDAO
	{
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
                    item.SampleNumber = entity.NAME;
                    item.FacilityNumber = entity.MACHINECODE;
                    item.ContainerNumber = entity.ID;
                    item.ContainerWeight = 0;
                    item.SampleWeight = entity.WEIGHT;
                    item.Stad = entity.STAD;
                    item.AssayUser = entity.ASSAYER;
                    item.AssayTime = entity.DATE1;
                    item.OrderNumber = 0;
                    item.ISEFFECTIVE = 0;
                    item.PKID = entity.PKID;

                    res += Dbers.GetInstance().SelfDber.Insert<CmcsSulfurAssay>(item);
                }
                else
                {
                    item.SampleNumber = entity.NAME;
                    item.FacilityNumber = entity.MACHINECODE;
                    item.ContainerNumber = entity.ID;
                    item.ContainerWeight = 0;
                    item.SampleWeight = entity.WEIGHT;
                    item.Stad = entity.STAD;
                    item.AssayUser = entity.ASSAYER;
                    item.AssayTime = entity.DATE1;
                    item.OrderNumber = 0;

                    res += Dbers.GetInstance().SelfDber.Update<CmcsSulfurAssay>(item);
                }
            }

            output(string.Format("生成标准测硫仪数据 {0} 条", res), eOutputType.Normal);

            return res;
        }
        #endregion

        #region 生成标准量热仪数据
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
                    item.SampleNumber = entity.MINGCHEN;
                    item.FacilityNumber = entity.MANCODING;
                    item.ContainerNumber = entity.NUMBER_EX;
                    item.ContainerWeight = 0;
                    item.SampleWeight = Convert.ToDecimal(entity.WEIGHT);
                    item.Qbad = Convert.ToDecimal(entity.QB);
                    item.AssayUser = entity.TESTMAN;
                    item.AssayTime = entity.TESTTIME;
                    item.IsEffective = 0;
                    item.PKID = entity.PKID;

                    res += Dbers.GetInstance().SelfDber.Insert<CmcsHeatAssay>(item);
                }
                else
                {
                    item.SampleNumber = entity.MINGCHEN;
                    item.FacilityNumber = entity.MANCODING;
                    item.ContainerNumber = entity.NUMBER_EX;
                    item.ContainerWeight = 0;
                    item.SampleWeight = Convert.ToDecimal(entity.WEIGHT);
                    item.Qbad = Convert.ToDecimal(entity.QB);
                    item.AssayUser = entity.TESTMAN;
                    item.AssayTime = entity.TESTTIME;

                    res += Dbers.GetInstance().SelfDber.Update<CmcsHeatAssay>(item);
                }

            }

            // .量热仪 型号：WS_C800
            foreach (LRY_WS_C800 entity in Dbers.GetInstance().SelfDber.Entities<LRY_WS_C800>("where csrq>= :TestTime", new { TestTime = DateTime.Now.AddDays(-days).Date }))
            {
                CmcsHeatAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsHeatAssay>("where PKID=:PKID", new { PKID = entity.PKID });
                if (item == null)
                {
                    item = new CmcsHeatAssay();
                    item.SampleNumber = entity.SYMC;
                    item.FacilityNumber = entity.MACHINECODE;
                    item.ContainerNumber = entity.ZDBH;
                    item.ContainerWeight = 0;
                    item.SampleWeight = Convert.ToDecimal(entity.SSZL);
                    item.Qbad = Convert.ToDecimal(entity.DTFRL);
                    item.AssayUser = entity.HYY;
                    item.AssayTime = entity.CSRQ;
                    item.IsEffective = 0;
                    item.PKID = entity.PKID;

                    res += Dbers.GetInstance().SelfDber.Insert<CmcsHeatAssay>(item);
                }
                else
                {
                    item.SampleNumber = entity.SYMC;
                    item.FacilityNumber = entity.MACHINECODE;
                    item.ContainerNumber = entity.ZDBH;
                    item.ContainerWeight = 0;
                    item.SampleWeight = Convert.ToDecimal(entity.SSZL);
                    item.Qbad = Convert.ToDecimal(entity.DTFRL);
                    item.AssayUser = entity.HYY;
                    item.AssayTime = entity.CSRQ;

                    res += Dbers.GetInstance().SelfDber.Update<CmcsHeatAssay>(item);
                }

            }

            output(string.Format("生成标准量热仪数据 {0} 条", res), eOutputType.Normal);

            return res;
        }
        #endregion

        #region 生成标准工分仪数据
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
                    item.SampleNumber = entity.SAMPLENAME;
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
                }
                else
                {
                    item.SampleNumber = entity.SAMPLENAME;
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

                }

            }

            //工分仪 型号 WS_G800
            foreach (GFY_WS_G800 entity in Dbers.GetInstance().SelfDber.Entities<GFY_WS_G800>(" where csrq>= :TestTime", new { TestTime = DateTime.Now.AddDays(-days).Date }))
            {
                CmcsProximateAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsProximateAssay>("where PKID=:PKID", new { PKID = entity.PKID });
                if (item == null)
                {
                    item = new CmcsProximateAssay();
                    item.SampleNumber = entity.SYMC;
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
                }
                else
                {
                    item.SampleNumber = entity.SYMC;
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
                }
            }

            output(string.Format("生成标准工分仪数据 {0} 条", res), eOutputType.Normal);

            return res;
        }
        #endregion

        #region 保存标准水分仪数据
        /// <summary>
        /// 保存标准水分仪数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int SaveToMoistureAssay(Action<string, eOutputType> output, Int32 days)
        {
            int res = 0;

            // .水分仪 型号：5E-MW6510
            foreach (SFY_WS_M700 entity in Dbers.GetInstance().SelfDber.Entities<SFY_WS_M700>())
            {
                string pkid = entity.PKID;

                CmcsMoistureAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsMoistureAssay>("where PKID=:PKID", new { PKID = pkid });

                if (item == null)
                {
                    item = new CmcsMoistureAssay();
                    item.SampleNumber = entity.SYMC;
                    item.FacilityNumber = entity.MachineCode;
                    item.ContainerNumber = entity.SYBH==null?"":entity.SYBH;
                    item.ContainerWeight = 0;
                    item.SampleWeight = entity.SYZL;
                    item.Mar = entity.SF;
                    item.IsEffective = 0;
                    item.PKID = pkid;
                    res += Dbers.GetInstance().SelfDber.Insert<CmcsMoistureAssay>(item);
                }
                else
                {
                    item.SampleNumber = entity.SYMC;
                    item.FacilityNumber = entity.MachineCode;
                    item.ContainerNumber = entity.SYBH == null ? "" : entity.SYBH;
                    item.ContainerWeight = 0;
                    item.SampleWeight = entity.SYZL;
                    item.Mar = entity.SF;
                    item.IsEffective = 0;
                    item.PKID = pkid;
                    res += Dbers.GetInstance().SelfDber.Update<CmcsMoistureAssay>(item);
                }
            }
            output(string.Format("生成标准水分仪数据 {0} 条", res), eOutputType.Normal);
            return res;
        }
        #endregion

        #region 生成标准碳氢仪数据
        public int SaveTOHydrocarbonAssay(Action<string, eOutputType> output, Int32 days)
        {
            int res = 0;
            foreach (TQY_SDCHN435 entity in Dbers.GetInstance().SelfDber.Entities<TQY_SDCHN435>(" where csrq>= :TestTime", new { TestTime = DateTime.Now.AddDays(-days).Date }))
            {
                CmcsHydrocarbonAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsHydrocarbonAssay>("where PKID=:PKID", new { PKID = entity.PKID });
                if (item == null)
                {
                    item = new CmcsHydrocarbonAssay();
                    item.SAMPLENUMBER = entity.SYBH;
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

                }
                else
                {
                    item.SAMPLENUMBER = entity.SYBH;
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
                }
            }

            output(string.Format("生成标准碳氢仪数据 {0} 条", res), eOutputType.Normal);

            return res;
        }
        #endregion
    }
}
