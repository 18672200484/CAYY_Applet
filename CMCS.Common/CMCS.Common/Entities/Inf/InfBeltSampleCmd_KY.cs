﻿using CMCS.Common.Entities.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Entities.Inf
{
    /// <summary>
    /// 开源皮带采样机接口-控制命令
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("inftbbeltsamplecmd_ky")]
    public class InfBeltSampleCmd_KY: EntityBase
    {
        private string machineCode;
        /// <summary>
        /// 设备编号
        /// </summary>
        public string MachineCode
        {
            get { return machineCode; }
            set { machineCode = value; }
        }

        private string cmdCode;
        /// <summary>
        /// 命令代码
        /// </summary>
        public string CmdCode
        {
            get { return cmdCode; }
            set { cmdCode = value; }
        }

        private string resultCode;
        /// <summary>
        /// 执行结果
        /// </summary>
        public string ResultCode
        {
            get { return resultCode; }
            set { resultCode = value; }
        }

        /// <summary>
		/// 操作员名
		/// </summary>
		public string OperatorName { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendDateTime { get; set; }


        private int dataFlag;
        /// <summary>
        /// 标识符
        /// </summary>
        public int DataFlag
        {
            get { return dataFlag; }
            set { dataFlag = value; }
        }

        private int syncFlag = 0;
        /// <summary>
        /// 同步标识 0=未同步 1=已同步
        /// </summary>
        public int SyncFlag
        {
            get { return syncFlag; }
            set { syncFlag = value; }
        }
    }
}
