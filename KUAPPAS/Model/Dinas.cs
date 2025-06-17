using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formatting;

namespace DTO
{
    public class Dinas
    {
        public int ID { set; get; }
        public int SKPD { set; get; }
        public int UK { set; get; }
        public string Nama { set; get; }
        public int KodeKategori { set; get;}
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }
        public string Tampilan { set; get; }
        public bool IsPPKD { set; get; }
        public int UrusanBaru { set; get; }


        public Dinas()
        {
            ID =0;
            SKPD=0;
            UK =0;
            Nama ="";
            KodeKategori=0;
            KodeUrusan =0;
            KodeSKPD =0;
            KodeUK =0;
            Tampilan ="";
            IsPPKD =false;
        }
        public Dinas(int _pDinas)
        {
            ID = _pDinas;
            if (_pDinas.ToString().Length > 5)
            {
                SKPD = DataFormat.GetInteger(_pDinas.ToString().Substring(0,5)+"00");
                if (DataFormat.GetInteger(_pDinas.ToString().Substring(6)) > 0)
                {
                    UK = _pDinas;
                }
                else
                {
                    UK = 0;
                }
                KodeKategori = DataFormat.GetInteger(_pDinas.ToString().Substring(0,1));
                KodeUrusan = DataFormat.GetInteger(_pDinas.ToString().Substring(1, 2) );
                KodeSKPD = DataFormat.GetInteger(_pDinas.ToString().Substring(3, 2) );
                KodeUK  = DataFormat.GetInteger(_pDinas.ToString().Substring(5,2));
            }
            else
            {
                SKPD = 0;                
                UK = 0;
                KodeKategori = 0;
                KodeUrusan = 0;
                KodeSKPD = 0;
                KodeUK = 0;

            }
        }
    }
}
