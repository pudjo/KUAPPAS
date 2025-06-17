using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using Syncfusion.Pdf;
//using Syncfusion.Pdf.Graphics;
//using System.Drawing;
using Syncfusion.Pdf.Graphics;
using DTO.Bendahara;
using BP.Bendahara;
using Formatting;
using DTO.Anggaran;
using BP.Anggaran;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using DTO;

namespace KUAPPAS.Bendahara
{
    public partial class frmLPJTU : ChildForm
    {
        private bool m_bNew;
        private long mNoUrut;
        private DateTime mTanggalAwal;
        private DateTime mTanggalAkhir;
        private int m_IDSKPD;
        private decimal m_cJumlah;
        List<SPJ> mlstSPJ = new List<SPJ>();
        List<BelanjaLPJUP> mListBelanja;
        List<BelanjaLPJUP> mlistBelanjaDipiliih = new List<BelanjaLPJUP>();
        private List<ProgramKegiatanAnggaran> m_lstProgramKegiatan;
        CetakPDF oCetakPDF;
        PdfPage previousPage;
        int m_iJenisBendahara;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        public frmLPJTU()
        {
            InitializeComponent();
            m_bNew = false;
            mNoUrut = 0;
            mListBelanja = new List<BelanjaLPJUP>();
        }

        private void frmLPJTU_Load(object sender, EventArgs e)
        {

            try
            {
                ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
                ctrlHeader1.SetCaption("LPJ TU");

                gridSPJ.FormatHeader();
                gridBelanja.FormatHeader();
                gridBelanja.FormatHeader();
               // gridRekening.FormatHeader();
                dtCetak.Value = DateTime.Now.Date;
                LoadProgramKegiatan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool LoadSPJ()
        {
            try
            {
                gridSPJ.Rows.Clear();
                mlstSPJ = new List<SPJ>();
                SPJLogic oLogic = new SPJLogic(GlobalVar.TahunAnggaran);
                mlstSPJ = oLogic.GetByDInasAndJenis(m_IDSKPD, 2);
                int i = 0;
                if (mlstSPJ != null)
                {
                    foreach (SPJ s in mlstSPJ)
                    {
                        string[] row = { (++i).ToString(), s.NoUrut.ToString(), s.NoSPJ, s.DtSPJ.ToTanggalIndonesia() };
                        gridSPJ.Rows.Add(row);

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
        public bool LoadProgramKegiatan()
        {
            try
            {
                ProgramKegiatanAnggaranLogic oLogic = new ProgramKegiatanAnggaranLogic(GlobalVar.TahunAnggaran);
                m_IDSKPD = ctrlSKPD1.GetID();

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
                        foreach (ProgramKegiatanAnggaran p in lst)
                        {
                            GlobalVar.gListProgramKegiatanRekeningAnggaran.Add(p);
                            m_lstProgramKegiatan.Add(p);
                        }
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


                if (oLogic.IsError())
                {
                    MessageBox.Show(oLogic.LastError());
                    return false;

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

        private void ctrlSKPD1_OnChanged(int pID)
        {
            m_IDSKPD = pID;
            LoadSPJ();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (ctrlSKPD1.GetID() == 0)
            {
                MessageBox.Show("Sila pilih dinas terlebih dahulu");
                return;
            }
            m_bNew = true;
            mNoUrut = 0;
            cmdBatal.Enabled = true;
            cmdAdd.Enabled = false;
            cmdSimpan.Enabled = true;
            txtNoSPJ.Text = "";
            txtKeterangan.Text = "";
            gridBelanja.Rows.Clear();
           // gridRekening.Rows.Clear();
          //  cmdCetak.Enabled = false;
            ctrlSPP1.Create(m_IDSKPD, -1, 2, 4);
        }

        private void cmdTampilkan_Click(object sender, EventArgs e)
        {
            gridBelanja.Rows.Clear();
          
            txtJumlah.Text = "0";
          
            TampilkanBelanja();

        }
        private void TampilkanBelanja()
        {
            try
            {
              
                mListBelanja = new List<BelanjaLPJUP>();
                SPJLogic oLogic = new SPJLogic(GlobalVar.TahunAnggaran);
                decimal Jumlah = 0;
                gridBelanja.Rows.Clear();
                if (LoadDariBelanja())
                {
                    foreach (BelanjaLPJUP b in mListBelanja)
                    {
                        string sPilih = "false";
                        if (mNoUrut != 0)
                        {
                            sPilih = "true";
                        }

                        string[] row = { sPilih, 
                                           b.NoUrut.ToString(), 
                                           b.Tanggal.ToTanggalIndonesia(), 
                                           b.Kode, 
                                           b.KeteranganBelanja, 
                                           b.Jumlah.ToRupiahInReport(),
                                       b.IDProgram.ToString(),
                                       b.IDkegiatan.ToString(),
                                       b.IDSubKegiatan.ToString(),
                                       b.IDRekening.ToString(),
                                       b.NamaRekening};

                        gridBelanja.Rows.Add(row);
                        Jumlah = Jumlah + b.Jumlah;
                    }
                }

                CekProgramKosong();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private bool LoadDariBelanja()
        {
            try
            {
                List<BelanjaLPJUP> listBelanja = new List<BelanjaLPJUP>();
                SPJLogic oLogic = new SPJLogic(GlobalVar.TahunAnggaran);
                if (mNoUrut == 0)
                {
                    listBelanja = oLogic.GetBelanjaUntukLPJTU(m_IDSKPD,ctrlSPP1.GetID(), 0);
                }
                else
                {
                    listBelanja = oLogic.GetBelanjaUntukLPJ(m_IDSKPD, mTanggalAwal, mTanggalAkhir, mNoUrut);
                }
                mListBelanja = listBelanja;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }


        }
        private void CekProgramKosong()
        {
            foreach (DataGridViewRow row in gridBelanja.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    // jika kosong saja
                    if (DataFormat.GetString(row.Cells[6].Value).Trim().Length == 0 ||
                        DataFormat.GetString(row.Cells[7].Value).Trim().Length == 0 ||
                        DataFormat.GetString(row.Cells[8].Value).Trim().Length == 0)
                    {
                        Pengeluaran p = new Pengeluaran();
                        PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
                        p = oLogic.GetByID(DataFormat.GetLong(row.Cells[1].Value));
                        if (p != null)
                        {
                            row.Cells[6].Value = p.IDProgram.ToString();
                            row.Cells[7].Value = p.IDKegiatan.ToString();
                            row.Cells[8].Value = p.IDSUbKegiatan.ToString();
                        }
                    }
                }
            }
        }

        private void ctrlSPP1_Load(object sender, EventArgs e)
        {

        }

        private void cmdPiliSemua_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridBelanja.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    row.Cells[0].Value = true;
                }
            }
            RefreshPilihan();
        }
        
        private void RefreshPilihan()
        {
            try
            {
                mlistBelanjaDipiliih = new List<BelanjaLPJUP>();
                decimal JumlahLPJ = 0l;
                m_cJumlah = 0;
                foreach (DataGridViewRow row in gridBelanja.Rows)
                {
                    if (row.Cells[1].Value != null)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        bool bDipilih = Convert.ToBoolean(row.Cells[0].Value);
                        if (bDipilih == true)
                        {
                            BelanjaLPJUP b = new BelanjaLPJUP();
                            b.KodeUK = 0;
                            b.IDProgram = DataFormat.GetInteger(row.Cells[6].Value);
                            b.IDkegiatan = DataFormat.GetInteger(row.Cells[7].Value);
                            b.IDSubKegiatan = DataFormat.GetLong(row.Cells[8].Value);
                            b.NoUrut = DataFormat.GetLong(row.Cells[1].Value);

                            b.IDRekening = DataFormat.GetLong(row.Cells[9].Value);
                            b.NamaRekening = DataFormat.GetString(row.Cells[10].Value);
                            b.Jumlah = DataFormat.FormatUangReportKeDecimal(row.Cells[5].Value);
                            mlistBelanjaDipiliih.Add(b);
                            JumlahLPJ = JumlahLPJ + b.Jumlah;

                        }
                    }
                }
                txtJumlah.Text = JumlahLPJ.ToRupiahInReport();
                txtJumlahRekening.Text = m_cJumlah.ToRupiahInReport();
                //txtJumlah.Text = JumlahLPJ.ToRupiahInReport();
                decimal sisa = 0;
                SPP spp = ctrlSPP1.GetSPP();
                sisa = spp.Jumlah - JumlahLPJ;
                txtSisa.Text = sisa.ToRupiahInReport();
         

                gridRekening.Rows.Clear();

                if (mlistBelanjaDipiliih.Count > 0)
                {
                    mlistBelanjaDipiliih.OrderBy(x => x.IDProgram).ThenBy(x => x.IDkegiatan).ThenBy(x => x.IDSubKegiatan).ThenBy(x => x.IDRekening);
                    ProgramKegiatanAnggaran pA = new ProgramKegiatanAnggaran();

                    var lstJumlahPerProgram = mlistBelanjaDipiliih.GroupBy(x => x.IDProgram).OrderBy(x => x.Key)
                       .Select(x => new
                       {
                           IDProgram = x.Key,
                           Jumlah = x.Sum(y => y.Jumlah),

                       }).ToList();

                    List<BelanjaLPJUP> BelanjaPerProgram = (from t in lstJumlahPerProgram
                                                            join j in m_lstProgramKegiatan
                                                            on t.IDProgram equals j.IDProgram

                                                            select new BelanjaLPJUP
                                                            {
                                                                IDProgram = t.IDProgram,
                                                                Kode = t.IDProgram.ToKodeProgram(),
                                                                NamaRekening = j.NamaProgram,
                                                                IDSubKegiatan = 0,
                                                                IDkegiatan = 0,
                                                                IDRekening = 0,
                                                                KodeUK = 0,
                                                                Jumlah = t.Jumlah,
                                                            }).ToList<BelanjaLPJUP>();

                    int oldProgrm = 0;

                    foreach (BelanjaLPJUP b in BelanjaPerProgram)
                    {
                        if (oldProgrm != b.IDProgram)
                        {
                            string[] row = { b.KodeUK.ToString(), b.IDProgram.ToString(),
                                     b.IDkegiatan.ToString(), b.IDSubKegiatan.ToString(), 
                                     b.IDRekening.ToString(),b.Kode, b.NamaRekening, b.Jumlah.ToRupiahInReport() };
                            gridRekening.Rows.Add(row);
                            ProsesPilihPerKegiatan(b.IDProgram);
                            oldProgrm = b.IDProgram;
                        }
                    }
                    txtJumlahRekening.Text = m_cJumlah.ToRupiahInReport();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProsesPilihPerKegiatan(int idProgarm)
        {

            mlistBelanjaDipiliih.OrderBy(x => x.IDProgram).ThenBy(x => x.IDkegiatan).ThenBy(x => x.IDSubKegiatan).ThenBy(x => x.IDRekening);
            ProgramKegiatanAnggaran pA = new ProgramKegiatanAnggaran();

            var lstJumlahPerKegiatan = mlistBelanjaDipiliih.Where(p => p.IDProgram == idProgarm).GroupBy(x => x.IDkegiatan).OrderBy(x => x.Key)
               .Select(x => new
               {
                   IDProgram = idProgarm,
                   IDKegiatan = x.Key,
                   Jumlah = x.Sum(y => y.Jumlah),

               }).ToList();

            List<BelanjaLPJUP> BelanjaPerProgram = (from t in lstJumlahPerKegiatan
                                                    join j in m_lstProgramKegiatan
                                                    .Where
                                                    (m => m.IDDInas == m_IDSKPD &&
                                                        m.IDProgram == idProgarm
                                                        )

                                                    on t.IDKegiatan equals j.IDKegiatan

                                                    select new BelanjaLPJUP
                                                    {
                                                        IDProgram = idProgarm,
                                                        IDkegiatan = j.IDKegiatan,
                                                        Kode = t.IDKegiatan.ToKodeKegiatan(),
                                                        NamaRekening = j.NamaKegiatan,
                                                        IDSubKegiatan = 0,

                                                        IDRekening = 0,
                                                        KodeUK = 0,
                                                        Jumlah = t.Jumlah,
                                                    }).ToList<BelanjaLPJUP>();

            int oldIDKegiatan = 0;

            foreach (BelanjaLPJUP b in BelanjaPerProgram)
            {
                if (b.IDProgram == idProgarm && oldIDKegiatan != b.IDkegiatan)
                {
                    string[] row = { b.KodeUK.ToString(), b.IDProgram.ToString(),
                                     b.IDkegiatan.ToString(), b.IDSubKegiatan.ToString(), 
                                     b.IDRekening.ToString(),b.Kode, b.NamaRekening, b.Jumlah.ToRupiahInReport() };
                    gridRekening.Rows.Add(row);
                    ProsesPilihPerSubKegiatan(idProgarm, b.IDkegiatan);
                    oldIDKegiatan = b.IDkegiatan;
                }
            }

        }
        private void ProsesPilihPerSubKegiatan(int idProgarm, int idKegiatan)
        {

            if (idKegiatan == 10302201)
            {
                idKegiatan = 10302201;

            }
            mlistBelanjaDipiliih.OrderBy(x => x.IDProgram).ThenBy(x => x.IDkegiatan).ThenBy(x => x.IDSubKegiatan).ThenBy(x => x.IDRekening);
            ProgramKegiatanAnggaran pA = new ProgramKegiatanAnggaran();

            var lstJumlahPerSuKegiatan = mlistBelanjaDipiliih.Where(p => p.IDProgram == idProgarm &&
                p.IDkegiatan == idKegiatan).GroupBy(x => x.IDSubKegiatan).OrderBy(x => x.Key)
               .Select(x => new
               {
                   IDProgram = idProgarm,
                   IDKegiatan = idKegiatan,

                   IDSubKegiatan = x.Key,
                   Jumlah = x.Sum(y => y.Jumlah),

               }).ToList();

            List<BelanjaLPJUP> BelanjaPerSubKegiatan = (from t in lstJumlahPerSuKegiatan
                                                        join j in m_lstProgramKegiatan.Where
                                                        (m => m.IDDInas == m_IDSKPD &&
                                                            m.IDProgram == idProgarm &&
                                                            m.IDKegiatan == idKegiatan)
                                                        on t.IDSubKegiatan equals j.IDSubKegiatan

                                                        select new BelanjaLPJUP
                                                        {
                                                            IDProgram = idProgarm,
                                                            IDkegiatan = idKegiatan,
                                                            Kode = j.IDSubKegiatan.ToKodeSubKegiatan(),
                                                            NamaRekening = j.NamaSubKegiatan,
                                                            IDSubKegiatan = j.IDSubKegiatan,
                                                            IDRekening = 0,
                                                            KodeUK = 0,
                                                            Jumlah = t.Jumlah,
                                                        }).ToList<BelanjaLPJUP>();

            long oldIDSubKegiatan = 0;

            foreach (BelanjaLPJUP b in BelanjaPerSubKegiatan)
            {
                if (b.IDProgram == idProgarm && oldIDSubKegiatan != b.IDSubKegiatan && b.IDkegiatan == idKegiatan)
                {
                    string[] row = { b.KodeUK.ToString(), b.IDProgram.ToString(),
                                     b.IDkegiatan.ToString(), b.IDSubKegiatan.ToString(), 
                                     b.IDRekening.ToString(),b.Kode, b.NamaRekening, b.Jumlah.ToRupiahInReport() };
                    gridRekening.Rows.Add(row);
                    ProsesPilihPerRekening(idProgarm, idKegiatan, b.IDSubKegiatan);
                    oldIDSubKegiatan = b.IDSubKegiatan;
                }
            }

        }
        private void ProsesPilihPerRekening(int idProgarm, int idKegiatan, long idSubKegiatan)
        {

            mlistBelanjaDipiliih.OrderBy(x => x.IDProgram).ThenBy(x => x.IDkegiatan).ThenBy(x => x.IDSubKegiatan).ThenBy(x => x.IDRekening);
            ProgramKegiatanAnggaran pA = new ProgramKegiatanAnggaran();

            var lstJumlahPerRekening = mlistBelanjaDipiliih.Where(p => p.IDProgram == idProgarm &&
                p.IDkegiatan == idKegiatan && p.IDSubKegiatan == idSubKegiatan
                ).GroupBy(x => x.IDRekening).OrderBy(x => x.Key)
               .Select(x => new
               {
                   IDProgram = idProgarm,
                   IDKegiatan = idKegiatan,
                   IdSubKegiatan = idSubKegiatan,
                   IDRekening = x.Key,
                   Jumlah = x.Sum(y => y.Jumlah),

               }).ToList();

            List<BelanjaLPJUP> BelanjaPerProgram = (from t in lstJumlahPerRekening
                                                    join j in m_lstProgramKegiatan.Where(
                                                    p => p.IDDInas == m_IDSKPD && p.IDProgram == idProgarm &&
                                                        p.IDKegiatan == idKegiatan && p.IDSubKegiatan == idSubKegiatan


                                                    )

                                                    on t.IDRekening equals j.IIDRekening

                                                    select new BelanjaLPJUP
                                                    {
                                                        IDProgram = idProgarm,
                                                        IDkegiatan = j.IDKegiatan,
                                                        Kode = t.IDRekening.ToKodeRekening(),
                                                        NamaRekening = j.NamaRekening,
                                                        IDSubKegiatan = j.IDSubKegiatan,
                                                        IDRekening = j.IIDRekening,
                                                        KodeUK = 0,
                                                        Jumlah = t.Jumlah,
                                                    }).ToList<BelanjaLPJUP>();

            long oldIDrekening = 0;

            foreach (BelanjaLPJUP b in BelanjaPerProgram)
            {
                if (b.IDProgram == idProgarm && oldIDrekening != b.IDRekening &&
                    b.IDkegiatan == idKegiatan && b.IDSubKegiatan == idSubKegiatan)
                {
                    string[] row = { b.KodeUK.ToString(), b.IDProgram.ToString(),
                                     b.IDkegiatan.ToString(), b.IDSubKegiatan.ToString(), 
                                     b.IDRekening.ToString(),b.Kode, b.NamaRekening, b.Jumlah.ToRupiahInReport() };
                    gridRekening.Rows.Add(row);
                    m_cJumlah = m_cJumlah + b.Jumlah;
                    oldIDrekening = b.IDRekening;
                }
            }


        }
        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNoSPJ.Text.Length == 0)
                {
                    MessageBox.Show("Nomor belum didefiniskan");
                    return;
                }
                if (txtKeterangan.Text.Length == 0)
                {
                    MessageBox.Show("Keterangan belum didefiniskan");
                    return;
                }
                if (ctrlTanggal1.Tanggal.Year != GlobalVar.TahunAnggaran)
                {
                    MessageBox.Show("Tanggal  SPJ bukan tahun anggaran");
                    return;
                }
                SPJLogic oLogic = new SPJLogic(GlobalVar.TahunAnggaran);
                SPJ spj = new SPJ();
                spj.NoUrut = mNoUrut;
                spj.NoSPJ = txtNoSPJ.Text;
                spj.IDDinas = m_IDSKPD;
                spj.Tahun = GlobalVar.TahunAnggaran;
                spj.KodeUk = 0;
                spj.Keterangan = txtKeterangan.Text;
                spj.DtSPJ = ctrlTanggal1.Tanggal;
                spj.DtAwal = ctrlTanggal1.Tanggal;
                spj.DtAkhir = ctrlTanggal1.Tanggal;

                spj.Rekenings =new List<SPJRekening>();// tidak ada detail
                
                spj.Jumlah = txtJumlah.Text.FormatUangReportKeDecimal ();
                spj.Jenis = 2;
                spj.NoUrutClient = ctrlSPP1.GetID();
                if (oLogic.Simpan(ref spj))
                {
                    mNoUrut = spj.NoUrut;
                    PengeluaranLogic oPengeluaranLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
                    if (oPengeluaranLogic.SPJKanBelanja(mlistBelanjaDipiliih, mNoUrut) == false)
                    {
                        MessageBox.Show("Kesalahan menyimpan status Belanja");

                    }
                    KoreksiLogic okLogic = new KoreksiLogic(GlobalVar.TahunAnggaran);
                    if (okLogic.SPJKanKoreksi(mlistBelanjaDipiliih, mNoUrut) == false)
                    {
                        MessageBox.Show("Kesalahan menyimpan status Koreksi");
                    }
                    cmdCetak.Enabled = true;
                    MessageBox.Show("Penyimpanan Selesai");
                }
                else
                {
                    MessageBox.Show("Penyimpanan Gagal " + oLogic.LastError());

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Setor GetSetor(long inorutSPP)
        {
            try
            {
                SetorLogic ologic = new SetorLogic(GlobalVar.TahunAnggaran);
                Setor str = ologic.GetByIDClient(inorutSPP);
                if (str != null)
                {
                    txtJumlahSetor.Text = str.Jumlah.ToRupiahInReport();
                    dtSetor.Value = str.dtBukuKas;
                    txtNoSetor.Text = str.NoBukti;
                    return str;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan mengambil data setoran "+  ex.Message);
                return null;
            }

        }
          private void PageAdded(object sender, PageAddedEventArgs args)
          {
            float yPos = PosisiTerakhir + 5;

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;
            PdfStringFormat stringFormat = new PdfStringFormat();


            CetakPDF oCetakPDF = new CetakPDF();


            if (SaatnyacetakKesimpulan == true)
            {





            }






            previousPage = args.Page;
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
                document.Pages.PageAdded += PageAdded;

                yPos = 10;
                PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);


                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;

                //stringFormat.CharacterSpacing = 2f;
                CetakPDF oCetakPDF = new CetakPDF();
                //SizeF size = font12.MeasureString("xxx");

                DateTime tanggal = dtCetak.Value;// ctrlTanggal1.Tanggal;
                Pejabat pimpinan = new Pejabat();
                Pejabat bendahara = new Pejabat();
                Pejabat ppk = new Pejabat();
                pimpinan = ctrlSKPD1.GetKepalaDinas(tanggal);
                bendahara = ctrlSKPD1.GetBendahara(tanggal);
                ppk = ctrlSKPD1.GetPPK(tanggal);

                if (pimpinan == null)
                {
                    MessageBox.Show("Data Pimpinan belum di setting di master pejabat");


                }

                if (bendahara == null)
                {
                    MessageBox.Show("Data Bndahara belum di setting di master pejabat");


                }
                if (ppk == null)
                {
                    MessageBox.Show("Data PPK belum di setting di master pejabat");


                }


                float kiri = 15;


                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                   page.GetClientSize().Width, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "REKAPITULASI LAPORAN PERTANGGUNGJAWABAN TAMBAHAN UANG PERSEDIAAN"
                     , 10, kiri, yPos,
                     page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "BENDAHARA PENGELUARAN"
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
                          ctrlSKPD1.GetNamaSKPD(), 10, 155, yPos,
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
                table.Columns.Add("Kode Rekening");
                table.Columns.Add("Uraian");
                table.Columns.Add("Jumlah");

                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();


                decimal akumulasi = 0L;
                decimal sisa = 0;
                List<int> lstBarisProgram = new List<int>();
                for (int idx = 0; idx < gridRekening.Rows.Count; idx++)
                {
                    table.Rows.Add(new string[]
                    {

                           
                       DataFormat.GetString(gridRekening.Rows[idx].Cells[5].Value),
                       DataFormat.GetString(gridRekening.Rows[idx].Cells[6].Value),
                       DataFormat.GetString(gridRekening.Rows[idx].Cells[7].Value),

                    });
                    if (DataFormat.GetLong(gridRekening.Rows[idx].Cells[4].Value) == 0)
                    {
                        lstBarisProgram.Add(idx);

                    }
                }

                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 100;
                //pdfGrid.Columns[1].Width = 20;
                //// Angka 
                //pdfGrid.Columns[2].Width = 50;
                pdfGrid.Columns[1].Width = 340;

                pdfGrid.Columns[2].Width = 75;


                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                //gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

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

                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {


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
                table.Columns.Add("Nilai");

                columnCount = table.Columns.Count;

                SPP spp = ctrlSPP1.GetSPP();
                decimal jumlahSPP =0;
                if(spp !=null){
                    jumlahSPP = spp.Jumlah;    
                }
                
                data = new List<object>();

              
                table.Rows.Add(new string[]
                    {

                       "Total",
                       txtJumlah.Text,                      
                       
                    });
                table.Rows.Add(new string[]
                    {

                       "Tambahan Uang Persediaan",
                       jumlahSPP.ToRupiahInReport()
                    });
               
                table.Rows.Add(new string[]
                    {

                      "Sisa Tambahan Uang Persediaan",
                      txtSisa.Text
                       
                    });



                pdfGridRingkasan.DataSource = table; //data


                pdfGrid.Columns[0].Width = 398;

                pdfGridRingkasan.Columns[1].Width = 75;

                gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                //gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGridRingkasan.Style = gridStyle;


                formatKolomAngka = new PdfStringFormat();

                formatKolomAngka.Alignment = PdfTextAlignment.Right;
                pdfGridRingkasan.Columns[0].Format = formatKolomAngka;
                pdfGridRingkasan.Columns[1].Format = formatKolomAngka;

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
                yPos = PosisiTerakhir + 5;

                float setengah = (previousPage.GetClientSize().Width / 2) - 20;
                float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;

                stringFormat.Alignment = PdfTextAlignment.Center;

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Mengetahui", 10, 30, yPos, setengah, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Telah diperiksa/diverivikasi dan Oleh PPK SKPD ", 10, 30, yPos, setengah, stringFormat, true, false, true);




                yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.gPemda.Ibukota + "," + tanggal.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ppk.Jabatan, 10, 30, yPos, setengah, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Jabatan, 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ppk.Nama, 10, 30, yPos, setengah, stringFormat, false, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + ppk.NIP, 10, 30, yPos, setengah, stringFormat, false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + bendahara.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Jabatan, 10, 10, yPos, setengah * 2, stringFormat, true, false, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Nama, 10, 10, yPos, setengah * 2, stringFormat, true, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + pimpinan.NIP, 10, 10, yPos, setengah * 2, stringFormat, true);





                SaatnyacetakKesimpulan = true;

                page = document.Pages.Add();

                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPJTU.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../SPJTU.pdf");
                pV.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void txtSisa_TextChanged(object sender, EventArgs e)
        {

        }

        private void ctrlSPP1_OnChanged(long pID)
        {
            GetSetor(pID);

        }

        private void gridSPJ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
