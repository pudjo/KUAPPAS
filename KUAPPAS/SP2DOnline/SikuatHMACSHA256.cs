using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SP2DOnline
{
    public class SikuatHMACSHA256
    {
        private string m_ClientId;
        private string m_ClientSecret;
        private const string sURL = "https://dev-sp2d.bankkalbar.co.id/";

//        UserAPI SP2DO Ketapang

//ClientId : ClientSP2DKetapang
//ClientSecret : lkaurLrcmDXrqRJzAOxAKQWuvAIYGG


//Email : ketapang@bankkalbar.co.id
//Nomor Referensi : 1016* (12 Digit)
//NomorHandphoneOTP : 081211619471

        public string ClientID{
            set{m_ClientId= value; }
            get {return m_ClientId;}
        }
        public string ClientSecret{
            set{m_ClientSecret= value; }
            get {return m_ClientSecret;}
        }

    


    }
}
