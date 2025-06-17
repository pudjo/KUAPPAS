using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Periode
    {
        public DateTime TanggalAwal { set; get; }
        public DateTime TanggalAkhir { set; get; }



        //public int  Triwulan
        //{
        //    set;
            

        //}
        public DateTime TanggalAwalTahun
        {
            //set;
            get
            {

                return new DateTime(TanggalAkhir.Year, 1, 1);

            }
        }
        public DateTime DayBeforeTanggalAwal
        {
            //set;
            get
            {
                return new DateTime(TanggalAkhir.Year, 1, 1);

            }
        }
        public Periode(DateTime dAwal, DateTime dAkhir)
        {
            TanggalAwal = dAwal;
            TanggalAkhir = dAkhir;
        }

    }
}
