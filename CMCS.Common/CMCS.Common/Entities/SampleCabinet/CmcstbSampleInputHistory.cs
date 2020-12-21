using CMCS.Common.Entities.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Entities.SampleCabinet
{
    /// <summary>
    /// 存样历史信息
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("cmcstbsampleinputhistory")]
    public class CmcstbSampleInputHistory : EntityBase1
    {
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
        /// 样品数据标识：0有效，1无效
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
        public string sampleweight { get; set; }

        /// <summary>
        /// 操作类型：存样，取样，清样
        /// </summary>
        public string OperationType { get; set; }
    }
}
