using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUAPPAS.DataAccess9.DTO.Akuntansi
{
    public class PerdaFungsi
    {

            public Single Level { set; get; }

         public int KodeFungsi { set; get; }

        public int KodeSubFungsi { set; get; }
        public int KodeKategori { set; get; }
        public int KodeUrusan { set; get; }
        public string Nama { set; get; }
            public string Kode { set; get; }
            public decimal AnggaranOperasi { set; get; }
            public decimal RealisasiOperasi { set; get; }

            public decimal AnggaranModal { set; get; }
            public decimal RealisasiModal { set; get; }

            public decimal AnggaranTakTerduga { set; get; }
            public decimal RealisasiTakTerduga { set; get; }
            public decimal AnggaranTransfer { set; get; }
            public decimal RealisasiTransfer { set; get; }



        
    }
}
