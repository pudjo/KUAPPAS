using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class RPJMDProfileRPJMD
    {
        public int Periode { set; get; }
        public string NamaProfile { set; get; }

        public int TahunAwal { set; get; }
        public int TahunAkhir { set; get; }
        public string NamaPemda { set; get; }
        public int Status { set; get; }
        public string NomorPerda { set; get; }
        public DateTime TanggalPerda { set; get; }
        public string Keterangan { set; get; }
        public string Visi { set; get; }


    }
}
