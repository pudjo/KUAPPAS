using BP.Bendahara;
using DTO;
using DTO.Bendahara;
using DTO.Laporan;
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
using BP;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf;
using System.IO;
using System.Diagnostics;
namespace KUAPPAS.Bendahara
{
    public partial class frmSPJFungsionalPenerimaan : ChildForm
    {
        private int m_IDinas;
        private DateTime mTanggal;
        private int mBulan;
        private List<Rekening> m_lstRekening;
        private List<SPJPenerimaan> mlistSPJPenerimaan;
        private List<TAnggaranRekening> mListAnggaran;
        private const int COL_KODE = 0;
        private const int COL_NAMA = 1;
        private const int COL_ANGGARANMURNI = 2;
        private const int COL_ANGGARANGESER = 3;
        private const int COL_ANGGARANRKAP = 4;
        private const int COL_ANGGARANABT = 5;
        private const int COL_PENERIMAANLALU = 6;
        private const int COL_PENYETORANLALU = 7;
        private const int COL_SISALALU = 8;
        private const int COL_PENERIMAANKINI = 9;
        private const int COL_PENYETORANKINI = 10;
        private const int COL_SISAKINI = 11;
        private const int COL_PENERIMAAN = 12;
        private const int COL_PENYETORAN = 13;
        private const int COL_SISA = 14;

        PdfPage previousPage;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        CetakPDF m_oCetakPDF;
        int halaman;
        int mcolAnggaran;
        public frmSPJFungsionalPenerimaan()
        {
            InitializeComponent();
            mBulan = 1;
        }

        private void frmSPJFungsionalPenerimaan_Load(object sender, EventArgs e)
        {
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            ctrlBulan1.Create();
            ctrlHeader1.SetCaption("SPJ Fungsional Penerimaan");
            gridSPJPenerimaan.FormatHeader();
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlSKPD1.SetID(GlobalVar.Pengguna.SKPD);
                m_IDinas = GlobalVar.Pengguna.SKPD;
            }
        }

        private void cmdLoadData_Click(object sender, EventArgs e)
        {
            if (ctrlBulan1.GetID() == 0)
            {
                MessageBox.Show("Belum memilih Bulan Laporan");
                return;
            
            }
            if (ctrlTahapAnggaran1.GetID() == 0)
            {
                MessageBox.Show("Belum memilih Tahapan Anggaran");
                return;
            }


            
            if (LoadData()== true ){
                DisplaySPJ();

                GetKolomAnggaran();
                RefreshSisa();
            }
        }
        private bool LoadRekening()
        {
            try
            {
                m_lstRekening = new List<Rekening>();
                RekeningLogic oRekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran);
                m_lstRekening = oRekeningLogic.Get().Where(r => r.ID >= 400000000000 && r.ID < 500000000000).ToList();

            
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }

        }
        private bool DisplayAnggaran()
        {
            //return true;
            gridSPJPenerimaan.Rows.Clear();
            var lstJumlah = mListAnggaran.GroupBy(x => x.IDRekening.ToString().Substring(0, 1))
              .Select(x => new
              {
                  IDRekening = x.Key,
                  JumlahMurni = x.Sum(y => y.JumlahMurni),
                  JumlahGeser = x.Sum(y => y.JumlahPergeseran),
                  JumlahRKAP = x.Sum(y => y.JumlahRKAP),
                  JumlahABT = x.Sum(y => y.JumlahABT),

              }).ToList();
            List<Laporan> lstUrusanDanAnggaran = (from t in m_lstRekening
                                                  join j in lstJumlah
                                                  on t.ID.ToString().Substring(0, 1) equals j.IDRekening
                                                  where t.Root == 1
                                                  select new Laporan
                                                  {
                                                      Kode = t.ID.ToKodeRekening(1),
                                                      Level = 1,
                                                      Nama = t.Nama,
                                                      IDRekening = t.ID,
                                                      AnggaranMurni = j.JumlahMurni,
                                                      AnggaranGeser = j.JumlahGeser,
                                                      AnggaranRKAP = j.JumlahRKAP,
                                                      AnggaranABT = j.JumlahABT,


                                                  }).ToList<Laporan>();

            foreach (Laporan lp in lstUrusanDanAnggaran)
            {
                string[] dataLRA = { lp.Kode, lp.Nama, 
                                       lp.AnggaranMurni.ToRupiahInReport(),
                                       lp.AnggaranGeser.ToRupiahInReport(),
                                       lp.AnggaranRKAP.ToRupiahInReport(), 
                                       lp.AnggaranABT.ToRupiahInReport(), 
                                       "0" };

                gridSPJPenerimaan.Rows.Add(dataLRA);


                DisplayAnggaranOnLevel(2, lp.IDRekening);





            }


            return true;

        }
        private bool DisplayAnggaranOnLevel(int Level, long IdParent)
        {
            //return true;

            int lenKode = 12;
            switch (Level)
            {
                case 1:
                    lenKode = 1;
                    break;
                case 2:
                    lenKode = 2;
                    break;
                case 3:
                    lenKode = 4;
                    break;
                case 4:
                    lenKode = 6;
                    break;
                case 5:
                    lenKode = 8;
                    break;
                case 6:
                    lenKode = 12;
                    break;
            }
            var lstJumlah = mListAnggaran.GroupBy(x => x.IDRekening.ToString().Substring(0, lenKode))
              .Select(x => new
              {
                  IDRekening = x.Key,
                  JumlahMurni = x.Sum(y => y.JumlahMurni),
                  JumlahGeser = x.Sum(y => y.JumlahPergeseran),
                  JumlahRKAP = x.Sum(y => y.JumlahRKAP),
                  JumlahABT = x.Sum(y => y.JumlahABT),

              }).ToList();
            List<Laporan> lstUrusanDanAnggaran = (from t in m_lstRekening
                                                  join j in lstJumlah
                                                 on t.ID.ToString().Substring(0, lenKode) equals j.IDRekening
                                                  where t.Root == Level && t.IDParent == IdParent
                                                  select new Laporan
                                                  {
                                                      Kode = t.ID.ToKodeRekening(Level),
                                                      Level = Level,
                                                      Nama = t.Nama,
                                                      IDRekening = t.ID,
                                                      AnggaranMurni = j.JumlahMurni,
                                                      AnggaranGeser = j.JumlahGeser,
                                                      AnggaranRKAP = j.JumlahRKAP,
                                                      AnggaranABT = j.JumlahABT,


                                                  }).ToList<Laporan>();

            foreach (Laporan lp in lstUrusanDanAnggaran)
            {
                string[] dataLRA = { lp.Kode, lp.Nama,
                                         lp.AnggaranMurni.ToRupiahInReport(),
                                       lp.AnggaranGeser.ToRupiahInReport(),
                                       lp.AnggaranRKAP.ToRupiahInReport(), 
                                       lp.AnggaranABT.ToRupiahInReport(),  "0" };

                gridSPJPenerimaan.Rows.Add(dataLRA);

                DisplayAnggaranOnLevel(Level + 1, lp.IDRekening);




            }


            return true;

        }
        private void GetTanggal()
        {
            mTanggal = ctrlBulan1.TanggalAkhir;

        }
        private bool GetAnggaran()
        {
            try
            {
                mListAnggaran = GlobalVar.gListRekeningAnggaran.FindAll(x => x.IDDinas == m_IDinas && x.Jenis == 1);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
               
        }
        private bool LoadData(){
            try
            {
                GetTanggal();
                if (GetAnggaran())
                {
                    if (LoadRekening() == true)
                    {
                        if (DisplayAnggaran() == true)
                        {
                            SPJPenerimaanLogic oLogic = new SPJPenerimaanLogic(GlobalVar.TahunAnggaran);
                            mlistSPJPenerimaan = new List<SPJPenerimaan>();
                            mlistSPJPenerimaan = oLogic.Get(m_IDinas, mTanggal);
                            if (mlistSPJPenerimaan == null)
                            {
                                MessageBox.Show(oLogic.LastError());
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void ctrlSKPD1_Load(object sender, EventArgs e)
        {
           
        }
        private bool ProsesDataLevel1(int level, int col = 3)
        {
            try
            {
                int lenKode = 12;
                switch (level)
                {
                    case 1:
                        lenKode = 1;
                        break;
                    case 2:
                        lenKode = 2;
                        break;
                    case 3:
                        lenKode = 4;
                        break;
                    case 4:
                        lenKode = 6;
                        break;
                    case 5:
                        lenKode = 8;
                        break;
                    case 6:
                        lenKode = 12;
                        break;
                }
           
                mBulan = ctrlBulan1.GetID();
                var lstJumlahLalu = mlistSPJPenerimaan.Where(t=>t.Tanggal.Month< mBulan).GroupBy(x => x.IIDRekening.ToString().Substring(0, lenKode))
                  .Select(x => new
                  {
                      IDRekening = x.Key,
                      Level = level,
                      PenerimaanLalu = x.Sum(y => y.Penerimaan),
                      PenyetoranLalu = x.Sum(y => y.Penyetoran),
                  }).ToList();

                var lstJumlahKini = mlistSPJPenerimaan.Where(t => t.Tanggal.Month == mBulan).GroupBy(x => x.IIDRekening.ToString().Substring(0, lenKode))
                  .Select(x => new
                  {
                      IDRekening = x.Key,
                      Level = level,
                      PenerimaanKini = x.Sum(y => y.Penerimaan),
                      PenyetoranKini = x.Sum(y => y.Penyetoran),



                  }).ToList();




                foreach (var r in lstJumlahLalu)
                {
                    for (int row = 0; row < gridSPJPenerimaan.Rows.Count; row++)
                    {
                        if (gridSPJPenerimaan.Rows[row].Cells[0].Value != null)
                        {
                            if (DataFormat.GetString(gridSPJPenerimaan.Rows[row].Cells[0].Value).Replace(".", "") == r.IDRekening)
                            {
                                gridSPJPenerimaan.Rows[row].Cells[COL_PENERIMAANLALU].Value = r.PenerimaanLalu.ToRupiahInReport();
                                gridSPJPenerimaan.Rows[row].Cells[COL_PENYETORANLALU].Value = r.PenyetoranLalu.ToRupiahInReport();
                                gridSPJPenerimaan.Rows[row].Cells[COL_SISALALU].Value = (r.PenerimaanLalu-r.PenyetoranLalu).ToRupiahInReport();

                                
                            }


                        }

                    }



                }
                foreach (var r in lstJumlahKini)
                {
                    for (int row = 0; row < gridSPJPenerimaan.Rows.Count; row++)
                    {
                        if (gridSPJPenerimaan.Rows[row].Cells[0].Value != null)
                        {
                            if (DataFormat.GetString(gridSPJPenerimaan.Rows[row].Cells[0].Value).Replace(".", "") == r.IDRekening)
                            {
                                gridSPJPenerimaan.Rows[row].Cells[COL_PENERIMAANKINI].Value = r.PenerimaanKini.ToRupiahInReport();
                                gridSPJPenerimaan.Rows[row].Cells[COL_PENYETORANKINI].Value = r.PenyetoranKini.ToRupiahInReport();
                                gridSPJPenerimaan.Rows[row].Cells[COL_SISAKINI].Value = (r.PenerimaanKini - r.PenyetoranKini).ToRupiahInReport();


                            }


                        }

                    }



                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        private void RefreshSisa()
        {
            decimal penerimaanLalu = 0M;
            decimal penyetoranlalu = 0M;
            decimal penerimaankini = 0M;
            decimal penyetorankini = 0M;
            decimal anggaran= 0M;

            foreach (DataGridViewRow row in gridSPJPenerimaan.Rows)
            {
                penerimaanLalu = 0M;
                penyetoranlalu = 0M;
                penerimaankini = 0M;
                penyetorankini = 0M;
                anggaran = 0M;
                penerimaanLalu = DataFormat.FormatUangReportKeDecimal(row.Cells[COL_PENERIMAANLALU].Value);
                penyetoranlalu = DataFormat.FormatUangReportKeDecimal(row.Cells[COL_PENYETORANLALU].Value);
                penerimaankini = DataFormat.FormatUangReportKeDecimal(row.Cells[COL_PENERIMAANKINI].Value);
                penyetorankini = DataFormat.FormatUangReportKeDecimal(row.Cells[COL_PENYETORANKINI].Value);
                anggaran = DataFormat.FormatUangReportKeDecimal(row.Cells[mcolAnggaran].Value);
                row.Cells[COL_PENERIMAAN].Value = (penerimaanLalu + penerimaankini).ToRupiahInReport();
                row.Cells[COL_PENYETORAN].Value = (penyetorankini+ penyetoranlalu).ToRupiahInReport();
                row.Cells[COL_SISA].Value = ((penerimaanLalu + penerimaankini) - (penyetorankini + penyetoranlalu)).ToRupiahInReport();
                row.Cells[COL_SISA+1].Value = (anggaran - (penerimaanLalu + penerimaankini)).ToRupiahInReport();



            }
        }
        private void GetKolomAnggaran(){
             
             switch (ctrlTahapAnggaran1.ID){
                 case 2:
                   mcolAnggaran = COL_ANGGARANMURNI;
                 break;
                 case 3:
                  mcolAnggaran = COL_ANGGARANGESER;
                 break;

                 case 4:
                  mcolAnggaran = COL_ANGGARANRKAP;
                 break;
                 case 5:
                  mcolAnggaran = COL_ANGGARANABT;
                 break;


             }
             
            gridSPJPenerimaan.Columns[mcolAnggaran].Visible= true;

        }
        private void ctrlSKPD1_OnChanged(int pID)
        {
            m_IDinas = pID;
        }
        private void DisplaySPJ()
        {
            ProsesDataLevel1(1);
            ProsesDataLevel1(2);
            ProsesDataLevel1(3);
            ProsesDataLevel1(4);
            ProsesDataLevel1(5);
            ProsesDataLevel1(6);
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

                                
                
                 Pejabat bendahara = new Pejabat ();
                 Pejabat pimpinan = new Pejabat();

                 bendahara = ctrlSKPD1.GetBendaharaPenerimaan(dtCetak.Value);
                pimpinan = ctrlSKPD1.GetKuaaPenggunaAnggaranPenerimaan(dtCetak.Value);
                yPos = yPos + 10;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.gPemda.Ibukota + "," + dtCetak.Value.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Jabatan, 10, 30, yPos, setengah, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Jabatan, 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Nama, 10, 30, yPos , setengah, stringFormat, false, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Nama, 10, posisiTengah, yPos , setengah, stringFormat, true, true, true);
                
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + pimpinan.NIP, 10, 30, yPos, setengah, stringFormat, false );
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + bendahara.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);
                               


                
            }



            previousPage = args.Page;


            previousPage = args.Page;
        }
        private void cmdCetak_Click(object sender, EventArgs e)
        {
             try
            {
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
 
              yPos = oCetakPDF.TulisItem(previousPage.Graphics, "LAPORAN PERTANGGUNGJAWABAN BENDAHARA PENERIMAAN "+
                         GlobalVar.TahunAnggaran.ToString(), 10, kiri, yPos,
                         page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SPJ PENDAPATAN - FUNGSIONAL"
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
                Pejabat bendahara = ctrlSKPD1.GetBendaharaPenerimaan(ctrlBulan1.TanggalAkhir);
 
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
                          ctrlBulan1.NamaBulan + " " + GlobalVar.TahunAnggaran.ToString(), 10, 155, yPos,
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
                tableHeader.Columns.Add("11");

                tableHeader.Columns.Add("12");
                //tableHeader.Columns.Add("13");
                
                 tableHeader.Rows.Add(new string[]
                    {            " Kode      Rekening"," Uraian","Jumlah Anggaran","Sampai dengan Bulan Lalu","Sampai dengan Bulan Lalu","Sampai dengan Bulan Lalu",
                                "Bulan Ini","Bulan Ini","Bulan Ini",
                                "Sampai dengan Bulan Ini","Sampai dengan Bulan Ini","Sampai dengan Bulan Ini","Sisa   Anggaran"});

                tableHeader.Rows.Add(new string[]
                    {            "Kode Rekening","Uraian","Jumlah Anggaran","Penerimaan","Penyetoran",
                                "Sisa","Penerimaan","Penyetoran","Sisa",
                                "Penerimaan","Penyetoran","Sisa","Sisa Anggaran"});

              

                    pdfGridHeader.DataSource = tableHeader; //data
                    pdfGridHeader.Columns[0].Width = 60;
                    pdfGridHeader.Columns[1].Width = 105;
                // Angka 
                    pdfGridHeader.Columns[2].Width = 60;

                    pdfGridHeader.Columns[3].Width = 60;
                    pdfGridHeader.Columns[4].Width = 60;
                    pdfGridHeader.Columns[5].Width = 55;

                    pdfGridHeader.Columns[6].Width = 60;
                    pdfGridHeader.Columns[7].Width = 60;
                    pdfGridHeader.Columns[8].Width = 55;

                    pdfGridHeader.Columns[9].Width = 60;
                    pdfGridHeader.Columns[10].Width = 60;
                    pdfGridHeader.Columns[11].Width = 60;
                    pdfGridHeader.Columns[12].Width = 60;
                    //pdfGridHeader.Columns[13].Width = 55;

                   pdfGridHeader.Rows[0].Cells[0].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[1].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[2].RowSpan = 2;
          
                pdfGridHeader.Rows[0].Cells[3].ColumnSpan = 3;
                pdfGridHeader.Rows[0].Cells[6].ColumnSpan = 3;
                pdfGridHeader.Rows[0].Cells[9].ColumnSpan = 3;
                pdfGridHeader.Rows[0].Cells[12].RowSpan = 2;
                
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
                table.Columns.Add("6=4-5");
                table.Columns.Add("7");
                table.Columns.Add("8");
                table.Columns.Add("9=7-8");
                table.Columns.Add("10=4+7");
                table.Columns.Add("11=5+8");
                table.Columns.Add("12=10-11");

                table.Columns.Add("3-12");            
                
                //table.Columns.Add("Level");



                //table. Columns[0]
                //Assign Column count
                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();

                
               

                decimal akumulasi = 0L;
                decimal sisa = 0;

                for (int idx = 0; idx < gridSPJPenerimaan.Rows.Count; idx++)
                {
                    if (gridSPJPenerimaan.Rows[idx].Cells[0].Value != null)
                    {
                        
                    table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[0].Value),
                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[1].Value),                      
                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[mcolAnggaran].Value),
   

                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[COL_PENERIMAANLALU].Value),
                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[COL_PENYETORANLALU].Value),
                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[COL_SISALALU].Value),
                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[COL_PENERIMAANKINI].Value),
                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[COL_PENYETORANKINI].Value),
                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[COL_SISAKINI].Value),
                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[COL_PENERIMAAN].Value),
                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[COL_PENYETORAN].Value),
                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[COL_SISA].Value),
                       DataFormat.GetString(gridSPJPenerimaan.Rows[idx].Cells[COL_SISA+1].Value),
                
                       

                       
                        
                    });
                    }


                }
              
                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 60;
                pdfGrid.Columns[1].Width = 105;

                // Angka 
                pdfGrid.Columns[2].Width = 60;

                pdfGrid.Columns[3].Width = 60;
                pdfGrid.Columns[4].Width = 60;
                pdfGrid.Columns[5].Width = 55;

                pdfGrid.Columns[6].Width = 60;
                pdfGrid.Columns[7].Width = 60;
                pdfGrid.Columns[8].Width = 55;

                pdfGrid.Columns[9].Width = 60;
                pdfGrid.Columns[10].Width = 60;
                pdfGrid.Columns[11].Width = 60;

                pdfGrid.Columns[12].Width = 60;

            
  
                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding

                gridStyle.CellPadding = new PdfPaddings(3,1, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                pdfGrid.Style = gridStyle;
                 PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                for (int col = 2; col < pdfGrid.Columns.Count; col++ )
                    pdfGrid.Columns[col].Format = formatKolomAngka;

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
                string namaFile = Path.GetFullPath(@"../../../SPJ.pdf");

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
        
    

        private void rbPergeseran_CheckedChanged(object sender, EventArgs e)
        {
            GetKolomAnggaran();
        }

        private void rbPerubahan_CheckedChanged(object sender, EventArgs e)
        {
            GetKolomAnggaran();
        }

        private void rbPenyempurnaanABT_CheckedChanged(object sender, EventArgs e)
        {
            GetKolomAnggaran();
        }

        private void rbMurni_CheckedChanged(object sender, EventArgs e)
        {
            GetKolomAnggaran();
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

                NamaFile = BuatFile();

                if (NamaFile == "")
                {
                    MessageBox.Show("Belum ditentukan nama filenya..");
                    return;
                }
                KillSpecificExcelFileProcess(NamaFile);
                // creating Excel Application  
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                // creating new WorkBook within Excel application  
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                // see the excel sheet behind the program  
                app.Visible = true;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                // changing the name of active sheet  
                worksheet.Name = "Exported from gridview";
                GetKolomAnggaran();
                // storing header part in Excel  
                for (int i = 1; i < gridSPJPenerimaan.Columns.Count + 1; i++)
                {
                    if (i < 3 || i == mcolAnggaran || i >= 7)
                        worksheet.Cells[1, i] = gridSPJPenerimaan.Columns[i - 1].HeaderText;
           
                }
                // storing Each row and column value to excel sheet  
                List<int> lstColToCetak = new List<int>();
            




                for (int i = 0; i < gridSPJPenerimaan.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < gridSPJPenerimaan.Columns.Count - 1; j++){
                        if (j < 3 || j == mcolAnggaran || j >= 7) { 
                            if (gridSPJPenerimaan.Rows[i].Cells[j].Value != null)
                            {


                                worksheet.Cells[i + 2, j + 1] = gridSPJPenerimaan.Rows[i].Cells[j].Value.ToString();
                            }
                            else
                            {
                                worksheet.Cells[i + 2, j + 1] = "";
                            }
                        }
                    }
                    
                }
                // save the application  
                workbook.SaveAs(NamaFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                // Exit from the application 
                //txtNamaFile.Text = NamaFile;
                MessageBox.Show("Selesai export ke Excell. Disimpan di: " + NamaFile);
                app.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke Excell." + ex.Message);
                if (File.Exists(NamaFile) == true)
                {
                    File.Delete(NamaFile);

                }
                MessageBox.Show(ex.Message);
            }
        
            
        
        }
    }
}
