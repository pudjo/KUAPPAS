using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class TSubKegiatan
    {

        //public int Tahun { set; get; }
        //public int IDDinas { set; get; }
        //public int IDUrusan { set; get; }        
        //public int IDProgram { set; get; }
        //public int IDKegiatan { set; get; }
        //public long  IDSubKegiatan { set; get; }  
        //public string Nama { set; get; }
        //public decimal Pagu { set; get; }    
        public int IDUrusanMaster { set; get; }
        public int IDProgramMaster { set; get; }
        public int IDKegiatanMaster { set; get; }
        public long IDSubKegiatanMaster { set; get; }
        public int Tahun { set; get; }
        public int IDDinas { set; get; }
        public int IDUnit{ set; get; }

        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }
        public string Nama { set; get; }
        public decimal Pagu { set; get; }
        public decimal PaguABT { set; get; }
        public decimal Realisasi { set; get; }
        public string KodePendek { set; get; }
        public int KodeUk { set; get; }
        public string WaktuPelaksanaan { set; get; }
        public string Lokasi { set; get; }
        public int Kecamatan { set; get; }
        public int Desa { set; get; }
        public string Keluaran { set; get; }
        public string Outcome { set; get; }
        public string SUmberPendanaan { set; get; }
        public string Keterangan { set; get; }
        public string Mulai { set; get; }
        public string Akhir { set; get; }
        public int Status { set; get; }
        public decimal Target { set; get; }
        public string SatuanTarget { set; get; }
        public string KodeUnit{ set; get; }
        public string KodeSKPD{ set; get; }
        public int KodeUK { set; get; }

        public ProgramKegiatan PK { set; get; }



    
    }
}
