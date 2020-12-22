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
    [CMCS.DapperDber.Attrs.DapperBind("CMCSTBSAMPLETAKEDETAIL")]
    public class CmcsSampleTakeDetail: EntityBase1
    {
        /// <summary>
        /// 取样主表ID
        /// </summary>
        public string MainId { get; set; }
        /// <summary>
        /// 存样柜名称
        /// </summary>
        public string MachineName { get; set; }
        /// <summary>
        /// 样品编码
        /// </summary>
        public string SampleCode { get; set; }
        /// <summary>
        /// 样品类型
        /// </summary>
        public string SampleType { get; set; }
        /// <summary>
        /// 存样时间
        /// </summary>
        public DateTime InputTime { get; set; }
        /// <summary>
        /// 存样人
        /// </summary>
        public string InputPle { get; set; }
        /// <summary>
        /// 数据标识：0未取样，1取样中，2取样成功，3取样失败
        /// </summary>
        public decimal DataFlag { get; set; }
        /// <summary>
        /// 行
        /// </summary>
        public decimal RowIndex { get; set; }
        /// <summary>
        /// 列
        /// </summary>
        public decimal ColumnIndex { get; set; }
        /// <summary>
        /// 区域标识
        /// </summary>

        public string AreaCode { get; set; }
        /// <summary>
        /// 入厂批次ID
        /// </summary>
        public string BatchId { get; set; }
      
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 存样样重
        /// </summary>
        public string SampleWeight { get; set; }

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
