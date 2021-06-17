using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.DumblyConcealer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace CMCS.DumblyConcealer.Tasks.AutoMt
{
    public class EquAutoMtUDP
    {
		/// <summary>
		/// 线程锁
		/// </summary>
		protected object objLock = new object();
		public EquAutoMtUDP(Action<string, eOutputType> output)
		{
			this.MachineCode = GlobalVars.MachineCode_ZXQS_1;
			//InitUDPInfo(output);
		}
		CommonDAO commonDAO = CommonDAO.GetInstance();

		/// <summary>
		/// 设备编码
		/// </summary>
		public string MachineCode;

		#region UDP
		/// <summary>
		/// 目标IP
		/// </summary>
		private string IP;

		/// <summary>
		/// 目标端口
		/// </summary>
		private int Port;

		/// <summary>
		/// 用于UDP发送的网络服务类
		/// </summary>
		private UdpClient udpcSend;

		/// <summary>
		/// 用于UDP接收的网络服务类
		/// </summary>
		private UdpClient udpcRecv;

		/// <summary>
		/// 地址与端口
		/// </summary>
		private IPEndPoint ipPoint;

		/// <summary>
		/// 是否等待接收数据
		/// </summary>
		private bool IsWaitReceiveData = false;

		/// <summary>
		/// 接收到的信息
		/// </summary>
		private string ReceiveData;
		#endregion

		/// <summary>
		/// 是否正在接收数据
		/// </summary>
		public bool IsReceiveData = false;

		/// <summary>
		/// 是否正在发送数据
		/// </summary>
		public bool IsSendData = false;


		private EquAutoMtUDP()
		{ }

		#region 同步智能存样柜数据
		/// <summary>
		/// 初始化UDP
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void InitUDPInfo(Action<string, eOutputType> output)
		{
			IP = CommonDAO.GetInstance().GetCommonAppletConfigString("全自动全水IP");
			Port = CommonDAO.GetInstance().GetCommonAppletConfigInt32("全自动全水端口号");
			// 实名发送
			ipPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
			if (udpcSend != null) udpcSend.Close();
			udpcSend = new UdpClient();//发送UDP实例

			if (udpcRecv != null) udpcRecv.Close();
			udpcRecv = new UdpClient(Port);//接收UDP实例
			output("初始化UDP通讯成功", eOutputType.Normal);
		}

		/// <summary>
		/// 同步数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SyncInfo(Action<string, eOutputType> output)
		{
			int res = 0;
			//if (IsSendData) return 0;

			//byte[] buffer = new byte[10];
			//buffer[0] = 0x01;
			//buffer[1] = 0x00;
			//buffer[2] = 0x0a;
			//buffer[3] = 0x00;
			//buffer[4] = 0x00;
			//buffer[5] = 0x00;
			//buffer[6] = 0x00;
			//buffer[7] = 0x00;
			//buffer[8] = 0x00;
			//buffer[9] = 0x00;

			//SendMessage(buffer);//发送查询命令 01 00 0a 00 00 00 00 00 00 00
			//output(string.Format("发送查询命令"), eOutputType.Normal);

			//Thread.Sleep(500);

			//IsSendData = false;
			return res;
		}

		///// <summary>
		///// 发送信息
		///// </summary>
		///// <param name="obj"></param>
		//private void SendMessage(byte[] message)
		//{
		//	//lock (objLock)
		//	//{
		//		if (udpcSend == null) return;
		//		//IsReceiveData = true;
		//		byte[] sendbytes = message;//Encoding.ASCII.GetBytes(message);
		//		udpcSend.Send(sendbytes, sendbytes.Length, ipPoint);
		//	//IsReceiveData = false;
		//	//}

		//	if (udpcSend == null) return;
		//	IPEndPoint remoteIpep = null;
		//	try
		//	{
		//		byte[] bytRecv = udpcRecv.Receive(ref remoteIpep);
		//		ReceiveData = Encoding.ASCII.GetString(bytRecv, 0, bytRecv.Length);
		//		if (!string.IsNullOrEmpty(ReceiveData))
		//		{
		//			//DataHandle(ReceiveData, Action<string, eOutputType> output);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		//output("接收数据错误：" + ex.Message, eOutputType.Error);
		//	}
		//}

		///// <summary>
		///// 接收数据
		///// </summary>
		///// <param name="obj"></param>
		//public void ReceiveMessage(Action<string, eOutputType> output)
		//{
		//	if (udpcSend == null) return;
		//	IPEndPoint remoteIpep = null;
		//	try
		//	{
		//		byte[] bytRecv = udpcRecv.Receive(ref remoteIpep);
		//		ReceiveData = Encoding.ASCII.GetString(bytRecv, 0, bytRecv.Length);
		//		if (!string.IsNullOrEmpty(ReceiveData))
		//		{
		//			DataHandle(ReceiveData, output);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		output("接收数据错误：" + ex.Message, eOutputType.Error);
		//	}
		//}

		/// <summary>
		/// 接收数据test
		/// </summary>
		/// <param name="obj"></param>
		public void ReceiveMessage(Action<string, eOutputType> output)
		{
			IP = CommonDAO.GetInstance().GetCommonAppletConfigString("全自动全水IP");
			Port = CommonDAO.GetInstance().GetCommonAppletConfigInt32("全自动全水端口号");
			// 实名发送
			ipPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
			if (udpcSend != null) udpcSend.Close();
			udpcSend = new UdpClient();//发送UDP实例

			//IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("192.168.70.225"), 338);
			//UdpClient uc = new UdpClient();

			byte[] bytes = proData();
			udpcSend.Send(bytes, bytes.Length, ipPoint);
			output(string.Format("发送温度取数信号成功"), eOutputType.Normal);

			byte[] bytesre = udpcSend.Receive(ref ipPoint);
			UnPackUdp_data(bytesre);

			output(string.Format("同步温度结果信号成功"), eOutputType.Normal);
		}
		private CmdClientToServer Data()
		{
			CmdClientToServer sd = new CmdClientToServer();
			sd.Header.CmdType = 0;

			return sd;
		}
		private void UnPackUdp_data(byte[] bytes)
		{
			//string str = "报文大小：" + bytes.Length.ToString();
			CmdServerToClient rd = new CmdServerToClient();
			rd = (CmdServerToClient)BytesToStruts(bytes, typeof(CmdServerToClient));

			//str = str + "；实际温度：" + rd.YiQiStatus.CurTemp.ToString() + "。加热状态：" + rd.YiQiStatus.Heat.ToString() + "。补偿温度：" + rd.YiQiStatus.CompTemp.ToString();
			commonDAO.SetSignalDataValue(this.MachineCode, "当前温度", (rd.YiQiStatus.CurTemp/10d).ToString());
			commonDAO.SetSignalDataValue(this.MachineCode, "加热状态", rd.YiQiStatus.Heat==0?"停止":"加热");

			
		}
		private byte[] proData()
		{
			// return Pack(1, 1, Encoding.UTF8.GetBytes(txt_SendMsg.Text.Trim()));
			return StrutsToBytesArray(Data());
		}
		public struct UDP_data
		{
			public Int16 packIndex;//包序号
			public Int16 type;//数据类型
			public Int16 length;//数据长度
								//public byte[] data;//数据体
			public Int16 data;//数据体

		}

		public struct CmdHeader
		{
			public ushort Number;
			public ushort Size;
			public ushort CmdType;//0：获取仪器状态

		}
		public struct CmdTail
		{
			public ushort Reserved1;
			public ushort Reserved2;

		}

		public struct CmdClientToServer
		{
			public CmdHeader Header;
			public CmdTail Tail;

		}

		public struct CmdRespose
		{
			public byte Response;  //0：命令执行不成功1：命令执行成功
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)] public string DesResponse;// public char[] DesResponse=new char[100];
		}

		public struct CmdYiQiStatus
		{
			public UInt16 CurTemp;         //当前实际温度*10
			public UInt16 ReqTemp;         //当前目标温度*10
			public UInt16 CompTemp;        //当前补偿温度*10
			public UInt16 Power;           //当前加热功率（百分比）

			public UInt32 Weight;           //当前天平读数*10000
			public UInt32 WeightOK;         //当前天平读数*10000，上报的质量，计算使用
			public UInt16 WeightFlag;      //收到与称重有关的命令时为0，天平稳定后为1，//为1时说明WeightOk可用

			public UInt16 Heat;            //烘箱加热状态：0=停止，1=加热

			public UInt16 UpperDoor;       //烘箱上面门状态 0=关闭，1=打开
			public UInt16 MiddleDoor;      //烘箱中间门状态 0=关闭，1=打开
			public UInt16 LowerDoor;       //烘箱下面门状态 0=关闭，1=打开

			public UInt16 HoistAction;     //提升电机动作  0=停止，1=前进 2=后退

			public UInt16 DivisionAction;  //缩分电机动作 0=停止，1=启动

			public UInt16 VibrationAction; //振动给料电机动作 0=停止，1=启动

			public UInt16 FlatCylinder;    //摊平机构气缸 0=缩 1=伸

			public UInt16 ManiCylinder;    //机械手伸缩气缸 0=缩，1=升

			public UInt16 HorAction;       //水平机械手运动：0=停止 1=前进 2=后退
			public UInt16 VerAction;       //垂直机械手运动：0=停止 1=前进 2=后退
			public UInt16 HorPosition;     //水平机械手当前位置编号：与表中位置编号相同
			public UInt16 VerPosition;     //垂直机械手当前位置编号：与表中位置编号相同

			public UInt16 VacuumAction;    //真空吸盘动作：0=停止 1=抽真空吸
			public UInt16 VacuumState;     //真空吸盘状态：0=没有吸住 1=已经吸住
			public UInt16 VacuumMotor;     //真空吸盘升降机构动作 0=停止 1=前进 2=后退

			public UInt16 AbandonTurnSensor; //弃样翻转传感器状态(0位=1:恢复，1位=1：翻转)

			public UInt16 bandonClipSensor;  //弃样夹紧气缸传感器状态(0位=1：松开；1位=1：夹紧)

			public UInt16 HoistSensor;     //提升机构位置传感器指示，每个传感器按位存储
										   //0位=1没感应 1位=1感应（底部感应器）
										   //0位=1没感应 1位=1感应（顶部传感器）

			public UInt16 DoorSensor;   //门的气缸位置传感器，每个传感器按位存储 0=没感应1=感应
										//0位=1下面门气缸初始端传感器感应
										//1位=1下面门气缸末端传感器感应
										//2位=1中间门气缸初始端传感器感应
										//3位=1中间门气缸末端传感器感应
										//4位=1上面门气缸初始端传感器感应
										//5位=1上面门气缸末端传感器感应

			public UInt16 FlatSensor;    //摊平机构的气缸位置传感器指示，每个传感器按位存储
										 //0=没感应 1=感应。
										 //0位=1气缸初始端传感器感应
										 //1位=1气缸末端传感器感应

			public UInt16 TraySensor;  //机械手浅盘检测传感器指示，每个传感器按位存储
									   //0=没感应 1=感应。
									   //0位=1机械手浅盘检测传感器感应

			public UInt16 ManiSensor;  //机械手伸缩气缸位置传感器指示，每个传感器按位存储
									   //0=没感应 1=感应。
									   //0位=1气缸初始端传感器感应
									   //1位=1气缸末端传感器感应

			public UInt16 HorSensor;   //水平机械手轨道上位置传感器指示，每个传感器按位存储
									   //0=没感应 1=感应。
									   //0位=1起始端位置传感器感应
									   //1位=1初始位置传感器感应
									   //2位=1末端位置传感器感应

			public UInt16 VerSensor;   //垂直机械手轨道上位置传感器指示，每个传感器按位存储
									   //0=没感应 1=感应。
									   //0位=1起始端位置传感器感应
									   //1位=1初始位置传感器感应
									   //2位=1末端位置传感器感应

			public UInt16 BrushSensor;  //弃样清扫刷伸缩传感器状态
										//0=没感应 1=感应。
										//0位=1缩回
										//1位=1伸出

			public UInt16 ErrorCode;   //故障代码详细见故障代码列表
			public UInt16 DropCoalSensor; //摊平和振动给料传感器状态,按为存储 0=没感应,1=感应
										  //0位，弃样上	 1                                       
										  // 1位，弃样下       2
										  // 2位，弃样中       4
										  // 3位，预留         8
										  // 4位，预留         16
										  // 5位，预留         32
										  // 6位，振动耙子伸   64
										  // 7位，振动耙子缩   128
										  // 8位，振动耙子上   256
										  // 9位，振动耙子下   512
			public UInt16 RestartFlag; //复位标识机械手和系统复位标志（1，正在进行；0，完成）
		}


		public struct CmdServerToClient
		{
			public CmdHeader Header;
			public CmdRespose Response;
			public byte StutusType;     //仪器状态类型目前为固定值1
			public CmdYiQiStatus YiQiStatus;
			public CmdTail Tail;            //增加了4字节的包尾
		}


		/// <summary>
		/// 结构体到byte[]
		/// </summary>
		/// <param name="structObj"></param>
		/// <returns></returns>
		public byte[] StrutsToBytesArray(object structObj)
		{
			//得到结构体的大小
			int size = Marshal.SizeOf(structObj);
			//创建byte数组
			byte[] bytes = new byte[size];
			//分配结构体大小的内存空间
			IntPtr structPtr = Marshal.AllocHGlobal(size);
			//将结构体拷到分配好的内存空间
			Marshal.StructureToPtr(structObj, structPtr, false);
			//从内存空间拷到byte数组
			Marshal.Copy(structPtr, bytes, 0, size);
			//释放内存空间
			Marshal.FreeHGlobal(structPtr);
			//返回byte数组
			return bytes;
		}
		/// <summary>
		/// byte[]到结构体
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public object BytesToStruts(byte[] bytes, Type type)
		{
			//得到结构体的大小
			int size = Marshal.SizeOf(type);
			//byte数组长度小于结构体的大小
			if (size > bytes.Length)
			{
				//返回空
				return null;
			}
			//分配结构体大小的内存空间
			IntPtr structPtr = Marshal.AllocHGlobal(size);
			//将byte数组拷到分配好的内存空间
			Marshal.Copy(bytes, 0, structPtr, size);
			//将内存空间转换为目标结构体
			object obj = Marshal.PtrToStructure(structPtr, type);
			//释放内存空间
			Marshal.FreeHGlobal(structPtr);
			//返回结构体
			return obj;
		}


		///// <summary>
		///// 数据解析
		///// </summary>
		///// <param name="data"></param>
		//public void DataHandle(string data, Action<string, eOutputType> output)
		//{
		//	string ygData = data.Substring(3, 6);




		//}

		public void CloseUDP()
		{
			if (this.udpcSend != null) this.udpcSend.Close();
			if (this.udpcRecv != null) this.udpcRecv.Close();
		}
		#endregion
	}

	class Barrel
	{
		/// <summary>
		/// 样品编码
		/// </summary>
		public string barCode { get; set; }

		/// <summary>
		/// 存样时间
		/// </summary>
		public string dt { get; set; }

		/// <summary>
		/// 操作人
		/// </summary>
		public string login { get; set; }
	}
}
