using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class STS
    {
        public   long NoUrut {set;get;}
        public int IDDinas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public long IDKegiatan { set; get; }
        public int NoUrutKasda { set;get;}
        public   int Tahun {set;get;}
        public int KodeKategori {set;get;}
        public int KodeUrusan {set;get;}
        public int KodeSKPD {set;get;}
        public int KodeUK {set;get;}
        public string NoSTS {set;get;}
        public DateTime TanggalSTS {set;get;}
        public string   NoBukti {set;get;}
        public DateTime  dtBukuKas {set;get;}
        public string   Keterangan {set;get;}
        public decimal Jumlah {set;get;}
        public  string NamaBank {set;get;}
        public string NamaSKPD { set; get; }
        public  string NoRekening {set;get;}
        public  Single  Status {set;get;}
      
        public  string Penyetor {set;get;}
        public   string Alamat {set;get;}
        public   int InstitusiPenyetor {set;get;}
        public   string JabatanPenyetor {set;get;}
        public   Single PPKD {set;get;}
        public   DateTime dtInput {set;get;}
        public   int BankBUD {set;get;}
        public   Single TransferData {set;get;}
        public   Single JenisKegiatan {set;get;}
        public   string NPWP {set;get;}
        public   long NoSKR {set;get;}
        public int  Jenis { set; get; }
        public int Bank { set; get; }
        public long NoUrutSetor { set; get; }
        public string NamaRekneing { set; get; }
        public string NamaFile { set; get; }

        public int idcrt { set; get; }
        public DateTime tcrt { set; get; }
        public int idupdate { set; get; }
        public DateTime tUpdate  { set; get; }

 
        public List<STSRekening> Rekenings { set; get; }
        
        public Single StatusJurnal { set; get; }


    }
    public class FileSTS
    {
        public string NamaFIle { set; get; }
    }
    public class STSDisetor
    {
        //string[] row = { s.NoUrut.ToString(), shoudChecked.ToString(), s.NoSTS, s.TanggalSTS.FormatTanggal(), s.NoRekening, s.NamaRekneing, s.Jumlah.ToRupiahInReport(), s.Keterangan };

        public long NoUrut { set; get; }
        public string NoSTS { set; get; }
        public DateTime TanggalSTS { set; get; }
        public string NoBukti { set; get; }
        public DateTime dtBukuKas { set; get; }
        public string Keterangan { set; get; }
        public Single Status { set; get; }
        public E_JENIS_PENERIMAAN btJenis { set; get; }
        public string Penyetor { set; get; }
        public int Jenis { set; get; }
        public long NoUrutSetor { set; get; }
        public string NamaRekneing { set; get; }
        public long IIDRekening { set; get; }
        public decimal Jumlah { set; get; }


    }
   public  enum E_JENIS_PENERIMAAN{
       E_JENIS_PENERIMAAN_KE_REK_BENDAHARA = 0,
      //  E_JENIS_PENERIMAAN_KE_REK_BENDAHARA = 1,
        E_JENIS_PENERIMAAN_KE_BUD = 1,
        E_JENIS_PENERIMAAN_BLUD = 5,

   }
    public class STSRekening{
        public long NoUrut { set; get; }
        public long IDRekening { set; get; }
        public string Nama { set; get; }
        public decimal Jumlah { set; get; }

    }
}
