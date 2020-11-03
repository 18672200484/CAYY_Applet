// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using System.ComponentModel;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.CarTransport
{
	/// <summary>
	/// 汽车智能化-火车车厢定位信息
	/// </summary>
	[Serializable]
	[Description("火车车厢定位信息")]
	[CMCS.DapperDber.Attrs.DapperBind("CMCSTBTRANSPORTPOSITION")]
	public class CmcsTransportPosition : EntityBase1
	{
		/// <summary>
		/// 轨道编号
		/// </summary>
		public string TrackNumber { get; set; }

		/// <summary>
		/// 车辆ID
		/// </summary>
		public string TransportId { get; set; }

		/// <summary>
		/// 是否翻车
		/// </summary>
		public int IsDisCharged { get; set; }

		/// <summary>
		/// 翻车时间
		/// </summary>
		public DateTime TurnCarDate { get; set; }

		/// <summary>
		/// 顺序号
		/// </summary>
		public int OrderNumber { get; set; }
	}
}
