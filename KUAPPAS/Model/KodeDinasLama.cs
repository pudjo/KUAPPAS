using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formatting;

namespace DataAccess9.DTO
{
    public class KodeDinasLama
    {
        public int KOdekategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }


        

        public KodeDinasLama(int idbaru)
        {

            KOdekategori = DataFormat.GetInteger(idbaru.ToString().Substring(0,1));
            KodeUrusan = DataFormat.GetInteger(idbaru.ToString().Substring(1,2));
            KodeSKPD = DataFormat.GetInteger(idbaru.ToString().Substring(3,2));
            KodeUK = DataFormat.GetInteger(idbaru.ToString().Substring(5,2));

        }


    }
}
