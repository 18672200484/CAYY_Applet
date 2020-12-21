using CMCS.Common.Entities.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Entities.SampleCabinet
{
    /// <summary>
    /// 取样信息
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CMCSTBSAMPLETAKE")]
    public class CmcsSampleTake : EntityBase1
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
        /// 发送取样命令人员
        /// </summary>
        public string SendcmdPle { get; set; }
        /// <summary>
        /// 发送取样命令时间
        /// </summary>
        public DateTime SendcmdTime { get; set; }
        /// <summary>
        /// 取样说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 审核状态：0未审核，1审核中，99已审核
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>
        public string WFName { get; set; }
        /// <summary>
        /// 取样站点
        /// </summary>
        public string SamplingStation { get; set; }

        private int syncFlag = 0;
        /// <summary>
        /// 同步标识 0=未同步 1=已同步
        /// </summary>
        public int SyncFlag
        {
            get { return syncFlag; }
            set { syncFlag = value; }
        }
    }
}
