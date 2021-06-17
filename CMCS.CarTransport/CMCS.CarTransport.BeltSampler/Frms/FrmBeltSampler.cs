using CMCS.CarTransport.BeltSampler.Core;
using CMCS.CarTransport.BeltSampler.Enums;
using CMCS.CarTransport.BeltSampler.Frms.Sys;
using CMCS.CarTransport.DAO;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Common.Views;
using CMCS.Forms.UserControls;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.Editors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Linq;

namespace CMCS.CarTransport.BeltSampler.Frms
{
	public partial class FrmBeltSampler : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// ����Ψһ��ʶ��
		/// </summary>
		public static string UniqueKey = "FrmBeltSampler";

		public FrmBeltSampler()
		{
			InitializeComponent();
		}

		#region Vars

		CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
		BeltSamplerDAO beltSamplerDAO = BeltSamplerDAO.GetInstance();
		CommonDAO commonDAO = CommonDAO.GetInstance();
		RTxtOutputer rTxtOutputer;
		/// <summary>
		/// ��������
		/// </summary>
		//VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

		eFlowFlag currentFlowFlag = eFlowFlag.���ͼƻ�;
		/// <summary>
		/// ��ǰҵ�����̱�ʶ
		/// </summary>
		public eFlowFlag CurrentFlowFlag
		{
			get { return currentFlowFlag; }
			set
			{
				currentFlowFlag = value;
				panCurrentCarNumber.Text = value.ToString();
			}
		}

		/// <summary>
		/// ���������� Ĭ��#1������
		/// </summary>
		string[] sampleMachineCodes = new string[] { GlobalVars.MachineCode_HCJXCYJ_1 };
		/// <summary>
		/// ��ǰѡ�е�Ƥ���������豸
		/// </summary>
		CmcsCMEquipment currentSampleMachine;
		/// <summary>
		/// ��ǰѡ��Ĳ������豸
		/// </summary>
		public CmcsCMEquipment CurrentSampleMachine
		{
			get { return currentSampleMachine; }
			set
			{
				currentSampleMachine = value;
				if (value != null)
				{
					lblCurrSamplerName.Text = value.EquipmentName;
					if (value.EquipmentName.Contains("Ƥ��"))
					{
						cmbTrainCode.Visible = false;
						label2.Visible = false;
						btnChangeTrain.Visible = false;
						btnSystemReset.Visible = false;
						btnErrorReset.Visible = false;
					}
				}
			}
		}

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

		#endregion

		/// <summary>
		/// �����ʼ��
		/// </summary>
		private void InitForm()
		{
			superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			superGridControl2.PrimaryGrid.AutoGenerateColumns = false;
			//��SuperGridControl�¼� gclmSetSampler
			GridButtonXEditControl btnSetSampler = superGridControl2.PrimaryGrid.Columns["gclmSetSampler"].EditControl as GridButtonXEditControl;
			btnSetSampler.Click += btnSetSampler_Click;

			// �������豸���룬����������һһ��Ӧ
			sampleMachineCodes = CommonDAO.GetInstance().GetCommonAppletConfigString("�𳵲������豸����").Split('|');

			// ���ó���Զ�̿�������
			commonDAO.ResetAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);

			this.cmbCYCount.Text = "2";
		}

		private void FrmCarSampler_Load(object sender, EventArgs e)
		{
			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);
		}

		private void FrmCarSampler_Shown(object sender, EventArgs e)
		{
			InitForm();

			CreateSamplerButton();
			CreateEquStatus();
			BindRCSampling(superGridControl2);
			// ������һ����ť
			if (lypanSamplerButton.Controls.Count > 0) (lypanSamplerButton.Controls[0] as RadioButton).Checked = true;
		}

		private void FrmCarSampler_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		#region ����ҵ��
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			timer1.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag)
				{
					case eFlowFlag.�ȴ�ִ��:

						if (!IsResultSample)
						{
							CurrentCmdResultCode = beltSamplerDAO.GetSampleCmdResult(CurrentSampleCMD.Id);
							if (CurrentCmdResultCode == eEquInfCmdResultCode.�ɹ�)
							{
								this.rTxtOutputer.Output("��������ִ�гɹ�", eOutputType.Success);
								this.CurrentFlowFlag = eFlowFlag.ִ�����;
							}
							else if (CurrentCmdResultCode == eEquInfCmdResultCode.ʧ��)
							{
								this.rTxtOutputer.Output("��������ִ��ʧ��", eOutputType.Warn);
								List<InfEquInfHitch> list_Hitch = commonDAO.GetEquInfHitch(DateTime.Now.AddMinutes(-1), DateTime.Now.AddMinutes(1), this.CurrentSampleMachine.EquipmentCode);
								foreach (InfEquInfHitch item in list_Hitch)
								{
									this.rTxtOutputer.Output("������:" + item.HitchDescribe, eOutputType.Error);
								}
							}
							IsResultSample = CurrentCmdResultCode != eEquInfCmdResultCode.Ĭ��;
						}

						break;
					case eFlowFlag.ִ�����:
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

		private void timer2_Tick(object sender, EventArgs e)
		{
			timer2.Stop();
			// 2��ִ��һ��
			timer2.Interval = 2000;

			try
			{
				RefreshEquStatus();
				BindBeltSampleBarrel(superGridControl1, this.CurrentSampleMachine.EquipmentCode);
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
			if (this.CurrentSampleMachine.EquipmentName != GlobalVars.MachineCode_HCJXCYJ_3 && !this.CurrentSampleMachine.EquipmentName.Contains("Ƥ��") && cmbTrainCode.SelectedItem == null) { MessageBoxEx.Show("����ѡ�������"); return; }
			//if((int)cmbCyCount.Value == 0){ MessageBoxEx.Show("���������������0", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
			if (!SendSamplingPlan()) { MessageBoxEx.Show("�����ƻ�����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("�����ƻ����ͳɹ�");
		}
		/// <summary>
		/// ��ʼ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStartSampler_Click(object sender, EventArgs e)
		{
			//if (this.CurrentFlowFlag == eFlowFlag.�ȴ�ִ��)
			//{ MessageBoxEx.Show("�ȴ���ǰ����ִ�����"); return; }
			if (CurrentRCSampling == null) { MessageBoxEx.Show("�������õ�ǰ������"); return; }

			if (!SendSamplingCMD(eEquInfSamplerCmd.��ʼ����)) { MessageBoxEx.Show("��ʼ���������ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("��ʼ��������ͳɹ����ȴ�ִ��");
			timer1.Enabled = true;
			this.CurrentFlowFlag = eFlowFlag.�ȴ�ִ��;
		}

		/// <summary>
		/// ϵͳ��λ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSystemReset_Click(object sender, EventArgs e)
		{
			//if (this.CurrentFlowFlag == eFlowFlag.�ȴ�ִ��)
			//{ MessageBoxEx.Show("�ȴ���ǰ����ִ�����"); return; }
			if (CurrentRCSampling == null) { MessageBoxEx.Show("�������õ�ǰ������"); return; }

			if (!SendSamplingCMD(eEquInfSamplerCmd.ϵͳ��λ)) { MessageBoxEx.Show("ϵͳ��λ�����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("ϵͳ��λ����ͳɹ����ȴ�ִ��");
			timer1.Enabled = true;
			this.CurrentFlowFlag = eFlowFlag.�ȴ�ִ��;
		}
		/// <summary>
		/// ֹͣ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEndSampler_Click(object sender, EventArgs e)
		{
			//if (this.CurrentFlowFlag == eFlowFlag.�ȴ�ִ��)
			//{ MessageBoxEx.Show("�ȴ���ǰ����ִ�����"); return; }
			//if (CurrentRCSampling == null) { MessageBoxEx.Show("�������õ�ǰ������"); return; }

			if (!SendSamplingCMD(eEquInfSamplerCmd.ϵͳ��ͣ)) { MessageBoxEx.Show("ϵͳ��ͣ�����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("ϵͳ��ͣ����ͳɹ����ȴ�ִ��");
			timer1.Enabled = true;
			this.CurrentFlowFlag = eFlowFlag.�ȴ�ִ��;
		}

		/// <summary>
		/// ���ϸ�λ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnErrorReset_Click(object sender, EventArgs e)
		{

			if (!SendSamplingCMD(eEquInfSamplerCmd.���ϸ�λ)) { MessageBoxEx.Show("���ϸ�λ�����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("���ϸ�λ����ͳɹ����ȴ�ִ��");
			timer1.Enabled = true;
			this.CurrentFlowFlag = eFlowFlag.�ȴ�ִ��;
		}

		/// <summary>
		/// �л����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnChangeTrain_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.�л����)) { MessageBoxEx.Show("�л���������ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("�л��������ͳɹ����ȴ�ִ��");
			timer1.Enabled = true;
			this.CurrentFlowFlag = eFlowFlag.�ȴ�ִ��;
		}

		/// <summary>
		/// ��/β��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnHeadTailSection_Click(object sender, EventArgs e)
		{
			if (!SendSamplingCMD(eEquInfSamplerCmd.��β��)) { MessageBoxEx.Show("��/β�������ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			MessageBoxEx.Show("��/β������ͳɹ����ȴ�ִ��");
			timer1.Enabled = true;
			this.CurrentFlowFlag = eFlowFlag.�ȴ�ִ��;
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
			InfBeltSamplePlan oldBeltSamplePlan = Dbers.GetInstance().SelfDber.Entity<InfBeltSamplePlan>("where InFactoryBatchId=:InFactoryBatchId and SampleCode=:SampleCode and MachineCode=:MachineCode", new { InFactoryBatchId = this.CurrentRCSampling.BatchId, SampleCode = this.CurrentRCSampling.SampleCode, MachineCode = this.CurrentSampleMachine.EquipmentName });
			if (oldBeltSamplePlan == null)
			{
				oldBeltSamplePlan = new InfBeltSamplePlan();
				oldBeltSamplePlan.DataFlag = 0;
				oldBeltSamplePlan.InterfaceType = this.CurrentSampleMachine.InterfaceType;
				oldBeltSamplePlan.InFactoryBatchId = this.CurrentRCSampling.BatchId;
				oldBeltSamplePlan.SampleCode = this.CurrentRCSampling.SampleCode;
				oldBeltSamplePlan.FuelKindName = this.CurrentRCSampling.FuelKindName;
				oldBeltSamplePlan.Mt = 0;
				oldBeltSamplePlan.TicketWeight = 0;
				oldBeltSamplePlan.GatherType = "��Ͱ";
				oldBeltSamplePlan.TrainCode = ((ComboItem)cmbTrainCode.SelectedItem) != null ? ((ComboItem)cmbTrainCode.SelectedItem).Text : "";
				oldBeltSamplePlan.SampleType = CurrentRCSampling.SamplingType;
				oldBeltSamplePlan.MachineCode = CurrentSampleMachine.EquipmentCode;
				oldBeltSamplePlan.CarCount = this.CurrentRCSampling.TransportNumber;
				//oldBeltSamplePlan.TrainCode = "#2";
				if (oldBeltSamplePlan.SampleType == eSamplingType.��е����.ToString()|| oldBeltSamplePlan.SampleType == eSamplingType.Ƥ������.ToString())
				{
					IList<CmcsTransport> transports = commonDAO.SelfDber.Entities<CmcsTransport>("where InFactoryBatchId=:InFactoryBatchId order by OrderNumber", new { InFactoryBatchId = CurrentRCSampling.BatchId });
					foreach (CmcsTransport item in transports)
					{
						InfBeltSamplePlanDetail samplePlanDetail = new InfBeltSamplePlanDetail();
						samplePlanDetail.PlanId = oldBeltSamplePlan.Id;
						samplePlanDetail.MchineCode = this.CurrentSampleMachine.EquipmentCode;
						samplePlanDetail.CarNumber = item.TransportNo;
						samplePlanDetail.OrderNumber = item.OrderNumber;
						samplePlanDetail.SyncFlag = 0;
						samplePlanDetail.CarModel = item.TrainType;
						samplePlanDetail.CyCount = Convert.ToInt32(this.cmbCYCount.Text);//(int)dbi_CyCount.Value;
						//samplePlanDetail.TrainCode = "#2";
						Dbers.GetInstance().SelfDber.Insert<InfBeltSamplePlanDetail>(samplePlanDetail);
					}
					commonDAO.SetSignalDataValue(this.CurrentSampleMachine.EquipmentName, eSignalDataName.����.ToString(), transports.Sum(a => a.TicketQty).ToString());
				}
				commonDAO.SetSignalDataValue(this.CurrentSampleMachine.EquipmentName, eSignalDataName.��������.ToString(), this.CurrentRCSampling.SampleCode);

				return Dbers.GetInstance().SelfDber.Insert<InfBeltSamplePlan>(oldBeltSamplePlan) > 0;
			}
			else
			{
				oldBeltSamplePlan.DataFlag = 0;
				oldBeltSamplePlan.FuelKindName = this.CurrentRCSampling.FuelKindName;
				oldBeltSamplePlan.Mt = 0;
				oldBeltSamplePlan.TicketWeight = 0;
				oldBeltSamplePlan.SampleType = CurrentRCSampling.SamplingType;
				oldBeltSamplePlan.MachineCode = CurrentSampleMachine.EquipmentCode;
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
							samplePlanDetail.MchineCode = this.CurrentSampleMachine.EquipmentCode;
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
		/// ���Ͳ�������
		/// </summary>
		/// <returns></returns>
		bool SendSamplingCMD(eEquInfSamplerCmd cmd)
		{
			CurrentSampleCMD = new InfBeltSampleCmd
			{
				DataFlag = 0,
				InterfaceType = this.CurrentSampleMachine.InterfaceType,
				MachineCode = this.CurrentSampleMachine.EquipmentCode,
				ResultCode = eEquInfCmdResultCode.Ĭ��.ToString(),
				SampleCode = this.CurrentRCSampling == null ? "" : this.CurrentRCSampling.SampleCode,
				CmdCode = cmd.ToString()
			};
			if (Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd>(CurrentSampleCMD) > 0)
			{
				this.rTxtOutputer.Output("��������ͳɹ�");
				return true;
			}
			return false;
		}

		/// <summary>
		/// �����볧ú�����¼
		/// </summary>
		void ResetBuyFuel()
		{
			this.CurrentFlowFlag = eFlowFlag.ѡ��ƻ�;
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

			foreach (string cMEquipmentCode in sampleMachineCodes)
			{
				LabelX lblMachineName = new LabelX()
				{
					Text = cMEquipmentCode,
					AutoSize = true,
					Anchor = AnchorStyles.Left,
					Font = new Font("Segoe UI", 14.25f, FontStyle.Bold)
				};

				flpanEquState.Controls.Add(lblMachineName);

				LabelX uCtrlSignalLight = new LabelX()
				{
					Tag = cMEquipmentCode,
					AutoSize = true,
					Anchor = AnchorStyles.Left,
					Font = new Font("Segoe UI", 14.25f, FontStyle.Bold),
					Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
				};
				SetSystemStatusToolTip(uCtrlSignalLight);
				flpanEquState.Controls.Add(uCtrlSignalLight);
			}

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
				//��ǰѡ��Ĳ�����״̬
				if (machineCode == CurrentSampleMachine.EquipmentCode)
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

		/// <summary>
		/// ���ɲ�����ѡ��
		/// </summary>
		private void CreateSamplerButton()
		{
			foreach (string machineCode in sampleMachineCodes)
			{
				CmcsCMEquipment Equipment = CommonDAO.GetInstance().GetCMEquipmentByMachineCode(machineCode);
				RadioButton rbtnSampler = new RadioButton();
				rbtnSampler.Font = new Font("Segoe UI", 15f, FontStyle.Bold);
				rbtnSampler.Text = Equipment.EquipmentName;
				rbtnSampler.Tag = Equipment;
				rbtnSampler.AutoSize = true;
				rbtnSampler.Padding = new System.Windows.Forms.Padding(10, 0, 0, 10);
				rbtnSampler.CheckedChanged += new EventHandler(rbtnSampler_CheckedChanged);

				lypanSamplerButton.Controls.Add(rbtnSampler);
			}
		}

		void rbtnSampler_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rbtnSampler = sender as RadioButton;
			this.CurrentSampleMachine = rbtnSampler.Tag as CmcsCMEquipment;

			BindBeltSampleBarrel(superGridControl1, this.CurrentSampleMachine.EquipmentCode);
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

	
	}
}
