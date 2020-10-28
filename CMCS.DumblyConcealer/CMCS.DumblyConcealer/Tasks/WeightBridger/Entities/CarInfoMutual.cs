using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CMCS.DumblyConcealer.WeightBridger.Entities
{
	/// <summary>
	/// 翻车衡车辆数据交互
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("CarInfoMutual")]
	public class CarInfoMutual
	{
		public CarInfoMutual()
		{
			Id = Guid.NewGuid().ToString();
			CreateDate = DateTime.Now;
		}

		[DapperDber.Attrs.DapperPrimaryKey]
		public string Id { get; set; }

		/// <summary>
		/// 翻车机号
		/// </summary>
		public string TurnCarNumber { get; set; }

		/// <summary>
		/// 车号
		/// </summary>
		public string CarNumber { get; set; }

		/// <summary>
		/// 采样编码
		/// </summary>
		public string SampleBillNumber { get; set; }

		/// <summary>
		/// 入厂时间
		/// </summary>
		public DateTime InFactoryDate { get; set; }

		/// <summary>
		/// 票重
		/// </summary>
		public double TicketWeight { get; set; }

		/// <summary>
		/// 毛重
		/// </summary>
		public double GrossWeight { get; set; }

		/// <summary>
		/// 皮重
		/// </summary>
		public double TareWeight { get; set; }

		/// <summary>
		/// 净重
		/// </summary>
		public double SuttleWeight { get; set; }

		/// <summary>
		/// 盈亏
		/// </summary>
		public double ProffLossWeight { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate { get; set; }

		/// <summary>
		/// 作废标识 1：数据已作废 0：数据有效
		/// </summary>
		public Int32 CancelSign { get; set; }

		/// <summary>
		/// 计量编号
		/// </summary>
		public string RecordId { get; set; }

		/// <summary>
		/// 数据标识 1：已处理 0：未处理
		/// </summary>
		public Int32 DataFlag { get; set; }

		/// <summary>
		/// 过衡时间
		/// </summary>
		public DateTime WeightDate { get; set; }
	}
}
