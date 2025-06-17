using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class SPP
    {
        public decimal Jumlah { set; get; }
        public long NoUrut { set; get; }
        public int Tahun { set; get; }
        public string NoSPP { set; get; }
        public string NoSPM { set; get; }
        public string NoSP2D { set; get; }
        public DateTime dtSPP { set; get; }
        public DateTime dtSPM { set; get; }
        public DateTime dtCair { set; get; }
        public DateTime dtTerbit { set; get; }
        public DateTime TanggalTerimaSPM { set; get; }
        public int SifatKegiatan { set; get; }
        public int NoReferensiBankOnline { set; get; }

        public string Peruntukan { set; get; }
        public int Status { set; get; }
        public int Kodekategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }
        public string NamaPenerima { set; get; }
     
        public int JenisKegiatan { set; get; }
        public bool bNew { set; get; }
        public string Keterangan { set; get; }
        public long NoUrutSPD { set; get; }
        public int NoBpp { set; get; }
        public int SUmberDana { set; get; }
        public int SubSumberDana { set; get; }

        public string NoSPJUP { set; get; }
        public DateTime dtSPJUP { set; get; }

        public string Alamat { set; get; }
        public string NoNPWP { set; get; }
        public string NoRek { set; get; }
        public string NamaBank { set; get; }
        public string KodeBank { set; get; }
        public string KeteranganNamaBank { set; get; }
        public string NamaDalamRekeningBank { set; get; }
        
        
        public int  Penerima { set; get; } //ID 
        public string JabatanPenerima { set; get; }
        public string NamaPerusahaan { set; get; }
       
        public int SSPSetor { set; get; }
        public string WaktuPelaksanaan { set; get; }
        public DateTime dtSSP { set; get; }
        public string NoSSP { set; get; }
        public DateTime dtKontrak { set; get; }
        public int PPKD { set; get; }
        public string Bulan { set; get; }
        public int StatusJurnal { set; get; }
        public int KodeProgram { set; get; }
        public int KodeKegiatan { set; get; }
        public int KodeSubKegiatan { set; get; }


        public int kodeKategoripelaksana { set; get; }
        public int Kodeurusanpelaksana { set; get; }

        public int MultiYear { set; get; }
        public int BUlan { set; get; }
        public int Bulan2 { set; get; }

        public int Tahun1 { set; get; }
        public int Tahun2 { set; get; }

        public int BankBUD { set; get; }
        public int PenandatanganSP2d { set; get; }
        public string NamaPencetak { set; get; }
        public int kalicetak { set; get; }
        public string NamaBankBUD { set; get; }
        public string NoRekBUD { set; get; }
        public string NoBAST { set; get; }
        public string NoSPPAT { set; get; }
        public int JenisDocSumber { set; get; }
        public int SifatPajak { set; get; }
        public decimal JumlahPotongan { set; get; }
        public decimal JumlahPotonganMPN { set; get; }
        public decimal JumlahPotonganNonMPN { set; get; }
        public decimal JumlahBayar { set; get; }
        public string JenisTransfer { set; get; }
        public int PenandatanganBUD { set; get; }
        //'0 -> pungut Setor
        //'1 -> pungut saja
        public string NamaSumberDana { set; get; }
        
        public int IDBank { set; get; }
        public int iNOSPP { set; get; }
        public int iNOSPM { set; get; }
        public int iNOSP2D { set; get; }
        public int Bendahara { set; get; }
        public int JenisGaji { set; get; }
        public int NoUrutKasda { set; get; }
        public int SubSUmberDana { set; get; }

        public string  NamaPenandaTanganSPM { set; get; }
        public string JabatanPenandaTanganSPM { set; get; }
        public string NIPPenandaTanganSPM { set; get; }


        public string KeteranganSumberDana { set; get; }
        public string NoKontrak { set; get; }
        public long INoUrutKontrak { set; get; }
        public string NamaPPTK { set; get; }
        public string IDPPTK { set; get; }
        public string NIPPPTK { set; get; }
        public string JabatanPPTK { set; get; }
        public string ErrorMessage { set; get; }
        public int Jenis { set; get; }
        public int IDDInas { set; get; }
        
        public int BanyakKegiatan{ set; get; }
        public int TahapEM{ set; get; }
        public int RealisasiFisik { set; get; }

        public string statusOnline { set; get; }

        public int idCetak { set; get; }
        public DateTime tCetak { set; get; }
        
    //    alter table tSPP add idcrt int, dcrt Datetime, idupdate int, dupdate DateTime ,idCetak int, dCetak DateTime
        public int idcrt { set; get; }
        public DateTime tcrt { set; get; }
        public int idupdate { set; get; }
        public DateTime tUpdate { set; get; }
        public int UnitAnggaran { set; get; }

        public List<int>  LstIDKegiatan { set; get; }
        public List<int> ListIDProgram { set; get; }
        public List<int> ListIDUrusan { set; get; }
        public List<int> ListIDSubKegiatan { set; get; }

        public List<SPPRekening> Rekenings { set; get; }
        public List<PotonganSPP> Potongans { set; get; }
        public List<BKU> ListBKU { set; get; }


      //  public string KodeUrusan { set; get; }
        public string NamaDinas { set; get; }
        public List<SPD> lstSPD { set; get; }
        public RingkasanBelanja Rb { set; get; }
        public string NamaProgram{ set; get; }
        public string NamaKegiatan { set; get; }
        public string NamaSubKegiatan { set; get; }
        public string NamaUrusan { set; get; }
        public List<KelengkapanSPM> Kelengkapaan { set; get; }

        public int diBKU { set; get; }
  

    }
    public class SilakanVerifikasi
    {
        public string status { set; get; }
        public string catatan_verifikator { set; get; }


    }
}
