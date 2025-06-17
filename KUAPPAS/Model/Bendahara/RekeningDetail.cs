using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class RekeningDetail
    {
        public long IDRekening { set; get; }
        public string NamaRekening { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public long IDKegiatan { set; get; }
        public decimal Sisa { set; get; }
        public decimal Nilai { set; get; }

    }
}
