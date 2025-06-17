using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUAPPAS.DTO.Bendahara
{
    public class TransaksiBank
    {
        public long NoUrut{set;get;}
        public string NoBukti{set;get;}
        public int Tahun{set;get;}
        public string Uraian{set;get;}
        public int  IDDinas{set;get;}
        public int  UnitKerja{set;get;}
        public decimal  Jumlah{set;get;}
        public int  Jenis{set;get;}
        public DateTime Tanggal { set; get; }

    }
}
