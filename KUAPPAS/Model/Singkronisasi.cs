using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Singkronisasi
    {
        public int Tahun { set; get; }
        public int IDProgram { set; get; }
        public int IDRekening { set; get; }
        public int IDSingkron { set; get; }
        public int IDUrusan { set; get; }
        public decimal Jumlah { set; get; }
        public string Nama { set; get; }
        public string NamaSingkron { set; get; }
        public string CJumlah {set;get;}
    }
}
