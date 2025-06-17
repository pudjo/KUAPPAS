//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace DTO
//{
//    public class Musrenmbang
//    {

//      public int id { set; get; }
//      public int desa_id{ set; get; }
//      public int  dusun_id{ set; get; }
//      public int kecamatan_id{ set; get; }
//      public int skpd_id{ set; get; }
//      public int  program_id{ set; get; }
//      public int kegiatan_id{ set; get; }
//      public int prioritas{ set; get; }
//      public string  nama{ set; get; }
//      public decimal  volume{ set; get; }
//      public int satuan{ set; get; }
//      public decimal  pagu{ set; get; }
//      public string  keterangan { set; get; }
//      public int iTahun { set; get; }
//      public int IDUrusan { set; get; }
//      public int IDDInas { set; get; }
//      public int IDProgram { set; get; }
//      public int IDKegiatan { set; get; }
//      public int status { set; get; }
//      public int TahunAPBD { set; get; }
//        public int TahapAPBD { set; get; }
//        public decimal Pagu2 { set; get; }
        
//        public int IDProgramMaster { set; get; }
//        public int IDKegiatanMaster { set; get; }
//        public int IDUrusanMaster { set; get; }
      


//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Musrenmbang
    {

        public int id { set; get; }
        public int Type { set; get; }
        public string nama { set; get; }
        public decimal volume { set; get; }
        public int IDDInas { set; get; }
        public string satuan { set; get; }
        public decimal pagu { set; get; }
        public string keterangan { set; get; }
        public int iTahun { set; get; }
        public long IDSUbKegiatan { set; get; }
        public long iidrekening { set; get; }
        public int Baris { set; get; }





    }
}

