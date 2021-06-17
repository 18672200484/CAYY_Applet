using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.IO.Ports;
using System.Threading;
using HslCommunication.Serial;

namespace RW.RF30WRQ80URS01
{
    /// <summary>
    /// 宜科读卡器 RF30WRQ80URS01Rwer
    /// </summary>
    public class RF30WRQ80URS01Rwer
    {
        public RF30WRQ80URS01Rwer()
        {
            timer1 = new System.Timers.Timer(3000)
            {
                AutoReset = true
            };
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);

            //timer2 = new System.Timers.Timer(200)
            //{
            //    AutoReset = true,
            //};
            //timer2.Elapsed += new System.Timers.ElapsedEventHandler(timer2_Elapsed);
        }

        private SerialPort serialPort = new SerialPort();
        private System.Timers.Timer timer1;
        private System.Timers.Timer timer2;

        public delegate void ReceivedEventHandler(List<string> receiveValue);
        public event ReceivedEventHandler OnReceived;
        public delegate void StatusChangeHandler(bool status);
        public event StatusChangeHandler OnStatusChange;

        private bool status = false;
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool Status
        {
            get { return status; }
        }

        /// <summary>
        /// 设置连接状态
        /// </summary>
        /// <param name="status"></param>
        public void SetStatus(bool status)
        {
            if (this.status != status && this.OnStatusChange != null) this.OnStatusChange(status);
            this.status = status;
        }

        /// <summary>
        /// 接收数据次数
        /// </summary>
        private int IOStateCount = 0;

        /// <summary>
        /// 接收到的数据
        /// </summary>
        public List<string> ReceiveValue = new List<string>();

        /// <summary>
        /// 临时数据
        /// </summary>
        private List<byte> ReceiveList = new List<byte>();

        /// <summary>
        /// 当前状态
        /// </summary>
        private string cType = "读卡";

        /// <summary>
        /// 打开串口
        /// 成功返回True;失败返回False;
        /// </summary>
        /// <param name="com">串口号</param>
        /// <param name="bandrate">波特率</param>
        /// <param name="dataBits">数据位</param>
        /// <param name="stopBits">停止位</param>
        /// <param name="parity">奇偶校验位</param>
        /// <returns></returns>
        public bool OpenCom(int com, int bandrate, int dataBits = 8, StopBits stopBits = StopBits.One, Parity parity = Parity.None)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.PortName = "COM" + com.ToString();
                    serialPort.BaudRate = bandrate;
                    serialPort.DataBits = dataBits;
                    serialPort.StopBits = stopBits;
                    serialPort.Parity = parity;
                    serialPort.ReceivedBytesThreshold = 1;
                    serialPort.RtsEnable = true;
                    serialPort.Open();
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

                    timer1.Enabled = true;
                    //timer2.Enabled = true;

                    SetStatus(true);
                }
            }
            catch (Exception)
            {
                this.status = false;
                if (this.OnStatusChange != null) this.OnStatusChange(status);
            }

            return this.status;
        }

        /// <summary>
        /// 关闭串口
        /// 成功返回True;失败返回False;
        /// </summary>
        /// <returns></returns>
        public void CloseCom()
        {
            try
            {
                timer1.Enabled = false;
                timer2.Enabled = false;

                serialPort.DataReceived -= new SerialDataReceivedEventHandler(serialPort_DataReceived);
                //serialPort.Close();
                serialPort.Dispose();
                SetStatus(false);
            }
            catch { }
        }

        /// <summary>
        /// 串口接收数据
        /// 数据示例：49 28 49 2C 30 30 30 30 30 30 31 30 31 30 30 30 30 29
        /// AA 00 17 06 00 10 43 59 32 31 30 32 30 33 31 31 31 30 33 30 33 33 78 AC 55
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.IOStateCount++;

            if (serialPort.IsOpen)
            {
                int bytesToRead = serialPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                serialPort.Read(buffer, 0, bytesToRead);

                try
                {
                    for (int i = 0; i < bytesToRead; i++)
                    {
                        if (buffer[i] == 0xAA) ReceiveList.Clear();

                        this.ReceiveList.Add(buffer[i]);


                        if (cType == "读卡")
                        {
                            //AA 00 17 06 00 10 43 59 32 31 30 33 31 35 36 37 39 30 33 30 33 33 20 9B 55
                            if (buffer[i] == 0x55 && (ReceiveList.Count == 25 || ReceiveList.Count == 8))
                            {
                                this.ReceiveValue.Clear();

                                this.ReceiveValue.Add(GetCmdStatus(buffer[4]));

                                string str = "";
                                for (int j = 6; j < 21; j++)
                                {
                                    str += (char)ReceiveList[j];//Convert.ToString(ReceiveList[j], 10); 
                                }
                                this.ReceiveValue.Add(str.Substring(0, 11));

                                if (this.ReceiveValue.Count == 1)
                                    this.ReceiveValue.Add("");

                                if (OnReceived != null) OnReceived(this.ReceiveValue);

                                ReceiveList.Clear();

                            }

                        }
                        else if (cType == "写卡")
                        {
                            //AA 00 06 07 00 E2 15 55
                            //AA 00 06 07 81 22 75 55

                            if (buffer[i] == 0x55 && (ReceiveList.Count == 8))
                            {
                                this.ReceiveValue.Clear();

                                this.ReceiveValue.Add(GetCmdStatus(buffer[4]));

                                if (OnReceived != null) OnReceived(this.ReceiveValue);

                                ReceiveList.Clear();

                            }
                        }
                    }
                }
                catch { ReceiveList.Clear(); }
                finally { }
            }
        }

        /// <summary>
        /// 输入
        /// </summary>
        /// <param name="pnum">通道号</param>
        /// <param name="type"></param>
        public void Output(int pnum, bool type)
        {
            if (serialPort.IsOpen)
            {
                byte[] buffer = new byte[8];
                buffer[0] = 0x4f;
                buffer[1] = 0x28;
                buffer[2] = 0x30;
                buffer[3] = 0x30;
                buffer[4] = Convert.ToByte((int)(0x30 + pnum));
                buffer[5] = 0x2c;

                if (type)
                    buffer[6] = 0x31;
                else
                    buffer[6] = 0x30;

                buffer[7] = 0x29;
                serialPort.Write(buffer, 0, 8);

            }
        }


        /// <summary>
        /// 读卡
        /// aa 00 08 06 03 00 10 10 9E 55 
        /// </summary>
        public void Read()
        {
            if (serialPort.IsOpen)
            {
                cType = "读卡";

                byte[] buffer = new byte[10];
                buffer[0] = 0xaa;
                buffer[1] = 0x00;
                buffer[2] = 0x08;
                buffer[3] = 0x06;
                buffer[4] = 0x03;
                buffer[5] = 0x00;
                buffer[6] = 0x10;
                buffer[7] = 0x10;
                buffer[8] = 0x9E;
                buffer[9] = 0x55;

                serialPort.Write(buffer, 0, 10);


            }
        }

        /// <summary>
        /// 写卡
        /// aa 00 11 07 03 00 09 43 59 32 31 30 32 30 33 31 E2 D2 55 
        /// </summary>
        public void Write(string code)
        {
            if (serialPort.IsOpen)
            {
                cType = "写卡";

                byte[] data1 = Encoding.Default.GetBytes(code);
                byte[] data = new byte[20];
                Array.Copy(data1, data, data1.Length);

                byte[] senddata = new byte[10 + data.Length];
                senddata[0] = 170;
                senddata[1] = 0;
                senddata[2] = (byte)(8 + data.Length);
                senddata[3] = 7;
                senddata[4] = 3;
                senddata[5] = 0;
                senddata[6] = (byte)data.Length;
                for (int i = 0; i < data.Length; i++)
                {
                    senddata[7 + i] = data[i];
                }
                byte[] crc = new byte[6 + data.Length];
                for (int j = 0; j < crc.Length; j++)
                {
                    crc[j] = senddata[j + 1];
                }
                byte[] crcResult = CRCCalc(crc);
                senddata[7 + data.Length] = crcResult[0];
                senddata[8 + data.Length] = crcResult[1];
                senddata[9 + data.Length] = 85;
                serialPort.Write(senddata, 0, senddata.Length);

                //List<byte> buffer = new List<byte>();

                //buffer.Add(0x00);
                //buffer.Add(0x14);
                //buffer.Add(0x07);
                //buffer.Add(0x03);
                //buffer.Add(0x00);
                //buffer.Add(0x0C);

                //byte[] code_b = HexStringToByteArray(code + "0");
                //foreach (var item in code_b)
                //{
                //    buffer.Add(item);
                //}

                //byte[] data = buffer.ToArray();

                //List<byte> result = HslCommunication.Serial.SoftCRC16.CRC16(data).ToList();

                //result.Add(0x55);

                //byte[] r = new byte[result.Count + 1];
                //r[0] = 0xaa;
                //for (int i = 1; i < result.Count + 1; i++)
                //{
                //    r[i] = (byte)Convert.ToByte(result[i - 1].ToString("X2"), 16);
                //}

                //serialPort.Write(r, 0, result.Count + 1);


            }
        }

        /// <summary>
        /// 间隔事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //if (IOStateCount > 0)
            //    SetStatus(true);
            //else
            //    SetStatus(false);

            //IOStateCount = 0;

            if (serialPort.IsOpen)
            {
                SetStatus(true);
            }
            else
            {
                SetStatus(false);
            }
        }

        /// <summary>
        /// 发送取数指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (serialPort.IsOpen) serialPort.Write("O(100,1)");
        }


        /// <summary>
        /// 字节数组转字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 字符串转字节数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private byte[] HexStringToByteArray(string str)
        {
            string code = "";
            char[] values = str.ToCharArray();
            foreach (char letter in values)
            {
                 code += String.Format("{0:X}", Convert.ToInt32(letter));
            }

            code = code.Replace(" ", "");
            byte[] buffer = new byte[code.Length / 2];
            for (int i = 0; i < code.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(code.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary>
        /// 判断读写状态
        /// 状态字，0x00 表示操作成功， 0x80 表示操作失败，0x81 表示命令有误或是传
        ///输有误，0x01 表示命令正在执行，处于忙状态；
        /// </summary>
        /// <returns></returns>
        public string GetCmdStatus(byte ReceiveValue)
        {
            if (ReceiveValue == 0x00)
            {
                return "操作成功";
            }
            else if(ReceiveValue == 0x80)
            {
                return "操作失败";
            }
            else if (ReceiveValue == 0x81)
            {
                return "命令有误或是传输有误";
            }
            else if (ReceiveValue == 0x01)
            {
                return "命令正在执行，处于忙状态";
            }
            else
            {
                return "操作失败";
            }
            
        }

        public byte[] CRCCalc(byte[] crcbuf)
        {
            int crc = 65535;
            int len = crcbuf.Length;
            for (int i = 0; i < len; i++)
            {
                crc ^= (int)crcbuf[i];
                for (byte j = 0; j < 8; j += 1)
                {
                    int TT = crc & 1;
                    crc >>= 1;
                    crc &= 32767;
                    bool flag = TT == 1;
                    if (flag)
                    {
                        crc ^= 40961;
                    }
                    crc &= 65535;
                }
            }
            byte[] redata = new byte[]
            {
                0,
                (byte)(crc >> 8 & 255)
            };
            redata[0] = (byte)(crc & 255);
            return redata;
        }
    }
}
