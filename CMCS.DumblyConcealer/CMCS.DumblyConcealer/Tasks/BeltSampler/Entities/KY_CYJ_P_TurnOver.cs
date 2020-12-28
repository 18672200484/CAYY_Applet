using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.DapperDber.Attrs;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.BeltSampler.Entities
{
	/// <summary>
	/// 翻车信息
	/// </summary>
	[DapperBind("KY_CYJ_P_TurnOver")]
	public class KY_CYJ_P_TurnOver
	{
		[CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
		public int Id { get; set; }

		/// <summary>
		/// 采样码
		/// </summary>
		public string CY_Code { get; set; }

		/// <summary>
		/// 车数
		/// </summary>
		public int Car_Count { get; set; }

		/// <summary>
		/// 已翻车数
		/// </summary>
		public int Ready_Count { get; set; }

		/// <summary>
		/// 是否翻车完成
		/// </summary>
		public int IsDone { get; set; }

		/// <summary>
		/// 标识
		/// </summary>
		public int DataFlag { get; set; }

		/// <summary>
		/// 发送时间
		/// </summary>
		public DateTime Send_Time { get; set; }

		/// <summary>
		/// 翻车机编号
		/// </summary>
		public string TurnCode { get; set; }

	}
}
