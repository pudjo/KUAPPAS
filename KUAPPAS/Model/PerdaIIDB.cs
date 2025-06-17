using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class PerdaIIDB
    {
            public int iTAhun {set;get;}
            public int IDDInas {set;get;}
                public int IDurusan {set;get;} 
                    public int IDProgram {set;get;}
                        public long  IDKegiatan {set;get;}
                            public long IIDRekening {set;get;}
                        public int btJenis {set;get;}
                        public decimal  PendapatanOlah {set;get;}
                            public decimal  Pendapatan {set;get;}                  
                        public decimal  BTLOlah {set;get;}
                        public decimal  BTL {set;get;}
                        public decimal   BLOlah {set;get;}
                        public decimal   BL {set;get;}             
                        public decimal   TerimaOlah {set;get;}
                        public decimal   Terima {set;get;}             
                        public decimal   BayarOlah {set;get;}
                        public decimal Bayar { set; get; }
             
                        
    }
}
