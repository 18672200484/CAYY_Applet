using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.CarJXSampler.Entities
{
	/// <summary>
	/// 汽车机械采样机接口 - 采样命令表
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("SAMPLE_INTERFACE_DATA")]
	public class Interface_Data
	{
		[CMCS.DapperDber.Attrs.DapperPrimaryKey]
		public string Interface_Id { get; set; }

		/// <summary>
		/// 采样机编号 CY01  CY02
		/// </summary>
		public string Sampler_No { get; set; }

		/// <summary>
		/// 流水号
		/// </summary>
		public string Weighing_Id { get; set; }

		/// <summary>
		/// 车牌号
		/// </summary>
		public string Car_Mark { get; set; }

		/// <summary>
		/// 矿点ID
		/// </summary>
		public string Mine_Id { get; set; }

		/// <summary>
		/// 矿点名称
		/// </summary>
		public string Mine_Name { get; set; }

		/// <summary>
		/// 采样样别id
		/// </summary>
		public string Category_Id { get; set; }

		/// <summary>
		/// 车厢序号
		/// </summary>
		public int Car_No { get; set; }

		/// <summary>
		/// 车厢长 mm
		/// </summary>
		public int Car_Length { get; set; }

		/// <summary>
		/// 车厢宽 mm
		/// </summary>
		public int Car_Width { get; set; }

		/// <summary>
		/// 车厢高 mm
		/// </summary>
		public int Car_Height { get; set; }

		/// <summary>
		/// 车厢底高 mm
		/// </summary>
		public int Chassis_Height { get; set; }

		/// <summary>
		/// 拉筋1 mm
		/// </summary>
		public int Tie_Rod_Place1 { get; set; }

		/// <summary>
		/// 拉筋2 mm
		/// </summary>
		public int Tie_Rod_Place2 { get; set; }

		/// <summary>
		/// 拉筋3 mm
		/// </summary>
		public int Tie_Rod_Place3 { get; set; }

		/// <summary>
		/// 拉筋4 mm
		/// </summary>
		public int Tie_Rod_Place4 { get; set; }

		/// <summary>
		/// 拉筋5 mm
		/// </summary>
		public int Tie_Rod_Place5 { get; set; }

		/// <summary>
		/// 拉筋6 mm
		/// </summary>
		public int Tie_Rod_Place6 { get; set; }

		/// <summary>
		/// 采样点数
		/// </summary>
		public int Point_Count { get; set; }

		/// <summary>
		/// 数据状态 1 准备采样  2 采样完成
		/// </summary>
		public int Data_Status { get; set; }

		/// <summary>
		/// 采样时间
		/// </summary>
		public string Sample_Time { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public string Remark { get; set; }
	}
}
