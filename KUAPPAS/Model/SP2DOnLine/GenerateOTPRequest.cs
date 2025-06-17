using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.SP2DOnLine
{
    public class GenerateOTPRequest
    {
  
     public string referenceNo { set; get; } 
     public string phoneNo { set; get; } 
     public string email { set; get; } 
     public string jumlahTransaksi { set; get; } 

 
    }
    public  class DataGenerateOTPResponse
     {
      public string keterangan { set; get; } 
      public string error_kode { set; get; } 
      public string message { set; get; } 
    }

}
