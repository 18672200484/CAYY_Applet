using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using DevComponents.DotNetBar.Metro;
using HikVisionSDK.Core;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmHikVideo : MetroForm
    {
        IPCer ipCer = new IPCer();
        CmcsCamare currentCamera = null;
        public FrmHikVideo(string videoName)
        {
            InitializeComponent();
            this.Text = videoName;
            currentCamera = CommonDAO.GetInstance().SelfDber.Entity<CmcsCamare>("where Name=:Name", new { Name = videoName });
        }

        private void FrmHikVideo_Load(object sender, EventArgs e)
        {
            if (currentCamera == null)
            {
                panVideo1.Text = "摄像机参数未配置";
                return;
            }
            try
            {
                //初始化SDK
                IPCer.InitSDK();

                bool b = ipCer.Login(currentCamera.Ip, currentCamera.Port, currentCamera.UserName, currentCamera.Password);
                if (!b)
                {
                    panVideo1.Text = "登录失败：" + returnError(IPCer.GetLastErrorCode());
                    return;
                }
                b = ipCer.StartPreview(panVideo1.Handle, currentCamera.Channel);
                if (!b)
                {
                    panVideo1.Text = "预览失败：" + returnError(IPCer.GetLastErrorCode());
                    return;
                }
            }
            catch (Exception ex)
            {
                panVideo1.Text = "预览异常：请检查参数是否正确;" + ex.Message;
            }
        }

        private void FrmHikVideo_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ipCer.StopPreview();
                ipCer.LoginOut();
                IPCer.CleanupSDK();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 根据错误编码获取错误信息
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        private string returnError(uint errorCode)
        {
            string errorInfo = string.Empty;
            switch (errorCode)
            {
                case 1:
                    errorInfo = "用户名密码错误。注册时输入的用户名或者密码错误。";
                    break;
                case 2:
                    errorInfo = "权限不足。该注册用户没有权限执行当前对设备的操作，可以与远程用户参数配置做对比。";
                    break;
                case 3:
                    errorInfo = "SDK 未初始化。";
                    break;
                case 4:
                    errorInfo = "通道号错误。设备没有对应的通道号。";
                    break;
                case 5:
                    errorInfo = "连接到设备的用户个数超过最大。";
                    break;
                case 6:
                    errorInfo = "版本不匹配。SDK 和设备的版本不匹配。";
                    break;
                case 7:
                    errorInfo = "连接设备失败。设备不在线或网络原因引起的连接超时等。";
                    break;
                case 8:
                    errorInfo = "向设备发送失败。";
                    break;
                case 9:
                    errorInfo = "从设备接收数据失败。";
                    break;
                case 10:
                    errorInfo = "从设备接收数据超时。";
                    break;
                case 11:
                    errorInfo = "传送的数据有误。发送给设备或者从设备接收到的数据错误，如远程参数配置时输入设备不支持的值。";
                    break;
                default:
                    errorInfo = "其他错误";
                    break;
            }
            return errorInfo;
        }
    }
}
