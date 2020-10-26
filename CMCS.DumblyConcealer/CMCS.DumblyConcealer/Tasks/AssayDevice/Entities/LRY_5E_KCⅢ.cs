using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.DapperDber.Attrs;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice.Entities
{
    /// <summary>
    /// .量热仪 型号：5E_KCⅢ
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("HYTBLRY")]
    public class LRY_5E_KCⅢ
    {

        #region 成员变量、构造函数
        string m_strTableName;
        Decimal m_YDNUMBER;
        String m_INSTRUMENT;
        String m_M5MIN8018QB;
        Decimal m_GDW;
        String m_QGRAD;
        String m_FASTM5MIN8018IN;
        DateTime m_DATE_EX;
        String m_NUMBER_EX;
        String m_DELTA;
        String m_FASTM8018QB;
        String m_MACHINECODE;
        Decimal m_CONNER;
        String m_NUMDALTA;
        Decimal m_CAPFIRE;
        Decimal m_METHOD;
        String m_MAD;
        String m_QB;
        Decimal m_XA;
        String m_QGRD;
        String m_F_C;
        String m_PORTNO;
        String m_M8018QB;
        String m_FASTM8018IN;
        String m_PLUS2;
        Decimal m_MTCOUNT;
        String m_THECAP;
        String m_NAD;
        Decimal m_TIAN;
        String m_MANCODING;
        Decimal m_TONGCLASS;
        String m_TIANJIA;
        String m_TMAEN;
        String m_FASTIN;
        String m_FASTQB;
        String m_QNETP;
        String m_PLUS1;
        String m_ST;
        String m_TOTAL;
        String m_ZONGHEQB;
        String m_NT;
        DateTime m_TESTTIME;
        String m_TESTMAN;
        Decimal m_XK;
        String m_OAD;
        String m_PKID;
        Decimal m_STANDARD;
        String m_QNETAR;
        String m_QN;
        String m_MAR;
        String m_HAD;
        String m_OLDQB;
        String m_ZONGHEIN;
        String m_FASTZONGHEQB;
        String m_WEIGHT;
        String m_SURPLUS;
        String m_FASTOLDIN;
        String m_FASTZONGHEIN;
        String m_M8018IN;
        String m_FASTOLDQB;
        String m_M5MIN8018IN;
        Decimal m_TESTSTANDARD;
        String m_PLUS3;
        String m_TMABE;
        Decimal m_TRACK;
        String m_MINGCHEN;
        String m_FASTM5MIN8018QB;
        Decimal m_CRITERIONFLAG;
        Decimal m_DIAN;
        String m_NAOH;
        String m_OLDIN;
        Decimal m_RESULTDISABLE;

        /// <summary>
        /// 初始化类 LRY_5E_KCⅢ 的新实例。
        /// </summary>
        public LRY_5E_KCⅢ()
        {
            m_strTableName = "HYTBLRY";
        }
        #endregion

        #region 字段属性
        /// <summary>
        /// 获取实体类对应的数据库表名。
        /// </summary>
        public string TableName
        {
            get
            {
                return m_strTableName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal YDNUMBER
        {
            get
            {
                return m_YDNUMBER;
            }
            set
            {
                m_YDNUMBER = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String INSTRUMENT
        {
            get
            {
                return m_INSTRUMENT;
            }
            set
            {
                m_INSTRUMENT = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String M5MIN8018QB
        {
            get
            {
                return m_M5MIN8018QB;
            }
            set
            {
                m_M5MIN8018QB = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal GDW
        {
            get
            {
                return m_GDW;
            }
            set
            {
                m_GDW = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String QGRAD
        {
            get
            {
                return m_QGRAD;
            }
            set
            {
                m_QGRAD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FASTM5MIN8018IN
        {
            get
            {
                return m_FASTM5MIN8018IN;
            }
            set
            {
                m_FASTM5MIN8018IN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DATE_EX
        {
            get
            {
                return m_DATE_EX;
            }
            set
            {
                m_DATE_EX = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NUMBER_EX
        {
            get
            {
                return m_NUMBER_EX;
            }
            set
            {
                m_NUMBER_EX = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String DELTA
        {
            get
            {
                return m_DELTA;
            }
            set
            {
                m_DELTA = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FASTM8018QB
        {
            get
            {
                return m_FASTM8018QB;
            }
            set
            {
                m_FASTM8018QB = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String MACHINECODE
        {
            get
            {
                return m_MACHINECODE;
            }
            set
            {
                m_MACHINECODE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal CONNER
        {
            get
            {
                return m_CONNER;
            }
            set
            {
                m_CONNER = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NUMDALTA
        {
            get
            {
                return m_NUMDALTA;
            }
            set
            {
                m_NUMDALTA = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal CAPFIRE
        {
            get
            {
                return m_CAPFIRE;
            }
            set
            {
                m_CAPFIRE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal METHOD
        {
            get
            {
                return m_METHOD;
            }
            set
            {
                m_METHOD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String MAD
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
        public String QB
        {
            get
            {
                return m_QB;
            }
            set
            {
                m_QB = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal XA
        {
            get
            {
                return m_XA;
            }
            set
            {
                m_XA = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String QGRD
        {
            get
            {
                return m_QGRD;
            }
            set
            {
                m_QGRD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String F_C
        {
            get
            {
                return m_F_C;
            }
            set
            {
                m_F_C = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String PORTNO
        {
            get
            {
                return m_PORTNO;
            }
            set
            {
                m_PORTNO = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String M8018QB
        {
            get
            {
                return m_M8018QB;
            }
            set
            {
                m_M8018QB = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FASTM8018IN
        {
            get
            {
                return m_FASTM8018IN;
            }
            set
            {
                m_FASTM8018IN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String PLUS2
        {
            get
            {
                return m_PLUS2;
            }
            set
            {
                m_PLUS2 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal MTCOUNT
        {
            get
            {
                return m_MTCOUNT;
            }
            set
            {
                m_MTCOUNT = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String THECAP
        {
            get
            {
                return m_THECAP;
            }
            set
            {
                m_THECAP = value;
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
        public Decimal TIAN
        {
            get
            {
                return m_TIAN;
            }
            set
            {
                m_TIAN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String MANCODING
        {
            get
            {
                return m_MANCODING;
            }
            set
            {
                m_MANCODING = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal TONGCLASS
        {
            get
            {
                return m_TONGCLASS;
            }
            set
            {
                m_TONGCLASS = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String TIANJIA
        {
            get
            {
                return m_TIANJIA;
            }
            set
            {
                m_TIANJIA = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String TMAEN
        {
            get
            {
                return m_TMAEN;
            }
            set
            {
                m_TMAEN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FASTIN
        {
            get
            {
                return m_FASTIN;
            }
            set
            {
                m_FASTIN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FASTQB
        {
            get
            {
                return m_FASTQB;
            }
            set
            {
                m_FASTQB = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String QNETP
        {
            get
            {
                return m_QNETP;
            }
            set
            {
                m_QNETP = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String PLUS1
        {
            get
            {
                return m_PLUS1;
            }
            set
            {
                m_PLUS1 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ST
        {
            get
            {
                return m_ST;
            }
            set
            {
                m_ST = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String TOTAL
        {
            get
            {
                return m_TOTAL;
            }
            set
            {
                m_TOTAL = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ZONGHEQB
        {
            get
            {
                return m_ZONGHEQB;
            }
            set
            {
                m_ZONGHEQB = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NT
        {
            get
            {
                return m_NT;
            }
            set
            {
                m_NT = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TESTTIME
        {
            get
            {
                return m_TESTTIME;
            }
            set
            {
                m_TESTTIME = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String TESTMAN
        {
            get
            {
                return m_TESTMAN;
            }
            set
            {
                m_TESTMAN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal XK
        {
            get
            {
                return m_XK;
            }
            set
            {
                m_XK = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String OAD
        {
            get
            {
                return m_OAD;
            }
            set
            {
                m_OAD = value;
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

        /// <summary>
        /// 
        /// </summary>
        public Decimal STANDARD
        {
            get
            {
                return m_STANDARD;
            }
            set
            {
                m_STANDARD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String QNETAR
        {
            get
            {
                return m_QNETAR;
            }
            set
            {
                m_QNETAR = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String QN
        {
            get
            {
                return m_QN;
            }
            set
            {
                m_QN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String MAR
        {
            get
            {
                return m_MAR;
            }
            set
            {
                m_MAR = value;
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
        public String OLDQB
        {
            get
            {
                return m_OLDQB;
            }
            set
            {
                m_OLDQB = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ZONGHEIN
        {
            get
            {
                return m_ZONGHEIN;
            }
            set
            {
                m_ZONGHEIN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FASTZONGHEQB
        {
            get
            {
                return m_FASTZONGHEQB;
            }
            set
            {
                m_FASTZONGHEQB = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String WEIGHT
        {
            get
            {
                return m_WEIGHT;
            }
            set
            {
                m_WEIGHT = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String SURPLUS
        {
            get
            {
                return m_SURPLUS;
            }
            set
            {
                m_SURPLUS = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FASTOLDIN
        {
            get
            {
                return m_FASTOLDIN;
            }
            set
            {
                m_FASTOLDIN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FASTZONGHEIN
        {
            get
            {
                return m_FASTZONGHEIN;
            }
            set
            {
                m_FASTZONGHEIN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String M8018IN
        {
            get
            {
                return m_M8018IN;
            }
            set
            {
                m_M8018IN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FASTOLDQB
        {
            get
            {
                return m_FASTOLDQB;
            }
            set
            {
                m_FASTOLDQB = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String M5MIN8018IN
        {
            get
            {
                return m_M5MIN8018IN;
            }
            set
            {
                m_M5MIN8018IN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal TESTSTANDARD
        {
            get
            {
                return m_TESTSTANDARD;
            }
            set
            {
                m_TESTSTANDARD = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String PLUS3
        {
            get
            {
                return m_PLUS3;
            }
            set
            {
                m_PLUS3 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String TMABE
        {
            get
            {
                return m_TMABE;
            }
            set
            {
                m_TMABE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal TRACK
        {
            get
            {
                return m_TRACK;
            }
            set
            {
                m_TRACK = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String MINGCHEN
        {
            get
            {
                return m_MINGCHEN;
            }
            set
            {
                m_MINGCHEN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FASTM5MIN8018QB
        {
            get
            {
                return m_FASTM5MIN8018QB;
            }
            set
            {
                m_FASTM5MIN8018QB = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal CRITERIONFLAG
        {
            get
            {
                return m_CRITERIONFLAG;
            }
            set
            {
                m_CRITERIONFLAG = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal DIAN
        {
            get
            {
                return m_DIAN;
            }
            set
            {
                m_DIAN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NAOH
        {
            get
            {
                return m_NAOH;
            }
            set
            {
                m_NAOH = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String OLDIN
        {
            get
            {
                return m_OLDIN;
            }
            set
            {
                m_OLDIN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal RESULTDISABLE
        {
            get
            {
                return m_RESULTDISABLE;
            }
            set
            {
                m_RESULTDISABLE = value;
            }
        }

        #endregion

    }
}
