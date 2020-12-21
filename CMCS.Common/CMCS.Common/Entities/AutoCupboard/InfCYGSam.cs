using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys; 

namespace CMCS.Common.Entities.AutoCupboard
{
    /// <summary>
    /// 实时样品表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("INFTBCYGSAM")]
    public class InfCYGSam : EntityBase
    {
        /// <summary>
        /// 柜号
        /// </summary>
        public virtual String MachineCode { get; set; }
        /// <summary>
        /// 制样编码
        /// </summary>
        public virtual String Code { get; set; }
        /// <summary>
        /// 瓶类型
        /// </summary>
        public virtual String SamType { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }
        /// <summary>
        /// 是否为新
        /// </summary>
        public virtual Decimal IsNew { get; set; }
        /// <summary>
        /// 层
        /// </summary>
        public virtual Int32 CellIndex { get; set; }
        /// <summary>
        /// 列
        /// </summary>
        public virtual Int32 ColumnIndex { get; set; }
        /// <summary>
        /// 区域，2左1右
        /// </summary>
        public virtual Int32 AreaNumber { get; set; }
        public virtual Decimal DataFlag { get; set; }
    }
}
