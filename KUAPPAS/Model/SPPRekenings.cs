using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class SPPRekeningsNggakkepakai
    {
        public long NoUrut{ set; get; }
        public int UnitKerja { set; get; }
        public decimal Jumlah { set; get; }
        public string NamaRekening { set; get; }
        public int KodeUrusanPelakasna { set; get; }
        public int KodeKategoriPelaksana { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public int KodeKategriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
        public int KodeProgram { set; get; }
        public int KodeKegiatan { set; get; }
        public long IDRekening { set; get; }


    }
}
