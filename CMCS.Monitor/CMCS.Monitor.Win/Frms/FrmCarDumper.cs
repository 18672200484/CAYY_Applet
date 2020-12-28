using CMCS.CarTransport.BeltSampler.Core;
using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace CMCS.CarTransport.BeltSampler.Frms
{
	public partial class FrmCarDumper : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmCarDumper";

		public FrmCarDumper()
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

		private void FrmCarSampler_Load(object sender, EventArgs e)
		{
			
		}

		private void FrmCarSampler_Shown(object sender, EventArgs e)
		{
			InitForm();

			//BindRCSampling(superGridControl1);
			
		}

		private void FrmCarSampler_FormClosing(object sender, FormClosingEventArgs e)
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
				BindGD_WF(superGridControl1,"#2");
				BindGD_WF(superGridControl2,"#4");
				BindGD_YF(superGridControl3, "#1");
				BindGD_YF(superGridControl4, "#5");
				CountCH();

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
		/// 绑定翻车机信息
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="machineCode">设备编码</param>
		private void BindGD_WF(SuperGridControl superGridControl,string gd)
		{
			string sql = string.Format(@"select t2.id batchid,
										t5.ordernum,t5.trainnumber,t5.carmodel,t5.PASSTIME,case when t6.isdischarged=1 then '已翻车' else '未翻车' end IsFC,t1.GROSSQTY,t1.SKINQTY,t1.SUTTLEQTY,'' as SampleCode
										from cmcstbtraincarriagepass t5 
										left join fultbtransport t1 on t1.pkid=t5.id
										left join fultbinfactorybatch t2 on t1.infactorybatchid=t2.id
										inner join cmcstbtransportposition t6 on t5.id=t6.transportid 
										where t6.tracknumber='{0}'  and t5.passtime>sysdate-1
										order by t5.passtime,t5.ordernum
										", gd);
			DataTable list = commonDAO.SelfDber.ExecuteDataTable(sql);
			for (int i = 0; i < list.Rows.Count; i++)
			{
				if (list.Rows[i]["batchid"] != DBNull.Value)
				{
					string sql1 = string.Format(@"select SampleCode from cmcstbrcsampling t1 where t1.infactorybatchid='{0}' and t1.samplecode not like 'CYCC%'", list.Rows[i]["batchid"].ToString());
					DataTable dt = commonDAO.SelfDber.ExecuteDataTable(sql1);
					if (dt != null && dt.Rows.Count > 0)
					{
						list.Rows[i]["SampleCode"] = dt.Rows[0]["SampleCode"];
					}
				}
			}
			superGridControl.PrimaryGrid.DataSource = list;
		}

		private void BindGD_YF(SuperGridControl superGridControl, string gd)
		{
			string sql = string.Format(@"select t2.id batchid,
										t5.ordernum,t5.trainnumber,t5.carmodel,t5.PASSTIME,case when t6.isdischarged=1 then '已翻车' else '未翻车' end IsFC,t1.GROSSQTY,t1.SKINQTY,t1.SUTTLEQTY,'' as SampleCode
										from cmcstbtraincarriagepass t5 
										left join fultbtransport t1 on t1.pkid=t5.id
										left join fultbinfactorybatch t2 on t1.infactorybatchid=t2.id
										inner join cmcstbtransportposition t6 on t5.id=t6.transportid 
										where t6.tracknumber='{0}'  and t5.passtime>sysdate-1
										order by t5.passtime desc,t5.ordernum desc
										", gd);
			DataTable list = commonDAO.SelfDber.ExecuteDataTable(sql);
			for (int i = 0; i < list.Rows.Count; i++)
			{
				if (list.Rows[i]["batchid"] != DBNull.Value)
				{
					string sql1 = string.Format(@"select SampleCode from cmcstbrcsampling t1 where t1.infactorybatchid='{0}' and t1.samplecode not like 'CYCC%'", list.Rows[i]["batchid"].ToString());
					DataTable dt = commonDAO.SelfDber.ExecuteDataTable(sql1);
					if (dt != null && dt.Rows.Count > 0)
					{
						list.Rows[i]["SampleCode"] = dt.Rows[0]["SampleCode"];
					}
				}
			}
			superGridControl.PrimaryGrid.DataSource = list;
		}

		/// <summary>
		/// 计算车数
		/// </summary>
		/// <returns></returns>
		private void CountCH()
		{
			int result = 0;
			string sql = string.Format(@"select count(t6.id) zcs,sum(t6.isdischarged) yfc,count(t6.id)-sum(t6.isdischarged) wfc
						from cmcstbtraincarriagepass t5 
						inner join cmcstbtransportposition t6 on t5.id=t6.transportid 
						where  t5.passtime>sysdate-1
						order by t5.passtime desc,t5.ordernum
										");
			DataTable list = commonDAO.SelfDber.ExecuteDataTable(sql);
			if (list != null && list.Rows.Count > 0)
			{
				lblG2WFC.Text = "总车数：" + list.Rows[0]["zcs"].ToString() + " 车,已翻车：" + list.Rows[0]["yfc"].ToString() + " 车，未翻车：" + list.Rows[0]["wfc"].ToString() + " 车" ;
			}

		}

		
	}
}
