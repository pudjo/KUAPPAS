using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DTO.Anggaran;
using BP;
using BP.Anggaran;
using DTO.Laporan;
using Formatting;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

using Syncfusion.Pdf.Grid;

namespace KUAPPAS.Anggaran
{
    public partial class treeStruktuAnggaran : UserControl
    {
        private List<ProgramKegiatanAnggaran> m_lstProgramKegiatan; 
        private int m_IDDInas = 0;
        private int m_iKodeUK = 0;
        private int m_iTahap;
        public treeStruktuAnggaran()
        {
            InitializeComponent();
            m_IDDInas = 0;
            m_iKodeUK = 0;
            m_iTahap = 2;
            m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran> (); 
        }

        public bool  SetData(ref List<ProgramKegiatanAnggaran> listProgramKegiatan)
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
        private void treeStruktuAnggaran_Load(object sender, EventArgs e)
        {
            TreeRingkasan.FormatHeader();
        }
        public int DInas
        {
            set
            {
                m_IDDInas = value;
            }

        }
        public int KodeUK
        {
            set
            {
                m_iKodeUK = value;
            }
        }
        public int Tahap
        {
            set
            {
                m_iTahap = value;
            }
        }
        public bool Create()
        {
            try
            {
                if (GlobalVar.Pengguna.SKPD > 0)
                {
                    if (GlobalVar.Pengguna.SKPD != m_IDDInas)
                    {
                        return false;
                    }

                }
                
                if (m_lstProgramKegiatan.Count == 0)
                {
                    if (LoadProgramKegiatan() == false)
                    {
                        return false;
                    }

                }
                DisplayRingkasan();
                SembunyikanKolomBasdTahap();
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuat tampilan anggaran");
                return false;
            }
        }
        public void Cetak()
        {
          
            //Create a new PDF document
            PdfDocument doc = new PdfDocument();
            PdfSection section1 = doc.Sections.Add();
            //section1.PageSettings.Size = PdfPageSize.A5;
            PdfPageOrientation orientation;

            orientation = PdfPageOrientation.Landscape;
            section1.PageSettings.Height = 500;
            section1.PageSettings.Width = 500;
            //Add a page
            PdfPage page = section1.Pages.Add();  //doc.Pages.Add();

            //Create a PdfGrid
            PdfGraphics graphics = page.Graphics;
            PdfGrid pdfGrid = new PdfGrid();

            //Create a DataTable
            DataTable dataTable = new DataTable();

            //Include columns to the DataTable
            dataTable.Columns.Add("Kode");
            dataTable.Columns.Add("Nama");
            dataTable.Columns.Add("ANggaran");

            string Kode = "";
            string Nama = "";
            string Anggaran = "";

            //Include rows to the DataTable
            for (int i = 0; i < TreeRingkasan.Rows.Count; i++)
            {
                Kode = TreeRingkasan.Rows[i].Cells[0].Value.ToString();
                Nama = TreeRingkasan.Rows[i].Cells[1].Value.ToString();
                Anggaran = TreeRingkasan.Rows[i].Cells[2].Value.ToString();


                dataTable.Rows.Add(new string[] { Kode, Nama + Nama + Nama, Anggaran });

            }

            for (int i = 0; i < TreeRingkasan.Rows.Count; i++)
            {
                Kode = TreeRingkasan.Rows[i].Cells[0].Value.ToString();
                Nama = TreeRingkasan.Rows[i].Cells[1].Value.ToString();
                Anggaran = TreeRingkasan.Rows[i].Cells[2].Value.ToString();


                dataTable.Rows.Add(new string[] { Kode, Nama + Nama + Nama, Anggaran });

            }
            for (int i = 0; i < TreeRingkasan.Rows.Count; i++)
            {
                Kode = TreeRingkasan.Rows[i].Cells[0].Value.ToString();
                Nama = TreeRingkasan.Rows[i].Cells[1].Value.ToString();
                Anggaran = TreeRingkasan.Rows[i].Cells[2].Value.ToString();


                dataTable.Rows.Add(new string[] { Kode, Nama + Nama + Nama, Anggaran });

            }

            //Assign data source
            pdfGrid.DataSource = dataTable;

            PdfGridColumnCollection columnCollection = pdfGrid.Columns;
            columnCollection[0].Width = 100;
            columnCollection[1].Width = 300;
            columnCollection[2].Width = 150;
            columnCollection[0].Format.Alignment = PdfTextAlignment.Left;
            columnCollection[1].Format.Alignment = PdfTextAlignment.Justify;
            columnCollection[2].Format.Alignment = PdfTextAlignment.Right;

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 6);
            pdfGrid.Style.Font = font;
            PdfFont fontText = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            pdfGrid.BeginCellLayout += PdfGrid_BeginCellLayout;

            //Apply the built-in table style
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

            graphics.DrawString("Data ANggaran ", fontText, PdfBrushes.Black, new PointF(50, 0));
            //graphics.Draw(page, new RectangleF(0, 0, page.GetClientSize().Width, page.GetClientSize().Height));

            //Draw grid to the page of PDF document
            pdfGrid.Draw(page, new PointF(10, 10));

            //Add a page to the document
            PdfPage page2 = section1.Pages.Add();  //document.Pages.Add();

            //Create PDF Graphics for the page
            PdfGraphics graphics2 = page2.Graphics;

            //Set the standard font
            PdfFont standardFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            //Load the text into PdfTextElement
            PdfTextElement textElement = new PdfTextElement("Adventure Works Cycles, the fictitious company on which the AdventureWorks sample databases are based, is a large, multinational manufacturing company. The company manufactures and sells metal and composite bicycles to North American, European and Asian commercial markets. While its base operation is located in Bothell, Washington with 290 employees, several regional sales teams are located throughout their market base.", standardFont);
            PdfGridStyle gridStyle = new PdfGridStyle();

            //Set the cell padding, which specifies the space between the border and content of the cell.
            gridStyle.CellPadding = new PdfPaddings(2, 2, 2, 2);

            //Set cell spacing, which specifies the space between the adjacent cells.
            gridStyle.CellSpacing = 2;

            //Enable to adjust PDF table row width based on the text length.


            //Apply style.
            pdfGrid.Style = gridStyle;
            //Get number of columns
            //Console.WriteLine("Enter number of columns : ");
            int n = 2;// Convert.ToInt16(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                //Draw the text
                //textElement.Draw(page2, new RectangleF(i * page2.GetClientSize().Width / n, 0, page2.GetClientSize().Width / n, page2.GetClientSize().Height));
                textElement.Draw(page, new RectangleF(i * page.GetClientSize().Width / n, 0, page.GetClientSize().Width / n, page.GetClientSize().Height));
            }

            //Save the PDF document


            //Save the document
            doc.Save("Output.pdf");

            //close the document
            doc.Close(true);

            //This will open the PDF file and the result will be seen in the default PDF Viewer
            pdfViewer pV = new pdfViewer();
            pV.Document = "Output.pdf";
            pV.Show();
 

        }
        private static void PdfGrid_BeginCellLayout(object sender, PdfGridBeginCellLayoutEventArgs args)
        {
            //Set the font
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Courier, 12, PdfFontStyle.Bold);

            //Change the column font size 
            if (args.CellIndex == 0)
            {
                args.Style.Font = font;
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
        private void DisplayRingkasan()
        {
         try
            {
                if (GlobalVar.gListRekening == null)
                {
                    GlobalVar.gListRekening = new List<Rekening>();
                    RekeningLogic oRekLogic = new RekeningLogic(GlobalVar.TahunAnggaran);
                    GlobalVar.gListRekening = oRekLogic.Get();

                }



                if (GlobalVar.gListRekening != null)
                {
                    ProsesRingkasanBelanja();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void ProsesRingkasanBelanja()
        {
            
            ProsesRingkasaBelanjaLevel1();

        }

        private void ProsesRingkasaBelanjaLevel1()
        {
            long oldIdRekening;
            TreeRingkasan.Rows.Clear();
            List<Rekening> lstRekeningBelanjaLevel1 = new List<Rekening>();
            lstRekeningBelanjaLevel1 = GlobalVar.gListRekening.FindAll(x => x.Root == 1 && x.ID.ToString().Substring(0, 1) == "5");

            List<ProgramKegiatanAnggaran> lstDistinctSubKegiatan = new List<ProgramKegiatanAnggaran>();
           

            var lstJumlahANggaran = m_lstProgramKegiatan.GroupBy(x => x.IIDRekening.ToString().Substring(0,1) )
            .Select(x => new
            {
                idrekening = x.Key,
                JumlahMurni = x.Sum(y => y.AnggaranMurni),
                JumlahGeser = x.Sum(y => y.AnggaranGeser),
                JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                JumlahABT = x.Sum(y => y.AnggaranABT),

            }).ToList();

            List<Laporan> lstLevel1 = (from m in lstRekeningBelanjaLevel1
                                       join j in lstJumlahANggaran
                                       on m.ID.ToString().Substring(0, 1) equals j.idrekening
                                       select new Laporan
                                       {
                                                    Kode  = m.ID.ToKodeRekening(1),
                                                    IDRekening= m.ID,
                                                    Level=1,

                                                    Nama =m.Nama ,
                                                    AnggaranMurni =j.JumlahMurni ,
                                                    AnggaranGeser =j.JumlahGeser ,
                                                    AnggaranRKAP = j.JumlahRKAP,
                                                    AnggaranABT=j.JumlahABT,

                                       }).ToList<Laporan>();
            oldIdRekening = 0;
            foreach (Laporan lap in lstLevel1)
            {
                if (lap.IDRekening != oldIdRekening)
                {
                    if (lap.AnggaranMurni > 0 || lap.AnggaranGeser>0 || lap.AnggaranRKAP >0 || lap.AnggaranABT >0 ){
                        TreeGridNode nodeLevel1 = TreeRingkasan.Nodes.Add(lap.Kode, lap.Nama, lap.AnggaranMurni.ToRupiahInReport(),
                               lap.AnggaranGeser.ToRupiahInReport(),
                            lap.AnggaranRKAP.ToRupiahInReport(), lap.AnggaranABT.ToRupiahInReport(), lap.Level.ToString());
                        ProsesRingkasaBelanjaLevel2(nodeLevel1, lap.IDRekening);
                    }
                        oldIdRekening = lap.IDRekening;
                    //ProcessRekening(nodeSubkegiatan, oldSubKegiatan);


                }
            }

        }

        private void ProsesRingkasaBelanjaLevel2(TreeGridNode node, long ParentRekening)
        {
            long oldIdRekening;

            List<Rekening> lstRekeningBelanjaLevel2 = new List<Rekening>();
            lstRekeningBelanjaLevel2 = GlobalVar.gListRekening.FindAll(x => x.Root == 2 && x.IDParent == ParentRekening);
            // di anak dari rekening itu 
            List<ProgramKegiatanAnggaran> lstDistinctSubKegiatan = new List<ProgramKegiatanAnggaran>();
                     
            // Jumlah anggaran adalah groupng level 2
            var lstJumlahANggaran = m_lstProgramKegiatan.GroupBy(x => x.IIDRekening.ToString().Substring(0, 2))
            .Select(x => new
            {
                idrekening = x.Key,
                JumlahMurni = x.Sum(y => y.AnggaranMurni),
                JumlahGeser = x.Sum(y => y.AnggaranGeser),
                JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                JumlahABT = x.Sum(y => y.AnggaranABT),

            }).ToList();

            List<Laporan> lstLevel1 = (from m in lstRekeningBelanjaLevel2
                                       join j in lstJumlahANggaran
                                       on m.ID.ToString().Substring(0, 2) equals j.idrekening
                                       select new Laporan
                                       {
                                           Kode = m.ID.ToKodeRekening(2),
                                           IDRekening = m.ID,
                                           Level = 2,

                                           Nama = m.Nama,
                                           AnggaranMurni = j.JumlahMurni,
                                           AnggaranGeser = j.JumlahGeser,
                                           AnggaranRKAP = j.JumlahRKAP,
                                           AnggaranABT = j.JumlahABT,

                                       }).ToList<Laporan>();
            oldIdRekening = 0;
            foreach (Laporan lap in lstLevel1)
            {
                if (lap.IDRekening != oldIdRekening)
                {

                    if (lap.AnggaranMurni > 0 || lap.AnggaranGeser > 0 || lap.AnggaranRKAP > 0 || lap.AnggaranABT > 0)
                    {
                        TreeGridNode nodeLevel2 = node.Nodes.Add(lap.Kode, lap.Nama, lap.AnggaranMurni.ToRupiahInReport(),
                                   lap.AnggaranGeser.ToRupiahInReport(),
                            lap.AnggaranRKAP.ToRupiahInReport(), lap.AnggaranABT.ToRupiahInReport(), lap.Level.ToString());
                        ProsesRingkasaBelanjaLevel3(nodeLevel2, lap.IDRekening);
                    }
                    oldIdRekening = lap.IDRekening;
                    //ProcessRekening(nodeSubkegiatan, oldSubKegiatan);


                }
            }

        }
        private void ProsesRingkasaBelanjaLevel3(TreeGridNode node, long ParentRekening)
        {
            long oldIdRekening;

            List<Rekening> lstRekeningBelanjaLevel3 = new List<Rekening>();
            lstRekeningBelanjaLevel3 = GlobalVar.gListRekening.FindAll(x => x.Root == 3 && x.IDParent == ParentRekening);
            // di anak dari rekening itu 
                     
            var lstJumlahANggaran = m_lstProgramKegiatan.GroupBy(x => x.IIDRekening.ToString().Substring(0, 4))
            .Select(x => new
            {
                idrekening = x.Key,
                JumlahMurni = x.Sum(y => y.AnggaranMurni),
                JumlahGeser = x.Sum(y => y.AnggaranGeser),
                JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                JumlahABT = x.Sum(y => y.AnggaranABT),

            }).ToList();

            List<Laporan> lstLevel1 = (from m in lstRekeningBelanjaLevel3
                                       join j in lstJumlahANggaran
                                       on m.ID.ToString().Substring(0, 4) equals j.idrekening
                                       select new Laporan
                                       {
                                           Kode = m.ID.ToKodeRekening(3),
                                           IDRekening = m.ID,
                                           Level = 3,

                                           Nama = m.Nama,
                                           AnggaranMurni = j.JumlahMurni,
                                           AnggaranGeser = j.JumlahGeser,
                                           AnggaranRKAP = j.JumlahRKAP,
                                           AnggaranABT = j.JumlahABT,

                                       }).ToList<Laporan>();
            oldIdRekening = 0;
            foreach (Laporan lap in lstLevel1)
            {
                if (lap.IDRekening != oldIdRekening)
                {
                    if (lap.AnggaranMurni > 0 || lap.AnggaranGeser > 0 || lap.AnggaranRKAP > 0 || lap.AnggaranABT > 0)
                    {

                        TreeGridNode nodeLevel3 = node.Nodes.Add(lap.Kode, lap.Nama, lap.AnggaranMurni.ToRupiahInReport(),
                                   lap.AnggaranGeser.ToRupiahInReport(),
                            lap.AnggaranRKAP.ToRupiahInReport(), lap.AnggaranABT.ToRupiahInReport(), lap.Level.ToString());
                        ProsesRingkasaBelanjaLevel4(nodeLevel3, lap.IDRekening);
                    }
                    oldIdRekening = lap.IDRekening;
                    //ProcessRekening(nodeSubkegiatan, oldSubKegiatan);


                }
            }

        }
        private void ProsesRingkasaBelanjaLevel4(TreeGridNode node, long ParentRekening)
        {
            long oldIdRekening;

            List<Rekening> lstRekeningBelanjaLevel4 = new List<Rekening>();
            lstRekeningBelanjaLevel4 = GlobalVar.gListRekening.FindAll(x => x.Root == 4 && x.IDParent == ParentRekening);
            // di anak dari rekening itu 

            var lstJumlahANggaran = m_lstProgramKegiatan.GroupBy(x => x.IIDRekening.ToString().Substring(0, 6))
            .Select(x => new
            {
                idrekening = x.Key,
                JumlahMurni = x.Sum(y => y.AnggaranMurni),
                JumlahGeser = x.Sum(y => y.AnggaranGeser),
                JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                JumlahABT = x.Sum(y => y.AnggaranABT),

            }).ToList();

            List<Laporan> lstLevel1 = (from m in lstRekeningBelanjaLevel4
                                       join j in lstJumlahANggaran
                                       on m.ID.ToString().Substring(0, 6) equals j.idrekening
                                       select new Laporan
                                       {
                                           Kode = m.ID.ToKodeRekening(4),
                                           IDRekening = m.ID,
                                           Level = 4,

                                           Nama = m.Nama,
                                           AnggaranMurni = j.JumlahMurni,
                                           AnggaranGeser = j.JumlahGeser,
                                           AnggaranRKAP = j.JumlahRKAP,
                                           AnggaranABT = j.JumlahABT,

                                       }).ToList<Laporan>();
            oldIdRekening = 0;
            foreach (Laporan lap in lstLevel1)
            {
                if (lap.IDRekening != oldIdRekening)
                {
                    if (lap.AnggaranMurni > 0 || lap.AnggaranGeser > 0 || lap.AnggaranRKAP > 0 || lap.AnggaranABT > 0)
                    {
                        TreeGridNode nodeLevel4 = node.Nodes.Add(lap.Kode, lap.Nama, lap.AnggaranMurni.ToRupiahInReport(),
                                   lap.AnggaranGeser.ToRupiahInReport(),
                            lap.AnggaranRKAP.ToRupiahInReport(), lap.AnggaranABT.ToRupiahInReport(), lap.Level.ToString());
                        ProsesRingkasaBelanjaLevel5(nodeLevel4, lap.IDRekening);
                    }
                    oldIdRekening = lap.IDRekening;
                    //ProcessRekening(nodeSubkegiatan, oldSubKegiatan);


                }
            }

        }
        private void ProsesRingkasaBelanjaLevel5(TreeGridNode node, long ParentRekening)
        {
            long oldIdRekening;

            List<Rekening> lstRekeningBelanjaLevel5 = new List<Rekening>();
            lstRekeningBelanjaLevel5 = GlobalVar.gListRekening.FindAll(x => x.Root == 5 && x.IDParent == ParentRekening);
            // di anak dari rekening itu 

            var lstJumlahANggaran = m_lstProgramKegiatan.GroupBy(x => x.IIDRekening.ToString().Substring(0, 8))
            .Select(x => new
            {
                idrekening = x.Key,
                JumlahMurni = x.Sum(y => y.AnggaranMurni),
                JumlahGeser = x.Sum(y => y.AnggaranGeser),
                JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                JumlahABT = x.Sum(y => y.AnggaranABT),

            }).ToList();

            List<Laporan> lstLevel1 = (from m in lstRekeningBelanjaLevel5
                                       join j in lstJumlahANggaran
                                       on m.ID.ToString().Substring(0, 8) equals j.idrekening
                                       select new Laporan
                                       {
                                           Kode = m.ID.ToKodeRekening(5),
                                           IDRekening = m.ID,
                                           Level = 5,

                                           Nama = m.Nama,
                                           AnggaranMurni = j.JumlahMurni,
                                           AnggaranGeser = j.JumlahGeser,
                                           AnggaranRKAP = j.JumlahRKAP,
                                           AnggaranABT = j.JumlahABT,

                                       }).ToList<Laporan>();
            oldIdRekening = 0;
            foreach (Laporan lap in lstLevel1)
            {
                if (lap.IDRekening != oldIdRekening)
                {
                    if (lap.AnggaranMurni > 0 || lap.AnggaranGeser > 0 || lap.AnggaranRKAP > 0 || lap.AnggaranABT > 0)
                    {
                        TreeGridNode nodeLevel5 = node.Nodes.Add(lap.Kode, lap.Nama, lap.AnggaranMurni.ToRupiahInReport(),
                                   lap.AnggaranGeser.ToRupiahInReport(),
                            lap.AnggaranRKAP.ToRupiahInReport(), lap.AnggaranABT.ToRupiahInReport(), lap.Level.ToString());
                        ProsesRingkasaBelanjaLevel6(nodeLevel5, lap.IDRekening);
                    }
                    oldIdRekening = lap.IDRekening;
                    //ProcessRekening(nodeSubkegiatan, oldSubKegiatan);


                }
            }

        }
        private void ProsesRingkasaBelanjaLevel6(TreeGridNode node, long ParentRekening)
        {
            long oldIdRekening;

            List<Rekening> lstRekeningBelanjaLevel6 = new List<Rekening>();
            lstRekeningBelanjaLevel6 = GlobalVar.gListRekening.FindAll(x => x.Root == 6 && x.IDParent == ParentRekening);
            // di anak dari rekening itu 

            var lstJumlahANggaran = m_lstProgramKegiatan.GroupBy(x => x.IIDRekening.ToString())
            .Select(x => new
            {
                idrekening = x.Key,
                JumlahMurni = x.Sum(y => y.AnggaranMurni),
                JumlahGeser = x.Sum(y => y.AnggaranGeser),
                JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                JumlahABT = x.Sum(y => y.AnggaranABT),

            }).ToList();

            List<Laporan> lstLevel1 = (from m in lstRekeningBelanjaLevel6
                                       join j in lstJumlahANggaran
                                       on m.ID.ToString().ToString() equals j.idrekening
                                       select new Laporan
                                       {
                                           Kode = m.ID.ToKodeRekening(6),
                                           IDRekening = m.ID,
                                           Level = 6,

                                           Nama = m.Nama,
                                           AnggaranMurni = j.JumlahMurni,
                                           AnggaranGeser = j.JumlahGeser,
                                           AnggaranRKAP = j.JumlahRKAP,
                                           AnggaranABT = j.JumlahABT,

                                       }).ToList<Laporan>();
            oldIdRekening = 0;
            foreach (Laporan lap in lstLevel1)
            {
                if (lap.IDRekening != oldIdRekening)
                {
                    if (lap.AnggaranMurni > 0 || lap.AnggaranGeser > 0 || lap.AnggaranRKAP > 0 || lap.AnggaranABT > 0)
                    {
                        TreeGridNode nodeLevel4 = node.Nodes.Add(lap.Kode, lap.Nama, lap.AnggaranMurni.ToRupiahInReport(),
                                   lap.AnggaranGeser.ToRupiahInReport(),
                            lap.AnggaranRKAP.ToRupiahInReport(), lap.AnggaranABT.ToRupiahInReport(), lap.Level.ToString());

                    }
                    oldIdRekening = lap.IDRekening;
                    //ProcessRekening(nodeSubkegiatan, oldSubKegiatan);


                }
            }

        }

        private void SembunyikanKolomBasdTahap()
        {

            switch (m_iTahap)
            {
                case 2:


                    TreeRingkasan.Columns[2].Visible = true;
                    TreeRingkasan.Columns[3].Visible = false;
                    TreeRingkasan.Columns[4].Visible = false;
                    TreeRingkasan.Columns[5].Visible = false;



                    break;
                case 3:

                    TreeRingkasan.Columns[2].Visible = true;
                    TreeRingkasan.Columns[3].Visible = true;
                    TreeRingkasan.Columns[4].Visible = false;
                    TreeRingkasan.Columns[5].Visible = false;


                    break;
                case 4:

                    TreeRingkasan.Columns[2].Visible = true;
                    TreeRingkasan.Columns[3].Visible = true;
                    TreeRingkasan.Columns[4].Visible = true;
                    TreeRingkasan.Columns[5].Visible = false;

                    break;
                case 5:

                    TreeRingkasan.Columns[2].Visible = true;
                    TreeRingkasan.Columns[3].Visible = true;
                    TreeRingkasan.Columns[4].Visible = true;
                    TreeRingkasan.Columns[5].Visible = true;

                    break;

            }
        }

        private void TreeRingkasan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
