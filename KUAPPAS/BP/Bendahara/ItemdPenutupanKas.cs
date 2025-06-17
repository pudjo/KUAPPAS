using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Bendahara
{
    public class ItemsPenutupanKas
    {

        public string Item { set; get; }
        public string row { set; get; }
        public string Keterangan { set; get; }
        public string Jumlah { set; get; }        
        public int bold { set; get; }
        public string Jumlah2 { set; get; }

        public ItemsPenutupanKas(
            string Item,
            string row,
            string Keterangan,
            string Jumlah,
         
            int bold,
               string Jumlah2="")
        {
            this.Item = Item;
            this.row = row;
            this.Jumlah = Jumlah;
              this.Jumlah2 = Jumlah2;
            this.Keterangan = Keterangan;
            this.bold = bold;

        }

    }
}
