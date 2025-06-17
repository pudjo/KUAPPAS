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
namespace KUAPPAS.Bendahara
{
    public partial class frmSPJAdministratifPenerimaan : ChildForm
    {
        private int m_IDSKPD;
        private int m_nMode;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;
        //List<BKU> mListBKU;
        private List<STS> m_lstSTS;

        private List<Setor> m_lstSetorPenerimaan;
        CetakPDF oCetakPDF;
        private List<ItemsPenutupanKas> mItemPenutupanKas;
        public frmSPJAdministratifPenerimaan()
        {
            InitializeComponent();
        }

        private void frmSPJAdministratifPenerimaan_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Laporan Pertanggungjawaban Administratif.", "Bendahara Penerimaan");
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            ctrlBulan1.Create();
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlSKPD1.SetID(GlobalVar.Pengguna.SKPD);
                m_IDSKPD = GlobalVar.Pengguna.SKPD;
            }
        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
            if (GetBKU() == true)
            {
                LoadData();
            }
        }
        private bool GetBKU()
        {
            try
            {
                int m_IDSKPD = ctrlSKPD1.GetID();

                SetorLogic oSetorLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
                m_lstSetorPenerimaan = new List<Setor>();
                m_lstSTS = new List<STS>();
                STSLogic oLogic = new STSLogic((int)GlobalVar.TahunAnggaran);
                DateTime tanggalAwalTahun = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                mTanggalAkhir = ctrlBulan1.TanggalAkhir;
                m_lstSTS = oLogic.GetByDinas(m_IDSKPD, tanggalAwalTahun, mTanggalAkhir,-1);//, E_JENIS_SETOR.E_SETOR_PAJAK);

                m_lstSetorPenerimaan = oSetorLogic.GetByDinas(m_IDSKPD, E_JENIS_SETOR.E_SETOR_STS, tanggalAwalTahun, mTanggalAkhir);
                if (m_lstSetorPenerimaan == null)
                {
                    MessageBox.Show(oSetorLogic.LastError());
                    return false;
                }
               
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private bool LoadData()
        {

            try
            {
                

                gridPenutupanKas.Rows.Clear();
                mItemPenutupanKas = new List<ItemsPenutupanKas>();
              
                mTanggalAwal = ctrlBulan1.TanggalAwal;
                mTanggalAkhir = ctrlBulan1.TanggalAkhir;


                decimal penerimaan = m_lstSTS.Where(x => x.TanggalSTS >= mTanggalAwal &&
                                        x.TanggalSTS <= mTanggalAkhir ).Sum(bku => bku.Jumlah );
                decimal penerimaanTunai = m_lstSTS.Where(x => x.TanggalSTS >= mTanggalAwal &&
                                     x.TanggalSTS <= mTanggalAkhir &&
                                     x.Jenis==0 && x.KodeUK<=1).Sum(bku => bku.Jumlah);

                decimal penerimaanTunaiPembantu = m_lstSTS.Where(x => x.TanggalSTS >= mTanggalAwal &&
                                     x.TanggalSTS <= mTanggalAkhir &&
                                     x.Jenis == 0 && x.KodeUK >1).Sum(bku => bku.Jumlah);
                decimal penerimaanTransfer = m_lstSTS.Where(x => x.TanggalSTS >= mTanggalAwal &&
                                     x.TanggalSTS <= mTanggalAkhir &&
                                     x.Jenis == 1 && x.KodeUK <= 1).Sum(bku => bku.Jumlah);
                decimal penerimanBulanLalu = m_lstSTS.Where(x => x.TanggalSTS < mTanggalAwal &&
                                     x.Jenis == 0 ).Sum(bku => bku.Jumlah);

                //decimal penerimaanKasBUD = mListBKU.Where(x =>
                //                        x.TanggalTransaksi <= mTanggalAkhir &&
                //                        x.Debet == 1 && x.Kodebank == 1 &&
                //                        x.KodeUk <= 1 && (int)x.JenisBendahara == 1).Sum(bku => bku.Jumlah); ;


                ItemsPenutupanKas p = new ItemsPenutupanKas("A", ".", "PENERIMAAN", "",1, penerimaan.ToRupiahInReport());
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("A", "1", "Tunai Melalui Bendahara Penerimaan " + mTanggalAkhir.ToTanggalIndonesia(), penerimaanTunai.ToRupiahInReport(), 0);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("A", "2", "Tunai Melalui Bendahara Penerimaan Pembantu", penerimaanTunaiPembantu.ToRupiahInReport(), 0);

                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("A", "3", "Transfer ke Rekening Bendahara Penerimaan ", "0.00", 0);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("A", "4", "Transfer ke RekeningKas Umum Daerah" + mTanggalAkhir.ToTanggalIndonesia(), penerimaanTransfer.ToRupiahInReport(), 1);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("B", ".", "Jumlah Penerimaan Yang Harus Disetorkan (A1+A2+A3+A4)","" ,1,(penerimaanTunai + penerimaanTunaiPembantu).ToRupiahInReport());
                mItemPenutupanKas.Add(p);

                decimal jumlhasetor = m_lstSetorPenerimaan.Where(x => x.dtBukuKas >= mTanggalAwal &&
                                                                   x.dtBukuKas <= mTanggalAkhir).Sum(s=>s.Jumlah);

                decimal jumlhasetorBulanLalu = m_lstSetorPenerimaan.Where(x => x.dtBukuKas < mTanggalAwal 
                                                                   ).Sum(s=>s.Jumlah);

                p = new ItemsPenutupanKas("C", ".", "Jumlah Penyetoran","",1, (jumlhasetor).ToRupiahInReport());
                mItemPenutupanKas.Add(p);

                p = new ItemsPenutupanKas("D1", ".", "Saldo Kas di Bendahara Penerimaan Bulan Lalu", "", 1, (penerimanBulanLalu - jumlhasetorBulanLalu).ToRupiahInReport());
                mItemPenutupanKas.Add(p);



                p = new ItemsPenutupanKas("D1", "1", "Bendahara Penerimaan " + mTanggalAwal.ToTanggalIndonesia(), (penerimanBulanLalu - jumlhasetorBulanLalu).ToRupiahInReport(), 0);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("D1", "2", "Bendahara Penerimaan Pembantu", "0,00", 0);

                mItemPenutupanKas.Add(p);

                p = new ItemsPenutupanKas("D2", ".", "Saldo Kas di Bendahara Bulan Ini (B+C+D1)","",1, ((penerimaanTunai-jumlhasetor)+(penerimanBulanLalu - jumlhasetorBulanLalu)).ToRupiahInReport());
                mItemPenutupanKas.Add(p);

                p = new ItemsPenutupanKas("D2", "1", "Bendahara Penerimaan " + mTanggalAwal.ToTanggalIndonesia(), ((penerimaanTunai - jumlhasetor)).ToRupiahInReport(), 0);
                mItemPenutupanKas.Add(p);
                p = new ItemsPenutupanKas("D2", "2", "Bendahara Penerimaan Pembantu", "0,00", 0);

                mItemPenutupanKas.Add(p);



                foreach (ItemsPenutupanKas it in mItemPenutupanKas)
                {
                    string[] row = { it.Item, it.row, it.Keterangan, it.Jumlah, it.bold.ToString(), it.Jumlah2 };
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



                yPos = 20;
                PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);

                float kiri = 30;
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                oCetakPDF=new CetakPDF();
                //stringFormat.CharacterSpacing = 2f;

                //SizeF size = font12.MeasureString("xxx");
                yPos = oCetakPDF.TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), 12, 10, yPos, (page.GetClientSize().Width - 20), stringFormat, true);
                yPos = oCetakPDF.TulisItem(graphics, "LAPORAN PERTANGGUNGJAWABAN ADMINISTRATIF", 12, 10, yPos, (page.GetClientSize().Width - 20), stringFormat, true);
                yPos = oCetakPDF.TulisItem(graphics, "BENDAHARA PENERIMAAN", 12, 10, yPos, (page.GetClientSize().Width - 20), stringFormat, true);
                yPos = yPos + 20;
                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(graphics, "Periode "
                         , 10, kiri, yPos,
                         page.GetClientSize().Width, stringFormat, false, false, false );

                yPos = oCetakPDF.TulisItem(graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, false);

                yPos = oCetakPDF.TulisItem(graphics,
                          ctrlBulan1.NamaBulan, 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, false);

                yPos = oCetakPDF.TulisItem(graphics, "Tahun Anggaran"
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, false);

                yPos = oCetakPDF.TulisItem(graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, false);
                yPos = oCetakPDF.TulisItem(graphics,
                          GlobalVar.TahunAnggaran.ToString(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, false);
                yPos = yPos + 20;
                Pejabat pimpinan = new Pejabat();
                pimpinan = ctrlSKPD1.GetKuaaPenggunaAnggaranPenerimaan(dtCetak.Value);

                if (pimpinan == null)
                {
                    MessageBox.Show("Data Pimpinan belum di setting di master pejabat");
                    return;
                }
                
                stringFormat.Alignment = PdfTextAlignment.Left;
                
                bool bold = false;
                
         
                foreach (DataGridViewRow row in gridPenutupanKas.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (DataFormat.GetString(row.Cells[1].Value) == ".")
                        {
                            bold = true;
                        }
                        else
                            bold = false;
 
                        stringFormat.Alignment = PdfTextAlignment.Left;


                        if (DataFormat.GetString(row.Cells[1].Value) != ".")
                        {
                            yPos = oCetakPDF.TulisItem(graphics, DataFormat.GetString(row.Cells[0].Value), 9, 40, yPos, 30, stringFormat, false,false , bold);
                            yPos = oCetakPDF.TulisItem(graphics, DataFormat.GetString(row.Cells[1].Value), 9, 45, yPos, 10, stringFormat, false, false, bold);
                        }
                        else
                        {
                            yPos = oCetakPDF.TulisItem(graphics, DataFormat.GetString(row.Cells[0].Value), 9, 30, yPos, 20, stringFormat, false, false, bold);
                            yPos = oCetakPDF.TulisItem(graphics, "", 9, 30, yPos, 10, stringFormat, false, bold);
                        }
                   

                        stringFormat.Alignment = PdfTextAlignment.Right;
                        if (DataFormat.GetString(row.Cells[1].Value) != ".")
                            yPos = oCetakPDF.TulisItem(graphics, "Rp. " + DataFormat.GetString(row.Cells[3].Value), 9, 310, yPos, 105, stringFormat, false,false, bold);
                        else
                        {
                            yPos = oCetakPDF.TulisItem(graphics, "Rp. " + DataFormat.GetString(row.Cells[5].Value), 9, 415, yPos, 105, stringFormat, false, false , bold);
                        }
                        stringFormat.Alignment = PdfTextAlignment.Left;
                        if (DataFormat.GetString(row.Cells[1].Value) != ".")
                        {
                            yPos = oCetakPDF.TulisItem(graphics, DataFormat.GetString(row.Cells[2].Value), 9, 70, yPos, 210, stringFormat, true, false, bold);

                        }
                        else
                        {
                            yPos = oCetakPDF.TulisItem(graphics, DataFormat.GetString(row.Cells[2].Value), 9, 60, yPos, 210, stringFormat, true, false, bold);

                        }

                        

                    }
                }



                yPos = yPos + 25;
                Pejabat m_oBendahara = new Pejabat();
                
                m_oBendahara = ctrlSKPD1.GetBendaharaPenerimaan(dtCetak.Value);
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



                yPos = oCetakPDF.TulisItem(graphics, pimpinan.Jabatan, 10, 20, yPos, setengah, stringFormat, false);

                yPos = oCetakPDF.TulisItem(graphics, m_oBendahara.Jabatan, 10, posisiTengah, yPos,
               setengah, stringFormat, true);


                yPos = yPos + 30;


                yPos = oCetakPDF.TulisItem(graphics, pimpinan.Nama, 10, 10, yPos, setengah, stringFormat, false, true);
                yPos = oCetakPDF.TulisItem(graphics, m_oBendahara.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true);

                yPos = oCetakPDF.TulisItem(graphics, "NIP " + pimpinan.NIP, 10, 10, yPos, setengah, stringFormat, false);
                yPos = oCetakPDF.TulisItem(graphics, "NIP " + m_oBendahara.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);
           
                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPJAdministratif.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../SPJAdministratif.pdf");
                pV.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal cetak SPJAdministratif " + System.Environment.NewLine + ex.Message);
            }
        }
    }
}
