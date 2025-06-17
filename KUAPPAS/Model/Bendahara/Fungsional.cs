using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class Fungsional
    {
        public long NoUrut {set;get;}
        public int Tahun {set;get;}
        public int IDDinas { set; get; }
        public string NoSPJ {set;get;}
        public DateTime DtSPJ {set;get;}
        public int Kodekategori {set;get;}
        public int KodeUrusan {set;get;}
        public int KodeSKPD {set;get;}
        public int KodeUk {set;get;}
        public int Jenis {set;get;}
        public int Bulan { set; get; }
        
        public int DtAwal {set;get;}
        public int DtAkhir {set;get;}
        public decimal Jumlah{ set; get; }
        public int Status { set; get; }
        public string Keterangan { set; get; }
        public int NoBPP { set; get; }
        public int PPKD { set; get; }
        public int JenisBelanja { set; get; }
        public List<FungsionalRekening> Rekenings { set; get; }

        public int KodekategoriPelaksana {set;get;}
        public int KodeUrusanPelaksana { set; get; }
    }

    public class FungsionalRekening
    {
        public long NoUrut { set; get; }
        public int IDDInas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram  { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }
        public long IDRekening { set; get; }
        public string Uraian { set; get; }
        public decimal  Anggaran  { set; get; }
        public int KodekategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
        public int KodeUK { set; get; }
        public int KodeProgram { set; get; }
        public int KodeKegiatan { set; get; }


        public decimal GL{ set; get; }
        public decimal GK { set; get; }
        public decimal GTot { set; get; }
                    
        public decimal BL { set; get; }
        public decimal BK { set; get; }
        public decimal BTot { set; get; }
                    
        public decimal UL { set; get; }
        public decimal UK { set; get; }
        public decimal UTot { set; get; }
        

    }
}
