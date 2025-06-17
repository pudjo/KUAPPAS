using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class PerdaIV
    {
        public Single Level { set; get; }

        public int IDDInas { set; get; }

        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDkegiatan { set; get; }

        public long IDSubkegiatan { set; get; }
        public string Nama { set; get; }
        public string Kode { set; get; }
        public string BP { set; get; }
        public string BBJ { set; get; }
        public string BM { set; get; }
        public string Jumlah { set; get; }
        public string BPMurni { set; get; }
        public string BBJMurni { set; get; }
        public string BMMurni { set; get; }

        public string BO { set; get; }
        public string BTT { set; get; }
        public string BT { set; get; }
        public string BOMurni { set; get; }
        public string BTMurni { set; get; }
        public string BTTMurni { set; get; }

        
        public string JunmlahMurni { set; get; }
        public string Selisih { set; get; }
        public string persentase { set; get; }
        public string Keluaran { set; get; }


    }
    public class PerdaSPM
    {
        public Single Level { set; get; }


        public string IDUrusan { set; get; }
        public string KodeKegiatan { set; get; }
        
        public string Nama { set; get; }
        public string KodeSubKegiatan { set; get; }
        public string NamaSubKegiatan { set; get; }
        public string NamaKegiatan { set; get; }

        public string Jenis { set; get; }
        public decimal Anggaran { set; get; }
        public decimal Realisasi { set; get; }
        public decimal DAnggaran { set; get; }
        public decimal DRealisasi { set; get; }

        public string No{ set; get; }
        public string NoDetail { set; get; }
        

    }
}
