using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DataTester.DAO;
using OPCAutomation;

namespace CMCS.DataTester.Frms
{
	public partial class FrmBuildTrainCarriagePass : Form
	{
		DataTesterDAO dataTesterDAO = DataTesterDAO.GetInstance();
		OPCGroups groups;
		OPCGroup group;
		OPCItems items;
		OPCItem item;
		public FrmBuildTrainCarriagePass()
		{
			InitializeComponent();
		}

		private void FrmBuildTrainCarriagePass_Load(object sender, EventArgs e)
		{
			cmbDirection.SelectedIndex = 0;
		}

		private void btnBuild_Click(object sender, EventArgs e)
		{
			OPCServer server = new OPCServer();
			try
			{
				server.Connect("Kepware.KEPServerEX.V6", "192.168.70.88");
				MessageBox.Show("连接成功");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			groups = server.OPCGroups;  //拿到组jih
			groups.DefaultGroupIsActive = true; //设置组集合默认为激活状态
			groups.DefaultGroupDeadband = 0;    //设置死区
			groups.DefaultGroupUpdateRate = 200;//设置更新频率

			group = server.OPCGroups.Add("汽车机械采样机.#1采样机");
			group.IsSubscribed = true; //是否为订阅
			group.UpdateRate = 200;    //刷新频率
			group.DataChange += MyOpcGroup_DataChange; ; //组内数据变化的回调函数
			group.AsyncReadComplete += MyOpcGroup_AsyncReadComplete; //异步读取完成回调
			group.AsyncWriteComplete += MyOpcGroup_AsyncWriteComplete; //异步写入完成回调
			group.AsyncCancelComplete += MyOpcGroup_AsyncCancelComplete;//异步取消读取、写入回调


			item = group.OPCItems.AddItem("汽车机械采样机.#1机械采样机.采样急停", 0);
			//item = group.OPCItems.AddItem("Channel1.Device1.tag2", 1);



			if (dataTesterDAO.CreateTrainCarriagePass(txtMachineCode.Text.Trim(), txtCarNumber.Text.Trim(), cmbDirection.Text))
			{
				MessageBox.Show("生成成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}


		void MyOpcGroup_AsyncCancelComplete(int CancelID)
		{
			throw new NotImplementedException();
		}

		void MyOpcGroup_AsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
		{
			throw new NotImplementedException();
		}

		void MyOpcGroup_AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
		{
			throw new NotImplementedException();
		}

		//datachange事件
		void MyOpcGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
		{
			try
			{

				string.Format("{0}", item.Value);
				string.Format("{0}", item.Quality);
				string.Format("{0}", item.TimeStamp);

			}
			catch (System.Exception error)
			{
				MessageBox.Show(error.Message, "Result--同步读", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
