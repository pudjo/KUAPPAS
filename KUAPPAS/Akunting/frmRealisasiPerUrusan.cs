using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP;
using BP.Anggaran;
using DTO;
using DTO.Akuntansi;
using DTO.Anggaran;
using Formatting;
using BP.Akuntansi;
using DTO.Laporan;
using System.IO;

namespace KUAPPAS.Akunting
{
    public partial class frmRealisasiPerUrusan : Form
    {
        public frmRealisasiPerUrusan()
        {
            InitializeComponent();
        }

        private void cmdGenerateData_Click(object sender, EventArgs e)
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

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;

                excelSheet.Name = "LRA perUrusan";
                


                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];
                //excelCellrange.EntireColumn.AutoFit();
                //excelSheet.Range (“G:G”).NumberFormat = “0.00”;
                //Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                //border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //border.Weight = 2d;
                string namaFile = BuatFile();
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
