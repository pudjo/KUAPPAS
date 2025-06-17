using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class BAST
    {
        public long NoUrut { set; get; }
        public int Tahun { set; get; }

        public int IDDInas{ set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }


        public int Kodekategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUk { set; get; }
        public int KodekategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
        public int KodeProgram { set; get; }
        public int KodeKegiatan { set; get; }
        public int KodeSubKegiatan { set; get; }
        public DateTime dtBAST { set; get; }
        public Single Status { set; get; }
        public string NoBAST { set; get; }
        public int PihakKetiga { set; get; }
        public string NamaPihakKetiga { set; get; }
        public Single PPKD { set; get; }
        public string Uraian { set; get; }
        public string NOKontrak { set; get; }
        public long NoUrutKontrak { set; get; }
        public string NoSP2D { set; get; }
        public DateTime TanggalSP2D { set; get; }
        public string  NoUrutSP2D { set; get; }
        public string JumlahSP2D { set; get; }
        public List<BASTRekening> Rekening { set; get; }
        public long IDSubKegiatan { set; get; }
        public Kontrak oKontrak { set; get; }

        public int idcrt { set; get; }
        public DateTime tcrt { set; get; }
        public int idupdate { set; get; }
        public DateTime tUpdate { set; get; }



        
    }
    public class BASTRekening
    {
        public string NoUrut { set; get; }
        public long IDRekening { set; get; }
        public decimal Jumlah { set; get; }
        public string NamaRekening { set; get; }
    }
        
}
