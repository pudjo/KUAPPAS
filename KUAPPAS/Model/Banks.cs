using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
  
    public class Banks
    {
        public string bankCode { set; get; }
        public string Nama { set; get; }

        public string accountNumber { set; get; }
        
        public string bic { set; get; }
        

    }

    public class BICBank{

        public string KodeBIC {set;get;}
        public string Nama {set;get;}
        public string ShortName{set;get;}
        public string Kode {set;get;}

    }



}
