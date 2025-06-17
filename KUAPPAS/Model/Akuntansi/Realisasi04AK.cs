using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class Realisasi04AK
    {
        public int IDDInas { set; get; }
        public int Tahun { set; get; }
        public int Tabel { set; get; }
        public int JenisSP2D { set; get; }
        public int Level { set; get; }
        
        public int IdUrusan { set; get; }
        public int IdProgram { set; get; }
        public int IdKegiatan { set; get; }
        public long IdSubKegiatan { set; get; }
        public long idRekening { set; get; }
        public string KpdeRekening { set; get; }
        public int Debet { set; get; }
        public decimal Jumlah { set; get; }
        public int Debet1 { set; get; }
        public decimal Jumlah1 { set; get; }


    }
}
