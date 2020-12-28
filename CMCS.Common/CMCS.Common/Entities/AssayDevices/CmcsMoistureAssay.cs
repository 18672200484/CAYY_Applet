﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.AssayDevices
{
	/// <summary>
	/// 化验数据-水分仪
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("CMCSTBMOISTUREASSAY")]
	public class CmcsMoistureAssay : EntityBase
	{
		/// <summary>
		/// 化验编码
		/// </summary>
		public string SampleNumber { get; set; }

		/// <summary>
		/// 设备编号
		/// </summary>
		public string FacilityNumber { get; set; }

		/// <summary>
		/// 器皿/坩埚编号
		/// </summary>
		public String ContainerNumber { get; set; }

		/// <summary>
		/// 坩埚重量
		/// </summary>
		public decimal ContainerWeight { get; set; }

		/// <summary>
		/// 样品重量
		/// </summary>
		public decimal SampleWeight { get; set; }

		/// <summary>
		/// 烘干后总质量
		/// </summary>
		public decimal DryWeight { get; set; }

		/// <summary>
		/// 水分值
		/// </summary>
		public decimal Mar { get; set; }

		/// <summary>
		/// 化验用户
		/// </summary>
		public string AssayUser { get; set; }

		/// <summary>
		/// 化验日期
		/// </summary>
		public DateTime AssayTime { get; set; }

		/// <summary>
		/// 顺序号
		/// </summary>
		public int OrderNumber { get; set; }

		/// <summary>
		/// 是否有效
		/// </summary>
		public int IsEffective { get; set; }

		/// <summary>
		/// 化验类型
		/// </summary>
		public string WaterType { get; set; }

		/// <summary>
		/// 第三方主键ID
		/// </summary>
		public string PKID { get; set; }

		/// <summary>
		/// 数据来源
		/// </summary>
		public String DataFrom { get; set; }

		/// <summary>
		/// 测定方式
		/// </summary>
		public String DeterMination { get; set; }
	}
}
