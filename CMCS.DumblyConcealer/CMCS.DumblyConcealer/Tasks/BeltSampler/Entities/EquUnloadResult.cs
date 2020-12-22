using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.DapperDber.Attrs;

namespace CMCS.DumblyConcealer.Tasks.BeltSampler.Entities
{
	/// <summary>
	/// 采样机卸料记录表
	/// </summary>
	[DapperBind("KY_CYJ_P_HANDLE_MATERIAL_RECORD")]
	public class EquUnloadResult
	{
		[CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
		public int Id { get; set; }

		/// <summary>
		/// 采样编号
		/// </summary>
		public string CY_Code { get; set; }

		/// <summary>
		/// 桶号
		/// </summary>
		public string Barrel_Code { get; set; }

		/// <summary>
		/// 缩分装样开始时间
		/// </summary>
		public DateTime Start_Time { get; set; }

		/// <summary>
		/// 缩分装样结束时间
		/// </summary>
		public DateTime End_Time { get; set; }

		/// <summary>
		/// 缩分装样次数
		/// </summary>
		public int Down_Count { get; set; }

		/// <summary>
		/// 缩分装样重量
		/// </summary>
		public int Barrel_Weight { get; set; }

		/// <summary>
		/// 是否满桶 1 是
		/// </summary>
		public int Down_Full { get; set; }

		/// <summary>
		/// 卸样开始时间
		/// </summary>
		public DateTime Start_Time_XL { get; set; }

		/// <summary>
		/// 卸样结束时间
		/// </summary>
		public DateTime End_Time_XL { get; set; }

	}
}
