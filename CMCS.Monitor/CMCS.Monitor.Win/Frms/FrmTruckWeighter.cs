using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.CarTransport.DAO;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using CMCS.Monitor.Win.Utilities;
using DevComponents.DotNetBar;
using Xilium.CefGlue.WindowsForms;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmTruckWeighter : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmTruckWeighter";

        CommonDAO commonDAO = CommonDAO.GetInstance();
        WeighterDAO weighterDAO = WeighterDAO.GetInstance();
        MonitorCommon monitorCommon = MonitorCommon.GetInstance();
        /// <summary>
        /// ��������
        /// </summary>
        VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

        CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

        string currentMachineCode = GlobalVars.MachineCode_QC_Weighter_1;
        /// <summary>
        /// ��ǰѡ�еĺ���
        /// </summary>
        public string CurrentMachineCode
        {
            get { return currentMachineCode; }
            set { currentMachineCode = value; }
        }

        public FrmTruckWeighter()
        {
            InitializeComponent();
            //currentMachineCode = machineCode;
        }

        private void FrmTruckWeighter_Load(object sender, EventArgs e)
        {
            FormInit();
        }

        /// <summary>
        /// �����ʼ��
        /// </summary>
        private void FormInit()
        {
#if DEBUG
            gboxTest.Visible = true;
#else
            gboxTest.Visible = false; 
#endif

            cefWebBrowser.StartUrl = SelfVars.Url_TruckWeighter;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);

            //��������
            voiceSpeaker.SetVoice(0, 100, "Girl XiaoKun");

            //���μ�����Ҫ���������ʾ����
            commonDAO.SetAppletConfig(GlobalVars.MachineCode_QC_Weighter_1, "����������Ϣ", "");
            commonDAO.SetAppletConfig(GlobalVars.MachineCode_QC_Weighter_2, "����������Ϣ", "");
            commonDAO.SetAppletConfig(GlobalVars.MachineCode_QC_Weighter_3, "����������Ϣ", "");
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;

            RequestData();

            //�״μ���ˢ���б�
            btnRefreshBuyTransport_Click(null, null);
        }

        /// <summary>
        /// ���� - ˢ��ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cefWebBrowser.Browser.Reload();
        }

        /// <summary>
        /// ���� - ����ˢ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRequestData_Click(object sender, EventArgs e)
        {
            RequestData();
        }

        /// <summary>
        /// ��������
        /// </summary>
        void RequestData()
        {
            string value = string.Empty, machineCode = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();

            datas.Clear();

            machineCode = this.CurrentMachineCode;

            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("changeSelected('" + machineCode + "');", "", 0);

            string machine = "";
            if (machineCode.Contains("1"))
                machine = "#1������";
            else if (machineCode.Contains("�ճ�"))
                machine = "�ճ���";
            else if (machineCode.Contains("3"))
                machine = "#3������";

            datas.Add(new HtmlDataItem("������_��ǰ����", machine, eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("������_1�ź�ϵͳ", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_2�ź�ϵͳ", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_2, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_3�ź�ϵͳ", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_3, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_ϵͳ", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_IO������", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.IO������_����״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_�ذ��Ǳ�", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ذ��Ǳ�_����״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_LED��", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.LED��1_����״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_������1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.������1_����״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_������2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.������2_����״̬.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("������_�Ǳ�����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString()) + " ��", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("������_�Ǳ�����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ذ��Ǳ�_�ȶ�.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.BeReady) : ColorTranslator.ToHtml(EquipmentStatusColors.Working), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_��ǰ����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("������_LED����ʾ", commonDAO.GetSignalDataValue(machineCode, "LED����ʾ��Ϣ"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("������_����", (!string.IsNullOrEmpty(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��ǰ����.ToString()))).ToString(), eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("������_�ظ�1", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ظ�1�ź�.ToString()).ToLower() == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_�ظ�2", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ظ�2�ź�.ToString()).ToLower() == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_����1", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.����1�ź�.ToString()).ToLower() == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_����2", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.����2�ź�.ToString()).ToLower() == "1" ? "#ff0000" : "#00ff00", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("������_��բ1", (commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ1����.ToString()) == "1").ToString(), eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("������_��բ2", (commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ2����.ToString()) == "1").ToString(), eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("������_����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ϰ�����.ToString()), eHtmlDataItemType.svg_scare));

            datas.Add(new HtmlDataItem("������_��������״̬", commonDAO.GetAppletConfigString(machineCode, "��������") == "1" ? "�����ѿ���" : "������ֹͣ", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("������_��������״̬", commonDAO.GetAppletConfigString(machineCode, "��������") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_����", commonDAO.GetAppletConfigString(machineCode, "��������") == "1" ? "#00ff00" : "#c0c0c0", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������_��ǰ����", commonDAO.GetAppletConfigString(machineCode, "��������") == "1" ? "#00ff00" : "#fbfbff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("������_��������״̬", commonDAO.GetAppletConfigString(machineCode, "��������") == "1" ? "�����ѿ���" : "������ֹͣ", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("������_��������״̬", commonDAO.GetAppletConfigString(machineCode, "��������") == "1" ? "#00ff00" : "#ff0000", eHtmlDataItemType.svg_color));
            // ��Ӹ���...

            if (machine == "�ճ���")
            {
                datas.Add(new HtmlDataItem("QGCY", "0", eHtmlDataItemType.btn_visible));
                datas.Add(new HtmlDataItem("TZCY", "0", eHtmlDataItemType.btn_visible));
                datas.Add(new HtmlDataItem("������_��������״̬", "false", eHtmlDataItemType.svg_visible));
            }
            else
            {
                datas.Add(new HtmlDataItem("QGCY", "1", eHtmlDataItemType.btn_visible));
                datas.Add(new HtmlDataItem("TZCY", "1", eHtmlDataItemType.btn_visible));
                datas.Add(new HtmlDataItem("������_��������״̬", "true", eHtmlDataItemType.svg_visible));
            }

            string carnumber = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��ǰ����.ToString());
            if (!string.IsNullOrEmpty(carnumber))
            {
                CmcsBuyFuelTransport transport = commonDAO.SelfDber.Entity<CmcsBuyFuelTransport>(" where CarNumber=:CarNumber order by creationtime desc", new { CarNumber = carnumber });
                if (transport != null)
                {
                    datas.Add(new HtmlDataItem("������_ë��", transport.GrossWeight.ToString() + " ��", eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("������_Ƥ��", transport.TareWeight.ToString() + " ��", eHtmlDataItemType.svg_text));
                }
            }

            //��������
            string speakerValue = commonDAO.GetSignalDataValue(machineCode, "����������Ϣ");
            if (!string.IsNullOrWhiteSpace(speakerValue))
                voiceSpeaker.Speak(speakerValue, 1, false);

            // ���͵�ҳ��
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // ���治�ɼ�ʱ��ֹͣ��������
            if (!this.Visible) return;

            RequestData();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            // ���͵�ҳ��
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("test1();", "", 0);
        }

        #region �����¼

        private void btnRefreshBuyTransport_Click(object sender, EventArgs e)
        {
            // �볧ú
            LoadTodayUnFinishBuyFuelTransport();
            LoadTodayFinishBuyFuelTransport();
        }

        /// <summary>
        /// ��ȡδ��ɵ��볧ú��¼
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetUnFinishBuyFuelTransport(this.CurrentMachineCode.Replace("�������ܻ�-", ""));
        }

        /// <summary>
        /// ��ȡָ����������ɵ��볧ú��¼
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), this.CurrentMachineCode.Replace("�������ܻ�-", ""));
        }

        #endregion

    }
}