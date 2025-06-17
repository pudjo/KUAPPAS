using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class UrusanDinas
    {
        public int Tahun { set; get; }
        public int IDDinas { set; get; }
        public int IDUrusan { set; get; }
        public int KodeKategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }        
        public int KodeKategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }

        public int KodeUK { set; get; }
        public string NamaUrusan { set; get; }
        public string NamaDinas { set; get; }
        public Single  UrusanPokok  { set; get; }
    }
}
