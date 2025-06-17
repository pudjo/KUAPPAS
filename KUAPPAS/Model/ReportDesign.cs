using Formatting;
using System;


namespace DTO
{
    public class ReportDesign
    {
        public int Spasi { set; get; }
        public string FontName { set; get; }
        public string TanggalCetak { set; get; }
        public string NamaReport { set; get; }
        public string NomorReport { set; get; }
        public bool Portrait{ set; get; }
        public string Judul1{ set; get; }
        public string Judul2 { set; get; }
        public string Judul3 { set; get; }


        public ReportDesign()
        {
            Spasi = 4;
            FontName = "Arial";
            TanggalCetak = DateTime.Now.Date.ToTanggalIndonesiaLengkap();
            NamaReport= "";
            NomorReport = "";
            Portrait = true;
        Judul1 = "";
        Judul2 = "";
        Judul3 = "";


    }

}
}
