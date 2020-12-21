using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 智能存样柜--iv.	实时样品表 
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("EquTbCYGSample")]
    public class EquCYGSample : EntityBase2
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public String MachineCode { get; set; }
        public String CupboardCode { get; set; }
        /// <summary>
        /// 样品编码
        /// </summary>
        public String SampleCode { get; set; }
        /// <summary>
        /// 样品类型
        /// </summary>
        public String SampleType { get; set; }
        public Int32 CellIndex { get; set; }
        public Int32 ColumnIndex { get; set; }
        public Int32 AreaNumber { get; set; }
        public DateTime DepositTime { get; set; }
        public Int32 DataFlag { get; set; }
    }
}
