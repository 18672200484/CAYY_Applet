using CMCS.DapperDber.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.DumblyConcealer.Tasks.BeltSampler.Entities
{
    /// <summary>
	/// 采样机命令表
	/// </summary>
	[DapperBind("KY_CYJ_P_CMD")]
    public class KY_CYJ_P_CMD
    {
		[CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
		public int Id { get; set; }

		/// <summary>
		/// 命令Id
		/// </summary>
		public string CMDId { get; set; }

		/// <summary>
		/// 设备编号
		/// </summary>
		public string MachineCode { get; set; }

		/// <summary>
		/// 命令代码
		/// </summary>
		public int CmdCode { get; set; }

		private int _ResultCode;
		/// <summary>
		/// 命令结果
		/// </summary>
		public int ResultCode
		{
			get { return _ResultCode; }
			set { _ResultCode = value; }
		}

		/// <summary>
		/// 操作员名
		/// </summary>
		public string OperatorName { get; set; }

		/// <summary>
		/// 发送时间
		/// </summary>
		public DateTime SendDateTime{ get; set; }

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
