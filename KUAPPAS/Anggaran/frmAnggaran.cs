using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Formatting;
using DTO;
using DTO.Anggaran;
using BP;
using BP.Anggaran;
using DTO.Laporan;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

using Syncfusion.Pdf.Grid;

namespace KUAPPAS.Anggaran
{
    public partial class frmAnggaran : Form 
    {

        private List<ProgramKegiatanAnggaran> m_lstProgramKegiatan;
        //List<Rekening> mlstRekening;
        private int m_IDDInas = 0;
        private int m_iKodeUK = 0;
        public frmAnggaran()
        {
            InitializeComponent();
            m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();

        }
        
        private void frmANggaran_Load(object sender, EventArgs e)
        {
            
            try
            {
                ctrlHeader1.SetCaption("Anggaran");
                ctrlDinas1.Create();



                    if (tabAnggaran.TabPages.Contains(tabPage1) == true)
                    {
                        tabAnggaran.TabPages.Remove(tabPage1);
                    }
                    

                
                if(GlobalVar.Pengguna.SKPD>0)
                {
                    if (tabControl1.TabPages.Contains(tabPage2) == true)
                    {
                        tabControl1.TabPages.Remove(tabPage2);
                    }

                }

                gridAnggaranPendapatan.FormatHeader();              
                SembunyikanKolomBasdTahap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            return;
        }

        private void SembunyikanKolomBasdTahap()
        {
            int tahap = ctrlDinas1.GetTahapAnggaran();
            switch (tahap)
            {
                case 2:
                   

                 
                    gridAnggaranPendapatan.Columns[2].Visible = true;
                    gridAnggaranPendapatan.Columns[4].Visible = false;
                    gridAnggaranPendapatan.Columns[5].Visible = false;
                    gridAnggaranPendapatan.Columns[6].Visible = false;
                    

                    break;
                case 3:
                   
                    gridAnggaranPendapatan.Columns[2].Visible = true;
                    gridAnggaranPendapatan.Columns[4].Visible = true;
                    gridAnggaranPendapatan.Columns[5].Visible = false;
                    gridAnggaranPendapatan.Columns[6].Visible = false;

                    break;
                case 4:
                    
                    gridAnggaranPendapatan.Columns[2].Visible = true;
                    gridAnggaranPendapatan.Columns[4].Visible = true;
                    gridAnggaranPendapatan.Columns[5].Visible = true;
                    gridAnggaranPendapatan.Columns[6].Visible = false;
                    break;
                case 5:
                   
                    gridAnggaranPendapatan.Columns[2].Visible = true;
                    gridAnggaranPendapatan.Columns[4].Visible = true;
                    gridAnggaranPendapatan.Columns[5].Visible = true;
                    gridAnggaranPendapatan.Columns[6].Visible = true;
                    break;

            }
        }
       

        public bool SetData(ref List<ProgramKegiatanAnggaran> listProgramKegiatan)
        {
            try
            {
                m_lstProgramKegiatan = listProgramKegiatan;

                return true;
            }
            catch (Exception ex)
            {

                return false;

            }

        }
       
        public bool LoadProgramKegiatan()
        {

            try
            {

                m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
                ProgramKegiatanAnggaranLogic oLogic = new ProgramKegiatanAnggaranLogic(GlobalVar.TahunAnggaran);
                m_lstProgramKegiatan = oLogic.GetByDInas(m_IDDInas, m_iKodeUK);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
            


        }
        
              

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            try
            {
                m_IDDInas = pIDSKPD;
                m_iKodeUK = pIDUK;
                treeAnggaran1.DInas = m_IDDInas;
                treeAnggaran1.KodeUK = pIDUK ;
                treeAnggaran1.Tahap = ctrlDinas1.GetTahapAnggaran();

            
                LoadAnggaranPendapatan();
                LoadAnggaranPembiayaan();

                if (LoadProgramKegiatan() == false)
                {
                    return;
                }
                treeAnggaran1.SetData(ref m_lstProgramKegiatan);
                treeAnggaran1.Create();


                treeStruktuAnggaran1.DInas = m_IDDInas;
                treeStruktuAnggaran1.KodeUK = pIDUK;
                treeStruktuAnggaran1.Tahap = ctrlDinas1.GetTahapAnggaran();
                treeStruktuAnggaran1.SetData(ref m_lstProgramKegiatan);
                treeStruktuAnggaran1.Create();

               
        
                //SembunyikanKolomBasdTahap();
                //ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void LoadAnggaranPendapatan()
        {
            int idKegiatan =0;
            long  idSubKegiatan = 0;
            int idProgram =0;
                
            int idUrusan =DataFormat.ToInteger (ctrlDinas1.GetIDSKPD().ToString().Substring(0,3));
      
            TAnggaranRekeningLogic oLogic= new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
            List<TAnggaranRekening> lstRekening = new List<TAnggaranRekening>();
            lstRekening = oLogic.GetPlafon50(GlobalVar.TahunAnggaran, m_IDDInas, 0, idUrusan, idProgram, idKegiatan, idSubKegiatan, 1, 0, GlobalVar.TahapAnggaran, 1);
            gridAnggaranPendapatan.Rows.Clear();

            if (lstRekening.Count == 0)
            {

                if (tabAnggaran.TabPages.Contains(tabPendapatan) == true)
                {
                    tabAnggaran.TabPages.Remove(tabPendapatan);
                }
            }
            else
            {
                if (tabAnggaran.TabPages.Contains(tabPendapatan)== false ){
                    tabAnggaran.TabPages.Insert (1,tabPendapatan);
                }
            }
            
               foreach (TAnggaranRekening ta in lstRekening)
                {
                    //              '0                       ,1   ,2       ,3   ,4   ,5   ,6   ,7  ,8  ,9                                          ,10  ,11      ,12  ,13  ,14 ,15                         ,16  ,17 ,18  ,19  ,20  ,21 ,22
                    //string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama, ta.Plafon.ToRupiahInReport(), ta.JumlahPergeseran.ToRupiahInReport(), ta.JumlahRKAP.ToRupiahInReport(), ta.PlafonABT.ToRupiahInReport(), ta.Realisasi.ToRupiahInReport() };
                    
                    string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama, ta.JumlahMurni.ToRupiahInReport(), ta.JumlahPergeseran .ToRupiahInReport(),ta.JumlahRKAP.ToRupiahInReport(),ta.JumlahABT.ToRupiahInReport(), ta.Realisasi.ToRupiahInReport() };
                    gridAnggaranPendapatan.Rows.Add(rowUtkSumberDana);
                }
            
        }
        private void LoadAnggaranPembiayaan()
        {
            int idKegiatan = 0;
            long idSubKegiatan = 0;
            int idProgram = 0;

            int idUrusan = DataFormat.ToInteger(ctrlDinas1.GetIDSKPD().ToString().Substring(0, 3));

            TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
            List<TAnggaranRekening> lstRekening = new List<TAnggaranRekening>();
            List<TAnggaranRekening> lstRekeningPembiayaan = new List<TAnggaranRekening>();

            lstRekening = oLogic.GetPlafon50(GlobalVar.TahunAnggaran, m_IDDInas, 0, idUrusan, idProgram, idKegiatan, idSubKegiatan, 4, 0, GlobalVar.TahapAnggaran, 1);
            gridPembiayaan.Rows.Clear();


            lstRekeningPembiayaan=  oLogic.GetPlafon50(GlobalVar.TahunAnggaran, m_IDDInas, 0, idUrusan, idProgram, idKegiatan, idSubKegiatan, 5, 0, GlobalVar.TahapAnggaran, 1);



            if (lstRekening.Count == 0 && lstRekeningPembiayaan.Count==0)
            {

                if (tabAnggaran.TabPages.Contains(tabPembiayaan) == true)
                {
                    tabAnggaran.TabPages.Remove(tabPembiayaan);
                }
            }
            else
            {
                if (tabAnggaran.TabPages.Contains(tabPembiayaan) == false)
                {
                    tabAnggaran.TabPages.Insert(3, tabPembiayaan);
                }
            }

            foreach (TAnggaranRekening ta in lstRekening)
            {
                //              '0                       ,1   ,2       ,3   ,4   ,5   ,6   ,7  ,8  ,9                                          ,10  ,11      ,12  ,13  ,14 ,15                         ,16  ,17 ,18  ,19  ,20  ,21 ,22
                //string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama, ta.Plafon.ToRupiahInReport(), ta.JumlahPergeseran.ToRupiahInReport(), ta.JumlahRKAP.ToRupiahInReport(), ta.PlafonABT.ToRupiahInReport(), ta.Realisasi.ToRupiahInReport() };

                string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama, ta.JumlahMurni.ToRupiahInReport(), ta.JumlahPergeseran.ToRupiahInReport(), ta.JumlahRKAP.ToRupiahInReport(), ta.JumlahABT.ToRupiahInReport(), ta.Realisasi.ToRupiahInReport() };
                gridPembiayaan.Rows.Add(rowUtkSumberDana);
            }


            foreach (TAnggaranRekening ta in lstRekeningPembiayaan)
            {
                //              '0                       ,1   ,2       ,3   ,4   ,5   ,6   ,7  ,8  ,9                                          ,10  ,11      ,12  ,13  ,14 ,15                         ,16  ,17 ,18  ,19  ,20  ,21 ,22
                //string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama, ta.Plafon.ToRupiahInReport(), ta.JumlahPergeseran.ToRupiahInReport(), ta.JumlahRKAP.ToRupiahInReport(), ta.PlafonABT.ToRupiahInReport(), ta.Realisasi.ToRupiahInReport() };

                string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama, ta.JumlahMurni.ToRupiahInReport(), ta.JumlahPergeseran.ToRupiahInReport(), ta.JumlahRKAP.ToRupiahInReport(), ta.JumlahABT.ToRupiahInReport(), ta.Realisasi.ToRupiahInReport() };
                gridPembiayaan.Rows.Add(rowUtkSumberDana);
            }

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void treeGridProgram_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cmdCetakDPASKPD_Click(object sender, EventArgs e)
        {
            ParameterLaporan p = new ParameterLaporan();
            p.KodeUrusan = ctrlDinas1.KodeUrusanPemerintahan();
            p.KodeOrganisasi = ctrlDinas1.KodeOrganisasi();
            p.NamaUrusan = ctrlDinas1.NamaUrusanPemerintahan();
            p.NamaDinas = ctrlDinas1.GetNamaSKPD();
            p.Tanggal = DateTime.Now.Date.ToString("dd MMM yyyy");
            p.Tahap = ctrlDinas1.GetTahapAnggaran();
            p.dTanggal = DateTime.Now.Date;
            p.skpd = ctrlDinas1.GetSKPD();
            p.pimpinan = ctrlDinas1.GetPimpinan(dtCetak.Value);
           
            List<SKPD> lstSKPD = new List<SKPD>();
            lstSKPD.Add(ctrlDinas1.GetSKPD());

            //frmReportViewer f = new frmReportViewer();

            //f.CetakDPARekapPerubahan(p, GlobalVar.TahunAnggaran, m_IDDInas, false, lstSKPD);
            //f.Show();
        }

        private void cmdetakStruktur_Click(object sender, EventArgs e)
        {
            treeStruktuAnggaran1.Cetak();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////Create a new PDF document
            //PdfDocument doc = new PdfDocument();


            //PdfSection section1 = doc.Sections.Add();
            ////section1.PageSettings.Size = PdfPageSize.A5;


            //PdfPageOrientation orientation;

            //orientation = PdfPageOrientation.Landscape;
            //section1.PageSettings.Height = 500;
            //section1.PageSettings.Width = 500;
            ////Add a page
            //PdfPage page = section1.Pages.Add();  //doc.Pages.Add();

            ////Create a PdfGrid
            //PdfGraphics graphics = page.Graphics;
            //PdfGrid pdfGrid = new PdfGrid();

            ////Create a DataTable
            //DataTable dataTable = new DataTable();

            ////Include columns to the DataTable
            //dataTable.Columns.Add("Kode");
            //dataTable.Columns.Add("Nama");
            //dataTable.Columns.Add("ANggaran");

            //string Kode = "";
            //string Nama = "";
            //string Anggaran = "";

            ////Include rows to the DataTable
            //for (int i = 0; i < treeGridProgram.Rows.Count; i++)
            //{
            //    Kode = treeGridProgram.Rows[i].Cells[0].Value.ToString();
            //    Nama = treeGridProgram.Rows[i].Cells[1].Value.ToString();
            //    Anggaran = treeGridProgram.Rows[i].Cells[2].Value.ToString();


            //    dataTable.Rows.Add(new string[] { Kode, Nama + Nama + Nama, Anggaran });

            //}

            //for (int i = 0; i < TreeRingkasan.Rows.Count; i++)
            //{
            //    Kode = treeGridProgram.Rows[i].Cells[0].Value.ToString();
            //    Nama = treeGridProgram.Rows[i].Cells[1].Value.ToString();
            //    Anggaran = treeGridProgram.Rows[i].Cells[2].Value.ToString();


            //    dataTable.Rows.Add(new string[] { Kode, Nama + Nama + Nama, Anggaran });

            //}
            //for (int i = 0; i < treeGridProgram.Rows.Count; i++)
            //{
            //    Kode = treeGridProgram.Rows[i].Cells[0].Value.ToString();
            //    Nama = treeGridProgram.Rows[i].Cells[1].Value.ToString();
            //    Anggaran = treeGridProgram.Rows[i].Cells[2].Value.ToString();


            //    dataTable.Rows.Add(new string[] { Kode, Nama + Nama + Nama, Anggaran });

            //}

            ////Assign data source
            //pdfGrid.DataSource = dataTable;

            //PdfGridColumnCollection columnCollection = pdfGrid.Columns;
            //columnCollection[0].Width = 100;
            //columnCollection[1].Width = 300;
            //columnCollection[2].Width = 150;
            //columnCollection[0].Format.Alignment = PdfTextAlignment.Left;
            //columnCollection[1].Format.Alignment = PdfTextAlignment.Justify;
            //columnCollection[2].Format.Alignment = PdfTextAlignment.Right;

            //PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 6);
            //pdfGrid.Style.Font = font;
            //PdfFont fontText = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            //pdfGrid.BeginCellLayout += PdfGrid_BeginCellLayout;

            ////Apply the built-in table style
            //pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

            //graphics.DrawString("Data ANggaran ", fontText, PdfBrushes.Black, new PointF(50, 0));
            ////graphics.Draw(page, new RectangleF(0, 0, page.GetClientSize().Width, page.GetClientSize().Height));

            ////Draw grid to the page of PDF document
            //pdfGrid.Draw(page, new PointF(10, 10));

            ////Add a page to the document
            //PdfPage page2 = section1.Pages.Add();  //document.Pages.Add();

            ////Create PDF Graphics for the page
            //PdfGraphics graphics2 = page2.Graphics;

            ////Set the standard font
            //PdfFont standardFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            ////Load the text into PdfTextElement
            //PdfTextElement textElement = new PdfTextElement("Adventure Works Cycles, the fictitious company on which the AdventureWorks sample databases are based, is a large, multinational manufacturing company. The company manufactures and sells metal and composite bicycles to North American, European and Asian commercial markets. While its base operation is located in Bothell, Washington with 290 employees, several regional sales teams are located throughout their market base.", standardFont);
            //PdfGridStyle gridStyle = new PdfGridStyle();

            ////Set the cell padding, which specifies the space between the border and content of the cell.
            //gridStyle.CellPadding = new PdfPaddings(2, 2, 2, 2);

            ////Set cell spacing, which specifies the space between the adjacent cells.
            //gridStyle.CellSpacing = 2;

            ////Enable to adjust PDF table row width based on the text length.


            ////Apply style.
            //pdfGrid.Style = gridStyle;
            ////Get number of columns
            ////Console.WriteLine("Enter number of columns : ");
            //int n = 2;// Convert.ToInt16(Console.ReadLine());

            //for (int i = 0; i < n; i++)
            //{
            //    //Draw the text
            //    //textElement.Draw(page2, new RectangleF(i * page2.GetClientSize().Width / n, 0, page2.GetClientSize().Width / n, page2.GetClientSize().Height));
            //    textElement.Draw(page, new RectangleF(i * page.GetClientSize().Width / n, 0, page.GetClientSize().Width / n, page.GetClientSize().Height));
            //}

            ////Save the PDF document


            ////Save the document
            //doc.Save("Output.pdf");

            ////close the document
            //doc.Close(true);

            ////This will open the PDF file and the result will be seen in the default PDF Viewer
            //pdfViewer pV = new pdfViewer();
            //pV.Document = "Output.pdf";
            //pV.Show();
 

        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
            if (LoadProgramKegiatan() == false)
            {
                return;
            }

         
         
        }

        private void cmdCetakDPA22_Click(object sender, EventArgs e)
        {
            //ParameterLaporan p = new ParameterLaporan();
            //p.KodeUrusan = ctrlDinas1.KodeUrusanPemerintahan();
            //p.KodeOrganisasi = ctrlDinas1.KodeOrganisasi();
            //p.NamaUrusan = ctrlDinas1.NamaUrusanPemerintahan();
            //p.NamaDinas = ctrlDinas1.GetNamaSKPD();
            //p.Tanggal = dtCetak.Value.ToString("DD MMM yyyy");
            //p.dTanggal = dtCetak.Value.Date;
            //p.Tahap = ctrlDinas1.GetTahapAnggaran();
            //p.skpd = ctrlDinas1.GetSKPD();
            //p.pimpinan = ctrlDinas1.GetPimpinan(dtCetak.Value);

            //if (ctrlDinas1.GetID() == 0)
            //{
            //    MessageBox.Show("Dinas Belum dipilih");
            //    return;
            //}

            //frmReportViewer f = new frmReportViewer();
            //List<SKPD> lstSKPD = new List<SKPD>();
            //lstSKPD.Add(ctrlDinas1.GetSKPD());
            //if (p.Tahap < 3)
            //{
            //    f.CetakDPA22(p, GlobalVar.TahunAnggaran,m_IDDInas , lstSKPD);
            //}
            //else
            //{
            //    f.CetakDPA22ABT(p, GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), lstSKPD);
            //}
            //f.Show();
        }

        private void cmdetakStruktur_Click_1(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_Load_1(object sender, EventArgs e)
        {

        }

        private void cmdPanggilDataAPBDPemda_Click(object sender, EventArgs e)
        {
            try
            {
               
                treeStruktuAnggaran2.DInas = 0;
                treeStruktuAnggaran2.KodeUK = 0;
                treeStruktuAnggaran2.Tahap = ctrlDinas1.GetTahapAnggaran();
                //treeStruktuAnggaran2.SetData(ref m_lstProgramKegiatan);
                treeStruktuAnggaran2.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmdPanggilData_Click_1(object sender, EventArgs e)
        {

        }

        private void treeAnggaran1_Load(object sender, EventArgs e)
        {

        }

        private void tabProgram_Click(object sender, EventArgs e)
        {

        }

        private void gridAnggaranPendapatan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridPembiayaan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }
        //private static void PdfGrid_BeginCellLayout(object sender, PdfGridBeginCellLayoutEventArgs args)
        //{
        //    //Set the font
        //    PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Courier, 12, PdfFontStyle.Bold);

        //    //Change the column font size 
        //    if (args.CellIndex == 0)
        //    {
        //        args.Style.Font = font;
        //    }
        //}
        //}
    }
}
