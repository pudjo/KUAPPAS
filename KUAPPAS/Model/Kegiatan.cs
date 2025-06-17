using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Kegiatan
    {
        public long ID { set; get; }
        public Single Tahun { set; get; }
        public Single KodeKategoriPelaksana { set; get; }
        public Single KodeUrusanPelaksana { set; get; }
        public Single KodeKategori { set; get; }
        public Single KodeUrusan { set; get; }
        public int SKPD { set; get; }
        public int Unit { set; get; }
        public int Program { set; get; }
        public int Kode { set; get; }
        public string Nama { set; get; }
        public decimal PlafonMurni { set; get; }
        public decimal PlafonPerubahan { set; get; }
        public decimal CurrentPlafon { set; get; }
        public decimal AnggaranMurni { set; get; }
        public decimal AnggaranPerubahan { set; get; }
        public string Tampilan { set; get; }
        

    }
}

