using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 存样柜异常信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("EquTbCYGError")]
    public class EquCYGError : EntityBase2
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public String MachineCode { get; set; }
        /// <summary>
        /// 命令代码
        /// </summary>
        public String ErrorCode { get; set; }
        public String ErrorDescribe { get; set; }
        public DateTime ErrorTime { get; set; }
        public Decimal DataFlag { get; set; }
    }
}
