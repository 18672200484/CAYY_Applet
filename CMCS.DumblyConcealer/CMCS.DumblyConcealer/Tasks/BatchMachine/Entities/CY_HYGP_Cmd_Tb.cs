using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
    /// <summary>
    /// 合样归批机接口 - 燃管对接命令表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CY_HYGP_Cmd_Tb")]
    public class CY_HYGP_Cmd_Tb
    {
        [CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
        public int Id { get; set; }

        private String _SampleID;
        /// <summary>
        /// 样桶编码
        /// </summary>
        public String SampleID
        {
            get { return _SampleID; }
            set { _SampleID = value; }
        }

        private Int32 _CommandCode;
        /// <summary>
        /// 命令代码
        /// 1为根据编码倒料；5为根据编码取桶不开盖倒料;
        /// </summary>
        public Int32 CommandCode
        {
            get { return _CommandCode; }
            set { _CommandCode = value; }
        }

        private Int32 _DataStatus;
        /// <summary>
        /// 命令特征字
        /// 0为管控插入；
        ///1为归批机已读取并执行此命令；
        ///11为归批机完成此命令，且样桶全部正常卸样；
        ///12为归批机人为强制结束该次流程；
        /// </summary>
        public Int32 DataStatus
        {
            get { return _DataStatus; }
            set { _DataStatus = value; }
        }

        private DateTime _SendTime;
        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime SendTime
        {
            get { return _SendTime; }
            set { _SendTime = value; }
        }

        private DateTime _DateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }
    }
}
