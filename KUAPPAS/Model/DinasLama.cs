using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class DinasLama
    {
        public int KodeKategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }

        public DinasLama(int _dinasBaru)
        {
            if (_dinasBaru.ToString().Length < 5)
            {
                KodeKategori = 0;
                KodeUrusan = 0;
                KodeSKPD = 0;
                KodeUK = 0;
            }
            if (_dinasBaru.ToString().Length == 5)
            {
                KodeKategori = Convert.ToInt32(_dinasBaru.ToString().Substring(0,1));
                KodeUrusan = Convert.ToInt32(_dinasBaru.ToString().Substring(1, 2));
                KodeSKPD = Convert.ToInt32(_dinasBaru.ToString().Substring(3,2));
                KodeUK = 0;
            }
            if (_dinasBaru.ToString().Length == 5)
            {
                KodeKategori = Convert.ToInt32(_dinasBaru.ToString().Substring(0, 1));
                KodeUrusan = Convert.ToInt32(_dinasBaru.ToString().Substring(1, 2));
                KodeSKPD = Convert.ToInt32(_dinasBaru.ToString().Substring(3, 2));
                KodeUK = Convert.ToInt32(_dinasBaru.ToString().Substring(5, 2));
            }

        }
    }
}
