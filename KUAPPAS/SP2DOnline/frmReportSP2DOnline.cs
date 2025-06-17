using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SP2DOnline;
namespace KUAPPAS.SP2DOnline
{
    public partial class frmReportSP2DOnline : ChildForm
    {
        public frmReportSP2DOnline()
        {
            InitializeComponent();
        }

        private void cmdProses_Click(object sender, EventArgs e)
        {
            GenerateReportRequest request= new GenerateReportRequest();
            request.noReferensi = "";
            request.tanggalReportAwal = tanggalAwal.Value.ToString("yyyy-MM-dd HH:mm:ss");
            request.tanggalReportAkhir= tanggalAkhir.Value.ToString("yyyy-MM-dd HH:mm:ss");
            request.jenisReport = "ReportBPN";
            request.formatReport = "pdf";
            SP2DOnlineAPI oAPI = new SP2DOnlineAPI();
            string sTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            GenerateReportRespon oResp = oAPI.GenerateReport(request, sTime);



            
            if (oResp.response_code != "00")
            {
                MessageBox.Show(oResp.message);
            }
            else
            {

                //Console.WriteLine resp

                frmDataCreateIdilling fData = new frmDataCreateIdilling();
                //fData.SetData(oResp.data);
                fData.Show();
                
            }
                    
        }

        private void frmReportSP2DOnline_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Generate Report ");
           
        }
    }
}
