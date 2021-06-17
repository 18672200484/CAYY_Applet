using CMCS.Common.Entities.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 入炉煤采样表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CmcsTbRlSampling")]
    public class CmcsRLSampling : EntityBase1
    {
        private string _InFurnaceId;

        /// <summary>
        /// 关联批次id
        /// </summary>
        public string InFurnaceId
        {
            get { return _InFurnaceId; }
            set { _InFurnaceId = value; }
        }
        private DateTime _SamplingDate;

        /// <summary>
        /// 采样时间
        /// </summary>
        public DateTime SamplingDate
        {
            get { return _SamplingDate; }
            set { _SamplingDate = value; }
        }
        private string _SamplingPle;

        /// <summary>
        /// 采样人
        /// </summary>
        public string SamplingPle
        {
            get { return _SamplingPle; }
            set { _SamplingPle = value; }
        }
        private string _SamplingType;

        /// <summary>
        /// 采样方式
        /// </summary>
        public string SamplingType
        {
            get { return _SamplingType; }
            set { _SamplingType = value; }
        }
        private string _SampleCode;

        /// <summary>
        /// 采样码
        /// </summary>
        public string SampleCode
        {
            get { return _SampleCode; }
            set { _SampleCode = value; }
        }

        private string _Remark;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

    }

}
