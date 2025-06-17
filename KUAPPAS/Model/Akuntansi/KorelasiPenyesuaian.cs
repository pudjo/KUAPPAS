using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class KorelasiPenyesuaian
    {
        public long IDRekeningAset { set; get; }
        public long IDRekeningLO { set; get; }

        public string NamaRekeningLO { set; get; }
        public string NamaRekeningAset { set; get; }
    }
}
