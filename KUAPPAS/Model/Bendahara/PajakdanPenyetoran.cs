using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Bendahara
{
    public class PajakdanPenyetoran
    {
        public int idDinas { set; get; }
        public int KodeUK { set; get; }
        public int iBank { set; get; }

        public long inourutPanjar { set; get; }
        public long inourutSetor { set; get; }
        public string NoBuktiBelanja { set; get; }
        public string NoBuktiSetor { set; get; }
        public DateTime TanggalBelanja {set;get;}
        public DateTime TanggalSetor { set; get; }
        public string KeteranganBelanja { get; set; }
        public string KeterangabSetor { set; get; }
        public int idRekeningBelanja { set; get; }
        public int idrekeningSetor { set; get; }
        public string NamaPungut{ set; get; }
        
        public decimal JumlahPungut { set; get; }
        public decimal JumlahSetor { set; get; }
        public string NTPN{ set; get; }
        public string KodeBIlling{ set; get; }


    }
}
