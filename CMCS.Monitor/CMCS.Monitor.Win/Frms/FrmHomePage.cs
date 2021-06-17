using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Enums;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using DevComponents.DotNetBar;
using Xilium.CefGlue;
using Xilium.CefGlue.WindowsForms;
using CMCS.Monitor.Win.Utilities;
using CMCS.Monitor.Win.CefGlue;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Utilities;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmHomePage : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// ����Ψһ��ʶ��
		/// </summary>
		public static string UniqueKey = "FrmHomePage";

		CommonDAO commonDAO = CommonDAO.GetInstance();
		MonitorCommon monitorCommon = MonitorCommon.GetInstance();

		CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

		public FrmHomePage()
		{
			InitializeComponent();
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
			cefWebBrowser.StartUrl = SelfVars.Url_HomePage;
			cefWebBrowser.Dock = DockStyle.Fill;
			cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
			cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
			panWebBrower.Controls.Add(cefWebBrowser);
		}

		void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
		{
			timer1.Enabled = true;
			timer1.Interval = 3000;

            RequestData();
        }

		private void FrmHomePage_Load(object sender, EventArgs e)
		{
			FormInit();
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
			//datas.Add(new HtmlDataItem("���볧����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.���볧����.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("�𳵷�������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.�𳵷�������.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("�𳵳�������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.�𳵳�������.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("#1�������ѷ�����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.�ѷ�����.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("#2�������ѷ�����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.�ѷ�����.ToString()), eHtmlDataItemType.svg_text));
			//string CarNumber_1 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.��ǰ����.ToString());
			//string CarNumber_2 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.��ǰ����.ToString());
			//datas.Add(new HtmlDataItem("#1��������ǰ����", CarNumber_1, eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("#2��������ǰ����", CarNumber_2, eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("#1������", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.��ǰ����.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#2������", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.��ǰ����.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#4����ʶ��", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.��ǰ����.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#5����ʶ��", monitorCommon.ConvertBooleanToColor(string.IsNullOrEmpty(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.��ǰ����.ToString())) ? "0" : "1"), eHtmlDataItemType.svg_color));

			//datas.Add(new HtmlDataItem("������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.������.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("����ת�˳���", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.����ת�˳���.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("�����볧����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.�����볧����.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("�����������س���", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.�����������س���.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("������Ƥ����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.������Ƥ����.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("������������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.������������.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("����������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.����������.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.������.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("��������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, eSignalDataName.��������.ToString()), eHtmlDataItemType.svg_text));

			//datas.Add(new HtmlDataItem("������_��������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "������_��������"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("������_��������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "������_��������"), eHtmlDataItemType.svg_text));

			#region ���� ���ƻ�
			//if (!string.IsNullOrEmpty(CarNumber_1))
			//{
			//	BindBatch(CarNumber_1, "#2", datas);
			//}
			//if (!string.IsNullOrEmpty(CarNumber_1))
			//{
			//	BindBatch(CarNumber_2, "#4", datas);
			//}
			#endregion

			#region ����������

			machineCode = GlobalVars.MachineCode_QC_JxSampler_1;
			//datas.Add(new HtmlDataItem("����_1�Ų���_ϵͳ", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("����_1�Ų���_����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("����_1�Ų���_��բ1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ1����.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("����_1�Ų���_��բ2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ2����.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#1����", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#1����a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color1));

			machineCode = GlobalVars.MachineCode_QC_JxSampler_2;
			//datas.Add(new HtmlDataItem("����_2�Ų���_ϵͳ", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("����_2�Ų���_����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("����_2�Ų���_��բ1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ1����.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("����_2�Ų���_��բ2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ2����.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2����", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#2����a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color1));

			#endregion

			#region ������

			machineCode = GlobalVars.MachineCode_QC_Weighter_1;
			////datas.Add(new HtmlDataItem("#1��ϵͳ", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("#1����ǰ����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("#1����ǰ����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString() + "t"), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("����_1�ź�_��բ1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ1����.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("����_1�ź�_��բ2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ2����.ToString())), eHtmlDataItemType.svg_color));
			////AddDataItemBySignal(datas, machineCode, "����_1�ź�_���̵�");
			//datas.Add(new HtmlDataItem("#1�ذ�", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));


			machineCode = GlobalVars.MachineCode_QC_Weighter_2;
			////datas.Add(new HtmlDataItem("#2��ϵͳ", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("#2����ǰ����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("#2����ǰ����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString() + "t"), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("����_2�ź�_��բ1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ1����.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("����_2�ź�_��բ2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ2����.ToString())), eHtmlDataItemType.svg_color));
			////AddDataItemBySignal(datas, machineCode, "����_2�ź�_���̵�");
			//datas.Add(new HtmlDataItem("�س���", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));

			machineCode = GlobalVars.MachineCode_QC_Weighter_3;
			////datas.Add(new HtmlDataItem("#3��ϵͳ", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("#3����ǰ����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("#3����ǰ����", commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString() + "t"), eHtmlDataItemType.svg_text));
			////datas.Add(new HtmlDataItem("����_3�ź�_��բ1", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ1����.ToString())), eHtmlDataItemType.svg_color));
			////datas.Add(new HtmlDataItem("����_3�ź�_��բ2", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.��բ2����.ToString())), eHtmlDataItemType.svg_color));
			////AddDataItemBySignal(datas, machineCode, "����_3�ź�_���̵�");
			//datas.Add(new HtmlDataItem("#3�ذ�", monitorCommon.ConvertBooleanToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
			#endregion

			//datas.Add(new HtmlDataItem("�Ž�_�����ҽ�", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "�Ž�_�����ҽ�"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("�Ž�_�����ҽ�", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "�Ž�_�����ҽ�"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("�Ž�_�����ҽ�", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "�Ž�_�����ҽ�"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("�Ž�_�칫¥��", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "�Ž�_�칫¥��"), eHtmlDataItemType.svg_text));

			//����ʶ��
			
			datas.Add(new HtmlDataItem("#1����ʶ��", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest(CommonDAO.GetInstance().GetCommonAppletConfigString("#1����ʶ��IP")) ? "1" : "0"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2����ʶ��", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest(CommonDAO.GetInstance().GetCommonAppletConfigString("#2����ʶ��IP")) ? "1" : "0"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#3����ʶ��", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest(CommonDAO.GetInstance().GetCommonAppletConfigString("#3����ʶ��IP")) ? "1" : "0"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#4����ʶ��", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest(CommonDAO.GetInstance().GetCommonAppletConfigString("#4����ʶ��IP")) ? "1" : "0"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#5����ʶ��", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest(CommonDAO.GetInstance().GetCommonAppletConfigString("#5����ʶ��IP")) ? "1" : "0"), eHtmlDataItemType.svg_color));

			//�𳵲���
			datas.Add(new HtmlDataItem("#1���", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#2���", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color1));
			//datas.Add(new HtmlDataItem("#3���", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_3, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#3���", monitorCommon.ConvertMachineStatusToColor("��������"), eHtmlDataItemType.svg_color1));

			datas.Add(new HtmlDataItem("#1���a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#2���a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color1));
			//datas.Add(new HtmlDataItem("#3���a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_3, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("#3���a", monitorCommon.ConvertMachineStatusToColor("��������"), eHtmlDataItemType.svg_color1));

			//Ƥ��
			datas.Add(new HtmlDataItem("2PAƤ��", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_1, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("2PAƤ��a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_1, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("2PBƤ��", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_2, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color1));
			datas.Add(new HtmlDataItem("2PBƤ��a", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_2, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color1));

			if (commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_1, "����������") == "1")
			{
				datas.Add(new HtmlDataItem("2PAƤ��", "#ff0000" , eHtmlDataItemType.svg_color1));
				datas.Add(new HtmlDataItem("2PAƤ��a", "#ff0000", eHtmlDataItemType.svg_color1));
			}
			if (commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_2, "����������") == "1")
			{
				datas.Add(new HtmlDataItem("2PBƤ��", "#ff0000", eHtmlDataItemType.svg_color1));
				datas.Add(new HtmlDataItem("2PBƤ��a", "#ff0000", eHtmlDataItemType.svg_color1));
			}

				//����⡢
				datas.Add(new HtmlDataItem("#1�����", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest("192.168.70.21") ? "1" : "0"), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2�����", monitorCommon.ConvertBooleanToColor(CommonUtil.PingReplyTest("192.168.70.22") ? "1" : "0"), eHtmlDataItemType.svg_color));

			//��������
			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "С����.��������");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("����״̬", "#ff0000", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("����״̬", "����", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("����״̬", "#00ff00", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("����״̬", "����", eHtmlDataItemType.svg_text));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "װ����.װ���˱���");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("װ����״̬", "#ff0000", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("װ����״̬", "����", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("װ����״̬", "#00ff00", eHtmlDataItemType.svg_color));

				string zcd1 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "װ����.װ����_����������������");
				string zcd2 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "װ����.װ����_��Ͱת����������");
				if (zcd1 == "1" || zcd2 == "1")
				{
					datas.Add(new HtmlDataItem("װ����״̬", "����", eHtmlDataItemType.svg_text));
				}
				else
				{
					datas.Add(new HtmlDataItem("װ����״̬", "δ����", eHtmlDataItemType.svg_text));
				}

				//atas.Add(new HtmlDataItem("װ����״̬", "����", eHtmlDataItemType.svg_text));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "ж����.ж���˱���");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("ж����״̬", "#ff0000", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("ж����״̬", "����", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("ж����״̬", "#00ff00", eHtmlDataItemType.svg_color));

				value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HYGPJ_1, "ж����.ж����_����������������");
				if (value == "1")
				{
					datas.Add(new HtmlDataItem("ж����״̬", "����", eHtmlDataItemType.svg_text));
				}
				else
				{
					datas.Add(new HtmlDataItem("ж����״̬", "δ����", eHtmlDataItemType.svg_text));
				}

				//datas.Add(new HtmlDataItem("ж����״̬", "����", eHtmlDataItemType.svg_text));
			}


			//��Ͱ��Ϣ
			List<InfBatchMachineBarrel> barrel = Dbers.GetInstance().SelfDber.Entities<InfBatchMachineBarrel>();
			foreach (InfBatchMachineBarrel item in barrel)
			{
				if (item.BarrelStatus == 1 && item.DataStatus == 1)
				{
					datas.Add(new HtmlDataItem(item.BarrelStation + "_" + item.BarrelCode, "1", eHtmlDataItemType.svg_visible));
				}
				else
				{
					datas.Add(new HtmlDataItem(item.BarrelStation + "_" + item.BarrelCode, "0", eHtmlDataItemType.svg_visible));
				}
			}

			#region ����ҳȡ��
			DateTime dtime = DateTime.Now;
			//�쳣��Ϣ
			List<InfEquInfHitch> infHitches = Dbers.GetInstance().SelfDber.TopEntities<InfEquInfHitch>(3, " order by HitchTime desc");
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadHitchs(" + Newtonsoft.Json.JsonConvert.SerializeObject(infHitches.Select(a => new { machineCode = a.MachineCode, abnormalTime = a.HitchTime.Year < 2000 ? "" : a.HitchTime.ToString("yyyy-MM-dd HH:mm"), abnormalInfo = a.HitchDescribe })) + ");", "", 0);

			//���ջ���Ϣ
			string sql = string.Format(@"select a.transportno,a.grossqty,a.skinqty,a.suttleqty,a.marginqty from fultbtransport a left join fultbinfactorybatch b on a.infactorybatchid=b.id 
									 where b.transporttypename='��' and to_char(a.infactorytime,'yyyy-MM-dd')='{0}' and a.taredate is not null
									 order by a.taredate desc", dtime.ToString("yyyy-MM-dd"));
			DataTable dt = Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
			List<HomePageTemp> list = new List<HomePageTemp>();
			if (dt.Rows.Count > 7)
			{
				for (int i = 0; i < 7; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = dt.Rows[i]["transportno"].ToString();
					item.grossqty = Convert.ToDecimal(dt.Rows[i]["grossqty"]).ToString("F2");
					item.skinqty = Convert.ToDecimal(dt.Rows[i]["skinqty"]).ToString("F2");
					item.suttleqty = Convert.ToDecimal(dt.Rows[i]["suttleqty"]).ToString("F2");
					item.marginqty = Convert.ToDecimal(dt.Rows[i]["marginqty"]).ToString("F2");
					list.Add(item);
				}
			}
			else
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = dt.Rows[i]["transportno"].ToString();
					item.grossqty = Convert.ToDecimal(dt.Rows[i]["grossqty"]).ToString("F2");
					item.skinqty = Convert.ToDecimal(dt.Rows[i]["skinqty"]).ToString("F2");
					item.suttleqty = Convert.ToDecimal(dt.Rows[i]["suttleqty"]).ToString("F2");
					item.marginqty = Convert.ToDecimal(dt.Rows[i]["marginqty"]).ToString("F2");
					list.Add(item);
				}
				for (int i = dt.Rows.Count; i < 7; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = "";
					item.grossqty = "";
					item.skinqty = "";
					item.suttleqty = "";
					item.marginqty = "";
					list.Add(item);
				}
			}
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadHCInfo(" + Newtonsoft.Json.JsonConvert.SerializeObject(list) + ");", "", 0);

			//����������Ϣ
			string sql1 = string.Format(@"select a.transportno,a.grossqty,a.skinqty,a.suttleqty,c.GROSSPLACE from fultbtransport a left join fultbinfactorybatch b on a.infactorybatchid=b.id 
										left join cmcstbbuyfueltransport c on a.pkid=c.id
										 where b.transporttypename='����'and to_char(a.infactorytime,'yyyy-MM-dd')='{0}' and a.taredate is not null
										 order by a.taredate desc
										 ", dtime.ToString("yyyy-MM-dd"));
			DataTable dt1 = Dbers.GetInstance().SelfDber.ExecuteDataTable(sql1);
			List<HomePageTemp> list1 = new List<HomePageTemp>();
			if (dt1.Rows.Count > 7)
			{
				for (int i = 0; i < 7; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = dt1.Rows[i]["transportno"].ToString();
					item.grossqty = Convert.ToDecimal(dt1.Rows[i]["grossqty"]).ToString("F2");
					item.skinqty = Convert.ToDecimal(dt1.Rows[i]["skinqty"].ToString()).ToString("F2");
					item.suttleqty = Convert.ToDecimal(dt1.Rows[i]["suttleqty"]).ToString("F2");
					if (dt1.Rows[i]["GROSSPLACE"] != DBNull.Value)
					{
						item.grossplace = dt1.Rows[i]["GROSSPLACE"].ToString().Contains("#1") ? "#1��" : "#2��";
					}

					list1.Add(item);
				}
			}
			else
			{
				for (int i = 0; i < dt1.Rows.Count; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = dt1.Rows[i]["transportno"].ToString();
					item.grossqty = Convert.ToDecimal(dt1.Rows[i]["grossqty"]).ToString("F2");
					item.skinqty = Convert.ToDecimal(dt1.Rows[i]["skinqty"]).ToString("F2");
					item.suttleqty = Convert.ToDecimal(dt1.Rows[i]["suttleqty"]).ToString("F2");
					if (dt1.Rows[i]["GROSSPLACE"] != DBNull.Value)
					{
						item.grossplace = dt1.Rows[i]["GROSSPLACE"].ToString().Contains("#1") ? "#1��" : "#2��";
					}
					list1.Add(item);
				}
				for (int i = dt1.Rows.Count; i < 7; i++)
				{
					HomePageTemp item = new HomePageTemp();
					item.transportno = "";
					item.grossqty = "";
					item.skinqty = "";
					item.suttleqty = "";
					item.grossplace = "";
					list1.Add(item);
				}
			}
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadQCInfo(" + Newtonsoft.Json.JsonConvert.SerializeObject(list1) + ");", "", 0);

			//��������Ϣ
			datas.Add(new HtmlDataItem("�������1_���в�λ", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "���в�λ"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("�������1_�Ѵ��λ", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "�Ѵ��λ"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("�������1_δ���λ", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "δ���λ"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("�������1_������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "������"), eHtmlDataItemType.svg_text));


			datas.Add(new HtmlDataItem("�������1_��ƿ����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "��ƿ��λ"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("�������1_��ƿ�Ѵ�", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "��ƿ�Ѵ��λ"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("�������1_��ƿ����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "��ƿ��λ"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("�������1_��ƿ�Ѵ�", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "��ƿ�Ѵ��λ"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("�������1_Сƿ����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "Сƿ��λ"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("�������1_Сƿ�Ѵ�", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "Сƿ�Ѵ��λ"), eHtmlDataItemType.svg_text));

			//������Ϣ
			string beltsamplecode1 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "��������");
			if(VerifyComplete(GlobalVars.MachineCode_TrunOver_1, beltsamplecode1))
			{
				datas.Add(new HtmlDataItem("2PAƤ��_��������", "", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PAƤ��_�ƻ���", "0", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PAƤ��_�Ѳ���", "0", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("2PAƤ��_��������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "��������"), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PAƤ��_�ƻ���", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, "����������"), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PAƤ��_�Ѳ���", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_1, "�ѷ�������"), eHtmlDataItemType.svg_text));
			}

			string beltsamplecode2 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "��������");
			if (VerifyComplete(GlobalVars.MachineCode_TrunOver_2, beltsamplecode2))
			{

				datas.Add(new HtmlDataItem("2PBƤ��_��������", "", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PBƤ��_�ƻ���", "0", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PBƤ��_�Ѳ���", "0", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("2PBƤ��_��������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "��������"), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PBƤ��_�ƻ���", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, "����������"), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2PBƤ��_�Ѳ���", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_2, "�ѷ�������"), eHtmlDataItemType.svg_text));
			}

			//ȫ�Զ�������
			datas.Add(new HtmlDataItem("ʪú������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "ʪú�����") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("��ʽ������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "��ʽ������") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("�Թ�����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "�Թ������") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mmһ��Բ��������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "3mmһ��Բ��������") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("����������ϻ�", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "����������ϻ�") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("ɸ������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "ɸ������") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("3mm����Բ��������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "3mm����Բ��������") == "1" ? "#00ff00" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("�����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "�����") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������ϻ�", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "���鵥Ԫ������ϻ�") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("�����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "�����豸��߷��������ź�") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("�ҷ���", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "�����豸�ұ߷��������ź�") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("ȫˮ��", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "6mmƿװ����װ����ƿ�ź�") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("�����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "mmƿװ����װ����ƿ�ź�3") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "mmƿװ����װ����ƿ�ź�1") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("�����2", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "mmƿװ����װ����ƿ�ź�2") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("3mmú��", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "3mmһ���������϶���ú��־") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("����ú��", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "3mm�����������϶���ú��־") == "1" ? "#00c000" : "#c0c0c0", eHtmlDataItemType.svg_color));

			//ȫˮ
			datas.Add(new HtmlDataItem("�ڲ���Ʒ����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_ZXQS_1, "�ڲ���Ʒ����"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("��ǰ�¶�", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_ZXQS_1, "��ǰ�¶�"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("����״̬", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_ZXQS_1, "����״̬"), eHtmlDataItemType.svg_text));

			//����
			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "������1.�ܵ�1��λ");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("ת����1_1b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("ת����1_1b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "������1.�ܵ�2��λ");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("ת����1_2b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("ת����1_2b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "������1.�ܵ�3��λ");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("ת����1_3b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("ת����1_3b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}


			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "������1.�ܵ�4��λ");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("ת����1_4b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("ת����1_4b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "������2.�ܵ�1��λ");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("ת����2_1b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("ת����2_1b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "������2.�ܵ�2��λ");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("ת����2_2b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("ת����2_2b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "������2.�ܵ�3��λ");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("ת����2_3b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("ת����2_3b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			value = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, "������2.�ܵ�4��λ");
			if (value == "1")
			{
				datas.Add(new HtmlDataItem("ת����2_4b", "#00ff00", eHtmlDataItemType.svg_color1));
			}
			else
			{
				datas.Add(new HtmlDataItem("ת����2_4b", "#a6a8ab", eHtmlDataItemType.svg_color1));
			}

			//����
			string samplecode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.��������.ToString());
			datas.Add(new HtmlDataItem("#1����_��������", samplecode, eHtmlDataItemType.svg_text));

			string sqls = string.Format(@" select count(*) from cmcstbbuyfueltransport a left join cmcstbrcsampling b on a.samplingid=b.id
										where b.samplecode='{0}'", samplecode);
			DataTable dts= Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			string qc1djs = "0";
			if (dts.Rows.Count > 0)
			{
				qc1djs = dts.Rows[0][0].ToString();
			}
			datas.Add(new HtmlDataItem("#1����_�Ǽ���", qc1djs, eHtmlDataItemType.svg_text));

		     sqls = string.Format(@" select count(*) from cmcstbbuyfueltransport a left join cmcstbrcsampling b on a.samplingid=b.id
										where b.samplecode='{0}' and a.SamplePlace is not null", samplecode);
		     dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			string qc1ycs = "0";
			if (dts.Rows.Count > 0)
			{
				qc1djs = dts.Rows[0][0].ToString();
			}
			datas.Add(new HtmlDataItem("#1����_�Ѳ���", qc1ycs, eHtmlDataItemType.svg_text));

			//����
			samplecode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.��������.ToString());
			datas.Add(new HtmlDataItem("#2����_��������", samplecode, eHtmlDataItemType.svg_text));

			sqls = string.Format(@" select count(*) from cmcstbbuyfueltransport a left join cmcstbrcsampling b on a.samplingid=b.id
										where b.samplecode='{0}'", samplecode);
		    dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			string qc2djs = "0";
			if (dts.Rows.Count > 0)
			{
				qc2djs = dts.Rows[0][0].ToString();
			}
			datas.Add(new HtmlDataItem("#2����_�Ǽ���", qc2djs, eHtmlDataItemType.svg_text));

			sqls = string.Format(@" select count(*) from cmcstbbuyfueltransport a left join cmcstbrcsampling b on a.samplingid=b.id
										where b.samplecode='{0}' and a.SamplePlace is not null", samplecode);
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			string qc2ycs = "0";
			if (dts.Rows.Count > 0)
			{
				qc2ycs = dts.Rows[0][0].ToString();
			}
			datas.Add(new HtmlDataItem("#2����_�Ѳ���", qc2ycs, eHtmlDataItemType.svg_text));

			//���
			samplecode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.��������.ToString());
			if (VerifyCompleteHC(GlobalVars.MachineCode_HCJXCYJ_1, samplecode))
			{
				datas.Add(new HtmlDataItem("#1���_��������", "", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#1���_�ƻ���", "0", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#1���_�Ѳ���", "0", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("#1���_��������", samplecode, eHtmlDataItemType.svg_text));
				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#1�𳵻�е������' and t1.samplecode='{0}'", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hcjhs1 = "0";
				if (dts.Rows.Count > 0)
				{
					hcjhs1 = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#1���_�ƻ���", hcjhs1, eHtmlDataItemType.svg_text));

				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#1�𳵻�е������' and t1.samplecode='{0}' and t.Sampleuser is not null", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hc1jhs = "0";
				if (dts.Rows.Count > 0)
				{
					hc1jhs = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#1���_�Ѳ���", hc1jhs, eHtmlDataItemType.svg_text));
			}

			samplecode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.��������.ToString());
			if (VerifyCompleteHC(GlobalVars.MachineCode_HCJXCYJ_2, samplecode))
			{
				datas.Add(new HtmlDataItem("#2���_��������", "", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#2���_�ƻ���", "0", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#2���_�Ѳ���", "0", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("#2���_��������", samplecode, eHtmlDataItemType.svg_text));

				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#2�𳵻�е������' and t1.samplecode='{0}'", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hcjhs2 = "0";
				if (dts.Rows.Count > 0)
				{
					hcjhs2 = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#2���_�ƻ���", hcjhs2, eHtmlDataItemType.svg_text));

				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#2�𳵻�е������' and t1.samplecode='{0}' and t.Sampleuser is not null", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hc2jhs = "0";
				if (dts.Rows.Count > 0)
				{
					hc2jhs = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#2���_�Ѳ���", hc2jhs, eHtmlDataItemType.svg_text));
			}

			samplecode = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_3, eSignalDataName.��������.ToString());
			if (VerifyCompleteHC(GlobalVars.MachineCode_HCJXCYJ_3, samplecode))
			{
				datas.Add(new HtmlDataItem("#3���_��������", "", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#3���_�ƻ���", "0", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("#3���_�Ѳ���", "0", eHtmlDataItemType.svg_text));
			}
			else
			{
				datas.Add(new HtmlDataItem("#3���_��������", samplecode, eHtmlDataItemType.svg_text));

				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#3�𳵻�е������' and t1.samplecode='{0}'", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hcjhs3 = "0";
				if (dts.Rows.Count > 0)
				{
					hcjhs3 = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#3���_�ƻ���", hcjhs3, eHtmlDataItemType.svg_text));

				sqls = string.Format(@"select count(*) from inftbbeltsampleplanDetail t left join inftbbeltsampleplan t1 on t.planid=t1.id 
									where t.mchinecode='#3�𳵻�е������' and t1.samplecode='{0}' and t.Sampleuser is not null", samplecode);
				dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
				string hc3jhs = "0";
				if (dts.Rows.Count > 0)
				{
					hc3jhs = dts.Rows[0][0].ToString();
				}
				datas.Add(new HtmlDataItem("#3���_�Ѳ���", hc3jhs, eHtmlDataItemType.svg_text));
			}

			//������Ϣ
			int zc1=0,zc2 = 0, zc4 = 0,zc5=0,zc6=0;
			sqls = "select count(*) from cmcstbtransportposition a where a.tracknumber='#1'";
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				zc1 = Convert.ToInt32(dts.Rows[0][0]);
			}
			datas.Add(new HtmlDataItem("1�쳵��", "���� " + zc1.ToString() + "��", eHtmlDataItemType.svg_text));

			sqls = "select count(*) from cmcstbtransportposition a where a.tracknumber='#2'";
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				zc2 = Convert.ToInt32(dts.Rows[0][0]);
			}
			datas.Add(new HtmlDataItem("2���س���", "�س��� "+ zc2 .ToString()+ "��", eHtmlDataItemType.svg_text));

			sqls = "select count(*) from cmcstbtransportposition a where a.tracknumber='#4'";
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				zc4 = Convert.ToInt32(dts.Rows[0][0]);
			}
			datas.Add(new HtmlDataItem("4���س���", "�س��� " + zc4.ToString() + "��", eHtmlDataItemType.svg_text));

			sqls = "select count(*) from cmcstbtransportposition a where a.tracknumber='#5'";
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				zc5 = Convert.ToInt32(dts.Rows[0][0]);
			}
			datas.Add(new HtmlDataItem("5�쳵��", "���� " + zc5.ToString() + "��", eHtmlDataItemType.svg_text));

			sqls = "select count(*) from cmcstbtransportposition a where a.tracknumber='�볧'";
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				zc6 = Convert.ToInt32(dts.Rows[0][0]);
			}
			datas.Add(new HtmlDataItem("6�쳵��", "���� " + zc6.ToString() + "��", eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("�����س���", (zc2+zc4).ToString(), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("���ڿճ���", (zc1 + zc5).ToString(), eHtmlDataItemType.svg_text));

			sqls = string.Format("select count(*) from cmcstbbuyfueltransport a where to_char(a.infactorytime,'yyyy-MM-dd')='{0}'",dtime.ToString("yyyy-MM-dd"));
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			string ydjcs = "0", wcccs = "0";
			if (dts.Rows.Count > 0)
			{
				ydjcs = dts.Rows[0][0].ToString() ;
			}
			datas.Add(new HtmlDataItem("�ѵǼǳ���", ydjcs, eHtmlDataItemType.svg_text));

			sqls = string.Format("select count(*) from cmcstbbuyfueltransport a where to_char(a.outfactorytime,'yyyy-MM-dd')='2000-01-01'");
			dts = Dbers.GetInstance().SelfDber.ExecuteDataTable(sqls);
			if (dts.Rows.Count > 0)
			{
				wcccs = dts.Rows[0][0].ToString();
			}
			datas.Add(new HtmlDataItem("δ��������", wcccs, eHtmlDataItemType.svg_text));
			#endregion


			// ��Ӹ���...

			// ���͵�ҳ��
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
		}

		public void BindBatch(string carnumber, string flag, List<HtmlDataItem> datas)
		{
			DataTable data = commonDAO.SelfDber.ExecuteDataTable(string.Format(@"select t.infactorybatchid,a.batch,b.samplecode,c.makecode,d.assaycode,a.fueltype,a.transportnumber,a.fuelkindname,a.minename,a.ticketqty,a.suttleqty 
																					from fultbtransport t inner join fultbinfactorybatch a on t.infactorybatchid=a.id inner join cmcstbrcsampling b on a.id=b.infactorybatchid inner join 
																					cmcstbmake c on c.samplingid=b.id inner join cmcstbassay d on d.makeid=c.id where trunc(a.factarrivedate)=trunc(sysdate) and t.transportno='{0}' and b.samplingtype!='�˹�����'", carnumber));
			if (data != null && data.Rows.Count > 0)
			{
				datas.Add(new HtmlDataItem(flag + "���α��", data.Rows[0]["batch"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "��������", data.Rows[0]["samplecode"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "��������", data.Rows[0]["makecode"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "�������", data.Rows[0]["assaycode"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "��ú��ʽ", data.Rows[0]["fueltype"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "��ú����", data.Rows[0]["transportnumber"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "ú��", data.Rows[0]["fuelkindname"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "���", data.Rows[0]["minename"].ToString(), eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem(flag + "����", data.Rows[0]["ticketqty"].ToString(), eHtmlDataItemType.svg_text));
				//datas.Add(new HtmlDataItem(flag + "����", data.Rows[0]["suttleqty"].ToString(), eHtmlDataItemType.svg_text));
				IList<CmcsTransport> list = commonDAO.SelfDber.Entities<CmcsTransport>("where InFactoryBatchId=:InFactoryBatchId", new { InFactoryBatchId = data.Rows[0]["infactorybatchid"] });
				if (list != null)
				{
					datas.Add(new HtmlDataItem(flag + "ë��", list.Sum(a => a.GrossQty).ToString(), eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem(flag + "Ƥ��", list.Sum(a => a.SkinQty).ToString(), eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem(flag + "����", list.Sum(a => a.SuttleQty).ToString(), eHtmlDataItemType.svg_text));
				}
			}

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			// ���治�ɼ�ʱ��ֹͣ��������
			if (!this.Visible) return;

            RequestData();
        }

		/// <summary>
		/// ��Ӻ��̵ƿ����ź�
		/// </summary>
		/// <param name="datas"></param>
		/// <param name="machineCode"></param>
		/// <param name="signalValue"></param>
		private void AddDataItemBySignal(List<HtmlDataItem> datas, string machineCode, string signalValue)
		{
			if (commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�źŵ�1.ToString()) == "1")
			{
				//���
				datas.Add(new HtmlDataItem(signalValue + "_��", "#FF0000", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem(signalValue + "_��", "#CCCCCC", eHtmlDataItemType.svg_color));
			}
			else
			{
				//�̵�
				datas.Add(new HtmlDataItem(signalValue + "_��", "#CCCCCC", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem(signalValue + "_��", "#00FF00", eHtmlDataItemType.svg_color));
			}
		}

		/// <summary>
		/// �ж�Ƥ�ɵ�ǰ�����Ƿ����
		/// </summary>
		/// <param name="fcj"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		public bool VerifyComplete(string fcj, string code)
		{
			string sql = string.Format(@"select t6.*
                    from cmcstbtraincarriagepass t5 
                    left join fultbtransport t1 on t1.pkid=t5.id
                    left join fultbinfactorybatch t2 on t1.infactorybatchid=t2.id
                    left join cmcstbrcsampling t3 on t3.infactorybatchid=t2.id
                    inner join cmcstbtransportposition t6 on t5.id=t6.transportid 
                    where t6.tracknumber='{0}' and t3.samplecode='{1}'", fcj == GlobalVars.MachineCode_TrunOver_1 ? "#4" : "#2", code);
			DataTable dt = Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
			if (dt.Rows.Count > 0)
			{
				return false;
			}
			else
			{
				return true;
			}

		}

		/// <summary>
		/// �жϻ�ɵ�ǰ�����Ƿ����
		/// </summary>
		/// <param name="fcj"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		public bool VerifyCompleteHC(string cyj, string code)
		{
			string sql = string.Format(@"select * from inftbbeltsampleplan a
										left join  inftbbeltsampleplandetail b on a.id=b.PLANID
										where a.machinecode='{0}' and a.samplecode='{1}' and b.endtime< to_date('2000-01-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS')", cyj, code);
			DataTable dt = Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
			if (dt.Rows.Count > 0)
			{
				return false;
			}
			else
			{
				return true;
			}

		}
	}

	public class HomePageCefWebClient : CefWebClient
	{
		CefWebBrowser cefWebBrowser;
		CommonDAO commonDAO = CommonDAO.GetInstance();

		public HomePageCefWebClient(CefWebBrowser cefWebBrowser)
			: base(cefWebBrowser)
		{
			this.cefWebBrowser = cefWebBrowser;
		}

		protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
		{
			if (message.Name == "OpenTruckWeighter")
			{
				SelfVars.MainFrameForm.OpenTruckWeighter();
				string b=message.Arguments.GetString(0);
				if (b.Contains("1"))
				{
					SelfVars.TruckWeighterForm.CurrentMachineCode = GlobalVars.MachineCode_QC_Weighter_1;
				}
				else if (b.Contains("��")) 
				{
					SelfVars.TruckWeighterForm.CurrentMachineCode = GlobalVars.MachineCode_QC_Weighter_2;
				}
				else if (b.Contains("3"))
				{
					SelfVars.TruckWeighterForm.CurrentMachineCode = GlobalVars.MachineCode_QC_Weighter_3;
				}
			}
			else if (message.Name == "TruckWeighterChangeSelected")
				SelfVars.TruckWeighterForm.CurrentMachineCode = MonitorCommon.GetInstance().GetTruckWeighterMachineCodeBySelected(message.Arguments.GetString(0));
			else if (message.Name == "CarSamplerChangeSelected")
				SelfVars.CarSamplerForm.CurrentMachineCode = MonitorCommon.GetInstance().GetCarSamplerMachineCodeBySelected(message.Arguments.GetString(0));
			else if (message.Name == "TrainSamplerChangeSelected")
				SelfVars.TrainSamplerForm.CurrentMachineCode = message.Arguments.GetString(0);
			else if (message.Name == "OpenHikVideo")
			{
				//��ƵԤ��
				SelfVars.MainFrameForm.OpenHikVideo(MonitorCommon.GetInstance().GetVideoBySelected(message.Arguments.GetString(0)));
			}
			else if (message.Name == "SaveOperationLog")
			{
				commonDAO.SaveOperationLog(message.Arguments.GetString(0), GlobalVars.LoginUser.Name);
			}
			else if (message.Name == "TrainBeltSamplerCmd")
			{
				string cmdtype = message.Arguments.GetString(0);
				string log = "";
				InfBeltSampleCmd_KY cmd = new InfBeltSampleCmd_KY();
				cmd.DataFlag = 0;
				if (cmdtype == "LeadCar1")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.����ǣ����).ToString();
					log = "��2PAƤ����������������ǣ������";
				}
				else if (cmdtype == "LeadCar2")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.����ǣ����).ToString();
					log = "��2PBƤ����������������ǣ������";
				}
				else if (cmdtype == "MovingBelt1")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.����Ƥ��).ToString();
					log = "��2PAƤ������������������Ƥ������";
				}
				else if (cmdtype == "MovingBelt2")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.����Ƥ��).ToString();
					log = "��2PBƤ������������������Ƥ������";
				}
				else if (cmdtype == "StopSampler1")
				{

					//cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					//cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.ֹͣ����).ToString();
					//log = "��2PAƤ������������ֹͣ��������";

					//��ΪԶ�̳�Ͱ��
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.Զ�̳�Ͱ).ToString();
					log = "��2PAƤ������������Զ�̳�Ͱ����";
				}
				else if (cmdtype == "StopSampler2")
				{
					//cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					//cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.ֹͣ����).ToString();
					//log = "��2PBƤ������������ֹͣ��������";

					//��ΪԶ�̳�Ͱ��
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.Զ�̳�Ͱ).ToString();
					log = "��2PBƤ������������Զ�̳�Ͱ����";
				}
				else if (cmdtype == "AlarmReset1")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.������λ).ToString();
					log = "��2PAƤ�����������ͱ�����λ����";
				}
				else if (cmdtype == "AlarmReset2")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.������λ).ToString();
					log = "��2PBƤ�����������ͱ�����λ����";
				}
				else if (cmdtype == "FZJAlarmReset1")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.��װ��������λ).ToString();
					log = "��2PAƤ�����������ͷ�װ��������λ����";
				}
				else if (cmdtype == "FZJAlarmReset2")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.��װ��������λ).ToString();
					log = "��2PBƤ�����������ͷ�װ��������λ����";
				}
				else if (cmdtype == "StopS1")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_1;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.ֹͣ����).ToString();
					log = "��2PAƤ������������ֹͣ��������";
				}
				else if (cmdtype == "StopS2")
				{
					cmd.MachineCode = GlobalVars.MachineCode_PDCYJ_2;
					cmd.CmdCode = ((int)eEquInfSamplerCmd_KY.ֹͣ����).ToString();
					log = "��2PBƤ������������ֹͣ��������";
				}

				cmd.ResultCode = eEquInfCmdResultCode.Ĭ��.ToString();
				cmd.OperatorName = GlobalVars.LoginUser.Name;
				cmd.SendDateTime = DateTime.Now;
				cmd.SyncFlag = 0;
				if (Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd_KY>(cmd) > 0)
				{
					commonDAO.SaveOperationLog(log, GlobalVars.LoginUser.Name);
				}

			}
			else if (message.Name == "TrainBeltSamplerPlan")
			{
				string gd = message.Arguments.GetString(0) == "SendSampler1" ? "#4" : "#2";

				SelfVars.MainFrameForm.OpenSendSampleCode(gd);
			}
			else if (message.Name == "AutoMakerErrorInfo")
			{
				SelfVars.MainFrameForm.OpenAutoMakerErrorInfo();
			}
			else if (message.Name == "BatchMachineErrorInfo")
			{
				SelfVars.MainFrameForm.OpenBatchMachineErrorInfo();
			}
			else if (message.Name == "BatchMachineSendDLCMD")
			{
				SelfVars.MainFrameForm.BatchMachineSendDLCMD();

			}
			else if (message.Name == "BatchMachineSendGPCMD")
			{
				string currentMessage = string.Empty;
				InfBatchMachineCmd batchMachineCmd = new InfBatchMachineCmd();
				batchMachineCmd.InterfaceType = GlobalVars.MachineCode_HYGPJ_1;
				batchMachineCmd.MachineCode = GlobalVars.MachineCode_HYGPJ_1;
				batchMachineCmd.CmdCode = eEquInfBatchMachineCmd.��������.ToString();
				batchMachineCmd.SampleCode = "0";
				batchMachineCmd.ResultCode = eEquInfCmdResultCode.Ĭ��.ToString();
				batchMachineCmd.SyncFlag = 0;

				if (Dbers.GetInstance().SelfDber.Insert<InfBatchMachineCmd>(batchMachineCmd) > 0)
				{
					commonDAO.SaveOperationLog("���������������͹�������", GlobalVars.LoginUser.Name);
					MessageBox.Show("����ͳɹ�", "��ʾ");
				}

			}
			else if (message.Name == "BatchMachineUnloadSwip")
			{
				string currentMessage = string.Empty;
				InfBatchMachineCmd batchMachineCmd = new InfBatchMachineCmd();
				batchMachineCmd.InterfaceType = GlobalVars.MachineCode_HYGPJ_1;
				batchMachineCmd.MachineCode = GlobalVars.MachineCode_HYGPJ_1;
				batchMachineCmd.CmdCode = eEquInfBatchMachineCmd.��������.ToString();
				batchMachineCmd.SampleCode = "0";
				batchMachineCmd.ResultCode = eEquInfCmdResultCode.Ĭ��.ToString();
				batchMachineCmd.SyncFlag = 0;

				if (Dbers.GetInstance().SelfDber.Insert<InfBatchMachineCmd>(batchMachineCmd) > 0)
				{
					commonDAO.SaveOperationLog("���������������͹�������", GlobalVars.LoginUser.Name);
					MessageBox.Show("����ͳɹ�", "��ʾ");
				}

			}
			else if (message.Name == "OpenAssayManage")
			{
				SelfVars.MainFrameForm.OpenAssayManage();
			}
			else if (message.Name == "OpenBatchMachine")
			{
				SelfVars.MainFrameForm.OpenBatchMachine();
			}
			else if (message.Name == "OpenAutoCupboard")
			{
				SelfVars.MainFrameForm.OpenAutoCupboard();
			}
			else if (message.Name == "OpenSampleCabinet")
			{
				SelfVars.MainFrameForm.OpenSampleCabinet();
			}
			else if (message.Name == "OpenAutoMaker")
			{
				SelfVars.MainFrameForm.OpenAutoMaker();
			}
			else if (message.Name == "OpenTrainBeltSampler")
			{
				SelfVars.MainFrameForm.OpenTrainBeltSampler();
			}
			else if (message.Name == "OpenTrainSampler")
			{
				SelfVars.MainFrameForm.OpenTrainSampler();
				string b = message.Arguments.GetString(0);
				if (b.Contains("1"))
				{
					SelfVars.TrainSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_HCJXCYJ_1;
				}
				else if (b.Contains("2"))
				{
					SelfVars.TrainSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_HCJXCYJ_2;
				}
				else if (b.Contains("3"))
				{
					SelfVars.TrainSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_HCJXCYJ_3;
				}
			}
			else if (message.Name == "OpenCarSampler1")
			{
				SelfVars.MainFrameForm.OpenCarSampler1();
				string b = message.Arguments.GetString(0);
				if (b.Contains("1"))
				{
					SelfVars.CarSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_QCJXCYJ_1;
				}
				else if (b.Contains("2"))
				{
					SelfVars.CarSamplerForm.CurrentMachineCode = GlobalVars.MachineCode_QCJXCYJ_2;
				}
				
			}
			//else if (message.Name == "OpenTruckWeighter")
			//{
			//	SelfVars.MainFrameForm.OpenTruckWeighter();
			//}
			else if (message.Name == "FaultRecord")
			{
				string m = message.Arguments.GetString(0);
				if (m == "��������")
				{
					SelfVars.MainFrameForm.OpenFaultRecordInfo(GlobalVars.MachineCode_HYGPJ_1);
				}
				else if (m == "ȫ�Զ�������")
				{
					SelfVars.MainFrameForm.OpenFaultRecordInfo(GlobalVars.MachineCode_QZDZYJ_1);
				}
				else if (m == "2PAƤ��")
				{
					SelfVars.MainFrameForm.OpenFaultRecordInfo(GlobalVars.MachineCode_PDCYJ_1);
				}
				else if (m == "2PBƤ��")
				{
					SelfVars.MainFrameForm.OpenFaultRecordInfo(GlobalVars.MachineCode_PDCYJ_2);
				}
			}
			else if(message.Name == "TrainBeltSampleAlarmInfo")
			{
				SelfVars.MainFrameForm.OpenTrainBeltSampler_warning();
			}
			return true;
		}

		protected override CefContextMenuHandler GetContextMenuHandler()
		{
			return new CefMenuHandler();
		}

		
	}

	/// <summary>
	/// ��ҳ��ʱʵ��
	/// </summary>
	public class HomePageTemp
	{
		public virtual String transportno { get; set; }
		public virtual String grossqty { get; set; }
		public virtual string skinqty { get; set; }

		public virtual string suttleqty { get; set; }
		public virtual string marginqty { get; set; }

		public virtual string grossplace { get; set; }

	}
}