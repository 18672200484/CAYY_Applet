using CMCS.Common.Entities.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Entities.Inf
{
    /// <summary>
	/// 开源皮带采样机接口-采样计划
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("inftbbeltsampleplan_ky")]
    public class InfBeltSamplePlan_KY : EntityBase
    {
		/// <summary>
		/// 设备编号
		/// </summary>
		public string MachineCode { get; set; }

		
		private string sampleCode;
		/// <summary>
		/// 采样码
		/// </summary>
		public string SampleCode
		{
			get { return sampleCode; }
			set { sampleCode = value; }
		}


		private int carCount;
		/// <summary>
		/// 车节数
		/// </summary>
		public int CarCount
		{
			get { return carCount; }
			set { carCount = value; }
		}

		private int dataFlag;
		/// <summary>
		/// 标识符
		/// </summary>
		public int DataFlag
		{
			get { return dataFlag; }
			set { dataFlag = value; }
		}

		private int syncFlag = 0;
		/// <summary>
		/// 同步标识 0=未同步 1=已同步
		/// </summary>
		public int SyncFlag
		{
			get { return syncFlag; }
			set { syncFlag = value; }
		}
	}
}
