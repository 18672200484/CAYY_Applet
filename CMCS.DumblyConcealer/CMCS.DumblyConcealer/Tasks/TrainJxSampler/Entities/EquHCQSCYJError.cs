using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.TrainJxSampler.Entities
{
    /// <summary>
    /// 火车机械采样机接口 - 故障信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("EquTbHCQSCYJError")]
    public class EquHCQSCYJError : EntityBase2
    {
        private string _ErrorCode;
        private string _ErrorDescribe;
        private DateTime _ErrorTime;
        private int _DataFlag; 

        /// <summary>
        /// 故障代码
        /// </summary>
        public string ErrorCode
        {
            get { return _ErrorCode; }
            set { _ErrorCode = value; }
        }

        /// <summary>
        /// 故障说明
        /// </summary>
        public string ErrorDescribe
        {
            get { return _ErrorDescribe; }
            set { _ErrorDescribe = value; }
        }

        /// <summary>
        /// 故障时间
        /// </summary>
        public DateTime ErrorTime
        {
            get { return _ErrorTime; }
            set { _ErrorTime = value; }
        }

        /// <summary>
        /// 标识符
        /// </summary>
        public int DataFlag
        {
            get { return _DataFlag; }
            set { _DataFlag = value; }
        }
    }
}
