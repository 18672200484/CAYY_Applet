// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using System.ComponentModel;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.CarTransport
{
	/// <summary>
	/// 汽车智能化-火车车型管理
	/// </summary>
	[Serializable]
	[Description("火车车型管理")]
	[CMCS.DapperDber.Attrs.DapperBind("CmcsTbTrainType")]
	public class CmcsTrainType : EntityBase1
	{
		/// <summary>
		/// 车型
		/// </summary>
		public string TypeName { get; set; }

		/// <summary>
		/// 额定载重
		/// </summary>
		public double RatedLoad { get; set; }

		/// <summary>
		/// 车宽
		/// </summary>
		public double TrainWidth { get; set; }

		/// <summary>
		/// 车长
		/// </summary>
		public double TrainLong { get; set; }

		/// <summary>
		/// 车高
		/// </summary>
		public double TrainHeight { get; set; }

		/// <summary>
		/// 是否停用 
		/// </summary>
		public int IsStop { get; set; }

	}
}
