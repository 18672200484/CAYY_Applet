using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
	/// <summary>
	/// 汽车制样机接口 - 故障信息表
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("tb_AlarmDataRecord")]
	public class ZY_Error_Tb
	{
		private String _MachineCode;
		/// <summary>
		/// 总体设备编号
		/// </summary>
		public String MachineCode
		{
			get { return _MachineCode; }
			set { _MachineCode = value; }
		}

		private DateTime _DateTime;
		/// <summary>
		/// 故障日期时间
		/// </summary>
		[CMCS.DapperDber.Attrs.DapperPrimaryKey]
		public DateTime DateTime
		{
			get { return _DateTime; }
			set { _DateTime = value; }
		}

		private String _AlarmName;
		/// <summary>
		/// 故障信息
		/// </summary>
		public String AlarmName
		{
			get { return _AlarmName; }
			set { _AlarmName = value; }
		}

		private Int32 _ReadStatus;
		/// <summary>
		/// 数据发送状态 0：未读取；1：已读取（接口读取，读完写1）
		/// </summary>
		public Int32 ReadStatus
		{
			get { return _ReadStatus; }
			set { _ReadStatus = value; }
		}
	}
}
