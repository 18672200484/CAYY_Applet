using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.DapperDber.Attrs;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice.Entities
{
	/// <summary>
	/// .水分仪 型号：WS_M700
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("hytbsfy")]
	public class SFY_WS_M700
	{
		[DapperPrimaryKey]
		public string PKID { get; set; }

		public string MachineCode { get; set; }
		/// <summary>
		/// 称量瓶位置
		/// </summary>
		public string CZPWZ { get; set; }
		/// <summary>
		/// 试样编号
		/// </summary>
		public string SYBH { get; set; }
		/// <summary>
		/// 试样名称
		/// </summary>
		public string SYMC { get; set; }
		/// <summary>
		/// 煤种
		/// </summary>
		public string MZ { get; set; }
		/// <summary>
		/// 水分
		/// </summary>
		public decimal SF { get; set; }
		/// <summary>
		/// 皮重
		/// </summary>
		public decimal PZ { get; set; }
		/// <summary>
		/// 试样重量
		/// </summary>
		public decimal SYZL { get; set; }
		/// <summary>
		/// 称量瓶残重
		/// </summary>
		public decimal CZPCZ { get; set; }
		/// <summary>
		/// 称量瓶残重1
		/// </summary>
		public decimal CZPCZ1 { get; set; }
		/// <summary>
		/// 称量瓶残重2
		/// </summary>
		public decimal CZPCZ2 { get; set; }
		/// <summary>
		/// 称量瓶残重3
		/// </summary>
		public decimal CZPCZ3 { get; set; }
		/// <summary>
		/// 称量瓶残重4
		/// </summary>
		public decimal CZPCZ4 { get; set; }
		/// <summary>
		/// 称量瓶残重5
		/// </summary>
		public decimal CZPCZ5 { get; set; }
		/// <summary>
		/// 称量瓶残重6
		/// </summary>
		public decimal CZPCZ6 { get; set; }
		/// <summary>
		/// 结果小数位数
		/// </summary>
		public decimal JGXSWS { get; set; }
		/// <summary>
		/// 最低小数取舍
		/// </summary>
		public decimal ZDXSQS { get; set; }
		/// <summary>
		/// 水分平均值
		/// </summary>
		public decimal SFPJZ { get; set; }

		/// <summary>
		/// 测试类别
		/// </summary>
		public string CSLB { get; set; }

		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime KSSJ { get; set; }

		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime JSSJ { get; set; }

		/// <summary>
		/// 化验员
		/// </summary>
		public string HYY { get; set; }

		/// <summary>
		/// 仪器编号
		/// </summary>
		public string YQBH { get; set; }

		/// <summary>
		/// 测试温度
		/// </summary>
		public decimal CSWD { get; set; }

		/// <summary>
		/// 恒温时间
		/// </summary>
		public decimal HWSJ { get; set; }

	}
}
