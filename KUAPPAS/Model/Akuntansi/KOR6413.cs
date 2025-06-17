using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Akuntansi
{
    public class KOR6413
    {
        public long IIDRekening13 { set; get; }
        public long IIDRekening64 { set; get; }
        public string NamaRekening13 { set; get; }
        public string NamaRekening64 { set; get; }

       

        public Single Default { set; get; }

    }
    public class KOR64LO
    {
        public long IIDRekening { set; get; }
        public long IIDRekeningLO { set; get; }
        public string NamaRekening { set; get; }
        public string NamaRekeningLO { set; get; }

    }
    public class KORUtang
    {
        public long IIDRekening { set; get; }
        public long IIDRekeningUtang { set; get; }
        public string NamaRekening { set; get; }
        public string NamaRekeningUtang { set; get; }

    }

}
