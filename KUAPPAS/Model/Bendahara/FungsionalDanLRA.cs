using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bendahara
{
    public class FungsionalDanLRA
    {
        public long IDSubKegiatanF { set; get; }
        public long IDRekeningF { set; get; }
        public long IDDubKegiatanLRA { set; get; }
        public long IDRekeningLRA { set; get; }
        public string NamaSubKegiatan { set; get; }
        public string NamaRekening { set; get; }
        public decimal LRA { set; get; }
        public decimal BKU { set; get; }
        public long NoUrut { set; get; }
        public string Tabel { set; get; }
        public string NoBukti { set; get; }
        public DateTime Tanggal { set; get; }
        public int NoBKU { set; get; }
        public int KdeUK { set; get; }
    }
}
