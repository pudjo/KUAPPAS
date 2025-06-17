using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SP2DOnline;
using DTO.SP2DOnLine;

namespace KUAPPAS.SP2DOnline
{
    public partial class frmDataNPWP : Form
    {
        public frmDataNPWP()
        {
            InitializeComponent();
        }

        private void frmDataNPWP_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Keterangan NPWP");

        }
        public void SetData(DataInquiriesNPWPResponseEx data)
        {
            txtNama.Text = "";
            txtAlamat.Text = "";
            txtKota.Text = "";
            txtKodeMap.Text = "";
            txtKodeSetor.Text = "";
            txtNPWP.Text = "";
            txtKeteranganKodeMap.Text = "";
            txtKeteranganKodeSetor.Text = "";

            txtNama.Text = data.namaWajibPajak;
            txtAlamat.Text = data.alamatWajibPajak;
            txtKota.Text = data.kota;
            txtKodeMap.Text = data.kodeMap;
            txtKodeSetor.Text = data.kodeSetor;
            txtNPWP.Text = data.nomorPokokWajibPajak;
            txtKeteranganKodeMap.Text  = data.keteranganKodeMap;
            txtKeteranganKodeSetor.Text = data.keteranganKodeSetor;

            

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
