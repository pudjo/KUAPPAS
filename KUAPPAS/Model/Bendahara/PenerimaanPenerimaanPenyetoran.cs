using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Bendahara
{
    public class PenerimaanPenerimaanPenyetoran
    {
        public long NoUrut { set; get; }
        public long NoUrutSetor { set; get; }
        public int IDDInas {set;get;}
        public DateTime Tanggal {set;get;}
        public string NoBukti{set;get;}
        public int Jenis {set;get;}
        public long IDRekening {set;get;}
        public decimal Jumlah { set; get; }
        public string Keterangan { set; get; }
        public int Debet { set; get; }
        public string NamaRekening { set; get; }

        // Overload + operator to add two Box objects.
        public  bool   ApakahSama (PenerimaanPenerimaanPenyetoran b)
        {
            bool ret = false;
         
            if (b.NoUrut == this.NoUrut && b.NoUrutSetor == this.NoUrutSetor && b.Jenis == this.Jenis) 
               ret= true;

            return ret;
        }
    }
}
