using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
    /// <summary>
	/// 合样归批机接口 - 命令代码
	/// </summary>
    public enum eEquInfBatchMachineCmd
    {
        //为根据编码倒料
        倒料 = 1,
        //为根据编码取桶不开盖倒料
        不开盖倒料 = 2,
        归批命令 = 3,
    }
}
