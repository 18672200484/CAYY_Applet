using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.DapperDber.Attrs;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.BeltSampler.Entities
{
	/// <summary>
	/// 采样机卸料桶状态
	/// </summary>
	[DapperBind("KY_CYJ_P_BARREL")]
	public class KY_CYJ_P_BARREL
	{
		[CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
		public int Id { get; set; }

		/// <summary>
		/// 采样机编号
		/// </summary>
		public string CYJ_Machine { get; set; }

		/// <summary>
		/// 桶号
		/// </summary>
		public string Barrel_Code { get; set; }

		/// <summary>
		/// 批次号
		/// </summary>
		public string Batch_Number { get; set; }

		/// <summary>
		/// 卸料次数
		/// </summary>
		public int Down_Count { get; set; }

		/// <summary>
		/// 是否满了 1 桶满
		/// </summary>
		public int Down_Full { get; set; }

		/// <summary>
		/// 样桶重量
		/// </summary>
		public int Barrel_Weight { get; set; }

		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime EditDate { get; set; }

		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime Start_Time { get; set; }

		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime End_Time { get; set; }

		/// <summary>
		/// 采样码
		/// </summary>
		public string Barrel_Name { get; set; }
	}
}
