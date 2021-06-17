using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CMCS.Common
{
	/// <summary>
	/// 设备元件状态标准色值
	/// </summary>
	public static class EquipmentStatusColors
	{
		/// <summary>
		/// 运行状态
		/// </summary>
		public static Color Working = ColorTranslator.FromHtml("#00c000");
		/// <summary>
		/// 就绪状态
		/// </summary>
		public static Color BeReady = ColorTranslator.FromHtml("#c0c0c0");
		/// <summary>
		/// 故障状态
		/// </summary>
		public static Color Breakdown = ColorTranslator.FromHtml("#ff0000");
		/// <summary>
		/// 停用状态
		/// </summary>
		public static Color Forbidden = ColorTranslator.FromHtml("#c0c0c0");
		/// <summary>
		/// 一般状态
		/// </summary>
		public static Color Normal = ColorTranslator.FromHtml("#FFFFFF");
	}
}
