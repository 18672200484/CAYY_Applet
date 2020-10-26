using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.DapperDber.Attrs;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice.Entities
{
    /// <summary>
    /// .测硫仪 型号：WS_S500
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("HYTBCLY")]
    public class CLY_WS_S500
    {
        #region 成员变量、构造函数
        String m_PKID;
        String m_MACHINECODE;
        Decimal m_KEY;
        String m_ID;
        Decimal m_CRUCIBLE;
        String m_NAME;
        Decimal m_WEIGHT;
        Decimal m_MAD;
        Decimal m_KC;
        Decimal m_STAD;
        Decimal m_STD;
        Decimal m_METHOD;
        Decimal m_TYPE;
        String m_ASSAYER;
        Decimal m_COUNT;
        DateTime m_DATE1;
        DateTime m_TIME;
        String m_REMARK;
        Decimal m_STATE;
        Decimal m_RESULTDISABLE;
        DateTime m_DATE_EX;
        Decimal m_K;
        Decimal m_B;
        Decimal m_AT;
        Decimal m_RH;


        #endregion

        #region 字段属性
        /// <summary>
        /// 
        /// </summary>
        [DapperPrimaryKey]
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
        public Decimal KEY
        {
            get
            {
                return m_KEY;
            }
            set
            {
                m_KEY = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ID
        {
            get
            {
                return m_ID;
            }
            set
            {
                m_ID = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal CRUCIBLE
        {
            get
            {
                return m_CRUCIBLE;
            }
            set
            {
                m_CRUCIBLE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NAME
        {
            get
            {
                return m_NAME;
            }
            set
            {
                m_NAME = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal WEIGHT
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
        public Decimal KC
        {
            get
            {
                return m_KC;
            }
            set
            {
                m_KC = value;
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
        public Decimal TYPE
        {
            get
            {
                return m_TYPE;
            }
            set
            {
                m_TYPE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ASSAYER
        {
            get
            {
                return m_ASSAYER;
            }
            set
            {
                m_ASSAYER = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal COUNT
        {
            get
            {
                return m_COUNT;
            }
            set
            {
                m_COUNT = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DATE1
        {
            get
            {
                return m_DATE1;
            }
            set
            {
                m_DATE1 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TIME
        {
            get
            {
                return m_TIME;
            }
            set
            {
                m_TIME = value;
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
        public Decimal STATE
        {
            get
            {
                return m_STATE;
            }
            set
            {
                m_STATE = value;
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
        public Decimal K
        {
            get
            {
                return m_K;
            }
            set
            {
                m_K = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal B
        {
            get
            {
                return m_B;
            }
            set
            {
                m_B = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal AT
        {
            get
            {
                return m_AT;
            }
            set
            {
                m_AT = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal RH
        {
            get
            {
                return m_RH;
            }
            set
            {
                m_RH = value;
            }
        }

        #endregion

    }
}
