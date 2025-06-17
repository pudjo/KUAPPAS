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
using BP;
using DTO.SP2DOnLine;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using KUAPPAS.BP.SP2DOnline;

namespace KUAPPAS
{
    public partial class frmPejabatDetail : Form
    {
        private int m_ID;
        private int m_iJenis;
        public frmPejabatDetail()
        {
            InitializeComponent();
            m_ID = 0;
            m_iJenis = 0;
        }
        public void OnNew(){
            
            ctrlJabatan1.Create();
            
            txtNama.Text = "";
            txtNIP.Text = "";
            txtNamaJabatan.Text = "";
            txtNamaDalamBank.Text = "";
            txtNoNPWP.Text = "";
            txtNoRekening.Text = "";

            ctrlDaftarBank1.Create();
            m_ID = 0;
        }
        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }

        private void cmdBaruBendahara_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        public bool SetPejabat(Pejabat p)
        {
            if (p !=null){
                m_ID = p.ID;
                ctrlDinas1.Create();
                ctrlDinas1.SetID(p.IDDInas,p.Unit);
               
                ctrlJabatan1.Create();
                ctrlJabatan1.ID = p.Jenis;
                m_iJenis = p.Jenis;

                txtNama.Text = p.Nama;
                txtNIP.Text = p.NIP;
                txtNamaJabatan.Text = p.Jabatan;

                txtNamaDalamBank.Text = p.NamaDalamRekeningBank;
                txtNoNPWP.Text = p.NPWP;
                txtNoRekening.Text = p.NoRekening;
                ctrlTanggal1.Tanggal = p.TanggalAktiv;
                ctrlDaftarBank1.Create();
                ctrlDaftarBank1.SetKode(p.NamaBank,p.NoRekening);
                if (p.Jenis == 8)
                {
                    groupBank.Visible = true;

                }
                else
                {
                    groupBank.Visible = false;

                }

            }
            return true;
        }

        private void cmdSImpanBendaharaPengeluaran_Click(object sender, EventArgs e)
        {

        }
        private bool CekInput()
        {
            try
            {
                if (ctrlJabatan1.ID == 0)
                {
                    MessageBox.Show("Jabatan belum dipilih.");
                    return false;
                }
                if (txtNama.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Belum diisi.");
                    return false;
                }

                if (txtNIP.Text.Trim().Length == 0)
                {
                    MessageBox.Show("NIP Belum diisi.");
                    return false;
                }

                if (txtNamaJabatan.Text.Trim().Length == 0)
                {
                    MessageBox.Show("NIP Belum diisi.");
                    return false;
                }


                if (m_iJenis == 8)
                {
                    if (txtNamaDalamBank.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("Nama dalam Rekening Bank masih Kosong.");
                        return false;
                    }
                    if (txtNoNPWP.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("NPWP masih Kosong.");
                        return false;
                    }
                    if (txtNoNPWP.Text.Trim().Length != 15)
                    {
                        MessageBox.Show("Jumlah angka dalam NPWP tidak benar");
                        return false;
                    }

                    if (txtNoRekening.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("Nomor Rekening Bank masih Kosong.");
                        return false;
                    }


                    return true;
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
            try
            {
                ret.ResponseStatus = true;
                if (CekInput() == false)
                {
                    ret.ResponseStatus = false;
                    return ret;
                }
                Pejabat p = new Pejabat();
                p.ID = m_ID;
                p.Jenis = ctrlJabatan1.ID;
                p.IDDInas = ctrlDinas1.GetID();
                p.Unit = ctrlDinas1.GetKodeUK();
                p.Nama = txtNama.Text;
                p.NIP = txtNIP.Text;
                p.Jabatan = txtNamaJabatan.Text;
                p.NamaDalamRekeningBank = txtNamaDalamBank.Text;
                p.NPWP = txtNoNPWP.Text;
                p.NoRekening = txtNoRekening.Text;

                p.NamaBank = ctrlDaftarBank1.Kode;
                p.TanggalAktiv = ctrlTanggal1.Tanggal;

                PejabatLogic oLogic = new PejabatLogic(GlobalVar.TahunAnggaran);
                if (oLogic.Simpan(ref p) == true)
                {
                    m_ID = p.ID;
                }
                else
                {
                    MessageBox.Show(oLogic.LastError());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return ret;
        }

        private void cmdCekNamaDalamRekening_Click(object sender, EventArgs e)
        {
            CekRekeningBank();
            
        }
        private void   CekRekeningBank()
        {
            try
            {
                InquiryRekeningRequest requestInquiry = new InquiryRekeningRequest();
                requestInquiry.nomorRekening = txtNoRekening.Text.Replace(".", "");
                requestInquiry.sandiBank = ctrlDaftarBank1.Kode;
                SP2DOnlineService service = new SP2DOnlineService();
                DataInquiriyRekeningResponEx resp = service.CekRekening(requestInquiry);
                if (resp.error_kode != "00")
                {
                    MessageBox.Show(resp.message);
                }
                else
                {
                    txtNamaDalamBank.Text = resp.namaPemilikRekening;
                    MessageBox.Show(resp.message);
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

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
            ctrlJabatan1.Create();
            m_ID = 0;
            txtNama.Text = "";
            txtNamaDalamBank.Text = "";
            ctrlDaftarBank1.Create();
            txtNamaJabatan.Text = "";
            txtNIP.Text = "";
            txtNoNPWP.Text = "";
            txtNoRekening.Text = "";
            return ret;

        }

        private void frmPejabatDetail_Load(object sender, EventArgs e)
        {

        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage ret = new EventResponseMessage();
            try
            {
                if (MessageBox.Show("Apa benar akan menghapus data ini?", "Konfirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ret.ResponseStatus = true;
                    Pejabat p = new Pejabat();
                    p.ID = m_ID;
                    p.Jenis = ctrlJabatan1.ID;
                    p.IDDInas = ctrlDinas1.GetID();
                    p.Unit = ctrlDinas1.GetKodeUK();
                    p.Nama = txtNama.Text;
                    PejabatLogic oLogic = new PejabatLogic(GlobalVar.TahunAnggaran);
                    if (oLogic.Hapus(p) == true)
                    {
                        m_ID = p.ID;
                    }
                    else
                    {
                        ret.ResponseStatus = false;
                        MessageBox.Show(oLogic.LastError());
                    }
                }
            }
            catch (Exception ex)
            {
                ret.ResponseStatus = false;
                MessageBox.Show(ex.Message);
            }


            return ret;
        }
            
            
    }
}
