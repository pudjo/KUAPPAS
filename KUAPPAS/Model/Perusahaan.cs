using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Perusahaan
    {
        public int  IDPerusahaan  {set;get;}
        public string NamaPerusahaan  {set;get;}
        public int Bentuk  {set;get;}
        public string  Alamat  {set;get;}
        public string  Pimpinan  {set;get;}
        public string  Rekening  {set;get;}
        public string  NPWP  {set;get;}
        public string Bank { set; get; }
        public string KodeBank { set; get; }
        public string NamaDalamRekeningBank { set; get; }
        public string KeteranganNamaBank { set; get; }
    }
}
