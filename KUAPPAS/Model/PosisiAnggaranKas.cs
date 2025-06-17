using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class PosisiAnggaranKas
    {
        public string Kode { set; get; }
        public string Nama { set; get; }
        public decimal Anggaran { set; get; }
        public decimal AnggaranKasTerpakai { set; get; }


    }
    public class parameterPosisiAnggaranKas
    {
        public int Tahun { set; get; }
        public long Dinas { set; get; }
        
    }

}
