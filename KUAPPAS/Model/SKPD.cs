using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    
    public class SKPD
    {
        public int ID { set; get; }
        public int Tahun { set; get; }
        public string KodeSIPD { set; get; }
        public int IDPemda { set; get; }
        public int KodeKategori { set; get; }
        public int KodeUrusan { set; get; }
        public int IDUrusan { set; get; }
        public int Parent { set; get; }
        public Single Root { set; get; }
        public Single Level { set; get; }
        public int KodeUnit { set; get; }

        public string KodeParent { set; get; }
        public string NamaParent { set; get; }
        public string TampilanKode { set; get; }


        public int Kode { set; get; }
        public string Nama { set; get; }
        public string Tampilan { set; get; }
        public string TampilanUrusan { set; get; }
        public string NamaUrusan { set; get; }
        public string NamaBendahara { set; get; }
        public string NIPBendahara { set; get; }


        public string NamaPPK { set; get; }
        public string NIPPPK { set; get; }

        public string NoRekening { set; get; }
        public string NamaBank { set; get; }
        public string NPWP { set; get; }

        public string JabatanPimpinan { set; get; }
        public string NamaPimpinan { set; get; }
        public string NIPPimpinan { set; get; }

        public string BendaharaPenerimaan { set; get; }
        public string NIPBendaharaPenerimaan { set; get; }     

        public int IDURusanBaru { set; get; }
        public int UrusanBAru { set; get; }
        public int IDKantor { set; get; }
        
    }
}
