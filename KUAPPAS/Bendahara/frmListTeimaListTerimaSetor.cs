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
namespace KUAPPAS.Bendahara
{
    public partial class frmListTeimaListTerimaSetor :  ChildForm
    {

        private int m_iIDDInas;//m_iSKPD;

        private List<Setor> m_lstSetorPenerimaan;
        private List<SetorRekening> m_listSetorRekening;

        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;


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

        public frmListTeimaListTerimaSetor()
        {
            InitializeComponent();
            m_iIDDInas = 0;
        }

        private void frmListTeimaListTerimaSetor_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Penerimaan Penyetoran di Kasda", "");
            DateTime tanggalsekarang= DateTime.Now.Date;
            ctrlPanelPencarian1.Create();
            ctrlPanelPencarian1.TanggalAwal = new DateTime(tanggalsekarang.Year, tanggalsekarang.Month, 1);
            ctrlPencarian1.setGrid(ref gridSetorSTS);
        }

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {
            
        }
        private bool LoadPenerimaanPenyetoran()
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
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Keslahan mengambil data." + ex.Message);
                return false;

            }

        }
        private bool DisplayPenerimaanPenyetoran()
        {
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
        private void ctrlPanelPencarian1_OnDisplay()
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
                        penerimaan = mListPenerimaanPenyetoran.Where(p => p.Debet == 1 && p.Tanggal >= mTanggalAwal).Sum(j => j.Jumlah);
                        txtPenerimaan.Text = penerimaan.ToRupiahInReport();

                        penyetoran = mListPenerimaanPenyetoran.Where(p => p.Debet == -1 && p.Tanggal >= mTanggalAwal).Sum(j => j.Jumlah);
                        penerimaanLangsung = mListPenerimaanPenyetoran.Where(p => p.Debet == 1 && p.Jenis == 1 && p.Tanggal >= mTanggalAwal).Sum(j => j.Jumlah);
                        txtPenyetoran.Text = (penyetoran + penerimaanLangsung).ToRupiahInReport();
                        txtSaldo.Text = (penerimaan - penerimaanLangsung - penyetoran).ToRupiahInReport();

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void LoadData(int idDinas, DateTime tanggalAwal, DateTime tanggalAkhir)
        {

            SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
            m_lstSetorPenerimaan = new List<Setor>();
            gridSetorSTS.Rows.Clear();
            m_iIDDInas = idDinas;
            m_lstSetorPenerimaan = oLogic.GetByDinas(m_iIDDInas, E_JENIS_SETOR.E_SETOR_STS, tanggalAwal, tanggalAkhir);
            decimal Jumlah = 0L;
            if (m_lstSetorPenerimaan != null)
            {
                foreach (Setor s in m_lstSetorPenerimaan)
                {
                    string[] row = { s.NoUrut.ToString(), "Detail", "BKU Kan", s.dtBukuKas.ToString("dd MMM"), s.NoBukti, s.Keterangan, s.Jumlah.ToRupiahInReport() };
                    gridSetorSTS.Rows.Add(row);
                    Jumlah = Jumlah + s.Jumlah;

                }
                txtJumlah.Text = Jumlah.ToRupiahInReport();

            }

            else
            {
                if (oLogic.IsError() == true)
                {
                    MessageBox.Show(oLogic.LastError());
                }
            }
            LoadDataDetail();
        }
        public void LoadDataDetail()
        {
            DateTime tanggalAwal = ctrlPanelPencarian1.TanggalAwal;
            DateTime tanggalAkhir = ctrlPanelPencarian1.TanggalAkhir;
            m_iIDDInas = ctrlPanelPencarian1.Dinas;
            SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
            m_listSetorRekening = oLogic.GetDetailByDinas(m_iIDDInas, E_JENIS_SETOR.E_SETOR_STS, tanggalAwal, tanggalAkhir);

        }

        private void gridSetorSTS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridSetorSTS.Rows.Count)
            {

                if (e.ColumnIndex == 1)
                {
                    Setor st = new Setor();
                    st = m_lstSetorPenerimaan[e.RowIndex];
                    if (m_listSetorRekening == null)
                    {
                        m_listSetorRekening = new List<SetorRekening>();

                    }
                    if (m_listSetorRekening.Count == 0)
                    {
                        LoadDataDetail();
                    }

                    st.Details = m_listSetorRekening.FindAll(x => x.NoUrut == st.NoUrut);

                    frmSetorPenerimaan fSetor = new frmSetorPenerimaan();

                    fSetor.SetSetor(st);
                    fSetor.ShowDialog();
                }
                if (e.ColumnIndex == 2)
                {
                    Setor st = new Setor();
                    st = m_lstSetorPenerimaan[e.RowIndex];
                    SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                    if (m_listSetorRekening == null)
                    {
                        m_listSetorRekening = new List<SetorRekening>();

                    }
                    if (m_listSetorRekening.Count == 0)
                    {
                        LoadDataDetail();
                    }

                    st.Details = m_listSetorRekening.FindAll(x => x.NoUrut == st.NoUrut);


                    if (oLogic.CatatBKU(st, 1) == true)
                    {
                        MessageBox.Show("Proses BKU selesai");

                    }
                    else
                    {
                        MessageBox.Show("Terjadi kesalahan proses BKU");
                    }


                }

            }
        }
    }
}
