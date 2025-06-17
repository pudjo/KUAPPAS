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
using BP;

//using DTO;
using Formatting;
using DTO.Bendahara;
using DTO;
namespace KUAPPAS.Bendahara
{
    public partial class frmRekapGaji : ChildForm
    {
        private List<SPP> m_lstSPP;

        public frmRekapGaji()
        {
            InitializeComponent();
        }

        private void frmRekapGaji_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Rekap Gaji");
            ctrlJenisGaji1.Create();
            gridRekapGaji.FormatHeader();

        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
            try
            {
                m_lstSPP = new List<SPP>();
                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);

                p.IDDInas =  0;// ctrlSKPD1.GetID();
                p.LstStatus = new List<int>();
                string kode;
                kode = ctrlJenisGaji1.GetKode();

                if (p.Jenis == 8)
                {
                    MessageBox.Show("Belum pilih jenis..");
                    return;
                }

                List<int> lstStatus = new List<int>();

                lstStatus.Add(3);
                lstStatus.Add(4);
                p.Status = -1;
                p.LstStatus = lstStatus;
                p.Jenis = -1;
                
                p.NoSP2D = kode.Trim();

                p.NoSPM = "";
                p.NoSPP = "";

                m_lstSPP = new List<SPP>();
                m_lstSPP=oLogic.GetSPP(p);
                if (m_lstSPP != null)
                {
                    OlahData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OlahData()
        {
            gridRekapGaji.Rows.Clear();
            if (m_lstSPP == null)
            {
                return;
            }
            int i=0;
            List<SKPD> lstSKPD = GlobalVar.gListSKPD;

            foreach (SKPD skpd in lstSKPD)
            {
                string[] dataSKPD = {
                                   (++i).ToString(),
                                   skpd.Nama,
                                   m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==1).Sum(s=>s.Jumlah).ToRupiahInReport(),
                                   m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==2).Sum(s=>s.Jumlah).ToRupiahInReport(),
                                   m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==3).Sum(s=>s.Jumlah).ToRupiahInReport(),
                                   m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==4).Sum(s=>s.Jumlah).ToRupiahInReport(),
                                    m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==5).Sum(s=>s.Jumlah).ToRupiahInReport(),
                                    m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==6).Sum(s=>s.Jumlah).ToRupiahInReport(),
                                    m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==7).Sum(s=>s.Jumlah).ToRupiahInReport(),
                                    m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==8).Sum(s=>s.Jumlah).ToRupiahInReport(),
                                    m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==9).Sum(s=>s.Jumlah).ToRupiahInReport(),
                                    m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==10).Sum(s=>s.Jumlah).ToRupiahInReport(),
                                    m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==11).Sum(s=>s.Jumlah).ToRupiahInReport(),
                                    m_lstSPP.Where(x=>x.IDDInas== skpd.ID && x.dtTerbit.Month==12).Sum(s=>s.Jumlah).ToRupiahInReport()


                                   
                                   };
                gridRekapGaji.Rows.Add(dataSKPD);

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

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;

                excelSheet.Name = "Rekap Gaji-" + ctrlJenisGaji1.GetKode();
                for (int row = 0; row < gridRekapGaji.Rows.Count; row++)
                {
                    for (int col = 0; col < gridRekapGaji.Columns.Count; col++)
                    {
                        if (col >1 )
                        {

                            excelSheet.Cells[row + 1, col + 1] = DataFormat.FormatUangReportKeDecimal(gridRekapGaji.Rows[row].Cells[col].Value);


                        }
                        else
                        {
                            excelSheet.Cells[row + 1, col + 1] = DataFormat.GetString(gridRekapGaji.Rows[row].Cells[col].Value);
                        }


                    }
                }


                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];
           
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



            sRet = fdlg.FileName;


            //  }
            return sRet;
        }
    }
}
