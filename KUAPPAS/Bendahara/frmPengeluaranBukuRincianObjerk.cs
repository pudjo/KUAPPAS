using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
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
//using Syncfusion.Pdf.Parsing;


using System.IO;
using NPOI.SS.Formula.Functions;
namespace KUAPPAS.Bendahara
{
    public partial class frmPengeluaranBukuRincianObjerk : ChildForm
    {


        int m_IDSKPD;
        private List<ProgramKegiatanAnggaran> m_lstProgramKegiatan;
        private List<RincianObjek> mROList;



        //private Single m_iStatus;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        private int m_UnitAnggaran;
        int currentContainingCellListIndex;
        PdfPage previousPage;
        bool SaatnyacetakKesimpulan;
        bool SudahCetakKesimpulan;
        float PosisiTerakhir;
        private int m_iTahapanAnggaran;
       
        int halaman;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;


        
        CetakPDF oCetakPDF;

        Pejabat pimpinan = new Pejabat();
        Pejabat bendahara = new Pejabat();
    
  

        public frmPengeluaranBukuRincianObjerk()
        {
            InitializeComponent();
            oCetakPDF = new CetakPDF();
            m_iTahapanAnggaran = 2;
        }

        private void frmPengeluaranBukuRincianObjerk_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Buku Rincian Objek");
            ctrlDinas1.Create();
            gridRincianObjek.FormatHeader();
            dtCetak.Value = DateTime.Now.Date;
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlDinas1.SetID(GlobalVar.Pengguna.SKPD);
                m_IDSKPD = GlobalVar.Pengguna.SKPD;
            }
            ctrlTanggalBulanVertikal1.TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
            ctrlTanggalBulanVertikal1.TanggalAkhir = DateTime.Now;
            //new DateTime(GlobalVar.TahunAnggaran, 1, 1);.Now.
        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
            try
            {//..
                gridRincianObjek.Rows.Clear();
                if (ctrlTahapAnggaran1.ID==0){
                    MessageBox.Show("Belum memilih Tahapan Anggaran");
                    return;
                }
                if (chkUPGU.Checked == false && chkLS.Checked == false)
                {
                    MessageBox.Show("Belum Jenis Belanja.");
                    return;
                }
                
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool GetListProgramKegiatan()
        {
            try
            {
                ProgramKegiatanAnggaranLogic oLogic = new ProgramKegiatanAnggaranLogic(GlobalVar.TahunAnggaran);
                m_IDSKPD = ctrlDinas1.GetID();
                if (GlobalVar.gListProgramKegiatanRekeningAnggaran == null)
                {
                    GlobalVar.gListProgramKegiatanRekeningAnggaran = new List<ProgramKegiatanAnggaran>();
                }
                if (GlobalVar.gListProgramKegiatanRekeningAnggaran.FindAll(p => p.IDDInas == m_IDSKPD).Count == 0)
                {
                    List<ProgramKegiatanAnggaran> lst = new List<ProgramKegiatanAnggaran>();
                    m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
                    lst = oLogic.GetByDInas(m_IDSKPD, 0);
                    if (lst != null)
                    {
                        //foreach (ProgramKegiatanAnggaran p in lst)
                        //{
                        //    GlobalVar.gListProgramKegiatanRekeningAnggaran.Add(p);
                        //    m_lstProgramKegiatan.Add(p);
                        //}
                        GlobalVar.gListProgramKegiatanRekeningAnggaran = lst;
                        m_lstProgramKegiatan = lst;
                    }
                    else
                    {
                        MessageBox.Show(oLogic.LastError());
                        return false;
                    }

                   
                }
                else
                {
                    m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
                    m_lstProgramKegiatan = GlobalVar.gListProgramKegiatanRekeningAnggaran.FindAll(p => p.IDDInas == m_IDSKPD);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool GetTanggal()
        {
            return ctrlTanggalBulanVertikal1.CekPilihan() ;
        }
        private void LoadData()
        {
            try
            {
                if (GetTanggal() == false)
                    return;
                //  mKartuKendaliList
                mROList = new List<RincianObjek>();
                mTanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                mTanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;

                m_IDSKPD = ctrlDinas1.GetID();

                RincianObjekLogic roLogic = new RincianObjekLogic(GlobalVar.TahunAnggaran);
                List<RincianObjek>lst = new  List<RincianObjek>();
                long idsubkegiatan = ctrlProgramKegiatan1.IdSubKegiatan;
                
                long idrek = ctrlPilihanRekeningAnggaran1.GetID();
                if (chkPilihProgram.Checked == false)
                {
                    idsubkegiatan = 0;

                    idrek = 0;
                }

                lst = roLogic.GetRincianObjek(m_IDSKPD, mTanggalAkhir, idsubkegiatan, idrek);
                if (roLogic.IsError() == true)
                {
                    MessageBox.Show(roLogic.LastError());
                    return;
                }
                if (chkLS.Checked == true && chkUPGU.Checked == false )
                {
                    mROList = lst.FindAll(x => x.JenisBeanja > 2);
                }
                if (chkUPGU.Checked == true && chkLS.Checked == false )
                {
                    mROList = lst.FindAll(x => x.JenisBeanja <= 2);
                }
                if (chkUPGU.Checked == true && chkLS.Checked == true)
                {
                    mROList = lst;
                }
                if (chkPilihProgram.Checked == true)
                {
                    if (ctrlProgramKegiatan1.IdSubKegiatan == 0)
                    {
                        MessageBox.Show("Sub Kegiatan Harus dipilih..");
                        return;

                    }
                    if (ctrlProgramKegiatan1.IdSubKegiatan > 0)
                    {
                        mROList = lst.FindAll(x => x.IDSUbKegiatan == ctrlProgramKegiatan1.IdSubKegiatan);
                    }
                    if (ctrlProgramKegiatan1.IdSubKegiatan > 0 && ctrlPilihanRekeningAnggaran1.GetID() > 0)
                    {
                        //long idrek = ctrlPilihanRekeningAnggaran1.GetID();
                        mROList = lst.FindAll(x => x.IDSUbKegiatan == ctrlProgramKegiatan1.IdSubKegiatan &&
                                                   x.IDRekening == idrek);
                    }

                }
                

                if (mROList != null)
                {

                    if (GetListProgramKegiatan() == true)
                    {


                        DisplayRincianObjek();
                    }

                }
                else
                {
                    MessageBox.Show(roLogic.LastError());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void DisplayRincianObjek()
        {
             

     
             int TahapanAnggaran = ctrlTahapAnggaran1.ID;
             var listROdanAnggaran = from anggaran in m_lstProgramKegiatan
                                     join ro in mROList 
                                     on
                                        new { f1 = anggaran.IDDInas, f2 =  anggaran.IDSubKegiatan, f3=anggaran.IIDRekening, f4=anggaran.KodeUK  }
                                        equals
                                        new { f1 = ro.IDDInas, f2 = ro.IDSUbKegiatan, f3 = ro.IDRekening, f4=ro.KodeUK } 
                                       orderby ro.IDSUbKegiatan,ro.IDRekening, ro.NoBKU 
                                     select new
                                     {
                                         IDSubKegiatan = anggaran.IDSubKegiatan,

                                         IDProgram = anggaran.IDProgram,
                                         IDKegiatan = anggaran.IDKegiatan,
                                         NamaProgram = anggaran.NamaProgram,
                                         NamaKegiatan = anggaran.NamaKegiatan,
                                         NamaSubKegiatan = anggaran.NamaSubKegiatan,
            
                                            NoBKU= ro.NoBKU,
                                            NoBukti= ro.NoBukti,
                                            NamaRekening= anggaran.NamaRekening,
                                            IDRekening = anggaran.IIDRekening,
                                            Uraian = ro.Uraian,
                                            Anggaran = TahapanAnggaran==2? anggaran.AnggaranMurni:
                                                            (TahapanAnggaran==3? anggaran.AnggaranGeser: (
                                                            TahapanAnggaran==4? anggaran.AnggaranRKAP:anggaran.AnggaranABT)),

                                            Tanggal = ro.Tanggal,
                                            UP=  ro.UP,
                                            LS= ro.LS,
                                            TU=ro.TU



                                     };



             long oldIDrekening = 0;
             long oldIDSUbKegiatan = 0;
             decimal JumlahBerjalan = 0l;
            foreach (var ro in listROdanAnggaran)
             {
                 if (ro.Tanggal >= mTanggalAwal)
                 {
                     if (oldIDrekening != ro.IDRekening || ro.IDSubKegiatan !=oldIDSUbKegiatan)
                     {
                         JumlahBerjalan = 0;
                         JumlahBerjalan = JumlahBerjalan + ro.UP + ro.LS + ro.TU;
                         string[] row = { ro.IDProgram.ToKodeProgram(), ro.NamaProgram,
                                      ro.IDKegiatan.ToKodeKegiatan(), ro.NamaKegiatan,
                                        ro.IDSubKegiatan.ToKodeSubKegiatan(), ro.NamaSubKegiatan,
                                        ro.IDRekening.ToKodeRekening(), ro.NamaRekening,
                                        ro.Anggaran.ToRupiahInReport(), ro.NoBKU.ToString(), ro.Tanggal.ToTanggalIndonesia(), ro.NoBukti, ro.Uraian, ro.LS.ToRupiahInReport(), ro.UP.ToRupiahInReport(), ro.TU.ToRupiahInReport(),JumlahBerjalan.ToRupiahInReport() };
                         gridRincianObjek.Rows.Add(row);
                         oldIDrekening = ro.IDRekening;
                         oldIDSUbKegiatan = ro.IDSubKegiatan;

                     }
                     else
                     {
                         JumlahBerjalan = JumlahBerjalan + ro.UP + ro.LS + ro.TU;
                         string[] rowx = { "", "", "", "", "", "", "", "", "", ro.NoBKU.ToString(), ro.Tanggal.ToTanggalIndonesia(), ro.NoBukti, ro.Uraian, ro.LS.ToRupiahInReport(), ro.UP.ToRupiahInReport(), ro.TU.ToRupiahInReport(), JumlahBerjalan.ToRupiahInReport() };
                         gridRincianObjek.Rows.Add(rowx);
                     }
                 }
             }



        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            try
            {
                //Create a new PDF document.
                PdfDocument document = new PdfDocument();

                long oldIDRekening = 0;
                long oldIDsubkegiatan = 0;
                // Get
                GetPejabat();
                for (int iRow = 0; iRow < gridRincianObjek.Rows.Count; iRow++)
                {
                    
                    if (gridRincianObjek.Rows[iRow].Cells[6].Value != null)
                    {
                        long idrekeningThisRow = DataFormat.GetLong(gridRincianObjek.Rows[iRow].Cells[6].Value.ToString().Replace(".", ""));
                        long idSubKegiatanThisRow = DataFormat.GetLong(gridRincianObjek.Rows[iRow].Cells[4].Value.ToString().Replace(".", ""));

                        if (idrekeningThisRow != oldIDRekening || idSubKegiatanThisRow != oldIDsubkegiatan)
                        {

                          
                            //if (idrekening == "5.1.02.24.01.0001")
                            //{
                            //    MessageBox.Show(idsubkegiatan);
                            //}

                           iRow=  CetakRekeningIni(ref document, iRow);
                          
                            oldIDRekening = idrekeningThisRow;
                            oldIDsubkegiatan = idSubKegiatanThisRow;
                        }
                    }
                }

                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPP.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }
              
               

                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.HapusBlank(@"../../../SPP.pdf");

                pV.Document = Path.GetFullPath(@"../../../SPP.pdf");
                pV.Show();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
           
            }
        }
        private void GetPejabat()
        {
      

            pimpinan = ctrlDinas1.GetPimpinan(mTanggalAkhir);
            bendahara = ctrlDinas1.GetBendaharaPengeluaran(mTanggalAkhir);

            if (pimpinan == null)
            {
                MessageBox.Show("Data Pimpinan belum di setting di master pejabat");
                //  return 0;
                pimpinan = new Pejabat();
                pimpinan.Nama = "";
                pimpinan.Jabatan = "Kepala Dinas " + ctrlDinas1.GetNamaSKPD();
                pimpinan.NIP = "NIP ";
            }

            if (bendahara == null)
            {
                MessageBox.Show("Data Bndahara belum di setting di master pejabat");
                bendahara = new Pejabat();
                bendahara.Nama = "";
                bendahara.Jabatan = "Bendahara Pengeluaran Dinas " + ctrlDinas1.GetNamaSKPD();
                bendahara.NIP = "NIP ";
            }

        }
        private int  CetakRekeningIni(ref PdfDocument document, int iRow)
        {
            int rowx = iRow;

            //Add a page.
            try
            {

                PdfSection section = document.Sections.Add();
                SaatnyacetakKesimpulan = false;
                SudahCetakKesimpulan = false;
                document.PageSettings.Margins.Left = 8;
                document.PageSettings.Margins.Top = 30;
                document.PageSettings.Margins.Right = 2;
                document.PageSettings.Margins.Bottom = 8;
                section.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section.PageSettings.Height = 935;// = PdfPageSize.Legal;       
                section.PageSettings.Orientation = PdfPageOrientation.Portrait;

                float yPos = 0;

                System.Drawing.Font tempfont = new System.Drawing.Font("Arial Narrow", 11, FontStyle.Regular);

                //Enable unicode to draw unicode characters
                PdfTrueTypeFont currentFont = new PdfTrueTypeFont(tempfont, true);

                //GlobalVar.gListProgramKegiatanRekeningAnggaran
                ProgramKegiatanAnggaran programKegiatan = new ProgramKegiatanAnggaran();

                string idprogram = DataFormat.GetString(gridRincianObjek.Rows[iRow].Cells[0].Value);
                string namaProgram = DataFormat.GetString(gridRincianObjek.Rows[iRow].Cells[1].Value);
                string idkegiatan = DataFormat.GetString(gridRincianObjek.Rows[iRow].Cells[2].Value);
                string namakegiatan = DataFormat.GetString(gridRincianObjek.Rows[iRow].Cells[3].Value);
                string idsubkegiatan = DataFormat.GetString(gridRincianObjek.Rows[iRow].Cells[4].Value);
                string namasubkegiatan = DataFormat.GetString(gridRincianObjek.Rows[iRow].Cells[5].Value);

                string idrekening = DataFormat.GetString(gridRincianObjek.Rows[iRow].Cells[6].Value);
                string namarekening = DataFormat.GetString(gridRincianObjek.Rows[iRow].Cells[7].Value);
                string anggaran = DataFormat.GetString(gridRincianObjek.Rows[iRow].Cells[8].Value);

                if (idsubkegiatan == "5.01.03.202.0008")
                {
                    ///MessageBox.Show(idsubkegiatan);
                }
                if (idrekening == "5.1.02.24.01.0001")
                {
                   // MessageBox.Show(idsubkegiatan);
                }
                //SaatnyacetakKesimpulan = false;
                PdfPage page = section.Pages.Add();

                PdfGraphics graphics = page.Graphics;
                previousPage = page;
                document.Pages.PageAdded += PagesPajak_PageAdded;

                yPos = 30;
                PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);

                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;

                //stringFormat.CharacterSpacing = 2f;
                oCetakPDF = new CetakPDF();
                //SizeF size = font12.MeasureString("xxx");



                


                float kiri = 30;
              

                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                   page.GetClientSize().Width, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "BUKU RINCIAN OBYEK BELANJA" +
                     GlobalVar.TahunAnggaran.ToString(), 10, kiri, yPos,
                     page.GetClientSize().Width, stringFormat, true, false, true);
                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = yPos + 20;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD "
                          , 8, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 110, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          ctrlDinas1.GetID().ToKodeDinas(), 8, 115, yPos,
                          page.GetClientSize().Width, stringFormat, false , false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          ctrlDinas1.GetNamaSKPD(), 8, 205, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Nama Program."
                          , 8, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 110, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
           
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          idprogram, 8, 115, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          namaProgram, 8, 205, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Kegiatan"
                          , 8, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 110, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          idkegiatan, 8, 115, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          namakegiatan , 8, 205, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Sub Kegiatan"
                          , 8, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 110, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          idsubkegiatan, 8, 115, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          namasubkegiatan, 8, 205, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Rekening"
                          , 8, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 110, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          idrekening, 8, 115, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          namarekening, 8, 205, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);
                
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Anggaran"
                          , 8, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 110, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                           "Rp. " + anggaran, 8, 115, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);

                yPos = yPos + 20;

                if (iRow == 58)
                {
                    kiri = 30;
                }
                

                

                #region gridKas
                PdfGrid pdfGrid = new PdfGrid();

                int count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("No BKU");
                table.Columns.Add("Tanggal");
                table.Columns.Add("Keterangan");
                table.Columns.Add("Belanja LS");
                table.Columns.Add("Belanja UP/GU");
                table.Columns.Add("Belanja TU");
                table.Columns.Add("Jumlah");

                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();
                
                decimal akumulasi = 0L;
                decimal sisa = 0;
                int lastRow = iRow;
                if (iRow == 58)
                {
                    kiri = 30;
                }
               // if (iRow != 58){
                for (int idx = iRow; idx < gridRincianObjek.Rows.Count; idx++)
                {
                    string Keterangan = "";
                    if (gridRincianObjek.Rows[idx].Cells[6].Value != null) {
                        if ((idsubkegiatan == DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[4].Value) &&
                             idrekening == DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[6].Value)) ||
                              (DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[4].Value) == "" &&
                              DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[6].Value) == "")
                            )
                        {
                            Keterangan = DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[12].Value).Replace("\t", "").Replace("\r\n", "").ReplaceUnicode();
                            table.Rows.Add(new string[]
                            {

                        
                           
                               DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[9].Value).Replace("\t", "").Replace("\r\n", ""),
                               DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[10].Value).Replace("\t", "").Replace("\r\n", ""),
                               Keterangan ,//DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[12].Value).Replace("\t", "").Replace("\r\n", ""),
                               DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[13].Value).Replace("\t", "").Replace("\r\n", ""),     
                               DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[14].Value).Replace("\t", "").Replace("\r\n", ""),     
                               DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[15].Value).Replace("\t", "").Replace("\r\n", ""), 
                               DataFormat.GetString(gridRincianObjek.Rows[idx].Cells[16].Value).Replace("\t", "").Replace("\r\n", ""), 
                       

                            });
                            lastRow = idx;
                        }
                        else
                        {
                            break;
                        }
                    }
                //}
                }

                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 40;
        
                pdfGrid.Columns[1].Width = 50;
                pdfGrid.Columns[2].Width = 140;
                pdfGrid.Columns[3].Width = 65;
                pdfGrid.Columns[4].Width = 65;
                pdfGrid.Columns[5].Width = 65;
                pdfGrid.Columns[6].Width = 65;

                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));

              //  gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGrid.Columns[3].Format = formatKolomAngka;
                pdfGrid.Columns[4].Format = formatKolomAngka;
                pdfGrid.Columns[5].Format = formatKolomAngka;
                pdfGrid.Columns[6].Format = formatKolomAngka;

                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 8));
                //PdfFont font = new PdfTrueTypeFont(sFont);
                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();

                pdfGrid.RepeatHeader = true;
                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;
                if (iRow == 58)
                {
                    kiri = 30;
                }

                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", font.Size, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));

                cellHeaderStyle.Font = font;
                
                cellHeaderStyle.StringFormat = stringFormatHeader;
                for (int c = 0; c < pdfGrid.Headers.Count; c++)
                {
                    pdfGrid.Headers[c].ApplyStyle(cellHeaderStyle);
                    pdfGrid.Headers[c].Height = 30;

                }


                for (int idx = 0; idx < pdfGrid.Rows.Count; idx++)
                {
                    pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 7,
                        FontStyle.Regular)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);

                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        //if (c == 4)
                        //{
                        //    pdfGrid.Rows[idx].Cells[c].Style.CellPadding = new PdfPaddings(1, 1, 1, 0);
                        //}
                        

                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.01F;

                    }

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.
                if (iRow == 58)
                {
                    kiri = 30;//wwwwee
                }

                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos));
          
                #endregion gridKas
                PdfGrid pdfGridRingkasan = new PdfGrid();

                RincianObjek roBulanIni = new RincianObjek();
                RincianObjek roBulanLalu = new RincianObjek();
                RincianObjek roJumlah = new RincianObjek();
                
                decimal lslalu = mROList.Where(x => x.Tanggal < mTanggalAwal &&
                                                x.IDRekening == DataFormat.GetLong(idrekening.Replace(".", ""))&&
                                                x.IDSUbKegiatan == DataFormat.GetLong(idsubkegiatan.Replace(".", ""))
                                                ).Sum(s => s.LS);
                decimal uplalu = mROList.Where(x => x.Tanggal < mTanggalAwal &&
                                                x.IDRekening == DataFormat.GetLong(idrekening.Replace(".", "")) &&
                                                x.IDSUbKegiatan == DataFormat.GetLong(idsubkegiatan.Replace(".", ""))
                                                ).Sum(s => s.UP);
                decimal tulalu = mROList.Where(x => x.Tanggal < mTanggalAwal &&
                                                                x.IDRekening == DataFormat.GetLong(idrekening.Replace(".", "")) &&
                                                x.IDSUbKegiatan == DataFormat.GetLong(idsubkegiatan.Replace(".", ""))
                                                                ).Sum(s => s.TU);
                roBulanLalu.Uraian = "Jumlah Sampai dengan Bulan Lalu";
                roBulanLalu.LS = lslalu;
                roBulanLalu.UP = uplalu;
                roBulanLalu.TU = tulalu;

                
                
                decimal lskini = mROList.Where(x => x.Tanggal >= mTanggalAwal && x.Tanggal<=mTanggalAkhir && 
                                                x.IDRekening == DataFormat.GetLong(idrekening.Replace(".", "")) &&
                                                x.IDSUbKegiatan == DataFormat.GetLong(idsubkegiatan.Replace(".", ""))
                                                ).Sum(s => s.LS);
                decimal upkini = mROList.Where(x => x.Tanggal >= mTanggalAwal && x.Tanggal<=mTanggalAkhir && 
                                                x.IDRekening == DataFormat.GetLong(idrekening.Replace(".", "")) &&
                                                x.IDSUbKegiatan == DataFormat.GetLong(idsubkegiatan.Replace(".", ""))
                                                ).Sum(s => s.UP);
                decimal tukini = mROList.Where(x => x.Tanggal >= mTanggalAwal && x.Tanggal<=mTanggalAkhir && 
                                                                x.IDRekening == DataFormat.GetLong(idrekening.Replace(".", "")) &&
                                                x.IDSUbKegiatan == DataFormat.GetLong(idsubkegiatan.Replace(".", ""))
                                                                ).Sum(s => s.TU);

                roBulanIni.Uraian = "Jumlah Bulan Ini";
                roBulanIni.LS = lskini;
                roBulanIni.UP = upkini;
                roBulanIni.TU = tukini;


                roJumlah.Uraian = "Jumlah Sampai dengan Bulan Ini";
                roJumlah.LS = lskini + lslalu;
                roJumlah.UP = upkini+uplalu;
                roJumlah.TU = tukini+tulalu;
                if (iRow == 58)
                {
                    kiri = 30;
                }





                ////Create a DataTable
                table = new DataTable();
                //Add columns to table

                table.Columns.Add("Keterangan");
                table.Columns.Add("LS");
                table.Columns.Add("UP");
                table.Columns.Add("TU");
                table.Columns.Add("Jumlah");


                columnCount = table.Columns.Count;
                data = new List<object>();




                table.Rows.Add(new string[]
                    {

                       roBulanIni.Uraian,
                       roBulanIni.LS.ToRupiahInReport(),                      
                       roBulanIni.UP.ToRupiahInReport(),
                       roBulanIni.TU.ToRupiahInReport(),
                       (roBulanIni.TU+roBulanIni.LS + roBulanIni.UP).ToRupiahInReport()

                      
                       
                    });
                table.Rows.Add(new string[]
                    {
                        roBulanLalu.Uraian,
                       roBulanLalu.LS.ToRupiahInReport(),                      
                       roBulanLalu.UP.ToRupiahInReport(),
                       roBulanLalu.TU.ToRupiahInReport(),
                       (roBulanLalu.TU+roBulanLalu.LS + roBulanLalu.UP).ToRupiahInReport()
                       
                    });

                table.Rows.Add(new string[]
                    {

                       roJumlah.Uraian,
                       roJumlah.LS.ToRupiahInReport(),                      
                       roJumlah.UP.ToRupiahInReport(),
                       roJumlah.TU.ToRupiahInReport(),
                       (roJumlah.TU+roJumlah.LS + roJumlah.UP).ToRupiahInReport()
                       
                    });



                pdfGridRingkasan.DataSource = table; //data

                pdfGrid.Columns[0].Width = 40;

                pdfGrid.Columns[1].Width = 50;
                pdfGrid.Columns[2].Width = 140;
                pdfGrid.Columns[3].Width = 65;
                pdfGrid.Columns[4].Width = 65;
                pdfGrid.Columns[5].Width = 65;
                pdfGrid.Columns[6].Width = 65;
     

                pdfGridRingkasan.Columns[0].Width = 40 + 50 + 140;
                pdfGridRingkasan.Columns[1].Width = 65;
                pdfGridRingkasan.Columns[2].Width = 65;
                pdfGridRingkasan.Columns[3].Width = 65;
                pdfGridRingkasan.Columns[4].Width = 65;

                gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGridRingkasan.Style = gridStyle;


                formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGridRingkasan.Columns[1].Format = formatKolomAngka;
                pdfGridRingkasan.Columns[2].Format = formatKolomAngka;
                pdfGridRingkasan.Columns[3].Format = formatKolomAngka;
                pdfGridRingkasan.Columns[4].Format = formatKolomAngka;

                pdfGridRingkasan.Headers.Clear();
                for (int idx = 0; idx < pdfGridRingkasan.Rows.Count; idx++)
                {
                    pdfGridRingkasan.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 7,
                        FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);

                    for (int c = 0; c < pdfGridRingkasan.Columns.Count; c++)
                    {
                       
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.05F;

                    }

                }

                ////Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.


                pdfGridLayoutResult = pdfGridRingkasan.Draw(pdfGridLayoutResult.Page, new PointF(kiri, pdfGridLayoutResult.Bounds.Bottom));


                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();

                

                return lastRow;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal cetak rincian Objek baris  " + iRow.ToString() + System.Environment.NewLine + ex.Message);
                return 0;
            }
        }
        private void PagesPajak_PageAdded(object sender, PageAddedEventArgs args)
        {
            float yPos = PosisiTerakhir + 5;

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;
            PdfStringFormat stringFormat = new PdfStringFormat();


            CetakPDF oCetakPDF = new CetakPDF();


            if (SaatnyacetakKesimpulan == true)
            {


                if (SudahCetakKesimpulan == false)
                {


                    Pejabat bendahara = new Pejabat();
                    Pejabat pimpinan = new Pejabat();

                    bendahara = ctrlDinas1.GetBendaharaPengeluaran(dtCetak.Value);

                    pimpinan = ctrlDinas1.GetPimpinan(dtCetak.Value);
                    yPos = yPos + 10;
                    stringFormat.Alignment = PdfTextAlignment.Center;
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.gPemda.Ibukota + "," + ctrlTanggalBulanVertikal1.TanggalAkhir.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Jabatan, 10, 30, yPos, setengah, stringFormat, false, false, true);
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Jabatan, 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                    yPos = yPos + 30;
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Nama, 10, 30, yPos, setengah, stringFormat, false, true, true);
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true, true);

                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + pimpinan.NIP, 10, 30, yPos, setengah, stringFormat, false);
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + bendahara.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);

                    SudahCetakKesimpulan = true;
                }


            }






            previousPage = args.Page;
        }

        private void cmdCari_Click(object sender, EventArgs e)
        {

        try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridRincianObjek.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value == DBNull.Value || cell.Value == null)
                            continue;
                        if (cell.Value.ToString().ToUpper().Contains(txtCari.Text.Trim().ToUpper()) && cell.Visible == true)
                        {
                            containingCells.Add(cell);
                        }
                    }
                }
                if (containingCells.Count > 0)
                    gridRincianObjek.CurrentCell = containingCells[currentContainingCellListIndex++];
                else
                    MessageBox.Show("Tidak diketemukan");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmdCariLagi_Click(object sender, EventArgs e)
        {
            if (containingCells.Count > 0 && currentContainingCellListIndex < containingCells.Count)
                gridRincianObjek.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_IDSKPD = pIDSKPD;
            m_UnitAnggaran = ctrlDinas1.UnitAnggaran;
            ctrlProgramKegiatan1.Create(m_IDSKPD,m_UnitAnggaran);
        }

        private void ctrlProgramKegiatan1_OnChanged(int pIDurusan, 
            int pIDProgram, 
            int pIDKegiaan, 
            long pIDSubKegiatan)
        {
            ctrlPilihanRekeningAnggaran1.Create(m_IDSKPD,
                                            pIDurusan,
                                            pIDProgram,
                                            pIDKegiaan,
                                            pIDSubKegiatan,m_UnitAnggaran, 2,3);


        
            
        }

        private void chkPilihProgram_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPilihProgram.Checked == true)
            {
                ctrlPilihanRekeningAnggaran1.Enabled = true;
                ctrlProgramKegiatan1.Enabled = true;

            }
            else
            {
                ctrlPilihanRekeningAnggaran1.Enabled = false ;
                ctrlProgramKegiatan1.Enabled = false;

            }
        }

        private void ctrlTanggalBulanVertikal1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlProgramKegiatan1_Load(object sender, EventArgs e)
        {

        }
    }
}
/*

 * CREATE FUNCTION dbo.fnRincianObjek D:\Development\ASETRUNNING\SIKUAT\KUAPPAS\ctrlJenisAnggaran.cs
(	
	-- Add the parameters for the function here
	@dinas int,
	@tanggal DateTime
)
RETURNS TABLE 
AS
RETURN 
(
select tBKU.IDDInas,tBKU.cJenisBelanja, tBKU.NoBKUSKPD, tBKU.dtBukti, tBKU.sUraian , tBKU.NoBukti,
tBKURekening.IDSubKegiatan, tBKURekening.IIDrekening,
case when tBKU.cJenisBelanja< 2 Then tBKURekening.cJumlah  else 0 end as up,
case when tBKU.cJenisBelanja=2 Then tBKURekening.cJumlah  else 0 end as tu,
case when tBKU.cJenisBelanja> 3 Then tBKURekening.cJumlah  else 0 end as ls
from tBKU inner join tBKURekening 
on tBKU.inourut= tBKURekening.inourut
where tBKU.IDDInas =@dinas and tBKU.dtBukti<=@tanggal
)
GO
*/