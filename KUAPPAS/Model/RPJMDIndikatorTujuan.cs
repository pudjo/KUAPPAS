using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class RPJMDIndikatorTujuan
    {
        public int ID { set; get; }
        public int IDTujuan { set; get; }
        public int  No { set; get; }
        public string Indikator { set; get; }
        public int Satuan { set; get; }
        public string SSatuan { set; get; }
        public string CapaianAwal { set; get; }
        public string KondisiAkhir { set; get; }

    }
}
