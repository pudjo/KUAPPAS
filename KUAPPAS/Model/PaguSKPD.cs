using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class PaguSKPD
    {
        public int IDDInas { set; get; }
        public int Tahun { set; get; }
        public decimal PaguMurni { set; get; }
        public decimal PaguPerubahan { set; get; }
        public int  Jenis{ set; get; }

    }
}
