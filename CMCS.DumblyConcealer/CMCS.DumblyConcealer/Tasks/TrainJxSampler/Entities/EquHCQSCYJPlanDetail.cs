using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.TrainJxSampler.Entities
{
	/// <summary>
	/// 火车机械采样机接口 - 采样计划详情表
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("EquTbHCQSCYJPlanDetail")]
	public class EquHCQSCYJPlanDetail : EntityBase2
	{
		/// <summary>
		/// 设备编号
		/// </summary>
		public string MachineCode { get; set; }

		/// <summary>
		/// 主表ID
		/// </summary>
		public string PlanId { get; set; }

		/// <summary>
		/// 车号
		/// </summary>
		public string CarNumber { get; set; }

		/// <summary>
		/// 车型
		/// </summary>
		public string CarModel { get; set; }

		/// <summary>
		/// 采样点数
		/// </summary>
		public int CyCount { get; set; }

		/// <summary>
		/// 顺序号
		/// </summary>
		public int OrderNumber { get; set; }

		/// <summary>
		/// 采样开始时间
		/// </summary>
		public DateTime StartTime { get; set; }

		/// <summary>
		/// 采样结束时间
		/// </summary>
		public DateTime EndTime { get; set; }

		/// <summary>
		/// 采样员
		/// </summary>
		public string SampleUser { get; set; }

		private int _DataFlag;
		/// <summary>
		/// 标识符
		/// </summary>
		public int DataFlag
		{
			get { return _DataFlag; }
			set { _DataFlag = value; }
		}
	}
}
