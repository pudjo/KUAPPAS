using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class Pajak
    {
            public long NoUrut {set; get;}
            public int  Tahun {set; get;}
            public int IDDInas {set; get;}
            public DateTime m_dtPajak {set; get;}
            public Single Debet {set; get;}
            public string NoUrutSumber {set; get;}
            public Single JenisSumber {set; get;}

            public string  Keterangan {set; get;}
            public int JenisBelanja {set; get;}
            public decimal Jumlah {set; get;}
            public Single Status {set; get;}
            public string NoBukti { set; get; }
    }
    public class PembayaranPajak
    {
        public long NoUrutBelanja { set; get; }
        public long NoUrutSetorPajak { set; get; }
        public string NoBuktiBelanja { set; get; }
        public string KeteranganBelanja { set; get; }
        public DateTime TanggalBelanja { set; get; }
        public long IDPotongan { set; get; }
        public string NamaPotongan { set; get; }        
        public int JenisBelanja { set; get; }        
        public decimal Jumlah { set; get; }        
        public Single StatusPajak { set; get; }
        public string NoBukti { set; get; }
        public string NTPN { set; get; }
        public string KodeBilling{ set; get; }
        public int IDDInas { set; get; }
        public int KodeUK { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }


    }

}
