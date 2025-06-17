
using System;
using System.Security.Policy;

namespace DTO.Akuntansi
{
    public class PerdaRealisasi_1_3

    {
        public string Kode { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }
        public long IDRekening{ set; get; }
        public int  KelompokRekening{ set; get; }
        public int JenisRekening { set; get; }
        public int ObjekRekening { set; get; }
        public int RincianRekening { set; get; }
        public int SubRincianRekening { set; get; }
        public string DasarHukum { set; get; }
        public string KodeUrusan { set; get; }
        public string KodeKategori { set; get; }
        public string KodeProgram { set; get; }
        public string KodeKegiatan { set; get; }
        public string KodeSubKegiatan { set; get; }
        public string KodeRekening { set; get; }
        public string KodeDinas { set; get; }
        public string Nama { set; get; }
        public decimal Anggaran { set; get; }
        public decimal Realisasi { set; get; }
        public decimal Selisih { set; get; }
        public string Persen { set; get; }
        public Single Depth { set; get; }

        
        public int BesarFont { set; get; }
        public int Bold { set; get; }
        public int Garis { set; get; }
        public int Jenis { set; get; }

        public PerdaRealisasi_1_3()
        {
            Kode = "";
            KodeUrusan = "";
            KodeKategori = "";
            KodeProgram = "";
            KodeKegiatan = "";
            KodeSubKegiatan = "";
            KodeRekening = "";

            Nama = "";
            Anggaran = 0;
            Realisasi = 0;
            Selisih = 0;
            Persen = "";
            Depth = 0;


            BesarFont = 0;
            Bold = 0;
            Garis = 1;
            Jenis = 0;

    }
}
}
