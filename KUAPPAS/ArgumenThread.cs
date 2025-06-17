using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;

namespace KUAPPAS
{
    public class ArgumenThread
    {
        public Single State;
        public List<string> ListName = new List<string>();
        public List<TAnggaranRekening> LstRek = new List<TAnggaranRekening>();
        public List<TKegiatanAPBD> LstKeg = new List<TKegiatanAPBD>();
        public List<TProgramAPBD> LstPrg = new List<TProgramAPBD>();
        public int IDDInas;
        public int Tahun;
        public bool HanyaPlafon;
        public int Tahap;


        
    }
}
