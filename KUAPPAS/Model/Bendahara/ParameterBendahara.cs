using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class ParameterBendahara
    {
        public int Jenis { set; get; }
        public long NoUrut { set; get; }
        public DateTime TanggalAwal { set; get; }
        public DateTime TanggalAkhir { set; get; }
        public int JenisTanggal { set; get; }
        public int JenisBelanja{ set; get; }
        public long NoUrutSP2D { set; get; }

        public List<int>   LstStatus { set; get; }
        public List<int> LstJenis { set; get; }
        
        public int Status {set;get;}
        public int IDDInas { set; get; }
        public int IDUrusan { set; get; }
        public int IDKegiatan { set; get; }
        
        public string Keterangan { set; get; }
        public string Nomor { set; get; }
        public long NoUrutSPJ {set;get;}
        public int KodeKategori { set; get; }
        public int KodeUrusan  { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK  { set; get; }
        
        public string NoSPM { set; get; }
        public string NoSP2D { set; get; }
        public string NoSPP { set; get; }
        public bool WithPotongan { set; get; }
        public string OrderBy { set; get; }
        public int TidakdiBKU { set; get; }








        public  ParameterBendahara( int Tahun )
        {
            Jenis =0;
            WithPotongan = false;
            TidakdiBKU = 0;
            int status = 0;
            LstStatus = new List<int>();
            LstStatus.Add(status);

            IDDInas =0;
            IDUrusan =0;
            IDKegiatan =0;
            NoSPP = "";
            NoSPM = "";
            NoSP2D = "";
            TanggalAwal = new DateTime(Tahun,1,1);
            TanggalAkhir = new DateTime(Tahun, 12, 31);
            OrderBy = "";



        }

    }
}
