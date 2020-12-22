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
	[CMCS.DapperDber.Attrs.DapperBind("tb_testresult_6550")]
	public class Tb_TestResult
	{
		[CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
		public int Id { get; set; }

		private String _SampleNo;
		/// <summary>
		/// 样品编码
		/// </summary>
		public String SampleNo
		{
			get { return _SampleNo; }
			set { _SampleNo = value; }
		}

		private Int32 _PositionNo;
		/// <summary>
		/// 烘箱编号
		/// </summary>
		public Int32 PositionNo
		{
			get { return _PositionNo; }
			set { _PositionNo = value; }
		}

		private string _SampleName;
		/// <summary>
		/// 样品名称
		/// </summary>
		public string SampleName
		{
			get { return _SampleName; }
			set { _SampleName = value; }
		}

		/// <summary>
		/// 浅盘重量
		/// </summary>
		public decimal TrayWeight { get; set; }

		/// <summary>
		/// 样品重量
		/// </summary>
		public decimal SampleWeight { get; set; }

		/// <summary>
		/// 残重
		/// </summary>
		public decimal LeftWeight { get; set; }

		/// <summary>
		/// 水分
		/// </summary>
		public decimal Moisture { get; set; }

		/// <summary>
		/// 水分损失率
		/// </summary>
		public decimal LossRate { get; set; }

		/// <summary>
		/// 修正水分
		/// </summary>
		public decimal ReviseMoisture { get; set; }

		private DateTime _StartingTime;
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime StartingTime
		{
			get { return _StartingTime; }
			set { _StartingTime = value; }
		}

		private DateTime _EndingTime;
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime EndingTime
		{
			get { return _EndingTime; }
			set { _EndingTime = value; }
		}

		private String _Status;
		/// <summary>
		/// 试验状态 
		/// </summary>
		public String Status
		{
			get { return _Status; }
			set { _Status = value; }
		}

		/// <summary>
		/// 恒温温度
		/// </summary>
		public decimal TempofConstanttemperature { get; set; }

		/// <summary>
		/// 恒温时间
		/// </summary>
		public decimal TimeofConstanttemperature { get; set; }

		/// <summary>
		/// 检查性干燥次数
		/// </summary>
		public decimal TimesOfDrying { get; set; }

		/// <summary>
		/// 检查性干燥时间
		/// </summary>
		public decimal InterValTime { get; set; }

		/// <summary>
		/// 恒温质量
		/// </summary>
		public decimal AnalySisPrecision { get; set; }

		/// <summary>
		/// 编号方式
		/// </summary>
		public string NumberingMethod { get; set; }

		/// <summary>
		/// 操作人
		/// </summary>
		public string Operator { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// 残重1
		/// </summary>
		public decimal LeftWeight1 { get; set; }

		/// <summary>
		/// 残重2
		/// </summary>
		public decimal LeftWeight2 { get; set; }

		/// <summary>
		/// 残重3
		/// </summary>
		public decimal LeftWeight3 { get; set; }

		/// <summary>
		/// 残重4
		/// </summary>
		public decimal LeftWeight4 { get; set; }

		/// <summary>
		/// 残重5
		/// </summary>
		public decimal LeftWeight5 { get; set; }

		/// <summary>
		/// 残重6
		/// </summary>
		public decimal LeftWeight6 { get; set; }

		/// <summary>
		/// 残重7
		/// </summary>
		public decimal LeftWeight7 { get; set; }

		/// <summary>
		/// 残重8
		/// </summary>
		public decimal LeftWeight8 { get; set; }

		/// <summary>
		/// 残重9
		/// </summary>
		public decimal LeftWeight9 { get; set; }

		/// <summary>
		/// 残重10
		/// </summary>
		public decimal LeftWeight10 { get; set; }

		/// <summary>
		/// 恒温干燥开始时间0
		/// </summary>
		public DateTime Time0 { get; set; }

		/// <summary>
		/// 恒温干燥开始时间1
		/// </summary>
		public DateTime Time1 { get; set; }

		/// <summary>
		/// 恒温干燥开始时间2
		/// </summary>
		public DateTime Time2 { get; set; }

		/// <summary>
		/// 恒温干燥开始时间3
		/// </summary>
		public DateTime Time3 { get; set; }

		/// <summary>
		/// 恒温干燥开始时间4
		/// </summary>
		public DateTime Time4 { get; set; }

		/// <summary>
		/// 恒温干燥开始时间5
		/// </summary>
		public DateTime Time5 { get; set; }

		/// <summary>
		/// 恒温干燥开始时间6
		/// </summary>
		public DateTime Time6 { get; set; }

		/// <summary>
		/// 恒温干燥开始时间7
		/// </summary>
		public DateTime Time7 { get; set; }

		/// <summary>
		/// 恒温干燥开始时间8
		/// </summary>
		public DateTime Time8 { get; set; }

		/// <summary>
		/// 恒温干燥开始时间9
		/// </summary>
		public DateTime Time9 { get; set; }

		/// <summary>
		/// 恒温干燥开始时间10
		/// </summary>
		public DateTime Time10 { get; set; }

		/// <summary>
		/// 预留字段
		/// </summary>
		public string ReserVed1 { get; set; }

		/// <summary>
		/// 预留字段
		/// </summary>
		public string ReserVed2 { get; set; }

		/// <summary>
		/// 预留字段
		/// </summary>
		public decimal ReserVed3 { get; set; }

		/// <summary>
		/// 预留字段
		/// </summary>
		public decimal ReserVed4 { get; set; }

		/// <summary>
		/// 预留字段
		/// </summary>
		public decimal ReserVed5 { get; set; }

	}
}
