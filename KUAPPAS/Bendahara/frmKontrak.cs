using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using BP.Bendahara;
using DTO;
using DTO.Bendahara;
using Formatting;
namespace KUAPPAS.Bendahara
{
    public partial class frmKontrak : Form
    {
        public int m_idUrusan = 0;
        public int m_idProgram = 0;
        public int  m_idKegiatan = 0;
        public long m_iNoUrut;
        public int m_idDinas;
        public long m_idSubKegiatan;
        private int m_iTahapAnggaran;
        private int m_iUnitAnggaran;

        private long m_ID;
        private int m_iKodeUK;
        public frmKontrak()
        {
            InitializeComponent();
            m_iUnitAnggaran = 0;
            m_idDinas = 0;
            m_iKodeUK = 0;
            m_idUrusan = 0;
            m_idProgram = 0;
            m_idKegiatan = 0;
            m_idSubKegiatan = 0;
            m_iNoUrut = 0L;

        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }
         public void SetNew()
        {
            ctrlNavigation1.SetNew();
        }
        private void frmKontrak_Load(object sender, EventArgs e)
        {
           
            ctrlRekeningKegiatan1.Keperluan = 4;
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                m_idDinas = GlobalVar.Pengguna.SKPD;
                ctrlDinas1.SetID(m_idDinas);
                if (ctrlDinas1.Unit != null)
                {
                    m_iUnitAnggaran = ctrlDinas1.Unit.UntAnggaran;
                }
                m_iTahapAnggaran = ctrlDinas1.GetTahapAnggaran();
                ctrlProgramKegiatan1.SetIDinas(m_idDinas, m_iUnitAnggaran);
                ctrlProgramKegiatan1.SetSumber(1);
            }
           
        }
        public void CreateNew()
        {
            ctrlNavigation1.ToAdd();
        }
        public void OnNew()
        {
            
           



        }

        private void ctrlUrusanPemerintahan1_OnChanged(int pID)
        {
            //m_idUrusan = pID;
            //ctrlProgram1.Create((int)(GlobalVar.TahunAnggaran),m_idDinas, m_idUrusan);

        }

        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            ctrlDinas1.Create();

            m_idDinas = ctrlDinas1.GetID();
            m_iKodeUK = ctrlDinas1.GetKodeUK();
            m_ID = 0;
            txtNoKontrak.Text = "";
            txtWaktuPelaksanaan.Text = "";
            txtKeterangan.Text = "";
            ctrlTanggal1.Tanggal = DateTime.Now.Date;
            ctrlRekeningKegiatan1.Clear();
            ctrlRekeningKegiatan1.Keperluan = 5;
            ctrlProgramKegiatan1.Clear();
            ctrlPerusahaan1.Clear();

            //  ctrlPerusahaan1.SetID(0);
            m_iNoUrut = 0;

            return ret;
        }
        
        public void SetKontrak(Kontrak  oKontrak)
        {
            try { 
            m_iNoUrut = oKontrak.NoUrut;
            if (oKontrak != null)
                {
                    int tahun = GlobalVar.TahunAnggaran;
                    ctrlDinas1.Create();
                    m_ID = oKontrak.NoUrut;
                    m_idDinas = oKontrak.IDDInas;
                    m_iKodeUK = oKontrak.KodeUk;
                    ctrlDinas1.SetID(m_idDinas, m_iKodeUK);
                    if (ctrlDinas1.Unit != null)
                    {
                        m_iUnitAnggaran = ctrlDinas1.Unit.UntAnggaran;
                    }
                    m_idUrusan = oKontrak.IDUrusan;
                    m_idProgram = oKontrak.IDProgram;
                    m_idKegiatan = oKontrak.IDKegiatan;
                    m_idSubKegiatan = oKontrak.IDSubKegiatan;
                    txtKeterangan.Text = oKontrak.Uraian;
                    txtNoKontrak.Text = oKontrak.NoKontrak;
                    txtWaktuPelaksanaan.Text = oKontrak.WaktuPelaksanaan;
                    ctrlProgramKegiatan1.SetIDinas(m_idDinas, m_iKodeUK);
                    ctrlPeriode1.TanggalAwaal = oKontrak.dAwal;
                    ctrlPeriode1.TanggalAkhir = oKontrak.dAkhir;
                    ctrlProgramKegiatan1.SetSumber(1);
                    ctrlProgramKegiatan1.SetValue(m_idDinas, m_iUnitAnggaran, m_idUrusan, m_idProgram, m_idKegiatan, m_idSubKegiatan);//IDSUbKegiatan);

                    m_iTahapAnggaran = ctrlDinas1.GetTahapAnggaran();
                    ctrlRekeningKegiatan1.Keperluan = 5;
                    ctrlTanggal1.Tanggal = DateTime.Now.Date;// oKontrak.DtKontrak;

                    ctrlRekeningKegiatan1.Clear();
                    ctrlRekeningKegiatan1.SetProgramKegiatan(m_idDinas, m_iUnitAnggaran, m_idUrusan, m_idProgram, m_idKegiatan, 3, 0, m_idSubKegiatan);
                    ctrlRekeningKegiatan1.LoadAnggaran(m_iTahapAnggaran, ctrlTanggal1.Tanggal);
                    ctrlRekeningKegiatan1.SetKontrak(oKontrak);


                    ctrlPerusahaan1.SetID(oKontrak.PihakKetiga);
                    txtJumlah.Text = ctrlRekeningKegiatan1.JumlahRekening.ToRupiahInReport();
                }
                return ;
            } catch(Exception ex){
                MessageBox.Show(ex.Message);
            }

        }
        private bool CekInput()
        {   try
            {
                if (ctrlDinas1.PilihanValid == false)
                {
                    return false;
                }
                if (txtNoKontrak.Text.Trim().Length== 0 )
                {
                    MessageBox.Show("No Kontrak belum diisi");
                    return false;
                }
                if (txtKeterangan.Text.Trim().Length == 0)
                {
                    MessageBox.Show("No keterangan belum diisi");
                    return false;
                }
                if (ctrlTanggal1.Tanggal.Year != GlobalVar.TahunAnggaran)
                {
                    MessageBox.Show("Pilihan tanggal Kontrak salah..");
                    return false;
                }
                if (txtWaktuPelaksanaan.Text.Length==0 )
                {
                    MessageBox.Show("Waktu Pelaksanaan Belum diisi");
                    return false;
                }
                //if (ctrlProgramKegiatan1)


                
            
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
            return true;

        }
        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage ret = new EventResponseMessage();
            try
            {
                Kontrak k = new Kontrak();
                KontrakLogic oLogic = new KontrakLogic(GlobalVar.TahunAnggaran);
                if (CekInput() == false)
                {
                    ret.ResponseStatus = false;
                    return ret;
                }
                Perusahaan p = ctrlPerusahaan1.GetPerusahaan();

                if (p.IDPerusahaan == 0)
                {
                    ret.ResponseStatus = false;
                    ret.Message = "Kesalahan menyimpan dat apihak ketiga";

                    return ret;

                }

                k.Tahun = GlobalVar.TahunAnggaran;
                k.IDDInas = m_idDinas;
                k.KodeUk = m_iKodeUK;
                k.IDUrusan = m_idUrusan;
                k.IDProgram = m_idProgram;
                k.IDKegiatan = m_idKegiatan;
                k.IDSubKegiatan = m_idSubKegiatan;
                k.NoUrut = m_ID;
                k.Kodekategori = m_idDinas.KodeKategori();
                k.KodeUrusan = m_idDinas.KodeUrusan();
                k.KodeSKPD = m_idDinas.KodeSKPD();
                k.KodeUk = m_iKodeUK ;
                k.KodekategoriPelaksana = m_idUrusan.KodeKategoriPelaksana();
                k.KodeUrusanPelaksana = m_idUrusan.KodeUrusanPelaksana();
                k.KodeProgram = m_idProgram.KodeProgram();
                k.KodeKegiatan = m_idKegiatan.KodeKegiatan();
                k.KodeSubKegiatan = m_idSubKegiatan.ToString().ToKodeSubKegiatan();
                k.dAwal = ctrlPeriode1.TanggalAwaal;
                k.dAkhir = ctrlPeriode1.TanggalAkhir;


                k.Uraian = txtKeterangan.Text;
                k.NoKontrak = txtNoKontrak.Text;
                k.DtKontrak = ctrlTanggal1.Tanggal;
                k.PihakKetiga = p.IDPerusahaan;
                k.Rekening = ctrlRekeningKegiatan1.getKontrakRekening();

                if (k.Rekening == null)
                {
                    MessageBox.Show("Terjadi kesalahan membaca kode Rekening...");
                    ret.ResponseStatus = false;
                    return ret;
                }
                if (k.Rekening.Count==0)
                {
                    MessageBox.Show("Belum ada rekening dipilih...");
                    ret.ResponseStatus = false;
                    return ret;

                }
                k.NoUrut = m_iNoUrut;
                k.WaktuPelaksanaan = txtWaktuPelaksanaan.Text;
                m_iNoUrut = oLogic.Simpan(k);
                k.NoUrut= m_iNoUrut;

                if (m_iNoUrut == 0)
                {
                    MessageBox.Show(oLogic.LastError());
                    ret.ResponseStatus = false;
                    return ret;
                }
     
                k.NamaPerusahaan = ctrlPerusahaan1.GetPerusahaan().NamaPerusahaan;
                bool found = false;
                if (GlobalVar.gListKontrak != null)
                {
                    foreach (Kontrak kontrak in GlobalVar.gListKontrak)
                    {

                        if (kontrak.NoUrut == k.NoUrut)
                        {
                            int id = GlobalVar.gListKontrak.IndexOf(kontrak);

                            GlobalVar.gListKontrak[id] = k;
                            found = true;
                            break;
                        }


                    }

                    if (found == false)
                        GlobalVar.gListKontrak.Add(k);
                }
                ret.ResponseStatus = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret.ResponseStatus = false;

            }
            return ret;
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_idDinas = pIDSKPD;
            m_iKodeUK = pIDUK;
            if (ctrlDinas1.Unit != null)
            {
                m_iUnitAnggaran = ctrlDinas1.Unit.UntAnggaran;
            }
            m_iTahapAnggaran = ctrlDinas1.GetTahapAnggaran();
            ctrlProgramKegiatan1.SetIDinas(m_idDinas, m_iUnitAnggaran);
            ctrlProgramKegiatan1.SetSumber(1);


            

            return ;



        }

        
        private void ctrlKegiatanAPBD1_Load(object sender, EventArgs e)
        {
           
        }

        private void ctrlKegiatanAPBD1_OnChanged(int pID)
        {
            m_iKodeUK = ctrlDinas1.GetKodeUK();
            m_idKegiatan = pID;
          
        }

        private void ctrlProgram1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlKegiatanAPBD1_Load_1(object sender, EventArgs e)
        {

        }

        private void ctrlSubKegiatan1_OnChanged(long pID)
        {
            
  //          ctrlRekeningKegiatan1.LoadAnggaran();
        }

        private void ctrlSubKegiatan1_Load(object sender, EventArgs e)
        {

        }

        private void cmdCekRekeningBank_Click(object sender, EventArgs e)
        {
            


        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlRekeningKegiatan1_OnChanged(decimal pJumlah)
        {
            txtJumlah.Text = ctrlRekeningKegiatan1.JumlahRekening.ToRupiahInReport();
        }

        private void ctrlProgramKegiatan1_OnChanged(int pIDurusan, int pIDProgram, int pIDKegiaan, long pIDSubKegiatan)
        {
            m_iKodeUK = ctrlDinas1.GetKodeUK();

            m_idUrusan=pIDurusan;
            m_idProgram=pIDProgram;
            m_idKegiatan=pIDKegiaan;
            m_idSubKegiatan = pIDSubKegiatan;

            ctrlRekeningKegiatan1.Clear();
            ctrlRekeningKegiatan1.Keperluan = 4;

            ctrlRekeningKegiatan1.SetProgramKegiatan(m_idDinas, m_iUnitAnggaran, m_idUrusan, m_idProgram, m_idKegiatan, 3, 0, m_idSubKegiatan);


            
            m_iTahapAnggaran = ctrlDinas1.GetTahapAnggaran();
            ctrlRekeningKegiatan1.LoadAnggaran(m_iTahapAnggaran, ctrlTanggal1.Tanggal);
        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage ret = new EventResponseMessage();
            try
            {
                Kontrak k = new Kontrak();
                KontrakLogic oLogic = new KontrakLogic(GlobalVar.TahunAnggaran);
                if (MessageBox.Show("Apakah benar akan menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool retVal = oLogic.Hapus(m_iNoUrut);

                    MessageBox.Show("Data Kontrak sudah dihapus");
                    //SetNew();
                }
                                ret.ResponseStatus = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret.ResponseStatus = false;

            }
            return ret;
        }

        private void ctrlProgramKegiatan1_Load(object sender, EventArgs e)
        {

        }
    }
}
