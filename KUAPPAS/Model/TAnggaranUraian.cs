using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class TAnggaranUraian
    {
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }

        public int IDDinas { set; get; }
        public Single TahapInput { set; get; }
        public Single Tahap { set; get; }

        public Single Tahun { set; get; }
        public int IDRincian { set; get; }
        public string Induk { set; get; }
        
        public int NoUrut { set; get; }
        
        public int NoUrutDPA { set; get; }
        public string SatuanDPA { set; get; }
        public int LevelDPA { set; get; }        
        public int IDUraian { set; get; }
        public int IDLokasi { set; get; }
        public Single Level { set; get; }
        public int KodeKegiatan { set; get; }        
        public int KodeKategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
        public int KodeKategori { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }
        public int KodeProgram { set; get; }
        public long  IDRekening { set; get; }


        public string Satuan { set; get; }



        public double VolOlah { set; get; }
        public double Vol { set; get; }
        public double VolPergeseran { set; get; }
        public double VolMurni { set; get; }
        public double VolABT { set; get; }

        public decimal PPNOlah { set; get; }
        public decimal PPNMurni { set; get; }
        public decimal PPNGeser { set; get; }
        public decimal PPNP { set; get; }
        public decimal PPNABT { set; get; }
        public string KodeSumberDana { set; get; }
        public string NamaSumberDana { set; get; }

        


        public decimal HargaOlah { set; get; }
        public decimal Harga { set; get; }
        public decimal HargaABT { set; get; }
        public decimal HargaPergeseran { set; get; }
        public decimal HargaMurni { set; get; }
        public decimal Realisasi { set; get; } 
     
        public decimal PPNRKA { set; get; }
        public decimal PPNPergeseran { set; get; }
        

        public string Uraian { set; get; }
        public string UraianAPBD { set; get; }
        public string UraianMurni { set; get; }
        public string UraianGeser { set; get; }
        
        
        public decimal JumlahOlah { set; get; }
        public decimal Jumlah { set; get; }
        public decimal JumlahMurni { set; get; }
        public decimal JumlahGeser { set; get; }
        public decimal JumlahYAD { set; get; }
        public decimal JumlahYADAPBD { set; get; }


        public string IDStandardHarga { set; get; }
        public Single Leaf { set; get; }
        public Single Jenis { set; get; }
        public long IDAnggaranKAS { set; get; }
        public Single StatusUpdate { set; get; }
        public int ID {set;get;}
        public Single ShowInReport { set; get; }
        public string  Label  { set; get; }
        public string LabelDPA { set; get; }
        public decimal Plafon { set; get; }
        public Single PPKD { set; get; }
        public int rincianID { set; get; }
        public int uraianID { set; get; }
        public decimal StandardHarga { set; get; }
        public long IDBarang { set; get; }
        public int IDRKBMD { set; get; }
        public int IDRKBMDBArang { set; get; }
        public string Keterangan { set; get; }
        public string KodeSKPDSIPD { set; get; }
        public string KodeUnit { set; get; }
        public string KodeSH { set; get; }

        //volBapeda decimal (10,5), sUraianBapeda varchar(200), satuanBapeda varchar(50), cHargaBapeda decimal(20,5), cJumlahBapeda decimal(20,5))






    }
}
