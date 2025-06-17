using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formatting;
namespace DTO
{
    public class AnggaranKas
    {
        public int IDUrusan { set; get; }
        public Single Tahun { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IdSubKegiatan { set; get; }
        public int KodeUK { set; get; }
        public string Nama { set; get; }

        public int IDDinas { set; get; }
        public long IDRekening { set; get; }
        public decimal Bulan1 { set; get; }
        public decimal Bulan2 { set; get; }
        public decimal Bulan3 { set; get; }
        public decimal Bulan4 { set; get; }
        public decimal Bulan5 { set; get; }
        public decimal Bulan6 { set; get; }
        public decimal Bulan7 { set; get; }
        public decimal Bulan8 { set; get; }
        public decimal Bulan9 { set; get; }
        public decimal Bulan10 { set; get; }
        public decimal Bulan11 { set; get; }
        public decimal Bulan12 { set; get; }
        public Single Jenis { set; get; }
        // public string Nama { set; get; }
        public decimal Anggaran { set; get; }
        public Single Level { set; get; }

        public Single Kelompok { set; get; }
        public Single Bold { set; get; }
        public Single Sifat { set; get; }
        public string Kode { set; get; }
        public int Tahap { set; get; }
        public int IDUnit { set; get; }
  




        public AnggaranKas(int pIDUrusan, Single pTahun, int pIDProgram, int pIDKegiatan,
                    int pIDDinas, long pIDRekening, decimal pBulan1, decimal pBulan2, decimal pBulan3, decimal pBulan4, decimal pBulan5
                    , decimal pBulan6, decimal pBulan7, decimal pBulan8, decimal pBulan9, decimal pBulan10, decimal pBulan11, decimal pBulan12, Single pJenis,
                    Single pLevel, Single pKelompok, Single pBold, Single pSifat, string pKode, int iTahap,long idsubkegiatan, int idUnit )
        {
            IDUrusan = pIDUrusan;
            Tahun = pTahun;
            IDProgram = pIDProgram;
            IDKegiatan = pIDKegiatan;
            IdSubKegiatan = idsubkegiatan;
            IDDinas = pIDDinas;

            IDRekening = pIDRekening;
            Bulan1 = pBulan1;
            Bulan2 = pBulan2;
            Bulan3 = pBulan3;
            Bulan4 = pBulan4;
            Bulan5 = pBulan5;
            Bulan6 = pBulan6;
            Bulan7 = pBulan7;
            Bulan8 = pBulan8;
            Bulan9 = pBulan9;
            Bulan10 = pBulan10;
            Bulan11 = pBulan11;
            Bulan12 = pBulan12;
            Jenis = pJenis;
            Level = pLevel;

            Kelompok = pKelompok;
            Bold = pBold;
            Sifat = pSifat;
            Kode = pKode;
            Tahap = iTahap;
            IDUnit = idUnit;




        }
        public AnggaranKas()
        {
            IDUrusan = 0;
            Tahun = 0;
            IDProgram = 0;
            IDKegiatan = 0;

            IDDinas = 0;
            IDRekening = 0;
            Bulan1 = 0;
            Bulan2 = 0;
            Bulan3 = 0;
            Bulan4 = 0;
            Bulan5 = 0;
            Bulan6 = 0;
            Bulan7 = 0;
            Bulan8 = 0;
            Bulan9 = 0;
            Bulan10 = 0;
            Bulan11 = 0;
            Bulan12 = 0;
            Jenis = 0;

        }
        public string TampilanKode(ProfileProgramKegiatan oProfile)
        {
            string sRet;
            if (IDKegiatan > 0)
            {
                //sRet = IDKegiatan.ToString().Substring(0, 1) + "." +
                //    IDKegiatan.ToString().Substring(2, 2) + "." +
                //    IDKegiatan.ToString().Substring(3, 2) + "." +
                //    IDKegiatan.ToString().Substring(5, 3);
                sRet = IDKegiatan.ToKodeKegiatan(oProfile) + ".";
            }
            else
            {
                sRet = "";
            }

            if (IDRekening > 0)
                sRet = sRet + IDRekening.ToString().Substring(0, 1) + "." +
                    IDRekening.ToString().Substring(1, 1) + "." +
                    IDRekening.ToString().Substring(2, 1) + "." +
                    IDRekening.ToString().Substring(3, 2) + "." +
                    IDRekening.ToString().Substring(5, 2);

            return sRet;

        }
    }
}
