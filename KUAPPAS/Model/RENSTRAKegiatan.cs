using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class RenstraKegiatan
    {
        public int ID { set; get; } // Format panjang 

        public int IDProgram { set; get; }
        public int IDMaster { set; get; }
        public int IDProgramMaster { set; get; }
        public int IDUrusan { set; get; }
        public int IDUrusanMaster { set; get; }
        public int SKPD { set; get; }
        public int Perriode { set; get; }
        public int Tingkat { set; get; }



        public int Kode { set; get; }
        public string Nama { set; get; }
        public string KondisiAwal { set; get; }
        public string Outcome { set; get; }
        public string Keluaran { set; get; }
        public string Target1 { set; get; }
        public decimal TargetRp1 { set; get; }
        public string Target2 { set; get; }
        public decimal TargetRp2 { set; get; }
        public string Target3 { set; get; }
        public decimal TargetRp3 { set; get; }
        public string Target4 { set; get; }
        public decimal TargetRp4 { set; get; }
        public string Target5 { set; get; }
        public decimal TargetRp5 { set; get; }

        public string Target4P { set; get; }
        public decimal TargetRp4P { set; get; }
        public string Target5P { set; get; }
        public decimal TargetRp5P { set; get; }
        
        public decimal KUA1 { set; get; }
        public decimal KUA2 { set; get; }
        public decimal KUA3 { set; get; }
        public decimal KUA4 { set; get; }
        public decimal KUA5 { set; get; }
    }
}
