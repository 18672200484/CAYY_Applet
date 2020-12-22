using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;
using System.ComponentModel;

namespace CMCS.DumblyConcealer.Tasks.AutoMt.Entities
{
	/// <summary>
	/// 全水测试仪-测试结果
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("YQ_Status")]
	public class YQ_Status
	{
		[CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
		public int Id { get; set; }

		/// <summary>
		/// 0为正常1为启动9为异常状态
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime St_Time { get; set; }

		/// <summary>
		/// 记录详情信息
		/// </summary>
		public string Memo { get; set; }

		/// <summary>
		/// 煤样编码
		/// </summary>
		public string SampleNo { get; set; }

		/// <summary>
		/// 故障代码
		/// </summary>
		public int ErroeCode { get; set; }

	}
}
