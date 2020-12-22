using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
    /// <summary>
    /// 汽车制样机接口 - 制样机总体状态表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("ZY_Status_Tb")]
    public class ZY_Status_Tb
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

        private Int32 _SamReady;
        /// <summary>
        /// 制样系统状态
        /// 5=可以制样状态
        /// 4=急停状态(需管控下发急停复位或就地复位制样机)
        /// 3=就绪状态(可以卸料状态)
        /// 2=故障(不可以制下一批样)
        /// 1=正在运行中（不可以制下一批样）
        /// 0=停止
        /// </summary>
        public Int32 SamReady
        {
            get { return _SamReady; }
            set { _SamReady = value; }
        }
    }
}
