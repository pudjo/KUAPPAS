using BP.Sinergi;
using DTO.Laporan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace KUAPPAS.External
{
    public partial class frmSinergi : Form
    {
        PdfPage previousPage;
        int m_iJenisBendahara;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        public frmSinergi()
        {
            InitializeComponent();
        }

        private void cmdDTH_Click(object sender, EventArgs e)
        {

            int periode= ctrlBulan1.GetID();
            SinergiLogic oLOgic = new SinergiLogic(GlobalVar.TahunAnggaran);
            DataTable dtSinergi = oLOgic.ExportDTA(periode);
          

            if (dtSinergi != null)
            {
                string fileName = BuatFile();

                Excel.Application xlsApp;
                Excel.Workbook xlsWorkbook;
                Excel.Worksheet xlsWorksheet;
                Excel.Worksheet xlsWorksheetpajak;

                object misValue = System.Reflection.Missing.Value;

                // Remove the old excel report file
                try
                {
                    FileInfo oldFile = new FileInfo(fileName);
                    if(oldFile.Exists)
                    {
                        File.SetAttributes(oldFile.FullName, FileAttributes.Normal);
                        oldFile.Delete();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error removing old Excel report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                try
                {
                    xlsApp = new Excel.Application();
                    xlsWorkbook = xlsApp.Workbooks.Add(misValue);
                    xlsWorksheet = (Excel.Worksheet)xlsWorkbook.Sheets[1];      

                    int i = 1;

        
                    if(dtSinergi.Rows.Count>0)
                    {
                        for(int j = 0; j < dtSinergi.Columns.Count; ++j)
                        {
                            xlsWorksheet.Cells[i, j + 1] = dtSinergi.Columns[j].ColumnName;
                        }
                        ++i;
                    }

                   for (int r = 2; r < dtSinergi.Rows.Count; r++)
                    {
                         DataRow dr = dtSinergi.Rows[r-2];
                         for (int j = 1; j <= dtSinergi.Columns.Count; j++)
                         {
                             if (dr[dtSinergi.Columns[j - 1].ColumnName] != null)
                             {
                                 xlsWorksheet.Cells[r, j] = dr[dtSinergi.Columns[j - 1].ColumnName];
                             }
                             else
                             {
                                 xlsWorksheet.Cells[r, j] = "";
                             }
                         } 
                        
                    }

                     

                    xlsWorksheetpajak = (Excel.Worksheet)xlsWorkbook.Sheets[2];

                    i = 1;
                    DataTable dtPajak = oLOgic.ExportPajak(periode);
                    if (dtPajak != null)
                    {
                        if (dtPajak.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtPajak.Columns.Count; ++j)
                            {
                                xlsWorksheetpajak.Cells[i, j + 1] = dtPajak.Columns[j].ColumnName;
                            }
                            ++i;
                        }

                        for (int r = 2; r < dtPajak.Rows.Count; r++)
                        {
                            DataRow dr = dtPajak.Rows[r - 2];
                            for (int j = 1; j <= dtPajak.Columns.Count; j++)
                            {
                                if (dr[dtPajak.Columns[j - 1].ColumnName] != null)
                                {
                                    xlsWorksheetpajak.Cells[r, j] = dr[dtPajak.Columns[j - 1].ColumnName];
                                }
                                else
                                {
                                    xlsWorksheetpajak.Cells[r, j] = "";
                                }
                            }

                        }
                    }

                    
                    //
                    for (int id = 1; id <= 2;id++ ){
                        Excel.Worksheet sheet;
                        sheet = (Excel.Worksheet)xlsWorkbook.Sheets[id];
                        if (id == 1)
                        {
                            sheet.Name = "Belanja";
                        }
                        if (id == 2)
                        {
                            sheet.Name = "Pajak";
                        }
                        
                       
    
                    }
  //                  xlsWorkbook.Worksheets[0].
                    xlsWorkbook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, Excel.XlSaveConflictResolution.xlLocalSessionChanges, misValue, misValue, misValue, misValue);
                    
                    xlsWorkbook.Close(true, misValue, misValue);
                    xlsApp.Quit();

                    ReleaseObject(xlsWorksheet);
                    ReleaseObject(xlsWorkbook);
                    ReleaseObject(xlsApp);

                    if(MessageBox.Show("Excel report has been created on your desktop\nWould you like to open it?", "Created Excel report",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Process.Start(fileName);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error creating Excel report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void frmSinergi_Load(object sender, EventArgs e)
        {
            ctrlBulan1.Create();
            this.Text = "Sinergi";
            ctrlHeader1.SetCaption("Persiapkan data sinergi");
        }
        static private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
             private string BuatFile()
        {

            string sRet = "";
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.Filter = "Excel|*.xlsx;*.xls";
            fdlg.Title = "Save an Image File";
            fdlg.ShowDialog();

            fdlg.Title = "Buat File file";
            fdlg.InitialDirectory = @"c:\";

            //fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel|*.xlsx;*.xls";
            fdlg.RestoreDirectory = true;



            sRet = fdlg.FileName;



            return sRet;
        }
        private void KillSpecificExcelFileProcess(string excelFileName)
        {
            var processes = from p in Process.GetProcessesByName("EXCEL")
                            select p;

            foreach (var process in processes)
            {
                if (process.MainWindowTitle == "Microsoft Excel - " + excelFileName)
                    process.Kill();
            }
        }

        private void cmdEditBTBT_Click(object sender, EventArgs e)
        {
            SinergiLogic oLogic = new SinergiLogic(GlobalVar.TahunAnggaran);
            if (oLogic.BetulkanBTBT() == true)
            {
                MessageBox.Show("Perbaikan sudah selesai");
            }
            else
            {
                MessageBox.Show(oLogic.LastError());
            }
        }

        private void cmdCetakDTH_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        List<CetakanDTH>lst = new List<CetakanDTH>();

        //        SinergiLogic oLOgic = new SinergiLogic(GlobalVar.TahunAnggaran);
        //       lst  = oLOgic.GetCetakanDTH();
        //       if (lst != null)
        //       {

        //       }
        //       else
        //       {
        //           MessageBox.Show(oLOgic.LastError());
        //       }
        //       return;

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //private void Cetak(ref List<CetakanDTH> lst)
        //{
        //    try
        //    {
        //        PdfDocument document = new PdfDocument();

        //        PdfSection section = document.Sections.Add();
        //        document.PageSettings.Margins.Left = 8;
        //        document.PageSettings.Margins.Top = 5;
        //        document.PageSettings.Margins.Right = 2;
        //        document.PageSettings.Margins.Bottom = 8;

        //        section.PageSettings.Width = 612;// = PdfPageSize.Legal;
        //        section.PageSettings.Height = 935;// = PdfPageSize.Legal;

        //        section.PageSettings.Orientation = PdfPageOrientation.Portrait;

        //        float yPos = 0;
     
        //        PdfPage page = section.Pages.Add();
        //        previousPage = page;
        //        PdfGraphics graphics = page.Graphics;
    

        //        yPos = 10;
        //        PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);


        //        PdfStringFormat stringFormat = new PdfStringFormat();
        //        stringFormat.Alignment = PdfTextAlignment.Center;
        //        stringFormat.LineAlignment = PdfVerticalAlignment.Middle;

        //        //stringFormat.CharacterSpacing = 2f;
        //        CetakPDF oCetakPDF = new CetakPDF();
        //        //SizeF size = font12.MeasureString("xxx");

               

        //        float kiri = 15;


        //        #region gridKas
        //        PdfGrid pdfGrid = new PdfGrid();

        //        int count = 0;
        //        //Create a DataTable
        //        DataTable table = new DataTable();

        //        //Add columns to table
        //        table.Columns.Add("No");
        //        table.Columns.Add("NamaSKPD");
        //        table.Columns.Add("JumalhTotalSPM");
        //        table.Columns.Add("JumlahBelanjaSPM");
        //        table.Columns.Add("JumalhTotalSP2D");
        //        table.Columns.Add("JumlahBelanjaSP2D");
        //        table.Columns.Add("JumlahPotongan");
        //        table.Columns.Add("Keterangan");
                

        //        int columnCount = table.Columns.Count;
        //        List<object> data = new List<object>();


        //        decimal akumulasi = 0L;
        //        decimal sisa = 0;

        //        for (int idx = 0; idx < lst.Count; idx++)
        //        {
        //            table.Rows.Add(new string[]
        //            {

                           
        //               DataFormat.GetString(gridRekening.Rows[idx].Cells[5].Value),
        //               DataFormat.GetString(gridRekening.Rows[idx].Cells[6].Value),
        //               DataFormat.GetString(gridRekening.Rows[idx].Cells[7].Value),

        //            });
        //            if (DataFormat.GetLong(gridRekening.Rows[idx].Cells[4].Value) == 0)
        //            {
        //                lstBarisProgram.Add(idx);

        //            }
        //        }

        //        pdfGrid.DataSource = table; //data
        //        pdfGrid.Columns[0].Width = 100;
        //        //pdfGrid.Columns[1].Width = 20;
        //        //// Angka 
        //        //pdfGrid.Columns[2].Width = 50;
        //        pdfGrid.Columns[1].Width = 340;

        //        pdfGrid.Columns[2].Width = 75;


        //        PdfGridStyle gridStyle = new PdfGridStyle();
        //        //Adding cell padding
        //        gridStyle.CellPadding = new PdfPaddings(5, 5,2, 2);
        //        //gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

        //        pdfGrid.Style = gridStyle;


        //        PdfStringFormat formatKolomAngka = new PdfStringFormat();
        //        formatKolomAngka.Alignment = PdfTextAlignment.Right;

        //        pdfGrid.Columns[2].Format = formatKolomAngka;

        //        PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));

        //        PdfGridCellStyle cellStyle = new PdfGridCellStyle();
        //        PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();

        //        pdfGrid.RepeatHeader = true;
        //        PdfStringFormat stringFormatHeader = new PdfStringFormat();
        //        stringFormatHeader.Alignment = PdfTextAlignment.Center;
        //        stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;

        //        font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", font.Size, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));

        //        cellHeaderStyle.Font = font;

        //        cellHeaderStyle.StringFormat = stringFormatHeader;
        //        for (int c = 0; c < pdfGrid.Headers.Count; c++)
        //        {
        //            pdfGrid.Headers[c].ApplyStyle(cellHeaderStyle);
        //            pdfGrid.Headers[c].Height = 30;

        //        }


        //        for (int idx = 0; idx < pdfGrid.Rows.Count; idx++)
        //        {
        //            pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 7,
        //                FontStyle.Regular)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);

        //            for (int c = 0; c < pdfGrid.Columns.Count; c++)
        //            {


        //                pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.01F;
        //                pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.01F;
        //                pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.01F;
        //                pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.01F;

        //            }
                   
        //        }

        //        //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

        //        PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos));
          


           

        //        PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
        //        yPos = PosisiTerakhir + 5;

        //        float setengah = (previousPage.GetClientSize().Width / 2) - 20;
        //        float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;

               

        //        page = document.Pages.Add();

        //        using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPJUP.pdf"), FileMode.Create, FileAccess.ReadWrite))
        //        {
        //            //Save the PDF document to file stream.
        //            document.Save(outputFileStream);

        //        }

        //        //Close the document.
        //        document.Close(true);
        //        pdfViewer pV = new pdfViewer();
        //        pV.Document = Path.GetFullPath(@"../../../SPJUP.pdf");
        //        pV.Show();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        }
    }
}
