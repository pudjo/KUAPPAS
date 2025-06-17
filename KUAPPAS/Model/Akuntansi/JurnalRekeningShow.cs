using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
   
    public class JurnalRekeningShow
    {
       
     
            public long NoJurnal { set; get; }
            public string NoBukti { set; get; }
            public string Keterangan { set; get; }
        public string NamaSKPD { set; get; }
        public DateTime Tanggal { set; get; }
            public int  PPKD{ set; get; }
            public long IIDRekening { set; get; }
            public int Debet { set; get; }
            public string NamaRekening { set; get; }
            public decimal Jumlah { set; get; }

        
    }
}
