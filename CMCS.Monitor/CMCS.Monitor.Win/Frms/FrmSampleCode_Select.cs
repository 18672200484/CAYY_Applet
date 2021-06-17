using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Common.Views;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmSampleCode_Select : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// ѡ�е�ʵ��
        /// </summary>
        public View_CarDumperInfo Output;

        CommonDAO commonDAO = CommonDAO.GetInstance();

        /// <summary>
        /// �������
        /// </summary>
        string sqlWhere;

        public FrmSampleCode_Select(string sqlWhere)
        {
            InitializeComponent();

            this.sqlWhere = sqlWhere;

            Search(string.Empty);
        }

        private void FrmSupplier_Select_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Output = null;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (superGridControl1.PrimaryGrid.Rows.Count > 0) superGridControl1.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                Return();
            }
            else
            {
                Search(txtInput.Text.Trim());
            }
        }

        void Search(string input)
        {
            List<View_CarDumperInfo> list = commonDAO.SelfDber.Entities<View_CarDumperInfo>(" where TrackNumber='"+ sqlWhere + "'");
            superGridControl1.PrimaryGrid.DataSource = list;
        }

        void Return()
        {
            GridRow gridRow = superGridControl1.PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;

            View_CarDumperInfo entity = gridRow.DataItem as View_CarDumperInfo;

            if (SendSamplingPlan(entity))
            {
                //this.Output = (entity);
                this.DialogResult = DialogResult.OK;
                commonDAO.SaveOperationLog("���Ͳ����ƻ�����Ʒ�룺" + entity.SampleCode, GlobalVars.LoginUser.Name);
                MessageBoxEx.Show("����ͳɹ����ȴ�ִ��");
            }
            else
            {
                //this.Output = (entity);
                this.DialogResult = DialogResult.No;
                MessageBoxEx.Show("�����ʧ�ܣ������·���");
            }

            
            this.Close();
        }

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // ȡ���༭ģʽ
            e.Cancel = true;
        }

        /// <summary>
        /// �����к�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        private void superGridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Return();
        }

        private void superGridControl1_CellDoubleClick(object sender, GridCellDoubleClickEventArgs e)
        {
            Return();
        }

        /// <summary>
		/// ���Ͳ����ƻ�
		/// </summary>
		/// <returns></returns>
		bool SendSamplingPlan(View_CarDumperInfo plan)
        {
            InfBeltSamplePlan_KY oldBeltSamplePlan = Dbers.GetInstance().SelfDber.Entity<InfBeltSamplePlan_KY>("where SampleCode=:SampleCode and MachineCode=:MachineCode", new { SampleCode = plan.SampleCode, MachineCode = this.sqlWhere == "#4" ? "#1" : "#2" }); ;
            if (oldBeltSamplePlan == null)
            {
                oldBeltSamplePlan = new InfBeltSamplePlan_KY();
                oldBeltSamplePlan.DataFlag = 0;
                oldBeltSamplePlan.SampleCode = plan.SampleCode;
                oldBeltSamplePlan.MachineCode = this.sqlWhere == "#4" ? "#1" : "#2";
                oldBeltSamplePlan.CarCount = plan.CarNum;

                if (Dbers.GetInstance().SelfDber.Insert<InfBeltSamplePlan_KY>(oldBeltSamplePlan) > 0)
                {
                    commonDAO.SetSignalDataValue(oldBeltSamplePlan.MachineCode == "#1" ? GlobalVars.MachineCode_TrunOver_1 : GlobalVars.MachineCode_TrunOver_2, eSignalDataName.��������.ToString(), oldBeltSamplePlan.SampleCode);
                    commonDAO.SetSignalDataValue(oldBeltSamplePlan.MachineCode == "#1" ? GlobalVars.MachineCode_TrunOver_1 : GlobalVars.MachineCode_TrunOver_2, eSignalDataName.����������.ToString(), oldBeltSamplePlan.CarCount.ToString());
                    return true;
                }
                return false;
            }
            else
            {
                oldBeltSamplePlan.DataFlag = 0;
                oldBeltSamplePlan.MachineCode = this.sqlWhere == "#4" ? "#1" : "#2";
                oldBeltSamplePlan.CarCount = plan.CarNum;

                oldBeltSamplePlan.SyncFlag = 0;

                if (Dbers.GetInstance().SelfDber.Update<InfBeltSamplePlan_KY>(oldBeltSamplePlan) > 0)
                {
                    commonDAO.SetSignalDataValue(oldBeltSamplePlan.MachineCode == "#1" ? GlobalVars.MachineCode_TrunOver_1 : GlobalVars.MachineCode_TrunOver_2, eSignalDataName.��������.ToString(), oldBeltSamplePlan.SampleCode);
                    commonDAO.SetSignalDataValue(oldBeltSamplePlan.MachineCode == "#1" ? GlobalVars.MachineCode_TrunOver_1 : GlobalVars.MachineCode_TrunOver_2, eSignalDataName.����������.ToString(), oldBeltSamplePlan.CarCount.ToString());
                    return true;
                }
                return false;
            }
        }
    }
}