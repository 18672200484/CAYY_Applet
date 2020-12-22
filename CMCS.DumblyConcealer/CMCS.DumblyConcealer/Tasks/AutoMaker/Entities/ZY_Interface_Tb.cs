using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
	/// <summary>
	/// 汽车制样机接口 - 煤样信息中间表（制样计划表）
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("ZY_Interface_Tb")]
	public class ZY_Interface_Tb
	{
		private String _SampleID;
		/// <summary>
		/// 制样编码
		/// </summary>
		[CMCS.DapperDber.Attrs.DapperPrimaryKey]
		public String SampleID
		{
			get { return _SampleID; }
			set { _SampleID = value; }
		}

		private Int32 _Type;
		/// <summary>
		/// 煤种
		/// </summary>
		public Int32 Type
		{
			get { return _Type; }
			set { _Type = value; }
		}

		private Int32 _Size;
		/// <summary>
		/// 粒度
		/// =1：6mm
		/// =2：3mm
		/// =3：0.2mm
		/// </summary>
		public Int32 Size
		{
			get { return _Size; }
			set { _Size = value; }
		}

		private Int32 _Water;
		/// <summary>
		/// 水分
		/// =1：湿煤
		/// =2：一般湿煤
		/// =3：干煤
		/// =4：一般干煤
		/// </summary>
		public Int32 Water
		{
			get { return _Water; }
			set { _Water = value; }
		}

		private Int32 _DataStatus;
		/// <summary>
		/// 0：未读取；1：已读取（制样机读该命令,读完写1）2:管控系统传输命令不符合要求(制样机需在报警表中告知不符合要求的原因)
		/// </summary>
		public Int32 DataStatus
		{
			get { return _DataStatus; }
			set { _DataStatus = value; }
		}

		/// <summary>
		/// 发送时间
		/// </summary>
		public DateTime SendCommandTime { get; set; }
	}
}
