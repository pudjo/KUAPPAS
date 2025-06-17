using BP.Bendahara;
using DTO.Bendahara;
using Formatting;
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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using DTO;
using System.IO;
namespace KUAPPAS.Bendahara
{
    public partial class frmBukuKas : ChildForm
    {
        private int m_IDSKPD;
        private int m_nMode;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;
        //List<BKU> mListBKU;
        List<BKUDISPLAY> mListBKU;
        List<Pengeluaran> mListPengeluaran; 
        BKUDISPLAY mJumlahBulanLalu;
        BKUDISPLAY mJumlahBulanIni;

        CetakPDF oCetakPDF;
        PdfPage previousPage;
        int m_iJenisBendahara;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;

        public frmBukuKas(int Mode)
        {
            InitializeComponent();
            m_nMode = Mode;
            mListBKU = new List<BKUDISPLAY>();
            m_iJenisBendahara = 2;

        }
        public int JenisBendahara
        {
            set
            {
                m_iJenisBendahara = value;
            }
        }
        private void frmBukuKas_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();

            ctrlHeader1.SetCaption("Register ..");
         
            gridKas.FormatHeader();
            gridPajak.FormatHeader();
            gridPajak.FormatHeader();

            foreach(TabPage tab in tabControl1.TabPages){
                tabControl1.TabPages.Remove(tab);
            }
           label3.Visible = false;

           ctrlCombPotongan1.Visible = false;

           if (GlobalVar.Pengguna.SKPD > 0)
           {
              m_IDSKPD = GlobalVar.Pengguna.SKPD;
               ctrlDinas1.SetID(m_IDSKPD);
           }

            switch (m_nMode)
            {
                case 1:
                    tabControl1.TabPages.Add(tabPage1);
                    tabPage1.Text = "Buku Kas Bank";
                    ctrlHeader1.SetCaption ("Buku Kas Bank");
                    ctrlPencarian1.setGrid(ref gridKas);
                    break;
               case 2:
                    tabControl1.TabPages.Add(tabPage1);
                    tabPage1.Text = "Buku Kas Tunai";
                    ctrlHeader1.SetCaption ("Buku Kas Tunai");
                    ctrlPencarian1.setGrid(ref gridKas);
                    break;
               case 3:
                    tabControl1.TabPages.Add(tabPage2);
                    tabPage2.Text = "Buku Pajak";
                    ctrlHeader1.SetCaption("Buku Pajak");
                    ctrlPencarian1.setGrid(ref gridPajak);
                    ctrlCombPotongan1.Create();
                    label3.Visible = true;

           ctrlCombPotongan1.Visible = true;
                    break;
               case 4:
                    tabControl1.TabPages.Add(tabPage3);
                    tabPage3.Text = "Buku Panjar";
                    ctrlHeader1.SetCaption("Buku Panjar");
                    ctrlPencarian1.setGrid(ref gridPanjar);
                    break;
           
                
                case 5:
                    tabControl1.TabPages.Add(tabPage5);
                    ctrlHeader1.SetCaption ("Rincian Objek");
                    break;



            }
            ctrlTanggalBulanVertikal1.Create();

        }
        private void GetTanggal()
        {
            
                mTanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                mTanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
           
        }
        private bool GetBKUPajak()
        {
            try
            {
                int m_IDSKPD = ctrlDinas1.GetID();
                GetTanggal();
                BKULogic oLogic = new BKULogic(GlobalVar.TahunAnggaran);
                mListBKU = new List<BKUDISPLAY>();
                List<int> lstJenisSumber = new List<int>();
                DateTime dAwaltahun = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                List<int> lstSumber = new List<int>();
lstJenisSumber.Add(10);
               lstJenisSumber.Add(9);
                lstJenisSumber.Add(17);

               
                mListBKU = oLogic.GetBKU(m_IDSKPD, 2, dAwaltahun, mTanggalAkhir, 0, lstJenisSumber);
                
                
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool GetBKU()
        {
            try
            {
                int m_IDSKPD = ctrlDinas1.GetID();
                if (ctrlTanggalBulanVertikal1.CekPilihan()== true ){
                    GetTanggal();
                } else 
                    return false ;
                BKULogic oLogic = new BKULogic(GlobalVar.TahunAnggaran);
                mListBKU = new List<BKUDISPLAY>();
                DateTime dAwaltahun = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                List<int> lstSumber = new List<int>();

                mListBKU = oLogic.GetBKU(m_IDSKPD, 2, dAwaltahun.AddDays(-1), mTanggalAkhir , 0, lstSumber);
                if (mListBKU == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return false;

                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void cmdCariFile_Click(object sender, EventArgs e)
        {

        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
           
            if (m_nMode == 1 || m_nMode == 2)
            {
                GetBKU();
                ProsesBukuKas();
            }
            if (m_nMode == 3)
            {
                GetBKUPajak();
                ProsesBukuPajak();
            }
            if (m_nMode == 4)
            {
                GetBKUPanjar();
                ProsesPanjar();
            }
        }


        private bool ProsesBukuPajak()
        {
            try
            {
                int No = 0;

                gridKas.Rows.Clear();
                int bank = m_nMode == 1 ? 1 : 0;

                decimal saldo = mListBKU.Where(x => x.Tanggal < mTanggalAwal &&
                    x.JenisBendahara == m_iJenisBendahara && x.Level == 1).Sum(s => s.Jumlah * s.Debet);


                decimal saldoTerima = mListBKU.Where(x => x.Tanggal < mTanggalAwal &&
                    x.JenisBendahara == m_iJenisBendahara && x.Level == 1 && x.Debet == 1).Sum(s => s.Jumlah);
                decimal saldoKeluar = mListBKU.Where(x => x.Tanggal < mTanggalAwal &&
                    x.JenisBendahara == m_iJenisBendahara && x.Level == 1 && x.Debet == -1).Sum(s => s.Jumlah);

                mJumlahBulanLalu = new BKUDISPLAY();
                mJumlahBulanLalu.Uraian = "Jumlah Periode Sebelum " + mTanggalAwal.ToTanggalIndonesia();
                mJumlahBulanLalu.Penerimaan = saldoTerima;
                mJumlahBulanLalu.Pengeluaran = saldoKeluar;

                mJumlahBulanIni = new BKUDISPLAY();
                mJumlahBulanIni.Uraian = "Jumlah Periode " + mTanggalAwal.ToTanggalIndonesia() + " s/d" + mTanggalAkhir.ToTanggalIndonesia();

                txtSaldoAwal.Text = saldo.ToRupiahInReport();

                List<BKUDISPLAY> lst = new List<BKUDISPLAY>();

                //var lst = mListBKU.Where(b => b.Kodebank == bank &&
                //                b.TanggalTransaksi >= mTanggalAwal &&
                //                b.TanggalTransaksi <= mTanggalAkhir);\



                foreach (BKUDISPLAY b in mListBKU)
                {
                    // &&
                    if (b.Tanggal >= mTanggalAwal &&
                        b.JenisBendahara == m_iJenisBendahara && b.Tanggal <= mTanggalAkhir)
                    {
                        if (b.Level == 1)
                        {
                            mJumlahBulanIni.Penerimaan = mJumlahBulanIni.Penerimaan + b.Penerimaan;
                            mJumlahBulanIni.Pengeluaran = mJumlahBulanIni.Pengeluaran + b.Pengeluaran;
                        }
                        lst.Add(b);
                        //   }
                    }
                }
                decimal saldoberjalan = saldo;
                decimal penerimaan = saldo;
                decimal pengeluaran = 0L;
                gridPajak.Rows.Clear();
                string[] rowSaldo = {" " ," " ," ",
                               "Saldo Awal",saldo.ToRupiahInReport(),"",
                               "","1"};
                gridPajak.Rows.Add(rowSaldo);

                foreach (BKUDISPLAY b in lst)
                {


                    penerimaan = b.Level == 1 ? 0 : b.Penerimaan;
                    pengeluaran = b.Level == 1 ? 0 : b.Pengeluaran;
                    saldoberjalan = saldoberjalan + penerimaan - pengeluaran;


                    //penerimaan = b.Debet == 1 ? b.Jumlah : 0;
                    //pengeluaran = b.Debet == -1 ? b.Jumlah : 0;


                    if (b.Level == 1)
                    {
                        string[] row = {b.NoBkUSKPD.ToString(),b.NoBukti,b.Tanggal.ToTanggalIndonesia(),
                               b.Uraian,"","",
                               "","1"};
                        gridPajak.Rows.Add(row);

                    }
                    else
                    {
                        string[] row2 = {"","","",
                               "-" + b.Uraian,b.Penerimaan.ToRupiahInReport(),b.Pengeluaran.ToRupiahInReport(),
                               saldoberjalan.ToRupiahInReport(),"2"};
                        gridPajak.Rows.Add(row2);
                    }




                }



                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private bool ProsesBukuKas()
        {
            try
            {
                int No = 0;

                gridKas.Rows.Clear();
                int bank = m_nMode == 1 ? 1 : 0;
             
                decimal saldoTerima = mListBKU.Where(x => x.Tanggal < mTanggalAwal && x.Bank== bank  && 
                    x.JenisBendahara == m_iJenisBendahara && x.Level==1 && x.Debet ==1).Sum(s => s.Jumlah );
                decimal saldoKeluar = mListBKU.Where(x => x.Tanggal < mTanggalAwal && x.Bank == bank &&
                    x.JenisBendahara == m_iJenisBendahara && x.Level == 1 && x.Debet == -1).Sum(s => s.Jumlah);
                decimal saldoAwal = saldoTerima - saldoKeluar;
                mJumlahBulanLalu = new BKUDISPLAY ();
                mJumlahBulanLalu.Uraian = "Jumlah Periode Sebelum " + mTanggalAwal.ToTanggalIndonesia();
                mJumlahBulanLalu.Penerimaan = saldoTerima;
                mJumlahBulanLalu.Pengeluaran = saldoKeluar;

                mJumlahBulanIni = new BKUDISPLAY();
                mJumlahBulanIni.Uraian = "Jumlah Periode " + mTanggalAwal.ToTanggalIndonesia() + " s/d" + mTanggalAkhir.ToTanggalIndonesia();

                txtSaldoAwal.Text = (saldoTerima - saldoKeluar).ToRupiahInReport();

                List<BKUDISPLAY> lst = new List<BKUDISPLAY>();

                //var lst = mListBKU.Where(b => b.Kodebank == bank &&
                //                b.TanggalTransaksi >= mTanggalAwal &&
                //                b.TanggalTransaksi <= mTanggalAkhir);

                foreach (BKUDISPLAY b in mListBKU)
                {
                    // &&
                    if (b.Tanggal >= mTanggalAwal && b.Bank == bank &&
                        b.JenisBendahara == m_iJenisBendahara && b.Tanggal <= mTanggalAkhir && b.Level == 1)
                    {
                        mJumlahBulanIni.Penerimaan = mJumlahBulanIni.Penerimaan + b.Penerimaan;
                        mJumlahBulanIni.Pengeluaran = mJumlahBulanIni.Pengeluaran + b.Pengeluaran;

                        lst.Add(b);
                    }
                }
                decimal saldoberjalan =0L;
                decimal penerimaan = 0L;
                decimal pengeluaran = 0L;
                gridKas.Rows.Clear();

                saldoberjalan = saldoAwal;
                
                string[] rowSaldoAwal ={(++No).ToString(),"", 
                                      "", 
                                      "", 
                                      "Saldo Awal", 
                                      saldoAwal.ToRupiahInReport(),
                                      "0",
                                       saldoAwal.ToRupiahInReport()};


                gridKas.Rows.Add(rowSaldoAwal);

                foreach (BKUDISPLAY b in lst)
                    {
                        saldoberjalan = saldoberjalan + (b.Debet * b.Jumlah);
                        penerimaan = b.Debet == 1 ? b.Jumlah : 0;
                        pengeluaran = b.Debet == -1 ? b.Jumlah : 0;

                        string[] row ={(++No).ToString(),b.NoBkUSKPD.ToString(), 
                                      b.NoBukti, 
                                      b.Tanggal.ToTanggalIndonesia(), 
                                      b.Uraian, 
                                      penerimaan.ToRupiahInReport(),
                                      pengeluaran.ToRupiahInReport(),
                                       saldoberjalan.ToRupiahInReport()};


                        gridKas.Rows.Add(row);

                    }

                

                return true; 
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool  GetBKUPanjar(){
            try
            {
                int m_IDSKPD = ctrlDinas1.GetID();
                GetTanggal();
                BKULogic oLogic = new BKULogic(GlobalVar.TahunAnggaran);
                mListBKU = new List<BKUDISPLAY>();
                DateTime dAwaltahun = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                List<int> lstSumber = new List<int>();
                //
                //(int)REFERENSI_PANJAR = 14,            
         
            //(int)REFERENSI_PERTANGGUNGJAWABANPANJAR = 6, 
           
            //(int)REFERENSI_PENGEMBALIANSISAPANJAR = 16,


                lstSumber.Add((int)E_JENIS_REFERENSIBKU.REFERENSI_PANJAR );
                lstSumber.Add((int)E_JENIS_REFERENSIBKU.REFERENSI_PENGEMBALIANSISAPANJAR);


             //   lstSumber.Add((int)E_JENIS_REFERENSIBKU.REFERENSI_PENGEMBALIANSISAPANJAR);

                mListBKU = oLogic.GetBKU(m_IDSKPD, 2, dAwaltahun, mTanggalAkhir, 0, lstSumber);

                mListPengeluaran = new List<Pengeluaran>();
                PengeluaranLogic oPengeluaranLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
                ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);
                p.TanggalAwal = mTanggalAwal;
                p.TanggalAkhir = mTanggalAkhir;
                p.IDDInas = m_IDSKPD;
                p.Jenis = 0;

                mListPengeluaran = oPengeluaranLogic.GetUntukBukuPanjar(p);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool ProsesPanjar()
        {

            try
            {
                
                gridPanjar.Rows.Clear();
                
                var PanjarDenganNama = mListBKU.Join(mListPengeluaran,
                       bku => bku.NoUrutSumber,
                       listpengeluaran => listpengeluaran.NoUrut,
                       (bku, listpengeluaran) => new BKUDenganNamaPenerima
                       { NoBKU= bku.NoBkUSKPD,

                         TanggalBKU  = bku.Tanggal,
                           Uraian = bku.Uraian,
                         Nama = listpengeluaran.Penerima,
                         Panerimaan = bku.Penerimaan,
                         Pengeluaran= bku.Pengeluaran

                       }).ToList();
                decimal saldoberjalan = 0M;

                foreach (var bkudengannama in PanjarDenganNama)
                {

                    saldoberjalan = saldoberjalan + bkudengannama.Panerimaan - bkudengannama.Pengeluaran;

                    string[] row ={bkudengannama.NoBKU.ToString(), 
                                      bkudengannama.TanggalBKU.ToTanggalIndonesia(), 
                                      bkudengannama.Uraian, 
                                      bkudengannama.Nama,
                                      bkudengannama.Panerimaan.ToRupiahInReport(),
                                      bkudengannama.Pengeluaran.ToRupiahInReport(),
                                      saldoberjalan.ToRupiahInReport()};


                    gridPanjar.Rows.Add(row);

                }



                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }


        private void cmdCetak_Click(object sender, EventArgs e)
        {
            switch (m_nMode)
            {
                case 1:
                    CetakKas(1);
                    break;
                case 2:
                    CetakKas(0);
                    break;
                case 3:
                    CetakPajak();
                    break;
                case 4:
                    CetakPanjar();
                    break;

            }
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

               



                Pejabat bendahara = new Pejabat();
                Pejabat pimpinan = new Pejabat();
                if (m_iJenisBendahara == 2)
                {
                    bendahara = ctrlDinas1.GetBendaharaPengeluaran(ctrlTanggalBulanVertikal1.TanggalAkhir);
                }
                else
                {
                    bendahara = ctrlDinas1.GetBendaharaPenerimaan(ctrlTanggalBulanVertikal1.TanggalAkhir);
                }
                pimpinan = ctrlDinas1.GetPimpinan(ctrlTanggalBulanVertikal1.TanggalAkhir);
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




            }



      


            previousPage = args.Page;
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





                Pejabat bendahara = new Pejabat();
                Pejabat pimpinan = new Pejabat();
                if (m_iJenisBendahara == 2)
                {
                    bendahara = ctrlDinas1.GetBendaharaPengeluaran(ctrlTanggalBulanVertikal1.TanggalAkhir);
                }
                else
                {
                    bendahara = ctrlDinas1.GetBendaharaPenerimaan(ctrlTanggalBulanVertikal1.TanggalAkhir);
                }
                pimpinan = ctrlDinas1.GetPimpinan(ctrlTanggalBulanVertikal1.TanggalAkhir);
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




            }






            previousPage = args.Page;
        }
        private void PagesPanjar_PageAdded(object sender, PageAddedEventArgs args)
        {
            float yPos = PosisiTerakhir + 5;

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;
            PdfStringFormat stringFormat = new PdfStringFormat();


            CetakPDF oCetakPDF = new CetakPDF();


            if (SaatnyacetakKesimpulan == true)
            {





                Pejabat bendahara = new Pejabat();
                Pejabat pimpinan = new Pejabat();
                if (m_iJenisBendahara == 2)
                {
                    bendahara = ctrlDinas1.GetBendaharaPengeluaran(ctrlTanggalBulanVertikal1.TanggalAkhir);
                }
                else
                {
                    bendahara = ctrlDinas1.GetBendaharaPenerimaan(ctrlTanggalBulanVertikal1.TanggalAkhir);
                }
                pimpinan = ctrlDinas1.GetPimpinan(ctrlTanggalBulanVertikal1.TanggalAkhir);
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




            }






            previousPage = args.Page;
        }
        private void PagesPanjar2_PageAdded(object sender, PageAddedEventArgs args)
        {
            float yPos = PosisiTerakhir + 5;
               

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;

            PdfStringFormat stringFormat = new PdfStringFormat();


            CetakPDF oCetakPDF = new CetakPDF();
            stringFormat.Alignment = PdfTextAlignment.Left;
            float posterakhir = previousPage.GetClientSize().Height;
            yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Buku Panjar " + ctrlDinas1.GetNamaSKPD() + " " + ctrlTanggalBulanVertikal1.Waktu, 6, 30, posterakhir, 510, stringFormat, false, false, true);

            

            if (SaatnyacetakKesimpulan == true)
            {


                //pdfGrid.Columns[0].Width = 40;
                //;
                //pdfGrid.Columns[1].Width = 70;
                //pdfGrid.Columns[2].Width = 430;

                //pdfGrid.Columns[3].Width = 80;
                //pdfGrid.Columns[4].Width = 65;
                //pdfGrid.Columns[5].Width = 65;
                //pdfGrid.Columns[6].Width = 65;
                float posketerangan = 40+70+200;
                float pospenerimaan=40+70+430+80;
                float pospenyetoran=40+70+430+80+65;
                //float possaldo=40+70+430+80+65+ 65;
                //float posakhir=40+70+430+80+65+ 65+ 65;

 
                decimal penerimaanSebelumPeriode = mListBKU.Where(x=>x.Tanggal< mTanggalAwal).Sum(s=>s.Penerimaan);
                decimal pengeluaranSebelumPeriode = mListBKU.Where(x=>x.Tanggal< mTanggalAwal).Sum(s=>s.Pengeluaran);
                decimal penerimaanPeriode = mListBKU.Where(x=>x.Tanggal>= mTanggalAwal &&
                                 x.Tanggal<=mTanggalAkhir).Sum(s=>s.Penerimaan);
                decimal pengeluaranPeriode = mListBKU.Where(x=>x.Tanggal>= mTanggalAwal && 
                                x.Tanggal<=mTanggalAkhir ).Sum(s=>s.Pengeluaran);




                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Jumlah Periode "+ ctrlTanggalBulanVertikal1.Waktu , 8, posketerangan, yPos, 510, stringFormat, false, false, true);
                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, penerimaanPeriode.ToRupiahInReport(), 8, pospenerimaan, yPos, 65, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pengeluaranPeriode.ToRupiahInReport(), 8, pospenyetoran, yPos, 65, stringFormat, true, false, true);

                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Jumlah Sebekum " + mTanggalAwal.ToTanggalIndonesia(), 8, posketerangan, yPos, 510, stringFormat, false, false, true);
                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, penerimaanSebelumPeriode.ToRupiahInReport(), 8, pospenerimaan, yPos, 65, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pengeluaranSebelumPeriode.ToRupiahInReport(), 8, pospenyetoran, yPos, 65, stringFormat, true, false, true);

                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Jumlah Sampai  " + mTanggalAkhir.ToTanggalIndonesia(), 8, posketerangan, yPos, 510, stringFormat, false, false, true);
                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, (penerimaanPeriode + penerimaanSebelumPeriode).ToRupiahInReport(), 8, pospenerimaan, yPos, 65, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, (pengeluaranPeriode + pengeluaranSebelumPeriode).ToRupiahInReport(), 8, pospenyetoran, yPos, 65, stringFormat, true, false, true);




                Pejabat bendahara = new Pejabat();
                Pejabat pimpinan = new Pejabat();
                if (m_iJenisBendahara == 2)
                {
                    bendahara = ctrlDinas1.GetBendaharaPengeluaran(ctrlTanggalBulanVertikal1.TanggalAkhir);
                }
                else
                {
                    bendahara = ctrlDinas1.GetBendaharaPenerimaan(ctrlTanggalBulanVertikal1.TanggalAkhir);
                }
                pimpinan = ctrlDinas1.GetPimpinan(ctrlTanggalBulanVertikal1.TanggalAkhir);
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




            }






            previousPage = args.Page;
        }
        private float CekPEnambahanTinggiTandaTanganPanjar(float yPos)
        {

            float yPoSaWAL  = yPos;
               

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;
             float posketerangan = 40+70+200;
                float pospenerimaan=40+70+430+80;
                float pospenyetoran=40+70+430+80+65;
            PdfStringFormat stringFormat = new PdfStringFormat();
            string someText ="000";

            CetakPDF oCetakPDF = new CetakPDF();
            
                stringFormat.Alignment = PdfTextAlignment.Left;

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Jumlah Periode "+ ctrlTanggalBulanVertikal1.Waktu , 8, posketerangan, yPos, 510, stringFormat, false, false, true,true );
                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "000", 8, pospenerimaan, yPos, 65, stringFormat, false, false, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 8, pospenyetoran, yPos, 65, stringFormat, true, false, true, true);

                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Jumlah Sebekum " + mTanggalAwal.ToTanggalIndonesia(), 8, posketerangan, yPos, 510, stringFormat, false, false, true, true);
                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 8, pospenerimaan, yPos, 65, stringFormat, false, false, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 8, pospenyetoran, yPos, 65, stringFormat, true, false, true, true);

                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Jumlah Sampai  " + mTanggalAkhir.ToTanggalIndonesia(), 8, posketerangan, yPos, 510, stringFormat, false, false, true, true);
                stringFormat.Alignment = PdfTextAlignment.Right;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 8, pospenerimaan, yPos, 65, stringFormat, false, false, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 8, pospenyetoran, yPos, 65, stringFormat, true, false, true, true);




                
                yPos = yPos + 10;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 10, posisiTengah, yPos, setengah, stringFormat, true, false, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 10, 30, yPos, setengah, stringFormat, false, false, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 10, posisiTengah, yPos, setengah, stringFormat, true, false, true, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 10, 30, yPos, setengah, stringFormat, false, true, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 10, posisiTengah, yPos, setengah, stringFormat, true, true, true, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 10, 30, yPos, setengah, stringFormat, false, true, true, true);
           //     yPos = oCetakPDF.TulisItem(previousPage.Graphics, someText, 10, posisiTengah, yPos, setengah, stringFormat, true, true, true);
                return yPos = yPoSaWAL;

        }
        private bool CetakKas(int bank)
        {
            int line = 0;
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
                document.Pages.PageAdded += Pages_PageAdded;
                line = 1;
                yPos = 10;
                PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);


                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;

                //stringFormat.CharacterSpacing = 2f;
                oCetakPDF = new CetakPDF();
                //SizeF size = font12.MeasureString("xxx");
                 
               
                Pejabat pimpinan = new Pejabat();
                Pejabat bendahara = new Pejabat ();
                pimpinan = ctrlDinas1.GetPimpinan(mTanggalAkhir);
                bendahara = ctrlDinas1.GetBendaharaPengeluaran(mTanggalAkhir);

                if (pimpinan == null)
                {
                    MessageBox.Show("Data Pimpinan belum di setting di master pejabat");
                    return false;

                }

                if (bendahara == null)
                {
                    MessageBox.Show("Data Bndahara belum di setting di master pejabat");
                    return false;

                }

            
                float kiri = 15;
                float posTitikdua = 150;
                float posNama = 155;
       
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                   page.GetClientSize().Width, stringFormat, true, false, true);
                line = 2;
                if (bank == 1)
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "BUKU PEMBANTU KAS BANK" 
                             , 10, kiri, yPos,
                             page.GetClientSize().Width, stringFormat, true, false, true);

                else
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "BUKU PEMBANTU KAS TUNAI"
                         , 10, kiri, yPos,
                         page.GetClientSize().Width, stringFormat, true, false, true);
                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = yPos + 20;
                line = 3;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, 
                          ctrlDinas1.GetNamaSKPD(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);

                line = 4;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Bendahara Pengeluaran "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, 
                          bendahara.Nama, 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);

                line = 5;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Periode "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
             
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, 
                          ctrlTanggalBulanVertikal1.Waktu, 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);
                line = 6;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Tahun Anggaran"
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, 
                          GlobalVar.TahunAnggaran.ToString(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);
                //MessageBox.Show("Masuk tabel");
#region gridKas
                PdfGrid pdfGrid = new PdfGrid();

                int count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("No");
                table.Columns.Add("No BKU");
                table.Columns.Add("No Bukti");
                table.Columns.Add("Tanggal");
                table.Columns.Add("Keterangan");
                table.Columns.Add("Penerimaan");
                table.Columns.Add("Pengeluaran");
                table.Columns.Add("Saldo");

                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();

                
                decimal akumulasi = 0L;
                decimal sisa = 0;
              
                for (int idx = 0; idx < gridKas.Rows.Count; idx++)
                {
                    table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(gridKas.Rows[idx].Cells[0].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridKas.Rows[idx].Cells[1].Value).ReplaceUnicode(),                      
                       DataFormat.GetString(gridKas.Rows[idx].Cells[2].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridKas.Rows[idx].Cells[3].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridKas.Rows[idx].Cells[4].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridKas.Rows[idx].Cells[5].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridKas.Rows[idx].Cells[6].Value).ReplaceUnicode(),     
                       DataFormat.GetString(gridKas.Rows[idx].Cells[7].Value).ReplaceUnicode(),     
                    });
               
                }
                
                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 20;
                pdfGrid.Columns[1].Width = 20;
                // Angka 
                pdfGrid.Columns[2].Width = 50;
                pdfGrid.Columns[3].Width = 50;
                pdfGrid.Columns[4].Width = 175;
                pdfGrid.Columns[5].Width = 65;
                pdfGrid.Columns[6].Width = 65;
                pdfGrid.Columns[7].Width = 65;
            //    MessageBox.Show("Selesai daftar..");
                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));

                line = 9;
                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;
                
                pdfGrid.Columns[5].Format = formatKolomAngka;
                pdfGrid.Columns[6].Format = formatKolomAngka;
                pdfGrid.Columns[7].Format = formatKolomAngka;
                









                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();

                pdfGrid.RepeatHeader = true;
                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;
                line = 10;
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

                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        if (c == 4)
                        {
                            pdfGrid.Rows[idx].Cells[c].Style.CellPadding = new PdfPaddings(1, 1, 1, 0);
                        }
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.05F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.05F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.05F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.05F;

                    }

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos));
             #endregion gridKas

                PdfGrid pdfGridRingkasan = new PdfGrid();

              
                //Create a DataTable
            table = new DataTable();
                //Add columns to table
              
                table.Columns.Add("Keterangan".ReplaceUnicode());
                table.Columns.Add("Penerimaan".ReplaceUnicode());
                table.Columns.Add("Pengeluaran".ReplaceUnicode());
                table.Columns.Add("Saldo".ReplaceUnicode());

                 columnCount = table.Columns.Count;
             data = new List<object>();


            
                
                    //table.Rows.Add(new string[]
                    //{

                    //   mJumlahBulanLalu.Uraian,
                    //   mJumlahBulanLalu.Penerimaan.ToRupiahInReport(),                      
                    //   mJumlahBulanLalu.Pengeluaran.ToRupiahInReport(),
                    //  (mJumlahBulanLalu.Penerimaan - mJumlahBulanLalu.Pengeluaran).ToRupiahInReport(),
                       
                    //});
                    //table.Rows.Add(new string[]
                    //{

                    //   mJumlahBulanIni.Uraian,
                    //   mJumlahBulanIni.Penerimaan.ToRupiahInReport(),                      
                    //   mJumlahBulanIni.Pengeluaran.ToRupiahInReport(),
                    //  (mJumlahBulanIni.Penerimaan - mJumlahBulanIni.Pengeluaran).ToRupiahInReport(),
                       
                    //});

                   table.Rows.Add(new string[]
                    {

                       "Jumlah ".ReplaceUnicode(),
                       (mJumlahBulanLalu.Penerimaan + mJumlahBulanIni.Penerimaan).ToRupiahInReport().ReplaceUnicode(),                      
                       ( mJumlahBulanLalu.Pengeluaran+ mJumlahBulanIni.Pengeluaran).ToRupiahInReport().ReplaceUnicode(),"",
                      //((mJumlahBulanLalu.Penerimaan - mJumlahBulanLalu.Pengeluaran) + (mJumlahBulanIni.Penerimaan - mJumlahBulanIni.Pengeluaran)).ToRupiahInReport(),
                       
                    });



                   pdfGridRingkasan.DataSource = table; //data
             
                   pdfGridRingkasan.Columns[0].Width = 20 +20 + 50 + 50 + 175;
                   pdfGridRingkasan.Columns[1].Width = 65;
                   pdfGridRingkasan.Columns[2].Width = 65;
                   pdfGridRingkasan.Columns[3].Width = 65;

                gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
               // gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGridRingkasan.Style = gridStyle;


                formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGridRingkasan.Columns[1].Format = formatKolomAngka;
                pdfGridRingkasan.Columns[2].Format = formatKolomAngka;
                pdfGridRingkasan.Columns[3].Format = formatKolomAngka;

                pdfGridRingkasan.Headers.Clear();
                for (int idx = 0; idx < pdfGridRingkasan.Rows.Count; idx++)
                {
                    pdfGridRingkasan.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 7,
                        FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);

                    for (int c = 0; c < pdfGridRingkasan.Columns.Count; c++)
                    {
                        if (c == 4)
                        {
                            pdfGridRingkasan.Rows[idx].Cells[c].Style.CellPadding = new PdfPaddings(1, 1, 1, 0);
                        }
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.05F;

                    }

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                pdfGridLayoutResult = pdfGridRingkasan.Draw(pdfGridLayoutResult.Page, new PointF(kiri, pdfGridLayoutResult.Bounds.Bottom));







                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();
             
                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../Bukukas.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../Bukukas.pdf");
                pV.Show();


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " ON LINE " + line.ToString());
                return false;
            }
        }
        private bool CetakPajak()
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
                document.Pages.PageAdded += PagesPajak_PageAdded;

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
                pimpinan = ctrlDinas1.GetPimpinan(mTanggalAkhir);
                bendahara = ctrlDinas1.GetBendaharaPengeluaran(mTanggalAkhir);

                if (pimpinan == null)
                {
                    MessageBox.Show("Data Pimpinan belum di setting di master pejabat");
                    return false;

                }

                if (bendahara == null)
                {
                    MessageBox.Show("Data Bndahara belum di setting di master pejabat");
                    return false;

                }


                float kiri = 15;
                float posTitikdua = 150;
                float posNama = 155;

                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                   page.GetClientSize().Width, stringFormat, true, false, true);

               
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "BUKU PEMBANTU PEMBANTU PAJAK" 
                         , 10, kiri, yPos,
                         page.GetClientSize().Width, stringFormat, true, false, true);
                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = yPos + 20;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          ctrlDinas1.GetNamaSKPD(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Bendahara Pengeluaran "
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
                table.Columns.Add("No BKU");
                table.Columns.Add("Tanggal");
                table.Columns.Add("Keterangan");
                table.Columns.Add("Penerimaan(Rp)");
                table.Columns.Add("Pengeluaran(Rp)");
                table.Columns.Add("Saldo (Rp)");

                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();


                decimal akumulasi = 0L;
                decimal sisa = 0;

                for (int idx = 0; idx < gridPajak.Rows.Count; idx++)
                {
                    table.Rows.Add(new string[]
                    {

                           
                       DataFormat.GetString(gridPajak.Rows[idx].Cells[0].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridPajak.Rows[idx].Cells[2].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridPajak.Rows[idx].Cells[3].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridPajak.Rows[idx].Cells[4].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridPajak.Rows[idx].Cells[5].Value).ReplaceUnicode(),     
                       DataFormat.GetString(gridPajak.Rows[idx].Cells[6].Value).ReplaceUnicode(),     
                    });
                }

                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 40;
                //pdfGrid.Columns[1].Width = 20;
                //// Angka 
                //pdfGrid.Columns[2].Width = 50;
                pdfGrid.Columns[1].Width = 70;
                pdfGrid.Columns[2].Width = 195;
                pdfGrid.Columns[3].Width = 65;
                pdfGrid.Columns[4].Width = 65;
                pdfGrid.Columns[5].Width = 65;

                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                //gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGrid.Columns[3].Format = formatKolomAngka;
                pdfGrid.Columns[4].Format = formatKolomAngka;
                pdfGrid.Columns[5].Format = formatKolomAngka;










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

                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        if (c == 4)
                        {
                            pdfGrid.Rows[idx].Cells[c].Style.CellPadding = new PdfPaddings(1, 1, 1, 0);
                        }
             

                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.01F;

                    }

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos));
                #endregion gridKas
                PdfGrid pdfGridRingkasan = new PdfGrid();


                //Create a DataTable
                table = new DataTable();
                //Add columns to table

                table.Columns.Add("Keterangan");
                table.Columns.Add("Penerimaan");
                table.Columns.Add("Pengeluaran");
                table.Columns.Add("Saldo");

                columnCount = table.Columns.Count;
                data = new List<object>();




                table.Rows.Add(new string[]
                    {

                       mJumlahBulanLalu.Uraian,
                       mJumlahBulanLalu.Penerimaan.ToRupiahInReport(),                      
                       mJumlahBulanLalu.Pengeluaran.ToRupiahInReport(),
                      (mJumlahBulanLalu.Penerimaan - mJumlahBulanLalu.Pengeluaran).ToRupiahInReport(),
                       
                    });
                table.Rows.Add(new string[]
                    {

                       mJumlahBulanIni.Uraian,
                       mJumlahBulanIni.Penerimaan.ToRupiahInReport(),                      
                       mJumlahBulanIni.Pengeluaran.ToRupiahInReport(),
                      (mJumlahBulanIni.Penerimaan - mJumlahBulanIni.Pengeluaran).ToRupiahInReport(),
                       
                    });

                table.Rows.Add(new string[]
                    {

                       "Jumlah sampai dengan "+ mTanggalAkhir.ToTanggalIndonesia(),
                       (mJumlahBulanLalu.Penerimaan + mJumlahBulanIni.Penerimaan).ToRupiahInReport(),                      
                       ( mJumlahBulanLalu.Pengeluaran+ mJumlahBulanIni.Pengeluaran).ToRupiahInReport(),
                      ((mJumlahBulanLalu.Penerimaan - mJumlahBulanLalu.Pengeluaran) + (mJumlahBulanIni.Penerimaan - mJumlahBulanIni.Pengeluaran)).ToRupiahInReport(),
                       
                    });



                pdfGridRingkasan.DataSource = table; //data

                //pdfGrid.Columns[0].Width = 40;
                ////pdfGrid.Columns[1].Width = 20;
                ////// Angka 
                ////pdfGrid.Columns[2].Width = 50;
                //pdfGrid.Columns[1].Width = 70;
                //pdfGrid.Columns[2].Width = 195;
                //pdfGrid.Columns[3].Width = 65;
                //pdfGrid.Columns[4].Width = 65;
                //pdfGrid.Columns[5].Width = 65;

                pdfGridRingkasan.Columns[0].Width = 40 + 70 + 195 ;
                pdfGridRingkasan.Columns[1].Width = 65;
                pdfGridRingkasan.Columns[2].Width = 65;
                pdfGridRingkasan.Columns[3].Width = 65;

                gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                //gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGridRingkasan.Style = gridStyle;


                formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGridRingkasan.Columns[1].Format = formatKolomAngka;
                pdfGridRingkasan.Columns[2].Format = formatKolomAngka;
                pdfGridRingkasan.Columns[3].Format = formatKolomAngka;

                pdfGridRingkasan.Headers.Clear();
                for (int idx = 0; idx < pdfGridRingkasan.Rows.Count; idx++)
                {
                    pdfGridRingkasan.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 7,
                        FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);

                    for (int c = 0; c < pdfGridRingkasan.Columns.Count; c++)
                    {
                        if (c == 4)
                        {
                            pdfGridRingkasan.Rows[idx].Cells[c].Style.CellPadding = new PdfPaddings(1, 1, 1, 0);
                        }
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.05F;

                    }

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                pdfGridLayoutResult = pdfGridRingkasan.Draw(pdfGridLayoutResult.Page, new PointF(kiri, pdfGridLayoutResult.Bounds.Bottom));


                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();

                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../Bukukas.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../Bukukas.pdf");
                pV.Show();


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CetakPanjar()
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

                section.PageSettings.Orientation = PdfPageOrientation.Landscape;

                float yPos = 0;
                SaatnyacetakKesimpulan = false;
                PdfPage page = section.Pages.Add();

                PdfGraphics graphics = page.Graphics;
                previousPage = page;
                document.Pages.PageAdded += PagesPanjar_PageAdded;// PagesPajak_PageAdded;

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
                pimpinan = ctrlDinas1.GetPimpinan(mTanggalAkhir);
                bendahara = ctrlDinas1.GetBendaharaPengeluaran(mTanggalAkhir);

                if (pimpinan == null)
                {
                    MessageBox.Show("Data Pimpinan belum di setting di master pejabat");
                    return false;

                }

                if (bendahara == null)
                {
                    MessageBox.Show("Data Bndahara belum di setting di master pejabat");
                    return false;

                }


                float kiri = 15;
                float posTitikdua = 150;
                float posNama = 155;

                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                   page.GetClientSize().Width, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "BUKU PEMBANTU PEMBANTU PANJAR"
                     , 10, kiri, yPos,
                     page.GetClientSize().Width, stringFormat, true, false, true);
                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = yPos + 20;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          ctrlDinas1.GetNamaSKPD(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Bendahara Pengeluaran "
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
                #region gridPanjar
                PdfGrid pdfGrid = new PdfGrid();

                int count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("No BKU");
                table.Columns.Add("Tanggal");
                table.Columns.Add("Keterangan");
                table.Columns.Add("Penerima/Penyetor");

                table.Columns.Add("Penerimaan(Rp)");
                table.Columns.Add("Pengeluaran(Rp)");
                table.Columns.Add("Saldo (Rp)");

                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();


                decimal akumulasi = 0L;
                decimal sisa = 0;

                for (int idx = 0; idx < gridPanjar.Rows.Count; idx++)
                {
                    table.Rows.Add(new string[]
                    {

                           
                       DataFormat.GetString(gridPanjar.Rows[idx].Cells[0].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridPanjar.Rows[idx].Cells[1].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridPanjar.Rows[idx].Cells[2].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridPanjar.Rows[idx].Cells[3].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridPanjar.Rows[idx].Cells[4].Value).ReplaceUnicode(),     
                       DataFormat.GetString(gridPanjar.Rows[idx].Cells[5].Value).ReplaceUnicode(),     
                       DataFormat.GetString(gridPanjar.Rows[idx].Cells[6].Value).ReplaceUnicode(),     
                    });
                }

                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 40;
                ;
                pdfGrid.Columns[1].Width = 70;
                pdfGrid.Columns[2].Width = 390;

                pdfGrid.Columns[3].Width = 90;
                pdfGrid.Columns[4].Width = 75;
                pdfGrid.Columns[5].Width = 75;
                pdfGrid.Columns[6].Width = 75;

                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                //gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGrid.Columns[3].Format = formatKolomAngka;
                pdfGrid.Columns[4].Format = formatKolomAngka;
                pdfGrid.Columns[5].Format = formatKolomAngka;










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

                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        if (c == 4)
                        {
                            pdfGrid.Rows[idx].Cells[c].Style.CellPadding = new PdfPaddings(1, 1, 1, 0);
                        }


                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.01F;

                    }

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos));
                #endregion gridKas

                PdfGrid pdfGridRingkasan = new PdfGrid();


                //Create a DataTable
                table = new DataTable();
                //Add columns to table

                float posketerangan = 40 + 70 + 200;
                float pospenerimaan = 40 + 70 + 430 + 80;
                float pospenyetoran = 40 + 70 + 430 + 80 + 65;
                //float possaldo=40+70+430+80+65+ 65;
                //float posakhir=40+70+430+80+65+ 65+ 65;


                decimal penerimaanSebelumPeriode = mListBKU.Where(x => x.Tanggal < mTanggalAwal).Sum(s => s.Penerimaan);
                decimal pengeluaranSebelumPeriode = mListBKU.Where(x => x.Tanggal < mTanggalAwal).Sum(s => s.Pengeluaran);
                decimal penerimaanPeriode = mListBKU.Where(x => x.Tanggal >= mTanggalAwal &&
                                 x.Tanggal <= mTanggalAkhir).Sum(s => s.Penerimaan);
                decimal pengeluaranPeriode = mListBKU.Where(x => x.Tanggal >= mTanggalAwal &&
                                x.Tanggal <= mTanggalAkhir).Sum(s => s.Pengeluaran);



                
                table.Columns.Add("Keterangan");
                table.Columns.Add("Penerimaan");
                table.Columns.Add("Pengeluaran");
                table.Columns.Add("Saldo");

                columnCount = table.Columns.Count;
                
                
                
                data = new List<object>();





                table.Rows.Add(new string[]
                    {

                       "Jumlah Periode "+ ctrlTanggalBulanVertikal1.Waktu,

                       penerimaanPeriode.ToRupiahInReport(),                      
                       pengeluaranPeriode.ToRupiahInReport(),"",
                      //(penerimaanPeriode - pengeluaranPeriode).ToRupiahInReport(),
                       
                    });

                table.Rows.Add(new string[]
                    {

                      "Jumlah Sebekum " + mTanggalAwal.ToTanggalIndonesia(),
                       penerimaanSebelumPeriode.ToRupiahInReport(),                      
                       pengeluaranSebelumPeriode.ToRupiahInReport(),"",
                      //(penerimaanSebelumPeriode - pengeluaranSebelumPeriode).ToRupiahInReport(),
                       
                    });

                table.Rows.Add(new string[]
                    {

                       "Jumlah Sampai  " + mTanggalAkhir.ToTanggalIndonesia(),
                       (penerimaanPeriode + penerimaanSebelumPeriode).ToRupiahInReport(),                      
                       ( pengeluaranPeriode + pengeluaranSebelumPeriode).ToRupiahInReport(),"",
                      //((penerimaanPeriode - pengeluaranPeriode) + (penerimaanSebelumPeriode - pengeluaranSebelumPeriode)).ToRupiahInReport(),
                       
                    });
                
                        decimal penerimaan =penerimaanPeriode + penerimaanSebelumPeriode ;
                        decimal pengeluaran=pengeluaranPeriode+pengeluaranSebelumPeriode;
                        if (penerimaan > pengeluaran){
                            table.Rows.Add(new string[]
                            {
                              "Saldo " + mTanggalAkhir.ToTanggalIndonesia(),(penerimaan-pengeluaran).ToRupiahInReport(),"",
                             });      
                        } else {
                            table.Rows.Add(new string[]
                            {
                              "Saldo " + mTanggalAkhir.ToTanggalIndonesia(),"",(pengeluaran-penerimaan).ToRupiahInReport(),
                             });      
                        }

                 



                pdfGridRingkasan.DataSource = table; //data


                pdfGridRingkasan.Columns[0].Width = 40 + 70 + 390+90 ;
                pdfGridRingkasan.Columns[1].Width = 75;
                pdfGridRingkasan.Columns[2].Width = 75;
                pdfGridRingkasan.Columns[3].Width = 75;

                //pdfGrid.Columns[0].Width = 40;
                
                //pdfGrid.Columns[1].Width = 70;
                //pdfGrid.Columns[2].Width = 390;

                //pdfGrid.Columns[3].Width = 90;
                //pdfGrid.Columns[4].Width = 75;
                //pdfGrid.Columns[5].Width = 75;
                //pdfGrid.Columns[6].Width = 75;


                gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                //gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGridRingkasan.Style = gridStyle;


                formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGridRingkasan.Columns[1].Format = formatKolomAngka;
                pdfGridRingkasan.Columns[2].Format = formatKolomAngka;
                pdfGridRingkasan.Columns[3].Format = formatKolomAngka;

                pdfGridRingkasan.Headers.Clear();
                for (int idx = 0; idx < pdfGridRingkasan.Rows.Count; idx++)
                {
                    pdfGridRingkasan.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 7,
                        FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);

                    for (int c = 0; c < pdfGridRingkasan.Columns.Count; c++)
                    {
                        if (c == 4)
                        {
                            pdfGridRingkasan.Rows[idx].Cells[c].Style.CellPadding = new PdfPaddings(1, 1, 1, 0);
                        }
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.05F;
                        pdfGridRingkasan.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.05F;

                    }

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                pdfGridLayoutResult = pdfGridRingkasan.Draw(pdfGridLayoutResult.Page, new PointF(kiri, pdfGridLayoutResult.Bounds.Bottom));



                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();

                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../Bukukas.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../Bukukas.pdf");
                pV.Show();


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
       

        private void cmdExcell_Click(object sender, EventArgs e)
        {
            if (m_nMode == 1 || m_nMode == 2)
            {
                ExcellBukuKas();
            }
            if (m_nMode == 3)
            {
                ExcellBukuPajak();
          
            }
            if (m_nMode == 4)
            {
                ExcellBukuPanjar();
                ProsesPanjar();
            }

            
        }
    private void ExcellBukuKas(){
     try{

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
                
                for (int row = 0; row < gridKas.Rows.Count; row++)
                {
                    for (int col = 0; col < gridKas.Columns.Count; col++)
                    {
                            excelSheet.Cells[row + 1, col + 1] = DataFormat.GetString(gridKas.Rows[row].Cells[col].Value);
                       

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
        private void ExcellBukuPajak(){
     try{

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

                for (int row = 0; row < gridPajak.Rows.Count; row++)
                {
                    for (int col = 0; col < gridPajak.Columns.Count; col++)
                    {
                        excelSheet.Cells[row + 1, col + 1] = DataFormat.GetString(gridPajak.Rows[row].Cells[col].Value).ReplaceUnicode();
                       

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
        private void ExcellBukuPanjar(){
     try{

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

                for (int row = 0; row < gridPanjar.Rows.Count; row++)
                {
                    for (int col = 0; col < gridPanjar.Columns.Count; col++)
                    {
                        excelSheet.Cells[row + 1, col + 1] = DataFormat.GetString(gridPanjar.Rows[row].Cells[col].Value).ReplaceUnicode();
                       

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


            sRet = fdlg.FileName;


            //  }
            return sRet;
        }

        private void ctrlCombPotongan1_Load(object sender, EventArgs e)
        {

        }

        
    }
    public class  BKUDenganNamaPenerima {
                       
        public int NoBKU{set;get;}
        public DateTime TanggalBKU{set;get;}
        public string Uraian {set;get;}
        public string Nama {set;get;}
        public decimal Panerimaan {set;get;}
        public decimal Pengeluaran{set;get;}

    }

}
