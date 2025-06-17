using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class AnggaranRealisasi
    {
        public int Tahun { set; get;}
        public int IDDinas { set; get; }
        public int KodeUK { set; get; }

        public int Jenis { set; get; }
        public int KodeKategori { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSUBKegiatan { set; get; }
        public long IDRekening { set; get; }
        public int KelompokRekening { set; get; }
        public int JenisRekening { set; get; }
        public int ObjectRekening { set; get; }
        public int RincianObjectRekening { set; get; }
        public string NamaDinas { set; get; }
        public string NamaUnit { set; get; }

        public string NamaUrusan { set; get; }
        public string NamaBidangUrusan { set; get; }
        public string NamaProgram { set; get; }
        public string NamaKegiatan { set; get; }
        public string NamaSubKegiatan { set; get; }
        public decimal Anggaran { set; get; }
        public decimal Realisasi { set; get; }
    }
}

