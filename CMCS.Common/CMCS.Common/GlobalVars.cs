﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common
{
	/// <summary>
	/// 全局变量
	/// </summary>
	public static class GlobalVars
	{
		/// <summary>
		/// 管理员账号
		/// </summary>
		public static string AdminAccount = "admin";
		/// <summary>
		/// 公共程序配置键名
		/// </summary>
		public static string CommonAppletConfigName = "公共配置";
		/// <summary>
		/// 第三方设备上位机心跳状态名
		/// </summary>
		public static string EquHeartbeatName = "上位机心跳";

		/// <summary>
		/// 当前登录用户CS
		/// </summary>
		public static CmcsUser LoginUser;

		/// <summary>
		/// 当前登录用户
		/// </summary>
		//public static SysUsers LoginUser;

		#region 皮带采样机

		/// <summary>
		/// 设备编码 - 皮带采样机 #1
		/// </summary>
		public static string MachineCode_PDCYJ_1 = "2PA皮带采样机";

		/// <summary>
		/// 设备编码 - 皮带采样机 #2
		/// </summary>
		public static string MachineCode_PDCYJ_2 = "2PB皮带采样机";

		/// <summary>
		/// 设备编码 - 封装机 #1
		/// </summary>
		public static string MachineCode_PDFZJ_1 = "2PA封装机";

		/// <summary>
		/// 设备编码 - 封装机 #2
		/// </summary>
		public static string MachineCode_PDFZJ_2 = "2PB封装机";

		/// <summary>
		/// 接口类型 - 徐州赛摩皮带采样机
		/// </summary>
		public static string InterfaceType_PDCYJ = "长沙开元皮带采样机";

		/// <summary>
		/// 设备编码 - 开元皮带采样机 #1
		/// </summary>
		public static string MachineCode_PDCYJKY_1 = "PC01";

		/// <summary>
		/// 设备编码 - 开元皮带采样机 #2
		/// </summary>
		public static string MachineCode_PDCYJKY_2 = "PC02";

		#endregion

		#region 火车机械采样机

		/// <summary>
		/// 设备编码 - 火车机械采样机 #1
		/// </summary>
		public static string MachineCode_HCJXCYJ_1 = "#1火车机械采样机";

		/// <summary>
		/// 设备编码 - 火车机械采样机 #2
		/// </summary>
		public static string MachineCode_HCJXCYJ_2 = "#2火车机械采样机";

		/// <summary>
		/// 设备编码 - 火车机械采样机 #3
		/// </summary>
		public static string MachineCode_HCJXCYJ_3 = "#3火车机械采样机";

		/// <summary>
		/// 接口类型 - 火车机械采样机
		/// </summary>
		public static string InterfaceType_HCJXCYJ = "长沙万通火车机械采样机";

		#endregion

		#region 全自动制样机

		/// <summary>
		/// 设备编码 - 全自动制样机 #1
		/// </summary>
		public static string MachineCode_QZDZYJ_1 = "#1全自动制样机";
		/// <summary>
		/// 设备编码 - 在线全水分析仪 
		/// </summary>
		public static string MachineCode_ZXQS_1 = "#1在线全水分析仪";

		/// <summary>
		/// 设备编码 - 合样归批机 #1
		/// </summary>
		public static string MachineCode_HYGPJ_1 = "#1合样归批机";

		/// <summary>
		/// 开元设备编码 - 合样归批机 #1 开元
		/// </summary>
		public static string MachineCode_HYGPJ_KY_1 = "HYGP1";

		/// <summary>
		/// 接口类型 - 全自动制样机
		/// </summary>
		public static string InterfaceType_QZDZYJ = "长沙开元全自动制样机";

		/// <summary>
		/// 开元设备编码 - 全自动制样机 #1 开元
		/// </summary>
		public static string MachineCode_QZDZYJ_KY_1 = "01";

		#endregion

		#region 智能存样柜

		/// <summary>
		/// 设备编码 - 智能存样柜
		/// </summary>
		public static string MachineCode_CYG1 = "#1智能存样柜";

		/// <summary>
		/// 设备编码 - 智能存样柜
		/// </summary>
		public static string MachineCode_CYG2 = "#2智能存样柜";


		/// <summary>
		/// 开元设备编码 - 智能存样柜
		/// </summary>
		public static string MachineCode_CYG1_KY = "ASS001";

		/// <summary>
		/// 开元设备编码 - 智能存样柜
		/// </summary>
		public static string MachineCode_CYG2_KY = "2";
		#endregion

		#region 气动传输

		/// <summary>
		/// 设备编码 - 气动传输
		/// </summary>
		public static string MachineCode_QD = "气动传输";

		#endregion

		#region 轨道衡

		/// <summary>
		/// 设备编码 - #1轨道衡
		/// </summary>
		public static string MachineCode_GDH_1 = "#1动态衡";

		#endregion

		#region 车号识别

		/// <summary>
		/// 设备编码 - #1火车入厂车号识别
		/// </summary>
		public static string MachineCode_HCRCCHSB1 = "#1车号识别";

		/// <summary>
		/// 设备编码 - #2火车入厂车号识别
		/// </summary>
		public static string MachineCode_HCRCCHSB2 = "#2车号识别";

		/// <summary>
		/// 设备编码 - #3火车入厂车号识别
		/// </summary>
		public static string MachineCode_HCRCCHSB3 = "#3车号识别";

		/// <summary>
		/// 设备编码 - #4火车入厂车号识别
		/// </summary>
		public static string MachineCode_HCRCCHSB4 = "#4车号识别";

		/// <summary>
		/// 设备编码 - #5火车入厂车号识别
		/// </summary>
		public static string MachineCode_HCRCCHSB5 = "#5车号识别";

		#endregion

		#region 翻车机

		/// <summary>
		/// 设备编码 - 翻车机 #1
		/// </summary>
		public static string MachineCode_TrunOver_1 = "#1翻车机";

		/// <summary>
		/// 设备编码 - 翻车机 #2
		/// </summary>
		public static string MachineCode_TrunOver_2 = "#2翻车机";

		#endregion

		#region 汽车机械采样机

		/// <summary>
		/// 设备编码 - 汽车机械采样机 #1
		/// </summary>
		public static string MachineCode_QCJXCYJ_1 = "#1汽车机械采样机";

		/// <summary>
		/// 设备编码 - 汽车机械采样机 #2
		/// </summary>
		public static string MachineCode_QCJXCYJ_2 = "#2汽车机械采样机";

		/// <summary>
		/// 设备编码 - 汽车机械采样机 #3
		/// </summary>
		public static string MachineCode_QCJXCYJ_3 = "#3汽车机械采样机";

		/// <summary>
		/// 设备编码 - 汽车机械采样机 #4
		/// </summary>
		public static string MachineCode_QCJXCYJ_4 = "#4汽车机械采样机";

		/// <summary>
		/// 设备编码 - 汽车机械采样机 #5
		/// </summary>
		public static string MachineCode_QCJXCYJ_5 = "#5汽车机械采样机";

		/// <summary>
		/// 设备编码 - 汽车机械采样机 #6
		/// </summary>
		public static string MachineCode_QCJXCYJ_6 = "#6汽车机械采样机";

		/// <summary>
		/// 设备编码 - 汽车机械采样机 #7
		/// </summary>
		public static string MachineCode_QCJXCYJ_7 = "#7汽车机械采样机";

		/// <summary>
		/// 接口类型 - 徐州赛摩汽车机械采样机
		/// </summary>
		public static string InterfaceType_NCGM_QCJXCYJ = "益阳通力汽车机械采样机";

		#endregion

		#region 汽车智能化

		/// <summary>
		/// 设备编码-汽车智能化-入厂端
		/// </summary>
		public static string MachineCode_QC_Queue_1 = "汽车智能化-入厂端";
		/// <summary>
		/// 设备编码-汽车智能化-#1过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_1 = "汽车智能化-#1过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#2过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_2 = "汽车智能化-空车过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#3过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_3 = "汽车智能化-#3过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#4过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_4 = "汽车智能化-#4过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#5过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_5 = "汽车智能化-#5过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#6过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_6 = "汽车智能化-#6过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#7过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_7 = "汽车智能化-#7过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#8过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_8 = "汽车智能化-#8过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#9过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_9 = "汽车智能化-#9过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#1机械采样机端
		/// </summary>
		public static string MachineCode_QC_JxSampler_1 = "汽车智能化-#1机械采样机端";
		/// <summary>
		/// 设备编码-汽车智能化-#2机械采样机端
		/// </summary>
		public static string MachineCode_QC_JxSampler_2 = "汽车智能化-#2机械采样机端";
		/// <summary>
		/// 设备编码-汽车智能化-#3机械采样机端
		/// </summary>
		public static string MachineCode_QC_JxSampler_3 = "汽车智能化-#3机械采样机端";
		/// <summary>
		/// 设备编码-汽车智能化-#4机械采样机端
		/// </summary>
		public static string MachineCode_QC_JxSampler_4 = "汽车智能化-#4机械采样机端";
		/// <summary>
		/// 设备编码-汽车智能化-#5机械采样机端
		/// </summary>
		public static string MachineCode_QC_JxSampler_5 = "汽车智能化-#5机械采样机端";
		/// <summary>
		/// 设备编码-汽车智能化-#6机械采样机端
		/// </summary>
		public static string MachineCode_QC_JxSampler_6 = "汽车智能化-#6机械采样机端";
		/// <summary>
		/// 设备编码-汽车智能化-出厂端
		/// </summary>
		public static string MachineCode_QC_Out_1 = "汽车智能化-出厂端";
		/// <summary>
		/// 设备编码-汽车智能化-#1成品仓
		/// </summary>
		public static string MachineCode_QC_Order_1 = "汽车智能化-#1成品仓";

		#endregion

		#region 化验室网络管理

		/// <summary>
		/// 设备编码 - 化验室网络管理
		/// </summary>
		public static string MachineCode_AssayManage = "化验室网络管理";

		#endregion

		#region 集控首页

		/// <summary>
		/// 信号前缀名-集控首页
		/// </summary>
		public static string MachineCode_HomePage_1 = "集控首页";

		#endregion
	}
}
