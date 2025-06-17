
using BP.Bendahara;
using DTO.Bendahara;
using DTO.SP2DOnLine;
using Formatting;
using KUAPPAS.SP2DOnline;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS.Bendahara
{
    public partial class frmTambahPajak : Form
    {
        private bool m_bOK;
        private int KodePotongan;

        private bool m_bTest;
        private int m_KodeMap;
        private RefPajak mRefPajak;

        public delegate void ValueChangedEventHandler(PotonganSPP pID);
        public event ValueChangedEventHandler OnAdd;
        private string m_sNPWP;
        private DateTime mTanggalSPM;
        private string msNoSPM;
        private string msNPWPDinas;
        private string m_sDinas;
        private string m_IDBILLING;
        public frmTambahPajak()
        {
            InitializeComponent();
            m_bOK = false;
            mRefPajak = new RefPajak();
            m_IDBILLING = "";
            m_bTest = false;
            m_sNPWP = "001496827018000";
        }
        public DateTime TanggalSPM
        {
            set { mTanggalSPM = value; }
        }
        public string Dinas
        {
            set
            {
                m_sDinas = value;
            }
        }
        public string NoSPM
        {
            set
            {
                msNoSPM = value;
            }
        }
        public string NPWP
        {
            set
            {
                m_sNPWP = value;
               m_sNPWP = "001496827018000";
            }
        }
        public string NPWPDinas
        {
            set
            {
                msNPWPDinas = value;
            }
        }
        public bool Test
        {
            set
            {
                m_bTest = false;
                if (m_bTest == true)
                {

                    NoSPM = "Test NoSPM";
                    mTanggalSPM = DateTime.Now.Date;
;
                }

            }
        }
        private void frmTambahPajak_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Pecarian Kode Potongan, Kode Setor Dan Pengisian Data Pembuatan Id Billing");
            ctrlPotongan1.Create(1);

            if (GlobalVar.gListRefPajak == null)
            {
                RefPajakLogic oLogic = new RefPajakLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gListRefPajak = oLogic.Get();

            }
            if (m_bTest == true)
            {
                cmdOK.Visible = true;// false;
                ctrlDinas1.Visible = true;
                cmdTest.Visible = true;
                ctrlDinas1.Create();
            }
            else
            {
                cmdTest.Visible = true;
                cmdInquiryMPN.Visible = false;
                cmdGenerateReport.Visible = false;


                ctrlDinas1.Visible = false;

            }

        }
        
        public bool OK
        {
            get { return m_bOK; }
        }
        public string NamaPajak
        {
            get { return ctrlPotongan1.NamaPajak; }
        }

         public string NoFaktur{
             get{
                 return txtnomorFakturPajak.Text;
             }
         }
         public string NIKRekanan
         {
             get
             {
                 return txtnikRekanan.Text;
             }
         }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            m_bOK = true;
            //if(CekInput())
            //CreateIdBilling();
            
            this.Hide();
        }
        private void InquiryMON()
        {
            int idx = 0;
    
            SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);
            idx = oSPPLogic.GetNoPeguji();
            if (m_IDBILLING.Length == 0)
            {
                MessageBox.Show ("ID  Billinhg Tidak Tersedia ");
                return;
            }
            else { 
            
                InquiryPaymentMPNRequest request = new InquiryPaymentMPNRequest();
                request.idBilling = m_IDBILLING;
                request.reInquiry = "false";
                request.referenceNo = (101300000000 + idx).ToString();
                InQuiryMPN(request);


                
                
            }

           
           

        }
        private DataInquiryPaymentMPNResponseEx InQuiryMPN(InquiryPaymentMPNRequest requestInquiry)
        {

            string url = "";//
            DataInquiryPaymentMPNResponseEx resp = new DataInquiryPaymentMPNResponseEx();



            url = GlobalVar.BANK_URL + "MPN";
            //url = "http://36.92.240.142:8080/MPN";



            WebResponse objResponse = null;
            WebRequest request = null;
            try
            {
                request = WebRequest.Create(url);
                request.Method = "POST";


                //  url = url + "?sandiBank=" + ctrlDaftarBank1.Kode.Trim() + "&nomorRekening=" + txtNoRekening.Text.Replace(".", "");
               // string JsonData = JsonConvert.SerializeObject(requestInquiry);
                //byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
               // request.ContentType = "application/Json";
               // request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                //request.ContentLength = byteArray.Length;
               // System.IO.Stream dataStream = request.GetRequestStream();
               // dataStream.Write(byteArray, 0, byteArray.Length);
               // dataStream.Close();


               // objResponse = (WebResponse)request.GetResponse();
               // Stream streamdata = objResponse.GetResponseStream();
              //  StreamReader strReader = new StreamReader(streamdata);
              //  string responseData = strReader.ReadToEnd();
              //  resp = JsonConvert.DeserializeObject<DataInquiryPaymentMPNResponseEx>(responseData);
                string JsonData = JsonConvert.SerializeObject(requestInquiry);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";
                request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                // request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();


                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                resp = JsonConvert.DeserializeObject<DataInquiryPaymentMPNResponseEx>(responseData);



                if (resp.error_kode != "00")
                {


                    //string message; 
                    //    message =resp.message;
                    //MessageBox.Show(message);
                    frmMPN fMPN = new frmMPN();
                    string message;

                    string idBilling =

                     message = "Id Billing no " + requestInquiry.idBilling + "  " + resp.message;

                    fMPN.Pesan = message;
                    fMPN.SetData(resp);
                    fMPN.ShowDialog();
                }
                else
                {

                    frmMPN fMPN = new frmMPN();
                    string message;

                    if (resp.ntpn.Trim().Length > 0)
                    {

                        message = "Id Billing no " + resp.idBilling + "   SUDAH dibayar ";

                    }
                    else
                    {
                        message = "Id Billing no " + resp.idBilling + "   BELUM dibayar ";
                    }
                    fMPN.Pesan = message;
                    fMPN.SetData(resp);
                    fMPN.ShowDialog();


                }
                return resp;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
        }
        private bool CreateIdBilling()
        {
            CreateIdBillingRequest requestInquiry = new CreateIdBillingRequest();

           
            requestInquiry.nomorPokokWajibPajak = m_sNPWP.Trim().Replace("-", "").Replace(".", "");
            requestInquiry.kodeMap = ctrlPotongan1.GetKodeMap().ToString().Trim();
            requestInquiry.kodeSetor =ctrlKodeSetor1.GetKode().Trim();     
            requestInquiry.masaPajak = mTanggalSPM.Month.ToString("00") + mTanggalSPM.Month.ToString("00");
            requestInquiry.tahunPajak = GlobalVar.TahunAnggaran.ToString();
            requestInquiry.jumlahBayar = DataFormat.GetDecimal(txtJumlah.Text).ToString("###");
            decimal d = DataFormat.GetDecimal(txtJumlah.Text);
            txtJumlah.Text = d.ToRupiahInReport();
          //  requestInquiry.nomorPokokWajibPajakPenyetor = msNPWPDinas.Trim().Replace("-", "").Replace(".", "");
            
            requestInquiry.nomorSPM = txtnomorSPM.Text;
            requestInquiry.namaWajibPajak = "";
            requestInquiry.alamatWajibPajak = "";// txtalamatWajibPajak.Text;
            requestInquiry.kota = "";// txtkota.Text;
            requestInquiry.nik = "";// txtnik.Text;
            requestInquiry.nomorFakturPajak = txtnomorFakturPajak.Text;
            requestInquiry.nomorObjekPajak = txtnomorObjekPajak.Text;
            requestInquiry.nomorPokokWajibPajakPenyetor = txtnomorPokokWajibPajakPenyetor.Text;
            requestInquiry.nomorPokokWajibPajakRekanan = txtnomorPokokWajibPajakRekanan.Text;
            requestInquiry.nomorSK = txtnomorSk.Text;
            requestInquiry.nikRekanan = txtnikRekanan.Text;
            requestInquiry.nomorSKPD = txtnomorSKPD.Text;
           



            //if (requestInquiry.kodeMap == "411121" && requestInquiry.kodeSetor == "100")
            //{
            //    requestInquiry.nomorSPM = msNoSPM;
            //    requestInquiry.nomorPokokWajibPajakRekanan = "";
            //    requestInquiry.nikRekanan = "";
            //}
            //if (requestInquiry.kodeMap == "411124" && requestInquiry.kodeSetor == "100")
            //{
            //    requestInquiry.nomorSPM = msNoSPM;
            //    requestInquiry.nomorPokokWajibPajakRekanan = m_sNPWP.Trim().Replace("-", "").Replace(".", "");
            //    requestInquiry.nikRekanan = "3200000612701111";
            //}
            //if (requestInquiry.kodeMap == "411211" && requestInquiry.kodeSetor == "920")
            //{
            //    requestInquiry.nomorSPM = msNoSPM;
            //    requestInquiry.nomorFakturPajak = "0200001000000000";
            //    requestInquiry.nomorPokokWajibPajakRekanan = m_sNPWP.Trim().Replace("-", "").Replace(".", "");
            //    //  requestInquiry.nikRekanan = p.NoFaktur.Trim();// "3200000612701111";

            //}
            //if (requestInquiry.kodeMap == "411128" && requestInquiry.kodeSetor == "409")
            //{
            //    requestInquiry.nomorSPM = msNoSPM;
            //    requestInquiry.nomorPokokWajibPajakRekanan = m_sNPWP.Trim().Replace("-", "").Replace(".", "");

            //    requestInquiry.nikRekanan = "3200000612701111";
            //}
            //// requestInquiry.nomorPokokWajibPajakPenyetor = txtNoNPWP.Text.Trim().Replace("-", "").Replace(".", "");
            //// requestInquiry.nomorPokokWajibPajakPenyetor = "020652384429000";
            //// case 6.3 
            //if (requestInquiry.kodeMap == "411122" && requestInquiry.kodeSetor == "920")
            //{
            //    requestInquiry.nomorPokokWajibPajakPenyetor = m_sNPWP; //"020652384429000";
            //    requestInquiry.nomorPokokWajibPajakRekanan = m_sNPWP; //; //"020652384429000";


            //    requestInquiry.nomorPokokWajibPajak = m_sNPWP.Trim().Replace("-", "").Replace(".", "");

            //    requestInquiry.nomorPokokWajibPajakPenyetor = "020652384429000";
            //    requestInquiry.nomorPokokWajibPajakRekanan = "020652384429000";

            //}

            //if (p.KodeMap.Trim() == "411122" && p.KodeSetor.Trim() == "100")
            //{
            //    requestInquiry.nomorSKPD = "";
            //    requestInquiry.nomorSPM = "";
            //}
            //else
            //{
            //    requestInquiry.nomorSKPD = m_sDinas;
            //}
            //if (p.KodeMap.Trim() == "411211" && p.KodeSetor.Trim() == "100")// case 4.3
            //{
            //    //                "nomorPokokWajibPajak": "147542823701000",
            //    //"kodeMap": "411122",
            //    //"kodeSetor": "920",
            //    //"nomorPokokWajibPajakPenyetor": "020652384429000",
            //    //"nomorPokokWajibPajakRekanan": "020652384429000",

            //    requestInquiry.nomorSKPD = "";
            //    requestInquiry.nomorSPM = "";
            //    //      requestInquiry.nomorPokokWajibPajakRekanan =  txtNoNPWP.Text.Trim();// dikosongkan 
            //    requestInquiry.nomorPokokWajibPajakPenyetor = "001496827018000";// txtNoNPWP.Text;
            //}

            //if (p.KodeMap.Trim() == "411128" && p.KodeSetor.Trim() == "423")// case 6.5
            //{
            //    requestInquiry.nomorSKPD = m_sDinas;
            //    requestInquiry.nomorSPM = msNoSPM;
            //    requestInquiry.nomorPokokWajibPajakRekanan = m_sNPWP;
            //    //requestInquiry.nomorPokokWajibPajakPenyetor = txtNoNPWP.Text;==> harus berbeda antara npwp penyetor dan npwp 
            //    /// yang benar adalah nPWP harus berbeda
            //    /// 
            //    if (p.NPWPPenyetor.Replace(" ", "") == "")
            //    {
            //        requestInquiry.nomorPokokWajibPajakPenyetor = m_sNPWP.Trim();
            //    }
            //    else
            //    {
            //        requestInquiry.nomorPokokWajibPajakPenyetor = p.NPWPPenyetor.Replace(" ", "");
            //    }
            //    //

            //}
            //if (p.KodeMap.Trim() == "411121" && p.KodeSetor.Trim() == "100")
            //{ // case 6.6

            //    requestInquiry.nomorSKPD = m_sDinas;
            //    requestInquiry.nomorSPM = msNoSPM;
            //    requestInquiry.nomorPokokWajibPajakRekanan = "";// txtNoNPWP.Text.Trim();// dikosongkan 
            //    requestInquiry.nomorPokokWajibPajakPenyetor = m_sNPWP;//==> harus berbeda antara npwp penyetor dan npwp 
            //    // harus sama NPWP penyetor .. ini salah  maka di comment 
            //    // requestInquiry.nomorPokokWajibPajakPenyetor = "020652384429000";

            //    //
            //    //untuk casenya, NOP disi. Sehingga error. Harusnya dikosongkan ... 
            //    //requestInquiry.nomorObjekPajak = "112233344455566667";
            //}
            //if (p.KodeMap.Trim() == "411128" && p.KodeSetor.Trim() == "402")
            //{ // case 6.7 ... NOP harus diisi

            //    requestInquiry.nomorSKPD = m_sDinas;
            //    requestInquiry.nomorSPM = m_sDinas;
            //    requestInquiry.nomorPokokWajibPajakRekanan = "020652384429000";
            //    requestInquiry.nikRekanan = "3200000612701111";

            //    requestInquiry.nomorPokokWajibPajakPenyetor = "001496827018000";
            //    //
            //    //untuk casenya, NOP tidak disi. Sehingga error. Harusnya diisi ... 
            //    //  requestInquiry.nomorObjekPajak = "112233344455566667";
            //}
            //// case 6.8 masa pajak range 
            //if (requestInquiry.kodeMap == "411121" && requestInquiry.kodeSetor == "100")
            //{  //ini salah maka di comment  
            //    // requestInquiry.masaPajak = dtSPM.Value.Month.ToString("##") + (dtSPM.Value.Month+1).ToString("##");
            //}



            string url = "";//
            DataCreateIdBillingResponseEx resp = new DataCreateIdBillingResponseEx();


            url = GlobalVar.BANK_URL + "InquiryIdBilling";
            //url = "http://36.92.240.142:8080/InquiryRekening";



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
                request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                // request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();


                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                resp = JsonConvert.DeserializeObject<DataCreateIdBillingResponseEx>(responseData);

                if (resp.error_kode != "00")
                {
                    MessageBox.Show(resp.message);
                    return false;
                }
                else
                {
                    //PotonganSPPLogic oLogic = new PotonganSPPLogic(GlobalVar.TahunAnggaran);
                    //p.IDBilling = resp.idBilling;
                    //if (oLogic.UpdateIDBilling(p) == false)
                    //{
                    //    MessageBox.Show(oLogic.LastError());

                    //}

                    frmDataCreateIdilling fHasilCreateIDBilling = new frmDataCreateIdilling();
                    fHasilCreateIDBilling.SetData(resp);
                    fHasilCreateIDBilling.ShowDialog();
                    m_IDBILLING = resp.idBilling;

                    //p.IDBilling = resp.idBilling;
                    // isi ke gctrlPotongan 



                    return true;
                    //   MessageBox.Show(resp.message);

                }
                // return resp;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }
        }

        private void xmsBatal_Click(object sender, EventArgs e)
        {
            m_bOK = false;
            this.Hide();
        }
        public int KodeMap
        {
            get { return m_KodeMap; }
        }
        public int IDPotongan
        {
            get { return KodePotongan; }
        }


        private void ctrlPotongan1_OnChanged(int pIDPotongan, int pIDKodePusat)
        {
            KodePotongan = pIDPotongan;
            m_KodeMap = pIDKodePusat;
            ctrlKodeSetor1.KodeMap = m_KodeMap;
            ctrlKodeSetor1.Create(m_KodeMap);
            ctrlKodeSetor1.Clear();
        }
        public string KodeSetor
        {
            get { return ctrlKodeSetor1.GetKode(); }
        }
        public decimal Jumlah
        {
            get { return DataFormat.FormatUangReportKeDecimal(txtJumlah.Text); }
        }

        private void txtJumlah_Leave(object sender, EventArgs e)
        {
            decimal d = DataFormat.GetDecimal(txtJumlah.Text);
            txtJumlah.Text = d.ToRupiahInReport();


            
        }

        private void txtJumlah_Enter(object sender, EventArgs e)
        {
            txtJumlah.Text = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text).ToString();
        }

        private void cmdOKTidakTutup_Click(object sender, EventArgs e)
        {
            if (KodePotongan == 0)
            {
                MessageBox.Show("Belum Pilih Kode Potongan");
                return;
            }
            if (KodeSetor =="")
            {
                MessageBox.Show("Belum Pilih Kode Setor");
                return;
            }
            PotonganSPP p = new PotonganSPP();
            p.IIDRekening = KodePotongan;
            p.KodeSetor = KodeSetor;
            p.IDBilling = "";
            p.Nama = NamaPajak;
            p.Jumlah = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
            if (OnAdd != null)
            {
                OnAdd(p);
            }
        }

        private void ctrlKodeSetor1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlPotongan1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlKodeSetor1_OnChanged(RefPajak refPajak)
        {
            mRefPajak = refPajak;
            if (refPajak != null)
            {


                if (mRefPajak.wp_badan == 0)
                {
                    m_sNPWP = msNPWPDinas;
                }

                if (mRefPajak.wp_pemungut == 0)
                {
                    m_sNPWP = msNPWPDinas;
                }
               

                txtnomorObjekPajak.Enabled = refPajak.butuh_nop == 1;
                if (txtnomorObjekPajak.Enabled == false)
                {
                    txtnomorObjekPajak.Text = "";
                }
                txtnomorSk.Enabled = refPajak.butuh_nosk == 1;

                label13.Visible = refPajak.butuh_nosk == 1;

                if (txtnomorSk.Enabled== false ){
                    txtnomorSk.Text = "";
                    label13.Visible = false;
                }
                else
                {
                    txtnomorSk.Text  =msNPWPDinas;
                }
                if (refPajak.npwp_lain == 0)
                {
                    txtnomorPokokWajibPajakPenyetor.Text = m_sNPWP;
                }
                if (refPajak.npwp_lain == 1 )               {
                    txtnomorPokokWajibPajakPenyetor.Text = "001496827018000";// msNPWPDinas;
                }
                if (refPajak.npwp_lain == 2){
                    txtnomorPokokWajibPajakPenyetor.Text = "";
                }
                txtnomorObjekPajak.Enabled = refPajak.butuh_nop == 1;
                if (txtnomorObjekPajak.Enabled == false)
                {
                    txtnomorObjekPajak.Text = "";
                }
                txtnikRekanan.Enabled = refPajak.nik_rekanan == 1;

                if (txtnikRekanan.Enabled == false)
                {
                    txtnikRekanan.Text = "";
                }


                if (refPajak.no_spm == 1)
                {
                    txtnomorSPM.Text = msNoSPM;
                }
                else
                {
                    txtnomorSPM.Enabled = false;
                    txtnomorSPM.Text = "";
                }
                if (refPajak.no_skpd == 1)
                {
                    txtnomorSKPD.Text = m_sDinas;
                }
                else
                {
                    txtnomorSKPD.Enabled = false;
                    txtnomorSKPD.Text  = "";
                }
                if (refPajak.npwp_rekanan == 1)
                {
                    txtnomorPokokWajibPajakRekanan.Text = m_sDinas;
                }
                else
                {
                    txtnomorPokokWajibPajakRekanan.Text = "";
                    txtnomorPokokWajibPajakRekanan.Enabled = false;
                }
                if (refPajak.wp_op == 1)
                {
                    txtnomorPokokWajibPajakRekanan.Text = "";
                }
                else
                {
                    txtnomorPokokWajibPajakRekanan.Text = "";
                    txtnomorPokokWajibPajakRekanan.Enabled = false;
                }
                txtnomorPokokWajibPajakRekanan.Enabled = refPajak.npwp_rekanan == 1;
                if (txtnomorPokokWajibPajakRekanan.Enabled == false)
                {
                    txtnomorPokokWajibPajakRekanan.Text = "";
                }
           //     txtnikRekanan.Enabled = refPajak.nik_rekanan==1;
                txtnomorFakturPajak.Enabled = refPajak.nomor_faktur == 1;
                if (txtnomorFakturPajak.Enabled == false)
                {
                    txtnomorFakturPajak.Text = "";
                }
            }
        }
        private void InquiriNPWP()
        {
            InquiryNPWPRequest requestInquiry;


            requestInquiry = new InquiryNPWPRequest();
            if (ctrlPotongan1.GetKodeMap().ToString().Length == 0)
            {
                    MessageBox.Show("Kesalahan Kode Map");
               }
                requestInquiry.kodeMap = ctrlPotongan1.GetKodeMap().ToString();
                requestInquiry.kodeSetor = ctrlKodeSetor1.GetKode();
                requestInquiry.nomorPokokWajibPajak = "001496827018000";// m_sNPWP.Trim();
                if (requestInquiry.kodeMap == "" || requestInquiry.kodeSetor == "" || m_sNPWP == "")
                {
                    MessageBox.Show("Data Inquiry NPWP kurang lengkap ");

                }
                else
                {
                   InQuiryNPWP(requestInquiry);
                }
            
        }

        private DataInquiriesNPWPResponseEx InQuiryNPWP(InquiryNPWPRequest requestInquiry)
        {

            string url = "";//
            DataInquiriesNPWPResponseEx resp = new DataInquiriesNPWPResponseEx();



            url = GlobalVar.BANK_URL + "InquiryNPWP";
            //url = "http://36.92.240.142:8080/InquiryRekening";



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
                request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();


                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                resp = JsonConvert.DeserializeObject<DataInquiriesNPWPResponseEx>(responseData);

                if (resp.error_kode != "00")
                {
                    string message = requestInquiry.nomorPokokWajibPajak + ", " +
                                     requestInquiry.kodeMap + " dan " + requestInquiry.kodeSetor;

                    message = message + "-> " + resp.message;
                    MessageBox.Show(message);
                }
                else
                {
                txtnamawajibpajak.Text=resp.namaWajibPajak ;
                txtalamatWajibPajak.Text = resp.alamatWajibPajak;
                txtkota.Text = resp.kota;

                }
                return resp;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
        }

        private bool CekInput()
        {
            if (mRefPajak != null)
            {

             
                if (mRefPajak.npwp_lain == 0)
                {
                    txtnomorPokokWajibPajakPenyetor.Text = m_sNPWP;

                }
                if (DataFormat.GetDecimal(txtJumlah.Text) == 0)
                {
                    MessageBox.Show("Jum;ah masih kosong");
                    return false;
                }
                ////if (mRefPajak.npwp_lain == 2)
                ////{
                ////    if (txtnomorPokokWajibPajakPenyetor.Text == m_sNPWP)
                ////    {
                ////        MessageBox.Show("NPWP penyetor harus beda");
                ////        return false;
                ////    }

                ////}

                ////if (mRefPajak.npwp_nol == 0)
                ////{
                ////    if (m_sNPWP.Length == 0)
                ////    {
                ////        MessageBox.Show("Kode Map dan setor ini harus ber NPWP");
                ////        return false;
                ////    }
                    

                ////}

                ////if (mRefPajak.butuh_nop == 1)
                ////{
                ////    if (txtnomorObjekPajak.Text.Length == 0)
                ////    {
                ////        MessageBox.Show("Nomor Objek Pajak harus diisi");
                ////        return false;
                ////    }
                ////}

                ////if (mRefPajak.butuh_nosk == 1)
                ////{
                ////    if (txtnomorSk.Text.Length == 0)
                ////    {
                ////        MessageBox.Show("Nomor SK harus diisi");
                ////        return false;
                ////    }
                ////}
                ////else
                ////{

                ////    if (txtnomorSk.Text.Length >0)
                ////    {
                ////        MessageBox.Show("Nomor SK tidak harus diisi");
                ////        return false;
                ////    }
                ////}
              
             

                ////if (mRefPajak.nik_rekanan == 1)
                ////{
                ////    if (txtnikRekanan.Text.Length == 0)
                ////    {
                ////        MessageBox.Show("NIK Rekenan harus  diisi");
                ////        return false;
                ////    }
                ////}
                ////else
                ////{
                ////    if (txtnikRekanan.Text.Length> 0)
                ////    {
                ////        MessageBox.Show("NIK Rekenan tidak perlu diisi");
                ////        return false;
                ////    }
                ////}

                ////if (mRefPajak.nomor_faktur == 1)
                ////{
                ////    if (txtnomorFakturPajak.Text.Length == 0)
                ////    {
                ////        MessageBox.Show("Nomor faktur harus  diisi");
                ////        return false;
                ////    }
                ////}
                ////else
                ////{
                ////    if (txtnomorFakturPajak.Text.Length> 0)
                ////    {
                ////        MessageBox.Show("Nomor faktur harus tidak  diisi");
                ////        return false;
                ////    }
                ////}
                ////if (mRefPajak.npwp_rekanan == 1)
                ////{
                ////    if (txtnomorPokokWajibPajakRekanan.Text.Length == 0)
                ////    {
                ////        MessageBox.Show("NPWP Rekenan belum diisi.");
                ////        return false;
                ////    }
                ////}
                ////else
                ////{
                ////    {
                ////        if (txtnomorPokokWajibPajakRekanan.Text.Length > 0)
                ////        {
                ////            MessageBox.Show("NPWP Rekenan tidak perlu diisi .");
                ////            return false;
                ////        }
                ////    }
                ////}
                ////// txtnomorPokokWajibPajakPenyetor.Enabled = refPajak.npwp_rekanan == 1;
                //////if(refPajak)
                //////txtnomorPokokWajibPajakPenyetor

             
            }
            return true;
        }
        private void txtnomorPokokWajibPajakPenyetor_TextChanged(object sender, EventArgs e)
        {
            int len = 0;
            label21.Text = "15 digit.( " + txtnomorPokokWajibPajakPenyetor.Text.Length.ToString() + ")";
            if (txtnomorPokokWajibPajakPenyetor.Text.Length == 15)
            {
                label21.Text = label21.Text + "  OK";

            }
            else
            {
                label21.Text = label21.Text.Replace("OK", "");
            }
        }

        private void txtnomorPokokWajibPajakPenyetor_Leave(object sender, EventArgs e)
        {
            if (mRefPajak != null)
            {
                if (mRefPajak.npwp_lain == 0)
                {
                    if (txtnomorPokokWajibPajakPenyetor.Text != m_sNPWP)
                    {
                        MessageBox.Show("Nomor Wajib Pajak Penteor harus sana dg NPMWP");

                    } 
                }
            }
        }

        private void cmdCekNPWP_Click(object sender, EventArgs e)
        {
            InquiriNPWP();
        }

        private void txttahunPajak_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtnomorSk_TextChanged(object sender, EventArgs e)
        {
            int len = 0;
            label13.Text = "15 digit.( " + txtnomorSk.Text.Length.ToString() +")";
            if (txtnomorSk.Text.Length == 15)
            {
                label13.Text = label13.Text + "  OK";
                
            }
            else
            {
                label13.Text = label13.Text.Replace("OK", "");
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtnomorObjekPajak_TextChanged(object sender, EventArgs e)
        {
            int len = 0;
            label20.Text = "18 digit.( " + txtnomorObjekPajak.Text.Length.ToString() + ")";
            if (txtnomorObjekPajak.Text.Length == 18)
            {
                label20.Text = label20.Text + "  OK";

            }
            else
            {
                label20.Text = label20.Text.Replace("OK", "");
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void txtnomorPokokWajibPajakRekanan_TextChanged(object sender, EventArgs e)
        {
            int len = 0;
            label22.Text = "15 digit.( " + txtnomorPokokWajibPajakRekanan.Text.Length.ToString() + ")";
            if (txtnomorPokokWajibPajakRekanan.Text.Length == 15)
            {
                label22.Text = label22.Text + "  OK";

            }
            else
            {
                label22.Text = label22.Text.Replace("OK", "");
            }
        }

        private void txtnikRekanan_TextChanged(object sender, EventArgs e)
        {
                int len = 0;
                label23.Text = "16 digit.( " + txtnikRekanan.Text.Length.ToString() + ")";
                if (txtnikRekanan.Text.Length == 16)
            {
                label23.Text = label23.Text + "  OK";

            }
                else
                {
                    label23.Text = label23.Text.Replace("OK", "");
                }
        }

        private void txtnomorFakturPajak_TextChanged(object sender, EventArgs e)
        {
            int len = 0;
            label24.Text = "18 digit.( " + txtnomorFakturPajak.Text.Length.ToString() + ")";
            if (txtnomorFakturPajak.Text.Length == 18)
            {
                label24.Text = label24.Text + "  OK";

            }
            else
            {
                label24.Text = label24.Text.Replace("OK","");
            }
        }

        private void cmdInquiryMPN_Click(object sender, EventArgs e)
        {
            InquiryMON();
        }

        private void cmdTutup_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            if (CekInput())
            {
                if (CreateIdBilling() == true)
                {
                    InquiryMON();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<PotonganSPP> lstPot = new List<PotonganSPP>();

             frmReportIdBilling fReport = new frmReportIdBilling();

             fReport.NoReference = m_IDBILLING;
              fReport.ShowDialog();


            
        }

        private void txtJumlah_TextChanged(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {
            
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {

            NPWPDinas = ctrlDinas1.GetBendaharaPengeluaran(DateTime.Now.Date).NPWP.Replace(".", "").Replace("-", "").Replace(" ", ""); ;

            Dinas = ctrlDinas1.GetID().ToString();
        }


    }
}
