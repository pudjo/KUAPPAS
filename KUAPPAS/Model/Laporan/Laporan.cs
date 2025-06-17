using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Laporan
{
    public class Laporan
    {
        public string Kode { set; get; }
        public long IDRekening { set; get; }
        public int Level { set; get; }
        public string Nama { set; get; }
        public decimal AnggaranMurni { set; get; }
        public decimal AnggaranGeser { set; get; }
        public decimal AnggaranRKAP { set; get; }
        public decimal AnggaranABT { set; get; }
        public decimal Jumlah { set; get; }
        public int SaldoNormal { set; get; }


    }
}
