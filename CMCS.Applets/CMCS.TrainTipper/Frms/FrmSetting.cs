using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace CMCS.TrainTipper.Frms
{
    public partial class FrmSetting : DevComponents.DotNetBar.Metro.MetroForm
    {
        CommonDAO commonDAO = CommonDAO.GetInstance();

        string Old_Param = string.Empty;
        public FrmSetting()
        {
            InitializeComponent();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            try
            {
                labelX1.ForeColor = Color.Red;
                labelX10.ForeColor = Color.Red;
                labelX4.ForeColor = Color.Red;

                txtCommonAppConfig.Text = CommonAppConfig.GetInstance().AppIdentifier;

                //翻车机编码
                txtTipperMachineCodes.Text = commonDAO.GetAppletConfigString("翻车机编码");

                //翻车机车号识别编码
                txtCarriageRecognitionerMachineCodes.Text = commonDAO.GetAppletConfigString("翻车机车号识别编码");

                //翻车机对应皮带采样机
                txtCarriageRecognitionerTrainBeltSamplerCodes.Text = commonDAO.GetAppletConfigString("翻车机对应皮带采样机编码");
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("参数初始化失败" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// 选中ComboItem
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cmb"></param>
        private void SelectedComboItem(string text, ComboBoxEx cmb)
        {
            foreach (ComboItem item in cmb.Items)
            {
                if (item.Text == text)
                {
                    cmb.SelectedItem = item;
                    break;
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //翻车机编码
            commonDAO.SetAppletConfig("翻车机编码", txtTipperMachineCodes.Text);

            //翻车机车号识别编码
            commonDAO.SetAppletConfig("翻车机车号识别编码", txtCarriageRecognitionerMachineCodes.Text);

            //翻车机对应皮带采样机
            commonDAO.SetAppletConfig("翻车机对应皮带采样机编码", txtCarriageRecognitionerTrainBeltSamplerCodes.Text);

            string[] tipperMachineCodes = txtTipperMachineCodes.Text.Split('|');
            string[] carriageRecognitionerTrainBeltSamplerCodes = txtCarriageRecognitionerTrainBeltSamplerCodes.Text.Split('|');
            if (tipperMachineCodes.Length > 0 && tipperMachineCodes.Length == carriageRecognitionerTrainBeltSamplerCodes.Length)
            {
                for (int i = 0; i < tipperMachineCodes.Length; i++)
                {
                    commonDAO.SetCommonAppletConfig(tipperMachineCodes[i] + "对应皮带采样机", carriageRecognitionerTrainBeltSamplerCodes[i]);
                }
            }

            if (MessageBoxEx.Show("更改的配置需要重启程序才能生效，是否立刻重启？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Restart();
            else
                this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}