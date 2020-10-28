using CMCS.Common.Entities.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Entities.AssayDevices
{
    /// <summary>
    ///化验数据- 碳氢仪
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CMCSHyDROCARBONASSAY")]
    public class CmcsHydrocarbonAssay : EntityBase
    {
        #region 成员变量、构造函数
        Decimal m_EXPERIMENTC;
        String m_HD;
        Decimal m_INTEGRAL;
        String m_CAD;
        String m_DRIFTINGC;
        String m_ND;
        String m_COMPLETENESS;
        Decimal m_BLACKC;
        Decimal m_ADAMOUNTH;
        Decimal m_BLACKN;
        String m_CD;
        Decimal m_INITIALSH;
        String m_HT_AD;
        Decimal m_MAD;
        String m_ORIGINALC;
        Decimal m_STARTBASIC;
        Decimal m_AAD;
        String m_DRIFWAY;
        Decimal m_CONCENTRATIONC;
        Decimal m_CARBON;
        String m_NDAF;
        Decimal m_INDICATED;
        String m_HAD;
        String m_ORIGINALN;
        String m_CDAF;
        Decimal m_INTEGRALNUMBER;
        Decimal m_INITIALSP;
        DateTime m_TESTDATE;
        Decimal m_OVERBASIC;
        Decimal m_FORECAST;
        Decimal m_STD;
        String m_AUTODIRF_H;
        Decimal m_CONCENTRATIONN;
        Decimal m_HYDROGEN;
        String m_LABORATORY;
        Decimal m_NITROGEN;
        String m_HDAF;
        String m_SAMPLENUMBER;
        Decimal m_INITIALSC;
        Decimal m_BLACKH;
        Decimal m_GAS;
        Decimal m_CONCENTRATIONH;
        String m_DRIFTINGH;
        Decimal m_REVISE;
        Decimal m_STAD;
        Decimal m_EXPERIMENTH;
        String m_ORIGINALH;
        String m_NAD;
        String m_STARTDATE;
        String m_ENDDATE;
        String m_AUTONUMBER;
        String m_REMARK;
        Decimal m_ADAMOUNTC;
        String m_DRIFTINGN;
        Decimal m_ENVIRONMENT;
        Decimal m_IDC;
        Decimal m_IsEffective;
        String m_PKID;
        #endregion

        #region 字段属性

        /// <summary>
        /// 
        /// </summary>
        public Decimal EXPERIMENTC
        {
            get
            {
                return m_EXPERIMENTC;
            }
            set
            {
                m_EXPERIMENTC = value;
            }
        }

        /// <summary>
        /// 试样编号
        /// </summary>
        public String HD
        {
            get
            {
                return m_HD;
            }
            set
            {
                m_HD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal INTEGRAL
        {
            get
            {
                return m_INTEGRAL;
            }
            set
            {
                m_INTEGRAL = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String CAD
        {
            get
            {
                return m_CAD;
            }
            set
            {
                m_CAD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String DRIFTINGC
        {
            get
            {
                return m_DRIFTINGC;
            }
            set
            {
                m_DRIFTINGC = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ND
        {
            get
            {
                return m_ND;
            }
            set
            {
                m_ND = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String COMPLETENESS
        {
            get
            {
                return m_COMPLETENESS;
            }
            set
            {
                m_COMPLETENESS = value;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public Decimal BLACKC
        {
            get
            {
                return m_BLACKC;
            }
            set
            {
                m_BLACKC = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal ADAMOUNTH
        {
            get
            {
                return m_ADAMOUNTH;
            }
            set
            {
                m_ADAMOUNTH = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal BLACKN
        {
            get
            {
                return m_BLACKN;
            }
            set
            {
                m_BLACKN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String CD
        {
            get
            {
                return m_CD;
            }
            set
            {
                m_CD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal INITIALSH
        {
            get
            {
                return m_INITIALSH;
            }
            set
            {
                m_INITIALSH = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String HT_AD
        {
            get
            {
                return m_HT_AD;
            }
            set
            {
                m_HT_AD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal MAD
        {
            get
            {
                return m_MAD;
            }
            set
            {
                m_MAD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ORIGINALC
        {
            get
            {
                return m_ORIGINALC;
            }
            set
            {
                m_ORIGINALC = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal STARTBASIC
        {
            get
            {
                return m_STARTBASIC;
            }
            set
            {
                m_STARTBASIC = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal AAD
        {
            get
            {
                return m_AAD;
            }
            set
            {
                m_AAD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String DRIFWAY
        {
            get
            {
                return m_DRIFWAY;
            }
            set
            {
                m_DRIFWAY = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal CONCENTRATIONC
        {
            get
            {
                return m_CONCENTRATIONC;
            }
            set
            {
                m_CONCENTRATIONC = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal CARBON
        {
            get
            {
                return m_CARBON;
            }
            set
            {
                m_CARBON = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NDAF
        {
            get
            {
                return m_NDAF;
            }
            set
            {
                m_NDAF = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public Decimal INDICATED
        {
            get
            {
                return m_INDICATED;
            }
            set
            {
                m_INDICATED = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String HAD
        {
            get
            {
                return m_HAD;
            }
            set
            {
                m_HAD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ORIGINALN
        {
            get
            {
                return m_ORIGINALN;
            }
            set
            {
                m_ORIGINALN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String CDAF
        {
            get
            {
                return m_CDAF;
            }
            set
            {
                m_CDAF = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Decimal INTEGRALNUMBER
        {
            get
            {
                return m_INTEGRALNUMBER;
            }
            set
            {
                m_INTEGRALNUMBER = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal INITIALSP
        {
            get
            {
                return m_INITIALSP;
            }
            set
            {
                m_INITIALSP = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TESTDATE
        {
            get
            {
                return m_TESTDATE;
            }
            set
            {
                m_TESTDATE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal OVERBASIC
        {
            get
            {
                return m_OVERBASIC;
            }
            set
            {
                m_OVERBASIC = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal FORECAST
        {
            get
            {
                return m_FORECAST;
            }
            set
            {
                m_FORECAST = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal STD
        {
            get
            {
                return m_STD;
            }
            set
            {
                m_STD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String AUTODIRF_H
        {
            get
            {
                return m_AUTODIRF_H;
            }
            set
            {
                m_AUTODIRF_H = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal CONCENTRATIONN
        {
            get
            {
                return m_CONCENTRATIONN;
            }
            set
            {
                m_CONCENTRATIONN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal HYDROGEN
        {
            get
            {
                return m_HYDROGEN;
            }
            set
            {
                m_HYDROGEN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String LABORATORY
        {
            get
            {
                return m_LABORATORY;
            }
            set
            {
                m_LABORATORY = value;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public Decimal NITROGEN
        {
            get
            {
                return m_NITROGEN;
            }
            set
            {
                m_NITROGEN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String HDAF
        {
            get
            {
                return m_HDAF;
            }
            set
            {
                m_HDAF = value;
            }
        }

        /// <summary>
        /// 试样编号
        /// </summary>
        public String SAMPLENUMBER
        {
            get
            {
                return m_SAMPLENUMBER;
            }
            set
            {
                m_SAMPLENUMBER = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal INITIALSC
        {
            get
            {
                return m_INITIALSC;
            }
            set
            {
                m_INITIALSC = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal BLACKH
        {
            get
            {
                return m_BLACKH;
            }
            set
            {
                m_BLACKH = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal GAS
        {
            get
            {
                return m_GAS;
            }
            set
            {
                m_GAS = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal CONCENTRATIONH
        {
            get
            {
                return m_CONCENTRATIONH;
            }
            set
            {
                m_CONCENTRATIONH = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String DRIFTINGH
        {
            get
            {
                return m_DRIFTINGH;
            }
            set
            {
                m_DRIFTINGH = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal REVISE
        {
            get
            {
                return m_REVISE;
            }
            set
            {
                m_REVISE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal STAD
        {
            get
            {
                return m_STAD;
            }
            set
            {
                m_STAD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal EXPERIMENTH
        {
            get
            {
                return m_EXPERIMENTH;
            }
            set
            {
                m_EXPERIMENTH = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ORIGINALH
        {
            get
            {
                return m_ORIGINALH;
            }
            set
            {
                m_ORIGINALH = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NAD
        {
            get
            {
                return m_NAD;
            }
            set
            {
                m_NAD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String STARTDATE
        {
            get
            {
                return m_STARTDATE;
            }
            set
            {
                m_STARTDATE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ENDDATE
        {
            get
            {
                return m_ENDDATE;
            }
            set
            {
                m_ENDDATE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String AUTONUMBER
        {
            get
            {
                return m_AUTONUMBER;
            }
            set
            {
                m_AUTONUMBER = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String REMARK
        {
            get
            {
                return m_REMARK;
            }
            set
            {
                m_REMARK = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal ADAMOUNTC
        {
            get
            {
                return m_ADAMOUNTC;
            }
            set
            {
                m_ADAMOUNTC = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String DRIFTINGN
        {
            get
            {
                return m_DRIFTINGN;
            }
            set
            {
                m_DRIFTINGN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal ENVIRONMENT
        {
            get
            {
                return m_ENVIRONMENT;
            }
            set
            {
                m_ENVIRONMENT = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal IDC
        {
            get
            {
                return m_IDC;
            }
            set
            {
                m_IDC = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal ISEFFECTIVE
        {
            get
            {
                return m_IsEffective;
            }
            set
            {
                m_IsEffective = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public String PKID
        {
            get
            {
                return m_PKID;
            }
            set
            {
                m_PKID = value;
            }
        }
        #endregion
    }
}
