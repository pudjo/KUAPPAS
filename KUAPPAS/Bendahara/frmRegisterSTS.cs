using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BP.Bendahara;
using DTO.Bendahara;
using Formatting;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using DTO;
using System.IO;
using System.Diagnostics;


namespace KUAPPAS.Bendahara
{
    public partial class frmRegisterSTS : ChildForm
    {
        private int m_IDSKPD;
        private int m_nMode;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;
        //List<BKU> mListBKU;
        List<STS> mListSTS;
        List<Setor> mListSetor;
      

        CetakPDF oCetakPDF;
        PdfPage previousPage;
        int m_iJenisBendahara;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        public frmRegisterSTS()
        {
            InitializeComponent();
        }

        private void frmRegisterSTS_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlDinas1.Create();
                m_IDSKPD = GlobalVar.Pengguna.SKPD;
               

            }
            ctrlHeader1.SetCaption("Register Penerimaan dan Penyetoran");

            gridSTS.FormatHeader();


        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkTBP.Checked == false || chkLangsung.Checked == false || chkSetor.Checked == true)
                {

                    MessageBox.Show("Belum pilih jenis..");
                    return;
                }

                    if (chkTBP.Checked == true || chkLangsung.Checked == true)
                    {

                        GetSTS();
                    }
                if (chkSetor.Checked == true)
                {
                    GetSetor();
                }
            
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool GetSTS()
        {
            try
            {
                m_IDSKPD = ctrlDinas1.GetID();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;

                STSLogic oLogic = new STSLogic((int)GlobalVar.TahunAnggaran);
                gridSTS.Rows.Clear();
                txtJumlah.Text = "0";
                int Jenis=0;
                if (chkTBP.Checked == true || chkLangsung.Checked == false)
                {
                    Jenis = 0;
                }
                if (chkTBP.Checked == false || chkLangsung.Checked == true)
                {
                    Jenis = 1;
                }
                DateTime tanggalAwalTahun = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                mListSTS = new List<STS>();
                mListSTS = oLogic.GetByDinas(m_IDSKPD, tanggalAwalTahun, tanggalAkhir, Jenis);
                decimal Jumlah = 0l;
                decimal saldo = mListSTS.Where(x => x.TanggalSTS < tanggalAwal).Sum(j => j.Jumlah);
                if (mListSTS != null)
                {
                    txtSaldoAwal.Text = saldo.ToRupiahInReport();
                    int i = 0;

                    foreach (STS s in mListSTS)
                    {
                        if (s.TanggalSTS >= tanggalAwal)
                        {
                            string[] row = { (++i).ToString(), s.TanggalSTS.ToString("dd MMM"), s.NoSTS, 
                                       s.Keterangan, s.Jumlah.ToRupiahInReport() };
                            gridSTS.Rows.Add(row);
                        }
                        Jumlah = Jumlah + s.Jumlah;
                    }
                    txtJumlah.Text = Jumlah.ToRupiahInReport();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool GetSetor()
        {
            try
            {
                m_IDSKPD = ctrlDinas1.GetID();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;

                SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
                gridSTS.Rows.Clear();
                txtJumlah.Text = "0";
                
                mListSetor = new List<Setor>();
                DateTime tanggalAwalTahun = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                mListSetor = oLogic.GetByDinas(m_IDSKPD, E_JENIS_SETOR.E_SETOR_STS, tanggalAwalTahun, tanggalAkhir);
                decimal Jumlah = 0l;
                if (mListSetor != null)
                {
                    decimal saldo = mListSetor.Where(x => x.dtBukuKas < tanggalAwal).Sum(j => j.Jumlah);
               
                    txtSaldoAwal.Text = saldo.ToRupiahInReport();
                    int i = 0;

                    foreach (Setor s in mListSetor)
                    {
                        if (s.dtBukuKas >= tanggalAwal)
                        {
                            string[] row = { (++i).ToString(), s.dtBukuKas.ToString("dd MMM"), s.NoBukti, 
                                       s.Keterangan, s.Jumlah.ToRupiahInReport() };
                            gridSTS.Rows.Add(row);
                        }
                        Jumlah = Jumlah + s.Jumlah;
                    }
                    txtJumlah.Text = Jumlah.ToRupiahInReport();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        private void cmdExcell_Click(object sender, EventArgs e)
        {
            try
            {

                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                Microsoft.Office.Interop.Excel.Range excelCellrange;
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // Make Excel invisible and disable alerts.
                excel.Visible = false;
                excel.DisplayAlerts = false;

                string namaFile = BuatFile();
                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Masih Kosong ");
                    return;
                }

                KillSpecificExcelFileProcess(namaFile);

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;

                excelSheet.Name = "RegisterSTS";


                for (int i = 1; i < gridSTS.Columns.Count + 1; i++)
                {
                    excelSheet.Cells[1, i] = gridSTS.Columns[i - 1].HeaderText;
                }
                for (int row = 0; row < gridSTS.Rows.Count; row++)
                {
                    for (int col = 0; col < gridSTS.Columns.Count; col++)
                    {
                        if (col == 4)
                        {
                           
                            excelSheet.Cells[row + 2, col + 1] = DataFormat.FormatUangReportKeDecimal(gridSTS.Rows[row].Cells[col].Value);
                            
              
                        }
                        else
                        {
                            excelSheet.Cells[row + 2, col + 1] = DataFormat.GetString(gridSTS.Rows[row].Cells[col].Value);
                        }


                    }
                }


                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];
                //excelCellrange.EntireColumn.AutoFit();
                //excelSheet.Range (“G:G”).NumberFormat = “0.00”;
                //Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                //border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //border.Weight = 2d;
                

                excelworkBook.SaveAs(namaFile);
                MessageBox.Show("File sudah disimpan di " + namaFile);


                excelworkBook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke excell" + ex.Message);
            }
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




            //if (fdlg.FileName != "")
            //{
            // Saves the Image via a FileStream created by the OpenFile method.  

            sRet = fdlg.FileName;


            //  }
            return sRet;
        }
    }
}
