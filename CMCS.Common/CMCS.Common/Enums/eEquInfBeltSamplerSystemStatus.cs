using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
	/// <summary>
	/// 火车皮带采样机接口 - 设备状态
	/// </summary>
	public enum eEquInfBeltSamplerSystemStatus
	{
		等待采样 = 0,
		正在采样 = 1,
		采样完成 = 2,
		采样异常 = 3,
	}
}
