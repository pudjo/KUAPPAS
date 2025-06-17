using DTO;
using DTO.Akuntansi;
using Formatting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Akunting
{
    public partial class frmJuurnal : Form
    {
        long m_inoJurnal;
        public frmJuurnal()
        {
            InitializeComponent();
            m_inoJurnal = 0;
        }

        private void frmJuurnal_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            this.Text = "Jurnal Umum";
            ctrlJurnalRekening1.Enable = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            frmCariRekening fCariRek = new frmCariRekening();
            fCariRek.ShowDialog();
            if (fCariRek.IsOK())
            {
                Rekening rek = fCariRek.GetRekening();
                if (rek != null) { 
                txtIDRekening.Text = rek.ID.ToKodeRekening(6);
                txtNamaRekening.Text = rek.Nama;
                }

            }
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
            ctrlJurnalRekening1.TambahkanKeList(jr);
            ctrlJurnalRekening1.Displey();
            if (ctrlJurnalRekening1.Valid)
            {
                ctrlNavigation1.Enabled = true;
            } else {
                ctrlNavigation1.Enabled = false;
                
            }
        }

        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            ctrlJurnalRekening1.Baru = true;
            txtNoBukti.Text = "";
            txtKeterangan.Text = "";
            optDebet.Checked = true;
            m_inoJurnal = 0;


 
            return ret;
        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

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
