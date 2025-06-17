using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine.Common
{
    public class DataPengirim
    {
          public string noRekening { set; get; } 
          public string kodeOpd { set; get; } 
          public string namaOpd { set; get; }
          public DataPengirim()
          {
              noRekening ="";
              kodeOpd="";
              namaOpd = "";
          }
    }
}
