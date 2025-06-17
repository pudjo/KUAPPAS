using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine
{
    public class DataInquiryPaymentMPNResponse
    {       public string idBilling { set; get; } //": "string (15)
             public string nomorSP2D { set; get; } //": "string (30)",
             public string statusTransaksi { set; get; } //": "string (30)",
             public string jumlahBayar { set; get; } //": "string (12)",
             public string referenceNo { set; get; } //": "string (12)",
             public string jenisPajak { set; get; } //": "string (5)", //DJP | DJA | DJBC
             public string namaWajibPajak { set; get; } //": "string (30)",
             public string ntpn { set; get; } //": "string (16)",
             public string tanggalDanWaktuTransaksi { set; get; } //": "string (19)", //yyyy-MM-dd HH:mm:ss
             public string tanggalBuku { set; get; } //": "string (10)", //yyyy-MM-dd
             public string waktuBuku { set; get; } //": "string (8)", //HH:mm:ss
             public string msgSTAN { set; get; } //": "string (6)",
             public string nomorPokokWajibPajak { set; get; } //": "string (15)",
             public string alamatWajibPajak { set; get; } //": "string (50)",
             public string kodeMap { set; get; } //": "string (6)",
             public string kodeSetor { set; get; } //": "string (3)",
             public string masaPajak { set; get; } //": "string (4)",
             public string tahunPajak { set; get; } //": "string (4)",
             public string nomorSk { set; get; } //": "string (15)",
             public string nomorObjekPajak { set; get; } //": "string (18)",
             public string kementrianLembaga { set; get; } //": "string (3)",
             public string unitEselonI { set; get; } //": "string (2)",
             public string kodeSatker { set; get; } //": "string (6)"

    }
    public class DataInquiryPaymentMPNResponseEx: DataInquiryPaymentMPNResponse
{
    public string error_kode { set; get; } 
    public string message { set; get; } 
}

}
