using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class SKRSKPD
    {
        public long NoUrut  {set;get;}
        public int Tahun {set;get;}
        public int IDDInas { set; get; }
        public E_JENIS_PIUTANG Jenis { set; get; }
        public int KodeKategori {set;get;}
        public int KodeUrusan {set;get;}

        public int KodeSKPD {set;get;}
        public int KodeUK {set;get;}
        public string NoBukti {set;get;}
        public int Masa {set;get;}
        public string Keterangan {set;get;}
        public DateTime TanggalSKRSKPD {set;get;}
        public int PPKD {set;get;}
        public string Nama {set;get;}
        public string Alamat {set;get;}
        public string NoNPWD {set;get;}
        public int Status {set;get;}
        public decimal Jumlah {set;get;}
        public List<SKRSKPDRekening> Rekenings { set; get; }

    }
    public enum E_JENIS_PIUTANG{
     
        CON_JENIS_SKR = 1,
        CON_JENIS_SKPD = 2

    }
    public class SKRSKPDRekening
    {
        public long NoUrut { set; get; }
        public long IDRekening { set; get; }
        public string Nama { set; get; }
        public decimal Jumlah { set; get; }
        public long IDRekening64 { set; get; }
        
    }
}
