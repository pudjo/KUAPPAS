using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    [Serializable()]
    public class PMK04_01
    {
        public int Tahun { set; get; }
        public string JenisLaporan { set; get; }
        public string TypeDaerah { set; get; }
        public string NamaProvinsi { set; get; }
        public string NamaDaerah { set; get; }
        public string KodeFungsi { set; get; }
        public string NamaFungsi { set; get; }
        public string TypeUrusan { set; get; }
        public string KodeUrusan { set; get; }
        public string NamaUrusan { set; get; }
        public string SKPD { set; get; }
        public string Program { set; get; }
        public string Kegiatan { set; get; }
        public string KodeAkun { set; get; }
        public string NamaAkun { set; get; }
        public string KodeKelompok { set; get; }
        public string NamaKelompok { set; get; }
        public string KodeJenis { set; get; }
        public string NamaJenis { set; get; }
        public string KodeObyek { set; get; }
        public string NamaObyek { set; get; }
        public string KodeRincian { set; get; }
        public string RincianObjek { set; get; }
        public string Jumlah { set; get; }

    }
}
