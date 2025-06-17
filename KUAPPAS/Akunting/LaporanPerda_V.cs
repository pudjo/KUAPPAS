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
    public class  LaporanPerda_V : XLaporan
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
                PerdaRealisasiVLogic oLogic = new PerdaRealisasiVLogic(p.Tahun);
                
                mpl = new ParameterLaporan();
                mpl = p;
                List<PerdaFungsi> lst = new List<PerdaFungsi>();
                lst = oLogic.GetPerdaVRealisasi050(p);



                return CetakPerda(lst, p);






            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;


            }
        }
        #region cetakPerda
        private bool CetakPerda(List <PerdaFungsi>  lst, ParameterLaporan pl)
        {
            mmrd = new ReportDesign();
            mmrd = pl.Rd;




            try
            {
                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                section1.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section1.PageSettings.Height = 950;// = PdfPageSize.Legal;
                section1.PageSettings.Orientation = PdfPageOrientation.Landscape;
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
                foreach (PerdaFungsi f in lst)
                {
                    PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                    pdfGridRow.Cells[0].Value = f.KodeFungsi.ToString();
                    pdfGridRow.Cells[1].Value = f.KodeSubFungsi>0? f.KodeSubFungsi.ToString():"";
                    pdfGridRow.Cells[2].Value = f.KodeKategori>0? f.KodeKategori.ToString():"";
                    pdfGridRow.Cells[3].Value = f.KodeUrusan>0? f.KodeUrusan.ToString():"";
                    pdfGridRow.Cells[4].Value = f.Nama;
                    pdfGridRow.Cells[5].Value = f.AnggaranOperasi.ToRupiahInReport();
                    pdfGridRow.Cells[6].Value = f.RealisasiOperasi.ToRupiahInReport(); ;
                    pdfGridRow.Cells[7].Value = f.AnggaranModal.ToRupiahInReport();
                    pdfGridRow.Cells[8].Value = f.RealisasiModal.ToRupiahInReport();
                    pdfGridRow.Cells[9].Value = f.AnggaranTakTerduga.ToRupiahInReport();
                    pdfGridRow.Cells[10].Value = f.RealisasiTakTerduga.ToRupiahInReport();
                    pdfGridRow.Cells[11].Value = f.AnggaranTransfer.ToRupiahInReport();
                    pdfGridRow.Cells[12].Value = f.RealisasiTransfer.ToRupiahInReport();


                }
                pdfGrid.Columns[0].Width = 20;
                pdfGrid.Columns[1].Width = 20;
                pdfGrid.Columns[2].Width = 20;

                pdfGrid.Columns[3].Width = 20;
                pdfGrid.Columns[4].Width = 150;
                pdfGrid.Columns[5].Width = 85;
                pdfGrid.Columns[6].Width = 85;//9933
                pdfGrid.Columns[7].Width = 80;//9933
                pdfGrid.Columns[8].Width = 80;//9933
                pdfGrid.Columns[9].Width = 75;//9933
                pdfGrid.Columns[10].Width = 75;//9933
                pdfGrid.Columns[11].Width = 80;//9933
                pdfGrid.Columns[12].Width = 80;//9933
                




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
                        if (c > 4)
                        {
                            pdfGrid.Rows[id].Cells[c].Style.StringFormat = new PdfStringFormat(PdfTextAlignment.Right,
                                PdfVerticalAlignment.Middle);
                        }
                        else
                        {
                            if (c == 4)
                            {
                                pdfGrid.Rows[id].Cells[c].Style.StringFormat = new PdfStringFormat(PdfTextAlignment.Left,
                                    PdfVerticalAlignment.Middle);
                            }
                            else
                            {
                                pdfGrid.Rows[id].Cells[c].Style.StringFormat = new PdfStringFormat(PdfTextAlignment.Center,
                                    PdfVerticalAlignment.Middle);
                            }
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

                pdfGrid.Columns.Add(13);
                pdfGrid.Headers.Add(3);


                PdfGridRow pdfGridHeader = pdfGrid.Headers[0];



                pdfGridHeader.Cells[0].Value = "Kode";
                pdfGridHeader.Cells[1].Value = "Kode";
                pdfGridHeader.Cells[2].Value = "Kode";
                pdfGridHeader.Cells[3].Value = "Kode";
                pdfGridHeader.Cells[4].Value = "Uraian";
                pdfGridHeader.Cells[5].Value = "Kelompok Belanja";
                pdfGridHeader.Cells[6].Value = "Kelompok Belanja";
                pdfGridHeader.Cells[7].Value = "Kelompok Belanja";
                pdfGridHeader.Cells[8].Value = "Kelompok Belanja";
                pdfGridHeader.Cells[9].Value = "Kelompok Belanja";
                pdfGridHeader.Cells[10].Value = "Kelompok Belanja";
                pdfGridHeader.Cells[11].Value = "Kelompok Belanja";
                pdfGridHeader.Cells[12].Value = "Kelompok Belanja";


                PdfGridRow pdfGridHeader1 = pdfGrid.Headers[1];

                pdfGridHeader1.Cells[0].Value = "Kode";
                pdfGridHeader1.Cells[1].Value = "Kode";
                pdfGridHeader1.Cells[2].Value = "Kode";
                pdfGridHeader1.Cells[3].Value = "Kode";
                pdfGridHeader1.Cells[4].Value = "Uraian";
                pdfGridHeader1.Cells[5].Value = "Operasi";
                pdfGridHeader1.Cells[6].Value = "Operasi";
                pdfGridHeader1.Cells[7].Value = "Modal";
                pdfGridHeader1.Cells[8].Value = "Modal";
                pdfGridHeader1.Cells[9].Value = "Tak Terduga";
                pdfGridHeader1.Cells[10].Value = "Tak Terduga";
                pdfGridHeader1.Cells[11].Value = "Transfer";
                pdfGridHeader1.Cells[12].Value = "Transfer";

                PdfGridRow pdfGridHeader2 = pdfGrid.Headers[2];

                pdfGridHeader2.Cells[0].Value = "Kode";
                pdfGridHeader2.Cells[1].Value = "Kode";
                pdfGridHeader2.Cells[2].Value = "Kode";
                pdfGridHeader2.Cells[3].Value = "Kode";
                pdfGridHeader2.Cells[4].Value = "Uraian";
                pdfGridHeader2.Cells[5].Value = "Anggaran";
                pdfGridHeader2.Cells[6].Value = "Realisasi";
                pdfGridHeader2.Cells[7].Value = "Anggaran";
                pdfGridHeader2.Cells[8].Value = "Realisasi";
                pdfGridHeader2.Cells[9].Value = "Anggaran";
                pdfGridHeader2.Cells[10].Value = "Realisasi";
                pdfGridHeader2.Cells[11].Value = "Anggaran";
                pdfGridHeader2.Cells[12].Value = "Realisasi";

                #region span

                pdfGridHeader.Cells[0].ColumnSpan = 4;
                pdfGridHeader1.Cells[0].ColumnSpan = 4;
                pdfGridHeader2.Cells[0].ColumnSpan = 4;
                pdfGridHeader.Cells[0].RowSpan = 3;
                
                pdfGridHeader.Cells[4].RowSpan = 3;
                
                pdfGridHeader.Cells[5].ColumnSpan = 8;
                pdfGridHeader1.Cells[5].ColumnSpan = 2;
                pdfGridHeader1.Cells[7].ColumnSpan = 2;
                pdfGridHeader1.Cells[9].ColumnSpan = 2;
                pdfGridHeader1.Cells[11].ColumnSpan = 2;

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