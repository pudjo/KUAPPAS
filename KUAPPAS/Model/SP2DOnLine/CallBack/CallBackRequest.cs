using DTO.SP2DOnLine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine.CallBack
{
    public class CallBackRequest
    {
        // public string sandiBank { set; get; } = string.Empty;
        public string nomorSP2D { set; get; } 
        public string nomorSPM { set; get; } 
        public string tanggalTransaksi { set; get; } 
        public string referenceNo { set; get; } 
        public string notes { set; get; } 
        public string responseCode { set; get; } 
        public string messageDetail { set; get; } 
        public DataPengirim pengirim { set; get; }
        public DataPenerima penerima { set; get; }
        public List<DetailPotonganMpn> detailPotonganMpn { set; get; }
        public List<DetailPotonganNonMpn> detailPotonganNonMpn { set; get; }

        public CallBackRequest()
        {
                   
            nomorSP2D ="";
            nomorSPM ="";
            tanggalTransaksi ="";
            referenceNo ="";
            notes ="";
            responseCode ="";
            messageDetail = "";
            pengirim =new DataPengirim ();
             penerima =new DataPenerima();
            detailPotonganMpn =new List<DetailPotonganMpn> ();
            detailPotonganNonMpn =new List<DetailPotonganNonMpn> ();
        }
    }
}
