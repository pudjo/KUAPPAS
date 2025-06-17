using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO.SP2DOnLine._511;

using DTO;
using DTO.Bendahara;
using BP;
using BP.Bendahara;
using Formatting;
using Syncfusion.Pdf;
using KUAPPAS.SP2DOnline;

using DTO.SP2DOnLine;
using System.Net;
using Newtonsoft.Json;
using System.IO;

using DTO.SP2DOnLine._511;
using KUAPPAS.Bendahara;


namespace KUAPPAS.SP2DOnline
{
    public partial class frmRespond511 : Form
    {
        private long mNoUrut; 
        public frmRespond511()
        {
            InitializeComponent();
        }

        private void frmRespond511_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Data Pembayaran SP2D.");

            gridMPN.FormatHeader();
            gridNonMPN.FormatHeader();
        }
        public void TampilkanDataTrxSP2DOnline(string sNoSP2D, long noUrut)
        {
            mNoUrut = noUrut;
            RequestSP2d requestInquiry = new RequestSP2d();
            requestInquiry.nomorSP2D = sNoSP2D;


            string url = "";//
            DataInformasi511ResponseEx resp = new DataInformasi511ResponseEx();


            // url = "https://localhost:7139/InformasiSP2D511";
            url = GlobalVar.BANK_URL + "InformasiSP2D511";



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
                resp = JsonConvert.DeserializeObject<DataInformasi511ResponseEx>(responseData);

                if (resp.error_kode == "00" || resp.error_kode == "11")
                {



                    Setdata(resp);
                }
                else
                {
                    MessageBox.Show(resp.message);
                }
                // return resp;
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool Setdata(DataInformasi511ResponseEx data)
        {
            try
            {
                txtMessage.Text = data.message;
                txtNoSP2D.Text = data.nomorSP2D;
                txtNoSPM.Text = data.nomorSPM;
                txtRefNo.Text = data.referenceNo;
                txtNote.Text = data.notes;
                txtResponCode.Text = data.error_kode;
                txtMessageDetail.Text = data.messageDetail;
                txtNominal.Text = data.jumlahNominalTransaksi;
                txtJumlahDibayar.Text = data.jumlahDibayar;
                txtJumlahMPN.Text = data.jumlahPotonganMpn;
                txtjumlahNonMPN.Text = data.jumlahPotonganNonMpn;
                txtTanggalTrx.Text = data.tanggalTransaksi;
                if (data.pengirim != null)
                {
                    txtNoRekening.Text = data.pengirim.noRekening;
                    txtNamaOPD.Text = data.pengirim.namaOpd;
                    txtKodeOPD.Text = data.pengirim.kodeOpd;
                }
                if (data.penerima != null)
                {
                    txtNoRekeningPenerima.Text = data.penerima.noRekening;
                    txtNamaPenerima.Text = data.penerima.namaPenerima;
                    txtNamaBank.Text = data.penerima.namaBank;
                    txtKodeBank.Text = data.penerima.kodeBank;
                    txtNPWP.Text = data.penerima.npwp;
                }
                foreach (DetailMpn511 pot in data.detailPotonganMpn)
                {
                    string[] row = { pot.idBilling,pot.referenceNo,pot.keteranganPotongan,
                                     pot.nominalPotongan,pot.ntpn,pot.responseCode,pot.messageDetail};


                    
                    gridMPN.Rows.Add(row);

                }
                foreach (DetailPotonganNonMpn p in data.detailPotonganNonMpn)
                {
                    string[] row = {
                      p.kodeMap ,p.keteranganKodeMap,p.nominalPotongan,p.responseCode,p.messageDetail };



                    gridNonMPN.Rows.Add(row);

                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cmdRiwayat_Click(object sender, EventArgs e)
        {
            frmRiwayat fRiwayat = new frmRiwayat();
            fRiwayat.GetRiwayat(mNoUrut);
            fRiwayat.Show();
        }
    }
}
