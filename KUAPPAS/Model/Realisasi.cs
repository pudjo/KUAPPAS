using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Realisasi
    {
        public int TABEL { set; get; }
        public int IDDInas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgran { set; get; }
        public long IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }
        
        public long inourut { set; get; }
        public int btJenis { set; get; }
        public int JenisSP2D { set; get; }
        public int iTahun { set; get; }
        public int btKodeKategori { set; get; }
        public int btKodeUrusan { set; get; }
        public int btKodeSKPD { set; get; }
        public DateTime dtDokument { set; get; }
        public DateTime dtBukukas { set; get; }
        public int btKodeUK { set; get; }
        public int btKodeKategoriPelaksana { set; get; }
        public int btKodeUrusanPelaksana { set; get; }
        public int btIDProgram { set; get; }
        public int btIDkegiatan { set; get; }
        public int btIDSubkegiatan { set; get; }
        public long IIDRekening { set; get; }
        public int Debet { set; get; }
        public Decimal cJumlah { set; get; }
        public int PPKD { set; get; }
        public int SumberDana { set; get; }
        public int Bank { set; get; }


    }
}
