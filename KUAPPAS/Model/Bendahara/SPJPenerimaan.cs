using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Bendahara
{
    public class SPJPenerimaan
    {
        public int IdDinas { set; get; }
        public long IIDRekening { set; get; }
        public DateTime Tanggal { set; get; }
        public decimal Penerimaan { set; get; }
        public decimal Penyetoran { set; get; }
    }
}
