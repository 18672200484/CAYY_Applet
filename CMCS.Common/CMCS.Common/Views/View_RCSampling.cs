// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
	/// <summary>
	/// 采样视图
	/// </summary>
	[Serializable]
	[CMCS.DapperDber.Attrs.DapperBind("View_RcSampling")]
	public class View_RCSampling : EntityBase2
	{
		/// <summary>
		/// 采样日期
		/// </summary>
		public DateTime SamplingDate { get; set; }

		/// <summary>
		/// 采样码
		/// </summary>
		public string SampleCode { get; set; }

		/// <summary>
		/// 采样类型
		/// </summary>
		public string SamplingType { get; set; }

		/// <summary>
		/// 批次类型
		/// </summary>
		public string BatchType { get; set; }

		/// <summary>
		/// 批次号
		/// </summary>
		public string Batch { get; set; }

		/// <summary>
		/// 批次Id
		/// </summary>
		public string BatchId { get; set; }

		/// <summary>
		/// 到厂时间
		/// </summary>
		public DateTime FactarriveDate { get; set; }

		/// <summary>
		/// 批次创建类型
		/// </summary>
		public string BatchCreateType { get; set; }

		/// <summary>
		/// 车数
		/// </summary>
		public int TransportNumber { get; set; }

		/// <summary>
		/// 供应商名称
		/// </summary>
		public string SupplierName { get; set; }

		/// <summary>
		/// 矿点名称
		/// </summary>
		public string MineName { get; set; }

		/// <summary>
		/// 煤种名称
		/// </summary>
		public string FuelKindName { get; set; }

	}
}
