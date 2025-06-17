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
using DTO.Akuntansi;
using Formatting;
using BP.Akuntansi;
namespace KUAPPAS.Akunting
{
    public partial class frmJurnal : Form
    {
        long m_inoJurnal;
        JENIS_JURNAL m_iJenisJurnal;
        JENIS_SUMBERJURNAL m_iSumberJurnal ;
        int m_iDDInas;
        public frmJurnal()
        {
            InitializeComponent();
        }

        private void cmdTambahkan_Click(object sender, EventArgs e)
        {
            JurnalRekeningShow jr = new JurnalRekeningShow();

            jr.Debet = optDebet.Checked == true ? 1 : -1;
            jr.Jumlah = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
            jr.NoBukti = txtNoBukti.Text;
            jr.NamaRekening = txtNamaRekening.Text;
            jr.IIDRekening = DataFormat.GetLong(txtIDRekening.Text.Replace(".", ""));
            jr.Keterangan = txtKeterangan.Text;
            jr.Tanggal = ctrlTanggal1.Tanggal;
            ctrlDaftarRekeningJurnal1.TambahkanKeList(jr);
            ctrlDaftarRekeningJurnal1.Displey();
            if (ctrlDaftarRekeningJurnal1.Valid)
            {
                ctrlNavigation1.Enabled = true;
            }
            else
            {
                ctrlNavigation1.Enabled = false;

            }
            if (m_iJenisJurnal !=JENIS_JURNAL.JENIS_JURNALUMUM){
                JurnalRekeningShow jrP = new JurnalRekeningShow();
                jrP = CariPasangan(DataFormat.GetLong(txtIDRekening.Text.Replace(".", "")));
                if (jrP != null)
                {
                    jrP.Debet = optDebet.Checked == true ? -1 : 1;
                    ctrlDaftarRekeningJurnal1.TambahkanKeList(jrP);
                    ctrlDaftarRekeningJurnal1.Displey();
                    if (ctrlDaftarRekeningJurnal1.Valid)
                    {
                        ctrlNavigation1.Enabled = true;
                    }
                    else
                    {
                        ctrlNavigation1.Enabled = false;

                    }
                }
       
             }
        }
        public JENIS_JURNAL Jenis
        {
            set
            {
                m_iJenisJurnal = value;
                ctrlJenisJurnalPenyesuaian1.Create();
                if (m_iJenisJurnal==JENIS_JURNAL.JENIS_JURNALUMUM){
                    ctrlJenisJurnalPenyesuaian1.Visible = false;
                    lblJenis.Visible = false;
                    this.Text = "Jurnal Umum";

                }
                else
                {
                    ctrlJenisJurnalPenyesuaian1.Visible = true;
                    lblJenis.Visible = true;
                    this.Text = "Jurnal Penyesuaian";
                }
            }
        }
        public JENIS_SUMBERJURNAL Sumber
        {
            set { m_iSumberJurnal = value; }
        }

        private void frmJurnal_Load(object sender, EventArgs e)
        {
            //this.Text = "Jurnal Umum";
        
            ctrlDaftarRekeningJurnal1.Enable = true;
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlDinas1.Create();

                m_iDDInas = GlobalVar.Pengguna.SKPD;

            }
            
        }
        public void SetJurnal(Jurnal j)
        {
            try
            {
                m_inoJurnal = j.NoJurnal;
                ctrlDinas1.Create();
                ctrlDinas1.SetID(j.IDDinas, j.KodeUK);
                ctrlDaftarRekeningJurnal1.Baru = true;
                ctrlJenisJurnalPenyesuaian1.SetID(j.JenisSumber);
                txtNoBukti.Text = j.NoBukti;
                txtKeterangan.Text = j.Uraian;
                ctrlTanggal1.Tanggal = j.TanggalBukti;
                List<JurnalRekeningShow> lst = new List<JurnalRekeningShow>();
                JurnalLogic oLogic = new JurnalLogic(GlobalVar.TahunAnggaran);
                lst = oLogic.GetByNoJurnal(j.NoJurnal);
                if (lst != null)
                {
                    ctrlDaftarRekeningJurnal1.SetJurnal(j.NoJurnal,lst);

                }
                ctrlDaftarRekeningJurnal1.Enable = true;
         
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SetNew()
        {

            ctrlNavigation1.SetNew();
        }
        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            ctrlDinas1.Create();
            ctrlDaftarRekeningJurnal1.Baru = true;
          
            txtNoBukti.Text = "";
            txtKeterangan.Text = "";
            optDebet.Checked = true;
            m_inoJurnal = 0;
            return ret;

        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            frmCariRekening fCariRek = new frmCariRekening();
            fCariRek.FoeJurnal = true;
            fCariRek.Parent = ctrlJenisJurnalPenyesuaian1.GetParent();
            fCariRek.ShowDialog();
         
            if (fCariRek.IsOK())
            {
                Rekening rek = fCariRek.GetRekening();
                if (rek != null)
                {
                    txtIDRekening.Text = rek.ID.ToKodeRekening(6);
                    txtNamaRekening.Text = rek.Nama;
                }

            }
        }
        private JurnalRekeningShow CariPasangan(long id){
            try
            {
                JurnalRekeningShow jr = new JurnalRekeningShow();
                KorelasiPenyesuaian k = new KorelasiPenyesuaian();
                KorelasiPenyesuaianLogic oLogic = new KorelasiPenyesuaianLogic(GlobalVar.TahunAnggaran);
                k = oLogic.GetByIDASET(id);
                if (k == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return null;
                }
                jr.IIDRekening = k.IDRekeningLO;
                jr.NamaRekening = k.NamaRekeningLO;
                jr.Debet = -1;
                jr.Jumlah = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
               
                return jr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private EventResponseMessage ctrlNavigation1_OnSave()
        {

            EventResponseMessage ret = new EventResponseMessage();
            try
            {

                ret.ResponseStatus = true;

                Jurnal j = new Jurnal();
                j.IDDinas = ctrlDinas1.GetID();
                j.KodeUK = ctrlDinas1.GetKodeUK();
                j.Tahun = GlobalVar.TahunAnggaran;
                j.NoBukti = txtNoBukti.Text;
                j.Uraian = txtKeterangan.Text;
                j.TanggalBukti = ctrlTanggal1.Tanggal;
                j.Jenis = (int)m_iJenisJurnal;
                j.JenisSumber = (int)ctrlJenisJurnalPenyesuaian1.GetID();
                j.NoUrutSumber=0;
                j.NoJurnal= m_inoJurnal;
                j.Details = ctrlDaftarRekeningJurnal1.GetJurnalRekenings();
                
                JurnalLogic jLogic = new JurnalLogic(GlobalVar.TahunAnggaran);
                if (jLogic.Simpan(j) == false)
                {
                    MessageBox.Show("Kesalahan menympan Jurnal: "+jLogic.LastError());
                    ret.ResponseStatus = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret.ResponseStatus = false;
            }
            return ret;
        }

        private void ctrlDaftarRekeningJurnal1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage ret = new EventResponseMessage();
            try
            {

                ret.ResponseStatus = true;

                Jurnal j = new Jurnal();
                j.IDDinas = ctrlDinas1.GetID();
                j.KodeUK = ctrlDinas1.GetKodeUK();
                j.Tahun = GlobalVar.TahunAnggaran;
                j.NoBukti = txtNoBukti.Text;
                j.Uraian = txtKeterangan.Text;
                j.TanggalBukti = ctrlTanggal1.Tanggal;
                j.Jenis = (int)m_iJenisJurnal;
                j.JenisSumber = (int)ctrlJenisJurnalPenyesuaian1.GetID();
                j.NoUrutSumber = 0;
                j.NoJurnal = m_inoJurnal;
                j.Details = ctrlDaftarRekeningJurnal1.GetJurnalRekenings();

                JurnalLogic jLogic = new JurnalLogic(GlobalVar.TahunAnggaran);
                if (jLogic.Hapus(m_inoJurnal) == false)
                {
                    MessageBox.Show("Kesalahan menghapus Jurnal: " + jLogic.LastError());
                    ret.ResponseStatus = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret.ResponseStatus = false;
            }
            return ret;
        }
    }
}
