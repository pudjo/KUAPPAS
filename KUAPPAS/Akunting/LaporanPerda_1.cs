using DTO;
using Formatting;

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using BP;
using KUAPPAS.DataAccess9.DTO.Akuntansi;
using BP.Akuntansi;
using DTO.Akuntansi;


namespace KUAPPAS.Akunting
{
    public class LaporanPerdaI:XLaporan
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
                PerdaRealisasi_1_1Logic oLogic = new PerdaRealisasi_1_1Logic(p.Tahun);
                List<PerdaRealisasi_1> lst = new List<PerdaRealisasi_1>();
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
        private bool CetakPerda(List<PerdaRealisasi_1>lst, ParameterLaporan pl)
        {
            mmrd = new ReportDesign();
            mmrd = pl.Rd;
            
            
            
            
            try
            {
                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                section1.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section1.PageSettings.Height = 950;// = PdfPageSize.Legal;
                section1.PageSettings.Orientation = PdfPageOrientation.Portrait;
                document.PageSettings.Margins.Left = 0;
                document.PageSettings.Margins.Bottom = 0;
                document.PageSettings.Margins.Right = 0;
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
                int posisisurplusdeisit = 0;
                foreach (PerdaRealisasi_1  r in lst)
                {
                    PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                    if (r.Nama.Contains("SUPLUS/DEFISIT") == false)
                    {
                        pdfGridRow.Cells[0].Value = r.Kode;
                        pdfGridRow.Cells[1].Value = r.KodeUrusan.Length > 2 ? r.KodeUrusan.Substring(1, 2) : "";
                        pdfGridRow.Cells[2].Value = r.KodeSKPD;
                        pdfGridRow.Cells[3].Value = r.Nama;

                        pdfGridRow.Cells[4].Value = r.Anggaran.ToRupiahInReport();
                        pdfGridRow.Cells[5].Value = r.Realisasi.ToRupiahInReport();

                        pdfGridRow.Cells[6].Value = r.Selisih.ToRupiahInReport();
                        pdfGridRow.Cells[7].Value = r.Persen;
                    }
                    else
                    {
                        pdfGridRow.Cells[0].Value = "";
                        pdfGridRow.Cells[1].Value = "";
                        pdfGridRow.Cells[2].Value = "";
                        pdfGridRow.Cells[3].Value = r.Nama;

                        pdfGridRow.Cells[4].Value = r.Anggaran.ToRupiahInReport();
                        pdfGridRow.Cells[5].Value = r.Realisasi.ToRupiahInReport();

                        pdfGridRow.Cells[6].Value = "";
                        pdfGridRow.Cells[7].Value = "";
                        posisisurplusdeisit = lst.IndexOf(r);
                    }
                    
                }
                pdfGrid.Columns[0].Width = 20;
                pdfGrid.Columns[1].Width = 20;
                pdfGrid.Columns[2].Width = 70;
                pdfGrid.Columns[3].Width = 180;
                pdfGrid.Columns[4].Width = 70;
                pdfGrid.Columns[5].Width = 70;
                pdfGrid.Columns[6].Width = 70;//9933
                pdfGrid.Columns[7].Width = 30;

                pdfGrid.Rows[posisisurplusdeisit].Cells[0].ColumnSpan = 4;





                PdfGridStyle gridStyle = new PdfGridStyle();
                gridStyle.CellPadding = new PdfPaddings(2, mmrd.Spasi, 2, mmrd.Spasi);
                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGrid.Columns[4].Format = formatKolomAngka;
                pdfGrid.Columns[5].Format = formatKolomAngka;
                pdfGrid.Columns[6].Format = formatKolomAngka;

                formatKolomAngka.Alignment = PdfTextAlignment.Center;
                pdfGrid.Columns[7].Format = formatKolomAngka;
                
                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 6));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();

                for (int id = 0; id < pdfGrid.Rows.Count; id++)
                {
                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
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
                        pdfGrid.Rows[id].Cells[c].Style.Borders.Bottom.Width = 0.01F;
                        pdfGrid.Rows[id].Cells[c].Style.Borders.Top.Width = 0.01F;
                        pdfGrid.Rows[id].Cells[c].Style.Borders.Left.Width = 0.01F;
                        pdfGrid.Rows[id].Cells[c].Style.Borders.Right.Width = 0.01F;
                        if (c >= 4)
                        {
                            pdfGrid.Rows[id].Cells[c].Style.StringFormat = new PdfStringFormat(PdfTextAlignment.Right,
                                PdfVerticalAlignment.Middle);
                        }
                        else
                        {

                            pdfGrid.Rows[id].Cells[c].Style.StringFormat = new PdfStringFormat(PdfTextAlignment.Left,
                                PdfVerticalAlignment.Middle);
                        }


                    }
                }
                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos));
                yPos = pdfGridLayoutResult.Bounds.Bottom;
                #endregion
                SaatnyacetakKesimpulan = true;
                m_kiri = kiri;
                PosisiTerakhir = yPos;

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
        #region cetakJudul
        private bool TulisJudulLaporan(PdfGrid pdfGrid)
        {

            try
            {

                pdfGrid.Columns.Add(8);
                pdfGrid.Headers.Add(3);


                PdfGridRow pdfGridHeader = pdfGrid.Headers[0];



                pdfGridHeader.Cells[0].Value = "Kode";
                pdfGridHeader.Cells[1].Value = "";
                pdfGridHeader.Cells[2].Value = "";
                pdfGridHeader.Cells[3].Value = "Urusan Pemerintah Daerah";
                pdfGridHeader.Cells[4].Value = "Jumlah (Rp)";
                pdfGridHeader.Cells[5].Value = "Jumlah (Rp)";
                pdfGridHeader.Cells[6].Value = "Bartambah/(Berkurang)";
                pdfGridHeader.Cells[7].Value = "Bartambah/(Berkurang)";

                PdfGridRow pdfGridHeader1 = pdfGrid.Headers[1];

                pdfGridHeader1.Cells[0].Value = "Kode";
                pdfGridHeader1.Cells[1].Value = "";
                pdfGridHeader1.Cells[2].Value = "";
                pdfGridHeader1.Cells[3].Value = "Urusan Pemerintah Daerah";
                pdfGridHeader1.Cells[4].Value = "Jumlah (Rp)";
                pdfGridHeader1.Cells[5].Value = "Jumlah (Rp)";
                pdfGridHeader1.Cells[6].Value = "Bartambah/(Berkurang)";
                pdfGridHeader1.Cells[7].Value = "Bartambah/(Berkurang)";

                PdfGridRow pdfGridHeader2 = pdfGrid.Headers[2];
                pdfGridHeader2.Cells[0].Value = "Kode";
                pdfGridHeader2.Cells[1].Value = "";
                pdfGridHeader2.Cells[2].Value = "";
                pdfGridHeader2.Cells[3].Value = "Urusan Pemerintah Daerah";
                pdfGridHeader2.Cells[4].Value = "Anggaran";
                pdfGridHeader2.Cells[5].Value = "Tealisasi";
                pdfGridHeader2.Cells[6].Value = "Rp";
                pdfGridHeader2.Cells[7].Value = "%";


                #region span

                pdfGridHeader.Cells[0].ColumnSpan = 3;
                pdfGridHeader1.Cells[0].ColumnSpan = 3;
                pdfGridHeader2.Cells[0].ColumnSpan = 3;

                pdfGridHeader.Cells[0].RowSpan = 3;
                pdfGridHeader.Cells[1].RowSpan = 3;
                pdfGridHeader.Cells[2].RowSpan = 3;
                pdfGridHeader.Cells[3].RowSpan = 3;

                pdfGridHeader.Cells[4].ColumnSpan = 2;
                pdfGridHeader.Cells[4].RowSpan = 2;
                pdfGridHeader.Cells[5].RowSpan = 2;
                pdfGridHeader.Cells[6].ColumnSpan = 2;
                pdfGridHeader.Cells[6].RowSpan = 2;
                pdfGridHeader.Cells[7].RowSpan = 2;
                #endregion

                PdfFont fontHeader = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 6, FontStyle.Bold));
                PdfGridCellStyle cellHeaderStyle0 = new PdfGridCellStyle();
                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;
                pdfGridHeader.Height = 15;
                pdfGridHeader1.Height = 15;
                pdfGridHeader2.Height = 15;
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
                    //lebar = lebar + pdfGridHeader.Columns[col].Width;
                    //pdfGridHeader.Columns[col].Format = stringFormatHeader0;
                    pdfGridHeader1.Cells[col].Style.StringFormat = stringFormatHeader; // new PdfStringFormat(PdfTextAlignment.Center);
                    pdfGridHeader1.Cells[col].Style.Font = fontHeader;

                    pdfGridHeader1.Cells[col].Style.Borders.Bottom.Width = 0.02F;
                    pdfGridHeader1.Cells[col].Style.Borders.Top.Width = 0.02F;
                    pdfGridHeader1.Cells[col].Style.Borders.Right.Width = 0.02F;
                    pdfGridHeader1.Cells[col].Style.Borders.Left.Width = 0.02F;
                }




                for (int col = 0; col < pdfGridHeader2.Cells.Count; col++)
                {


                    pdfGridHeader2.Cells[col].Style.StringFormat = stringFormatHeader; // new PdfStringFormat(PdfTextAlignment.Center);
                    pdfGridHeader2.Cells[col].Style.Font = fontHeader;

                    pdfGridHeader2.Cells[col].Style.Borders.Bottom.Width = 0.02F;
                    pdfGridHeader2.Cells[col].Style.Borders.Top.Width = 0.02F;
                    pdfGridHeader2.Cells[col].Style.Borders.Right.Width = 0.02F;
                    pdfGridHeader2.Cells[col].Style.Borders.Left.Width = 0.02F;

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
