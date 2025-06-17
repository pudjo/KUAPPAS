using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine
{
    public class DataGenerateReportResponseEx
    {
         public string noReferensi { set; get; } 
         public string jenisReport { set; get; } 
         public string formatReport { set; get; } 
         public string tanggalReportAwal { set; get; } 
         public string tanggalReportAkhir { set; get; } 
         public string tanggalExpiredLink { set; get; } 
         public string linkDownload { set; get; } 
         public string error_kode { set; get; } 
         public string message { set; get; } 
    }
}
