using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class Setor
    {
        public long NoUrut { set; get; }
        public int Tahun { set; get; }

        public int KodeKategori { set; get; }
        public int KodeUrusan  { set; get; }
        public int KodeSKPD  { set; get; }
        public int KodeUK  { set; get; }

        public int KodekategoriPelaksana  { set; get; }
        public int KodeUrusanPelaksana  { set; get; }
        public int KodeProgram { set; get; }
        public int KodeKegiatan { set; get; }
        public int KodeSubKegiatan { set; get; }
        
        public string NoBukti { set; get; }
        public DateTime dtBukuKas { set; get; }
        public string NoBukuKas { set; get; }
        public string Keterangan { set; get; }
        public decimal Jumlah { set; get; }
        public int Jenis { set; get; }
        public Single Status { set; get; }
        public int JenisSP2D { set; get; }
        
        public string NobuktiClient { set; get; }    

        
        
        public int SubKegiatan { set; get; }

        public Single PPKD { set; get; }
        public int BankBUD { set; get; }
        public string NamaBank { set; get; }
        public string NoRekening { set; get; }
        public string Penerima { set; get; }
        public string Alamat { set; get; }
        public string NPWP { set; get; }
        
        public DateTime dtInput { set; get; }
        public long NoUrutClient { set; get; }
        public Single JenisBendahara { set; get; }
        public Single Sumber { set; get; }
        public bool bnew { set; get; }
        public Single StatusJurnal { set; get; }
        public int Kodebank { set; get; }
        public int IDImport { set; get; }
        public Single BLUD { set; get; }
        public string NoNTPN { set; get; }
        public string KodeBilling { set; get; }

        public Single SetorKeKasda  { set; get; }
        public string NourutBayangan { set; get; }
        public Single TahunLalu { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int  IDKegiatan { set; get; }
        public long IDSubKegiatan{ set; get; }
        
        public int IDDinas { set; get; }

        public int idcrt { set; get; }
        public DateTime tcrt { set; get; }
        public int idupdate { set; get; }
        public DateTime tUpdate { set; get; }
        public int UnitAnggaran { set; get; }
  

        public List<SetorRekening> Details { set; get; }
        public List<STSDisetor> STSDisetors { set; get; }



    }
    public class SetorRekening
    {
        public int KodeuK { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IdKegiatan { set; get; }
       
        public long IDSubKegiatan { set; get; }
        public long  NoUrut { set; get; }
        public long NoUrutBelanja { set; get; }

        public long IDRekening { set; get; }
        public decimal Jumlah { set; get; }
        public string NamaRekening { set; get; }
   


    }
    public enum E_JENIS_SETOR{

        E_SETOR_STS = 1,
        E_SETOR_UYHD = 2,
        E_SETOR_CP = 3,
        E_SETOR_PAJAK = 4,
        E_SETOR_SISATU=6, 
        E_ALL = 5 // untuk query 2,3


    }

}
