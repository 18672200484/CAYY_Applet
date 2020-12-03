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

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmDoor : MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmDoor";

		public FrmDoor()
		{
			InitializeComponent();
		}


		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void FormInit()
		{
			Application.Idle += Application_Idle;
			appBox.AppFilename = @"D:\Program Files (x86)\tencent\WeChat\WeChat.exe";// CommonDAO.GetInstance().GetCommonAppletConfigString("门禁程序路径");
			appBox.Start();
			if (appBox.IsStarted)
			{
				//txtAppFilename.Text = appBox.AppFilename;

			}
		}

		void Application_Idle(object sender, EventArgs e)
		{
			//if (appBox.IsStarted)
			//	lblInfo.Text = string.Format("{0}", appBox.AppProcess.MainWindowHandle);
		}

		private void FrmCarMonitor_Load(object sender, EventArgs e)
		{
			FormInit();
		}

	}
}
