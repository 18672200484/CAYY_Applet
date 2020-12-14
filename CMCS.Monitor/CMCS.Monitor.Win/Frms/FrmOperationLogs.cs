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
using CMCS.Common.Entities.Sys;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmOperationLogs : MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmOperationLogs";
		CommonDAO commonDAO = CommonDAO.GetInstance();

		public FrmOperationLogs()
		{
			InitializeComponent();
		}
		

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void FormInit()
		{
			BindData();
			
		}

		private void FrmCarMonitor_Load(object sender, EventArgs e)
		{
			FormInit();
		}

		private void BindData()
		{
			string sqlWhere = "";
			if (!string.IsNullOrEmpty(txtSearch.Text))
				sqlWhere += " where OperationUser like '%" + txtSearch.Text + "%'";
			superGridControl1.PrimaryGrid.DataSource = CommonDAO.GetInstance().SelfDber.Entities<CmcsOperationLog>(sqlWhere + " order by OperationTime desc");
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
			
		}
	}
}
