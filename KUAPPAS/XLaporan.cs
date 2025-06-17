using BP;
using DTO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUAPPAS
{
    public class XLaporan
    {
        Single lebar;
        Single m_kiri;
        string msNamaSKPD;
        int mTahun;
        bool SaatnyaCetakKesimpulan;
        float PosisiTerakhir;
        CetakPDF oCetakPDF;
        PdfPage previousPage;
        private string sError;
        private long dinas;
        Pejabat Pimpinan = new Pejabat();
        Pejabat PenggunaBarang = new Pejabat();
        Pejabat PengelolaAset = new Pejabat();

        string sPesan;
        public int m_halaman;
        public DateTime TanggalCetak;
        public string Tempat;


        public long Dinas
        {
            set { dinas = value; }
            get { return dinas; }

        }
        public string Pesan
        {
            set
            {
                sPesan = value;
            }
            get
            {
                return sPesan + "";
            }
        }
        public string Error
        {
            set
            {
                sError = value;
            }
            get
            {
                return sError;
            }
        }
        public Pemda GetPemda()
        {
            try
            {
                PemdaLogic oLogicPemdaLogic = new PemdaLogic((int)GlobalVar.TahunAnggaran);
                Pemda oPemda = new Pemda();
                oPemda = oLogicPemdaLogic.Get();
                return oPemda;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }

        }
        public Single TulisNamaLaporan(CetakPDF pCetakPDF,PdfPage page, ParameterLaporan p )
        {
            try
            {
                Single yPos = 0;
                Single kiri;
                Single xtitikdua;
                Single xisi;
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Left;
                Single lebar = page.GetClientSize().Width;
                Single besarFont = 7;
                kiri = lebar - (lebar / 3);
                xtitikdua = kiri + 55;
                xisi = xtitikdua + 5;
                
                yPos = pCetakPDF.TulisItem(page.Graphics, p.NamaLaporan, besarFont, kiri, yPos,
                       50, stringFormat, false, false, true);
                yPos = pCetakPDF.TulisItem(page.Graphics, ":", besarFont, xtitikdua, yPos,
                       10, stringFormat, false, false, true);
                yPos = pCetakPDF.TulisItem(page.Graphics, p.Keterangan, besarFont, xisi, yPos,
                       180, stringFormat, true, false, true);
                yPos = yPos - 2;
                yPos = pCetakPDF.TulisItem(page.Graphics, "Nomor", besarFont, kiri, yPos,
                       50, stringFormat, false, false, true);
                yPos = pCetakPDF.TulisItem(page.Graphics, ":", besarFont, xtitikdua, yPos,
                       10, stringFormat, false, false, true);
                 
                    yPos = pCetakPDF.TulisItem(page.Graphics, p.Nomor, besarFont, xisi, yPos,
                           180, stringFormat, true, false, true);
                yPos = yPos - 2;

                yPos = pCetakPDF.TulisItem(page.Graphics, "Tanggal", besarFont, kiri, yPos,
                       50, stringFormat, false, false, true);
                yPos = pCetakPDF.TulisItem(page.Graphics, ":", besarFont, xtitikdua, yPos,
                       10, stringFormat, false, false, true);
                yPos = pCetakPDF.TulisItem(page.Graphics, p.Tanggal, besarFont, xisi, yPos,
                       180, stringFormat, true, false, true);

                return yPos;

            }catch(Exception ex)
            {
                Error = ex.Message;
                return 0;
            }
        }
        public Single TulisJudulUtamaLaporan(CetakPDF pCetakPDF, PdfPage page, ParameterLaporan p, Single yPos, Single kiri)
        {
            try
            {
                ReportDesign rd = p.Rd;
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;

                PdfStringFormat stringFormatLeft = new PdfStringFormat();
                stringFormatLeft.Alignment = PdfTextAlignment.Left;


                if (rd.Judul1.Length > 0)
                {
                    yPos = pCetakPDF.TulisItem(page.Graphics, rd.Judul1, 10, kiri, yPos,
                       page.GetClientSize().Width, stringFormat, true, false, true);

                }
                if (rd.Judul2.Length > 0)
                {
                    yPos = pCetakPDF.TulisItem(page.Graphics, rd.Judul2
                    , 8, kiri, yPos,
                     page.GetClientSize().Width, stringFormat, true, false, true);
                }
                if (rd.Judul3.Length > 0)
                {
                    yPos = pCetakPDF.TulisItem(page.Graphics, rd.Judul3
                    , 8, kiri, yPos,
                     page.GetClientSize().Width, stringFormat, true, false, true);
                }
                
                return yPos;
            } catch(Exception ex)
            {
                Error = ex.Message;
                return 0;

            }
        }
        //public Pemda GetPerda()
        //{
        //    try
        //    {
        //        PerdaLogic oPerdaLogic = new PerdaLogic((int)GlobalVar.TahunAnggaran);
        //        Perda oPerda = new Perda();
        //        oPerda = oPerdaLogic.Get
        //        return oPemda;
        //    }
        //    catch (Exception ex)
        //    {
        //        Error = ex.Message;
        //        return null;
        //    }

        //}


    }
}
