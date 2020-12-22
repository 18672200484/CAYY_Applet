using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
    /// <summary>
    /// 汽车制样机接口 - 控制信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("ZY_Cmd_Tb")]
    public class ZY_Cmd_Tb
    {
        private String _MachineCode;
        /// <summary>
        /// 制样机编号：APS1（1#制样机）；APS2（2#制样机）
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public String MachineCode
        {
            get { return _MachineCode; }
            set { _MachineCode = value; }
        }

        private Int32 _CommandCode;
        /// <summary>
        /// 命令代码
        /// </summary>
        public Int32 CommandCode
        {
            get { return _CommandCode; }
            set { _CommandCode = value; }
        }

        private String _SampleCode;
        /// <summary>
        /// 制样编码
        /// </summary>
        public String SampleCode
        {
            get { return _SampleCode; }
            set { _SampleCode = value; }
        }

        private DateTime _SendCommandTime;
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendCommandTime
        {
            get { return _SendCommandTime; }
            set { _SendCommandTime = value; }
        }

        private Int32 _DataStatus;
        /// <summary>
        /// 0：未读取；1：已读取（制样机读该命令,读完写1）2:管控系统传输命令不符合要求(制样机需在报警表中告知不符合要求的原因)
        /// </summary>
        public Int32 DataStatus
        {
            get { return _DataStatus; }
            set { _DataStatus = value; }
        }
    }
}
