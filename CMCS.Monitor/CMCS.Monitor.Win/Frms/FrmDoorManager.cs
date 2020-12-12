using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Monitor.DAO;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using DevComponents.DotNetBar.Metro;
using Xilium.CefGlue.WindowsForms;
using CMCS.Monitor.Win.UserControls;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;
using CMCS.Common.Entities.BaseInfo;
using HikVisionSDK.Core;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmDoorManager : MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmDoorManager";

		public FrmDoorManager()
		{
			InitializeComponent();
		}
		#region 海康视频

		/// <summary>
		/// 海康网络摄像机
		/// </summary>
		IPCer iPCer1 = new IPCer();
		bool IsLogin = false;
		#endregion

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void FormInit()
		{
			BindData();
			// 生成开门按钮
			GridButtonXEditControl btnNewCode = superGridControl1.PrimaryGrid.Columns["gcmlOpen"].EditControl as GridButtonXEditControl;
			btnNewCode.ColorTable = eButtonColor.BlueWithBackground;
			btnNewCode.Click += new EventHandler(btnOpen_Click);

			IPCer.InitSDK();
		}

		/// <summary>
		/// 开门
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnOpen_Click(object sender, EventArgs e)
		{
			GridButtonXEditControl btn = sender as GridButtonXEditControl;
			if (btn == null) return;
			CmcsCamare camera = btn.EditorCell.GridRow.DataItem as CmcsCamare;
			if (camera == null) return;
			if (IsLogin) iPCer1.LoginOut();
			if (iPCer1.Login(camera.Ip, camera.Port, camera.UserName, camera.Password) && iPCer1.OpenDoor())
			{
				IsLogin = true;
				MessageBoxEx.Show("开门成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void FrmCarMonitor_Load(object sender, EventArgs e)
		{
			FormInit();
		}

		private void BindData()
		{
			string sqlWhere = "where Type='门禁' and IP is not null ";
			if (!string.IsNullOrEmpty(txtSearch.Text))
				sqlWhere += " and Name like '%" + txtSearch.Text + "%'";
			superGridControl1.PrimaryGrid.DataSource = CommonDAO.GetInstance().SelfDber.Entities<CmcsCamare>(sqlWhere + " order by Sort");
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
			BindData();
		}

		private void FrmDoorManager_FormClosing(object sender, FormClosingEventArgs e)
		{
			IPCer.CleanupSDK();
		}
	}
}
