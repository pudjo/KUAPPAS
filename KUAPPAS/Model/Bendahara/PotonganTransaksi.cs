using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class PotonganTransaksi
    {
        public long NoUrut { set; get; }
        public int IDRekeningPotongan { set; get; }
        public string NamaPotongan { set; get; }
        public decimal Jumlah { set; get; }
        public int No { set; get; }
        public Single  Informasi{ set; get; }
        public long NoUrutSetor { set; get; }
        public int Status { set; get; }


    }
}
