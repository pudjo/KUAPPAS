using DTO;
using Formatting;

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using BP.Akuntansi;
using DTO.Akuntansi;
using BP;
using Syncfusion.Pdf.Parsing;
using Microsoft.Office.Interop.Word;
using NPOI.SS.Formula.Functions;
using System.Diagnostics.Eventing.Reader;


namespace KUAPPAS.Akunting
{
    public class LaporanPerda_1_3 : XLaporan
    {
        
        ReportDesign mmrd;
        Single lebar;
        Single m_kiri;
        ParameterLaporan mpl;
        string msNamaSKPD;
        int mTahun;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        CetakPDF oCetakPDF;
        PdfPage previousPage;

        public bool CetakLaporanPerda(ParameterLaporan p)
        {
            try
            {

                PerdaRealisasi_1_3Logic oLogic = new PerdaRealisasi_1_3Logic(p.Tahun);
                List<PerdaRealisasi_1_3> lst = new List<PerdaRealisasi_1_3>();
                mpl = new ParameterLaporan();
                mpl = p;
                oLogic.jenis = p.Jenis;

                lst = oLogic.Process(p.IDDinas);
                return CetakPerda(lst,  p);       
        




                
            } catch(Exception ex)
            {
                Error = ex.Message;
                return false;


            }
        }
        #region cetakPerda
        private bool CetakPerda(List<PerdaRealisasi_1_3>lst, ParameterLaporan pl)
        {
            mmrd = new ReportDesign();
            mmrd = pl.Rd;
            
            
            
            
            try
            {
                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                section1.PageSettings.Width = 700;// = PdfPageSize.Legal;
                section1.PageSettings.Height = 950;// = PdfPageSize.Legal;
                section1.PageSettings.Orientation = PdfPageOrientation.Landscape;
                
                
                PdfMargins margin = new PdfMargins();
                margin.All = 0;
                //Create new instance for page settings.
                PdfPageSettings setting = new PdfPageSettings();
                //Set document margin.
                setting.Margins = margin;
                document.PageSettings = setting;


                PdfPage page = section1.Pages.Add();
                
                previousPage = page;
                document.Pages.PageAdded += Perda_I_PageAdded;
                SaatnyacetakKesimpulan = false;

                CetakPDF oCetakPDF = new CetakPDF();
                float yPos;
                float kiri = 0;
                yPos = TulisNamaLaporan(oCetakPDF, previousPage, pl);
                if (yPos == 0)
                {
                    return false;
                }

                yPos = TulisJudulUtamaLaporan(oCetakPDF, previousPage, pl, yPos, kiri);
                if (yPos == 0)
                {
                    return false;
                }

                PdfStringFormat stringFormatLeft = new PdfStringFormat();
                stringFormatLeft.Alignment = PdfTextAlignment.Left;
                UrusanLogic uLogic = new UrusanLogic(GlobalVar.TahunAnggaran);
                int idUrusan = 0;
                 Int32.TryParse(pl.IDDinas.ToString().Substring(0,3),out idUrusan);
                Urusan u = uLogic.GetByID(idUrusan);
                if (u== null)
                {
                    Error = "Tidak bisa memangggil Urusan Pemerintahan";
                    return false;
                }
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Urusan Pemerintahan ", 8, kiri, yPos,
                       page.GetClientSize().Width, stringFormatLeft, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 90, yPos,
                       page.GetClientSize().Width, stringFormatLeft, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, u.Nama, 8, 95, yPos,
                       page.GetClientSize().Width, stringFormatLeft, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Organisasi ", 8, kiri, yPos,
                       page.GetClientSize().Width, stringFormatLeft, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 8, 90, yPos,
                       page.GetClientSize().Width, stringFormatLeft, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pl.NamaDinas  , 8, 95, yPos,
                       page.GetClientSize().Width, stringFormatLeft, true , false, true);


                PdfGrid pdfGrid = new PdfGrid();

                if (TulisJudulLaporan(pdfGrid) == false)
                {
                    return false;
                }
                pdfGrid.RepeatHeader = true;
                #region isi
                //< PerdaRealisasi_1 > lst
                if(lst== null)
                {
                    Error = "Tidak menemukan data";
                    return false;
                }
                foreach (PerdaRealisasi_1_3 r in lst)
                {
                    PdfGridRow pdfGridRow = pdfGrid.Rows.Add();

                    pdfGridRow.Cells[0].Value = r.KodeUrusan;
                    pdfGridRow.Cells[1].Value = r.KodeDinas;

                    pdfGridRow.Cells[2].Value = r.KodeSubKegiatan.Trim();
                    pdfGridRow.Cells[3].Value = r.Kode;
                    pdfGridRow.Cells[4].Value = r.Nama;
                    pdfGridRow.Cells[5].Value = r.Anggaran.ToRupiahInReport();
                    pdfGridRow.Cells[6].Value = r.Realisasi.ToRupiahInReport();
                    pdfGridRow.Cells[7].Value = r.Selisih.ToRupiahInReport();
                    pdfGridRow.Cells[8].Value = r.Persen;
                    pdfGridRow.Cells[9].Value = r.DasarHukum;







                }
                pdfGrid.Columns[0].Width = 20;
                pdfGrid.Columns[1].Width = 60;
                pdfGrid.Columns[2].Width = 45;
                pdfGrid.Columns[3].Width = 50;
                pdfGrid.Columns[4].Width = 280;
                pdfGrid.Columns[5].Width = 70;
                pdfGrid.Columns[6].Width = 70;
                pdfGrid.Columns[7].Width = 70;
                pdfGrid.Columns[8].Width = 40;
                pdfGrid.Columns[9].Width = 140;
                DasarHukumLogic dasarHukumLogic = new DasarHukumLogic(GlobalVar.TahunAnggaran);
                PerdaRealisasi_1_3Logic oLogic = new PerdaRealisasi_1_3Logic(GlobalVar.TahunAnggaran);
                List<Rekening> lstRekening = oLogic.GetListRekeningPendapatan();
                List<DasarHukum> lstDasarHukum = dasarHukumLogic.GetByIDRekening(lstRekening);
                

                int jmlBaris = 5;
                int baris = 0;
                foreach (DasarHukum dh in lstDasarHukum)
                {
                    pdfGrid.Rows[baris].Cells[9].RowSpan = jmlBaris;
                    pdfGrid.Rows[baris].Cells[9].Value = dh.Keterangan;
                }
                 







                PdfGridStyle gridStyle = new PdfGridStyle();
                gridStyle.CellPadding = new PdfPaddings(2, mmrd.Spasi, 2, mmrd.Spasi);
                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                
                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 6));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                
                cellStyle.TextBrush = PdfBrushes.Blue;
                PdfStringFormat format = new PdfStringFormat();
                format.Alignment = PdfTextAlignment.Center;
                //Set string format to grid cell.
                PdfBorders borders = new PdfBorders();
                borders.All = PdfPens.White;
                cellStyle.Borders = borders;
               
                cellStyle.StringFormat = format;

                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();

                for (int id = 0; id < pdfGrid.Rows.Count; id++)
                {


                    pdfGrid.Rows[id].Height = 10;
                                        
                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        
                           pdfGrid.Rows[id].Cells[c].Style.Borders.Bottom.Width = 0.01F;
                            pdfGrid.Rows[id].Cells[c].Style.Borders.Top.Width = 0.01F;
                            pdfGrid.Rows[id].Cells[c].Style.Borders.Left.Width = 0.01F;
                            pdfGrid.Rows[id].Cells[c].Style.Borders.Right.Width = 0.01F;

                        pdfGrid.Rows[id].Cells[c].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 6));
                        
                        if (c==5 || c == 6 || c == 7)
                        {
                            pdfGrid.Rows[id].Cells[c].Style.StringFormat = new PdfStringFormat(PdfTextAlignment.Right,
                                PdfVerticalAlignment.Middle);
                        }
                        if (c == 8 )
                        {
                            pdfGrid.Rows[id].Cells[c].Style.StringFormat = new PdfStringFormat(PdfTextAlignment.Center,
                                PdfVerticalAlignment.Middle);
                        }
                        if (c < 5)
                        {
                            pdfGrid.Rows[id].Cells[c].Style.StringFormat = new PdfStringFormat(PdfTextAlignment.Left,
                                PdfVerticalAlignment.Middle);
                        }
                        if (lst[id].Bold == 1)
                        {
                            pdfGrid.Rows[id].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font(mmrd.FontName, 6,
                                FontStyle.Bold));

                        }
                        else
                        {
                            pdfGrid.Rows[id].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font(mmrd.FontName, 6,
                                FontStyle.Regular));
                        }
                        
                    }
                }

                PdfGridLayoutFormat fmt = new PdfGridLayoutFormat();
                fmt.Break = PdfLayoutBreakType.FitPage;
                fmt.PaginateBounds = new RectangleF(0, 0, page.Size.Width - 0, page.Size.Height - 130);
                 

                    


                        PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos), fmt);
                yPos = pdfGridLayoutResult.Bounds.Bottom;
                #endregion
                SaatnyacetakKesimpulan = true;
                m_kiri = kiri;
                PosisiTerakhir = yPos;
                page = document.Pages.Add();



                
                //Add footer
                AddFooter(document, pl.AwalHalaman);

                string namaFile = Path.GetFullPath(@"Perda1.pdf");

                using (FileStream outputFileStream = new FileStream(namaFile, FileMode.Create, FileAccess.ReadWrite))
                {
                    document.Save(outputFileStream);
                    
                }



                document.Close(true);
                System.Diagnostics.Process.Start(namaFile);


                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;

            }
        }
        #endregion
        
        public static void AddFooter(PdfDocument doc, int offset)
        {
            //Set the font and brush
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 6);
            PdfBrush brush = new PdfSolidBrush(Color.Black);

            for (int i = 0; i < doc.Pages.Count; i++)
            {
                int count = i;
                string value = offset.ToString();

                //Create a PDF Template
                PdfTemplate template = new PdfTemplate(doc.Pages[i].GetClientSize().Width, 300);
                
                    template.Graphics.DrawString("Halaman " + value, font, brush, new PointF(495, 295));
                    doc.Pages[i].Graphics.DrawPdfTemplate(template, new PointF(300, 295));
                //600
                offset += 1;
            }
        }
        #region cetakJudul
        private bool TulisJudulLaporan(PdfGrid pdfGrid)
        {

            try
            {

                pdfGrid.Columns.Add(10);
                pdfGrid.Headers.Add(2);


                PdfGridRow pdfGridHeader = pdfGrid.Headers[0];



                pdfGridHeader.Cells[0].Value = "Kode Rekening";
                pdfGridHeader.Cells[1].Value = "Kode Rekening";
                pdfGridHeader.Cells[2].Value = "Kode Rekening";
                pdfGridHeader.Cells[3].Value = "Kode Rekening";
                pdfGridHeader.Cells[4].Value = "Uraian";

                pdfGridHeader.Cells[5].Value = "Jumlah";
                pdfGridHeader.Cells[6].Value = "Jumlah";
                
                pdfGridHeader.Cells[7].Value = "Bertambah/(Berkurang)";
                pdfGridHeader.Cells[8].Value = "Bertambah/(Berkurang)";
                pdfGridHeader.Cells[9].Value = "Dasar Hukum";
                
                PdfGridRow pdfGridHeader1 = pdfGrid.Headers[1];
                pdfGridHeader1.Cells[0].Value = "Kode Rekening";
                pdfGridHeader1.Cells[1].Value = "Kode Rekening";
                pdfGridHeader1.Cells[2].Value = "Kode Rekening";
                pdfGridHeader1.Cells[3].Value = "Kode Rekening";
                pdfGridHeader1.Cells[4].Value = "Uraian";

                pdfGridHeader1.Cells[5].Value = "Anggaran";
                pdfGridHeader1.Cells[6].Value = "Realisasi";
                
                pdfGridHeader1.Cells[7].Value = "Rp";
                pdfGridHeader1.Cells[8].Value = "%";
                pdfGridHeader1.Cells[9].Value = "Dasar Hukum";
                #region span


                pdfGridHeader.Cells[0].ColumnSpan= 4;
                pdfGridHeader1.Cells[0].ColumnSpan = 4;

                pdfGridHeader.Cells[0].RowSpan = 2;
                pdfGridHeader.Cells[1].RowSpan = 2;
                pdfGridHeader.Cells[2].RowSpan = 2;
                pdfGridHeader.Cells[3].RowSpan = 2;

                 pdfGridHeader.Cells[4].RowSpan = 2 ;

                pdfGridHeader.Cells[5].ColumnSpan = 2;
                pdfGridHeader.Cells[7].ColumnSpan = 2;
                #endregion

                PdfFont fontHeader = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 6, FontStyle.Bold));
                PdfGridCellStyle cellHeaderStyle0 = new PdfGridCellStyle();
                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;
                pdfGridHeader.Height = 15;
                pdfGridHeader1.Height = 15;
                
                for (int col = 0; col < pdfGridHeader.Cells.Count; col++)
                {
                    pdfGridHeader.Cells[col].Style.StringFormat = stringFormatHeader; // new PdfStringFormat(PdfTextAlignment.Center);
                    pdfGridHeader.Cells[col].Style.Font = fontHeader;
                    pdfGridHeader.Cells[col].Style.Borders.Bottom.Width = 0.02F;

                    pdfGridHeader.Cells[col].Style.Borders.Top.Width = 0.02F;
                    pdfGridHeader.Cells[col].Style.Borders.Right.Width = 0.02F;
                    pdfGridHeader.Cells[col].Style.Borders.Left.Width = 0.02F;


                }
                for (int col = 0; col < pdfGridHeader1.Cells.Count; col++)
                {
                    pdfGridHeader1.Cells[col].Style.StringFormat = stringFormatHeader; // new PdfStringFormat(PdfTextAlignment.Center);
                    pdfGridHeader1.Cells[col].Style.Font = fontHeader;

                    pdfGridHeader1.Cells[col].Style.Borders.Bottom.Width = 0.02F;
                    pdfGridHeader1.Cells[col].Style.Borders.Top.Width = 0.02F;
                    pdfGridHeader1.Cells[col].Style.Borders.Right.Width = 0.02F;
                    pdfGridHeader1.Cells[col].Style.Borders.Left.Width = 0.02F;
                }



                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }
        #endregion
        #region gantiHalaman
        private void Perda_I_PageAdded(object sender, PageAddedEventArgs args)
        {

            PdfStringFormat stringFormat = new PdfStringFormat();
            CetakPDF oCetakPDF = new CetakPDF();

            if (SaatnyacetakKesimpulan == true)
            {
                float yPos = PosisiTerakhir + 5;

                stringFormat.Alignment = PdfTextAlignment.Center;

                

                

                string namaPimpinan = "-";
                string jabatanpimpinan = "-";
                string nipPimpinan = "-";
                
                namaPimpinan = mpl.Penandatangan.Nama;
                    
                jabatanpimpinan = mpl.Penandatangan.Jabatan;
                

                lebar = previousPage.GetClientSize().Width;
                Single setengah = lebar / 2;
                Single posisitengah = m_kiri + lebar / 2;
                stringFormat.Alignment = PdfTextAlignment.Center;
                string s = mpl.Tempat + ",";
                
                s = s + mpl.Tanggal;
                
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, s
                    , 7, posisitengah, yPos,
                     lebar / 2, stringFormat, true, false, false);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Disetujui, "
                    , 7, posisitengah, yPos,
                     lebar / 2, stringFormat, true, false, false);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, jabatanpimpinan
                    , 7, posisitengah, yPos,
                     lebar / 2, stringFormat, false, false, false);
                yPos = yPos + 25;


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, namaPimpinan
                    , 7, posisitengah, yPos,
                     lebar / 2, stringFormat, true, true, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, nipPimpinan
                    , 7, posisitengah, yPos,
                     lebar / 2, stringFormat, false, false, false);

            }
            else
            {

                //AddHeader(args.Page, 0, kiri);




            }
            previousPage = args.Page;
        }
        #endregion


    }
}
