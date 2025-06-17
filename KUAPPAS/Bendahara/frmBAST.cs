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

namespace KUAPPAS.Bendahara
{
    public partial class frmBAST : Form
    {
        private long m_iNoUrut;
        
        private int  m_idDinas;
        private int m_iKodeUK;
        private int m_idUrusan;
        private int m_idProgram;
        private int m_IDKKegiatan;
        private long m_IDSUbKegiatan;
        private frmListBAST frmParent;
        private int m_iTahapAnggaran;
        private int m_iUnitAnggaran;
        private bool m_bNew;
        public frmBAST()
        {
            InitializeComponent();
            m_iNoUrut = 0;
            m_idDinas = 0;
            m_IDSUbKegiatan = 0;
            m_IDKKegiatan = 0;
            m_idProgram = 0;
            m_bNew = false;
            m_iUnitAnggaran = 0;
        }

        public frmListBAST Parent{
            set
            {
                frmParent = new frmListBAST();
                frmParent = value;
            }
        }
        private void ctrlKontrak1_OnChanged(long pID)
        {
            Kontrak k =  ctrlKontrak1.GetKontrak();
            if (k!=null){
                TanggalKontrak.Tanggal = k.DtKontrak;
                txtKeteranganKontrak.Text  = k.Uraian;
                txtKeteranganKontrak.Enabled = false;
                TanggalKontrak.Enabled = false;
                SetKontrak(k);
              



            }
            
        }

        private void frmBAST_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            TanggalBAST.Tanggal= DateTime.Now.Date;
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                m_idDinas = GlobalVar.Pengguna.SKPD;
                ctrlDinas1.SetID(m_idDinas);
                if (ctrlDinas1.Unit != null)
                {
                    m_iUnitAnggaran = ctrlDinas1.Unit.UntAnggaran;
                }
                if (m_bNew == true)
                {
                    if (ctrlKontrak1.Create(m_idDinas, TanggalBAST.Tanggal) == false)
                    {
                        return;
                    }

                }
            }

        }
        public void SetBAST(BAST oBAST)
        {
            try
            {
                Kontrak oKontrak = oBAST.oKontrak;
                m_bNew = false;
                m_iNoUrut = oBAST.NoUrut;
                m_idDinas = oBAST.IDDInas;
                m_iKodeUK = oBAST.KodeUk;
                txtNoBAST.Text = oBAST.NoBAST;
                ctrlDinas1.Create();
                ctrlDinas1.SetID(m_idDinas, m_iKodeUK);
                if (ctrlDinas1.Unit != null)
                {
                    m_iUnitAnggaran = ctrlDinas1.Unit.UntAnggaran;
                }
                ctrlKontrak1.SetKontrak(m_idDinas, oKontrak);
                if (oKontrak != null)
                {
                    ctrlKontrak1.SetID(oKontrak.NoUrut);
                    txtKeteranganKontrak.Text = oKontrak.Uraian;

                }
                txtKeteranganBAST.Text = oBAST.Uraian;
                TanggalBAST.Tanggal = oBAST.dtBAST;
            //    SetKontrak(oKontrak);
              
                ctrlRekeningKegiatan1.SetBAST(oBAST);

                ctrlPerusahaan1.SetID(oBAST.PihakKetiga);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan BAST ini.."+ ex.Message);

            }

        }
        private bool SetKontrak(Kontrak oKontrak)
        {  
            try
            {
                int tahun = GlobalVar.TahunAnggaran;

              
                //ctrlDinas1.SetID(m_idDinas, m_iKodeUK);
                if (oKontrak.IDDInas != m_idDinas )
                {
                    MessageBox.Show("Ada masalah data Dinas. ");
                    return false;

                }
                ctrlDinas1.UK = oKontrak.KodeUk;
                m_idUrusan = oKontrak.IDUrusan;
                m_idProgram = oKontrak.IDProgram;
                m_IDKKegiatan= oKontrak.IDKegiatan;
                m_IDSUbKegiatan = oKontrak.IDSubKegiatan;
              
                ctrlProgramKegiatan1.SetIDinas(m_idDinas, m_iKodeUK);
                if (ctrlDinas1.Unit != null)
                {
                    m_iUnitAnggaran = ctrlDinas1.Unit.UntAnggaran;
                }
                ctrlRekeningKegiatan1.Clear();
                ctrlProgramKegiatan1.SetSumber(1);
                ctrlProgramKegiatan1.SetValue(m_idDinas, m_iUnitAnggaran, m_idUrusan, m_idProgram, m_IDKKegiatan, m_IDSUbKegiatan);//IDSUbKegiatan);
                m_iTahapAnggaran = ctrlDinas1.GetTahapAnggaran();

                ctrlRekeningKegiatan1.Keperluan = 6;
                ctrlRekeningKegiatan1.SetProgramKegiatan(m_idDinas, m_iUnitAnggaran, m_idUrusan, m_idProgram, m_IDKKegiatan, 3, 0, m_IDSUbKegiatan);
               ctrlRekeningKegiatan1.LoadKontrak(oKontrak.NoUrut);
                
                 ctrlPerusahaan1.SetID(oKontrak.PihakKetiga);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_idDinas = pIDSKPD;
            m_iKodeUK = pIDUK;
            if (ctrlDinas1.Unit != null)
            {
                m_iUnitAnggaran = ctrlDinas1.Unit.UntAnggaran;
            }
            if (m_bNew == true)
            {
                if (ctrlKontrak1.Create(m_idDinas, TanggalBAST.Tanggal) == false)
                {
                    return;
                }

            } 
            //ctrlUrusanPemerintahan1.Create(pIDSKPD, (int)GlobalVar.TahunAnggaran);
        }

        private void ctrlProgram1_OnChanged(int pID)
        {
            m_idProgram = pID;
     //       ctrlKegiatanAPBD1.Create((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan, m_idProgram , 0 ); 
        }

        private void ctrlUrusanPemerintahan1_OnChanged(int pID)
        {
            m_idUrusan = pID;
        //    ctrlProgram1.Create((int)(GlobalVar.TahunAnggaran), m_idDinas, m_idUrusan);
        }

        private void ctrlKegiatanAPBD1_OnChanged(int pID)
        {
            m_IDKKegiatan= pID;
            ctrlRekeningKegiatan1.SetProgramKegiatan(m_idDinas,m_iKodeUK, m_idUrusan, m_idProgram, m_IDKKegiatan, 3, 0,m_IDSUbKegiatan);

        }

        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            try
            {

                ctrlDinas1.Create();
                
                txtNoBAST.Text = "";
                ctrlKontrak1.Clear();
                txtKeteranganBAST.Text = "";
                txtKeteranganKontrak.Text = "";
                ctrlPerusahaan1.Clear();
                ctrlProgramKegiatan1.Clear();
                ctrlRekeningKegiatan1.Clear();
                ctrlKontrak1.Create(m_idDinas,TanggalBAST.Tanggal);

                m_bNew = true;

                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret.ResponseStatus = false;

                return ret;
            }
        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {
            
        }

        private bool CekInput()
        {
            try
            {
                if (ctrlDinas1.PilihanValid == false)
                {
                    return false;
                }
                if (txtNoBAST.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Belum isi no BAST");
                    return false;

                }

                if (txtKeteranganBAST.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Belum isi Keterangan BAST");
                    return false;

                }
                if (ctrlKontrak1.GetID() <= 0) {
                    MessageBox.Show("Belum Memilih SPK/Kontrak.");
                    return false;
                }
                if (ctrlProgramKegiatan1.CekPilihan()==false)
                {
                    MessageBox.Show("Pilihan Program Kegiatan tidak benar.");
                    return false;
                }
                if (ctrlRekeningKegiatan1.JumlahRekening == 0)
                {
                    MessageBox.Show("Belum mengisi nilai BAST");
                    return false;

                }
                if (ctrlPerusahaan1.GetPerusahaan().IDPerusahaan == 0)
                {
                    MessageBox.Show("Belum memilih/Mengisi Pihak ketiga..");
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
        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;

            
             try
            {
                if (CekInput()== false){
                    ret.ResponseStatus = false ;
                    return ret;
                }
            
                BAST oBAST = new BAST();
                oBAST.Tahun = GlobalVar.TahunAnggaran;
                oBAST.NoUrut = m_iNoUrut;
                long nourut = m_iNoUrut;
                oBAST.IDDInas = m_idDinas;
                oBAST.KodeUk = m_iKodeUK;
                oBAST.IDUrusan = m_idUrusan;
                oBAST.IDProgram = m_idProgram;
                oBAST.IDKegiatan = m_IDKKegiatan;
                oBAST.IDSubKegiatan = m_IDSUbKegiatan;
                oBAST.Uraian = txtKeteranganBAST.Text;
                oBAST.NoBAST = txtNoBAST.Text;
                oBAST.PihakKetiga = ctrlPerusahaan1.GetPerusahaan().IDPerusahaan;

                oBAST.dtBAST= TanggalBAST.Tanggal;
                oBAST.Kodekategori = m_idDinas.ToString().ToKodeKategori();
                oBAST.KodeUrusan= m_idDinas.ToString().ToKodeUrusan();
                oBAST.KodeSKPD = m_idDinas.ToString().ToKodeSKPD();
                oBAST.KodekategoriPelaksana = m_idUrusan.ToString().ToKodeKategoriPelaksana();
                oBAST.KodeUrusanPelaksana = m_idUrusan.ToString().ToKodeUrusanPelaksana();
                oBAST.KodeProgram = m_idProgram.ToString().ToKodeProgram();
                oBAST.KodeKegiatan = m_IDKKegiatan.ToString().ToKodeKegiatan();
                oBAST.KodeSubKegiatan = m_IDSUbKegiatan.ToString().ToKodeSubKegiatan();
                oBAST.NoUrutKontrak = ctrlKontrak1.GetID();
                oBAST.NamaPihakKetiga = ctrlPerusahaan1.GetPerusahaan().NamaPerusahaan;
                oBAST.NOKontrak = ctrlKontrak1.GetKontrak().NoKontrak;

                oBAST.Rekening = ctrlRekeningKegiatan1.GetBASTRekening();
                BASTLogic oLogic = new BASTLogic(GlobalVar.TahunAnggaran);
                if (oLogic.Simpan(ref oBAST) == true)
                {
                    m_iNoUrut = oBAST.NoUrut;
                    bool bFound = false;
                 /*   oBAST.oKontrak = ctrlKontrak1.GetKontrak();

                    if (GlobalVar.gListBAST == null)
                    {
                        GlobalVar.gListBAST = oLogic.GetByIDDInas(m_idDinas);
                        KontrakLogic oKontrakLogic = new KontrakLogic(GlobalVar.TahunAnggaran);
                        GlobalVar.gListKontrak = oKontrakLogic.GetByIDDinas(m_idDinas);
                    }


                    foreach (BAST b in GlobalVar.gListBAST){
                        if (b.NoUrut == oBAST.NoUrut)
                        {
                            int idx = GlobalVar.gListBAST.IndexOf(b);
                            GlobalVar.gListBAST[idx] = oBAST;
                            bFound = true;
                            break;
                        }
                    }
                    if (bFound == false)
                    {*/
                 
                    // GlobalVar.gListBAST.Add(oBAST);
                    
                        

                    //}

                }
                else
                {


                }


                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret.ResponseStatus= false;

                return ret;
            }

        }

        private void TanggalBAST_OnChanged(DateTime pTanggal)
        {
            if (m_bNew == true)
            {
                if (ctrlKontrak1.Create(m_idDinas, TanggalBAST.Tanggal) == false)
                {
                    return;
                }

            } 
        }

        private void ctrlKontrak1_Load(object sender, EventArgs e)
        {

        }

        private void TanggalBAST_Load(object sender, EventArgs e)
        {

        }
        public void OnNew()
        {
            ctrlNavigation1.SetNew();
        }

        private void ctrlDinas1_Load_1(object sender, EventArgs e)
        {

        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;


            try
            {


                if (MessageBox.Show("Benar akan menghapus BAST ini?", "Confirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    BASTLogic oLogic = new BASTLogic(GlobalVar.TahunAnggaran);
                    if (oLogic.Hapus(m_iNoUrut) == true)
                    {
                        MessageBox.Show("Data sudah dihapus");
                        ret.ResponseStatus = true;
                    }
                    else
                    {
                        MessageBox.Show(oLogic.LastError());
                        ret.ResponseStatus = false;
                    }
                }
               

                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret.ResponseStatus = false;

                return ret;
            }
           
        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            frmCariKontrak fCariKontrak = new frmCariKontrak();
            fCariKontrak.IDDinas = m_idDinas;

            fCariKontrak.ShowDialog();
            if (fCariKontrak.IsOk() == true)
            {
                Kontrak kontrakdipilih = new Kontrak();
                kontrakdipilih = fCariKontrak.GetKontrakDipilih();
                ctrlKontrak1.SetID(kontrakdipilih.NoUrut);

            }
            
        }
        //private void frmBAST_Load(object sender, EventArgs e)
        //{
            

        //}
    }
}
