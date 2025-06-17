using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine
{
    public class InquiryRekeningRequest
    {
        public string sandiBank {set;get;}
        public string nomorRekening { set; get; }

    }
    public class InquiryRekeningResponse
    {
        public string response_code { set; get; }
        public string message { set; get; }
        public InquiryRekeningResponseData data { set; get; }

    }
    public class InquiryRekeningResponseData //: InquiryRekeningResponse
    {
        public string nomorRekening { set; get; }
        public string namaPemilikRekening { set; get; }

    }
    public class DataInquiriyRekeningResponEx: InquiryRekeningResponseData
{
    public string error_kode { set; get; } 
    public string message { set; get; } 

}

}
