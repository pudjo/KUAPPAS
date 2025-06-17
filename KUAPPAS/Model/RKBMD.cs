using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class RKBMD
    {
        public int IDDInasKeuangan { set; get; }
        public int ID { set; get; }

        public int Tahun {set;get;}
        public int SKPD { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }
        public int Status { set; get; }
        public int IDRKBMDBArang{ set; get; }
        public long IDBarang { set; get; }
        public string NamaBarang { set; get; }
        public int JumlahUsulan { set; get; }
        public int JumlahUsulanPerubahan { set; get; }
        public string Satuan { set; get; }
        public long IDrekening { set; get; }
        public string NamaRekening { set; get; }


    }
}
