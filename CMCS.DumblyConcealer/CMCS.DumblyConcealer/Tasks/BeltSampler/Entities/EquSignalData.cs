using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.DapperDber.Attrs;

namespace CMCS.DumblyConcealer.Tasks.BeltSampler.Entities
{
	/// <summary>
	/// 采样机实时状态表
	/// </summary>
	[DapperBind("KY_CYJ_P_LOG")]
	public class EquSignalData
	{
		[CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
		public int Id { get; set; }

		/// <summary>
		/// 采样编号
		/// </summary>
		public string CYJ_Machine { get; set; }

		/// <summary>
		/// 部件编号
		/// </summary>
		public string Machine_Code { get; set; }

		/// <summary>
		/// 信息类型 0为一般信息
		/// 1为警告
		/// 2为故障
		/// </summary>
		public int Msg_Type { get; set; }

		/// <summary>
		/// 信息代码
		/// </summary>
		public string Msg_Code { get; set; }

		/// <summary>
		/// 信息内容
		/// </summary>
		public string Msg_Content { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime Creat_Time { get; set; }

		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime Edit_Time { get; set; }

		/// <summary>
		/// 信息处理状态 处理完后改为1
		/// </summary>
		public int Msg_State { get; set; }

	}
}
