using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class PotonganSPP
    {
        public long NoUrut { set; get; }
        public long IIDRekening { set; get; }
        public string NamaRekening { set; get; }
        public string KodeRekening { set; get; }
        
        public string JumlahString { set; get; }

        public decimal Jumlah { set; get; }
        public Single Informasi { set; get; }
        public Single No { set; get; }
        public string KodeMap { set; get; }
        public string NTPN { set; get; }
        public string Nama { set; get; }

        public string NPWPPenyetor { set; get; }
        public string NoFaktur { set; get; }
        public string NIKRekeninan { set; get; }

        
        public string IDBilling { set; get; }
        public string KodeSetor { set; get; }

        public int masa_bulan { set; get; }
        public int masa_tahun { set; get; }
        public int mata_uang { set; get; }
        public int wp_badan { set; get; }
        public int wp_pemungut { set; get; }
        public int wp_op { set; get; }
        public int npwp_nol { set; get; }
        public int npwp_lain { set; get; }
        public int butuh_nop { set; get; }
        public int butuh_nosk { set; get; }
        public int npwp_rekanan { set; get; }
        public int nik_rekanan { set; get; }
        public int nomor_faktur { set; get; }
        public int no_skpd { set; get; }
        public int no_spm { set; get; }
        public string keterangan { set; get; }

      public string nomorPokokWajibPajak{ set; get; } //": "string(15)", //mandatory field
public string kodeMap { set; get; } //": "string(6)", //mandatory field
public string kodeSetor{ set; get; } //, //mandatory field
public string masaPajak{ set; get; } //, //mandatory field | MMMM
public string tahunPajak{ set; get; } //, //mandatory field | yyyy
public string jumlahBayar{ set; get; } //, //mandatory field
public string nomorObjekPajak{ set; get; } //,
public string nomorSK{ set; get; } //",
public string nomorPokokWajibPajakPenyetor{ set; get; } //", //mandatory field
public string namaWajibPajak{ set; get; } //: "string(30)",
public string alamatWajibPajak{ set; get; } //: "string(50)",
public string kota{ set; get; } //: "string(30)"
      public string nik{ set; get; } //: "string(16)",
public string nomorPokokWajibPajakRekanan{ set; get; } //: "string(15)",
public string nikRekanan{ set; get; } //: "string(16)",
public string nomorFakturPajak{ set; get; } //: "string(16)",
public string nomorSKPD{ set; get; } // : "string(70)", //6digit kode Pemda + max 15digit kode OPD/SKPD
public string nomorSPM { set; get; } //: "string(70)"
        
    }
}
