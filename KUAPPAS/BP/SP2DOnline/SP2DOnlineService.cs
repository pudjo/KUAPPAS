using DTO.SP2DOnLine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KUAPPAS.BP.SP2DOnline
{
    public class SP2DOnlineService
    {
        public DataInquiriyRekeningResponEx CekRekening(InquiryRekeningRequest requestInquiry)
        {
          
            string url = "";//
            DataInquiriyRekeningResponEx resp = new DataInquiriyRekeningResponEx();
            url = GlobalVar.BANK_URL + "InquiryRekening";
            WebResponse objResponse = null;
            WebRequest request = null;
            try
            {
                request = WebRequest.Create(url);
                request.Method = "POST";
                //  url = url + "?sandiBank=" + ctrlDaftarBank1.Kode.Trim() + "&nomorRekening=" + txtNoRekening.Text.Replace(".", "");
                string JsonData = JsonConvert.SerializeObject(requestInquiry);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";
                //request.Headers.Set("client_secret", "");
                request.ContentLength = byteArray.Length;
                request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();


                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                resp = JsonConvert.DeserializeObject<DataInquiriyRekeningResponEx>(responseData);
                return resp;

            }
            catch (Exception ex)
            {
                resp.error_kode = "99";
                resp.message = ex.Message;
                return resp;
            }
        }
    }
}
