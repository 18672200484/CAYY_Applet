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

namespace CMCS.CarTransport.BeltSampler.Frms
{
	public partial class FrmTrainSampler_Warning : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmTrainSampler_Warning";

		public FrmTrainSampler_Warning()
		{
			InitializeComponent();
		}

		#region Vars

		CommonDAO commonDAO = CommonDAO.GetInstance();
		RTxtOutputer rTxtOutputer;
		

		#endregion

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void InitForm()
		{
			superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			////绑定SuperGridControl事件 gclmSetSampler
			//GridButtonXEditControl btnSetSampler = superGridControl1.PrimaryGrid.Columns["gclmSetSampler"].EditControl as GridButtonXEditControl;
			//btnSetSampler.Click += btnSetSampler_Click;


		}

		private void FrmTrainSampler_Warning_Load(object sender, EventArgs e)
		{
			
		}

		private void FrmTrainSampler_Warning_Shown(object sender, EventArgs e)
		{
			InitForm();

			//BindRCSampling(superGridControl1);
			
		}

		private void FrmTrainSampler_Warning_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		#region 公共业务
		

		private void timer2_Tick(object sender, EventArgs e)
		{
			timer2.Stop();
			// 2秒执行一次
			timer2.Interval = 10000;

			try
			{
				BindGrid();

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


		#region 其他

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
		/// 绑定信息
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="machineCode">设备编码</param>
		private void BindGrid()
		{
			string sql = string.Format(@"select
										a.signalprefix,a.signalname,a.updatetime
										from cmcstbsignaldata a where a.signalprefix like '%皮带采样机%' and a.signalname like '%M_%'and a.signalvalue='1' order by updatetime
										");
			DataTable dt = commonDAO.SelfDber.ExecuteDataTable(sql);
			List<WarningTemp> list = new List<WarningTemp>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				WarningTemp entity = new WarningTemp();
				entity.tDate = Convert.ToDateTime(dt.Rows[i]["updatetime"]).ToShortDateString();
				entity.tTime = Convert.ToDateTime(dt.Rows[i]["updatetime"]).ToShortTimeString();
				entity.MachineName = dt.Rows[i]["signalprefix"].ToString();
				if (entity.MachineName.Contains("2PA"))
				{
					entity.Name = "A侧" + dt.Rows[i]["signalname"].ToString().TrimStart('M');
				}
				else if(entity.MachineName.Contains("2PB"))
				{
					entity.Name = "B侧" + dt.Rows[i]["signalname"].ToString().TrimStart('M');
				}
				entity.Priority = "1";
				entity.Type = "报警";
				entity.User = "---";
				entity.Remark = entity.Name;
				list.Add(entity);
			}
			superGridControl1.PrimaryGrid.DataSource = list;
		}

		/// <summary>
		/// 报警复位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAlarmReset_Click(object sender, EventArgs e)
		{
			InfBeltSampleCmd_KY cmd = new InfBeltSampleCmd_KY
			{
				DataFlag = 0,
				MachineCode = GlobalVars.MachineCode_PDCYJ_1,
				ResultCode = eEquInfCmdResultCode.默认.ToString(),
				CmdCode = ((int)eEquInfSamplerCmd_KY.报警复位).ToString(),
				OperatorName = GlobalVars.LoginUser.Name,
				SendDateTime = DateTime.Now,
				SyncFlag=0
			};
			InfBeltSampleCmd_KY cmd1 = new InfBeltSampleCmd_KY
			{
				DataFlag = 0,
				MachineCode = GlobalVars.MachineCode_PDCYJ_2,
				ResultCode = eEquInfCmdResultCode.默认.ToString(),
				CmdCode = ((int)eEquInfSamplerCmd_KY.报警复位).ToString(),
				OperatorName = GlobalVars.LoginUser.Name,
				SendDateTime = DateTime.Now,
				SyncFlag = 0
			};
			if (Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd_KY>(cmd) > 0&& Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd_KY>(cmd1)>0)
			{
				commonDAO.SaveOperationLog("发送皮带采样机报警复位命令", GlobalVars.LoginUser.Name);
				MessageBoxEx.Show("报警复位命令发送成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.None); return;
			}
			else
			{
				MessageBoxEx.Show("报警复位命令发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
			}
			
		}

		/// <summary>
		/// 报警复位2
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAlarmReset2_Click(object sender, EventArgs e)
		{
			InfBeltSampleCmd_KY cmd = new InfBeltSampleCmd_KY
			{
				DataFlag = 0,
				MachineCode = GlobalVars.MachineCode_PDCYJ_1,
				ResultCode = eEquInfCmdResultCode.默认.ToString(),
				CmdCode = ((int)eEquInfSamplerCmd_KY.封装机报警复位).ToString(),
				OperatorName = GlobalVars.LoginUser.Name,
				SendDateTime = DateTime.Now,
				SyncFlag = 0
			};
			InfBeltSampleCmd_KY cmd1 = new InfBeltSampleCmd_KY
			{
				DataFlag = 0,
				MachineCode = GlobalVars.MachineCode_PDCYJ_2,
				ResultCode = eEquInfCmdResultCode.默认.ToString(),
				CmdCode = ((int)eEquInfSamplerCmd_KY.封装机报警复位).ToString(),
				OperatorName = GlobalVars.LoginUser.Name,
				SendDateTime = DateTime.Now,
				SyncFlag = 0
			};
			if (Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd_KY>(cmd) > 0&& Dbers.GetInstance().SelfDber.Insert<InfBeltSampleCmd_KY>(cmd1) > 0)
			{
				commonDAO.SaveOperationLog("发送皮带采样机封装机报警复位命令", GlobalVars.LoginUser.Name);
				MessageBoxEx.Show("封装机报警复位命令发送成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.None); return;
			}
			else
			{
				MessageBoxEx.Show("封装机报警复位命令发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
			}
		}
	}
	public class WarningTemp
	{
		/// <summary>
		/// 报警日期
		/// </summary>
		public virtual String tDate { get; set; }

		/// <summary>
		/// 报警时间
		/// </summary>
		public virtual String tTime { get; set; }

		/// <summary>
		/// 变量名
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// 优先级
		/// </summary>
		public virtual string Priority { get; set; }

		/// <summary>
		/// 事件类型
		/// </summary>
		public virtual string Type { get; set; }

		/// <summary>
		/// 操作员
		/// </summary>
		public virtual string User { get; set; }

		/// <summary>
		/// 变量描述
		/// </summary>
		public virtual string Remark { get; set; }

		/// <summary>
		/// 机器名
		/// </summary>
		public virtual string MachineName { get; set; }

		
	}
}
