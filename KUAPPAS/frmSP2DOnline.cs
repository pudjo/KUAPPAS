using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KUAPPAS.Bendahara;
using DTO;
using BP;
using BP.Bendahara;
using DTO.Bendahara;
using KUAPPAS.SP2DOnline;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using Formatting;
using System.Security.Cryptography;
using SP2DOnline;
using DTO.SP2DOnLine.DetailTransaksiSP2DOnline57;
using DTO.SP2DOnLine;
//using DTO.SP2DOnLine._511;
//using DTO.SP2DOnLine._511;

//https://bsre.bssn.go.id/index.php/tte/

namespace KUAPPAS
{
    public partial class frmSP2DOnline : ChildForm
    {

   

        DataGridViewCellStyle _merah = new DataGridViewCellStyle();
        DataGridViewCellStyle _pink = new DataGridViewCellStyle();
        DataGridViewCellStyle _ijo = new DataGridViewCellStyle();

        DataGridViewCellStyle _ditolakStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _didiskusikanStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _diTerimaStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _diTangguhkanStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _baruStyle = new DataGridViewCellStyle();
        private readonly Random _random = new Random();

        List<SPP> m_lstspp;
        private string m_sBearer;
        private int _InternalCounter ;
        private int m_inoref;

        public frmSP2DOnline()
        {
            InitializeComponent();
            m_lstspp = new List<SPP>();
            m_sBearer = "";

        }

        public static void SignFile(byte[] key, String sourceFile, String destFile)
        {
            // Initialize the keyed hash object.
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                using (FileStream inStream = new FileStream(sourceFile, FileMode.Open))
                {
                    using (FileStream outStream = new FileStream(destFile, FileMode.Create))
                    {
                        // Compute the hash of the input file.
                        byte[] hashValue = hmac.ComputeHash(inStream);
                        // Reset inStream to the beginning of the file.
                        inStream.Position = 0;
                        // Write the computed hash value to the output file.
                        outStream.Write(hashValue, 0, hashValue.Length);
                        // Copy the contents of the sourceFile to the destFile.
                        int bytesRead;
                        // read 1K at a time
                        byte[] buffer = new byte[1024];
                        do
                        {
                            // Read from the wrapping CryptoStream.
                            bytesRead = inStream.Read(buffer, 0, 1024);
                            outStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }
                }
            }
            return;
        } // end SignFile

        // Compares the key in the source file with a new key created for the data portion of the file. If the keys
        // compare the data has not been tampered with.
        public static bool VerifyFile(byte[] key, String sourceFile)
        {
            bool err = false;
            // Initialize the keyed hash object.
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                // Create an array to hold the keyed hash value read from the file.
                byte[] storedHash = new byte[hmac.HashSize / 8];
                // Create a FileStream for the source file.
                using (FileStream inStream = new FileStream(sourceFile, FileMode.Open))
                {
                    // Read in the storedHash.
                    inStream.Read(storedHash, 0, storedHash.Length);
                    // Compute the hash of the remaining contents of the file.
                    // The stream is properly positioned at the beginning of the content,
                    // immediately after the stored hash value.
                    byte[] computedHash = hmac.ComputeHash(inStream);
                    // compare the computed hash with the stored value

                    for (int i = 0; i < storedHash.Length; i++)
                    {
                        if (computedHash[i] != storedHash[i])
                        {
                            err = true;
                        }
                    }
                }
            }
            if (err)
            {
                Console.WriteLine("Hash values differ! Signed file has been tampered with!");
                return false;
            }
            else
            {
                Console.WriteLine("Hash values agree -- no tampering occurred.");
                return true;
            }
        } //end VerifyFile

        private void cmdTampilkan_Click(object sender, EventArgs e)
        {
            SPPLogic ologic = new SPPLogic(GlobalVar.TahunAnggaran);
            ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);
            p.TanggalAwal = ctrlTanggal1.Tanggal ;
            p.TanggalAkhir = ctrlTanggal2.Tanggal;
            p.NoSP2D = txtNoSP2D.Text;
            if (optBelumCair.Checked == true)
            {
                p.Status = 3;
            }
            if (optCair.Checked == true)
            {
                p.Status = 4;
            }
            if (optPending.Checked == true)
            {
                p.Status = 7;
            }
            p.IDDInas = chkSemuaDinas.Checked == true ? 0 : ctrlSKPD1.GetID();
            m_lstspp = new List<SPP>();
            p.Jenis = -1;
            m_lstspp = ologic.GetSP2DOnline(p);

            if (ologic.IsError() == true)
            {
                MessageBox.Show(ologic.LastError());
                return;
            }
            int iRow = 0;

            gridSP2D.Rows.Clear();
 
            foreach (SPP spp in m_lstspp)
            {

                //jumlahdibayar = 0;
                //jumlahdibayar = spp.Jumlah - spp.JumlahPotongan;

                string[] row = { 
                                   spp.NoUrut.ToString(), //0
                                   "Detail", //1
                                   "false", //2
                                   spp.NoSP2D, //3
                                   spp.NoSPM,//4
                                   spp.NamaPenerima, //5
                                   spp.KodeBank,    //6
                                   spp.NamaBank,  //7                                 
                                   
                                   spp.NoRek, //8
                                   spp.NoNPWP, //9 

                                   spp.Jumlah.ToString("###"),//10 
                                   spp.JumlahPotonganMPN.ToString("###"),//11
                                   spp.JumlahPotonganNonMPN.ToString("###"),//12
                                   spp.JumlahBayar.ToString("###"), //13
                                   spp.Jumlah.ToRupiahInReport(),//14 
                                   spp.JumlahPotonganMPN.ToRupiahInReport(),//15
                                   spp.JumlahPotonganNonMPN.ToRupiahInReport(),//16
                                   spp.JumlahBayar.ToRupiahInReport(), //17
                                   spp.IDDInas.ToString(),//18
                                   spp.NamaDinas,//19
                                   spp.NoReferensiBankOnline.ToString(),//20
                                   
                                   spp.statusOnline,//21
                                   "SKN",
                                   //spp.JenisTransfer.Trim(),//22
                                 };


              


                gridSP2D.Rows.Add(row);
                if (spp.Status == 1)
                    gridSP2D.Rows[iRow ].DefaultCellStyle.BackColor = Color.AliceBlue;
                if (spp.Status == 2)
                    gridSP2D.Rows[iRow ].DefaultCellStyle.BackColor = Color.LightBlue;

                if (spp.Status == 3)
                    gridSP2D.Rows[iRow ].DefaultCellStyle.BackColor = Color.AntiqueWhite;// Red;
                if (spp.Status == 4)
                {
                   
                    gridSP2D.Rows[iRow ].DefaultCellStyle.BackColor = Color.LightPink;// PaleVioletRed;// IndianRed;// Red;
                    
                }
                if (spp.Status == 10)
                    gridSP2D.Rows[iRow ].DefaultCellStyle.BackColor = Color.DeepPink;
                if (spp.Status == 6)
                    gridSP2D.Rows[iRow ].DefaultCellStyle.BackColor = Color.DodgerBlue;
                        



                DataGridViewButtonColumn c = (DataGridViewButtonColumn)gridSP2D.Columns[1];
                c.FlatStyle = FlatStyle.Popup;
                c.DefaultCellStyle.ForeColor = Color.Navy;
                c.DefaultCellStyle.BackColor = Color.Bisque ;

                 

                iRow++;


            }


        }
        private int GetNoRef()
        {
            SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);
            return oSPPLogic.GetNoPeguji();

        }
        private void froSP2DOnline_Load(object sender, EventArgs e)
        {
            //panel1.Dock = FillErrorEventArgs;
            gridSP2D.FormatHeader();
            this.WindowState = FormWindowState.Maximized;
            ctrlHeader1.SetCaption("Pembayaran SP2D melalui SP2D Online");
            this.Text = "SP2D Online";

            ctrlTanggal1.Tanggal = DateTime.Now ;
            ctrlTanggal2.Tanggal = DateTime.Now ;
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            ctrlBank1.Create();
            m_inoref = GetNoRef();
            ctrlPencarian1.setGrid(ref gridSP2D);
      

        }

        //private string Encrypt(string str)
        //{
        //    string s = DESEncrypt.Encrypt(str, GlobalVar.TRIPPLEDES_KEY);
        //    return s;



        //}
        //private string Decrypt(string str)
        //{
        //    string s = DESEncrypt.Decrypt(str, GlobalVar.TRIPPLEDES_KEY);
        //    return s;
        //}
        private void cmdLogin_Click(object sender, EventArgs e)
        {

        }
        /*
         * 
         * 
         * var myUri = new Uri(fullpath);
var myWebRequest = WebRequest.Create(myUri);
var myHttpWebRequest = (HttpWebRequest)myWebRequest;
myHttpWebRequest.PreAuthenticate = true;
myHttpWebRequest.Headers.Add("Authorization", "Bearer " + AccessToken);
myHttpWebRequest.Accept = "application/json";

var myWebResponse = myWebRequest.GetResponse();
var responseStream = myWebResponse.GetResponseStream();
if (responseStream == null) return null;

var myStreamReader = new StreamReader(responseStream, Encoding.Default);
var json = myStreamReader.ReadToEnd();

responseStream.Close();
myWebRespons
         * */
        private string StatasReq(int kode)
        {

            string description = "";
            switch (kode)
            {
                case 202:
                    description = "Accepted: The request has been accepted for further processing.";
                    break;
                case 400:
                    description = "Bad Request: The server could not understand the request. This error is also used for any errors not classified by the HttpStatusCode enumeration";
                    break;

                case 100:
                    description = "Continue: 	The client can continue with the request.";
                    break;
                case 403:
                    description = "Forbidden: The server has refused to respond to the request.";
                    break;
                case 504:
                    description = "GatewayTimeout: An intermediate proxy server timed out while waiting for a response.";
                    break;
                case 500:
                    description = "InternalServerError: A generic error occurred on the server while responding to the request.";
                    break;

                case 404:
                    description = "NotFound: An intermediate proxy server timed out while waiting for a response.";
                    break;
                case 200:
                    description = "OK: The request was successful and the requested information was sent.";
                    break;
                case 407:
                    description = "ProxyAuthenticationRequired: The proxy server requires an authentication header.";
                    break;
                case 408:
                    description = "RequestTimeout: The client did not send the request in the time expected by the server..";
                    break;
                case 503:
                    description = "ServiceUnavailable: The server cannot handle the request due to high load or because it is down.";
                    break;
                case 401:
                    description = "Unauthorized: he requested resource requires authentication to access.";
                    break;
            }
            return description;
        }


        private string GetExceptionstatus(WebException st)
        {

            string description = "";
            switch (st.Status)
            {
                case WebExceptionStatus.ConnectFailure:
                    description = "An error occurred at the transport (e.g., TCP) level.";
                    break;

                case WebExceptionStatus.ConnectionClosed:

                    description = "The connection was unexpectedly closed.";
                    break;

                case WebExceptionStatus.Pending:

                    description = "The asynchronous request is pending.";
                    break;

                case WebExceptionStatus.ProtocolError:

                    description = "A response was received but a protocol-level error occurred (see Table 10-4 for HTTP protocol-level errors).";
                    break;

                case WebExceptionStatus.ReceiveFailure:

                    description = "An error occurred receiving the response.";
                    break;

                case WebExceptionStatus.RequestCanceled:

                    description = "The request was canceled by the Abort() method. This error is used for any error not classified by the WebExceptionStatus enumeration.";
                    break;

                case WebExceptionStatus.SendFailure:

                    description = "An error occurred sending the request to the server.";
                    break;

                case WebExceptionStatus.Success:

                    description = "The operation completed successfully.";
                    break;

                case WebExceptionStatus.Timeout:

                    description = "No response was received during the specified timeout period.";
                    break;

                case WebExceptionStatus.TrustFailure:

                    description = "The server certificate could not be validated.";
                    break;

            }
            return description;
        }


        private void BIC()
        {

            string url = GlobalVar.BANK_URL;
            WebResponse objResponse;




            TrxLogin tlogin = new TrxLogin();
            tlogin.username = "";
            tlogin.password = "";

            MemoryStream memmoryStream = new MemoryStream();
            BinaryFormatter binayformator = new BinaryFormatter();
            binayformator.Serialize(memmoryStream, tlogin);


            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            request.ContentLength = memmoryStream.Length;


            Stream reqStream = request.GetRequestStream();
            //Write the memory stream data into stream object before send it.
            byte[] buffer = new byte[memmoryStream.Length];
            int count = memmoryStream.Read(buffer, 0, buffer.Length);
            reqStream.Write(buffer, 0, buffer.Length);

            // Create a request stream which holds request data
            //Stream reqStream = request.GetRequestStream();

            try
            {

                objResponse = request.GetResponse();
                //Get a stream from the response.
                //string[] s = objResponse.Headers.GetValues(0); 
                Stream streamdata = objResponse.GetResponseStream();
                //read the response using streamreader class as stream is read by reader class.
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();

                List<string> results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(responseData);

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {

                    objResponse = (HttpWebResponse)ex.Response;
                    // net.5 
                    //Console.WriteLine("HTTP Response Code: {0}", objResponse.StStatusCode.ToString());
                    Console.WriteLine("HTTP Response Code: {0}", objResponse.Headers.GetValues("Response Code"));//.ToString());
                    objResponse.Close();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //frmSP2DOnlineTestEncrypt fTest = new frmSP2DOnlineTestEncrypt();
            //fTest.Show();

        }

        private void cmdCheckSaldo_Click(object sender, EventArgs e)
        {


            string url = "";//



            url = "http://dev-api.bankkalbar.co.id:8898/api/v1/balances";
            WebResponse objResponse;
            WebRequest request = null;
            try
            {
                request = WebRequest.Create(url);
                request.Method = "GET";

                request.ContentType = "application/Json";
                request.Headers["Authorization"] = m_sBearer;
                // System.IO.Stream dataStream = request.GetRequestStream();
                //dataStream.Close();
                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                Balances resp = JsonConvert.DeserializeObject<Balances>(responseData);
                if (resp.success == false)
                {
                    MessageBox.Show(resp.message);
                    txtBalances.Text = "0";

                }
                else
                {

                    //for (int i = 0; i < objResponse.Headers.Count; ++i)
                    //    Console.WriteLine("\nHeader Name:{0}, Header value :{1}", objResponse.Headers.Keys[i], objResponse.Headers[i]);

                    txtBalances.Text = resp.balance;

                    //string Bearer = objResponse.Headers["Authorization"];
                    //     objResponse.Headers()
                }


            }






            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {

                    objResponse = (HttpWebResponse)ex.Response;
                    // net.5 
                    //Console.WriteLine("HTTP Response Code: {0}", objResponse.StStatusCode.ToString());
                    Console.WriteLine("HTTP Response Code: {0}", objResponse.Headers.GetValues("Response Code"));//.ToString());


                    objResponse.Close();

                }



            }
        }

        private void cmdDaftarank_Click(object sender, EventArgs e)
        {
            


        }
        private List<Kodebic>  GetBic()
        {
            string url = "";

            url = "http://dev-api.bankkalbar.co.id:8898/api/v1/banks/bic";
            WebResponse objResponse;
            WebRequest request = null;
            try
            {
                request = WebRequest.Create(url);
                request.Method = "GET";

                request.ContentType = "application/Json";
                request.Headers["Authorization"] = m_sBearer;
                // System.IO.Stream dataStream = request.GetRequestStream();
                //dataStream.Close();
                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                HttpWebResponse hr = (HttpWebResponse)objResponse;
                string responseData = strReader.ReadToEnd();
                if (hr.StatusCode != HttpStatusCode.OK)
                {
                    loginresponse resp = JsonConvert.DeserializeObject<loginresponse>(responseData);

                    MessageBox.Show("get bic " + resp.message);
                    return null;

                }
                else
                {
                    List<Kodebic> lstBank = JsonConvert.DeserializeObject<List<Kodebic>>(responseData);
                    return lstBank;

                    //froSP2DOnlineDaftarBank fDaftarBank = new froSP2DOnlineDaftarBank();
                    

               }

            }
            catch (WebException wex)
            {
                MessageBox.Show(wex.Message);
                return null;

            }



        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //string responseData = "[{\"accountNumber\":\"519000123990\",\"bic\":\"LPEIIDJ1\",\"bankCode\":\"186\",\"participantName\":\"INDONESIA EXIMBANK\"},{\"accountNumber\":\"520002000990\",\"bic\":\"BRINIDJA\",\"bankCode\":\"002\",\"participantName\":\"PT. BANK RAKYAT INDONESIA (PERSERO), TBK.\"},{\"accountNumber\":\"520008000990\",\"bic\":\"BMRIIDJA\",\"bankCode\":\"008\",\"participantName\":\"PT. BANK MANDIRI (PERSERO), TBKA\"}]";
            //List<Banks> lstBank = JsonConvert.DeserializeObject<List<Banks>>(responseData);
                  


        }

        private void cmdInquiy_Click(object sender, EventArgs e)
        {

            SP2DOnlineAPI oAPI = new SP2DOnlineAPI();

            InquiriesRespond resp = oAPI.InQuiry(txtIDBank.Text, txtAccountNo.Text);

            if (resp.response_code != "00")
            {
                MessageBox.Show(resp.message);



            }
            else
            {

                txtNama.Text  = resp.data.namaPemilikRekening;



            }

        
        }
        private bool InquiryOnUs(){
            //string url = GlobalVar.BANK_URL + "Api/v1/login";

            string url = "https://dev-sp2d.bankkalbar.co.id/api/sp2d/inquiryRekening";// +txtAccountNo.Text.Trim();// GlobalVar.BANK_URL + "Api/vxx1/loginx";


            InquiriesRequest inquiry = new InquiriesRequest();
            txtNama.Text = "";
            inquiry.sandiBank = "123";
            inquiry.nomorRekening = txtAccountNo.Text;
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
                string ssignature =BuildSignature(inquiry, time);
                request.Headers.Add("x-bankkalbar-Signature", ssignature);//<clientSecret>,concat(


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
                if (resp.response_code != "00")
                {
                    MessageBox.Show(resp.message);
                    return false;
              

                }
                else
                {



                    dataInquiriesRespond data = resp.data;
                    txtNama.Text = data.namaPemilikRekening;
                    return true;
                    //     objResponse.Headers()
                }
            }
            catch (WebException wex)
            {

                using (WebResponse response = wex.Response)
                {
                    // TODO: Handle response being null
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        Console.WriteLine(text);
                    }
                }
                return false ;

            }
        }
        public string BuildSignature (InquiriesRequest inq, string  sTime)
        {
            // Serialize the header and claimSet
            string Secret = "lkaurLrcmDXrqRJzAOxAKQWuvAIYGG";

            string sInquiry = JsonConvert.SerializeObject(inq);
           sInquiry= sInquiry.Replace(" ", string.Empty).ToLower();
             
            string message = sInquiry + "|" + sTime;


            byte[] key = Encoding.ASCII.GetBytes(Secret);
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(message);
            MemoryStream stream = new MemoryStream(byteArray);
            string result = myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            Console.WriteLine(result);
            return result;
  


            //2 // Salah Signature 
            //HMACSHA256 hashObject = new HMACSHA256(Encoding.UTF8.GetBytes(Secret));

            //// Computes the signature by hashing the salt with the secret key as the key
            //var signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(message));

            //// Base 64 Encode
            //string encodedSignature = Convert.ToBase64String(signature);
            ////result
            //return encodedSignature;

            // 3 
            //HMACSHA256 hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(Secret));
            //byte[] byteArray = Encoding.ASCII.GetBytes(message);
            //return BytesToHexString(hs256.ComputeHash(byteArray)).Replace("-", "");


            
         
        }
        //private bool InquiryAntarBank()
        //{
        //    //string url = GlobalVar.BANK_URL + "Api/v1/login";

        //   // string url = "http://dev-api.bankkalbar.co.id:8898/api/v1/inquiries/" + txtAccountNo.Text.Trim();// GlobalVar.BANK_URL + "Api/vxx1/loginx";
        //   // /api/v1/inquiries/{bankCode}/{accountNo}

        //    InquiriesRequest inquiey = new InquiriesRequest();

        //  //  inquiey.accountNo = txtAccountNo.Text;// "ESSE_MAKER";//, "20561889-ac47-48d2-9211-8f4b4ed0");
        //  //public string accountNo { set; get; }
        // //public string accountName{ set; get; }

        //// public string bankCode { set; get; }
        //string bankKode= ctrlBank1.KodeBank.Trim();
        //string accountno = txtAccountNo.Text;
        //accountno = accountno.Replace("-", "");
        //accountno = accountno.Replace(" ", "");
        //accountno = accountno.Replace(".", "");
        //string url = "http://dev-api.bankkalbar.co.id:8898/api/v1/inquiries/" + bankKode.Trim() + "/" + accountno;

        //txtNama.Text = "";
    
            
        //    WebResponse objResponse;
        //    WebRequest request = null;
        //    try
        //    {
        //        request = WebRequest.Create(url);
        //        request.Method = "GET";
        //        request.Headers["Authorization"] = m_sBearer;
        //        objResponse = (WebResponse)request.GetResponse();
        //        Stream streamdata = objResponse.GetResponseStream();
        //        StreamReader strReader = new StreamReader(streamdata);
        //        string responseData = strReader.ReadToEnd();
        //        Inquiries resp = JsonConvert.DeserializeObject<Inquiries>(responseData);
        //        if (resp.success == false)
        //        {
        //            MessageBox.Show(resp.message);
        //            return false;


        //        }
        //        else
        //        {



        //            txtNama.Text = resp.account.accountName;

        //            return true;
            
        //        }
        //    }
        //    catch (WebException wex)
        //    {
        //        MessageBox.Show(wex.Message);
        //        return false;

        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private bool GenerateOTP( int index, int jumlahTrx){

            string url = GlobalVar.BANK_URL + "GenerateOTP";


            WebResponse objResponse = null;
            WebRequest request = null;
            try
            {


                GenerateOTPRequest datarequest = new GenerateOTPRequest();



                datarequest.referenceNo = index.ToReference(12, "10162");//"1013");//
                datarequest.phoneNo = "081211619471";// "081253570188";
                datarequest.email = "pudjoisnanto@gmail.com";// "tarsiusptk99@gmail.com";//   "pudjoisnanto@gmail.com";// "ketapang@bankkalbar.co.id";
                datarequest.jumlahTransaksi = jumlahTrx.ToString("####");
                
                request = WebRequest.Create(url);
                request.Method = "POST";
         request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");

                string JsonData = JsonConvert.SerializeObject(datarequest);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";

                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                DataGenerateOTPResponse resp = JsonConvert.DeserializeObject<DataGenerateOTPResponse>(responseData);

                if (resp.error_kode  != "00")
                {
                    MessageBox.Show(resp.message);
                    cmdCheckSaldo.Enabled = false;
                    return false;

                }
                else
                {


                    return true;
                }

                // return false;
            }


            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
                return false;


            }

        }
        //private bool Bayar(TransferOnUs onOs)
        //{

        //    string url = GlobalVar.BANK_URL + "Api/v1/login";



        //    url = "http://dev-api.bankkalbar.co.id:8898/api/v1/transfers/on-us";
          
        //    WebResponse objResponse=null;
        //    WebRequest request = null;
        //    try
        //    {
        //        request = WebRequest.Create(url);
        //        request.Method = "POST";
        //        request.Headers["Authorization"] = m_sBearer;

        //        string JsonData = JsonConvert.SerializeObject(onOs);
        //        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
        //        request.ContentType = "application/Json";

        //        request.ContentLength = byteArray.Length;
        //        System.IO.Stream dataStream = request.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        dataStream.Close();
        //        objResponse = (WebResponse)request.GetResponse();
        //        Stream streamdata = objResponse.GetResponseStream();
        //        StreamReader strReader = new StreamReader(streamdata);
        //        string responseData = strReader.ReadToEnd();
        //        loginresponse resp = JsonConvert.DeserializeObject<loginresponse>(responseData);
        //        if (resp.success == false)
        //        {
        //            MessageBox.Show(resp.message);
        //            cmdCheckSaldo.Enabled = false;
        //            return false;

        //        }
        //        else
        //        {

        //            MessageBox.Show(resp.message);
        //            return true;
        //        }

        //       // return false;
        //    }


        //    catch (WebException ex)
        //    {

        //      //  HttpWebResponse hr = (HttpWebResponse)objResponse;
        //        MessageBox.Show(ex.Message);


        //        return false;


        //    }
        //}
        private void cmdBayar_Click(object sender, EventArgs e)
        {
            int indexref = m_inoref;            
            try
            {


                string sKodeOTP = "";
                //if (chkgenerateOTP.Checked)
                //{
                //    if (GenerateOTP(20, 10) == false)
                //    {
                //        MessageBox.Show("Gagal membuat OTP");
                //        return;
                //    }
                //    else
                //    {
                //        MessageBox.Show("Generate OTP berhasil.. ");
                //    }
                //}
                if (MessageBox.Show("Apakah akan generate OTP?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (GenerateOTP(20, 10) == false)
                    {
                        MessageBox.Show("Gagal membuat OTP");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Generate OTP berhasil.. ");
                    }
                }
                frmInputOTP fOTP = new frmInputOTP();
                fOTP.ShowDialog();
                if (fOTP.OK == false)
                {
                    MessageBox.Show("Anda membatalkan pembayaran");
                    return;

                }

                sKodeOTP = fOTP.KodeOTP;

                ////for (int i = 0; i < gridSP2D.Rows.Count; i++)
                    for (int i = gridSP2D.Rows.Count-1 ; i >= 0; i--)
                {
               
                    bool bDipilih = Convert.ToBoolean(gridSP2D.Rows[i].Cells[2].Value);

              
                    if (bDipilih == true)
                    {



                        if (gridSP2D.Rows[i].Cells[0].Value != null)
                        {
                            indexref = GetNoRef();
                            txtIndex.Text = indexref.ToString();

                            long noUrut = DataFormat.GetLong(gridSP2D.Rows[i].Cells[0].Value);
                            string kodeBank = DataFormat.GetString(gridSP2D.Rows[i].Cells[6].Value);
                            string noRekeningTujuan = DataFormat.GetString(gridSP2D.Rows[i].Cells[8].Value);
                            decimal jumlahdibayar = DataFormat.GetDecimal(gridSP2D.Rows[i].Cells[13].Value);
                            DetailTransaksi57Request detailTrx = new DetailTransaksi57Request();
                            detailTrx.nomorSP2D = DataFormat.GetString(gridSP2D.Rows[i].Cells[3].Value);
                            detailTrx.nomorSPM = DataFormat.GetString(gridSP2D.Rows[i].Cells[4].Value);
                            detailTrx.tanggalTransaksi = DateTime.Now.Date.ToString("yyyy-MM-dd");

                            indexref = DataFormat.GetInteger(gridSP2D.Rows[i].Cells[20].Value);
                            detailTrx.referenceNo = indexref.ToReference(12,"10162");// "1013");
                            string j = "SKN";// DataFormat.GetString(gridSP2D.Rows[i].Cells[22].Value);

                            if (kodeBank == "123")
                            {
                                if (noRekeningTujuan.Length != 10)
                                {
                                    detailTrx.kodeJenisTransaksi = "Transfer-OnUs";
                                }
                                else
                                {
                                    detailTrx.kodeJenisTransaksi = "Transfer-OnUs";
                                    //detailTrx.kodeJenisTransaksi = "Transfer-SKN";
                                }
                            }
                            else
                            {
                                detailTrx.kodeJenisTransaksi = "Transfer-RTGS";
                            }

                           



                            detailTrx.notes = noUrut.ToString();
                            DataPengirim pengirimSp2D = new DataPengirim();

                            pengirimSp2D.kodeOpd = DataFormat.GetString(gridSP2D.Rows[i].Cells[18].Value);
                            pengirimSp2D.namaOpd = DataFormat.GetString(gridSP2D.Rows[i].Cells[19].Value);

                            pengirimSp2D.noRekening = "0709999020";// "7001007372";

                            //16
                            detailTrx.pengirim = pengirimSp2D;

                            DataPenerima penerimaSP2D = new DataPenerima();
                            penerimaSP2D.kodeBank = "123";// DataFormat.GetString(gridSP2D.Rows[i].Cells[6].Value);
                            penerimaSP2D.namaBank = DataFormat.GetString(gridSP2D.Rows[i].Cells[7].Value);
                            penerimaSP2D.noRekening = "1025284549";// DataFormat.GetString(gridSP2D.Rows[i].Cells[8].Value);
                            penerimaSP2D.namaPenerima = "DONNY ARDYANSYAH";// DataFormat.GetString(gridSP2D.Rows[i].Cells[5].Value);
                            penerimaSP2D.npwp = "001496827018000";// DataFormat.GetString(gridSP2D.Rows[i].Cells[9].Value).Replace(".", "").Replace("-", "");
                            detailTrx.penerima = penerimaSP2D;


                            detailTrx.jumlahNominalTransaksi = DataFormat.GetString(gridSP2D.Rows[i].Cells[10].Value);
                            detailTrx.jumlahPotonganMpn = DataFormat.GetString(gridSP2D.Rows[i].Cells[11].Value);
                            detailTrx.jumlahPotonganNonMpn = DataFormat.GetString(gridSP2D.Rows[i].Cells[12].Value);
                            detailTrx.jumlahDibayar = DataFormat.GetString(gridSP2D.Rows[i].Cells[13].Value);

                            detailTrx.detailPotonganMpn = new List<DetailPotonganMpn>();
                            foreach (PotonganSPP p in m_lstspp[0].Potongans)
                            {
                                if (p.Informasi == 0)
                                {
                                    if (p.IDBilling.Trim().Length > 0)
                                    {
                                        DetailPotonganMpn pt = new DetailPotonganMpn();

                                        pt.idBilling = p.IDBilling.Trim();

                                        pt.keteranganPotongan = p.Nama;
                                        indexref = GetNoRef();
                                        txtIndex.Text = indexref.ToString();
                                        pt.referenceNo = (indexref++).ToReference(12,"10162");// "1013");
                                        pt.nominalPotongan = p.Jumlah.ToString("###");//.Replace(".00", "").Replace(",", "");
                                        detailTrx.detailPotonganMpn.Add(pt);
                                    }
                                }

                            }


                            detailTrx.detailPotonganNonMpn = new List<DetailPotonganNonMpn>();
                            foreach (PotonganSPP p in m_lstspp[0].Potongans)
                            {
                                if (p.Informasi == 1)
                                {
                                    if (p.Jumlah > 0)
                                    {
                                        DetailPotonganNonMpn pt = new DetailPotonganNonMpn();

                                        pt.kodeMap = p.IIDRekening.ToString().Trim();
                                        pt.keteranganKodeMap = p.Nama;
                                        pt.nominalPotongan = p.Jumlah.ToString("###");//.Replace(".00", "").Replace(",", "");
                                        detailTrx.detailPotonganNonMpn.Add(pt);
                                    }
                                }

                            }

                            //indexref++;

                            DetailTransaksi57RequestEx detailTrxEx = new DetailTransaksi57RequestEx();
                            detailTrxEx.KodeOTP = sKodeOTP;
                            detailTrxEx.data = detailTrx;

                            DataTransaksiSP2DOnline510ResponseEx resp = BayarAPIBaru(detailTrxEx);
                            if (resp != null)
                            {

                                //txtIndex.Text = indexref.ToString();
                                SPPLogic sppLogic = new SPPLogic(GlobalVar.TahunAnggaran);

                                if (resp.error_kode == "00" || resp.error_kode == "11")
                                {
                                    if (sppLogic.ProcessSP2DOnlneResult(noUrut, resp) == false)
                                    {
                                        MessageBox.Show(sppLogic.LastError());

                                    }

                                    else
                                    {
                                        //gridSP2D.Rows[i].Visible = false;
                                        DataGridViewRow row = gridSP2D.Rows[i];
                                        gridSP2D.Rows.Remove(row);
                                        MessageBox.Show("Pembayaran Selesai " + resp.message);

                                    }

                                }
                                else
                                {

                                    MessageBox.Show(resp.message);

                                }
                                ///
                                /// Tulis Log 
                                /// 
                                SP2DOnLineLog log = new SP2DOnLineLog();
                                log.NoUrut = noUrut;
                                log.Waktu = DateTime.Now;
                                log.otp = sKodeOTP;
                                log.responseKode = resp.error_kode;
                                log.pesan = resp.message;
                                sppLogic.LogSP2DOnline(log);

                                gridSP2D.Rows[i].Cells[21].Value = resp.message;
                            }
                        }


                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan dalam proses pembayaran...." + ex.Message);

            }
            
        }

        private DataTransaksiSP2DOnline510ResponseEx BayarAPIBaru(DetailTransaksi57RequestEx detailTrx)
        {

            try
            {


                string url = "";//
                DataTransaksiSP2DOnline510ResponseEx resp = new DataTransaksiSP2DOnline510ResponseEx();


                

                url = GlobalVar.BANK_URL + "DetailTransaksiSP2DOnline";


                WebResponse objResponse = null;
                WebRequest request = null;
                try
                {
                    request = WebRequest.Create(url);
                    request.Method = "POST";

                    //request.Headers["Authorization"] = m_sBearer;
                    request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                    string JsonData = JsonConvert.SerializeObject(detailTrx);
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                    request.ContentType = "application/Json";

                    request.ContentLength = byteArray.Length;
                    System.IO.Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    objResponse = (WebResponse)request.GetResponse();
                    Stream streamdata = objResponse.GetResponseStream();
                    StreamReader strReader = new StreamReader(streamdata);
                    string responseData = strReader.ReadToEnd();
                    resp = JsonConvert.DeserializeObject<DataTransaksiSP2DOnline510ResponseEx>(responseData);

                    return resp;

                }


                catch (WebException ex)
                {

                    resp.error_kode = "99";
                    resp.message = ex.Message;
                    return resp;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

        }
        
        //private bool BayarRTGS(RTGS oRTGS )
        //{

            

        //    string url = GlobalVar.BANK_URL + "Api/v1/login";



        //    url = "http://dev-api.bankkalbar.co.id:8898/api/v1/transfers/skn-rtgs";



        //    WebResponse objResponse = null;
        //    WebRequest request = null;
        //    try
        //    {
        //        request = WebRequest.Create(url);
        //        request.Method = "POST";
                
        //        request.Headers["Authorization"] = m_sBearer;

        //        string JsonData = JsonConvert.SerializeObject(oRTGS);
        //        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
        //        request.ContentType = "application/Json";

        //        request.ContentLength = byteArray.Length;
        //        System.IO.Stream dataStream = request.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        dataStream.Close();
        //        objResponse = (WebResponse)request.GetResponse();
        //        Stream streamdata = objResponse.GetResponseStream();
        //        StreamReader strReader = new StreamReader(streamdata);
        //        string responseData = strReader.ReadToEnd();
        //        loginresponse resp = JsonConvert.DeserializeObject<loginresponse>(responseData);
        //        if (resp.success == false)
        //        {
        //            MessageBox.Show(resp.message);
        //            cmdCheckSaldo.Enabled = false;
        //            return false;

        //        }
        //        else
        //        {

        //            MessageBox.Show(resp.message);
        //            return true;
        //        }

        //        // return false;
        //    }


        //    catch (WebException ex)
        //    {

        //        //  HttpWebResponse hr = (HttpWebResponse)objResponse;
        //        MessageBox.Show(ex.Message);


        //        return false;


        //    }


        //}
        private void gridSP2D_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridSP2D.Rows.Count)
            {
                if (e.ColumnIndex == 1)
                {
                    TampilkanRiwayatSP2DOnline(e.RowIndex);

                }

                /*
                txtAccountNo.Text = gridSP2D.Rows[e.RowIndex].Cells[6].Value.ToString().Replace(".", "").Replace(" ", ""); ;
                txtNPWP.Text = gridSP2D.Rows[e.RowIndex].Cells[7].Value.ToString().Replace(".", "").Replace(" ", ""); ;
                string sNoUrut = gridSP2D.Rows[e.RowIndex].Cells[0].Value.ToString();
                long lNoUrut = DataFormat.GetLong(sNoUrut);
                SPP oSPP = new SPP();
                if (m_lstspp != null)
                {
                    oSPP = m_lstspp[e.RowIndex];
                }
                //        ctrlRekeningKegiatan1.CreateSPP(oSPP);
                if (e.ColumnIndex == 1)
                {
                    frmSPP fSPP = new frmSPP();
                    fSPP.SetModeForm(0);
                    //fSPP.SetSPP(oSPP);
                    fSPP.SetID(lNoUrut);
                    fSPP.Show();

                }

                */

            }
        }
        private void TampilkanRiwayatSP2DOnline(int i)
        {

            
            string sNoSP2D;
            sNoSP2D = DataFormat.GetString(gridSP2D.Rows[i].Cells[3].Value);
            long noUrut = DataFormat.GetLong(gridSP2D.Rows[i].Cells[0].Value);
            frmRespond511 fD = new frmRespond511();
            fD.TampilkanDataTrxSP2DOnline(sNoSP2D, noUrut);
            fD.Show();

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void froSP2DOnline_Resize(object sender, EventArgs e)
        {
          //  panel1.Size = this.Size;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private  string GetrandomRef()
        {
            var now = DateTime.Now;

            var days = (int)(now - new DateTime(2000, 1, 1)).TotalDays;
            var seconds = (int)(now - DateTime.Today).TotalSeconds;

            var counter = _InternalCounter++ % 100;
            string ret = "070" + RandomDigits(9);// (days % 100).ToString("00") + seconds.ToString("00000") + counter.ToString("00");

            return ret;
       }
        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(9).ToString());
            return s;
        }

        
        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public string get9number(){
            string ret="";
           // for (int ctr = 0; ctr <= 9; ctr++)
                ret  = ret + _random.Next();

            return ret;
        
        }

        private void button2_Click_1(object sender, EventArgs e)
        {


            //string url = GlobalVar.BANK_URL + "Api/v1/login";

            //// string url = "http://dev-api.bankkalbar.coc.id:8898/Api/v1/login";// GlobalVar.BANK_URL + "Api/vxx1/loginx";


            //TrxLogin tlogin = new TrxLogin();
            //url = "http://dev-api.bankkalbar.co.id:8898/api/v1/login";
            //tlogin.username = "KASDA_MKR";//txtUser.Te "KASDA_MKR";// "ESSE_MAKER";//, "20561889-ac47-48d2-9211-8f4b4ed0");
            //tlogin.password = Encrypt("K@lbar2");//, "20561889-ac47-48d2-9211-8f4b4ed0"); ;
            //WebResponse objResponse;
            //WebRequest request = null;
            //try
            //{
            //    request = WebRequest.Create(url);
            //    request.Method = "POST";
            //    string JsonData = JsonConvert.SerializeObject(tlogin);
            //    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
            //    request.ContentType = "application/Json";
            //    // request.Headers.A
            //    request.ContentLength = byteArray.Length;
            //    System.IO.Stream dataStream = request.GetRequestStream();
            //    dataStream.Write(byteArray, 0, byteArray.Length);
            //    dataStream.Close();
            //    objResponse = (WebResponse)request.GetResponse();
            //    Stream streamdata = objResponse.GetResponseStream();
            //    StreamReader strReader = new StreamReader(streamdata);
            //    string responseData = strReader.ReadToEnd();
            //    loginresponse resp = JsonConvert.DeserializeObject<loginresponse>(responseData);
            //    if (resp.success == false)
            //    {
            //        MessageBox.Show(resp.message);
            //        cmdCheckSaldo.Enabled = false;


            //    }
            //    else
            //    {

            //        for (int i = 0; i < objResponse.Headers.Count; ++i)
            //            Console.WriteLine("\nHeader Name:{0}, Header value :{1}", objResponse.Headers.Keys[i], objResponse.Headers[i]);


            //        string Bearer = objResponse.Headers["Authorization"];
            //        m_sBearer = Bearer;
            //        cmdCheckSaldo.Enabled = true;
            //        txtBalances.Visible = true;

            //        MessageBox.Show("Login Sukses");
            //        cmdCheckSaldo.Enabled = true;

            //        //panel1.Hide();
            //        //     objResponse.Headers()
            //    }


            //}
            //catch (WebException ex)
            //{
            //    MessageBox.Show(ex.Message);



            //}
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {


            //string url = GlobalVar.BANK_URL + "Api/v1/login";

            //// string url = "http://dev-api.bankkalbar.coc.id:8898/Api/v1/login";// GlobalVar.BANK_URL + "Api/vxx1/loginx";


            //TrxLogin tlogin = new TrxLogin();
            //url = "http://dev-api.bankkalbar.co.id:8898/api/v1/login";

            //tlogin.username = "KASDA_MKR";//txtUser.Te "KASDA_MKR";// "ESSE_MAKER";//, "20561889-ac47-48d2-9211-8f4b4ed0");
            //tlogin.password = Encrypt("K@lbar2");//, "20561889-ac47-48d2-9211-8f4b4ed0"); ;

            //WebResponse objResponse;
            //WebRequest request = null;
            //try
            //{
            //    request = WebRequest.Create(url);
            //    request.Method = "POST";
            //    string JsonData = JsonConvert.SerializeObject(tlogin);
            //    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
            //    request.ContentType = "application/Json";
            //    // request.Headers.A
            //    request.ContentLength = byteArray.Length;
            //    System.IO.Stream dataStream = request.GetRequestStream();
            //    dataStream.Write(byteArray, 0, byteArray.Length);
            //    dataStream.Close();

            //    objResponse = (WebResponse)request.GetResponse();

            //    Stream streamdata = objResponse.GetResponseStream();
            //    StreamReader strReader = new StreamReader(streamdata);
            //    string responseData = strReader.ReadToEnd();
            //    loginresponse resp = JsonConvert.DeserializeObject<loginresponse>(responseData);
            //    if (resp.success == false)
            //    {
            //        MessageBox.Show(resp.message);
            //        cmdCheckSaldo.Enabled = false;


            //    }
            //    else
            //    {

            //        for (int i = 0; i < objResponse.Headers.Count; ++i)
            //            Console.WriteLine("\nHeader Name:{0}, Header value :{1}", objResponse.Headers.Keys[i], objResponse.Headers[i]);


            //        string Bearer = objResponse.Headers["Authorization"];

            //        m_sBearer = Bearer;
            //        cmdCheckSaldo.Enabled = true;
            //        txtBalances.Visible = true;

            //        MessageBox.Show("Login Sukses");
            //        cmdCheckSaldo.Enabled = true;

            //        //panel1.Hide();
            //        //     objResponse.Headers()
            //    }


            //}
            //catch (WebException ex)
            //{
            //    MessageBox.Show(ex.Message);



            //}
        }

        private void btnInquiriNPWP_Click(object sender, EventArgs e)
        {

            
             SP2DOnlineAPI oAPI = new SP2DOnlineAPI();

            InquiriesNPWPRespond resp = oAPI.InQuiryNPWP (txtNPWP.Text, txtKodeMap.Text ,txtKodeSetor.Text) ;

            if (resp.response_code != "00")
            {
                MessageBox.Show(resp.message);
             }
            else
            {


                frmDataNPWP fData = new frmDataNPWP();
             //   fData.SetData(resp.data);
                fData.Show();
             
                
               
            }

        }

        private void cmdInqIDBilling_Click(object sender, EventArgs e)
        {

            //SP2DOnlineAPI oAPI = new SP2DOnlineAPI();

            //InquiriesIdBillingRespon resp = oAPI.InQuiryIdBilling(txtIDBilling.Text);

            //if (resp.response_code != "00")
            //{
            //    MessageBox.Show(resp.message);
            //}
            //else
            //{

            //    //Console.WriteLine resp

            //    frmDataIDBilling fData = new frmDataIDBilling();
            //    fData.SetData(resp);
            //    fData.Show();



            //}

        }

        private void cmdCreateIdBilling_Click(object sender, EventArgs e)
        {
            //frmCreateIDBilling fCIdBilling = new frmCreateIDBilling();
            //fCIdBilling.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdTestResponse_Click(object sender, EventArgs e)
        {
            //string url = "";//



            //url = "https://localhost:7139/TransaksiSP2DOnline510";



            //WebResponse objResponse = null;
            //WebRequest request = null;
            //try
            //{
            //    request = WebRequest.Create(url);
            //    request.Method = "POST";

            //    //request.Headers["Authorization"] = m_sBearer;

            //    string JsonData = "";
            //    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
            //    request.ContentType = "application/Json";

            //    request.ContentLength = byteArray.Length;
            //    System.IO.Stream dataStream = request.GetRequestStream();
            //    dataStream.Write(byteArray, 0, byteArray.Length);
            //    dataStream.Close();
            //    objResponse = (WebResponse)request.GetResponse();
            //    Stream streamdata = objResponse.GetResponseStream();
            //    StreamReader strReader = new StreamReader(streamdata);
            //    string responseData = strReader.ReadToEnd();

            //    DataTransaksiSP2DOnline510ResponseEx resp = JsonConvert.DeserializeObject<DataTransaksiSP2DOnline510ResponseEx>(responseData);

            //    if (resp.error_kode =="00")
            //    {
            //        MessageBox.Show(resp.message);
            //        //DataTransaksiSP2DOnline510ResponseEx data = resp.data;

            //        SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
            //        oLogic.ProcessSP2DOnlneResult(230210101031611,resp);

              
            //        return;

            //    }
            //    else
            //    {

            //        MessageBox.Show(resp.message);
            //        return;
            //    }

            //    // return false;
            //}


            //catch (WebException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return;


            //}

        }

        private void chkCair_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkCair.Checked)
            //{
            //    lblStatus.Text = "Tanggal Cair";
            //}
            //else
            //{
            //    lblStatus.Text = "Tanggal Terbt SP2D";
            //}
        }

     

    }
}
/*
public string  accountNumber{set;get;}
 * 
public string  accountNumber{set;get;}

    public string  bankCode{set;get;}

    public string  bic{set;get;}

    public string  participantName{set;get;}

 * 

*/