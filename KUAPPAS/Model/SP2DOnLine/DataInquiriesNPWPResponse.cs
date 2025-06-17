using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine
{
    public class DataInquiriesNPWPResponse
    {
          public string nomorPokokWajibPajak { set; get; } 
          public string namaWajibPajak { set; get; } 
          public string alamatWajibPajak { set; get; }
          public string kota { set; get; } 
          public string kodeMap { set; get; } 
          public string kodeSetor { set; get; } 
          public string keteranganKodeMap { set; get; } 
          public string keteranganKodeSetor { set; get; }
          
        public DataInquiriesNPWPResponse()
          {
              nomorPokokWajibPajak="";
              namaWajibPajak="";
              alamatWajibPajak ="";
              kota ="";
              kodeMap ="";
              kodeSetor ="";
              keteranganKodeMap ="";
              keteranganKodeSetor ="";

          }
    }
    public class DataInquiriesNPWPResponseEx : DataInquiriesNPWPResponse
    {
        public string error_kode { set; get; } 
        public string message { set; get; } 

    }
}
