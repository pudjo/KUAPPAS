using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Bendahara
{
    public class SaldoAwal
    {
        public int IDDInas { set; get; }
        public int Tahun { set; get; }
        public int Jenis { set; get; }
        public long IDRekening { set; get; }
        public decimal Jumlah { set; get; }
        public int Bank { set; get; }
    }
}
