using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Akuntansi
{
    public class JurnalPajak
    {
        

                  public long NoUrut{ set; get; }
                  public DateTime Tanggal { set; get; }
                  public string NoBukti { set; get; }
                  public int Kelompok { set; get; }
                  public decimal Pungut { set; get; }
                  public decimal Setor { set; get; }
                  public string Keterangan { set; get; }
                  public int Dijurnal { set; get; }
         

        
    }
}
