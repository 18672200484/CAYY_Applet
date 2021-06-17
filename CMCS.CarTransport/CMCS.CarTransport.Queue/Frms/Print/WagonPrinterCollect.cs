using System;
using System.Collections.Generic;
//
using System.Drawing;
using System.Drawing.Printing;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using CMCS.Common.Entities.CarTransport;
using System.Linq;

namespace CMCS.CarTransport.Queue.Frms.Print
{
    public partial class WagonPrinterCollect : MetroAppForm
    {
        Font TitleFont = new Font("宋体", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        Font ContentFont = new Font("宋体", 15, FontStyle.Regular, GraphicsUnit.Pixel);
        PrintDocument _PrintDocument = null;
        List<CmcsBuyFuelTransport> _Transport = null;
        DateTime StartTime = new DateTime();
        DateTime EndTime = new DateTime();
        //private int count;
        int currPage = 1;//当前打印页数
        int pagesize = 30;//每页打印行数
        int printRowCount = 0;//已打印数据
        int LineHeight = 30;//行高
        int paddingleft = 30;//矩形距离左边的距离
        int paddingtopAll = 60;//整体距离上部的距离
        bool b;
        
        public WagonPrinterCollect(PrintDocument printDoc)
        {
            this._PrintDocument = printDoc;
            //this._PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 800, LineHeight * (pagesize + 1) + paddingtopAll + 200);
            //this._PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 850, 1000);
            this._PrintDocument.OriginAtMargins = true;
            this._PrintDocument.DefaultPageSettings.Margins.Left = 10;
            this._PrintDocument.DefaultPageSettings.Margins.Right = 0;
            this._PrintDocument.DefaultPageSettings.Margins.Top = 0;
            this._PrintDocument.DefaultPageSettings.Margins.Bottom = 0;
            //this._PrintDocument.DefaultPageSettings.Landscape = false;
            this._PrintDocument.PrintController = new StandardPrintController();
            this._PrintDocument.PrintPage += new PrintPageEventHandler(_PrintDocument_PrintPage);
            

            InitializeComponent();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transport"></param>
        /// <param name="gs"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void Print(List<CmcsBuyFuelTransport> transport, Graphics gs, DateTime start, DateTime end)
        {
            if (transport == null) return;
            _Transport = transport;
            //this.b = b;
            StartTime = start;
            EndTime = end;

            //当前页条数
            int totalPapge = (this._Transport.Count / this.pagesize) + (this._Transport.Count % this.pagesize == 0 ? 0 : 1);
            int pageCount = pagesize;
            if (currPage >= totalPapge)
            {
                pageCount = this._Transport.Count - ((currPage - 1) * pagesize);
            }
  

            //this._PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", 1200, LineHeight * (pageCount + 1) + paddingtopAll + 200);
            //this.ClientSize = new System.Drawing.Size(1200, LineHeight * (pageCount + 1) + paddingtopAll + 200);
            try
            {
                PrintPreviewDialog PrintPriview = new PrintPreviewDialog();
                PrintPriview.Document = this._PrintDocument;
                PrintPriview.WindowState = FormWindowState.Maximized;
                PrintPriview.ShowDialog();
                //for (int i = 0; i < totalPapge; i++)
                //{
                //    //this._PrintDocument.Print();
                //    //e.hasmorePage = true;
                //    PrintPriview.ShowDialog();
                //    if (currPage < totalPapge) 
                //        currPage++;
                //    else
                //        currPage = 1;
                //}



            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("打印异常，请检查打印机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _Transport = null;
        }

        private void _PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics gs = e.Graphics;


            #region 汇总页头

            int paddingtop = paddingtopAll + 80;//矩形距离上部的距离
            float cloum1 = 220;//供应商的宽
            float cloum2 = 70;//车数的宽
            float cloum3 = 60;//重量的宽
            float cloum4 = 82;
            float cloum0 = 40;

            int RowWeight = Convert.ToInt32(cloum0+cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 * 3);//列表宽

            //当前页条数
            int totalPapge = (this._Transport.Count / this.pagesize) + (this._Transport.Count % this.pagesize == 0 ? 0 : 1);
            int pageCount = pagesize;
            if (currPage >= totalPapge)
            {
                pageCount = this._Transport.Count - ((currPage - 1) * pagesize);
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
               
            }

            int Height = LineHeight * (pageCount + 1);//矩形的高
            int top = 0;
            Rectangle r = new Rectangle(paddingleft, paddingtop, RowWeight, Height);//矩形的位置和大小
            Pen pen = new Pen(Color.Black, 1);//画笔
            gs.DrawRectangle(pen, r);

            string title = "汽车煤数量统计";
            Font titlefont = new Font("宋体", 20, FontStyle.Bold, GraphicsUnit.Pixel);

            gs.DrawString(title, titlefont, Brushes.Black, (RowWeight - gs.MeasureString(title, titlefont).Width) / 2, paddingtopAll);

            string recordDate = "统计时间：" + StartTime.ToString() + "至" + EndTime.ToString();
            gs.DrawString(recordDate, ContentFont, Brushes.Black, (RowWeight - gs.MeasureString(recordDate, ContentFont).Width) / 2, paddingtopAll+50);

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
            gs.DrawString(SupplierNameTitle, ContentFont, Brushes.Black, paddingleft + cloum0+(cloum1 - SupplierNameSizeTitle.Width) / 2, paddingtop + (LineHeight - SupplierNameSizeTitle.Height) / 2);
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
            gs.DrawString(GrossWeightTitle, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4+(cloum3 - GrossWeightSizeTitle.Width) / 2, paddingtop + (LineHeight - GrossWeightSizeTitle.Height) / 2);

            //皮重
            string TareWeightTitle = "皮重";
            SizeF TareWeightSizeTitle = gs.MeasureString(TareWeightTitle, ContentFont);
            gs.DrawString(TareWeightTitle, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3 + (cloum3 - TareWeightSizeTitle.Width) / 2, paddingtop + (LineHeight - TareWeightSizeTitle.Height) / 2);
          
            //净重
            string SuttleWeightTitle = "净重";
            SizeF SuttleWeightSizeTitle = gs.MeasureString(SuttleWeightTitle, ContentFont);
            gs.DrawString(SuttleWeightTitle, ContentFont, Brushes.Black, paddingleft + cloum0 + cloum1 + cloum2 + cloum4 + cloum3 + cloum4 + cloum3*2+ (cloum3 - SuttleWeightSizeTitle.Width) / 2, paddingtop + (LineHeight - SuttleWeightSizeTitle.Height) / 2);

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
                    string SupplierName = item.SupplierName.Length>14?item.SupplierName.Substring(0,14): item.SupplierName;
                    SizeF SupplierNameSize = gs.MeasureString(SupplierName, ContentFont);

                    if (item.SupplierName == "合计"|| item.SupplierName == "小计")
                        gs.DrawString(SupplierName, ContentFont, Brushes.Black, paddingleft + (cloum0+cloum1 - SupplierNameSize.Width) / 2, paddingtop + top + (LineHeight - SupplierNameSize.Height) / 2);
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
                    string SupplierName = item.SupplierName.Length > 14 ? item.SupplierName.Substring(0, 14) : item.SupplierName;
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
                gs.DrawLine(pen, paddingleft + cloum0, paddingtop, paddingleft + cloum0, paddingtop + Height- LineHeight- LineHeight);
            }
            else
            {
                gs.DrawLine(pen, paddingleft + cloum0, paddingtop, paddingleft + cloum0, paddingtop + Height- LineHeight);
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

            if (currPage >= totalPapge)
            {
                currPage=1;
            }
            else
            {
                currPage++;
            }
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
    }
}
