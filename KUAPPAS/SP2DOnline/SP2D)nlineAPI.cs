using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace SP2DOnline
{
    public class SP2DOnlineAPI
    {
        private string m_URL { set; get; }
        private string clientid { set; get; }

        //Nomor Referensi : 1016* (12 Digit)

        private const string FUNCTION_URL_INQUIRY = "api/sp2d/inquiryRekening";



        public string BuildInquirySignature(InquiriesRequest inq, string sTime)
        {
            // Serialize the header and claimSet
            string Secret = "lkaurLrcmDXrqRJzAOxAKQWuvAIYGG";

            string sInquiry = JsonConvert.SerializeObject(inq);
            sInquiry = sInquiry.Replace(" ", string.Empty).ToLower();

            string message = sInquiry + "|" + sTime;


            byte[] key = Encoding.ASCII.GetBytes(Secret);
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(message);
            MemoryStream stream = new MemoryStream(byteArray);
            string result = myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            Console.WriteLine(result);
            return result;
        }

        private  string BuildInquiryNPWPSignature(InquiriesNPWPRequest inq, string sTime)
        {
            // Serialize the header and claimSet
            string Secret = "lkaurLrcmDXrqRJzAOxAKQWuvAIYGG";

            string sInquiry = JsonConvert.SerializeObject(inq);
            sInquiry = sInquiry.Replace(" ", string.Empty).ToLower();

            string message = sInquiry + "|" + sTime;


            byte[] key = Encoding.ASCII.GetBytes(Secret);
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(message);
            MemoryStream stream = new MemoryStream(byteArray);
            string result = myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            Console.WriteLine(result);
            return result;
        }

        private string BuildInquiryIdBillingSignature(InquiriesIdBillingRequest inq, string sTime)
        {
            // Serialize the header and claimSet
            string Secret = "lkaurLrcmDXrqRJzAOxAKQWuvAIYGG";

            string sInquiry = JsonConvert.SerializeObject(inq);
            sInquiry = sInquiry.Replace(" ", string.Empty).ToLower();

            string message = sInquiry + "|" + sTime;


            byte[] key = Encoding.ASCII.GetBytes(Secret);
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(message);
            MemoryStream stream = new MemoryStream(byteArray);
            string result = myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            Console.WriteLine(result);
            return result;
        }
        private string BuildCreateIdBillingSignature(CreateIdBillingRequest inq, string sTime)
        {
            // Serialize the header and claimSet
            string Secret = "lkaurLrcmDXrqRJzAOxAKQWuvAIYGG";

            string sInquiry = JsonConvert.SerializeObject(inq);
            sInquiry = sInquiry.Replace(" ", string.Empty).ToLower();

            string message = sInquiry + "|" + sTime;


            byte[] key = Encoding.ASCII.GetBytes(Secret);
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(message);
            MemoryStream stream = new MemoryStream(byteArray);
            string result = myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            Console.WriteLine(result);
            return result;
        }

        public string BuildInquiryMPNSignature(InquiryMPN inq, string sTime)
        {
            // Serialize the header and claimSet
            string Secret = "lkaurLrcmDXrqRJzAOxAKQWuvAIYGG";
  
            string sInquiry = JsonConvert.SerializeObject(inq);
            sInquiry = sInquiry.Replace(" ", string.Empty).ToLower();

            string message = sInquiry + "|" + sTime;


            byte[] key = Encoding.ASCII.GetBytes(Secret);
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(message);
            MemoryStream stream = new MemoryStream(byteArray);
            string result = myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            Console.WriteLine(result);
            return result;

        }

        public string BuildGenerateRepprtSignature(GenerateReportRequest inq, string sTime)
        {
            // Serialize the header and claimSet
            string Secret = "lkaurLrcmDXrqRJzAOxAKQWuvAIYGG";

            string sInquiry = JsonConvert.SerializeObject(inq);
            sInquiry = sInquiry.Replace(" ", string.Empty).ToLower();

            string message = sInquiry + "|" + sTime;


            byte[] key = Encoding.ASCII.GetBytes(Secret);
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(message);
            MemoryStream stream = new MemoryStream(byteArray);
            string result = myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            //Console.WriteLine(result);
            return result;

        }




        public InquiriesRespond InQuiry(string idbank, string noRekening)
        {

            //string url = GlobalVar.BANK_URL + "Api/v1/login";

            string url = "https://dev-sp2d.bankkalbar.co.id/api/sp2d/inquiryRekening";// +txtAccountNo.Text.Trim();// GlobalVar.BANK_URL + "Api/vxx1/loginx";


            InquiriesRequest inquiry = new InquiriesRequest();
          
            inquiry.sandiBank = idbank;
            inquiry.nomorRekening = noRekening;
            WebResponse objResponse;
            HttpWebRequest request = null;


            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                //request.Headers.Add("Accept", "application/json");

                request.Accept = "application/json";

                request.Headers.Add("x-bankkalbar-clientid", "ClientSP2DKetapang");
                request.Headers.Add("x-bankkalbar-Timestamp", time);
                string ssignature = BuildInquirySignature(inquiry, time);
                request.Headers.Add("x-bankkalbar-Signature", ssignature);


                string JsonData = JsonConvert.SerializeObject(inquiry);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";

                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();



                objResponse = (HttpWebResponse)request.GetResponse();

                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                InquiriesRespond resp = JsonConvert.DeserializeObject<InquiriesRespond>(responseData);
                return resp;
 
                
            }
            catch (WebException wex)
            {
                InquiriesRespond respError = new InquiriesRespond();
               
                using (WebResponse response = wex.Response)
                {
                    // TODO: Handle response being null
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    respError.response_code = httpResponse.StatusCode.ToString();
                    respError.message = "";
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        respError.message = respError.message + text;

                    }
                }
                return respError;

            }
        }


        public InquiriesNPWPRespond InQuiryNPWP(string NPWP ,string kodemap, string kodesetor)
        {

            //string url = GlobalVar.BANK_URL + "Api/v1/login";

            string url = "https://dev-sp2d.bankkalbar.co.id/api/sp2d/inquiryNPWP";



            InquiriesNPWPRequest inquiry = new InquiriesNPWPRequest();

            inquiry.kodeMap= kodemap;
            inquiry.kodeSetor  = kodesetor ;
            inquiry.nomorPokokWajibPajak = NPWP;

            WebResponse objResponse;
            HttpWebRequest request = null;


            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                //request.Headers.Add("Accept", "application/json");

                request.Accept = "application/json";

                request.Headers.Add("x-bankkalbar-clientid", "ClientSP2DKetapang");
                request.Headers.Add("x-bankkalbar-Timestamp", time);
                string ssignature = BuildInquiryNPWPSignature(inquiry, time);

                request.Headers.Add("x-bankkalbar-Signature", ssignature);


                string JsonData = JsonConvert.SerializeObject(inquiry);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";
                
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();



                objResponse = (HttpWebResponse)request.GetResponse();

                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                InquiriesNPWPRespond resp = JsonConvert.DeserializeObject<InquiriesNPWPRespond>(responseData);
                return resp;


            }
            catch (WebException wex)
            {
                InquiriesNPWPRespond respError = new InquiriesNPWPRespond();

                using (WebResponse response = wex.Response)
                {
                    // TODO: Handle response being null
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    respError.response_code = httpResponse.StatusCode.ToString();
                    respError.message = "";
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        respError.message = respError.message + text;

                    }
                }
                return respError;

            }
        }

        public InquiriesIdBillingRespon InQuiryIdBilling(string idbilling)
        {

            //string url = GlobalVar.BANK_URL + "Api/v1/login";

            string url = "https://dev-sp2d.bankkalbar.co.id/api/sp2d/inquiryIDBilling";



            InquiriesIdBillingRequest inquiry = new InquiriesIdBillingRequest();

            inquiry.idBilling = idbilling;

            WebResponse objResponse;
            HttpWebRequest request = null;


            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                //request.Headers.Add("Accept", "application/json");

                request.Accept = "application/json";

                request.Headers.Add("x-bankkalbar-clientid", "ClientSP2DKetapang");
                request.Headers.Add("x-bankkalbar-Timestamp", time);
                string ssignature = BuildInquiryIdBillingSignature(inquiry, time);

                request.Headers.Add("x-bankkalbar-Signature", ssignature);


                string JsonData = JsonConvert.SerializeObject(inquiry);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";

                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();



                objResponse = (HttpWebResponse)request.GetResponse();

                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                InquiriesIdBillingRespon resp = JsonConvert.DeserializeObject<InquiriesIdBillingRespon>(responseData);
                return resp;


            }
            catch (WebException wex)
            {
                InquiriesIdBillingRespon respError = new InquiriesIdBillingRespon();

                using (WebResponse response = wex.Response)
                {
                    // TODO: Handle response being null
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    respError.response_code = httpResponse.StatusCode.ToString();
                    respError.message = "";
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        respError.message = respError.message + text;

                    }
                }
                return respError;

            }
        }
        public CreateIdBillingRespon  CreateIdBilling(CreateIdBillingRequest ocreatebilling)
        {

         
            string url = "https://dev-sp2d.bankkalbar.co.id/api/sp2d/createBilling";


            WebResponse objResponse;
            HttpWebRequest request = null;


            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                //request.Headers.Add("Accept", "application/json");

                request.Accept = "application/json";

                request.Headers.Add("x-bankkalbar-clientid", "ClientSP2DKetapang");
                request.Headers.Add("x-bankkalbar-Timestamp", time);
                string ssignature = BuildCreateIdBillingSignature(ocreatebilling, time);

                request.Headers.Add("x-bankkalbar-Signature", ssignature);


                string JsonData = JsonConvert.SerializeObject(ocreatebilling);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";

                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();



                objResponse = (HttpWebResponse)request.GetResponse();

                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                CreateIdBillingRespon resp = JsonConvert.DeserializeObject<CreateIdBillingRespon>(responseData);
                return resp;


            }
            catch (WebException wex)
            {
                CreateIdBillingRespon respError = new CreateIdBillingRespon();

                using (WebResponse response = wex.Response)
                {
                    // TODO: Handle response being null
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    respError.response_code = httpResponse.StatusCode.ToString();
                    respError.message = "";
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        respError.message = respError.message + text;

                    }
                }
                return respError;

            }
        }


        public InquiryMPNespon InQuirMPN(InquiryMPN inquirtMPN)
        {


         
            string url = "https://dev-sp2d.bankkalbar.co.id/api/sp2d/inquiryPaymentMPN";
          
            inquirtMPN.referenceNo = "101612345678";

            WebResponse objResponse;
            HttpWebRequest request = null;


            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                //request.Headers.Add("Accept", "application/json");

                request.Accept = "application/json";

                request.Headers.Add("x-bankkalbar-clientid", "ClientSP2DKetapang");
                request.Headers.Add("x-bankkalbar-Timestamp", time);
                string ssignature = BuildInquiryMPNSignature(inquirtMPN, time);

                request.Headers.Add("x-bankkalbar-Signature", ssignature);


                string JsonData = JsonConvert.SerializeObject(inquirtMPN);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";

                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();



                objResponse = (HttpWebResponse)request.GetResponse();

                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                InquiryMPNespon resp = JsonConvert.DeserializeObject<InquiryMPNespon>(responseData);
                return resp;


            }
            catch (WebException wex)
            {
                InquiryMPNespon respError = new InquiryMPNespon();

                using (WebResponse response = wex.Response)
                {
                    // TODO: Handle response being null
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    respError.response_code = httpResponse.StatusCode.ToString();
                    respError.message = "";
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        respError.message = respError.message + text;

                    }
                }
                return respError;

            }
        }


         //SP2DOnlineAPI oAPI = new SP2DOnlineAPI();
         //   InquiryMPN inquirtMPN = new InquiryMPN();
         //   inquirtMPN.idBilling = txtidBilling.Text;
         //   inquirtMPN.reInquiry = "false";
         //   inquirtMPN.referenceNo = "";
         //   oAPI.InQuirMPN( SP2DOnlineAPI inquirtMPN);

        public GenerateReportRespon GenerateReport (GenerateReportRequest requestGenerateReport , string time ){

            string url = "https://dev-sp2d.bankkalbar.co.id/api/sp2d/generateReport";

            requestGenerateReport.noReferensi = "101612345678";

            WebResponse objResponse;
            HttpWebRequest request = null;


            try
            {
                //string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                //request.Headers.Add("Accept", "application/json");

                request.Accept = "application/json";

                request.Headers.Add("x-bankkalbar-clientid", "ClientSP2DKetapang");
                request.Headers.Add("x-bankkalbar-Timestamp", time);
                string ssignature = BuildGenerateRepprtSignature(requestGenerateReport, time);

                request.Headers.Add("x-bankkalbar-Signature", ssignature);


                string JsonData = JsonConvert.SerializeObject(requestGenerateReport);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";

                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();



                objResponse = (HttpWebResponse)request.GetResponse();

                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                GenerateReportRespon resp = JsonConvert.DeserializeObject<GenerateReportRespon>(responseData);
                return resp;


            }
            catch (WebException wex)
            {
                GenerateReportRespon respError = new GenerateReportRespon();

                using (WebResponse response = wex.Response)
                {
                    // TODO: Handle response being null
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    respError.response_code = httpResponse.StatusCode.ToString();
                    respError.message = "";
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        respError.message = respError.message + text;

                    }
                }
                return respError;

            }
        }
    }
}
