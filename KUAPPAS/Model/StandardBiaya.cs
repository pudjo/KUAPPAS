using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DTO
{
    public class StandardBiaya
    {
        public int Tahun { set; get; }
        public string IDBiaya { set; get;}
        public int ID { set; get; }

        public string Nama { set; get; }
        
        public string Parent { set; get; }
        public int Kelompok { set; get; }
        
        public decimal Harga { set; get; }
        public string  Uraian{ set; get; }  
        public int Satuan { set; get; }
        public Single   Level { set; get; }
        public string NamaSatuan { set; get; }
        public int  RootReport { set; get; }
        public string DisplayedText { set; get; }
        public int PPN { set; get; }
        public string Jenis { set; get; }

    }
}
