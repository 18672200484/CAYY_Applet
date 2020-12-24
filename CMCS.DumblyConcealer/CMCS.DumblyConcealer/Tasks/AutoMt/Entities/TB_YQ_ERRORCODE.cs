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
	/// 全水测试仪-故障编码
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("TB_YQ_ERRORCODE")]
	public class TB_YQ_ERRORCODE
	{
		[CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
		public int Id { get; set; }

		/// <summary>
		/// 故障信息
		/// </summary>
		public string ErrorDes { get; set; }

		/// <summary>
		/// 故障代码
		/// </summary>
		public int ErrorCode { get; set; }
	}
}
