using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.SP2DOnLine
{
    public class TransaksiSP2DOnline510Response
    {
         public string response_code { set; get; } 
         public string message { set; get; } 
         public DataTransaksiSP2DOnline510Response data { set; get; }
    }

     public  class DataTransaksiSP2DOnline510Response
     {
         public string tx_id { set; get; } 
         public string nomorSP2D { set; get; } 
         public string nomorSPM { set; get; } 
         public string referenceNo { set; get; } 
         public string kodeJenisTransaksi { set; get; } 
         public string nominalTransaksi { set; get; } 
         public string tanggalTransaksi { set; get; }
         public List<DetailPotonganMpnResponse> detailPotonganMpn { set; get; }

     }
     public class DataTransaksiSP2DOnline510ResponseEx : DataTransaksiSP2DOnline510Response
     {
         public string error_kode { set; get; }
         public string message { set; get; }
     }
        public class DetailPotonganMpnResponse
        {
            public string idBilling { set; get; } 
            public string referenceNo { set; get; } 
            public string nominalPotongan { set; get; } 
            public string statusPaymentMpn { set; get; } 
            public string ntpn { set; get; } 
    
        }
}
