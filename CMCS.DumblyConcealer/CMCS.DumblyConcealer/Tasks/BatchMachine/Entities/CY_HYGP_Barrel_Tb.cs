using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
    /// <summary>
    /// 合样归批机接口 - 样桶信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CY_HYGP_Barrel_Tb")]
    public class CY_HYGP_Barrel_Tb
    {
        [CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
        public int Id { get; set; }

        private String _MachineCode;
        /// <summary>
        /// 设备代码：如：HYGP1
        /// </summary>
        public String MachineCode
        {
            get { return _MachineCode; }
            set { _MachineCode = value; }
        }

        private Int32 _BarrelStation;
        /// <summary>
        /// 样桶工位
        /// </summary>
        public Int32 BarrelStation
        {
            get { return _BarrelStation; }
            set { _BarrelStation = value; }
        }

        private Int32 _BarrelCode;
        /// <summary>
        /// 样桶号
        /// </summary>
        public Int32 BarrelCode
        {
            get { return _BarrelCode; }
            set { _BarrelCode = value; }
        }

        private String _SampleID;
        /// <summary>
        /// 样桶码
        /// </summary>
        public String SampleID
        {
            get { return _SampleID; }
            set { _SampleID = value; }
        }

        private decimal _SampleWeight;
        /// <summary>
        /// 样桶重量
        /// </summary>
        public decimal SampleWeight
        {
            get { return _SampleWeight; }
            set { _SampleWeight = value; }
        }

        private Int32 _BarrelStatus;
        /// <summary>
        /// 是否有桶：有桶为1无桶为0
        /// </summary>
        public Int32 BarrelStatus
        {
            get { return _BarrelStatus; }
            set { _BarrelStatus = value; }
        }

        private Int32 _DataStatus;
        /// <summary>
        /// 样桶特征：正常桶为1故障桶为2
        /// </summary>
        public Int32 DataStatus
        {
            get { return _DataStatus; }
            set { _DataStatus = value; }
        }

        private DateTime _StrartTime;
        /// <summary>
        /// 进桶时间
        /// </summary>
        public DateTime StrartTime
        {
            get { return _StrartTime; }
            set { _StrartTime = value; }
        }

        private DateTime _EndTime;
        /// <summary>
        /// 出桶时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
    }
}
