using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class PerdaII
    {
        public string Kode { set; get; }
        public int Level { set; get; }
        public string IDKategori { set; get; }
        public string IDUrusan { set; get; }
        public Single IDDInas { set; get; }
        public string Rek { set; get; }
        
        public Single root { set; get; }
        public string Nama { set; get; }
        public string Pendapatan { set; get; }
        public string BelanjaTidakLangsung { set; get; }
        public string BelanjaLangsung { set; get; }
        public string JumlahBelanja { set; get; }
        public string PendapatanMurni { set; get; }
        public string BelanjaTidakLangsungMurni { set; get; }
        public string BelanjaLangsungMurni { set; get; }
        public string JumlahBelanjaMurni { set; get; }
        public string SilpaTB { set; get; }
        public string SilpaTBMurni { set; get; }
        public string SilpaTBOlah { set; get; }
        public string Penerimaan { set; get; }
        public string PenerimaanMurni { set; get; }
        public string Pembayaran { set; get; }
        public string PembayaranMurni { set; get; }
        
        public string SelisihPendapatan { set; get; }
        public string PersenPendapatan { set; get; }

        public string SelisihBelanja { set; get; }
        public string PersenBelanja { set; get; }

        public string BelanjaOperasi { set; get; }
        public string BelanjaModal { set; get; }
        public string BelanjaTakTerduga { set; get; }
        public string BelanjaTransfer { set; get; }

        public string BelanjaOperasiMurni { set; get; }
        public string BelanjaModalMurni { set; get; }
        public string BelanjaTakTerdugaMurni { set; get; }
        public string BelanjaTransferMurni { set; get; }




    }

    public class PerdaIIB
    {
        public string Kode { set; get; }
        public int Level { set; get; }
        public Single IDKategori { set; get; }
        public Single IDUrusan { set; get; }
        public Single IDDInas { set; get; }

        public Single root { set; get; }
        public string Nama { set; get; }
        public string Pendapatan { set; get; }
        public string AnggaranPegawai { set; get; }
        public string AnggaranBarangJasa { set; get; }
        public string AnggaranPemeliharaan { set; get; }
        public string AnggaranModal { set; get; }

        public string JumlahAnggaran { set; get; }


        public string PendapatanMurni { set; get; }
        public string RealisasiPegawai { set; get; }
        public string RealisasiBarangJasa { set; get; }
        public string RealisasiPemeliharaan { set; get; }
        public string RealisasiModal { set; get; }
        public string JumlahRealisasi { set; get; }

        public string SelisihPendapatan { set; get; }
        public string PersenPendapatan { set; get; }

        public string SelisihBelanja { set; get; }
        public string PersenBelanja { set; get; }





    }
}
