using BP.Akuntansi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class JurnalData
    {
        public string  Nourut {set;get; }
        public  JENIS_TABLE Table{set;get; }
        public  int  iJenisSPP {set;get; } 
        public int bPotongan {set;get; }
        public int bppkd {set;get; }
        public bool bTHL {set;get; }
        public int fromSKPD { set; get; }

    }
}
