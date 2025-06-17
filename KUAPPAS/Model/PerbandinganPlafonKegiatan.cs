using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class PerbandinganPlafonKegiatan
    {
        public int Tahun { set; get; }
        public int IDDInas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }

        public int IDKegiatan2 { set; get; }
        public long IDRekening { set; get; }
        

        public string Nama { set; get; }
        public string Nama2 { set; get; }
        public int Jenis { set; get; }
        public decimal Plafon { set; get; }
        public decimal DPA { set; get; }        
        public int PPKD { set; get; }
        public string Kode { set; get; }

    }
}
