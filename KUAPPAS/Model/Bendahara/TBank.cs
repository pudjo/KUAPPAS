using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class TBank
    {
        public int ID { set; get; }
        public int IDDinas { set; get; }
        
        public string Nama { set; get; }
        public string NamaSKPD { set; get; }
        public string NamaJenisBendahara { set; get; }
        
        public int KodeKategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }

        public string NoRekening { set; get; }
        public long IDRekening { set; get; }
        public int JenisBendahara { set; get; }

        public int idcrt { set; get; }
        public DateTime tcrt { set; get; }
        public int idupdate { set; get; }
        public DateTime tUpdate { set; get; }


    }
}
