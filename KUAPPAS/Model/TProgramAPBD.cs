using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class TProgramAPBD
    {
        public int IDUrusan { set; get; }
        public Single Tahun { set; get; }
        public int IDProgram { set; get; }
        public int KodeProgram { set; get; }
        public int IDDinas { set; get; }
        public int KodeKategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
        public int KodeKategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }
        public string Nama { set; get; }
        public string Nama2 { set; get; }
        public decimal Pagu { set; get; }
        public string  Plafon { set; get; }
        public int Jenis { set; get; }
        public decimal Realisasi { set; get; }
        public int IDUnit { set; get; }
        
        public string Keluaran { set; get; }
        public string Outcome { set; get; }
        public int PrioritasNasional { set; get; }
        public decimal Target { set; get; }
        public string SatuanTarget { set; get; }
        public decimal RPJMD { set; get; }
        


        public string TampilanKode { set; get; }
        public string JumlahDiInput { set; get; }
        public int IDurusanBaru { set; get; }
        public string NamaUrusanBaru { set; get; }
        public int KodeUrusanM { set; get; }
        public int KodeProgramM { set; get; }
        public string KodePendek{ set; get; }
 


        

    }

}
