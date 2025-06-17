using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class ProgramKegiatan
    {
        public int IDDInas { set; get; }
        public string NamaDinas { set; get; }
        public string KodeDinas { set; get; }
        public int Tahun { set; get; }
        public int KodeUK { set; get; }
        public string NamaUK { set; get; }
        
        public string  StrIDUrusan { set; get; }
        public string StrIDProgram { set; get; }
        public string StrIDKegiatan { set; get; }
        public string StrIDSubKegiatan { set; get; }


        public int  IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long  IDSubKegiatan { set; get; }

        public long KeySubKegiatan { set; get; }
        public long KeyKegiatan { set; get; }
//        public long KeyIDSubKegiatan { set; get; }


        public string NamaUrusan { set; get; }
        public string NamaProgram { set; get; }
        public string NamaKegiatan { set; get; }
        public string NamaSubKegiatan { set; get; }

    }
}
