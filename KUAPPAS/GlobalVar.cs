using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using DTO;
using DTO.Bendahara;
using DTO.Anggaran;


namespace KUAPPAS
{
    public static class GlobalVar
    {
        public  const string Password = "KUAPPAS";
        public static OleDbConnection g_Connection;
        public static int g_ProjectID;

        public const int PROJECT_DEMO = 1;
        static public bool SPECIFICBIDANG;
        static public int KODE_BIDANG;
        static public string NAMADAERAH;
        static public int IDSKPD;
        static public string NAMASKPD;
        static public string KODEPROVINSI;
        static public string NAMAPROVINSI;
        static public string KODEDAERAH;
        static public int TahunAnggaran;
        static public bool PP90;
        static public Pengguna Pengguna;        
        static public string NamaDatabase;
        static public string NamaServer;
        static public Single TahapKUA;
        static public int TahapAnggaran;
        static public int Theme;
        static public string Versi;
        static public ProfileRekening ProfileRekening;
        static public ProfileProgramKegiatan ProfileProgramKegiatan;
        static public string ImageLocation;
        static public string Key = "01234567890123456789012345678901";  
        static public int TahapAnggaranDinas;
        static public List<cOtoritas> gListOtoritas;
        static public Pemda gPemda;
        static public List<SKPD> guserSKPD;
        static public List<UrusanDinas> gListUrusanDinas;
        static public List<TProgramAPBD> gListProgram;
        static public List<TKegiatanAPBD> gListKegiatan;
        static public List<RefPajak> gListRefPajak;
        static public List<ProgramKegiatanAnggaran> gListProgramKegiatanRekeningAnggaran; 


        static public List<TSubKegiatan> gSubKegiatan;
        static public List<TAnggaranRekening> gListRekeningAnggaran;
        static public List<Pengguna> glistPengguna;
        static public List<Rekening> gListRekening;
        static public List<SKPD> gListSKPD;
        static public List<Unit> gListOrganisasi;
        static public List<Kontrak> gListKontrak;
        static public List<BAST> gListBAST;
        static public List<SPP> gListSPP;
        static public List<DaftarBank> gLstBanks;

        static public List<SPD> gListSPD;


        static public List<SPPRekening> gListSPPRekening;
        static public List<PotonganSPP> gListSPPPotongan;
        static public List<RedaksiSPP> gListRedaksiSPP;

        //static public List<PotonganSPP> gListSPP;



        static public List<Pengeluaran> gListPengeluaran;
        static public List<PengeluaranRekening> gListPengeluaranRekening;

        static public List<BKU> gListBKU;

        


        



       // static public int Koneksi;
        static public int JENIS_KONEKSI;
      static  public int PERBAIKAN;
    static public string BANK_URL = "http://36.92.240.142:8082/" ;// "http://36.92.240.142:8082/";////"https://103.28.53.130/";//
     // static public string BANK_URL = "https://localhost:7139/";// 

       // static public string BANK_URL = "https://localhost:7139/";// "http://36.92.240.142:8080/";////"https://103.28.53.130/";//
        
        //"https://localhost:7139/";//
        static public string TRIPPLEDES_KEY = "20561889-ac47-48d2-9211-8f4b4ed0";
        static public string Salt = "indahnyadunia";
        
        static public RemoteConnection CONNECTION_ASET;
        static public RemoteConnection CONNECTION_PERENCANAAN;
        static public  string DataSource;
        //static public string NanaDaerah;
        public enum JENIS_TABLE{
            TABLE_STS = 1,
            TABLE_SPP = 2,
            TABLE_SETOR = 3,
            TABLE_PANJAR = 4,
            TABLE_SKR = 5,
            TABLE_BAST = 6,
            TABLE_ASET = 7,
            TABLE_KOREKSI = 8,
        }
        

    }
}
