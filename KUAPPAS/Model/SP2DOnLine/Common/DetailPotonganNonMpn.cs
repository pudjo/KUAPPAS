using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine.Common
{
    public class DetailPotonganNonMpn
    {
         public string kodeMap { set; get; } 
         public string keteranganKodeMap { set; get; } 
         public string nominalPotongan { set; get; }

         public DetailPotonganNonMpn()
         {
             kodeMap ="";
             keteranganKodeMap ="";
             nominalPotongan ="";
         }
         
    }
}
