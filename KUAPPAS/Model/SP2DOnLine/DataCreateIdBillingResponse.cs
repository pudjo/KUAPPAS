using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine
{
     public class DataCreateIdBillingResponse
    {
        public string nomorPokokWajibPajak { set; get; }
        public string namaWajibPajak { set; get; }
        public string alamatWajibPajak { set; get; }
        public string kota { set; get; }
        public string nik { set; get; }
        public string kodeMap { set; get; }
        public string kodeSetor { set; get; }
        public string masaPajak { set; get; }
        public string tahunPajak { set; get; }
        public string jumlahBayar { set; get; }
        public string nomorObjekPajak { set; get; }
        public string nomorSK { set; get; }
        public string nomorPokokWajibPajakPenyetor { set; get; }
        public string nomorPokokWajibPajakRekanan { set; get; }
        public string nikRekanan { set; get; }
        public string nomorFakturPajak { set; get; }
        public string nomorSKPD { set; get; }
        public string nomorSPM { set; get; }
        public string idBilling { set; get; }
        public string tanggalExpiredBilling { set; get; }
        public string keteranganKodeMap { set; get; }
        public string keteranganKodeSetor { set; get; }

    }
   public class DataCreateIdBillingResponseEx : DataCreateIdBillingResponse
   {
       public string error_kode { set; get; }
       public string message { set; get; }
   }

}
