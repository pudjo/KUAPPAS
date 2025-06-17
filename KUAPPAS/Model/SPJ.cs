using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess9.DTO
{
    public class SPJ
    {
       
            public long NoUrut { set; get; }
            public int Tahun { set; get; }
            public int IDDinas { set; get; }

            public string NoSPJ { set; get; }
            public DateTime DtSPJ { set; get; }
            public int Jenis { set; get; }
            public int Bulan { set; get; }

            public int DtAwal { set; get; }
            public int DtAkhir { set; get; }
            public decimal Jumlah { set; get; }
            public int Status { set; get; }
            public string Keterangan { set; get; }
            public int NoBPP { set; get; }
            public int PPKD { set; get; }
            public int JenisBelanja { set; get; }
            public List<SPJRekening> Rekenings { set; get; }

            
        }

        public class SPJRekening
        {
            public long NoUrut { set; get; }
            public int IDUrusan { set; get; }
            public int IDProgram { set; get; }
            public int IDKegiatan { set; get; }
            public long IDSubKegiatan { set; get; }
            public long IIDRekening { set; get; }
           
            public decimal Jumlah { set; get; }
            


        }
    
}
