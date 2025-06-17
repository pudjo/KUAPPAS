using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUAPPAS.DTO.Akuntansi
{
    public class RealisasiRinci
    {
        public int IdDinas { set; get; }

        public string  NamaDinas { set; get; }
        public string NamaUnitDinas { set; get; }

        public string NamaUrusan { set; get; }
        public string NamaUrusanBidang { set; get; }
        public string NamaProgram { set; get; }

        public string NamaKegiatan { set; get; }
        public string NamaSubKegiatan { set; get; }

        public string NamaAkun { set; get; }
        public string NamaKelompok { set; get; }
        public string NamaJenis { set; get; }
        public string NamaObject { set; get; }
        public string NamaRincianObject{ set; get; }
        public string NamaSubRincianObject { set; get; }

        public decimal Anggaran { set; get; }
        public decimal Realisasi{ set; get; }

    }
}
