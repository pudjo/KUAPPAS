using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Akuntansi
{
    public class LaporanAkt
    {
        public string Nama { set; get; }
        public string Nama2 { set; get; }
        public string Keterangan1 { set; get; }
        public string Keterangan2 { set; get; }
        public string JumlahAnggaran { set; get; }
        public string Jumlahlalu { set; get; }
        public string JumlahKini { set; get; }
        public string Selisih { set; get; }
        public string Persen { set; get; }

        public string Tempat { set; get; }
        public string Tanggal { set; get; }
        public string PenandaTangan1 { set; get; }
        public string Jabatan1 { set; get; }
        public string NIP1 { set; get; }
        public string PenandaTangan2 { set; get; }
        public string Jabatan2 { set; get; }
        public string NIP2 { set; get; }
        public List<LaporanDetail> Details{ set; get; }
        




    }
    public class LaporanDetail{
        public int Level {set;get;}
        public int Jenis { set; get; }
        public int ID { set; get; }
        
        public string Kode { set; get; }
        public string Anggaran { set; get; }
        public string Sebelum { set; get; }
        public string SaatIni { set; get; }
        public string Selisih { set; get; }
        public string Persen { set; get; }

    }
}
