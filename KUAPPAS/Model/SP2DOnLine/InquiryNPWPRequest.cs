using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine
{
    public class InquiryNPWPRequest
    {
        public string kodeMap { set; get; } 
        public string kodeSetor { set; get; } 
        public string nomorPokokWajibPajak { set; get; }

        public InquiryNPWPRequest()
        {
            kodeMap ="";
            kodeSetor ="";
            nomorPokokWajibPajak ="";

        }


    }
}
