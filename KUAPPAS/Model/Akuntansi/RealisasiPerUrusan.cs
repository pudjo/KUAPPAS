using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class RealisasiPerUrusan
    {
            public int kelompok{set;get;} 
            public int level{set;get;} 
            public int IDDInas{set;get;}  
            public int IDurusan{set;get;} 
            public int IDProgram{set;get;}  
            public int idkegiatan{set;get;}  
            public long idsubkegiatan {set;get;} 
            public long iidrekening {set;get;}  
            public int kodeKategori {set;get;}  
            public int KodeUrusan{set;get;} 
            public int Kodeskpd{set;get;} 
            public string    Kode{set;get;}  
            public int  kodeprogram{set;get;}  
            public int  kodekegiatan{set;get;}  
            public int  kodesubkegiatan{set;get;} 
            public int  Rek{set;get;} 
            public string   Nama{set;get;}  
            public decimal AnggaranOperasi{set;get;} 
            public decimal  RealisasiOperasi{set;get;} 
            public decimal AnggaranModal{set;get;} 
            public decimal RealisasiModal{set;get;} 
            public decimal  AnggaranTakTerduga{set;get;} 
            public decimal  RealisasiTakTerduga{set;get;} 
            public decimal  AnggaranTransfer{set;get;} 
            public decimal  RealisasiTransfer{set;get;} 
            public decimal  AnggaranOperasiPegawai{set;get;} 
            public decimal  AnggaranOperasiBarangJasa{set;get;} 
            public decimal  AnggaranOperasiHibah{set;get;} 
            public decimal  AnggaranOperasiBantuanSosial{set;get;} 
            public decimal  AnggaranModalTanah{set;get;} 
            public decimal  AnggaranModalPeralatanMesin{set;get;} 
            public decimal  AnggaranModalBangunan{set;get;} 
            public decimal  AnggaranModalJIJ{set;get;} 
            public decimal  AnggaranModalAsetTetapLainnya{set;get;} 
            public decimal  AnggaranBagiHasil{set;get;} 
            public decimal  AnggaranBantuanKeuangan{set;get;} 
            public decimal  Anggaran{set;get;} 
            public decimal  RealisasiOperasiPegawai{set;get;} 
            public decimal  RealisasiOperasiBarangJasa{set;get;} 
            public decimal  RealisasiOperasiHibah{set;get;} 
            public decimal  RealisasiOperasiBantuanSosial{set;get;} 
            public decimal  RealisasiModalTanah{set;get;} 
            public decimal  RealisasiModalPeralatanMesin{set;get;} 
            public decimal  RealisasiModalBangunan{set;get;} 
            public decimal  RealisasiModalJIJ{set;get;} 
            public decimal  RealisasiModalAsetTetapLainnya{set;get;} 

            public decimal  RealisasiBagiHasil{set;get;} 
            public decimal  RealisasiBantuanKeuangan{set;get;} 
            public decimal  Realisasi{set;get;}
            public string Keluaran { set; get; } 
    }
}
