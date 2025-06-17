using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class PenggunaDinas
    {
        public int UserID { set; get; }
        public int SKPD { set; get; }
        public int Unit { set; get; }
        public int PPKD { set; get; }
        public int SubUnit { set; get; }
    }
    public class PenggunaGroup
    {
        public int UserID { set; get; }
        public int Group { set; get; }

    }
}
