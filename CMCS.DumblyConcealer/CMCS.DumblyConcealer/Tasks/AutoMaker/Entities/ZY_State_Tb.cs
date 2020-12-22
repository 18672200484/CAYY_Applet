using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
    /// <summary>
    /// 汽车制样机接口 - 制样设备状态表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("ZY_State_Tb")]
    public class ZY_State_Tb
    {
        private String _MachineCode;
        /// <summary>
        /// 制样机编号：APS1（1#制样机）；APS2（2#制样机）
        /// </summary>
        public String MachineCode
        {
            get { return _MachineCode; }
            set { _MachineCode = value; }
        }

        private String _DeviceCode;
        /// <summary>
        /// 详细设备编号 001(破碎机)，002（研磨机）…（10 表示公共设备）
        /// </summary>
        public String DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }

        private String _SampleID;
        /// <summary>
        /// 制样编码
        /// </summary>
        public String SampleID
        {
            get { return _SampleID; }
            set { _SampleID = value; }
        }

        private String _DeviceName;
        /// <summary>
        /// 详细设备名称
        /// </summary>
        public String DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; }
        }

        private Int32 _DeviceStatus;
        /// <summary>
        /// 工作状态
        /// </summary>
        public Int32 DeviceStatus
        {
            get { return _DeviceStatus; }
            set { _DeviceStatus = value; }
        }

        private DateTime _LastUpdateTime;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public DateTime LastUpdateTime
        {
            get { return _LastUpdateTime; }
            set { _LastUpdateTime = value; }
        }

        private Int32 _DataStatus;
        /// <summary>
        /// 工作状态
        /// </summary>
        public Int32 DataStatus
        {
            get { return _DataStatus; }
            set { _DataStatus = value; }
        }

    }
}
