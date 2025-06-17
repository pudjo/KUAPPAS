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

using System.Data.OleDb;
using DTO.Bendahara;
using BP.Bendahara;

namespace KUAPPAS.Bendahara
{
    public partial class frmPajak2 : Form
    {
        private bool mBNew;
        private int m_IDDInas;
        private int m_iKodeUK ;
        private long m_inourut;
        private long m_inourutBelanja;
        public frmPajak2()
        {
            InitializeComponent();
        }

        private void frmPajak2_Load(object sender, EventArgs e)
        {
            if (GlobalVar.Pengguna.Kelompok != 1000 ){
                txtNoUrutPanjar.Visible = false ;
                txtNoUrutSetor.Visible = false;

            }

        }
        public void SetPajak(PajakdanPenyetoran ps)
        {
            try
            {
                ctrlDinas1.Create();
                if (ps.idDinas > 0)
                {
                    ctrlDinas1.SetID(ps.idDinas);
                    m_IDDInas = ps.idDinas;

                    m_iKodeUK = ps.KodeUK;
                }

                txtNoBuktiBelanja.Text = ps.NoBuktiBelanja;
                tanggalSPJ.Value = ps.TanggalBelanja;
                txtKeteranganBelanja.Text = ps.KeteranganBelanja;
                txtNoUrutPanjar.Text = ps.inourutPanjar.ToString();
                txtNoUrutSetor.Text = ps.inourutSetor.ToString();

                txtNoBukti.Text = ps.NoBuktiSetor;
                tanggalSetor.Tanggal = ps.inourutSetor==0? ps.TanggalBelanja:  ps.TanggalSetor;

                txtKeterangan.Text = ps.KeterangabSetor;
                chkBank.Checked = ps.iBank == 1 ? true : false;
                txtNTPN.Text = ps.NTPN;
                txtKodeBILLING.Text = ps.KodeBIlling;
                txtKodePajak.Text = ps.idRekeningBelanja.ToString();
                txtNamaPajak.Text = ps.NamaPungut;
                txtJumlah.Text = ps.inourutSetor > 0 ? ps.JumlahSetor.ToRupiahInReport() : ps.JumlahPungut.ToRupiahInReport();
                
                m_inourut= ps.inourutSetor;
                m_inourutBelanja = ps.inourutPanjar;
                mBNew = ps.inourutSetor > 0 ? true : false;



            }
            catch (Exception ex)
            {

            }

        }
        private bool CekInput()
        {
            if (GlobalVar.Pengguna.Status == 2)
            {
                MessageBox.Show("Pengguna Tidak bisa melakukan Transaksi");
                return false;

            }
            if (ctrlDinas1.PilihanValid == false)
            {
                return false;

            }
            if (txtNoBukti.Text.Trim().Length == 0)
            {
                MessageBox.Show("Belum mengisi No Bukti");
                return false;
            }
            if (txtKeterangan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Belum mengisi Keterangan..");
                return false;
            }
            if (tanggalSetor.Tanggal < tanggalSPJ.Value )
            {
                MessageBox.Show("Mask sih tanggal setornya lebih dulu dari tanggal pungutnya");
                return false;
            }
            if (tanggalSetor.Tanggal.Year != GlobalVar.TahunAnggaran)
            {
                MessageBox.Show("Tanggal Salah..");
                return false;
            }
            if (DataFormat.FormatUangReportKeDecimal(txtJumlah.Text) == 0)
            {
                MessageBox.Show("Belum ada pilihan Pajak..");
                return false;
            }

            return true;

        }
        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            try
            {

                if (CekInput() == false)
                {
                    return;
                }
                Setor oSetor = new Setor();
                oSetor.NoUrut = m_inourut;
                oSetor.IDDinas = m_IDDInas;
                oSetor.KodeUK = m_iKodeUK;

                oSetor.IDUrusan = DataFormat.GetInteger(m_IDDInas.ToString().Substring(0, 3));
                oSetor.IDProgram = 0;
                oSetor.IDKegiatan = 0;
                oSetor.IDSubKegiatan = 0;
                oSetor.KodeKategori = m_IDDInas.ToString().ToKodeKategori();
                oSetor.KodeUrusan = m_IDDInas.ToString().ToKodeUrusan();
                oSetor.KodeSKPD = m_IDDInas.ToString().ToKodeSKPD();
                oSetor.KodekategoriPelaksana = m_IDDInas.ToString().ToKodeKategoriPelaksana();

                oSetor.KodeUrusanPelaksana = m_IDDInas.ToString().ToKodeUrusanPelaksana();
                oSetor.KodeProgram = 0;
                oSetor.KodeKegiatan = 0;
                oSetor.KodeSubKegiatan = 0;


                oSetor.NoBukti = txtNoBukti.Text.Trim();
                oSetor.dtBukuKas = tanggalSetor.Tanggal;
                oSetor.Keterangan = txtKeterangan.Text;
                oSetor.NoUrutClient = 0;
                oSetor.JenisSP2D = 1;
                oSetor.Jenis = (int)E_JENIS_SETOR.E_SETOR_PAJAK;

                oSetor.JenisBendahara = 2;

                oSetor.TahunLalu = 0;
                oSetor.Kodebank = chkBank.Checked ? 1 : 0;
                oSetor.Tahun = GlobalVar.TahunAnggaran;
                oSetor.NobuktiClient = "";
                oSetor.NamaBank = "";
                oSetor.NoNTPN = txtNTPN.Text.Trim();
                oSetor.KodeBilling = txtKodeBILLING.Text.Trim();
                oSetor.NoRekening = "";
         
                oSetor.NourutBayangan = "";
                oSetor.NPWP = "";
                oSetor.Penerima = "";
                oSetor.PPKD = 0;
                oSetor.SetorKeKasda = 1;
                oSetor.Sumber = 1;
                oSetor.Alamat = "";
                oSetor.UnitAnggaran = ctrlDinas1.UnitAnggaran;
                oSetor.Jumlah = 0;

                oSetor.Details = GetDetail();
                foreach (SetorRekening sr in oSetor.Details)
                {
                    oSetor.Jumlah = oSetor.Jumlah + sr.Jumlah;
                }





                SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                long noUrut = 0;
                noUrut = oLogic.Simpan(oSetor);

                if (oLogic.IsError())
                {
                    MessageBox.Show("Kesalahan menyimpan pengembalian Belanja" + oLogic.LastError());
                }
                else
                {
                    m_inourut = noUrut;
                    mBNew = false;

                    MessageBox.Show("Penyimpanan selesai..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan dalam penyimpanan penyetoran Pajak");

            }

           

        }

        private List<SetorRekening> GetDetail()
        {
            List<SetorRekening> lst = new List<SetorRekening>();
            SetorRekening sr = new SetorRekening();
                           
            sr.KodeuK = m_iKodeUK;
            sr.IDUrusan = 0;
            sr.IDProgram = 0;
            sr.IdKegiatan = 0;
            sr.IDSubKegiatan = 0;
            sr.NoUrutBelanja = m_inourutBelanja;
            sr.IDRekening   = DataFormat.GetLong(txtKodePajak.Text );
            sr.Jumlah = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
                           
            lst.Add(sr);
            return lst;

        }

        private void cmdBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
