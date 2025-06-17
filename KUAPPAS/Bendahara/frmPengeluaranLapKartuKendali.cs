using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace KUAPPAS.Bendahara
{
    public partial class frmPengeluaranLapKartuKendali : ChildForm
    {

        int m_IDSKPD;
        private List<ProgramKegiatanAnggaran> m_lstProgramKegiatan;
        private List<KartuKendali> mKartuKendaliList;
   
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

        private const int COL_SP2DUP = 17;
        private const int COL_SP2DGU = 18;
        private const int COL_SP2DTU = 19;
        private const int COL_SP2DLS = 20;
        private const int COL_JUMLAHSP2D = 21;

        private const int COL_RUPGU = 22;
        private const int COL_RUPTU = 23;
        private const int COL_RUPLS = 24;

        private const int COL_JUMLAHREALISASI = 25;
        private const int COL_SISAKAS = 26;
        private const int COL_SISAPAGUKEGIATAN = 27;
        private const int COL_SISAPAGUBELANJA = 28;


        private decimal JUMLAH_ANGGARAN = 0;
        private decimal JUMLAH_SP2DUP = 0;
        private decimal JUMLAH_SP2DGU = 0;
        private decimal JUMLAH_SP2DTU = 0;
        private decimal JUMLAH_SP2DLS = 0;
        private decimal JUMLAH_JUMLAHSP2D = 0;

        private decimal JUMLAH_RUPGU = 0;
        private decimal JUMLAH_RUPTU = 0;
        private decimal JUMLAH_RUPLS = 0;


        private const int LEVEL_DINAS = 0;
        private const int LEVEL_UNIT = 1;
        private const int LEVEL_URUSAN = 2;
        private const int LEVEL_PROGRAM = 3;
        private const int LEVEL_KEGIATAN = 4;
        private const int LEVEL_SUBKEGIATAN = 5;
        private const int LEVEL_REKANING = 6;



        private decimal JumlahUP;
        private decimal m_cJumlahDPA;
        private decimal m_cJumlahSPJSebelum;
        private decimal m_cJumlahSISADANASEBELUM;
        private decimal m_cJumlahSPJ;
        private decimal m_cJumlahSISADPA;
        private decimal m_cJumlahTotalSPJ;

        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _level2style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level3style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level4style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level5style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level6style = new DataGridViewCellStyle();

        DataGridViewCellStyle _level7style = new DataGridViewCellStyle();
        DataGridViewCellStyle _redstyle = new DataGridViewCellStyle();

        DataGridViewCellStyle _normalstyle = new DataGridViewCellStyle();

        //public delegate void GetKegiatan();
        //public GetKegiatan getKegiatan;
        private int prevJenis = 0;
        private int preDinas = 0;
        private int m_iRowTerakhir;
  
        private Single m_iStatus;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        PdfPage previousPage;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        CetakPDF m_oCetakPDF;
        int halaman;
        DateTime mTanggalAwal;
       

        public frmPengeluaranLapKartuKendali()
        {
            InitializeComponent();
        }

        private void frmKartuKendali_Load(object sender, EventArgs e)
        {
            try
            {
                ctrlHeader1.SetCaption("Kartu Kendali Kegiatan.");
                ctrlTanggal1.Tanggal = DateTime.Now.Date;
                ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
                if (GlobalVar.Pengguna.SKPD > 0)
                {
                    ctrlSKPD1.SetID(GlobalVar.Pengguna.SKPD);
                    m_IDSKPD = GlobalVar.Pengguna.SKPD;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
            try
            {//..
                gridKartuKendali.Rows.Clear();
                JumlahUP = 0;
                LoadData();
                SembunyikanKolomAnggaranBukanPilihan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SembunyikanKolomAnggaranBukanPilihan()
        {
                gridKartuKendali.Columns[COL_ANGGARANMURNI].Visible = true;
                gridKartuKendali.Columns[COL_ANGGARANGESER].Visible = true;
                gridKartuKendali.Columns[COL_ANGGARANRKAP].Visible = true;
                gridKartuKendali.Columns[COL_ANGGARANABT].Visible = true;

            
            if (mcolAnggaran != COL_ANGGARANMURNI)
                gridKartuKendali.Columns[COL_ANGGARANMURNI].Visible = false;

            if (mcolAnggaran != COL_ANGGARANGESER)
                gridKartuKendali.Columns[COL_ANGGARANGESER].Visible = false;

            if (mcolAnggaran != COL_ANGGARANRKAP)
                gridKartuKendali.Columns[COL_ANGGARANRKAP].Visible = false;

            if (mcolAnggaran != COL_ANGGARANABT)
                gridKartuKendali.Columns[COL_ANGGARANABT].Visible = false;

 

        }
        private void LoadData()
        {
            try
            {
                if (LoadProgramKegiatan())
                {
                    if (DisplayProgramKegiatanSubKegiatan() == true)
                    {
                       m_iRowTerakhir = gridKartuKendali.Rows.Count;
                       DisplayKendali();
                    }
                    RefreshSisaPagu();

                    FormatGrid();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        #region Kartukendali
        private void DisplayKendali()
        {
            try
            {
                //  mKartuKendaliList
                mKartuKendaliList = new List<KartuKendali>();
                KartuKendaliLogic kkLogic = new KartuKendaliLogic(GlobalVar.TahunAnggaran);
                DateTime tanggal = ctrlTanggal1.Tanggal;
                mKartuKendaliList = kkLogic.GetDataKendali(m_IDSKPD, tanggal);

                if (mKartuKendaliList != null)
                {
                    DisplayDetail();
                   
                }
                else
                {
                    MessageBox.Show(kkLogic.LastError());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan menampilkan data Kendali.." + ex.Message);

            }

        }
        #endregion Kartukendali 
        #region formatting
        private int GetLevel(int Baris)
        {
            return DataFormat.GetInteger(gridKartuKendali.Rows[Baris].Cells[COL_LEVEL].Value);
        }
        private void FormatGrid()
        {
            try
            {
                FontStyle styleFont = new FontStyle();

                _hilightstyle.Font = new System.Drawing.Font(gridKartuKendali.Font, FontStyle.Bold);
                _hilightstyle.ForeColor = Color.White;

                _hilightstyle.BackColor = Color.LightSlateGray;


                _level2style.Font = new System.Drawing.Font(gridKartuKendali.Font, FontStyle.Bold);
                _level2style.BackColor = Color.LightSteelBlue;

                _level3style.Font = new System.Drawing.Font(gridKartuKendali.Font, FontStyle.Bold);
                _level3style.BackColor = Color.LightSteelBlue;// new Font(gridKUA.Font, FontStyle.Bold);

                _level4style.Font = new System.Drawing.Font(gridKartuKendali.Font, FontStyle.Regular);
                _level4style.BackColor = Color.LightGray;// new Font(gridKUA.Font, FontStyle.Bold);

                _level5style.Font = new System.Drawing.Font(gridKartuKendali.Font, FontStyle.Italic);
                _level5style.BackColor = Color.Lavender;// new Font(gridKUA.Font, FontStyle.Bold);
                _level6style.Font = new System.Drawing.Font(gridKartuKendali.Font, FontStyle.Italic);
                _level6style.BackColor = Color.Honeydew;// new Font(gridKUA.Font, FontStyle.Bold);

                _level7style.Font = new System.Drawing.Font(gridKartuKendali.Font, FontStyle.Regular);

                _level7style.BackColor = Color.Honeydew;// new Font(gridKUA.Font, FontStyle.Bold);

                for (int idx = 0; idx < gridKartuKendali.Rows.Count; idx++)
                {
                    int level = GetLevel(idx);

                    switch (level)
                    {
                        case LEVEL_DINAS:
                            gridKartuKendali.Rows[idx].DefaultCellStyle = _hilightstyle;

                            break;
                        case LEVEL_UNIT:
                            gridKartuKendali.Rows[idx].DefaultCellStyle = _level2style;
                            break;

                        case LEVEL_URUSAN:
                            gridKartuKendali.Rows[idx].DefaultCellStyle = _level3style;
                            break;

                        case LEVEL_PROGRAM:
                            gridKartuKendali.Rows[idx].DefaultCellStyle = _level4style;
                            break;
                        case LEVEL_KEGIATAN:
                            gridKartuKendali.Rows[idx].DefaultCellStyle = _level5style;
                            break;
                        case LEVEL_SUBKEGIATAN:
                            gridKartuKendali.Rows[idx].DefaultCellStyle = _level6style;
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion formatting
        #region Pemanggilan_Anggaran
        public bool LoadProgramKegiatan()
        {



            //  m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
            ProgramKegiatanAnggaranLogic oLogic = new ProgramKegiatanAnggaranLogic(GlobalVar.TahunAnggaran);
            m_IDSKPD = ctrlSKPD1.GetID();
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

                gridKartuKendali.Rows.Clear();
                // ***********************************
                m_IDSKPD = ctrlSKPD1.GetID();
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
                             m_IDSKPD.ToString(), ctrlSKPD1.GetNamaSKPD(),                                                                                     totalOPD.AnggaranMurni.ToRupiahInReport(), 
                                                                                            totalOPD.AnggaranGeser.ToRupiahInReport(), 
                                                                                            totalOPD.AnggaranRKAP.ToRupiahInReport(), 
                                                                                            totalOPD.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_DINAS.ToString(),
                                                                                            "0",
                                                                                            "0",
                                                                                            "0",
                                                                                            "0",
                                                                                            "0" ,"0" };
                gridKartuKendali.Rows.Add(rowOPD);
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
                            gridKartuKendali.Rows.Add(rowOPD);
                        }


                        ProcessUrusan(p.KodeUK, parameterNamaUK);


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

                             p.StrIDUrusan , "Urusan " + p.NamaUrusan +  NamaUK, 
                                                                                            p.AnggaranMurni.ToRupiahInReport(), 
                                                                                            p.AnggaranGeser.ToRupiahInReport(), 
                                                                                            p.AnggaranRKAP.ToRupiahInReport(), 
                                                                                            p.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_URUSAN.ToString(),
                                                                                            p.IDUrusan.ToString(),
                                                                                            p.IDProgram.ToString(),
                                                                                            p.IDKegiatan.ToString(),
                                                                                            p.IDSubKegiatan.ToString(),
                                                                                            p.IIDRekening.ToString(),KodeUK.ToString() };
                        gridKartuKendali.Rows.Add(row);

                        ProcessProgram(p.IDUrusan, KodeUK, NamaUK);
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
                        gridKartuKendali.Rows.Add(rowProgram);

                        oldIdProgram = pr.IDProgram;
                        ProcessKegiatan(oldIdProgram, idUrusan, KodeUK, NamaUK);
                        //     nodeprogram.Expand();
                    }
                }
            }




            catch (Exception ex)
            {

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

                lstKegiatan = m_lstProgramKegiatan.FindAll(keg => keg.IDDInas == m_IDSKPD && keg.IDProgram == idProgram && keg.KodeUK == KodeUK);

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

                        gridKartuKendali.Rows.Add(rowkegiatan);

                        oldKegiatan = keg.IDKegiatan;
                        ProcessSubKegiatan(oldKegiatan, KodeUK, NamaUK);
                        //nodekegiatan.Expand();


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void ProcessSubKegiatan(int idKegiatan, int KodeUK, string NamaUK)
        {
            long oldSubKegiatan;
            oldSubKegiatan = 0;
            List<ProgramKegiatanAnggaran> lstKSubegiatan = new List<ProgramKegiatanAnggaran>();
            List<ProgramKegiatanAnggaran> lstDistinctSubKegiatan = new List<ProgramKegiatanAnggaran>();
            lstKSubegiatan = m_lstProgramKegiatan.FindAll(keg => keg.IDKegiatan == idKegiatan
                                                   && keg.IDDInas == m_IDSKPD
                                                  && keg.KodeUK == KodeUK);

            lstKSubegiatan.OrderBy(x => x.IDSubKegiatan);


            foreach (ProgramKegiatanAnggaran subkegiatan in lstKSubegiatan)
            {
                if (oldSubKegiatan != subkegiatan.IDSubKegiatan)
                {
                    lstDistinctSubKegiatan.Add(subkegiatan);
                    oldSubKegiatan = subkegiatan.IDSubKegiatan;
                }
            }

            var lstJumlahSubKegiatan = m_lstProgramKegiatan.FindAll(p => p.IDDInas == m_IDSKPD).GroupBy(x => x.IDSubKegiatan)
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
                if (subkeg.IDSubKegiatan != oldSubKegiatan)
                {

                    string[] strSubkegiatan = {subkeg.StrIDSubKegiatan ,subkeg.NamaSubKegiatan + NamaUK,
                                        subkeg.AnggaranMurni.ToRupiahInReport(), subkeg.AnggaranGeser.ToRupiahInReport(),
                                        subkeg.AnggaranRKAP.ToRupiahInReport(), subkeg.AnggaranABT.ToRupiahInReport(), "0", "0", "0", LEVEL_SUBKEGIATAN.ToString(),
                                                                                            subkeg.IDUrusan.ToString(),
                                                                                            subkeg.IDProgram.ToString(),
                                                                                            subkeg.IDKegiatan.ToString(),
                                                                                            subkeg.IDSubKegiatan.ToString(),
                                                                                            subkeg.IIDRekening.ToString(),KodeUK.ToString()};
                    gridKartuKendali.Rows.Add(strSubkegiatan);
                    oldSubKegiatan = subkeg.IDSubKegiatan;
                    ProcessRekening(oldSubKegiatan, KodeUK);


                }
            }
        }
        private void ProcessRekening(long IDSubKegiatan, int KodeUK)
        {
            try
            {
                List<ProgramKegiatanAnggaran> lstRekening = new List<ProgramKegiatanAnggaran>();
                List<ProgramKegiatanAnggaran> lstDistinctSubKegiatan = new List<ProgramKegiatanAnggaran>();
                lstRekening = m_lstProgramKegiatan.FindAll(rek => rek.IDSubKegiatan == IDSubKegiatan
                                              && rek.IDDInas == m_IDSKPD
                                              && rek.KodeUK == KodeUK);

                lstRekening.OrderBy(x => x.IIDRekening);

                foreach (ProgramKegiatanAnggaran rek in lstRekening)
                {
                    if (rek.NamaRekening.Contains("Fungsional PNS"))
                    {
                        Console.WriteLine(rek.NamaRekening);
                    }

                    string[] row = { rek.IIDRekening.ToKodeRekening() ,rek.NamaRekening,
                                    rek.AnggaranMurni.ToRupiahInReport(), rek.AnggaranGeser.ToRupiahInReport(),
                                    rek.AnggaranRKAP.ToRupiahInReport(), rek.AnggaranABT.ToRupiahInReport(), "0", "0", "0", LEVEL_REKANING.ToString(),
                                                                                            rek.IDUrusan.ToString(),
                                                                                            rek.IDProgram.ToString(),
                                                                                            rek.IDKegiatan.ToString(),
                                                                                            rek.IDSubKegiatan.ToString(),
                                                                                            rek.IIDRekening.ToString(), KodeUK.ToString(),"0"};
                    gridKartuKendali.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion 

        #region displayKartuKendali
        private bool DisplayDetail()
        {
            try
            {
    
                var q = from d in mKartuKendaliList
                        select new KartuKendali
                        {
                            IDDInas = m_IDSKPD,
                            IDUrusan = d.IDUrusan,
                            IDProgram = d.IDProgram,
                            IDKegiatan = d.IDKegiatan,
                            IDSubKegiatan = d.IDSubKegiatan,
                            IDRekening = d.IDRekening,
                            KodeUK = d.KodeUK,
                            SP2DUP = d.SP2DUP,
                            SP2DGU = d.SP2DGU,
                            SP2DTU = d.SP2DTU,
                            SP2DLS = d.SP2DLS,
                            RSP2DGU = d.RSP2DGU,
                            RSP2DTU = d.RSP2DTU,
                            RSP2DLS= d.RSP2DLS,
                            TotalSP2D= d.SP2DUP+d.RSP2DGU +d.SP2DTU + d.RSP2DLS,
                            TotalRealisasi= d.RSP2DGU+ d.RSP2DTU+ d.RSP2DLS,
                            
                        };



                mKartuKendaliList = q.ToList();



                if (mKartuKendaliList != null)
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

                    foreach (Unit u in GlobalVar.gListOrganisasi)
                    {
                        if (u.SKPD == m_IDSKPD)
                        {
                            lstKodeUK.Add(u.UntAnggaran);
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
                   
                    }

                }
                else
                {
                   
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
            if (ctrlTahapAnggaran1.ID == 0)
                mcolAnggaran = COL_ANGGARANMURNI;
            if (ctrlTahapAnggaran1.ID==2)
                mcolAnggaran = COL_ANGGARANMURNI;
            if (ctrlTahapAnggaran1.ID==3)
                mcolAnggaran = COL_ANGGARANGESER;
            if (ctrlTahapAnggaran1.ID==4)
                mcolAnggaran = COL_ANGGARANRKAP;
            if (ctrlTahapAnggaran1.ID==5)
                mcolAnggaran = COL_ANGGARANABT;
            
            decimal nilaisisaPagu = 0M;
            decimal nilaiRealisasi = 0l;
            JUMLAH_ANGGARAN = 0;
            JUMLAH_SP2DUP = JumlahUP;
        JUMLAH_SP2DGU = 0;
        JUMLAH_SP2DTU = 0;
        JUMLAH_SP2DLS = 0;
        JUMLAH_JUMLAHSP2D = 0;

        JUMLAH_RUPGU = 0;
        JUMLAH_RUPTU = 0;
        JUMLAH_RUPLS = 0;
        decimal JUMLAH_UP = DataFormat.GetString(gridKartuKendali.Rows[0].Cells[COL_SP2DUP].Value).FormatUangReportKeDecimal();
        decimal JUMLAH_REALISASI = 0;

            foreach (DataGridViewRow row in gridKartuKendali.Rows)
            {

                if (DataFormat.GetInteger(row.Cells[COL_LEVEL].Value) == LEVEL_REKANING)
                {
                   
                    JUMLAH_ANGGARAN = JUMLAH_ANGGARAN + DataFormat.GetString(row.Cells[mcolAnggaran].Value).FormatUangReportKeDecimal();
                    //JUMLAH_SP2DUP = JUMLAH_SP2DUP + DataFormat.GetString(row.Cells[COL_JUMLAHSP2D].Value).FormatUangReportKeDecimal();

                    JUMLAH_SP2DGU = JUMLAH_SP2DGU + DataFormat.GetString(row.Cells[COL_SP2DGU].Value).FormatUangReportKeDecimal();
                    JUMLAH_SP2DTU = JUMLAH_SP2DTU + DataFormat.GetString(row.Cells[COL_SP2DTU].Value).FormatUangReportKeDecimal();
                    JUMLAH_SP2DLS = JUMLAH_SP2DLS + DataFormat.GetString(row.Cells[COL_SP2DLS].Value).FormatUangReportKeDecimal();
                    JUMLAH_JUMLAHSP2D = JUMLAH_JUMLAHSP2D + DataFormat.GetString(row.Cells[COL_JUMLAHSP2D].Value).FormatUangReportKeDecimal();

                    JUMLAH_RUPGU = JUMLAH_RUPGU + DataFormat.GetString(row.Cells[COL_RUPGU].Value).FormatUangReportKeDecimal();
                    JUMLAH_RUPTU = JUMLAH_RUPTU + DataFormat.GetString(row.Cells[COL_RUPTU].Value).FormatUangReportKeDecimal();
                    JUMLAH_RUPLS = JUMLAH_RUPLS + DataFormat.GetString(row.Cells[COL_RUPLS].Value).FormatUangReportKeDecimal();
                    JUMLAH_REALISASI = JUMLAH_REALISASI + DataFormat.GetString(row.Cells[COL_JUMLAHREALISASI].Value).FormatUangReportKeDecimal();
                }

                nilaiAnggaran = DataFormat.GetString(row.Cells[mcolAnggaran].Value).FormatUangReportKeDecimal();
                nilaiSPJ = DataFormat.GetString(row.Cells[COL_JUMLAHSP2D].Value).FormatUangReportKeDecimal();
                nilaiRealisasi = DataFormat.GetString(row.Cells[COL_JUMLAHREALISASI].Value).FormatUangReportKeDecimal();

                nilaisisaPagu = nilaiAnggaran -  nilaiSPJ;
                row.Cells[COL_SISAPAGUKEGIATAN].Value = nilaisisaPagu.ToRupiahInReport();
                row.Cells[COL_SISAPAGUBELANJA].Value = (nilaiAnggaran- nilaiRealisasi).ToRupiahInReport();


            }

           // gridKartuKendali.Rows.Add();
            if (gridKartuKendali.Rows.Count == m_iRowTerakhir)
            {
                gridKartuKendali.Rows.Add();

                // int irowTerakhir = gridKartuKendali.Rows.Count - 1;
                DataGridViewRow rowTerakhir = gridKartuKendali.Rows[m_iRowTerakhir];
                rowTerakhir.Cells[1].Value = "J U M L A H";

                rowTerakhir.Cells[mcolAnggaran].Value = JUMLAH_ANGGARAN.ToRupiahInReport();
                JUMLAH_SP2DUP = JumlahUP;
                rowTerakhir.Cells[COL_SP2DUP].Value = JumlahUP.ToRupiahInReport();
                rowTerakhir.Cells[COL_SP2DGU].Value = JUMLAH_SP2DGU.ToRupiahInReport();
                rowTerakhir.Cells[COL_SP2DTU].Value = JUMLAH_RUPTU.ToRupiahInReport();
                rowTerakhir.Cells[COL_SP2DLS].Value = JUMLAH_SP2DLS.ToRupiahInReport();
                rowTerakhir.Cells[COL_JUMLAHSP2D].Value = JUMLAH_JUMLAHSP2D.ToRupiahInReport();

                rowTerakhir.Cells[COL_RUPGU].Value = JUMLAH_RUPGU.ToRupiahInReport();
                rowTerakhir.Cells[COL_RUPTU].Value = JUMLAH_SP2DTU.ToRupiahInReport();
                rowTerakhir.Cells[COL_RUPLS].Value = JUMLAH_RUPLS.ToRupiahInReport();
                rowTerakhir.Cells[COL_JUMLAHREALISASI].Value = JUMLAH_REALISASI.ToRupiahInReport();
                rowTerakhir.Cells[COL_SISAKAS].Value = (JUMLAH_SP2DUP + JUMLAH_JUMLAHSP2D - JUMLAH_REALISASI).ToRupiahInReport();
                rowTerakhir.Cells[COL_SISAPAGUKEGIATAN].Value = (JUMLAH_ANGGARAN - JUMLAH_JUMLAHSP2D).ToRupiahInReport();
                rowTerakhir.Cells[COL_SISAPAGUBELANJA].Value = (JUMLAH_ANGGARAN - JUMLAH_REALISASI).ToRupiahInReport();
            }

        }
        private void ProsesSPJPerDinas()
        {

            JumlahUP = mKartuKendaliList.FirstOrDefault(x=>x.IDDInas== m_IDSKPD).SP2DUP;
            var lstJumlah = mKartuKendaliList.FindAll(x => x.IDDInas == m_IDSKPD).GroupBy(g => g.IDDInas)
             .Select(x => new
             {
                 IDDINAS = x.Key,

                            //SP2DUP = (y=>y.SP2DUP),
                            SP2DGU = x.Sum(y => y.SP2DGU),
                            SP2DTU = x.Sum(y => y.SP2DTU),
                            SP2DLS = x.Sum(y => y.SP2DLS),
                            RSP2DGU = x.Sum(y => y.RSP2DGU),
                            RSP2DTU = x.Sum(y => y.RSP2DTU),
                            RSP2DLS= x.Sum(y => y.RSP2DLS),
                      


             }).ToList();

            for (int idx = 0; idx < gridKartuKendali.Rows.Count; idx++)
            {
                if (gridKartuKendali.Rows[idx].Cells[COL_IDURUSAN].Value != null &&
                    gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value != null)
                {
                    if (DataFormat.GetInteger(gridKartuKendali.Rows[idx].Cells[COL_IDDINAS].Value) == m_IDSKPD &&
                        DataFormat.GetInteger(gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_DINAS
                        )
                    {
                        if (lstJumlah.Count > 0)
                        {
                            gridKartuKendali.Rows[idx].Cells[COL_SP2DUP].Value = JumlahUP.ToRupiahInReport();
                            gridKartuKendali.Rows[idx].Cells[COL_SP2DGU].Value = lstJumlah[0].SP2DGU.ToRupiahInReport();
                            gridKartuKendali.Rows[idx].Cells[COL_SP2DTU].Value = lstJumlah[0].SP2DTU.ToRupiahInReport();
                            gridKartuKendali.Rows[idx].Cells[COL_SP2DLS].Value = lstJumlah[0].SP2DLS.ToRupiahInReport();
                            gridKartuKendali.Rows[idx].Cells[COL_JUMLAHSP2D].Value = (JumlahUP + lstJumlah[0].SP2DLS + lstJumlah[0].SP2DGU + lstJumlah[0].SP2DTU).ToRupiahInReport();

                            gridKartuKendali.Rows[idx].Cells[COL_RUPGU].Value = lstJumlah[0].RSP2DGU.ToRupiahInReport();
                            gridKartuKendali.Rows[idx].Cells[COL_RUPTU].Value = lstJumlah[0].RSP2DTU.ToRupiahInReport();
                            gridKartuKendali.Rows[idx].Cells[COL_RUPLS].Value = lstJumlah[0].RSP2DLS.ToRupiahInReport();
                            
                            decimal jumlahSP2D=JumlahUP + lstJumlah[0].SP2DLS + lstJumlah[0].SP2DGU + lstJumlah[0].SP2DTU;
                            decimal totalRealisasi = lstJumlah[0].RSP2DGU +lstJumlah[0].RSP2DTU +lstJumlah[0].RSP2DLS;

                            gridKartuKendali.Rows[idx].Cells[COL_JUMLAHREALISASI].Value = (lstJumlah[0].RSP2DGU +
                                                                                           lstJumlah[0].RSP2DTU +
                                                                                            lstJumlah[0].RSP2DLS).ToRupiahInReport();

                            
                            gridKartuKendali.Rows[idx].Cells[COL_SISAKAS].Value=(jumlahSP2D-totalRealisasi).ToRupiahInReport();
 
                        }
                        break;
                    }
                }
            }
        }
        private void ProsesSPJPerUK(int KodeUK)
        {
            var lstJumlah = mKartuKendaliList.Where(u => u.KodeUK == KodeUK).GroupBy(x => x.KodeUK)
                   .Select(x => new
                   {
                       KodeUK = x.Key,
            
                       SP2DGU = x.Sum(y => y.SP2DGU),
                       SP2DTU = x.Sum(y => y.SP2DTU),
                       SP2DLS = x.Sum(y => y.SP2DLS),
                       RSP2DGU = x.Sum(y => y.RSP2DGU),
                       RSP2DTU = x.Sum(y => y.RSP2DTU),
                       RSP2DLS = x.Sum(y => y.RSP2DLS),



                   }).ToList();


            List<KartuKendali> lstJumlahKartuKendaliPerUK = (from t in mKartuKendaliList
                                                              join j in lstJumlah
                                                              on t.KodeUK equals j.KodeUK
                                                              select new KartuKendali
                                                              {
                                                                  IDUrusan = 0,
                                                                  IDProgram = 0,
                                                                  IDKegiatan = 0,
                                                                  IDSubKegiatan = 0,
                                                                  IDRekening = 0,
                                                                  KodeUK = t.KodeUK,

                                                                 
                                                                  SP2DGU = j.SP2DGU,
                                                                  SP2DTU = j.SP2DTU,
                                                                  SP2DLS = j.SP2DLS,
                                                                  RSP2DGU = j.RSP2DGU,
                                                                  RSP2DTU = j.RSP2DTU,
                                                                  RSP2DLS = j.RSP2DLS,


                                                              }).ToList<KartuKendali>();//.Distinct();// < KartuKendali>();

            List<KartuKendali> lstJumlahKartuKendaliPerUKDistincted = new List<KartuKendali>();
            var lst = lstJumlahKartuKendaliPerUK
             .Select(p => new { p.KodeUK, p.SP2DGU, p.SP2DTU, p.SP2DLS, p.RSP2DGU, p.RSP2DTU, p.RSP2DLS })
             .Distinct().ToList();

            int oldKodeUK = 0;

            foreach (var u in lst)
            {

                if (u.KodeUK != oldKodeUK)
                {

                    for (int idx = 0; idx < gridKartuKendali.Rows.Count; idx++)
                    {
                        if (gridKartuKendali.Rows[idx].Cells[COL_IDURUSAN].Value != null &&
                            gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {
                            if (gridKartuKendali.Rows[idx].Cells[COL_KODEUK].Value.ToString() == u.KodeUK.ToString() &&
                                DataFormat.GetInteger(gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_UNIT
                                )
                            {
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DUP].Value = "0,00";
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DGU].Value = u.SP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DTU].Value = u.SP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DLS].Value = u.SP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHSP2D].Value = 0 + (u.SP2DLS + u.SP2DGU + u.SP2DTU).ToRupiahInReport();

                                gridKartuKendali.Rows[idx].Cells[COL_RUPGU].Value = u.RSP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPTU].Value = u.RSP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPLS].Value = u.RSP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHREALISASI].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();

                                //gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUKEGIATAN].Value = ((u.SP2DLS + u.SP2DGU + u.SP2DTU) - (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS)).ToRupiahInReport();
                                //gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUBELANJA].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();
  


                            }
                        }
                    }
                    oldKodeUK = u.KodeUK;
                }

            }


        }
        private void ProsesSPJPerurusan(int KodeUK)
        {
            var lstJumlah = mKartuKendaliList.FindAll(sk => sk.KodeUK == KodeUK).GroupBy(x => x.IDUrusan)
                   .Select(x => new
                   {

                       IDUrusan = x.Key,
                       SP2DGU = x.Sum(y => y.SP2DGU),
                       SP2DTU = x.Sum(y => y.SP2DTU),
                       SP2DLS = x.Sum(y => y.SP2DLS),
                       RSP2DGU = x.Sum(y => y.RSP2DGU),
                       RSP2DTU = x.Sum(y => y.RSP2DTU),
                       RSP2DLS = x.Sum(y => y.RSP2DLS),



                   }).ToList();

            List<KartuKendali> lstOnThisUK = mKartuKendaliList.FindAll(x => x.KodeUK == KodeUK);

            List<KartuKendali> lstJumlahKartuKendaliPerurusan = (from t in lstOnThisUK
                                                              join j in lstJumlah
                                                              on t.IDUrusan equals j.IDUrusan
                                                              select new KartuKendali
                                                              {

                                                                  IDUrusan = t.IDUrusan,
                                                                  IDProgram = 0,
                                                                  IDKegiatan = 0,
                                                                  IDSubKegiatan = 0,
                                                                  IDRekening = 0,
                                                                  KodeUK = KodeUK,
                                                                  SP2DGU = j.SP2DGU,
                                                                  SP2DTU = j.SP2DTU,
                                                                  SP2DLS = j.SP2DLS,
                                                                  RSP2DGU = j.RSP2DGU,
                                                                  RSP2DTU = j.RSP2DTU,
                                                                  RSP2DLS = j.RSP2DLS,


                                                              }).ToList<KartuKendali>();//.Distinct();// < KartuKendali>();

            List<KartuKendali> lstJumlahSPJPerurusanDistincted = new List<KartuKendali>();
            int oldUrusan = 0;

            foreach (KartuKendali u in lstJumlahKartuKendaliPerurusan)
            {

                if (u.IDUrusan != oldUrusan)
                {

                    for (int idx = 0; idx < gridKartuKendali.Rows.Count; idx++)
                    {
                        if (gridKartuKendali.Rows[idx].Cells[COL_IDURUSAN].Value != null &&
                            gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {
                            if (gridKartuKendali.Rows[idx].Cells[COL_IDURUSAN].Value.ToString() == u.IDUrusan.ToString() &&
                                gridKartuKendali.Rows[idx].Cells[COL_KODEUK].Value.ToString() == u.KodeUK.ToString() &&
                                GetLevel(idx) == LEVEL_URUSAN
                                //DataFormat.GetInteger(gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_URUSAN
                                )
                            {
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DUP].Value = "0,00";
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DGU].Value = u.SP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DTU].Value = u.SP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DLS].Value = u.SP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHSP2D].Value = 0 + (u.SP2DLS + u.SP2DGU + u.SP2DTU).ToRupiahInReport();

                                gridKartuKendali.Rows[idx].Cells[COL_RUPGU].Value = u.RSP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPTU].Value = u.RSP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPLS].Value = u.RSP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHREALISASI].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();

                                //gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUKEGIATAN].Value = ((u.SP2DLS + u.SP2DGU + u.SP2DTU) - (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS)).ToRupiahInReport();
                                //gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUBELANJA].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();
  

                            }
                        }
                    }
                    oldUrusan = u.IDUrusan;
                }

            }


        }
        private void ProsesSPJPerProgram(int KodeUK)
        {
            var lstJumlah = mKartuKendaliList.Where(w => w.KodeUK == KodeUK).GroupBy(x => x.IDProgram)
                   .Select(x => new
                   {
                       IDProg = x.Key,

                       SP2DGU = x.Sum(y => y.SP2DGU),
                       SP2DTU = x.Sum(y => y.SP2DTU),
                       SP2DLS = x.Sum(y => y.SP2DLS),
                       RSP2DGU = x.Sum(y => y.RSP2DGU),
                       RSP2DTU = x.Sum(y => y.RSP2DTU),
                       RSP2DLS = x.Sum(y => y.RSP2DLS),

                   }).ToList();

            List<KartuKendali> lstOnThisUK = mKartuKendaliList.FindAll(x => x.KodeUK == KodeUK);
            List<KartuKendali> lstJumlahSPJPerProgram = (from t in lstOnThisUK
                                                               join j in lstJumlah
                                                               on t.IDProgram equals j.IDProg
                                                               select new KartuKendali
                                                               {

                                                                   IDUrusan = t.IDUrusan,
                                                                   IDProgram = t.IDProgram,
                                                                   IDKegiatan = 0,
                                                                   IDSubKegiatan = 0,
                                                                   IDRekening = 0,
                                                                   KodeUK = KodeUK,
                                                                   SP2DGU = j.SP2DGU,
                                                                   SP2DTU = j.SP2DTU,
                                                                   SP2DLS = j.SP2DLS,
                                                                   RSP2DGU = j.RSP2DGU,
                                                                   RSP2DTU = j.RSP2DTU,
                                                                   RSP2DLS = j.RSP2DLS,


                                                               }).ToList<KartuKendali>();


            var lst = lstJumlahSPJPerProgram
                   .Select(p => new { p.IDProgram, p.SP2DGU, p.SP2DTU, p.SP2DLS, p.RSP2DGU, p.RSP2DTU, p.RSP2DLS })
                   .Distinct().ToList();



            int oldProgram = 0;

            // foreach (KartuKendali u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {

                if (u.IDProgram != oldProgram)
                {
                    for (int idx = 0; idx < gridKartuKendali.Rows.Count; idx++)
                    {
                        if (gridKartuKendali.Rows[idx].Cells[COL_IDPROGRAM].Value != null &&
                            gridKartuKendali.Rows[idx].Cells[COL_IDPROGRAM].Value != null)
                        {


                            if (gridKartuKendali.Rows[idx].Cells[COL_IDPROGRAM].Value.ToString() == u.IDProgram.ToString() &&
                                gridKartuKendali.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                DataFormat.GetInteger(gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_PROGRAM

                                )
                            {

                                gridKartuKendali.Rows[idx].Cells[COL_SP2DUP].Value = "0,00";
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DGU].Value = u.SP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DTU].Value = u.SP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DLS].Value = u.SP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHSP2D].Value = 0 + (u.SP2DLS + u.SP2DGU + u.SP2DTU).ToRupiahInReport();

                                gridKartuKendali.Rows[idx].Cells[COL_RUPGU].Value = u.RSP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPTU].Value = u.RSP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPLS].Value = u.RSP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHREALISASI].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();

                                gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUKEGIATAN].Value = ((u.SP2DLS + u.SP2DGU + u.SP2DTU) - (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS)).ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUBELANJA].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();
  
                            }
                        }
                    }

                }

            }

        }
        private void ProsesDisplayDetailPerKegiatan(int KodeUK)
        {
            List<KartuKendali> lstOnThisUK = mKartuKendaliList.FindAll(x => x.KodeUK == KodeUK);
            var lstJumlah = lstOnThisUK.GroupBy(x => x.IDKegiatan)
                   .Select(x => new
                   {
                       IDKegiatan = x.Key,
                       SP2DGU = x.Sum(y => y.SP2DGU),
                       SP2DTU = x.Sum(y => y.SP2DTU),
                       SP2DLS = x.Sum(y => y.SP2DLS),
                       RSP2DGU = x.Sum(y => y.RSP2DGU),
                       RSP2DTU = x.Sum(y => y.RSP2DTU),
                       RSP2DLS = x.Sum(y => y.RSP2DLS),

                   }).ToList();


            List<KartuKendali> lstJumlahSPJPerKegiatan = (from t in lstOnThisUK
                                                                join j in lstJumlah
                                                                on t.IDKegiatan equals j.IDKegiatan
                                                                select new KartuKendali
                                                                {


                                                                    IDUrusan = t.IDUrusan,
                                                                    IDProgram = t.IDProgram,
                                                                    IDKegiatan = t.IDKegiatan,
                                                                    IDSubKegiatan = 0,
                                                                    IDRekening = 0,
                                                                    KodeUK = KodeUK,
                                                                    SP2DGU = j.SP2DGU,
                                                                    SP2DTU = j.SP2DTU,
                                                                    SP2DLS = j.SP2DLS,
                                                                    RSP2DGU = j.RSP2DGU,
                                                                    RSP2DTU = j.RSP2DTU,
                                                                    RSP2DLS = j.RSP2DLS,


                                                                }).ToList<KartuKendali>();


            var lst = lstJumlahSPJPerKegiatan
                  .Select(p => new { p.IDProgram, p.IDKegiatan, p.SP2DGU, p.SP2DTU, p.SP2DLS, p.RSP2DGU, p.RSP2DTU, p.RSP2DLS })
                   .Distinct().ToList();




            int oldKegiatan = 0;

            // foreach (KartuKendali u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {

                if (u.IDKegiatan != oldKegiatan)
                {
                    for (int idx = 0; idx < gridKartuKendali.Rows.Count; idx++)
                    {
                        if (gridKartuKendali.Rows[idx].Cells[COL_IDKEGIATAN].Value != null && gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridKartuKendali.Rows[idx].Cells[COL_IDKEGIATAN].Value.ToString() == u.IDKegiatan.ToString() &&
                                GetLevel(idx) == LEVEL_KEGIATAN && KodeUK == DataFormat.GetInteger(gridKartuKendali.Rows[idx].Cells[COL_KODEUK].Value )

                                )
                            {

                                gridKartuKendali.Rows[idx].Cells[COL_SP2DUP].Value = "0,00";
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DGU].Value = u.SP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DTU].Value = u.SP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DLS].Value = u.SP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHSP2D].Value = 0 + (u.SP2DLS + u.SP2DGU + u.SP2DTU).ToRupiahInReport();

                                gridKartuKendali.Rows[idx].Cells[COL_RUPGU].Value = u.RSP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPTU].Value = u.RSP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPLS].Value = u.RSP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHREALISASI].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();

                                gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUKEGIATAN].Value = ((u.SP2DLS + u.SP2DGU + u.SP2DTU) - (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS)).ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUBELANJA].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();
  
                            }
                        }
                    }
                    oldKegiatan = u.IDKegiatan;

                }

            }

        }
        private void ProsesDisplayDetailPerSubKegiatan(int KodeUK)
        {
            List<KartuKendali> lstOnThisUK = mKartuKendaliList.FindAll(x => x.KodeUK == KodeUK);

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
                       SP2DGU = x.Sum(y => y.SP2DGU),
                       SP2DTU = x.Sum(y => y.SP2DTU),
                       SP2DLS = x.Sum(y => y.SP2DLS),
                       RSP2DGU = x.Sum(y => y.RSP2DGU),
                       RSP2DTU = x.Sum(y => y.RSP2DTU),
                       RSP2DLS = x.Sum(y => y.RSP2DLS),
                   }).ToList();


            List<KartuKendali> lstJumlahSPJPerSubKegiatan = (from t in lstOnThisUK
                                                                   join j in lstJumlah
                                                                   on t.IDSubKegiatan equals j.IDSubKegiatan
                                                                   select new KartuKendali
                                                                   {



                                                                       IDUrusan = t.IDUrusan,
                                                                       IDProgram = t.IDProgram,
                                                                       IDKegiatan = t.IDKegiatan,
                                                                       IDSubKegiatan = t.IDSubKegiatan,
                                                                       IDRekening = 0,
                                                                       KodeUK = KodeUK,
                                                                       SP2DGU = j.SP2DGU,
                                                                       SP2DTU = j.SP2DTU,
                                                                       SP2DLS = j.SP2DLS,
                                                                       RSP2DGU = j.RSP2DGU,
                                                                       RSP2DTU = j.RSP2DTU,
                                                                       RSP2DLS = j.RSP2DLS,

                                                                   }).ToList<KartuKendali>();


            var lst = lstJumlahSPJPerSubKegiatan
                   .Select(p => new { p.IDProgram, p.IDKegiatan, p.IDSubKegiatan, p.SP2DGU, p.SP2DTU, p.SP2DLS, p.RSP2DGU, p.RSP2DTU, p.RSP2DLS })
                   .Distinct().ToList();





            long oldIDSubKegiatan = 0;

            // foreach (KartuKendali u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {

                if (u.IDSubKegiatan != oldIDSubKegiatan)
                {
                    for (int idx = 0; idx < gridKartuKendali.Rows.Count; idx++)
                    {
                        if (gridKartuKendali.Rows[idx].Cells[COL_IDSubKegiatan].Value != null && gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridKartuKendali.Rows[idx].Cells[COL_IDSubKegiatan].Value.ToString() == u.IDSubKegiatan.ToString() &&
                                DataFormat.GetInteger(gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_SUBKEGIATAN &&
                                KodeUK == DataFormat.GetInteger(gridKartuKendali.Rows[idx].Cells[COL_KODEUK].Value)

                                )
                            {
                               
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DUP].Value = "0,00";
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DGU].Value = u.SP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DTU].Value = u.SP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DLS].Value = u.SP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHSP2D].Value = 0 + (u.SP2DLS + u.SP2DGU + u.SP2DTU).ToRupiahInReport();

                                gridKartuKendali.Rows[idx].Cells[COL_RUPGU].Value = u.RSP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPTU].Value = u.RSP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPLS].Value = u.RSP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHREALISASI].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();

                                gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUKEGIATAN].Value = ((u.SP2DLS + u.SP2DGU + u.SP2DTU) - (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS)).ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUBELANJA].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();
  
    

                            }
                        }
                    }
                    oldIDSubKegiatan = u.IDSubKegiatan;
                }


            }

        }
        private void ProsesDisplayDetailPerRekening(int KodeUK)
        {
            List<KartuKendali> lstOnThisUK = mKartuKendaliList.FindAll(x => x.KodeUK == KodeUK);
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
                    SP2DGU = x.Sum(y => y.SP2DGU),
                    SP2DTU = x.Sum(y => y.SP2DTU),
                    SP2DLS = x.Sum(y => y.SP2DLS),
                    RSP2DGU = x.Sum(y => y.RSP2DGU),
                    RSP2DTU = x.Sum(y => y.RSP2DTU),
                    RSP2DLS = x.Sum(y => y.RSP2DLS),
                }).ToList();


            List<KartuKendali> lstJumlahSPJPerSubKegiatanRekening = (from t in lstOnThisUK
                                                                           join j in lstJumlah
                                                                          on t.IDSubKegiatan equals j.IDSubKegiatan
                                                                           where t.IDRekening == j.IDRekening
                                                                           select new KartuKendali
                                                                           {
                                                                               IDUrusan = t.IDUrusan,
                                                                               IDProgram = t.IDProgram,
                                                                               IDKegiatan = t.IDKegiatan,
                                                                               IDSubKegiatan = t.IDSubKegiatan,
                                                                               IDRekening = t.IDRekening,
                                                                               KodeUK = KodeUK,
                                                                               SP2DGU = j.SP2DGU,
                                                                               SP2DTU = j.SP2DTU,
                                                                               SP2DLS = j.SP2DLS,
                                                                               RSP2DGU = j.RSP2DGU,
                                                                               RSP2DTU = j.RSP2DTU,
                                                                               RSP2DLS = j.RSP2DLS,


                                                                           }).ToList<KartuKendali>();


            var lst = lstJumlahSPJPerSubKegiatanRekening
                   .Select(p => new { p.IDProgram, p.IDKegiatan, p.IDSubKegiatan, p.IDRekening, p.SP2DGU, p.SP2DTU, p.SP2DLS, p.RSP2DGU, p.RSP2DTU, p.RSP2DLS })
                   .Distinct().ToList();




            long oldIDSubKegiatan = 0;
            long oldIdRekening = 0;
            // foreach (KartuKendali u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {

                if (u.IDSubKegiatan != oldIDSubKegiatan || u.IDRekening != oldIdRekening)
                {
                    for (int idx = 0; idx < gridKartuKendali.Rows.Count; idx++)
                    {
                        if (gridKartuKendali.Rows[idx].Cells[COL_IDSubKegiatan].Value != null &&
                            gridKartuKendali.Rows[idx].Cells[COL_IDREKENING].Value != null &&
                            gridKartuKendali.Rows[idx].Cells[COL_KODEUK].Value != null &&

                            gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridKartuKendali.Rows[idx].Cells[COL_IDSubKegiatan].Value.ToString() == u.IDSubKegiatan.ToString() &&
                                gridKartuKendali.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                gridKartuKendali.Rows[idx].Cells[COL_IDREKENING].Value.ToString() == u.IDRekening.ToString() &&
                                GetLevel(idx) == LEVEL_REKANING
                                //gridKartuKendali.Rows[idx].Cells[COL_LEVEL].Value.ToString() == "5"

                                )
                            {
                                if (gridKartuKendali.Rows[idx].Cells[COL_IDSubKegiatan].Value.ToString() == "402012030006" &&
                                gridKartuKendali.Rows[idx].Cells[COL_KODEUK].Value.ToString() == "0" &&
                                gridKartuKendali.Rows[idx].Cells[COL_IDREKENING].Value.ToString() == "510103070002")
                                {
                                 //   MessageBox.Show( "510103070002");
                                }
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DUP].Value = "0,00";
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DGU].Value = u.SP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DTU].Value = u.SP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SP2DLS].Value = u.SP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHSP2D].Value = 0 + (u.SP2DLS + u.SP2DGU + u.SP2DTU).ToRupiahInReport();

                                gridKartuKendali.Rows[idx].Cells[COL_RUPGU].Value = u.RSP2DGU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPTU].Value = u.RSP2DTU.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_RUPLS].Value = u.RSP2DLS.ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_JUMLAHREALISASI].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();

                                gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUKEGIATAN].Value = ((u.SP2DLS + u.SP2DGU + u.SP2DTU) - (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS)).ToRupiahInReport();
                                gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUBELANJA].Value = (u.RSP2DGU + u.RSP2DTU + u.RSP2DLS).ToRupiahInReport();
  

                            }
                        }
                    }
                    oldIDSubKegiatan = u.IDSubKegiatan;
                }


            }

        }
        #endregion displayKartuKendali

        private void cmsCetak_Click(object sender, EventArgs e)
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
                DateTime tanggal = ctrlTanggal1.Tanggal;
                CetakPDF oCetakPDF = new CetakPDF();
                float yPos;
                float kiri = 5;

                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                    page.GetClientSize().Width, stringFormat, true, false, true);

               
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "KARTU KENDALI KEGIATAN" +
                             GlobalVar.TahunAnggaran.ToString(), 10, kiri, yPos,
                             page.GetClientSize().Width, stringFormat, true, false, true);

               
                stringFormat.Alignment = PdfTextAlignment.Left;

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          ctrlSKPD1.GetNamaSKPD(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);
                Pejabat ppk  = ctrlSKPD1.GetPPK(tanggal);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PPK SKPD "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          ppk.Nama, 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);


              


                PdfGrid pdfGridHeader = new PdfGrid();
                DataTable tableHeader = new DataTable();

                tableHeader.Columns.Add("Kode ");
                tableHeader.Columns.Add("Uraian");
                tableHeader.Columns.Add("Anggaran");
                tableHeader.Columns.Add("SP2DUP");
                tableHeader.Columns.Add("SP2DGU");
                tableHeader.Columns.Add("JSP2DTU");
                tableHeader.Columns.Add("SP2DLS");
                tableHeader.Columns.Add("JumlahSP2d");
                tableHeader.Columns.Add("reaipgutu");
                tableHeader.Columns.Add("realtu");
                tableHeader.Columns.Add("realls");
                tableHeader.Columns.Add("Jumlahrea");
                tableHeader.Columns.Add("Jumlah SPJ");
                tableHeader.Columns.Add("Sisa Kas");
                tableHeader.Columns.Add("Sisa Anggaran");
                //tableHeader.Columns.Add("Sisa Anggaran");


              

                tableHeader.Rows.Add(new string[]
                    {            " Kode   ",
                                " Uraian",
                                "Jumlah Anggaran",
                                "SP2D","SP2D","SP2D","SP2D","Jumlah SP2D",
                                "REALISASI BELANJA","REALISASI BELANJA","REALISASI BELANJA","JUMLAH REALISASI",
                                "SISA KAS","SISA ANGGARAN","SISA ANGGARAN"});

                tableHeader.Rows.Add(new string[]
                    {           " Kode   "," Uraian","Jumlah Anggaran",
                                "UP","GU","TU","LS", "Jumlah SP2D",
                                "UP/GU","TU","LS","JUMLAH REALISASI","SISA KAS","KEGIATAN","BELANJA"});

               

                pdfGridHeader.DataSource = tableHeader; //data
                pdfGridHeader.Columns[0].Width = 60;
                pdfGridHeader.Columns[1].Width = 75;

                // Angka 
                pdfGridHeader.Columns[2].Width = 50;
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
                pdfGridHeader.Columns[13].Width = 55;
                pdfGridHeader.Columns[14].Width = 55;

                pdfGridHeader.Rows[0].Cells[0].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[1].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[2].RowSpan = 2;
                
                pdfGridHeader.Rows[0].Cells[7].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[11].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[12].RowSpan = 2;


                pdfGridHeader.Rows[0].Cells[3].ColumnSpan = 4;
                pdfGridHeader.Rows[0].Cells[8].ColumnSpan = 3;
                pdfGridHeader.Rows[0].Cells[13].ColumnSpan = 2;

                PdfFont fontHeader = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 7));
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



                //#region spjAtas

                PdfGrid pdfGrid = new PdfGrid();
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("1");
                table.Columns.Add("2");
                table.Columns.Add("3");
                table.Columns.Add("4");
                table.Columns.Add("5");
                table.Columns.Add("6");
                table.Columns.Add("7");
                table.Columns.Add("8= 4+5+6+7");
                table.Columns.Add("9");
                table.Columns.Add("10");
                table.Columns.Add("11");
                table.Columns.Add("12=9+10+11");

                table.Columns.Add("13=8-12");
                table.Columns.Add("14=3-8");
                table.Columns.Add("15=3-12");


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
                int idx = 0;
                for (idx = 0; idx <  gridKartuKendali.Rows.Count; idx++)
                {
                    if (gridKartuKendali.Rows[idx].Cells[mcolAnggaran].Value != null)
                    {

                    table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[0].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[1].Value),                      
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[mcolAnggaran].Value),
        

                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_SP2DUP].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_SP2DGU].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_SP2DTU].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_SP2DLS].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_JUMLAHSP2D].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_RUPGU].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_RUPTU].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_RUPLS].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_JUMLAHREALISASI].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_SISAKAS].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUKEGIATAN].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[idx].Cells[COL_SISAPAGUBELANJA].Value),

                       

                       
                        
                    });
                    }


                }

                if (idx < m_iRowTerakhir)
                {
                    table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[0].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[1].Value),                      
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[mcolAnggaran].Value),
        

                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_SP2DUP].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_SP2DGU].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_SP2DTU].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_SP2DLS].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_JUMLAHSP2D].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_RUPGU].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_RUPTU].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_RUPLS].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_JUMLAHREALISASI].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_SISAKAS].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_SISAPAGUKEGIATAN].Value),
                       DataFormat.GetString(gridKartuKendali.Rows[m_iRowTerakhir].Cells[COL_SISAPAGUBELANJA].Value),

                       

                       
                        
                    });


                }

                pdfGrid.DataSource = table; //data
                 pdfGrid.Columns[0].Width = 60;
                pdfGrid.Columns[1].Width = 75;

                // Angka 
                pdfGrid.Columns[2].Width = 50;
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
                pdfGrid.Columns[13].Width = 55;
                pdfGrid.Columns[14].Width = 55;



                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding

                gridStyle.CellPadding = new PdfPaddings(3, 1, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                pdfGrid.Style = gridStyle;
                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                for (int col = 2; col < pdfGrid.Columns.Count; col++)
                    pdfGrid.Columns[col].Format = formatKolomAngka;

                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 6));
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
                cellStyle.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 6, FontStyle.Regular));
                for ( idx = 0; idx < pdfGrid.Rows.Count; idx++)
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
       
             
                //#endregion PenerimaanPengeluaran

                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();

                //System.Diagnostics.Process.Start(namaFile);


                //string namaFile = Path.GetFullPath(@"../../../SPD_" + txtINO.Text.Trim() + "_" + ctrlSKPD1.GetNamaSKPD() + ".pdf");
                string namaFile = Path.GetFullPath(@"../../../KartuKendali.pdf");

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


                Pejabat ppk = ctrlSKPD1.GetPPK(ctrlTanggal1.Tanggal);
        
                yPos = yPos + 10;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.gPemda.Ibukota + "," + ctrlTanggal1.Tanggal.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ppk.Jabatan, 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ppk.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + ppk.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);




            }



            previousPage = args.Page;


        }


        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtSpasi_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbMurni_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cndExcell_Click(object sender, EventArgs e)
        {
         //   EWxport();
            EWxportHanyaBelanja();
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
        private void EWxport()
        {

            string NamaFile = "";
            string namaFile = BuatFile();
            if (namaFile.Trim().Length == 0)
            {
                MessageBox.Show("Nama Masih Kosong ");
                return;
            }
            try
            {

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

                excelSheet.Name = "Kartu Kendali";

                // storing header part in Excel  
                for (int i = 1; i < gridKartuKendali.Columns.Count + 1; i++)
                {
                    if (i <= 3 || i > 17)
                    {
                        if (i >= 17)
                        {
                            excelSheet.Cells[1, i - 14] = gridKartuKendali.Columns[i - 1].HeaderText;
                        }
                        else
                        {
                            excelSheet.Cells[1, i] = gridKartuKendali.Columns[i - 1].HeaderText;
                        }
                    }

                }
                // storing Each row and column value to excel sheet  
                List<int> lstColToCetak = new List<int>();

                for (int i = 0; i < gridKartuKendali.Rows.Count ; i++)
                {
                    int c = 0;
                    for (int j = 0; j <= gridKartuKendali.Columns.Count - 1; j++)
                    {
                        if (gridKartuKendali.Columns[j].Visible == true)
                        {
                            ++c;
                            if (gridKartuKendali.Rows[i].Cells[j].Value != null)
                            {
                                string s = "";
                                if (j >= 2)
                                {
                                    s = DataFormat.FormatUangReportKeDecimal(gridKartuKendali.Rows[i].Cells[j].Value).ToString();
                                }
                                else
                                {
                                    s = DataFormat.GetString(gridKartuKendali.Rows[i].Cells[j].Value);
                                }

                                excelSheet.Cells[i + 2, c] = s;



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
        private void EWxportHanyaBelanja()
        {

            string NamaFile = "";
            string namaFile = BuatFile();
            if (namaFile.Trim().Length == 0)
            {
                MessageBox.Show("Nama Masih Kosong ");
                return;
            }
            try
            {

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

                excelSheet.Name = "Kartu Kendali";

                // storing header part in Excel  
                for (int i = 1; i < gridKartuKendali.Columns.Count + 1; i++)
                {
                    if (i <= 3 || i > 17)
                    {
                        if (i >= 17)
                        {
                            excelSheet.Cells[1, i - 14] = gridKartuKendali.Columns[i - 1].HeaderText;
                        }
                        else
                        {
                            excelSheet.Cells[1, i] = gridKartuKendali.Columns[i - 1].HeaderText;
                        }
                    }

                }
                // storing Each row and column value to excel sheet  
                List<int> lstColToCetak = new List<int>();
                int baris = 0;
                for (int i = 0; i < gridKartuKendali.Rows.Count; i++)
                {
                    
                        int c = 0;
                        for (int j = 0; j <= gridKartuKendali.Columns.Count - 1; j++)
                        {
                            if (gridKartuKendali.Columns[j].Visible == true)
                            {
                                ++c;
                                if (gridKartuKendali.Rows[i].Cells[j].Value != null)
                                {
                                    string s = "";
                                    if (j >= 2)
                                    {
                                        s = DataFormat.FormatUangReportKeDecimal(gridKartuKendali.Rows[i].Cells[j].Value).ToString();
                                    }
                                    else
                                    {
                                        s = DataFormat.GetString(gridKartuKendali.Rows[i].Cells[j].Value);
                                    }

                                    if (DataFormat.GetInteger(gridKartuKendali.Rows[i].Cells[COL_LEVEL].Value) == LEVEL_REKANING)
                                    {
                                        excelSheet.Cells[baris + 2, c] = s;




                                    }
                                }
                            }
                        }
                        if (DataFormat.GetInteger(gridKartuKendali.Rows[i].Cells[COL_LEVEL].Value) == LEVEL_REKANING)
                        {
                            baris++;




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

        private void ctrlTahapAnggaran1_Load(object sender, EventArgs e)
        {

        }

    }
}
