using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class BatasUP
    {
   
        public int IDDinas { set; get; }
        public int Tahun { set; get; }
        public int KodeUK { set; get; }

        public string  NamaSKPD { set; get; }
       

        public decimal Jumlah{ set; get; }
        
    }
}
