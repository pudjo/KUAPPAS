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
    public class LaporanPerda_SPM : XLaporan
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


                LaporanAPMLogic ologic = new LaporanAPMLogic();

                List<PerdaSPM> lst = ologic.GetPerdaSPM050(p);



                mpl = new ParameterLaporan();
                mpl = p;



                return CetakPerda(lst, p);






            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;


            }
        }
        #region cetakPerda
        private bool CetakPerda(List <PerdaSPM>  lst, ParameterLaporan pl)
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

                yPos = 10;

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
                if (lst == null)
                {
                    Error = "Tidak menemukan data";
                    return false;
                }
                int posisisurplusdeisit = 0;
                foreach (PerdaSPM f in lst)
                {
                    PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                    pdfGridRow.Cells[0].Value = f.Anggaran ==1? f.No.ToString():"";
                    pdfGridRow.Cells[1].Value = f.Nama;
                    pdfGridRow.Cells[2].Value = f.NamaKegiatan;
                    pdfGridRow.Cells[3].Value = f.Anggaran>1?f.Anggaran.ToRupiahInReport():"";
                    pdfGridRow.Cells[4].Value = f.Anggaran > 1 ?  f.Realisasi.ToRupiahInReport():"";
                    

                }
                pdfGrid.Columns[0].Width = 20;
                pdfGrid.Columns[1].Width = 180;
                pdfGrid.Columns[2].Width = 180;

                pdfGrid.Columns[3].Width = 75;
                pdfGrid.Columns[4].Width = 75;
                



                PdfGridStyle gridStyle = new PdfGridStyle();
                gridStyle.CellPadding = new PdfPaddings(2, mmrd.Spasi, 2, mmrd.Spasi);
                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                formatKolomAngka.Alignment = PdfTextAlignment.Center;
                
                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 6));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                

                for (int id = 0; id < pdfGrid.Rows.Count; id++)
                {
                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                     
                        pdfGrid.Rows[id].Cells[c].Style.Borders.Bottom.Width = 0.01F;
                        pdfGrid.Rows[id].Cells[c].Style.Borders.Top.Width = 0.01F;
                        pdfGrid.Rows[id].Cells[c].Style.Borders.Left.Width = 0.01F;
                        pdfGrid.Rows[id].Cells[c].Style.Borders.Right.Width = 0.01F;
                        if (c >= 3)
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

                pdfGrid.Columns.Add(5);
                pdfGrid.Headers.Add(1);


                PdfGridRow pdfGridHeader = pdfGrid.Headers[0];



                pdfGridHeader.Cells[0].Value = "No";
                pdfGridHeader.Cells[1].Value = "Jenis Pelayanan Dasar";
                pdfGridHeader.Cells[2].Value = "Kegiatan";
                pdfGridHeader.Cells[3].Value = "Anggaran";
                pdfGridHeader.Cells[4].Value = "Realisasi";
                

                PdfFont fontHeader = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 6, FontStyle.Bold));
                PdfGridCellStyle cellHeaderStyle0 = new PdfGridCellStyle();
                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;
                pdfGridHeader.Height = 15;
                for (int col = 0; col < pdfGridHeader.Cells.Count; col++)
                {
                    pdfGridHeader.Cells[col].Style.StringFormat = stringFormatHeader; // new PdfStringFormat(PdfTextAlignment.Center);
                    pdfGridHeader.Cells[col].Style.Font = fontHeader;
                    pdfGridHeader.Cells[col].Style.Borders.Bottom.Width = 0.02F;

                    pdfGridHeader.Cells[col].Style.Borders.Top.Width = 0.02F;
                    pdfGridHeader.Cells[col].Style.Borders.Right.Width = 0.02F;
                    pdfGridHeader.Cells[col].Style.Borders.Left.Width = 0.02F;


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