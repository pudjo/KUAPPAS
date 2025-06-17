using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Indikator
    {
        public int ID { set; get; }
        public int Tahun { set; get; }
        public int IDDInas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram{ set; get; }
        public int IDKegiatan { set; get; }
        public Single iJenis { set; get; }
        public Single iIndikator{ set; get; }
        public string sIndikator { set; get; }
        public string sIndikatorMurni { set; get; }        
        public string Target { set; get; }
        public string TargetMurni { set; get; } 
       
        public string TargetPerubahan { set; get; }
        public string NamaJenis { set; get; }


    }
}
