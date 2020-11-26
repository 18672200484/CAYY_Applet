using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Enums;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using CMCS.Monitor.Win.Utilities;
using DevComponents.DotNetBar;
using Xilium.CefGlue.WindowsForms;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmTrainSampler : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// ����Ψһ��ʶ��
		/// </summary>
		public static string UniqueKey = "FrmTrainSampler";

		CommonDAO commonDAO = CommonDAO.GetInstance();
		MonitorCommon monitorCommon = MonitorCommon.GetInstance();

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

		public FrmTrainSampler()
		{
			InitializeComponent();
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

			cefWebBrowser.StartUrl = SelfVars.Url_TrainSampler;
			cefWebBrowser.Dock = DockStyle.Fill;
			cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
			cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
			panWebBrower.Controls.Add(cefWebBrowser);
		}

		void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
		{
			timer1.Enabled = true;

			RequestData();
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

			datas.Add(new HtmlDataItem("������_��ǰ����", machineCode, eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("������_1�ź�ϵͳ", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_2�ź�ϵͳ", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_2, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_3�ź�ϵͳ", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_3, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_ϵͳ", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_IO������", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.IO������_����״̬.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_�ذ��Ǳ�", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ذ��Ǳ�_����״̬.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_LED��", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.LED��1_����״̬.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_ץ�����", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.ץ�����_����״̬.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_�Ǳ�����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString()) + " ��", eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("������_�Ǳ�����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ذ��Ǳ�_�ȶ�.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.BeReady) : ColorTranslator.ToHtml(EquipmentStatusColors.Working), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_��ǰ����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("������_ë��", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.ë��.ToString()) + " ��", eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("������_Ƥ��", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.Ƥ��.ToString()) + " ��", eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("������_����", (!string.IsNullOrEmpty(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��ǰ����.ToString()))).ToString(), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("������_�ظ�1", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ظ�1�ź�.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_�ظ�2", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ظ�2�ź�.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_����1", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.����1�ź�.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_����2", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.����2�ź�.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_����3", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.����3�ź�.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������_��բ1", (commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ1����.ToString()) == "0").ToString(), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("������_��բ2", (commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ2����.ToString()) == "0").ToString(), eHtmlDataItemType.svg_visible));
			datas.Add(new HtmlDataItem("������_����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ϰ�����.ToString()), eHtmlDataItemType.svg_scare));
			// ���Ӹ���...

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

	}
}