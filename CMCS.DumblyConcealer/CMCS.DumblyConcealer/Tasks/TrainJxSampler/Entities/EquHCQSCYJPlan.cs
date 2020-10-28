using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.TrainJxSampler.Entities
{
	/// <summary>
	/// 火车机械采样机接口 - 采样计划表
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("EquTbHCQSCYJPlan")]
	public class EquHCQSCYJPlan : EntityBase2
	{
		/// <summary>
		/// 设备编号
		/// </summary>
		public string MachineCode { get; set; }

		/// <summary>
		/// 轨道衡编号
		/// </summary>
		public string TrainCode { get; set; }

		/// <summary>
		/// 批次ID
		/// </summary>
		public string InFactoryBatchId { get; set; }

		/// <summary>
		/// 采样码
		/// </summary>
		public string SampleCode { get; set; }

		/// <summary>
		/// 矿发量
		/// </summary>
		public double TicketWeight { get; set; }

		/// <summary>
		/// 车节数
		/// </summary>
		public int CarCount { get; set; }

		/// <summary>
		/// 采样点
		/// </summary>
		public int CyCount { get; set; }

		/// <summary>
		/// 水分
		/// </summary>
		public double Mt { get; set; }

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
