using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Unit
    {
        public int ID { set; get; }     
        public int KodeKategori { set; get; }
        public int KodeUrusan { set; get; }
        public int SKPD { set; get; }
        public int KodeSKPD { set; get; }
        public int Kode { set; get; }
        public int UntAnggaran { set; get; }
        public string Nama {set;get;}

        public string KodeSIPD { set; get; }
      

        public string Tampilan { set; get; }
        public string TampilanUrusan { set; get; }
        public string NamaUrusan { set; get; }
        public string NamaBendahara { set; get; }
        public string NIPBendahara { set; get; }


        public string NoRekening { set; get; }
        public string NamaBank { set; get; }
        public string NPWP { set; get; }

        public string JabatanPimpinan { set; get; }
        public string NamaPimpinan { set; get; }
        public string NIPPimpinan { set; get; }

        public string BendaharaPenerimaan { set; get; }
        public string NIPBendaharaPenerimaan { set; get; }   
    }
}
