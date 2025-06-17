using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public  class SessionInputRKA
    {
        public int ID { set; get; }
        public string Nama { set; get; }
        
        public int IDDInas { set; get; }
        public long IDKegiatan { set; get; }

        public int SessionLow { set; get; }
        public int SessionUp { set; get; }

    }
}
