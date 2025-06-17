using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class rptPerda_IV

    {
        public Single Level { set; get; }
        public string Kode { set; get; }
        public Single Jenis { set; get; }
        public int IDUrusan { set; get; }
        public long IDRekening { set; get; }
        public int IDPRogram { set; get; }
        public int IDKegiatan {set;get;}
        public string Nama { set; get; }
        public string JumlahOlah { set; get; }
        public string Jumlah { set; get; }
        public string JumlahMurni { set; get; }
        
        public string Selisih { set; get; }
        public string Keterangan{ set; get; }


    }
}
