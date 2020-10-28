using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.TrainJxSampler.Entities
{
	/// <summary>
	/// 火车机械采样机接口 - 采样命令表
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("EquTbHCQSCYJCmd")]
	public class EquHCQSCYJSampleCmd : EntityBase2
	{
		/// <summary>
		/// 设备编号
		/// </summary>
		public string MachineCode { get; set; }

		/// <summary>
		/// 采样码
		/// </summary>
		public string SampleCode { get; set; }

		/// <summary>
		/// 命令代码
		/// </summary>
		public string CmdCode { get; set; }

		private string _ResultCode;
		/// <summary>
		/// 采样结果
		/// </summary>
		public string ResultCode
		{
			get { return _ResultCode; }
			set { _ResultCode = value; }
		}

		private int _DataFlag;
		/// <summary>
		/// 标识符
		/// </summary>
		public int DataFlag
		{
			get { return _DataFlag; }
			set { _DataFlag = value; }
		}
	}
}
