using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class RKA22Murni
    {

        public int IDUrusan { set; get; }

        public int  IDProgram { set; get; }
        public int IDkegiatan { set; get; }

        public string KodeProgram { set; get; }
        public string KodeKegiatan { set; get; }
        public string KodeSubKegiatan { set; get; }
        
        public string Nama { set; get; }
        public string SumberPendanaan{ set; get; }
        
        public string Lokasi { set; get; }
        public string Target { set; get; }
        public string BelanjaPegawai { set; get; }
        public string BelanjaBarangJasa { set; get; }
        public string BelanjaModal { set; get; }
        public string Jumlah { set; get; }

        public string BelanjaPegawaiMurni { set; get; }
        public string BelanjaBarangJasaMurni { set; get; }
        public string BelanjaModalMurni { set; get; }

        public string JumlahMurni { set; get; }
        public string Selisih { set; get; }
        public string Persen { set; get; }

        public string BelanjaBantuanSosial{ set; get; }
public string BelanjaBantuanSosialMurni{ set; get; }
public string BelanjaHibah{ set; get; }
public string BelanjaHibahMurni{ set; get; }

    public string BelanjaSubsidi{ set; get; }
    public string BelanjaSubsidiMurni { set; get; }

        public string BelanjaBunga{ set; get; }
        public string BelanjaBungaMurni { set; get; }

            public string BelanjaBTT{ set; get; }
            public string BelanjaBTTMurni { set; get; }

                public string BelanjaBagiHasil{ set; get; }
public string BelanjaBagiHasilMurni{ set; get; }

public string BelanjaBantuanKeuangan { set; get; }



        //public decimal KodeProgram { set; get; }

    }
    
}
