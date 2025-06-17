using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class SPDDetail
    {
        public int Jenis { set; get; }
        public string NoSPD { set; get; }
        public DateTime TanggalSPD { set; get; }
        public long NoUrut { set; get; }
        public int IDDInas { set; get; }
        public int  IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubkegiatan { set; get; }
        public int KodeUK { set; get; }
        

        public long IDRekening { set; get; }
        public decimal Jumlah { set; get; }
        public Single StatusUpdate { set; get; }
        public string NamaRekening {set;get;}


    }
}
