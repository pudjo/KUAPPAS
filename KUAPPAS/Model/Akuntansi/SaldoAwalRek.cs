using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class SaldoAwalRek
    {
        public int IDDinas { set; get; }
        public long IDRekening { set; get; }
        public decimal Jumlah { set; get; }
        public int Debet { set; get; }
        public string Nama { set; get; }
        public DateTime Tanggal { set; get; }

    }
}
