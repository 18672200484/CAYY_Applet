using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Views
{
    /// <summary>
	/// 翻车机信息视图
	/// </summary>
	[Serializable]
    [CMCS.DapperDber.Attrs.DapperBind("view_CarDumperInfo")]
    public class View_CarDumperInfo
    {
		/// <summary>
		/// 采样码
		/// </summary>
		public string SampleCode { get; set; }

		/// <summary>
		/// 轨道编号
		/// </summary>
		public string TrackNumber { get; set; }

		/// <summary>
		/// 车数
		/// </summary>
		public int CarNum { get; set; }
	}
}
