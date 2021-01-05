using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;
using CMCS.DapperDber.Attrs;

namespace CMCS.DumblyConcealer.Tasks.BeltSampler.Entities
{
	/// <summary>
	/// 采样机操作表
	/// </summary>
	[DapperBind("KY_CYJ_P_OUTRUN")]
	public class KY_CYJ_P_OUTRUN
	{
		[CMCS.DapperDber.Attrs.DapperAutoPrimaryKey]
		public int Id { get; set; }

		/// <summary>
		/// 采样机号
		/// </summary>
		public string CYJ_Machine { get; set; }

		/// <summary>
		/// 采样编码
		/// </summary>
		public string CY_Code { get; set; }

		/// <summary>
		/// 缩分间隔 单位s
		/// </summary>
		public int Division_Time { get; set; }

		/// <summary>
		/// 缩分次数
		/// </summary>
		public int Division_Count { get; set; }

		/// <summary>
		/// 采样间隔 单位s
		/// </summary>
		public string CY_Time { get; set; }

		/// <summary>
		/// 缩分比
		/// </summary>
		public int CY_SFB { get; set; }

		/// <summary>
		/// 批次号
		/// </summary>
		public string Batch_Number { get; set; }

		/// <summary>
		/// 车皮号码
		/// </summary>
		public string CY_CheHao { get; set; }

		/// <summary>
		/// 采样状态 0代表等待采样
		///1代表正在采样
		///2代表采样完成
		///3代表采样异常
		/// </summary>
		public string CY_State { get; set; }

		/// <summary>
		/// 发送时间
		/// </summary>
		public DateTime Send_Time { get; set; }

		/// <summary>
		/// 命令读取标记位 0：未读取 1：已读取
		/// </summary>
		public int CY_Flag { get; set; }

		/// <summary>
		/// 采样中止标记 0：未读取 1：已读取
		/// </summary>
		public int Stop_Flag { get; set; }

		/// <summary>
		/// 翻车机编号
		/// </summary>
		public string TurnCode { get; set; }

	}
}
