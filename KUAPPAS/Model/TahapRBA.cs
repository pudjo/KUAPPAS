using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class TahapRBA
    {
        public int ID{ set; get; }
        public int Unit { set; get; }
        public int Tahun { set; get; }
        public int Tahap { set; get; }
        public string Nama { set; get; }
        public decimal AmbangBatas { set; get; }
        public int Status { set; get; }

    }
}
