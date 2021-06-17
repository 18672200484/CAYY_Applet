using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using CMCS.Monitor.Win.Utilities;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
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

		string currentMachineCode = GlobalVars.MachineCode_HCJXCYJ_1;
		/// <summary>
		/// ��ǰѡ�е��豸
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

			datas.Add(new HtmlDataItem("��ǰ������", machineCode, eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("#1������״̬", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#2������״̬", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#3������״̬", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_3, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("��ǰ�豸״̬", monitorCommon.ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("������", commonDAO.GetSignalDataValue(machineCode, "��������"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("����", commonDAO.GetSignalDataValue(machineCode, "����"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("��ʼʱ��", commonDAO.GetSignalDataValue(machineCode, "��ʼʱ��"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("��ú����", commonDAO.GetSignalDataValue(machineCode, "��ú����"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("��������", commonDAO.GetSignalDataValue(machineCode, "��������"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("��ǰ����", commonDAO.GetSignalDataValue(machineCode, "��ǰ����"), eHtmlDataItemType.svg_text));

			//datas.Add(new HtmlDataItem("2������ǣ��", commonDAO.GetSignalDataValue(machineCode, "2������ǣ��"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("4������ǣ��", commonDAO.GetSignalDataValue(machineCode, "4������ǣ��"), eHtmlDataItemType.svg_text));

			//datas.Add(new HtmlDataItem("2���������", commonDAO.GetSignalDataValue(machineCode, "2���������"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("4���������", commonDAO.GetSignalDataValue(machineCode, "4���������"), eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("�϶�", monitorCommon.ConvertStatusToColor(commonDAO.GetSignalDataValue(machineCode, "�϶�")), eHtmlDataItemType.svg_color));
			string point = commonDAO.GetSignalDataValue(machineCode, "ʵʱ����");
			if (!string.IsNullOrEmpty(point))
			{
				string[] points = point.Split(',');
				if (points.Length == 4)
				{
					datas.Add(new HtmlDataItem("������", points[0], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("С������", points[1], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("��������", points[2], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("��ǰ���", "", eHtmlDataItemType.svg_text));
				}
				else if (points.Length == 5)
				{
					datas.Add(new HtmlDataItem("������", points[0], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("С������", points[1], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("��������", points[2], eHtmlDataItemType.svg_text));
					datas.Add(new HtmlDataItem("��ǰ���", points[4], eHtmlDataItemType.svg_text));

				}
			}

			datas.Add(new HtmlDataItem("ǰ�߽�", commonDAO.GetSignalDataValue(machineCode, "��ǰ��λ") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("��߽�", commonDAO.GetSignalDataValue(machineCode, "С������λ") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("�ϱ߽�", commonDAO.GetSignalDataValue(machineCode, "��������λ") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("��߽�", commonDAO.GetSignalDataValue(machineCode, "�󳵺���λ") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("�ұ߽�", commonDAO.GetSignalDataValue(machineCode, "С������λ") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("�±߽�", commonDAO.GetSignalDataValue(machineCode, "��������λ") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			
			datas.Add(new HtmlDataItem("��λ��", commonDAO.GetSignalDataValue(machineCode, "���϶�����λ") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("�ر�λ��", commonDAO.GetSignalDataValue(machineCode, "���϶��ص�λ") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
			
			datas.Add(new HtmlDataItem("��", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "��")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("С��", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "С��")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("����", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "����")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("��������", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "����ͷ")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("���϶�", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "�϶�")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("������", ConvertMachineToColor(commonDAO.GetSignalDataValue(machineCode, "�ֿ��")), eHtmlDataItemType.svg_color));

			
			if (machineCode.Contains("3"))
			{
				datas.Add(new HtmlDataItem("2���������", commonDAO.GetSignalDataValue(machineCode, "�������") == "�������" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("�����½���", commonDAO.GetSignalDataValue(machineCode, "�����½�") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("�쳵������", commonDAO.GetSignalDataValue(machineCode, "������") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("2������ǣ��", commonDAO.GetSignalDataValue(machineCode, "����ǣ��") == "��ֹǣ��" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
				

				datas.Add(new HtmlDataItem("2���������l", "�������", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("�����½���l", "�����½�", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("�쳵������l", "�쳵����", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2������ǣ��l", "��ֹǣ��", eHtmlDataItemType.svg_text));

				datas.Add(new HtmlDataItem("4���������", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4���������l", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("�����½���", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("�����½���l", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("�쳵������", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("�쳵������l", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4������ǣ��", "false", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4������ǣ��l", "false", eHtmlDataItemType.svg_visible));
			}
			else
			{
				datas.Add(new HtmlDataItem("2���������", commonDAO.GetSignalDataValue(machineCode, "2���������") == "�������" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("4���������", commonDAO.GetSignalDataValue(machineCode, "4���������") == "�������" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

				datas.Add(new HtmlDataItem("�����½���", commonDAO.GetSignalDataValue(machineCode, "��������½�") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("�����½���", commonDAO.GetSignalDataValue(machineCode, "�ҵ������½�") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

				datas.Add(new HtmlDataItem("�쳵������", commonDAO.GetSignalDataValue(machineCode, "�����⳵��") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("�쳵������", commonDAO.GetSignalDataValue(machineCode, "�ҵ���⳵��") == "1" ? "#00ff00" : "#c8c8c8", eHtmlDataItemType.svg_color));

				datas.Add(new HtmlDataItem("2������ǣ��", commonDAO.GetSignalDataValue(machineCode, "2������ǣ��") == "��ֹǣ��" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));
				datas.Add(new HtmlDataItem("4������ǣ��", commonDAO.GetSignalDataValue(machineCode, "4������ǣ��") == "��ֹǣ��" ? "#ff0000" : "#c8c8c8", eHtmlDataItemType.svg_color));

				datas.Add(new HtmlDataItem("2���������l", "2���������", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("�����½���l", "�����½���", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("�쳵������l", "�쳵������", eHtmlDataItemType.svg_text));
				datas.Add(new HtmlDataItem("2������ǣ��l", "2����ֹǣ��", eHtmlDataItemType.svg_text));

				datas.Add(new HtmlDataItem("4���������", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4���������l", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("�����½���", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("�����½���l", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("�쳵������", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("�쳵������l", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4������ǣ��", "true", eHtmlDataItemType.svg_visible));
				datas.Add(new HtmlDataItem("4������ǣ��l", "true", eHtmlDataItemType.svg_visible));


			}

			// ��Ӹ���...

			// ���͵�ҳ��
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

			BindSampling(superGridControl1, machineCode, commonDAO.GetSignalDataValue(machineCode, "��������"));
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

		private void btnStartSampler_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.��ʼ����)) { MessageBoxEx.Show("��ʼ���������ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("��ʼ��������ͳɹ����ȴ�ִ��");

			commonDAO.SaveOperationLog(this.CurrentMachineCode+ "���Ϳ�ʼ��������", GlobalVars.LoginUser.Name);
		}

		private void btnEndSampler_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.ϵͳ��ͣ)) { MessageBoxEx.Show("ϵͳ��ͣ�����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("ϵͳ��ͣ����ͳɹ����ȴ�ִ��");

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "����ϵͳ��ͣ����", GlobalVars.LoginUser.Name);
		}

		private void btnSystemReset_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.ϵͳ��λ)) { MessageBoxEx.Show("ϵͳ��λ�����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("ϵͳ��λ����ͳɹ����ȴ�ִ��");

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "����ϵͳ��λ����", GlobalVars.LoginUser.Name);
		}

		private void btnErrorReset_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.���ϸ�λ)) { MessageBoxEx.Show("���ϸ�λ�����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("���ϸ�λ����ͳɹ����ȴ�ִ��");

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "���͹��ϸ�λ����", GlobalVars.LoginUser.Name);
		}

		private void btnChangeTrain_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.�л����)) { MessageBoxEx.Show("�л���������ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("�л��������ͳɹ����ȴ�ִ��");

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "�����л��������", GlobalVars.LoginUser.Name);
		}

		private void btnHeadTailSection_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.��β��)) { MessageBoxEx.Show("��/β�������ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("��/β������ͳɹ����ȴ�ִ��");

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "������/β������", GlobalVars.LoginUser.Name);
		}

		/// <summary>
		/// ���Ͳ�������
		/// </summary>
		/// <returns></returns>
		bool SendSamplingCMD(eEquInfSamplerCmd cmd)
		{
			CmcsCMEquipment Equipment = CommonDAO.GetInstance().GetCMEquipmentByMachineCode(this.CurrentMachineCode);
			InfBeltSampleCmd CurrentSampleCMD = new InfBeltSampleCmd
			{
				DataFlag = 0,
				InterfaceType = Equipment.InterfaceType,
				MachineCode = Equipment.EquipmentCode,
				ResultCode = eEquInfCmdResultCode.Ĭ��.ToString(),
				SampleCode = commonDAO.GetSignalDataValue(this.CurrentMachineCode, "��������"),
				CmdCode = cmd.ToString()
			};
			if (Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd>(CurrentSampleCMD) > 0)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// �󶨲�����Ϣ
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="machineCode">�豸����</param>
		private void BindSampling(SuperGridControl superGridControl, string machinecode,string samplecode)
		{
			string sql = string.Format(@"SELECT T1.SAMPLECODE,T2.CARNUMBER,T2.CARMODEL,T2.ORDERNUMBER,T2.CYCOUNT,T1.TRAINCODE,T2.STARTTIME,T2.ENDTIME,'' STATUS FROM INFTBBELTSAMPLEPLANDETAIL T2 LEFT JOIN INFTBBELTSAMPLEPLAN T1 ON T2.PLANID=T1.ID
									WHERE T1.MACHINECODE='{0}'  AND T1.SAMPLECODE='{1}' ORDER BY T2.ORDERNUMBER
										", machinecode, samplecode);
			DataTable list = commonDAO.SelfDber.ExecuteDataTable(sql);
			for (int i = 0; i < list.Rows.Count; i++)
			{
				DateTime s = Convert.ToDateTime(list.Rows[i]["starttime"]);
				DateTime e = Convert.ToDateTime(list.Rows[i]["endtime"]);
				if (s.Year < 2000)
				{
					list.Rows[i]["status"] = "������";
				}
				else if(s.Year>2000&&e.Year<2000)
				{
					list.Rows[i]["status"] = "���ڲ���";
				}
				else if(e.Year > 2000)
				{
					list.Rows[i]["status"] = "�Ѳ���";
				}
			}
			superGridControl.PrimaryGrid.DataSource = list;
		}

		/// <summary>
		/// ת���豸״̬Ϊ��ɫֵ
		/// </summary>
		/// <param name="systemStatus">ϵͳ״̬</param>
		/// <returns></returns>
		public string ConvertMachineToColor(string status)
		{
			if (status== "����")
				return "#ffff00";
			else if (status == "����")
				return "#ff0000";
			else
				return "#c8c8c8";
		}

		/// <summary>
		/// ��ʾ��ʷ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFaultRecord_Click(object sender, EventArgs e)
		{
			FrmWarningInfo frm = new FrmWarningInfo(this.CurrentMachineCode);
			frm.ShowDialog();
		}
	}
}