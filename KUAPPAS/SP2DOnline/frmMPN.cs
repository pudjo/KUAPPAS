using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO.SP2DOnLine;
namespace KUAPPAS.SP2DOnline
{
    public partial class frmMPN : Form
    {
        public frmMPN()
        {
            InitializeComponent();
        }

        private void frmMPN_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Data Id Billing");
            gridMPN.FormatHeader();
        }
        public string Pesan{
        set{lblPesan.Text = value; }
           }
        public void SetData(DataInquiryPaymentMPNResponseEx data)
        {
            gridMPN.Rows.Clear();
            if (data != null)
            {
                string[] rowx= {"Id Billing" , data.idBilling };;
                    gridMPN.Rows.Add(rowx);

             string[] row= {"nomorSP2D" , data.nomorSP2D};
                gridMPN.Rows.Add(row);
             string[] row1= {"Status Transaksi", data.statusTransaksi };
             gridMPN.Rows.Add(row1);
             string[] row2= {"Jumlah Bayar ",data.jumlahBayar };
                gridMPN.Rows.Add(row2);
             string[] row3= {"No Reference  ",data.referenceNo };
                gridMPN.Rows.Add(row3);
             string[] row4= {"Jenis Pajak ", data.jenisPajak };
                gridMPN.Rows.Add(row4);
             string[] row5= {"namaWajibPajak ", data.namaWajibPajak}; 
                gridMPN.Rows.Add(row5);
             string[] row6= {"ntpn",data.ntpn };
                gridMPN.Rows.Add(row6);
             string[] row7= {"Tanggal Dan Waktu Transaksi",data.tanggalDanWaktuTransaksi };
                gridMPN.Rows.Add(row7);
             string[] row8= {"Tanggal Buku  ",data.tanggalBuku };
                gridMPN.Rows.Add(row8);
             string[] row9= {"WaktuBuku",  data.waktuBuku };
                gridMPN.Rows.Add(row9);
             string[] row10= {"MSG STAN ", data.msgSTAN };
                gridMPN.Rows.Add(row10);
             string[] row11= {"Nomor Pokok Wajib Pajak ", data.nomorPokokWajibPajak };
                gridMPN.Rows.Add(row11);
             string[] row12= {"Alamat Wajib Pajak ",data.alamatWajibPajak };
                gridMPN.Rows.Add(row12);
             string[] row13= {"Kode Map", data.kodeMap };
                gridMPN.Rows.Add(row13);
             string[] row14= {"kodeSetor ", data.kodeSetor}; 
                gridMPN.Rows.Add(row14);
             string[] row15= {"Masa Pajak",data.masaPajak };
                gridMPN.Rows.Add(row15);
             string[] row16= {"Tahun Pajak ",data.tahunPajak}; 
                gridMPN.Rows.Add(row16);
             string[] row17= {"Nomor SK",  data.nomorSk };
                gridMPN.Rows.Add(row17);
             string[] row18= {"Nomor Objek Pajak ", data.nomorObjekPajak };
                gridMPN.Rows.Add(row18);
             string[] row19= {"Kementrian Lembaga ", data.kementrianLembaga}; 
                gridMPN.Rows.Add(row19);
             string[] row20= {"unitEselonI ",data.unitEselonI };
                gridMPN.Rows.Add(row20);
             string[] row21 = { "kodeSatker", data.kodeSatker };
                gridMPN.Rows.Add(row21);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
