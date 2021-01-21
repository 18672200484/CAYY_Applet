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
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Forms.UserControls;
using CMCS.WeighCheck.DAO;
using CMCS.WeighCheck.MakeWeight.Enums;
using CMCS.WeighCheck.MakeWeight.Frms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using RW.HFReader;

namespace CMCS.WeighCheck.MakeWeight.Frms.SampleWeigth
{
	public partial class FrmMakeWeight : MetroForm
	{
		public FrmMakeWeight()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmMakeWeight";

		#region Vars

		CommonDAO commonDAO = CommonDAO.GetInstance();
		CZYHandlerDAO czyHandlerDAO = CZYHandlerDAO.GetInstance();
		CodePrinter _CodePrinter = null;

		eFlowFlag currentFlowFlag = eFlowFlag.等待扫码;
		/// <summary>
		/// 当前流程标识
		/// </summary>
		public eFlowFlag CurrentFlowFlag
		{
			get { return currentFlowFlag; }
			set
			{
				currentFlowFlag = value;
				lblCurrentFlowFlag.Text = value.ToString();
			}
		}

		string resMessage = string.Empty;

		#endregion

		/// <summary>
		/// 初始化
		/// </summary>
		public void InitFrom()
		{
			this.IsUseWeight = Convert.ToBoolean(commonDAO.GetAppletConfigInt32("启用称重"));
			this._CodePrinter = new CodePrinter(printDocument1);

			// 生成编码按钮
			GridButtonXEditControl btnNewCode = superGridControl1.PrimaryGrid.Columns["gclmNewCode"].EditControl as GridButtonXEditControl;
			btnNewCode.ColorTable = eButtonColor.BlueWithBackground;
			btnNewCode.Click += new EventHandler(btnNewCode_Click);
			// 写入编码按钮
			GridButtonXEditControl btnPrintCode = superGridControl1.PrimaryGrid.Columns["gclmWriteCode"].EditControl as GridButtonXEditControl;
			btnNewCode.ColorTable = eButtonColor.BlueWithBackground;
			btnPrintCode.Click += new EventHandler(btnWriteCode_Click);
		}

		private void FrmMakeWeight_Load(object sender, EventArgs e)
		{
			// 初始化
			InitFrom();
			// 初始化设备
			InitHardware();
		}

		private void FrmMakeWeight_FormClosing(object sender, FormClosingEventArgs e)
		{
			UnloadHardware();
		}

		#region 电子秤

		/// <summary>
		/// 电子秤
		/// </summary>
		WB.TOLEDO.IND231.TOLEDO_IND231Wber wber = new WB.TOLEDO.IND231.TOLEDO_IND231Wber(3);

		bool isUseWeight = true;
		/// <summary>
		/// 启用电子秤
		/// </summary>
		public bool IsUseWeight
		{
			get { return isUseWeight; }
			set
			{
				isUseWeight = value;

				lblWber.Visible = value;
				slightWber.Visible = value;
			}
		}

		bool wbSteady = false;
		/// <summary>
		/// 电子秤仪表稳定状态
		/// </summary>
		public bool WbSteady
		{
			get { return wbSteady; }
			set
			{
				wbSteady = value;
			}
		}

		double wbMinWeight = 0;
		/// <summary>
		/// 电子秤仪表最小称重 单位：吨
		/// </summary>
		public double WbMinWeight
		{
			get { return wbMinWeight; }
			set
			{
				wbMinWeight = value;
			}
		}

		/// <summary>
		/// 重量稳定事件
		/// </summary>
		/// <param name="steady"></param>
		void Wber_OnSteadyChange(bool steady)
		{
			// 仪表稳定状态 
			InvokeEx(() =>
			{
				this.WbSteady = steady;
			});
		}

		/// <summary>
		/// 电子秤状态变化
		/// </summary>
		/// <param name="status"></param>
		void Wber_OnStatusChange(bool status)
		{
			// 接收设备状态 
			InvokeEx(() =>
			{
				slightWber.LightColor = (status ? Color.Green : Color.Red);
			});
		}

		#endregion

		#region 读卡器
		HFReaderRwer ReadRwer = new HFReaderRwer();
		/// <summary>
		/// 读卡成功事件
		/// </summary>
		/// <param name="steady"></param>
		void Rwer_OnReadSuccess(string rfid)
		{
			InvokeEx(() =>
			{
				txtInputMakeCode.Text = rfid.ToUpper();
			});
		}

		/// <summary>
		///  读卡器状态变化
		/// </summary>
		/// <param name="status"></param>
		void Rwer_OnStatusChange(bool status)
		{
			// 接收设备状态 
			InvokeEx(() =>
			{
				if (status) ShowMessage("读卡器连接成功", eOutputType.Normal);
				else ShowMessage("读卡器连接失败", eOutputType.Error);
				slightRwer.LightColor = (status ? Color.Green : Color.Red);
			});
		}

		/// <summary>
		/// 读卡
		/// </summary>
		/// <returns></returns>
		private string ReadRf()
		{
			byte SecNumber = 2;// Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器扇区"));
			byte BlockNumber = 0;// Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器块区"));

			if (ReadRwer.OpenRF())
			{
				ShowMessage("射频打开成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("射频打开失败", eOutputType.Error);
				return string.Empty;
			}

			if (ReadRwer.ChangeToISO14443A())
			{
				ShowMessage("切换到1443模式成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("切换到1443模式失败", eOutputType.Error);
				return string.Empty;
			}

			if (ReadRwer.Request14443A())
			{
				ShowMessage("获取卡类型成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡类型失败", eOutputType.Error);
				return string.Empty;
			}

			if (ReadRwer.Anticoll14443A())
			{
				ShowMessage("获取卡号成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡号失败", eOutputType.Error);
				return string.Empty;
			}

			if (ReadRwer.Select14443A())
			{
				ShowMessage("获取卡容量成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡容量失败", eOutputType.Error);
				return string.Empty;
			}

			if (ReadRwer.AuthKey14443A(SecNumber, BlockNumber))
			{
				ShowMessage("标签密钥验证成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("标签密钥验证失败", eOutputType.Error);
				return string.Empty;
			}
			if (ReadRwer.RWRead14443A(SecNumber, BlockNumber) != string.Empty)
			{
				ShowMessage("读卡成功", eOutputType.Normal);
				return ReadRwer.Byte16ToString(ReadRwer.ReadData);
			}
			return string.Empty;
		}

		/// <summary>
		/// 写卡
		/// </summary>
		/// <returns></returns>
		private string WriteRf(string rf)
		{
			byte SecNumber = 2;// Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器扇区"));
			byte BlockNumber = 0;// Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器块区"));

			if (ReadRwer.OpenRF())
			{
				ShowMessage("射频打开成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("射频打开失败", eOutputType.Error);
				return string.Empty;
			}

			if (ReadRwer.ChangeToISO14443A())
			{
				ShowMessage("切换到1443模式成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("切换到1443模式失败", eOutputType.Error);
				return string.Empty;
			}

			if (ReadRwer.Request14443A())
			{
				ShowMessage("获取卡类型成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡类型失败", eOutputType.Error);
				return string.Empty;
			}

			if (ReadRwer.Anticoll14443A())
			{
				ShowMessage("获取卡号成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡号失败", eOutputType.Error);
				return string.Empty;
			}

			if (ReadRwer.Select14443A())
			{
				ShowMessage("获取卡容量成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡容量失败", eOutputType.Error);
				return string.Empty;
			}

			if (ReadRwer.AuthKey14443A(SecNumber, BlockNumber))
			{
				ShowMessage("标签密钥验证成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("标签密钥验证失败", eOutputType.Error);
				return string.Empty;
			}
			if (ReadRwer.Write14443(rf, Convert.ToInt32(SecNumber), Convert.ToInt32(BlockNumber)))
			{
				ShowMessage("编码：" + rf + "写卡成功", eOutputType.Normal);
				string rf_new = ReadRwer.Byte16ToString(ReadRwer.RWRead14443A(SecNumber, BlockNumber));
				if (rf == rf_new)
				{
					ShowMessage("编码：" + rf + "读卡验证成功", eOutputType.Normal);
					return rf_new;
				}
			}

			return string.Empty;
		}

		#endregion

		#region 设备初始化与卸载

		/// <summary>
		/// 初始化外接设备
		/// </summary>
		private void InitHardware()
		{
			try
			{
				bool success = false;
				ReadRwer.OnStatusChange += new HFReaderRwer.StatusChangeHandler(Rwer_OnStatusChange);
				Rwer_OnStatusChange(ReadRwer.Status);

				// 初始化-读卡器
				success = ReadRwer.OpenComPort(commonDAO.GetAppletConfigInt32("读卡器串口"));

				// 初始化-电子秤
				if (IsUseWeight)
				{
					this.WbMinWeight = commonDAO.GetAppletConfigDouble("电子秤最小重量");

					wber.OnStatusChange += new WB.TOLEDO.IND231.TOLEDO_IND231Wber.StatusChangeHandler(Wber_OnStatusChange);
					wber.OnSteadyChange += new WB.TOLEDO.IND231.TOLEDO_IND231Wber.SteadyChangeEventHandler(Wber_OnSteadyChange);
					success = wber.OpenCom(commonDAO.GetAppletConfigInt32("电子秤串口"), commonDAO.GetAppletConfigInt32("电子秤波特率"), commonDAO.GetAppletConfigInt32("电子秤数据位"), commonDAO.GetAppletConfigInt32("电子秤停止位"));
				}
			}
			catch (Exception ex)
			{
				Log4Neter.Error("设备初始化", ex);
			}
		}

		/// <summary>
		/// 卸载设备
		/// </summary>
		private void UnloadHardware()
		{
			// 注意此段代码
			Application.DoEvents();

			try
			{
				wber.CloseCom();
			}
			catch { }
			try
			{
				ReadRwer.CloseRF();
				ReadRwer.CloseComPort();
			}
			catch { }
		}
		#endregion

		#region 业务

		/// <summary>
		///  重置流程信息
		/// </summary>
		private void Restet()
		{
			this.CurrentFlowFlag = eFlowFlag.等待扫码;

			txtInputMakeCode.ResetText();
			rtxtMakeWeightInfo.ResetText();

			// 方便客户快速使用，获取焦点
			txtInputMakeCode.Focus();
		}

		#endregion

		#region 操作

		/// <summary>
		/// 键入Enter检测有效性
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtInputMakeCode_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (!String.IsNullOrEmpty(txtInputMakeCode.Text.Trim()))
				{
					LoadRCMakeDetail();
				}
			}
		}

		/// <summary>
		/// 清空
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtMakeWeightCode_ButtonCustomClick(object sender, EventArgs e)
		{
			Restet();
		}

		/// <summary>
		/// 加载制样明细记录
		/// </summary>
		private void LoadRCMakeDetail()
		{
			string makeId = string.Empty;
			bool res = false;

			List<CmcsRCMakeDetail> rCMakeDetails = czyHandlerDAO.GetRCMakeDetails(txtInputMakeCode.Text.Trim(), ref makeId, ref res, out resMessage);
			if (res)
			{
				if (rCMakeDetails.Count == 0)
				{
					//没有制样明细记录就自动生成出样记录
					foreach (CodeContent codeContent in CommonDAO.GetInstance().GetCodeContentByKind("样品类型"))
					{
						CmcsRCMakeDetail cmcsrcmakedetail = new CmcsRCMakeDetail()
						{
							MakeId = makeId,
							SampleCode = string.Empty,
							BackupCode = string.Empty,
							CheckWeight = 0,
							SampleType = codeContent.Code,
							SampleWeight = 0,
						};
						if (Dbers.GetInstance().SelfDber.Insert<CmcsRCMakeDetail>(cmcsrcmakedetail) > 0)
							rCMakeDetails.Add(cmcsrcmakedetail);
					}
				}

				ShowMessage(resMessage, eOutputType.Normal);
			}
			else
				ShowMessage(resMessage, eOutputType.Error);

			superGridControl1.PrimaryGrid.DataSource = rCMakeDetails;
		}

		/// <summary>
		/// 生成编码
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnNewCode_Click(object sender, EventArgs e)
		{
			GridButtonXEditControl btn = sender as GridButtonXEditControl;
			if (btn == null) return;

			CmcsRCMakeDetail rCMakeDetail = btn.EditorCell.GridRow.DataItem as CmcsRCMakeDetail;
			if (rCMakeDetail == null) return;

			if (!string.IsNullOrEmpty(rCMakeDetail.SampleCode) && MessageBoxEx.Show("样罐编码已存在，确定要重新生成？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				return;

			// 生成随机样罐编码
			string newBarrelCode = commonDAO.CreateNewMakeBarrelCodeByMakeCode(rCMakeDetail.TheRCMake.MakeCode, rCMakeDetail.SampleType);
			// 称重校验
			if (IsUseWeight)
			{
				if (wber.Status && wber.Weight > 0 && wber.Weight > WbMinWeight)
				{
					rCMakeDetail.SampleCode = newBarrelCode;
					rCMakeDetail.SampleWeight = wber.Weight;

					czyHandlerDAO.UpdateMakeDetailWeightAndBarrelCode(rCMakeDetail.Id, wber.Weight, newBarrelCode);
				}
				else
					MessageBoxEx.Show("未检测到重量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			// 不称重校验
			else
			{
				rCMakeDetail.SampleCode = newBarrelCode;

				czyHandlerDAO.UpdateMakeDetailWeightAndBarrelCode(rCMakeDetail.Id, 0, newBarrelCode);
			}
		}

		/// <summary>
		/// 写入编码
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnWriteCode_Click(object sender, EventArgs e)
		{
			GridButtonXEditControl btn = sender as GridButtonXEditControl;
			if (btn == null) return;

			CmcsRCMakeDetail rCMakeDetail = btn.EditorCell.GridRow.DataItem as CmcsRCMakeDetail;
			if (rCMakeDetail == null) return;

			if (!string.IsNullOrEmpty(rCMakeDetail.SampleCode))
				WriteRf(rCMakeDetail.SampleCode);
			else
				MessageBoxEx.Show("请先点击[生成]按钮生成样罐编码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
		{
			// 取消编辑
			e.Cancel = true;
		}

		#endregion

		#region 其他

		private void ShowMessage(string info, eOutputType outputType)
		{
			OutputRunInfo(rtxtMakeWeightInfo, info, outputType);
		}

		/// <summary>
		/// 输出运行信息
		/// </summary>
		/// <param name="richTextBox"></param>
		/// <param name="text"></param>
		/// <param name="outputType"></param>
		private void OutputRunInfo(RichTextBoxEx richTextBox, string text, eOutputType outputType = eOutputType.Normal)
		{
			this.Invoke((EventHandler)(delegate
			{
				if (richTextBox.TextLength > 100000) richTextBox.Clear();

				text = string.Format("{0}  {1}", DateTime.Now.ToString("HH:mm:ss"), text);

				richTextBox.SelectionStart = richTextBox.TextLength;

				switch (outputType)
				{
					case eOutputType.Normal:
						richTextBox.SelectionColor = ColorTranslator.FromHtml("#BD86FA");
						break;
					case eOutputType.Important:
						richTextBox.SelectionColor = ColorTranslator.FromHtml("#A50081");
						break;
					case eOutputType.Warn:
						richTextBox.SelectionColor = ColorTranslator.FromHtml("#F9C916");
						break;
					case eOutputType.Error:
						richTextBox.SelectionColor = ColorTranslator.FromHtml("#DB2606");
						break;
					default:
						richTextBox.SelectionColor = Color.White;
						break;
				}

				richTextBox.AppendText(string.Format("{0}\r", text));

				richTextBox.ScrollToCaret();

			}));
		}

		/// <summary>
		/// 输出信息类型
		/// </summary>
		public enum eOutputType
		{
			/// <summary>
			/// 普通
			/// </summary>
			[Description("#BD86FA")]
			Normal,
			/// <summary>
			/// 重要
			/// </summary>
			[Description("#A50081")]
			Important,
			/// <summary>
			/// 警告
			/// </summary>
			[Description("#F9C916")]
			Warn,
			/// <summary>
			/// 错误
			/// </summary>
			[Description("#DB2606")]
			Error
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
	}
}
