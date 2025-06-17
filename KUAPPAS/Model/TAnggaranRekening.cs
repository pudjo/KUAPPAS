using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class TAnggaranRekening
    {
       
            public int IDUrusan { set; get; }
            public Single Tahun { set; get; }
            public int IDProgram { set; get; }
            public int IDKegiatan { set; get; }
            public long SubKegiatan { set; get; }
            public int KodeKegiatan { set; get; }
            public long IDSubKegiatan { set; get; }
            public int IDDinas { set; get; }
            public int KodeKategoriPelaksana { set; get; }
            public int KodeUrusanPelaksana { set; get; }
            public int KodeKategori { set; get; }
            public int KodeUrusan { set; get; }
            public int KodeSKPD { set; get; }
            public int KodeUK { set; get; }
            public Single TahapInput { set; get; }
            public Single Tahap { set; get; }
            public int KodeProgram { set; get; }            
            public long  IDRekening { set; get; }
            public decimal JumlahOlah { set; get; }
            public decimal Jumlah { set; get; }
            public decimal JumlahPergeseran { set; get; }
            public decimal JumlahMurni { set; get; }
            public decimal Pagu { set; get; }
            public Single PPKD { set; get; }
            public Single Jenis { set; get; }
            public string Nama { set; get; }
        public string KodeSumberDana { set; get; }
        public string NamaSumberDana { set; get; }
        public List<TAnggaranUraian> ListUraian { set; get; }
            public Single StatusUpdate { set; get; }
            public decimal Plafon { set; get; }
            public decimal JumlahYAD { set; get; }
            public decimal JumlahYADAPBD { set; get; }
            public decimal JumlahDPA { set; get; }    
            public decimal Fisik { set; get; }
            public decimal Administrasi { set; get; }
            public decimal JumlahRKAP { set; get; }
            public decimal JumlahRKA { set; get; }
  
            public decimal PlafonABT { set; get; }
            public decimal JumlahABT { set; get; }
            public int rincian_ID { set; get; }
            public decimal Realisasi { set; get; }
            public decimal DPA { set; get; }
            public string SumberDana { set; get; }
            public int KOdeUnit { set; get; }
            public int IDUnit { set; get; }


            public TAnggaranRekening()
            {
                IDUrusan = 0;
                Tahun = 0;
                IDProgram = 0;
                IDKegiatan = 0;
                KodeKegiatan = 0;
                IDDinas = 0;
                KodeKategoriPelaksana = 0;
                KodeUrusanPelaksana = 0;
                KodeKategori = 0;
                KodeUrusan = 0;
                KodeSKPD = 0;
                KodeUK = 0;
                TahapInput = 0;
                KodeProgram = 0;
                IDRekening = 0;
                JumlahOlah = 0;
                Jumlah = 0;
                JumlahPergeseran = 0;
                JumlahMurni = 0;
                PPKD = 0;
                Jenis = 0;
                Plafon = 0;
                StatusUpdate = 0;



            }

            public TAnggaranRekening(
                int pIDUrusan,Single pTahun,int pIDProgram,int pIDKegiatan, int pKodeKegiatan,
                    int pIDDinas ,int pKodeKategoriPelaksana ,int pKodeUrusanPelaksana,int pKodeKategori ,int pKodeUrusan ,
                    int pKodeSKPD ,int pKodeUK ,Single pTahapInput ,int pKodeProgram ,long pIDRekening ,decimal pJumlahOlah ,decimal pJumlah,
                    decimal pJumlahPergeseran, decimal  pJumlahMurni ,Single pPPKD ,Single pJenis)                
            {

                IDUrusan =pIDUrusan;
                Tahun=pTahun;
                IDProgram=pIDProgram;
                IDKegiatan=pIDKegiatan;
                KodeKegiatan=pKodeKegiatan;
                IDDinas =pIDDinas;
                KodeKategoriPelaksana =pKodeKategoriPelaksana;
                KodeUrusanPelaksana =pKodeUrusanPelaksana;
                KodeKategori =pKodeKategori;
                KodeUrusan =pKodeUrusan;
                KodeSKPD =pKodeSKPD;
                KodeUK =pKodeUK;
                TahapInput =pTahapInput;
                KodeProgram = pKodeProgram;
                IDRekening =pIDRekening;
                JumlahOlah =pJumlahOlah;
                Jumlah =pJumlah ;
                JumlahPergeseran =pJumlahPergeseran;
                JumlahMurni =pJumlahMurni;            
                PPKD =pPPKD;
                Jenis = pJenis;   
            }

           
    }
    public class AnggaranKesehatan
    {
        public int ID { set; get; }
        public string Kode { set; get; }
        public int UK { set; get; }
        public decimal Jummlah { set; get; }

    }
}
