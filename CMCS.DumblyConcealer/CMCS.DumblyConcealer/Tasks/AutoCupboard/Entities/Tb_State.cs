using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 设备状态信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("tb_state")]
    public class Tb_State
    {
        ///// <summary>
        ///// 自增主键
        ///// </summary>
        //[CMCS.DapperDber.Attrs.DapperPrimaryKey, CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
        //public Int32 Id { get; set; }

        /// <summary>
        /// 总体设备编号
        /// </summary>
        public String MachineCode { get; set; }

        /// <summary>
        /// 详细设备编号
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public String DeviceCode { get; set; }

        /// <summary>
        /// 制样编码
        /// </summary>
        public String SampleID { get; set; }

        /// <summary>
        /// 详细设备名称
        /// </summary>
        public String DeviceName { get; set; }

        /// <summary>
        /// 工作状态
        /// </summary>
        public Int32 DeviceStatus { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 数据发送状态(0：未读取；1：已读取（接口读取，读完写1）
        /// </summary>
        public Int32 DataStatus { get; set; }

    }
}
