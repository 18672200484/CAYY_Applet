using CMCS.DapperDber.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.DumblyConcealer.Tasks.BeltSampler.Entities
{
    /// <summary>
	/// 采样机报警表
	/// </summary>
	[DapperBind("KY_CYJ_P_Alarm")]
    public class KY_CYJ_P_Alarm
    {
		//[CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
		//public int Id { get; set; }

		/// <summary>
		/// 时间
		/// </summary>
		public DateTime AlarmDateTime { get; set; }

		/// <summary>
		/// 故障名称
		/// </summary>
		public string VarName { get; set; }

		/// <summary>
		/// 故障描述
		/// </summary>
		public string VarComment { get; set; }
	}
}
