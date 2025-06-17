using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class SPD
    {
        public long NoUrut { set; get; }
        public int Tahun { set; get; }
        public Single Jenis { set; get; }
        public int IDDInas { set; get; }
        public int Bulan { set; get; }
        public int Bulan2 { set; get; }
        public DateTime Tanggal { set; get; }
        public Single PPKD { set; get; }
        public string NoSPD { set; get; }
        public string NamaBendahara { set; get; }
        public string NamaPPTK { set; get; }
        public string KetentuanLain { set; get; }
        public Single Triwulan { set; get; }
        public List<SPDDetail> ListDetail { set; get; }
        public Single Status { set; get; }
        public string Keterangan { set; get; }
        public int IDBendahara { set; get; }
        public int INoSPD { set; get; }
        public Single JenisRekening { set; get; }
        public int JenisAnggaran { set; get; }
        public string Prefix { set; get; }


        public decimal Jumlah { set; get; }

    }
}
