using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine.Common
{
    public class DataPenerima
    {
        
        public string kodeBank { set; get; } 
        public string namaBank { set; get; } 
        public string noRekening { set; get; } 
        public string namaPenerima { set; get; } 
        public string npwp { set; get; }

        public DataPenerima()
        {
            kodeBank ="";        
            namaBank ="";
            noRekening ="";
            namaPenerima ="";
            npwp = "";
        }

    }
}
