using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
	/// <summary>
	/// 火车机械采样机接口 - 控制命令
	/// </summary>
	public enum eEquInfSamplerCmd
	{
		开始采样 = 0,
		系统暂停 = 1,
		系统复位 = 2,
		故障复位 = 3,
		切换轨道 = 4,
		首尾车 = 5,
	}
}
