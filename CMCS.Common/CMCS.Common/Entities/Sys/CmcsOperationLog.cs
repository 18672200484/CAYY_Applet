using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Entities.Sys
{
    /// <summary>
    /// 通用操作日志记录
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CmcsOperationLog")]
    public class CmcsOperationLog : EntityBase
    {
        /// <summary>
        /// 程序唯一标识
        /// </summary>
        public string AppIdentifier { get; set; }
        /// <summary>
        /// 电脑IP
        /// </summary>
        public string MoudleIP { get; set; }
        /// <summary>
        /// 操作事项
        /// </summary>
        public string OperationItems { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperationUser { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
