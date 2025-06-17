using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class DasarHukum
    {
        public int Tahun{set;get;}
        public long IDDInas {set;get;}
        public int KodeKategori {set;get;}
        public int KodeUrusan{set;get;}
        public int KodeSKPD{set;get;}
        public int KodeUK { set; get; }
        public long IDRekening{set;get;}
        public string Keterangan {set;get;}
        public string NamaRekening { set; get; }
        public Single OnPerda { set; get; }

        public Single NoUrut { set; get; }




    }
}
