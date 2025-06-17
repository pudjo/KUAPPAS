using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formatting;


namespace KUAPPAS.DataAccess9.DTO
{
    class ProgramKegiatanLama

    {
        public int KOdeProgram { set; get; }
        public int KodeKegiatan { set; get; }
        public int KodeSubKEgiatan { set; get; }
        
        public ProgramKegiatanLama(long idsubkegiatan)
        {
            if (idsubkegiatan.ToString().Length != 7)
            {
                KOdeProgram = 0;
                KodeKegiatan = 0;
                KodeSubKEgiatan = 0;

            }
            else
            {
                KOdeProgram = DataFormat.GetInteger(idsubkegiatan.ToString().Substring(0, 2));
                KodeKegiatan = DataFormat.GetInteger(idsubkegiatan.ToString().Substring(3, 3));
                KodeSubKEgiatan = DataFormat.GetInteger(idsubkegiatan.ToString().Substring(6, 2));
            }
                // KodeUK = DataFormat.GetInteger(idbaru.ToString().Substring(5,2));

        }

    }
}
