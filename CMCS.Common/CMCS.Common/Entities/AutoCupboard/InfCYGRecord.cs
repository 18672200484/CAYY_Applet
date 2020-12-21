using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.AutoCupboard
{
    /// <summary>
    /// 操作记录表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("INFTBCYGRecord")]
    public class InfCYGRecord : EntityBase1
    {
        /// <summary>
        /// 设备编码
        /// </summary>
        public virtual String MachineCode { get; set; }

        /// <summary>
        /// 制样码
        /// </summary>
        public virtual String Code { get; set; }

        /// <summary>
        /// 瓶号(在存查样库的位置)
        /// </summary>
        public Int32 Bolt_Id { get; set; }

        /// <summary>
        /// 操作类型 存样 取样 弃样
        /// </summary>
        public virtual String OperType { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public virtual String OperName { get; set; }

        public virtual Decimal DataFlag { get; set; }

    }
}
