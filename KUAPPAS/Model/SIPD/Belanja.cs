using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUAPPAS.DataAccess9.DTO.SIPD
{
    class Belanja
    {
        public string namatahapan { set; get; } 
        public string namajadwal { set; get; } 
        public long  kodesatker { set; get; }
        public string kodepemda { set; get; } 
        public string namapemda { set; get; } 
        public int tahunrpjmdawal { set; get; }
        public int  tahunrpjmdakhir { set; get; }
        public int tahunanggaran { set; get; }
        public string  nomorperda { set; get; } 
        public DateTime tanggalperda { set; get; }
        public string namaaplikasi { set; get; } 
        public string pengembangaplikasi { set; get; } 
        public string kodefungsi { set; get; } 
        public string namafungsi { set; get; } 
        public string[] arrsubfungsi { set; get; } 
        public string kodeurusanprogram { set; get; } 
        public string namaurusanprogram { set; get; } 
        public string kodeurusanpelaksana { set; get; } 
        public string namaurusanpelaksana { set; get; } 
        public string kodeskpd { set; get; } 
        public string namaskpd { set; get; } 
        public string kodeunitskpd { set; get; } 
        public string namaunitskpd { set; get; } 
        public string kodeprogram { set; get; } 
        public string namaprogram { set; get; } 
        public string[] arroutcome { set; get; } 
        public string kodekegiatan { set; get; } 
        public string namakegiatan { set; get; } 
        public string kodesubkegiatan { set; get; } 
        public string namasubkegiatan { set; get; } 
        public string[] arroutput { set; get; }
        public string kodelokasi { set; get; } 
        public string namalokasi { set; get; }
        public string [] arrsumberdana { set; get; } 
        public string kodeakunutama { set; get; } 
        public string namaakunutama { set; get; } 
        public string kodeakunkelompok { set; get; } 
        public string namaakunkelompok { set; get; } 
        public string kodeakunjenis { set; get; } 
        public string namaakunjenis { set; get; } 
        public string kodeakunobjek { set; get; } 
        public string namaakunobjek { set; get; } 
        public string kodeakunrinci { set; get; } 
        public string namaakunrinci { set; get; } 
        public string kodeakunsubrinci { set; get; } 
        public string namaakunsubrinci { set; get; } 
        public decimal nilaianggaran { set; get; }
        public decimal  nilaianggaranperubahan { set; get; }
    }
}
