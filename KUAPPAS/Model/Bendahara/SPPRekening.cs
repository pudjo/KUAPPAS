using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class SPPRekening
    {
        
        public long NoUrut { set; get; }
        public int IDUrusan { set; get; }
        public int IDDinas { set; get; }
        public int IDProgram { set; get; }  
        public long IDSubKegiatan { set; get; }
        public long IDRekening { set; get; }
        public int UnitKerja { set; get; }
        
        public decimal Jumlah { set; get; }
        public int KodekategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
        public int KodeProgram{ set; get; }

        public int KodeKegiatan{ set; get; }
        public int KodeSubKegiatan { set; get; }
        
        public int IDKegiatan { set; get; }
        public string NamaRekening { set; get; }
        public string NamaKegiatan { set; get; }
        public string NamaProgram { set; get; }
        public string NamaSubKegiatan { set; get; }
        public string NamaUrusan { set; get; }
        public string NamaUnit { set; get; }
        
        
        
        public string KodeRekening { set; get; }
        public string JumlahInString { set; get; }
        
    }
}
