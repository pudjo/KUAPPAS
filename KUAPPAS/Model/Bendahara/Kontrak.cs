using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class Kontrak

    {
        public int IDDInas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int  IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }
        public int IDLokasi { set; get; }
        public long  NoUrut {set;get;}
        public int Tahun {set;get;}
        public int Kodekategori {set;get;}
        public int KodeUrusan {set;get;}
        public int KodeSKPD {set;get;}
        public int KodeUk {set;get;}
        public int KodekategoriPelaksana {set;get;}
        public int KodeUrusanPelaksana {set;get;}
        public int KodeProgram {set;get;}
        public int KodeKegiatan {set;get;}
        public int KodeSubKegiatan { set; get; }
        public string KeteranganNamaBank { set; get; }
        public DateTime  DtKontrak {set;get;}
        public Single Status {set;get;}
        public string NoKontrak {set;get;}
        public int PihakKetiga {set;get;}
        public Perusahaan oPerusahaan {set;get;}
        public Single PPKD {set;get;}
        public string Uraian {set;get;}
        public string WaktuPelaksanaan {set;get;}
        public DateTime dAwal {set;get;}
        
        public DateTime dAkhir { set; get; }
        public string NamaPerusahaan { set; get; }
        public List<KontrakRekening> Rekening { set; get; }
        public decimal Jumlah { set; get; }
        public int idcrt { set; get; }
        public DateTime tcrt { set; get; }
        public int idupdate { set; get; }
        public DateTime tUpdate { set; get; }

    }
    public class KontrakRekening
    {
        public string NoUrut { set; get; }
        public long  IDRekening { set; get; }
        public decimal  Jumlah { set; get; }
        public string  NamaRekening { set; get; }
        

    }
}
