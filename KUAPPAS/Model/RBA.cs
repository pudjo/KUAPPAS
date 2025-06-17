using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class RBA
    {

        public int ID { set; get; }
        
        public int Tahun { set; get; }
        public int Unit { set; get; }
        public long IDRekening { set; get; }
        public int Tahap { set; get; }
        public string Uraian { set; get; }
        public decimal HargaSatuan { set; get; }
        public decimal Jumlah { set; get; }
        public int Satuan { set; get; }
        // Setiap kali 
        // public int ID { set; get; }

        //public int Tahun ,Unit ,Tahap ,Ureaian ,HargaSatuan ,Jumlah ,Satuan 

    }
}
