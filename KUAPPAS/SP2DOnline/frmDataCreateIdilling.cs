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
    public partial class frmDataCreateIdilling : Form
    {
        public frmDataCreateIdilling()
        {
            InitializeComponent();
        }

        private void frmDataCreateIdilling_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Data hasil pembuatan IDBilling");
        }
        public void SetData(DataCreateIdBillingResponseEx data)
        {
            txtnomorPokokWajibPajakPenyetor.Text = data.nomorPokokWajibPajak;
            txtnamawajibpajak.Text = data.namaWajibPajak;
            txtalamatWajibPajak.Text = data.alamatWajibPajak;
            txtkota.Text = data.kota;
            txtnik.Text = data.nik;
            txtkodeMap.Text = data.kodeMap;
            txtkodeSetor.Text = data.kodeSetor;
            txtmasaPajak.Text = data.masaPajak;
            txttahunPajak.Text = data.tahunPajak;
            txtjumlahBayar.Text = data.jumlahBayar;
            txtnomorObjekPajak.Text = data.nomorObjekPajak;
            txtnomorSk.Text = data.nomorSK;
            txtnomorPokokWajibPajakPenyetor.Text = data.nomorPokokWajibPajakPenyetor;
            txtnomorPokokWajibPajakRekanan.Text = data.nomorPokokWajibPajakRekanan;
           
            txtnikRekanan.Text = data.nikRekanan;
            txtnomorFakturPajak.Text = data.nomorFakturPajak;
            txtnomorSKPD.Text = data.nomorSKPD;
            txtnomorSPM.Text = data.nomorSPM;
            txtidBilling.Text = data.idBilling;
            txttanggalExpiredBilling.Text = data.tanggalExpiredBilling;
            txtketeranganKodeMap.Text = data.keteranganKodeMap;
            txtketeranganKodeSetor.Text = data.keteranganKodeSetor;
        }

        private void cmdInquiryMPN_Click(object sender, EventArgs e)
        {
            SP2DOnlineAPI oAPI = new SP2DOnlineAPI();
            InquiryMPN inquirtMPN = new InquiryMPN();
            inquirtMPN.idBilling = txtidBilling.Text;
            inquirtMPN.reInquiry = "false";
            inquirtMPN.referenceNo = "";
            InquiryMPNespon resp=oAPI.InQuirMPN(inquirtMPN);

            if (resp.response_code != "00")
            {
                MessageBox.Show(resp.message);
            }
            else
            {

                //Console.WriteLine resp

                frmDataCreateIdilling fData = new frmDataCreateIdilling();
                //fData.SetData(resp.data);
                fData.Show();
                
            }
                    }
    }
}
