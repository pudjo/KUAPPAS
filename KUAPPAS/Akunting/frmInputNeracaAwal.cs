using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP.Akuntansi;
using Formatting;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using DTO;
using System.IO;
using BP;
using BP.Anggaran;
using DTO;
using DTO.Akuntansi;
using DTO.Anggaran;
using DTO.Laporan;
using System.Diagnostics;

namespace KUAPPAS.Akunting
{
    public partial class frmInputNeracaAwal : ChildForm
    {
        private List<SaldoAwalRek> m_lstNeracaAwal;

        private int m_iSKPD;
        public frmInputNeracaAwal()
        {
            InitializeComponent();
        }

        private void frmInputSaldoAwal_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Input Neraca Awal");
            treeRekening1.Create();
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            gridNeracaAwal.FormatHeader();

        }

        private void cmdPanggil_Click(object sender, EventArgs e)
        {
            try
            {
                m_iSKPD = ctrlSKPD1.GetID();
                if (m_iSKPD == 0)
                {
                    MessageBox.Show("Belum Pilih Dinas");
                    return;
                }
                gridNeracaAwal.Rows.Clear();
                gridNeracaAwalLO.Rows.Clear();

                m_lstNeracaAwal = new List<SaldoAwalRek>();
                SaldoAwalRehLogic oLogic = new SaldoAwalRehLogic(GlobalVar.TahunAnggaran);
                m_lstNeracaAwal = oLogic.GetSaldoAwal(m_iSKPD);
                decimal Jumlahdebt = 0;
                decimal JumlahKredit = 0;
                decimal debet = 0;// sa.Debet == 1 ? sa.Jumlah : 0;
                decimal kredit = 0;// sa.Debet == -1 ? sa.Jumlah : 0;
                m_lstNeracaAwal.OrderBy(x=>x.IDRekening);
                if (m_lstNeracaAwal != null)
                {
                    foreach (SaldoAwalRek sa in m_lstNeracaAwal)
                    {
                        if (sa.IDRekening < 500000000000)
                        {

                            debet = sa.Debet == 1 ? sa.Jumlah : 0;
                            kredit = sa.Debet == -1 ? sa.Jumlah : 0;
                            Jumlahdebt = Jumlahdebt + debet;// +kredit;
                            JumlahKredit = JumlahKredit + kredit;
                            string[] row = { sa.IDRekening.ToString(), sa.IDRekening.ToKodeRekening(), sa.Nama, debet.ToRupiahInReport(), kredit.ToRupiahInReport(), "1" };
                            gridNeracaAwal.Rows.Add(row);
                        }
                        else
                        {

                            debet = sa.Debet == 1 ? sa.Jumlah : 0;
                            kredit = sa.Debet == -1 ? sa.Jumlah : 0;

                            string[] row = { sa.IDRekening.ToString(), sa.IDRekening.ToKodeRekening(), sa.Nama, debet.ToRupiahInReport(), kredit.ToRupiahInReport(), "1" };
                            gridNeracaAwalLO.Rows.Add(row);
                        }
                    }
         
                   
                }
                txtJumlahDebet.Text = Jumlahdebt.ToRupiahInReport();
                txtJumlahKredit.Text = JumlahKredit.ToRupiahInReport();


            }
            catch (Exception ex)
            {

            }
        }

        private void treeRekening1_DoubleClicking(global::DTO.Rekening rek)
        {
            if (rek.Root < 6)
            {
                MessageBox.Show("Harud Rekening Sub Rincian Objek");
                return;
            }
            string[] row = { rek.ID.ToString(), rek.ID.ToKodeRekening(), rek.Nama, "0", "0", "0" };

            gridNeracaAwal.Rows.Add(row);

        }

        private void gridNeracaAwal_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < gridNeracaAwal.Rows.Count)
            {
                if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
                {

                    decimal debet = DataFormat.GetDecimal(gridNeracaAwal.Rows[e.RowIndex].Cells[3].Value);// DataFormat.FormatUangReportKeDecimal(gridRekening.Rows[e.RowIndex].Cells[5].Value);
                    decimal kredit = DataFormat.GetString((gridNeracaAwal.Rows[e.RowIndex].Cells[4].Value)).FormatUangReportKeDecimal();

                    gridNeracaAwal.Rows[e.RowIndex].Cells[3].Value = debet.ToRupiahInReport();

                    gridNeracaAwal.Rows[e.RowIndex].Cells[4].Value = kredit.ToRupiahInReport();

                    gridNeracaAwal.Rows[e.RowIndex].Cells[5].Value = "0";

                }
            }
        }

        private void gridNeracaAwal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                List<SaldoAwalRek> saldoAwalHarusDisaimpan = new List<SaldoAwalRek>();
                foreach (DataGridViewRow row in gridNeracaAwal.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        int Status = 1;
                        Status = DataFormat.GetInteger(row.Cells[5].Value);
                        decimal debet = DataFormat.FormatUangReportKeDecimal(row.Cells[3].Value);
                        decimal kredit = DataFormat.FormatUangReportKeDecimal(row.Cells[4].Value);

                        int isdebet = debet != 0 ? 1 : -1;
                        decimal jumlah = debet != 0 ? debet : kredit;

                        if (Status == 0)
                        {
                            SaldoAwalRek sa = new SaldoAwalRek();
                            sa.IDRekening = DataFormat.GetLong(row.Cells[0].Value);
                            sa.Tanggal = new DateTime(GlobalVar.TahunAnggaran, 12, 31);
                            sa.Jumlah = jumlah;
                            sa.Debet = isdebet;
                            sa.IDDinas = m_iSKPD;
                            saldoAwalHarusDisaimpan.Add(sa);

                        }
                    }
                    
                   
                }
                if (saldoAwalHarusDisaimpan.Count == 0)
                {
                    MessageBox.Show("Tidak ada perubahan Data Neraca Awal.Tidak ada proses penyimpanan.");
                    return;
                }
                else
                {
                    SaldoAwalRehLogic oLogic = new SaldoAwalRehLogic(GlobalVar.TahunAnggaran);
                    foreach (SaldoAwalRek sa in saldoAwalHarusDisaimpan)
                    {
                        oLogic.Simpan(sa);
                    }


                    MessageBox.Show("Neraca Awal sudah didimpan");
                }
                foreach (DataGridViewRow row in gridNeracaAwal.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        row.Cells[5].Value = "1"; 
                    }
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Menyimpan " + ex.Message);

            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void CmdSimpanLO_Click(object sender, EventArgs e)
        {
            try
            {
                List<SaldoAwalRek> saldoAwalHarusDisaimpan = new List<SaldoAwalRek>();
                foreach (DataGridViewRow row in gridNeracaAwalLO.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        int Status = 1;
                        Status = DataFormat.GetInteger(row.Cells[5].Value);
                        decimal debet = DataFormat.FormatUangReportKeDecimal(row.Cells[3].Value);
                        decimal kredit = DataFormat.FormatUangReportKeDecimal(row.Cells[4].Value);

                        int isdebet = debet != 0 ? 1 : -1;
                        decimal jumlah = debet != 0 ? debet : kredit;

                        if (Status == 0)
                        {
                            SaldoAwalRek sa = new SaldoAwalRek();
                            sa.IDRekening = DataFormat.GetLong(row.Cells[0].Value);
                            sa.Tanggal = new DateTime(GlobalVar.TahunAnggaran, 12, 31);
                            sa.Jumlah = jumlah;
                            sa.Debet = isdebet;
                            sa.IDDinas = m_iSKPD;
                            saldoAwalHarusDisaimpan.Add(sa);

                        }
                    }


                }
                if (saldoAwalHarusDisaimpan.Count == 0)
                {
                    MessageBox.Show("Tidak ada perubahan Data Saldo Awal LO..Tidak ada proses penyimpanan.");
                    return;
                }
                else
                {
                    SaldoAwalRehLogic oLogic = new SaldoAwalRehLogic(GlobalVar.TahunAnggaran);
                    foreach (SaldoAwalRek sa in saldoAwalHarusDisaimpan)
                    {
                        oLogic.Simpan(sa);
                    }


                    MessageBox.Show("Saldo Awal LO sudah didimpan");
                }
                foreach (DataGridViewRow row in gridNeracaAwalLO.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        row.Cells[5].Value = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Menyimpan " + ex.Message);

            }
        }

        private void gridNeracaAwalLO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridNeracaAwalLO_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < gridNeracaAwalLO.Rows.Count)
            {
                if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
                {

                    decimal debet = DataFormat.GetDecimal(gridNeracaAwalLO.Rows[e.RowIndex].Cells[3].Value);// DataFormat.FormatUangReportKeDecimal(gridRekening.Rows[e.RowIndex].Cells[5].Value);
                    decimal kredit = DataFormat.GetString((gridNeracaAwalLO.Rows[e.RowIndex].Cells[4].Value)).FormatUangReportKeDecimal();

                    gridNeracaAwalLO.Rows[e.RowIndex].Cells[3].Value = debet.ToRupiahInReport();

                    gridNeracaAwalLO.Rows[e.RowIndex].Cells[4].Value = kredit.ToRupiahInReport();

                    gridNeracaAwalLO.Rows[e.RowIndex].Cells[5].Value = "0";

                }
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
                if (sRet.Length == 0)
                {
                    MessageBox.Show("Nama File Tidak boleh kosong..");
                    return "";

                }

                //  }
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
        private void button1_Click(object sender, EventArgs e)
        {
            string namaFile = "";
            namaFile = BuatFile();

            KillSpecificExcelFileProcess(namaFile);
            if (namaFile.Length == 0)
            {
                return;
            }
            try
            {
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objWorkSheet);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objWorkBook);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objWorkBooks);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objAppln);
                //_objWorkSheet = null; _objWorkBooks = null; _objWorkBooks = null; _objAppln = null;


                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                Microsoft.Office.Interop.Excel.Range excelCellrange;
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();
                while (excel.Interactive == true)
                {
                    // If Excel is currently busy, try until go thru
                    excel.Interactive = false;
                }

                // Make Excel invisible and disable alerts.
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;

                excelSheet.Name = "Neraca Awal";
                for (int row = 0; row < gridNeracaAwal.Rows.Count; row++)
                {
                    for (int col = 0; col < gridNeracaAwal.Columns.Count; col++)
                    {
                        if (col >= 3)
                        {
                            excelSheet.Cells[row + 1, col + 1] = DataFormat.GetString(gridNeracaAwal.Rows[row].Cells[col].Value).ReplaceUnicode(); //DataFormat.FormatUangReportKeDecimal(gridNeracaAwal.Rows[row].Cells[col].Value).ToString();
                        }else
                        {
                            excelSheet.Cells[row + 1, col + 1] = DataFormat.GetString(gridNeracaAwal.Rows[row].Cells[col].Value).ReplaceUnicode();
                        }


                    }
                }
                for (int row = 0; row < gridNeracaAwalLO.Rows.Count; row++)
                {
                    for (int col = 0; col < gridNeracaAwalLO.Columns.Count; col++)
                    {
                        if (col < 3)
                        {
                            excelSheet.Cells[row + 1, col + 1] = DataFormat.FormatUangReportKeDecimal(gridNeracaAwalLO.Rows[row].Cells[col].Value).ToString();
                        }
                        else
                        {
                            {
                                excelSheet.Cells[row + 1, col + 1] = DataFormat.GetString(gridNeracaAwalLO.Rows[row].Cells[col].Value).ReplaceUnicode();
                            }


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

                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Masih Kosong ");
                    return;
                }

                excelworkBook.SaveAs(namaFile);
                MessageBox.Show("File sudah disimpan di " + namaFile);


                excelworkBook.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Gagal export ke excell " + namaFile + "   " + ex.Message);
            }
        }
        
        
    }
}
