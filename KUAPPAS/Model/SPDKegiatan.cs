using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class SPDKegiatan
    {
        

        public long NoUrut { set; get; }
        public int KodeUK { set; get; }
        public int IDUrusan { set; get; }
        public string NamaRekening { set; get; }

        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long  IDSubKegiatan { set; get; }

        public long IDRekening { set; get; }
        public decimal Jumlah { set; get; }
        public Single PPKD { set; get; }

    }
}
