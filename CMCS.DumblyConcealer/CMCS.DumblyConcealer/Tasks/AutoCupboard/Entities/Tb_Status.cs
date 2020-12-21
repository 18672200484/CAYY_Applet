using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 设备状态信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("tb_status")]
    public class Tb_Status
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey, CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
        public Int32 Id { get; set; }

        /// <summary>
        /// 存查柜名
        /// </summary>
        public Int32 SSName { get; set; }

        /// <summary>
        /// 设备总状态
        /// </summary>
        public String M_STATUS { get; set; }

        /// <summary>
        /// 机械手空闲标志位
        /// </summary>
        public Int32 S1 { get; set; }

        /// <summary>
        /// 人工取瓶准备好标志位
        /// </summary>
        public Int32 S2 { get; set; }

        /// <summary>
        /// 自动取瓶准备好标志位
        /// </summary>
        public Int32 S3 { get; set; }

        /// <summary>
        /// 自动存瓶准备好标志位
        /// </summary>
        public Int32 S4 { get; set; }

        /// <summary>
        /// 人工存瓶准备好标志位
        /// </summary>
        public Int32 S5 { get; set; }

        /// <summary>
        /// 人工存瓶启动中标志位
        /// </summary>
        public Int32 S6 { get; set; }

        /// <summary>
        /// 自动取瓶启动中标志位
        /// </summary>
        public Int32 S7 { get; set; }

        /// <summary>
        /// 人工存瓶启动中标志位
        /// </summary>
        public Int32 S8 { get; set; }

        /// <summary>
        /// 异常存瓶中标志位
        /// </summary>
        public Int32 S9 { get; set; }

        /// <summary>
        /// 自动存瓶中标志位
        /// </summary>
        public Int32 S10 { get; set; }

        /// <summary>
        /// 回零中标志位
        /// </summary>
        public Int32 S11 { get; set; }

        /// <summary>
        /// 自动刷新中
        /// </summary>
        public Int32 S12 { get; set; }

        /// <summary>
        /// 自动盘库中
        /// </summary>
        public Int32 S13 { get; set; }

        /// <summary>
        /// 人工弃瓶启动中
        /// </summary>
        public Int32 S14 { get; set; }

        /// <summary>
        /// 气送弃瓶启动中
        /// </summary>
        public Int32 S15 { get; set; }

        /// <summary>
        /// 瓶底RFID全码
        /// </summary>
        public string sample_ID { get; set; }

        /// <summary>
        /// 目的站
        /// </summary>
        public Int32 OP_DEST { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime M_Time { get; set; }

        public Int32 Status { get; set; }
    }
}
