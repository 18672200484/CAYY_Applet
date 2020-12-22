using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 智能存样柜接口表 - 上位机运行状态表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("DATAFLAG")]
    public class EquCYGDataFlag
    {
        private int _DataFlag;
        /// <summary>
        /// 标识符
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public int DataFlag
        {
            get { return _DataFlag; }
            set { _DataFlag = value; }
        }
    }
}
