using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class LaporanBendahara
    {
        public int No { set; get; }
        public int NoBKU { set; get; }
        public string Uraian { set; get; }
        public string Penerimaan { set; get; }
        public string Pengeluaran{ set; get; }
        public string Saldo { set; get; }
        public string Tanggal { set; get; }
        public string UPGU { set; get; }
        public string LS { set; get; }
        public string TU { set; get; }
        public string NoBukti { set; get; }
        public decimal Jumlah { set; get; }
        public int Debet { set; get; }

    }

    public class RegisterSP2D
    {
        public int No { set; get; }
        public string NoSPP { set; get; }
        public string NoSPM { set; get; }
        public string NoSP2D { set; get; }

        public string TanggalSPP { set; get; }
        public string TanggalSPM { set; get; }
        public string TanggalSP2D { set; get; }
        public string Uraian { set; get; }
        public string Tanggal { set; get; }
        public string UPGU { set; get; }
        public string LS { set; get; }
        public string LSGaji { set; get; }
        public string TU { set; get; }
        public string Jumlah { set; get; }

    }
}
