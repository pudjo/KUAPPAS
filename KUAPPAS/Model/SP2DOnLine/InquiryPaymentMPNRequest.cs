using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine
{
    public class InquiryPaymentMPNRequest
    {
        public string idBilling { set; get; } //":"string(15)", // mandatory field
        public string reInquiry { set; get; } // ":"string (5)", // "true" | "false" | mandatory field
        public string referenceNo { set; get; } //":"string (12)" //terdiri 4 digit parameter user 8 digit sequence
        public InquiryPaymentMPNRequest()
        {
            idBilling = "";
            reInquiry = "";
            referenceNo = "";
        }
    }
}
