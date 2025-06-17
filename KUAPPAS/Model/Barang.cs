using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Barang
    {
        public long ID { get; set; }
        public string Nama { set; get; }
        public long Parent { set; get; }
        public Single Level { set; get; }
        public Single MasaManfaat { set; get; }
        public bool Baru { set; get; }

    }
}
