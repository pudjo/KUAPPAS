using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class ClsPosisiKas
    {
        public int Kelompok { set; get; }
        public string Label { set; get; }
        public string Nama { set; get; }
        public decimal Jumlah { set; get; }
        public string JudulKelompok { set; get; }
        public int Isi { set; get; }
        public int Root { set; get; }
    }
}
