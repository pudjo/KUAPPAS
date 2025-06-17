using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class TProgram
    {
        public int ID { set; get; }
        public string  Nama { set; get; }
        public decimal Anggaran { set; get; }

        public decimal Realisasi{ set; get; }

    }
}
