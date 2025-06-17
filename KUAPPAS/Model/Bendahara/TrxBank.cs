using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class TrxBank
    {
        public long ID { set; get; }
        public int Tahun { set; get; }
        public int Kodekategori { set; get; }
        public int IDDinas  { set; get; }


        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }

        public int Bank { set; get; }
        public int jenis { set; get; }
        public decimal Jumlah { set; get; }
        public int BankTujuan { set; get; }
        public string cCreate { set; get; }
        public string dCreate { set; get; }
        public string cUpdate { set; get; }
        
        public DateTime dUpdate { set; get; }
        public string Uraian  { set; get; }
        public DateTime DTrx { set; get; }
        public string NoBukti { set; get; }
        public int  JenisBelanja { set; get; }
        public Single JenisBendahara { set; get; }
        public Single PPKD { set; get; }
    }
}
