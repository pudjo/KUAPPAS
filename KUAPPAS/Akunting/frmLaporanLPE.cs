using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formatting;
using BP.Akuntansi;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

using System.IO;
using DTO;
using BP;

namespace KUAPPAS.Akunting
{
    public partial class frmLaporanLPE : ChildForm
    {
        private int m_iSKPD;
        private bool m_bEliminsiRK;
        CetakPDF oCetakPDF;
        PdfPage previousPage;
        int m_iJenisBendahara;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        public frmLaporanLPE()
        {
            InitializeComponent();
            m_iSKPD = 0;
            m_bEliminsiRK= false;
        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }

        private void frmLaporanLPE_Load(object sender, EventArgs e)
        {
            gridLPE.FormatHeader();
            ctrlHeader1.SetCaption("Laporan Perubahan Equitas");
            this.Text="Laporan Perubahan Equitas";
            ctrlDinas1.Create();
            gridLPE.Columns[2].HeaderText = GlobalVar.TahunAnggaran.ToString();
            gridLPE.Columns[3].HeaderText = (GlobalVar.TahunAnggaran-1).ToString();
            ctrlTanggalBulanVertikal1.TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
            ctrlTanggalBulanVertikal1.TanggalAkhir = new DateTime(GlobalVar.TahunAnggaran,  12,31);
            
            if (GlobalVar.Pengguna.IsUserDinas ==0 )
            {
                chkSemuaDinas.Checked= true ;
            } else {

                chkSemuaDinas.Checked = false ;
                chkSemuaDinas.Enabled = false;

            }
            

        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            int i ;//Dim i As Integer
            decimal cEquitas;//Dim cEquitas As Currency
            LapLPELogic oLogic = new LapLPELogic((int)GlobalVar.TahunAnggaran);
            bool semuadinas = chkSemuaDinas.Checked;
            DateTime tanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);// ctrlTanggalBulanVertikal1.TanggalAwal;
            DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
            int ppkd = chkPPKD.Checked ? 1 : 0;
            int dinas = ctrlDinas1.GetID();
    decimal cEkuitasTahunBerjalan ;//Dim cEkuitasTahunBerjalan As Currency
    decimal cSurplusDefisit;//Dim cSurplusDefisit As Currency
    decimal cSurplusDefisitLalu;//Dim cSurplusDefisitLalu As Currency
    decimal cRKPPKDLalu;//Dim cRKPPKDLalu As Currency
    decimal cLainLainLalu;// Dim cLainLainLalu As Currency
    decimal cLainLain;//Dim cLainLain As Currency


    gridLPE.Rows.Clear();
    decimal cRKPPKD;//Dim cRKPPKD As Currency
    decimal cSaldo ;//Dim cSaldo  As Currency
    decimal cSD;//Dim cSD As Currency
    cEquitas = GetEkuitas();
    cSD = oLogic.GetValue("31102",semuadinas,dinas);
    decimal cSaldoLalu ;//As Currency
    cEkuitasTahunBerjalan = oLogic.GetEkuitasEx((int)GlobalVar.TahunAnggaran, ppkd, semuadinas,dinas, tanggalAwal, tanggalAkhir);
    cSurplusDefisit = oLogic.GetSurplusDefisit((int)GlobalVar.TahunAnggaran, semuadinas, dinas, ppkd, tanggalAwal, tanggalAkhir);
   // If m_bEliminsiRK = False Then
    
    if( m_bEliminsiRK == false ){
        cRKPPKD = oLogic.GetRKPPKD(semuadinas,dinas,GlobalVar.TahunAnggaran, tanggalAwal, tanggalAkhir);
    } else{
        cRKPPKD = 0;
    }
    //i =i++; AddRow
    //gridLPE.TextMatrix(i, 0) = i
    //gridLPE.TextMatrix(i, 1) = "Ekuitas Awal","0.00",

    cSaldo = 0;//'cEkuitasTahunBerjalan
    
    cSaldoLalu = oLogic.GetSaldoAwalLPELalu(semuadinas,dinas);

    //gridLPE.TextMatrix(i, 2) = fMoney(cSaldo)
    //gridLPE.TextMatrix(i, 3) = fMoney(cSaldoLalu)
    //        string[] rowx={i.ToString (),"Ekuitas Awal",cSaldo.ToRupiahInReport(),}
    i = 0;
    string[] row = { i.ToString(), "Ekuitas Awal", "", cSaldoLalu.ToRupiahInReport() };
    int barisSaldoAwal = 0;
    gridLPE.Rows.Add (row);

            //i = AddRow
    
    //Dim cLalu As Currency
    decimal cLalu=0;
        //    310101010002
        //If g_nTahun <= 2020 Then
        if (GlobalVar.TahunAnggaran<=2020)
            cLalu =oLogic. GetFromNeracaAwal("3110201",semuadinas,dinas); 
        else{
            cLalu = oLogic.GetFromNeracaAwal("310102010001", semuadinas, dinas);
        }
        

        
    //    gridLPE.TextMatrix(i, 3) = fMoney(cLalu)
        
        cSaldo = cSaldo + cSurplusDefisit;
        cSaldoLalu = cSaldoLalu + cLalu;
    i++;
    string[] row2 = { i.ToString(), "Surplus/Defisit LO", cSurplusDefisit.ToRupiahInReport(), cLalu.ToRupiahInReport() };
    gridLPE.Rows.Add(row2);
    
    
            if (m_bEliminsiRK == false)
            {
                cRKPPKD = oLogic.GetRKPPKD(semuadinas, dinas, GlobalVar.TahunAnggaran, tanggalAwal, tanggalAkhir);
            }
            else
            {
                cRKPPKD = 0;
            } 
                
            if (GlobalVar.TahunAnggaran<2020){
                cLalu = oLogic.GetFromNeracaAwal("3130101", semuadinas, dinas);
            } else {
                cLalu = oLogic.GetFromNeracaAwal("310301010001", semuadinas, dinas);
    
            }
            //310101010002
            cSaldoLalu = cSaldoLalu + cLalu;
            if (chkSemuaDinas.Checked == false)
            {
                i++;
                string[] row3 = { i.ToString(), "RK PPKD", cRKPPKD.ToRupiahInReport(), cLalu.ToRupiahInReport() };
                gridLPE.Rows.Add(row3);
            }
            else
            {
                cRKPPKD=0;
            }

            cSaldo = cSaldo + cRKPPKD;

            i++;
      string[] row4 = { i.ToString(), "Dampak kumulatif perubahan kebijakan/kesalahan mendasar:","",""};
    gridLPE.Rows.Add(row4);
    //gridLPE.TextMatrix(i, 1) = "Lain-lain:"
    cLainLainLalu = oLogic.GetFromNeracaAwal("310101010002", semuadinas, dinas);
    cSaldoLalu = cSaldoLalu + cLainLainLalu;
    cLainLain = oLogic.GetLainLain(GlobalVar.TahunAnggaran, semuadinas, dinas, tanggalAwal,tanggalAkhir,ppkd);
    
    //gridLPE.TextMatrix(i, 3) = fMoney(cLainLainLalu)
    //gridLPE.TextMatrix(i, 2) = fMoney(cLainLain)
   
              string[] row5 = { i.ToString(), "Lain-lain:", cLainLain.ToRupiahInReport(), cLainLainLalu.ToRupiahInReport() };
    gridLPE.Rows.Add(row5);
        
        
    
    
    //i = AddRow
    //gridLPE.TextMatrix(i, 0) = i - 3
    //gridLPE.TextMatrix(i, 1) = "Ekuitas Akhir"
    
    cSaldo = cSaldo + cSaldoLalu + cLainLain;
    
    
    //gridLPE.TextMatrix(i, 2) = fMoney(cSaldo)
    
    //gridLPE.TextMatrix(1, 2) = fMoney(cSaldoLalu)
    
    //gridLPE.TextMatrix(i, 3) = fMoney(cSaldoLalu)
              string[] row7 = { i.ToString(), "Ekuitas Akhir", cSaldo.ToRupiahInReport(), cSaldoLalu.ToRupiahInReport() };
    gridLPE.Rows.Add(row7);
    gridLPE.Rows[barisSaldoAwal].Cells[2].Value = cSaldoLalu.ToRupiahInReport();
        }
        private decimal GetEkuitas(){
            try{
   
    
                decimal e=0;
                return e;
            } catch(Exception ex){
                MessageBox.Show (ex.Message);
                return 0;
            }
        }

        //
       
        
       

        
       
        private decimal GetSaldoAwalLPELalu(){
             try{

                decimal e=0;
                return e;
            } catch(Exception ex){
                MessageBox.Show (ex.Message);
                return 0;
            }
        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                PdfSection section = document.Sections.Add();
                document.PageSettings.Margins.Left = 8;
                document.PageSettings.Margins.Top = 5;
                document.PageSettings.Margins.Right = 2;
                document.PageSettings.Margins.Bottom = 8;
                section.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section.PageSettings.Height = 935;// = PdfPageSize.Legal;
                section.PageSettings.Orientation = PdfPageOrientation.Portrait;

                float yPos = 0;
                SaatnyacetakKesimpulan = false;
                PdfPage page = section.Pages.Add();

                PdfGraphics graphics = page.Graphics;
                previousPage = page;
           

                yPos = 10;
                PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);


                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;

                //stringFormat.CharacterSpacing = 2f;
                oCetakPDF = new CetakPDF();
                //SizeF size = font12.MeasureString("xxx");


                Pejabat pimpinan = new Pejabat();
                Pejabat bendahara = new Pejabat();
                //pimpinan = ctrlDinas1.GetPimpinan(mTanggalAkhir);
                //bendahara = ctrlDinas1.GetBendaharaPengeluaran(mTanggalAkhir);

                //if (pimpinan == null)
                //{
                //    MessageBox.Show("Data Pimpinan belum di setting di master pejabat");
                //    return ;

                //}

                //if (bendahara == null)
                //{
                //    MessageBox.Show("Data Bndahara belum di setting di master pejabat");
                //    return ;

                //}


                float kiri = 15;
                float posTitikdua = 150;
                float posNama = 155;

                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                int id = ctrlDinas1.GetID();

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "LAPORAN PERUBAHAN EQUITAS", 10, kiri, yPos,
                page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "UNTUK TAHUN YANG BERAKHIR SAMPAI DENGAN " + ctrlTanggalBulanVertikal1.TanggalAkhir.ToTanggalIndonesia(), 10, kiri, yPos,
                page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = yPos + 20;
       



                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = yPos + 20;
                if (m_iSKPD > 0)
                {
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD "
                              , 10, kiri, yPos,
                              page.GetClientSize().Width, stringFormat, false, false, true);
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                              page.GetClientSize().Width, stringFormat, false, false, true);
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                              ctrlDinas1.GetNamaSKPD(), 10, 155, yPos,
                              page.GetClientSize().Width, stringFormat, true, false, true);
                }


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Periode "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          ctrlTanggalBulanVertikal1.Waktu, 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Tahun Anggaran"
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          GlobalVar.TahunAnggaran.ToString(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);
                #region gridKas
                PdfGrid pdfGrid = new PdfGrid();

                int count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("Uraian");
                table.Columns.Add(GlobalVar.TahunAnggaran.ToString());
                table.Columns.Add((GlobalVar.TahunAnggaran-1).ToString());


                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();


                decimal akumulasi = 0L;
                decimal sisa = 0;
                string kode = "";

                for (int idx = 0; idx < gridLPE.Rows.Count; idx++)
                {
               
                    table.Rows.Add(new string[]
                    {
            
                       DataFormat.GetString(gridLPE.Rows[idx].Cells[1].Value).ReplaceUnicode(),                      
                       DataFormat.GetString(gridLPE.Rows[idx].Cells[2].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridLPE.Rows[idx].Cells[3].Value).ReplaceUnicode(),
              
                  
                    });
                }

                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 280;
                pdfGrid.Columns[1].Width = 100;
                pdfGrid.Columns[2].Width = 100;
                
                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, 4, 4);
                //gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                PdfStringFormat formatKolomTengah = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGrid.Columns[1].Format = formatKolomAngka;
                pdfGrid.Columns[2].Format = formatKolomAngka;
                









                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();

                pdfGrid.RepeatHeader = true;
                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;

                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", font.Size, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));

                cellHeaderStyle.Font = font;

                cellHeaderStyle.StringFormat = stringFormatHeader;
                for (int c = 0; c < pdfGrid.Headers.Count; c++)
                {
                    pdfGrid.Headers[c].ApplyStyle(cellHeaderStyle);
                    pdfGrid.Headers[c].Height = 30;

                }


                for (int idx = 0; idx < pdfGrid.Rows.Count; idx++)
                {
                    pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 7,
                        FontStyle.Regular)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);

                    

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos));
                #endregion gridKas
                //  pdfGridLayoutResult = pdfGridRingkasan.Draw(pdfGridLayoutResult.Page, new PointF(kiri, pdfGridLayoutResult.Bounds.Bottom));

                yPos = pdfGridLayoutResult.Bounds.Bottom + 10;

                PejabatLogic oLogicPejabat = new PejabatLogic(GlobalVar.TahunAnggaran);

                Pejabat oKada = new Pejabat();

                DateTime d = DateTime.Now;
                if (chkSemuaDinas.Checked == true)
                {
                    oKada = oLogicPejabat.GetKepalaDaerah();
                }
                else
                {
                    ctrlDinas1.SetID(id);
                    oKada = ctrlDinas1.GetPimpinan(d);
                }

                float setengah = page.GetClientSize().Width / 2;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                           "Ketapang, "+ ctrlTanggal1.TextTanggalLengkap, 9, kiri + setengah, yPos,
                           setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          oKada.Jabatan, 9, kiri + setengah, yPos,
                          setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                         oKada.Nama, 9, kiri + setengah, yPos,
                         setengah, stringFormat, true, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                         oKada.NIP, 9, kiri + setengah, yPos,
                         setengah, stringFormat, true, false, true);





                //PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                //SaatnyacetakKesimpulan = true;
                //page = document.Pages.Add();

                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../LPE.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../LPE.pdf");
                pV.Show();


                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void ctrlTanggalBulanVertikal1_Load(object sender, EventArgs e)
        {

        }

        private void cmdExcell_Click(object sender, EventArgs e)
        {


        
            try
            {

                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                Microsoft.Office.Interop.Excel.Range excelCellrange;
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // Make Excel invisible and disable alerts.
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                if (chkSemuaDinas.Checked == true)
                {
                    excelSheet.Name = "LPS" + ctrlDinas1.GetNamaSKPD();
                }
                else
                {
                    excelSheet.Name = "LPE";
                }

                // header
                for (int i = 1; i < gridLPE.Columns.Count + 1; i++)
                {
                    excelSheet.Cells[1, i] = gridLPE.Columns[i - 1].HeaderText;
                }

                for (int row = 0; row < gridLPE.Rows.Count; row++)
                {
                    //for (int col = 0; col < gridLRATrx.Columns.Count; col++)
                    for (int col = 0; col < 4; col++)
                    {
                        if (col > 1)
                        {

                            string s = DataFormat.GetString(gridLPE.Rows[row].Cells[col].Value);
                            if (s == "")
                            {
                                s = "0";
                            }
                            excelSheet.Cells[row + 2, col + 1] = DataFormat.FormatUangReportKeDecimal(s);


                        }
                        else
                        {
                            excelSheet.Cells[row + 2, col + 1] = DataFormat.GetString(gridLPE.Rows[row].Cells[col].Value);
                        }


                    }
                }


                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];
                //excelCellrange.EntireColumn.AutoFit();
                //excelSheet.Range (“G:G”).NumberFormat = “0.00”;
                //Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                //border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //border.Weight = 2d;
                string namaFile = BuatFile();
                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Masih Kosong ");
                    return;
                }

                excelworkBook.SaveAs(namaFile);
                MessageBox.Show("File sudah disimpan di " + namaFile);


                excelworkBook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke excell" + ex.Message);
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




            //if (fdlg.FileName != "")
            //{
            // Saves the Image via a FileStream created by the OpenFile method.  

            sRet = fdlg.FileName;


            //  }
            return sRet;
        }

        private void gridLPE_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
