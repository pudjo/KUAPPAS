using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Bendahara
{
    public class RincianObjek
    {


        public decimal Anggaran { set; get; }
        public int IDDInas { set; get; }
        public long IDSUbKegiatan { set; get; }
        public string NamaSubKegiatan { set; get; }
        public long IDRekening { set; get; }
        
        public string NamaRekening{ set; get; }
        


        public int JenisBeanja { set; get; }
        public int NoBKU { set; get; }
        public string NoBukti { set; get; }
        public string Uraian { set; get; }
        public DateTime Tanggal { set; get; }
        public decimal UP { set; get; }
        public decimal TU { set; get; }
        public decimal LS { set; get; }
        public int KodeUK { set; get; }


    }
}
