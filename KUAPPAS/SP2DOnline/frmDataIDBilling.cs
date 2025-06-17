using SP2DOnline;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS.SP2DOnline
{
    public partial class frmDataIDBilling : Form
    {
        public frmDataIDBilling()
        {
            InitializeComponent();
        }

        private void frmDataIDBilling_Load(object sender, EventArgs e)
        {

        }
        public void SetData(InquiriesIdBillingRespon databilling)
        {
            txtidbilling.Text = databilling.data.idBilling;
            txtStatusTransaksi.Text = databilling.data.statusTransaksi;
            txtntpn.Text = databilling.data.ntpn;
            txtntb.Text = databilling.data.ntb;
            txtjenispajak.Text = databilling.data.jenisPajak;
            txttanggalwaktuttransaksi.Text = databilling.data.tanggalDanWaktuTransaksi;
            txtTanggalbuku.Text = databilling.data.tanggalBuku;
            txtnominal.Text = databilling.data.nominal.ToString();
            txtnpwp.Text = databilling.data.nomorPokokWajibPajak;
            txtnamawajibpajak.Text = databilling.data.namaWajibPajak;
            txtAlamat.Text = databilling.data.alamatWajibPajak;
            txtkodeMap.Text = databilling.data.kodeMap;
            txtkodeSetor.Text = databilling.data.kodeSetor;
            txtmasaPajak.Text = databilling.data.masaPajak;
            txtnomorSk.Text = databilling.data.nomorSk;
            txttahunPajak.Text = databilling.data.tahunPajak;
            txtnomorObjekPajak.Text = databilling.data.nomorObjekPajak;



        }
    }
}
