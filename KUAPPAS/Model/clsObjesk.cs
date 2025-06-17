using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class clsObject
    {

        public long NoUrut { set; get; }
        public string NoBukti { set; get; }
        public DateTime Tanggal { set; get; }
        public decimal Jumlah { set; get; }
        public long IDsubkegiatan { set; get; }
        public long IDrekening{ set; get; }
        public decimal SP2DGU { set; get; }
        public decimal SP2DLS { set; get; }
        public decimal SP2DTU { set; get; }
        public decimal RSP2DGU { set; get; }
        public decimal RSP2DLS { set; get; }
        public decimal RSP2DTU { set; get; }
        
    }
}
