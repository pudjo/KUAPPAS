using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Pengguna
    {
        public string Nama { set; get; }
        public int ID { set; get; }
        public string Password { set; get; }
        public string Password2 { set; get; }
        public Single Status { set; get; }
        public Single IsUserDinas { set; get; }
        public string NIK { set; get; }
        public string UserID { set; get; }
        public List<PenggunaDinas> lstDinas { set; get; }
        public Single Level { set; get; }
        public int SKPD { set; get; }
        public int KodeUK { set; get; }
        public bool PPKD { set; get; }
        public int Kelompok{ set; get; }
        public List<PenggunaGroup> lstGroup { set; get; }
        public int Sumber { set; get; }

     

    }
}
