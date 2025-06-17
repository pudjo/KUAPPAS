using BP;
using BP.Bendahara;
using DTO;
using DTO.Bendahara;
using Formatting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace KUAPPAS.Bendahara
{
    public partial class frmPenerimaanSPM : Form
    {
        private int m_iJenis;
        private int m_iSKPD;
        List<SPP> m_lstSPP;

        public frmPenerimaanSPM()
        {
            InitializeComponent();
            m_iJenis = 1;
        }

        public int Jenis
        {
            set
            {
                m_iJenis = value;

            }
        }
        private void frmPenerimaanSPM_Load(object sender, EventArgs e)
        {

            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            gridTerima.FormatHeader();
            ctrlPencarian1.setGrid(ref gridTerima);

            ctrlTanggal2.Tanggal = DateTime.Now.Date;
            Timex.Format = DateTimePickerFormat.Time;
            Timex.ShowUpDown = true;
            
            if (m_iJenis == 2)
            {
                this.Height = 465;
                ctrlHeader1.SetCaption("Penerimaan SP2D");
                label2.Text = "Nomor SP2D";
                label6.Visible = true;
                txtNoUrut.Visible = true;
            }
            else
            {
                this.Height = 677;
                ctrlHeader1.SetCaption("Penerimaan SPM");
            }

        }

        private void cmdBaru_Click(object sender, EventArgs e)
        {
            SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
            int maxReg = oLogic.GetMaxNoRegSPM();
            if (m_iJenis == 1)
                maxReg = maxReg = oLogic.GetMaxNoUrutKasda();
            else
                maxReg = maxReg = oLogic.GetMaxNoUrutKasda();

            txtNoUrut.Text = (maxReg + 1).ToString();
               ctrlSKPD1.Clear();
 
            txtJumlah.Text="0";
            txtKeterangan.Text="";
            Timex.Value = DateTime.Now;
            ctrlSPP1.Clear();
          
        }

        private void ctrlSKPD1_OnChanged(int pID)
        {
            m_iSKPD = pID;
            if(m_iJenis ==1)
                ctrlSPP1.Create(m_iSKPD, 0, -1, 1);
            else
                ctrlSPP1.Create(m_iSKPD, 0, -1,3);


        }

        private void ctrlSPP1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlSPP1_OnChanged(long pID)
        {
            try
            {
                SPP oSPP = new SPP();
                oSPP = ctrlSPP1.GetSPP();
                if (oSPP != null )
                {
                    decimal jumlahdetail = 0;
                    if (oSPP.Jenis > 0)
                    {
                        foreach (SPPRekening sr in oSPP.Rekenings)
                        {
                            jumlahdetail = jumlahdetail + sr.Jumlah;
                        }
                        if (jumlahdetail != oSPP.Jumlah)
                        {
                            MessageBox.Show("Ada permesalahan data SPM no " + oSPP.NoSPM + " ada masalah jumlah. Hubungi admin");
                            cmdSimpan.Enabled = false;
                            return;

                        }
                    }
                    txtJumlah.Text = oSPP.Jumlah.ToRupiahInReport();
                    txtKeterangan.Text = oSPP.Keterangan;
                    
                    cmdSimpan.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
            long nUrut = ctrlSPP1.GetID();
            
            int noreg = DataFormat.GetInteger(txtNoUrut.Text);
            if (m_iJenis != 1)
            {
                if (noreg == 0)
                {
                    MessageBox.Show("Nomor belum diisi atau salah format");
                }
            }

            if (oLogic.SPMdiTerima(nUrut, noreg, m_iJenis, ctrlTanggal2.Tanggal +
                Timex.Value.TimeOfDay ))
                {
                    MessageBox.Show("Data Sudah disimpan..");
                    if (m_iJenis == 1)
                      CatatSilakan();
                    cmdSimpan.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Data GAGAL disimpan..");
                }
            
        }

        private void CatatSilakan()
        {
            try
            {
                SPP oSPP = new SPP();
                oSPP = ctrlSPP1.GetSPP();
                SKPD oSKPD = new SKPD();
                SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);

                oSKPD = oSKPDLogic.GetByID(m_iSKPD);

                string Cadena = "https://api-silakan.seibutomasua.xyz/api/sp2d";

                //string today = DateTime.Now.Year.ToString() + "-" +
                  //              DateTime.Now.Month.ToString() + "-" +
                    //            DateTime.Now.Day.ToString();
                string today = ctrlTanggal2.Tanggal.Year.ToString() + "-" +
                                ctrlTanggal2.Tanggal.Month.ToString() + "-" +
                                ctrlTanggal2.Tanggal.Day.ToString();
                
                //sDate = "'" & Year(Now) & "-" & month(Now) & "-" & Day(Now) & "'"//

                Silakan oSilakan = new Silakan();
                oSilakan.kantor_id = oSKPD.IDKantor;
                oSilakan.no_sp2d = oSPP.NoSP2D;
                oSilakan.no_spm = oSPP.NoSPM;
                oSilakan.uraian = "Kirim dari SIKUAT";
                oSilakan.keterangan = oSPP.Keterangan;
                oSilakan.jumlah = oSPP.Jumlah;
                oSilakan.kode_reg = oSPP.NoUrut.ToString();
                oSilakan.tgl_terima = today;
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(oSilakan);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
                    HttpContent jsonContent = new StringContent(JsonConvert.SerializeObject(oSilakan), System.Text.Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "3|ajVZEgNjgFuSDy1c3edVPhFLtoILOEAf8A1DgmoY");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    try
                    {
                        var result = client.PostAsync(Cadena, stringContent).Result;
                        if (result.IsSuccessStatusCode == true)
                        {
                            MessageBox.Show("Catat Silakan berhasil");
                        }
                    }
                    catch (AggregateException err)
                    {
                        foreach (var errInner in err.InnerExceptions)
                        {
                            Console.WriteLine(errInner); //this will call ToString() on the inner execption and get you message, stacktrace and you could perhaps drill down further into the inner exception of it if necessary 
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtNoUrut_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdPanggil_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tanggalAwal = ctrlTanggal1.Tanggal;
                DateTime tanggalAkhir = ctrlTanggal1.Tanggal;
                //MessageBox.Show("Before load");

                LoadData();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void LoadData()
        {
            try
            {


                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);

                m_lstSPP = new List<SPP>();

                m_lstSPP = oLogic.GetPenerimaanSPM(ctrlTanggal1.Tanggal.ToSQLFormat2Angka());


                // MessageBox.Show("end toDatabase");

                gridTerima.Rows.Clear();

                int iRow = 0;
                decimal cJumlah = 0L;
                string sAction = "";
                string Status = "";

                int i;
                i = 0;
                foreach (SPP spp in m_lstSPP)
                {

                    if (spp.TanggalTerimaSPM.Date == ctrlTanggal1.Tanggal)
                    {
                        string rupiah;
                        if (spp.Jumlah > 0)
                        {
                            rupiah = spp.Jumlah.ToRupiahInReport();
                        }
                        else
                        {
                            rupiah = spp.Jumlah.ToString();
                        }

                        string[] row = { (i++).ToString(), spp.NoBpp.ToString(), spp.NamaDinas, spp.NoSPM, spp.Keterangan, rupiah, spp.TanggalTerimaSPM.ToTanggalIndonesiaLengkap(true) };
                        gridTerima.Rows.Add(row);

                        if (spp.Status == 1)
                            gridTerima.Rows[gridTerima.Rows.Count - 1].DefaultCellStyle.BackColor = Color.AliceBlue;
                        if (spp.Status == 2)
                            gridTerima.Rows[gridTerima.Rows.Count - 1].DefaultCellStyle.BackColor = Color.AliceBlue;

                        if (spp.Status == 3)
                            gridTerima.Rows[gridTerima.Rows.Count - 1].DefaultCellStyle.BackColor = Color.AntiqueWhite;// Red;

                        if (spp.Status == 4)


                            gridTerima.Rows[gridTerima.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSalmon;// PaleVioletRed;// IndianRed;// Red;







                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void cmdHariIni_Click(object sender, EventArgs e)
        {
            ctrlTanggal1.Tanggal = DateTime.Now.Date;
        }

        private string BuatFile()
        {

            string sRet = "";
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.Filter = "Excel|*.xlsx;*.xls";
            fdlg.Title = "Save an Image File";
            fdlg.ShowDialog();

            fdlg.Title = "Buat File file";
            fdlg.InitialDirectory = @"c:\";

            //fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel|*.xlsx;*.xls";
            fdlg.RestoreDirectory = true;



            sRet = fdlg.FileName;



            return sRet;
        }
        private void KillSpecificExcelFileProcess(string excelFileName)
        {
            var processes = from p in Process.GetProcessesByName("EXCEL")
                            select p;

            foreach (var process in processes)
            {
                if (process.MainWindowTitle == "Microsoft Excel - " + excelFileName)
                    process.Kill();
            }
        }
        private void Export()
        {

            string NamaFile = "";

            try
            {
                string namaFile = BuatFile();
                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Masih Kosong ");
                    return;
                }
                KillSpecificExcelFileProcess(namaFile);

                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                Microsoft.Office.Interop.Excel.Range excelCellrange;
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // Make Excel invisible and disable alerts.
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;

                excelSheet.Name = "Penerimaan SPM";

                // storing header part in Excel  
                for (int i = 1; i < gridTerima.Columns.Count + 1; i++)
                {


                    excelSheet.Cells[1, i] = gridTerima.Columns[i - 1].HeaderText;

                }
                // storing Each row and column value to excel sheet  
                List<int> lstColToCetak = new List<int>();
                for (int i = 0; i < gridTerima.Rows.Count - 1; i++)
                {
                    int c = 0;
                    for (int j = 0; j <= gridTerima.Columns.Count - 1; j++)
                    {
                        if (gridTerima.Columns[j].Visible == true)
                        {
                            ++c;
                            if (gridTerima.Rows[i].Cells[j].Value != null)
                            {
                                string s = "";
                                if (j == 5)
                                {
                                    s = DataFormat.FormatUangReportKeDecimal(gridTerima.Rows[i].Cells[j].Value).ToString("###.##");
                                }
                                else
                                {
                                    s = DataFormat.GetString(gridTerima.Rows[i].Cells[j].Value);
                                }

                                excelSheet.Cells[i + 2, c] = s;



                            }
                        }
                    }

                }


                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1],
                                  excelSheet.Cells[excelSheet.Rows.Count - 1,
                                  excelSheet.Columns.Count - 1]];
                //    excelSheet.Columns.AutoFit();
                excelSheet.Columns.ColumnWidth = 20;
                excelSheet.Columns[2].ColumnWidth = 50;
                excelSheet.Columns[2].WrapText = true;


                excelworkBook.SaveAs(namaFile);
                MessageBox.Show("File sudah disimpan di " + namaFile);


                excelworkBook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke excell" + ex.Message);
            }

        }

        private void cmdExcell_Click(object sender, EventArgs e)
        {
            try
            {
                Export();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ctrlSKPD1_Load(object sender, EventArgs e)
        {

        }
    }

    public class Silakan
    {
        public string no_sp2d {set;get;}
        public string no_spm {set;get;}
        public string tgl_terima{set;get;}
        public int kantor_id{set;get;}
        public string uraian{set;get;}
        public decimal jumlah {set;get;}
        public string keterangan{set;get;}
        public string kode_reg{set;get;}

       
    }
}
