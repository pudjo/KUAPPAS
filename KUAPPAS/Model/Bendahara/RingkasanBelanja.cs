using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class RingkasanBelanja
    {
        public decimal JumlahGU { set; get; }
        public decimal JumlahTU { set; get; }
        public decimal JumlahLS { set; get; }
        public decimal JumlahGaji { set; get; }
        public decimal JumlahPPKD { set; get; }
        public decimal Jumlah
        {
            get
            {
                return JumlahGU +
                JumlahTU +
                JumlahLS +
                JumlahGaji +
                JumlahPPKD ;
            }
        }
        public RingkasanBelanja()
        {
            JumlahGU =0;
            JumlahTU =0;
            JumlahLS =0;
            JumlahGaji =0;
            JumlahPPKD =0;
        }
    }
}
