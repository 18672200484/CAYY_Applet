using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.CarTransport.BeltSampler.Core;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using CMCS.Monitor.Win.Utilities;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.Editors;
using Xilium.CefGlue.WindowsForms;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmTrainSampler : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// ����Ψһ��ʶ��
		/// </summary>
		public static string UniqueKey = "FrmTrainSampler";
		RTxtOutputer rTxtOutputer;
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
			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);
			timer2.Enabled = true;
			timer3.Enabled = true;

			superGridControl2.PrimaryGrid.AutoGenerateColumns = false;
			superGridControl3.PrimaryGrid.AutoGenerateColumns = false;

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

			cefWebBrowser.StartUrl = Core.SelfVars.Url_TrainSampler;
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

			commonDAO.SaveOperationLog(this.CurrentMachineCode + "���Ϳ�ʼ��������", GlobalVars.LoginUser.Name);
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
		private void BindSampling(SuperGridControl superGridControl, string machinecode, string samplecode)
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
				else if (s.Year > 2000 && e.Year < 2000)
				{
					list.Rows[i]["status"] = "���ڲ���";
				}
				else if (e.Year > 2000)
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
			if (status == "����")
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

		#region ����Զ�̲���

		View_RCSampling currentRCSampling;
		/// <summary>
		/// ��ǰ������
		/// </summary>
		public View_RCSampling CurrentRCSampling
		{
			get { return currentRCSampling; }
			set
			{
				currentRCSampling = value;
				if (value != null)
				{
					lblBatch.Text = value.Batch;
					lblFactarriveDate.Text = value.FactarriveDate.ToString("yyyy-MM-dd");
					lblSupplierName.Text = value.SupplierName;
					lblFuelKindName.Text = value.FuelKindName;
				}
				else
				{
					lblBatch.Text = "####";
					lblFactarriveDate.Text = "####";
					lblSupplierName.Text = "####";
					lblFuelKindName.Text = "####";
				}
			}
		}

		eTrainSampleFlowFlag currentFlowFlag = eTrainSampleFlowFlag.���ͼƻ�;
		/// <summary>
		/// ��ǰҵ�����̱�ʶ
		/// </summary>
		public eTrainSampleFlowFlag CurrentFlowFlag
		{
			get { return currentFlowFlag; }
			set
			{
				currentFlowFlag = value;
				panCurrentCarNumber.Text = value.ToString();
			}
		}

		InfBeltSampleCmd currentSampleCMD;
		/// <summary>
		/// ��ǰ��������
		/// </summary>
		public InfBeltSampleCmd CurrentSampleCMD
		{
			get { return currentSampleCMD; }
			set { currentSampleCMD = value; }
		}

		eEquInfCmdResultCode currentCmdResultCode = eEquInfCmdResultCode.Ĭ��;
		/// <summary>
		/// ��ǰ����ִ�н�� 
		/// </summary>
		public eEquInfCmdResultCode CurrentCmdResultCode
		{
			get { return currentCmdResultCode; }
			set
			{
				currentCmdResultCode = value;

				lblResult.Text = currentCmdResultCode.ToString();
			}
		}

		eEquInfSamplerSystemStatus currentSystemStatus = eEquInfSamplerSystemStatus.��������;
		/// <summary>
		/// ��ǰ������ϵͳ״̬
		/// </summary>
		public eEquInfSamplerSystemStatus CurrentSystemStatus
		{
			get { return currentSystemStatus; }
			set
			{
				currentSystemStatus = value;
				lblSampleState.Text = value.ToString();
			}
		}

		/// <summary>
		/// ����������Ƿ�ִ��
		/// </summary>
		bool IsResultSample = false;

		#region ����ҵ��
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer2_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			timer1.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag)
				{
					case eTrainSampleFlowFlag.�ȴ�ִ��:

						if (!IsResultSample)
						{
							CurrentCmdResultCode = BeltSamplerDAO.GetInstance().GetSampleCmdResult(CurrentSampleCMD.Id);
							if (CurrentCmdResultCode == eEquInfCmdResultCode.�ɹ�)
							{
								this.rTxtOutputer.Output("��������ִ�гɹ�", eOutputType.Success);
								this.CurrentFlowFlag = eTrainSampleFlowFlag.ִ�����;
							}
							else if (CurrentCmdResultCode == eEquInfCmdResultCode.ʧ��)
							{
								this.rTxtOutputer.Output("��������ִ��ʧ��", eOutputType.Warn);
								List<InfEquInfHitch> list_Hitch = commonDAO.GetEquInfHitch(DateTime.Now.AddMinutes(-1), DateTime.Now.AddMinutes(1), this.CurrentMachineCode);
								foreach (InfEquInfHitch item in list_Hitch)
								{
									this.rTxtOutputer.Output("������:" + item.HitchDescribe, eOutputType.Error);
								}
							}
							IsResultSample = CurrentCmdResultCode != eEquInfCmdResultCode.Ĭ��;
						}

						break;
					case eTrainSampleFlowFlag.ִ�����:
						ResetBuyFuel();
						break;
				}
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer1_Tick", ex);
			}
			finally
			{
				timer1.Start();
			}

			timer1.Start();
		}

		private void timer3_Tick(object sender, EventArgs e)
		{
			timer2.Stop();
			// 2��ִ��һ��
			timer2.Interval = 2000;

			try
			{
				CreateEquStatus();
				RefreshEquStatus();
				BindBeltSampleBarrel(superGridControl3, this.CurrentMachineCode);
				BindRCSampling(superGridControl2);
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

		#endregion

		#region ����

		/// <summary>
		/// ���Ͳ����ƻ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSendSamplePlan_Click(object sender, EventArgs e)
		{
			if (CurrentRCSampling == null) { MessageBoxEx.Show("�������õ�ǰ������"); return; }
			if (this.CurrentMachineCode != GlobalVars.MachineCode_HCJXCYJ_3 && cmbTrainCode.SelectedItem == null) { MessageBoxEx.Show("����ѡ�������"); return; }
			//if((int)cmbCyCount.Value == 0){ MessageBoxEx.Show("���������������0", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
			if (!SendSamplingPlan()) { MessageBoxEx.Show("�����ƻ�����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("�����ƻ����ͳɹ�");
		}
		/// <summary>
		/// ���õ�ǰ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSetSampler_Click(object sender, EventArgs e)
		{
			GridRow gridRow = (superGridControl2.PrimaryGrid.ActiveRow as GridRow);
			if (gridRow == null) return;

			if (MessageBoxEx.Show("�Ƿ����øü�¼Ϊ��ǰ������", "������ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
				CurrentRCSampling = gridRow.DataItem as View_RCSampling;
		}
		#endregion

		#region �볧ú����ҵ��
		/// <summary>
		/// ���Ͳ����ƻ�
		/// </summary>
		/// <returns></returns>
		bool SendSamplingPlan()
		{
			InfBeltSamplePlan oldBeltSamplePlan = Dbers.GetInstance().SelfDber.Entity<InfBeltSamplePlan>("where InFactoryBatchId=:InFactoryBatchId and SampleCode=:SampleCode and MachineCode=:MachineCode", new { InFactoryBatchId = this.CurrentRCSampling.BatchId, SampleCode = this.CurrentRCSampling.SampleCode, MachineCode = this.CurrentMachineCode });
			if (oldBeltSamplePlan == null)
			{
				oldBeltSamplePlan = new InfBeltSamplePlan();
				oldBeltSamplePlan.DataFlag = 0;
				oldBeltSamplePlan.InterfaceType = GlobalVars.InterfaceType_HCJXCYJ;
				oldBeltSamplePlan.InFactoryBatchId = this.CurrentRCSampling.BatchId;
				oldBeltSamplePlan.SampleCode = this.CurrentRCSampling.SampleCode;
				oldBeltSamplePlan.FuelKindName = this.CurrentRCSampling.FuelKindName;
				oldBeltSamplePlan.Mt = 0;
				oldBeltSamplePlan.TicketWeight = 0;
				oldBeltSamplePlan.GatherType = "��Ͱ";
				oldBeltSamplePlan.TrainCode = ((ComboItem)cmbTrainCode.SelectedItem) != null ? ((ComboItem)cmbTrainCode.SelectedItem).Text : "";
				oldBeltSamplePlan.SampleType = CurrentRCSampling.SamplingType;
				oldBeltSamplePlan.MachineCode = this.CurrentMachineCode;
				oldBeltSamplePlan.CarCount = this.CurrentRCSampling.TransportNumber;
				//oldBeltSamplePlan.TrainCode = "#2";
				if (oldBeltSamplePlan.SampleType == eSamplingType.��е����.ToString() || oldBeltSamplePlan.SampleType == eSamplingType.Ƥ������.ToString())
				{
					IList<CmcsTransport> transports = commonDAO.SelfDber.Entities<CmcsTransport>("where InFactoryBatchId=:InFactoryBatchId order by OrderNumber", new { InFactoryBatchId = CurrentRCSampling.BatchId });
					foreach (CmcsTransport item in transports)
					{
						InfBeltSamplePlanDetail samplePlanDetail = new InfBeltSamplePlanDetail();
						samplePlanDetail.PlanId = oldBeltSamplePlan.Id;
						samplePlanDetail.MchineCode = this.CurrentMachineCode;
						samplePlanDetail.CarNumber = item.TransportNo;
						samplePlanDetail.OrderNumber = item.OrderNumber;
						samplePlanDetail.SyncFlag = 0;
						samplePlanDetail.CarModel = item.TrainType;
						samplePlanDetail.CyCount = Convert.ToInt32(this.cmbCYCount.Text);//(int)dbi_CyCount.Value;
																						 //samplePlanDetail.TrainCode = "#2";
						Dbers.GetInstance().SelfDber.Insert<InfBeltSamplePlanDetail>(samplePlanDetail);
					}
					commonDAO.SetSignalDataValue(this.CurrentMachineCode, eSignalDataName.����.ToString(), transports.Sum(a => a.TicketQty).ToString());
				}
				commonDAO.SetSignalDataValue(this.CurrentMachineCode, eSignalDataName.��������.ToString(), this.CurrentRCSampling.SampleCode);

				return Dbers.GetInstance().SelfDber.Insert<InfBeltSamplePlan>(oldBeltSamplePlan) > 0;
			}
			else
			{
				oldBeltSamplePlan.DataFlag = 0;
				oldBeltSamplePlan.FuelKindName = this.CurrentRCSampling.FuelKindName;
				oldBeltSamplePlan.Mt = 0;
				oldBeltSamplePlan.TicketWeight = 0;
				oldBeltSamplePlan.SampleType = CurrentRCSampling.SamplingType;
				oldBeltSamplePlan.MachineCode = this.CurrentMachineCode;
				oldBeltSamplePlan.CarCount = this.CurrentRCSampling.TransportNumber;
				oldBeltSamplePlan.TrainCode = ((ComboItem)cmbTrainCode.SelectedItem) != null ? ((ComboItem)cmbTrainCode.SelectedItem).Text : "";
				oldBeltSamplePlan.SyncFlag = 0;
				if (oldBeltSamplePlan.SampleType == eSamplingType.��е����.ToString() || oldBeltSamplePlan.SampleType == eSamplingType.Ƥ������.ToString())
				{
					IList<CmcsTransport> transports = commonDAO.SelfDber.Entities<CmcsTransport>("where InFactoryBatchId=:InFactoryBatchId order by OrderNumber", new { InFactoryBatchId = CurrentRCSampling.BatchId });
					foreach (CmcsTransport item in transports)
					{
						InfBeltSamplePlanDetail samplePlanDetail = commonDAO.SelfDber.Entity<InfBeltSamplePlanDetail>("where PlanId=:PlanId and CarNumber=:CarNumber order by OrderNumber", new { PlanId = oldBeltSamplePlan.Id, CarNumber = item.TransportNo });
						if (samplePlanDetail == null)
						{
							samplePlanDetail = new InfBeltSamplePlanDetail();
							samplePlanDetail.PlanId = oldBeltSamplePlan.Id;
							samplePlanDetail.MchineCode = this.CurrentMachineCode;
							samplePlanDetail.CarNumber = item.TransportNo;
							samplePlanDetail.OrderNumber = item.OrderNumber;
							samplePlanDetail.SyncFlag = 0;
							samplePlanDetail.CarModel = item.TrainType;
							samplePlanDetail.CyCount = Convert.ToInt32(this.cmbCYCount.Text);// (int)dbi_CyCount.Value;
																							 //samplePlanDetail.TrainCode = "#2";
							Dbers.GetInstance().SelfDber.Insert<InfBeltSamplePlanDetail>(samplePlanDetail);
						}
					}
				}
				return Dbers.GetInstance().SelfDber.Update(oldBeltSamplePlan) > 0;
			}
		}

		/// <summary>
		/// �����볧ú�����¼
		/// </summary>
		void ResetBuyFuel()
		{
			this.CurrentFlowFlag = eTrainSampleFlowFlag.ѡ��ƻ�;
			this.CurrentCmdResultCode = eEquInfCmdResultCode.Ĭ��;
			this.CurrentSampleCMD = null;
			//this.CurrentRCSampling = null;
			IsResultSample = false;
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReset_Click(object sender, EventArgs e)
		{
			ResetBuyFuel();
		}

		#endregion

		#region �ź�ҵ��
		/// <summary>
		/// ����Ƥ��������
		/// </summary>
		private void CreateEquStatus()
		{
			flpanEquState.SuspendLayout();
			flpanEquState.Controls.Clear();
			LabelX lblMachineName = new LabelX()
			{
				Text = this.CurrentMachineCode,
				AutoSize = true,
				Anchor = AnchorStyles.Left,
				Font = new Font("Segoe UI", 14.25f, FontStyle.Bold)
			};

			flpanEquState.Controls.Add(lblMachineName);

			LabelX uCtrlSignalLight = new LabelX()
			{
				Tag = this.CurrentMachineCode,
				AutoSize = true,
				Anchor = AnchorStyles.Left,
				Font = new Font("Segoe UI", 14.25f, FontStyle.Bold),
				Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
			};
			SetSystemStatusToolTip(uCtrlSignalLight);
			flpanEquState.Controls.Add(uCtrlSignalLight);

			flpanEquState.ResumeLayout();
			if (this.flpanEquState.Controls.Count == 0)
				MessageBoxEx.Show("Ƥ��������������������δ���ã�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// ����Ƥ��������״̬
		/// </summary>
		private void RefreshEquStatus()
		{
			foreach (LabelX uCtrlSignalLight in flpanEquState.Controls.OfType<LabelX>())
			{
				if (uCtrlSignalLight.Tag == null) continue;

				string machineCode = uCtrlSignalLight.Tag.ToString();
				if (string.IsNullOrEmpty(machineCode)) continue;
				string systemStatus = CommonDAO.GetInstance().GetSignalDataValue(machineCode, eSignalDataName.�豸״̬.ToString());
				uCtrlSignalLight.Text = systemStatus;
				if (systemStatus == eEquInfSamplerSystemStatus.��������.ToString())
					uCtrlSignalLight.BackColor = EquipmentStatusColors.BeReady;
				else if (systemStatus == eEquInfSamplerSystemStatus.��������.ToString() || systemStatus == eEquInfSamplerSystemStatus.����ж��.ToString())
					uCtrlSignalLight.BackColor = EquipmentStatusColors.Working;
				else if (systemStatus == eEquInfSamplerSystemStatus.��������.ToString())
					uCtrlSignalLight.BackColor = EquipmentStatusColors.Breakdown;
				else if (systemStatus == eEquInfSamplerSystemStatus.ϵͳֹͣ.ToString())
					uCtrlSignalLight.BackColor = EquipmentStatusColors.Forbidden;

				eEquInfSamplerSystemStatus status;

				if (Enum.TryParse(systemStatus, out status))
					CurrentSystemStatus = status;
			}
		}

		/// <summary>
		/// ����ToolTip��ʾ
		/// </summary>
		private void SetSystemStatusToolTip(Control control)
		{
			this.toolTip1.SetToolTip(control, "<��ɫ> ��������\r\n<��ɫ> ��������\r\n<��ɫ> ��������\r\n<��ɫ> ϵͳֹͣ");
		}

		#endregion

		#region ����

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
		/// �󶨼�������Ϣ
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="machineCode">�豸����</param>
		private void BindBeltSampleBarrel(SuperGridControl superGridControl, string machineCode)
		{
			IList<InfEquInfSampleBarrel> list = CommonDAO.GetInstance().GetEquInfSampleBarrels(machineCode);
			superGridControl.PrimaryGrid.DataSource = list;
		}

		private void BindRCSampling(SuperGridControl superGridControl)
		{
			List<View_RCSampling> list = commonDAO.SelfDber.Entities<View_RCSampling>(string.Format("where BatchType='��' and SamplingType!='�˹�����' and to_char(SamplingDate,'yyyy-MM-dd hh24:mm:ss')>='{0}' order by SamplingDate desc", DateTime.Now.AddDays(-3).Date.ToString("yyyy-MM-dd")));
			superGridControl.PrimaryGrid.DataSource = list;
		}

		#endregion
	}
}

/// <summary>
/// ���̱�ʶ
/// </summary>
public enum eTrainSampleFlowFlag
{
	ѡ��ƻ�,
	���ͼƻ�,
	�ȴ�ִ��,
	ִ�����
}
