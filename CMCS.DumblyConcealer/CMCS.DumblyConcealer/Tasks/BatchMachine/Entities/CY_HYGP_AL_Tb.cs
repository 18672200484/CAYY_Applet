using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
    /// <summary>
    /// 合样归批接口 - 故障记录表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CY_HYGP_AL_Tb")]
    public class CY_HYGP_AL_Tb
    {
        [CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
        public int Id { get; set; }

        private String _Error_Record;
        /// <summary>
        /// 故障信息
        /// </summary>
        public String Error_Record
        {
            get { return _Error_Record; }
            set { _Error_Record = value; }
        }

        private String _GroupName;
        /// <summary>
        /// 设备
        /// </summary>
        public String GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        private DateTime _Date_Time;
        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime Date_Time
        {
            get { return _Date_Time; }
            set { _Date_Time = value; }
        }

      
    }
}
