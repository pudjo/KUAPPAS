
using System;

namespace DTO.Akuntansi
{
    public class PerdaRealisasi_1_4

    {
        public string Kode { set; get; }
        public int IDUrusan { set; get; }
        public  int IDDinas { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }
        public long IDRekening{ set; get; }
        


        public string Nama { set; get; }
        public decimal Anggaran { set; get; }
        public decimal Realisasi { set; get; }

        public decimal AnggaranOperasi { set; get; }
        public decimal RealisasiOperasi { set; get; }

        public decimal AnggaranModal { set; get; }
        public decimal RealisasiModal { set; get; }

        public decimal AnggaranTakTerduga { set; get; }
        public decimal RealisasiTakTerduga { set; get; }

        public decimal AnggaranTransfer { set; get; }
        public decimal RealisasiTransfer { set; get; }


        public Single Depth { set; get; }

        
        public int BesarFont { set; get; }
        public int Bold { set; get; }
        public int Garis { set; get; }
        public int Jenis { set; get; }

        public PerdaRealisasi_1_4()
        {
            Kode = "";

            Nama = "";
            Anggaran = 0;
            Realisasi = 0;
            Depth = 0;


            BesarFont = 0;
            Bold = 0;
            Garis = 1;
            Jenis = 0;

    }
}
}
