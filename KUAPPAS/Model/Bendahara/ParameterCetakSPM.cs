using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class ParameterCetakSPM
    {
        public double Height { set; get; }
        public double Width { set; get; }
        public double FontSize { set; get; }
        
        public string NamaLaporan { set; get; }
        public string Tahun { set; get; }

        //list potongan 
        //list rekening




        public string NamaPemda { get; set; }

        public string kodeSKPD { set; get; }
        public string namaSKPD { set; get; }

        public string kodeSubUnit { set; get; }
        public string namaSUbUnit { set; get; }


        public string NoSPP { set; get; }

        public string tanggalSPP { set; get; }
        public string sumberDana { set; get; }


        public string NoSPM { set; get; }
        public string tanggalSPM{ set; get; }
        public string JenisSPM{ set; get; }


        public string jenispenerima { set; get; }
        public string namapenerima { set; get; }
        public string norekeningpenerima { set; get; }
        public string namabankpenerima { set; get; }
        public string npwppenerima { set; get; }
        public string nomorspd { set; get; }
        public string jumlahspd { set; get; }

        public string keterangan { set; get; }
        public string jumlah { set; get; }
        public string jumlahterbilang { set; get; }
        public List<SPPRekening> Rekenings { set; get; }

        public List<PotonganSPP> Informasipotongan{ set; get; }
        public List<PotonganSPP> Potongan { set; get; }

        public string JumlahPotonganString { set; get; }
        public string JumlahInformasiString { set; get; }

        public decimal JumlahMurni { set; get; }
        public decimal JumlahPotongan { set; get; }
        public decimal JumlahInformasi { set; get; }


        public string jabatanpenandatangan { set; get; }
        public string namapenandatangan { set; get; }
        public string nippenandatangan { set; get; }


    }
}
