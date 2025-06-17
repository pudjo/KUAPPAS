using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class Pengeluaran

    {
        public long  NoUrut {set;get;}
        public int  Tahun  {set;get;}

        public E_JENISPENGELUARAN Jenis { set; get; }

        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long  IDSUbKegiatan { set; get; }
        public int Paket  { set; get; }
        

        public int  Kodekategori  {set;get;}
        public int  KodeUrusan  {set;get;}
        public int  KodeSKPD  {set;get;}
        public int  KodeUK  {set;get;}
        public int  KodekategoriPelaksana  {set;get;}
        public int  KodeurusanPelaksana  {set;get;}
        public int  KodeProgram  {set;get;}
        public int  Kodekegiatan  {set;get;}
        public int  KodeSubKegiatan  {set;get;}
        public int  Debet  {set;get;}
        public int  IDDInas  {set;get;}
        public int  KodebankPungut  {set;get;}
        public DateTime  Tanggal  {set;get;}
        public string  NoBukti  {set;get;}
        public decimal Jumlah  {set;get;}
        public string  Uraian  {set;get;}
        public long  NoUrutSPP  {set;get;}
        public int  PPKD  {set;get;}
        public int  Status  {set;get;}
        public long   NoUrutSPJUP  {set;get;}
        public long   NoUrutBAST  {set;get;}
        public string  Penerima  {set;get;}
        public int  Global  {set;get;}
        public decimal   JumlahDikembalikan  {set;get;}
        public int  Kodebank  {set;get;}
        public int IDBank { set; get; }
        public int  JenisBelanja  {set;get;}
        public long   NoReferensi  {set;get;}
        public int  StatusPajak  {set;get;}
        public int  NoUrutSetorPajak  {set;get;}
        public string   NoPungut  {set;get;}
        public int  nourutManual  {set;get;}
        public int  KodeBPP  {set;get;}
        public int Kelompok { set; get; }
        public int TahapEM { set; get; }
        public int RealisasiFisik { set; get; }
        public string NamaProgram { set; get; }
        public string NamaKegiatan { set; get; }

        public List<PengeluaranRekening> Details { set; get; }
        public List<PotonganPanjar> Potongans { set; get; }
        public List<BKU> ListBKU { set; get; }

        public int idcrt { set; get; }
        public DateTime tcrt { set; get; }
        public int idupdate { set; get; }
        public DateTime tUpdate { set; get; }
        public int UnitAnggaran { set; get; }
        public int tahap { set; get; }// Tahap anggaran kas


        public Pengeluaran()
        {
            Potongans = new List<PotonganPanjar>();
            Details = new List<PengeluaranRekening>();
            ListBKU = new List<BKU>();
            NoUrut =0;
            Tahun  =0;

            Jenis =0;

            IDUrusan =0;
            IDProgram =0;
            IDKegiatan =0;
            IDSUbKegiatan =0;
            Paket  =0;
        

        Kodekategori  =0;
        KodeUrusan  =0;
        KodeSKPD  =0;
        KodeUK  =0;
        KodekategoriPelaksana  =0;
        KodeurusanPelaksana  =0;
        KodeProgram  =0;
        Kodekegiatan  =0;
        KodeSubKegiatan =0;
        Debet  =0;
        IDDInas  =0;
        KodebankPungut  =0;
        
        NoBukti ="";
        Jumlah  =0;
        Uraian = "";
        NoUrutSPP  =0;
        PPKD  =0;
        Status =0;
        NoUrutSPJUP  =0;
        NoUrutBAST =0;
        Penerima  ="";
        Global  =0;
        JumlahDikembalikan  =0;
        Kodebank  =0;
        IDBank =0;
        JenisBelanja  =0;
        NoReferensi  =0;
        StatusPajak  =0;
        NoUrutSetorPajak  =0;
        NoPungut  ="";
        nourutManual  =0;
        KodeBPP  =0;
        Kelompok =0;
        TahapEM =0;
        RealisasiFisik =0;
        NamaProgram="";
        NamaKegiatan ="";


        }
    }
    
    public class PengeluaranRekening
    {
        public long NoUrut { set; get; }
        public long IDRekening { set; get; }
        public decimal Jumlah { set; get; }
        public string Nama { set; get; }
        public long IDSUbKegiatan { set; get; }

    }
    public class PengeluaranDanRekening:Pengeluaran
    {
        public long NoUrut { set; get; }
        public long IDRekening { set; get; }
        public decimal Jumlah { set; get; }
        public string Nama { set; get; }

    }
}
