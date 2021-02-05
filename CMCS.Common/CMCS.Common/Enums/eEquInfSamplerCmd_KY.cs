using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
    /// <summary>
	/// 火车机械采样机接口 - 控制命令
	/// </summary>
    public enum eEquInfSamplerCmd_KY
    {
		报警复位 = 0,
		封装机报警复位 = 1,
		解锁牵车机 = 2,
		解锁皮带 = 3,
		停止采样=4,
		远程出桶 = 5,

	}
}
