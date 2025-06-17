using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class RekapDashBoard
    {
        
        public int Level{ set; get; }
        public int IDDInas{ set; get; }
         public int IDUrusan{ set; get; }
         public int IDProgram{ set; get; }
         public int IDkegiatan{ set; get; }
         public long IDRekening{ set; get; }
        public string Nama{ set; get; }
        public string PENDAPATANINPUT{ set; get; }
        public string BTLINPUT { set; get; }
        public string BLINPUT{ set; get; }
        public string PENDAPATANPAGU{ set; get; }
        public string BTLPAGU { set; get; }
        public string BLPAGU{ set; get; }
        public string SelisihPendapatan { set; get; }
        public string SelisihBelanja { set; get; }
        public string Kode { set; get; }
        public Single Jenis { set; get; }
    }
}
