using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 操作记录表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("tb_action")]
    public class Tb_Action
    {
        /// <summary>
        /// 自增序号
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey, CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
        public Int32 Id { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public string MachineCode { get; set; }

        /// <summary>
        /// 瓶底RFID全码
        /// </summary>
        public String Sample_Id { get; set; }

        /// <summary>
        /// 瓶号(在存查样库的位置)
        /// </summary>
        public Int32 Bolt_Id { get; set; }

        /// <summary>
        /// 操作类型 1存样 2 取样 3 弃样
        /// </summary>
        public Int32 Operate_Code { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public Int32 Person_Id { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Operate_Time { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Int32 Priortity { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public String PersonName { get; set; }

        /// <summary>
        /// 读取状态，默认为0
        /// </summary>
        public Int32 STATUS { get; set; }
    }
}
