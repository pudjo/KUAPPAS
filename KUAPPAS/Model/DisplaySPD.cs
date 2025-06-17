using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class DisplaySPD
    {

        public long NoUrut { set; get; }
        public int Tahun { set; get; }
        public int Jenis { set; get; }
        public int IDDInas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubkegiatan { set; get; }
        public int KodeUK { set; get; }

        public long IDRekening { set; get; }
        public decimal JumlahLalu { set; get; }
        public decimal Jumlah { set; get; }




    }
}
