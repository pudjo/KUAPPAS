using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP.Akuntansi;
using Formatting;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using DTO;
using System.IO;
using BP;
using BP.Anggaran;
using DTO;
using DTO.Akuntansi;
using DTO.Anggaran;
using DTO.Laporan;
namespace KUAPPAS.Akunting
{
    public partial class frmBukuBesar : ChildForm 
    {
        private List<BukuBesar> m_lstBukuBesar;

        DateTime mTanggalAkhir;
        DateTime mTanggalAwal;

        private List<Rekening> m_lstRekening;
        private List<Realisasi04AK> m_lstNeracaAwal;

        private int m_iSKPD;
        private long mIDRekening;



        private const int COL_KODEREKENING = 0;
        private const int COL_NAMAREKENING = 1;
        private const int COL_LOKINI = 2;
        private const int COL_LOLALU = 3;
        private const int COL_SELISIH = 4;
        private const int COL_LEVEL = 5;
        private const int COL_IDREKENING = 6;


        private const string CON_STRING_JUMLAHPENDAPATAN = "JP";
        private const string CON_STRING_JUMLAHBELANJA = "JB";
        private const string CON_STRING_SURPLUSDEFISIT = "SD";



        CetakPDF oCetakPDF;
        PdfPage previousPage;
        int m_iJenisBendahara;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;

        decimal JumlahNABeban = 0l;
        decimal JumlahNAPendapatan = 0L;
        int Tahun;


        decimal JumlahBeban = 0l;
        decimal JumlahPendapatan = 0L;
        public frmBukuBesar()
        {
            InitializeComponent();
        }

        private void frmBukuBesar_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Buku Besar");
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);

            ctrlTanggalBulanVertikal1.TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
            ctrlTanggalBulanVertikal1.TanggalAkhir = new DateTime(GlobalVar.TahunAnggaran, 12, 31);
            gridBukuBesar.FormatHeader();
            treeRekening1.Create();
            ctrlPencarian1.setGrid(ref gridBukuBesar);
            gridBukuBesar.FormatHeader();
        }

        public void SetRekening(long idrekening)
        {
            txtIDrekening.Text = idrekening.ToString();

        }
        public bool PangilData()
        {
            try
            {
                m_lstBukuBesar = new List<BukuBesar>();
                int iLevelRekening = 6;

                List<BukuBesar> lstInRpt = new List<BukuBesar>();
                BukuBesarLogic oLogic = new BukuBesarLogic(GlobalVar.TahunAnggaran);
                m_iSKPD = ctrlSKPD1.GetID();
                mTanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
                if (txtIDrekening.Text.Trim().Replace(".", "").Length == 0)
                {
                    MessageBox.Show("Kode Rekenig Belum di defnisikan..");
                    txtIDrekening.Focus();
                    return false;
                }
                mIDRekening = DataFormat.GetLong(txtIDrekening.Text.Trim().Replace(".", ""));

                m_lstBukuBesar = oLogic.GetBukuBesar(m_iSKPD, mTanggalAkhir, mIDRekening);


                if (m_lstBukuBesar == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return false;
                }

                gridBukuBesar.Rows.Clear();
                decimal saldo = 0l;
                foreach (BukuBesar v in m_lstBukuBesar)
                {

                    decimal debet;
                    decimal kredit;
                    debet = v.Debet == 1 ? v.Jumlah : 0;
                    kredit = v.Debet == -1 ? v.Jumlah : 0;
                    saldo = saldo + (v.SaldoNormal * debet) - (v.SaldoNormal * kredit);
                    string[] row = { v.TanggalTransaksi.ToTanggalIndonesia(), v.NoBukti,
                                         v.Keterangan, debet.ToRupiahInReport(), kredit.ToRupiahInReport() ,
                                         saldo.ToRupiahInReport(),
                                         v.NoSumber.ToString()
                                     };
                    gridBukuBesar.Rows.Add(row);
                }


                MessageBox.Show("Pemanggilan Selesai..");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }

        }
        private void cmdPanggil_Click(object sender, EventArgs e)
        {
            GetSaldoAwal();
            PangilData();   

        }
        private void GetSaldoAwal()
        {
            decimal saldoAwal = 0;
            
            int iLevelRekening = 6;

            SaldoAwalRehLogic oLogic = new SaldoAwalRehLogic(GlobalVar.TahunAnggaran);
            
            m_iSKPD = ctrlSKPD1.GetID();
            mTanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
            if (txtIDrekening.Text.Trim().Replace(".", "").Length == 0)
            {
                MessageBox.Show("Kode Rekenig Belum di defnisikan..");
                txtIDrekening.Focus();
                return ;
            }
            int tahun = GlobalVar.TahunAnggaran - 1;
            mIDRekening = DataFormat.GetLong(txtIDrekening.Text.Trim().Replace(".", ""));
            saldoAwal = oLogic.GetSaldoAwal(m_iSKPD, tahun, mIDRekening);
            txtSaldoAwal.Text = saldoAwal.ToRupiahInReport();

        }
        private void treeRekening1_Load(object sender, EventArgs e)
        {

        }

        private void treeRekening1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void treeRekening1_DoubleClicking(global::DTO.Rekening rek)
        {
            txtIDrekening.Text = rek.ID.ToString();
            mIDRekening = rek.ID;
            txtNamaRekening.Text = rek.Nama;
        }

        private void ctrlPencarian1_Load(object sender, EventArgs e)
        {

        }
    }
}
