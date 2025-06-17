using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class SumberDana
    {
       public  int ID { set; get; }
       public long IDRekening { set; get; }
       public  string Nama { set; get; }
       public long IIDParent { set; get; }
       public int Root { set; get; }
       public int Leaf { set; get; }
       public string NamaRekening { set; get; }
       public Single StatusUpdate { set; get; }


    }
}
