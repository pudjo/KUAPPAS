using BP.Akuntansi;
using DTO.Akuntansi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formatting;
using Newtonsoft.Json;
using System.Diagnostics;

namespace KUAPPAS.Akunting
{
    public partial class frmLaporanJurnal : ChildForm
    {
        decimal JumlahDebet = 0;
        decimal JumlahKredit = 0;
        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        List<JurnalRekeningShow> lst = new List<JurnalRekeningShow>();

        public frmLaporanJurnal()
        {
            InitializeComponent();
        }

        private void frmLaporanJurnal_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            ctrlTanggalBulanVertikal1.TanggalAkhir = DateTime.Now.Date;
            ctrlTanggalBulanVertikal1.TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
            ctrlHeader1.SetCaption("Laporan Jurnal");
            gridLJurnal.FormatHeader();
            gridTidakBalance.FormatHeader();
            ctrlJenisSumber1.Create();
 
   
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int dinas= ctrlDinas1.GetID();
                     int ppkd=0;
                     int jenissumber = ctrlJenisSumber1.GetID();
                  DateTime tanggalawal=ctrlTanggalBulanVertikal1.TanggalAwal;
                    DateTime tanggalakhir=ctrlTanggalBulanVertikal1.TanggalAkhir;
                     JumlahDebet = 0;
                     JumlahKredit = 0;
                     gridLJurnal.Rows.Clear();
                gridTidakBalance.Rows.Clear();
                int potongan = 0;
                if (jenissumber == 19)
                {
                    potongan = 1;
                }
                JurnalLogic oLogic = new JurnalLogic(GlobalVar.TahunAnggaran);
                  lst = new  List<JurnalRekeningShow>();
                if (jenissumber == 3 || jenissumber == 8)
                {

                    lst = oLogic.GetByJenis(dinas, ppkd, jenissumber, tanggalawal, tanggalakhir, potongan);
                }
                else
                {
                    lst = oLogic.Get(dinas, ppkd, jenissumber, tanggalawal, tanggalakhir, potongan);
                }
                    decimal debet = 0;
                decimal kredit=0;
                decimal jumlahTampil;
                long oldNoJurnal = 0;
                string[] row;
                decimal JumalhDebetPerJurnal = 0;
                decimal JumlahKreditPerjurnal = 0;
  
                if (lst != null)
                 {
                     int i = 0;
                     foreach (JurnalRekeningShow j in lst)
                     {
                         if (j.Debet == 1){
                             debet = j.Jumlah;
                             kredit =0;

                         }
                         else {
                             debet = 0;
                             kredit =j.Jumlah;
                         }
                         JumlahDebet = JumlahDebet + debet;
                         JumlahKredit = JumlahKredit + kredit;
                         if (j.NoJurnal != oldNoJurnal) { 
                             string[] rowHeader = { j.NoJurnal.ToString(),j.NamaSKPD,j.Tanggal.ToTanggalIndonesia(), j.NoBukti,  j.IIDRekening.ToKodeRekening(6),j.NamaRekening,  debet.ToRupiahInReport(), kredit.ToRupiahInReport() };
                                gridLJurnal.Rows.Add(rowHeader);

                                if (JumalhDebetPerJurnal != JumlahKreditPerjurnal)
                             //////   gridLJurnal.Rows[i].DefaultCellStyle = _hilightstyle;
                                    WarnaiKetidakBalace(oldNoJurnal);
                                 JumalhDebetPerJurnal = 0;
                                 JumlahKreditPerjurnal = 0;
                                 JumalhDebetPerJurnal = JumalhDebetPerJurnal+ debet;
                                 JumlahKreditPerjurnal = JumlahKreditPerjurnal+kredit ;
                                oldNoJurnal = j.NoJurnal;

                         }
                         else
                         {
                             string[] rowDetail = { j.NoJurnal.ToString(),"", "", "",  j.IIDRekening.ToKodeRekening(6), j.NamaRekening,  debet.ToRupiahInReport(), kredit.ToRupiahInReport() };
                             gridLJurnal.Rows.Add(rowDetail);
                             JumalhDebetPerJurnal = JumalhDebetPerJurnal + debet;
                             JumlahKreditPerjurnal = JumlahKreditPerjurnal + kredit;
                         }
                         i++;
                     }
                 }
                txtJumlahDebet.Text = JumlahDebet.ToRupiahInReport();
                txtJumlahKredit.Text = JumlahKredit.ToRupiahInReport(); 

            }
            catch (Exception ex)
            {

            }
        }
        private void WarnaiKetidakBalace(long noJurnal)
        { 
            _hilightstyle.BackColor=Color.Red ;
            //     gridTidakBalance.Rows.Clear();
           for (int i = 0; i < gridLJurnal.Rows.Count; i++)
            {
                if (DataFormat.GetLong(gridLJurnal.Rows[i].Cells[0].Value) == noJurnal)
                {
                    gridLJurnal.Rows[i].DefaultCellStyle = _hilightstyle;

                    JurnalRekeningShow j = lst.FirstOrDefault(x => x.NoJurnal == noJurnal);
                    if (j != null)
                    {
                        string[] r = { j.NoBukti, j.Tanggal.ToTanggalIndonesia() };
                        gridTidakBalance.Rows.Add(r);
                    }
                }
            
           
           }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (e.ClickedItem.Text == "Copy" && gridTidakBalance.CurrentCell.Value != null)
            //{
                Clipboard.SetDataObject(gridTidakBalance.CurrentCell.Value.ToString(), false);
           // }
        }

        private void gridTidakBalance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                // excel.DisplayAlerts = false;

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;


                // header
                excelSheet.Cells[1, 2] = "Buku Jurnal";


                excelSheet.Cells[3, 1] = "Sampai Tanggal";
                excelSheet.Cells[3, 2] = ctrlTanggalBulanVertikal1.TanggalAkhir.ToTanggalIndonesiaLengkap();

                if (ctrlDinas1.GetID() > 0)
                {
                    excelSheet.Cells[3, 1] = "Sampai Tanggal";
                    excelSheet.Cells[3, 2] = ctrlDinas1.GetNamaSKPD();
                }
                if (ctrlJenisSumber1.GetID() > 0)
                {
                    excelSheet.Cells[4, 1] = "Sumber";
                    excelSheet.Cells[4, 2] = ctrlJenisSumber1.NamaSumber;
                    
                }
                int jumlahkolom = gridLJurnal.ColumnCount;

                for (int i = 1; i <=jumlahkolom; i++)
                {
                    excelSheet.Cells[4, i] = gridLJurnal.Columns[i - 1].HeaderText;
                }
                //gridLRA.Columns.Count + 1
                for (int row = 0; row < gridLJurnal.Rows.Count; row++)
                {
                    for (int col = 0; col < jumlahkolom ; col++)
                    {
                        if (col > 5)
                        {
                            string s = DataFormat.GetString(gridLJurnal.Rows[row].Cells[col].Value);
                            excelSheet.Cells[row + 5, col + 1] = s.FormatUangReportKeDecimal().ToString().ReplaceUnicode();
                        }
                        else
                        {
                            excelSheet.Cells[row + 5, col + 1] = DataFormat.GetString(gridLJurnal.Rows[row].Cells[col].Value).ReplaceUnicode();
                        }




                    }
                }


                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];
                string namaFile = BuatFile();
                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama File Masih Kosong ");
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

        private void cmdCetak_Click(object sender, EventArgs e)
        {

        }
    }
}
