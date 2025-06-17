using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class KorelasiUtangBelanja
    {
        public long IDRekening { set; get; }
        public long IDRekeningUtang { set; get; }

        public string NamaRekening { set; get; }
        public string NamaRekeningUtang { set; get; }
    }
}
