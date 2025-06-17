using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using DTO;
using Formatting;


namespace KUAPPAS
{
    public partial class frmSettingPemda : Form
    {
        public frmSettingPemda()
        {
            InitializeComponent();
        }
        private void IsiComboJenis()
        {
            cmbJenis.Items.Clear();
            cmbJenis.Items.Add("Provinsi");
            cmbJenis.Items.Add("Kota");
            cmbJenis.Items.Add("Kabupaten");
        }
        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            PemdaLogic oLogic = new PemdaLogic(GlobalVar.TahunAnggaran);
            Pemda oPemda = new Pemda();
            GlobalVar.gPemda.ID = 1;
            GlobalVar.gPemda.Jenis = cmbJenis.SelectedIndex;

            GlobalVar.gPemda.Nama = txtNamaDearah.Text;
            GlobalVar.gPemda.Ibukota = txtIbukota.Text;
            GlobalVar.gPemda.Alamat = txtAlamat.Text;
            GlobalVar.gPemda.NamaKaDaerah = txtNamaPimpinanDaerah.Text;
            GlobalVar.gPemda.JabatanKaDaerah = txtJabatanPimpinanDaerah.Text;
            if (oLogic.Simpan(GlobalVar.gPemda) == false)
            {
                MessageBox.Show(oLogic.LastError());
            }
            else
            {
                MessageBox.Show("Penyimpanan Berhasil.");
            }

            return;


        }

        private void frmSettingPemda_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ctrlHeader1.SetCaption("Setting Daerah", "");

            IsiComboJenis();
            LoadDaerah();
        }
        private void LoadDaerah()
        {
            //PemdaLogic oLogic = new PemdaLogic(GlobalVar.TahunAnggaran);
            //Pemda oPemda = new Pemda();
            //oPemda = oLogic.Get();
            IsiComboJenis();

            cmbJenis.SelectedIndex = GlobalVar.gPemda.Jenis == null ? 0 : (int)GlobalVar.gPemda.Jenis;

            txtNamaDearah.Text = GlobalVar.gPemda.Nama;
            txtIbukota.Text = GlobalVar.gPemda.Ibukota;
            txtAlamat.Text = GlobalVar.gPemda.Alamat;
            txtNamaPimpinanDaerah.Text = GlobalVar.gPemda.NamaKaDaerah;
            txtJabatanPimpinanDaerah.Text = GlobalVar.gPemda.JabatanKaDaerah;

            return;

        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd1 = new OpenFileDialog();
        
            fd1.Filter = "image files|*.jpg;*.png;*.gif;*.icon;.*;";
            DialogResult dres1 = fd1.ShowDialog();
            if (dres1 == DialogResult.Abort)
                return;
            if (dres1 == DialogResult.Cancel)
                return;
            MemoryStream m = new MemoryStream();

         //   textBox4.Text = fd1.FileName;
        
        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }
    }
}
