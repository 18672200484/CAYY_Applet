using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
	/// <summary>
	/// 火车皮带采样机接口 - 设备状态
	/// </summary>
	public enum eEquInfBeltSamplerUnloadStatus
	{
		默认 = 0,
		需要卸料 = 1,
		开始卸料 = 2,
		正在卸料 = 3,
		卸料完成 = 4,
		卸料异常 = 5
	}
}
