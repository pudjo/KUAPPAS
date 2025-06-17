using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class SelisihTrxBB
    {
        public long NoUrut{ set; get; }
        public string NoBukti { set; get; }
        public DateTime Tanggal { set; get; }
            public decimal Trx{ set; get; }
            public decimal BB { set; get; }

    }
}
