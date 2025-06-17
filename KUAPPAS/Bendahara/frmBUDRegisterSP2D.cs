using BP.Bendahara;
using DTO;
using DTO.Bendahara;
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
using System.IO;
using System.Diagnostics;


namespace KUAPPAS.Bendahara
{
    public partial class frmBUDRegisterSP2D : ChildForm
    {
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;
       
        public frmBUDRegisterSP2D()
        {
            InitializeComponent();
        }

        private void frmRegisterSP2D_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Register SP2D");
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            ctrlBulan1.Create();
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add ("Terbit");
            cmbStatus.Items.Add ("Cair");
            ctrlPeriode1.TanggalAwaal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
            DateTime hariini = DateTime.Now.Date;
            ctrlPeriode1.TanggalAkhir = hariini; 

            gridRegister.FormatHeader();

        }
    
        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
           

            try
            {

                if (GetTanggal() == false)
                {
                    return;
                }
            SPPLogic ologic = new SPPLogic(GlobalVar.TahunAnggaran);
                List<SPP> m_lstspp = new List<SPP>();
            ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);
            p.TanggalAwal = mTanggalAwal ;
            p.TanggalAkhir = mTanggalAkhir;
            if (chkSemuaDinas.Checked== true ){
                    p.IDDInas=0;
            } else {

                if (ctrlSKPD1.GetID() == 0)
                {
                    MessageBox.Show("Belum memilih OPD");
                    return;
                }
                p.IDDInas = ctrlSKPD1.GetID();
            }
             
            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Belu pilih Status");
                return;

            }
            p.Status = cmbStatus.SelectedIndex==0? 3 : 4;

            p.WithPotongan = true;
            p.LstStatus = new List<int>();
            m_lstspp = new List<SPP>();
            p.Jenis = -1;// DataFormat.GetInteger(txtJenis.Text);
            p.OrderBy = " tSPP.inourutkasda";
            m_lstspp = ologic.GetSPP(p);

            if (ologic.IsError() == true)
            {
                MessageBox.Show(ologic.LastError());
                return;
            }
            int iRow = 0;
            gridRegister.Rows.Clear();

           

            foreach (SPP spp in m_lstspp)
            {

                //jumlahdibayar = 0;
                //jumlahdibayar = spp.Jumlah - spp.JumlahPotongan;
                string namaSKPD="";
                SKPD oSKPD = GlobalVar.gListSKPD.FirstOrDefault(skpd => skpd.ID == spp.IDDInas);
                if (oSKPD != null)
                {
                    namaSKPD = oSKPD.Nama;
                }
                string[] row = { 
                     //              spp.NoUrut.ToString(), //0
                                   spp.NoUrutKasda.ToString(),
                                    namaSKPD, //5
                                    spp.NoSP2D,
                                   spp.Keterangan  , 
                                   spp.Jumlah.ToRupiahInReport(),//10 
                                   spp.dtTerbit.ToTanggalIndonesia (),//13
                                   spp.dtCair.ToTanggalIndonesia (),
                                   spp.NamaPenerima ,
                                   spp.NamaSumberDana 
                               };

                gridRegister.Rows.Add(row);
                    if (spp.Status == 4)
                        gridRegister.Rows[iRow].Cells[2].Style.BackColor = Color.Aqua;
                    if (spp.Status == 3)
                        gridRegister.Rows[iRow].Cells[2].Style.BackColor = Color.OrangeRed;// PaleVioletRed;// IndianRed;// Red;

               


                    iRow++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedIndex==0){
                rbTanggal.Text ="Tanggal Terbit";
                rbBulan.Text ="Bulan Terbit";
            } else {
                rbTanggal.Text ="Tanggal Cair";
                rbBulan.Text ="Bulan Cair";
            }
        }
 


        
        private bool  GetTanggal()
        {
            if (rbTanggal.Checked == false && rbBulan.Checked == false)
            {
                MessageBox.Show("Belum pilih TanggalCair atau Bulan..");
                return false;
            }
            if (rbTanggal.Checked == true)
            {
                mTanggalAwal = ctrlPeriode1.TanggalAwaal;
                mTanggalAkhir = ctrlPeriode1.TanggalAkhir;
            }
            else
            {
                mTanggalAwal = ctrlBulan1.TanggalAwal;
                mTanggalAkhir = ctrlBulan1.TanggalAkhir;
            }
            return true;

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

        private void cmdExportExcell_Click(object sender, EventArgs e)
        {
 

            string NamaFile = "";
            string namaFile = BuatFile();
            
            
            if (namaFile.Trim().Length == 0)
            {
                MessageBox.Show("Nama Masih Kosong ");
                return;
            }
            KillSpecificExcelFileProcess(namaFile);
            try
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

                    excelSheet.Name = "SP2D";

                    for (int i = 1; i < gridRegister.Columns.Count + 1; i++)
                    {
                        excelSheet.Cells[1, i] = gridRegister.Columns[i - 1].HeaderText;
                    }




                    for (int i =0; i < gridRegister.Rows.Count ; i++)
                    {

                        for (int j = 0; j < gridRegister.Columns.Count ; j++)
                        {
                            if (gridRegister.Rows[i].Cells[j].Value != null)
                            {
                                if (j == 4)
                                {

                                    excelSheet.Cells[i + 2, j + 1] = DataFormat.FormatUangReportKeDecimal(gridRegister.Rows[i].Cells[j].Value).ToString();
                                }
                                else
                                {
                                    excelSheet.Cells[i + 2, j + 1] = DataFormat.GetString(gridRegister.Rows[i].Cells[j].Value).Trim();
                                }

                                
                            }
                            else
                            {
                                excelSheet.Cells[i + 2, j + 1] = "";
                            }
                        }
                    }

                  

                    // now we resize the columns
                    excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];
             
                    

                    excelworkBook.SaveAs(namaFile);
                    MessageBox.Show("File sudah disimpan di " + namaFile);


                    excelworkBook.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal export ke excell" + ex.Message);
                }
  

                // Exit from the application 

                MessageBox.Show("Selesai export ke Excell. Disimpan di: " + NamaFile);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke Excell." + ex.Message);
             
            }
        }
        
       private string BuatFile(){

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
