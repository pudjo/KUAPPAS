using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Anggaran
{
    public class ProgramKegiatanAnggaran : ProgramKegiatan  
    {
       
        public int KodeUK { set; get; }
        public int Jenis { set; get; }

        public long IIDRekening { set; get; }
        public string NamaRekening { set; get; }
        public decimal AnggaranMurni { set; get; }
        public decimal AnggaranGeser { set; get; }
        public decimal AnggaranRKAP { set; get; }
        public decimal AnggaranABT { set; get; }


    }
}
