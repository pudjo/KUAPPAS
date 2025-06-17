using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formatting;

namespace DTO
{
    public class RingkasanPerda
    {
        public int Kelompok { set; get; }
        public long IDRekening { set; get; }
        public int Level { set; get; }
        public Single Bold { set; get; }
        public int Root { set; get; }
        public string Kode { set; get; }
        public string Nama { set; get; }
        public decimal JumlahOlah { set; get; }
        public decimal Jumlah { set; get; }
        public decimal JumlahMurni { set; get; }
        public string Selisih { set; get; }
        public string Persen { set; get; }
        public string SJumlah { set; get; }
        public string SJumlahMurni { set; get; }
        


        public RingkasanPerda()
        {
            Kelompok =0;
            Level =0;
            Kode = "";
            Bold =0;
            Nama ="";
            JumlahOlah=0;
            Jumlah =0;
            JumlahMurni = 0;
            Selisih = "0";
            Persen = "0";

        }
        public RingkasanPerda(int pKelompok ,int pLevel ,Single pBold ,string pKode,string pNama , decimal pJumlahOlah ,decimal pJumlah ,decimal pJumlahMurni )
        {
            Kelompok = pKelompok;
            Level = pLevel;
            Bold = pBold;
            Kode = pKode;
            Nama = pNama;
            JumlahOlah = pJumlahOlah;
            Jumlah = pJumlah;
            JumlahMurni = pJumlahMurni;
            Selisih =( pJumlah - pJumlahMurni).ToRupiahInReport();
            Persen = DataFormat.GetProsentase(pJumlahMurni,pJumlah);

        }

    }
}

