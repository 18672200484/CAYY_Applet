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

		Array strItemIDs;
		Array lClientHandles;
		Array lserverhandles;
		Array lErrors;
		object RequestedDataTypes = null;
		object AccessPaths = null;
		Array lErrors_Wt;
		int lTransID_Wt = 2;
		int lCancelID_Wt;
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
				server.Connect("Kepware.KEPServerEX.V6", "localhost");
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

			group = server.OPCGroups.Add("OPCDOTNETGROUP");
			group.IsSubscribed = true; //是否为订阅
			group.UpdateRate = 200;    //刷新频率
			group.DataChange += MyOpcGroup_DataChange; ; //组内数据变化的回调函数
			group.AsyncReadComplete += MyOpcGroup_AsyncReadComplete; //异步读取完成回调
			group.AsyncWriteComplete += MyOpcGroup_AsyncWriteComplete; //异步写入完成回调
			group.AsyncCancelComplete += MyOpcGroup_AsyncCancelComplete;//异步取消读取、写入回调
			List<string> tags = new List<string>();
			tags.Add("汽车机械采样机.#1采样机.ss");
			tags.Add("汽车机械采样机.#1采样机.采样机急停");
			//tags.Add("汽车机械采样机.#1采样机.ss");
			//item = group.OPCItems.AddItem("汽车机械采样机.#1采样机._System._DeviceId", 1);

			//for (int i = 0; i < tags.Count; i++)
			//{
			//	item = group.OPCItems.AddItem(tags[i], i);
			//}
			int[] tmpCHandles = new int[3];
			for (int i = 0; i < tmpCHandles.Length; i++)
			{
				tmpCHandles[i] = i;
			}
			string[] tmpIDs = new string[3];
			tmpIDs[1] = "汽车机械采样机.#1采样机._System._DemandPoll";
			tmpIDs[2] = "汽车机械采样机.#1采样机._System._AutoDemoted";
			strItemIDs = (Array)tmpIDs;//必须转成Array型，否则不能调用AddItems方法
			lClientHandles = (Array)tmpCHandles;
			// 添加opc标签
			group.OPCItems.AddItems(2, ref strItemIDs, ref lClientHandles, out lserverhandles, out lErrors, RequestedDataTypes, AccessPaths);

			//items = group.OPCItems;
			//item = group.OPCItems.AddItem("Channel1.Device1.tag2", 1);

			//if (dataTesterDAO.CreateTrainCarriagePass(txtMachineCode.Text.Trim(), txtCarNumber.Text.Trim(), cmbDirection.Text))
			//{
			//	MessageBox.Show("生成成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//}
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
			//throw new NotImplementedException();
		}

		//datachange事件
		void MyOpcGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
		{
			try
			{
				for (int i = 1; i < NumItems + 1; i++)
				{
					string ss = ClientHandles.GetValue(i).ToString() + "-" + ItemValues.GetValue(i).ToString();
				}
			}
			catch (System.Exception error)
			{
				MessageBox.Show(error.Message, "Result--同步读", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Array AsyncValue_Wt;
			Array SerHandles;
			object[] tmpWtData = new object[3];//写入的数据必须是object型的，否则会报错
			int[] tmpSerHdles = new int[3];
			//将输入数据赋给数组，然后再转成Array型送给AsyncValue_Wt
			tmpWtData[1] = (object)"192.168.70.251";
			tmpWtData[2] = (object)1;
			AsyncValue_Wt = (Array)tmpWtData;
			//将输入数据送给的Item对应服务器句柄赋给数组，然后再转成Array型送给SerHandles
			tmpSerHdles[1] = Convert.ToInt32(lserverhandles.GetValue(1));
			tmpSerHdles[2] = Convert.ToInt32(lserverhandles.GetValue(2));
			SerHandles = (Array)tmpSerHdles;
			group.AsyncWrite(2, ref SerHandles, ref AsyncValue_Wt, out lErrors_Wt, lTransID_Wt, out lCancelID_Wt);
			//item = group.OPCItems.AddItem("汽车机械采样机.#1采样机._System._Simulated", 3);
			//item.Write((object)true);
		}
	}
}
