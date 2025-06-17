using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class PelaksanaUrusan
    {
        public int Tahun { set; get; }
        public int Dinas { set; get; }
        public int Urusan { set; get; }
        public string NamaDinas { set; get; }
        public string KodeDinas { set; get; }

        public int KodeKategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }
        public int KodeKategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }

    }
}
