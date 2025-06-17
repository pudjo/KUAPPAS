using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class ProgramMusrenmbang
    {
        public int ID { set; get; }
        public int Tahun { set; get; }
        public int IDDInas { set; get; }

        public int IDProgram { set; get; }
        public string NamaProgram { set; get; }
        public int IDUrusan { set; get; }

    }
}
