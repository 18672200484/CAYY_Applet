using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.CarTransport.DAO;
using CMCS.CarTransport.Queue.Enums;
using CMCS.CarTransport.Queue.Frms.BaseInfo.Supplier;
using CMCS.CarTransport.Queue.Frms.Print;
using CMCS.CarTransport.Queue.Frms.Transport.TransportPicture;
using CMCS.CarTransport.Queue.Print;
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Enums;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using NPOI.HSSF.UserModel;

namespace CMCS.CarTransport.Queue.Frms.Transport.BuyFuelTransport
{
    public partial class FrmBuyFuelTransport_List : MetroAppForm
    {
        /// <summary>
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmBuyFuelTransport_List";

        /// <summary>
        /// ÿҳ��ʾ����
        /// </summary>
        int PageSize = 18;

        /// <summary>
        /// ��ҳ��
        /// </summary>
        int PageCount = 0;

        /// <summary>
        /// �ܼ�¼��
        /// </summary>
        int TotalCount = 0;

        /// <summary>
        /// ��ǰҳ����
        /// </summary>
        int CurrentIndex = 0;

        string SqlWhere = string.Empty;

        List<CmcsBuyFuelTransport> CurrExportData = new List<CmcsBuyFuelTransport>();

        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();

        WagonPrinterCollect wagonPrinter = null;

        private CmcsSupplier selectedSupplier_BuyFuel;
        /// <summary>
        /// ѡ��Ĺ�ú��λ
        /// </summary>
        public CmcsSupplier SelectedSupplier_BuyFuel
        {
            get { return selectedSupplier_BuyFuel; }
            set
            {
                selectedSupplier_BuyFuel = value;

                if (value != null)
                    txtSupplierName_BuyFuel.Text = value.Name;
                else
                    txtSupplierName_BuyFuel.ResetText();
            }
        }

        public FrmBuyFuelTransport_List()
        {
            InitializeComponent();
        }

        private void FrmBuyFuelTransport_List_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;

            //01�鿴 02���� 03�޸� 04ɾ��
            GridColumn clmEdit = superGridControl1.PrimaryGrid.Columns["clmEdit"];
            clmEdit.Visible = QueuerDAO.GetInstance().CheckPower(this.GetType().ToString(), "03", GlobalVars.LoginUser);
            GridColumn clmDelete = superGridControl1.PrimaryGrid.Columns["clmDelete"];
            clmDelete.Visible = QueuerDAO.GetInstance().CheckPower(this.GetType().ToString(), "04", GlobalVars.LoginUser);

            //��ʼ����ѯ����
            dtInputStart.Value = DateTime.Now.Date;
            dtInputEnd.Value = dtInputStart.Value.AddDays(1);
            cmbTimeType.SelectedIndex = 0;
            BindStepName();

            this.wagonPrinter = new WagonPrinterCollect(printDocument1);

            btnSearch_Click(null, null);
        }

        public void BindData()
        {
            object param = new { InFactoryStartTime = this.dtInputStart.Value, InFactoryEndTime = this.dtInputEnd.Value, TareStartTime = this.dtInputStart.Value, TareEndTime = this.dtInputEnd.Value };

            string tempSqlWhere = this.SqlWhere;
            //List<CmcsBuyFuelTransport> list = Dbers.GetInstance().SelfDber.ExecutePager<CmcsBuyFuelTransport>(PageSize, CurrentIndex, tempSqlWhere + " order by SerialNumber desc", param);

            List<CmcsBuyFuelTransport> list = Dbers.GetInstance().SelfDber.Entities<CmcsBuyFuelTransport>(tempSqlWhere + " order by SerialNumber desc", param);
            //CurrExportData = list;
            if (list.Count > 0)
            {
                CmcsBuyFuelTransport total = new CmcsBuyFuelTransport();
                total.SerialNumber = "�ϼ�";
                total.CarNumber = list.Count.ToString();
                total.GrossWeight = list.Sum(a => a.GrossWeight);
                total.TareWeight = list.Sum(a => a.TareWeight);
                total.SuttleWeight = list.Sum(a => a.SuttleWeight);
                total.DeductWeight = list.Sum(a => a.DeductWeight);
                total.TicketWeight = list.Sum(a => a.TicketWeight);
                list.Add(total);
            }
            superGridControl1.PrimaryGrid.DataSource = list;

            CurrExportData = Dbers.GetInstance().SelfDber.Entities<CmcsBuyFuelTransport>(tempSqlWhere + " order by CreationTime desc", param);

            //GetTotalCount(tempSqlWhere, param);
            //PagerControlStatue();

            //lblPagerInfo.Text = string.Format("�� {0} ����¼��ÿҳ {1} ������ {2} ҳ����ǰ�� {3} ҳ", TotalCount, PageSize, PageCount, CurrentIndex + 1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = " where 1=1";

            if (!string.IsNullOrEmpty(txtCarNumber_Ser.Text)) this.SqlWhere += " and CarNumber like '%" + txtCarNumber_Ser.Text + "%'";

            if (cmbTimeType.SelectedItem.ToString() == "�볧ʱ��")
            {
                if (!string.IsNullOrEmpty(dtInputStart.Text)) this.SqlWhere += " and InFactoryTime >=:InFactoryStartTime";

                if (!string.IsNullOrEmpty(dtInputEnd.Text)) this.SqlWhere += " and InFactoryTime <:InFactoryEndTime";
            }
            else if (cmbTimeType.SelectedItem.ToString() == "��Ƥʱ��")
            {
                if (!string.IsNullOrEmpty(dtInputStart.Text)) this.SqlWhere += " and TareTime >=:TareStartTime";

                if (!string.IsNullOrEmpty(dtInputEnd.Text)) this.SqlWhere += " and TareTime <:TareEndTime";
            }

            if (!string.IsNullOrEmpty(txtShipName.Text)) this.SqlWhere += " and ShipName like '%" + txtShipName.Text + "%'";

            if (this.SelectedSupplier_BuyFuel != null) this.SqlWhere += " and SupplierId = '" + this.SelectedSupplier_BuyFuel.Id + "'";

            if (this.cmbStepName.Text != "ȫ��") this.SqlWhere += " and StepName = '" + this.cmbStepName.Text + "'";

            CurrentIndex = 0;
            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;
            txtCarNumber_Ser.Text = string.Empty;

            CurrentIndex = 0;
            BindData();
        }

        private void btnReportExport_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream file = new FileStream("������ϸģ��.xls", FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                HSSFSheet sheetl = (HSSFSheet)hssfworkbook.GetSheet("������ϸ");

                if (CurrExportData.Count == 0)
                {
                    MessageBox.Show("���Ȳ�ѯ����");
                    return;
                }

                if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                    return;

                for (int i = 0; i < CurrExportData.Count; i++)
                {
                    CmcsBuyFuelTransport entity = CurrExportData[i];
                    Mysheet1(sheetl, i + 1, 0, entity.SerialNumber.ToString());
                    Mysheet1(sheetl, i + 1, 1, entity.CarNumber);
                    Mysheet1(sheetl, i + 1, 2, entity.SupplierName);
                    Mysheet1(sheetl, i + 1, 3, entity.FuelKindName);
                    Mysheet1(sheetl, i + 1, 4, entity.GrossWeight.ToString());
                    Mysheet1(sheetl, i + 1, 5, entity.TareWeight.ToString());
                    Mysheet1(sheetl, i + 1, 6, entity.DeductWeight.ToString());
                    Mysheet1(sheetl, i + 1, 7, entity.SuttleWeight.ToString());
                    Mysheet1(sheetl, i + 1, 8, entity.TareTime.Year < 2000 ? "" : entity.TareTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                #region �ϼ�
                Mysheet1(sheetl, CurrExportData.Count + 1, 0, "�ϼ�");
                Mysheet1(sheetl, CurrExportData.Count + 1, 1, CurrExportData.Count + "��");
                Mysheet1(sheetl, CurrExportData.Count + 1, 4, Math.Round(CurrExportData.Sum(a => a.GrossWeight), 2).ToString());
                Mysheet1(sheetl, CurrExportData.Count + 1, 5, Math.Round(CurrExportData.Sum(a => a.TareWeight), 2).ToString());
                Mysheet1(sheetl, CurrExportData.Count + 1, 6, Math.Round(CurrExportData.Sum(a => a.DeductWeight), 2).ToString());
                Mysheet1(sheetl, CurrExportData.Count + 1, 7, Math.Round(CurrExportData.Sum(a => a.SuttleWeight), 2).ToString());
                #endregion

                sheetl.ForceFormulaRecalculation = true;
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "������ϸ.xls";
                GC.Collect();

                FileStream fs = File.OpenWrite(folderBrowserDialog.SelectedPath + "\\" + fileName);
                hssfworkbook.Write(fs);   //��򿪵����xls�ļ���д������档  
                fs.Close();
                MessageBox.Show("�����ɹ�");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Mysheet1(HSSFSheet sheet1, int x, int y, String Value)
        {
            if (sheet1.GetRow(x) == null)
            {
                sheet1.CreateRow(x);
            }
            if (sheet1.GetRow(x).GetCell(y) == null)
            {
                sheet1.GetRow(x).CreateCell(y);
            }
            sheet1.GetRow(x).GetCell(y).SetCellValue(Value);
        }

        /// <summary>
        /// �����̽ڵ�
        /// </summary>
        private void BindStepName()
        {
            cmbStepName.Items.Add("ȫ��");
            cmbStepName.Items.Add(eTruckInFactoryStep.�볧.ToString());
            cmbStepName.Items.Add(eTruckInFactoryStep.����.ToString());
            cmbStepName.Items.Add(eTruckInFactoryStep.�س�.ToString());
            cmbStepName.Items.Add(eTruckInFactoryStep.�ᳵ.ToString());
            cmbStepName.Items.Add(eTruckInFactoryStep.����.ToString());
            cmbStepName.SelectedIndex = 0;
        }

        #region ��Ӧ��
        private void btnSelectSupplier_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmSupplier_Select frm = new FrmSupplier_Select("where IsStop=0 order by Name asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedSupplier_BuyFuel = frm.Output;
            }
        }

        private void btnDelSupplier_BuyFuel_Click(object sender, EventArgs e)
        {
            this.SelectedSupplier_BuyFuel = null;
        }
        #endregion

        #region Pager

        private void btnPagerCommand_Click(object sender, EventArgs e)
        {
            ButtonX btn = sender as ButtonX;
            switch (btn.CommandParameter.ToString())
            {
                case "First":
                    CurrentIndex = 0;
                    break;
                case "Previous":
                    CurrentIndex = CurrentIndex - 1;
                    break;
                case "Next":
                    CurrentIndex = CurrentIndex + 1;
                    break;
                case "Last":
                    CurrentIndex = PageCount - 1;
                    break;
            }

            BindData();
        }

        public void PagerControlStatue()
        {
            if (PageCount <= 1)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnLast.Enabled = false;
                btnNext.Enabled = false;

                return;
            }

            if (CurrentIndex == 0)
            {
                // ��ҳ
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex > 0 && CurrentIndex < PageCount - 1)
            {
                // ��һҳ/��һҳ
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex == PageCount - 1)
            {
                // ĩҳ
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = false;
                btnNext.Enabled = false;
            }
        }

        private void GetTotalCount(string sqlWhere, object param)
        {
            TotalCount = Dbers.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>(sqlWhere, param);
            if (TotalCount % PageSize != 0)
                PageCount = TotalCount / PageSize + 1;
            else
                PageCount = TotalCount / PageSize;
        }

        #endregion

        #region DataGridView

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // ȡ���༭
            e.Cancel = true;
        }

        private void superGridControl1_CellMouseDown(object sender, DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs e)
        {
            CmcsBuyFuelTransport entity = Dbers.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(superGridControl1.PrimaryGrid.GetCell(e.GridCell.GridRow.Index, superGridControl1.PrimaryGrid.Columns["clmId"].ColumnIndex).Value.ToString());
            if (entity == null || entity.SerialNumber == "�ϼ�") return;
            switch (superGridControl1.PrimaryGrid.Columns[e.GridCell.ColumnIndex].Name)
            {

                case "clmShow":
                    FrmBuyFuelTransport_Oper frmShow = new FrmBuyFuelTransport_Oper(entity.Id, eEditMode.�鿴);
                    if (frmShow.ShowDialog() == DialogResult.OK)
                    {
                        BindData();
                    }
                    break;
                case "clmEdit":
                    FrmBuyFuelTransport_Oper frmEdit = new FrmBuyFuelTransport_Oper(entity.Id, eEditMode.�޸�);
                    if (frmEdit.ShowDialog() == DialogResult.OK)
                    {
                        BindData();
                    }
                    break;
                case "clmDelete":
                    // ��ѯ����ʹ�øü�¼�ĳ��� 
                    if (MessageBoxEx.Show("ȷ��Ҫɾ���ü�¼��", "������ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            Dbers.GetInstance().SelfDber.Delete<CmcsBuyFuelTransport>(entity.Id);

                            //ɾ����ʱ�����¼
                            Dbers.GetInstance().SelfDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = entity.Id });
                        }
                        catch (Exception)
                        {
                            MessageBoxEx.Show("�ü�¼����ʹ���У���ֹɾ����", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        BindData();
                    }
                    break;
                case "clmPic":

                    if (Dbers.GetInstance().SelfDber.Entities<CmcsTransportPicture>(String.Format(" where TransportId='{0}'", entity.Id)).Count > 0)
                    {
                        FrmTransportPicture frmPic = new FrmTransportPicture(entity.Id, entity.CarNumber);
                        if (frmPic.ShowDialog() == DialogResult.OK)
                        {
                            BindData();
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("����ץ��ͼƬ��", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
            }
        }

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {

            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsBuyFuelTransport entity = gridRow.DataItem as CmcsBuyFuelTransport;
                if (entity == null) return;

                // �����Ч״̬
                gridRow.Cells["clmIsUse"].Value = (entity.IsUse == 1 ? "��" : "��");
                CmcsInFactoryBatch cmcsinfactorybatch = Dbers.GetInstance().SelfDber.Get<CmcsInFactoryBatch>(entity.InFactoryBatchId);
                if (cmcsinfactorybatch != null)
                {
                    gridRow.Cells["clmInFactoryBatchNumber"].Value = cmcsinfactorybatch.Batch;
                }

                //List<CmcsTransportPicture> cmcstrainwatchs = Dbers.GetInstance().SelfDber.Entities<CmcsTransportPicture>(String.Format(" where TransportId='{0}'", gridRow.Cells["clmId"].Value));
                //if (cmcstrainwatchs.Count == 0)
                //{
                //    //gridRow.Cells["clmPic"].Value = "";
                //}

                if (entity.SerialNumber == "�ϼ�")
                {
                    gridRow.Cells["clmShow"].Value = string.Empty;
                    gridRow.Cells["clmEdit"].Value = string.Empty;
                    gridRow.Cells["clmDelete"].Value = string.Empty;
                    gridRow.Cells["clmPic"].Value = string.Empty;

                    gridRow.Cells["InFactoryTime"].Visible = false;
                    gridRow.Cells["clmIsUse"].Visible = false;
                }

            }
        }

        #endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (CurrExportData.Count == 0)
            {
                MessageBox.Show("���Ȳ�ѯ����");
                return;
            }
            List<CmcsBuyFuelTransport> CurrPrinttData = new List<CmcsBuyFuelTransport>();
            List<CmcsBuyFuelTransport> Data1 = new List<CmcsBuyFuelTransport>();
           
            Data1 = CurrExportData;
            int totalPapge = (CurrExportData.Count / 29) + (CurrExportData.Count % 29 == 0 ? 0 : 1);

            for (int i = 1;i <= totalPapge; i++)
            {
                List<CmcsBuyFuelTransport> Data2 = new List<CmcsBuyFuelTransport>();
                if (i< totalPapge)
                {
                    for(int j = (i - 1) * 29; j < i * 29; j++)
                    {
                        CurrExportData[j].OrderNumber = j + 1;
                        Data2.Add(CurrExportData[j]);
                        CurrPrinttData.Add(CurrExportData[j]);
                    }
                    CmcsBuyFuelTransport item = new CmcsBuyFuelTransport();
                    item.SupplierName = "С��";
                    item.CarNumber = Data2.Count.ToString();
                    item.GrossWeight = Data2.Sum(a=>a.GrossWeight);
                    item.TareWeight = Data2.Sum(a => a.TareWeight);
                    item.SuttleWeight = Data2.Sum(a => a.SuttleWeight);
                    CurrPrinttData.Add(item);
                }
                else
                {
                    for (int j = (i - 1) * 29; j < CurrExportData.Count; j++)
                    {
                        CurrExportData[j].OrderNumber = j + 1;
                        Data2.Add(CurrExportData[j]);
                        CurrPrinttData.Add(CurrExportData[j]);
                    }
                    CmcsBuyFuelTransport item = new CmcsBuyFuelTransport();
                    item.SupplierName = "С��";
                    item.CarNumber = Data2.Count.ToString();
                    item.GrossWeight = Data2.Sum(a => a.GrossWeight);
                    item.TareWeight = Data2.Sum(a => a.TareWeight);
                    item.SuttleWeight = Data2.Sum(a => a.SuttleWeight);
                    CurrPrinttData.Add(item);
                }
            }
            CmcsBuyFuelTransport item1 = new CmcsBuyFuelTransport();
            item1.SupplierName = "�ϼ�";
            item1.CarNumber = Data1.Count.ToString();
            item1.GrossWeight = Data1.Sum(a => a.GrossWeight);
            item1.TareWeight = Data1.Sum(a => a.TareWeight);
            item1.SuttleWeight = Data1.Sum(a => a.SuttleWeight);
            CurrPrinttData.Add(item1);

            this.wagonPrinter.Print(CurrPrinttData, null, this.dtInputStart.Value, this.dtInputEnd.Value);
            //FrmPrintWeb frm = new FrmPrintWeb(CurrPrinttData, null, this.dtInputStart.Value, this.dtInputEnd.Value);
            //frm.ShowDialog();
        }

        private void superGridControl1_GetRowHeaderText(object sender, GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (PageSize* CurrentIndex+ e.GridRow.RowIndex + 1).ToString();
        }
    }
}
