using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUAPPAS.DataAccess9.DTO
{
    
    public class PerdaRealisasi
    {
        public int IdDinas { set; get; }
        public string KodeDinas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public long IDKegiatan { set; get; }
        public long IDSubkegiatan { set; get; }

        public string Kode1 { set; get; }
        public string Kode2 { set; get; }
        public string Nama { set; get; }
        public decimal Anggaren { set; get; }
        public decimal Realisasi { set; get; }
        public decimal Selisih { set; get; }
        public string Prosentase { set; get; }

    }
}
