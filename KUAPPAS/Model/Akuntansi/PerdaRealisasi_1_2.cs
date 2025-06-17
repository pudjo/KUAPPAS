
using System;

namespace DTO.Akuntansi
{
    public class PerdaRealisasi_1_2

    {
        public string Kode { set; get; }
        public decimal Selisih { set; get; }
        
        public string Nama { set; get; }
        public decimal Anggaran { set; get; }
        public decimal Realisasi { set; get; }
        public decimal RealisasiSebelum { set; get; }
        public Single Depth { set; get; }

        public string KodeRekening { set; get; }

        public int BesarFont { set; get; }
        public int Bold { set; get; }
        public int Garis { set; get; }
        public string Persen{ set; get; }
        public int Jenis { set; get; }
    }
}
