using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP;
using BP.Bendahara;
using DTO;
using DTO.Bendahara;
using Formatting;
using Excel = Microsoft.Office.Interop.Excel;
using DTO.Anggaran;
using Syncfusion.Pdf;
using BP.Anggaran;

using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System.IO;
using System.Diagnostics;
namespace KUAPPAS.Bendahara
{
    public partial class frmPenerimaanPenerimaandanPenyetoran : ChildForm
    {
        int m_IDSKPD;
        private List<ProgramKegiatanAnggaran> m_lstProgramKegiatan;
        private List<KartuKendali> mKartuKendaliList;

        private int mcolAnggaran;
        private const int COL_ANGGARANMURNI = 2;
        private const int COL_ANGGARANGESER = 3;
        private const int COL_ANGGARANRKAP = 4;
        private const int COL_ANGGARANABT = 5;
        private const int COL_AKUMULASI = 6;

        private const int COL_SPJINI = 7;
        private const int COL_SISA = 8;

        private const int COL_LEVEL = 9;
        private const int COL_IDURUSAN = 10;
        private const int COL_IDPROGRAM = 11;
        private const int COL_IDKEGIATAN = 12;
        private const int COL_IDSubKegiatan = 13;
        private const int COL_IDREKENING = 14;
        private const int COL_IDDINAS = 0;
        private const int COL_KODEUK = 15;

        private const int COL_SP2DUP = 17;
        private const int COL_SP2DGU = 18;
        private const int COL_SP2DTU = 19;
        private const int COL_SP2DLS = 20;
        private const int COL_JUMLAHSP2D = 21;

        private const int COL_RUPGU = 22;
        private const int COL_RUPTU = 23;
        private const int COL_RUPLS = 24;

        private const int COL_JUMLAHREALISASI = 25;
        private const int COL_SISAKAS = 26;
        private const int COL_SISAPAGUKEGIATAN = 27;
        private const int COL_SISAPAGUBELANJA = 28;



        private const int LEVEL_DINAS = 0;
        private const int LEVEL_UNIT = 1;
        private const int LEVEL_URUSAN = 2;
        private const int LEVEL_PROGRAM = 3;
        private const int LEVEL_KEGIATAN = 4;
        private const int LEVEL_SUBKEGIATAN = 5;
        private const int LEVEL_REKANING = 6;




        private decimal m_cJumlahDPA;
        private decimal m_cJumlahSPJSebelum;
        private decimal m_cJumlahSISADANASEBELUM;
        private decimal m_cJumlahSPJ;
        private decimal m_cJumlahSISADPA;
        private decimal m_cJumlahTotalSPJ;

      

        //public delegate void GetKegiatan();
        //public GetKegiatan getKegiatan;
        private int prevJenis = 0;
        private int preDinas = 0;


        private Single m_iStatus;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        PdfPage previousPage;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        CetakPDF m_oCetakPDF;
        int halaman;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;
        List<PenerimaanPenerimaanPenyetoran> mListPenerimaanPenyetoran = new List<PenerimaanPenerimaanPenyetoran>();

        public frmPenerimaanPenerimaandanPenyetoran()
        {
            InitializeComponent();
        }

        private void frmPenerimaanPenerimaandanPenyetoran_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Laporan Penerimaan Peyetoran Bendahara Penerimaan");
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            gridPenerimaanPenyetoran.FormatHeader();
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlSKPD1.SetID(GlobalVar.Pengguna.SKPD);
                m_IDSKPD= GlobalVar.Pengguna.SKPD;
            }
            
        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
            try
            {//..
                gridPenerimaanPenyetoran.Rows.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadData()
        {
            try
            {
                if (LoadPenerimaanPenyetoran())
                {
                    if (DisplayPenerimaanPenyetoran() == true)
                    {
                        decimal penerimaan = 0;
                        decimal penyetoran = 0;
                        decimal penerimaanLangsung = 0;
                        
                    //    if (p.Tanggal >= mTanggalAwal)
                    //{
                        penerimaan = mListPenerimaanPenyetoran.Where(p => p.Debet == 1 && p.Tanggal>=mTanggalAwal).Sum(j => j.Jumlah);
                        txtPenerimaan.Text = penerimaan.ToRupiahInReport();

                        penyetoran = mListPenerimaanPenyetoran.Where(p => p.Debet == -1 && p.Tanggal >= mTanggalAwal).Sum(j => j.Jumlah);
                        penerimaanLangsung = mListPenerimaanPenyetoran.Where(p => p.Debet == 1 && p.Jenis == 1 && p.Tanggal >= mTanggalAwal ).Sum(j => j.Jumlah);
                        txtPenyetoran.Text = (penyetoran+penerimaanLangsung).ToRupiahInReport();
                        txtSaldo.Text = (penerimaan - penerimaanLangsung - penyetoran).ToRupiahInReport(); 

                    }
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private  bool  LoadPenerimaanPenyetoran()
        {
            mListPenerimaanPenyetoran = new List<PenerimaanPenerimaanPenyetoran>();
            try
            {
               
                PenerimaanDanPenyetoranLogic oLogic = new PenerimaanDanPenyetoranLogic(GlobalVar.TahunAnggaran);
                mListPenerimaanPenyetoran = oLogic.GetPenerimaanPenyetoran(m_IDSKPD, ctrlTanggalBulanVertikal1.TanggalAkhir);
                if (mListPenerimaanPenyetoran == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return false;
                }
                return true ;
            }
            catch (Exception ex)
            {
            MessageBox.Show("Keslahan mengambil data." + ex.Message);
                return false;

            }
            
        }
        private bool DisplayPenerimaanPenyetoran(){
            List<PenerimaanPenerimaanPenyetoran> lstSudahDisplay = new List<PenerimaanPenerimaanPenyetoran>();
            int i = 0;
            mTanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
            try
            {
                foreach (PenerimaanPenerimaanPenyetoran p in mListPenerimaanPenyetoran)
                {
                    if (p.Tanggal >= mTanggalAwal)
                    {
                        if (p.Jenis == 0 && p.Debet == 1)
                        {
                            PenerimaanPenerimaanPenyetoran pSetorUntukSTSIni = mListPenerimaanPenyetoran.FirstOrDefault(x => x.NoUrut == p.NoUrutSetor);
                            if (pSetorUntukSTSIni != null)
                            {


                                string[] strPenerimaan = { (++i).ToString(),p.Tanggal.ToTanggalIndonesia(), p.NoBukti, "Tunai", p.IDRekening.ToKodeRekening(),p.NamaRekening, p.Jumlah.ToRupiahInReport(), 
                                                         pSetorUntukSTSIni.Tanggal.ToTanggalIndonesia(), pSetorUntukSTSIni.NoBukti, 
                                                         pSetorUntukSTSIni.Jumlah.ToRupiahInReport(), p.Keterangan };
                                lstSudahDisplay.Add(pSetorUntukSTSIni);
                                gridPenerimaanPenyetoran.Rows.Add(strPenerimaan);

                            }
                            else
                            {

                                string[] strPenerimaan = { (++i).ToString(), p.Tanggal.ToTanggalIndonesia(), p.NoBukti, "Tunai", p.IDRekening.ToKodeRekening(), p.NamaRekening, p.Jumlah.ToRupiahInReport(), "", "", "", p.Keterangan };
                                gridPenerimaanPenyetoran.Rows.Add(strPenerimaan);

                            }
                        }
                        if (p.Jenis == 1 && p.Debet == 1)
                        {
                            string[] strPenerimaan2 = { (++i).ToString(), p.Tanggal.ToTanggalIndonesia(), p.NoBukti, "Langsung", p.IDRekening.ToKodeRekening(), p.NamaRekening, p.Jumlah.ToRupiahInReport(), p.Tanggal.ToTanggalIndonesia(), p.NoBukti, p.Jumlah.ToRupiahInReport(), p.Keterangan };

                            gridPenerimaanPenyetoran.Rows.Add(strPenerimaan2);
                        }
                        if (p.Debet == -1)
                        {
                            bool bFound = false;
                            foreach (PenerimaanPenerimaanPenyetoran pSudahdiTampilkan in lstSudahDisplay)
                            {
                                if (pSudahdiTampilkan.ApakahSama(p))
                                {
                                    bFound = true || bFound;
                                }


                            }
                            if (bFound == false)
                            {

                                string[] strPenerimaan3 = { (++i).ToString(), "", "", "", "", "", "", p.Tanggal.ToTanggalIndonesia(), p.NoBukti, p.Jumlah.ToRupiahInReport(), p.Keterangan };

                                gridPenerimaanPenyetoran.Rows.Add(strPenerimaan3);
                            }
                        }
                    }
                }
              
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Keslahan mengambil data." + ex.Message);
                return false;

            }

        }

        private void ctrlSKPD1_OnChanged(int pID)
        {
            m_IDSKPD = pID;
        }
         private void Pages_PageAdded(object sender, PageAddedEventArgs args)
      
        {
            float yPos = PosisiTerakhir + 5;

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;
            PdfStringFormat stringFormat = new PdfStringFormat();

       
            CetakPDF oCetakPDF = new CetakPDF();
           

            if (SaatnyacetakKesimpulan == true)
            {

                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Jumlah Penerimaan ", 8, 30, yPos, 240, stringFormat, false, false);

                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,txtPenerimaan.Text, 8, 180, yPos, 85, stringFormat, false, false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 170, yPos, 5, stringFormat, true, false);


                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Jumlah Penyetoran ", 8, 30, yPos, 240, stringFormat, false, false);
                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtPenyetoran.Text, 8, 180, yPos, 85, stringFormat, false, false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 170, yPos, 5, stringFormat, true, false);


                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Saldo Kas di Bandahara Penerimaan", 8, 30, yPos, 240, stringFormat, false, false);

                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtSaldo.Text, 8, 180, yPos, 85, stringFormat, false, false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 170, yPos, 5, stringFormat, true, false);

                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Terdidi atas:", 8, 30, yPos, 240, stringFormat, true ,true,true );

                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Tunai Sebesar", 8, 30, yPos, 240, stringFormat, false, false);
              
                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtTunai.Text, 8, 180, yPos, 85, stringFormat, false, false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 170, yPos, 5, stringFormat, true, false);

                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Bank Sebesar", 8, 30, yPos, 240, stringFormat, false, false);

                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtBank.Text, 8, 180, yPos, 85, stringFormat, false, false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 170, yPos, 5, stringFormat, true, false);

                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Lainnya", 8, 30, yPos, 240, stringFormat, false, false);
         
                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "0", 8, 180, yPos, 85, stringFormat, false, false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 170, yPos, 5, stringFormat, true, false);

                yPos = yPos + 20;
                
                 Pejabat bendahara = new Pejabat ();
                 Pejabat pimpinan = new Pejabat();

                 bendahara = ctrlSKPD1.GetBendaharaPenerimaan(dtCetak.Value);
                pimpinan = ctrlSKPD1.GetKuaaPenggunaAnggaranPenerimaan(dtCetak.Value);
                yPos = yPos + 10;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.gPemda.Ibukota + "," + ctrlTanggalBulanVertikal1.TanggalAkhir.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Jabatan, 10, 30, yPos, setengah, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Jabatan, 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Nama, 10, 30, yPos , setengah, stringFormat, false, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Nama, 10, posisiTengah, yPos , setengah, stringFormat, true, true, true);
                
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + pimpinan.NIP, 10, 30, yPos, setengah, stringFormat, false );
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + bendahara.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);
                               


                
            }



            previousPage = args.Page;


        }
        private void cmsCetak_Click(object sender, EventArgs e)
        {
         try{
                //Create a new PDF document.
                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                section1.PageSettings.Width = 612 ;// = PdfPageSize.Legal;
                section1.PageSettings.Height  = 935;// = PdfPageSize.Legal;

                section1.PageSettings.Orientation = PdfPageOrientation.Landscape;
                document.PageSettings.Margins.Bottom= 0;
                PdfPage page = section1.Pages.Add();
                previousPage = page;
                document.Pages.PageAdded += Pages_PageAdded;
                halaman = 1;
                SaatnyacetakKesimpulan = false;
              
                CetakPDF oCetakPDF = new CetakPDF();
                float yPos;
                float kiri = 20;

                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos, 
                    page.GetClientSize().Width , stringFormat, true, false, true);
 
              yPos = oCetakPDF.TulisItem(previousPage.Graphics, "LAPORAN PENERIMAAN DAN PENYETORAN "
                         , 10, kiri, yPos,
                         page.GetClientSize().Width, stringFormat, true, false, true);


           
                stringFormat.Alignment = PdfTextAlignment.Left;

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD " 
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":" , 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, 
                          ctrlSKPD1.GetNamaSKPD(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);
                Pejabat bendahara = ctrlSKPD1.GetBendaharaPenerimaan(ctrlTanggalBulanVertikal1.TanggalAkhir);
 
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Bendahara Penerimaan "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, 
                          bendahara.Nama, 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Periode "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, 
                          ctrlTanggalBulanVertikal1.Waktu , 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Tahun Anggaran"
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, 
                          GlobalVar.TahunAnggaran.ToString(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);


                PdfGrid pdfGridHeader = new PdfGrid();
                DataTable tableHeader = new DataTable();
                tableHeader.Columns.Add("0 ");
                tableHeader.Columns.Add("1");
                tableHeader.Columns.Add("2");
                tableHeader.Columns.Add("3");
                tableHeader.Columns.Add("4");
                tableHeader.Columns.Add("5");
                tableHeader.Columns.Add("6");
                tableHeader.Columns.Add("7");
                tableHeader.Columns.Add("8");
                tableHeader.Columns.Add("9");
                tableHeader.Columns.Add("10");

        

          
                //tableHeader.Columns.Add("13");
                
                 tableHeader.Rows.Add(new string[]
                    {            " No","PENERIMAAN","PENERIMAAN","PENERIMAAN","PENERIMAAN","PENERIMAAN",
                                "PENERIMAAN","PENYETORAN","PENYETORAN",
                                "PENYETORAN","KETERANGAN"});

                tableHeader.Rows.Add(new string[]
                    {            "No","TANGGAL","NO BUKTI","CARA","KODE REKENING",
                                "URAIAN","JUMLAH","TANGGAL","NO STS",
                                "JUMLAH","KETERANGAN"});

              

                    pdfGridHeader.DataSource = tableHeader; //data
                    pdfGridHeader.Columns[0].Width = 30;
                    pdfGridHeader.Columns[1].Width = 60;
                // Angka 
                    pdfGridHeader.Columns[2].Width = 60;

                    pdfGridHeader.Columns[3].Width = 40;
                    pdfGridHeader.Columns[4].Width = 70;
                    pdfGridHeader.Columns[5].Width = 70;

                    pdfGridHeader.Columns[6].Width = 60;
                    pdfGridHeader.Columns[7].Width = 80;
                    pdfGridHeader.Columns[8].Width = 80;

                    pdfGridHeader.Columns[9].Width = 70;
                    pdfGridHeader.Columns[10].Width = 200;
                    
                   pdfGridHeader.Rows[0].Cells[0].RowSpan = 2;
           
                pdfGridHeader.Rows[0].Cells[10].RowSpan = 2;
          
                pdfGridHeader.Rows[0].Cells[1].ColumnSpan = 6;
                pdfGridHeader.Rows[0].Cells[7].ColumnSpan = 3;

                
                PdfFont fontHeader = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));            
                PdfGridCellStyle cellHeaderStyle0 = new PdfGridCellStyle();
                               
                PdfStringFormat stringFormatHeader0 = new PdfStringFormat();                
                stringFormatHeader0.Alignment = PdfTextAlignment.Center;
                stringFormatHeader0.LineAlignment = PdfVerticalAlignment.Middle;

                fontHeader = new PdfTrueTypeFont(new System.Drawing.Font("Arial", fontHeader.Size, 
                FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));
                cellHeaderStyle0.Font = fontHeader;
                 cellHeaderStyle0.StringFormat = stringFormatHeader0;


                 for (int col = 0; col < pdfGridHeader.Columns.Count; col++)
                     pdfGridHeader.Columns[col].Format = stringFormatHeader0;
                 pdfGridHeader.Headers.Clear();

                PdfGridLayoutResult pdfHeaderGridResult = pdfGridHeader.Draw(page, new PointF(kiri, yPos));
                yPos = pdfHeaderGridResult.Bounds.Bottom;


#region spjAtas

                PdfGrid pdfGrid = new PdfGrid();
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("1");
                table.Columns.Add("2");
                table.Columns.Add("3");

                table.Columns.Add("4");
                table.Columns.Add("5");
                table.Columns.Add("6");
                table.Columns.Add("7");
                table.Columns.Add("8");
                table.Columns.Add("9");
                table.Columns.Add("10");
                table.Columns.Add("11");
                
                //table.Columns.Add("Level");



                //table. Columns[0]
                //Assign Column count
                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();


                decimal akumulasi = 0L;
                decimal sisa = 0;

                for (int idx = 0; idx <  gridPenerimaanPenyetoran .Rows.Count; idx++)
                {
                    if (gridPenerimaanPenyetoran.Rows[idx].Cells[0].Value != null)
                    {
                        
                    table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(gridPenerimaanPenyetoran.Rows[idx].Cells[0].Value),
                       DataFormat.GetString(gridPenerimaanPenyetoran.Rows[idx].Cells[1].Value),                      
                     

                       DataFormat.GetString(gridPenerimaanPenyetoran.Rows[idx].Cells[2].Value),
                       DataFormat.GetString(gridPenerimaanPenyetoran.Rows[idx].Cells[3].Value),
                       DataFormat.GetString(gridPenerimaanPenyetoran.Rows[idx].Cells[4].Value),
                       DataFormat.GetString(gridPenerimaanPenyetoran.Rows[idx].Cells[5].Value),
                       DataFormat.GetString(gridPenerimaanPenyetoran.Rows[idx].Cells[6].Value),
                       DataFormat.GetString(gridPenerimaanPenyetoran.Rows[idx].Cells[7].Value),
                       DataFormat.GetString(gridPenerimaanPenyetoran.Rows[idx].Cells[8].Value),
                       DataFormat.GetString(gridPenerimaanPenyetoran.Rows[idx].Cells[9].Value),
                       DataFormat.GetString(gridPenerimaanPenyetoran.Rows[idx].Cells[10].Value),
                  
                
                       

                       
                        
                    });
                    }


                }
              
                pdfGrid.DataSource = table; //data
              





                    pdfGrid.Columns[0].Width = 30;
                    pdfGrid.Columns[1].Width = 60;
                    // Angka 
                    pdfGrid.Columns[2].Width = 60;

                    pdfGrid.Columns[3].Width = 40;
                    pdfGrid.Columns[4].Width = 70;
                    pdfGrid.Columns[5].Width = 70;

                    pdfGrid.Columns[6].Width = 60;
                    pdfGrid.Columns[7].Width = 80;
                    pdfGrid.Columns[8].Width = 80;

                    pdfGrid.Columns[9].Width = 70;
                    pdfGrid.Columns[10].Width = 200;

         

           

            
  
                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding

                gridStyle.CellPadding = new PdfPaddings(3,1, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                pdfGrid.Style = gridStyle;
                 PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                for (int col = 2; col < pdfGrid.Columns.Count; col++)
                {
                    if (col == 6 || col ==9)
                        pdfGrid.Columns[col].Format = formatKolomAngka;
                }

                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));            
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
                    pdfGrid.Headers[c].Height = 20;

                }


                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                cellStyle.Borders.All = new PdfPen(new PdfColor(192, 192, 217), 0.50f);
                cellStyle.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 6, FontStyle.Regular)); 
                for (int idx = 0; idx < pdfGrid.Rows.Count;idx++ )
                {
                    pdfGrid.Rows[idx].Style = cellStyle;
                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.1F;
                    }


                    //    pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 6, FontStyle.Bold)); 
                

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(pdfHeaderGridResult.Page , new PointF(kiri, yPos));
                yPos = pdfGridLayoutResult.Bounds.Bottom;
             
        #endregion spjAtas

                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();
          
                //System.Diagnostics.Process.Start(namaFile);


                //string namaFile = Path.GetFullPath(@"../../../SPD_" + txtINO.Text.Trim() + "_" + ctrlSKPD1.GetNamaSKPD() + ".pdf");
                string namaFile = Path.GetFullPath(@"../../../Administratif.pdf");

                //using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPD.pdf"), FileMode.Create, FileAccess.ReadWrite))
                using (FileStream outputFileStream = new FileStream(namaFile, FileMode.Create, FileAccess.ReadWrite))

                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);

              
                    pdfViewer pV = new pdfViewer();
                    pV.Document = namaFile;// Path.GetFullPath(@"../../../BKU.pdf");
                    pV.Show();


             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
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
        private void cmdExcell_Click(object sender, EventArgs e)
        {
            string NamaFile = "";
            try
            {
                
                    Microsoft.Office.Interop.Excel.Application excel;
                    Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                    Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                    Microsoft.Office.Interop.Excel.Range excelCellrange;
                    // Start Excel and get Application object.
                    excel = new Microsoft.Office.Interop.Excel.Application();

                string namaFile = BuatFile();
                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Masih Kosong ");
                    return;
                }

                KillSpecificExcelFileProcess(namaFile);

                    // Make Excel invisible and disable alerts.
                    excel.Visible = false;
                    excel.DisplayAlerts = false;

                    // Create a new Workbook.
                    excelworkBook = excel.Workbooks.Add(Type.Missing);

                    // Create a Worksheet.
                    excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;

                    excelSheet.Name = "PenerimaanDanPenyetoran";

                    for (int i = 1; i < gridPenerimaanPenyetoran.Columns.Count + 1; i++)
                    {
                        excelSheet.Cells[1, i] = gridPenerimaanPenyetoran.Columns[i - 1].HeaderText;
                    }




                    for (int i = 0; i < gridPenerimaanPenyetoran.Rows.Count - 1; i++)
                    {

                        for (int j = 0; j < gridPenerimaanPenyetoran.Columns.Count - 1; j++)
                        {
                            if (gridPenerimaanPenyetoran.Rows[i].Cells[j].Value != null)
                            {
                                
                                    excelSheet.Cells[i + 2, j + 1] = DataFormat.GetString(gridPenerimaanPenyetoran.Rows[i].Cells[j].Value).Trim();
                                

                            }
                        }
                    }



                    // now we resize the columns
                    excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];

                    excelworkBook.SaveAs(namaFile);
                    MessageBox.Show("File sudah disimpan di " + namaFile);


                    excelworkBook.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal export ke excell" + ex.Message);
                }

           }
          
        
        }
    
}
/*
-- ================================================
-- Template generated from Template Explorer using:
-- Create Inline Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION dbo.fnPenerimaanPenyetoran 
(	
	@pidDinas int ,
	@pTanggal DateTime
)
RETURNS TABLE 
AS
RETURN 
(
	Select tSTS.IDDInas, 1 as Debet,tSTS.dtBukuKas, tSTS.sNoSTS as NoBukti, tSTS.btJenis, tSTSRekening.iIDRekening, tSTSRekening.cJumlah,
	tSTS.sKeterangan from tSTS Inner join tSTSRekening ON tSTS.inourutkasda = tSTSRekening.inourut 
	 WHERE tSTS.IDDInas=@pidDinas and tSTS.dtBukuKas<=@pTanggal
	 UNION 
	
	Select tSetor.IDDInas, -1 as Debet, tSetor.dtBukuKas, tSetor.sNoBukti as NoBukti, tSetor.btJenis, tSetorRekening.iIDRekening, tSetorRekening.cJumlah,
	tSetor.sKeterangan from tSetor Inner join tSetorRekening ON tSetor.inourut = tSetorRekening.inourut 
	 WHERE tSetor.IDDInas=@pidDinas and tSetor.dtBukuKas<=@pTanggal 
)
GO

*/