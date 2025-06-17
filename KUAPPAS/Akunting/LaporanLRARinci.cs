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
using KUAPPAS.DTO.Akuntansi;


namespace KUAPPAS.Akunting
{
    public class LaporanLRARinci : XLaporan
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
                PerkdaRealisasiRinciLogic oLogic = new PerkdaRealisasiRinciLogic(p.Tahun);
                List<RealisasiRinci> lst = new List<RealisasiRinci>();
                mpl = new ParameterLaporan();
                mpl = p;
                lst = oLogic.Process();
                return CetakPerda(lst,  p);       
        




                
            } catch(Exception ex)
            {
                Error = ex.Message;
                return false;


            }
        }
        #region cetakPerda
        private bool CetakPerda(List<RealisasiRinci> lst, ParameterLaporan pl)
        {
            mmrd = new ReportDesign();
            mmrd = pl.Rd;
            
            
            
            
            try
            {
                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                section1.PageSettings.Width = 650;// = PdfPageSize.Legal;
                section1.PageSettings.Height = 1000;// = PdfPageSize.Legal;
                section1.PageSettings.Orientation = PdfPageOrientation.Landscape;
                document.PageSettings.Margins.Left = 0;
                document.PageSettings.Margins.Bottom = 0;
                document.PageSettings.Margins.Right = 0;

                PdfPage page = section1.Pages.Add();
                
                previousPage = page;
                document.Pages.PageAdded += Perda_I_PageAdded;
                SaatnyacetakKesimpulan = false;

                CetakPDF oCetakPDF = new CetakPDF();
                float yPos=10;
                float kiri = 0;
                

                yPos = TulisJudulUtamaLaporan(oCetakPDF, previousPage, pl, yPos, kiri);
                if (yPos == 0)
                {
                    return false;
                }

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
                foreach (RealisasiRinci r in lst)
                {
                    PdfGridRow pdfGridRow = pdfGrid.Rows.Add();

                    pdfGridRow.Cells[0].Value = r.NamaUrusan;
                    pdfGridRow.Cells[1].Value = r.NamaUrusanBidang;
                    pdfGridRow.Cells[2].Value = r.NamaDinas;

                    pdfGridRow.Cells[3].Value = r.NamaUnitDinas==""? r.NamaDinas: r.NamaUnitDinas;
                    pdfGridRow.Cells[4].Value = r.NamaProgram;
                    pdfGridRow.Cells[5].Value = r.NamaKegiatan;
                    pdfGridRow.Cells[6].Value = r.NamaSubKegiatan;
                    pdfGridRow.Cells[7].Value = r.NamaAkun;
                    pdfGridRow.Cells[8].Value = r.NamaKelompok;
                    pdfGridRow.Cells[9].Value = r.NamaJenis;
                    pdfGridRow.Cells[10].Value = r.NamaObject;
                    pdfGridRow.Cells[11].Value = r.NamaRincianObject;
                    pdfGridRow.Cells[12].Value = r.NamaSubRincianObject;
                    pdfGridRow.Cells[13].Value = r.Anggaran.ToRupiahInReport();
                    pdfGridRow.Cells[14].Value = r.Realisasi.ToRupiahInReport();


                }
                pdfGrid.Columns[0].Width = 60;
                pdfGrid.Columns[1].Width = 60;
                pdfGrid.Columns[2].Width = 60;
                pdfGrid.Columns[3].Width = 60;
                pdfGrid.Columns[4].Width = 60;
                pdfGrid.Columns[5].Width = 60;
                pdfGrid.Columns[6].Width = 60;
                pdfGrid.Columns[7].Width = 60;
                pdfGrid.Columns[8].Width = 60;
                pdfGrid.Columns[9].Width = 60;
                pdfGrid.Columns[10].Width =60;
                pdfGrid.Columns[11].Width = 60;
                pdfGrid.Columns[12].Width = 60;
                pdfGrid.Columns[13].Width = 50;
                pdfGrid.Columns[14].Width = 50;







                PdfGridStyle gridStyle = new PdfGridStyle();
                gridStyle.CellPadding = new PdfPaddings(2, mmrd.Spasi, 2, mmrd.Spasi);
                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                ////pdfGrid.Columns[4].Format = formatKolomAngka;
                ////pdfGrid.Columns[5].Format = formatKolomAngka;
                ////pdfGrid.Columns[6].Format = formatKolomAngka;

                ////formatKolomAngka.Alignment = PdfTextAlignment.Center;
                ////pdfGrid.Columns[7].Format = formatKolomAngka;
                
                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 6));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                
                cellStyle.TextBrush = PdfBrushes.Blue;
                PdfStringFormat format = new PdfStringFormat();
                format.Alignment = PdfTextAlignment.Center;
                //Set string format to grid cell.
                PdfBorders borders = new PdfBorders();
                borders.All = PdfPens.White;
                cellStyle.Borders = borders;
                cellStyle.CellPadding = new PdfPaddings(15, 5, 5, 5);
                cellStyle.StringFormat = format;

                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();

                for (int id = 0; id < pdfGrid.Rows.Count; id++)
                {


                    pdfGrid.Rows[id].Cells[1].Style.CellPadding = new PdfPaddings(5, mmrd.Spasi, 5, mmrd.Spasi);
                                        
                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        
                           pdfGrid.Rows[id].Cells[c].Style.Borders.Bottom.Width = 0.01F;
                            pdfGrid.Rows[id].Cells[c].Style.Borders.Top.Width = 0.01F;
                            pdfGrid.Rows[id].Cells[c].Style.Borders.Left.Width = 0.01F;
                            pdfGrid.Rows[id].Cells[c].Style.Borders.Right.Width = 0.01F;

                        pdfGrid.Rows[id].Cells[c].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 6));
                        if (c==13 || c == 14 )
                        {
                            pdfGrid.Rows[id].Cells[c].Style.StringFormat = new PdfStringFormat(PdfTextAlignment.Right,
                                PdfVerticalAlignment.Middle);
                        }
                        else{
                            pdfGrid.Rows[id].Cells[c].Style.StringFormat = new PdfStringFormat(PdfTextAlignment.Left,
                                PdfVerticalAlignment.Middle);
                        }

                        
                    }
                }

                PdfGridLayoutFormat fmt = new PdfGridLayoutFormat();
                fmt.Break = PdfLayoutBreakType.FitPage;
                fmt.PaginateBounds = new RectangleF(0, 0, page.Size.Width - 0, page.Size.Height - 120);


                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos), fmt);
                yPos = pdfGridLayoutResult.Bounds.Bottom;
                #endregion
                SaatnyacetakKesimpulan = true;
                m_kiri = kiri;
                PosisiTerakhir = yPos;
                AddFooter(document, pl.AwalHalaman);

                page = document.Pages.Add();


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

                template.Graphics.DrawString("Halaman " + value, font, brush, new PointF(420, 280));
                doc.Pages[i].Graphics.DrawPdfTemplate(template, new PointF(420, 280));
                //600
                offset += 1;
            }
        }
        #region cetakJudul
        private bool TulisJudulLaporan(PdfGrid pdfGrid)
        {

            try
            {

                pdfGrid.Columns.Add(15);
                pdfGrid.Headers.Add(2);


                PdfGridRow pdfGridHeader = pdfGrid.Headers[0];



                pdfGridHeader.Cells[0].Value = "Nama Urusan";
                pdfGridHeader.Cells[1].Value = "Bidang Urusan";
                pdfGridHeader.Cells[2].Value = "SKPD";
                pdfGridHeader.Cells[3].Value = "Unit SKPD";
                pdfGridHeader.Cells[4].Value = "Program";

                pdfGridHeader.Cells[5].Value = "Kegiatan";
                pdfGridHeader.Cells[6].Value = "Sub Kegiatan";
                
                pdfGridHeader.Cells[7].Value = "Akun";
                pdfGridHeader.Cells[8].Value = "Kelompok";
                pdfGridHeader.Cells[9].Value = "Jenis";
                pdfGridHeader.Cells[10].Value = "Objek";
                pdfGridHeader.Cells[11].Value = "Rincian Objek";
                pdfGridHeader.Cells[12].Value = "Sub Rincian Objek";
                pdfGridHeader.Cells[13].Value = "Anggaran";
                pdfGridHeader.Cells[14].Value = "Realisasi";

                PdfGridRow pdfGridHeader1 = pdfGrid.Headers[1];
                pdfGridHeader1.Cells[0].Value = "Nama Urusan";
                pdfGridHeader1.Cells[1].Value = "Bidang Urusan";
                pdfGridHeader1.Cells[2].Value = "SKPD";
                pdfGridHeader1.Cells[3].Value = "Unit SKPD";
                pdfGridHeader1.Cells[4].Value = "Program";

                pdfGridHeader1.Cells[5].Value = "Kegiatan";
                pdfGridHeader1.Cells[6].Value = "Sub Kegiatan";

                pdfGridHeader1.Cells[7].Value = "Akun";
                pdfGridHeader1.Cells[8].Value = "Kelompok";
                pdfGridHeader1.Cells[9].Value = "Jenis";
                pdfGridHeader1.Cells[10].Value = "Objek";
                pdfGridHeader1.Cells[11].Value = "Rincian Objek";
                pdfGridHeader1.Cells[12].Value = "Sub Rincian Objek";
                pdfGridHeader1.Cells[13].Value = "Anggaran";
                pdfGridHeader1.Cells[14].Value = "Realisasi";

                #region span

                for (int c=0; c <= 14; c++)
                {
                    pdfGridHeader.Cells[c].RowSpan = 2;
                }
                
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
