using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Kecamatan
    {
        public int ID { set; get; }
        public int Kota { set; get; }
        public string Nama { set; get; }
        public int Kode { set; get; }
        public List<Desa> ListDesa { set; get; }
        public List<Dusun> ListDusun { set; get; }

        public string Tampilan { set; get; }
        public string TampilanLengkap { set; get; }

    }
}
