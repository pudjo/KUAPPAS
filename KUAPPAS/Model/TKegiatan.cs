using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    // Data ini terkain KUA.. 
    // di extract dari tKUA

    public class TKegiatan
    {
        public int IDUrusan { set; get; }
        public Single Tahun { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public int KodeKegiatan { set; get; }
        public int IDDinas { set; get; }
        public int KodeKategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
        public int KodeKategori { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }
        public int KodeProgram { set; get; }
        public string Nama { set; get; }
        public decimal Pagu { set; get; }

    }
}
