using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP.Bendahara;
using BP;


using Formatting;
using DTO.Bendahara;
using DTO;
using System.IO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System.Threading.Tasks;
using System.Diagnostics;


namespace KUAPPAS.Bendahara
{
    public partial class frmBKU : ChildForm
    {
        private int m_iIDDInas;
        private decimal m_sJumlahPenerimaan;
        private decimal m_sJumlahPengeluaran;
        private int m_iKodeUK;
        PdfLayoutResult layoutResult = null;
        int columnCount;
        int count = 0;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;
       string[] headerValues = { "OrderID", "CustomerID", "ShipName", "ShipAddress", "ShipCity", "ShipPostalCode" };
        bool isNewPage;
        PdfPage previousPage;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        private int miJenisBendahara;
        private decimal m_cSaldoAwal;
        private int m_iBankSaldoAwal;
        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
         List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
         int m_rowSelected = 0;
        int currentContainingCellListIndex;

        public frmBKU()
        {
            InitializeComponent();
            miJenisBendahara = 0;
            m_cSaldoAwal = 0;
            m_iBankSaldoAwal = 0;
        

            _hilightstyle.Font = new System.Drawing.Font(gridBKU.Font, FontStyle.Regular);
            _hilightstyle.ForeColor = Color.Black;
            _hilightstyle.BackColor = Color.Lavender;

        }

        private void frmBKU_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Buku Kas Umum");
            ctrlDinas1.Create();
            gridBKU.FormatHeader();
            cmbSUmber.Items.Clear();
            ListItemData li = new ListItemData("Semua", 0);
            cmbSUmber.Items.Add(li);
            ctrlTanggal1.Tanggal = DateTime.Now.Date;

            if (GlobalVar.Pengguna.IsUserDinas != 0)
            {
                chksemuaDinas.Checked = false;
                chksemuaDinas.Visible = false;

            }
            else
            {
                
                chksemuaDinas.Visible = true ;
            }
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                m_iIDDInas = GlobalVar.Pengguna.SKPD;
                ctrlDinas1.SetID(m_iIDDInas);
            }
            li = new ListItemData("S P 2 D", (int )E_JENIS_REFERENSIBKU.REFERENSI_SP2D );
            cmbSUmber.Items.Add(li);

            li = new ListItemData("Penerimaan/STS", (int)E_JENIS_REFERENSIBKU.REFERENSI_TSTS );
            cmbSUmber.Items.Add(li);    

            li = new ListItemData("Penyetoran Penerimaan/STS Ke Kasda", (int)E_JENIS_REFERENSIBKU.REFERENSI_SETORSTS);
            cmbSUmber.Items.Add(li);
            li = new ListItemData("Potongan Pajak SP2D", (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSP2D );
            cmbSUmber.Items.Add(li);
            li = new ListItemData("Belanja BPK/SPJ", (int)E_JENIS_REFERENSIBKU.REFERENSI_PENGELUARANLANGSUNG );
            cmbSUmber.Items.Add(li);
            li = new ListItemData("Panjar", (int)E_JENIS_REFERENSIBKU.REFERENSI_PANJAR );
            cmbSUmber.Items.Add(li);
            
            li = new ListItemData("Pertanggung Jawaban Panjar", (int)E_JENIS_REFERENSIBKU.REFERENSI_PERTANGGUNGJAWABANPANJAR);
            cmbSUmber.Items.Add(li);
            
            li = new ListItemData("Pungutan Pajak SPJ/BPK", (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSPJPANJAR);
            cmbSUmber.Items.Add(li);
            li = new ListItemData("Penyetoran Pajak", (int)E_JENIS_REFERENSIBKU.REFERENSI_PENYETORANPAJAK);
            cmbSUmber.Items.Add(li);
            li = new ListItemData("Pencairan Bank", (int)E_JENIS_REFERENSIBKU.REFERENSI_BANK );
            cmbSUmber.Items.Add(li);
            li = new ListItemData("Koreksi Belanja", (int)E_JENIS_REFERENSIBKU.REFERENSI_KOREKSI);
            cmbSUmber.Items.Add(li);
            li = new ListItemData("Pengembalian Belanja", (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP);
            cmbSUmber.Items.Add(li);
            

            cmbBank.Items.Clear();
            cmbBank.Items.Add("Semua");
            cmbBank.Items.Add("Tunai");
            cmbBank.Items.Add("Transfer");
            cmbBank.SelectedIndex = 0;

            ctrlTanggalBulanVertikal1.Create();

           
        }
        private bool GetSaldoAwal()
        {
            try
            {
                int iddinas = ctrlDinas1.GetID();

                BKULogic oBKULogic = new BKULogic(GlobalVar.TahunAnggaran);
                BKU saldoBKU = new BKU();
                saldoBKU = oBKULogic.GetBKUSaldoAwal(iddinas, miJenisBendahara);
                if (saldoBKU != null)
                {
                    m_cSaldoAwal = saldoBKU.Jumlah;
                    m_iBankSaldoAwal = saldoBKU.Kodebank;


                }
                else
                {
                    m_cSaldoAwal = 0;
                    m_iBankSaldoAwal = 0; 

                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public int JenisBendahara
        {
            set { miJenisBendahara = value ;}
        }
        private void cmdPanggilBKU_Click(object sender, EventArgs e)
        {


        }
       

        private void cmdCetakBKU_Click(object sender, EventArgs e)
        {

        }
        private void GetTanggal()
        {
            mTanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
            mTanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;

        }
        private void Panggil()
        {
            try
            {
                BKULogic oLogic = new BKULogic((int)GlobalVar.TahunAnggaran);
                if (chksemuaDinas.Checked == true)
                {
                    m_iIDDInas = 0;
                }
                else
                {
                    m_iIDDInas = ctrlDinas1.GetID();
                    m_iKodeUK = ctrlDinas1.GetKodeUK();
                }
                GetTanggal();
                DateTime tanggalawal = mTanggalAwal;
                DateTime tanggalakhir = mTanggalAkhir;
                GetSaldoAwal();
                
                txtSaldo.Text = m_cSaldoAwal.ToRupiahInReport();
                decimal saldoTahunLalu = m_cSaldoAwal;
                List<BKUDISPLAY> lst = new List<BKUDISPLAY>();
                List<int> lstJenisSumber = new List<int>();
                DateTime tanggalAwalTahun = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                long noUrutSumber = DataFormat.GetLong(txtNoUrutSUmber.Text);
                

                ListItemData li =  (ListItemData)cmbSUmber.SelectedItem;
               
                if (li != null)
                {
                    if (cmbSUmber.Text.Trim().Length > 0)
                    {
                        if (li.Itemdata > 0)
                            lstJenisSumber.Add(li.Itemdata);
                    }

                }
                int bank = cmbBank.SelectedIndex - 1;
                lst = oLogic.GetBKU(m_iIDDInas, miJenisBendahara, tanggalAwalTahun, tanggalakhir, m_iKodeUK, lstJenisSumber, noUrutSumber,bank);
                
                gridBKU.Rows.Clear();
                if (lst == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return;
                }
                decimal saldoTerima = lst.Where(x => x.Tanggal <mTanggalAwal &&
                 x.JenisBendahara == miJenisBendahara && x.Debet == 1  && x.Level == 1).Sum(s => s.Jumlah);
                decimal saldoKeluar = lst.Where(x => x.Tanggal < mTanggalAwal &&
                    x.JenisBendahara == miJenisBendahara && x.Debet ==-1   && x.Level == 1 ).Sum(s => s.Jumlah);
                m_cSaldoAwal = m_cSaldoAwal + saldoTerima - saldoKeluar;
                txtSaldo.Text = m_cSaldoAwal.ToRupiahInReport();

                decimal Terima = lst.Where(x => x.Tanggal >= mTanggalAwal && x.Tanggal <= mTanggalAkhir  &&
                 x.JenisBendahara == miJenisBendahara && x.Debet ==1 &&  x.Level == 1 ).Sum(s => s.Jumlah);
                decimal Keluar = lst.Where(x => x.Tanggal >= mTanggalAwal && x.Tanggal <= mTanggalAkhir &&
                    x.JenisBendahara == miJenisBendahara && x.Debet == -1 && x.Level == 1 ).Sum(s => s.Jumlah);

                txtPenerimaanKini.Text = (m_cSaldoAwal+ Terima).ToRupiahInReport();
                txtPenerimaanLalu.Text = saldoTerima.ToRupiahInReport();

                txtPengeluaranLalu.Text = saldoKeluar.ToRupiahInReport();
                txtPengeluaranKini.Text = Keluar.ToRupiahInReport();

                txtSaldoAkhir.Text = (m_cSaldoAwal + Terima - Keluar).ToRupiahInReport();

                decimal saldoBank = 0L;
                decimal saldoTunai = 0L;
                if (miJenisBendahara == 1)
                {
                    saldoBank = 0;// lst.Where(x => x.Tanggal <= mTanggalAkhir &&
                        // x.JenisBendahara == miJenisBendahara && x.Level == 1 && x.Bank == 0).Sum(s => s.Debet * s.Jumlah);
                     saldoTunai = lst.Where(x => x.Tanggal <= mTanggalAkhir &&
                        x.JenisBendahara == miJenisBendahara && x.Level == 1 && x.Bank == 0 && x.Debet ==1).Sum(s => s.Jumlah);
                      decimal setor= lst.Where(x => x.Tanggal <= mTanggalAkhir &&
                        x.JenisBendahara == miJenisBendahara && x.Level == 1 && x.JenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_SETORSTS).Sum(s =>  s.Jumlah);
                      saldoTunai = saldoTunai - setor;

                      BKUDISPLAY bkumasalah = lst.FirstOrDefault(x => x.Tanggal <= mTanggalAkhir &&
                        x.JenisBendahara == miJenisBendahara && x.Level == 1 && x.Bank == 0 && x.Debet == 1);

                }
                else
                {
                     saldoBank =  lst.Where(x => x.Tanggal <= mTanggalAkhir &&
                         x.JenisBendahara == miJenisBendahara && x.Level == 1 && x.Bank == 1).Sum(s => s.Debet * s.Jumlah);
                     saldoBank = saldoTahunLalu + saldoBank;
                     saldoTunai = lst.Where(x => x.Tanggal <= mTanggalAkhir &&
                        x.JenisBendahara == miJenisBendahara && x.Level == 1 && x.Bank == 0).Sum(s => s.Debet * s.Jumlah);


                }
                

                txtTunai.Text = saldoTunai.ToRupiahInReport();
                txtBank.Text = saldoBank.ToRupiahInReport();

                
                decimal RunningSaldo = m_cSaldoAwal;
                string[] rowSaldo = { "1", "0", ", ", " .", " .", "Saldo Awal", m_cSaldoAwal.ToRupiahInReport(), ".", RunningSaldo.ToRupiahInReport(), ""};
                gridBKU.Rows.Add(rowSaldo);
                int i = 1;
                if (lst != null)
                {


                    foreach (BKUDISPLAY b in lst)
                    {
                        if (b.NoUrut == 245020100020000281)
                        {
                            MessageBox.Show(b.NoBukti);
                        }
                            if (b.NoUrut == 242120100172100370)
                            {
                              //  MessageBox.Show("Tet");
                            }
                       
                        if (b.Tanggal >= mTanggalAwal)
                        {

                            if (b.Level == 1)
                            {
                                if (b.LevelTampilan == 1)
                                {// 0,1
                                    RunningSaldo = RunningSaldo + b.Penerimaan - b.Pengeluaran;

                                    string[] row = { (++i).ToString(), b.NoBkUSKPD.ToString(), b.NoBukti, b.Tanggal.ToString("dd MMM"),
                                                       b.IDDinas.ToKodeDinas(), b.Uraian, b.Penerimaan.ToRupiahInReport(), b.Pengeluaran.ToRupiahInReport(), RunningSaldo.ToRupiahInReport(), b.Bank.ToString(),b.NoUrutSumber.ToString(),b.JenisSumber.ToString() };
                                    gridBKU.Rows.Add(row);

                                }
                                else
                                {

                                    string[] row2 = { (++i).ToString(), b.NoBkUSKPD.ToString(), b.NoBukti, b.Tanggal.ToString("dd MMM"), b.IDDinas.ToKodeDinas(), b.Uraian, "", "", RunningSaldo.ToRupiahInReport(), b.Bank.ToString(), b.NoUrutSumber.ToString(),b.JenisSumber.ToString()};
                                    gridBKU.Rows.Add(row2);
                                }


                            }
                            else
                            {
                                RunningSaldo = RunningSaldo + b.Penerimaan - b.Pengeluaran;
                                if (b.IdSubKegiatan > 0)
                                {
                                    string[] rowLevelA2 = { "", "", "", "", b.IdSubKegiatan.ToKodeSubKegiatan() + "-" + b.IDRekening.ToKodeRekening(), b.Uraian, b.Penerimaan.ToRupiahInReport(), b.Pengeluaran.ToRupiahInReport(), RunningSaldo.ToRupiahInReport(), b.Bank.ToString() };
                                    gridBKU.Rows.Add(rowLevelA2);
                                }
                                else
                                {
                                    string[] rowLevelB2 = { "", "", "", "", b.IDRekening.ToKodeRekening(), b.Uraian, b.Penerimaan.ToRupiahInReport(), b.Pengeluaran.ToRupiahInReport(), RunningSaldo.ToRupiahInReport(), b.Bank.ToString() };
                                    gridBKU.Rows.Add(rowLevelB2);
                                }
                            }

                        }
                       
                    }
                    foreach (DataGridViewRow row in gridBKU.Rows)
                    {
                        if (row.Cells[9].Value != null)
                        {
                            if (DataFormat.GetInteger(row.Cells[9].Value) == 1)
                            {
                                
                                    row.DefaultCellStyle = _hilightstyle;
                                
                            }
                        }
                    }
                }
        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void Pages_PageAdded(object sender, PageAddedEventArgs args)
        {
            float yPos = PosisiTerakhir + 5;

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;
            PdfStringFormat stringFormat = new PdfStringFormat();

       
            //stringFormat.Alignment = PdfTextAlignment.Right ;
            CetakPDF oCetakPDF = new CetakPDF();
           

         
           // oCetakPDF.TulisItem(previousPage.Graphics, "Halaman " + halaman.ToString(), 8, posisiTengah, previousPage.GetClientSize().Height-5,
            //setengah-3, stringFormat,false,false);
            //halaman++;

            if (SaatnyacetakKesimpulan == true)
            {

                stringFormat.Alignment = PdfTextAlignment.Left;
                 yPos =oCetakPDF.TulisItem(previousPage.Graphics, "Jumlah Bulan ini ", 8, 330, yPos, 240,stringFormat,false,false);

                 stringFormat.Alignment = PdfTextAlignment.Right;
                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtPenerimaanKini.Text, 8, 570, yPos, 85, stringFormat, false, false);

                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtPengeluaranKini.Text , 8, 655, yPos, 85, stringFormat, true, false);


                 stringFormat.Alignment = PdfTextAlignment.Left;
                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Jumlah Bulan Lalu ", 8, 330, yPos, 240, stringFormat, false , false);

                 stringFormat.Alignment = PdfTextAlignment.Right;
                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtPenerimaanLalu.Text, 8, 570, yPos, 85, stringFormat, false, false);

                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtPengeluaranLalu.Text, 8, 655, yPos, 85, stringFormat, true, false);
                 stringFormat.Alignment = PdfTextAlignment.Left;
                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Sampai Bulan Ini", 8, 330, yPos, 240, stringFormat, false, false);

                 stringFormat.Alignment = PdfTextAlignment.Right;


                 decimal JumlahPenerimaan = DataFormat.FormatUangReportKeDecimal(txtPenerimaanKini.Text) + DataFormat.FormatUangReportKeDecimal(txtPenerimaanLalu.Text);
                 decimal JumlahPengeluaran = DataFormat.FormatUangReportKeDecimal(txtPengeluaranLalu.Text) + DataFormat.FormatUangReportKeDecimal(txtPengeluaranKini.Text);
                 decimal saldoawal = DataFormat.FormatUangReportKeDecimal(txtSaldo.Text);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, JumlahPenerimaan.ToRupiahInReport(), 8, 570, yPos, 85, stringFormat, false, false);

                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, JumlahPengeluaran.ToRupiahInReport(), 8, 655, yPos, 85, stringFormat, true, false);

                 PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);
                 yPos = yPos + 2;
                 previousPage.Graphics.DrawLine(pen, 330, yPos, 740, yPos);
                 yPos = yPos + 2;
                 stringFormat.Alignment = PdfTextAlignment.Left;
                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Sisa", 8, 330, yPos, 240, stringFormat, false, false);
                 stringFormat.Alignment = PdfTextAlignment.Right;
                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtSaldoAkhir.Text, 8, 655, yPos, 85, stringFormat, true, false);
                 //yPos = oCetakPDF.TulisItem(previousPage.Graphics, (saldoawal+ JumlahPenerimaan - JumlahPengeluaran).ToRupiahInReport(), 8, 655, yPos, 85, stringFormat, true, false);







                 stringFormat.Alignment = PdfTextAlignment.Left;
                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Pada hari ini: Tanggal " + ctrlTanggalBulanVertikal1.TanggalAkhir.ToTanggalIndonesia() + " diPeroleh sisa kas sebesar: " + txtSaldoAkhir.Text, 8, 30, yPos, 500, stringFormat, true, false);
                 if (txtSaldoAkhir.Text.Contains("-") || txtSaldoAkhir.Text.Contains("("))
                 {
                     decimal sisasaldo = -1 * (DataFormat.FormatUangReportKeDecimal(txtSaldoAkhir.Text.Replace("-", "").Replace("(", "").Replace(")", "")));
                     yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                      "Minus   " + ((-1 ) * sisasaldo).Terbilang(), 8, 30, yPos, 500, stringFormat, true, false);

                 }
                 else
                 {
                     decimal sisasaldo = DataFormat.FormatUangReportKeDecimal(txtSaldoAkhir.Text);
                     yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                         sisasaldo.Terbilang(), 8, 30, yPos, 500, stringFormat, true, false);

                 }


                 Pejabat bendahara = new Pejabat ();

                 Pejabat pimpinan = new Pejabat();
                 if (miJenisBendahara == 2)
                 {
                     bendahara = ctrlDinas1.GetBendaharaPengeluaran(ctrlTanggal1.Tanggal);
                     pimpinan = ctrlDinas1.GetPimpinan(ctrlTanggal1.Tanggal);
                 }
                 else
                 {
                     bendahara = ctrlDinas1.GetBendaharaPenerimaan(ctrlTanggal1.Tanggal);
                     pimpinan = ctrlDinas1.GetKuaaPenggunaAnggaranPenerimaan(ctrlTanggal1.Tanggal);
                 
                 }
                 if (miJenisBendahara == 2)
                 {
                     yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Kas di Bendahara Pengeluaran:", 8, 30, yPos, 240, stringFormat, true, false);
                 }
                 else
                 {
                     yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Kas di Bendahara Penerimaan:", 8, 30, yPos, 240, stringFormat, true, false);
                 }
                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Saldo Bank ", 8, 30, yPos, 240, stringFormat, false, false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 140, yPos, 5, stringFormat, false, false);
                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtBank.Text , 8, 145, yPos, 85, stringFormat, true , false);
                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Saldo Tunai", 8, 30, yPos, 240, stringFormat, false , false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 140, yPos, 5, stringFormat, false, false);
                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtTunai.Text, 8, 145, yPos, 85, stringFormat, true, false);
                yPos = yPos + 10;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.gPemda.Ibukota + "," + ctrlTanggal1.Tanggal.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
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
            int baris = 0;
            int barisLuar = 0;
            int nobku = 0;
            try
            {

                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                section1.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section1.PageSettings.Height = 935;// = PdfPageSize.Legal;

                section1.PageSettings.Orientation = PdfPageOrientation.Landscape;
                document.PageSettings.Margins.Bottom = 0;
                PdfPage page = section1.Pages.Add();
                previousPage = page;
                document.Pages.PageAdded += Pages_PageAdded;
                //halaman = 1;
                SaatnyacetakKesimpulan = false;
                ///*
                ///Header
                ///
                PdfBorders border = new PdfBorders();
                border.All = PdfPens.Blue;
                PdfPen pen= new PdfPen(Color.Gray,1);
                float width= 2;
                //border.Bottom.Width= width;

                PdfGridCellStyle rowStyle = new PdfGridCellStyle();
               // rowStyle.Borders = border;
               


                //PdfGrid headerGrid = new PdfGrid();
                //List<object> dataHeader = new List<object>();
                CetakPDF oCetakPDF = new CetakPDF();
                float yPos;
                float kiri = 30;
                barisLuar++;
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                    page.GetClientSize().Width, stringFormat, true, false, true);

               
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "BUKU KAS UMUM " +
                         GlobalVar.TahunAnggaran.ToString(), 10, kiri, yPos,
                         page.GetClientSize().Width, stringFormat, true, false, true);
                stringFormat.Alignment = PdfTextAlignment.Left;

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, 
                          ctrlDinas1.GetNamaSKPD(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);
                
                Pejabat bendahara= new Pejabat();
                if (miJenisBendahara==1){
                    bendahara = ctrlDinas1.GetBendaharaPenerimaan(mTanggalAkhir);
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Bendahara Penerimaan "
                    , 10, kiri, yPos,
                    page.GetClientSize().Width, stringFormat, false, false, true);
                }
                if (miJenisBendahara == 2)
                {
                    bendahara = ctrlDinas1.GetBendaharaPengeluaran(mTanggalAkhir);


                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Bendahara Pengeluaran "
                              , 10, kiri, yPos,
                              page.GetClientSize().Width, stringFormat, false, false, true);
                }
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
                
                barisLuar++;
                
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                stringFormat.CharacterSpacing = 2f;
         

                PdfGrid pdfGrid = new PdfGrid();

                count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("No BKU");
                table.Columns.Add("Tanggal");
                table.Columns.Add("Nomor Bukti");
                table.Columns.Add("Kode Rekening");
                table.Columns.Add("Uraian");
                table.Columns.Add("Penerimaan");
                table.Columns.Add("Pengeluaran");
                table.Columns.Add("Saldo");
               //table. Columns[0]
                //Assign Column count
                barisLuar++;
                columnCount = table.Columns.Count;
                List<object> data = new List<object>();
            
                for (int idx = 0; idx < gridBKU.Rows.Count; idx++)
                // for (int idx =0 ; idx <=0; idx++)
                    {
                        if (gridBKU.Rows[idx].Cells[7].Value != null)
                        {
                            baris = idx;
                             if (DataFormat.GetInteger(gridBKU.Rows[idx].Cells[1].Value )>0){
                                 nobku = DataFormat.GetInteger(gridBKU.Rows[idx].Cells[1].Value);
                            }
                            //string[] row = { (++i).ToString(), b.NoBkU.ToString(), b.NoBukti, b.Tanggal.ToString("dd MMM"), b.IDKegiatan.ToKodeKegiatan(), b.Uraian, b.Penerimaan.ToRupiahInReport(), b.Pengeluaran.ToRupiahInReport() };
                            table.Rows.Add(new string[]
                            {
                                DataFormat.GetString(gridBKU.Rows[idx].Cells[1].Value).Replace(System.Environment.NewLine, "").Replace("\r\n"," ").Replace (" – ","-").ReplaceUnicode(),
                                DataFormat.GetString(gridBKU.Rows[idx].Cells[3].Value).Replace(System.Environment.NewLine, "").Replace("\r\n"," ").Replace (" – ","-").ReplaceUnicode(),
                                DataFormat.GetString(gridBKU.Rows[idx].Cells[2].Value).Replace("2024"," 2024").ReplaceUnicode(),
                                DataFormat.GetString(gridBKU.Rows[idx].Cells[4].Value).Replace(System.Environment.NewLine, "").Replace("\r\n"," ").Replace (" – ","-").ReplaceUnicode(),
                                
                                
                                
                                DataFormat.GetString(gridBKU.Rows[idx].Cells[5].Value).Replace(System.Environment.NewLine, "").Replace("\r\n"," ").Replace (" – ","-").ReplaceUnicode(),
                                DataFormat.GetString(gridBKU.Rows[idx].Cells[6].Value).Replace(System.Environment.NewLine, "").Replace("\r\n"," ").Replace (" – ","-").ReplaceUnicode(),
                               DataFormat.GetString(gridBKU.Rows[idx].Cells[7].Value).Replace(System.Environment.NewLine, "").Replace("\r\n"," ").Replace (" – ","-").ReplaceUnicode(),
                               DataFormat.GetString(gridBKU.Rows[idx].Cells[8].Value).Replace(System.Environment.NewLine, "").Replace("\r\n"," ").Replace (" – ","-").ReplaceUnicode(),

                            
                            });
                        }


                    }

                barisLuar++;
                //Add list to IEnumerable.
                pdfGrid.DataSource = table   ; //data

               
                pdfGrid.Columns[0].Width = 40;
                pdfGrid.Columns[1].Width = 50;
                pdfGrid.Columns[2].Width = 90;
                pdfGrid.Columns[3].Width = 150;
                pdfGrid.Columns[4].Width = 240;
                pdfGrid.Columns[5].Width = 85;
                pdfGrid.Columns[6].Width = 85;
                pdfGrid.Columns[7].Width = 85;
                pdfGrid.RepeatHeader = true;
                //pdfGrid.BeginCellLayout += PdfGrid_BeginCellLayout;
                barisLuar++;


                rowStyle.Borders = border;
               
                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;
                for (int col = 5; col < 8; col++)
                    pdfGrid.Columns[col].Format = formatKolomAngka;

                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));
                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();
                barisLuar++;
                pdfGrid.RepeatHeader = true;

                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;
                barisLuar++;
                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", font.Size, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));
                cellHeaderStyle.Font = font;
                cellHeaderStyle.StringFormat = stringFormatHeader;
                for (int c = 0; c < pdfGrid.Headers.Count; c++)
                {
                    pdfGrid.Headers[c].ApplyStyle(cellHeaderStyle);
                    pdfGrid.Headers[c].Height = 50;

                }
                barisLuar++;
                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5F,DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));

                pdfGrid.Style = gridStyle;

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                cellStyle.Borders.All = new PdfPen(new PdfColor(192, 192, 217), 0.50f);
                cellStyle.CellPadding = new PdfPaddings(10, 1, 1, 1);

                cellStyle.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8, FontStyle.Regular));
                cellStyle.Borders.Bottom.Width = 0.1F;
                cellStyle.Borders.Top.Width = 0.1F;

                barisLuar++;
                for (int idx = 0; idx < pdfGrid.Rows.Count; idx++)
                {
                    pdfGrid.Rows[idx].Style = cellStyle;
                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.1F;


                    }           

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.
                barisLuar++;
                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos));
                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();

          
                
                barisLuar++;
                //dataTable = data;

                //Assign data source.
                //ttdpdfGrid.DataSource = data; // Table;

                //ttdpdfGrid.Columns[0].Width = page.GetClientSize().Width / 2;
                //ttdpdfGrid.Columns[1].Width = page.GetClientSize().Width / 2;
                //ttdpdfGrid.Headers.Clear();
                //barisLuar++;
                //font = new PdfTrueTypeFont(new Font("Arial", 10));
                //cellStyle = new PdfGridCellStyle();
                //cellStyle.Borders.All = new PdfPen(Color.Transparent);
                //cellStyle.Font = font;

                //barisLuar++;
                //for (int i = 0; i < ttdpdfGrid.Rows.Count; i++)
                //{
                //    PdfGridRow row = ttdpdfGrid.Rows[i];
                //    for (int j = 0; j < row.Cells.Count; j++)
                //    {
                //        PdfGridCell cell = row.Cells[j];
                //        cell.Style = cellStyle;

                //    }
                //}
                //stringFormat = new PdfStringFormat();
                //stringFormat.Alignment = PdfTextAlignment.Center;
                //stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                //stringFormat.CharacterSpacing = 2f;
                //ttdpdfGrid.Columns[0].Format = stringFormat;
                //ttdpdfGrid.Columns[1].Format = stringFormat;
                //barisLuar++;
                //ttdpdfGrid.Draw(page, new PointF(10, pdfGridLayoutResult.Bounds.Bottom + 50));

                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../BKU.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                barisLuar++;
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../BKU.pdf");
                pV.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "   " + barisLuar.ToString() + " bku:  " + nobku.ToString() + "   " + baris.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {





        }
     

        private void button2_Click(object sender, EventArgs e)
        {
          

                PdfDocument doc = new PdfDocument();
                PdfPage page = doc.Pages.Add();
                PdfGrid grid = new PdfGrid();
                grid.Columns.Add(6);
                grid.Headers.Add(1);
                //Get the length of largest string 
                var length = 0;
                var longestString = string.Empty;
                for (var i = 0; i < headerValues.Length; i++)
                {
                    if (headerValues[i].Length > length)
                    {
                        length = headerValues[i].Length;
                        longestString = headerValues[i];
                    }
                }
                //Create a DataTable
                DataTable dataTable1 = new DataTable();
                //Add columns to the DataTable
                for (int i = 0; i < 6; i++)
                    dataTable1.Columns.Add();
                //Add rows to the DataTable
                dataTable1.Rows.Add(new string[] { "10248", "VINET", "Vins et alcools Chevalier", "59 rue de l'Abbaye", "Reims", "51100" });
                dataTable1.Rows.Add(new string[] { "10249", "TOMSP", "Toms Spezialitäten", "Luisenstr. 48", "Münster", "44087" });
                dataTable1.Rows.Add(new string[] { "10250", "HANAR", "Hanari Carnes", "Rua do Paço, 67", "Rio de Janeiro", "05454-876" });
                dataTable1.Rows.Add(new string[] { "10251", "VICTE", "Victuailles en stock", "2, rue du Commerce", "Lyon", "69004" });
                dataTable1.Rows.Add(new string[] { "10252", "SUPRD", "Suprêmes délices", "Boulevard Tirou, 255", "Charleroi", "B-6000" });
                dataTable1.Rows.Add(new string[] { "10253", "HANAR", "Hanari Carnes", "Rua do Paço, 67", "Rio de Janeiro", "05454-876" });
                //Assign data source
                grid.DataSource = dataTable1;
                //Measure the font size with longest string
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 6);
                SizeF size = font.MeasureString(longestString);
                grid.Headers[0].Height = size.Width * 2;
                //Repeat the header 
                grid.RepeatHeader = true;
                grid.BeginPageLayout += new BeginPageLayoutEventHandler(grid_BeginPageLayout);
                grid.BeginCellLayout += new PdfGridBeginCellLayoutEventHandler(BeginCellEvent);
                grid.Draw(page, new PointF(0, 0));
                doc.Save("DrawRotatedHeader.pdf");
                doc.Close(true);
                System.Diagnostics.Process.Start("DrawRotatedHeader.pdf");
            }
 
            //Event triggers for new beginning of the page
            private void grid_BeginPageLayout(object sender, BeginPageLayoutEventArgs e)
            {
                isNewPage = true;
            }
 
            //Event trigger for beginning of the cell
            private void BeginCellEvent(object sender, PdfGridBeginCellLayoutEventArgs args)
            {
                PdfGrid grid = (PdfGrid)sender;
                if (isNewPage)
                {
                    grid.Headers[0].Cells[args.CellIndex].Value = string.Empty;
                    args.Graphics.Save();
                    args.Graphics.TranslateTransform(args.Bounds.X, args.Bounds.Height);
                    args.Graphics.RotateTransform(-90);
                    //Draw the text at particular bounds
                    args.Graphics.DrawString(headerValues[args.CellIndex], new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold), PdfBrushes.Red, new PointF(0, 0));
                    args.Graphics.Restore();
                    //If all columns drawn
                    if (args.CellIndex == grid.Columns.Count - 1)
                        isNewPage = false;
                }
            }
           

            private void cmdPanggilData_Click(object sender, EventArgs e)
            {
                if (ctrlTanggalBulanVertikal1.CekPilihan() == true)
                {
                    GetTanggal();
                }
                else
                    return ;

                Panggil();
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
                string namaFile="";
                namaFile = BuatFile();
                
                KillSpecificExcelFileProcess(namaFile);
                if (namaFile.Length == 0)
                {
                    return;
                }
                try
                {
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objWorkSheet);
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objWorkBook);
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objWorkBooks);
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objAppln);
                    //_objWorkSheet = null; _objWorkBooks = null; _objWorkBooks = null; _objAppln = null;


                    Microsoft.Office.Interop.Excel.Application excel;
                    Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                    Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                    Microsoft.Office.Interop.Excel.Range excelCellrange;
                    // Start Excel and get Application object.
                    excel = new Microsoft.Office.Interop.Excel.Application();
                     while (excel.Interactive == true)
                        {
                            // If Excel is currently busy, try until go thru
                            excel.Interactive = false;
                        }

                    // Make Excel invisible and disable alerts.
                    excel.Visible = false;
                    excel.DisplayAlerts = false;

                    // Create a new Workbook.
                    excelworkBook = excel.Workbooks.Add(Type.Missing);
                    
                    // Create a Worksheet.
                    excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;

                    excelSheet.Name = "BKU";
                    for (int row = 0; row < gridBKU.Rows.Count; row++)
                    {
                        for (int col = 0; col < gridBKU.Columns.Count; col++)
                        {
                            if (col >= 6)
                            {
                                if (DataFormat.GetString(gridBKU.Rows[row].Cells[col].Value) == "")
                                {
                                    excelSheet.Cells[row + 1, col + 1] = 0;
                                }
                                else
                                {
                                    excelSheet.Cells[row + 1, col + 1] = DataFormat.GetString(gridBKU.Rows[row].Cells[col].Value).ReplaceUnicode().FormatUangReportKeDecimal(); 
                                }
                                //excelSheet.Cells.NumberFormat = "0.00";
                                //excelSheet.Cells.HorizontalAlignment = HorizontalAlignment.Right;
                            }
                            else
                            {
                                excelSheet.Cells[row + 1, col + 1] = DataFormat.GetString(gridBKU.Rows[row].Cells[col].Value).ReplaceUnicode();
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
                    
                    MessageBox.Show("Gagal export ke excell " + namaFile + "   " + ex.Message);
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
                if (sRet.Length == 0)
                {
                    MessageBox.Show("Nama File Tidak boleh kosong..");
                    return "";

                }

                //  }
                return sRet;
            }

            private void cmdPerbaikiBKU_Click(object sender, EventArgs e)
            {
                frmPerbaikiNoBKU fPerbaikiBKU = new frmPerbaikiNoBKU();
                fPerbaikiBKU.Dinas = m_iIDDInas;
                fPerbaikiBKU.TanggalAwal = mTanggalAwal;
                fPerbaikiBKU.TanggalAkhir = mTanggalAkhir;
                fPerbaikiBKU.Bulan = ctrlTanggalBulanVertikal1.Bulan;
                fPerbaikiBKU.JenisPeriode = ctrlTanggalBulanVertikal1.JenisPeriode;
                fPerbaikiBKU.JenisBendahara = miJenisBendahara;
               
                                fPerbaikiBKU.ShowDialog();

            }

            private void cmdCari_Click(object sender, EventArgs e)
            {
try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridBKU.Rows)
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
                    gridBKU.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridBKU.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void contextMenuBKU_Click(object sender, EventArgs e)
        {
           
        }

        private void contextMenuBKU_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Copy" && gridBKU.CurrentCell.Value != null)
            {
                Clipboard.SetDataObject(gridBKU.CurrentCell.Value.ToString(), false);
            }
        }

        private void gridBKU_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                gridBKU.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip = contextMenuBKU;
                gridBKU.CurrentCell = gridBKU.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void cmdCekBKU_Click(object sender, EventArgs e)
        {
            try
            {
                GetTanggal();
                m_iIDDInas = ctrlDinas1.GetID();
                if (m_iIDDInas == 0)
                {
                    MessageBox.Show("Belum Pilih Dinas..");
                    return;

                }
                frmCekBKU fPerbaikiBKU = new frmCekBKU();
                fPerbaikiBKU.Dinas = m_iIDDInas;
                fPerbaikiBKU.TanggalAwal = mTanggalAwal;
                fPerbaikiBKU.TanggalAkhir = mTanggalAkhir;
                fPerbaikiBKU.JenisBendahara = miJenisBendahara;
                fPerbaikiBKU.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnuPindahKebank_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_rowSelected >=0 || m_rowSelected <gridBKU.Rows.Count){

                    long NoUrutSumber = DataFormat.GetLong(gridBKU.Rows[m_rowSelected].Cells[10].Value);
                    int JenisSumber= DataFormat.GetInteger(gridBKU.Rows[m_rowSelected].Cells[11].Value);
                    BKULogic oBKULogic = new BKULogic(GlobalVar.TahunAnggaran);
                    if (oBKULogic.UbahKeBank(NoUrutSumber, 1))
                    {
                        switch (JenisSumber)
                        {
                            case 2:
                                STSLogic oSTSLogic = new STSLogic(GlobalVar.TahunAnggaran);
                                if (oSTSLogic.UbahKeBank(NoUrutSumber, 1)==false)
                                {
                                    MessageBox.Show(oSTSLogic.LastError());
                                    oBKULogic.UbahKeBank(NoUrutSumber, 0);
                                }
                                else
                                {
                                    MessageBox.Show("Selesai");
                                }
                                break;
                        }
                    }
                }
                
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void gridBKU_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            m_rowSelected = e.RowIndex;
        }

        private void gridBKU_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_rowSelected = e.RowIndex;
        }

        private void gridBKU_Click(object sender, EventArgs e)
        {
          
        }

        private void chksemuaDinas_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    }

        
     
