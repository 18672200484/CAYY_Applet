using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.AutoCupboard
{
    /// <summary>
    /// 自动存样柜-命令从表（一次性操作多个样瓶）
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("InfTBCYGCONTROLCMDDETAIL")]
    public class InfCYGControlCMDDetail : EntityBase1
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual String MachineCode { get; set; }
        /// <summary>
        /// 制样明细编码
        /// </summary>
        public virtual String MakeCode { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }
        /// <summary>
        /// 执行结果
        /// </summary>
        public virtual String ResultCode { get; set; }
        /// <summary>
        /// 主表id
        /// </summary>
        public virtual String CYGControlCMDId { get; set; }
        /// <summary>
        /// 样瓶位置
        /// </summary>
        public virtual String Bolt_Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual String Status { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public virtual String Errors { get; set; }

        [DapperDber.Attrs.DapperIgnore]
        public InfCYGControlCMD TheCmcsCYGControlCMD
        {
            get
            {
                return Dbers.GetInstance().SelfDber.Get<InfCYGControlCMD>(this.CYGControlCMDId);
            }
        }
    }
}
