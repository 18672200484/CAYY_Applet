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
        /// ����Ψһ��ʶ��
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
        /// �ȴ��ϴ���ץ��
        /// </summary>
        Queue<string> waitForUpload = new Queue<string>();

        IocControler iocControler;
        /// <summary>
        /// ��������
        /// </summary>
        VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

        public static int g_iCardNum = 1;      //���ƿ���ַ

        public static int g_iProgramIndex = 0;

        public static int timingvalue = 0;

        public static int timing1 = 0;

        public static int timing = 0;

        bool inductorCoil1 = false;
        /// <summary>
        /// �ظ�1״̬ true=���ź�  false=���ź�
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ظ�1�ź�.ToString(), value ? "1" : "0");

                inductorCoil1 = value;

                panCurrentWeight.Refresh();
            }
        }

        int inductorCoil1Port;
        /// <summary>
        /// �ظ�1�˿�
        /// </summary>
        public int InductorCoil1Port
        {
            get { return inductorCoil1Port; }
            set { inductorCoil1Port = value; }
        }

        bool inductorCoil2 = false;
        /// <summary>
        /// �ظ�2״̬ true=���ź�  false=���ź�
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ظ�2�ź�.ToString(), value ? "1" : "0");
                inductorCoil2 = value;

                panCurrentWeight.Refresh();

            }
        }

        int inductorCoil2Port;
        /// <summary>
        /// �ظ�2�˿�
        /// </summary>
        public int InductorCoil2Port
        {
            get { return inductorCoil2Port; }
            set { inductorCoil2Port = value; }
        }

        bool infraredSensor1 = false;
        /// <summary>
        /// ����1״̬ true=�ڵ�  false=��ͨ
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.����1�ź�.ToString(), value ? "1" : "0");

                infraredSensor1 = value;

                panCurrentWeight.Refresh();
            }
        }

        int infraredSensor1Port;
        /// <summary>
        /// ����1�˿�
        /// </summary>
        public int InfraredSensor1Port
        {
            get { return infraredSensor1Port; }
            set { infraredSensor1Port = value; }
        }

        bool infraredSensor2 = false;
        /// <summary>
        /// ����2״̬ true=�ڵ�  false=��ͨ
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.����2�ź�.ToString(), value ? "1" : "0");

                infraredSensor2 = value;

                panCurrentWeight.Refresh();
            }
        }

        int infraredSensor2Port;
        /// <summary>
        /// ����2�˿�
        /// </summary>
        public int InfraredSensor2Port
        {
            get { return infraredSensor2Port; }
            set { infraredSensor2Port = value; }
        }

        bool wbSteady = false;
        /// <summary>
        /// �ذ��Ǳ��ȶ�״̬
        /// </summary>
        public bool WbSteady
        {
            get { return wbSteady; }
            set
            {
                if (wbSteady != value)
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ذ��Ǳ�_�ȶ�.ToString(), value ? "1" : "0");

                wbSteady = value;

                this.panCurrentWeight.Style.ForeColor.Color = (value ? Color.Lime : Color.Red);

                panCurrentWeight.Refresh();
            }
        }

        double wbMinWeight = 0;
        /// <summary>
        /// �ذ��Ǳ���С���� ��λ����
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
        /// �Զ�ģʽ=true  �ֶ�ģʽ=false
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

                if (CommonAppConfig.GetInstance().AppIdentifier.Contains("��"))
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
        /// ʶ���ѡ��ĳ���ƾ֤
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
                    panCurrentCarNumber.Text = "�ȴ�����";
            }
        }

        eDirection currentDirection;
        /// <summary>
        /// ��ǰ�ϰ�����
        /// </summary>
        public eDirection CurrentDirection
        {
            get { return currentDirection; }
            set { currentDirection = value; }
        }

        string direction;
        /// <summary>
        /// �̶��ϰ�����
        /// </summary>
        public string Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                if (value == "˫���")
                {
                    slightRwer2.Visible = true;
                    label_Rwer2.Visible = true;
                }
                else if (value == "�����")
                {
                    slightRwer2.Visible = false;
                    label_Rwer2.Visible = false;
                }
            }
        }

        eFlowFlag currentFlowFlag = eFlowFlag.�ȴ�����;
        /// <summary>
        /// ��ǰҵ�����̱�ʶ
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
        /// ��ǰ��
        /// </summary>
        public CmcsAutotruck CurrentAutotruck
        {
            get { return currentAutotruck; }
            set
            {
                currentAutotruck = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString(), value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), value.CarNumber);

                    CmcsEPCCard ePCCard = Dbers.GetInstance().SelfDber.Get<CmcsEPCCard>(value.EPCCardId);
                    if (value.CarType == eCarType.�볧ú.ToString())
                    {
                        if (ePCCard != null) txtTagId_BuyFuel.Text = ePCCard.TagId;

                        txtCarNumber_BuyFuel.Text = value.CarNumber;
                        superTabControl2.SelectedTab = superTabItem_BuyFuel;
                    }
                    else if (value.CarType == eCarType.��������.ToString())
                    {
                        if (ePCCard != null) txtTagId_Goods.Text = ePCCard.TagId;

                        txtCarNumber_Goods.Text = value.CarNumber;
                        superTabControl2.SelectedTab = superTabItem_Goods;
                    }

                    panCurrentCarNumber.Text = value.CarNumber;
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), string.Empty);

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
        /// ��ǰ��������
        /// </summary>
        public InfQCJXCYSampleCMD CurrentSampleCMD
        {
            get { return currentSampleCMD; }
            set { currentSampleCMD = value; }
        }

        #endregion

        /// <summary>
        /// �����ʼ��
        /// </summary>
        private void InitForm()
        {
            lblFlowFlag.ForeColor = Color.White;
            FrmDebugConsole.GetInstance();
            SamplerMachineCode = commonDAO.GetAppletConfigString("����������");
            // Ĭ���Զ�
            sbtnChangeAutoHandMode.Value = true;

            timingvalue = commonDAO.GetAppletConfigInt32("�����ʱ");

            // ���ó���Զ�̿�������
            commonDAO.ResetAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);

            btnRefresh_Click(null, null);

            if (CommonAppConfig.GetInstance().AppIdentifier.Contains("��"))
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
            Direction = commonDAO.GetAppletConfigString("�ϰ�����");
            InitHardware();

            InitForm();
        }

        private void FrmQueuer_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ж���豸
            UnloadHardware();
        }

        #region �豸���

        #region IO������

        void Iocer_StatusChange(bool status)
        {
            // �����豸״̬ 
            InvokeEx(() =>
            {
                slightIOC.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.IO������_����״̬.ToString(), status ? "1" : "0");
            });
        }

        /// <summary>
        /// IO��������������ʱ����
        /// </summary>
        /// <param name="receiveValue"></param>
        void Iocer_Received(int[] receiveValue)
        {
            // ���յظ�״̬  
            InvokeEx(() =>
            {
                this.InductorCoil1 = (receiveValue[this.InductorCoil1Port - 1] == 1);
                this.InductorCoil2 = (receiveValue[this.InductorCoil2Port - 1] == 1);
                this.InfraredSensor1 = (receiveValue[this.InfraredSensor1Port - 1] == 1);
                this.InfraredSensor2 = (receiveValue[this.InfraredSensor2Port - 1] == 1);
            });
        }

        /// <summary>
        /// ǰ������
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
        /// ǰ������
        /// </summary>
        void FrontGateDown()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
            {
                if (!this.InfraredSensor2)
                    this.iocControler.Gate2Down();
                else
                    Log4Neter.Info("����2���źţ�ǰ���޷�����");

                this.iocControler.RedLight2();
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
            {
                if (!this.InfraredSensor1)
                    this.iocControler.Gate1Down();
                else
                    Log4Neter.Info("����1���źţ�ǰ���޷�����");

                this.iocControler.RedLight1();
            }
        }

        /// <summary>
        /// ������
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
        /// �󷽽���
        /// </summary>
        void BackGateDown()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
            {
                if (!this.InfraredSensor1)
                    this.iocControler.Gate1Down();
                else
                    Log4Neter.Info("����1���źţ����޷�����");

                this.iocControler.RedLight1();
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
            {
                if (!this.InfraredSensor2)
                    this.iocControler.Gate2Down();
                else
                    Log4Neter.Info("����2���źţ����޷�����");

                this.iocControler.RedLight2();
            }
        }

        #endregion

        #region ������

        void Rwer1_OnScanError(Exception ex)
        {
            Log4Neter.Error("������1", ex);
        }

        void Rwer1_OnStatusChange(bool status)
        {
            // �����豸״̬ 
            InvokeEx(() =>
            {
                slightRwer1.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.������1_����״̬.ToString(), status ? "1" : "0");
            });
        }

        void Rwer2_OnScanError(Exception ex)
        {
            Log4Neter.Error("������2", ex);
        }

        void Rwer2_OnStatusChange(bool status)
        {
            // �����豸״̬ 
            InvokeEx(() =>
            {
                slightRwer2.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.������2_����״̬.ToString(), status ? "1" : "0");
            });
        }

        #endregion

        #region ������ʾ
        /// <summary>
        /// ��������������Ϣ
        /// </summary>
        private void UpdateSpeaker(string value, int count, bool reset)
        {
            this.voiceSpeaker.Speak(value, count, reset);
            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "����������Ϣ", value);
        }
        #endregion

        #region LED��ʾ��

        /// <summary>
        /// ����LED��̬����
        /// </summary>
        /// <param name="value1">��һ������</param>
        /// <param name="value2">�ڶ�������</param>
        private void UpdateLedShow(string value1 = "", string value2 = "")
        {
            UpdateLed1Show(value1, value2);
        }

        #region LED1���ƿ�

        private bool _LED1ConnectStatus;
        /// <summary>
        /// LED1����״̬
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

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.LED��1_����״̬.ToString(), value ? "1" : "0");
            }
        }

        /// <summary>
        /// LED1��ʾ�����ı�
        /// </summary>
        string LED1TempFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Led1TempFile.txt");

        /// <summary>
        /// LED1��һ����ʾ����
        /// </summary>
        string LED1PrevLedFileContent = string.Empty;

        /// <summary>
        /// ����LED��̬����
        /// </summary>
        /// <param name="value1">��һ������</param>
        /// <param name="value2">�ڶ�������</param>
        private void UpdateLed1Show(string value1 = "", string value2 = "")
        {
            if (!this.LED1ConnectStatus) return;
            if (this.LED1PrevLedFileContent == value1 + value2) return;
            FrmDebugConsole.GetInstance().Output("����LED1:|" + value1 + "|" + value2 + "|");

            this.LED1PrevLedFileContent = value1 + value2;
            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "LED����ʾ��Ϣ", this.LED1PrevLedFileContent);


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
            Text.FontInfo.strFontName = "����";
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

        #region �ذ��Ǳ�

        /// <summary>
        /// �����ȶ��¼�
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
        /// �ذ��Ǳ�״̬�仯
        /// </summary>
        /// <param name="status"></param>
        void Wber_OnStatusChange(bool status)
        {
            // �����豸״̬ 
            InvokeEx(() =>
            {
                slightWber.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ذ��Ǳ�_����״̬.ToString(), status ? "1" : "0");
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

        #region �豸��ʼ����ж��

        /// <summary>
        /// ��ʼ������豸
        /// </summary>
        private void InitHardware()
        {
            try
            {
                bool success = false;

                this.InductorCoil1Port = commonDAO.GetAppletConfigInt32("IO������_�ظ�1�˿�");
                this.InductorCoil2Port = commonDAO.GetAppletConfigInt32("IO������_�ظ�2�˿�");
                this.InfraredSensor1Port = commonDAO.GetAppletConfigInt32("IO������_����1�˿�");
                this.InfraredSensor2Port = commonDAO.GetAppletConfigInt32("IO������_����2�˿�");

                this.WbMinWeight = commonDAO.GetAppletConfigDouble("�ذ��Ǳ�_��С����");

                // IO������
                Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
                success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("IO������_����"), commonDAO.GetAppletConfigInt32("IO������_������"), commonDAO.GetAppletConfigInt32("IO������_����λ"), (StopBits)commonDAO.GetAppletConfigInt32("IO������_ֹͣλ"), (Parity)commonDAO.GetAppletConfigInt32("IO������_У��λ"));
                if (!success) MessageBoxEx.Show("IO����������ʧ�ܣ�", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.iocControler = new IocControler(Hardwarer.Iocer);

                // �ذ��Ǳ�
                Hardwarer.Wber.OnStatusChange += new WB.TOLEDO.IND245.TOLEDO_IND245Wber.StatusChangeHandler(Wber_OnStatusChange);
                Hardwarer.Wber.OnSteadyChange += new WB.TOLEDO.IND245.TOLEDO_IND245Wber.SteadyChangeEventHandler(Wber_OnSteadyChange);
                Hardwarer.Wber.OnWeightChange += new WB.TOLEDO.IND245.TOLEDO_IND245Wber.WeightChangeEventHandler(Wber_OnWeightChange);
                success = Hardwarer.Wber.OpenCom(commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_����"), commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_������"), commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_����λ"), (StopBits)commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_ֹͣλ"), (Parity)commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_У��λ"), commonDAO.GetAppletConfigString("�ذ��Ǳ�_����"));

                // ������1
                Hardwarer.Rwer1.StartWith = commonDAO.GetAppletConfigString("������_��ǩ����");
                Hardwarer.Rwer1.OnStatusChange += new RW.LZR12.Lzr12Rwer.StatusChangeHandler(Rwer1_OnStatusChange);
                Hardwarer.Rwer1.OnScanError += new RW.LZR12.Lzr12Rwer.ScanErrorEventHandler(Rwer1_OnScanError);
                success = Hardwarer.Rwer1.OpenCom(commonDAO.GetAppletConfigInt32("������1_����"));
                if (!success) MessageBoxEx.Show("������1����ʧ�ܣ�", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (this.Direction == "˫���")
                {
                    // ������2
                    Hardwarer.Rwer2.StartWith = commonDAO.GetAppletConfigString("������_��ǩ����");
                    Hardwarer.Rwer2.OnStatusChange += new RW.LZR12.Lzr12Rwer.StatusChangeHandler(Rwer2_OnStatusChange);
                    Hardwarer.Rwer2.OnScanError += new RW.LZR12.Lzr12Rwer.ScanErrorEventHandler(Rwer2_OnScanError);
                    success = Hardwarer.Rwer2.OpenCom(commonDAO.GetAppletConfigInt32("������2_����"));
                    if (!success) MessageBoxEx.Show("������2����ʧ�ܣ�", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                #region LED���ƿ�

                //��������
                if (EQ2013.User_RealtimeConnect(g_iCardNum))
                {
                    // ��ʼ���ɹ�
                    this.LED1ConnectStatus = true;

                    UpdateLed1Show("  �ȴ�����");
                }
                else
                {
                    this.LED1ConnectStatus = false;
                    MessageBoxEx.Show("LED1���ƿ�����ʧ�ܣ�", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                #endregion

                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                Log4Neter.Error("�豸��ʼ��", ex);
            }
        }

        /// <summary>
        /// ж���豸
        /// </summary>
        private void UnloadHardware()
        {
            // ע��˶δ���
            Application.DoEvents();

            try
            {
                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString(), string.Empty);
                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), string.Empty);
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
                if (this.Direction == "˫���")
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

        #region ��բ���ư�ť

        /// <summary>
        /// ��բ1����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate1Up_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate1Up();
        }

        /// <summary>
        /// ��բ1����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate1Down_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate1Down();
        }

        /// <summary>
        /// ��բ2����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate2Up_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate2Up();
        }

        /// <summary>
        /// ��բ2����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate2Down_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate2Down();
        }

        #endregion

        #region ����ҵ��

        /// <summary>
        /// ����������ʶ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Interval = 2000;

            try
            {
                // ִ��Զ������
                ExecAppRemoteControlCmd();

                if (commonDAO.GetAppletConfigString("��������") == "0")
                {
                    UpdateLed1Show("ֹͣ����");
                    return;
                }

                switch (this.CurrentFlowFlag)
                {
                    case eFlowFlag.�ȴ�����:
                        #region
                        UpdateLed1Show("�ȴ�����");
                        this.CurrentFlowFlag = eFlowFlag.��ʼ����;

                        #endregion
                        break;

                    case eFlowFlag.��ʼ����:
                        #region
                        List<string> tags1 = new List<string>();
                        List<string> tags2 = new List<string>();
                        tags1 = Hardwarer.Rwer1.ScanTags();
                        if (this.Direction == "˫���")
                            tags2 = Hardwarer.Rwer2.ScanTags();

                        if (tags1.Count > 0)
                        {
                            passCarQueuer.Enqueue(eDirection.Way1, tags1[0]);
                            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ϰ�����.ToString(), "1");

                        }
                        else if (tags2.Count > 0)
                        {
                            passCarQueuer.Enqueue(eDirection.Way2, tags2[0]);
                            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ϰ�����.ToString(), "0");
                        }
                        else
                        {
                            this.CurrentFlowFlag = eFlowFlag.�ȴ�����;
                        }

                        if (passCarQueuer.Count > 0)
                        {
                            this.CurrentFlowFlag = eFlowFlag.ʶ����;
                            UpdateLedShow("  ���ڶ���");
                        }

                        #endregion
                        break;

                    case eFlowFlag.ʶ����:
                        #region

                        // �������޳�ʱ���ȴ�����
                        if (passCarQueuer.Count == 0)
                        {
                            UpdateLedShow("  �ȴ�����");
                            this.CurrentImperfectCar = null;
                            this.CurrentDirection = eDirection.UnKnow;
                            this.CurrentFlowFlag = eFlowFlag.�ȴ�����;
                            break;
                        }

                        this.CurrentImperfectCar = passCarQueuer.Dequeue();

                        // ��ʽһ������ʶ��ĳ��ƺŲ��ҳ�����Ϣ
                        this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar.Voucher);
                        if (this.CurrentAutotruck == null)
                            // ��ʽ��������ʶ��ı�ǩ�����ҳ�����Ϣ
                            this.CurrentAutotruck = carTransportDAO.GetAutotruckByTagId(this.CurrentImperfectCar.Voucher);

                        if (this.CurrentAutotruck != null)
                        {
                            UpdateLedShow(this.CurrentAutotruck.CarNumber + "�����ɹ�");
                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " �����ɹ�", 1, false);

                            if (this.CurrentAutotruck.IsUse == 1)
                            {
                                if (this.CurrentAutotruck.CarType == eCarType.�볧ú.ToString())
                                {
                                    this.timer_BuyFuel_Cancel = false;
                                    this.CurrentFlowFlag = eFlowFlag.��֤��Ϣ;
                                    timer_BuyFuel_Tick(null, null);
                                }
                                else if (this.CurrentAutotruck.CarType == eCarType.��������.ToString())
                                {
                                    this.timer_Goods_Cancel = false;
                                    this.CurrentFlowFlag = eFlowFlag.��֤��Ϣ;
                                }
                            }
                            else
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "��ͣ��");
                                UpdateSpeaker(this.CurrentAutotruck.CarNumber + " ��ͣ�ã���ֹͨ��", 1, false);
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentImperfectCar.Voucher, "δ�Ǽ�");
                            // ��ʽһ������ʶ��
                            UpdateSpeaker(this.CurrentImperfectCar.Voucher + " δ�Ǽǣ���ֹͨ��", 1, false);
                        }

                        #endregion
                        break;
                }
                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString(), Hardwarer.Wber.Weight.ToString());
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
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            // ������ִ��һ��
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
        /// �г������ϰ��ĵ�·��
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
        /// �г������°��ĵ�·��
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
        /// ִ��Զ������
        /// </summary>
        void ExecAppRemoteControlCmd()
        {
            // ��ȡ���µ�����
            CmcsAppRemoteControlCmd appRemoteControlCmd = commonDAO.GetNewestAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);
            if (appRemoteControlCmd != null)
            {
                if (appRemoteControlCmd.CmdCode == "���Ƶ�բ")
                {
                    Log4Neter.Info("����Զ�����" + appRemoteControlCmd.CmdCode + "��������" + appRemoteControlCmd.Param);

                    if (appRemoteControlCmd.Param.Equals("Gate1Up", StringComparison.CurrentCultureIgnoreCase))
                        this.iocControler.Gate1Up();
                    else if (appRemoteControlCmd.Param.Equals("Gate1Down", StringComparison.CurrentCultureIgnoreCase) && !this.InductorCoil1)
                        this.iocControler.Gate1Down();
                    else if (appRemoteControlCmd.Param.Equals("Gate2Up", StringComparison.CurrentCultureIgnoreCase))
                        this.iocControler.Gate2Up();
                    else if (appRemoteControlCmd.Param.Equals("Gate2Down", StringComparison.CurrentCultureIgnoreCase) && !this.InductorCoil2)
                        this.iocControler.Gate2Down();

                    // ����ִ�н��
                    commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.�ɹ�);
                }
                else if (appRemoteControlCmd.CmdCode == "���²���")
                {
                    SendSamplePlan("Զ��");

                    // ����ִ�н��
                    commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.�ɹ�);
                }
            }
        }

        /// <summary>
        /// �л��ֶ�/�Զ�ģʽ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnChangeAutoHandMode_ValueChanged(object sender, EventArgs e)
        {
            this.AutoHandMode = sbtnChangeAutoHandMode.Value;
        }

        /// <summary>
        /// ˢ���б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // �볧ú
            LoadTodayUnFinishBuyFuelTransport();
            LoadTodayFinishBuyFuelTransport();

            // ��������
            LoadTodayUnFinishGoodsTransport();
            LoadTodayFinishGoodsTransport();

        }

        #endregion

        #region �볧úҵ��

        bool timer_BuyFuel_Cancel = true;

        CmcsBuyFuelTransport currentBuyFuelTransport;
        /// <summary>
        /// ��ǰ�����¼
        /// </summary>
        public CmcsBuyFuelTransport CurrentBuyFuelTransport
        {
            get { return currentBuyFuelTransport; }
            set
            {
                currentBuyFuelTransport = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "��Ӧ������", value.SupplierName);

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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "��Ӧ������", string.Empty);

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
        /// ѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select("where CarType='" + eCarType.�볧ú.ToString() + "' order by CreationTime desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.InductorCoil1)
                    passCarQueuer.Enqueue(eDirection.Way1, frm.Output.CarNumber);
                else if (this.InductorCoil2)
                    passCarQueuer.Enqueue(eDirection.Way2, frm.Output.CarNumber);
                else
                    passCarQueuer.Enqueue(eDirection.UnKnow, frm.Output.CarNumber);

                this.CurrentFlowFlag = eFlowFlag.ʶ����;
            }
        }

        /// <summary>
        /// �����볧ú�����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_BuyFuel_Click(object sender, EventArgs e)
        {
            if (!SaveBuyFuelTransport()) MessageBoxEx.Show("����ʧ��", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ���������¼
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
                    if (commonDAO.GetAppletConfigString("��������") == "1" && this.CurrentBuyFuelTransport.StepName != eTruckInFactoryStep.�ᳵ.ToString())
                    {
                        if (this.CurrentBuyFuelTransport.StepName == eTruckInFactoryStep.�س�.ToString())
                        {
                            this.CurrentFlowFlag = eFlowFlag.׼������;
                            UpdateLedShow("�������", "��ʼ����");
                            UpdateSpeaker("������� ��ʼ����", 1, false);
                        }
                        else
                        {
                            UpdateLedShow("δ����", "���������");
                            UpdateSpeaker("δ���� ���������", 1, false);
                        }
                    }
                    else
                    {
                        FrontGateUp();
                        this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;
                        timing = 0;
                        UpdateLedShow("�������", "���°�");
                        UpdateSpeaker("������� ���°�", 1, false);
                    }
                    LoadTodayUnFinishBuyFuelTransport();
                    LoadTodayFinishBuyFuelTransport();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("����ʧ��\r\n" + ex.Message, "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("���������¼", ex);
            }

            return false;
        }

        /// <summary>
        /// �����볧ú�����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_BuyFuel_Click(object sender, EventArgs e)
        {
            ResetBuyFuel();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        void ResetBuyFuel()
        {
            this.timer_BuyFuel_Cancel = true;

            this.CurrentFlowFlag = eFlowFlag.�ȴ�����;

            this.CurrentAutotruck = null;
            this.CurrentBuyFuelTransport = null;
            this.CurrentDirection = eDirection.UnKnow;

            txtTagId_BuyFuel.ResetText();

            btnSaveTransport_BuyFuel.Enabled = false;

            FrontGateDown();
            BackGateDown();

            UpdateLedShow("  �ȴ�����");

            // �������
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// �볧ú�����¼ҵ��ʱ��
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
                    case eFlowFlag.��֤��Ϣ:
                        #region

                        // ���Ҹó�δ��ɵ������¼
                        CmcsUnFinishTransport unFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id, eCarType.�볧ú.ToString());
                        if (unFinishTransport != null)
                        {
                            this.CurrentBuyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(unFinishTransport.TransportId);
                            if (this.CurrentBuyFuelTransport != null)
                            {
                                if (this.CurrentBuyFuelTransport.SuttleWeight == 0)
                                {
                                    string poundtype = commonDAO.GetAppletConfigString("������");

                                    if (this.CurrentBuyFuelTransport.GrossWeight == 0)
                                    {
                                        if (poundtype == "�ճ���")
                                        {
                                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "δ���س�");
                                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " δ���س����뵽�س���", 1, false);

                                            timer_BuyFuel.Interval = 20000;
                                        }
                                        else
                                        {
                                            if (CommonAppConfig.GetInstance().AppIdentifier.Contains(CurrentBuyFuelTransport.HeavyWeight))
                                            {
                                                BackGateUp();
                                                this.CurrentFlowFlag = eFlowFlag.�ȴ��ϰ�;
                                                timing1 = 0;

                                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "���ϰ�");
                                                UpdateSpeaker(this.CurrentAutotruck.CarNumber + " ���ϰ�", 1, false);
                                            }
                                            else
                                            {
                                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "��ָ����");
                                                UpdateSpeaker(this.CurrentAutotruck.CarNumber + " ��ָ�������뵽ָ��������", 1, false);

                                                timer_BuyFuel.Interval = 20000;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        if (poundtype == "�س���")
                                        {
                                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "�ѳ��س�");
                                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " �ѳ��س����뵽�ᳵ��", 1, false);

                                            timer_BuyFuel.Interval = 20000;
                                        }
                                        else
                                        {
                                            BackGateUp();
                                            this.CurrentFlowFlag = eFlowFlag.�ȴ��ϰ�;
                                            timing1 = 0;

                                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "���ϰ�");
                                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " ���ϰ�", 1, false);
                                        }
                                    }



                                }
                                else
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "�ѳ���");
                                    UpdateSpeaker(this.CurrentAutotruck.CarNumber + " �ѳ���", 1, false);

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
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "δ�Ŷ�");
                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " δ�ҵ��ŶӼ�¼", 1, false);

                            timer_BuyFuel.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��ϰ�:
                        #region

                        // ���ذ��Ǳ�����������С��������������Ķ������źţ����ж����Ѿ���ȫ�ϰ�
                        if (Hardwarer.Wber.Weight >= this.WbMinWeight && !HasCarOnEnterWay())
                        {
                            Log4Neter.Info("������" + Hardwarer.Wber.Weight + ";�ź����ڵ�;��ʱ��" + timing1);
                            //int timingvalue = commonDAO.GetAppletConfigInt32("�����ʱ");
                            if (timing1 >= timingvalue)
                            {
                                Log4Neter.Info("��ʱ��" + timing1 + ";�趨��" + timingvalue + ";����̧��ָ��");
                                BackGateDown();

                                this.CurrentFlowFlag = eFlowFlag.�ȴ��ȶ�;

                                // ����������
                                timer_BuyFuel.Interval = 4000;
                            }
                            else
                            {
                                // ���������
                                timer_BuyFuel.Interval = 1000;
                            }
                            timing1++;

                        }



                        #endregion
                        break;

                    case eFlowFlag.�ȴ��ȶ�:
                        #region

                        // ���������
                        timer_BuyFuel.Interval = 1000;

                        btnSaveTransport_BuyFuel.Enabled = this.WbSteady;

                        UpdateLedShow(this.CurrentAutotruck.CarNumber, Hardwarer.Wber.Weight.ToString("#0.######"));

                        if (this.WbSteady)
                        {
                            if (this.AutoHandMode)
                            {
                                // �Զ�ģʽ
                                if (!SaveBuyFuelTransport())
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "����ʧ��");
                                    UpdateSpeaker(this.CurrentAutotruck.CarNumber + " ����ʧ�ܣ�����ϵ����Ա", 1, false);
                                }
                            }
                            else
                            {
                                // �ֶ�ģʽ 
                            }
                        }

                        #endregion
                        break;
                    case eFlowFlag.׼������:
                        #region
                        //�жϵ�ǰ�������Ƿ����δ��ɵĲ������������ܷ����µĲ�������
                        InfQCJXCYSampleCMD infQCJXCYSampleCMD = commonDAO.SelfDber.Entity<InfQCJXCYSampleCMD>("where MachineCode=:MachineCode and ResultCode = 'Ĭ��'", new { MachineCode = this.SamplerMachineCode });
                        if (infQCJXCYSampleCMD == null)
                        {

                            CmcsRCSampling sampling = carTransportDAO.GetRCSamplingById(this.CurrentBuyFuelTransport.SamplingId);
                            if (sampling != null)
                            {
                                string status = commonDAO.GetSignalDataValue(this.SamplerMachineCode, eSignalDataName.ϵͳ.ToString());
                                if (status != "��������")
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
                                        // ����Ԥ��
                                        TicketWeight = 0,
                                        // ����Ԥ��
                                        CarCount = 0,
                                        // ����������������߼�����
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
                                        ResultCode = eEquInfCmdResultCode.Ĭ��.ToString(),
                                        DataFlag = 0
                                    };

                                    // ���Ͳ����ƻ�
                                    if (commonDAO.SelfDber.Insert<InfQCJXCYSampleCMD>(CurrentSampleCMD) > 0)
                                        this.CurrentFlowFlag = eFlowFlag.�ȴ�����;
                                    //}
                                    //else
                                    //{
                                    //	this.UpdateLedShow("�������޿�Ͱ");
                                    //  UpdateSpeaker("�������޿�Ͱ������ϵ����Ա", 1, false);

                                    //	timer1.Interval = 10000;
                                    //}
                                }
                                else
                                {
                                    UpdateLedShow("����������");
                                    UpdateSpeaker("���������ϣ�����ϵ����Ա", 1, false);

                                    timer1.Interval = 5000;
                                }
                            }
                            else
                            {
                                UpdateLedShow("δ�ҵ���������Ϣ");
                                UpdateSpeaker("δ�ҵ���������Ϣ������ϵ����Ա", 1, false);

                                timer1.Interval = 5000;
                            }
                        }
                        else
                        {
                            UpdateLedShow("��δ��ɵĲ�������");
                            UpdateSpeaker("��δ��ɵĲ����������ϵ����Ա", 1, false);

                            timer1.Interval = 5000;
                        }
                        #endregion
                        break;
                    case eFlowFlag.�ȴ�����:
                        #region

                        // �жϲ����Ƿ����
                        InfQCJXCYSampleCMD qCJXCYSampleCMD = commonDAO.SelfDber.Get<InfQCJXCYSampleCMD>(this.CurrentSampleCMD.Id);
                        if (qCJXCYSampleCMD.ResultCode == eEquInfCmdResultCode.�ɹ�.ToString())
                        {
                            if (JxSamplerDAO.GetInstance().SaveBuyFuelTransport(this.CurrentBuyFuelTransport.Id, DateTime.Now, this.SamplerMachineCode))
                            {
                                FrontGateUp();

                                UpdateLedShow("�������", " ���뿪");
                                UpdateSpeaker("������ϣ����뿪", 1, false);

                                this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;
                                timing = 0;
                            }
                        }
                        else
                        {
                            if (qCJXCYSampleCMD.ResultCode == "��λ����" || qCJXCYSampleCMD.ResultCode == "ϵͳ����")
                            {
                                UpdateLedShow("������" + qCJXCYSampleCMD.ResultCode);
                                UpdateSpeaker("������" + qCJXCYSampleCMD.ResultCode + "������ϵ����Ա", 1, false);

                                timer1.Interval = 5000;
                            }
                        }

                        // ����������
                        timer_BuyFuel.Interval = 4000;

                        #endregion
                        break;
                    case eFlowFlag.�ȴ��뿪:
                        #region

                        // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnLeaveWay())
                        {
                            //int timingvalue = commonDAO.GetAppletConfigInt32("�����ʱ");
                            if (timing >= timingvalue)
                            {
                                ResetBuyFuel();
                                // ����������
                                timer_BuyFuel.Interval = 4000;
                            }
                            else
                            {
                                // ���������
                                timer_BuyFuel.Interval = 1000;
                            }
                            timing++;
                        }
                        #endregion
                        break;
                }

                // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                //if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.�ȴ�����
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
        /// ��ȡδ��ɵ��볧ú��¼
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetUnFinishBuyFuelTransport(CommonAppConfig.GetInstance().AppIdentifier.Replace("�������ܻ�-", ""));
        }

        /// <summary>
        /// ��ȡָ����������ɵ��볧ú��¼
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), CommonAppConfig.GetInstance().AppIdentifier.Replace("�������ܻ�-", ""));
        }

        #endregion

        #region ��������ҵ��

        bool timer_Goods_Cancel = true;

        CmcsGoodsTransport currentGoodsTransport;
        /// <summary>
        /// ��ǰ�����¼
        /// </summary>
        public CmcsGoodsTransport CurrentGoodsTransport
        {
            get { return currentGoodsTransport; }
            set
            {
                currentGoodsTransport = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), value.Id);

                    txtSupplyUnitName_Goods.Text = value.SupplyUnitName;
                    txtReceiveUnitName_Goods.Text = value.ReceiveUnitName;
                    txtGoodsTypeName_Goods.Text = value.GoodsTypeName;

                    txtFirstWeight_Goods.Text = value.FirstWeight.ToString("F2");
                    txtSecondWeight_Goods.Text = value.SecondWeight.ToString("F2");
                    txtSuttleWeight_Goods.Text = value.SuttleWeight.ToString("F2");
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), string.Empty);

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
        /// ѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_Goods_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select("where CarType='" + eCarType.��������.ToString() + "' order by CreationTime desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.InductorCoil1)
                    passCarQueuer.Enqueue(eDirection.Way1, frm.Output.CarNumber);
                else if (this.InductorCoil2)
                    passCarQueuer.Enqueue(eDirection.Way2, frm.Output.CarNumber);
                else
                    passCarQueuer.Enqueue(eDirection.UnKnow, frm.Output.CarNumber);

                this.CurrentFlowFlag = eFlowFlag.ʶ����;
            }
        }

        /// <summary>
        /// �����ŶӼ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_Goods_Click(object sender, EventArgs e)
        {
            if (!SaveGoodsTransport()) MessageBoxEx.Show("����ʧ��", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ���������¼
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
                    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;

                    UpdateLedShow("�������", "���°�");
                    UpdateSpeaker("����������°�", 1, false);

                    LoadTodayUnFinishGoodsTransport();
                    LoadTodayFinishGoodsTransport();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("����ʧ��\r\n" + ex.Message, "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("���������¼", ex);
            }

            return false;
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Goods_Click(object sender, EventArgs e)
        {
            ResetGoods();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        void ResetGoods()
        {
            this.timer_Goods_Cancel = true;

            this.CurrentFlowFlag = eFlowFlag.�ȴ�����;

            this.CurrentAutotruck = null;
            this.CurrentGoodsTransport = null;
            this.CurrentDirection = eDirection.UnKnow;

            txtTagId_Goods.ResetText();

            btnSaveTransport_Goods.Enabled = false;

            FrontGateDown();
            BackGateDown();

            UpdateLedShow("  �ȴ�����");

            // �������
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// �������������¼ҵ��ʱ��
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
                    case eFlowFlag.��֤��Ϣ:
                        #region

                        // ���Ҹó�δ��ɵ������¼
                        CmcsUnFinishTransport unFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id, eCarType.��������.ToString());
                        if (unFinishTransport != null)
                        {
                            this.CurrentGoodsTransport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(unFinishTransport.TransportId);
                            if (this.CurrentGoodsTransport != null)
                            {
                                // �ж�·������
                                string nextPlace;
                                if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentGoodsTransport.StepName, "��һ�γ���|�ڶ��γ���", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                {
                                    if (this.CurrentGoodsTransport.SuttleWeight == 0)
                                    {
                                        BackGateUp();

                                        this.CurrentFlowFlag = eFlowFlag.�ȴ��ϰ�;

                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "���ϰ�");
                                        UpdateSpeaker(this.CurrentAutotruck.CarNumber + " ���ϰ�", 1, false);
                                    }
                                    else
                                    {
                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "�ѳ���");
                                        UpdateSpeaker(this.CurrentAutotruck.CarNumber + " �ѳ���", 1, false);

                                        timer_Goods.Interval = 20000;
                                    }
                                }
                                else
                                {
                                    UpdateLedShow("·�ߴ���", "��ֹͨ��");
                                    UpdateSpeaker("·�ߴ��� ��ֹͨ�� ", 1, false);

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
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "δ�Ŷ�");
                            UpdateSpeaker(this.CurrentAutotruck.CarNumber + " δ�ҵ��ŶӼ�¼", 1, false);

                            timer_Goods.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��ϰ�:
                        #region

                        // ���ذ��Ǳ�����������С��������������ĵظ����������źţ����ж����Ѿ���ȫ�ϰ�
                        if (Hardwarer.Wber.Weight >= this.WbMinWeight && !HasCarOnEnterWay())
                        {
                            BackGateDown();

                            this.CurrentFlowFlag = eFlowFlag.�ȴ��ȶ�;
                        }

                        // ����������
                        timer_Goods.Interval = 4000;

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��ȶ�:
                        #region

                        // ���������
                        timer_Goods.Interval = 1000;

                        btnSaveTransport_Goods.Enabled = this.WbSteady;

                        UpdateLedShow(this.CurrentAutotruck.CarNumber, Hardwarer.Wber.Weight.ToString("#0.######"));

                        if (this.WbSteady)
                        {
                            if (this.AutoHandMode)
                            {
                                // �Զ�ģʽ
                                if (!SaveGoodsTransport())
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "����ʧ��");
                                    UpdateSpeaker(this.CurrentAutotruck.CarNumber + " ����ʧ�ܣ�����ϵ����Ա", 1, false);
                                }
                            }
                            else
                            {
                                // �ֶ�ģʽ 
                            }
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��뿪:
                        #region

                        // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnLeaveWay()) ResetGoods();

                        // ����������
                        timer_Goods.Interval = 4000;

                        #endregion
                        break;
                }

                // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.�ȴ�����
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
        /// ��ȡδ��ɵ��������ʼ�¼
        /// </summary>
        void LoadTodayUnFinishGoodsTransport()
        {
            superGridControl1_Goods.PrimaryGrid.DataSource = weighterDAO.GetUnFinishGoodsTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ��������ʼ�¼
        /// </summary>
        void LoadTodayFinishGoodsTransport()
        {
            superGridControl2_Goods.PrimaryGrid.DataSource = weighterDAO.GetFinishedGoodsTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region ��������

        Font directionFont = new Font("΢���ź�", 16);

        Pen redPen1 = new Pen(Color.Red, 1);
        Pen greenPen1 = new Pen(Color.Lime, 1);
        Pen redPen3 = new Pen(Color.Red, 3);
        Pen greenPen3 = new Pen(Color.Lime, 3);

        /// <summary>
        /// ��ǰ�Ǳ�����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panCurrentWeight_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                PanelEx panel = sender as PanelEx;

                int height = 12;

                // ���Ƶظ�1
                e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 1, 15, height);
                e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, panel.Height - height, 15, panel.Height - 1);
                // ���ƶ���1
                e.Graphics.DrawLine(this.InfraredSensor1 ? redPen1 : greenPen1, 35, 1, 35, height);
                e.Graphics.DrawLine(this.InfraredSensor1 ? redPen1 : greenPen1, 35, panel.Height - height, 35, panel.Height - 1);

                // ���ƶ���2
                e.Graphics.DrawLine(this.InfraredSensor2 ? redPen1 : greenPen1, panel.Width - 35, 1, panel.Width - 35, height);
                e.Graphics.DrawLine(this.InfraredSensor2 ? redPen1 : greenPen1, panel.Width - 35, panel.Height - height, panel.Width - 35, panel.Height - 1);
                // ���Ƶظ�2
                e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, panel.Width - 15, 1, panel.Width - 15, height);
                e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, panel.Width - 15, panel.Height - height, panel.Width - 15, panel.Height - 1);

                // �ϰ�����
                eDirection direction = this.CurrentDirection;
                e.Graphics.DrawString("��>", directionFont, direction == eDirection.Way1 ? Brushes.Red : Brushes.Lime, 2, 17);
                e.Graphics.DrawString("<��", directionFont, direction == eDirection.Way2 ? Brushes.Red : Brushes.Lime, panel.Width - 47, 17);
            }
            catch (Exception ex)
            {
                Log4Neter.Error("panCurrentCarNumber_Paint�쳣", ex);
            }
        }

        private void superGridControl_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // ȡ������༭
            e.Cancel = true;
        }

        /// <summary>
        /// �����к�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        /// <summary>
        /// Invoke��װ
        /// </summary>
        /// <param name="action"></param>
        public void InvokeEx(Action action)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(action);
        }

        #endregion

        /// <summary>
        /// ���Ͳ����ƻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendSamplePlan_Click(object sender, EventArgs e)
        {
            SendSamplePlan("�͵�");
        }

        /// <summary>
        /// ���²���
        /// </summary>
        /// <param name="operMode">����ģʽ��Զ�̣��͵�</param>
        private void SendSamplePlan(string operMode)
        {
            if (this.CurrentBuyFuelTransport == null)
            {
                if (operMode == "�͵�")
                    MessageBoxEx.Show("�����¼������", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    FrmDebugConsole.GetInstance().Output("Զ�����²����������¼������");

                return;
            }

            string status = commonDAO.GetSignalDataValue(this.SamplerMachineCode, eSignalDataName.ϵͳ.ToString());
            if (status == "��������")
            {
                if (operMode == "�͵�")
                    MessageBoxEx.Show("���������ϣ����ܷ��Ͳ����ƻ�", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    FrmDebugConsole.GetInstance().Output("Զ�����²��������������ϣ����ܷ��Ͳ����ƻ�");
                return;
            }

            //�жϵ�ǰ�������Ƿ����δ��ɵĲ�������δ������״̬���ó�ʧ��
            List<InfQCJXCYSampleCMD> infQCJXCYSampleCMDList = commonDAO.SelfDber.Entities<InfQCJXCYSampleCMD>("where MachineCode=:MachineCode and ResultCode = 'Ĭ��'", new { MachineCode = this.SamplerMachineCode });
            if (infQCJXCYSampleCMDList != null && infQCJXCYSampleCMDList.Count > 0)
            {
                foreach (var item in infQCJXCYSampleCMDList)
                {
                    item.ResultCode = eEquInfCmdResultCode.ʧ��.ToString();
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
                    // ����Ԥ��
                    TicketWeight = 0,
                    // ����Ԥ��
                    CarCount = 0,
                    // ����������������߼�����
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
                    ResultCode = eEquInfCmdResultCode.Ĭ��.ToString(),
                    DataFlag = 0
                };

                // ���Ͳ����ƻ�
                if (commonDAO.SelfDber.Insert<InfQCJXCYSampleCMD>(CurrentSampleCMD) > 0)
                {
                    if (operMode == "�͵�")
                        MessageBoxEx.Show("���ͳɹ�", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.None);
                    else
                        FrmDebugConsole.GetInstance().Output("Զ�����²��������ͳɹ�");

                    this.CurrentFlowFlag = eFlowFlag.�ȴ�����;
                }
            }
            else
            {
                if (operMode == "�͵�")
                    MessageBoxEx.Show("δ�ҵ���������Ϣ", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    FrmDebugConsole.GetInstance().Output("Զ�����²�����δ�ҵ���������Ϣ");
            }
        }

        /// <summary>
        /// ���ò�����״̬�ź�
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
            status = commonDAO.GetSignalDataValue(this.SamplerMachineCode, eSignalDataName.ϵͳ.ToString());
            //}
            //else
            //{
            //	status = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.ϵͳ.ToString());
            //}

            if (status == "��������")
            {
                slightCYJ.LightColor = Color.Green;
            }
            else if (status == "��������")
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

