using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class RegisterSPP
    {
        public int No { set; get; }
        public string NoSPP { set; get; }
        public string NoSPM { set; get; }
        public string NoSP2D { set; get; }
        public string Keterangan { set; get; }


        public DateTime TanggalSPP { set; get; }
        public DateTime TanggalSPM { set; get; }
        public DateTime TanggalSP2D { set; get; }

        public string Uraian { set; get; }
        public decimal Jumlah { set; get; }


        public int Jenis { set; get; }

        
    }
}
