using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using BP.Bendahara;
using DTO;
using DTO.Bendahara;
using Formatting;
using Excel = Microsoft.Office.Interop.Excel;
using DTO.Anggaran;
using Syncfusion.Pdf;
using BP.Anggaran;

using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System.IO;
using Bendahara;
using System.Diagnostics;

namespace KUAPPAS.Bendahara
{
    public partial class frmSPJFungsional : ChildForm
    {

        private List<ProgramKegiatanAnggaran> m_lstProgramKegiatan;
        private List<FungsionalRekening> mFungsionalList;
        List<FungsionalRekening> mListPenerimaanPengeluaran;
        private int m_iRowPerbandinganSPJLRA = -1;

        string[] satuan = new string[10] { "nol", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan" };
        string[] belasan = new string[10] { "sepuluh", "sebelas", "dua belas", "tiga belas", "empat belas", "lima belas", "enam belas", "tujuh belas", "delapan belas", "sembilan belas" };
        string[] puluhan = new string[10] { "", "", "dua puluh", "tiga puluh", "empat puluh", "lima puluh", "enam puluh", "tujuh puluh", "delapan puluh", "sembilan puluh" };
        string[] ribuan = new string[5] { "", "ribu", "juta", "milyar", "triliyun" };

        string[] bulanpanjang = new string[12] { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };

        private int m_iJenis;
        private long m_NoUrut;
        private bool m_bNew;
        private int m_nMode;

        private int mcolAnggaran;
        private const int COL_ANGGARANMURNI = 2;
        private const int COL_ANGGARANGESER = 3;
        private const int COL_ANGGARANRKAP = 4;
        private const int COL_ANGGARANABT = 5;
        private const int COL_AKUMULASI = 6;

        private const int COL_SPJINI = 7;
        private const int COL_SISA = 8;

        private const int COL_LEVEL = 9;
        private const int COL_IDURUSAN = 10;
        private const int COL_IDPROGRAM = 11;
        private const int COL_IDKEGIATAN = 12;
        private const int COL_IDSubKegiatan = 13;
        private const int COL_IDREKENING = 14;
        private const int COL_IDDINAS = 0;
        private const int COL_KODEUK = 15;
        private const int COL_STATUSUPDATE = 16;

        private const int COL_GL = 17;
        private const int COL_GK = 18;
        private const int COL_GTot = 19;

        private const int COL_BL = 20;
        private const int COL_BK = 21;
        private const int COL_BTot = 22;
        private const int COL_UL = 23;
        private const int COL_UK = 24;
        private const int COL_UTot = 25;
        private const int COL_SPJTot = 26;
        private const int COL_SISASPJ = 27;



        private const int LEVEL_DINAS = 0;
        private const int LEVEL_UNIT = 1;
        private const int LEVEL_URUSAN = 2;
        private const int LEVEL_PROGRAM = 3;
        private const int LEVEL_KEGIATAN = 4;
        private const int LEVEL_SUBKEGIATAN = 5;
        private const int LEVEL_REKANING = 6;



        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _level2style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level3style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level4style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level5style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level6style = new DataGridViewCellStyle();

        DataGridViewCellStyle _level7style = new DataGridViewCellStyle();
        DataGridViewCellStyle _redstyle = new DataGridViewCellStyle();

        DataGridViewCellStyle _normalstyle = new DataGridViewCellStyle();

        FungsionalRekening fRJumlah;
        ProgramKegiatanAnggaran mAnggJumlah;
        private int prevJenis = 0;
        private int preDinas = 0;

        private int m_IDSKPD;
        private Single m_iStatus;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        PdfPage previousPage;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        CetakPDF m_oCetakPDF;
        int halaman;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;

        public frmSPJFungsional(int mode = 1)
        {
            InitializeComponent();
            m_IDSKPD = 0;
            m_nMode = mode;
            mcolAnggaran = 3;
            fRJumlah = new FungsionalRekening();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            gridSPJ.FormatHeader();

        }

        private void frmSPJFungsional_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            if (m_nMode == 1)
            {
                ctrlHeader1.SetCaption("SPJ Fungsional..");
            }
            else
            {
                ctrlHeader1.SetCaption("SPJ Administratif");

            }
            m_iRowPerbandinganSPJLRA = -1;

            gridSPJ.FormatHeader();
            gridPenerimaanPengeluaran.FormatHeader();
            ctrlTanggalBulan1.Create();
        }

        private void GetTanggal()
        {

            mTanggalAwal = ctrlTanggalBulan1.TanggalAwal;
            mTanggalAkhir = ctrlTanggalBulan1.TanggalAkhir;

        }
        private void LoadData()
        {
            try
            {

                mAnggJumlah = new ProgramKegiatanAnggaran(); ;
                if (LoadProgramKegiatan())
                {

                    if (DisplayProgramKegiatanSubKegiatan() == true)
                    {
                        DisplaySPJ();
                    }


                    FormatGrid();
                    // tampilkan jumlah di bawah
                    DisplayJumlah();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private int GetColAnggaran(){
            mcolAnggaran = ctrlTahapAnggaran1.GetID();
            switch (mcolAnggaran)
            {
                case 2:
                    return COL_ANGGARANMURNI;
                    

                case 3:
                    return COL_ANGGARANGESER;
                    
                    
                case 4:
                    
                    return COL_ANGGARANRKAP;
                    
                    
                case 5:
                    return COL_ANGGARANABT;
                    

            }
            return 2 + mcolAnggaran;

        }
        #region DisplayKolomAnggaran
        private void HideColumnAnggaran()
        {
            mcolAnggaran = ctrlTahapAnggaran1.GetID();
            gridSPJ.Columns[COL_ANGGARANMURNI].Visible = false;
            gridSPJ.Columns[COL_ANGGARANGESER].Visible = false;
            gridSPJ.Columns[COL_ANGGARANRKAP].Visible = false;
            gridSPJ.Columns[COL_ANGGARANABT].Visible = false;
            switch (mcolAnggaran)
            {
                case 2:
                    gridSPJ.Columns[COL_ANGGARANMURNI].Visible = true;
                    break;

                case 3:
                    gridSPJ.Columns[COL_ANGGARANGESER].Visible = true;
                    break;
                case 4:

                    gridSPJ.Columns[COL_ANGGARANRKAP].Visible = true;
                    break;
                case 5:
                    gridSPJ.Columns[COL_ANGGARANABT].Visible = true;
                    break;

            }




        }
        #endregion DisplayKolomAnggaran
        #region DisplaySPJ

        private void DisplayJumlah()
        {
            try
            {
                fRJumlah = new FungsionalRekening();
                for (int i = 0; i < gridSPJ.Rows.Count; i++)
                {

                    if (GetLevel(i) == LEVEL_REKANING)
                    {

                        fRJumlah.GL = fRJumlah.GL + DataFormat.FormatUangReportKeDecimal(gridSPJ.Rows[i].Cells[COL_GL].Value);

                        fRJumlah.GK = fRJumlah.GK + DataFormat.FormatUangReportKeDecimal(gridSPJ.Rows[i].Cells[COL_GK].Value);

                        fRJumlah.BL = fRJumlah.BL + DataFormat.FormatUangReportKeDecimal(gridSPJ.Rows[i].Cells[COL_BL].Value);
                        fRJumlah.BK = fRJumlah.BK + DataFormat.FormatUangReportKeDecimal(gridSPJ.Rows[i].Cells[COL_BK].Value);

                        fRJumlah.UL = fRJumlah.UL + DataFormat.FormatUangReportKeDecimal(gridSPJ.Rows[i].Cells[COL_UL].Value);
                        fRJumlah.UK = fRJumlah.UK + DataFormat.FormatUangReportKeDecimal(gridSPJ.Rows[i].Cells[COL_UK].Value);
                       Console.WriteLine((fRJumlah.GL+
                           fRJumlah.GK+
                           fRJumlah.BL+
                           fRJumlah.BK+
                           fRJumlah.UL +
                           fRJumlah.UK).ToString());
                    }
                }





                gridSPJ.Rows.Add();
                int idx = gridSPJ.Rows.Count - 2;
                gridSPJ.Rows[idx].Cells[COL_LEVEL].Value="0";
                gridSPJ.Rows[idx].Cells[COL_ANGGARANMURNI - 1].Value = "Jumlah";
                gridSPJ.Rows[idx].Cells[COL_ANGGARANMURNI].Value = mAnggJumlah.AnggaranMurni.ToRupiahInReport();
                gridSPJ.Rows[idx].Cells[COL_ANGGARANGESER].Value = mAnggJumlah.AnggaranGeser.ToRupiahInReport();
                gridSPJ.Rows[idx].Cells[COL_ANGGARANRKAP].Value = mAnggJumlah.AnggaranRKAP.ToRupiahInReport();
                gridSPJ.Rows[idx].Cells[COL_ANGGARANABT].Value = mAnggJumlah.AnggaranABT.ToRupiahInReport();


                gridSPJ.Rows[idx].Cells[COL_GL].Value = fRJumlah.GL.ToRupiahInReport();
                gridSPJ.Rows[idx].Cells[COL_GK].Value = fRJumlah.GK.ToRupiahInReport();
                gridSPJ.Rows[idx].Cells[COL_GTot].Value = (fRJumlah.GL + fRJumlah.GK).ToRupiahInReport();

                gridSPJ.Rows[idx].Cells[COL_BL].Value = fRJumlah.BL.ToRupiahInReport();
                gridSPJ.Rows[idx].Cells[COL_BK].Value = fRJumlah.BK.ToRupiahInReport();
                gridSPJ.Rows[idx].Cells[COL_BTot].Value = (fRJumlah.BK + fRJumlah.BL).ToRupiahInReport();
                gridSPJ.Rows[idx].Cells[COL_UL].Value = fRJumlah.UL.ToRupiahInReport();
                gridSPJ.Rows[idx].Cells[COL_UK].Value = fRJumlah.UK.ToRupiahInReport();
                gridSPJ.Rows[idx].Cells[COL_UTot].Value = (fRJumlah.UL + fRJumlah.UK).ToRupiahInReport();
                decimal total = fRJumlah.UL + fRJumlah.UK + fRJumlah.GL + fRJumlah.GK + fRJumlah.BL + fRJumlah.BK;
                gridSPJ.Rows[idx].Cells[COL_SPJTot].Value = total.ToRupiahInReport();
                decimal sisa = DataFormat.FormatUangReportKeDecimal(
                gridSPJ.Rows[idx].Cells[mcolAnggaran].Value.ToString()) - total;
                gridSPJ.Rows[idx].Cells[COL_SISASPJ].Value = sisa.ToRupiahInReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private bool DisplaySPJ()
        {
            try
            {
                m_IDSKPD = ctrlDinas1.GetID();
                //  mFungsionalList

                mFungsionalList = new List<FungsionalRekening>();
                SPJLogic oSPJLogic = new SPJLogic(GlobalVar.TahunAnggaran);
                mFungsionalList = oSPJLogic.GetFungsionalDariBKU(m_IDSKPD, mTanggalAwal, mTanggalAkhir);

                if (mFungsionalList != null)
                {
                    DisplayDetail();
                    DisplayPenerimaanPengeluaran();
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void DisplayPenerimaanPengeluaran()
        {
            try
            {
                SPJLogic oSPJLogic = new SPJLogic(GlobalVar.TahunAnggaran);
                mListPenerimaanPengeluaran = new List<FungsionalRekening>();
                mListPenerimaanPengeluaran = oSPJLogic.GetFungsionalPenerimaanPengeluarandariBKU(m_IDSKPD, mTanggalAwal, mTanggalAkhir);
                FungsionalRekening frTerima = new FungsionalRekening();
                FungsionalRekening frKeluar = new FungsionalRekening();



                if (mListPenerimaanPengeluaran != null)
                {
                    var lstJumlah = mListPenerimaanPengeluaran
                           .GroupBy(
                           c => new
                           {
                               c.IDDInas,
                               c.IDRekening,
                               c.Uraian

                           }
                          )
                          .Select(x => new
                           {
                               IDRekening = x.Key.IDRekening,
                               IDDInas = x.Key.IDDInas,
                               Uraian = x.Key.Uraian,
                               GL = x.Sum(y => y.GL),
                               GK = x.Sum(y => y.GK),
                               BL = x.Sum(y => y.BL),
                               BK = x.Sum(y => y.BK),
                               UL = x.Sum(y => y.UL),
                               UK = x.Sum(y => y.UK),
                           }).ToList();


                    gridPenerimaanPengeluaran.Rows.Clear();
                    int oldDinas = 0;
                    foreach (var o in lstJumlah)
                    {
                        if (o.IDDInas == 1 && oldDinas != o.IDDInas)
                        {


                            string[] row = { "", " PENERIMAAN:" };
                            gridPenerimaanPengeluaran.Rows.Add(row);
                            oldDinas = o.IDDInas;
                        }


                        if (o.IDDInas == 4 && oldDinas != o.IDDInas)
                        {
                            string[] rowj = {  "","Jumlah Penerimaan", frTerima.GL.ToRupiahInReport(), 

                                           frTerima.GK.ToRupiahInReport(),(frTerima.GL+frTerima.GK).ToRupiahInReport(),
                                           frTerima.BL.ToRupiahInReport(), frTerima.BK.ToRupiahInReport(),
                                           (frTerima.BL+ frTerima.BK).ToRupiahInReport(),
                                           frTerima.UL.ToRupiahInReport(), frTerima.UK.ToRupiahInReport(),
                                           (frTerima.UL+ frTerima.UK).ToRupiahInReport(),
                                           ( frTerima.GK+frTerima.GL+frTerima.BL + frTerima.BK+frTerima.UL+ frTerima.UK).ToRupiahInReport(),
                                       };

                            gridPenerimaanPengeluaran.Rows.Add(rowj);

                            rowj[0] = "";
                            rowj[1] = "";
                            rowj[2] = "";
                            rowj[3] = "";
                            rowj[4] = "";
                            rowj[5] = "";
                            rowj[6] = "";
                            rowj[7] = "";
                            rowj[8] = "";
                            rowj[9] = "";
                            rowj[10] = "";
                            rowj[11] = "";


                            gridPenerimaanPengeluaran.Rows.Add(rowj);

                            string[] row = { "", "PENGELUARAN:" };

                            gridPenerimaanPengeluaran.Rows.Add(row);
                            oldDinas = 7;
                        }
                        string[] rowx = {  o.IDRekening.ToString(), o.Uraian, o.GL.ToRupiahInReport(), 
                                           o.GK.ToRupiahInReport(),(o.GL+o.GK).ToRupiahInReport(),
                                           o.BL.ToRupiahInReport(), o.BK.ToRupiahInReport(),
                                           (o.BL+ o.BK).ToRupiahInReport(),
                                           o.UL.ToRupiahInReport(), o.UK.ToRupiahInReport(),
                                           (o.UL+ o.UK).ToRupiahInReport(),
                                           ( o.GK+o.GL+o.BL + o.BK+o.UL+ o.UK).ToRupiahInReport(),
                                       };
                        gridPenerimaanPengeluaran.Rows.Add(rowx);
                        if (o.IDDInas < 4)
                        {
                            frTerima.GL = frTerima.GL + o.GL;
                            frTerima.GK = frTerima.GK + o.GK;
                            frTerima.BL = frTerima.BL + o.BL;
                            frTerima.BK = frTerima.BK + o.BK;
                            frTerima.UL = frTerima.UL + o.UL;
                            frTerima.UK = frTerima.UK + o.UK;
                        }
                        else
                        {
                            frKeluar.GL = frKeluar.GL + o.GL;
                            frKeluar.GK = frKeluar.GK + o.GK;
                            frKeluar.BL = frKeluar.BL + o.BL;
                            frKeluar.BK = frKeluar.BK + o.BK;
                            frKeluar.UL = frKeluar.UL + o.UL;
                            frKeluar.UK = frKeluar.UK + o.UK;
                        }



                    }




                    string[] rowjk = {  "","Jumlah Pengeluaran", frKeluar.GL.ToRupiahInReport(), 
                                           frKeluar.GK.ToRupiahInReport(),(frKeluar.GL+frKeluar.GK).ToRupiahInReport(),
                                           frKeluar.BL.ToRupiahInReport(), frKeluar.BK.ToRupiahInReport(),
                                           (frKeluar.BL+ frKeluar.BK).ToRupiahInReport(),
                                           frKeluar.UL.ToRupiahInReport(), frKeluar.UK.ToRupiahInReport(),
                                           (frKeluar.UL+ frKeluar.UK).ToRupiahInReport(),
                                           ( frKeluar.GK+frKeluar.GL+frKeluar.BL + frKeluar.BK+frKeluar.UL+ frKeluar.UK).ToRupiahInReport(),
                                       };

                    gridPenerimaanPengeluaran.Rows.Add(rowjk);

                    string[] rowtotal = {  "","Sisa Saldo", (frTerima.GL- frKeluar.GL).ToRupiahInReport(), 
                                           (frTerima.GK- frKeluar.GK).ToRupiahInReport(),
                                           (frTerima.GL- frKeluar.GL+ frTerima.GK- frKeluar.GK).ToRupiahInReport(),
                                           (frTerima.BL- frKeluar.BL).ToRupiahInReport(), 
                                           ( frTerima.BK- frKeluar.BK).ToRupiahInReport(),
                                           (frTerima.BL- frKeluar.BL+ frTerima.BK- frKeluar.BK).ToRupiahInReport(),
                                           (frTerima.UL- frKeluar.UL).ToRupiahInReport(),
                                            (frTerima.UK- frKeluar.UK).ToRupiahInReport(),
                                           (frTerima.UL- frKeluar.UL+ frTerima.UK- frKeluar.UK).ToRupiahInReport(),
                                           ( frTerima.GK- frKeluar.GK+ frTerima.GL- frKeluar.GL+ 
                                           frTerima.BL- frKeluar.BL + frTerima.BK- frKeluar.BK +
                                           frTerima.UL- frKeluar.UL+ frTerima.UK-frKeluar.UK).ToRupiahInReport(),
                                       };

                    gridPenerimaanPengeluaran.Rows.Add(rowtotal);


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool DisplayDetail()
        {
            try
            {
                SPJLogic SPJLogic = new SPJLogic(GlobalVar.TahunAnggaran);
                var q = from d in mFungsionalList
                        select new FungsionalRekening
                         {
                             IDDInas = m_IDSKPD,
                             IDUrusan = d.IDUrusan,
                             IDProgram = d.IDProgram,
                             IDKegiatan = d.IDKegiatan,
                             IDSubKegiatan = d.IDSubKegiatan,
                             IDRekening = d.IDRekening,
                             KodeUK = d.KodeUK,
                             GL = d.GL,
                             GK = d.GK,
                             BL = d.BL,
                             BK = d.BK,
                             UL = d.UL,
                             UK = d.UK

                         };



                mFungsionalList = q.ToList();



                if (mFungsionalList != null)
                {

                    ProsesSPJPerDinas();
                    List<int> lstKodeUK = new List<int>();
                    if (GlobalVar.gListOrganisasi == null)
                    {
                        GlobalVar.gListOrganisasi = new List<Unit>();
                    }
                    if (GlobalVar.gListOrganisasi.Count == 0)
                    {
                        UnitKerjaLogic ukLogic = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
                        GlobalVar.gListOrganisasi = ukLogic.Get();
                    }
                    int oldUK = -1;
                    foreach (Unit u in GlobalVar.gListOrganisasi)
                    {
                        if (u.SKPD == m_IDSKPD)
                        {
                            if (oldUK != u.UntAnggaran)
                            {
                                lstKodeUK.Add(u.UntAnggaran);
                                oldUK = u.UntAnggaran;
                            }
                        }
                    }

                    if (lstKodeUK.Count == 0)
                    {
                        lstKodeUK.Add(0);
                    }
                    foreach (int kodeunit in lstKodeUK)
                    {
                        ProsesSPJPerUK(kodeunit);
                        ProsesSPJPerurusan(kodeunit);
                        ProsesSPJPerProgram(kodeunit);
                        ProsesDisplayDetailPerKegiatan(kodeunit);
                        ProsesDisplayDetailPerSubKegiatan(kodeunit);
                        ProsesDisplayDetailPerRekening(kodeunit);
                        RefreshSisaPagu();
                    }

                }
                else
                {
                    MessageBox.Show(SPJLogic.LastError());
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan menampilkan detail SPJ" + ex.Message);
                return false;
            }

        }

        public void RefreshSisaPagu()
        {
            decimal nilaiAnggaran = 0M;
            decimal nilaiSPJ = 0M;
            switch (ctrlTahapAnggaran1.ID)
            {
                case 2:
                    mcolAnggaran = COL_ANGGARANMURNI;
                    break;
                case 3:
                    mcolAnggaran = COL_ANGGARANGESER;
                    break;
                case 4:
                    mcolAnggaran = COL_ANGGARANRKAP;
                    break;
                case 5:
                    mcolAnggaran = COL_ANGGARANABT;
                    break;
            }

            decimal nilaisisaPagu = 0M;
            foreach (DataGridViewRow row in gridSPJ.Rows)
            {
                nilaiAnggaran = DataFormat.GetString(row.Cells[mcolAnggaran].Value).FormatUangReportKeDecimal();
                nilaiSPJ = DataFormat.GetString(row.Cells[COL_SPJTot].Value).FormatUangReportKeDecimal();
                nilaisisaPagu = nilaiAnggaran - nilaiSPJ;
                row.Cells[COL_SISASPJ].Value = nilaisisaPagu.ToRupiahInReport();


            }
        }
        private void ProsesSPJPerDinas()
        {
            var lstJumlah = mFungsionalList.FindAll(x => x.IDDInas == m_IDSKPD).GroupBy(g => g.IDDInas)
             .Select(x => new
             {
                 IDDINAS = x.Key,

                 GL = x.Sum(y => y.GL),
                 GK = x.Sum(y => y.GK),
                 BL = x.Sum(y => y.BL),
                 BK = x.Sum(y => y.BK),
                 UL = x.Sum(y => y.UL),
                 UK = x.Sum(y => y.UK),

             }).ToList();

            for (int idx = 0; idx < gridSPJ.Rows.Count; idx++)
            {
                if (gridSPJ.Rows[idx].Cells[COL_IDURUSAN].Value != null &&
                    gridSPJ.Rows[idx].Cells[COL_LEVEL].Value != null)
                {
                    if (DataFormat.GetInteger(gridSPJ.Rows[idx].Cells[COL_IDDINAS].Value) == m_IDSKPD &&
                        DataFormat.GetInteger(gridSPJ.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_DINAS
                        )
                    {
                        if (lstJumlah.Count > 0)
                        {

                            gridSPJ.Rows[idx].Cells[COL_GL].Value = lstJumlah[0].GL.ToRupiahInReport();
                            gridSPJ.Rows[idx].Cells[COL_GK].Value = lstJumlah[0].GK.ToRupiahInReport();
                            gridSPJ.Rows[idx].Cells[COL_GTot].Value = (lstJumlah[0].GL + lstJumlah[0].GK).ToRupiahInReport();

                            gridSPJ.Rows[idx].Cells[COL_BL].Value = lstJumlah[0].BL.ToRupiahInReport();
                            gridSPJ.Rows[idx].Cells[COL_BK].Value = lstJumlah[0].BK.ToRupiahInReport();
                            gridSPJ.Rows[idx].Cells[COL_BTot].Value = (lstJumlah[0].BK + lstJumlah[0].BL).ToRupiahInReport();
                            gridSPJ.Rows[idx].Cells[COL_UL].Value = lstJumlah[0].UL.ToRupiahInReport();
                            gridSPJ.Rows[idx].Cells[COL_UK].Value = lstJumlah[0].UK.ToRupiahInReport();
                            gridSPJ.Rows[idx].Cells[COL_UTot].Value = (lstJumlah[0].UL + lstJumlah[0].UK).ToRupiahInReport();
                            decimal total = lstJumlah[0].UL + lstJumlah[0].UK + lstJumlah[0].GL + lstJumlah[0].GK +
                                lstJumlah[0].BL + lstJumlah[0].BK;
                            gridSPJ.Rows[idx].Cells[COL_SPJTot].Value = total.ToRupiahInReport();
                            decimal sisa = DataFormat.FormatUangReportKeDecimal(
                                gridSPJ.Rows[idx].Cells[mcolAnggaran].Value.ToString()) - total;

                            gridSPJ.Rows[idx].Cells[COL_SISASPJ].Value = sisa.ToRupiahInReport();
                        }
                        break;
                    }
                }
            }
        }
        private void ProsesSPJPerUK(int KodeUK)
        {
            var lstJumlah = mFungsionalList.Where(u => u.KodeUK == KodeUK).GroupBy(x => x.KodeUK)
                   .Select(x => new
                   {
                       KodeUK = x.Key,
                       GL = x.Sum(y => y.GL),
                       GK = x.Sum(y => y.GK),
                       BL = x.Sum(y => y.BL),
                       BK = x.Sum(y => y.BK),
                       UL = x.Sum(y => y.UL),
                       UK = x.Sum(y => y.UK),



                   }).ToList();


            List<FungsionalRekening> lstJumlahSPJPerurusan = (from t in mFungsionalList
                                                              join j in lstJumlah
                                                              on t.KodeUK equals j.KodeUK
                                                              select new FungsionalRekening
                                                              {
                                                                  IDUrusan = 0,
                                                                  IDProgram = 0,
                                                                  IDKegiatan = 0,
                                                                  IDSubKegiatan = 0,
                                                                  IDRekening = 0,
                                                                  KodeUK = t.KodeUK,
                                                                  GL = j.GL,
                                                                  GK = j.GK,
                                                                  BL = j.BL,
                                                                  BK = j.BK,
                                                                  UL = j.UL,
                                                                  UK = j.UK,


                                                              }).ToList<FungsionalRekening>();//.Distinct();// < FungsionalRekening>();

            List<FungsionalRekening> lstJumlahSPJPerurusanDistincted = new List<FungsionalRekening>();
            var lst = lstJumlahSPJPerurusan
             .Select(p => new { p.KodeUK, p.GL, p.GK, p.BL, p.BK, p.UL, p.UK })
             .Distinct().ToList();

            int oldKodeUK = 0;

            foreach (var u in lst)
            {

                if (u.KodeUK != oldKodeUK)
                {

                    for (int idx = 0; idx < gridSPJ.Rows.Count; idx++)
                    {
                        if (gridSPJ.Rows[idx].Cells[COL_IDURUSAN].Value != null &&
                            gridSPJ.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {
                            if (gridSPJ.Rows[idx].Cells[COL_KODEUK].Value.ToString() == u.KodeUK.ToString() &&
                                DataFormat.GetInteger(gridSPJ.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_UNIT
                                )
                            {

                                gridSPJ.Rows[idx].Cells[COL_GL].Value = u.GL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GK].Value = u.GK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GTot].Value = (u.GL + u.GK).ToRupiahInReport();

                                gridSPJ.Rows[idx].Cells[COL_BL].Value = u.BL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BK].Value = u.BK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BTot].Value = (u.BK + u.BL).ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UL].Value = u.UL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UK].Value = u.UK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UTot].Value = (u.UL + u.UK).ToRupiahInReport();
                                decimal total = u.UL + u.UK + u.GL + u.GK + u.BL + u.BK;
                                gridSPJ.Rows[idx].Cells[COL_SPJTot].Value = total.ToRupiahInReport();
                                decimal sisa = DataFormat.FormatUangReportKeDecimal(
                                    gridSPJ.Rows[idx].Cells[mcolAnggaran].Value.ToString()) - total;

                                gridSPJ.Rows[idx].Cells[COL_SISASPJ].Value = sisa.ToRupiahInReport();


                            }
                        }
                    }
                    oldKodeUK = u.KodeUK;
                }

            }


        }
        private void ProsesSPJPerurusan(int KodeUK)
        {
            var lstJumlah = mFungsionalList.Where(x => x.KodeUK == KodeUK).GroupBy(x => x.IDUrusan)
                   .Select(x => new
                   {

                       IDUrusan = x.Key,
                       GL = x.Sum(y => y.GL),
                       GK = x.Sum(y => y.GK),
                       BL = x.Sum(y => y.BL),
                       BK = x.Sum(y => y.BK),
                       UL = x.Sum(y => y.UL),
                       UK = x.Sum(y => y.UK),



                   }).ToList();

            List<FungsionalRekening> lstOnThisUK = mFungsionalList.FindAll(x => x.KodeUK == KodeUK);//.FindAll();;

            List<FungsionalRekening> lstJumlahSPJPerurusan = (from t in lstOnThisUK
                                                              join j in lstJumlah
                                                              on t.IDUrusan equals j.IDUrusan
                                                              select new FungsionalRekening
                                                              {

                                                                  IDUrusan = t.IDUrusan,
                                                                  IDProgram = 0,
                                                                  IDKegiatan = 0,
                                                                  IDSubKegiatan = 0,
                                                                  IDRekening = 0,
                                                                  KodeUK = KodeUK,
                                                                  GL = j.GL,
                                                                  GK = j.GK,
                                                                  BL = j.BL,
                                                                  BK = j.BK,
                                                                  UL = j.UL,
                                                                  UK = j.UK,


                                                              }).ToList<FungsionalRekening>();//.Distinct();// < FungsionalRekening>();

            List<FungsionalRekening> lstJumlahSPJPerurusanDistincted = new List<FungsionalRekening>();
            int oldUrusan = 0;
            foreach (FungsionalRekening u in lstJumlahSPJPerurusan)
            {

                if (u.IDUrusan != oldUrusan)
                {

                    for (int idx = 0; idx < gridSPJ.Rows.Count; idx++)
                    {
                        if (gridSPJ.Rows[idx].Cells[COL_IDURUSAN].Value != null &&
                            gridSPJ.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {
                            if (gridSPJ.Rows[idx].Cells[COL_IDURUSAN].Value.ToString() == u.IDUrusan.ToString() &&
                               gridSPJ.Rows[idx].Cells[COL_KODEUK].Value.ToString() == u.KodeUK.ToString() &&
                               GetLevel(idx) == LEVEL_URUSAN
                                //DataFormat.GetInteger(gridSPJ.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_URUSAN
                                )
                            {
                                if (u.IDUrusan == 210)
                                {
                      //              MessageBox.Show(DataFormat.GetString( gridSPJ.Rows[idx].Cells[1].Value));
                                }
                                gridSPJ.Rows[idx].Cells[COL_GL].Value = u.GL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GK].Value = u.GK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GTot].Value = (u.GL + u.GK).ToRupiahInReport();

                                gridSPJ.Rows[idx].Cells[COL_BL].Value = u.BL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BK].Value = u.BK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BTot].Value = (u.BK + u.BL).ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UL].Value = u.UL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UK].Value = u.UK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UTot].Value = (u.UL + u.UK).ToRupiahInReport();

                                decimal total = u.UL + u.UK + u.GL + u.GK + u.BL + u.BK;
                                gridSPJ.Rows[idx].Cells[COL_SPJTot].Value = total.ToRupiahInReport();
                                decimal sisa = DataFormat.FormatUangReportKeDecimal(
                                    gridSPJ.Rows[idx].Cells[mcolAnggaran].Value.ToString()) - total;

                                gridSPJ.Rows[idx].Cells[COL_SISASPJ].Value = sisa.ToRupiahInReport();
                            }
                        }
                    }
                    oldUrusan = u.IDUrusan;
                }

            }


        }
        private void ProsesSPJPerProgram(int KodeUK)
        {
            var lstJumlah = mFungsionalList.Where(x => x.KodeUK == KodeUK).GroupBy(x => x.IDProgram)
                   .Select(x => new
                   {
                       IDProg = x.Key,

                       GL = x.Sum(y => y.GL),
                       GK = x.Sum(y => y.GK),
                       BL = x.Sum(y => y.BL),
                       BK = x.Sum(y => y.BK),
                       UL = x.Sum(y => y.UL),
                       UK = x.Sum(y => y.UK),

                   }).ToList();

            List<FungsionalRekening> lstOnThisUK = mFungsionalList;//.FindAll();
            List<FungsionalRekening> lstJumlahSPJPerProgram = (from t in lstOnThisUK
                                                               join j in lstJumlah
                                                               on t.IDProgram equals j.IDProg
                                                               select new FungsionalRekening
                                                               {

                                                                   IDUrusan = t.IDUrusan,
                                                                   IDProgram = t.IDProgram,
                                                                   IDKegiatan = 0,
                                                                   IDSubKegiatan = 0,
                                                                   IDRekening = 0,
                                                                   KodeUK = KodeUK,
                                                                   GL = j.GL,
                                                                   GK = j.GK,
                                                                   BL = j.BL,
                                                                   BK = j.BK,
                                                                   UL = j.UL,
                                                                   UK = j.UK,


                                                               }).ToList<FungsionalRekening>();


            var lst = lstJumlahSPJPerProgram
                   .Select(p => new { p.IDProgram, p.GL, p.GK, p.BL, p.BK, p.UL, p.UK })
                   .Distinct().ToList();



            int oldProgram = 0;

            // foreach (FungsionalRekening u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {

                if (u.IDProgram != oldProgram)
                {
                    for (int idx = 0; idx < gridSPJ.Rows.Count; idx++)
                    {
                        if (gridSPJ.Rows[idx].Cells[COL_IDPROGRAM].Value != null &&
                            gridSPJ.Rows[idx].Cells[COL_IDPROGRAM].Value != null)
                        {


                            if (gridSPJ.Rows[idx].Cells[COL_IDPROGRAM].Value.ToString() == u.IDProgram.ToString() &&
                                gridSPJ.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                DataFormat.GetInteger(gridSPJ.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_PROGRAM

                                )
                            {

                                gridSPJ.Rows[idx].Cells[COL_GL].Value = u.GL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GK].Value = u.GK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GTot].Value = (u.GL + u.GK).ToRupiahInReport();

                                gridSPJ.Rows[idx].Cells[COL_BL].Value = u.BL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BK].Value = u.BK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BTot].Value = (u.BK + u.BL).ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UL].Value = u.UL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UK].Value = u.UK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UTot].Value = (u.UL + u.UK).ToRupiahInReport();
                                decimal total = u.UL + u.UK + u.GL + u.GK + u.BL + u.BK;
                                gridSPJ.Rows[idx].Cells[COL_SPJTot].Value = total.ToRupiahInReport();
                                decimal sisa = DataFormat.FormatUangReportKeDecimal(
                                    gridSPJ.Rows[idx].Cells[mcolAnggaran].Value.ToString()) - total;

                                gridSPJ.Rows[idx].Cells[COL_SISASPJ].Value = sisa.ToRupiahInReport();
                            }
                        }
                    }

                }

            }

        }
        private void ProsesDisplayDetailPerKegiatan(int KodeUK)
        {
            List<FungsionalRekening> lstOnThisUK = mFungsionalList.FindAll(x => x.KodeUK == KodeUK);
            var lstJumlah = lstOnThisUK.GroupBy(x => x.IDKegiatan)
                   .Select(x => new
                   {
                       IDKegiatan = x.Key,
                       GL = x.Sum(y => y.GL),
                       GK = x.Sum(y => y.GK),
                       BL = x.Sum(y => y.BL),
                       BK = x.Sum(y => y.BK),
                       UL = x.Sum(y => y.UL),
                       UK = x.Sum(y => y.UK),

                   }).ToList();


            List<FungsionalRekening> lstJumlahSPJPerKegiatan = (from t in lstOnThisUK
                                                                join j in lstJumlah
                                                                on t.IDKegiatan equals j.IDKegiatan
                                                                select new FungsionalRekening
                                                                {


                                                                    IDUrusan = t.IDUrusan,
                                                                    IDProgram = t.IDProgram,
                                                                    IDKegiatan = t.IDKegiatan,
                                                                    IDSubKegiatan = 0,
                                                                    IDRekening = 0,
                                                                    KodeUK = KodeUK,
                                                                    GL = j.GL,
                                                                    GK = j.GK,
                                                                    BL = j.BL,
                                                                    BK = j.BK,
                                                                    UL = j.UL,
                                                                    UK = j.UK,


                                                                }).ToList<FungsionalRekening>();


            var lst = lstJumlahSPJPerKegiatan
                  .Select(p => new { p.IDProgram, p.IDKegiatan, p.GL, p.GK, p.BL, p.BK, p.UL, p.UK })
                   .Distinct().ToList();




            int oldKegiatan = 0;

            // foreach (FungsionalRekening u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {

                if (u.IDKegiatan != oldKegiatan)
                {
                    if (u.IDKegiatan == 21301201)
                    {
                        Console.WriteLine(u.IDKegiatan.ToString());

                    }
                    for (int idx = 0; idx < gridSPJ.Rows.Count; idx++)
                    {
                        if (gridSPJ.Rows[idx].Cells[COL_IDKEGIATAN].Value != null && gridSPJ.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridSPJ.Rows[idx].Cells[COL_IDKEGIATAN].Value.ToString() == u.IDKegiatan.ToString() &&
                                     gridSPJ.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                GetLevel(idx) == LEVEL_KEGIATAN

                                )
                            {

                                gridSPJ.Rows[idx].Cells[COL_GL].Value = u.GL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GK].Value = u.GK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GTot].Value = (u.GL + u.GK).ToRupiahInReport();

                                gridSPJ.Rows[idx].Cells[COL_BL].Value = u.BL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BK].Value = u.BK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BTot].Value = (u.BK + u.BL).ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UL].Value = u.UL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UK].Value = u.UK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UTot].Value = (u.UL + u.UK).ToRupiahInReport();
                                decimal total = u.UL + u.UK + u.GL + u.GK + u.BL + u.BK;
                                gridSPJ.Rows[idx].Cells[COL_SPJTot].Value = total.ToRupiahInReport();
                                decimal sisa = DataFormat.FormatUangReportKeDecimal(
                                    gridSPJ.Rows[idx].Cells[mcolAnggaran].Value.ToString()) - total;

                                gridSPJ.Rows[idx].Cells[COL_SISASPJ].Value = sisa.ToRupiahInReport();
                            }
                        }
                    }
                    oldKegiatan = u.IDKegiatan;

                }

            }

        }
        private void ProsesDisplayDetailPerSubKegiatan(int KodeUK)
        {
            List<FungsionalRekening> lstOnThisUK = mFungsionalList.FindAll(x => x.KodeUK == KodeUK);

            var lstJumlah = lstOnThisUK
                            .GroupBy(
                            c => new
                            {
                                c.IDSubKegiatan,

                            }
                            )
                   .Select(x => new
                   {
                       IIDRekening = 0,
                       IDSubKegiatan = x.Key.IDSubKegiatan,
                       GL = x.Sum(y => y.GL),
                       GK = x.Sum(y => y.GK),
                       BL = x.Sum(y => y.BL),
                       BK = x.Sum(y => y.BK),
                       UL = x.Sum(y => y.UL),
                       UK = x.Sum(y => y.UK),
                   }).ToList();


            List<FungsionalRekening> lstJumlahSPJPerSubKegiatan = (from t in lstOnThisUK
                                                                   join j in lstJumlah
                                                                   on t.IDSubKegiatan equals j.IDSubKegiatan
                                                                   select new FungsionalRekening
                                                                   {



                                                                       IDUrusan = t.IDUrusan,
                                                                       IDProgram = t.IDProgram,
                                                                       IDKegiatan = t.IDKegiatan,
                                                                       IDSubKegiatan = t.IDSubKegiatan,
                                                                       IDRekening = 0,
                                                                       KodeUK = KodeUK,
                                                                       GL = j.GL,
                                                                       GK = j.GK,
                                                                       BL = j.BL,
                                                                       BK = j.BK,
                                                                       UL = j.UL,
                                                                       UK = j.UK,


                                                                   }).ToList<FungsionalRekening>();


            var lst = lstJumlahSPJPerSubKegiatan
                   .Select(p => new { p.IDProgram, p.IDKegiatan, p.IDSubKegiatan, p.GL, p.GK, p.BL, p.BK, p.UL, p.UK })
                   .Distinct().ToList();





            long oldIDSubKegiatan = 0;

            // foreach (FungsionalRekening u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {

                if (u.IDSubKegiatan != oldIDSubKegiatan)
                {
                    for (int idx = 0; idx < gridSPJ.Rows.Count; idx++)
                    {
                        if (gridSPJ.Rows[idx].Cells[COL_IDSubKegiatan].Value != null && gridSPJ.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridSPJ.Rows[idx].Cells[COL_IDSubKegiatan].Value.ToString() == u.IDSubKegiatan.ToString() &&
                                     gridSPJ.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                DataFormat.GetInteger(gridSPJ.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_SUBKEGIATAN

                                )
                            {

                                gridSPJ.Rows[idx].Cells[COL_GL].Value = u.GL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GK].Value = u.GK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GTot].Value = (u.GL + u.GK).ToRupiahInReport();

                                gridSPJ.Rows[idx].Cells[COL_BL].Value = u.BL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BK].Value = u.BK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BTot].Value = (u.BK + u.BL).ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UL].Value = u.UL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UK].Value = u.UK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UTot].Value = (u.UL + u.UK).ToRupiahInReport();
                                decimal total = u.UL + u.UK + u.GL + u.GK + u.BL + u.BK;
                                gridSPJ.Rows[idx].Cells[COL_SPJTot].Value = total.ToRupiahInReport();
                                decimal sisa = DataFormat.FormatUangReportKeDecimal(
                                    gridSPJ.Rows[idx].Cells[mcolAnggaran].Value.ToString()) - total;

                                gridSPJ.Rows[idx].Cells[COL_SISASPJ].Value = sisa.ToRupiahInReport();

                            }
                        }
                    }
                    oldIDSubKegiatan = u.IDSubKegiatan;
                }


            }

        }
        private void ProsesDisplayDetailPerRekening(int KodeUK)
        {
            List<FungsionalRekening> lstOnThisUK = mFungsionalList.FindAll(x => x.KodeUK == KodeUK);
            var lstJumlah = lstOnThisUK
                         .GroupBy(
                         c => new
                         {
                             c.IDSubKegiatan,
                             c.IDRekening,
                         }
                         )
                .Select(x => new
                {
                    IDRekening = x.Key.IDRekening,
                    IDSubKegiatan = x.Key.IDSubKegiatan,
                    GL = x.Sum(y => y.GL),
                    GK = x.Sum(y => y.GK),
                    BL = x.Sum(y => y.BL),
                    BK = x.Sum(y => y.BK),
                    UL = x.Sum(y => y.UL),
                    UK = x.Sum(y => y.UK),
                }).ToList();


            List<FungsionalRekening> lstJumlahSPJPerSubKegiatanRekening = (from t in lstOnThisUK
                                                                           join j in lstJumlah
                                                                          on t.IDSubKegiatan equals j.IDSubKegiatan
                                                                           where t.IDRekening == j.IDRekening
                                                                           select new FungsionalRekening
                                                                           {
                                                                               IDUrusan = t.IDUrusan,
                                                                               IDProgram = t.IDProgram,
                                                                               IDKegiatan = t.IDKegiatan,
                                                                               IDSubKegiatan = t.IDSubKegiatan,
                                                                               IDRekening = t.IDRekening,
                                                                               KodeUK = KodeUK,
                                                                               GL = j.GL,
                                                                               GK = j.GK,
                                                                               BL = j.BL,
                                                                               BK = j.BK,
                                                                               UL = j.UL,
                                                                               UK = j.UK,


                                                                           }).ToList<FungsionalRekening>();


            var lst = lstJumlahSPJPerSubKegiatanRekening
                   .Select(p => new { p.IDProgram, p.IDKegiatan, p.IDSubKegiatan, p.IDRekening, p.GL, p.GK, p.BL, p.BK, p.UL, p.UK })
                   .Distinct().ToList();




            long oldIDSubKegiatan = 0;
            long oldIdRekening = 0;
            // foreach (FungsionalRekening u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {
                if (u.IDRekening == 510101010001)
                {
                    Console.WriteLine(u.GK.ToString());

                }
                if (u.IDSubKegiatan != oldIDSubKegiatan || u.IDRekening != oldIdRekening)
                {
                    for (int idx = 0; idx < gridSPJ.Rows.Count; idx++)
                    {
                        if (gridSPJ.Rows[idx].Cells[COL_IDSubKegiatan].Value != null &&
                            gridSPJ.Rows[idx].Cells[COL_IDREKENING].Value != null &&
                            gridSPJ.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridSPJ.Rows[idx].Cells[COL_IDSubKegiatan].Value.ToString() == u.IDSubKegiatan.ToString() &&
                              gridSPJ.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                gridSPJ.Rows[idx].Cells[COL_IDREKENING].Value.ToString() == u.IDRekening.ToString() &&
                                GetLevel(idx) == LEVEL_REKANING
                                //gridSPJ.Rows[idx].Cells[COL_LEVEL].Value.ToString() == "5"

                                )
                            {

                                gridSPJ.Rows[idx].Cells[COL_GL].Value = u.GL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GK].Value = u.GK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_GTot].Value = (u.GL + u.GK).ToRupiahInReport();

                                gridSPJ.Rows[idx].Cells[COL_BL].Value = u.BL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BK].Value = u.BK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_BTot].Value = (u.BK + u.BL).ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UL].Value = u.UL.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UK].Value = u.UK.ToRupiahInReport();
                                gridSPJ.Rows[idx].Cells[COL_UTot].Value = (u.UL + u.UK).ToRupiahInReport();
                                decimal total = u.UL + u.UK + u.GL + u.GK + u.BL + u.BK;
                                gridSPJ.Rows[idx].Cells[COL_SPJTot].Value = total.ToRupiahInReport();
                                decimal sisa = DataFormat.FormatUangReportKeDecimal(
                                    gridSPJ.Rows[idx].Cells[mcolAnggaran].Value.ToString()) - total;

                                gridSPJ.Rows[idx].Cells[COL_SISASPJ].Value = sisa.ToRupiahInReport();



                            }
                        }
                    }
                    oldIDSubKegiatan = u.IDSubKegiatan;
                }


            }

        }

        #endregion DisplaySPJ
        #region formatting

        private void FormatGrid()
        {
            FontStyle styleFont = new FontStyle();

            _hilightstyle.Font = new System.Drawing.Font(gridSPJ.Font, FontStyle.Bold);
            _hilightstyle.ForeColor = Color.White;

            _hilightstyle.BackColor = Color.LightSlateGray;


            _level2style.Font = new System.Drawing.Font(gridSPJ.Font, FontStyle.Bold);
            _level2style.BackColor = Color.LightSteelBlue;

            _level3style.Font = new System.Drawing.Font(gridSPJ.Font, FontStyle.Bold);
            _level3style.BackColor = Color.LightSteelBlue;// new Font(gridKUA.Font, FontStyle.Bold);

            _level4style.Font = new System.Drawing.Font(gridSPJ.Font, FontStyle.Regular);
            _level4style.BackColor = Color.LightGray;// new Font(gridKUA.Font, FontStyle.Bold);

            _level5style.Font = new System.Drawing.Font(gridSPJ.Font, FontStyle.Italic);
            _level5style.BackColor = Color.Lavender;// new Font(gridKUA.Font, FontStyle.Bold);
            _level6style.Font = new System.Drawing.Font(gridSPJ.Font, FontStyle.Italic);
            _level6style.BackColor = Color.Honeydew;// new Font(gridKUA.Font, FontStyle.Bold);

            _level7style.Font = new System.Drawing.Font(gridSPJ.Font, FontStyle.Regular);

            _level7style.BackColor = Color.Honeydew;// new Font(gridKUA.Font, FontStyle.Bold);

            for (int idx = 0; idx < gridSPJ.Rows.Count; idx++)
            {
                int level = GetLevel(idx);

                switch (level)
                {
                    case LEVEL_DINAS:
                        gridSPJ.Rows[idx].DefaultCellStyle = _hilightstyle;

                        break;
                    case LEVEL_UNIT:
                        gridSPJ.Rows[idx].DefaultCellStyle = _level2style;
                        break;

                    case LEVEL_URUSAN:
                        gridSPJ.Rows[idx].DefaultCellStyle = _level3style;
                        break;

                    case LEVEL_PROGRAM:
                        gridSPJ.Rows[idx].DefaultCellStyle = _level4style;
                        break;
                    case LEVEL_KEGIATAN:
                        gridSPJ.Rows[idx].DefaultCellStyle = _level5style;
                        break;
                    case LEVEL_SUBKEGIATAN:
                        gridSPJ.Rows[idx].DefaultCellStyle = _level6style;
                        break;
                }

            }
        }
        #endregion formatting

        private int GetLevel(int Baris)
        {
            return DataFormat.GetInteger(gridSPJ.Rows[Baris].Cells[COL_LEVEL].Value);
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_IDSKPD = pIDSKPD;// ctrlPanelPencarian1.Dinas
            m_IDSKPD = pIDSKPD;
        }

        private void cmdExcell_Click(object sender, EventArgs e)
        {
            txtNamaFile.Text = "";
            if (chkKodeRekeninSaja.Checked)
            {
                ExportRekOnly();
            }
            else
            {
                Export();
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



            return sRet;
        }
        private void KillSpecificExcelFileProcess(string excelFileName)
        {
            var processes = from p in Process.GetProcessesByName("EXCEL")
                            select p;

            foreach (var process in processes)
            {
                if (process.MainWindowTitle == "Microsoft Excel - " + excelFileName)
                    process.Kill();
            }
        }
        private void Export()
        {

            string NamaFile = "";
   
            try
            {
                string namaFile = BuatFile();
                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Masih Kosong ");
                    return;
                }
                KillSpecificExcelFileProcess(namaFile);

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
                int colAnggaran=0;

                excelSheet.Name = "SPJ Fungsional";
                List<int> lstColToCetak = new List<int>();
                lstColToCetak.Add(0);
                lstColToCetak.Add(1);
                lstColToCetak.Add(GetColAnggaran());

                lstColToCetak.Add(COL_GL);
                lstColToCetak.Add(COL_GK);
                lstColToCetak.Add(COL_GTot);

                lstColToCetak.Add(COL_BL);
                lstColToCetak.Add(COL_BK);
                lstColToCetak.Add(COL_BTot);
                lstColToCetak.Add(COL_UL);
                lstColToCetak.Add(COL_UK);
                lstColToCetak.Add(COL_UTot);
                lstColToCetak.Add(COL_SPJTot);
                lstColToCetak.Add(COL_SISASPJ);
                // storing header part in Excel 
                excelSheet.Cells[1, 1] = "SKPD" ;
                excelSheet.Cells[1, 2] = ctrlDinas1.GetNamaSKPD();
                excelSheet.Cells[2, 1] = "Periode " ;
                excelSheet.Cells[2, 2] = ctrlTanggalBulan1.Waktu;

                int col = 1;
                for (int i = 0; i < gridSPJ.Columns.Count ; i++)
                {
                    if (lstColToCetak.Contains(i))
                    {
                        
                        excelSheet.Cells[3, col] = gridSPJ.Columns[i ].HeaderText;
                        col++;
                    }
              

                }
                int awalrow = 4;
                for (int i =0; i < gridSPJ.Rows.Count - 1; i++)
                {
                    int c = 1;
                    for (int j = 0; j < gridSPJ.Columns.Count + 1; j++)
                    {
                        if (lstColToCetak.Contains(j))
                        {
                            string s;
                            if (j >= 2)
                            {
                                s = DataFormat.FormatUangReportKeDecimal(gridSPJ.Rows[i].Cells[j].Value).ToString("###.##");
                            }
                            else
                            {
                                s = DataFormat.GetString(gridSPJ.Rows[i].Cells[j].Value);
                            }

                            excelSheet.Cells[awalrow, c] = s;
                             c++;
                           }
                        
                    }
                    awalrow++;
                }
                

              awalrow++;
              awalrow++;
              awalrow++;
                for (int i = 0; i < gridPenerimaanPengeluaran.Rows.Count - 1; i++)
                {
                    int c = 1;
                    for (int j = 0; j <= gridPenerimaanPengeluaran.Columns.Count - 1; j++)
                    {
                        if (j == 2)
                        {
                            c = 4;
                        }
                        
                            if (gridPenerimaanPengeluaran.Rows[i].Cells[j].Value != null)
                            {
                                string s = "";
                                if (j >= 2)
                                {
                                    s = DataFormat.FormatUangReportKeDecimal(gridPenerimaanPengeluaran.Rows[i].Cells[j].Value).ToString("###.##");
                                }
                                else
                                {
                                    s = DataFormat.GetString(gridPenerimaanPengeluaran.Rows[i].Cells[j].Value);
                                }

                                excelSheet.Cells[ awalrow, c] = s;
                                c++;
                            }
                           
                            
                        }
                    
                    

                    awalrow++;
                }
                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1],
                                  excelSheet.Cells[excelSheet.Rows.Count - 1,
                                  excelSheet.Columns.Count - 1]];
                //    excelSheet.Columns.AutoFit();
                excelSheet.Columns.ColumnWidth = 20;
                excelSheet.Columns[2].ColumnWidth = 50;
                excelSheet.Columns[2].WrapText = true;

             
                excelworkBook.SaveAs(namaFile);
                MessageBox.Show("File sudah disimpan di " + namaFile);


                excelworkBook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke excell" + ex.Message);
            }

        }
        private void ExportRekOnly()
        {

            string NamaFile = "";

            try
            {
                string namaFile = BuatFile();
                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Masih Kosong ");
                    return;
                }
                KillSpecificExcelFileProcess(namaFile);

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

                excelSheet.Name = "SPJ Fungsional";

                // storing header part in Excel  
                for (int i = 1; i < gridSPJ.Columns.Count + 1; i++)
                {
                    if (i <= 3 || i > 17)
                    {
                        if (i >= 17)
                        {
                            excelSheet.Cells[1, i - 14] = gridSPJ.Columns[i - 1].HeaderText;
                        }
                        else
                        {
                            excelSheet.Cells[1, i] = gridSPJ.Columns[i - 1].HeaderText;
                        }
                    }

                }
                // storing Each row and column value to excel sheet  
                List<int> lstColToCetak = new List<int>();
                int  baris = 0;
                for (int i = 0; i < gridSPJ.Rows.Count - 1; i++)
                {
                    int c = 0;
                    for (int j = 0; j <= gridSPJ.Columns.Count - 1; j++)
                    {
                        if (gridSPJ.Columns[j].Visible == true)
                        {
                            ++c;
                            if (gridSPJ.Rows[i].Cells[j].Value != null)
                            {
                                string s = "";
                                if (j >= 2)
                                {
                                    s = DataFormat.FormatUangReportKeDecimal(gridSPJ.Rows[i].Cells[j].Value).ToString("###.##");
                                }
                                else
                                {
                                    s = DataFormat.GetString(gridSPJ.Rows[i].Cells[j].Value);
                                }
                                if (DataFormat.GetInteger(gridSPJ.Rows[i].Cells[COL_LEVEL].Value) == 6)
                                {
                                    excelSheet.Cells[baris + 2, c] = s;
                                }



                            }
                        }
                    }
                    if (DataFormat.GetInteger(gridSPJ.Rows[i].Cells[COL_LEVEL].Value) == 6)
                    {
                        baris++;
                    }

                }

                int awalrow = gridSPJ.Rows.Count + 1;

                for (int i = 0; i < gridPenerimaanPengeluaran.Rows.Count - 1; i++)
                {
                    int c = 0;
                    for (int j = 0; j <= gridPenerimaanPengeluaran.Columns.Count - 1; j++)
                    {
                        if (gridPenerimaanPengeluaran.Columns[j].Visible == true)
                        {
                            ++c;
                            if (gridPenerimaanPengeluaran.Rows[i].Cells[j].Value != null)
                            {
                                string s = "";
                                if (j >= 2)
                                {
                                    s = DataFormat.FormatUangReportKeDecimal(gridPenerimaanPengeluaran.Rows[i].Cells[j].Value).ToString("###.##");
                                }
                                else
                                {
                                    s = DataFormat.GetString(gridPenerimaanPengeluaran.Rows[i].Cells[j].Value);
                                }

                                excelSheet.Cells[i + 2 + awalrow, c] = s;



                            }
                        }
                    }

                }
                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1],
                                  excelSheet.Cells[excelSheet.Rows.Count - 1,
                                  excelSheet.Columns.Count - 1]];
                //    excelSheet.Columns.AutoFit();
                excelSheet.Columns.ColumnWidth = 20;
                excelSheet.Columns[2].ColumnWidth = 50;
                excelSheet.Columns[2].WrapText = true;


                excelworkBook.SaveAs(namaFile);
                MessageBox.Show("File sudah disimpan di " + namaFile);


                excelworkBook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke excell" + ex.Message);
            }

        }

        private void ctrlBulan1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void cmdCariFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.Filter = "Excel|*.xlsx;*.xls";
            fdlg.Title = "Save an Image File";
            fdlg.ShowDialog();

            fdlg.Title = "Buat File file";
            fdlg.InitialDirectory = @"c:\";

            //fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel|*.xlsx;*.xls";
            fdlg.RestoreDirectory = true;


            //if (fdlg.ShowDialog() == DialogResult.OK)
            //{

            if (fdlg.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                txtNamaFile.Text = fdlg.FileName;



            }
        }

        private void ctrlPanelPencarian1_OnDisplay()
        {

        }
        #region Pemanggilan_Anggaran
        public bool LoadProgramKegiatan()
        {



            //  m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
            ProgramKegiatanAnggaranLogic oLogic = new ProgramKegiatanAnggaranLogic(GlobalVar.TahunAnggaran);
            m_IDSKPD = ctrlDinas1.GetID();
            if (GlobalVar.gListProgramKegiatanRekeningAnggaran == null)
            {
                GlobalVar.gListProgramKegiatanRekeningAnggaran = new List<ProgramKegiatanAnggaran>();
            }
            if (GlobalVar.gListProgramKegiatanRekeningAnggaran.FindAll(p => p.IDDInas == m_IDSKPD).Count == 0)
            {
                List<ProgramKegiatanAnggaran> lst = new List<ProgramKegiatanAnggaran>();
                m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
                lst = oLogic.GetByDInas(m_IDSKPD, 0);
                if (lst != null)
                {
                    foreach (ProgramKegiatanAnggaran p in lst)
                    {
                        GlobalVar.gListProgramKegiatanRekeningAnggaran.Add(p);
                        m_lstProgramKegiatan.Add(p);
                    }
                    m_lstProgramKegiatan = lst;
                }
                else
                {
                    MessageBox.Show(oLogic.LastError());
                    return false;
                }


            }
            else
            {
                m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
                m_lstProgramKegiatan = oLogic.GetByDInas(m_IDSKPD, 0);
                m_lstProgramKegiatan = GlobalVar.gListProgramKegiatanRekeningAnggaran.FindAll(p => p.IDDInas == m_IDSKPD);
            }


            if (oLogic.IsError())
            {
                MessageBox.Show(oLogic.LastError());
                return false;

            }
            return true;


        }
        private bool DisplayProgramKegiatanSubKegiatan()
        {
            try
            {

                gridSPJ.Rows.Clear();
                // ***********************************
                m_IDSKPD = ctrlDinas1.GetID();
                var lstJumlahOPD = m_lstProgramKegiatan.FindAll(p => p.IDDInas == m_IDSKPD).GroupBy(x => x.IDDInas)
                   .Select(x => new
                   {
                       IDDInas = x.Key,
                       JumlahMurni = x.Sum(y => y.AnggaranMurni),
                       JumlahGeser = x.Sum(y => y.AnggaranGeser),
                       JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                       JumlahABT = x.Sum(y => y.AnggaranABT),

                   }).ToList();

                ProgramKegiatanAnggaran totalOPD = new ProgramKegiatanAnggaran
                {

                    StrIDUrusan = "0",
                    KodeUK = 0,
                    NamaUrusan = "",
                    IDUrusan = 0,
                    IDProgram = 0,
                    IDKegiatan = 0,
                    IDSubKegiatan = 0,
                    IIDRekening = 0,
                    AnggaranMurni = lstJumlahOPD[0].JumlahMurni,
                    AnggaranGeser = lstJumlahOPD[0].JumlahGeser,
                    AnggaranRKAP = lstJumlahOPD[0].JumlahRKAP,
                    AnggaranABT = lstJumlahOPD[0].JumlahABT
                };


                string[] rowOPD ={
                             m_IDSKPD.ToString(), ctrlDinas1.GetNamaSKPD(),                                                                                     totalOPD.AnggaranMurni.ToRupiahInReport(), 
                            totalOPD.AnggaranGeser.ToRupiahInReport(), 
                            totalOPD.AnggaranRKAP.ToRupiahInReport(), 
                            totalOPD.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_DINAS.ToString(),
                            "0",
                            "0",
                            "0",
                            "0",
                            "0" ,"0" };
                gridSPJ.Rows.Add(rowOPD);
                ProcessUnit();

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void ProcessUnit()
        {
            try
            {
                int oldKodeUK = -1;

                List<ProgramKegiatanAnggaran> lstProgramKegiatanDinas = new List<ProgramKegiatanAnggaran>();
                List<ProgramKegiatanAnggaran> lstUnit = new List<ProgramKegiatanAnggaran>();
                lstProgramKegiatanDinas = m_lstProgramKegiatan.FindAll(x => x.IDDInas == m_IDSKPD);
                lstProgramKegiatanDinas.OrderBy(x => x.KodeUK);

                // ***********************************
                foreach (ProgramKegiatanAnggaran uk in lstProgramKegiatanDinas)
                {
                    if (oldKodeUK != uk.KodeUK)
                    {
                        lstUnit.Add(uk);
                        oldKodeUK = uk.KodeUK;
                    }
                }

                var lstJumlahUK = lstProgramKegiatanDinas.GroupBy(x => x.KodeUK)
                  .Select(x => new
                  {
                      KodeUK = x.Key,
                      JumlahMurni = x.Sum(y => y.AnggaranMurni),
                      JumlahGeser = x.Sum(y => y.AnggaranGeser),
                      JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                      JumlahABT = x.Sum(y => y.AnggaranABT),

                  }).ToList();

                List<ProgramKegiatanAnggaran> lstUKDanAnggaran = (from k in lstUnit
                                                                  join j in lstJumlahUK
                                                                      on k.KodeUK equals j.KodeUK
                                                                  select new ProgramKegiatanAnggaran
                                                                  {

                                                                      KodeUK = k.KodeUK,
                                                                      NamaUK = k.NamaUK,
                                                                      AnggaranMurni = j.JumlahMurni,
                                                                      AnggaranGeser = j.JumlahGeser,
                                                                      AnggaranRKAP = j.JumlahRKAP,
                                                                      AnggaranABT = j.JumlahABT,


                                                                  }).ToList<ProgramKegiatanAnggaran>();

                oldKodeUK = -1;

                string parameterNamaUK = "";
                foreach (ProgramKegiatanAnggaran p in lstUKDanAnggaran)
                {
                    if ((p.KodeUK != oldKodeUK))
                    {
                        string[] rowOPD ={
                             m_IDSKPD.ToString(), "Unit " + p.NamaUK, 
                                            p.AnggaranMurni.ToRupiahInReport(), 
                                            p.AnggaranGeser.ToRupiahInReport(), 
                                            p.AnggaranRKAP.ToRupiahInReport(), 
                                            p.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_UNIT.ToString(),
                                            "0",
                                            "0",
                                            "0",
                                            "0",
                                            "0",p.KodeUK.ToString() };
                        if (p.KodeUK > 0)
                        {
                            parameterNamaUK = " (" + p.NamaUK + ")";
                            gridSPJ.Rows.Add(rowOPD);
                        }


                        ProcessUrusan(p.KodeUK, parameterNamaUK);
                        oldKodeUK = p.KodeUK;

                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void ProcessUrusan(int KodeUK, string NamaUK)
        {

            try
            {

                int oldIdUrusan = 0;

                //  
                List<ProgramKegiatanAnggaran> lstUrusan = new List<ProgramKegiatanAnggaran>();
                List<ProgramKegiatanAnggaran> lstProgramKegiatanThisUK = new List<ProgramKegiatanAnggaran>();
                lstProgramKegiatanThisUK = m_lstProgramKegiatan.FindAll(x => x.IDDInas == m_IDSKPD && x.KodeUK == KodeUK);

                foreach (ProgramKegiatanAnggaran u in lstProgramKegiatanThisUK)
                {
                    if (oldIdUrusan != u.IDUrusan)
                    {
                        lstUrusan.Add(u);
                        oldIdUrusan = u.IDUrusan;
                    }
                }

                var lstJumlah = lstProgramKegiatanThisUK.GroupBy(x => x.IDUrusan)

                   .Select(x => new
                   {
                       IDUrusan = x.Key,
                       JumlahMurni = x.Sum(y => y.AnggaranMurni),
                       JumlahGeser = x.Sum(y => y.AnggaranGeser),
                       JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                       JumlahABT = x.Sum(y => y.AnggaranABT),

                   }).ToList();
                // *********************************************************



                List<ProgramKegiatanAnggaran> lstUrusanDanAnggaran = (from t in lstUrusan
                                                                      join j in lstJumlah
                                                                      on t.IDUrusan equals j.IDUrusan
                                                                      select new ProgramKegiatanAnggaran
                                                                      {
                                                                          StrIDUrusan = t.StrIDUrusan,
                                                                          NamaUrusan = t.NamaUrusan,
                                                                          IDUrusan = t.IDUrusan,
                                                                          IDProgram = 0,
                                                                          IDKegiatan = 0,
                                                                          IDSubKegiatan = 0,
                                                                          IIDRekening = 0,
                                                                          AnggaranMurni = j.JumlahMurni,
                                                                          AnggaranGeser = j.JumlahGeser,
                                                                          AnggaranRKAP = j.JumlahRKAP,
                                                                          AnggaranABT = j.JumlahABT,


                                                                      }).ToList<ProgramKegiatanAnggaran>();

                oldIdUrusan = 0;
                foreach (ProgramKegiatanAnggaran p in lstUrusanDanAnggaran)
                {
                    if ((p.IDUrusan != oldIdUrusan))
                    {

                        //TreeGridNode urusannode = treeGridProgram.Nodes.Add
                        string[] row ={

                             p.StrIDUrusan , "Urusan " + p.NamaUrusan , 
                                                                                            p.AnggaranMurni.ToRupiahInReport(), 
                                                                                            p.AnggaranGeser.ToRupiahInReport(), 
                                                                                            p.AnggaranRKAP.ToRupiahInReport(), 
                                                                                            p.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_URUSAN.ToString(),
                                                                                            p.IDUrusan.ToString(),
                                                                                            p.IDProgram.ToString(),
                                                                                            p.IDKegiatan.ToString(),
                                                                                            p.IDSubKegiatan.ToString(),
                                                                                            p.IIDRekening.ToString(),KodeUK.ToString() };
                        gridSPJ.Rows.Add(row);

                        ProcessProgram(p.IDUrusan, KodeUK, NamaUK);
                        //                        ProcessProgram(p.IDUrusan, 0,"");

                        oldIdUrusan = p.IDUrusan;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }
        private void ProcessProgram(int idUrusan, int KodeUK, string NamaUK)
        {
            try
            {

                int oldIdProgram = 0;
                //  
                List<ProgramKegiatanAnggaran> lstProgramKegiatanThisUK = new List<ProgramKegiatanAnggaran>();

                lstProgramKegiatanThisUK = m_lstProgramKegiatan.FindAll(x => x.IDDInas == m_IDSKPD && x.KodeUK == KodeUK && x.IDUrusan == idUrusan);
                // lstProgramKegiatanThisUK = m_lstProgramKegiatan.FindAll(x => x.IDDInas == m_IDSKPD && x.IDUrusan == idUrusan);



                List<ProgramKegiatanAnggaran> lstPDistinctrogram = new List<ProgramKegiatanAnggaran>();

                foreach (ProgramKegiatanAnggaran program in lstProgramKegiatanThisUK)
                {
                    if (oldIdProgram != program.IDProgram)
                    {
                        lstPDistinctrogram.Add(program);
                        oldIdProgram = program.IDProgram;
                    }
                }
                var lstJumlahProgram = lstProgramKegiatanThisUK.GroupBy(x => x.IDProgram)
                   .Select(x => new
                   {
                       IDProgram = x.Key,
                       JumlahMurni = x.Sum(y => y.AnggaranMurni),
                       JumlahGeser = x.Sum(y => y.AnggaranGeser),
                       JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                       JumlahABT = x.Sum(y => y.AnggaranABT),

                   }).ToList();
                //*************************===
                List<ProgramKegiatanAnggaran> lstProgramDanAnggaran = (from t in lstPDistinctrogram
                                                                       join j in lstJumlahProgram
                                                                       on t.IDProgram equals j.IDProgram
                                                                       select new ProgramKegiatanAnggaran
                                                                       {
                                                                           StrIDProgram = t.StrIDProgram,
                                                                           NamaProgram = t.NamaProgram,
                                                                           IDProgram = t.IDProgram,
                                                                           IDKegiatan = 0,
                                                                           IDSubKegiatan = 0,
                                                                           IIDRekening = 0,
                                                                           AnggaranMurni = j.JumlahMurni,
                                                                           AnggaranGeser = j.JumlahGeser,
                                                                           AnggaranRKAP = j.JumlahRKAP,
                                                                           AnggaranABT = j.JumlahABT,


                                                                       }).ToList<ProgramKegiatanAnggaran>();

                //*****************************
                oldIdProgram = 0;
                foreach (ProgramKegiatanAnggaran pr in lstProgramDanAnggaran)
                {
                    if (pr.IDProgram != oldIdProgram)
                    {
                        //TreeGridNode nodeprogram = urusannode.Nodes.Add( 

                        string[] rowProgram ={pr.StrIDProgram , pr.NamaProgram + NamaUK, 
                                                                                            pr.AnggaranMurni.ToRupiahInReport(),
                                                                                            pr.AnggaranGeser.ToRupiahInReport(), 
                                                                                            pr.AnggaranRKAP.ToRupiahInReport(),
                                                                                            pr.AnggaranABT.ToRupiahInReport(),"0","0","0",LEVEL_PROGRAM.ToString(),
                                                                                            pr.IDUrusan.ToString(),
                                                                                            pr.IDProgram.ToString(),
                                                                                            pr.IDKegiatan.ToString(),
                                                                                            pr.IDSubKegiatan.ToString(),
                                                                                            pr.IIDRekening.ToString(),KodeUK.ToString()};
                        gridSPJ.Rows.Add(rowProgram);

                        oldIdProgram = pr.IDProgram;
                        ProcessKegiatan(oldIdProgram, idUrusan, KodeUK, NamaUK);
                        //     nodeprogram.Expand();
                    }
                }
            }




            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan Display Anggaran tingkat Program " + ex.Message);
            }
        }

        private void ProcessKegiatan(int idProgram, int idurusan, int KodeUK, string NamaUK)
        {
            try
            {
                int oldKegiatan;
                oldKegiatan = 0;
                List<ProgramKegiatanAnggaran> lstKegiatan = new List<ProgramKegiatanAnggaran>();
                List<ProgramKegiatanAnggaran> lstDistinctKegiatan = new List<ProgramKegiatanAnggaran>();

                lstKegiatan = m_lstProgramKegiatan.FindAll(keg => keg.IDDInas == m_IDSKPD &&
                                                         keg.IDProgram == idProgram && keg.KodeUK == KodeUK);

                foreach (ProgramKegiatanAnggaran kegiatan in lstKegiatan)
                {
                    if (oldKegiatan != kegiatan.IDKegiatan)
                    {
                        lstDistinctKegiatan.Add(kegiatan);
                        oldKegiatan = kegiatan.IDKegiatan;
                    }
                }

                var lstJumlahKegiatan = lstKegiatan.GroupBy(x => x.IDKegiatan)
                .Select(x => new
                {
                    IDKegiatan = x.Key,
                    JumlahMurni = x.Sum(y => y.AnggaranMurni),
                    JumlahGeser = x.Sum(y => y.AnggaranGeser),
                    JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                    JumlahABT = x.Sum(y => y.AnggaranABT),

                }).ToList();

                List<ProgramKegiatanAnggaran> lstPKegiatanDanAnggaran = (from t in lstDistinctKegiatan
                                                                         join j in lstJumlahKegiatan
                                                                         on t.IDKegiatan equals j.IDKegiatan
                                                                         select new ProgramKegiatanAnggaran
                                                                         {
                                                                             StrIDKegiatan = t.StrIDKegiatan,
                                                                             NamaKegiatan = t.NamaKegiatan,
                                                                             IDKegiatan = t.IDKegiatan,
                                                                             IDSubKegiatan = 0,
                                                                             IIDRekening = 0,
                                                                             KodeUK = t.KodeUK,
                                                                             IDProgram = t.IDProgram,
                                                                             IDUrusan = t.IDUrusan,
                                                                             AnggaranMurni = j.JumlahMurni,
                                                                             AnggaranGeser = j.JumlahGeser,
                                                                             AnggaranRKAP = j.JumlahRKAP,
                                                                             AnggaranABT = j.JumlahABT,
                                                                         }).ToList<ProgramKegiatanAnggaran>();
                oldKegiatan = 0;
                foreach (ProgramKegiatanAnggaran keg in lstPKegiatanDanAnggaran)
                {
                    if (keg.IDKegiatan != oldKegiatan)
                    {

                        string[] rowkegiatan = { keg.StrIDKegiatan , keg.NamaKegiatan + NamaUK,
                                                                                             keg.AnggaranMurni.ToRupiahInReport(), 
                                                                                             keg.AnggaranGeser.ToRupiahInReport(), 
                                                                                             keg.AnggaranRKAP.ToRupiahInReport(), 
                                                                                             keg.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_KEGIATAN.ToString(),
                                                                                            keg.IDUrusan.ToString(),
                                                                                            keg.IDProgram.ToString(),
                                                                                            keg.IDKegiatan.ToString(),
                                                                                            keg.IDSubKegiatan.ToString(),
                                                                                            keg.IIDRekening.ToString(),KodeUK.ToString()};

                        gridSPJ.Rows.Add(rowkegiatan);

                        oldKegiatan = keg.IDKegiatan;
                        ProcessSubKegiatan(oldKegiatan, KodeUK, NamaUK);
                        //nodekegiatan.Expand();


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan Display Anggaran tingkat Kegiatan " + ex.Message);

            }
        }
        private void ProcessSubKegiatan(int idKegiatan, int KodeUK, string NamaUK)
        {
            try
            {
                long oldSubKegiatan;
                oldSubKegiatan = 0;
                List<ProgramKegiatanAnggaran> lstKSubegiatan = new List<ProgramKegiatanAnggaran>();
                List<ProgramKegiatanAnggaran> lstDistinctSubKegiatan = new List<ProgramKegiatanAnggaran>();
                lstKSubegiatan = m_lstProgramKegiatan.FindAll(keg => keg.IDKegiatan == idKegiatan
                                                       && keg.IDDInas == m_IDSKPD && keg.KodeUK == KodeUK);

                lstKSubegiatan.OrderBy(x => x.IDSubKegiatan);


                foreach (ProgramKegiatanAnggaran subkegiatan in lstKSubegiatan)
                {
                    if (oldSubKegiatan != subkegiatan.IDSubKegiatan)
                    {
                        lstDistinctSubKegiatan.Add(subkegiatan);
                        oldSubKegiatan = subkegiatan.IDSubKegiatan;
                    }
                }

                var lstJumlahSubKegiatan = m_lstProgramKegiatan.FindAll(p => p.IDDInas == m_IDSKPD && p.KodeUK == KodeUK).GroupBy(x => x.IDSubKegiatan).OrderBy(x => x.Key)
                .Select(x => new
                {
                    IDSubKegiatan = x.Key,
                    JumlahMurni = x.Sum(y => y.AnggaranMurni),
                    JumlahGeser = x.Sum(y => y.AnggaranGeser),
                    JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                    JumlahABT = x.Sum(y => y.AnggaranABT),

                }).ToList();

                List<ProgramKegiatanAnggaran> lstPSubKegiatanDanAnggaran = (from t in lstDistinctSubKegiatan
                                                                            join j in lstJumlahSubKegiatan
                                                                            on t.IDSubKegiatan equals j.IDSubKegiatan
                                                                            select new ProgramKegiatanAnggaran
                                                                            {
                                                                                StrIDKegiatan = t.StrIDKegiatan,
                                                                                StrIDSubKegiatan = t.StrIDSubKegiatan,
                                                                                IDSubKegiatan = t.IDSubKegiatan,
                                                                                NamaSubKegiatan = t.NamaSubKegiatan,
                                                                                NamaKegiatan = t.NamaKegiatan,
                                                                                KodeUK = t.KodeUK,
                                                                                IDKegiatan = t.IDKegiatan,
                                                                                AnggaranMurni = j.JumlahMurni,
                                                                                AnggaranGeser = j.JumlahGeser,
                                                                                AnggaranRKAP = j.JumlahRKAP,
                                                                                AnggaranABT = j.JumlahABT,
                                                                            }).ToList<ProgramKegiatanAnggaran>();
                oldSubKegiatan = 0;
                foreach (ProgramKegiatanAnggaran subkeg in lstPSubKegiatanDanAnggaran)
                {
                    if (subkeg.IDSubKegiatan != oldSubKegiatan && subkeg.KodeUK == KodeUK)
                    {

                        string[] strSubkegiatan = {subkeg.StrIDSubKegiatan ,subkeg.NamaSubKegiatan + NamaUK,
                                        subkeg.AnggaranMurni.ToRupiahInReport(), subkeg.AnggaranGeser.ToRupiahInReport(),
                                        subkeg.AnggaranRKAP.ToRupiahInReport(), subkeg.AnggaranABT.ToRupiahInReport(), "0", "0", "0", LEVEL_SUBKEGIATAN.ToString(),
                                                                                            subkeg.IDUrusan.ToString(),
                                                                                            subkeg.IDProgram.ToString(),
                                                                                            subkeg.IDKegiatan.ToString(),
                                                                                            subkeg.IDSubKegiatan.ToString(),
                                                                                            subkeg.IIDRekening.ToString(),KodeUK.ToString()};
                        gridSPJ.Rows.Add(strSubkegiatan);
                        oldSubKegiatan = subkeg.IDSubKegiatan;
                        ProcessRekening(oldSubKegiatan, KodeUK);


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan Display Anggaran tingkat SubKegiatan " + ex.Message);
            }
        }
        private void ProcessRekening(long IDSubKegiatan, int KodeUK)
        {
            try
            {
                List<ProgramKegiatanAnggaran> lstRekening = new List<ProgramKegiatanAnggaran>();
                List<ProgramKegiatanAnggaran> lstDistinctSubKegiatan = new List<ProgramKegiatanAnggaran>();
                lstRekening = m_lstProgramKegiatan.FindAll(rek => rek.IDSubKegiatan == IDSubKegiatan
                                              && rek.IDDInas == m_IDSKPD && rek.KodeUK == KodeUK);

                lstRekening.OrderBy(x => x.IIDRekening);

                foreach (ProgramKegiatanAnggaran rek in lstRekening)
                {


                    string[] row = { rek.IIDRekening.ToKodeRekening() ,rek.NamaRekening,
                                    rek.AnggaranMurni.ToRupiahInReport(), rek.AnggaranGeser.ToRupiahInReport(),
                                    rek.AnggaranRKAP.ToRupiahInReport(), rek.AnggaranABT.ToRupiahInReport(), "0", "0", "0", LEVEL_REKANING.ToString(),
                                                                                            rek.IDUrusan.ToString(),
                                                                                            rek.IDProgram.ToString(),
                                                                                            rek.IDKegiatan.ToString(),
                                                                                            rek.IDSubKegiatan.ToString(),
                                                                                            rek.IIDRekening.ToString(), KodeUK.ToString(),"0"};
                    gridSPJ.Rows.Add(row);

                    mAnggJumlah.NamaRekening = "JUMLAH";
                    mAnggJumlah.AnggaranMurni = mAnggJumlah.AnggaranMurni + rek.AnggaranMurni;
                    mAnggJumlah.AnggaranGeser = mAnggJumlah.AnggaranGeser + rek.AnggaranGeser;
                    mAnggJumlah.AnggaranRKAP = mAnggJumlah.AnggaranRKAP + rek.AnggaranRKAP;
                    mAnggJumlah.AnggaranABT = mAnggJumlah.AnggaranABT + rek.AnggaranABT;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan Display Anggaran tingkat Rekening " + ex.Message);
            }
        }
        #endregion

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {

        }

        private void cmdLoadData_Click(object sender, EventArgs e)
        {
            try
            {//..
                gridSPJ.Rows.Clear();
                if (ctrlTahapAnggaran1.ID == 0)
                {
                    MessageBox.Show("Belum memilih Tahapan Anggaran");
                    return;
                }
                if (ctrlTanggalBulan1.CekPilihan() == true)
                {
                    GetTanggal();
                }
                else
                    return;





                LoadData();


                HideColumnAnggaran();
                m_iRowPerbandinganSPJLRA = -1;
                gridPerbandinganLRABKU.Rows.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ctrlDinas1_Load_1(object sender, EventArgs e)
        {

        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridSPJ.Rows)
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
                    gridSPJ.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridSPJ.CurrentCell =
                        containingCells[currentContainingCellListIndex++];

        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {

            try
            {
                //Create a new PDF document.
                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                section1.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section1.PageSettings.Height = 935;// = PdfPageSize.Legal;

                section1.PageSettings.Orientation = PdfPageOrientation.Landscape;
                document.PageSettings.Margins.Bottom = 0;
                PdfPage page = section1.Pages.Add();
                previousPage = page;
                document.Pages.PageAdded += Pages_PageAdded;
                halaman = 1;
                SaatnyacetakKesimpulan = false;

                CetakPDF oCetakPDF = new CetakPDF();
                float yPos;
                float kiri = 20;

                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                    page.GetClientSize().Width, stringFormat, true, false, true);

                if (m_nMode == 1)
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SPJ FUNGSIONAL" +
                             GlobalVar.TahunAnggaran.ToString(), 10, kiri, yPos,
                             page.GetClientSize().Width, stringFormat, true, false, true);

                else
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SPJ ADMINISTRATIF" +
                         GlobalVar.TahunAnggaran.ToString(), 10, kiri, yPos,
                         page.GetClientSize().Width, stringFormat, true, false, true);
                stringFormat.Alignment = PdfTextAlignment.Left;

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          ctrlDinas1.GetNamaSKPD(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);
                Pejabat bendahara = ctrlDinas1.GetBendaharaPengeluaran(mTanggalAkhir);

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
                          ctrlTanggalBulan1.Waktu, 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Tahun Anggaran"
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          GlobalVar.TahunAnggaran.ToString(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);


                PdfGrid pdfGridHeader = new PdfGrid();
                DataTable tableHeader = new DataTable();
                tableHeader.Columns.Add("Kode ");
                tableHeader.Columns.Add("Uraian");
                tableHeader.Columns.Add("Anggaran");
                tableHeader.Columns.Add("Gaji lalu");
                tableHeader.Columns.Add("Gaji Kini");
                tableHeader.Columns.Add("Jumlah Gaji ");
                tableHeader.Columns.Add("Barang & Jasa Lalu");
                tableHeader.Columns.Add("Barang & Jasa Kini");
                tableHeader.Columns.Add("Jumlah Barang & Jasa");
                tableHeader.Columns.Add("UP/GU/TU Lalu");
                tableHeader.Columns.Add("UP/GU/TU Kini");
                tableHeader.Columns.Add("Jumlah UP/GU/TU");

                tableHeader.Columns.Add("Jumlah SPJ");
                tableHeader.Columns.Add("Sisa Anggaran");

                tableHeader.Rows.Add(new string[]
                    {            " Kode      Rekening"," Uraian","Jumlah Anggaran","SPJ LS Gaji","SPJ LS Gaji",
                                "SPJ LS Gaji","SPJ LS Barang Jasa","SPJ LS Barang Jasa","SPJ LS Barang Jasa",
                                "SPJ UP/GU/TU","SPJ UP/GU/TU","SPJ UP/GU/TU","Jumlah SPJ Smp Bln Ini","Sisa Pagu Anggaran"});

                tableHeader.Rows.Add(new string[]
                    {            "Kode Rekening","Uraian","Jumlah Anggaran","s/d Bln Lalu","Bulan Ini",
                                "sd Bulan Ini","s/d Bln Lalu","Bulan Ini","s/d Bln Ini",
                                "s/d Bln Lalu","Bulan Ini","s/d Bln Ini","Smp Bln Ini","Anggaran"});

                // pdfGridHeader.Rows[1].Cells[2].RowSpan = 1;
                // pdfGridHeader.Rows[0].Cells[3].ColumnSpan= 3;
                // pdfGridHeader.Rows[0].Cells[6].ColumnSpan = 3;
                //pdfGridHeader.Rows[0].Cells[9].ColumnSpan = 3;

                pdfGridHeader.DataSource = tableHeader; //data
                pdfGridHeader.Columns[0].Width = 60;
                pdfGridHeader.Columns[1].Width = 100;

                // Angka 
                pdfGridHeader.Columns[2].Width = 60;
                pdfGridHeader.Columns[3].Width = 55;
                pdfGridHeader.Columns[4].Width = 55;
                pdfGridHeader.Columns[5].Width = 55;
                pdfGridHeader.Columns[6].Width = 55;
                pdfGridHeader.Columns[7].Width = 55;
                pdfGridHeader.Columns[8].Width = 55;
                pdfGridHeader.Columns[9].Width = 55;
                pdfGridHeader.Columns[10].Width = 55;
                pdfGridHeader.Columns[11].Width = 55;
                pdfGridHeader.Columns[12].Width = 55;

                pdfGridHeader.Rows[0].Cells[0].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[1].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[2].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[12].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[13].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[3].ColumnSpan = 3;
                pdfGridHeader.Rows[0].Cells[6].ColumnSpan = 3;
                pdfGridHeader.Rows[0].Cells[9].ColumnSpan = 3;

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


                #region spjAtas

                PdfGrid pdfGrid = new PdfGrid();
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("1");
                table.Columns.Add("2");
                table.Columns.Add("3");
                table.Columns.Add("4");
                table.Columns.Add("5");
                table.Columns.Add("6=4+5");
                table.Columns.Add("7");
                table.Columns.Add("8");
                table.Columns.Add("9=7+8");
                table.Columns.Add("10");
                table.Columns.Add("11");
                table.Columns.Add("12=10+11");

                table.Columns.Add("13=6+9+12");
                table.Columns.Add("14=9-12");

                //table.Columns.Add("Level");



                //table. Columns[0]
                //Assign Column count
                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();
                switch (ctrlTahapAnggaran1.ID)
                {
                    case 2:
                        mcolAnggaran = COL_ANGGARANMURNI;
                        break;
                    case 3:
                        mcolAnggaran = COL_ANGGARANGESER;
                        break;
                    case 4:
                        mcolAnggaran = COL_ANGGARANRKAP;
                        break;
                    case 5:
                        mcolAnggaran = COL_ANGGARANABT;
                        break;
                }



                decimal akumulasi = 0L;
                decimal sisa = 0;

                for (int idx = 0; idx < gridSPJ.Rows.Count; idx++)
                {
                    if (gridSPJ.Rows[idx].Cells[1].Value != null)
                    {

                        table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[0].Value),
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[1].Value),                      
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[mcolAnggaran].Value),
        

                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[COL_GL].Value),
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[COL_GK].Value),
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[COL_GTot].Value),
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[COL_BL].Value),
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[COL_BK].Value),
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[COL_BTot].Value),
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[COL_UL].Value),
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[COL_UK].Value),
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[COL_UTot].Value),
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[COL_SPJTot].Value),
                       DataFormat.GetString(gridSPJ.Rows[idx].Cells[COL_SISASPJ].Value),
                       

                       
                        
                    });
                    }


                }

                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 60;
                pdfGrid.Columns[1].Width = 100;

                // Angka 
                pdfGrid.Columns[2].Width = 60;
                pdfGrid.Columns[3].Width = 55;
                pdfGrid.Columns[4].Width = 55;
                pdfGrid.Columns[5].Width = 55;
                pdfGrid.Columns[6].Width = 55;
                pdfGrid.Columns[7].Width = 55;
                pdfGrid.Columns[8].Width = 55;
                pdfGrid.Columns[9].Width = 55;
                pdfGrid.Columns[10].Width = 55;
                pdfGrid.Columns[11].Width = 55;
                pdfGrid.Columns[12].Width = 55;



                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding

                gridStyle.CellPadding = new PdfPaddings(3, 1, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                pdfGrid.Style = gridStyle;
                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                for (int col = 2; col < pdfGrid.Columns.Count; col++)
                    pdfGrid.Columns[col].Format = formatKolomAngka;

                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));
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
                    pdfGrid.Headers[c].Height = 20;

                }


                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                cellStyle.Borders.All = new PdfPen(new PdfColor(192, 192, 217), 0.50f);
                cellStyle.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 6, FontStyle.Regular));
                for (int idx = 0; idx < pdfGrid.Rows.Count; idx++)
                {
                    pdfGrid.Rows[idx].Style = cellStyle;
                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.1F;
                    }


                    //    pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 6, FontStyle.Bold)); 


                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(pdfHeaderGridResult.Page, new PointF(kiri, yPos));
                yPos = pdfGridLayoutResult.Bounds.Bottom;
                #endregion spjAtas
                #region PenerimaandanPengeluaran
                PdfGrid pdfGridBawah = new PdfGrid();

                //Create a DataTable
                table = new DataTable();
                //Add columns to table
                table.Columns.Add("1");
                table.Columns.Add("2");
                table.Columns.Add("3");
                table.Columns.Add("4");
                table.Columns.Add("5");
                table.Columns.Add("6=4+5");
                table.Columns.Add("7");
                table.Columns.Add("8");
                table.Columns.Add("9=7+8");
                table.Columns.Add("10");
                table.Columns.Add("11");
                table.Columns.Add("12=10+11");

                table.Columns.Add("13=6+9+12");
                table.Columns.Add(".");


                columnCount = table.Columns.Count;
                data = new List<object>();




                for (int idx = 0; idx < gridPenerimaanPengeluaran.Rows.Count; idx++)
                {
                    if (gridPenerimaanPengeluaran.Rows[idx].Cells[1].Value != null)
                    {

                        table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[0].Value),
                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[1].Value), "",                     
                       
                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[2].Value),
                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[3].Value),
                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[4].Value),
                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[5].Value),
                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[6].Value),
                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[7].Value),
                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[8].Value),
                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[9].Value),
     
                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[10].Value),
                       DataFormat.GetString(gridPenerimaanPengeluaran.Rows[idx].Cells[11].Value),
                      
                       

                       
                        
                    });
                    }


                }

                pdfGridBawah.DataSource = table; //data


                pdfGridBawah.Columns[0].Width = 60;
                pdfGridBawah.Columns[1].Width = 100;

                // Angka 
                pdfGridBawah.Columns[2].Width = 60;
                pdfGridBawah.Columns[3].Width = 55;
                pdfGridBawah.Columns[4].Width = 55;
                pdfGridBawah.Columns[5].Width = 55;
                pdfGridBawah.Columns[6].Width = 55;
                pdfGridBawah.Columns[7].Width = 55;
                pdfGridBawah.Columns[8].Width = 55;
                pdfGridBawah.Columns[9].Width = 55;
                pdfGridBawah.Columns[10].Width = 55;
                pdfGridBawah.Columns[11].Width = 55;
                pdfGridBawah.Columns[12].Width = 55;



                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(1, 0.5F, 1, 1);//DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));


                pdfGrid.Style = gridStyle;


                formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;
                for (int col = 2; col < pdfGridBawah.Columns.Count; col++)
                    pdfGridBawah.Columns[col].Format = formatKolomAngka;

                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));
                cellHeaderStyle = new PdfGridCellStyle();

                pdfGridBawah.RepeatHeader = false;


                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", font.Size, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));
                cellHeaderStyle.Font = font;
                cellHeaderStyle.StringFormat = stringFormatHeader;
                for (int c = 0; c < pdfGridBawah.Headers.Count; c++)
                {
                    pdfGridBawah.Headers[c].ApplyStyle(cellHeaderStyle);
                    pdfGridBawah.Headers[c].Height = 10;

                }


                cellStyle = new PdfGridCellStyle();
                cellStyle.Borders.All = new PdfPen(new PdfColor(192, 192, 217), 0.50f);
                cellStyle.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 7, FontStyle.Regular));

                for (int idx = 0; idx < pdfGridBawah.Rows.Count; idx++)
                {
                    pdfGridBawah.Rows[idx].Style = cellStyle;

                    //    pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 6, FontStyle.Bold)); 


                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.
                //pdfGridHeader
                for (int row = 0; row < 2; row++)
                {
                    pdfGridHeader.Rows[row].Cells[1].ColumnSpan = 2;
                }

                PdfGridLayoutResult pdfGridLayoutResult3 = pdfGridHeader.Draw(pdfGridLayoutResult.Page, new PointF(kiri, pdfGridLayoutResult.Bounds.Bottom));

                for (int row = 0; row < pdfGridBawah.Rows.Count; row++)
                {
                    pdfGridBawah.Rows[row].Cells[1].ColumnSpan = 2;
                }
                pdfGridBawah.Headers.Clear();
                pdfGridLayoutResult = pdfGridBawah.Draw(pdfGridLayoutResult3.Page, new PointF(kiri, pdfGridLayoutResult3.Bounds.Bottom));


                #endregion PenerimaanPengeluaran

                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();

                //System.Diagnostics.Process.Start(namaFile);


                //string namaFile = Path.GetFullPath(@"../../../SPD_" + txtINO.Text.Trim() + "_" + ctrlSKPD1.GetNamaSKPD() + ".pdf");
                string namaFile = Path.GetFullPath(@"../../../SPJ.pdf");

                //using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPD.pdf"), FileMode.Create, FileAccess.ReadWrite))
                using (FileStream outputFileStream = new FileStream(namaFile, FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);

                ////
                //if (chkDefaultPrinter.Checked == true)
                //{
                //    System.Diagnostics.Process.Start(namaFile);
                //}
                //else
                //{
                pdfViewer pV = new pdfViewer();
                pV.Document = namaFile;// Path.GetFullPath(@"../../../BKU.pdf");
                pV.Show();
                // }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Pages_PageAdded(object sender, PageAddedEventArgs args)
        {
            float yPos = PosisiTerakhir + 5;

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;
            PdfStringFormat stringFormat = new PdfStringFormat();


            CetakPDF oCetakPDF = new CetakPDF();


            if (SaatnyacetakKesimpulan == true)
            {



                Pejabat bendahara = new Pejabat();
                Pejabat pimpinan = new Pejabat();

                bendahara = ctrlDinas1.GetBendaharaPengeluaran(ctrlTanggalBulan1.TanggalAkhir);
                pimpinan = ctrlDinas1.GetPimpinan(ctrlTanggalBulan1.TanggalAkhir);
                yPos = yPos + 10;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.gPemda.Ibukota + "," + dtCetak.Value.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Jabatan, 10, 30, yPos, setengah, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Jabatan, 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Nama, 10, 30, yPos, setengah, stringFormat, false, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + pimpinan.NIP, 10, 30, yPos, setengah, stringFormat, false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + bendahara.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);




            }



            previousPage = args.Page;


            previousPage = args.Page;
        }

        private void manuSPJ_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Copy" && gridSPJ.CurrentCell.Value != null)
            {
                Clipboard.SetDataObject(gridSPJ.CurrentCell.Value.ToString(), false);
            }
        }

        private void gridSPJ_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                gridSPJ.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip = manuSPJ;
                gridSPJ.CurrentCell = gridSPJ.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cmdPerbandinganLRAdanBKU_Click(object sender, EventArgs e)
        {

            try
            {
                if (m_IDSKPD != ctrlDinas1.GetID())
                {
                    MessageBox.Show("Dinas beda dengan Dinas untk SPJFungsional. Data di SPJ akan di bersihkan.. ");
                    gridSPJ.Rows.Clear();
                    gridPenerimaanPengeluaran.Rows.Clear();
                }
                m_IDSKPD = ctrlDinas1.GetID();
                GetTanggal();

                gridPerbandinganLRABKU.Rows.Clear();
                txtJumlahLRA.Text = "0";
                txtJumlahSPJ.Text = "0";
                decimal JumlahLRA = 0;
                decimal JumlahSPJ = 0;
                SPJLogic oSPJLogic = new SPJLogic(GlobalVar.TahunAnggaran);
                mFungsionalList = oSPJLogic.GetFungsionalDariBKU(m_IDSKPD, mTanggalAwal, mTanggalAkhir);
                // dari BKU 
                List<FungsionalDanLRA> lst = new List<FungsionalDanLRA>();
                lst = oSPJLogic.PerbandinganSPJdanLRA(m_IDSKPD, mTanggalAkhir);
                foreach (FungsionalRekening f in mFungsionalList) {
                    if (lst != null)
                    {
                        int i = 0;

                        foreach (FungsionalDanLRA BKULRA in lst)
                        {
                            if (BKULRA.KdeUK == f.KodeUK && BKULRA.IDSubKegiatanF  == f.IDSubKegiatan
                                && BKULRA.IDRekeningF == f.IDRekening)
                            {
                                string[] row = {
                            BKULRA.KdeUK.ToString(),
                            BKULRA.IDDubKegiatanLRA.ToKodeSubKegiatan(),
                            BKULRA.IDRekeningLRA.ToKodeRekening(),
                            BKULRA.IDRekeningLRA==0? BKULRA.NamaSubKegiatan:BKULRA.NamaRekening,

                            BKULRA.LRA.ToRupiahInReport(),BKULRA.BKU.ToRupiahInReport()};
                                gridPerbandinganLRABKU.Rows.Add(row);
                                if (BKULRA.IDRekeningLRA > 0)
                                {
                                    JumlahLRA = JumlahLRA + BKULRA.LRA;
                                    JumlahSPJ = JumlahSPJ + BKULRA.BKU;
                                }
                                if (BKULRA.LRA != BKULRA.BKU)
                                {

                                    gridPerbandinganLRABKU.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;


                                }

                                i++;

                                txtJumlahLRA.Text = JumlahLRA.ToRupiahInReport();
                                txtJumlahSPJ.Text = JumlahSPJ.ToRupiahInReport();
                                if (JumlahLRA != JumlahSPJ)
                                {
                                    txtJumlahLRA.BackColor = Color.Red;
                                    txtJumlahSPJ.BackColor = Color.Red;
                                }
                                else
                                {
                                    txtJumlahLRA.BackColor = Color.White;
                                    txtJumlahSPJ.BackColor = Color.White;
                                }
                            }
                        }
                    }
                }
                //else
                //{
                //    MessageBox.Show(oSPJLogic.LastError());
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void gridPerbandinganLRABKU_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            m_iRowPerbandinganSPJLRA = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < gridPerbandinganLRABKU.Rows.Count)
            {
                lblSUbKegiatan.Text = DataFormat.GetString(gridPerbandinganLRABKU.Rows[e.RowIndex].Cells[0].Value) + ": " +
                      DataFormat.GetString(gridPerbandinganLRABKU.Rows[e.RowIndex].Cells[1].Value);
                lblRekening.Text = DataFormat.GetString(gridPerbandinganLRABKU.Rows[e.RowIndex].Cells[2].Value) + ": " +
                      DataFormat.GetString(gridPerbandinganLRABKU.Rows[e.RowIndex].Cells[3].Value);
            }
        }
        private void GetDataLRARinci(int i)
        {

            try
            {

                gridLRA.Rows.Clear();

                SPJLogic oSPJLogic = new SPJLogic(GlobalVar.TahunAnggaran);
                long idsubkegiatan = DataFormat.GetLong(DataFormat.GetString(gridPerbandinganLRABKU.Rows[i].Cells[0].Value).Replace(".", ""));
                long idRekening = DataFormat.GetLong(DataFormat.GetString(gridPerbandinganLRABKU.Rows[i].Cells[1].Value).Replace(".", ""));


                List<FungsionalDanLRA> lst = new List<FungsionalDanLRA>();
                lst = oSPJLogic.GetListLRA(m_IDSKPD, idsubkegiatan, idRekening, mTanggalAkhir);
                if (lst != null)
                {
                    foreach (FungsionalDanLRA BKULRA in lst)
                    {

                        string[] row = { BKULRA.NoUrut.ToString(), BKULRA.Tabel, BKULRA.NoBukti, BKULRA.Tanggal.ToTanggalIndonesia(),
                                       BKULRA.LRA.ToRupiahInReport(),"0"};
                        gridLRA.Rows.Add(row);
                    }
                }
                else
                {
                    MessageBox.Show(oSPJLogic.LastError());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void GetDataSPJRinci(int i)
        {

            try
            {

                gridBKU.Rows.Clear();

                SPJLogic oSPJLogic = new SPJLogic(GlobalVar.TahunAnggaran);
                long idsubkegiatan = DataFormat.GetLong(DataFormat.GetString(gridPerbandinganLRABKU.Rows[i].Cells[0].Value).Replace(".", ""));
                long idRekening = DataFormat.GetLong(DataFormat.GetString(gridPerbandinganLRABKU.Rows[i].Cells[1].Value).Replace(".", ""));


                List<FungsionalDanLRA> lst = new List<FungsionalDanLRA>();
                lst = oSPJLogic.GetListSPJ(m_IDSKPD, idsubkegiatan, idRekening, mTanggalAkhir);
                if (lst != null)
                {
                    foreach (FungsionalDanLRA BKULRA in lst)
                    {

                        string[] row = { BKULRA.NoUrut.ToString(), BKULRA.NoBukti, BKULRA.Tanggal.ToTanggalIndonesia(), BKULRA.NoBKU.ToString(), BKULRA.BKU.ToRupiahInReport(),"0" };
                        gridBKU.Rows.Add(row);
                    }
                }
                else
                {
                    MessageBox.Show(oSPJLogic.LastError());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void cmdCek_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_iRowPerbandinganSPJLRA < 0 || m_iRowPerbandinganSPJLRA >= gridPerbandinganLRABKU.Rows.Count)
                {
                    MessageBox.Show("Silakan pilih baris di tabel perbandingan samping..");
                    return;
                }
                GetDataLRARinci(m_iRowPerbandinganSPJLRA);
                GetDataSPJRinci(m_iRowPerbandinganSPJLRA);
                int idx = 0;
                foreach (DataGridViewRow row in gridLRA.Rows)
                {
                    long noUrut = DataFormat.GetLong(row.Cells[0].Value);
                    bool bFound = false;
                    foreach (DataGridViewRow rowSpj in gridBKU.Rows)
                    {
                        long noUrutSPJ = DataFormat.GetLong(rowSpj.Cells[0].Value);
                        if (noUrutSPJ == noUrut)
                        {
                            bFound = true;
                        }

                    }
                    if (bFound == false)
                    {
                        gridLRA.Rows[idx].DefaultCellStyle.BackColor = Color.IndianRed;
                        gridLRA.Rows[idx].Cells[5].Value = "1";
                    }
                    idx++;

                }
                for (int i = 0; i< gridLRA.Rows.Count-1; i++)
                {
                    if (DataFormat.GetInteger(gridLRA.Rows[i].Cells[5].Value) == 1)
                    {
                        gridLRA.Rows[i].Visible = true;
                    }
                    else
                    {
                        gridLRA.Rows[i].Visible = false;
                    }
                }

                foreach (DataGridViewRow rowSpj in gridBKU.Rows)
                {
                    long noUrut = DataFormat.GetLong(rowSpj.Cells[0].Value);
                    bool bFound = false;
                    idx = 0;
                    foreach (DataGridViewRow row in gridLRA.Rows)
                    {
                        long noUrutSPJ = DataFormat.GetLong(row.Cells[0].Value);
                        if (noUrutSPJ == noUrut)
                        {
                            bFound = true;
                        }

                    }
                    if (bFound == false)
                    {
                        gridBKU.Rows[idx].DefaultCellStyle.BackColor = Color.IndianRed;
                        gridBKU.Rows[idx].Cells[5].Value = "1";
                    }
                    idx++;

                }
                for (int idxspj = 0 ; idxspj < gridBKU.Rows.Count-1;idxspj++)
                {
                    if (DataFormat.GetInteger(gridBKU.Rows[idxspj].Cells[5].Value) == 1)
                    {
                        gridBKU.Rows[idxspj].Visible= true ;
                    } else {
                        gridBKU.Rows[idxspj].Visible= false  ;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          }

        private void manuSPJ_Opening(object sender, CancelEventArgs e)
        {

        }

        private void gridSPJ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdCekNegatid_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gridSPJ.Rows)
                {
                    
                    if (DataFormat.GetString(row.Cells[COL_SISASPJ].Value).Contains("(")==false
                        )
                    {
                        row.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void ctrlTahapAnggaran1_Load(object sender, EventArgs e)
        {

        }
    }
    

}
