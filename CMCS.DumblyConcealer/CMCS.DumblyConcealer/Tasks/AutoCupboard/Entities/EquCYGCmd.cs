using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;
using CMCS.Common;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{

    /// <summary>
    /// 智能存样柜-命令表 
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("EquTbCYGCmd")]
    public class EquCYGCmd : EntityBase2
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public String MachineCode { get; set; }
        /// <summary>
        /// 命令代码
        /// </summary>
        public String CmdCode { get; set; }
        /// <summary>
        /// 样品编号
        /// </summary>
        public String SampleCode { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishTime { get; set; }
        /// <summary>
        /// 执行结果
        /// </summary>
        public Int32 ResultCode { get; set; }
        /// <summary>
        /// 标识符
        /// </summary>
        public Int32 DataFlag { get; set; }
    }
}
