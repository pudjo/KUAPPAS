using BP.Bendahara;
using DTO.Bendahara;
using Formatting;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using DTO;
using System.IO;
namespace KUAPPAS.Bendahara
{
    public partial class frmRegisterSPP : ChildForm
    {
        private int m_iIDDInas;
        private int m_iKodeUK;
        private List<SPP> m_lstSPP;
        private List<SPPRekening> m_DetailSPP;
  

        DataGridViewCellStyle _merah = new DataGridViewCellStyle();
        DataGridViewCellStyle _pink = new DataGridViewCellStyle();
        DataGridViewCellStyle _ijo = new DataGridViewCellStyle();

        DataGridViewCellStyle _ditolakStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _didiskusikanStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _diTerimaStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _diTangguhkanStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _baruStyle = new DataGridViewCellStyle();

        private bool mbOnlyDisplay;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        private int m_iMode;
        CetakPDF oCetakPDF;
        PdfPage previousPage;
        int m_iJenisBendahara;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        public frmRegisterSPP(int mode)
        {
            InitializeComponent();
            m_iIDDInas = 0;
            m_iKodeUK = 0;
            m_iMode = mode;

        }
        public int Mode
        {
            set { m_iMode = value; }
        }
        private void frmRegisterSPP_Load(object sender, EventArgs e)
        {
            
            ctrlDinas1.Create();
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlDinas1.SetID(GlobalVar.Pengguna.SKPD);
                m_iIDDInas = GlobalVar.Pengguna.SKPD;
            }
            gridSPP.FormatHeader();
            switch (m_iMode)
            {
                case 0:
                    ctrlHeader1.SetCaption("Daftar Register SPP");
                    gridSPP.Columns["NoSPM"].Visible = false;
                    gridSPP.Columns["TglSPM"].Visible = false;
                    gridSPP.Columns["NoSP2D"].Visible = false;
                    gridSPP.Columns["TglSP2D"].Visible = false;
                    break;
                case 1:
                    ctrlHeader1.SetCaption("Daftar Register SPM");
                    gridSPP.Columns["NoSP2D"].Visible = false;
                    gridSPP.Columns["TglSP2D"].Visible = false;
                    gridSPP.Columns["UP"].HeaderText ="Jumlah";
                    gridSPP.Columns["GU"].Visible = false;
                    gridSPP.Columns["TU"].Visible = false;
                    gridSPP.Columns["LSGJ"].Visible = false;
                    gridSPP.Columns["LSBJ"].Visible = false;
                    

                    break;
                case 2:
                    ctrlHeader1.SetCaption("Daftar Register SP2D");
                   
                    gridSPP.Columns["UP"].HeaderText = "Jumlah";
                    gridSPP.Columns["GU"].Visible = false;
                    gridSPP.Columns["TU"].Visible = false;
                    gridSPP.Columns["LSGJ"].Visible = false;
                    gridSPP.Columns["LSBJ"].Visible = false;
                    
                
                    break;


            }
        }

        private void cmdTampilkan_Click(object sender, EventArgs e)
        {
            try
            {
                m_iIDDInas = ctrlDinas1.GetID();
                m_iKodeUK = ctrlDinas1.KodeUK();

                DateTime tanggalAwal = ctrlPeriode1.TanggalAwaal;
                DateTime tanggalAkhir = ctrlPeriode1.TanggalAkhir;

                LoadData(m_iIDDInas, tanggalAwal, tanggalAkhir);

                lblPencarian.Visible = true;
                txtCari.Visible = true;
                cmdCari.Visible = true;
                cmdCariLagi.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        public void LoadData(int pIDDinas, DateTime tanggalAwal, DateTime tanggalAkhir, int JenisTanggal = 1)
        {

            try
            {

                string[] arrJenis = {"UP","GU", "TU","LS","GJ"};

                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);
                m_iIDDInas = pIDDinas;
                p.IDDInas = m_iIDDInas;// ctrlSKPD1.GetID();
                p.LstStatus = new List<int>();

                p.Status = -1;

                p.Jenis = -1;
                GlobalVar.gListSPP = oLogic.GetSPP(p);
                






                
                // p.Status 
                m_lstSPP = new List<SPP>();

                // m_lstSPP = oLogic.GetSPP(p);

                m_lstSPP = GlobalVar.gListSPP;//.FindAll(spp=>)
             
             


                gridSPP.Rows.Clear();

                int iRow = 0;
                decimal cJumlah = 0L;

                foreach (SPP spp in m_lstSPP)
                {
                    if (m_iMode == 0)
                    {
                        if (spp.dtSPP >= tanggalAwal && spp.dtSPP <= tanggalAkhir)
                        {
                            string[] rowSPP = { spp.NoUrut.ToString(), arrJenis[spp.Jenis],  spp.dtSPP.ToString("dd MMM"), spp.NoSPP,
                                       spp.dtSPM.ToString("dd MMM"), spp.NoSPM,
                                       spp.dtTerbit.ToString("dd MMM"), spp.NoSP2D,
                                        spp.Keterangan, 
                                        spp.Jenis ==0 ? spp.Jumlah.ToRupiahInReport():"0",
                                        spp.Jenis ==1? spp.Jumlah.ToRupiahInReport():"0",
                                        spp.Jenis ==2? spp.Jumlah.ToRupiahInReport():"0",
                                        spp.Jenis ==3? spp.Jumlah.ToRupiahInReport():"0",
                                        spp.Jenis ==4? spp.Jumlah.ToRupiahInReport():"0"                                   
                                   };
                            gridSPP.Rows.Add(rowSPP);
                            cJumlah = cJumlah + spp.Jumlah;
                            iRow++;
                        }

                    }
                    if (m_iMode == 1)
                    {
                        if (spp.dtSPM >= tanggalAwal && spp.dtSPM <= tanggalAkhir)
                        {
                            string[] rowSPM = { spp.NoUrut.ToString(), arrJenis[spp.Jenis],  spp.dtSPP.ToString("dd MMM"), spp.NoSPP,
                                       spp.dtSPM.ToString("dd MMM"), spp.NoSPM,
                                       spp.dtTerbit.ToString("dd MMM"), spp.NoSP2D,
                                        spp.Keterangan, 
                                        spp.Jumlah.ToRupiahInReport()
                                   };

                            gridSPP.Rows.Add(rowSPM);
                            cJumlah = cJumlah + spp.Jumlah;
                            iRow++;
                        }

                    }
                    if (m_iMode == 2)
                    {
                        if (spp.dtTerbit >= tanggalAwal && spp.dtTerbit <= tanggalAkhir)
                        {
                            string[] rowSP2D = { spp.NoUrut.ToString(), arrJenis[spp.Jenis],  spp.dtSPP.ToString("dd MMM"), spp.NoSPP,
                                       spp.dtSPM.ToString("dd MMM"), spp.NoSPM,
                                       spp.dtTerbit.ToString("dd MMM"), spp.NoSP2D,
                                        spp.Keterangan, 
                                        spp.Jumlah.ToRupiahInReport()
                                   };
                            gridSPP.Rows.Add(rowSP2D);

                            cJumlah = cJumlah + spp.Jumlah;
                            iRow++;
                        }

                    }


                    
                    //if (spp.Status == 1)
                    //    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.AliceBlue;
                    //if (spp.Status == 2)
                    //    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.LightBlue;

                    //if (spp.Status == 3)
                    //    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.AntiqueWhite;// Red;
                    //if (spp.Status == 4)
                    //    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.LightSalmon;// PaleVioletRed;// IndianRed;// Red;

              //      cJumlah = cJumlah + spp.Jumlah;
                    


                }
                txtJumlah.Text = cJumlah.ToRupiahInReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        
        private void cmdCari_Click(object sender, EventArgs e)
        {

            try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridSPP.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value == DBNull.Value || cell.Value == null)
                            continue;
                        if (cell.Value.ToString().ToUpper().Contains(txtCari.Text.Trim().ToUpper()) && cell.Visible == true)
                        {
                            containingCells.Add(cell);
                        }
                    }
                }
                if (containingCells.Count > 0)
                    gridSPP.CurrentCell = containingCells[currentContainingCellListIndex++];
                else
                    MessageBox.Show("Tidak diketemukan");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmdCariLagi_Click(object sender, EventArgs e)
        {
            if (containingCells.Count > 0 && currentContainingCellListIndex < containingCells.Count)
                gridSPP.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            try
            {
                PdfDocument document = new PdfDocument();

                PdfSection section = document.Sections.Add();
                document.PageSettings.Margins.Left = 8;
                document.PageSettings.Margins.Top = 5;
                document.PageSettings.Margins.Right = 2;
                document.PageSettings.Margins.Bottom = 8;

                section.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section.PageSettings.Height = 935;// = PdfPageSize.Legal;

                section.PageSettings.Orientation = PdfPageOrientation.Landscape;

                float yPos = 0;
                SaatnyacetakKesimpulan = false;
                PdfPage page = section.Pages.Add();

                PdfGraphics graphics = page.Graphics;
                previousPage = page;
                document.Pages.PageAdded += PagesPanjar_PageAdded;// PagesPajak_PageAdded;

                yPos = 10;
                PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);


                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;

                //stringFormat.CharacterSpacing = 2f;
                oCetakPDF = new CetakPDF();
                //SizeF size = font12.MeasureString("xxx");


                Pejabat pimpinan = new Pejabat();
                Pejabat bendahara = new Pejabat();
                pimpinan = ctrlDinas1.GetPimpinan(ctrlPeriode1.TanggalAkhir);
                bendahara = ctrlDinas1.GetBendaharaPengeluaran(ctrlPeriode1.TanggalAkhir);

                if (pimpinan == null)
                {
                    MessageBox.Show("Data Pimpinan belum di setting di master pejabat");
                    return ;

                }

                if (bendahara == null)
                {
                    MessageBox.Show("Data Bndahara belum di setting di master pejabat");
                    return ;

                }


                float kiri = 15;
                float posTitikdua = 150;
                float posNama = 155;

                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                   page.GetClientSize().Width, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "REGISTER SPP/SPM/SP2D"
                     , 10, kiri, yPos,
                     page.GetClientSize().Width, stringFormat, true, false, true);
                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = yPos + 20;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          ctrlDinas1.GetNamaSKPD(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Bendahara Pengeluaran "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          bendahara.Nama, 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Periode "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          ctrlPeriode1.TanggalAwaal.ToTanggalIndonesia() + "-" +
                ctrlPeriode1.TanggalAkhir.ToTanggalIndonesia(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Tahun Anggaran"
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          GlobalVar.TahunAnggaran.ToString(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);
                #region judul
                PdfGrid pdfGridHeader = new PdfGrid();
                DataTable tableHeader = new DataTable();
                tableHeader.Columns.Add("No");
                tableHeader.Columns.Add("Jenis");
                tableHeader.Columns.Add("TGLSPP");
                tableHeader.Columns.Add("NoSPP");
                tableHeader.Columns.Add("TglSPM");
                tableHeader.Columns.Add("NoSPM");
                tableHeader.Columns.Add("TGlSP2D");
                tableHeader.Columns.Add("NoSP2D");
                tableHeader.Columns.Add("Keterangan");
                tableHeader.Columns.Add("Jumlah");
               // tableHeader.Columns.Add("SKPD");
                
                tableHeader.Rows.Add(new string[]
                    {"No","Jenis","SPP","SPP","SPM","SPM","SP2D","SP2D","Uraian","Jumlah"});

                tableHeader.Rows.Add(new string[] { "No", "Jenis", "Tanggal", "Nomor", "Tanggal", "Nomor", "Tanggal", "Nomor", "Uraian", "Jumlah" });
                
                pdfGridHeader.DataSource = tableHeader; //data


                pdfGridHeader.Columns[0].Width = 20;
                pdfGridHeader.Columns[1].Width = 20;

                // Angka 
                pdfGridHeader.Columns[2].Width = 40;
                pdfGridHeader.Columns[3].Width = 100;
                pdfGridHeader.Columns[4].Width = 40;
                pdfGridHeader.Columns[5].Width = 100;
                pdfGridHeader.Columns[6].Width = 40;
                pdfGridHeader.Columns[7].Width = 100;
                pdfGridHeader.Columns[8].Width = 290;
                pdfGridHeader.Columns[9].Width = 85;
                


                pdfGridHeader.Rows[0].Cells[0].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[1].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[2].ColumnSpan = 2;
                pdfGridHeader.Rows[0].Cells[4].ColumnSpan = 2;
                pdfGridHeader.Rows[0].Cells[6].ColumnSpan = 2;

                pdfGridHeader.Rows[0].Cells[8].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[9].RowSpan = 2;
              //  pdfGridHeader.Rows[0].Cells[10].RowSpan = 2;


                PdfFont fontHeader = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));
                PdfGridCellStyle cellHeaderStyle0 = new PdfGridCellStyle();

                PdfStringFormat stringFormatHeader0 = new PdfStringFormat();
                stringFormatHeader0.Alignment = PdfTextAlignment.Center;
                stringFormatHeader0.LineAlignment = PdfVerticalAlignment.Middle;

                fontHeader = new PdfTrueTypeFont(new System.Drawing.Font("Arial", fontHeader.Size,
                     FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));
                cellHeaderStyle0.Font = fontHeader;
                cellHeaderStyle0.StringFormat = stringFormatHeader0;


                for (int col = 0; col < pdfGridHeader.Columns.Count; col++)
                    pdfGridHeader.Columns[col].Format = stringFormatHeader0;
                pdfGridHeader.Headers.Clear();
                PdfGridLayoutResult pdfHeaderGridResult = pdfGridHeader.Draw(page, new PointF(kiri, yPos));
                yPos = pdfHeaderGridResult.Bounds.Bottom;
                #endregion judul

                #region gridPanjar
                PdfGrid pdfGrid = new PdfGrid();

                int count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                table.Columns.Add("No");
                table.Columns.Add("Jenis");
                table.Columns.Add("TGLSPP");
                table.Columns.Add("NoSPP");
                table.Columns.Add("TglSPM");
                table.Columns.Add("NoSPM");
                table.Columns.Add("TGlSP2D");
                table.Columns.Add("NoSP2D");
                table.Columns.Add("Keterangan");
                table.Columns.Add("Jumlah");
                
                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();


                decimal akumulasi = 0L;
                decimal sisa = 0;
                int i =0;
                for (int idx = 0; idx < gridSPP.Rows.Count; idx++)
                {
                    table.Rows.Add(new string[]
                        {


                           (++i).ToString(),
                           DataFormat.GetString(gridSPP.Rows[idx].Cells[1].Value).ReplaceUnicode(),
                           DataFormat.GetString(gridSPP.Rows[idx].Cells[2].Value).ReplaceUnicode(),
                           DataFormat.GetString(gridSPP.Rows[idx].Cells[3].Value).ReplaceUnicode(),
                           DataFormat.GetString(gridSPP.Rows[idx].Cells[4].Value).ReplaceUnicode(),     
                           DataFormat.GetString(gridSPP.Rows[idx].Cells[5].Value).ReplaceUnicode(),     
                           DataFormat.GetString(gridSPP.Rows[idx].Cells[6].Value).ReplaceUnicode(),   
                           DataFormat.GetString(gridSPP.Rows[idx].Cells[7].Value).ReplaceUnicode(),   
                            DataFormat.GetString(gridSPP.Rows[idx].Cells[8].Value).ReplaceUnicode(),   
                            DataFormat.GetString(gridSPP.Rows[idx].Cells[9].Value).ReplaceUnicode(),   
                
  
                        });
                }

                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 20;
                pdfGrid.Columns[1].Width = 20;

                // Angka 
                pdfGrid.Columns[2].Width = 40;
                pdfGrid.Columns[3].Width = 100;
                pdfGrid.Columns[4].Width = 40;
                pdfGrid.Columns[5].Width = 100;
                pdfGrid.Columns[6].Width = 40;
                pdfGrid.Columns[7].Width = 100;
                pdfGrid.Columns[8].Width = 290;
                pdfGrid.Columns[9].Width = 85;
                

                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                //gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGrid.Style = gridStyle;
                pdfGrid.Headers.Clear();

                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

             
                pdfGrid.Columns[9].Format = formatKolomAngka;










                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();

                pdfGrid.RepeatHeader = true;
                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;

                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", font.Size, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));

                cellHeaderStyle.Font = font;

                cellHeaderStyle.StringFormat = stringFormatHeader;
                for (int c = 0; c < pdfGrid.Headers.Count; c++)
                {
                    pdfGrid.Headers[c].ApplyStyle(cellHeaderStyle);
                    pdfGrid.Headers[c].Height = 30;

                }


                for (int idx = 0; idx < pdfGrid.Rows.Count; idx++)
                {
                    pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 7,
                        FontStyle.Regular)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);

                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        if (c == 4)
                        {
                            pdfGrid.Rows[idx].Cells[c].Style.CellPadding = new PdfPaddings(1, 1, 1, 0);
                        }


                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.01F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.01F;

                    }

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.
                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(pdfHeaderGridResult.Page, new PointF(kiri, yPos));
                yPos = pdfGridLayoutResult.Bounds.Bottom;
             
                #endregion gridKas

                PdfGrid pdfGridRingkasan = new PdfGrid();


                //Create a DataTable
                table = new DataTable();
                //Add columns to table

                float posketerangan = 40 + 70 + 200;
                float pospenerimaan = 40 + 70 + 430 + 80;
                float pospenyetoran = 40 + 70 + 430 + 80 + 65;
                //float possaldo=40+70+430+80+65+ 65;
                //float posakhir=40+70+430+80+65+ 65+ 65;




                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();

                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../Bukukas.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../Bukukas.pdf");
                pV.Show();


                return ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ;
            }
        }
        private void PagesPanjar_PageAdded(object sender, PageAddedEventArgs args)
        {
            float yPos = PosisiTerakhir + 5;

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;
            PdfStringFormat stringFormat = new PdfStringFormat();


            CetakPDF oCetakPDF = new CetakPDF();


            if (SaatnyacetakKesimpulan == true)
            {





                Pejabat bendahara = new Pejabat();
               
                    bendahara = ctrlDinas1.GetBendaharaPengeluaran(ctrlPeriode1.TanggalAkhir);
                
               
                yPos = yPos + 10;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.gPemda.Ibukota + "," + ctrlPeriode1.TanggalAkhir.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Jabatan, 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + bendahara.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);




            }






            previousPage = args.Page;
        }
        
    }
}
