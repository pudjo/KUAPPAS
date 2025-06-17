using SP2DOnline;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SP2DOnline;
using Formatting;

namespace KUAPPAS.SP2DOnline
{
    public partial class frmCreateIDBilling : Form
    {
        public frmCreateIDBilling()
        {
            InitializeComponent();
        }

        private void cmdBuatIDBilling_Click(object sender, EventArgs e)
        {
             SP2DOnlineAPI oAPI = new SP2DOnlineAPI();

            CreateIdBillingRequest oCreateIDBilling = new CreateIdBillingRequest();
            oCreateIDBilling.nomorPokokWajibPajak=txtnomorPokokWajibPajak.Text ;
            oCreateIDBilling.kodeMap= txtkodeMap.Text;
            oCreateIDBilling.kodeSetor= txtkodeSetor.Text ;
            oCreateIDBilling.masaPajak= txtmasaPajak.Text ;
            oCreateIDBilling.tahunPajak= txttahunPajak.Text;
            oCreateIDBilling.jumlahBayar= txtjumlahBayar.Text;

            oCreateIDBilling.nomorObjekPajak= txtnomorObjekPajak.Text;
            oCreateIDBilling.nomorSK= txtnomorSk.Text;
            oCreateIDBilling.nomorPokokWajibPajakPenyetor= txtnomorPokokWajibPajakPenyetor.Text ;
            oCreateIDBilling.namaWajibPajak= txtnamawajibpajak.Text;
            oCreateIDBilling.alamatWajibPajak= txtalamatWajibPajak.Text ;
            oCreateIDBilling.kota= txtkota.Text ;

            oCreateIDBilling.nik= txtnik.Text;
            oCreateIDBilling.nomorPokokWajibPajakRekanan= txtnomorPokokWajibPajakRekanan.Text ;
            oCreateIDBilling.nikRekanan= txtnikRekanan.Text;
            oCreateIDBilling.nomorFakturPajak= txtnomorFakturPajak.Text ;
            oCreateIDBilling.nomorSKPD= txtnomorSKPD.Text ;
            oCreateIDBilling.nomorSPM= txtnomorSPM.Text ;

            CreateIdBillingRespon oResp= oAPI.CreateIdBilling (oCreateIDBilling);
                





            if (oResp.response_code != "00")
            {
                MessageBox.Show(oResp.message);
            }
            else
            {

                //Console.WriteLine resp

                frmDataCreateIdilling fData = new frmDataCreateIdilling();
               // fData.SetData(oResp.data);
                fData.Show();



            }

        }

        private void frmCreateIDBilling_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Pembuatan ID Billing");
        
        }
    }
}
