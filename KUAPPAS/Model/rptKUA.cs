using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class rptKUA
    {

        public int ID { set; get; }
        public int IDDInas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }
        
        public int IDLokasi { set; get; }
        public string Uraian { set; get; }
        public string NamaKecamatan { set; get; }
        public string NamaDesa { set; get; }
        public string NamaDusun { set; get; }
        public decimal RKPD { set; get; }
        public decimal Pagu { set; get; }

        public string  Kode { set; get; }
        public Single Level { set; get; }

    }
}
