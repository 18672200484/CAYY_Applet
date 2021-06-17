using CMCS.CarTransport.BeltSampler.Core;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmWarningInfo : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// ����Ψһ��ʶ��
		/// </summary>
		public static string UniqueKey = "FrmWarningInfo";

		/// <summary>
		/// �豸���
		/// </summary>
		string MachineCode;

		public FrmWarningInfo(string machinecode)
		{
			InitializeComponent();
			this.MachineCode = machinecode;
		}

		#region Vars

		CommonDAO commonDAO = CommonDAO.GetInstance();
		RTxtOutputer rTxtOutputer;
		

		#endregion

		/// <summary>
		/// �����ʼ��
		/// </summary>
		private void InitForm()
		{
			superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			////��SuperGridControl�¼� gclmSetSampler
			//GridButtonXEditControl btnSetSampler = superGridControl1.PrimaryGrid.Columns["gclmSetSampler"].EditControl as GridButtonXEditControl;
			//btnSetSampler.Click += btnSetSampler_Click;

			lblTitle.Text = MachineCode + "��ʷ������Ϣ";
			BindGrid();
		}

		private void FrmWarningInfo_Load(object sender, EventArgs e)
		{
			
		}

		private void FrmWarningInfo_Shown(object sender, EventArgs e)
		{
			InitForm();

			//BindRCSampling(superGridControl1);
			
		}

		private void FrmWarningInfo_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		#region ����ҵ��
		

		private void timer2_Tick(object sender, EventArgs e)
		{
			timer2.Stop();
			//// 2��ִ��һ��
			//timer2.Interval = 10000;

			//try
			//{
			//	BindGrid();

			//}
			//catch (Exception ex)
			//{
			//	Log4Neter.Error("timer2_Tick", ex);
			//}
			//finally
			//{
			//	timer2.Start();
			//}
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
		/// ����Ϣ
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="machineCode">�豸����</param>
		private void BindGrid()
		{
			string sql = string.Format(@"select a.machinecode,a.hitchdescribe,a.hitchtime from inftbequinfhitch a where a.machinecode='{0}' and a.hitchtime> sysdate-30 order by a.hitchtime desc
										", MachineCode);
			DataTable dt = commonDAO.SelfDber.ExecuteDataTable(sql);
			List<WarningTemp> list = new List<WarningTemp>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				WarningTemp entity = new WarningTemp();
				entity.tDate = Convert.ToDateTime(dt.Rows[i]["hitchtime"]).ToShortDateString();
				entity.tTime = Convert.ToDateTime(dt.Rows[i]["hitchtime"]).ToShortTimeString();
				entity.MachineName = dt.Rows[i]["machinecode"].ToString();
				entity.Remark = dt.Rows[i]["hitchdescribe"].ToString();
				list.Add(entity);
			}
			superGridControl1.PrimaryGrid.DataSource = list;
		}

		/// <summary>
		/// ������λ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAlarmReset_Click(object sender, EventArgs e)
		{
			InfBeltSampleCmd_KY cmd = new InfBeltSampleCmd_KY
			{
				DataFlag = 0,
				MachineCode = GlobalVars.MachineCode_PDCYJ_1,
				ResultCode = eEquInfCmdResultCode.Ĭ��.ToString(),
				CmdCode = ((int)eEquInfSamplerCmd_KY.������λ).ToString(),
				OperatorName = GlobalVars.LoginUser.Name,
				SendDateTime = DateTime.Now,
				SyncFlag=0
			};
			InfBeltSampleCmd_KY cmd1 = new InfBeltSampleCmd_KY
			{
				DataFlag = 0,
				MachineCode = GlobalVars.MachineCode_PDCYJ_2,
				ResultCode = eEquInfCmdResultCode.Ĭ��.ToString(),
				CmdCode = ((int)eEquInfSamplerCmd_KY.������λ).ToString(),
				OperatorName = GlobalVars.LoginUser.Name,
				SendDateTime = DateTime.Now,
				SyncFlag = 0
			};
			if (Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd_KY>(cmd) > 0&& Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd_KY>(cmd1)>0)
			{
				commonDAO.SaveOperationLog("����Ƥ��������������λ����", GlobalVars.LoginUser.Name);
				MessageBoxEx.Show("������λ����ͳɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.None); return;
			}
			else
			{
				MessageBoxEx.Show("������λ�����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
			}
			
		}

		
	}
	public class WarningInfoTemp
	{
		/// <summary>
		/// ��������
		/// </summary>
		public virtual String tDate { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public virtual String tTime { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// ���ȼ�
		/// </summary>
		public virtual string Priority { get; set; }

		/// <summary>
		/// �¼�����
		/// </summary>
		public virtual string Type { get; set; }

		/// <summary>
		/// ����Ա
		/// </summary>
		public virtual string User { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public virtual string Remark { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public virtual string MachineName { get; set; }

		
	}
}
