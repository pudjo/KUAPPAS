using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class ListSubKegiatanRPT
    {
        public long IDSubKegiatan { set; get; }
        public string Lokasi { set; get; }
        public string SumberPendanaan { set; get; }
        public string Mulai { set; get; }
        public string Akhir { set; get; }
        public string Nama { set; get; }
        public string Keluaran { set; get; }
        public string Jumlah { set; get; }
        public string JumlahMurni { set; get; }
        public string Selisih { set; get; }
        public string Persen { set; get; }
        public string KodeSub { set; get; }
        public string Keterangan { set; get; }

    }
}
