using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.DapperDber.Attrs;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.BeltSampler.Entities
{
	/// <summary>
	/// 采样机采样及卸料状态表
	/// </summary>
	[DapperBind("KY_CYJ_P_STATE")]
	public class KY_CYJ_P_STATE
	{
		[CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
		public int Id { get; set; }

		/// <summary>
		/// 采样机编号
		/// </summary>
		public string CYJ_Machine { get; set; }

		/// <summary>
		/// 采样状态  0代表等待采样
		///1代表正在采样
		///2代表采样完成
		///3代表采样异常
		/// </summary>
		public string CY_State { get; set; }

		/// <summary>
		/// 卸料状态  
		///0：默认状态
		///1：需要卸料状态
		///2：开始卸料状态
		///3：正在卸料状态
		///4：卸料完成
		///5：异常
		/// </summary>
		public string XL_State { get; set; }

	}
}
