using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public static class clsJenisJabatan
    {
       public  enum JenisJabatan
        {
            KEPALA_DAERAH = 1,
            SEKRETARIS_DAERAH = 2,
            PPKD = 3,
            PEJABAT_BUD = 4,
            PEGAWAI_KASDA = 5,
            KEPALA_DINAS = 6,
            BENDAHARA_PENGELUARAN = 7,
            BENDAHARA_PENERIMAAN = 8,
            PPK_SKPD = 9,
            BENDAHARA_PENGELUARANPPKD = 10,
            BENDAHARA_PENERIMAANPPKD = 11
        };
    }
}
