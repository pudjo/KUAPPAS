using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Formatting;
using DTO;
using BP;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlTypes;

namespace KUAPPAS
{

    public partial class frmImportAPBD : Form
    {
        private frmStatus _fStatus;
        private List<string> _shetNames;
        private int mprofile;

        private static Excel.Workbook MyBook = null;
        private static Excel.Application MyApp = null;
        private static Excel.Worksheet MySheet = null;
        private int m_mode;
        private List<Unit> lstUnit;
        public frmImportAPBD()
        {
            InitializeComponent();
            _fStatus = new frmStatus();
            mprofile = 3;
            m_mode = 1;
            lstUnit = new List<Unit>();

        }

        private void button1_Click(object sender, EventArgs e)
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
            // ctrlDinas1.SetID(0);

        }
        public int Mode { set { m_mode = value; } }
        private void ShowSheet()
        {
            try
            {


                MyApp = new Excel.Application();
                MyApp.Visible = false;
                MyBook = MyApp.Workbooks.Open(txtFileName.Text);
                MySheet = (Excel.Worksheet)MyBook.Sheets[1];


                //   if (tables != null && tables.Rows.Count > 0)
                //  {/

                // foreach (DataRow row in tables.Rows)
                // {
                //    arg.ListName.Add(row["TABLE_NAME"].ToString());
                // }
                // }

                //foreach (Excel.Worksheet s in MyBook.Sheets)
                // {

                // worksheetsComboBox.Items.Add(s.Name);
                //}
                // Explicit cast is not required here
                // lastRow = MySheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row; 

                //System.Data.OleDb.OleDbConnection MyCnn;
                //System.Data.DataSet DSet;
                //System.Data.OleDb.OleDbDataAdapter MyCmd;


                ////MyCnn = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtFileName.Text + " ;Extended Properties=Excel 8.0;");
                //string sourceConnectionString = String.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES;\'", txtFileName.Text);




                ////if (Path.GetExtension(path).ToLower().Trim() == ".xls" && Environment.Is64BitOperatingSystem == false)
                //if (rb1.Checked == true)
                //{
                //    sourceConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtFileName.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                //}
                //else
                //{

                //    if (rb1.Checked == true)
                //        sourceConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName.Text + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                //}


                ////string sourceConnectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES'", txtFileName.Text);
                //MyCnn = new System.Data.OleDb.OleDbConnection(sourceConnectionString);
                //MyCnn.Open();


                //DataTable tables = MyCnn.GetSchema("Tables", new String[] { null, null, null, "TABLE" });
                //MyCnn.Dispose();
                //Add each table name to the combo box
                //if (tables != null && tables.Rows.Count > 0)
                //{

                //    foreach (DataRow row in tables.Rows)
                //    {
                //        _shetNames.Add(row["TABLE_NAME"].ToString());

                //        //worksheetsComboBox.Items.Add(row["TABLE_NAME"].ToString());
                //    }
                //}

                //MyCmd = new System.Data.OleDb.OleDbDataAdapter("select * from " + txtSheet.Text.Trim(), MyCnn);

                //MyCmd.TableMappings.Add("Table", "TestTable");

                //DSet = new System.Data.DataSet();
                //MyCmd.Fill(DSet);
                //worksheetsComboBox.DataSource = DSet.Tables[0];
                //MyCnn.Close();
                //  _fStatus.Close();
                backgroundWorker1.ReportProgress(1);

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }
        }
        private void importProgram()
        {
            try
            {
         
                int idprogramlama = 0;
                TProgramAPBD orpgramLama = new TProgramAPBD();
                for (int i = 0; i < gridData2.Rows.Count; i++)
                {
                    if (gridData2.Rows[i].Cells[2] != null) { 
                    TProgramAPBD o = GetProgram22(i);
                    orpgramLama = new TProgramAPBD();
                    if (o != null && o.IDProgram != idprogramlama)
                    {
                        TProgramAPBDLogic oLogic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran, mprofile);
                        o.Outcome = "";
                        o.Keluaran = "";
                        o.RPJMD = 0;
                        o.PrioritasNasional = 0;
                        o.Target = 0;



                        o.Jenis = 3;

                        if (orpgramLama.IDDinas != o.IDDinas &&
                            orpgramLama.IDProgram != o.IDProgram)
                        {
                            oLogic.Simpan(o);

                            orpgramLama = o;
                            idprogramlama = o.IDProgram;
                        }
                    }

                    }
                }
                MessageBox.Show("Selesai Import Program");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        private void importKegiatan()
        {
            try
            {
                //string sSalahDinas = "";

                //if (ctrlDinas1.GetID() == 0)
                //{
                //    MessageBox.Show("Pemilihan dinas digunakan untuk mempertegas dinas dari data. Sila pilih.");
                //    return;

                //}
                //int idDinas = 0;
                //idDinas = ctrlDinas1.GetID();
                int idkegiatanlama = 0;
                for (int i = 0; i < gridData2.Rows.Count; i++)
                {
                    TKegiatanAPBD o = GetKegiatan22(i);
                    if (o != null && o.IDKegiatan != idkegiatanlama)
                    {
                        TKegiatanAPBDLogic oLogic = new TKegiatanAPBDLogic(2025, mprofile);
                        o.Outcome = "";
                        o.Keluaran = "";


                        o.Lokasi = "";
                        o.Kondisi = "";
                        o.Waktu = "";
                        //paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                        o.Keterangan = "";
                        o.AnggaranTahunDepan = 0;
                        o.AnggaranTahunLalu = 0;
                        o.KelompokSasaran = "";

                        o.SumberDana = "";
                        o.AlasanPerubahan = "";

                    
                        o.Jenis = 3;

                        o.Pagu = 0;
                        o.PaguABT = 0;
                        o.Outcome = "";
                        o.Keluaran = "";
                        o.TanggalPembahasan = new DateTime(2023, 12, 31);



                        oLogic.Simpan(o);
                        idkegiatanlama = o.IDKegiatan;

                    }
                }
                MessageBox.Show("Selesai Import Kegiatan");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void TampilkanRekenigDJPK()
        {
            int lastrow = 0;
            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex + 1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount;
                rowCount = xlWorksheet.Rows.Count;

                gridRekening.Rows.Clear();
              
                Cursor.Current = Cursors.WaitCursor;
                  
                int row = 0;
                gridRekening.Rows.Add();
                int kodeuk = 0;
                for (row = 1; row <= xlRange.Rows.Count; row++)
                {

                    if (xlRange.Cells[row, 1].Value != null)
                    {
                        
                          gridRekening.Rows.Add();
                        
                           gridRekening.Rows[row].Cells[0].Value = xlRange.Cells[row, 1].Value.ToString();
                           gridRekening.Rows[row].Cells[1].Value = xlRange.Cells[row, 2].Value.ToString().Replace("\t", "").Replace("\r", "").Replace("\n", "");
                         //  gridRekening.Rows[row].Cells[2].Value = xlRange.Cells[row, 3].Value.ToString();
                        
                            lastrow = row;
                        
                            
                          
                      }

                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);//"Kesalahan membuka fileexcell. Jika file dalam keadaan dibuka, sila tutup terlebih dahulu");
            }
        

        }
        private void TampilkanDinkes()
        {
            int lastrow = 0;
            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex + 1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount;
                rowCount = xlWorksheet.Rows.Count;

                gridKesehatan.Rows.Clear();
              
                Cursor.Current = Cursors.WaitCursor;
                  
                int row = 0;
                gridKesehatan.Rows.Add();
                int kodeuk = 0;
                for (row = 1; row <= xlRange.Rows.Count; row++)
                {

                    if (xlRange.Cells[row, 1].Value != null)
                    {
                        if (row == 991)
                        {
                            lastrow = 991;
                        }
                           string kode= xlRange.Cells[row, 1].Value.ToString();
                            gridKesehatan.Rows.Add();
                         //   row = row + 1;

                            gridKesehatan.Rows[row].Cells[0].Value = xlRange.Cells[row, 1].Value.ToString();
                            gridKesehatan.Rows[row].Cells[1].Value = xlRange.Cells[row, 2].Value.ToString();
                            gridKesehatan.Rows[row].Cells[2].Value = xlRange.Cells[row, 3].Value.ToString();
                        
                            if (kode.Replace(".", "").Replace(" ", "").Length == 10)
                                kodeuk = GetKodeUK(row);
                            gridKesehatan.Rows[row].Cells[3].Value = kodeuk.ToString();

                            gridKesehatan.Rows[row].Cells[4].Value = row.ToString();
                            gridKesehatan.Rows[row].Cells[5].Value = row.ToString();
                            lastrow = row;
                            //gridKesehatan.Rows[row].Cells[2].Value = xlRange.Cells[i, 1].Value.ToString();

                            
                          
                      }

                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);//"Kesalahan membuka fileexcell. Jika file dalam keadaan dibuka, sila tutup terlebih dahulu");
            }
        }
        private int GetKodeUK(int row)
        {
         string urian =DataFormat.GetString (gridKesehatan.Rows[row].Cells[1].Value);
         if (row > 120)
         {
          //   Console.WriteLine(DataFormat.GetString(gridKesehatan.Rows[row].Cells[1].Value));
         }
           

if (urian.ToLower().Replace(" ","").Contains("tuantuan")) return 2;
if (urian.ToLower().Replace(" ", "").Contains("tuan-tuan")) return 2;
if (urian.ToLower().Replace(" ","").Contains("sungaiawan")) return 3;
if (urian.ToLower().Replace(" ","").Contains("kedondong")) return 4;
if (urian.ToLower().Replace(" ","").Contains("muliabaru")) return 5;
if (urian.ToLower().Replace(" ","").Contains("sukabangun")) return 6;
if (urian.ToLower().Replace(" ","").Contains("pesaguan")) return 7;
if (urian.ToLower().Replace(" ","").Contains("kualasatong")) return 8;
if (urian.ToLower().Replace(" ","").Contains("sungaibesar")) return 9;
if (urian.ToLower().Replace(" ","").Contains("sungaimelayu")) return 10;
if (urian.ToLower().Replace(" ","").Contains("tumbangtiti")) return 11;
if (urian.ToLower().Replace(" ","").Contains("tanjungpura")) return 12;
if (urian.ToLower().Replace(" ","").Contains("sandai")) return 13;
if (urian.ToLower().Replace(" ","").Contains("sungailaur")) return 14;
if (urian.ToLower().Replace(" ","").Contains("tayap")) return 15;
if (urian.ToLower().Replace(" ","").Contains("pemahan")) return 16;
if (urian.ToLower().Replace(" ","").Contains("airupas")) return 17;
if (urian.ToLower().Replace(" ","").Contains("balaiberkuak")) return 18;
if (urian.ToLower().Replace(" ","").Contains("simpangdua")) return 19;
if (urian.ToLower().Replace(" ","").Contains("manismata")) return 20;
if (urian.ToLower().Replace(" ","").Contains("kendawangan")) return 21;
if (urian.ToLower().Replace(" ","").Contains("sukamulya")) return 22;
if (urian.ToLower().Replace(" ", "").Contains("sukamulia")) return 22;
if (urian.ToLower().Replace(" ","").Contains("marau")) return 23;
if (urian.ToLower().Replace(" ","").Contains("hulusungai")) return 24;
if (urian.ToLower().Replace(" ","").Contains("riam")) return 25;
           
            return 1;


        }

        private void TampilkanPendapatan()
        {
            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex + 1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount;
                rowCount = xlWorksheet.Rows.Count;


                //gridData2.DataSource = null;
                SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
                List<SKPD> lstSKPD = new List<SKPD>();
                lstSKPD = oSKPDLogic.Get(2021);
                gridData2.Rows.Clear();
                //string sIDDInas = ctrlDinas1.GetID().ToString().Trim();
                SKPD oSKPD = ctrlDinas1.GetSKPD();
                string sIDDInas = oSKPD.KodeSIPD;
                Cursor.Current = Cursors.WaitCursor;
                string sIDSKPD = "";
                string sKodeSIPD = "";
                int row = 0;
                string sel = "";

                for (int i = 1; i <= xlRange.Rows.Count; i++)
                //    for (int i = 2; i <= 4; i++)
                {

                    if (i >= 83)
                        i = i;
                    if (xlRange.Cells[i, 1].Value != null)
                    {
                        if (xlRange.Cells[i, 1].Value.ToString().Length >= 22)
                        {
                            sel = xlRange.Cells[i, 1].Value.ToString().Trim().Substring(0, 22);

                            //  if (sIDDInas.Trim() == sel)
                            // {
                            gridData.Rows.Add();
                            // gridData.Rows[row].Cells[0].Value = (i - 1).ToString();
                            if (xlRange.Cells[i, 1].Value != null && xlRange.Cells[i, 1].Value.ToString().Length >= 1)
                            {
                                gridData.Rows[row].Cells[0].Value = xlRange.Cells[i, 1].Value.ToString().Substring(0, 4);
                                gridData.Rows[row].Cells[1].Value = xlRange.Cells[i, 1].Value.ToString().Substring(4);

                            }

                            if (xlRange.Cells[i, 2].Value != null && xlRange.Cells[i, 2].Value.ToString().Length >= 17)
                            {
                                //opd
                                gridData.Rows[row].Cells[2].Value = xlRange.Cells[i, 2].Value.ToString().Substring(0, 17);
                                gridData.Rows[row].Cells[3].Value = xlRange.Cells[i, 2].Value.ToString().Substring(18);
                            }

                            if (xlRange.Cells[i, 3].Value != null)
                            {
                                //idurusan 
                                gridData.Rows[row].Cells[4].Value = xlRange.Cells[i, 3].Value.ToString();

                            }


                            sIDSKPD = GetIDSKPD(lstSKPD, xlRange.Cells[i, 1].Value.ToString().Substring(0, 22));
                            //sKodeSIPD = xlRange.Cells[i, 2].Value.ToString().Substring(0, 22).Trim();
                            gridData.Rows[row].Cells[5].Value = sIDSKPD;


                            row = row + 1;
                        }
                        //}



                    }


                    //gridData.DataSource = dt;


                }
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Selesai. Sila Periksa data.");
                xlWorkbook.Close();
                //xlApp.dispo = Nothing;

                //xlApp.ExClose();// = xlApp.Workbooks.Open(txtFileName.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);//"Kesalahan membuka fileexcell. Jika file dalam keadaan dibuka, sila tutup terlebih dahulu");
            }
        }
        
        private void TampilkanPendapatan2024()
        {
            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex + 1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount;
                rowCount = xlWorksheet.Rows.Count;


                //gridData2.DataSource = null;
                SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
                List<SKPD> lstSKPD = new List<SKPD>();
                lstSKPD = oSKPDLogic.Get(2024);
                gridData2.Rows.Clear();
                //string sIDDInas = ctrlDinas1.GetID().ToString().Trim();
                SKPD oSKPD = ctrlDinas1.GetSKPD();
                string sIDDInas = oSKPD.KodeSIPD;
                Cursor.Current = Cursors.WaitCursor;
                string sIDSKPD = "";
                string sKodeSIPD = "";
                int row = 0;
                string sel = "";

                for (int i = 2; i <= xlRange.Rows.Count; i++)
                //    for (int i = 2; i <= 4; i++)
                {

                    //if (i >= 83)
                    //    i = i;
                    gridData.Rows.Add();
                    if (xlRange.Cells[i, 1].Value != null)
                    {
                        gridData.Rows[row].Cells[0].Value = xlRange.Cells[i, 3].Value.ToString();//idrekening
                        gridData.Rows[row].Cells[1].Value = xlRange.Cells[i, 4].Value.ToString();// nama
                        gridData.Rows[row].Cells[2].Value = xlRange.Cells[i, 5].Value.ToString();//kode skpd 
                        gridData.Rows[row].Cells[3].Value = xlRange.Cells[i, 6].Value.ToString();// nama SKPD
                        gridData.Rows[row].Cells[4].Value = xlRange.Cells[i, 7].Value.ToString();//uraian
                        gridData.Rows[row].Cells[5].Value = xlRange.Cells[i, 9].Value.ToString();//pagu
                        gridData.Rows[row].Cells[6].Value = xlRange.Cells[i, 3].Value.ToString();//idskpd
                       

                        sIDSKPD = GetIDSKPD(lstSKPD, xlRange.Cells[i, 5].Value.ToString());
                       //     ////sKodeSIPD = xlRange.Cells[i, 2].Value.ToString().Substring(0, 22).Trim();
                       //     //gridData.Rows[row].Cells[5].Value = sIDSKPD;
                       gridData.Rows[row].Cells[6].Value = sIDSKPD;

                            row = row + 1;
                     }
            ////            //}



            }


            ////        //gridData.DataSource = dt;


            //    }
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Selesai. Sila Periksa data.");
                xlWorkbook.Close();
            //    //xlApp.dispo = Nothing;

            //    //xlApp.ExClose();// = xlApp.Workbooks.Open(txtFileName.Text);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);//"Kesalahan membuka fileexcell. Jika file dalam keadaan dibuka, sila tutup terlebih dahulu");
            }
        }
        
        private void TampilkanPembiayaan()
        {
            try

            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex + 1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount;
                rowCount = xlWorksheet.Rows.Count;


                //gridData2.DataSource = null;
                SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
                List<SKPD> lstSKPD = new List<SKPD>();
                lstSKPD = oSKPDLogic.Get(2024);
                gridData2.Rows.Clear();
                //string sIDDInas = ctrlDinas1.GetID().ToString().Trim();
                SKPD oSKPD = ctrlDinas1.GetSKPD();
                string sIDDInas = oSKPD.KodeSIPD;
                Cursor.Current = Cursors.WaitCursor;
                string sIDSKPD = "";
                string sKodeSIPD = "";
                int row = 0;
                string sel = "";

                for (int i = 2; i <= xlRange.Rows.Count; i++)
                //    for (int i = 2; i <= 4; i++)
                {

                    //if (i >= 83)
                    //    i = i;
                    gridPembiayaan.Rows.Add();
                    if (xlRange.Cells[i, 1].Value != null)
                    {
                        gridPembiayaan.Rows[row].Cells[0].Value = xlRange.Cells[i, 3].Value.ToString();//idrekening
                        gridPembiayaan.Rows[row].Cells[1].Value = xlRange.Cells[i, 4].Value.ToString();// nama
                        gridPembiayaan.Rows[row].Cells[2].Value = xlRange.Cells[i, 5].Value.ToString();//kode skpd 
                        gridPembiayaan.Rows[row].Cells[3].Value = xlRange.Cells[i, 6].Value.ToString();// nama SKPD
                        gridPembiayaan.Rows[row].Cells[4].Value = xlRange.Cells[i, 7].Value.ToString();//uraian
                        gridPembiayaan.Rows[row].Cells[5].Value = xlRange.Cells[i, 9].Value.ToString();//pagu
                        gridPembiayaan.Rows[row].Cells[6].Value = xlRange.Cells[i, 3].Value.ToString();//idskpd


                        sIDSKPD = GetIDSKPD(lstSKPD, xlRange.Cells[i, 5].Value.ToString());
                        gridPembiayaan.Rows[row].Cells[6].Value = sIDSKPD;

                        row = row + 1;
                    }
                }


                ////        //gridData.DataSource = dt;


                //    }
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Selesai. Sila Periksa data.");
                xlWorkbook.Close();
                //    //xlApp.dispo = Nothing;

                //    //xlApp.ExClose();// = xlApp.Workbooks.Open(txtFileName.Text);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);//"Kesalahan membuka fileexcell. Jika file dalam keadaan dibuka, sila tutup terlebih dahulu");
            }
        }
        private void TampilkanBelanja()
        {
            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex + 1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount;
                rowCount = xlWorksheet.Rows.Count;

                if (m_mode == 1)
                {
                    chkPlafon.Checked = true;

                    gridData.DataSource = null;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID");
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Plafon");
                    dt.Columns.Add("PlafonABT");
                    Cursor.Current = Cursors.WaitCursor;

                    for (int i = 1; i <= xlRange.Rows.Count; i++)
                    {
                        //gridData.Rows.Add ();
                        DataRow dr = dt.NewRow();
                        for (int j = 1; j <= 4; j++)
                        {
                            if (xlRange.Cells[i, j].Value != null && xlRange.Cells[i, j].Value2 != null)
                            {
                                dr[j - 1] = (xlRange.Cells[i, j] as Excel.Range).Value2.ToString();

                                // gridData.Rows[i].Cells[j].Value = (xlRange.Cells[i, j] as Excel.Range).Value2.ToString();// "444";// xlWorksheet.Cells[i, j].Value.ToString();
                            }
                            else
                            {
                                dr[j - 1] = "";

                            }
                            ////new line
                            //if (j == 1)
                            //    Console.Write("\r\n");

                            ////write the value to the console
                            //if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            //    Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");

                            ////add useful things here!   
                        }
                        dt.Rows.Add(dr); // adding Row into DataTable
                        dt.AcceptChanges();

                    }

                    gridData.DataSource = dt;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Selesai . Sila Periksa data,");


                }
                else
                {

                    //gridData2.DataSource = null;
                    SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
                    List<SKPD> lstSKPD = new List<SKPD>();
                    lstSKPD = oSKPDLogic.Get(GlobalVar.TahunAnggaran);
                    gridData2.Rows.Clear();
                    //string sIDDInas = ctrlDinas1.GetID().ToString().Trim();
                    SKPD oSKPD = ctrlDinas1.GetSKPD();
                    if (oSKPD == null)
                    {
                        MessageBox.Show("Belum pilih SKPD");
                        return;

                    }
                    string sIDDInas = oSKPD.KodeSIPD;
                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("ID");
                    //dt.Columns.Add("Name");
                    //dt.Columns.Add("Plafon");
                    //dt.Columns.Add("PlafonABT");
                    Cursor.Current = Cursors.WaitCursor;
                    string sIDSKPD = "";
                    string sKodeSIPD = "";
                    int row = 0;
                    string skodeUnit = "";
                    int idUnit = 0;
                    int noUnit = 0;


                    for (int i = 2; i <= xlRange.Rows.Count; i++)
                    //    for (int i = 2; i <= 4; i++)
                    {
                        //gridData.Rows.Add ();
                        //DataRow dr = dt.NewRow();
                        //for (int j = 1; j <= 4; j++)
                        //{

                        if (xlRange.Cells[i, 1].Value != null)
                        {
                            //if (sIDDInas.Trim() == xlRange.Cells[i, 2].Value.ToString().Substring(0, 22).Trim())
                            //{
                            gridData2.Rows.Add();
                            gridData2.Rows[row].Cells[0].Value = (i - 1).ToString();
                            if (xlRange.Cells[i, 1].Value != null && xlRange.Cells[i, 1].Value.ToString().Length >= 1)
                            {
                                gridData2.Rows[row].Cells[1].Value = xlRange.Cells[i, 1].Value.ToString().Substring(0, 1);
                                //   gridData2.Rows[row].Cells[2].Value = xlRange.Cells[i, 1].Value.ToString().Substring(2);
                            }

                            if (xlRange.Cells[i, 2].Value != null && xlRange.Cells[i, 2].Value.ToString().Length >= 22)
                            {
                                //opd
                                gridData2.Rows[row].Cells[3].Value = xlRange.Cells[i, 2].Value.ToString().Substring(0, 22);
                                if (xlRange.Cells[i, 2].Value.ToString().Substring(0, 22).Trim() != sKodeSIPD)
                                {
                                    sIDSKPD = GetIDSKPD(lstSKPD, xlRange.Cells[i, 2].Value.ToString().Substring(0, 22)); // sIDSKPD = oSKPD.ID.ToString();//
                                    sKodeSIPD = xlRange.Cells[i, 2].Value.ToString().Substring(0, 22).Trim();
                                }

                                gridData2.Rows[row].Cells[4].Value = xlRange.Cells[i, 2].Value.ToString().Substring(23);
                            }

                            if (xlRange.Cells[i, 3].Value != null && xlRange.Cells[i, 3].Value.ToString().Length >= 4)
                            {
                                //idurusan 
                                gridData2.Rows[row].Cells[5].Value = xlRange.Cells[i, 3].Value.ToString().Substring(0, 4);
                                gridData2.Rows[row].Cells[6].Value = xlRange.Cells[i, 3].Value.ToString().Substring(4);
                            }


                            if (xlRange.Cells[i, 3].Value != null && xlRange.Cells[i, 3].Value.ToString().Length >= 22)
                            {
                                // Urusan 
                                gridData2.Rows[row].Cells[7].Value = xlRange.Cells[i, 3].Value.ToString().Substring(0, 4);
                                gridData2.Rows[row].Cells[8].Value = xlRange.Cells[i, 3].Value.ToString().Substring(4);
                            }
                            if (xlRange.Cells[i, 4].Value != null && xlRange.Cells[i, 4].Value.ToString().Length >= 4)
                            {
                                //Unit
                                gridData2.Rows[row].Cells[9].Value = xlRange.Cells[i, 4].Value.ToString().Substring(0, 22);
                                gridData2.Rows[row].Cells[10].Value = xlRange.Cells[i, 4].Value.ToString().Substring(23);
                                
                                if (skodeUnit != xlRange.Cells[i, 4].Value.ToString().Substring(0, 22).Trim()){
                                    skodeUnit = xlRange.Cells[i, 4].Value.ToString().Substring(0, 22);
                                    noUnit = noUnit + 1;
                                    idUnit = DataFormat.GetInteger(sIDSKPD) + noUnit;
                                }


                            }

                            if (xlRange.Cells[i, 5].Value != null && xlRange.Cells[i, 5].Value.ToString().Length >= 7)
                            {
                                //idprogrsam 
                                gridData2.Rows[row].Cells[11].Value = xlRange.Cells[i, 5].Value.ToString().Substring(0, 7);
                                gridData2.Rows[row].Cells[12].Value = xlRange.Cells[i, 5].Value.ToString().Substring(7);
                            }
                            if (xlRange.Cells[i, 6].Value != null && xlRange.Cells[i, 6].Value.ToString().Length >= 12)
                            {//-----------------------
                                //Kegiatan 
                                gridData2.Rows[row].Cells[13].Value = xlRange.Cells[i, 6].Value.ToString().Substring(0, 12);
                                gridData2.Rows[row].Cells[14].Value = xlRange.Cells[i, 6].Value.ToString().Substring(12); ;
                            }
                            if (xlRange.Cells[i, 7].Value != null && xlRange.Cells[i, 7].Value.ToString().Length >= 12)
                            {
                                //Sub Kegiatan 
                                gridData2.Rows[row].Cells[15].Value = xlRange.Cells[i, 7].Value.ToString().Substring(0, 15);
                                gridData2.Rows[row].Cells[16].Value = xlRange.Cells[i, 7].Value.ToString().Substring(15); ;
                            }

                            if (xlRange.Cells[i, 8].Value != null && xlRange.Cells[i, 8].Value.ToString().Length >= 15)
                            {
                                // rEKENING
                                gridData2.Rows[row].Cells[17].Value = xlRange.Cells[i, 8].Value.ToString().Substring(0, 17);
                                gridData2.Rows[row].Cells[18].Value = xlRange.Cells[i, 8].Value.ToString().Substring(17); ;
                            }

                            //o.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[5].Value.ToString().Replace(".", ""));
                            //o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[9].Value.ToString().Replace(".", ""));
                            //o.IDKegiatan = DataFormat.GetInteger(gridData2.Rows[i].Cells[11].Value.ToString().Replace(".", ""));
                            ////o.IDSubKegiatan = DataFormat.GetLong(gridData2.Rows[i].Cells[13].Value.ToString().Replace(".", ""));
                            //IDRekening = DataFormat.GetLong(gridData2.Rows[i].Cells[15].Value.ToString().Replace(".", ""));
                            //o.PPKD = 0;
                            //o.StatusUpdate = 0;
                            ////o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                            //o.JumlahRKAP = DataFormat.GetDecimal(gridData2.Rows[i].Cells[17].Value.ToString().Replace(".", ""));

                            if (xlRange.Cells[i, 9].Value != null && xlRange.Cells[i, 9].Value.ToString().Length > 0)
                            {
                                gridData2.Rows[row].Cells[19].Value = xlRange.Cells[i, 9].Value.ToString();// niliai
                            }
                            gridData2.Rows[row].Cells[20].Value = sIDSKPD;
                            //if (xlRange.Cells[i, 8].Value != null && xlRange.Cells[i, 8].Value.ToString().Length > 0)
                            //{
                            gridData2.Rows[row].Cells[21].Value = xlRange.Cells[i, 10].Value.ToString();// SumberDana
                            //}
                            // kodeUnit 
                            // 
                            //noUnit =noUnit+1;
                            //idUnit = DataFormat.GetInteger (sIDSKPD) + noUnit;
                            gridData2.Rows[row].Cells[22].Value = noUnit.ToString();// kode Unit
                            gridData2.Rows[row].Cells[23].Value = idUnit.ToString();// idunit 
                            row = row + 1;
                        }
                        //}
                        // }
                        // dt.Rows.Add(dr); // adding Row into DataTable
                        //dt.AcceptChanges();

                    }


                    //gridData.DataSource = dt;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Selesai. Sila Periksa data. Ada 4 kolom,");

                }
                xlWorkbook.Close();
                //xlApp.dispo = Nothing;

                //xlApp.ExClose();// = xlApp.Workbooks.Open(txtFileName.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);//"Kesalahan membuka fileexcell. Jika file dalam keadaan dibuka, sila tutup terlebih dahulu");
            }
        }
        private void TampilkanBelanja2024()
        {
            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex + 1];
                Excel.Range xlRange = xlWorksheet.UsedRange;
                decimal Jumlah = 0L;
                int rowCount;
                rowCount = xlWorksheet.Rows.Count;


                    //gridData2.DataSource = null;
                    SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
                    List<SKPD> lstSKPD = new List<SKPD>();
                    lstSKPD = oSKPDLogic.Get(GlobalVar.TahunAnggaran);
                    gridData2.Rows.Clear();
                    string sIDDInas = ctrlDinas1.GetID().ToString().Trim();
                    SKPD oSKPD = ctrlDinas1.GetSKPD();
                    //if (oSKPD == null)
                    //{
                    //    MessageBox.Show("Belum pilih SKPD");
                    //    return;

                    //}

                    //string sIDDInas = oSKPD.KodeSIPD;
                    Cursor.Current = Cursors.WaitCursor;

                    string sIDSKPD = "";
                    string sKodeSIPD = "";
                    int row = 0;
                    string skodeUnit = "";
                    int idUnit = 0;
                    int noUnit = 0;
                    string KodeUrusanPokok = sIDDInas.Substring(0, 1) + "." + sIDDInas.Substring(1, 2);
                  
                    for (int i = 2; i <= xlRange.Rows.Count; i++)
                  //  for (int i = 2; i <= 100; i++)
                    {
                        string skode =DataFormat.GetString(xlRange.Cells[i, 5].Value);

                        //if (oSKPD.KodeSIPD == skode || oSKPD == null)
                        //{
                           if (xlRange.Cells[i, 1].Value != null)
                           {
                               gridData2.Rows.Add();
                               gridData2.Rows[row].Cells[0].Value = DataFormat.GetString(xlRange.Cells[i, 1].Value);
                               gridData2.Rows[row].Cells[1].Value = DataFormat.GetString(xlRange.Cells[i, 2].Value);
                               gridData2.Rows[row].Cells[2].Value = DataFormat.GetString(xlRange.Cells[i, 3].Value);
                               gridData2.Rows[row].Cells[3].Value = DataFormat.GetString(xlRange.Cells[i, 4].Value);

                               gridData2.Rows[row].Cells[4].Value = DataFormat.GetString(xlRange.Cells[i, 5].Value);
                               gridData2.Rows[row].Cells[5].Value = DataFormat.GetString(xlRange.Cells[i, 6].Value);
                               gridData2.Rows[row].Cells[6].Value = DataFormat.GetString(xlRange.Cells[i, 7].Value);
                               gridData2.Rows[row].Cells[7].Value = DataFormat.GetString(xlRange.Cells[i, 8].Value);
                               gridData2.Rows[row].Cells[8].Value = DataFormat.GetString(xlRange.Cells[i, 9].Value).Replace("x.xx", KodeUrusanPokok).Replace("X.XX", KodeUrusanPokok);
                               gridData2.Rows[row].Cells[9].Value = DataFormat.GetString(xlRange.Cells[i, 10].Value);
                               gridData2.Rows[row].Cells[10].Value = DataFormat.GetString(xlRange.Cells[i, 11].Value).Replace("x.xx", KodeUrusanPokok).Replace("X.XX", KodeUrusanPokok);
                               gridData2.Rows[row].Cells[11].Value = DataFormat.GetString(xlRange.Cells[i, 12].Value);
                               gridData2.Rows[row].Cells[12].Value = DataFormat.GetString(xlRange.Cells[i, 13].Value).Replace("x.xx", KodeUrusanPokok).Replace("X.XX", KodeUrusanPokok);

                               gridData2.Rows[row].Cells[13].Value = DataFormat.GetString(xlRange.Cells[i, 14].Value);
                               gridData2.Rows[row].Cells[14].Value = DataFormat.GetString(xlRange.Cells[i, 15].Value).Replace("x.xx", KodeUrusanPokok).Replace("X.XX", KodeUrusanPokok);
                               gridData2.Rows[row].Cells[15].Value = DataFormat.GetString(xlRange.Cells[i, 16].Value);
                               gridData2.Rows[row].Cells[16].Value = DataFormat.GetString(xlRange.Cells[i, 17].Value);


                               gridData2.Rows[row].Cells[17].Value = DataFormat.GetString(xlRange.Cells[i, 18].Value);
                               gridData2.Rows[row].Cells[18].Value = DataFormat.GetString(xlRange.Cells[i, 19].Value);
                               gridData2.Rows[row].Cells[19].Value = DataFormat.GetString(xlRange.Cells[i, 20].Value);
                               gridData2.Rows[row].Cells[20].Value = DataFormat.GetString(xlRange.Cells[i, 21].Value);
                               gridData2.Rows[row].Cells[21].Value = DataFormat.GetString(xlRange.Cells[i, 22].Value);
                               gridData2.Rows[row].Cells[22].Value = DataFormat.GetString(xlRange.Cells[i, 23].Value);
                               if (xlRange.Cells[i, 23].Value != null)
                               {
                                   //if (xlRange.Cells[i, 23].Value != "")
                                   //{
                                   Jumlah = Jumlah + DataFormat.GetDecimal(xlRange.Cells[i, 23].Value);
                                   // }
                               }
                               string sunit = "0000";
                               sunit = DataFormat.GetString(gridData2.Rows[row].Cells[6].Value).Substring(18, 4);
                               sIDSKPD = GetIDSKPD(lstSKPD, DataFormat.GetString(gridData2.Rows[row].Cells[4].Value));
                               gridData2.Rows[row].Cells[23].Value = sIDSKPD;
                               idUnit = DataFormat.GetInteger(sIDSKPD) + DataFormat.GetInteger(sunit);
                               gridData2.Rows[row].Cells[24].Value = idUnit.ToString();
                               gridData2.Rows[row].Cells[25].Value = sunit;



                               row++;
                           }
                       // }  

                    }
                    gridData2.Rows.Add();
                    gridData2.Rows[row].Cells[22].Value = Jumlah.ToRupiahInReport();
                    txtJumlah.Text = Jumlah.ToRupiahInReport(); 
                    //gridData.DataSource = dt;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Selesai. Sila Periksa data. ");

                
                xlWorkbook.Close();
                //xlApp.dispo = Nothing;

                //xlApp.ExClose();// = xlApp.Workbooks.Open(txtFileName.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);//"Kesalahan membuka fileexcell. Jika file dalam keadaan dibuka, sila tutup terlebih dahulu");
            }
        }
        private void TampilkanBKeluaran2024()
        {
            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex + 1];
                Excel.Range xlRange = xlWorksheet.UsedRange;
                decimal Jumlah = 0L;
                int rowCount;
                rowCount = xlWorksheet.Rows.Count;


                ////gridData2.DataSource = null;
                //SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
                //List<SKPD> lstSKPD = new List<SKPD>();
                //lstSKPD = oSKPDLogic.Get(GlobalVar.TahunAnggaran);
                gridKeluaran.Rows.Clear();
                //string sIDDInas = ctrlDinas1.GetID().ToString().Trim();
                //SKPD oSKPD = ctrlDinas1.GetSKPD();
                //if (oSKPD == null)
                //{
                //    MessageBox.Show("Belum pilih SKPD");
                //    return;

                //}

                //string sIDDInas = oSKPD.KodeSIPD;
                Cursor.Current = Cursors.WaitCursor;

                string sIDSKPD = "";
                string sKodeSIPD = "";
                int row = 0;
                string skodeUnit = "";
                int idUnit = 0;
                int noUnit = 0;
                // 
                for (int i = 2; i <= xlRange.Rows.Count; i++)
                {

                    if (xlRange.Cells[i, 1].Value != null)
                    {
                        gridKeluaran.Rows.Add();
                        gridKeluaran.Rows[row].Cells[0].Value = DataFormat.GetString(xlRange.Cells[i, 1].Value);
                        gridKeluaran.Rows[row].Cells[1].Value = DataFormat.GetString(xlRange.Cells[i, 2].Value);
                        gridKeluaran.Rows[row].Cells[2].Value = DataFormat.GetString(xlRange.Cells[i, 3].Value);
                        gridKeluaran.Rows[row].Cells[3].Value = DataFormat.GetString(xlRange.Cells[i, 4].Value);

                        gridKeluaran.Rows[row].Cells[4].Value = DataFormat.GetString(xlRange.Cells[i, 5].Value);
                        gridKeluaran.Rows[row].Cells[5].Value = DataFormat.GetString(xlRange.Cells[i, 6].Value);
                        gridKeluaran.Rows[row].Cells[6].Value = DataFormat.GetString(xlRange.Cells[i, 7].Value);
                        gridKeluaran.Rows[row].Cells[7].Value = DataFormat.GetString(xlRange.Cells[i, 8].Value);
                        gridKeluaran.Rows[row].Cells[8].Value = DataFormat.GetString(xlRange.Cells[i, 9].Value);
                      



                        row++;
                    }

                }
              
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Selesai. Sila Periksa data Keluaran");


                xlWorkbook.Close();
                //xlApp.dispo = Nothing;

                //xlApp.ExClose();// = xlApp.Workbooks.Open(txtFileName.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);//"Kesalahan membuka fileexcell. Jika file dalam keadaan dibuka, sila tutup terlebih dahulu");
            }
        }
        //private decimal GetJumlah()
        //{

        //}
        private void TampilkanUraian()
        {
            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex + 1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount;
                rowCount = xlWorksheet.Rows.Count;
                SKPD oSKPD = ctrlDinas1.GetSKPD();
                string sIDDInas = oSKPD.KodeSIPD;

                //gridData2.DataSource = null;
                gridUraian.Rows.Clear();
                //string sIDDInas = ctrlDinas1.GetID().ToString().Trim();
                Cursor.Current = Cursors.WaitCursor;
                int row = 0;


                for (int i = 1; i <= xlRange.Rows.Count; i++)
                //for (int i = 1; i <= 100; i++)
                {


                    if (xlRange.Cells[i, 9].Value != null && xlRange.Cells[i, 12].Value != null && xlRange.Cells[i,8].Value != null)
                    {
                        if (sIDDInas.Trim() == xlRange.Cells[i, 1].Value.ToString().Trim())
                        {

                            if (xlRange.Cells[i, 9].Value.ToString() != "0")
                            {
                                gridUraian.Rows.Add();

                                gridUraian.Rows[row].Cells[0].Value = xlRange.Cells[i, 1].Value.ToString();//kdskpd
                                gridUraian.Rows[row].Cells[1].Value = xlRange.Cells[i, 3].Value.ToString(); // kdsubunit
                                gridUraian.Rows[row].Cells[2].Value = xlRange.Cells[i, 5].Value.ToString();// kprg
                                gridUraian.Rows[row].Cells[3].Value = xlRange.Cells[i, 6].Value.ToString();// skeg 
                                gridUraian.Rows[row].Cells[4].Value = xlRange.Cells[i, 7].Value.ToString();// subkeg

                                gridUraian.Rows[row].Cells[5].Value = xlRange.Cells[i, 9].Value.ToString();//kdrek
                                gridUraian.Rows[row].Cells[6].Value = xlRange.Cells[i, 12].Value.ToString();// kd 
                                gridUraian.Rows[row].Cells[7].Value = xlRange.Cells[i, 13].Value.ToString();// Nama

                                gridUraian.Rows[row].Cells[8].Value = xlRange.Cells[i, 18].Value.ToString();// volume
                                gridUraian.Rows[row].Cells[9].Value = xlRange.Cells[i, 14].Value.ToString();// satuan 
                                gridUraian.Rows[row].Cells[10].Value = xlRange.Cells[i, 17].Value.ToString();// harga set 
                                gridUraian.Rows[row].Cells[11].Value = xlRange.Cells[i, 16].Value.ToString();// Hargasat
                                gridUraian.Rows[row].Cells[12].Value = xlRange.Cells[i, 8].Value.ToString();// nilai





                                row = row + 1;
                            }
                        }
                    }



                }
                xlWorkbook.Close();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Selesai. Menampilkan paket");
                //xlApp.dispo = Nothing;

                //xlApp.ExClose();// = xlApp.Workbooks.Open(txtFileName.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);//"Kesalahan membuka fileexcell. Jika file dalam keadaan dibuka, sila tutup terlebih dahulu");
            }
        }

        private void TampilkanPaket()
        {
            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFileName.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[worksheetsComboBox.SelectedIndex + 1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount;
                rowCount = xlWorksheet.Rows.Count;


                //gridData2.DataSource = null;
                gridPaket.Rows.Clear();
                //string sIDDInas = ctrlDinas1.GetID().ToString().Trim();
                Cursor.Current = Cursors.WaitCursor;
                int row = 0;


                for (int i = 1; i <= xlRange.Rows.Count; i++)
                //    for (int i = 2; i <= 4; i++)
                {

                    if (xlRange.Cells[i, 2].Value != null)
                    {

                        gridPaket.Rows.Add();

                        gridPaket.Rows[row].Cells[0].Value = xlRange.Cells[i, 1].Value.ToString();//idsub
                        gridPaket.Rows[row].Cells[1].Value = xlRange.Cells[i, 2].Value.ToString(); // Nama
                        gridPaket.Rows[row].Cells[2].Value = xlRange.Cells[i, 3].Value.ToString();// volume
                        gridPaket.Rows[row].Cells[3].Value = xlRange.Cells[i, 4].Value.ToString();// satuan 
                        gridPaket.Rows[row].Cells[4].Value = xlRange.Cells[i, 5].Value.ToString();// Harga

                        row = row + 1;
                    }



                }
                xlWorkbook.Close();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Selesai. Menampilkan paket");
                //xlApp.dispo = Nothing;

                //xlApp.ExClose();// = xlApp.Workbooks.Open(txtFileName.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);//"Kesalahan membuka fileexcell. Jika file dalam keadaan dibuka, sila tutup terlebih dahulu");
            }
        }
        private string GetIDSKPD(List<SKPD> lst, string sKode)
        {
            foreach (SKPD s in lst)
            {
                if (s.KodeSIPD.Trim() == sKode.Trim())
                {
                    return s.ID.ToString();
                }
            }
            return "";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            importProgram();

            try
            {
                string sSalahDinas = "";
                if (gridData.Rows.Count < 5)
                {
                    MessageBox.Show("Sila tampilkan data terlebih dahulu.");
                    return;

                }
                if (ctrlDinas1.GetID() == 0)
                {
                    MessageBox.Show("Pemilihan dinas digunakan untuk mempertegas dinas dari data. Sila pilih.");
                    return;

                }
                int idDinas = 0;
                idDinas = ctrlDinas1.GetID();

                List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();
                List<TKegiatanAPBD> _lstKeg = new List<TKegiatanAPBD>();
                List<TProgramAPBD> _lstPrg = new List<TProgramAPBD>();
                //List<Rekening> _lstRekMaster = new List<Rekening>();
                string kodejenis = "xx";

                TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, 0, mprofile);
                for (int i = 0; i < gridData.Rows.Count; i++)
                {
                    if (i == 23)
                    {
                        i = 23;
                    }
                    string sCol1 = DataFormat.GetString(gridData.Rows[i].Cells[0].Value).Trim();

                    string nama = DataFormat.GetString(gridData.Rows[i].Cells[1].Value).Trim();
                    if (nama.Contains("BELANJA OPERASI") || nama.Contains("BELANJA MODAL") ||
                        nama.Contains("PENDAPATAN ASLI DAERAH (PAD)") || nama.Contains("PENDAPATAN TRANSFER") || nama.Contains("LAIN-LAIN PENDAPATAN DAERAH YANG SAH") ||
                        nama.Contains("BEKANJA TIDAK TERDUGA") || nama.Contains("BELANJA TRANSFER"))
                    {


                        if (nama.Contains("PENDAPATAN ASLI DAERAH (PAD)"))
                            kodejenis = "41";
                        //break;

                        if (nama.Contains("PENDAPATAN TRANSFER"))

                            kodejenis = "42";
                        // break;

                        if (nama.Contains("LAIN-LAIN PENDAPATAN DAERAH YANG SAH"))
                            kodejenis = "43";
                        //break;

                        if (nama.Contains("BELANJA OPERASI"))
                            kodejenis = "51";
                        //break;
                        if (nama.Contains("BELANJA MODAL"))
                            kodejenis = "52";
                        //break;
                        if (nama.Contains("BEKANJA TIDAK TERDUGA"))
                            kodejenis = "53";
                        //break;
                        if (nama.Contains("BELANJA TRANSFER"))
                            kodejenis = "54";
                        //break;

                    }

                    //BELANJA MODAL 
                    //BEKANJA TIDAK TERDUGA 
                    //BELANJA TRANSFER 
                    if (sCol1 != "")
                    {



                        if (sCol1.Length > 36)// 34 || sCol1.Length == 31 || sCol1.Length == 30 || sCol1.Length == 29 || sCol1.Replace(".", "").Length == 19 || sCol1.Replace(".", "").Length == 20)
                        {



                            if (sCol1.Replace(".", "").All(char.IsDigit) == true)
                            {
                                TAnggaranRekening tR = GetRekening(i, kodejenis);
                                if (tR != null)
                                {
                                    _lstRek.Add(tR);
                                }
                            }

                            //ProsesRekening(i);
                        }
                    }
                }

                bool hanyaPagu = chkPlafon.Checked;//?1:0;

                TAnggaranRekeningLogic oRekLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, 0, mprofile);
                int tahap = rb1.Checked ? 1 : (rb2.Checked ? 3 : 0);
                oRekLogic.SimpanImport3(_lstRek, idDinas, GlobalVar.TahunAnggaran, tahap, hanyaPagu);
                MessageBox.Show("Import selesai...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private TProgramAPBD GetProgram(int i)
        {
            TProgramAPBD o = new TProgramAPBD();
            o.Tahun = GlobalVar.TahunAnggaran;

            //1.25.1.25.01.00.00.5.1.1.01.01.
            //1.02.1.02.01.17.

            string col1;
            col1 = DataFormat.GetString(gridData.Rows[i].Cells[0].Value).Trim();

            //o.IDDinas = DataFormat.GetInteger(col1.Substring(5, 7).Replace(".", "") + "00");
            o.IDDinas = ctrlDinas1.GetID();
            o.IDUrusan = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", ""));
            o.IDProgram = 0;

            //if (DataFormat.GetInteger(col1.Substring(13).Replace(".","")) > 0)
            //{

            if (col1.Substring(14, 1) == ".")
            {
                o.IDProgram = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", "") +
                                                "0" + col1.Substring(13, 1));

            }
            else
            {
                o.IDProgram = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", "") +
                                                col1.Substring(13, 2));


            }



            o.Jenis = 3;

            o.Pagu = DataFormat.GetDecimal(gridData.Rows[i].Cells[2].Value);
            o.Nama2 = DataFormat.GetString(gridData.Rows[i].Cells[1].Value);
            o.Pagu = DataFormat.GetDecimal(gridData.Rows[i].Cells[2].Value);

            return o;

        }
        //private TProgramAPBD GetProgram22(int i)
        //{
        //    TProgramAPBD o = new TProgramAPBD();
        //    o.Tahun = GlobalVar.TahunAnggaran;

        //    string col1;
        //    col1 = DataFormat.GetString(gridData2.Rows[i].Cells[0].Value).Trim();

        //    //o.IDDinas = DataFormat.GetInteger(gridData2.Rows[i].Cells[18].Value);
        //    //o.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[5].Value.ToString().Replace(".", ""));
        //    //o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[9].Value.ToString().Replace(".", ""));
        //    //o.IDKegiatan = DataFormat.GetInteger(gridData2.Rows[i].Cells[11].Value.ToString().Replace(".", ""));
        //    //o.IDSubKegiatan = DataFormat.GetLong(gridData2.Rows[i].Cells[13].Value.ToString().Replace(".", ""));
        //    //o.IDRekening = DataFormat.GetLong(gridData2.Rows[i].Cells[15].Value.ToString().Replace(".", ""));
        //    //o.PPKD = 0;

        //    //o.IDDinas = DataFormat.GetInteger(col1.Substring(5, 7).Replace(".", "") + "00");
        //    o.IDDinas = ctrlDinas1.GetID();
        //    o.IDUrusan = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", ""));
        //    o.IDProgram = 0;

        //    //if (DataFormat.GetInteger(col1.Substring(13).Replace(".","")) > 0)
        //    //{


        //    o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[9].Value.ToString().Replace(".", ""));
        //    o.Nama = DataFormat.GetString(gridData2.Rows[i].Cells[10].Value.ToString());



        //    o.Jenis = 3;

        //    return o;

        //}

        private TProgramAPBD GetProgram22(int i)
        {

            try
            {
                TProgramAPBD o = new TProgramAPBD();
                o.Tahun = GlobalVar.TahunAnggaran;

                string col1;
                o.Jenis = 3;
                o.IDDinas = DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value);
                //  o.KodeUK = DataFormat.GetInteger(gridData2.Rows[i].Cells[25].Value.ToString().Replace(".", ""));
                o.KodeUK = DataFormat.GetInteger(DataFormat.GetString(gridData2.Rows[i].Cells[6].Value.ToString()).Substring(18, 4));
                o.IDUnit = o.IDDinas + o.KodeUK;

                o.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[8].Value.ToString().Replace(".", ""));
                o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[10].Value.ToString().Replace(".", ""));
                o.Nama = DataFormat.GetString(gridData2.Rows[i].Cells[11].Value.ToString());
//                o.IDUnit = DataFormat.GetInteger(gridData2.Rows[i].Cells[24].Value.ToString().Replace(".", ""));



              //  o.KodeUK = DataFormat.GetInteger(gridData2.Rows[i].Cells[25].Value.ToString().Replace(".", ""));
                o.KodeUK = DataFormat.GetInteger(DataFormat.GetString(gridData2.Rows[i].Cells[6].Value.ToString()).Substring(18, 4));

                
                
                


                o.Jenis = 3;

                return o;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        private TKegiatanAPBD GetKegiatan22(int i)
        {
            try
            {
                TKegiatanAPBD o = new TKegiatanAPBD();
                o.Tahun = 2025;

                string col1;

                o.IDDinas = DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value);
                if (o.IDDinas == 0)
                {
                    return null;

                }
                o.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[8].Value.ToString().Replace(".", ""));
                o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[10].Value.ToString().Replace(".", ""));
                o.IDUnit = DataFormat.GetInteger(gridData2.Rows[i].Cells[24].Value.ToString().Replace(".", ""));
                
                //o.KodeUK = DataFormat.GetInteger(gridData2.Rows[i].Cells[25].Value.ToString().Replace(".", ""));
                o.KodeUK = DataFormat.GetInteger(DataFormat.GetString(gridData2.Rows[i].Cells[6].Value.ToString()).Substring(18, 4));

                o.IDKegiatan = DataFormat.GetInteger(gridData2.Rows[i].Cells[12].Value.ToString().Replace(".", ""));
                o.Nama = DataFormat.GetString(gridData2.Rows[i].Cells[13].Value.ToString());
                
                //o.IDSubKegiatan = DataFormat.GetLong(gridData2.Rows[i].Cells[14].Value.ToString().Replace(".", ""));
                //o.IDRekening = DataFormat.GetLong(gridData2.Rows[i].Cells[18].Value.ToString().Replace(".", ""));

                o.Jenis = 3;

                return o;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //private TSubKegiatan GetKegiatan22(int i)
        //{
        //    TSubKegiatan o = new TSubKegiatan();
        //    o.Tahun = GlobalVar.TahunAnggaran;

        //    string col1;
        //    col1 = DataFormat.GetString(gridData2.Rows[i].Cells[0].Value).Trim();



        //    //o.IDDinas = DataFormat.GetInteger(col1.Substring(5, 7).Replace(".", "") + "00");
        //    o.IDDinas = ctrlDinas1.GetID();
        //    o.IDUrusan = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", ""));
        //    o.IDProgram = 0;

        //    o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[9].Value.ToString().Replace(".", ""));
        //    //o.Nama = DataFormat.GetString(gridData2.Rows[i].Cells[10].Value.ToString());
        //    o.IDKegiatan = DataFormat.GetInteger(gridData2.Rows[i].Cells[11].Value.ToString().Replace(".", ""));
        //    o.IDSubKegiatan= DataFormat.GetLong(gridData2.Rows[i].Cells[13].Value.ToString().Replace(".", ""));
        //    o.Nama = DataFormat.GetString(gridData2.Rows[i].Cells[14].Value.ToString());
        //    return o;

        //}

        private TKegiatanAPBD GetKegatan(int i)
        {
            TKegiatanAPBD o = new TKegiatanAPBD();
            o.Tahun = GlobalVar.TahunAnggaran;

            string col1;
            col1 = DataFormat.GetString(gridData.Rows[i].Cells[0].Value).Trim();
            //if (col1.Substring(col1.Length - 1, 1) == ".")
            //    col1 = col1.Substring(0, col1.Length - 1);


            //o.IDDinas = DataFormat.GetInteger(col1.Substring(5, 7).Replace(".", "") + "00");
            o.IDDinas = ctrlDinas1.GetID();
            o.IDUrusan = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", ""));

            o.IDProgram = 0;
            o.IDKegiatan = 0;


            if (col1.Length == 17)
            {
                //1.02.1.02.01.1.1.
                o.IDProgram = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", "") +
                                              "0" + col1.Substring(13, 1));
                o.IDKegiatan = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", "") +
                                              "0" + col1.Substring(13, 1) + "00" + col1.Substring(15, 1));

            }


            if (col1.Length == 18 || col1.Length == 19)
            {
                //1.02.1.02.01.01.1.
                if (col1.Substring(14, 1) == ".")
                {
                    string sPrg = col1.Substring(13, 1);
                    sPrg = DataFormat.GetInteger(sPrg).IntToStringWithLeftPad(2);
                    o.IDProgram = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", "") +
                                                  sPrg);
                }
                else
                {
                    string sPrg2 = col1.Substring(13, 2);
                    sPrg2 = DataFormat.GetInteger(sPrg2).IntToStringWithLeftPad(2);
                    o.IDProgram = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", "") +
                                                  sPrg2);

                    //o.IDProgram = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", "") +
                    //                               col1.Substring(13, 2));

                }
            }




            if (col1.Length == 18 || col1.Length == 19)
            {
                //1.02.1.02.01.1.01.
                if (col1.Substring(14, 1) == ".")
                {
                    string sKeg = col1.Substring(15).Replace(".", "");
                    sKeg = DataFormat.GetInteger(sKeg).IntToStringWithLeftPad(3);
                    o.IDKegiatan = DataFormat.GetInteger(o.IDProgram.ToString() + sKeg);

                } //1.02.1.02.01.01.1.

                else
                {
                    string sKeg2 = col1.Substring(16).Replace(".", "");
                    sKeg2 = DataFormat.GetInteger(sKeg2).IntToStringWithLeftPad(3);
                    o.IDKegiatan = DataFormat.GetInteger(o.IDProgram.ToString() + sKeg2);


                }
            }




            o.Jenis = 3;
            o.Plafon = DataFormat.GetDecimal(gridData.Rows[i].Cells[2].Value);
            o.Nama2 = DataFormat.GetString(gridData.Rows[i].Cells[1].Value);
            o.Pagu = DataFormat.GetDecimal(gridData.Rows[i].Cells[2].Value);
            o.Lokasi = DataFormat.GetString(gridData.Rows[i].Cells[3].Value).Replace("Lokasi Kegiatan :", "");

            return o;

        }
        private TAnggaranRekening GetRekening(int i, string kodejenis)
        {


            TAnggaranRekening o = new TAnggaranRekening();
            o.Tahun = GlobalVar.TahunAnggaran;

            string col1;
            col1 = DataFormat.GetString(gridData.Rows[i].Cells[0].Value).Trim();

            o.Nama = DataFormat.GetString(gridData.Rows[i].Cells[1].Value).Trim();
            o.IDUrusan = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", ""));// + col1.Substring(13, 3).Replace(".", "") );
            o.IDProgram = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", "") + col1.Substring(13, 3).Replace(".", ""));
            o.IDKegiatan = DataFormat.GetInteger(col1.Substring(0, 4).Replace(".", "") + col1.Substring(13, 3).Replace(".", "") + col1.Substring(16, 3).Replace(".", ""));
            o.IDSubKegiatan = DataFormat.GetLong(
                col1.Substring(0, 4).Replace(".", "") + col1.Substring(13, 3).Replace(".", "") + col1.Substring(16, 3).Replace(".", "")
                + col1.Substring(20, 2).Replace(".", ""));


            if (kodejenis.Substring(0, 1) == "4")
                o.IDRekening = DataFormat.GetLong(kodejenis + col1.Substring(27).Replace(".", ""));//.IntToStringWithLeftPad(7));
            else
                o.IDRekening = DataFormat.GetLong(kodejenis + col1.Substring(25).Replace(".", ""));//.IntToStringWithLeftPad(7));
            if (o.IDRekening == 0)
            {
                o.IDRekening = 0;
            }
            if (o.IDRekening.ToString().Substring(0, 1) == "4")
                o.Jenis = 1;
            else
                o.Jenis = 3;

            o.PPKD = 0;
            o.Plafon = (DataFormat.GetString(gridData.Rows[i].Cells[2].Value)).FormatUangReportKeDecimal();
            o.IDDinas = ctrlDinas1.GetID();
            return o;


        }

        private void frmImportAPBD_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            ctrlHeader1.SetCaption("Import Data Importan dari SIPD", "");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ArgumenThread arg = (ArgumenThread)e.Argument;
                if (arg.State == 1)
                {
                    // ShowSheet();


                    MyApp = new Excel.Application();
                    MyApp.Visible = false;
                    MyBook = MyApp.Workbooks.Open(txtFileName.Text);
                    MySheet = (Excel.Worksheet)MyBook.Sheets[1];


                    //if (tables != null && tables.Rows.Count > 0)
                    //{

                    //   foreach (DataRow row in tables.Rows)
                    //   {
                    //       arg.ListName.Add(row["TABLE_NAME"].ToString());
                    //  }
                    // }

                    foreach (Excel.Worksheet s in MyBook.Sheets)
                    {

                        arg.ListName.Add(s.Name);
                        //worksheetsComboBox.Items.Add(s.Name);
                    }



                    //System.Data.OleDb.OleDbConnection MyCnn;
                    //System.Data.DataSet DSet;
                    //System.Data.OleDb.OleDbDataAdapter MyCmd;
                    //List<string> _namaSheet = new List<string>();

                    //MyCnn = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtFileName.Text + " ;  Extended Properties=Excel 8.0");//+ Extended Properties=Excel 8.0;");

                    //////string sourceConnectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES'", txtFileName.Text);

                    ////MyCnn = new System.Data.OleDb.OleDbConnection(sourceConnectionString);

                    //MyCnn.Open();


                    //DataTable tables = MyCnn.GetSchema("Tables", new String[] { null, null, null, "TABLE" });
                    //MyCnn.Dispose();


                    ////Add each table name to the combo box
                    //if (tables != null && tables.Rows.Count > 0)
                    // {

                    //    foreach (DataRow row in tables.Rows)
                    //   {
                    //       arg.ListName.Add(row["TABLE_NAME"].ToString());
                    //  }
                    // }

                }
                else
                {


                    arg.Tahun = GlobalVar.TahunAnggaran;
                    arg.IDDInas = ctrlDinas1.GetID();
                    TAnggaranRekeningLogic oRekLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
                    oRekLogic.SimpanImport2(arg.LstRek, arg.IDDInas, arg.Tahun, arg.Tahap, arg.HanyaPlafon);

                    TKegiatanAPBDLogic oKegLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
                    oKegLogic.SimpanImport(arg.LstKeg, arg.IDDInas, arg.Tahun);
                    arg.LstRek.Clear();
                    TProgramAPBDLogic oPRGLogic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran);
                    oPRGLogic.SimpanImport(arg.LstPrg, arg.IDDInas, arg.Tahun);

                }

                e.Result = arg;//.ToString();
            }
            catch (Exception ex)
            {

            }


        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            worksheetsComboBox.Items.Clear();
            foreach (string s in _shetNames)
            {
                worksheetsComboBox.Items.Add(s);
            }
            _fStatus.Close();
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

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdLihatHasil_Click(object sender, EventArgs e)
        {

            //frmReportViewer fV = new frmReportViewer();
            //ParameterLaporan _p = new ParameterLaporan();
            ////fV.SetTanggal(tanggalPerda.Value.Date);
            //fV.Profile = mprofile;

            //_p.HanyaPlafon = false;
            //_p.HanyaPlafon = chkPlafon.Checked;

            //_p.Tahun = GlobalVar.TahunAnggaran;
            ///// if (GlobalVar.TahunAnggaran == 2017)
            //// {
            //_p.Tahap = 1;
            //// } else
            ////    _p.Tahap = 1;

            //// if (_p.Tahap == 0)
            //// {
            /////   MessageBox.Show("Belum pilih tahapan.");
            ///// return;
            ///// }
            //_p.Keternagan = "";//txtKeterangan.Text;
            //_p.NamaUser = GlobalVar.Pengguna.UserID;
            //_p.AwalHalaman = 1;//DataFormat.GetInteger(txtAwalNoHal.Text);
            //_p.HanyaUrusanPokok = false;
            //_p.JabatanPimpinan = "";
            //_p.NomorPerda = "";
            //_p.TampilkanTanggal = false;
            //_p.NamaLaporan = "";
            //_p.NamaPerda = "";


            //if (ctrlDinas1.GetID() == 0)
            //{
            //    MessageBox.Show("Sila Pilih Dinas terlebih dahulu.");
            //    return;

            //}
            //_p.IDDinas = ctrlDinas1.GetID();
            //_p.LastLevel = 5;
            //_p.Tahap = -1;


            //fV.PerdaIII(_p);


            //fV.Show();

        }

        private void cmdEditPlafon_Click(object sender, EventArgs e)
        {
            frmInputPlafonPerRekening fINput = new frmInputPlafonPerRekening(1);
            fINput.SetMode(1);
            if (GlobalVar.TahunAnggaran == 2021)
                fINput.Profile = 3;

            fINput.Show();
        }

        private void worksheetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            //ImportAnggaranRekening();
            ImportAnggaranRekening2024();


        }
        private void ImportAnggaranRekening(){

            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, 0, 3);
            List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();
            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 3);

            List<TSumberDana> _lsd = new List<TSumberDana>();
            string KodeUnit = "";
            int iddinas = 0;
            int noUnit = 0;
            int idUnit = 0;
            for (int i = 0; i < gridData2.Rows.Count; i++)
            {

                if (gridData2.Rows[i].Cells[1].Value != null && gridData2.Rows[i].Cells[15].Value != null && gridData2.Rows[i].Cells[17].Value != null)
                {
                    TAnggaranRekening o = new TAnggaranRekening();
                    o.Tahun = GlobalVar.TahunAnggaran;
                    o.Jenis = 3;
                    if (iddinas != DataFormat.GetInteger(gridData2.Rows[i].Cells[20].Value))
                    {
                        noUnit = noUnit + 1;
                    }
                    o.IDDinas = DataFormat.GetInteger(gridData2.Rows[i].Cells[20].Value);

                    o.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[5].Value.ToString().Replace(".", ""));
                    o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[11].Value.ToString().Replace(".", ""));
                    //if (KodeUnit.Trim() != DataFormat.GetString(gridData2.Rows[i].Cells[9].Value).Trim())
                    //{
                    //    KodeUnit = "";
                    //    noUnit = noUnit + 1;
                    //    idUnit = o.IDDinas + noUnit;
                    //}
                    o.KodeUK = noUnit;
                    o.IDKegiatan = DataFormat.GetInteger(gridData2.Rows[i].Cells[13].Value.ToString().Replace(".", ""));
                    o.IDSubKegiatan = DataFormat.GetLong(gridData2.Rows[i].Cells[15].Value.ToString().Replace(".", ""));
                    o.IDRekening = DataFormat.GetLong(gridData2.Rows[i].Cells[17].Value.ToString().Replace(".", ""));
                    o.PPKD = 0;
                    o.StatusUpdate = 0;
                    //o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    o.JumlahRKAP = DataFormat.GetDecimal(gridData2.Rows[i].Cells[19].Value.ToString().Replace(".", ""));
                    o.JumlahABT = DataFormat.GetDecimal(gridData2.Rows[i].Cells[19].Value.ToString().Replace(".", ""));

                    o.SumberDana = DataFormat.GetString(gridData2.Rows[i].Cells[21].Value.ToString());
                    
                    o.KodeUK = DataFormat.GetInteger(gridData2.Rows[i].Cells[22].Value.ToString().Replace(".", ""));
                    o.IDUnit= DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value.ToString().Replace(".", ""));

                    if (o.KodeUK > 1)
                    {
                        o.IDUnit = DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value.ToString().Replace(".", ""));
                    }
                    if (rb1.Checked == true )
                    o.Tahap = 5;
                    if (rbPergeseran.Checked == true)
                        o.Tahap = 3;
                    if (rb2.Checked == true)
                        o.Tahap = 3;



                    //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));

                    if (o.IDRekening > 0)
                        _lstRek.Add(o);
                }
                else
                {
                    // MessageBox.Show(i.ToString());
                }

            }




            oLogicRek.SImpanSIPD(_lstRek);


            MessageBox.Show("Import Anggaran Belanja Selesai");


        }
        private void ImportAnggaranRekening2024()
        {
            int jenis = 0;
            if (optBelanja.Checked == true)
            {
                jenis = 3;
                TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, 0, 3);
                List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();
                TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 3);

                List<TSumberDana> _lsd = new List<TSumberDana>();
                string KodeUnit = "";
                int iddinas = 0;
                int noUnit = 0;
                int idUnit = 0;
                for (int i = 0; i < gridData2.Rows.Count; i++)
                {

                    if (gridData2.Rows[i].Cells[1].Value != null && gridData2.Rows[i].Cells[20].Value != null
                        && gridData2.Rows[i].Cells[18].Value != null)
                    {
                        if (DataFormat.GetString(gridData2.Rows[i].Cells[18].Value).Length > 0)
                        {
                            TAnggaranRekening o = new TAnggaranRekening();
                            o.Tahun = GlobalVar.TahunAnggaran;
                            o.Jenis = 3;
                            if (iddinas != DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value))
                            {
                                noUnit = noUnit + 1;
                            }
                            if (DataFormat.GetInteger(gridData2.Rows[i].Cells[0].Value) == 1162)
                            {
                                o.KodeUK = DataFormat.GetInteger(DataFormat.GetString(gridData2.Rows[i].Cells[6].Value.ToString()).Substring(18, 4));
                            }
                            o.IDDinas = DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value);
                            //  o.KodeUK = DataFormat.GetInteger(gridData2.Rows[i].Cells[25].Value.ToString().Replace(".", ""));
                            o.KodeUK = DataFormat.GetInteger(DataFormat.GetString(gridData2.Rows[i].Cells[6].Value.ToString()).Substring(18, 4));
                            o.IDUnit = o.IDDinas + o.KodeUK;

                            o.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[8].Value.ToString().Replace(".", ""));
                            o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[10].Value.ToString().Replace(".", ""));


                            o.IDKegiatan = DataFormat.GetInteger(gridData2.Rows[i].Cells[12].Value.ToString().Replace(".", ""));
                            o.IDSubKegiatan = DataFormat.GetLong(gridData2.Rows[i].Cells[14].Value.ToString().Replace(".", ""));
                            o.IDRekening = DataFormat.GetLong(gridData2.Rows[i].Cells[18].Value.ToString().Replace(".", ""));
                            o.PPKD = DataFormat.GetInteger(gridData2.Rows[i].Cells[0].Value.ToString().Replace(".", ""));
                            o.TahapInput = DataFormat.GetInteger(gridData2.Rows[i].Cells[0].Value.ToString().Replace(".", "")); ;
                            o.KodeSumberDana = DataFormat.GetString(gridData2.Rows[i].Cells[16].Value.ToString());
                            o.NamaSumberDana = DataFormat.GetString(gridData2.Rows[i].Cells[17].Value.ToString().Trim());

                            o.JumlahRKA = DataFormat.GetDecimal(DataFormat.GetString(gridData2.Rows[i].Cells[20].Value.ToString().Replace(",", "")));







                            //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));

                            _lstRek.Add(o);
                        }
                    }
                    else
                    {
                        // MessageBox.Show(i.ToString());
                    }


                }
                if (oLogicRek.SimpanSIPDTemp(_lstRek, jenis) == true)
                {

                    
                    MessageBox.Show("Import Anggaran Belanja Selesai");
                }
                else
                {
                    MessageBox.Show("Kesalahan import data: " + oLogicRek.LastError());

                }
            } else
            {
                if(optPendapatan.Checked = true)
                {
                    jenis = 1;
                    TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, 0, 3);
                    List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();
                    TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 3);

                    List<TSumberDana> _lsd = new List<TSumberDana>();
                    for (int i = 0; i < gridData.Rows.Count; i++)
                    {

                        if (gridData.Rows[i].Cells[1].Value != null)
                        {
                            //  if (ctrlDinas1.GetID()== DataFormat.GetInteger(gridData.Rows[i].Cells[5].Value)){
                            TAnggaranRekening o = new TAnggaranRekening();
                            o.Tahun = GlobalVar.TahunAnggaran;
                            o.Jenis = 1;
                            o.IDDinas = DataFormat.GetInteger(gridData.Rows[i].Cells[6].Value);
                            o.IDUrusan = DataFormat.GetInteger(DataFormat.GetString(gridData.Rows[i].Cells[6].Value).Substring(0, 3));// DataFormat.GetInteger(gridData.Rows[i].Cells[0].Value.ToString().Replace(".", ""));
                            o.IDProgram = 0;
                            o.IDKegiatan = 0;
                            o.KodeUK = 0;
                            o.IDSubKegiatan = 0;
                            o.IDRekening = DataFormat.GetLong(gridData.Rows[i].Cells[0].Value.ToString().Replace(".", ""));
                            o.KodeSumberDana = "";// DataFormat.GetString(gridData2.Rows[i].Cells[16].Value.ToString());
                            o.NamaSumberDana = "";//DataFormat.GetString(gridData2.Rows[i].Cells[17].Value.ToString().Trim());

                            o.PPKD = 0;
                            o.StatusUpdate = 0;
                            o.Tahap = 3;
                            //o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                            o.JumlahRKAP = DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString().Replace(".", ""));
                            o.JumlahMurni = DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString().Replace(".", ""));
                            o.JumlahPergeseran = DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString().Replace(".", ""));

                            //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));

                            if (o.IDRekening > 0)
                                _lstRek.Add(o);
                        }
                    }

                    //}




                    oLogicRek.SimpanSIPDTemp(_lstRek, jenis);


                    MessageBox.Show("Import Anggaran Pendapatan Selesai");

                }
            }

        }

        private void cmdImportRekening_Click(object sender, EventArgs e)
        {


            //if (optPendapatan.Checked == true)
            //    ImportRekeningPendapatan();
            //if (optBelanja.Checked == true)
            if (optPendapatan.Checked == true)
            {
                ImportRekeningPendapatan();
            }
            if (chkPembiayaan.Checked == true)
            {
                ImportRekeningPembiayaan();
            }
            

            if (optBelanja.Checked== true)
            {
                ImportRekeningBelanja();
            }
            //IportRekeningBelanja();
            //IportRekening();
            //

            //if (optPakrt.Checked == true)
            //{
            //    TampilkanPaket();

            //}
        } 

        private void IportRekeningBelanja(){
            RekeningLogic oLogicRek = new RekeningLogic(GlobalVar.TahunAnggaran, RekeningLogic.E_REKENING_TYPE.REKENING_13);
            List<Rekening> _lstRek = new List<Rekening>();

            for (int i = 0; i < gridData2.Rows.Count; i++)
            {

                if (gridData2.Rows[i].Cells[1].Value != null && gridData2.Rows[i].Cells[15].Value != null && gridData2.Rows[i].Cells[17].Value != null)
                {
                    Rekening r = new Rekening();

                    r.ID = DataFormat.GetLong(gridData2.Rows[i].Cells[15].Value.ToString().Replace(".", ""));
                    r.IDParent = DataFormat.GetLong(gridData2.Rows[i].Cells[15].Value.ToString().Replace(".", "").Trim().Substring(0, 8) + "0000");
                    r.Nama = DataFormat.GetString(gridData2.Rows[i].Cells[16].Value.ToString());
                    r.Leaf = 1;
                    r.Root = 6;
                    r.Debet = -1;


                    //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));

                    if (r.ID > 0)
                        oLogicRek.Simpan(ref r);


                }

            }

        }
        private void IportRekening()
        {
            RekeningLogic oLogicRek = new RekeningLogic(GlobalVar.TahunAnggaran, RekeningLogic.E_REKENING_TYPE.REKENING_13);
            List<Rekening> _lstRek = new List<Rekening>();

            for (int i = 2; i < gridRekening.Rows.Count; i++)
            {

                if (gridRekening.Rows[i].Cells[0].Value != null )
                {
                    Rekening r = new Rekening();

                    r.Kode = DataFormat.GetString(gridRekening.Rows[i].Cells[0].Value).Replace(".", "");
                    r.Nama = DataFormat.GetString(gridRekening.Rows[i].Cells[1].Value.ToString()).Trim();
                    switch (r.Kode.Length)
                    {
                        case 1:
                            r.Leaf = 1;
                            r.Leaf = 0;
                            r.ID = DataFormat.GetLong(r.Kode.Trim() + "00000000000");
                            r.IDParent = 0;
                            if (r.Kode.Trim().Substring(0, 1) == "4")
                                r.Debet = -1;
                            else
                                r.Debet = 1;
                            r.NoBaris = 1;

                            break;
                        case 2:
                            r.Leaf = 2;
                            r.Leaf = 0;

                            r.ID = DataFormat.GetLong(r.Kode.Trim() + "0000000000");
                            r.IDParent = DataFormat.GetLong(r.Kode.Trim().Substring(0,1)+  "00000000000");

                            break;
                        case 4:
                            r.Leaf = 3;
                            r.Leaf = 0;
                            r.ID = DataFormat.GetLong(r.Kode.Trim() + "00000000");
                            r.IDParent = DataFormat.GetLong(r.Kode.Trim().Substring(0, 2) + "0000000000");
                            break;
                        case 6:
                            r.Leaf = 4;
                            r.Leaf = 0;
                            r.ID = DataFormat.GetLong(r.Kode.Trim() + "000000");
                            r.IDParent = DataFormat.GetLong(r.Kode.Trim().Substring(0, 4) + "00000000");
                            break;
                        case 8:
                            r.Leaf = 5;
                            r.Leaf = 0;
                            r.ID = DataFormat.GetLong(r.Kode.Trim() + "0000");
                            r.IDParent = DataFormat.GetLong(r.Kode.Trim().Substring(0, 6) + "000000");
                            break;
                        case 12:
                            r.Leaf = 6;
                            r.Leaf = 1;
                            r.ID = DataFormat.GetLong(r.Kode.Trim());
                            r.IDParent = DataFormat.GetLong(r.Kode.Trim().Substring(0, 8) + "0000");
                            break;
                     }
                    if (r.Kode.Trim().Substring(0, 1) == "4")
                        r.Debet = -1;
                    else
                        r.Debet = 1;

                    r.NoBaris = i;
                                      
                    oLogicRek.Simpan2024(r);


                }

            }

        }
        private void ImportRekeningPendapatan()
        {


            RekeningLogic oLogicRek = new RekeningLogic(GlobalVar.TahunAnggaran, RekeningLogic.E_REKENING_TYPE.REKENING_13);
            List<Rekening> _lstRek = new List<Rekening>();

            for (int i = 0; i < gridData.Rows.Count; i++)
            {

                if (gridData.Rows[i].Cells[0].Value != null )
                {
                    Rekening r = new Rekening();

                    //r.ID = DataFormat.GetLong(gridData2.Rows[i].Cells[15].Value.ToString().Replace(".", ""));
                    r.ID = DataFormat.GetLong(gridData.Rows[i].Cells[0].Value.ToString().Replace(".", ""));

                    r.IDParent = DataFormat.GetLong(gridData.Rows[i].Cells[0].Value.ToString().Replace(".", "").Trim().Substring(0, 8) + "0000");
                    r.Nama = DataFormat.GetString(gridData.Rows[i].Cells[1].Value.ToString() );
                    r.Leaf = 1;
                    r.Root = 6;
                    r.Debet = -1;


                    //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));

                    if (r.ID > 0)
                        oLogicRek.Simpan(ref r);


                }

            }

        }
        private void ImportRekeningPembiayaan()
        {


            RekeningLogic oLogicRek = new RekeningLogic(GlobalVar.TahunAnggaran, RekeningLogic.E_REKENING_TYPE.REKENING_13);
            List<Rekening> _lstRek = new List<Rekening>();

            for (int i = 0; i < gridPembiayaan.Rows.Count; i++)
            {

                if (gridPembiayaan.Rows[i].Cells[0].Value != null)
                {
                    Rekening r = new Rekening();

                    //r.ID = DataFormat.GetLong(gridPembiayaan2.Rows[i].Cells[15].Value.ToString().Replace(".", ""));
                    r.ID = DataFormat.GetLong(gridPembiayaan.Rows[i].Cells[0].Value.ToString().Replace(".", ""));

                    r.IDParent = DataFormat.GetLong(gridPembiayaan.Rows[i].Cells[0].Value.ToString().Replace(".", "").Trim().Substring(0, 8) + "0000");
                    r.Nama = DataFormat.GetString(gridPembiayaan.Rows[i].Cells[1].Value.ToString());
                    r.Leaf = 1;
                    r.Root = 6;
                    r.Debet = -1;


                    //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));

                    if (r.ID > 0)
                        oLogicRek.Simpan(ref r);


                }

            }

        }
        private void ImportRekeningBelanja()
        {


            RekeningLogic oLogicRek = new RekeningLogic(GlobalVar.TahunAnggaran, RekeningLogic.E_REKENING_TYPE.REKENING_13);
            List<Rekening> _lstRek = new List<Rekening>();

            for (int i = 0; i < gridData2.Rows.Count; i++)
            {

                if (gridData2.Rows[i].Cells[18].Value != null)
                {
                    Rekening r = new Rekening();

                    //r.ID = DataFormat.GetLong(gridData2.Rows[i].Cells[15].Value.ToString().Replace(".", ""));
                    r.ID = DataFormat.GetLong(gridData2.Rows[i].Cells[18].Value.ToString().Replace(".", ""));

                    r.IDParent = DataFormat.GetLong(gridData2.Rows[i].Cells[18].Value.ToString().Replace(".", "").Trim().Substring(0, 8) + "0000");
                    r.Nama = DataFormat.GetString(gridData2.Rows[i].Cells[19].Value.ToString());
                    r.Leaf = 1;
                    r.Root = 6;
                    r.Debet = -1;


                    //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));

                    if (r.ID > 0)
                        oLogicRek.Simpan(ref r);


                }

            }

        }

        private void cmdImportSubegiatan_Click(object sender, EventArgs e)
        {

            TSubKegiatanLogic otSUbRek = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, 3);//,RekeningLogic.E_REKENING_TYPE.REKENING_13 );
            List<TSubKegiatan> _lstRek = new List<TSubKegiatan>();
            long oldidsubkegiatan = 0;
            SKPDLogic oSKPDLOgic = new SKPDLogic(GlobalVar.TahapAnggaran);
            List<SKPD> lstSKPD = oSKPDLOgic.Get(2025);
            for (int i = 0; i < gridData2.Rows.Count; i++)
            {

                if (gridData2.Rows[i].Cells[13].Value != null && gridData2.Rows[i].Cells[13].Value != null && gridData2.Rows[i].Cells[14].Value != null)
                {
                    string ckode = DataFormat.GetString(gridData2.Rows[i].Cells[4].Value.ToString());

                        TSubKegiatan o = new TSubKegiatan();
                        o.Tahun = 2025;
                        int cunit =DataFormat.GetInteger( DataFormat.GetString(gridData2.Rows[i].Cells[6].Value.ToString()).Substring(18,4));
                    //int cunit =DataFormat.GetInteger(gridData2.Rows[i].Cells[22].Value.ToString().Replace(".", ""));

                    
                    o.Tahun = GlobalVar.TahunAnggaran;
                        
                    if (DataFormat.GetInteger(gridData2.Rows[i].Cells[0].Value) == 1162)
                    {
                        o.KodeUK = DataFormat.GetInteger(DataFormat.GetString(gridData2.Rows[i].Cells[6].Value.ToString()).Substring(18, 4));
                    }
                    o.IDDinas = DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value);
                    //  o.KodeUK = DataFormat.GetInteger(gridData2.Rows[i].Cells[25].Value.ToString().Replace(".", ""));
                    o.KodeUK = DataFormat.GetInteger(DataFormat.GetString(gridData2.Rows[i].Cells[6].Value.ToString()).Substring(18, 4));
                    o.IDUnit = o.IDDinas + o.KodeUK;

                    o.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[8].Value.ToString().Replace(".", ""));
                    o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[10].Value.ToString().Replace(".", ""));


                    o.IDKegiatan = DataFormat.GetInteger(gridData2.Rows[i].Cells[12].Value.ToString().Replace(".", ""));
                    o.IDSubKegiatan = DataFormat.GetLong(gridData2.Rows[i].Cells[14].Value.ToString().Replace(".", ""));
                    o.Nama = DataFormat.GetString(gridData2.Rows[i].Cells[15].Value);




                    ProgramKegiatan pk = new ProgramKegiatan();

                        pk.IDDInas= DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value);

                      
                        pk.KodeUK = cunit;// DataFormat.GetInteger(gridData2.Rows[i].Cells[22].Value.ToString().Replace(".", ""));
                        //pk.IDUnit = o.IDDinas + cunit;// DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value.ToString().Replace(".", ""));



                        if (o.KodeUK > 1)
                        {
                            //pk.IDUnit = DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value.ToString().Replace(".", ""));
                        }
                        
                        pk.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[8].Value.ToString().Replace(".", ""));
                        pk.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[10].Value.ToString().Replace(".", ""));
                        //pk.IDUnit = DataFormat.GetInteger(gridData2.Rows[i].Cells[24].Value.ToString().Replace(".", ""));
                        
                        
                        pk.IDKegiatan = DataFormat.GetInteger(gridData2.Rows[i].Cells[12].Value.ToString().Replace(".", ""));
                        pk.NamaKegiatan = DataFormat.GetString(gridData2.Rows[i].Cells[13].Value.ToString());
                        pk.NamaProgram = DataFormat.GetString(gridData2.Rows[i].Cells[11].Value.ToString());
                        pk.NamaSubKegiatan = DataFormat.GetString(gridData2.Rows[i].Cells[15].Value.ToString());
                        pk.NamaUrusan = DataFormat.GetString(gridData2.Rows[i].Cells[9].Value.ToString());
                        pk.IDSubKegiatan = DataFormat.GetLong(gridData2.Rows[i].Cells[14].Value.ToString().Replace(".", ""));
                        pk.Tahun = 2025;
                        pk.NamaDinas = DataFormat.GetString(gridData2.Rows[i].Cells[7].Value.ToString());
                        pk.KodeDinas = DataFormat.GetString(gridData2.Rows[i].Cells[6].Value.ToString());
                        o.PK = pk;
                        otSUbRek.SimpanSIPD(o);//.Add(o);
                        oldidsubkegiatan = o.IDSubKegiatan;
                        //}

                    


                }

            }
            MessageBox.Show("Selesai import Sub Kegiatan");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            importProgram();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            importKegiatan();
        }

        private void cmdImportPaket_Click(object sender, EventArgs e)
        {
            MusrenmbangLogic oPaketLogic = new MusrenmbangLogic(GlobalVar.TahunAnggaran);

            try
            {
                for (int i = 0; i < gridPaket.Rows.Count; i++)
                {

                    int dinas = ctrlDinas1.GetID();

                    if (gridPaket.Rows[i].Cells[1].Value != null && gridPaket.Rows[i].Cells[2].Value != null)
                    {
                        Musrenmbang o = new Musrenmbang();

                        o.IDDInas = dinas;
                        o.IDSUbKegiatan = DataFormat.GetLong(gridPaket.Rows[i].Cells[0].Value.ToString().Replace(".", "").Trim());
                        //     o.IDRekening = DataFormat.GetLong(gridData2.Rows[i].Cells[15].Value.ToString().Replace(".", ""));
                        o.nama = DataFormat.GetString(gridPaket.Rows[i].Cells[1].Value.ToString());
                        o.volume = 1;// DataFormat.GetDecimal(gridPaket.Rows[i].Cells[2].Value.ToString().Replace(".", ""));
                        o.satuan = DataFormat.GetString(gridPaket.Rows[i].Cells[3].Value.ToString().Replace(".", ""));
                        o.pagu = DataFormat.GetDecimal(gridPaket.Rows[i].Cells[4].Value.ToString().Replace(".", ""));

                        oPaketLogic.Simpan(o);
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }





        }

        private void cmdImportPendapatan_Click(object sender, EventArgs e)
        {

            //ImportUraianPendapatan2024();
            //return;


            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, 0, 3);
            List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();
            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 3);

            List<TSumberDana> _lsd = new List<TSumberDana>();
            for (int i = 0; i < gridData.Rows.Count; i++)
            {

                if (gridData.Rows[i].Cells[1].Value != null)
                {
                    //  if (ctrlDinas1.GetID()== DataFormat.GetInteger(gridData.Rows[i].Cells[5].Value)){
                    TAnggaranRekening o = new TAnggaranRekening();
                    o.Tahun = GlobalVar.TahunAnggaran;
                    o.Jenis = 1;
                    o.IDDinas = DataFormat.GetInteger(gridData.Rows[i].Cells[6].Value);
                    o.IDUrusan = DataFormat.GetInteger(DataFormat.GetString(gridData.Rows[i].Cells[6].Value).Substring(0, 3));// DataFormat.GetInteger(gridData.Rows[i].Cells[0].Value.ToString().Replace(".", ""));
                    o.IDProgram = 0;
                    o.IDKegiatan = 0;
                    o.IDSubKegiatan = 0;
                    o.IDRekening = DataFormat.GetLong(gridData.Rows[i].Cells[0].Value.ToString().Replace(".", ""));

                    o.PPKD = 0;
                    o.StatusUpdate = 0;
                    o.Tahap =3;
                    //o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    o.JumlahRKAP = DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString().Replace(".", ""));
                    o.JumlahMurni= DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString().Replace(".", ""));
                    o.JumlahPergeseran= DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString().Replace(".", ""));

                    //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));

                    if (o.IDRekening > 0)
                        _lstRek.Add(o);
                }
            }

            //}




            oLogicRek.SImpanSIPD(_lstRek);


            MessageBox.Show("Import Anggaran Uraian Selesai");


        }

        private void cmdImportSUmberDana_Click(object sender, EventArgs e)
        {
            ImportSumberDana2024();
            MessageBox.Show("Import SumberDana Selesai");
            return;
            /*
            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, 0, 3);
            List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();
            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 3);

            List<TSumberDana> _lsd = new List<TSumberDana>();
            for (int i = 0; i < gridData2.Rows.Count; i++)
            {

                if (gridData2.Rows[i].Cells[1].Value != null && gridData2.Rows[i].Cells[15].Value != null && gridData2.Rows[i].Cells[17].Value != null)
                {
                    TAnggaranRekening o = new TAnggaranRekening();
                    o.Tahun = GlobalVar.TahunAnggaran;
                    o.Jenis = 3;
                    o.IDDinas = DataFormat.GetInteger(gridData2.Rows[i].Cells[18].Value);

                    o.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[5].Value.ToString().Replace(".", ""));
                    o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[9].Value.ToString().Replace(".", ""));
                    o.IDKegiatan = DataFormat.GetInteger(gridData2.Rows[i].Cells[11].Value.ToString().Replace(".", ""));
                    o.IDSubKegiatan = DataFormat.GetLong(gridData2.Rows[i].Cells[13].Value.ToString().Replace(".", ""));
                    o.IDRekening = DataFormat.GetLong(gridData2.Rows[i].Cells[15].Value.ToString().Replace(".", ""));
                    o.PPKD = 0;
                    o.StatusUpdate = 0;
                    o.SumberDana = DataFormat.GetString(gridData2.Rows[i].Cells[19].Value.ToString().Replace(".", ""));


                    //o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    o.JumlahRKAP = DataFormat.GetDecimal(gridData2.Rows[i].Cells[17].Value.ToString().Replace(".", ""));
                    o.Tahap = 1;




                    if (o.IDRekening > 0)
                        _lstRek.Add(o);
                }
                else
                {
                    // MessageBox.Show(i.ToString());
                }
           */
             
            //}




            //oLogicRek.SimpanSumberDana(_lstRek);


            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // if (optBelanja.Checked == true ){
            //        ImportBelanja();
            //    }
            //    if (optPendapatan.Checked == true){
            //        cmdImportPendapatan ()
            //    }


            //} 
            //private void ImportBelanja{} 

            //importUrusan();

            //importProgram();


            UnitKerjaLogic  oLogicRek = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
            List<Unit> _lstRek = new List<Unit>();
            
            string KodeUnit = "";
            int iddinas = 0;
            int noUnit = 0;
            int idUnit = 0;
            for (int i = 0; i < gridData2.Rows.Count; i++)
            {

                if (gridData2.Rows[i].Cells[1].Value != null && gridData2.Rows[i].Cells[15].Value != null && gridData2.Rows[i].Cells[17].Value != null)
                {
                    Unit o = new Unit();
                    o.SKPD= DataFormat.GetInteger(gridData2.Rows[i].Cells[20].Value);
                    
                    o.Kode= noUnit;

                    o.ID = DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value.ToString().Replace(".", ""));
                    if (idUnit != o.ID)
                    {//    gridData2.Rows[row].Cells[10].Value
                        idUnit = o.ID;
                        o.Nama =  DataFormat.GetString (gridData2.Rows[i].Cells[10].Value);

                        o.KodeKategori = DataFormat.GetInteger(o.SKPD.ToString().Substring(0, 1));
                        o.KodeUrusan = DataFormat.GetInteger(o.SKPD.ToString().Substring(1, 2));
                        o.KodeSKPD = DataFormat.GetInteger(o.SKPD.ToString().Substring(3, 2));
                        //o.Kode = noUnit+1;
                        o.Kode = DataFormat.GetInteger(gridData2.Rows[i].Cells[22].Value.ToString().Replace(".", ""));


                        oLogicRek.Simpan(ref o);
                    }

                    
                }
                else
                {
                    // MessageBox.Show(i.ToString());
                }

            }



            MessageBox.Show("ImportUnit selesai");



        }

        private void cmdImportUraian_Click(object sender, EventArgs e)
        {
           // ImportUraian();
            Cursor.Current = Cursors.WaitCursor;
            ImportUraian2024();

            MessageBox.Show("Selesai Import Uraian");
            Cursor.Current = Cursors.Default;
        }
        private void ImportUraian(){

            
            int pIDDInas = ctrlDinas1.GetID();

            TAnggaranUraianLogic oLogicRek = new TAnggaranUraianLogic(pIDDInas, GlobalVar.TahunAnggaran, 1, 3);
            List<TAnggaranUraian> _lstRek = new List<TAnggaranUraian>();
            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 3);

            List<TSumberDana> _lsd = new List<TSumberDana>();


            for (int i = 0; i < gridUraian.Rows.Count; i++)
            {

                if (gridUraian.Rows[i].Cells[0].Value != null && gridUraian.Rows[i].Cells[5].Value != null && gridUraian.Rows[i].Cells[12].Value != null)
                {
                    TAnggaranUraian o = new TAnggaranUraian();
                    o.Tahun = GlobalVar.TahunAnggaran;
                    o.Jenis = 3;
                    o.IDDinas = pIDDInas;

                    
                    o.IDUrusan = DataFormat.GetInteger(gridUraian.Rows[i].Cells[2].Value.ToString().Replace(".", "").Substring(0,3));

                    o.IDProgram = DataFormat.GetInteger(gridUraian.Rows[i].Cells[2].Value.ToString().Replace(".", ""));

                    //o.KodeUK = noUnit;
                    o.IDKegiatan = DataFormat.GetInteger(gridUraian.Rows[i].Cells[3].Value.ToString().Replace(".", ""));
                    o.IDSubKegiatan = DataFormat.GetLong(gridUraian.Rows[i].Cells[4].Value.ToString().Replace(".", ""));
                    o.IDRekening = DataFormat.GetLong(gridUraian.Rows[i].Cells[5].Value.ToString().Replace(".", ""));
                    o.PPKD = 0;
                    o.StatusUpdate = 0;
                    //o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    //o.JumlahRKAP = DataFormat.GetDecimal(gridUraian.Rows[i].Cells[19].Value.ToString().Replace(".", ""));

                    //o.SumberDana = DataFormat.GetString(gridUraian.Rows[i].Cells[21].Value.ToString());
                    o.Vol = DataFormat.GetDouble(gridUraian.Rows[i].Cells[8].Value.ToString());
                    o.VolOlah = DataFormat.GetDouble(gridUraian.Rows[i].Cells[8].Value.ToString());
                    o.VolMurni= DataFormat.GetDouble(gridUraian.Rows[i].Cells[8].Value.ToString());
                    
                    o.Satuan =  DataFormat.GetString(gridUraian.Rows[i].Cells[9].Value.ToString());
                    o.Harga = DataFormat.GetDecimal(gridUraian.Rows[i].Cells[11].Value.ToString());
                    o.HargaMurni = DataFormat.GetDecimal(gridUraian.Rows[i].Cells[11].Value.ToString());
                    o.HargaOlah = DataFormat.GetDecimal(gridUraian.Rows[i].Cells[11].Value.ToString());
                    o.PPNMurni = DataFormat.GetDecimal(gridUraian.Rows[i].Cells[10].Value.ToString()); 
                    o.PPNOlah= DataFormat.GetDecimal(gridUraian.Rows[i].Cells[10].Value.ToString());

                    o.Label = "-";
                    o.IDStandardHarga = DataFormat.GetString(gridUraian.Rows[i].Cells[6].Value.ToString().Replace(".", ""));
                    o.JumlahMurni = DataFormat.GetDecimal(gridUraian.Rows[i].Cells[12].Value.ToString());
                    o.Jumlah= DataFormat.GetDecimal(gridUraian.Rows[i].Cells[12].Value.ToString());
                    o.JumlahOlah = DataFormat.GetDecimal(gridUraian.Rows[i].Cells[12].Value.ToString()); 
                    o.NoUrut = i;

                    o.KodeUK = 0;// DataFormat.GetInteger(gridUraian.Rows[i].Cells[22].Value.ToString().Replace(".", ""));
                   // o.IDUnit = DataFormat.GetInteger(gridUraian.Rows[i].Cells[23].Value.ToString().Replace(".", ""));
                    o.KodeSKPDSIPD = DataFormat.GetString(gridUraian.Rows[i].Cells[0].Value.ToString());
                    o.KodeUnit = DataFormat.GetString(gridUraian.Rows[i].Cells[1].Value.ToString());
                    o.KodeSH = DataFormat.GetString(gridUraian.Rows[i].Cells[6].Value.ToString().Replace(".", ""));
                    o.Uraian = DataFormat.GetString(gridUraian.Rows[i].Cells[7].Value.ToString());
                    //if (o.KodeUK > 1)
                    //{
                    //    o.IDUnit = DataFormat.GetInteger(gridUraian.Rows[i].Cells[23].Value.ToString().Replace(".", ""));
                    //}

                    o.Tahap = 1;

                    oLogicRek.SimpanSIPD(o, pIDDInas, GlobalVar.TahunAnggaran, 1);

                    //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));

                    //if (o.IDRekening > 0)
                    //    _lstRek.Add(o);
                }
                else
                {
                    // MessageBox.Show(i.ToString());
                }

            }

        }
        private void ImportUraianPendapatan2024()
        {


            int pIDDInas = ctrlDinas1.GetID();

            TAnggaranUraianLogic oLogicRek = new TAnggaranUraianLogic(pIDDInas, GlobalVar.TahunAnggaran, 1, 3);
            List<TAnggaranUraian> _lstRek = new List<TAnggaranUraian>();
            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 3);

            List<TSumberDana> _lsd = new List<TSumberDana>();


            for (int i = 0; i < gridData.Rows.Count; i++)
            {

                if (gridData.Rows[i].Cells[0].Value != null && gridData.Rows[i].Cells[1].Value != null)
                {
                    //gridData.Rows[row].Cells[0].Value = xlRange.Cells[i, 3].Value.ToString();//idrekening
                    //gridData.Rows[row].Cells[1].Value = xlRange.Cells[i, 4].Value.ToString();// nama
                    //gridData.Rows[row].Cells[2].Value = xlRange.Cells[i, 5].Value.ToString();//kode skpd 
                    //gridData.Rows[row].Cells[3].Value = xlRange.Cells[i, 6].Value.ToString();// nama SKPD
                    //gridData.Rows[row].Cells[4].Value = xlRange.Cells[i, 7].Value.ToString();//uraian
                    //gridData.Rows[row].Cells[5].Value = xlRange.Cells[i, 9].Value.ToString();//pagu
                    //gridData.Rows[row].Cells[6].Value = xlRange.Cells[i, 3].Value.ToString();//idskpd
                    TAnggaranUraian o = new TAnggaranUraian();
                    o.Tahun = 2025;
                    o.Jenis = 1;
                    o.IDDinas = DataFormat.GetInteger(gridData.Rows[i].Cells[6].Value.ToString().Replace(".", ""));
                    o.IDUrusan = DataFormat.GetInteger(gridData.Rows[i].Cells[6].Value.ToString().Substring(0,3));
                    o.IDProgram = 0;
                    o.IDKegiatan = 0;
                    o.IDSubKegiatan = 0;
                    o.IDRekening = DataFormat.GetLong(gridData.Rows[i].Cells[0].Value.ToString().Replace(".", ""));

                    o.PPKD = 0;
                    o.StatusUpdate = 0;
                    //o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    //o.JumlahRKAP = DataFormat.GetDecimal(gridData.Rows[i].Cells[19].Value.ToString().Replace(".", ""));

                    //o.SumberDana = DataFormat.GetString(gridData.Rows[i].Cells[21].Value.ToString());
                    o.Vol = 1;// DataFormat.GetDouble(gridData.Rows[i].Cells[8].Value.ToString());
                    o.VolOlah = 1;// DataFormat.GetDouble(gridData.Rows[i].Cells[8].Value.ToString());
                    o.VolMurni = 1;//DataFormat.GetDouble(gridData.Rows[i].Cells[8].Value.ToString());

                    o.Satuan = "Tahun";// DataFormat.GetString(gridData.Rows[i].Cells[9].Value.ToString());
                    o.Harga = DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString());
                    o.HargaMurni = DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString());
                    o.HargaOlah = DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString());
                    o.PPNMurni = DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString());
                    o.PPNOlah = DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString());

                    o.Label = "-";
                    o.JumlahMurni = DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString());
                    o.Jumlah =  DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString());
                    o.JumlahOlah = DataFormat.GetDecimal(gridData.Rows[i].Cells[5].Value.ToString());
                    o.NoUrut = i;// DataFormat.GetInteger(gridData.Rows[i].Cells[0].Value);

                    o.KodeUK = 0;//DataFormat.GetInteger(gridData.Rows[i].Cells[].Value.ToString().Replace(".", ""));
                    if (o.KodeUK > 0)
                    {
                        // MessageBox.Show(o.KodeUK.ToString());
                    }
                    o.ID = i;// DataFormat.GetInteger(gridData.Rows[i].Cells[0].Value.ToString().Replace(".", ""));
                    //o.IdUIDUnit = DataFormat.GetInteger(gridData.Rows[i].Cells[24].Value.ToString().Replace(".", ""));

                    o.KodeSKPDSIPD = DataFormat.GetString(gridData.Rows[i].Cells[2].Value.ToString());
                    o.KodeUnit = DataFormat.GetString(gridData.Rows[i].Cells[6].Value.ToString());

                    o.IDStandardHarga = "";// DataFormat.GetString(gridData.Rows[i].Cells[20].Value.ToString());
                    o.KodeSH = "";// DataFormat.GetString(gridData.Rows[i].Cells[20].Value.ToString());
                    o.Uraian = DataFormat.GetString(gridData.Rows[i].Cells[4].Value.ToString());

                    //if (o.IDStandardHarga.Trim().Length == 0)
                    //{
                    //    o.IDStandardHarga = DataFormat.GetString(gridData.Rows[i].Cells[18].Value.ToString());
                    //    o.KodeSH = DataFormat.GetString(gridData.Rows[i].Cells[18].Value.ToString());
                    //    o.Uraian = DataFormat.GetString(gridData.Rows[i].Cells[19].Value.ToString());

                    //}
                    o.Tahap = 1;
                    o.Label = "-";
                    o.Leaf = 1;
                    o.Plafon = 0;
                    o.PPNABT = 0;
                    o.PPNGeser = 0;
                    o.PPNMurni = 0;
                    o.PPNOlah = 0;
                    o.PPKD = 0;

                    //if (o.IDDinas != 1020100)
                    oLogicRek.SimpanSIPD2024(o);// pIDDInas, GlobalVar.TahunAnggaran, 1);

                }
                else
                {
                    // MessageBox.Show(i.ToString());
                }

            }

        }
        private void ImportUraian2024()
        {


            int pIDDInas = ctrlDinas1.GetID();

            TAnggaranUraianLogic oLogicRek = new TAnggaranUraianLogic(pIDDInas, GlobalVar.TahunAnggaran, 1, 3);
            List<TAnggaranUraian> _lstRek = new List<TAnggaranUraian>();
            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 3);

            List<TSumberDana> _lsd = new List<TSumberDana>();


            for (int i = 0; i < gridData2.Rows.Count; i++)
            {

                if (gridData2.Rows[i].Cells[0].Value != null &&
                    gridData2.Rows[i].Cells[5].Value != null && 
                    gridData2.Rows[i].Cells[12].Value != null && 
                    gridData2.Rows[i].Cells[18].Value!=null 
                    )
                {
                    TAnggaranUraian o = new TAnggaranUraian();
                    o.Tahun = 2024;
                    o.Jenis = 3;
                    o.IDDinas = DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value.ToString().Replace(".", ""));
                    o.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[8].Value.ToString().Replace(".", ""));
                    o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[10].Value.ToString().Replace(".", ""));
                    o.IDKegiatan = DataFormat.GetInteger(gridData2.Rows[i].Cells[12].Value.ToString().Replace(".", ""));
                    o.IDSubKegiatan = DataFormat.GetLong(gridData2.Rows[i].Cells[14].Value.ToString().Replace(".", ""));
                    o.IDRekening = DataFormat.GetLong(gridData2.Rows[i].Cells[18].Value.ToString().Replace(".", ""));
                    
                    o.PPKD = 0;
                    o.StatusUpdate = 0;
                    //o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    //o.JumlahRKAP = DataFormat.GetDecimal(gridData2.Rows[i].Cells[19].Value.ToString().Replace(".", ""));

                    //o.SumberDana = DataFormat.GetString(gridData2.Rows[i].Cells[21].Value.ToString());
                    o.Vol = 1;// DataFormat.GetDouble(gridData2.Rows[i].Cells[8].Value.ToString());
                    o.VolOlah = 1;// DataFormat.GetDouble(gridData2.Rows[i].Cells[8].Value.ToString());
                    o.VolMurni = 1;//DataFormat.GetDouble(gridData2.Rows[i].Cells[8].Value.ToString());

                    o.Satuan = "Tahun";// DataFormat.GetString(gridData2.Rows[i].Cells[9].Value.ToString());
                    o.Harga = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                    o.HargaMurni = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                    o.HargaOlah = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                    o.PPNMurni = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                    o.PPNOlah = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());

                    o.Label = "-";
                    o.JumlahMurni = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                    o.Jumlah = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                    o.JumlahOlah = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                    o.NoUrut = DataFormat.GetInteger(gridData2.Rows[i].Cells[0].Value); 

                    o.KodeUK =  DataFormat.GetInteger(gridData2.Rows[i].Cells[25].Value.ToString().Replace(".", ""));
                    if (o.KodeUK > 0)
                    {
                       // MessageBox.Show(o.KodeUK.ToString());
                    }
                    o.ID  = DataFormat.GetInteger(gridData2.Rows[i].Cells[0].Value.ToString().Replace(".", ""));
                    //o.IdUIDUnit = DataFormat.GetInteger(gridData2.Rows[i].Cells[24].Value.ToString().Replace(".", ""));

                    o.KodeSKPDSIPD = DataFormat.GetString(gridData2.Rows[i].Cells[4].Value.ToString());
                    o.KodeUnit = DataFormat.GetString(gridData2.Rows[i].Cells[6].Value.ToString());

                    o.IDStandardHarga = DataFormat.GetString(gridData2.Rows[i].Cells[20].Value.ToString());
                    o.KodeSH = DataFormat.GetString(gridData2.Rows[i].Cells[20].Value.ToString());
                    o.Uraian = DataFormat.GetString(gridData2.Rows[i].Cells[21].Value.ToString());

                    if (o.IDStandardHarga.Trim().Length == 0)
                    {
                        o.IDStandardHarga = DataFormat.GetString(gridData2.Rows[i].Cells[18].Value.ToString());
                        o.KodeSH = DataFormat.GetString(gridData2.Rows[i].Cells[18].Value.ToString());
                        o.Uraian = DataFormat.GetString(gridData2.Rows[i].Cells[19].Value.ToString());

                    }
                    o.Tahap = 1;
                    o.Label = "-";
                    o.Leaf = 1;
                    o.Plafon = 0;
                    o.PPNABT = 0;
                    o.PPNGeser = 0;
                    o.PPNMurni = 0;
                    o.PPNOlah = 0;
                    o.PPKD = 0;


                    oLogicRek.SimpanSIPD2024(o,3);// pIDDInas, GlobalVar.TahunAnggaran, 1);

                }
                else
                {
                    // MessageBox.Show(i.ToString());
                }

            }

        }
        private void ImportSumberDana2024()
        {
            int i = 0;
            try
            {
                int pIDDInas = ctrlDinas1.GetID();

                TAnggaranUraianLogic oLogicRek = new TAnggaranUraianLogic(pIDDInas, GlobalVar.TahunAnggaran, 1, 3);
                List<TAnggaranUraian> _lstRek = new List<TAnggaranUraian>();
                TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 3);

                List<TSumberDana> _lsd = new List<TSumberDana>();


                for ( i = 0; i < gridData2.Rows.Count; i++)
                {
                    if (i == 75)
                    {
                        MessageBox.Show(i.ToString());
                    }

                    if (gridData2.Rows[i].Cells[0].Value != null && gridData2.Rows[i].Cells[5].Value != null && gridData2.Rows[i].Cells[12].Value != null)
                    {
                        TAnggaranUraian o = new TAnggaranUraian();
                        o.Tahun = 2024;
                        o.Jenis = 3;
                        o.IDDinas = DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value.ToString().Replace(".", ""));
                        o.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[8].Value.ToString().Replace(".", ""));
                        o.IDProgram = DataFormat.GetInteger(gridData2.Rows[i].Cells[10].Value.ToString().Replace(".", ""));
                        o.IDKegiatan = DataFormat.GetInteger(gridData2.Rows[i].Cells[12].Value.ToString().Replace(".", ""));
                        o.IDSubKegiatan = DataFormat.GetLong(gridData2.Rows[i].Cells[14].Value.ToString().Replace(".", ""));
                        o.IDRekening = DataFormat.GetLong(gridData2.Rows[i].Cells[18].Value.ToString().Replace(".", ""));
                       
                        o.KodeSumberDana = DataFormat.GetString(gridData2.Rows[i].Cells[16].Value.ToString());
                        o.NamaSumberDana = DataFormat.GetString(gridData2.Rows[i].Cells[17].Value.ToString());
                        
                        o.PPKD = 0;
                        o.StatusUpdate = 0;
                        o.Vol = 1;// DataFormat.GetDouble(gridData2.Rows[i].Cells[8].Value.ToString());
                        o.VolOlah = 1;// DataFormat.GetDouble(gridData2.Rows[i].Cells[8].Value.ToString());
                        o.VolMurni = 1;//DataFormat.GetDouble(gridData2.Rows[i].Cells[8].Value.ToString());

                        o.Satuan = "Tahun";// DataFormat.GetString(gridData2.Rows[i].Cells[9].Value.ToString());
                        o.Harga = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                        o.HargaMurni = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                        o.HargaOlah = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                        o.PPNMurni = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                        o.PPNOlah = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());

                        o.Label = "-";
                        o.JumlahMurni = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                        o.Jumlah = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                        o.JumlahOlah = DataFormat.GetDecimal(gridData2.Rows[i].Cells[22].Value.ToString());
                        o.NoUrut = DataFormat.GetInteger(gridData2.Rows[i].Cells[0].Value);

                        o.KodeUK = DataFormat.GetInteger(gridData2.Rows[i].Cells[25].Value.ToString().Replace(".", ""));
                        if (o.KodeUK > 0)
                        {
                            // MessageBox.Show(o.KodeUK.ToString());
                        }
                        o.ID = DataFormat.GetInteger(gridData2.Rows[i].Cells[0].Value.ToString().Replace(".", ""));
                        //o.IdUIDUnit = DataFormat.GetInteger(gridData2.Rows[i].Cells[24].Value.ToString().Replace(".", ""));

                        o.KodeSKPDSIPD = DataFormat.GetString(gridData2.Rows[i].Cells[4].Value.ToString());
                        o.KodeUnit = DataFormat.GetString(gridData2.Rows[i].Cells[6].Value.ToString());

                        o.IDStandardHarga = DataFormat.GetString(gridData2.Rows[i].Cells[20].Value.ToString());
                        o.KodeSH = DataFormat.GetString(gridData2.Rows[i].Cells[20].Value.ToString());
                        o.Uraian = DataFormat.GetString(gridData2.Rows[i].Cells[21].Value.ToString());

                        if (o.IDStandardHarga.Trim().Length == 0)
                        {
                            o.IDStandardHarga = DataFormat.GetString(gridData2.Rows[i].Cells[18].Value.ToString());
                            o.KodeSH = DataFormat.GetString(gridData2.Rows[i].Cells[18].Value.ToString());
                            o.Uraian = DataFormat.GetString(gridData2.Rows[i].Cells[19].Value.ToString());

                        }
                        o.Tahap = 1;
                        o.Label = "-";
                        o.Leaf = 1;
                        o.Plafon = 0;
                        o.PPNABT = 0;
                        o.PPNGeser = 0;
                        o.PPNMurni = 0;
                        o.PPNOlah = 0;
                        o.PPKD = 0;

                        if (o.KodeSumberDana.Trim().Length == 0)
                        {
                            o.KodeSumberDana = "1.2.01.01.02";
                            o.NamaSumberDana = "Dana Transfer Umum-Dana Alokasi Umum";

                        }
                        if (oLogicRek.SimpanSumberDanaSIPD2024(o) == false)
                        {
                            MessageBox.Show("Kesalahan pada baris ke " + DataFormat.GetInteger(gridData2.Rows[i].Cells[0].Value) +
                                     " " + DataFormat.GetString(gridData2.Rows[i].Cells[17].Value.ToString().Replace(".", "")) + "  " +
                                     "Panjang: " + DataFormat.GetString(gridData2.Rows[i].Cells[17].Value.ToString().Replace(".", "")).Length.ToString());


                        }
                            ;// pIDDInas, GlobalVar.TahunAnggaran, 1);

                    }
                    else
                    {
                        // MessageBox.Show(i.ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( i.ToString() + "   " + ex.Message);
            }

        }

        private void btnImportKesehatan_Click(object sender, EventArgs e)
        {
            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, 0, 3);
            List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();
            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 3);

            List<TSumberDana> _lsd = new List<TSumberDana>();
            string KodeUnit = "";
            int iddinas = 1020100;
            int noUnit = 0;
            string kode;
            
            int idUrusan=0;
            int idProgram=0;
            int idkegiatan=0;
            long idSubKegiatan=0 ;

            int idUnit = 0;
            for (int i = 2; i < gridKesehatan.Rows.Count; i++)
            {

                
                kode = DataFormat.GetString (gridKesehatan.Rows[i].Cells[0].Value ).Replace(".", "").Replace(" ", "");
                
                if (gridKesehatan.Rows[i].Cells[0].Value != null )
                {
                    if (kode.Length == 10){
                        noUnit= DataFormat.GetInteger(gridKesehatan.Rows[i].Cells[3].Value );
                        idUrusan=DataFormat.GetInteger (kode.ToString().Substring(0,3));
                        idProgram =DataFormat.GetInteger (kode.ToString().Substring(0,5));
                        idkegiatan=DataFormat.GetInteger (kode.ToString().Substring(0,8));
                        idSubKegiatan=DataFormat.GetLong (kode);
                    }
                                            
                  if   (kode.Length == 12){


                    TAnggaranRekening o = new TAnggaranRekening();
                    o.Tahun = GlobalVar.TahunAnggaran;
                    o.Jenis = 3;
                    o.IDDinas =iddinas;// DataFormat.GetInteger(gridData2.Rows[i].Cells[20].Value);


                    o.IDUrusan =idUrusan;
                    o.IDProgram = idProgram;
                    o.KodeUK = noUnit;
                    o.IDKegiatan = idkegiatan;
                    o.IDSubKegiatan = idSubKegiatan;
                    o.IDRekening = DataFormat.GetLong(kode);
                    o.PPKD = 0;
                    o.StatusUpdate = 0;
                    //o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    o.JumlahRKAP = DataFormat.GetDecimal(gridKesehatan.Rows[i].Cells[2].Value.ToString());
                    
                    o.SumberDana ="";// DataFormat.GetString(gridData2.Rows[i].Cells[21].Value.ToString());
                    
                    o.KodeUK = noUnit;// DataFormat.GetInteger(gridData2.Rows[i].Cells[22].Value.ToString().Replace(".", ""));
                    o.IDUnit= iddinas+ noUnit;//DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value.ToString().Replace(".", ""));

                   
                        o.Tahap = 3;
         

          
                    if (o.IDRekening > 0)
                        _lstRek.Add(o);
                  }
                }
            }
            oLogicRek.SImpanSIPD(_lstRek);


            MessageBox.Show("Import Anggaran Belanja Selesai");


        

        }

        private void GetDataAnggaranDinkes()
        {
            TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
            List<AnggaranDInkes> lst = oLogic.GetAnggaranDinkes();
            int row = 0;
            int kodeuk = 0;
            foreach (AnggaranDInkes ad in lst) {
                gridKesehatan.Rows.Add();
                
                gridKesehatan.Rows[row].Cells[0].Value = ad.Kode ;
                gridKesehatan.Rows[row].Cells[1].Value = ad.Keterangan ;
                gridKesehatan.Rows[row].Cells[2].Value = ad.Nilai.ToString();

                if (ad.Kode.Replace(".", "").Replace(" ", "").Length == 10)
                    kodeuk = GetKodeUK(row);
                gridKesehatan.Rows[row].Cells[3].Value = kodeuk.ToString();
                row = row + 1;


                
            
            }
        //    public int  No { set; get; }
        
        //public string Kode { set; get; }
        //public string Keterangan { set; get; }
        //public decimal Nilai { set; get; }

        }

        private void cmdImportRek_Click(object sender, EventArgs e)
        {
            RekeningLogic oLogicRek = new RekeningLogic(GlobalVar.TahunAnggaran, RekeningLogic.E_REKENING_TYPE.REKENING_64 );
            List<Rekening> _lstRek = new List<Rekening>();

            for (int i = 2; i < gridRekening.Rows.Count; i++)
            {

                if (gridRekening.Rows[i].Cells[1].Value != null )
                {
                    Rekening r = new Rekening();

                    r.ID = DataFormat.GetLong(gridRekening.Rows[i].Cells[0].Value.ToString());
                    r.Nama = DataFormat.GetString(gridRekening.Rows[i].Cells[1].Value.ToString());
 

                    oLogicRek.SimpanRekeningDJPK (ref r);


                }

            }
            MessageBox.Show("Selesai");
        }

        private void cmdImportUrusan_Click(object sender, EventArgs e)
        {
            UrusanDinas oldud = new UrusanDinas();

            for (int i = 0; i < gridData2.Rows.Count; i++)
            {
                if (gridData2.Rows[i].Cells[2].Value != null)
                {

                    UrusanDinas ud = new UrusanDinas();

                    ud.Tahun = 2024;
                    ud.IDDinas = DataFormat.GetInteger(gridData2.Rows[i].Cells[23].Value.ToString().Replace(".", ""));
                    ud.IDUrusan = DataFormat.GetInteger(gridData2.Rows[i].Cells[8].Value.ToString().Replace(".", ""));
                    ud.UrusanPokok = 0;
                    UrusanDinasLogic oLogic = new UrusanDinasLogic(2024, 2);
                    if (oldud.IDDinas != ud.IDDinas || oldud.IDUrusan != ud.IDUrusan)
                    {
                        oLogic.Simpan(ud);
                        oldud = ud;
                    }
                }
            }
            MessageBox.Show("Import Urusan Selesai.");
        }

        private void cmdImportKeluaran_Click(object sender, EventArgs e)
        {

            TAnggaranRekeningLogic oLogicAnggaran = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
            int dinas = ctrlDinas1.GetID();
            oLogicAnggaran.PerbaikiABT(dinas);
            return;


            UrusanDinas oldud = new UrusanDinas();
            int iddinas = 0;
            string keluaran;
            string target;
            string satuan;

            string keluaransub="";
            string targetsub = "";
            string satuansub = "";
            TSubKegiatanLogic oLogic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, 1);
            int posisiSpasi=0;
            char spasi=' ';
            string keterangankeluaran = "";
            string keterangankeluaransub = "";
             keluaran="";
                target = ""; 
                satuan = "";
                for (int i = 0; i < gridKeluaran.Rows.Count; i++)
            {
               

                if (gridKeluaran.Rows[i].Cells[1].Value != null &&
                    gridKeluaran.Rows[i].Cells[2].Value != null &&
                        gridKeluaran.Rows[i].Cells[3].Value != null)


                {

                    iddinas = DataFormat.GetInteger(gridKeluaran.Rows[i].Cells[0].Value);



                    int idProgram =
                        DataFormat.GetInteger(
                        DataFormat.GetString(gridKeluaran.Rows[i].Cells[1].Value).Replace(".","")+
                        DataFormat.GetString(gridKeluaran.Rows[i].Cells[2].Value).Replace(".","")+
                        DataFormat.GetString(gridKeluaran.Rows[i].Cells[3].Value).Replace(".", "")
                        );
                    if (idProgram == 10102)
                    {
                        idProgram = 10102;
                    }
                    if (DataFormat.GetString(gridKeluaran.Rows[i].Cells[4].Value) ==""){
                        if (gridKeluaran.Rows[i].Cells[7].Value !=null){
                            if (DataFormat.GetString(gridKeluaran.Rows[i].Cells[7].Value) != "")
                            {
                                keluaran = DataFormat.GetString(gridKeluaran.Rows[i].Cells[7].Value);
                                keterangankeluaran = DataFormat.GetString(gridKeluaran.Rows[i].Cells[8].Value);
                                if (keterangankeluaran.Contains(" ") == true)
                                {
                                    posisiSpasi = keterangankeluaran.IndexOf(spasi);
                                    if (posisiSpasi > 0)
                                    {
                                        target = keterangankeluaran.Substring(0, posisiSpasi);
                                        satuan = keterangankeluaran.Substring(posisiSpasi + 1);

                                    }
                                }
                                else
                                {
                                    target = "1";
                                    satuan = "Tahun";
                                }
                            }
                            else
                            {
                                keluaran = DataFormat.GetString(gridKeluaran.Rows[i].Cells[6].Value);
                                target = "1";
                                satuan ="Tahun";


                            }
                          
                            oLogic.SimpanKeluaranProgram(iddinas, idProgram, 0, keluaran, target, satuan, keluaransub, targetsub, satuansub);

                        }
                        else
                        {
                            keluaran = DataFormat.GetString(gridKeluaran.Rows[i].Cells[6].Value);
                            target = "1";
                            satuan = "Tahun";

                        }

                    
                    }

                    long idsubkegiatan  =0;

                    if (gridKeluaran.Rows[i].Cells[4].Value != null &&
                            gridKeluaran.Rows[i].Cells[5].Value != null)
                    {
                        if (DataFormat.GetInteger(gridKeluaran.Rows[i].Cells[4].Value.ToString().Replace(".", "")) > 0 &&
                             DataFormat.GetInteger(gridKeluaran.Rows[i].Cells[5].Value.ToString().Replace(".", "")) > 0)
                        {


                            idsubkegiatan = DataFormat.GetLong(
                                    DataFormat.GetString(gridKeluaran.Rows[i].Cells[1].Value).Replace(".", "") +
                                    DataFormat.GetString(gridKeluaran.Rows[i].Cells[2].Value).Replace(".", "") +
                                    DataFormat.GetString(gridKeluaran.Rows[i].Cells[3].Value).Replace(".", "") +
                                    DataFormat.GetString(gridKeluaran.Rows[i].Cells[4].Value).Replace(".", "") +
                                    DataFormat.GetString(gridKeluaran.Rows[i].Cells[5].Value).Replace(".", "")
                                    );

                            if (gridKeluaran.Rows[i + 1].Cells[7].Value != null)
                            {
                                keluaransub = DataFormat.GetString(gridKeluaran.Rows[i + 1].Cells[7].Value);

                            }
                            else
                            {
                                DataFormat.GetString(gridKeluaran.Rows[i].Cells[6].Value);
                            }
                            if (gridKeluaran.Rows[i + 1].Cells[8].Value != null)
                            {
                                keterangankeluaransub = DataFormat.GetString(gridKeluaran.Rows[i + 1].Cells[8].Value);
                                if (keterangankeluaransub.Contains(" ") == true)
                                {
                                    posisiSpasi = keterangankeluaransub.IndexOf(spasi);
                                    if (posisiSpasi > 0)
                                    {
                                        targetsub = keterangankeluaransub.Substring(0, posisiSpasi);
                                        satuansub = keterangankeluaransub.Substring(posisiSpasi + 1);

                                    }
                                }
                            }
                            else
                            {
                                targetsub = "1";
                                satuansub = "Tahun";


                            }

                        
                            oLogic.SimpanKeluaran(iddinas, idProgram, idsubkegiatan, keluaran, target, satuan, keluaransub, targetsub, satuansub);

                            
                        }



                    }
                }
            }
            
            MessageBox.Show("Import Urusan Selesai.");
        }

        private void cmdTampilkan_Click(object sender, EventArgs e)
        {
            
            if (optPendapatan.Checked == true)
                //TampilkanPendapatan();
                TampilkanPendapatan2024();
            if (chkPembiayaan.Checked == true)
            {
                TampilkanPembiayaan();
            }
            if (optBelanja.Checked == true)
            {
                //TampilkanBelanja();
                TampilkanBelanja2024();
            }
            if (optPakrt.Checked == true)
            {
                TampilkanPaket();

            }
            if (rbUraian.Checked == true)
            {
                TampilkanUraian();

            }
            if (optDinkes.Checked == true)
            {
                //TampilkanDinkes();
                GetDataAnggaranDinkes();
            }
            if (optRekDJPK.Checked == true)
            {
                TampilkanRekenigDJPK();
            }
            if (rbKeluaran.Checked == true)
            {
                TampilkanBKeluaran2024();
            }
        }

        private void cmdimpPembiayaan_Click(object sender, EventArgs e)
        {

            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, 0, 3);
            List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();
            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 3);

            List<TSumberDana> _lsd = new List<TSumberDana>();
            for (int i = 0; i < gridPembiayaan.Rows.Count; i++)
            {

                if (gridPembiayaan.Rows[i].Cells[1].Value != null)
                {
                    //  if (ctrlDinas1.GetID()== DataFormat.GetInteger(gridData.Rows[i].Cells[5].Value)){
                    TAnggaranRekening o = new TAnggaranRekening();
                    o.Tahun = GlobalVar.TahunAnggaran;
                    
                    o.IDDinas = DataFormat.GetInteger(gridPembiayaan.Rows[i].Cells[6].Value);
                    o.IDUrusan = DataFormat.GetInteger(DataFormat.GetString(gridPembiayaan.Rows[i].Cells[6].Value).Substring(0, 3));// DataFormat.GetInteger(gridData.Rows[i].Cells[0].Value.ToString().Replace(".", ""));
                    o.IDProgram = 0;
                    o.IDKegiatan = 0;
                    o.IDSubKegiatan = 0;
                    o.IDRekening = DataFormat.GetLong(gridPembiayaan.Rows[i].Cells[0].Value.ToString().Replace(".", ""));
                    if (o.IDRekening.ToString().Substring(0,2)=="61")
                    o.Jenis = 4;
                    else
                        o.Jenis = 5;

                    o.PPKD = 0;
                    o.StatusUpdate = 0;
                    o.Tahap = 3;
                    //o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    o.JumlahRKAP = DataFormat.GetDecimal(gridPembiayaan.Rows[i].Cells[5].Value.ToString().Replace(".", ""));
                    o.JumlahMurni = 0;
                    o.JumlahPergeseran= DataFormat.GetDecimal(gridPembiayaan.Rows[i].Cells[5].Value.ToString().Replace(".", ""));

                    o.JumlahABT = DataFormat.GetDecimal(gridPembiayaan.Rows[i].Cells[5].Value.ToString().Replace(".", ""));

                    //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));

                    if (o.IDRekening > 0)
                        _lstRek.Add(o);
                }
            }

            //}




            oLogicRek.SImpanSIPD(_lstRek);


            MessageBox.Show("Import Anggaran Uraian Selesai");

        }

        private void optBelanja_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdSimpanDariTemp_Click(object sender, EventArgs e)
        {
            int jenis = 3;
            if (optPendapatan.Checked == true)
                jenis = 1;
            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, 0, 3);
        //public bool TempToAnggaranRekening(int jenis)


            if (oLogicRek.TempToAnggaranRekening(jenis))
            {
                MessageBox.Show("Penyimpanan selesai");

            }
            else
            {
                MessageBox.Show("Penyimpanan salah " + oLogicRek.LastError()); ;
            }
        }
    }
  }


