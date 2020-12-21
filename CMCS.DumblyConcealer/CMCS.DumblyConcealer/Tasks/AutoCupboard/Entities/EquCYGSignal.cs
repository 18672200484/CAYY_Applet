using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 存样柜设备信号表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("EquTbCYGSignal")]
    public class EquCYGSignal
    {
        [CMCS.DapperDber.Attrs.DapperAutoPrimaryKey, CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public Int32 Id { get; set; }
        public String SSName { get; set; }
        public String JXS_行走 { get; set; }
        public String JXS_升降 { get; set; }
        public String JXS_旋转 { get; set; }
    }
}
