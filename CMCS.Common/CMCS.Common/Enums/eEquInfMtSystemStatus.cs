using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
	/// <summary>
	/// 第三方设备接口 - 全水测试仪系统状态
	/// </summary>
	public enum eEquInfMtSystemStatus
	{
		就绪待机 = 0,
		正在运行 = 1,
		发生故障 = 9,
	}
}
