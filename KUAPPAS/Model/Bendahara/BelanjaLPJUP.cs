using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Bendahara
{
    public class BelanjaLPJUP
    {
        public int Sumber { set; get; }
        public long NoUrut { set; get; }
        public string Kode { set; get; }
        public DateTime Tanggal { set; get; }
        public string NanaProgram { set; get; }
        public string NamaKegiatan { set; get; }
        public string NamaSubKegiatan { set; get; }
        public string KeteranganBelanja { set; get; }
        public decimal Nilai { set; get; }
        public long IDRekening { set; get; }
        public long IDSubKegiatan { set; get; }
        public int IDProgram { set; get; }
        public int  IDkegiatan { set; get; }
        public int KodeUK { set; get; }
        public decimal Jumlah { get; set; }
        public string NamaRekening { set; get; }
        
    }
}
