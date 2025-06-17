using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class SPJ
    {
        public long NoUrut { set; get; }
        public int Tahun { set; get; }
        public int IDDinas { set; get; }

        public string NoSPJ { set; get; }
        public DateTime DtSPJ { set; get; }
        public int Kodekategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUk { set; get; }
        public int Jenis { set; get; }
        public int Bulan { set; get; }
        public int UnitAnggaran { set; get; }
        public DateTime DtAwal { set; get; }
        public DateTime DtAkhir { set; get; }
        public decimal Jumlah { set; get; }
        public int Status { set; get; }
        public string Keterangan { set; get; }
        public int NoBPP { set; get; }
        public int PPKD { set; get; }
        public int JenisBelanja { set; get; }
        public long NoUrutClient{ set; get; }
        
        public List<SPJRekening> Rekenings { set; get; }
        
        public int KodekategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
    }

   
}
