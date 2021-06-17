using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Monitor.Win.Html
{
	/// <summary>
	/// 页面传输 - 数据类型
	/// </summary>
	public enum eHtmlDataItemType
	{
		/// <summary>
		/// 键值对
		/// </summary>
		key_value = 0,
		/// <summary>
		/// json数据
		/// </summary>
		json_data = 1,
		/// <summary>
		/// SVG文本
		/// </summary>
		svg_text = 2,
		/// <summary>
		/// SVG颜色
		/// </summary>
		svg_color = 3,
		/// <summary>
		/// SVG颜色1
		/// </summary>
		svg_color1 = 31,
		/// <summary>
		/// SVG可见性
		/// </summary>
		svg_visible = 4,
		/// <summary>
		/// SVG宽度
		/// </summary>
		svg_width = 5,
		/// <summary>
		/// SVG动态变色
		/// </summary>
		svg_dyncolor = 6,
		/// <summary>
		/// SVG文本和颜色
		/// </summary>
		svg_textcolor = 7,
		/// <summary>
		/// SVG旋转
		/// </summary>
		svg_scroll = 9999,
		/// <summary>
		/// SVG旋转
		/// </summary>
		svg_scroll2 = 9998,
		/// <summary>
		/// 气动风机SVG旋转
		/// </summary>
		svg_scroll3 = 9997,
		/// <summary>
		/// SCARE旋转
		/// </summary>
		svg_scare = 8999,
		/// <summary>
		/// SVGTemp
		/// </summary>
		svg_Temp = 5050,
		/// <summary>
		/// 按钮字体切换
		/// </summary>
		btn_text = 6789,
		/// <summary>
		/// 按钮显示隐藏
		/// </summary>
		btn_visible = 6790,
		/// <summary>
		/// 按钮颜色切换
		/// </summary>
		btn_color = 6791,
	}

}
