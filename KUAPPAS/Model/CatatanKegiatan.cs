using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class CatatanKegiatan
    {
        public int Tahun {set;get;}
        public int IDDInas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public int NoCatatan { set; get; }
        public string CatatanMurni { set; get; }
        public string CatatanPerubahan { set; get; }
        public Single Jenis { set; get; }



    }
}
