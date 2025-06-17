using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class RekapProgramKegiatanUmum
    {
         public Single Visible{set;get;}
         public Single Header { set; get; }
        public int KodeProgram {set;get;}
        public int KodeKegiatan {set;get;}
        public string KodeDinas {set;get;}
        public string KodeRekening{set;get;}
        public string Nama {set;get;}
        public string JumlahInput {set;get;}
        public string JumlahPagu{set;get;}
        public decimal Selisih{set;get;}

    }
}
