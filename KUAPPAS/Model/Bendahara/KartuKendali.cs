using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Bendahara
{
    public class KartuKendali
    {
             public int IDDInas{set;get;} 
            public int IDUrusan{set;get;}  
            public int IDProgram{set;get;}
            public int IDKegiatan { set; get; }
            public long IDSubKegiatan { set; get; }
            public long IDRekening { set; get; }
            public int KodeUK { set; get; }
            public decimal TotalSP2D { set; get; }
            public decimal TotalRealisasi { set; get; }

            public decimal SP2DUP{set;get;}
            public decimal SP2DGU{set;get;}
            public decimal SP2DTU{set;get;}
            public decimal SP2DLS{set;get;}
            public decimal RSP2DGU{set;get;}
            public decimal RSP2DTU{set;get;}
            public decimal RSP2DLS { set; get; }


    }
}
