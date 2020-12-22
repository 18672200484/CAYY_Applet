using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 取、弃样接口表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("tb_preaction")]
    public class Tb_PreAction
    {
        /// <summary>
        /// 自增序号
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey, CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
        public Int32 Id { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public String MachineCode { get; set; }

        /// <summary>
        /// 瓶底RFID全码
        /// </summary>
        public String Sample_Id { get; set; }

        /// <summary>
        /// 操作类型 2取，3弃
        /// </summary>
        public Int32 Operate_Code { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public Int32 Person_Id { get; set; }

        /// <summary>
        /// 瓶id
        /// </summary>
        public Int32 Bolt_Id { get; set; }

        /// <summary>
        /// 1自动弃样2人工弃样4自动取样6人工取样7 自动盘库
        /// </summary>
        public Int32 Priority { get; set; }

        /// <summary>
        /// 0未执行  1正在执行  2执行异常 3出瓶完成  4 执行完成
        /// </summary>
        public Int32 DoneState { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// 目的站
        /// </summary>
        public Int32 OP_DEST { get; set; }

        /// <summary>
        /// 备用
        /// </summary>
        public Int32 OP_STATUS { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime I_Time { get; set; }

        /// <summary>
        /// 读取状态，默认为0
        /// </summary>
        public Int32 ReadState { get; set; }

    }
}
