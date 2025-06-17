using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.SP2DOnLine.DetailTransaksiSP2DOnline57
{
    public class DetailTransaksi57Request
    {
    
    
        public string nomorSP2D { set; get; } 
        public string nomorSPM { set; get; } 
        public string tanggalTransaksi { set; get; } //yyyy-MM-dd
        
        public string referenceNo { set; get; } 
        public string kodeJenisTransaksi { set; get; } //Transfer-OnUs||Transfer-SKN||Transfer-RTGS
        public string notes { set; get; }   //optional

        public DataPengirim pengirim { set; get; }
        public DataPenerima penerima { set; get; }

        public string jumlahNominalTransaksi { set; get; } 
        public string jumlahPotonganMpn { set; get; } 
        public string jumlahPotonganNonMpn { set; get; } 
        public string jumlahDibayar { set; get; } 
        
        public List<DetailPotonganMpn> detailPotonganMpn { set; get; }
        public List<DetailPotonganNonMpn> detailPotonganNonMpn { set; get; }

        //public DetailPotonganMPN detailPotonganMPN { set; get; }
        //public DetailPotonganNonMpn detailPotonganNonMpn { set; get; }
   }
    public class DetailTransaksi57RequestEx
   {
     public string KodeOTP { set; get; }
     public DetailTransaksi57Request data { set; get; }


   }
    public class DataPengirim
    {

        public string noRekening { set; get; } //": "string (10)",
        public string kodeOpd { set; get; } //": "string (30)",
        public string namaOpd { set; get; } //": "string (50)"
    }
    public class DataPenerima
    {

        public string kodeBank { set; get; } 
        public string namaBank { set; get; } 
        public string noRekening { set; get; } 
        public string namaPenerima { set; get; } 
        public string npwp { set; get; } 
    }
    public class DetailPotonganMpn
    {

        public string idBilling { set; get; } 
        public string referenceNo { set; get; } 
        public string keteranganPotongan { set; get; } 
        public string nominalPotongan { set; get; } 
    }

    public class DetailPotonganNonMpn
    {

        public string kodeMap { set; get; } 
        public string keteranganKodeMap { set; get; } 
        public string nominalPotongan { set; get; } 


    }
}
