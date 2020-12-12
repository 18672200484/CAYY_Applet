using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opc;
using Opc.Da;
using OpcCom;

namespace OPCClientCore
{
    public class OPCClient
    {
        /// <summary>
        /// 定义数据存取服务器
        /// </summary>
        private Opc.Da.Server Server;

        /// <summary>
        /// 定义组对象
        /// </summary>
        private Subscription Subscription;

        public delegate void DataReveiced(List<ItemResults> list);
        public event DataReveiced OnDataReveiced;

        public delegate void ScanErrorEventHandler(Exception ex);
        public event ScanErrorEventHandler OnScanError;

        /// <summary>
        /// 连接OPC
        /// </summary>
        /// <returns></returns>
        public bool Connect(string opcServerName, string opcServerIp)
        {
            try
            {
                Opc.Server[] serverArray = new ServerEnumerator().GetAvailableServers(Specification.COM_DA_20, opcServerIp, null);
                if (serverArray != null)
                {
                    Opc.Server server = serverArray.Where(a => a.Name == opcServerName).FirstOrDefault();
                    if (server != null)
                    {
                        server.Connect();
                        this.Server = (Opc.Da.Server)server;
                        return true;
                    }
                }
            }
            catch (Exception ex) { if (OnScanError != null) OnScanError(ex); }

            this.Close();
            return false;
        }

        /// <summary>
        /// 关闭OPC
        /// </summary>
        public void Close()
        {
            try { if (this.Server != null && this.Server.IsConnected) { this.Server.Disconnect(); } }
            catch { }
        }

        /// <summary>
        /// 创建组
        /// </summary>
        /// <param name="itemNames"></param>
        public void CreateGroup(List<string> itemNames)
        {
            if (this.Subscription != null && this.Subscription.GetState() != null) return;

            SubscriptionState state = new SubscriptionState
            {
                Name = "tGroup1",   //组名
                ServerHandle = null,    //服务器给该组分配的句柄。
                ClientHandle = Guid.NewGuid().ToString(),   //客户端给该组分配的句柄。
                Active = true,  //激活该组。
                UpdateRate = 100,   //刷新频率为1秒。
                Deadband = 0,   // 死区值，设为0时，服务器端该组内任何数据变化都通知组。
                Locale = null   //不设置地区值。
            };
            this.Subscription = (Subscription)this.Server.CreateSubscription(state);

            Item[] items = new Item[itemNames.Count];
            for (int i = 0; i < itemNames.Count; i++)
            {
                items[i] = new Item() { ClientHandle = Guid.NewGuid().ToString(), ItemName = itemNames[i] };
            }
            this.Subscription.AddItems(items);

        }

        /// <summary>
        /// 读数
        /// </summary>
        /// <param name="itemNames"></param>
        public void Read()
        {
            this.Subscription.DataChanged += new DataChangedEventHandler(Subscription_DataChanged);

            if (this.OnDataReveiced != null)
            {
                //初始化读取所有点
                ItemValueResult[] values = this.Subscription.Read(this.Subscription.Items);
                List<ItemResults> list = new List<ItemResults>();
                foreach (ItemValueResult item in values)
                {
                    list.Add(new ItemResults() { ItemName = item.ItemName, Value = item.Value });
                }
                OnDataReveiced(list);
            }
        }

        /// <summary>
        /// 写数
        /// </summary>
        /// <param name="itemValues"></param>
        public bool Write(Dictionary<string, object> dicItemValues)
        {
            if (this.Subscription == null) return false;

            ItemValue[] itemValuesWrite = new ItemValue[dicItemValues.Count];

            int i = 0;
            foreach (var item in dicItemValues)
            {
                ItemIdentifier itemIdentifier = this.Subscription.Items.Where(a => a.ItemName == item.Key).FirstOrDefault();

                if (itemIdentifier != null)
                {
                    itemValuesWrite[i] = new ItemValue(itemIdentifier);
                    itemValuesWrite[i].Value = item.Value;

                    i++;
                }
            }

            if (itemValuesWrite.Where(a => a != null).Count() > 0)
            {
                itemValuesWrite = itemValuesWrite.Where(a => a != null).ToArray();
                this.Subscription.Write(itemValuesWrite);
                return true;
            }

            return false;
        }

        //DataChange回调
        public void Subscription_DataChanged(object subscriptionHandle, object requestHandle, ItemValueResult[] values)
        {
            if (this.OnDataReveiced != null)
            {
                List<ItemResults> list = new List<ItemResults>();
                foreach (ItemValueResult item in values)
                {
                    list.Add(new ItemResults() { ItemName = item.ItemName, Value = item.Value });
                }
                OnDataReveiced(list);
            }
        }
    }

    public class ItemResults
    {
        public string ItemName { get; set; }
        public object Value { get; set; }
    }
}
