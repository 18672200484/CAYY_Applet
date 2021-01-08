using CMCS.CarTransport.BeltSampler.Core;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.AutoCupboard;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmSampleCabinetManager : MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmSampleCabinetManager";
		CommonDAO commonDAO = CommonDAO.GetInstance();
		RTxtOutputer rTxtOutputer;

		public FrmSampleCabinetManager()
		{
			InitializeComponent();
		}

		#region Vars
		eFlowFlag currentFlowFlag = eFlowFlag.发送命令;
		/// <summary>
		/// 当前业务流程标识
		/// </summary>
		public eFlowFlag CurrentFlowFlag
		{
			get { return currentFlowFlag; }
			set
			{
				currentFlowFlag = value;
			}
		}

		InfCYGControlCMD currentCMD;
		/// <summary>
		/// 当前命令
		/// </summary>
		public InfCYGControlCMD CurrentCMD
		{
			get { return currentCMD; }
			set { currentCMD = value; }
		}

		InfCYGControlCMDDetail currentCMDDetail;
		/// <summary>
		/// 当前命令明细
		/// </summary>
		public InfCYGControlCMDDetail CurrentCMDDetail
		{
			get { return currentCMDDetail; }
			set { currentCMDDetail = value; }
		}

		eEquInfCmdResultCode currentCmdResultCode = eEquInfCmdResultCode.默认;
		/// <summary>
		/// 当前命令执行结果 
		/// </summary>
		public eEquInfCmdResultCode CurrentCmdResultCode
		{
			get { return currentCmdResultCode; }
			set
			{
				currentCmdResultCode = value;

				//lblResult.Text = currentCmdResultCode.ToString();
			}
		}

		/// <summary>
		/// 采样机结果是否执行
		/// </summary>
		bool IsResult = false;
		#endregion


		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void FormInit()
		{
			BindData();
			//生成取样按钮
		   GridButtonXEditControl btnTake = superGridControl1.PrimaryGrid.Columns["gcmlTake"].EditControl as GridButtonXEditControl;
			btnTake.ColorTable = eButtonColor.BlueWithBackground;
			btnTake.Click += new EventHandler(btnTake_Click); 
		   //生成弃样按钮
		   GridButtonXEditControl btnClear = superGridControl1.PrimaryGrid.Columns["gcmlClear"].EditControl as GridButtonXEditControl;
			btnClear.ColorTable = eButtonColor.BlueWithBackground;
			btnClear.Click += new EventHandler(btnClear_Click);
		}

		#region 公共业务
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
					case eFlowFlag.等待执行:

						if (!IsResult)
						{
							eEquInfCmdResultCode res;
							InfCYGControlCMDDetail CmdDetail = Dbers.GetInstance().SelfDber.Get<InfCYGControlCMDDetail>(CurrentCMDDetail.Id);
							Enum.TryParse<eEquInfCmdResultCode>(CmdDetail.ResultCode, out res);

							if (res == eEquInfCmdResultCode.成功)
							{
								this.rTxtOutputer.Output("命令执行成功", eOutputType.Success);
								this.CurrentFlowFlag = eFlowFlag.执行完毕;
							}
							else if (res == eEquInfCmdResultCode.失败)
							{
								this.rTxtOutputer.Output("命令执行失败", eOutputType.Warn);
								//List<InfEquInfHitch> list_Hitch = commonDAO.GetEquInfHitch(DateTime.Now.AddMinutes(-1), DateTime.Now.AddMinutes(1), this.CurrentSampleMachine.EquipmentCode);
								//foreach (InfEquInfHitch item in list_Hitch)
								//{
								//	this.rTxtOutputer.Output("采样机:" + item.HitchDescribe, eOutputType.Error);
								//}
								this.CurrentFlowFlag = eFlowFlag.执行完毕;
							}
							IsResult = CurrentCmdResultCode != eEquInfCmdResultCode.默认;
						}

						break;
					case eFlowFlag.执行完毕:
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
			// 2秒执行一次
			timer2.Interval = 10000;

			try
			{
				BindData();
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

		#region 操作
		/// <summary>
		/// 取样
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnTake_Click(object sender, EventArgs e)
		{
			GridButtonXEditControl btn = sender as GridButtonXEditControl;
			if (btn == null) return;
			DataRowView drv = btn.EditorCell.GridRow.DataItem as DataRowView;
			if (drv == null) return;

			string code = drv.Row.ItemArray[1].ToString();

			if (this.CurrentFlowFlag == eFlowFlag.等待执行)
			{
				MessageBoxEx.Show("等待当前命令执行完成"); return;
			}

			FrmSampleTake_Select frm = new FrmSampleTake_Select("取样站点");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				CodeContent content = frm.Output;

				if (!SendTakeCMD(code, Convert.ToInt32(content.Value))) { MessageBoxEx.Show("取样命令发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

				commonDAO.SaveOperationLog("发送取样命令，样品码：" + code, GlobalVars.LoginUser.Name);
				MessageBoxEx.Show("命令发送成功，等待执行");
				timer1.Enabled = true;
				this.CurrentFlowFlag = eFlowFlag.等待执行;
			}
		}

		/// <summary>
		/// 弃样
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnClear_Click(object sender, EventArgs e)
		{
			GridButtonXEditControl btn = sender as GridButtonXEditControl;
			if (btn == null) return;
			DataRowView drv = btn.EditorCell.GridRow.DataItem as DataRowView;
			if (drv == null) return;

			string code = drv.Row.ItemArray[1].ToString();

			if (this.CurrentFlowFlag == eFlowFlag.等待执行)
			{
				MessageBoxEx.Show("等待当前命令执行完成"); return;
			}

			if (!SendClearCMD(code)) { MessageBoxEx.Show("弃样命令发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

			commonDAO.SaveOperationLog("发送弃样命令，样品码："+ code, GlobalVars.LoginUser.Name);
			MessageBoxEx.Show("命令发送成功，等待执行");
			timer1.Enabled = true;
			this.CurrentFlowFlag = eFlowFlag.等待执行;
		}

		/// <summary>
		/// 发送弃样命令
		/// </summary>
		/// <returns></returns>
		bool SendClearCMD(string code)	
		{
			CurrentCMD = Dbers.GetInstance().SelfDber.Entity<InfCYGControlCMD>(String.Format("where Bill='{0}'", code));
			if (CurrentCMD == null)
			{
				CurrentCMD = new InfCYGControlCMD();
				CurrentCMD.Id = Guid.NewGuid().ToString();
				CurrentCMD.Bill = code;
				CurrentCMD.OperPerson = GlobalVars.LoginUser.Name;
				CurrentCMD.OperType = "弃样";
				CurrentCMD.UpdateTime = DateTime.Now;
				CurrentCMD.DataFlag = 0;
				//cmd.MachineCode=entity
				CurrentCMD.Op_Dest = 41;//Convert.ToInt32(entity.SamplingStation);
				if (Dbers.GetInstance().SelfDber.Insert(CurrentCMD) > 0)
				{
					CurrentCMDDetail = new InfCYGControlCMDDetail();
					CurrentCMDDetail.CYGControlCMDId = CurrentCMD.Id;
					CurrentCMDDetail.MachineCode = GlobalVars.MachineCode_CYG1;
					CurrentCMDDetail.MakeCode = code;
					CurrentCMDDetail.UpdateTime = DateTime.Now;
					//cmddetail.Bolt_Id = "";
					CurrentCMDDetail.Status = "0";
					if (Dbers.GetInstance().SelfDber.Insert(CurrentCMDDetail) > 0)
					{
						this.rTxtOutputer.Output("弃样命令发送成功");
						return true;
					}
				}


			}
			else
			{
				CurrentCMD.OperPerson = GlobalVars.LoginUser.Name;
				CurrentCMD.OperType = "弃样";
				CurrentCMD.UpdateTime = DateTime.Now;
				CurrentCMD.DataFlag = 0;
				//infCYGControlCMD.MachineCode=entity
				CurrentCMD.Op_Dest = 41;// Convert.ToInt32(entity.SamplingStation);

				if (Dbers.GetInstance().SelfDber.Update(CurrentCMD) > 0)
				{

					CurrentCMDDetail = Dbers.GetInstance().SelfDber.Entity<InfCYGControlCMDDetail>(String.Format("where MakeCode='{0}'", code));
					if (CurrentCMDDetail == null)
					{
						CurrentCMDDetail = new InfCYGControlCMDDetail();
						CurrentCMDDetail.CYGControlCMDId = CurrentCMD.Id;
						CurrentCMDDetail.MachineCode = GlobalVars.MachineCode_CYG1;
						CurrentCMDDetail.MakeCode = code;
						CurrentCMDDetail.UpdateTime = DateTime.Now;
						//cmddetail.Bolt_Id = "";
						CurrentCMDDetail.ResultCode = "";
						CurrentCMDDetail.Status = "0";
						if (Dbers.GetInstance().SelfDber.Insert(CurrentCMDDetail) > 0)
						{
							this.rTxtOutputer.Output("弃样命令发送成功");
							return true;
						}
					}
					else
					{
						CurrentCMDDetail.CYGControlCMDId = CurrentCMD.Id;
						CurrentCMDDetail.MachineCode = GlobalVars.MachineCode_CYG1;
						CurrentCMDDetail.MakeCode = code;
						CurrentCMDDetail.UpdateTime = DateTime.Now;
						//cmddetail.Bolt_Id = "";
						CurrentCMDDetail.ResultCode = "";
						CurrentCMDDetail.Status = "0";
						if (Dbers.GetInstance().SelfDber.Update(CurrentCMDDetail) > 0)
						{
							this.rTxtOutputer.Output("弃样命令发送成功");
							return true;
						}
					}

				}

			}
			return false;
		}

		/// <summary>
		/// 发送取样命令
		/// </summary>
		/// <returns></returns>
		bool SendTakeCMD(string code,int samplingStation)
		{
			CurrentCMD = Dbers.GetInstance().SelfDber.Entity<InfCYGControlCMD>(String.Format("where Bill='{0}'", code));
			if (CurrentCMD == null)
			{
				CurrentCMD = new InfCYGControlCMD();
				CurrentCMD.Id = Guid.NewGuid().ToString();
				CurrentCMD.Bill = code;
				CurrentCMD.OperPerson = GlobalVars.LoginUser.Name;
				CurrentCMD.OperType = "取样";
				CurrentCMD.UpdateTime = DateTime.Now;
				CurrentCMD.DataFlag = 0;
				//cmd.MachineCode=entity
				CurrentCMD.Op_Dest = samplingStation;//Convert.ToInt32(entity.SamplingStation);
				if (Dbers.GetInstance().SelfDber.Insert(CurrentCMD) > 0)
				{
					CurrentCMDDetail = new InfCYGControlCMDDetail();
					CurrentCMDDetail.CYGControlCMDId = CurrentCMD.Id;
					CurrentCMDDetail.MachineCode = GlobalVars.MachineCode_CYG1;
					CurrentCMDDetail.MakeCode = code;
					CurrentCMDDetail.UpdateTime = DateTime.Now;
					//cmddetail.Bolt_Id = "";
					CurrentCMDDetail.Status = "0";
					if (Dbers.GetInstance().SelfDber.Insert(CurrentCMDDetail) > 0)
					{
						this.rTxtOutputer.Output("取样命令发送成功");
						return true;
					}
				}


			}
			else
			{
				CurrentCMD.OperPerson = GlobalVars.LoginUser.Name;
				CurrentCMD.OperType = "取样";
				CurrentCMD.UpdateTime = DateTime.Now;
				CurrentCMD.DataFlag = 0;
				//infCYGControlCMD.MachineCode=entity
				CurrentCMD.Op_Dest = samplingStation;// Convert.ToInt32(entity.SamplingStation);

				if (Dbers.GetInstance().SelfDber.Update(CurrentCMD) > 0)
				{

					CurrentCMDDetail = Dbers.GetInstance().SelfDber.Entity<InfCYGControlCMDDetail>(String.Format("where MakeCode='{0}'", code));
					if (CurrentCMDDetail == null)
					{
						CurrentCMDDetail = new InfCYGControlCMDDetail();
						CurrentCMDDetail.CYGControlCMDId = CurrentCMD.Id;
						CurrentCMDDetail.MachineCode = GlobalVars.MachineCode_CYG1;
						CurrentCMDDetail.MakeCode = code;
						CurrentCMDDetail.UpdateTime = DateTime.Now;
						//cmddetail.Bolt_Id = "";
						CurrentCMDDetail.ResultCode = "";
						CurrentCMDDetail.Status = "0";
						if (Dbers.GetInstance().SelfDber.Insert(CurrentCMDDetail) > 0)
						{
							this.rTxtOutputer.Output("取样命令发送成功");
							return true;
						}
					}
					else
					{
						CurrentCMDDetail.CYGControlCMDId = CurrentCMD.Id;
						CurrentCMDDetail.MachineCode = GlobalVars.MachineCode_CYG1;
						CurrentCMDDetail.MakeCode = code;
						CurrentCMDDetail.UpdateTime = DateTime.Now;
						//cmddetail.Bolt_Id = "";
						CurrentCMDDetail.ResultCode = "";
						CurrentCMDDetail.Status = "0";
						if (Dbers.GetInstance().SelfDber.Update(CurrentCMDDetail) > 0)
						{
							this.rTxtOutputer.Output("取样命令发送成功");
							return true;
						}
					}

				}

			}
			return false;
		}

		/// <summary>
		/// 重置
		/// </summary>
		void ResetBuyFuel()
		{
			this.CurrentFlowFlag = eFlowFlag.发送命令;
			this.CurrentCmdResultCode = eEquInfCmdResultCode.默认;
			this.CurrentCMD = null;
			this.CurrentCMDDetail = null;
			IsResult = false;
		}
		#endregion

		private void FrmCarMonitor_Load(object sender, EventArgs e)
		{
			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);
			FormInit();
		}

		private void BindData()
		{
			string sqlWhere = " where a.samplecode is not null  ";
			if (!string.IsNullOrEmpty(dtInputStart.Text)) sqlWhere += " and  to_char(a.inputtime,'yyyy-MM-dd') >='" + dtInputStart.Text + "'";
			if (!string.IsNullOrEmpty(dtInputEnd.Text)) sqlWhere += " and to_char(a.inputtime,'yyyy-MM-dd') <='" + dtInputEnd.Text + "'";
			if (!string.IsNullOrEmpty(txtSearch.Text))
				sqlWhere += " and a.samplecode like '%" + txtSearch.Text + "%'";
			string sql = string.Format(@"select a.machinename,a.samplecode,a.sampletype,a.inputtime,a.inputple,b.batch,c.name suppliername
									from cmcstbsampleinput a
									left join fultbinfactorybatch b on a.batchid=b.id
									left join fultbsupplier c on b.SUPPLIERID=c.id
										{0}
										order by a.inputtime desc", sqlWhere);
			DataTable dt = CommonDAO.GetInstance().SelfDber.ExecuteDataTable(sql);
			superGridControl1.PrimaryGrid.DataSource = dt;
		}

		#region DataGridView

		private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
		{
			// 取消编辑
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

		#endregion

		private void btnSearch_Click(object sender, EventArgs e)
		{
			BindData();
		}

		private void btnAll_Click(object sender, EventArgs e)
		{
			txtSearch.Text = string.Empty;
			dtInputStart.Text= string.Empty;
			dtInputEnd.Text = string.Empty;
			BindData();
		}

		private void FrmDoorManager_FormClosing(object sender, FormClosingEventArgs e)
		{

		}
	}

	/// <summary>
	/// 流程标识
	/// </summary>
	public enum eFlowFlag
	{
		发送命令,
		等待执行,
		执行完毕
	}

}
