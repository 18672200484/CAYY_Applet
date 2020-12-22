using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
	/// <summary>
	/// 第三方设备接口 - 全自动制样机系统状态
	/// </summary>
	public enum eEquInfAutoMakerSystemStatus
	{
		设备停止 = 0,
		正在运行 = 1,
		发生故障 = 2,
		允许卸料 = 3,
		设备急停 = 4,
		允许制样 = 5
	}
}
