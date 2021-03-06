using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
//
using CMCS.CarTransport.DAO;
using CMCS.CarTransport.Weighter.Core;
using CMCS.CarTransport.Weighter.Enums;
using CMCS.CarTransport.Weighter.Frms.Sys;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using DevComponents.DotNetBar;
using EQ2008_DataStruct;
using HikVisionSDK.Core;
using LED.EQ2013;

namespace CMCS.CarTransport.Weighter.Frms
{
    public partial class FrmWeighter : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmWeighter";

        public FrmWeighter()
        {
            InitializeComponent();
        }

        #region Vars

        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
        WeighterDAO weighterDAO = WeighterDAO.GetInstance();
        CommonDAO commonDAO = CommonDAO.GetInstance();

        /// <summary>
        /// 等待上传的抓拍
        /// </summary>
        Queue<string> waitForUpload = new Queue<string>();

        IocControler iocControler;
        /// <summary>
        /// 语音播报
        /// </summary>
        VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

        public static int g_iCardNum = 1;      //控制卡地址

        public static int g_iProgramIndex = 0;

        public static int timingvalue = 0;

        public static int timing1 = 0;

        public static int timing = 0;

        bool inductorCoil1 = false;
        /// <summary>
        /// 地感1状态 true=有信号  false=无信号
        /// </summary>
        public bool InductorCoil1
        {
            get
            {
                return inductorCoil1;
            }
            set
            {
                if (inductorCoil1 != value)
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地感1信号.ToString(), value ? "1" : "0");

                inductorCoil1 = value;

                panCurrentWeight.Refresh();
            }
        }

        int inductorCoil1Port;
        /// <summary>
        /// 地感1端口
        /// </summary>
        public int InductorCoil1Port
        {
            get { return inductorCoil1Port; }
            set { inductorCoil1Port = value; }
        }

        bool inductorCoil2 = false;
        /// <summary>
        /// 地感2状态 true=有信号  false=无信号
        /// </summary>
        public bool InductorCoil2
        {
            get
            {
                return inductorCoil2;
            }
            set
            {
                if (inductorCoil2 != value)
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地感2信号.ToString(), value ? "1" : "0");
                inductorCoil2 = value;

                panCurrentWeight.Refresh();

            }
        }

        int inductorCoil2Port;
        /// <summary>
        /// 地感2端口
        /// </summary>
        public int InductorCoil2Port
        {
            get { return inductorCoil2Port; }
            set { inductorCoil2Port = value; }
        }

        bool infraredSensor1 = false;
        /// <summary>
        /// 对射1状态 true=遮挡  false=连通
        /// </summary>
        public bool InfraredSensor1
        {
            get
            {
                return infraredSensor1;
            }
            set
            {
                if (infraredSensor1 != value)
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.对射1信号.ToString(), value ? "1" : "0");

                infraredSensor1 = value;

                panCurrentWeight.Refresh();
            }
        }

        int infraredSensor1Port;
        /// <summary>
        /// 对射1端口
        /// </summary>
        public int InfraredSensor1Port
        {
            get { return infraredSensor1Port; }
            set { infraredSensor1Port = value; }
        }

        bool infraredSensor2 = false;
        /// <summary>
        /// 对射2状态 true=遮挡  false=连通
        /// </summary>
        public bool InfraredSensor2
        {
            get
            {
                return infraredSensor2;
            }
            set
            {
                if (infraredSensor2 != value)
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.对射2信号.ToString(), value ? "1" : "0");

                infraredSensor2 = value;

                panCurrentWeight.Refresh();
            }
        }

        int infraredSensor2Port;
        /// <summary>
        /// 对射2端口
        /// </summary>
        public int InfraredSensor2Port
        {
            get { return infraredSensor2Port; }
            set { infraredSensor2Port = value; }
        }

        bool wbSteady = false;
        /// <summary>
        /// 地磅仪表稳定状态
        /// </summary>
        public bool WbSteady
        {
            get { return wbSteady; }
            set
            {
                if (wbSteady != value)
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地磅仪表_稳定.ToString(), value ? "1" : "0");

                wbSteady = value;

                this.panCurrentWeight.Style.ForeColor.Color = (value ? Color.Lime : Color.Red);

                panCurrentWeight.Refresh();
            }
        }

        double wbMinWeight = 0;
        /// <summary>
        /// 地磅仪表最小称重 单位：吨
        /// </summary>
        public double WbMinWeight
        {
            get { return wbMinWeight; }
            set
            {
                wbMinWeight = value;
            }
        }

        bool autoHandMode = true;
        /// <summary>
        /// 自动模式=true  手动模式=false
        /// </summary>
        public bool AutoHandMode
        {
            get { return autoHandMode; }
            set
            {
                autoHandMode = value;

                btnSelectAutotruck_BuyFuel.Visible = !value;
                btnSelectAutotruck_Goods.Visible = !value;

                btnSaveTransport_BuyFuel.Visible = !value;
                btnSaveTransport_Goods.Visible = !value;

                btnReset_BuyFuel.Visible = !value;
                btnReset_Goods.Visible = !value;

                if (CommonAppConfig.GetInstance().AppIdentifier.Contains("空"))
                {
                    btnSendSamplePlan.Visible = false;
                }
                else
                {
                    btnSendSamplePlan.Visible = !value;
                }
            }
        }

        public static PassCarQueuer passCarQueuer = new PassCarQueuer();

        ImperfectCar currentImperfectCar;
        /// <summary>
        /// 识别或选择的车辆凭证
        /// </summary>
        public ImperfectCar CurrentImperfectCar
        {
            get { return currentImperfectCar; }
            set
            {
                currentImperfectCar = value;

                if (value != null)
                    panCurrentCarNumber.Text = value.Voucher;
                else
                    panCurrentCarNumber.Text = "等待车辆";
            }
        }

        eDirection currentDirection;
        /// <summary>
        /// 当前上磅方向
        /// </summary>
        public eDirection CurrentDirection
        {
            get { return currentDirection; }
            set { currentDirection = value; }
        }

        string direction;
        /// <summary>
        /// 固定上磅方向
        /// </summary>
        public string Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                if (value == "双向磅")
                {
                    slightRwer2.Visible = true;
                    label_Rwer2.Visible = true;
                }
                else if (value == "单向磅")
                {
                    slightRwer2.Visible = false;
                    label_Rwer2.Visible = false;
                }
            }
        }

        eFlowFlag currentFlowFlag = eFlowFlag.等待车辆;
        /// <summary>
        /// 当前业务流程标识
        /// </summary>
        public eFlowFlag CurrentFlowFlag
        {
            get { return currentFlowFlag; }
            set
            {
                currentFlowFlag = value;

                lblFlowFlag.Text = value.ToString();
            }
        }

        CmcsAutotruck currentAutotruck;
        /// <summary>
        /// 当前车
        /// </summary>
        public CmcsAutotruck CurrentAutotruck
        {
            get { return currentAutotruck; }
            set
            {
                currentAutotruck = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), value.CarNumber);

                    CmcsEPCCard ePCCard = Dbers.GetInstance().SelfDber.Get<CmcsEPCCard>(value.EPCCardId);
                    if (value.CarType == eCarType.入厂煤.ToString())
                    {
                        if (ePCCard != null) txtTagId_BuyFuel.Text = ePCCard.TagId;

                        txtCarNumber_BuyFuel.Text = value.CarNumber;
                        superTabControl2.SelectedTab = superTabItem_BuyFuel;
                    }
                    else if (value.CarType == eCarType.其他物资.ToString())
                    {
                        if (ePCCard != null) txtTagId_Goods.Text = ePCCard.TagId;

                        txtCarNumber_Goods.Text = value.CarNumber;
                        superTabControl2.SelectedTab = superTabItem_Goods;
                    }

                    panCurrentCarNumber.Text = value.CarNumber;
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), string.Empty);

                    txtCarNumber_BuyFuel.ResetText();
                    //txtCarNumber_SaleFuel.ResetText();
                    txtCarNumber_Goods.ResetText();

                    txtTagId_BuyFuel.ResetText();
                    //txtTagId_SaleFuel.ResetText();
                    txtTagId_Goods.ResetText();

                    panCurrentCarNumber.ResetText();
                }
            }
        }
        private string samplerMachineCode;
        public string SamplerMachineCode
        {
            get { return samplerMachineCode; }
            set { samplerMachineCode = value; }
        }

        private InfQCJXCYSampleCMD currentSampleCMD;
        /// <summary>
        /// 当前采样命令
        /// </summary>
        public InfQCJXCYSampleCMD CurrentSampleCMD
        {
            get { return currentSampleCMD; }
            set { currentSampleCMD = value; }
        }

        #endregion

        /// <summary>
        /// 窗体初始化
        /// </summary>
        private void InitForm()
        {
            lblFlowFlag.ForeColor = Color.White;
            FrmDebugConsole.GetInstance();
            SamplerMachineCode = commonDAO.GetAppletConfigString("采样机编码");
            // 默认自动
            sbtnChangeAutoHandMode.Value = true;

            timingvalue = commonDAO.GetAppletConfigInt32("落杆延时");

            // 重置程序远程控制命令
            commonDAO.ResetAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);

            btnRefresh_Click(null, null);

            if (CommonAppConfig.GetInstance().AppIdentifier.Contains("空"))
            {
                btnSendSamplePlan.Visible = false;
                slightCYJ.Visible = false;
                lblCYJ.Visible = false;
            }
        }

        private void FrmWeighter_Load(object sender, EventArgs e)
        {

        }

        private void FrmWeighter_Shown(object sender, EventArgs e)
        {
            Direction = commonDAO.GetAppletConfigString("上磅方向");
            InitHardware();

            InitForm();
        }

        private void FrmQueuer_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 卸载设备
            UnloadHardware();
        }

        #region 设备相关

        #region IO控制器

        void Iocer_StatusChange(bool status)
        {
            // 接收设备状态 
            InvokeEx(() =>
            {
                slightIOC.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.IO控制器_连接状态.ToString(), status ? "1" : "0");
            });
        }

        /// <summary>
        /// IO控制器接收数据时触发
        /// </summary>
        /// <param name="receiveValue"></param>
        void Iocer_Received(int[] receiveValue)
        {
            // 接收地感状态  
            InvokeEx(() =>
            {
                this.InductorCoil1 = (receiveValue[this.InductorCoil1Port - 1] == 1);
                this.InductorCoil2 = (receiveValue[this.InductorCoil2Port - 1] == 1);
                this.InfraredSensor1 = (receiveValue[this.InfraredSensor1Port - 1] == 1);
                this.InfraredSensor2 = (receiveValue[this.InfraredSensor2Port - 1] == 1);
            });
        }

        /// <summary>
        /// 前方升杆
        /// </summary>
        void FrontGateUp()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
            {
                this.iocControler.Gate2Up();
                this.iocControler.GreenLight2();
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
            {
                this.iocControler.Gate1Up();
                this.iocControler.GreenLight1();
            }
        }

        /// <summary>
        /// 前方降杆
        /// </summary>
        void FrontGateDown()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
            {
                if (!this.InfraredSensor2)
                    this.iocControler.Gate2Down();
                else
                    Log4Neter.Info("对射2有信号，前方无法降杆");

                this.iocControler.RedLight2();
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
            {
                if (!this.InfraredSensor1)
                    this.iocControler.Gate1Down();
                else
                    Log4Neter.Info("对射1有信号，前方无法降杆");

                this.iocControler.RedLight1();
            }
        }

        /// <summary>
        /// 后方升杆
        /// </summary>
        void BackGateUp()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
            {
                this.iocControler.Gate1Up();
                this.iocControler.GreenLight1();
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
            {
                this.iocControler.Gate2Up();
                this.iocControler.GreenLight2();
            }
        }

        /// <summary>
        /// 后方降杆
        /// </summary>
        void BackGateDown()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
            {
                if (!this.InfraredSensor1)
                    this.iocControler.Gate1Down();
                else
                    Log4Neter.Info("对射1有信号，后方无法降杆");

                this.iocControler.RedLight1();
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
            {
                if (!this.InfraredSensor2)
                    this.iocControler.Gate2Down();
                else
                    Log4Neter.Info("对射2有信号，后方无法降杆");

                this.iocControler.RedLight2();
            }
        }

        #endregion

        #region 读卡器

        void Rwer1_OnScanError(Exception ex)
        {
            Log4Neter.Error("读卡器1", ex);
        }

        void Rwer1_OnStatusChange(bool status)
        {
            // 接收设备状态 
            InvokeEx(() =>
            {
                slightRwer1.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.读卡器1_连接状态.ToString(), status ? "1" : "0");
            });
        }

        void Rwer2_OnScanError(Exception ex)
        {
            Log4Neter.Error("读卡器2", ex);
        }

        void Rwer2_OnStatusChange(bool status)
        {
            // 接收设备状态 
            InvokeEx(() =>
            {
                slightRwer2.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.读卡器2_连接状态.ToString(), status ? "1" : "0");
            });
        }

        #endregion

        #region 语音提示
        /// <summary>
        /// 更新语音播报信息
        /// </summary>
        private void UpdateSpeaker(string value, int count, bool reset)
        {
            this.voiceSpeaker.Speak(value, count, reset);
            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "语音播放信息", value);
        }
        #endregion

        #region LED显示屏

        /// <summary>
        /// 更新LED动态区域
        /// </summary>
        /// <param name="value1">第一行内容</param>
        /// <param name="value2">第二行内容</param>
        private void UpdateLedShow(string value1 = "", string value2 = "")
        {
            UpdateLed1Show(value1, value2);
        }

        #region LED1控制卡

        private bool _LED1ConnectStatus;
        /// <summary>
        /// LED1连接状态
        /// </summary>
        public bool LED1ConnectStatus
        {
            get
            {
                return _LED1ConnectStatus;
            }

            set
            {
                _LED1ConnectStatus = value;

                slightLED1.LightColor = (value ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.LED屏1_连接状态.ToString(), value ? "1" : "0");
            }
        }

        /// <summary>
        /// LED1显示内容文本
        /// </summary>
        string LED1TempFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Led1TempFile.txt");

        /// <summary>
        /// LED1上一次显示内容
        /// </summary>
        string LED1PrevLedFileContent = string.Empty;

        /// <summary>
        /// 更新LED动态区域
        /// </summary>
        /// <param name="value1">第一行内容</param>
        /// <param name="value2">第二行内容</param>
        private void UpdateLed1Show(string value1 = "", string value2 = "")
        {
            if (!this.LED1ConnectStatus) return;
            if (this.LED1PrevLedFileContent == value1 + value2) return;
            FrmDebugConsole.GetInstance().Output("更新LED1:|" + value1 + "|" + value2 + "|");

            this.LED1PrevLedFileContent = value1 + value2;
            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "LED屏显示信息", this.LED1PrevLedFileContent);


            EQ2013.User_DelAllProgram(g_iCardNum);
            g_iProgramIndex = EQ2013.User_AddProgram(g_iCardNum, false, 10);

            User_Text Text = new User_Text();

            Text.BkColor = 0;
            Text.chContent = value1 + "\n" + value2;

            Text.PartInfo.FrameColor = 0;
            Text.PartInfo.iFrameMode = 0;
            Text.PartInfo.iHeight = 64;
            Text.PartInfo.iWidth = 128;
            Text.PartInfo.iX = 0;
            Text.PartInfo.iY = 0;

            Text.FontInfo.bFontBold = false;
            Text.FontInfo.bFontItaic = false;
            Text.FontInfo.bFontUnderline = false;
            Text.FontInfo.colorFont = 0xFF;
            Text.FontInfo.iFontSize = 9;
            Text.FontInfo.strFontName = "宋体";
            Text.FontInfo.iAlignStyle = 0;
            Text.FontInfo.iVAlignerStyle = 0;
            Text.FontInfo.iRowSpace = 0;
            Text.FontInfo.iAlignStyle = 1;

            Text.MoveSet.bClear = false;
            Text.MoveSet.iActionSpeed = 1;
            Text.MoveSet.iActionType = 1;
            Text.MoveSet.iHoldTime = 200;
            Text.MoveSet.iClearActionType = 0;
            Text.MoveSet.iClearSpeed = 4;
            Text.MoveSet.iFrameTime = 20;
            int count = EQ2013.User_AddText(g_iCardNum, ref Text, g_iProgramIndex);
            EQ2013.User_SendToScreen(g_iCardNum);
        }

        #endregion

        #endregion

        #region 地磅仪表

        /// <summary>
        /// 重量稳定事件
        /// </summary>
        /// <param name="steady"></param>
        void Wber_OnSteadyChange(bool steady)
        {
            InvokeEx(() =>
            {
                this.WbSteady = steady;
            });
        }

        /// <summary>
        /// 地磅仪表状态变化
        /// </summary>
        /// <param name="status"></param>
        void Wber_OnStatusChange(bool status)
        {
            // 接收设备状态 
            InvokeEx(() =>
            {
                slightWber.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地磅仪表_连接状态.ToString(), status ? "1" : "0");
            });
        }

        void Wber_OnWeightChange(double weight)
        {
            InvokeEx(() =>
            {
                panCurrentWeight.Text = weight.ToString();
            });
        }

        #endregion

        #region 设备初始化与卸载

        /// <summary>
        /// 初始化外接设备
        /// </summary>
        private void InitHardware()
        {
            try
            {
                bool success = false;

                this.InductorCoil1Port = commonDAO.GetAppletConfigInt32("IO控制器_地感1端口");
                this.InductorCoil2Port = commonDAO.GetAppletConfigInt32("IO控制器_地感2端口");
                this.InfraredSensor1Port = commonDAO.GetAppletConfigInt32("IO控制器_对射1端口");
                this.InfraredSensor2Port = commonDAO.GetAppletConfigInt32("IO控制器_对射2端口");

                this.WbMinWeight = commonDAO.GetAppletConfigDouble("地磅仪表_最小称重");

                // IO控制器
                Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
                success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("IO控制器_串口"), commonDAO.GetAppletConfigInt32("IO控制器_波特率"), commonDAO.GetAppletConfigInt32("IO控制器_数据位"), (StopBits)commonDAO.GetAppletConfigInt32("IO控制器_停止位"), (Parity)commonDAO.GetAppletConfigInt32("IO控制器_校验位"));
                if (!success) MessageBoxEx.Show("IO控制器连接失败！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.iocControler = new IocControler(Hardwarer.Iocer);

                // 地磅仪表
                Hardwarer.Wber.OnStatusChange += new WB.TOLEDO.IND245.TOLEDO_IND245Wber.StatusChangeHandler(Wber_OnStatusChange);
                Hardwarer.Wber.OnSteadyChange += new WB.TOLEDO.IND245.TOLEDO_IND245Wber.SteadyChangeEventHandler(Wber_OnSteadyChange);
                Hardwarer.Wber.OnWeightChange += new WB.TOLEDO.IND245.TOLEDO_IND245Wber.WeightChangeEventHandler(Wber_OnWeightChange);
                success = Hardwarer.Wber.OpenCom(commonDAO.GetAppletConfigInt32("地磅仪表_串口"), commonDAO.GetAppletConfigInt32("地磅仪表_波特率"), commonDAO.GetAppletConfigInt32("地磅仪表_数据位"), (StopBits)commonDAO.GetAppletConfigInt32("地磅仪表_停止位"), (Parity)commonDAO.GetAppletConfigInt32("地磅仪表_校验位"), commonDAO.GetAppletConfigString("地磅仪表_类型"));

                // 读卡器1
                Hardwarer.Rwer1.StartWith = commonDAO.GetAppletConfigString("读卡器_标签过滤");
                Hardwarer.Rwer1.OnStatusChange += new RW.LZR12.Lzr12Rwer.StatusChangeHandler(Rwer1_OnStatusChange);
                Hardwarer.Rwer1.OnScanError += new RW.LZR12.Lzr12Rwer.ScanErrorEventHandler(Rwer1_OnScanError);
                success = Hardwarer.Rwer1.OpenCom(commonDAO.GetAppletConfigInt32("读卡器1_串口"));
                if (!success) MessageBoxEx.Show("读卡器1连接失败！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (this.Direction == "双向磅")
                {
                    // 读卡器2
                    Hardwarer.Rwer2.StartWith = commonDAO.GetAppletConfigString("读卡器_标签过滤");
                    Hardwarer.Rwer2.OnStatusChange += new RW.LZR12.Lzr12Rwer.StatusChangeHandler(Rwer2_OnStatusChange);
                    Hardwarer.Rwer2.OnScanError += new RW.LZR12.Lzr12Rwer.ScanErrorEventHandler(Rwer2_OnScanError);
                    success = Hardwarer.Rwer2.OpenCom(commonDAO.GetAppletConfigInt32("读卡器2_串口"));
                    if (!success) MessageBoxEx.Show("读卡器2连接失败！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                #region LED控制卡

                //建立连接
                if (EQ2013.User_RealtimeConnect(g_iCardNum))
                {
                    // 初始化成功
                    this.LED1ConnectStatus = true;

                    UpdateLed1Show("  等待车辆");
                }
                else
                {
                    this.LED1ConnectStatus = false;
                    MessageBoxEx.Show("LED1控制卡连接失败！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                #endregion

                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                Log4Neter.Error("设备初始化", ex);
            }
        }

        /// <summary>
        /// 卸载设备
        /// </summary>
        private void UnloadHardware()
        {
            // 注意此段代码
            Application.DoEvents();

            try
            {
                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), string.Empty);
                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), string.Empty);
            }
            catch { }
            try
            {
                Hardwarer.Iocer.OnReceived -= new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange -= new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);

                Hardwarer.Iocer.CloseCom();
            }
            catch { }
            //try
            //{
            //    Hardwarer.Wber.OnStatusChange -= new WB.TOLEDO.IND245.TOLEDO_IND245Wber.StatusChangeHandler(Wber_OnStatusChange);
            //    Hardwarer.Wber.OnSteadyChange -= new WB.TOLEDO.IND245.TOLEDO_IND245Wber.SteadyChangeEventHandler(Wber_OnSteadyChange);
            //    Hardwarer.Wber.OnWeightChange -= new WB.TOLEDO.IND245.TOLEDO_IND245Wber.WeightChangeEventHandler(Wber_OnWeightChange);

            //    Hardwarer.Wber.CloseCom();
            //}
            //catch { }
            try
            {
                Hardwarer.Rwer1.CloseCom();
            }
            catch { }
            try
            {
                if (this.Direction == "双向磅")
                    Hardwarer.Rwer2.CloseCom();
            }
            catch { }
            //try
            //{
            //    if (this.LED1ConnectStatus)
            //    {
            //        EQ2013.User_CloseScreen(g_iCardNum);
            //    }
            //}
            //catch { }
        }

        #endregion

        #endregion

        #region 道闸控制按钮

        /// <summary>
        /// 道闸1升杆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate1Up_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate1Up();
        }

        /// <summary>
        /// 道闸1降杆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate1Down_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate1Down();
        }

        /// <summary>
        /// 道闸2升杆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate2Up_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate2Up();
        }

        /// <summary>
        /// 道闸2降杆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate2Down_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate2Down();
        }

        #endregion

        #region 公共业务

        /// <summary>
        /// 读卡、车号识别任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Interval = 2000;

            try
            {
                // 执行远程命令
                ExecAppRemoteControlCmd();

                if (commonDAO.GetAppletConfigString("启动过衡") == "0")
                {
                    UpdateLed1Show("停止过磅");
                    return;
                }

                switch (this.CurrentFlowFlag)
                {
                    case eFlowFlag.等待车辆:
                        #region
                        UpdateLed1Show("等待车辆");
                        this.CurrentFlowFlag = eFlowFlag.开始读卡;

                        #endregion
                        break;

                    case eFlowFlag.开始读卡:
                        #region
                        List<string> tags1 = new List<string>();
                        List<string> tags2 = new List<string>();
                        tags1 = Hardwarer.Rwer1.ScanTags();
                        if (this.Direction == "双向磅")
                            tags2 = Hardwarer.Rwer2.ScanTags();

                        if (tags1.Count > 0)
                        {
                            passCarQueuer.Enqueue(eDirection.Way1, tags1[0]);
                            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.上磅方向.ToString(), "1");

                        }
                        else if (tags2.Count > 0)
                        {
                            passCarQueuer.Enqueue(eDirection.Way2, tags2[0]);
                            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.上磅方向.ToString(), "0");
                        }
                        else
                        {
                            this.CurrentFlowFlag = eFlowFlag.等待车辆;
                        }

                        if (passCarQueuer.Count > 0)
                        {
                            this.CurrentFlowFlag = eFlowFlag.识别车辆;
                            UpdateLedShow("  正在读卡");
                        }

                        #endregion
                        break;

                    case eFlowFlag.识别车辆:
                        #region

                        // 队列中无车时，等待车辆
                        if (passCarQueuer.Count == 0)
                        {
                            UpdateLedShow("  等待车辆");
                            this.CurrentImperfectCar = null;
                            this.CurrentDirection = eDirection.UnKnow;
                            this.CurrentFlowFlag = eFlowFlag.等待车辆;
                            break;
                        }

                        this.CurrentImperfectCar = passCarQueuer.Dequeue();

                        // 方式一：根据识别的车牌号查找车辆信息
                        this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar.Voucher);
                        if (this.CurrentAutotruck == null)
                            // 方式二：根据识别的标签卡查找车辆信息
                            this.CurrentAutotruck = carTransportDAO.GetAutotruckByTagId(this.CurrentImperfectCar.Voucher);

                        if (this.CurrentAutotruck != null)
                        {
                            UpdateLedShow(this.CurrentAutotruck.CarNumber + "读卡成功");
                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 读卡成功", 1, false);

                            if (this.CurrentAutotruck.IsUse == 1)
                            {
                                if (this.CurrentAutotruck.CarType == eCarType.入厂煤.ToString())
                                {
                                    this.timer_BuyFuel_Cancel = false;
                                    this.CurrentFlowFlag = eFlowFlag.验证信息;
                                    timer_BuyFuel_Tick(null, null);
                                }
                                else if (this.CurrentAutotruck.CarType == eCarType.其他物资.ToString())
                                {
                                    this.timer_Goods_Cancel = false;
                                    this.CurrentFlowFlag = eFlowFlag.验证信息;
                                }
                            }
                            else
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "已停用");
                                UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 已停用，禁止通过", 1, false);
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentImperfectCar.Voucher, "未登记");
                            // 方式一：车号识别
                            UpdateSpeaker(this.CurrentImperfectCar.Voucher + " 未登记，禁止通过", 1, false);
                        }

                        #endregion
                        break;
                }
                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地磅仪表_实时重量.ToString(), Hardwarer.Wber.Weight.ToString());
            }
            catch (Exception ex)
            {
                Log4Neter.Error("timer1_Tick", ex);
            }
            finally
            {
                timer1.Start();
            }
        }

        /// <summary>
        /// 慢速任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            // 三分钟执行一次
            timer2.Interval = 6000;

            try
            {
                SetCYJStatus();
            }
            catch (Exception ex)
            {
                Log4Neter.Error("timer2_Tick", ex);
            }
            finally
            {
                timer2.Start();
            }
        }

        /// <summary>
        /// 有车辆在上磅的道路上
        /// </summary>
        /// <returns></returns>
        bool HasCarOnEnterWay()
        {
            if (this.CurrentImperfectCar == null) return false;

            if (this.CurrentImperfectCar.PassWay == eDirection.UnKnow)
                return false;
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
                return this.InfraredSensor1;
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
                return this.InfraredSensor2;

            return true;
        }

        /// <summary>
        /// 有车辆在下磅的道路上
        /// </summary>
        /// <returns></returns>
        bool HasCarOnLeaveWay()
        {
            if (this.CurrentImperfectCar == null) return false;

            if (this.CurrentImperfectCar.PassWay == eDirection.UnKnow)
                return false;
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
                return this.InfraredSensor2;
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
                return this.InfraredSensor1;

            return true;
        }

        /// <summary>
        /// 执行远程命令
        /// </summary>
        void ExecAppRemoteControlCmd()
        {
            // 获取最新的命令
            CmcsAppRemoteControlCmd appRemoteControlCmd = commonDAO.GetNewestAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);
            if (appRemoteControlCmd != null)
            {
                if (appRemoteControlCmd.CmdCode == "控制道闸")
                {
                    Log4Neter.Info("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param);

                    if (appRemoteControlCmd.Param.Equals("Gate1Up", StringComparison.CurrentCultureIgnoreCase))
                        this.iocControler.Gate1Up();
                    else if (appRemoteControlCmd.Param.Equals("Gate1Down", StringComparison.CurrentCultureIgnoreCase) && !this.InductorCoil1)
                        this.iocControler.Gate1Down();
                    else if (appRemoteControlCmd.Param.Equals("Gate2Up", StringComparison.CurrentCultureIgnoreCase))
                        this.iocControler.Gate2Up();
                    else if (appRemoteControlCmd.Param.Equals("Gate2Down", StringComparison.CurrentCultureIgnoreCase) && !this.InductorCoil2)
                        this.iocControler.Gate2Down();

                    // 更新执行结果
                    commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
                }
                else if (appRemoteControlCmd.CmdCode == "重新采样")
                {
                    SendSamplePlan("远程");

                    // 更新执行结果
                    commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
                }
            }
        }

        /// <summary>
        /// 切换手动/自动模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnChangeAutoHandMode_ValueChanged(object sender, EventArgs e)
        {
            this.AutoHandMode = sbtnChangeAutoHandMode.Value;
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // 入厂煤
            LoadTodayUnFinishBuyFuelTransport();
            LoadTodayFinishBuyFuelTransport();

            // 其他物资
            LoadTodayUnFinishGoodsTransport();
            LoadTodayFinishGoodsTransport();

        }

        #endregion

        #region 入厂煤业务

        bool timer_BuyFuel_Cancel = true;

        CmcsBuyFuelTransport currentBuyFuelTransport;
        /// <summary>
        /// 当前运输记录
        /// </summary>
        public CmcsBuyFuelTransport CurrentBuyFuelTransport
        {
            get { return currentBuyFuelTransport; }
            set
            {
                currentBuyFuelTransport = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "供应商名称", value.SupplierName);

                    txtFuelKindName_BuyFuel.Text = value.FuelKindName;
                    txtMineName_BuyFuel.Text = value.MineName;
                    txtSupplierName_BuyFuel.Text = value.SupplierName;
                    txtTransportCompanyName_BuyFuel.Text = value.TransportCompanyName;

                    txtGrossWeight_BuyFuel.Text = value.GrossWeight.ToString("F2");
                    txtTicketWeight_BuyFuel.Text = value.TicketWeight.ToString("F2");
                    txtTareWeight_BuyFuel.Text = value.TareWeight.ToString("F2");
                    txtDeductWeight_BuyFuel.Text = value.DeductWeight.ToString("F2");
                    txtSuttleWeight_BuyFuel.Text = value.SuttleWeight.ToString("F2");
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "供应商名称", string.Empty);

                    txtFuelKindName_BuyFuel.ResetText();
                    txtMineName_BuyFuel.ResetText();
                    txtSupplierName_BuyFuel.ResetText();
                    txtTransportCompanyName_BuyFuel.ResetText();

                    txtGrossWeight_BuyFuel.ResetText();
                    txtTicketWeight_BuyFuel.ResetText();
                    txtTareWeight_BuyFuel.ResetText();
                    txtDeductWeight_BuyFuel.ResetText();
                    txtSuttleWeight_BuyFuel.ResetText();
                }
            }
        }

        /// <summary>
        /// 选择车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select("where CarType='" + eCarType.入厂煤.ToString() + "' order by CreationTime desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.InductorCoil1)
                    passCarQueuer.Enqueue(eDirection.Way1, frm.Output.CarNumber);
                else if (this.InductorCoil2)
                    passCarQueuer.Enqueue(eDirection.Way2, frm.Output.CarNumber);
                else
                    passCarQueuer.Enqueue(eDirection.UnKnow, frm.Output.CarNumber);

                this.CurrentFlowFlag = eFlowFlag.识别车辆;
            }
        }

        /// <summary>
        /// 保存入厂煤运输记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_BuyFuel_Click(object sender, EventArgs e)
        {
            if (!SaveBuyFuelTransport()) MessageBoxEx.Show("保存失败", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <returns></returns>
        bool SaveBuyFuelTransport()
        {
            if (this.CurrentBuyFuelTransport == null) return false;

            try
            {
                if (weighterDAO.SaveBuyFuelTransport(this.CurrentBuyFuelTransport.Id, (decimal)Hardwarer.Wber.Weight, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    this.CurrentBuyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(this.CurrentBuyFuelTransport.Id);

                    btnSaveTransport_BuyFuel.Enabled = false;
                    if (commonDAO.GetAppletConfigString("启动采样") == "1" && this.CurrentBuyFuelTransport.StepName != eTruckInFactoryStep.轻车.ToString())
                    {
                        if (this.CurrentBuyFuelTransport.StepName == eTruckInFactoryStep.重车.ToString())
                        {
                            this.CurrentFlowFlag = eFlowFlag.准备采样;
                            UpdateLedShow("称重完毕", "开始采样");
                            UpdateSpeaker("称重完毕 开始采样", 1, false);
                        }
                        else
                        {
                            UpdateLedShow("未称重", "不允许采样");
                            UpdateSpeaker("未称重 不允许采样", 1, false);
                        }
                    }
                    else
                    {
                        FrontGateUp();
                        this.CurrentFlowFlag = eFlowFlag.等待离开;
                        timing = 0;
                        UpdateLedShow("称重完毕", "请下磅");
                        UpdateSpeaker("称重完毕 请下磅", 1, false);
                    }
                    LoadTodayUnFinishBuyFuelTransport();
                    LoadTodayFinishBuyFuelTransport();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("保存失败\r\n" + ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("保存运输记录", ex);
            }

            return false;
        }

        /// <summary>
        /// 重置入厂煤运输记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_BuyFuel_Click(object sender, EventArgs e)
        {
            ResetBuyFuel();
        }

        /// <summary>
        /// 重置信息
        /// </summary>
        void ResetBuyFuel()
        {
            this.timer_BuyFuel_Cancel = true;

            this.CurrentFlowFlag = eFlowFlag.等待车辆;

            this.CurrentAutotruck = null;
            this.CurrentBuyFuelTransport = null;
            this.CurrentDirection = eDirection.UnKnow;

            txtTagId_BuyFuel.ResetText();

            btnSaveTransport_BuyFuel.Enabled = false;

            FrontGateDown();
            BackGateDown();

            UpdateLedShow("  等待车辆");

            // 最后重置
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// 入厂煤运输记录业务定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_BuyFuel_Tick(object sender, EventArgs e)
        {
            if (this.timer_BuyFuel_Cancel) return;

            timer_BuyFuel.Stop();
            timer_BuyFuel.Interval = 2000;

            try
            {
                switch (this.CurrentFlowFlag)
                {
                    case eFlowFlag.验证信息:
                        #region

                        // 查找该车未完成的运输记录
                        CmcsUnFinishTransport unFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id, eCarType.入厂煤.ToString());
                        if (unFinishTransport != null)
                        {
                            this.CurrentBuyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(unFinishTransport.TransportId);
                            if (this.CurrentBuyFuelTransport != null)
                            {
                                if (this.CurrentBuyFuelTransport.SuttleWeight == 0)
                                {
                                    string poundtype = commonDAO.GetAppletConfigString("磅类型");

                                    if (this.CurrentBuyFuelTransport.GrossWeight == 0)
                                    {
                                        if (poundtype == "空车磅")
                                        {
                                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "未称重车");
                                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 未称重车，请到重车磅", 1, false);

                                            timer_BuyFuel.Interval = 20000;
                                        }
                                        else
                                        {
                                            if (CommonAppConfig.GetInstance().AppIdentifier.Contains(CurrentBuyFuelTransport.HeavyWeight))
                                            {
                                                BackGateUp();
                                                this.CurrentFlowFlag = eFlowFlag.等待上磅;
                                                timing1 = 0;

                                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "请上磅");
                                                UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 请上磅", 1, false);
                                            }
                                            else
                                            {
                                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "非指定磅");
                                                UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 非指定磅，请到指定磅称重", 1, false);

                                                timer_BuyFuel.Interval = 20000;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        if (poundtype == "重车磅")
                                        {
                                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "已称重车");
                                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 已称重车，请到轻车磅", 1, false);

                                            timer_BuyFuel.Interval = 20000;
                                        }
                                        else
                                        {
                                            BackGateUp();
                                            this.CurrentFlowFlag = eFlowFlag.等待上磅;
                                            timing1 = 0;

                                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "请上磅");
                                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 请上磅", 1, false);
                                        }
                                    }



                                }
                                else
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "已称重");
                                    UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 已称重", 1, false);

                                    timer_BuyFuel.Interval = 20000;
                                }
                            }
                            else
                            {
                                commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(unFinishTransport.Id);
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "未排队");
                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 未找到排队记录", 1, false);

                            timer_BuyFuel.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待上磅:
                        #region

                        // 当地磅仪表重量大于最小称重且来车方向的对射无信号，则判定车已经完全上磅
                        if (Hardwarer.Wber.Weight >= this.WbMinWeight && !HasCarOnEnterWay())
                        {
                            Log4Neter.Info("重量：" + Hardwarer.Wber.Weight + ";信号无遮挡;延时：" + timing1);
                            //int timingvalue = commonDAO.GetAppletConfigInt32("落杆延时");
                            if (timing1 >= timingvalue)
                            {
                                Log4Neter.Info("延时：" + timing1 + ";设定：" + timingvalue + ";发送抬杆指令");
                                BackGateDown();

                                this.CurrentFlowFlag = eFlowFlag.等待稳定;

                                // 降低灵敏度
                                timer_BuyFuel.Interval = 4000;
                            }
                            else
                            {
                                // 提高灵敏度
                                timer_BuyFuel.Interval = 1000;
                            }
                            timing1++;

                        }



                        #endregion
                        break;

                    case eFlowFlag.等待稳定:
                        #region

                        // 提高灵敏度
                        timer_BuyFuel.Interval = 1000;

                        btnSaveTransport_BuyFuel.Enabled = this.WbSteady;

                        UpdateLedShow(this.CurrentAutotruck.CarNumber, Hardwarer.Wber.Weight.ToString("#0.######"));

                        if (this.WbSteady)
                        {
                            if (this.AutoHandMode)
                            {
                                // 自动模式
                                if (!SaveBuyFuelTransport())
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "称重失败");
                                    UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 称重失败，请联系管理员", 1, false);
                                }
                            }
                            else
                            {
                                // 手动模式 
                            }
                        }

                        #endregion
                        break;
                    case eFlowFlag.准备采样:
                        #region
                        //判断当前采样机是否存在未完成的采样，若有则不能发送新的采样命令
                        InfQCJXCYSampleCMD infQCJXCYSampleCMD = commonDAO.SelfDber.Entity<InfQCJXCYSampleCMD>("where MachineCode=:MachineCode and ResultCode = '默认'", new { MachineCode = this.SamplerMachineCode });
                        if (infQCJXCYSampleCMD == null)
                        {

                            CmcsRCSampling sampling = carTransportDAO.GetRCSamplingById(this.CurrentBuyFuelTransport.SamplingId);
                            if (sampling != null)
                            {
                                string status = commonDAO.GetSignalDataValue(this.SamplerMachineCode, eSignalDataName.系统.ToString());
                                if (status != "发生故障")
                                {
                                    //if (carTransportDAO.CheckSampleBarrel(sampling, this.SamplerMachineCode))
                                    //{
                                    this.CurrentSampleCMD = new InfQCJXCYSampleCMD()
                                    {
                                        MachineCode = this.SamplerMachineCode,
                                        CarNumber = this.CurrentBuyFuelTransport.CarNumber,
                                        InFactoryBatchId = this.CurrentBuyFuelTransport.InFactoryBatchId,
                                        SampleCode = sampling.SampleCode,
                                        SerialNumber = CurrentBuyFuelTransport.SerialNumber,
                                        Mt = 0,
                                        // 根据预报
                                        TicketWeight = 0,
                                        // 根据预报
                                        CarCount = 0,
                                        // 采样点数根据相关逻辑计算
                                        PointCount = 1,
                                        CarriageLength = this.CurrentAutotruck.CarriageLength,
                                        CarriageWidth = this.CurrentAutotruck.CarriageWidth,
                                        CarriageHeight = this.CurrentAutotruck.CarriageHeight,
                                        CarriageBottomToFloor = this.CurrentAutotruck.CarriageBottomToFloor,
                                        Obstacle1 = this.CurrentAutotruck.LeftObstacle1,
                                        Obstacle2 = this.CurrentAutotruck.LeftObstacle2,
                                        Obstacle3 = this.CurrentAutotruck.LeftObstacle3,
                                        Obstacle4 = this.CurrentAutotruck.LeftObstacle4,
                                        Obstacle5 = this.CurrentAutotruck.LeftObstacle5,
                                        Obstacle6 = this.CurrentAutotruck.LeftObstacle6,
                                        ResultCode = eEquInfCmdResultCode.默认.ToString(),
                                        DataFlag = 0
                                    };

                                    // 发送采样计划
                                    if (commonDAO.SelfDber.Insert<InfQCJXCYSampleCMD>(CurrentSampleCMD) > 0)
                                        this.CurrentFlowFlag = eFlowFlag.等待采样;
                                    //}
                                    //else
                                    //{
                                    //	this.UpdateLedShow("采样机无空桶");
                                    //  UpdateSpeaker("采样机无空桶，请联系管理员", 1, false);

                                    //	timer1.Interval = 10000;
                                    //}
                                }
                                else
                                {
                                    UpdateLedShow("采样机故障");
                                    UpdateSpeaker("采样机故障，请联系管理员", 1, false);

                                    timer1.Interval = 5000;
                                }
                            }
                            else
                            {
                                UpdateLedShow("未找到采样单信息");
                                UpdateSpeaker("未找到采样单信息，请联系管理员", 1, false);

                                timer1.Interval = 5000;
                            }
                        }
                        else
                        {
                            UpdateLedShow("有未完成的采样命令");
                            UpdateSpeaker("有未完成的采样命令，请联系管理员", 1, false);

                            timer1.Interval = 5000;
                        }
                        #endregion
                        break;
                    case eFlowFlag.等待采样:
                        #region

                        // 判断采样是否完成
                        InfQCJXCYSampleCMD qCJXCYSampleCMD = commonDAO.SelfDber.Get<InfQCJXCYSampleCMD>(this.CurrentSampleCMD.Id);
                        if (qCJXCYSampleCMD.ResultCode == eEquInfCmdResultCode.成功.ToString())
                        {
                            if (JxSamplerDAO.GetInstance().SaveBuyFuelTransport(this.CurrentBuyFuelTransport.Id, DateTime.Now, this.SamplerMachineCode))
                            {
                                FrontGateUp();

                                UpdateLedShow("采样完毕", " 请离开");
                                UpdateSpeaker("采样完毕，请离开", 1, false);

                                this.CurrentFlowFlag = eFlowFlag.等待离开;
                                timing = 0;
                            }
                        }
                        else
                        {
                            if (qCJXCYSampleCMD.ResultCode == "定位错误" || qCJXCYSampleCMD.ResultCode == "系统故障")
                            {
                                UpdateLedShow("采样机" + qCJXCYSampleCMD.ResultCode);
                                UpdateSpeaker("采样机" + qCJXCYSampleCMD.ResultCode + "，请联系管理员", 1, false);

                                timer1.Interval = 5000;
                            }
                        }

                        // 降低灵敏度
                        timer_BuyFuel.Interval = 4000;

                        #endregion
                        break;
                    case eFlowFlag.等待离开:
                        #region

                        // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnLeaveWay())
                        {
                            //int timingvalue = commonDAO.GetAppletConfigInt32("落杆延时");
                            if (timing >= timingvalue)
                            {
                                ResetBuyFuel();
                                // 降低灵敏度
                                timer_BuyFuel.Interval = 4000;
                            }
                            else
                            {
                                // 提高灵敏度
                                timer_BuyFuel.Interval = 1000;
                            }
                            timing++;
                        }
                        #endregion
                        break;
                }

                // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                //if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.等待车辆
                //    && this.CurrentImperfectCar != null) ResetBuyFuel();
            }
            catch (Exception ex)
            {
                Log4Neter.Error("timer_BuyFuel_Tick", ex);
            }
            finally
            {
                timer_BuyFuel.Start();
            }
        }

        /// <summary>
        /// 获取未完成的入厂煤记录
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetUnFinishBuyFuelTransport(CommonAppConfig.GetInstance().AppIdentifier.Replace("汽车智能化-", ""));
        }

        /// <summary>
        /// 获取指定日期已完成的入厂煤记录
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), CommonAppConfig.GetInstance().AppIdentifier.Replace("汽车智能化-", ""));
        }

        #endregion

        #region 其他物资业务

        bool timer_Goods_Cancel = true;

        CmcsGoodsTransport currentGoodsTransport;
        /// <summary>
        /// 当前运输记录
        /// </summary>
        public CmcsGoodsTransport CurrentGoodsTransport
        {
            get { return currentGoodsTransport; }
            set
            {
                currentGoodsTransport = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), value.Id);

                    txtSupplyUnitName_Goods.Text = value.SupplyUnitName;
                    txtReceiveUnitName_Goods.Text = value.ReceiveUnitName;
                    txtGoodsTypeName_Goods.Text = value.GoodsTypeName;

                    txtFirstWeight_Goods.Text = value.FirstWeight.ToString("F2");
                    txtSecondWeight_Goods.Text = value.SecondWeight.ToString("F2");
                    txtSuttleWeight_Goods.Text = value.SuttleWeight.ToString("F2");
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), string.Empty);

                    txtSupplyUnitName_Goods.ResetText();
                    txtReceiveUnitName_Goods.ResetText();
                    txtGoodsTypeName_Goods.ResetText();

                    txtFirstWeight_Goods.ResetText();
                    txtSecondWeight_Goods.ResetText();
                    txtSuttleWeight_Goods.ResetText();
                }
            }
        }

        /// <summary>
        /// 选择车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_Goods_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select("where CarType='" + eCarType.其他物资.ToString() + "' order by CreationTime desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.InductorCoil1)
                    passCarQueuer.Enqueue(eDirection.Way1, frm.Output.CarNumber);
                else if (this.InductorCoil2)
                    passCarQueuer.Enqueue(eDirection.Way2, frm.Output.CarNumber);
                else
                    passCarQueuer.Enqueue(eDirection.UnKnow, frm.Output.CarNumber);

                this.CurrentFlowFlag = eFlowFlag.识别车辆;
            }
        }

        /// <summary>
        /// 保存排队记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_Goods_Click(object sender, EventArgs e)
        {
            if (!SaveGoodsTransport()) MessageBoxEx.Show("保存失败", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <returns></returns>
        bool SaveGoodsTransport()
        {
            if (this.CurrentGoodsTransport == null) return false;

            try
            {
                if (weighterDAO.SaveGoodsTransport(this.CurrentGoodsTransport.Id, (decimal)Hardwarer.Wber.Weight, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    this.CurrentGoodsTransport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(this.CurrentGoodsTransport.Id);

                    FrontGateUp();

                    btnSaveTransport_Goods.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.等待离开;

                    UpdateLedShow("称重完毕", "请下磅");
                    UpdateSpeaker("称重完毕请下磅", 1, false);

                    LoadTodayUnFinishGoodsTransport();
                    LoadTodayFinishGoodsTransport();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("保存失败\r\n" + ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("保存运输记录", ex);
            }

            return false;
        }

        /// <summary>
        /// 重置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Goods_Click(object sender, EventArgs e)
        {
            ResetGoods();
        }

        /// <summary>
        /// 重置信息
        /// </summary>
        void ResetGoods()
        {
            this.timer_Goods_Cancel = true;

            this.CurrentFlowFlag = eFlowFlag.等待车辆;

            this.CurrentAutotruck = null;
            this.CurrentGoodsTransport = null;
            this.CurrentDirection = eDirection.UnKnow;

            txtTagId_Goods.ResetText();

            btnSaveTransport_Goods.Enabled = false;

            FrontGateDown();
            BackGateDown();

            UpdateLedShow("  等待车辆");

            // 最后重置
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// 其他物资运输记录业务定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Goods_Tick(object sender, EventArgs e)
        {
            if (this.timer_Goods_Cancel) return;

            timer_Goods.Stop();
            timer_Goods.Interval = 2000;

            try
            {
                switch (this.CurrentFlowFlag)
                {
                    case eFlowFlag.验证信息:
                        #region

                        // 查找该车未完成的运输记录
                        CmcsUnFinishTransport unFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id, eCarType.其他物资.ToString());
                        if (unFinishTransport != null)
                        {
                            this.CurrentGoodsTransport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(unFinishTransport.TransportId);
                            if (this.CurrentGoodsTransport != null)
                            {
                                // 判断路线设置
                                string nextPlace;
                                if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentGoodsTransport.StepName, "第一次称重|第二次称重", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                {
                                    if (this.CurrentGoodsTransport.SuttleWeight == 0)
                                    {
                                        BackGateUp();

                                        this.CurrentFlowFlag = eFlowFlag.等待上磅;

                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "请上磅");
                                        UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 请上磅", 1, false);
                                    }
                                    else
                                    {
                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "已称重");
                                        UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 已称重", 1, false);

                                        timer_Goods.Interval = 20000;
                                    }
                                }
                                else
                                {
                                    UpdateLedShow("路线错误", "禁止通过");
                                    UpdateSpeaker("路线错误 禁止通过 ", 1, false);

                                    timer_Goods.Interval = 20000;
                                }
                            }
                            else
                            {
                                commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(unFinishTransport.Id);
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "未排队");
                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 未找到排队记录", 1, false);

                            timer_Goods.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待上磅:
                        #region

                        // 当地磅仪表重量大于最小称重且来车方向的地感与对射均无信号，则判定车已经完全上磅
                        if (Hardwarer.Wber.Weight >= this.WbMinWeight && !HasCarOnEnterWay())
                        {
                            BackGateDown();

                            this.CurrentFlowFlag = eFlowFlag.等待稳定;
                        }

                        // 降低灵敏度
                        timer_Goods.Interval = 4000;

                        #endregion
                        break;

                    case eFlowFlag.等待稳定:
                        #region

                        // 提高灵敏度
                        timer_Goods.Interval = 1000;

                        btnSaveTransport_Goods.Enabled = this.WbSteady;

                        UpdateLedShow(this.CurrentAutotruck.CarNumber, Hardwarer.Wber.Weight.ToString("#0.######"));

                        if (this.WbSteady)
                        {
                            if (this.AutoHandMode)
                            {
                                // 自动模式
                                if (!SaveGoodsTransport())
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "称重失败");
                                    UpdateSpeaker(this.CurrentAutotruck.CarNumber + " 称重失败，请联系管理员", 1, false);
                                }
                            }
                            else
                            {
                                // 手动模式 
                            }
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待离开:
                        #region

                        // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnLeaveWay()) ResetGoods();

                        // 降低灵敏度
                        timer_Goods.Interval = 4000;

                        #endregion
                        break;
                }

                // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.等待车辆
                    && this.CurrentImperfectCar != null) ResetGoods();
            }
            catch (Exception ex)
            {
                Log4Neter.Error("timer_Goods_Tick", ex);
            }
            finally
            {
                timer_Goods.Start();
            }
        }

        /// <summary>
        /// 获取未完成的其他物资记录
        /// </summary>
        void LoadTodayUnFinishGoodsTransport()
        {
            superGridControl1_Goods.PrimaryGrid.DataSource = weighterDAO.GetUnFinishGoodsTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的其他物资记录
        /// </summary>
        void LoadTodayFinishGoodsTransport()
        {
            superGridControl2_Goods.PrimaryGrid.DataSource = weighterDAO.GetFinishedGoodsTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region 其他函数

        Font directionFont = new Font("微软雅黑", 16);

        Pen redPen1 = new Pen(Color.Red, 1);
        Pen greenPen1 = new Pen(Color.Lime, 1);
        Pen redPen3 = new Pen(Color.Red, 3);
        Pen greenPen3 = new Pen(Color.Lime, 3);

        /// <summary>
        /// 当前仪表重量面板绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panCurrentWeight_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                PanelEx panel = sender as PanelEx;

                int height = 12;

                // 绘制地感1
                e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 1, 15, height);
                e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, panel.Height - height, 15, panel.Height - 1);
                // 绘制对射1
                e.Graphics.DrawLine(this.InfraredSensor1 ? redPen1 : greenPen1, 35, 1, 35, height);
                e.Graphics.DrawLine(this.InfraredSensor1 ? redPen1 : greenPen1, 35, panel.Height - height, 35, panel.Height - 1);

                // 绘制对射2
                e.Graphics.DrawLine(this.InfraredSensor2 ? redPen1 : greenPen1, panel.Width - 35, 1, panel.Width - 35, height);
                e.Graphics.DrawLine(this.InfraredSensor2 ? redPen1 : greenPen1, panel.Width - 35, panel.Height - height, panel.Width - 35, panel.Height - 1);
                // 绘制地感2
                e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, panel.Width - 15, 1, panel.Width - 15, height);
                e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, panel.Width - 15, panel.Height - height, panel.Width - 15, panel.Height - 1);

                // 上磅方向
                eDirection direction = this.CurrentDirection;
                e.Graphics.DrawString("﹥>", directionFont, direction == eDirection.Way1 ? Brushes.Red : Brushes.Lime, 2, 17);
                e.Graphics.DrawString("<﹤", directionFont, direction == eDirection.Way2 ? Brushes.Red : Brushes.Lime, panel.Width - 47, 17);
            }
            catch (Exception ex)
            {
                Log4Neter.Error("panCurrentCarNumber_Paint异常", ex);
            }
        }

        private void superGridControl_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消进入编辑
            e.Cancel = true;
        }

        /// <summary>
        /// 设置行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        /// <summary>
        /// Invoke封装
        /// </summary>
        /// <param name="action"></param>
        public void InvokeEx(Action action)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(action);
        }

        #endregion

        /// <summary>
        /// 发送采样计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendSamplePlan_Click(object sender, EventArgs e)
        {
            SendSamplePlan("就地");
        }

        /// <summary>
        /// 重新采样
        /// </summary>
        /// <param name="operMode">操作模式：远程，就地</param>
        private void SendSamplePlan(string operMode)
        {
            if (this.CurrentBuyFuelTransport == null)
            {
                if (operMode == "就地")
                    MessageBoxEx.Show("运输记录不存在", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    FrmDebugConsole.GetInstance().Output("远程重新采样：运输记录不存在");

                return;
            }

            string status = commonDAO.GetSignalDataValue(this.SamplerMachineCode, eSignalDataName.系统.ToString());
            if (status == "发生故障")
            {
                if (operMode == "就地")
                    MessageBoxEx.Show("采样机故障，不能发送采样计划", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    FrmDebugConsole.GetInstance().Output("远程重新采样：采样机故障，不能发送采样计划");
                return;
            }

            //判断当前采样机是否存在未完成的采样，把未采样的状态设置成失败
            List<InfQCJXCYSampleCMD> infQCJXCYSampleCMDList = commonDAO.SelfDber.Entities<InfQCJXCYSampleCMD>("where MachineCode=:MachineCode and ResultCode = '默认'", new { MachineCode = this.SamplerMachineCode });
            if (infQCJXCYSampleCMDList != null && infQCJXCYSampleCMDList.Count > 0)
            {
                foreach (var item in infQCJXCYSampleCMDList)
                {
                    item.ResultCode = eEquInfCmdResultCode.失败.ToString();
                    commonDAO.SelfDber.Update<InfQCJXCYSampleCMD>(item);
                }
            }
            CmcsRCSampling sampling = carTransportDAO.GetRCSamplingById(this.CurrentBuyFuelTransport.SamplingId);
            if (sampling != null)
            {
                this.CurrentSampleCMD = new InfQCJXCYSampleCMD()
                {
                    MachineCode = this.SamplerMachineCode,
                    CarNumber = this.CurrentBuyFuelTransport.CarNumber,
                    InFactoryBatchId = this.CurrentBuyFuelTransport.InFactoryBatchId,
                    SampleCode = sampling.SampleCode,
                    SerialNumber = CurrentBuyFuelTransport.SerialNumber,
                    Mt = 0,
                    // 根据预报
                    TicketWeight = 0,
                    // 根据预报
                    CarCount = 0,
                    // 采样点数根据相关逻辑计算
                    PointCount = 1,
                    CarriageLength = this.CurrentAutotruck.CarriageLength,
                    CarriageWidth = this.CurrentAutotruck.CarriageWidth,
                    CarriageHeight = this.CurrentAutotruck.CarriageHeight,
                    CarriageBottomToFloor = this.CurrentAutotruck.CarriageBottomToFloor,
                    Obstacle1 = this.CurrentAutotruck.LeftObstacle1,
                    Obstacle2 = this.CurrentAutotruck.LeftObstacle2,
                    Obstacle3 = this.CurrentAutotruck.LeftObstacle3,
                    Obstacle4 = this.CurrentAutotruck.LeftObstacle4,
                    Obstacle5 = this.CurrentAutotruck.LeftObstacle5,
                    Obstacle6 = this.CurrentAutotruck.LeftObstacle6,
                    ResultCode = eEquInfCmdResultCode.默认.ToString(),
                    DataFlag = 0
                };

                // 发送采样计划
                if (commonDAO.SelfDber.Insert<InfQCJXCYSampleCMD>(CurrentSampleCMD) > 0)
                {
                    if (operMode == "就地")
                        MessageBoxEx.Show("发送成功", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    else
                        FrmDebugConsole.GetInstance().Output("远程重新采样：发送成功");

                    this.CurrentFlowFlag = eFlowFlag.等待采样;
                }
            }
            else
            {
                if (operMode == "就地")
                    MessageBoxEx.Show("未找到采样单信息", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    FrmDebugConsole.GetInstance().Output("远程重新采样：未找到采样单信息");
            }
        }

        /// <summary>
        /// 设置采样机状态信号
        /// </summary>
        public void SetCYJStatus()
        {
            if (this.SamplerMachineCode == null)
            {
                slightCYJ.LightColor = Color.Gray;
                return;
            }

            string status = "";
            //if (CommonAppConfig.GetInstance().AppIdentifier.Contains("1"))
            //{
            status = commonDAO.GetSignalDataValue(this.SamplerMachineCode, eSignalDataName.系统.ToString());
            //}
            //else
            //{
            //	status = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.系统.ToString());
            //}

            if (status == "正在运行")
            {
                slightCYJ.LightColor = Color.Green;
            }
            else if (status == "发生故障")
            {
                slightCYJ.LightColor = Color.Red;
            }
            else
            {
                slightCYJ.LightColor = Color.Gray;
            }

        }
    }
}

