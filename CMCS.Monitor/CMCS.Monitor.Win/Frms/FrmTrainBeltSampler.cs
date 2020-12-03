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
using CMCS.Monitor.Win.Frms.Sys;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmTrainBeltSampler : MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmTrainBeltSampler";

		public FrmTrainBeltSampler()
		{
			InitializeComponent();
		}


		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void FormInit()
		{
			
		}

		private void FrmCarMonitor_Load(object sender, EventArgs e)
		{
			FormInit();
		}

	}
}
