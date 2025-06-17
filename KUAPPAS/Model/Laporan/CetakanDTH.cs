using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Laporan
{
    public class CetakanDTH
    {
        public string KodeSKPD { set; get; }
        public string NamaSKPD{ set; get; }
        public int JumlahSPM{ set; get; }

        public decimal Belanja { set; get; }
        public int JumlahSP2D { set; get; }
        public decimal BelanjaTotal { set; get; }
        public decimal TotalPotongan { set; get; }
        public string Keterangan { set; get; }
    }
}
