using CMCS.Common.Entities.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Entities.SampleCabinet
{
    /// <summary>
    /// 销样信息
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("cmcstbcygrejectedsample")]
    public class CmcsSampleClear: EntityBase1
    {
        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplyUser { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 审核人员
        /// </summary>
        public string CheckPle { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime CheckTime { get; set; }

        /// <summary>
        /// 发送弃样命令人员
        /// </summary>
        public string SendCmdpPle { get; set; }

        /// <summary>
        /// 发送弃样命令时间
        /// </summary>
        public DateTime SendCmdTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 审核状态：0未审核，1已审核
        /// </summary>
        public decimal Status { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string WFName { get; set; }

        /// <summary>
        /// 取样站点
        /// </summary>
        public string SamplingStation { get; set; }

        /// <summary>
        /// CS接口端同步标识:0，1未同步
        /// </summary>
        public decimal SyncFlag { get; set; }
    }
}
