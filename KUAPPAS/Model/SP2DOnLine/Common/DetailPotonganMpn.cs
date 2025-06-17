using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine.Common
{
    public class DetailPotonganMpn
    {
         public string idBilling { set; get; } 
         public string referenceNo { set; get; } 
         public string keteranganPotongan { set; get; } 
         public string nominalPotongan { set; get; }

         public DetailPotonganMpn()
         {


             idBilling = "";
             referenceNo = "";
             keteranganPotongan = "";
             nominalPotongan = "";
         }

    }
}
