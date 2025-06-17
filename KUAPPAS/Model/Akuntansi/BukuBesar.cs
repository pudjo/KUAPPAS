using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class BukuBesar
    {
        public int IDUrusan { get;set;}
        public int IDProgram{ get;set;}
        public Single Tahun{ get;set;}
        public DateTime TanggalTransaksi { get;set;}

        public int IDKegiatan{ get;set;}
 
        public int KodeKegiatan{ get;set;}
        public long IDSubKegiatan{ get;set;}
        public int IDDinas{ get;set;}
        public int PPKD{ get;set;}
        public int KodeKategoriPelaksana{ get;set;}
        public int KodeUrusanPelaksana{ get;set;}
        public int KodeKategori{ get;set;}
        public int KodeUrusan{ get;set;}
        public int KodeSKPD{ get;set;}
        public int KodeUK{ get;set;}
   
        public int KodeProgram{ get;set;}
  
        public int KodeSubKegiatan{ get;set;}
        
        public long IDRekening{ get;set;}
    
        public decimal Jumlah{ get;set;}
 
        public Single Jenis{ get;set;}
        
        public string Keterangan{ get;set;}
        public long NoJurnal{ get;set;}
        public int Debet{set;get;}
        public int JenisJurnal{ get;set;}
        public string NoBukti{set;get;}
        public string NamaSKPD { set; get; }
        public long NoSumber { set; get; }
        public int SaldoNormal { get; set; }
        public BukuBesar()
        {
            IDUrusan=0;
           Tahun=0;
         // TanggalTransaksi { get;set;}
        IDProgram =0;
        IDKegiatan=0;
        IDSubKegiatan=0;

         IDSubKegiatan=0;
        IDDinas=0;
        PPKD=0;
        KodeKategoriPelaksana=0;
         KodeUrusanPelaksana=0;
       KodeKategori=0;
        KodeUrusan=0;
        KodeSKPD=0;
        KodeUK=0;
   
         KodeProgram=0;
         KodeKegiatan=0;
         KodeSubKegiatan=0;
        
        IDRekening=0;
    
        Jumlah=0;
 
        Jenis=0;

        Keterangan = "";
         NoJurnal=0;
         Debet = 1;
             JenisJurnal=0;
        NoBukti="";

        }
    }
}
