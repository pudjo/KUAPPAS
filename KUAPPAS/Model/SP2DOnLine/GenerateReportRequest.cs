using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine
{
    public class GenerateReportRequest
    {
        public string noReferensi { set; get; } 
        public string jenisReport { set; get; } 
        public string tanggalReportAwal { set; get; } 
        public string tanggalReportAkhir { set; get; } 
        public string formatReport { set; get; }

        public GenerateReportRequest()
        {
        
            noReferensi ="";
            jenisReport ="";
            tanggalReportAwal ="";
            tanggalReportAkhir ="";
            formatReport = "";


        }
    }
}
