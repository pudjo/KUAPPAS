using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class TahapanAnggaran

    {
        public int Tahun { set; get; }
        public int IDDInas { set; get; }
        public string NamaDinas { set; get; }
        public string NamaTahap { set; get; }

        public int Tahap { set; get; }
        public int StatusAnggaranKas { set; get; }
        public int StatusInput { set; get; }


    }
}
