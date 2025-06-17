using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class EMBelanja
    {
        public int No { set; get; }
        public string Kegiatan { set; get; }
        public string SubKegiatan { set; get; }
        public decimal Anggaran { set; get; }
        public decimal Tahap1 { set; get; }
        public decimal Tahap2 { set; get; }
        public decimal Tahap3 { set; get; }
        public decimal JumlahBelanja { set; get; }
        public string Keluaran { set; get; }
        public string SatuanTarget { set; get; }
        public int Volume { set; get; }
    }
}
