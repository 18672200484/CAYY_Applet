using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.AutoCupboard
{
    /// <summary>
    /// 自动存样柜-命令主表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("InfTBCYGCONTROLCMD")]
    public class InfCYGControlCMD : EntityBase
    {
        /// <summary>
        /// 存样柜命令编号
        /// </summary>
        public virtual String Bill { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public virtual String OperPerson { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public virtual String OperType { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }
        /// <summary>
        /// 同步标识
        /// </summary>
        public virtual Decimal DataFlag { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual String ReMark { get; set; }

        /// <summary>
        /// 命令处理状态
        /// </summary>
        public virtual String Status { get; set; }
        /// <summary>
        /// 是否运行中
        /// </summary>
        public virtual String CanWorking { get; set; }

        [DapperDber.Attrs.DapperIgnore]
        public List<InfCYGControlCMDDetail> CmdDetails
        { get { return Dbers.GetInstance().SelfDber.Entities<InfCYGControlCMDDetail>("where CYGControlCMDId=:CYGControlCMDId", new { CYGControlCMDId = this.Id }); } }

        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual String MachineCode { get; set; }

        /// <summary>
        /// 目的站
        /// </summary>
        public virtual Int32 Op_Dest { get; set; }

    }
}
