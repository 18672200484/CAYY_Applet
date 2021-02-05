using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
	/// <summary>
	/// 信号名
	/// </summary>
	public enum eSignalDataName
	{
		#region 综合
		设备状态,
		系统,
		当前车号,
		当前车Id,

		#endregion

		#region 皮带秤

		速度,
		累计量,
		瞬时流量,

		#endregion

		#region 汽车智能化

		当前运输记录Id,

		地感1信号,
		地感2信号,
		地感3信号,
		地感4信号,
		地感5信号,
		地感6信号,
		地感7信号,
		地感8信号,

		对射1信号,
		对射2信号,
		对射3信号,

		信号灯1,
		信号灯2,

		道闸1升杆,
		道闸2升杆,
		道闸3升杆,
		道闸4升杆,

		上磅方向,
		地磅仪表_稳定,
		地磅仪表_实时重量,

		读卡器1_连接状态,
		读卡器2_连接状态,
		LED屏1_连接状态,
		LED屏2_连接状态,
		抓拍相机_连接状态,
		地磅仪表_连接状态,
		IO控制器_连接状态,
		毛重,
		皮重,
		#endregion

		#region 集控
		集控,
		火车入厂车数,
		火车翻车车数,
		火车出厂车数,
		已翻车数,
		来船量,
		汽车转运车数,
		汽车入厂车数,
		汽车采样称重车数,
		汽车回皮车数,
		汽车出厂车数,
		制样合批数,
		存样数,
		待化验数,
		采样编码,
		矿发量,
		采样时间,
		采样点数,
		#endregion

		#region 皮带采样机
		卸料机状态,
		翻车机车数,
		#endregion
	}
}
