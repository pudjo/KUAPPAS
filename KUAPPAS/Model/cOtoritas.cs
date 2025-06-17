using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class cOtoritas
    {
        public int ID { set; get; }
        public string Nama { set; get; }

        public cOtoritas(int id, string snama)
        {
            ID = id;
            Nama = snama;
        }
    }
}
