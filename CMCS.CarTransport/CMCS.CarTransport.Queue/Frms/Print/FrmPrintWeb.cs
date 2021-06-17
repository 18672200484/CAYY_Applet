using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar;
using DevComponents.Editors;
using CMCS.CarTransport.Queue.Print;

using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.Common.Entities.CarTransport;
using CMCS.CarTransport.Queue.Frms.Print;

namespace CMCS.CarTransport.Queue.Print
{
    public partial class FrmPrintWeb : MetroAppForm
    {
        CommonDAO commonDAO = CommonDAO.GetInstance();
        WagonPrinterCollect wagonPrinter = null;

        //CmcsGoodsTransport _Transport = null;
        //Font TitleFont = new Font("宋体", 20, FontStyle.Bold, GraphicsUnit.Pixel);
        //Font ContentFont = new Font("宋体", 14, FontStyle.Regular, GraphicsUnit.Pixel);

        Font TitleFont = new Font("宋体", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        Font ContentFont = new Font("宋体", 15, FontStyle.Regular, GraphicsUnit.Pixel);

        List<CmcsBuyFuelTransport> _Transport = null;
        DateTime StartTime = new DateTime();
        DateTime EndTime = new DateTime();
        //private int count;
        int currPage = 1;//当前打印页数
        int pagesize = 30;//每页打印行数
        int printRowCount = 0;//已打印数据
        int LineHeight = 30;//行高
        int paddingleft = 10;//矩形距离左边的距离
        int paddingtopAll = 60;//整体距离上部的距离
        bool b;

        Graphics gs = null;
        public FrmPrintWeb(List<CmcsBuyFuelTransport> transport, Graphics gs, DateTime start, DateTime end)
        {
            if(transport!=null)
            _Transport = transport;
            StartTime = start;
            EndTime = end;

            InitializeComponent();
        }

        /// <summary>
        /// 打印磅单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void tsmiPrint_Click(object sender, EventArgs e)
        {
            this.wagonPrinter.Print(this._Transport, null, StartTime, EndTime);
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.panel1.BackColor = Color.White;
            this.Width = 820;
            this.Height = 800;


            Graphics gs = e.Graphics;
            #region 汇总页头

            int paddingtop = paddingtopAll + 80;//矩形距离上部的距离
            float cloum1 = 180;//供应商的宽
            float cloum2 = 70;//车数的宽
            float cloum3 = 65;//重量的宽
            float cloum4 = 100;
            float cloum0 = 40;

            int RowWeight = Convert.ToInt32(cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 * 3);//列表宽

            //当前页条数
            int totalPapge = (this._Transport.Count / this.pagesize) + (this._Transport.Count % this.pagesize == 0 ? 0 : 1);
            int pageCount = pagesize;
            if (currPage >= totalPapge)
                pageCount = this._Transport.Count - ((currPage - 1) * pagesize);

            int Height = LineHeight * (pageCount + 1);//矩形的高
            int top = 0;
            Rectangle r = new Rectangle(paddingleft, paddingtop, RowWeight, Height);//矩形的位置和大小
            Pen pen = new Pen(Color.Black, 1);//画笔
            gs.DrawRectangle(pen, r);

            string title = "汽车煤数量统计";
            Font titlefont = new Font("宋体", 20, FontStyle.Bold, GraphicsUnit.Pixel);

            gs.DrawString(title, titlefont, Brushes.Black, (RowWeight - gs.MeasureString(title, titlefont).Width) / 2, paddingtopAll);

            string recordDate = "统计时间：" + StartTime.ToString() + "至" + EndTime.ToString();
            gs.DrawString(recordDate, ContentFont, Brushes.Black, (RowWeight - gs.MeasureString(recordDate, ContentFont).Width) / 2, paddingtopAll + 50);

            //gs.DrawString("范围：货物名称=" + _Transport[0].FuelKindName, ContentFont, Brushes.Black, paddingleft + 10, paddingtopAll + 65);

            int seralinumber = 1;//序号
                                 //列标题

            //序号
            string OrderNumTitle = "序号";
            SizeF OrderNumSizeTitle = gs.MeasureString(OrderNumTitle, ContentFont);
            gs.DrawString(OrderNumTitle, ContentFont, Brushes.Black, paddingleft + (cloum0 - OrderNumSizeTitle.Width) / 2, paddingtop + (LineHeight - OrderNumSizeTitle.Height) / 2);
            //矿点
            string SupplierNameTitle = "矿点";
            SizeF SupplierNameSizeTitle = gs.MeasureString(SupplierNameTitle, ContentFont);
            gs.DrawString(SupplierNameTitle, ContentFont, Brushes.Black, paddingleft + cloum0 + (cloum1 - SupplierNameSizeTitle.Width) / 2, paddingtop + (LineHeight - SupplierNameSizeTitle.Height) / 2);
            //车号
            string CarNumberTitle = "车号";
            SizeF CarNumberTitleSizeTitle = gs.MeasureString(CarNumberTitle, ContentFont);
            gs.DrawString(CarNumberTitle, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + (cloum2 - CarNumberTitleSizeTitle.Width) / 2, paddingtop + (LineHeight - CarNumberTitleSizeTitle.Height) / 2);
            //入厂日期
            string InFactoryTimeTitle = "入厂日期";
            SizeF InFactoryTimeSizeTitle = gs.MeasureString(InFactoryTimeTitle, ContentFont);
            gs.DrawString(InFactoryTimeTitle, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + (cloum4 - InFactoryTimeSizeTitle.Width) / 2, paddingtop + (LineHeight - InFactoryTimeSizeTitle.Height) / 2);
            //驳船号
            string ShipNumberTitle = "驳船号";
            SizeF ShipNumberSizeTitle = gs.MeasureString(ShipNumberTitle, ContentFont);
            gs.DrawString(ShipNumberTitle, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + (cloum3 - ShipNumberSizeTitle.Width) / 2, paddingtop + (LineHeight - ShipNumberSizeTitle.Height) / 2);
            //轻车设备
            string TarePlaceTitle = "轻车设备";
            SizeF TarePlaceSizeTitle = gs.MeasureString(TarePlaceTitle, ContentFont);
            gs.DrawString(TarePlaceTitle, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + (cloum4 - TarePlaceSizeTitle.Width) / 2, paddingtop + (LineHeight - TarePlaceSizeTitle.Height) / 2);

            //毛重
            string GrossWeightTitle = "毛重";
            SizeF GrossWeightSizeTitle = gs.MeasureString(GrossWeightTitle, ContentFont);
            gs.DrawString(GrossWeightTitle, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + (cloum3 - GrossWeightSizeTitle.Width) / 2, paddingtop + (LineHeight - GrossWeightSizeTitle.Height) / 2);

            //皮重
            string TareWeightTitle = "皮重";
            SizeF TareWeightSizeTitle = gs.MeasureString(TareWeightTitle, ContentFont);
            gs.DrawString(TareWeightTitle, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 + (cloum3 - TareWeightSizeTitle.Width) / 2, paddingtop + (LineHeight - TareWeightSizeTitle.Height) / 2);

            //净重
            string SuttleWeightTitle = "净重";
            SizeF SuttleWeightSizeTitle = gs.MeasureString(SuttleWeightTitle, ContentFont);
            gs.DrawString(SuttleWeightTitle, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 * 2 + (cloum3 - SuttleWeightSizeTitle.Width) / 2, paddingtop + (LineHeight - SuttleWeightSizeTitle.Height) / 2);

            gs.DrawLine(pen, paddingleft, paddingtop + LineHeight, paddingleft + RowWeight, paddingtop + LineHeight);

            #endregion
            if (currPage >= totalPapge)
            {
                for (int i = (currPage - 1) * 30; i < this._Transport.Count; i++)
                {
                    top = LineHeight * seralinumber;
                    CmcsBuyFuelTransport item = this._Transport[i];
                    //序号
                    string OrderNum = item.OrderNumber.ToString();
                    SizeF OrderNumSize = gs.MeasureString(OrderNum, ContentFont);

                    //矿点
                    string SupplierName = item.SupplierName;
                    SizeF SupplierNameSize = gs.MeasureString(SupplierName, ContentFont);

                    if (item.SupplierName == "合计" || item.SupplierName == "小计")
                        gs.DrawString(SupplierName, ContentFont, Brushes.Black, paddingleft + (cloum0 + cloum1 - SupplierNameSize.Width) / 2, paddingtop + top + (LineHeight - SupplierNameSize.Height) / 2);
                    else
                        gs.DrawString(OrderNum, ContentFont, Brushes.Black, paddingleft + (cloum0 - OrderNumSize.Width) / 2, paddingtop + top + (LineHeight - OrderNumSize.Height) / 2);

                    if (SupplierName == "合计" || item.SupplierName == "小计")
                    {
                        //gs.DrawString(SupplierName, ContentFont, Brushes.Black, paddingleft + (cloum1 - SupplierNameSize.Width) / 2, paddingtop + top + (LineHeight - SupplierNameSize.Height) / 2);
                    }
                    else
                    {
                        gs.DrawString(SupplierName, ContentFont, Brushes.Black, paddingleft + cloum0 + (cloum1 - SupplierNameSize.Width) / 2, paddingtop + top + (LineHeight - SupplierNameSize.Height) / 2);
                    }
                    //车号
                    string CarNumber = item.CarNumber;
                    SizeF CarNumberSize = gs.MeasureString(CarNumber, ContentFont);
                    //if (SupplierName == "")
                    //    gs.DrawString(FuelKindName, ContentFont, Brushes.Black, paddingleft + cloum1 + (cloum2 - FuelKindNameSize.Width) / 2, paddingtop + top + (LineHeight - FuelKindNameSize.Height) / 2);
                    //else
                    gs.DrawString(CarNumber, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + (cloum2 - CarNumberSize.Width) / 2, paddingtop + top + (LineHeight - CarNumberSize.Height) / 2);
                    //入厂日期
                    string InFactoryTime = item.InFactoryTime.Year > 2000 ? item.InFactoryTime.ToString("yyyy-MM-dd ") : "";
                    SizeF InFactoryTimeSize = gs.MeasureString(InFactoryTime, ContentFont);
                    gs.DrawString(InFactoryTime, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + (cloum4 - InFactoryTimeSize.Width) / 2, paddingtop + top + (LineHeight - InFactoryTimeSize.Height) / 2);
                    //驳船号
                    string ShipNumber = item.ShipName;
                    SizeF ShipNumberSize = gs.MeasureString(ShipNumber, ContentFont);
                    gs.DrawString(ShipNumber, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + (cloum3 - ShipNumberSize.Width) / 2, paddingtop + top + (LineHeight - ShipNumberSize.Height) / 2);
                    //轻车设备
                    string TarePlace = string.IsNullOrEmpty(item.TarePlace) ? "" : item.TarePlace.Replace("汽车智能化-", "");
                    SizeF TarePlaceSize = gs.MeasureString(TarePlace, ContentFont);
                    gs.DrawString(TarePlace, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + (cloum4 - TarePlaceSize.Width) / 2, paddingtop + top + (LineHeight - TarePlaceSize.Height) / 2);
                    //毛重
                    string GrossWeight = item.GrossWeight.ToString("F2");
                    SizeF GrossWeightSize = gs.MeasureString(GrossWeight, ContentFont);
                    gs.DrawString(GrossWeight, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + (cloum3 - GrossWeightSize.Width) / 2, paddingtop + top + (LineHeight - GrossWeightSize.Height) / 2);
                    //皮重
                    string TareWeight = item.TareWeight.ToString("F2");
                    SizeF TareWeightSize = gs.MeasureString(TareWeight, ContentFont);
                    gs.DrawString(TareWeight, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 + (cloum3 - TareWeightSize.Width) / 2, paddingtop + top + (LineHeight - TareWeightSize.Height) / 2);
                    //净重
                    string SuttleWeight = item.SuttleWeight.ToString("F2");
                    SizeF SuttleWeightSize = gs.MeasureString(SuttleWeight, ContentFont);
                    gs.DrawString(SuttleWeight, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 * 2 + (cloum3 - SuttleWeightSize.Width) / 2, paddingtop + top + (LineHeight - SuttleWeightSize.Height) / 2);

                    gs.DrawLine(pen, paddingleft, paddingtop + LineHeight * seralinumber, paddingleft + RowWeight, paddingtop + LineHeight * seralinumber);
                    seralinumber++;
                }
            }
            else
            {
                for (int i = (currPage - 1) * 30; i < currPage * 30; i++)
                {
                    top = LineHeight * seralinumber;
                    CmcsBuyFuelTransport item = this._Transport[i];
                    //序号
                    string OrderNum = item.OrderNumber.ToString();
                    SizeF OrderNumSize = gs.MeasureString(OrderNum, ContentFont);

                    //矿点
                    string SupplierName = item.SupplierName;
                    SizeF SupplierNameSize = gs.MeasureString(SupplierName, ContentFont);

                    if (item.SupplierName == "合计" || item.SupplierName == "小计")
                        gs.DrawString(SupplierName, ContentFont, Brushes.Black, paddingleft + (cloum0 + cloum1 - SupplierNameSize.Width) / 2, paddingtop + top + (LineHeight - SupplierNameSize.Height) / 2);
                    else
                        gs.DrawString(OrderNum, ContentFont, Brushes.Black, paddingleft + (cloum0 - OrderNumSize.Width) / 2, paddingtop + top + (LineHeight - OrderNumSize.Height) / 2);

                    if (SupplierName == "合计" || item.SupplierName == "小计")
                    {
                        //gs.DrawString(SupplierName, ContentFont, Brushes.Black, paddingleft + (cloum1 - SupplierNameSize.Width) / 2, paddingtop + top + (LineHeight - SupplierNameSize.Height) / 2);
                    }
                    else
                    {
                        gs.DrawString(SupplierName, ContentFont, Brushes.Black, paddingleft + cloum0 + (cloum1 - SupplierNameSize.Width) / 2, paddingtop + top + (LineHeight - SupplierNameSize.Height) / 2);
                    }

                    //车号
                    string CarNumber = item.CarNumber;
                    SizeF CarNumberSize = gs.MeasureString(CarNumber, ContentFont);
                    //if (SupplierName == "")
                    //    gs.DrawString(FuelKindName, ContentFont, Brushes.Black, paddingleft + cloum1 + (cloum2 - FuelKindNameSize.Width) / 2, paddingtop + top + (LineHeight - FuelKindNameSize.Height) / 2);
                    //else
                    gs.DrawString(CarNumber, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + (cloum2 - CarNumberSize.Width) / 2, paddingtop + top + (LineHeight - CarNumberSize.Height) / 2);
                    //入厂日期
                    string InFactoryTime = item.InFactoryTime.Year > 2000 ? item.InFactoryTime.ToString("yyyy-MM-dd ") : "";
                    SizeF InFactoryTimeSize = gs.MeasureString(InFactoryTime, ContentFont);
                    gs.DrawString(InFactoryTime, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + (cloum4 - InFactoryTimeSize.Width) / 2, paddingtop + top + (LineHeight - InFactoryTimeSize.Height) / 2);
                    //驳船号
                    string ShipNumber = item.ShipName;
                    SizeF ShipNumberSize = gs.MeasureString(ShipNumber, ContentFont);
                    gs.DrawString(ShipNumber, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + (cloum3 - ShipNumberSize.Width) / 2, paddingtop + top + (LineHeight - ShipNumberSize.Height) / 2);
                    //轻车设备
                    string TarePlace = string.IsNullOrEmpty(item.TarePlace) ? "" : item.TarePlace.Replace("汽车智能化-", "");
                    SizeF TarePlaceSize = gs.MeasureString(TarePlace, ContentFont);
                    gs.DrawString(TarePlace, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + (cloum4 - TarePlaceSize.Width) / 2, paddingtop + top + (LineHeight - TarePlaceSize.Height) / 2);
                    //毛重
                    string GrossWeight = item.GrossWeight.ToString("F2");
                    SizeF GrossWeightSize = gs.MeasureString(GrossWeight, ContentFont);
                    gs.DrawString(GrossWeight, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + (cloum3 - GrossWeightSize.Width) / 2, paddingtop + top + (LineHeight - GrossWeightSize.Height) / 2);
                    //皮重
                    string TareWeight = item.TareWeight.ToString("F2");
                    SizeF TareWeightSize = gs.MeasureString(TareWeight, ContentFont);
                    gs.DrawString(TareWeight, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 + (cloum3 - TareWeightSize.Width) / 2, paddingtop + top + (LineHeight - TareWeightSize.Height) / 2);
                    //净重
                    string SuttleWeight = item.SuttleWeight.ToString("F2");
                    SizeF SuttleWeightSize = gs.MeasureString(SuttleWeight, ContentFont);
                    gs.DrawString(SuttleWeight, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 * 2 + (cloum3 - SuttleWeightSize.Width) / 2, paddingtop + top + (LineHeight - SuttleWeightSize.Height) / 2);

                    gs.DrawLine(pen, paddingleft, paddingtop + LineHeight * seralinumber, paddingleft + RowWeight, paddingtop + LineHeight * seralinumber);
                    seralinumber++;
                }
            }

            if (currPage >= totalPapge)
            {
                gs.DrawLine(pen, paddingleft + cloum0, paddingtop, paddingleft + cloum0, paddingtop + Height - LineHeight - LineHeight);
            }
            else
            {
                gs.DrawLine(pen, paddingleft + cloum0, paddingtop, paddingleft + cloum0, paddingtop + Height - LineHeight);
            }
            gs.DrawLine(pen, paddingleft + cloum0 + cloum1, paddingtop, paddingleft + cloum0 + cloum1, paddingtop + Height);
            gs.DrawLine(pen, paddingleft + cloum0 + cloum1 + cloum2, paddingtop, paddingleft + cloum0 + cloum1 + cloum2, paddingtop + Height);

            if (currPage >= totalPapge)
            {
                gs.DrawLine(pen, paddingleft + cloum0 + cloum1 + cloum2 + cloum4, paddingtop, paddingleft + cloum0 + cloum1 + cloum2 + cloum4, paddingtop + Height - LineHeight - LineHeight);
                gs.DrawLine(pen, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3, paddingtop, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3, paddingtop + Height - LineHeight - LineHeight);
            }
            else
            {
                gs.DrawLine(pen, paddingleft + cloum0 + cloum1 + cloum2 + cloum4, paddingtop, paddingleft + cloum0 + cloum1 + cloum2 + cloum4, paddingtop + Height - LineHeight);
                gs.DrawLine(pen, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3, paddingtop, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3, paddingtop + Height - LineHeight);
            }

            gs.DrawLine(pen, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4, paddingtop, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4, paddingtop + Height);
            gs.DrawLine(pen, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3, paddingtop, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3, paddingtop + Height);
            gs.DrawLine(pen, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 * 2, paddingtop, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 * 2, paddingtop + Height);
            //gs.DrawLine(pen, paddingleft + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 * 3, paddingtop, paddingleft + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 * 3, paddingtop + Height);


        }
        public static string DisposeTime(string dt, string format)
        {
            if (!string.IsNullOrEmpty(dt))
            {
                DateTime dti = DateTime.Parse(dt);
                if (dti != DateTime.MinValue)
                    return dti.ToString(format);
            }
            return string.Empty;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void FrmPrintWeb_Load(object sender, EventArgs e)
        {
            this.wagonPrinter = new WagonPrinterCollect(printDocument1);
        }


        /// <summary>
        /// 字符串按指定长度换行
        /// </summary>
        /// <param name="conten">字符串</param>
        /// <param name="start">指定长度</param>
        /// <param name="sSymbol">换行符</param>
        /// <returns>按指定长度换行后的字符串</returns>
        public static string SqritBySymbol(string conten, int start, string sSymbol)
        {
            string str = "";
            char[] param = conten.ToCharArray();
            if (param.Length > 0)
            {

                if (param.Length > start)
                {
                    int j = 1;
                    for (int i = 0; i < param.Length; i++)
                    {
                        str += param[i];
                        if (i == (start * j))
                        {
                            j++;
                            str = str + sSymbol;
                        }
                    }
                }
            }
            return str;
        }

        private void metroShell1_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            //this.printPreviewDialog1.ShowDialog();

            //PrintPreviewDialog PrintPriview = new PrintPreviewDialog();
            //PrintPriview.Document = CreatePrintDocument(dt, Title);
            //PrintPriview.WindowState = FormWindowState.Maximized;
            //PrintPriview.ShowDialog();

        }
    }
}
