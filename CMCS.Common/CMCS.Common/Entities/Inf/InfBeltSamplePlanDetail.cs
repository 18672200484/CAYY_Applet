using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Inf
{
	/// <summary>
	/// 皮带采样机接口-采样计划详情
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("inftbbeltsampleplanDetail")]
	public class InfBeltSamplePlanDetail : EntityBase
	{
		/// <summary>
		/// 设备编号
		/// </summary>
		public string MchineCode { get; set; }

		/// <summary>
		/// 主表id 
		/// </summary>
		public string PlanId { get; set; }

		/// <summary>
		/// 轨道编号
		/// </summary>
		public string TrainCode { get; set; }

		/// <summary>
		/// 车号
		/// </summary>
		public string CarNumber { get; set; }

		/// <summary>
		/// 车型
		/// </summary>
		public string CarModel { get; set; }

		/// <summary>
		/// 采样点数
		/// </summary>
		public int CyCount { get; set; }

		/// <summary>
		/// 顺序号
		/// </summary>
		public int OrderNumber { get; set; }

		private DateTime startTime;
		/// <summary>
		/// 采样开始时间
		/// </summary>
		public DateTime StartTime
		{
			get { return startTime; }
			set { startTime = value; }
		}

		private DateTime endTime;
		/// <summary>
		/// 采样结束时间
		/// </summary>
		public DateTime EndTime
		{
			get { return endTime; }
			set { endTime = value; }
		}

		private string sampleUser;
		/// <summary>
		/// 采样员
		/// </summary>
		public string SampleUser
		{
			get { return sampleUser; }
			set { sampleUser = value; }
		}

		/// <summary>
		/// 采样坐标点1
		/// </summary>
		public string Point1 { get; set; }

		/// <summary>
		/// 采样坐标点2
		/// </summary>
		public string Point2 { get; set; }

		/// <summary>
		/// 采样坐标点3
		/// </summary>
		public string Point3 { get; set; }

		/// <summary>
		/// 采样坐标点4
		/// </summary>
		public string Point4 { get; set; }

		/// <summary>
		/// 采样坐标点5
		/// </summary>
		public string Point5 { get; set; }

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
