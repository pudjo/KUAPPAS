using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP.Bendahara;
using DTO.Bendahara;
using Formatting;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using DTO;
using System.IO;
using System.Data.SqlTypes;
using BP;
namespace KUAPPAS.Bendahara
{
    //frmCetakPenutupanKasBendahara

    public partial class frmPengeluaranLaporanPenutupanKas : ChildForm
    {
        private int m_IDSKPD;
        private int m_nMode;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;
        List<BKU> mListBKU;
        CetakPDF oCetakPDF;
        int kodeUKBendaharapengeluaran = 0;
        private List<ItemsPenutupanKas> mItemPenutupanKas;
        public frmPengeluaranLaporanPenutupanKas()
        {
            InitializeComponent();
            mItemPenutupanKas = new List<ItemsPenutupanKas>();
            oCetakPDF = new CetakPDF();

        }
        private void GetKodeUKBendahara()
        {
            try
            {
               
                int SKPD = ctrlSKPD1.GetID();
                UnitKerjaLogic uLogic = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
                List<Unit> lstUnit = uLogic.GetBySKPD(SKPD);
                lstUnit.OrderBy(x => x.Kode);

                if (lstUnit != null)
                {
                    if (lstUnit.Count > 0)
                    {
                        kodeUKBendaharapengeluaran = lstUnit[0].Kode;
                    }
                }


            } catch(Exception ex)
            {
                return ;
            }
        }
        private void frmLaporanPenutupanKas_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Laporan Penutupan Kas");
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            gridPenutupanKas.FormatHeader();
            ctrlBulan1.Create();
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlSKPD1.SetID(GlobalVar.Pengguna.SKPD);
                m_IDSKPD = GlobalVar.Pengguna.SKPD;
                GetKodeUKBendahara();
            }
            ctrlPeriode1.TanggalAwaal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
            ctrlPeriode1.TanggalAkhir = DateTime.Now.Date;


        }
        private void GetTanggal()
        {
            if (rbTanggal.Checked == true)
            {
                mTanggalAwal = ctrlPeriode1.TanggalAwaal;
                mTanggalAkhir = ctrlPeriode1.TanggalAkhir;
            }
            else
            {
                mTanggalAwal = ctrlBulan1.TanggalAwal;
                mTanggalAkhir = ctrlBulan1.TanggalAkhir;
            }
        }
        private bool GetBKU()
        {
            try
            {
                int m_IDSKPD = ctrlSKPD1.GetID();
                GetTanggal();
                BKULogic oLogic = new BKULogic(GlobalVar.TahunAnggaran);
                List<BKU> lst = new List<BKU>();
                mListBKU = new List<BKU>();
                lst = oLogic.GetByDinas(m_IDSKPD, mTanggalAkhir);
                foreach (BKU bku in lst)
                {
                    if ((bku.JenisSumber == 1 && bku.JenisBelanja <= 2) || (bku.JenisSumber != 1 && bku.JenisSumber != 10))
                    {
                        if (bku.JenisBendahara == E_JENISBENDAHARA.BENDAHARA_PENGELUARAN)
                        {
                            mListBKU.Add(bku);
                        }
                    }
                }
                return true;

            }

            catch (Exception ex)
            {
                return false;
            }

        }
        private bool LoadData(){

            try{
                GetBKU();

                gridPenutupanKas.Rows.Clear();
                mItemPenutupanKas = new List<ItemsPenutupanKas>();
                ItemsPenutupanKas p= new ItemsPenutupanKas("A",".","Kas di Bendahara Pengeluaran","0",1);
                mItemPenutupanKas.Add(p);
                
                decimal nilai = mListBKU.Where(x=>x.TanggalTransaksi< mTanggalAwal  && (int)x.JenisBendahara==2 ).Sum(bku => bku.Jumlah * bku.Debet);
                
                //decimal penerimaan = mListBKU.Where(x => x.TanggalTransaksi >= mTanggalAwal && 
                //                     x.TanggalTransaksi <= mTanggalAkhir && 
                //                     x.Debet == 1 &&
                //                       (int)x.JenisBendahara == 2 
                //                      ).Sum(bku => bku.Jumlah);
                //decimal pengeluaran = mListBKU.Where(x => x.TanggalTransaksi >= mTanggalAwal && 
                //                        x.TanggalTransaksi <= mTanggalAkhir  &&
                //                        x.Debet == -1 &&
                //                         (int)x.JenisBendahara == 2 ).Sum(bku => bku.Jumlah);

                decimal penerimaan = GetSaldo(-1 , kodeUKBendaharapengeluaran,"=",1);
                decimal pengeluaran = GetSaldo(-1, kodeUKBendaharapengeluaran, "=", -1);
                p = new ItemsPenutupanKas ("A","1","Saldo awal tanggal " + mTanggalAwal.ToTanggalIndonesia() ,nilai.ToRupiahInReport(),0);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("A", "2", "Jumlah Penerimaan ", penerimaan.ToRupiahInReport(), 0);

                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("A", "3", "Jumlah Pengeluaran ", pengeluaran.ToRupiahInReport(), 0);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("A", "4", "Saldo AKhir tanggal " + mTanggalAkhir.ToTanggalIndonesia(), (nilai + penerimaan - pengeluaran).ToRupiahInReport(), 1);
                mItemPenutupanKas.Add(p);
                
                decimal penerimaanBPP = GetSaldo(-1, kodeUKBendaharapengeluaran, ">", 1);  ;// mListBKU.Where(x => x.TanggalTransaksi >= mTanggalAwal &&
                decimal pengeluaranBPP = GetSaldo(-1, kodeUKBendaharapengeluaran, ">", -1); ;//]//mListBKU.Where(x => x.TanggalTransaksi >= mTanggalAwal &&


                p = new ItemsPenutupanKas("B", ".", "Kas di Bendahara Pengeluaran Pembantu", "0", 1);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("B", "1", "Saldo awal tanggal " + mTanggalAwal.ToTanggalIndonesia(), "0", 0);
                mItemPenutupanKas.Add(p);


                p = new ItemsPenutupanKas("B", "2", "JumlahPenerimaan ", penerimaanBPP.ToRupiahInReport(), 0);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("B", "3", "Jumlah Pengeluaran ", pengeluaranBPP.ToRupiahInReport(), 0);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("B", "4", "Saldo AKhir tanggal "  + mTanggalAkhir.ToTanggalIndonesia(),
                     ( penerimaanBPP- pengeluaranBPP).ToRupiahInReport(), 1);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("C", ".", "Rekapitulasi Posisi Kas di Bendahara Pengeluaran", "0", 1);
                mItemPenutupanKas.Add(p);

                decimal nilaiTunai = mListBKU.Where(x => x.TanggalTransaksi <= mTanggalAkhir && x.Kodebank == 0 && (int)x.JenisBendahara == 2  ).Sum(bku => bku.Jumlah * bku.Debet);
                
                p = new ItemsPenutupanKas("C", "1", "Saldo di Kas Tunai ", nilaiTunai.ToRupiahInReport(), 0);
                mItemPenutupanKas.Add(p);
                decimal nilaiBank = mListBKU.Where(x => x.TanggalTransaksi <= mTanggalAkhir && x.Kodebank == 1 && (int)x.JenisBendahara == 2 ).Sum(bku => bku.Jumlah * bku.Debet);
                p = new ItemsPenutupanKas("C", "2", "Saldo di Bank ", nilaiBank.ToRupiahInReport(), 0);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("C", "3", "Saldo total ", (nilaiBank + nilaiTunai).ToRupiahInReport(), 1);
                mItemPenutupanKas.Add(p);
               
                foreach (ItemsPenutupanKas it in mItemPenutupanKas)
                {
                    string[] row = { it.Item, it.row, it.Keterangan, it.Jumlah, it.bold.ToString() };
                    gridPenutupanKas.Rows.Add(row);

                
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


                return false;
            }
        }


        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
            GetKodeUKBendahara();
            LoadData();
        }
        private decimal GetSaldo(int bank , int kodeUK ,string tanda ="=",int debet =-1)
        {
            try
            {
                int m_IDSKPD = ctrlSKPD1.GetID();
                GetTanggal();
                BKULogic oLogic = new BKULogic(GlobalVar.TahunAnggaran);
                List<BKU> lstSaldo = new List<BKU>();

                lstSaldo  = oLogic.GetByDinasBank(m_IDSKPD, mTanggalAkhir,bank,kodeUK, tanda, debet);
                decimal saldo = 0l;
                if (lstSaldo != null)
                {
                    
                        foreach (BKU bku in lstSaldo)
                    {
                        if (bku.TanggalTransaksi >= mTanggalAwal)
                        {
                            if ((bku.JenisSumber == 1 && bku.JenisBelanja <= 2) || (bku.JenisSumber != 1 && bku.JenisSumber != 10 ))
                            {
                                saldo = saldo + (bku.Debet * bku.Jumlah);
                            }
                        }
                    }
                }
                return Math.Abs(saldo);

            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        
        private void cmdCetak_Click(object sender, EventArgs e)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                PdfSection section = document.Sections.Add();

                section.PageSettings.Size = PdfPageSize.Legal;
                section.PageSettings.Orientation = PdfPageOrientation.Portrait;
            
                float yPos = 0;
  
                PdfPage page = section.Pages.Add();
                
                PdfGraphics graphics = page.Graphics;

               
        
                yPos = 10;
                 PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);
            

                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;

                //stringFormat.CharacterSpacing = 2f;

                //SizeF size = font12.MeasureString("xxx");
                yPos = oCetakPDF.TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), 12, 10, yPos, (page.GetClientSize().Width - 20), stringFormat, true);
                yPos = oCetakPDF.TulisItem(graphics, "LAPORAN PENUTUPAN KAS", 12, 10, yPos, (page.GetClientSize().Width - 20), stringFormat, true);
                
                yPos = yPos + 20;
                Pejabat pimpinan = new Pejabat();
                pimpinan = ctrlSKPD1.GetKepalaDinas(mTanggalAkhir);

                if (pimpinan == null)
                {
                    MessageBox.Show("Data Pimpinan belum di setting di master pejabat");
                    return;
                }
                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(graphics, "Kepada Yth." , 12, 10, yPos, (page.GetClientSize().Width - 20), stringFormat, true);
                yPos = oCetakPDF.TulisItem(graphics, pimpinan.Jabatan, 12, 10, yPos, (page.GetClientSize().Width - 20), stringFormat, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(graphics, "di Tempat", 12, 10, yPos, (page.GetClientSize().Width - 20), stringFormat, true);
                yPos = yPos + 30;
                decimal saldo;
                saldo = mListBKU.Where(b => b.TanggalTransaksi <= mTanggalAkhir).Sum(s => s.Jumlah * s.Debet);
                string text = "Dengan memperhatikan Peraturan Bupati Nomor " + txtNoPerbup.Text +
                        " mengenai Sistem dan Prosedur Pengelolaan Keuangan Daerah," +
                        " bersama ini kami Sampaikan Laporan Penutupan Kas Bulanan yang terdapat di Bendahara" +
                        " Pengeluaran SKPD " + ctrlSKPD1.GetNamaSKPD() +" adalah sejumlah Rp. " + saldo.ToRupiahInReport() +
                        " dengan perincian sebagai berikut:";
                stringFormat.Alignment = PdfTextAlignment.Justify;
                yPos = oCetakPDF.TulisItem(graphics, text, 12, 10, yPos, (page.GetClientSize().Width - 20), stringFormat, true);
                
                stringFormat.Alignment = PdfTextAlignment.Left ;
                bool bold = false;
                foreach (DataGridViewRow row in gridPenutupanKas.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {  
                        if (DataFormat.GetString(row.Cells[1].Value) == ".")
                        {
                            yPos = yPos + 20;
                            if (DataFormat.GetString(row.Cells[0].Value) == "B")
                            {
                                //decimal bankBendahara = mListBKU.Where(x => x.TanggalTransaksi <= mTanggalAkhir &&
                                //     x.KodeUk <=  1 && x.Kodebank==1 && (int)x.JenisBendahara == 2 ).Sum(bku => bku.Jumlah * bku.Debet);

                                decimal bankBendahara = GetSaldo(1, kodeUKBendaharapengeluaran);
                                decimal tunaiBendahara = GetSaldo(0, kodeUKBendaharapengeluaran);

                                text = "Saldo akhir bulan tanggal " + mTanggalAkhir.ToTanggalIndonesia() + " terdiri dari saldo" +
                                    " di kas tunai sebesar Rp. " + tunaiBendahara.ToRupiahInReport() + " dan saldo di bank sebesar Rp. " +
                                    bankBendahara.ToRupiahInReport();

                                
                                stringFormat.Alignment = PdfTextAlignment.Justify;
                                yPos = oCetakPDF.TulisItem(graphics, text, 12, 50, yPos, (page.GetClientSize().Width - 20), stringFormat, true);
                                yPos = yPos + 20;
                            }

                            if (DataFormat.GetString(row.Cells[0].Value) == "C")
                            {



                                decimal bankBendaharaPembantu = GetSaldo(1, kodeUKBendaharapengeluaran,">");
                                decimal tunaiBendaharaPembantu = GetSaldo(0, kodeUKBendaharapengeluaran,">");




                                text = "Saldo akhir bulan tanggal "  + mTanggalAkhir.ToTanggalIndonesia() +  " terdiri dari saldo" +
                                    " di kas tunai sebesar Rp. " + tunaiBendaharaPembantu.ToRupiahInReport()  + " dan saldo di bank sebesar Rp. " +
                                    bankBendaharaPembantu.ToRupiahInReport();

                                stringFormat.Alignment = PdfTextAlignment.Justify;
                                yPos = oCetakPDF.TulisItem(graphics, text, 12, 50, yPos, (page.GetClientSize().Width - 20), stringFormat, true);
                                yPos = yPos + 20;
                            }


                            yPos = oCetakPDF.TulisItem(graphics, DataFormat.GetString(row.Cells[0].Value), 12, 10,
                                 yPos, (page.GetClientSize().Width - 20), stringFormat, false,false,true);

                            yPos = oCetakPDF.TulisItem(graphics, DataFormat.GetString(row.Cells[2].Value), 12, 50,
                                 yPos, (page.GetClientSize().Width - 20), stringFormat, true, false, true);

                        }
                        else
                        {
                            if ((DataFormat.GetString(row.Cells[0].Value) == "A" && DataFormat.GetString(row.Cells[1].Value) == "4") ||
                                (DataFormat.GetString(row.Cells[0].Value) == "B" && DataFormat.GetString(row.Cells[1].Value) == "4") ||
                                (DataFormat.GetString(row.Cells[0].Value) == "C" && DataFormat.GetString(row.Cells[1].Value) == "3"))
                            {

                                graphics.DrawLine(pen, 380, yPos+1, 150 + 380, yPos + 1);
                                yPos  = yPos + 1;
                            }
                            if (DataFormat.GetString(row.Cells[4].Value) == "1")
                            {
                                bold = true;
                            }
                            else
                            {
                                bold = false ;
                            }
                            yPos = oCetakPDF.TulisItem(graphics, DataFormat.GetString(row.Cells[0].Value) + " . " + DataFormat.GetString(row.Cells[1].Value), 10, 50,
                                 yPos, (page.GetClientSize().Width - 20), stringFormat, false, false, bold);

                            yPos = oCetakPDF.TulisItem(graphics, DataFormat.GetString(row.Cells[2].Value), 10, 80,
                                 yPos, (page.GetClientSize().Width - 20), stringFormat, false, false, bold);


                            yPos = oCetakPDF.TulisItem(graphics, "Rp.", 10, 380,
                                 yPos, 50, stringFormat, false, false, bold);
                            stringFormat.Alignment = PdfTextAlignment.Right;

                            yPos = oCetakPDF.TulisItem(graphics, DataFormat.GetString(row.Cells[3].Value), 10, 380,
                                 yPos, 150, stringFormat, true, false, bold);

                            stringFormat.Alignment = PdfTextAlignment.Left;


                        }
                        
                    }


                }



                     yPos = yPos + 25;
                     Pejabat m_oBendahara = new Pejabat();
                     m_oBendahara = ctrlSKPD1.GetBendahara(mTanggalAkhir);
                     if (m_oBendahara == null)
                     {
                         MessageBox.Show("Bendahara Belum di setting");
                         return;
                     }
                     float setengah = (page.GetClientSize().Width / 2) - 20;
                     float posisiTengah = page.GetClientSize().Width / 2 + 10;
                     stringFormat.Alignment = PdfTextAlignment.Center;
                     yPos = oCetakPDF.TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtCetak.Value.ToTanggalIndonesia(), 10, posisiTengah, yPos,
                    setengah, stringFormat, true);
                     
                     yPos = oCetakPDF.TulisItem(graphics, m_oBendahara.Jabatan, 10, posisiTengah, yPos,
                      setengah, stringFormat, true);



                     yPos = yPos + 30;

                     yPos = oCetakPDF.TulisItem(graphics, m_oBendahara.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true);

                     yPos = oCetakPDF.TulisItem(graphics, "NIP " + m_oBendahara.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);

                    using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPP.pdf"), FileMode.Create, FileAccess.ReadWrite))
                    {
                        //Save the PDF document to file stream.
                        document.Save(outputFileStream);

                    }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../SPP.pdf");
                pV.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal cetak Peutupan kas" + System.Environment.NewLine + ex.Message);
            }

        }

        private void ctrlSKPD1_Load(object sender, EventArgs e)
        {

        }
        

    }
    
   
}
