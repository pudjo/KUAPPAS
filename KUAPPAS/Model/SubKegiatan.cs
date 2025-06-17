using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class SubKegiatan
    {
        public int ID { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public int IDDInas { set; get; }

        public int KategoriPelaksana { set; get; }
        public int UrusanPelaksana { set; get; }
        public int Kegiatan { set; get; }
        public int Program { set; get; }
        public int Kode { set; get; }
        public string Nama { set; get; }
        public bool Default { set; get; }
        public string Tampilan { set; get; }

        
    }
}
