using BP.Bendahara;
using DTO.Bendahara;
using Formatting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;  

namespace KUAPPAS.Bendahara
{
    public partial class frmImportPendapatan : Form
    {
        private Excel.Application xlApp = new Excel.Application();
        private frmStatus _fStatus;
        private string NamaSheet;

        public delegate void UpdateUIDelegate(bool IsDataTampil);
        Thread threadSplash;

        public frmImportPendapatan()
        {
            InitializeComponent();
            _fStatus = new frmStatus();
        }

        private bool BukaFile()
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Select file";
                fdlg.InitialDirectory = @"c:\";

                fdlg.FileName = txtFileName.Text;
                fdlg.Filter = "Excel|*.xlsx;*.xls";
                fdlg.RestoreDirectory = true;


                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtFileName.Text = fdlg.FileName;
                    worksheetsComboBox.Items.Clear();





                    ArgumenThread arg = new ArgumenThread();
                    arg.State = 1;
                    if (_fStatus.IsDisposed == true)
                    {
                        _fStatus = new frmStatus();
                    }
                    _fStatus.Show();
                    backgroundWorker1.RunWorkerAsync(arg);

                }
                // ctrlDinas1.SetI
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan membaca file excell." + ex.Message);

                return false;
            }
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
        private void cmdTampilkam_Click(object sender, EventArgs e)
        {


            UpdateUI(false);
            threadSplash = new Thread(new ThreadStart(StartForm));
            threadSplash.Start();

            if (worksheetsComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Belum dipilih sheet nya");
                return;
            }
            TampilkanData();

        }
        public void StartForm()
        {
            try
            {
                progressBar1.Visible = true;
            }
            catch (Exception)
            {

            }
        }
        private void UpdateUI(bool IsProcessFinished)
        {
            if (IsProcessFinished)
            {

                threadSplash.Abort();


                progressBar1.Visible = false ;

            }
            else
            {
                progressBar1.Visible = true;


            }
        }
        private void TampilkanData()
        {

            KillSpecificExcelFileProcess(txtFileName.Text);


            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex+1];
            Excel.Range xlRange = xlWorksheet.UsedRange;


            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            NamaSheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex + 1].Name;
            gridExcell.Rows.Clear();
            for (int i = 1; i <= rowCount; i++)
            {
                gridExcell.Rows.Add();
                for (int j = 1; j <= colCount; j++)
                {
                    if (j == 5)
                    {
                       // MessageBox.Show(xlRange.Cells[i, j].Value2.ToString());
                       // MessageBox.Show(xlRange.Cells[i, j].Value.ToString());

                    }
                     if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null){

                         gridExcell.Rows[i - 1].Cells[j - 1].Value = xlRange.Cells[i, j].Value.ToString();
                    }
                 
                }
            }
            //cleanup


            GC.Collect();
            GC.WaitForPendingFinalizers();

            xlWorkbook.Close();


            //quit and release
            xlApp.Quit();
            //Marshal.ReleaseComObject(xlApp);
            UpdateUI(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (BukaFile() == true)
            {

            }
        }

        private void frmImportPendapatan_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Import Data Pendapatan.");
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ArgumenThread arg = (ArgumenThread)e.Result;
            if (arg != null)
            {
                if (arg.State == 1)
                {
                    worksheetsComboBox.Items.Clear();
                    foreach (string s in arg.ListName)
                    {
                        worksheetsComboBox.Items.Add(s);
                    }
                }
            }
            _fStatus.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ArgumenThread arg = (ArgumenThread)e.Argument;
                xlApp.Visible = false;
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
                
                foreach (Excel.Worksheet s in xlWorkbook.Sheets)
                {

                        arg.ListName.Add(s.Name);
                }
                e.Result = arg;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan baca file excell" + ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            try
            {
                BKULogic oBKULogic = new BKULogic(GlobalVar.TahunAnggaran);
                int iddinas = ctrlSKPD1.GetID();
                if (oBKULogic.BersihkanBKUPendapatanImport(iddinas,NamaSheet))
                {
                    STSLogic oLogic = new STSLogic(GlobalVar.TahunAnggaran);
                    oLogic.BersihkanBKUPendapatanImport(iddinas, NamaSheet);

                    STS oSTS = new STS();
                    foreach (DataGridViewRow row in gridExcell.Rows)
                    {
                        if (row.Cells[3].Value != null)
                        {

                            oSTS = new STS();
                            oSTS.IDDinas = iddinas;
                            oSTS.KodeUK = 0;
                            oSTS.NoUrut = 0;

                            oSTS.Jenis = DataFormat.GetInteger(row.Cells[0].Value);


                            oSTS.Keterangan = DataFormat.GetString(row.Cells[2].Value); ;
                            oSTS.KodeKategori = iddinas.ToString().ToKodeKategori();
                            oSTS.KodeUrusan = iddinas.ToString().ToKodeUrusan();
                            oSTS.KodeSKPD = iddinas.ToString().ToKodeSKPD();

                            oSTS.NoSTS = DataFormat.GetString(row.Cells[3].Value);
                            oSTS.Penyetor = NamaSheet;
                            oSTS.NoSKR = 0;
                            oSTS.Bank = oSTS.Jenis = DataFormat.GetInteger(row.Cells[0].Value);


                            oSTS.TanggalSTS = DataFormat.GetDate(row.Cells[4].Value);
                            oSTS.NamaBank = "";
                            oSTS.NoRekening = "";
                            oSTS.Alamat = "";
                            oSTS.NoBukti = DataFormat.GetString(row.Cells[3].Value);
                            oSTS.JabatanPenyetor = "";
                            oSTS.InstitusiPenyetor = 0;
                            oSTS.NPWP = "";
                            oSTS.Tahun = GlobalVar.TahunAnggaran;
                            oSTS.NamaFile = NamaSheet;

                            oSTS.Rekenings = new List<STSRekening>();
                            //List<STSRekening> lstSTSRekening = new List<STSRekening>();
                            decimal cJumlah = 0L;

                            STSRekening sr = new STSRekening();

                            sr.IDRekening = DataFormat.GetLong(DataFormat.GetString(row.Cells[5].Value).Replace(".", ""));
                            
                            sr.Jumlah = DataFormat.GetDecimal(row.Cells[6].Value);
                            if (sr.IDRekening == 0)
                            {
                                MessageBox.Show("Kode Rekening Pendapatan belum diisi pada baris " + row.Index.ToString() );

                            }

                            cJumlah = cJumlah + sr.Jumlah;
                            oSTS.Rekenings.Add(sr);

                            oSTS.Jumlah = cJumlah;
                            long nourutHasil = 0;
                            nourutHasil = oLogic.Simpan(oSTS);

                            if ((nourutHasil == 0 && oSTS.NoUrut == 0)  )
                            {
                                MessageBox.Show(oLogic.LastError());
                                oLogic.Simpan(oSTS);
                            }
                            else
                            {


                            }
                        }
                    }

                        string sPesan = "Penyimpanan Berhasil.";
                        MessageBox.Show(sPesan);
                    
                }
                else
                {
                    MessageBox.Show(oBKULogic.LastError());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan Import Pendapatan..");
            }

        }

        private void frmImportPendapatan_FormClosed(object sender, FormClosedEventArgs e)
        {
            Marshal.ReleaseComObject(xlApp);
        }

        private void cmdCek_Click(object sender, EventArgs e)
        {
            frmShowDataImport fShow = new frmShowDataImport();
             int iddinas = ctrlSKPD1.GetID(); ;
             fShow.dinas = iddinas;
            fShow.Sheet = NamaSheet;
            if (fShow.LoadData()>0)
            {
                fShow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Belum ada data dengan nama sheet tersebut");
            }
        }
    }
}
