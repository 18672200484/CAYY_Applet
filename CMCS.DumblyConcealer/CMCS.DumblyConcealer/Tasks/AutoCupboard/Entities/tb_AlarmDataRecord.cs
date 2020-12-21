using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 故障信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("tb_AlarmDataRecord")]
    public class Tb_AlarmDataRecord
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey, CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
        public Int32 Id { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public string MachineCode { get; set; }

        /// <summary>
        /// 操作人员编号
        /// </summary>
        public String Person_ID { get; set; }

        /// <summary>
        /// 工作人员
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 报警代号
        /// </summary>
        public String AlarmNum { get; set; }

        /// <summary>
        /// 报警名称
        /// </summary>
        public String AlarmName { get; set; }

        /// <summary>
        /// 报警数值
        /// </summary>
        public string AlarmValue { get; set; }

        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 0：未读取；1：已读取
        /// </summary>
        public Int32 ReadStatus { get; set; }

        
    }
}
