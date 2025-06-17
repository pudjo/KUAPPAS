using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Pemda
    {
        public int ID { set; get; }
        public int ProvinsiID { set; get; }
        public int KotaID { set; get; }
        public string Nama { set; get; }
        public string Ibukota { set; get; }
        public Single Jenis { set; get; }
        public string NamaJenis { set; get; }
        public string Alamat { set; get; }
        public string NamaPanjang { set; get; }
        public string KodeProvinsi { set; get; }

        public string NamaProvinsi { set; get; }
        public string KodeKota { set; get; }
        public string NamaKota { set; get; }
        public string KodeLokasi { set; get; }
        public string NamaKaDaerah { set; get; }
        public string NIPKaDaerah { set; get; }
        public string JabatanKaDaerah { set; get; }
        public string NamaSekda { set; get; }
        public string NIPSekda { set; get; }
        public string JabatanSekda { set; get; }
        public string NamaAsSekda { set; get; }
        public string JabatanAsSekda { set; get; }
        public string NIPAsSekda { set; get; }
        public string NamaKaKeu { set; get; }
        public string NIPKaKeu { set; get; }
        public string JabatanKaKeu { set; get; }
        public string MaxUP { set; get; }
        public string Kewenangan { set; get; }


        public Pemda()
        {
            ID =1;
            ProvinsiID =1;
            KotaID =1;
            Nama ="";
            Ibukota ="";
            Jenis =0;
            NamaJenis ="";
            Alamat ="";
            NamaPanjang="";
            KodeProvinsi ="";

            NamaProvinsi ="";
            KodeKota ="";
            NamaKota ="";
            KodeLokasi ="";
            NamaKaDaerah ="";
            NIPKaDaerah ="";
            JabatanKaDaerah ="";
            NamaSekda ="";
            NIPSekda ="";
            JabatanSekda ="";
            NamaAsSekda ="";
            JabatanAsSekda ="";
            NIPAsSekda ="";
            NamaKaKeu ="";
            NIPKaKeu ="";
            JabatanKaKeu ="";
            MaxUP ="";
            Kewenangan="";
        }
     }
}
//http://redux.js.org/docs/introduction/ThreePrinciples.html
