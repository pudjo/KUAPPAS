using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DTO.Bendahara;

using BP;
using BP.Bendahara;
using Formatting;
namespace KUAPPAS.Bendahara
{
    public partial class frmKoreksi : Form
    {
        private int m_IDDInas;
        private int m_KodeUK;
        private long m_NoUrut;
        private bool m_bNew;
        private int m_iUnitAnggaran;
        private int m_iTahapanAnggaran;
        private long mNoUrutDiKoreksi;

        private      int m_IDProgram ;
        private  int m_IDUrusan ;
        private  int m_IDKegiatan ;
        private  long m_IDSubKegiatan ;
            private int m_IDProgram2;
            private int m_IDUrusan2;
            private int m_IDKegiatan2;
            private long m_IDSubKegiatan2;
            private int m_iJenisSumber;

        public frmKoreksi()
        {
            InitializeComponent();

            m_IDDInas = 0;
            m_KodeUK = 0;
            m_NoUrut=0;
            m_bNew= false;
        }

        public bool SetKoreksi(Koreksi koreksi)
        {
            try
            {
                m_NoUrut = koreksi.NoUrut;
                m_IDDInas = koreksi.IDDInas;
                m_KodeUK = koreksi.KodeUK;
                
                ctrlDinas1.SetID(m_IDDInas, m_KodeUK);
                m_iUnitAnggaran =koreksi.UnitAnggaran;
                textBox2.Text = koreksi.Uraian;
                m_iTahapanAnggaran = ctrlDinas1.GetTahapAnggaran();
                
                //txtKeterangan.Text = koreksi.Uraian;
                txtNoBukti.Text = koreksi.NoBukti;
                ctrlTanggal1.Tanggal = koreksi.DtKoreksi;
                m_iJenisSumber = 0;
                ctrlPanjar1.Create(m_IDDInas, m_KodeUK, 3, koreksi.DtKoreksi);
                ctrlPanjar1.SetID(koreksi.NoUrutSumber);
                
                refreshBelanja(koreksi.NoUrutSumber);
                ctrlRekeningKegiatan1.SetDataKoreksi(koreksi.Detail);

                chkBedaKegiatan.Checked = koreksi.BedaKegiatan == 1 ? true : false;
                int idx = 0;
                if (koreksi.BedaKegiatan == 1)
                {
                    foreach (KoreksiDetail kd in koreksi.Detail)
                    {

                        // ambil yang kedua 
                        
                        //if (kd.IDSubKegiatan != m_IDSubKegiatan )
                        //{

                        if (kd.IDSubKegiatan != m_IDSubKegiatan)
                        {

                            ctrlProgramKegiatan2.SetValue(
                                m_IDDInas,
                                m_iUnitAnggaran, 
                                kd.IDurusan, 
                                kd.IDProgram, 
                                kd.IDKegiatan , 
                                kd.IDSubKegiatan);//IDSUbKegiatan);

                            ctrlRekeningKegiatan2.Clear();

                            if (ctrlRekeningKegiatan2.LoadAnggaranKas(m_IDDInas, m_iUnitAnggaran, m_iTahapanAnggaran, ctrlTanggal1.Tanggal, kd.IDSubKegiatan) == true)
                            {
                                ctrlRekeningKegiatan2.SetDataKoreksi(koreksi.Detail,1);
                                
                            }
                            break;

                        }
                        idx++;
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
        private void frmKoreksi_Load(object sender, EventArgs e)
        {
           
           

            ctrlRekeningKegiatan1.Keperluan = 3;
            ctrlRekeningKegiatan2.Keperluan = 3;
            if (GlobalVar.Pengguna.SKPD>0){
                ctrlPanjar1.Visible = true;
                m_IDDInas = GlobalVar.Pengguna.SKPD;
                ctrlDinas1.SetID(m_IDDInas);
                
            m_iTahapanAnggaran = ctrlDinas1.GetTahapAnggaran();
            m_iUnitAnggaran = ctrlDinas1.UnitAnggaran;
            ctrlPanjar1.Create(m_IDDInas, m_KodeUK, 3, ctrlTanggal1.Tanggal);

            }



        }
        public bool SetNew()
        {
            try
            {
                ctrlNavigation1.SetNew();
                
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

            m_IDDInas = pIDSKPD;
            m_KodeUK = pIDUK;
            ctrlPanjar1.Visible = true;
            m_iTahapanAnggaran = ctrlDinas1.GetTahapAnggaran();
            m_iUnitAnggaran = ctrlDinas1.UnitAnggaran;
            ctrlPanjar1.Create(m_IDDInas, m_KodeUK, 3, ctrlTanggal1.Tanggal);

        }
        private void refreshBelanja(long pID)
        {
            Pengeluaran p = new Pengeluaran();
            PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
            m_iJenisSumber = 0;
            mNoUrutDiKoreksi = pID;

            p = ctrlPanjar1.GetPengeluaran();
            if (p == null)
            {
                p = oLogic.GetByID(mNoUrutDiKoreksi);
            }
            if (p != null)
            {
                txtJumlah.Text = p.Jumlah.ToRupiahInReport();
                txtKeterangan.Text = p.Uraian;
                m_iUnitAnggaran = p.UnitAnggaran;
                m_iTahapanAnggaran = p.tahap;
                ctrlProgramKegiatan1.SetIDinas(m_IDDInas, m_iUnitAnggaran);
                ctrlProgramKegiatan1.SetSumber(1);
                ctrlProgramKegiatan1.CreateUrusan();
                ctrlProgramKegiatan1.SetValue(m_IDDInas, m_iUnitAnggaran, p.IDUrusan, p.IDProgram, p.IDKegiatan, p.IDSUbKegiatan);//IDSUbKegiatan);
                ctrlRekeningKegiatan1.Clear();
                m_IDSubKegiatan = p.IDSUbKegiatan;
                if (ctrlRekeningKegiatan1.LoadAnggaranKas(m_IDDInas, m_iUnitAnggaran, m_iTahapanAnggaran, p.Tanggal, p.IDSUbKegiatan) == true)
                {
                    decimal cJumlah = ctrlRekeningKegiatan1.CreatePanjar(p.NoUrut);
                    //txtJumlah.Text = cJumlah.ToRupiahInReport();
                }



            }

        }
        private void ctrlPanjar1_OnChanged(long pID)
        {

            refreshBelanja( pID);

            



        }

        private void chkBedaKegiatan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBedaKegiatan.Checked == true)
            {
                lblUrusan.Visible = true;
                lblProgram.Visible = true;
                lblKegiatan.Visible = true;
                lblSUbKegiatan.Visible = true;
                ctrlProgramKegiatan2.SetIDinas(m_IDDInas, m_iUnitAnggaran);
                ctrlProgramKegiatan2.SetSumber(1);
                ctrlProgramKegiatan2.CreateUrusan();
                ctrlProgramKegiatan2.Visible = true;
                ctrlRekeningKegiatan2.Visible = true;

            }
            else
            {
                lblUrusan.Visible = false ;
                lblProgram.Visible = false;
                lblKegiatan.Visible = false;
                lblSUbKegiatan.Visible = false;
                ctrlProgramKegiatan2.Visible = false;
                ctrlRekeningKegiatan2.Visible = false ;
            }
        }

      

        private void ctrlPanjar1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlProgramKegiatan2_OnChanged(int pIDurusan, int pIDProgram, int pIDKegiaan, long pIDSubKegiatan)
        {
            m_IDProgram2 = pIDProgram;
            m_IDUrusan2 = pIDurusan;
            m_IDKegiatan2 = pIDKegiaan;
            m_IDSubKegiatan2 = pIDSubKegiatan;
                       
            ctrlRekeningKegiatan2.Clear();

            ctrlRekeningKegiatan2.CreateDariAnggaranKas(m_IDDInas, m_IDProgram2, m_IDKegiatan2, m_IDSubKegiatan2, ctrlTanggal1.Tanggal, 3, m_iUnitAnggaran);
            
        }

        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            try
            {
     
                ctrlDinas1.Create();
                ctrlTanggal1.Tanggal = DateTime.Now.Date;
                ctrlPanjar1.Visible = true;
                m_iTahapanAnggaran = ctrlDinas1.GetTahapAnggaran();
                m_iUnitAnggaran = ctrlDinas1.UnitAnggaran;
                ctrlPanjar1.Create(m_IDDInas, m_KodeUK, 3, ctrlTanggal1.Tanggal);

                txtNoBukti.Text = "";
                textBox2.Text = "";

                txtKeterangan.Text = "";
                ctrlProgramKegiatan1.Clear();
                ctrlProgramKegiatan2.Clear();
                ctrlRekeningKegiatan1.Clear();
                ctrlRekeningKegiatan2.Clear();
                m_iTahapanAnggaran = ctrlDinas1.GetTahapAnggaran();
                m_bNew = true;
                m_NoUrut = 0;
                txtJumlah.Text = "0";

               

                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret.ResponseStatus = false;
                return ret;
            }
        }

        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            try
            {
                Koreksi k = new Koreksi();
                k.IDDInas = m_IDDInas;
                k.Tahun = GlobalVar.TahunAnggaran;
                k.KodeUK = m_KodeUK;
                k.NoUrut = m_NoUrut;
                k.NoBukti = txtNoBukti.Text.Trim().ReplaceUnicode();
                k.Uraian = textBox2.Text.Trim().ReplaceUnicode();
                k.DtKoreksi = ctrlTanggal1.Tanggal;
                k.NoUrutSumber = ctrlPanjar1.GetID();
                k.JenisBelanja = 1;
                k.JenisSumber = 0;
                k.Kodekategori = m_IDDInas.ToString().ToKodeKategori();
                k.KodeUrusan = m_IDDInas.ToString().ToKodeUrusan();
                k.KodeSKPD = m_IDDInas.ToString().ToKodeSKPD();
                k.Detail = ctrlRekeningKegiatan1.GetKoreksiDetail();
                k.BedaKegiatan = chkBedaKegiatan.Checked == true ? 1 : 0;
                
                    m_iUnitAnggaran = ctrlDinas1.UnitAnggaran;
                
                k.UnitAnggaran = m_iUnitAnggaran;

                if (chkBedaKegiatan.Checked == true)
                {
                    foreach(KoreksiDetail kd in ctrlRekeningKegiatan2.GetKoreksiDetail()){
                        k.Detail.Add(kd);

                    }
                    

                }
                KoreksiLogic okLogic = new KoreksiLogic(GlobalVar.TahunAnggaran);
                if (okLogic.Simpan(k) == false)
                {
                    ret.ResponseStatus = false;
                    MessageBox.Show(okLogic.LastError());
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

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            try
            {
                Koreksi k = new Koreksi();
                k.IDDInas = m_IDDInas;
                k.Tahun = GlobalVar.TahunAnggaran;
                k.KodeUK = m_KodeUK;
                k.NoUrut = m_NoUrut;
              
                KoreksiLogic okLogic = new KoreksiLogic(GlobalVar.TahunAnggaran);
                if (okLogic.Hapus(m_NoUrut) == false)
                {
                    ret.ResponseStatus = false;
                    MessageBox.Show(okLogic.LastError());
                }
                else
                {
                    MessageBox.Show("Data Sudah di hapus");
                    this.Close();

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

        private void ctrlTanggal1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlRekeningKegiatan1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }
    }
}
