using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class RekapAnggaran
    {
        public int Tahun { set; get; }
        public long IDUrusan { set; get; }
        public int IDDInas { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan{ set; get; }
        public int KodeUnit { set; get; }

        public long IDRekening { set; get; }
        public int IDPaket { set; get; }
        public string Nama { set; get; }

        public decimal Jumlah { set; get; }
        public decimal JumlahOlah { set; get; }
        public decimal JumlahMurni { set; get; }
        public Single Level { set; get; }
        public Single Jenis { set; get; }
        public string Kode { set; get; }
        public Single Tahap { set; get; }

        
    }
}
