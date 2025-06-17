using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class Koreksi
    {
        public long NoUrut {set;get;}
        
        public int  IDDInas {set;get;}
        public int  Tahun {set;get;}
        
        public int  Kodekategori {set;get;}
        public int  KodeUrusan {set;get;}
        public int  KodeSKPD {set;get;}
        public int  KodeUK {set;get;}
        public int  KodeKategoripelaksana {set;get;}
        public int  Kodeurusanpelaksana {set;get;}
        public int  KodeProgram {set;get;}
        public int  KodeKegiatan {set;get;}

        public string NoBukti { set; get; }
       

        public decimal Jumlah {set;get;}
        public DateTime DtKoreksi {set;get;}
        public string Uraian {set;get;}
        public int  Status {set;get;}
        public int  JenisBelanja {set;get;}
        public long   NoUrutSumber {set;get;}
        public int  JenisSumber {set;get;}
        public long  NourutSPJUP {set;get;}
        public Single BedaKegiatan { set; get; }
        
        public List<KoreksiDetail> Detail { set; get; }

        public int idcrt { set; get; }
        public DateTime tcrt { set; get; }
        public int idupdate { set; get; }
        public DateTime tUpdate { set; get; }
        public int UnitAnggaran { set; get; }


    }
    public class KoreksiDetail
    {
        public long NoUrut { set; get; }
        public int IDurusan{ set; get; }
        public int IDProgram{ set; get; }
        public int IDKegiatan{ set; get; }
        public long  IDSubKegiatan { set; get; }

        public int KodeUK1 { set; get; }
        public long   IDRekening1{ set; get; }
        public decimal  Jumlah1{ set; get; }
        public int  Debet1 { set; get; }
        public string NamaRekening{ set; get; }
        public int KodeKategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
        public int KodeProgram { set; get; }
        public int KodeKegiatan { set; get; }
        public int KodeSubKegiatan { set; get; }

        

    }
}
