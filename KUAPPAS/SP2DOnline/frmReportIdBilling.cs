using DTO.SP2DOnLine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Web.Hosting;

namespace KUAPPAS.SP2DOnline
{
    public partial class frmReportIdBilling : Form
    {
        public frmReportIdBilling()
        {
            InitializeComponent();
        }
        public string NoReference
        {
            set { txtNoReference.Text = value;}
        }
        private void frmReportIdBilling_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Generate Report Id Billing");
            this.Text = "Generate Report Id Billing";
            cmbJenisReport.Items.Add("ReportBPN");
            cmbJenisReport.Items.Add("ReportCreateBilling");

            cmbFormat.Items.Add("pdf");
            cmbFormat.Items.Add("");
        }

        public void SetData(DataGenerateReportResponseEx data)
        {
            txtExpiredDate.Text = data.tanggalExpiredLink;
            txtLinkLaporan.Text = data.linkDownload;

        }

        private void cmdGenerateReport_Click(object sender, EventArgs e)
        {
            if (txtNoReference.Text.Trim().Length == 0)
            {
                MessageBox.Show("No Reference belum diisi..");
                    return;

            }


            if (cmbJenisReport.Text.Trim().Length == 0)
            {
                MessageBox.Show("Jenis Laporan belum dipilih..");
                return;

            }
      

            if (cmbFormat.Text.Trim().Length == 0)
            {
                MessageBox.Show("Format Laporan belum dipilih..");
                return;

            }

                GenerateReportRequest grrequest = new GenerateReportRequest();
            grrequest.noReferensi = txtNoReference.Text.Trim();
            grrequest.jenisReport = cmbJenisReport.Text.Trim();
            grrequest.tanggalReportAkhir = dtAkhir.Value.ToString("yyyy-MM-ddHH:mm:ss");
            grrequest.tanggalReportAwal = dtAwal.Value.ToString("yyyy-MM-ddHH:mm:ss");
            grrequest.formatReport = cmbFormat.Text.Trim();
            
            string url = "";//
            DataGenerateReportResponseEx resp = new DataGenerateReportResponseEx();



            url = GlobalVar.BANK_URL + "GenerateReport";
            //url = "http://36.92.240.142:8080/MPN";



            WebResponse objResponse = null;
            WebRequest request = null;
            try
            {
                request = WebRequest.Create(url);
                request.Method = "POST";


                //  url = url + "?sandiBank=" + ctrlDaftarBank1.Kode.Trim() + "&nomorRekening=" + txtNoRekening.Text.Replace(".", "");
                string JsonData = JsonConvert.SerializeObject(grrequest);
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
                resp = JsonConvert.DeserializeObject<DataGenerateReportResponseEx>(responseData);

                if (resp.error_kode != "00")
                {


                    string message;
                    message = resp.message;
                    MessageBox.Show(message);
                }
                else
                {

                   
                    string message;
                    SetData(resp);
                    //if (resp.ntpn.Trim().Length > 0)
                    //{

                    //    message = "Id Billing no " + resp.idBilling + "   SUDAH dibayar ";

                    //}
                    //else
                    //{
                    //    message = "Id Billing no " + resp.idBilling + "   BELUM dibayar ";
                    //}
                 


                }
         


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
              

            }

        }

        private async void cmdDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = @"C:\Temp\Test.pdf";
                string PDFUrl = txtLinkLaporan.Text;

                WebClient client = new WebClient();

                client.DownloadFile(PDFUrl, FileName);

                FileInfo PDFFile = new FileInfo(FileName);


                pdfViewer pV = new pdfViewer();


                pV.Document = Path.GetFullPath(FileName);

                pV.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //using (HttpClient client = new HttpClient())
            //{
            //    Uri uri = new Uri(txtLinkLaporan.Text);
            //    var response = await client.GetAsync(uri);
            //    using (var fs = new FileStream(
            //        HostingEnvironment.MapPath(string.Format("~/{0}.pdf", txtNoReference)),
            //        FileMode.CreateNew))
            //    {
            //        await response.Content.CopyToAsync(fs);
            //    }

            //}
        }
    }
}
