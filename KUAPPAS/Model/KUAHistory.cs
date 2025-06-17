using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class KUAHistory
    {

        public long ID { set; get; }
        public string  IDKUA { set; get; }
        public Single Action{ set; get; }
        public string Value{ set; get; }
        public int UserID{ set; get; }
        public Single Tanggal{ set; get; }
        public Single Bulan { set; get; }
        public Single Tahun { set; get; }
        public decimal Jumlah{ set; get; }

        public string Komputer{ set; get; }
        public Single Jam{ set; get; }
        public Single Menit { set; get; }

    }
}
