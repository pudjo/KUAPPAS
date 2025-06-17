using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
namespace DTO.Bendahara
{
    public class ParameterLaporanBKU
    {
        public double Height { set; get; }
        public double Width { set; get; }
        public SKPD  Skpd { set; get; }
        public int PPKD { set; get; }
        public int JenisBendahara { set; get; }
        public Pemda PEMDA { set; get; }
        public double FontSize { set; get; }
        
        //public string Periode { set; get; }
        public string NamaLaporan { set; get; }
        public int Tahun { set; get; }

        public decimal  SaldoAwal { set; get; }
        public string PeriodeIni { set; get; }
        public string PeriodeLalu { set; get; }
        public decimal SaldoAkhir{ set; get; }

        public decimal GUPeriodeIni { set; get; }
        public decimal GUPeriodeLalu { set; get; }

        public decimal UPPeriodeIni { set; get; }
        public decimal UPPeriodeLalu { set; get; }



        public decimal TUPeriodeIni { set; get; }
        public decimal TUPeriodeLalu { set; get; }

        public decimal GJPeriodeIni { set; get; }
        public decimal GJPeriodeLalu { set; get; }

        public decimal LSPeriodeIni { set; get; }
        public decimal LSPeriodeLalu { set; get; }

        public decimal PPKDPeriodeIni { set; get; }
        public decimal PPKDPeriodeLalu { set; get; }

        public decimal SaldoBank { set; get; }
        public decimal SaldoTunai { set; get; }
        public decimal PenerimaanKini { set; get; }
        public decimal Penerimaanlalu { set; get; }
        public decimal PengeluaranKini { set; get; }
        public decimal PengeluaranLalu { set; get; }
        public decimal Penerimaan { set; get; }
        public decimal Pengeluaran { set; get; }
        public Periode periode { set; get; }

    }
}
 