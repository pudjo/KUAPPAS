using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Perda
    {
        public int Tahun { set; get; }
        public string Nomor { set; get; }
        public string Keterangan { set; get; }
        public DateTime Tanggal { set; get; }
        public Single Jenis { set; get; }
        public Single Tahap { set; get; }

      }
}
