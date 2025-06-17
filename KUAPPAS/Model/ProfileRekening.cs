using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class ProfileRekening
    {
        public int ID { set; get; }
        public string Nama { set; get; }
        public string Keterangan { set; get; }
        public int NumSegment { set; get; }
        public int Kode1 { set; get; }
        public int Kode2 { set; get; }
        public int Kode3 { set; get; }
        public int Kode4 { set; get; }
        public int Kode5 { set; get; }
        public int Kode6 { set; get; }

        public int LEN1 { set; get; }
        public int LEN2 { set; get; }
        public int LEN3 { set; get; }
        public int LEN4 { set; get; }
        public int LEN5 { set; get; }
        public int LEN6 { set; get; }
        public int LEN { set; get; }

        public string FORMAT1 { set; get; }
        public string FORMAT2 { set; get; }
        public string FORMAT3 { set; get; }
        public string FORMAT4 { set; get; }
        public string FORMAT5 { set; get; }
        public string FORMAT6 { set; get; }

        public ProfileRekening()
        {
            ID =1;
            Nama ="Pemrmen 13";
        Keterangan ="";
        NumSegment =5;
        Kode1 =1;
        Kode2 =1;
        Kode3 =1;
        Kode4 =2;
        Kode5 =2;
     //   Kode6 =2;

LEN1 =1;
LEN2 =2;
LEN3 =3;
LEN4 =5;
LEN5 =7;
//LEN6 =9;

        FORMAT1 ="0";
        FORMAT2 ="0";
        FORMAT3 ="0";
        FORMAT4 ="00";
        FORMAT5 ="00";
  //      FORMAT6 = "00";

        }
    }
}
