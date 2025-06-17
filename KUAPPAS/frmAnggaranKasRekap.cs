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
using Formatting;
using BP.Bendahara;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System.IO;
using DTO;
using BP;
using BP.Bendahara;
using DTO;
using DTO.Bendahara;
using Formatting;
using Excel = Microsoft.Office.Interop.Excel;
using DTO.Anggaran;
using Syncfusion.Pdf;
using BP.Anggaran;



namespace KUAPPAS
{
    public partial class frmAnggaranKasRekap : ChildForm
    {
        private List<ProgramKegiatanAnggaran> m_lstProgramKegiatan;


      //  private List<AnggaranKas> mAnggaranKasList;
        private List<AnggaranKas> mAnggaranKasList;

        string[] satuan = new string[10] { "nol", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan" };
        string[] belasan = new string[10] { "sepuluh", "sebelas", "dua belas", "tiga belas", "empat belas", "lima belas", "enam belas", "tujuh belas", "delapan belas", "sembilan belas" };
        string[] puluhan = new string[10] { "", "", "dua puluh", "tiga puluh", "empat puluh", "lima puluh", "enam puluh", "tujuh puluh", "delapan puluh", "sembilan puluh" };
        string[] ribuan = new string[5] { "", "ribu", "juta", "milyar", "triliyun" };

        string[] bulanpanjang = new string[12] { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };

        private int m_iJenis;
        private long m_NoUrut;
        private bool m_bNew;



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

        private const int COL_JAN = 17;
        private const int COL_FEB = 18;
        private const int COL_MAR = 19;

        private const int COL_APR = 20;
        private const int COL_MEI = 21;
        private const int COL_JUN = 22;
        private const int COL_JUL = 23;
        private const int COL_AGU = 24;
        private const int COL_SEP = 25;
        private const int COL_OKT = 26;
        private const int COL_NOP = 27;
        private const int COL_DES = 28;



        private const int LEVEL_DINAS = 0;
        private const int LEVEL_UNIT = 1;
        private const int LEVEL_URUSAN = 2;
        private const int LEVEL_PROGRAM = 3;
        private const int LEVEL_KEGIATAN = 4;
        private const int LEVEL_SUBKEGIATAN = 5;
        private const int LEVEL_REKANING = 6;




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

        public frmAnggaranKasRekap()
        {
            InitializeComponent();
        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
            try
            {
                gridAnggaranKas.Rows.Clear();

                

                LoadData();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }
        private void LoadData()
        {//Panggil data
            try
            {
                //Panggil data prog..sub
                if (LoadProgramKegiatan())
                {
                    if (DisplayProgramKegiatanSubKegiatan() == true)
                    {
                        DisplayAnggaranKas();
                    }


                  //  FormatGrid();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
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

                gridAnggaranKas.Rows.Clear();
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
                             m_IDSKPD.ToString(), ctrlSKPD1.GetNamaSKPD(),  totalOPD.AnggaranRKAP.ToRupiahInReport(), 
                                                                                            totalOPD.AnggaranRKAP.ToRupiahInReport(), 
                                                                                            totalOPD.AnggaranRKAP.ToRupiahInReport(), 
                                                                                            totalOPD.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_DINAS.ToString(),
                                                                                            "0",
                                                                                            "0",
                                                                                            "0",
                                                                                            "0",
                                                                                            "0" ,"0" };
                gridAnggaranKas.Rows.Add(rowOPD);
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
                                            p.AnggaranRKAP.ToRupiahInReport(), 
                                            p.AnggaranRKAP.ToRupiahInReport(), 
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
                            gridAnggaranKas.Rows.Add(rowOPD);
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
                                                                                         p.IDSubKegiatan.ToString(),
                                                                                            p.IIDRekening.ToString(),KodeUK.ToString() };
                        gridAnggaranKas.Rows.Add(row);

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
                                                                                            pr.AnggaranRKAP.ToRupiahInReport(),
                                                                                            pr.AnggaranRKAP.ToRupiahInReport(), 
                                                                                            pr.AnggaranRKAP.ToRupiahInReport(),
                                                                                            pr.AnggaranABT.ToRupiahInReport(),"0","0","0",LEVEL_PROGRAM.ToString(),
                                                                                            pr.IDUrusan.ToString(),
                                                                                            pr.IDProgram.ToString(),
                                                                                            pr.IDKegiatan.ToString(),
                                                                                            pr.IDSubKegiatan.ToString(),
                                                                                            pr.IIDRekening.ToString(),KodeUK.ToString()};
                        gridAnggaranKas.Rows.Add(rowProgram);

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

                lstKegiatan = m_lstProgramKegiatan.FindAll(keg => keg.IDDInas == m_IDSKPD && 
                                                           keg.IDProgram == idProgram && 
                                                           keg.KodeUK == KodeUK);

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
                                                                                             keg.AnggaranRKAP.ToRupiahInReport(), 
                                                                                             keg.AnggaranRKAP.ToRupiahInReport(), 
                                                                                             keg.AnggaranRKAP.ToRupiahInReport(), 
                                                                                             keg.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_KEGIATAN.ToString(),
                                                                                            keg.IDUrusan.ToString(),
                                                                                            keg.IDProgram.ToString(),
                                                                                            keg.IDKegiatan.ToString(),
                                                                                            keg.IDSubKegiatan.ToString(),
                                                                                            keg.IIDRekening.ToString(),KodeUK.ToString()};

                        gridAnggaranKas.Rows.Add(rowkegiatan);

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

            var lstJumlahSubKegiatan = m_lstProgramKegiatan.FindAll(p => p.IDDInas == m_IDSKPD && p.KodeUK== KodeUK).GroupBy(
                x => x.IDSubKegiatan)
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
                                        subkeg.AnggaranRKAP.ToRupiahInReport(), subkeg.AnggaranRKAP.ToRupiahInReport(),
                                        subkeg.AnggaranRKAP.ToRupiahInReport(), subkeg.AnggaranABT.ToRupiahInReport(), "0", "0", "0", LEVEL_SUBKEGIATAN.ToString(),
                                                                                            subkeg.IDUrusan.ToString(),
                                                                                            subkeg.IDProgram.ToString(),
                                                                                            subkeg.IDKegiatan.ToString(),
                                                                                            subkeg.IDSubKegiatan.ToString(),
                                                                                            subkeg.IIDRekening.ToString(),KodeUK.ToString()};
                    gridAnggaranKas.Rows.Add(strSubkegiatan);
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


                    string[] row = { rek.IIDRekening.ToKodeRekening() ,rek.NamaRekening,
                                    rek.AnggaranRKAP.ToRupiahInReport(), rek.AnggaranRKAP.ToRupiahInReport(),
                                    rek.AnggaranRKAP.ToRupiahInReport(), rek.AnggaranABT.ToRupiahInReport(), "0", "0", "0", LEVEL_REKANING.ToString(),
                                                                                            rek.IDUrusan.ToString(),
                                                                                            rek.IDProgram.ToString(),
                                                                                            rek.IDKegiatan.ToString(),
                                                                                            rek.IDSubKegiatan.ToString(),
                                                                                            rek.IIDRekening.ToString(), KodeUK.ToString(),"0"};
                    gridAnggaranKas.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion 
        private int GetLevel(int Baris)
        {
            return DataFormat.GetInteger(gridAnggaranKas.Rows[Baris].Cells[COL_LEVEL].Value);
        }

        #region formatting

        private void FormatGrid()
        {
            FontStyle styleFont = new FontStyle();

            _hilightstyle.Font = new System.Drawing.Font(gridAnggaranKas.Font, FontStyle.Bold);
            _hilightstyle.ForeColor = Color.White;

            _hilightstyle.BackColor = Color.LightSlateGray;


            _level2style.Font = new System.Drawing.Font(gridAnggaranKas.Font, FontStyle.Bold);
            _level2style.BackColor = Color.Pink ;

            _level3style.Font = new System.Drawing.Font(gridAnggaranKas.Font, FontStyle.Bold);
            _level3style.BackColor = Color.LightSteelBlue;// new Font(gridKUA.Font, FontStyle.Bold);

            _level4style.Font = new System.Drawing.Font(gridAnggaranKas.Font, FontStyle.Regular);
            _level4style.BackColor = Color.LightGray;// new Font(gridKUA.Font, FontStyle.Bold);

            _level5style.Font = new System.Drawing.Font(gridAnggaranKas.Font, FontStyle.Italic);
            _level5style.BackColor = Color.Lavender;// new Font(gridKUA.Font, FontStyle.Bold);
            _level6style.Font = new System.Drawing.Font(gridAnggaranKas.Font, FontStyle.Italic);
            _level6style.BackColor = Color.Honeydew;// new Font(gridKUA.Font, FontStyle.Bold);

            _level7style.Font = new System.Drawing.Font(gridAnggaranKas.Font, FontStyle.Regular);

            _level7style.BackColor = Color.Honeydew;// new Font(gridKUA.Font, FontStyle.Bold);

            for (int idx = 0; idx < gridAnggaranKas.Rows.Count; idx++)
            {
                int level = GetLevel(idx);

                switch (level)
                {
                    case LEVEL_DINAS:
                        gridAnggaranKas.Rows[idx].DefaultCellStyle = _hilightstyle;

                        break;
                    case LEVEL_UNIT:
                        gridAnggaranKas.Rows[idx].DefaultCellStyle = _level2style;
                        break;

                    case LEVEL_URUSAN:
                        gridAnggaranKas.Rows[idx].DefaultCellStyle = _level3style;
                        break;

                    case LEVEL_PROGRAM:
                        gridAnggaranKas.Rows[idx].DefaultCellStyle = _level4style;
                        break;
                    case LEVEL_KEGIATAN:
                        gridAnggaranKas.Rows[idx].DefaultCellStyle = _level5style;
                        break;
                    case LEVEL_SUBKEGIATAN:
                        gridAnggaranKas.Rows[idx].DefaultCellStyle = _level6style;
                        break;
                }

            }
        }
        #endregion formatting
        #region displayAnggarankas
       

        private bool DisplayAnggaranKas()
        {
            try
            {
                m_IDSKPD = ctrlSKPD1.GetID();
                //  mAnggaranKasList
                mAnggaranKasList = new List<AnggaranKas>();
                AnggaranKasLogic oAKLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
                mAnggaranKasList = oAKLogic.Get(GlobalVar.TahunAnggaran,m_IDSKPD,2);
                if (mAnggaranKasList != null)
                {
                    DisplayDetail();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool DisplayDetail()
        {
            try
            {
                




                var q = from d in mAnggaranKasList
                        select new AnggaranKas
                        {
                            IDDinas = m_IDSKPD,
                            IDUrusan = d.IDUrusan,
                            IDProgram = d.IDProgram,
                            IDKegiatan = d.IDKegiatan,
                            IdSubKegiatan= d.IdSubKegiatan,
                            IDRekening = d.IDRekening,
                             KodeUK = d.KodeUK,
                              Bulan1 = d.Bulan1,
                            Bulan2 = d.Bulan2,
                            Bulan3 = d.Bulan3,
                            Bulan4 = d.Bulan4,
                            Bulan5 = d.Bulan5,
                            Bulan6 = d.Bulan6,
                            Bulan7 = d.Bulan7,
                            Bulan8 = d.Bulan8,
                            Bulan9 = d.Bulan9,
                            Bulan10 = d.Bulan10,
                            Bulan11 = d.Bulan11,
                            Bulan12 = d.Bulan12,

           

                        };



                mAnggaranKasList = q.ToList();



                if (mAnggaranKasList != null)
                {

                    ProsesSPJPerDinas();
                    List<int> lstKodeUK = new List<int>();
                    if (GlobalVar.gListOrganisasi == null)
                    {
                        GlobalVar.gListOrganisasi = new List<Unit>();
                    }
                    if (GlobalVar.gListOrganisasi.Count==0)
                    {
                        UnitKerjaLogic ukLogic = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
                        GlobalVar.gListOrganisasi = ukLogic.Get();
                    }

                    foreach (Unit u in GlobalVar.gListOrganisasi)
                    {
                        if (u.SKPD == m_IDSKPD)
                        {
                            lstKodeUK.Add(u.Kode);
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
                    FormatGrid();
                }
                else
                {
                    
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


        private void ProsesSPJPerDinas()
        {
            var lstJumlah = mAnggaranKasList.FindAll(x => x.IDDinas== m_IDSKPD).GroupBy(g => g.IDDinas)
             .Select(x => new
             {
                 IDDINAS = x.Key,
            
                 Bulan1 = x.Sum(y => y.Bulan1),
                 Bulan2 = x.Sum(y => y.Bulan2),
                 Bulan3 = x.Sum(y => y.Bulan3),
                 Bulan4 = x.Sum(y => y.Bulan4),
                 Bulan5 = x.Sum(y => y.Bulan5),
                 Bulan6 = x.Sum(y => y.Bulan6),
                 Bulan7 = x.Sum(y => y.Bulan7),
                 Bulan8 = x.Sum(y => y.Bulan8),
                 Bulan9 = x.Sum(y => y.Bulan9),
                 Bulan10 = x.Sum(y => y.Bulan10),
                 Bulan11 = x.Sum(y => y.Bulan11),
                 Bulan12 = x.Sum(y => y.Bulan12),
                 

             }).ToList();

            for (int idx = 0; idx < gridAnggaranKas.Rows.Count; idx++)
            {
                if (gridAnggaranKas.Rows[idx].Cells[COL_IDURUSAN].Value != null &&
                    gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value != null)
                {
                    if (DataFormat.GetInteger(gridAnggaranKas.Rows[idx].Cells[COL_IDDINAS].Value) == m_IDSKPD &&
                        DataFormat.GetInteger(gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_DINAS
                        )
                    {
                        if (lstJumlah.Count > 0)
                        {

      
                            gridAnggaranKas.Rows[idx].Cells[COL_JAN].Value = lstJumlah[0].Bulan1.ToRupiahInReport();
                            gridAnggaranKas.Rows[idx].Cells[COL_FEB].Value = lstJumlah[0].Bulan2.ToRupiahInReport();

                            gridAnggaranKas.Rows[idx].Cells[COL_MAR].Value = lstJumlah[0].Bulan3.ToRupiahInReport();
                            gridAnggaranKas.Rows[idx].Cells[COL_APR].Value = lstJumlah[0].Bulan4.ToRupiahInReport();
                            gridAnggaranKas.Rows[idx].Cells[COL_MEI].Value = lstJumlah[0].Bulan5.ToRupiahInReport();
                            gridAnggaranKas.Rows[idx].Cells[COL_JUN].Value = lstJumlah[0].Bulan6.ToRupiahInReport();
                            gridAnggaranKas.Rows[idx].Cells[COL_JUL].Value = lstJumlah[0].Bulan7.ToRupiahInReport();

                            gridAnggaranKas.Rows[idx].Cells[COL_AGU].Value = lstJumlah[0].Bulan8.ToRupiahInReport();
                            gridAnggaranKas.Rows[idx].Cells[COL_SEP].Value = lstJumlah[0].Bulan9.ToRupiahInReport();
                            gridAnggaranKas.Rows[idx].Cells[COL_OKT].Value = lstJumlah[0].Bulan10.ToRupiahInReport();
                            gridAnggaranKas.Rows[idx].Cells[COL_NOP].Value = lstJumlah[0].Bulan11.ToRupiahInReport();
                            gridAnggaranKas.Rows[idx].Cells[COL_DES].Value = lstJumlah[0].Bulan12.ToRupiahInReport();
 
                        }
                        break;
                    }
                }
            }
        }
        private void ProsesSPJPerUK(int KodeUK)
        {
            var lstJumlah = mAnggaranKasList.Where(u => u.KodeUK == KodeUK).GroupBy(x => x.KodeUK)
                   .Select(x => new
                   {
                       KodeUK = x.Key,



                       Bulan1 = x.Sum(y => y.Bulan1),
                       Bulan2 = x.Sum(y => y.Bulan2),
                       Bulan3 = x.Sum(y => y.Bulan3),
                       Bulan4 = x.Sum(y => y.Bulan4),
                       Bulan5 = x.Sum(y => y.Bulan5),
                       Bulan6 = x.Sum(y => y.Bulan6),
                       Bulan7 = x.Sum(y => y.Bulan7),
                       Bulan8 = x.Sum(y => y.Bulan8),
                       Bulan9 = x.Sum(y => y.Bulan9),
                       Bulan10 = x.Sum(y => y.Bulan10),
                       Bulan11 = x.Sum(y => y.Bulan11),
                       Bulan12 = x.Sum(y => y.Bulan12),



                   }).ToList();


            List<AnggaranKas> lstJumlahSPJPerurusan = (from t in mAnggaranKasList
                                                              join j in lstJumlah
                                                              on t.KodeUK equals j.KodeUK
                                                              select new AnggaranKas
                                                              {
                                                                  IDUrusan = 0,
                                                                  IDProgram = 0,
                                                                  IDKegiatan = 0,
                                                                  IdSubKegiatan = 0,
                                                                  IDRekening = 0,
                                                                  KodeUK = t.KodeUK,
                                                                  Bulan1=j.Bulan1,
                                                                  Bulan2 = j.Bulan2,
                                                                  Bulan3 = j.Bulan3,
                                                                  Bulan4 = j.Bulan4,
                                                                  Bulan5 = j.Bulan5,
                                                                  Bulan6 = j.Bulan6,
                                                                  Bulan7 = j.Bulan7,
                                                                  Bulan8 = j.Bulan8,
                                                                  Bulan9 = j.Bulan9,
                                                                  Bulan10 = j.Bulan10,
                                                                  Bulan11 = j.Bulan11,
                                                                  Bulan12 = j.Bulan12,


                                                              }).ToList<AnggaranKas>();//.Distinct();// < AnggaranKas>();

            List<AnggaranKas> lstJumlahSPJPerurusanDistincted = new List<AnggaranKas>();
            var lst = lstJumlahSPJPerurusan
             .Select(p => new
             {
                 p.KodeUK,
                 p.Bulan1,
                 p.Bulan2,
                 p.Bulan3,
                 p.Bulan4,
                 p.Bulan5,
                 p.Bulan6,
                 p.Bulan7,
                 p.Bulan8,
                 p.Bulan9,
                 p.Bulan10,
                 p.Bulan11,
                 p.Bulan12,
             })
             .Distinct().ToList();

            int oldKodeUK = 0;

            foreach (var u in lst)
            {

                if (u.KodeUK != oldKodeUK)
                {

                    for (int idx = 0; idx < gridAnggaranKas.Rows.Count; idx++)
                    {
                        if (gridAnggaranKas.Rows[idx].Cells[COL_IDURUSAN].Value != null &&
                            gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {
                            if (gridAnggaranKas.Rows[idx].Cells[COL_KODEUK].Value.ToString() == u.KodeUK.ToString() &&
                                DataFormat.GetInteger(gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_UNIT
                                )
                            {

                                gridAnggaranKas.Rows[idx].Cells[COL_JAN].Value = u.Bulan1.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_FEB].Value = u.Bulan2.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_MAR].Value = u.Bulan3.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_APR].Value = u.Bulan4.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_MEI].Value = u.Bulan5.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUN].Value = u.Bulan6.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUL].Value = u.Bulan7.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_AGU].Value = u.Bulan8.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_SEP].Value = u.Bulan9.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_OKT].Value = u.Bulan10.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_NOP].Value = u.Bulan11.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_DES].Value = u.Bulan12.ToRupiahInReport();

                            }
                        }
                    }
                    oldKodeUK = u.KodeUK;
                }

            }


        }
        private void ProsesSPJPerurusan(int KodeUK)
        {
            var lstJumlah = mAnggaranKasList.FindAll(sk => sk.KodeUK == KodeUK).GroupBy(x => x.IDUrusan)
                   .Select(x => new
                   {

                       IDUrusan = x.Key,
                       Bulan1 = x.Sum(y => y.Bulan1),
                       Bulan2 = x.Sum(y => y.Bulan2),
                       Bulan3 = x.Sum(y => y.Bulan3),
                       Bulan4 = x.Sum(y => y.Bulan4),
                       Bulan5 = x.Sum(y => y.Bulan5),
                       Bulan6 = x.Sum(y => y.Bulan6),
                       Bulan7 = x.Sum(y => y.Bulan7),
                       Bulan8 = x.Sum(y => y.Bulan8),
                       Bulan9 = x.Sum(y => y.Bulan9),
                       Bulan10 = x.Sum(y => y.Bulan10),
                       Bulan11 = x.Sum(y => y.Bulan11),
                       Bulan12 = x.Sum(y => y.Bulan12),



                   }).ToList();

            List<AnggaranKas> lstOnThisUK = mAnggaranKasList.FindAll(x => x.KodeUK == KodeUK);

            List<AnggaranKas> lstJumlahSPJPerurusan = (from t in lstOnThisUK
                                                              join j in lstJumlah
                                                              on t.IDUrusan equals j.IDUrusan
                                                              select new AnggaranKas
                                                              {

                                                                  IDUrusan = t.IDUrusan,
                                                                  IDProgram = 0,
                                                                  IDKegiatan = 0,
                                                                  IdSubKegiatan = 0,
                                                                  IDRekening = 0,
                                                                  KodeUK = KodeUK,
                                                                  Bulan1 = j.Bulan1,
                                                                  Bulan2 = j.Bulan2,
                                                                  Bulan3 = j.Bulan3,
                                                                  Bulan4 = j.Bulan4,
                                                                  Bulan5 = j.Bulan5,
                                                                  Bulan6 = j.Bulan6,
                                                                  Bulan7 = j.Bulan7,
                                                                  Bulan8 = j.Bulan8,
                                                                  Bulan9 = j.Bulan9,
                                                                  Bulan10 = j.Bulan10,
                                                                  Bulan11 = j.Bulan11,
                                                                  Bulan12 = j.Bulan12,


                                                              }).ToList<AnggaranKas>();//.Distinct();// < AnggaranKas>();

            List<AnggaranKas> lstJumlahSPJPerurusanDistincted = new List<AnggaranKas>();
            int oldUrusan = 0;
            foreach (AnggaranKas u in lstJumlahSPJPerurusan)
            {

                if (u.IDUrusan != oldUrusan)
                {

                    for (int idx = 0; idx < gridAnggaranKas.Rows.Count; idx++)
                    {
                        if (gridAnggaranKas.Rows[idx].Cells[COL_IDURUSAN].Value != null &&
                            gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {
                            if (gridAnggaranKas.Rows[idx].Cells[COL_IDURUSAN].Value.ToString() == u.IDUrusan.ToString() &&
                                gridAnggaranKas.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                               GetLevel(idx) == LEVEL_URUSAN
                                //DataFormat.GetInteger(gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_URUSAN
                                )
                            {
                                gridAnggaranKas.Rows[idx].Cells[COL_JAN].Value = u.Bulan1.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_FEB].Value = u.Bulan2.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_MAR].Value = u.Bulan3.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_APR].Value = u.Bulan4.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_MEI].Value = u.Bulan5.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUN].Value = u.Bulan6.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUL].Value = u.Bulan7.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_AGU].Value = u.Bulan8.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_SEP].Value = u.Bulan9.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_OKT].Value = u.Bulan10.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_NOP].Value = u.Bulan11.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_DES].Value = u.Bulan12.ToRupiahInReport();
                            }
                        }
                    }
                    oldUrusan = u.IDUrusan;
                }

            }


        }
        private void ProsesSPJPerProgram(int KodeUK)
        {
            var lstJumlah = mAnggaranKasList.Where(w => w.KodeUK == KodeUK).GroupBy(x => x.IDProgram)
                   .Select(x => new
                   {
                       IDProg = x.Key,

                       Bulan1 = x.Sum(y => y.Bulan1),
                       Bulan2 = x.Sum(y => y.Bulan2),
                       Bulan3 = x.Sum(y => y.Bulan3),
                       Bulan4 = x.Sum(y => y.Bulan4),
                       Bulan5 = x.Sum(y => y.Bulan5),
                       Bulan6 = x.Sum(y => y.Bulan6),
                       Bulan7 = x.Sum(y => y.Bulan7),
                       Bulan8 = x.Sum(y => y.Bulan8),
                       Bulan9 = x.Sum(y => y.Bulan9),
                       Bulan10 = x.Sum(y => y.Bulan10),
                       Bulan11 = x.Sum(y => y.Bulan11),
                       Bulan12 = x.Sum(y => y.Bulan12),

                   }).ToList();

            List<AnggaranKas> lstOnThisUK = mAnggaranKasList.FindAll(x => x.KodeUK == KodeUK);
            List<AnggaranKas> lstJumlahSPJPerProgram = (from t in lstOnThisUK
                                                               join j in lstJumlah
                                                               on t.IDProgram equals j.IDProg
                                                               select new AnggaranKas
                                                               {

                                                                   IDUrusan = t.IDUrusan,
                                                                   IDProgram = t.IDProgram,
                                                                   IDKegiatan = 0,
                                                                   IdSubKegiatan = 0,
                                                                   IDRekening = 0,
                                                                   KodeUK = KodeUK,
                                                                   Bulan1 = j.Bulan1,
                                                                   Bulan2 = j.Bulan2,
                                                                   Bulan3 = j.Bulan3,
                                                                   Bulan4 = j.Bulan4,
                                                                   Bulan5 = j.Bulan5,
                                                                   Bulan6 = j.Bulan6,
                                                                   Bulan7 = j.Bulan7,
                                                                   Bulan8 = j.Bulan8,
                                                                   Bulan9 = j.Bulan9,
                                                                   Bulan10 = j.Bulan10,
                                                                   Bulan11 = j.Bulan11,
                                                                   Bulan12 = j.Bulan12,


                                                               }).ToList<AnggaranKas>();


            var lst = lstJumlahSPJPerProgram
                   .Select(p => new
                   {
                       p.IDProgram,
                       p.Bulan1,
                       p.Bulan2,
                       p.Bulan3,
                       p.Bulan4,
                       p.Bulan5,
                       p.Bulan6,
                       p.Bulan7,
                       p.Bulan8,
                       p.Bulan9,
                       p.Bulan10,
                       p.Bulan11,
                       p.Bulan12,
                   })
                   .Distinct().ToList();



            int oldProgram = 0;

            // foreach (AnggaranKas u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {

                if (u.IDProgram != oldProgram)
                {
                    for (int idx = 0; idx < gridAnggaranKas.Rows.Count; idx++)
                    {
                        if (gridAnggaranKas.Rows[idx].Cells[COL_IDPROGRAM].Value != null &&
                            gridAnggaranKas.Rows[idx].Cells[COL_IDPROGRAM].Value != null)
                        {


                            if (gridAnggaranKas.Rows[idx].Cells[COL_IDPROGRAM].Value.ToString() == u.IDProgram.ToString() &&
                                gridAnggaranKas.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                DataFormat.GetInteger(gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_PROGRAM

                                )
                            {

                                gridAnggaranKas.Rows[idx].Cells[COL_JAN].Value = u.Bulan1.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_FEB].Value = u.Bulan2.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_MAR].Value = u.Bulan3.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_APR].Value = u.Bulan4.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_MEI].Value = u.Bulan5.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUN].Value = u.Bulan6.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUL].Value = u.Bulan7.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_AGU].Value = u.Bulan8.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_SEP].Value = u.Bulan9.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_OKT].Value = u.Bulan10.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_NOP].Value = u.Bulan11.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_DES].Value = u.Bulan12.ToRupiahInReport();
                            }
                        }
                    }

                }

            }

        }
        private void ProsesDisplayDetailPerKegiatan(int KodeUK)
        {
            List<AnggaranKas> lstOnThisUK = mAnggaranKasList.FindAll(x => x.KodeUK == KodeUK);
            var lstJumlah = lstOnThisUK.GroupBy(x => x.IDKegiatan)
                   .Select(x => new
                   {
                       IDKegiatan = x.Key,
                       Bulan1 = x.Sum(y => y.Bulan1),
                       Bulan2 = x.Sum(y => y.Bulan2),
                       Bulan3 = x.Sum(y => y.Bulan3),
                       Bulan4 = x.Sum(y => y.Bulan4),
                       Bulan5 = x.Sum(y => y.Bulan5),
                       Bulan6 = x.Sum(y => y.Bulan6),
                       Bulan7 = x.Sum(y => y.Bulan7),
                       Bulan8 = x.Sum(y => y.Bulan8),
                       Bulan9 = x.Sum(y => y.Bulan9),
                       Bulan10 = x.Sum(y => y.Bulan10),
                       Bulan11 = x.Sum(y => y.Bulan11),
                       Bulan12 = x.Sum(y => y.Bulan12),

                   }).ToList();


            List<AnggaranKas> lstJumlahSPJPerKegiatan = (from t in lstOnThisUK
                                                                join j in lstJumlah
                                                                on t.IDKegiatan equals j.IDKegiatan
                                                                select new AnggaranKas
                                                                {


                                                                    IDUrusan = t.IDUrusan,
                                                                    IDProgram = t.IDProgram,
                                                                    IDKegiatan = t.IDKegiatan,
                                                                    IdSubKegiatan = 0,
                                                                    IDRekening = 0,
                                                                    KodeUK = KodeUK,
                                                                    Bulan1 = j.Bulan1,
                                                                    Bulan2 = j.Bulan2,
                                                                    Bulan3 = j.Bulan3,
                                                                    Bulan4 = j.Bulan4,
                                                                    Bulan5 = j.Bulan5,
                                                                    Bulan6 = j.Bulan6,
                                                                    Bulan7 = j.Bulan7,
                                                                    Bulan8 = j.Bulan8,
                                                                    Bulan9 = j.Bulan9,
                                                                    Bulan10 = j.Bulan10,
                                                                    Bulan11 = j.Bulan11,
                                                                    Bulan12 = j.Bulan12,


                                                                }).ToList<AnggaranKas>();


            var lst = lstJumlahSPJPerKegiatan
                  .Select(p => new
                  {
                      p.IDProgram,
                      p.IDKegiatan,
                      p.Bulan1,
                      p.Bulan2,
                      p.Bulan3,
                      p.Bulan4,
                      p.Bulan5,
                      p.Bulan6,
                      p.Bulan7,
                      p.Bulan8,
                      p.Bulan9,
                      p.Bulan10,
                      p.Bulan11,
                      p.Bulan12,
                  })
                   .Distinct().ToList();




            int oldKegiatan = 0;

            // foreach (AnggaranKas u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {

                if (u.IDKegiatan != oldKegiatan)
                {
                    for (int idx = 0; idx < gridAnggaranKas.Rows.Count; idx++)
                    {
                        if (gridAnggaranKas.Rows[idx].Cells[COL_IDKEGIATAN].Value != null && gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridAnggaranKas.Rows[idx].Cells[COL_IDKEGIATAN].Value.ToString() == u.IDKegiatan.ToString() &&
                                gridAnggaranKas.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                GetLevel(idx) == LEVEL_KEGIATAN

                                )
                            {

                                gridAnggaranKas.Rows[idx].Cells[COL_JAN].Value = u.Bulan1.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_FEB].Value = u.Bulan2.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_MAR].Value = u.Bulan3.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_APR].Value = u.Bulan4.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_MEI].Value = u.Bulan5.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUN].Value = u.Bulan6.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUL].Value = u.Bulan7.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_AGU].Value = u.Bulan8.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_SEP].Value = u.Bulan9.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_OKT].Value = u.Bulan10.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_NOP].Value = u.Bulan11.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_DES].Value = u.Bulan12.ToRupiahInReport();
                            }
                        }
                    }
                    oldKegiatan = u.IDKegiatan;

                }

            }

        }
        private void ProsesDisplayDetailPerSubKegiatan(int KodeUK)
        {
            List<AnggaranKas> lstOnThisUK = mAnggaranKasList.FindAll(x => x.KodeUK == KodeUK);

            var lstJumlah = lstOnThisUK
                            .GroupBy(
                            c => new
                            {
                                c.IdSubKegiatan,

                            }
                            )
                   .Select(x => new
                   {
                       IIDRekening = 0,
                       IDSubKegiatan = x.Key.IdSubKegiatan,
                       Bulan1 = x.Sum(y => y.Bulan1),
                       Bulan2 = x.Sum(y => y.Bulan2),
                       Bulan3 = x.Sum(y => y.Bulan3),
                       Bulan4 = x.Sum(y => y.Bulan4),
                       Bulan5 = x.Sum(y => y.Bulan5),
                       Bulan6 = x.Sum(y => y.Bulan6),
                       Bulan7 = x.Sum(y => y.Bulan7),
                       Bulan8 = x.Sum(y => y.Bulan8),
                       Bulan9 = x.Sum(y => y.Bulan9),
                       Bulan10 = x.Sum(y => y.Bulan10),
                       Bulan11 = x.Sum(y => y.Bulan11),
                       Bulan12 = x.Sum(y => y.Bulan12),
                   }).ToList();


            List<AnggaranKas> lstJumlahSPJPerSubKegiatan = (from t in lstOnThisUK
                                                                   join j in lstJumlah
                                                                   on t.IdSubKegiatan equals j.IDSubKegiatan
                                                                   select new AnggaranKas
                                                                   {



                                                                       IDUrusan = t.IDUrusan,
                                                                       IDProgram = t.IDProgram,
                                                                       IDKegiatan = t.IDKegiatan,
                                                                       IdSubKegiatan = t.IdSubKegiatan,
                                                                       IDRekening = 0,
                                                                       KodeUK = KodeUK,
                                                                       Bulan1 = j.Bulan1,
                                                                       Bulan2 = j.Bulan2,
                                                                       Bulan3 = j.Bulan3,
                                                                       Bulan4 = j.Bulan4,
                                                                       Bulan5 = j.Bulan5,
                                                                       Bulan6 = j.Bulan6,
                                                                       Bulan7 = j.Bulan7,
                                                                       Bulan8 = j.Bulan8,
                                                                       Bulan9 = j.Bulan9,
                                                                       Bulan10 = j.Bulan10,
                                                                       Bulan11 = j.Bulan11,
                                                                       Bulan12 = j.Bulan12,


                                                                   }).ToList<AnggaranKas>();


            var lst = lstJumlahSPJPerSubKegiatan
                   .Select(p => new
                   {
                       p.IDProgram,
                       p.IDKegiatan,
                       p.IdSubKegiatan,
                       p.Bulan1,
                       p.Bulan2,
                       p.Bulan3,
                       p.Bulan4,
                       p.Bulan5,
                       p.Bulan6,
                       p.Bulan7,
                       p.Bulan8,
                       p.Bulan9,
                       p.Bulan10,
                       p.Bulan11,
                       p.Bulan12,
                   })
                   .Distinct().ToList();





            long oldIDSubKegiatan = 0;

            // foreach (AnggaranKas u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {

                if (u.IdSubKegiatan != oldIDSubKegiatan)
                {
                    for (int idx = 0; idx < gridAnggaranKas.Rows.Count; idx++)
                    {
                        if (gridAnggaranKas.Rows[idx].Cells[COL_IDSubKegiatan].Value != null && gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridAnggaranKas.Rows[idx].Cells[COL_IDSubKegiatan].Value.ToString() == u.IdSubKegiatan.ToString() &&
                                   gridAnggaranKas.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                DataFormat.GetInteger(gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_SUBKEGIATAN

                                )
                            {

                                gridAnggaranKas.Rows[idx].Cells[COL_JAN].Value = u.Bulan1.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_FEB].Value = u.Bulan2.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_MAR].Value = u.Bulan3.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_APR].Value = u.Bulan4.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_MEI].Value = u.Bulan5.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUN].Value = u.Bulan6.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUL].Value = u.Bulan7.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_AGU].Value = u.Bulan8.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_SEP].Value = u.Bulan9.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_OKT].Value = u.Bulan10.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_NOP].Value = u.Bulan11.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_DES].Value = u.Bulan12.ToRupiahInReport();

                            }
                        }
                    }
                    oldIDSubKegiatan = u.IdSubKegiatan;
                }


            }

        }
        private void ProsesDisplayDetailPerRekening(int KodeUK)
        {
            List<AnggaranKas> lstOnThisUK = mAnggaranKasList.FindAll(x => x.KodeUK == KodeUK);
            var lstJumlah = lstOnThisUK
                         .GroupBy(
                         c => new
                         {
                             c.IdSubKegiatan,
                             c.IDRekening,
                         }
                         )
                .Select(x => new
                {
                    IDRekening = x.Key.IDRekening,
                    IDSubKegiatan = x.Key.IdSubKegiatan,
                    Bulan1 = x.Sum(y => y.Bulan1),
                    Bulan2 = x.Sum(y => y.Bulan2),
                    Bulan3 = x.Sum(y => y.Bulan3),
                    Bulan4 = x.Sum(y => y.Bulan4),
                    Bulan5 = x.Sum(y => y.Bulan5),
                    Bulan6 = x.Sum(y => y.Bulan6),
                    Bulan7 = x.Sum(y => y.Bulan7),
                    Bulan8 = x.Sum(y => y.Bulan8),
                    Bulan9 = x.Sum(y => y.Bulan9),
                    Bulan10 = x.Sum(y => y.Bulan10),
                    Bulan11 = x.Sum(y => y.Bulan11),
                    Bulan12 = x.Sum(y => y.Bulan12),
                }).ToList();


            List<AnggaranKas> lstJumlahSPJPerSubKegiatanRekening = (from t in lstOnThisUK
                                                                           join j in lstJumlah
                                                                          on t.IdSubKegiatan equals j.IDSubKegiatan
                                                                           where t.IDRekening == j.IDRekening
                                                                           select new AnggaranKas
                                                                           {
                                                                               IDUrusan = t.IDUrusan,
                                                                               IDProgram = t.IDProgram,
                                                                               IDKegiatan = t.IDKegiatan,
                                                                               IdSubKegiatan = t.IdSubKegiatan,
                                                                               IDRekening = t.IDRekening,
                                                                               KodeUK = KodeUK,
                                                                               Bulan1 = j.Bulan1,
                                                                               Bulan2 = j.Bulan2,
                                                                               Bulan3 = j.Bulan3,
                                                                               Bulan4 = j.Bulan4,
                                                                               Bulan5 = j.Bulan5,
                                                                               Bulan6 = j.Bulan6,
                                                                               Bulan7 = j.Bulan7,
                                                                               Bulan8 = j.Bulan8,
                                                                               Bulan9 = j.Bulan9,
                                                                               Bulan10 = j.Bulan10,
                                                                               Bulan11 = j.Bulan11,
                                                                               Bulan12 = j.Bulan12,

                                                                           }).ToList<AnggaranKas>();


            var lst = lstJumlahSPJPerSubKegiatanRekening
                   .Select(p => new { p.IDProgram, p.IDKegiatan, p.IdSubKegiatan, p.IDRekening,
                                      p.Bulan1,
                                      p.Bulan2,
                                      p.Bulan3,
                                      p.Bulan4,
                                      p.Bulan5,
                                      p.Bulan6,
                                      p.Bulan7,
                                      p.Bulan8,
                                      p.Bulan9,
                                      p.Bulan10,
                                      p.Bulan11,
                                      p.Bulan12,


                   })
                   .Distinct().ToList();




            long oldIDSubKegiatan = 0;
            long oldIdRekening = 0;
            // foreach (AnggaranKas u in lstJumlahSPJPerProgram)
            foreach (var u in lst)
            {

                if (u.IdSubKegiatan != oldIDSubKegiatan || u.IDRekening != oldIdRekening)
                {
                    for (int idx = 0; idx < gridAnggaranKas.Rows.Count; idx++)
                    {
                        if (gridAnggaranKas.Rows[idx].Cells[COL_IDSubKegiatan].Value != null &&
                            gridAnggaranKas.Rows[idx].Cells[COL_IDREKENING].Value != null &&
                            gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridAnggaranKas.Rows[idx].Cells[COL_IDSubKegiatan].Value.ToString() == u.IdSubKegiatan.ToString() &&
                                gridAnggaranKas.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                gridAnggaranKas.Rows[idx].Cells[COL_IDREKENING].Value.ToString() == u.IDRekening.ToString() &&
                                GetLevel(idx) == LEVEL_REKANING
                                //gridAnggaranKas.Rows[idx].Cells[COL_LEVEL].Value.ToString() == "5"

                                )
                            {
                                gridAnggaranKas.Rows[idx].Cells[COL_JAN].Value = u.Bulan1.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_FEB].Value = u.Bulan2.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_MAR].Value = u.Bulan3.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_APR].Value = u.Bulan4.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_MEI].Value = u.Bulan5.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUN].Value = u.Bulan6.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_JUL].Value = u.Bulan7.ToRupiahInReport();

                                gridAnggaranKas.Rows[idx].Cells[COL_AGU].Value = u.Bulan8.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_SEP].Value = u.Bulan9.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_OKT].Value = u.Bulan10.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_NOP].Value = u.Bulan11.ToRupiahInReport();
                                gridAnggaranKas.Rows[idx].Cells[COL_DES].Value = u.Bulan12.ToRupiahInReport();
                            }
                        }
                    }
                    oldIDSubKegiatan = u.IdSubKegiatan;
                }


            }

        }
        #endregion displayAnggaranKas
        private void frmAnggaranKasRekap_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Rekap Anggaran Kas");
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
          
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlSKPD1.SetID(GlobalVar.Pengguna.SKPD);
             
            }
        }

        private void cmdExcell_Click(object sender, EventArgs e)
        {
            string NamaFile = "";
            try
            {

                NamaFile = BuatFile();
                if (NamaFile == "")
                {
                    MessageBox.Show("Belum ditentukan nama filenya..");
                    return;
                }
                // creating Excel Application  
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                // creating new WorkBook within Excel application  
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                // see the excel sheet behind the program  
                app.Visible = true;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                // changing the name of active sheet  
                worksheet.Name = "Exported from gridview";
                // storing header part in Excel  
               
                // storing Each row and column value to excel sheet  
                List<int> lstColToCetak = new List<int>();
                lstColToCetak.Add(1);
                lstColToCetak.Add(2);
                lstColToCetak.Add(COL_ANGGARANMURNI);
                lstColToCetak.Add(COL_JAN);
                lstColToCetak.Add(COL_FEB);
                lstColToCetak.Add(COL_MAR);
                lstColToCetak.Add(COL_APR);
                lstColToCetak.Add(COL_MEI);
                lstColToCetak.Add(COL_JUN);
                lstColToCetak.Add(COL_JUL);
                lstColToCetak.Add(COL_AGU);
                lstColToCetak.Add(COL_SEP);
                lstColToCetak.Add(COL_OKT);
                lstColToCetak.Add(COL_NOP);
                lstColToCetak.Add(COL_DES);

                //for (int i = 1; i < gridAnggaranKas.Columns.Count + 1; i++)
                foreach (int i in lstColToCetak)
                  
                {
                    worksheet.Cells[1, i] = gridAnggaranKas.Columns[lstColToCetak[i] - 1].HeaderText;
                }



                for (int i = 0; i < gridAnggaranKas.Rows.Count - 1; i++)
                {
                    //for (int j = 1; j < gridAnggaranKas.Columns.Count + 1; j++)
                    //{
                    foreach (int j in lstColToCetak){

                        if (gridAnggaranKas.Rows[i].Cells[j].Value != null)
                        {


                            worksheet.Cells[i + 2, j + 1] = gridAnggaranKas.Rows[i].Cells[lstColToCetak[j]].Value.ToString();
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1] = "";
                        }
                    }
                }
                // save the application  
                workbook.SaveAs(NamaFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                // Exit from the application 

                MessageBox.Show("Selesai export ke Excell. Disimpan di: " + NamaFile);
                app.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke Excell." + ex.Message);
                if (File.Exists(NamaFile) == true)
                {
                    File.Delete(NamaFile);

                }
                MessageBox.Show(ex.Message);
            }
        }

       
       private string BuatFile(){

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


           

            //if (fdlg.FileName != "")
            //{
                // Saves the Image via a FileStream created by the OpenFile method.  
               
                sRet = fdlg.FileName;


          //  }
            return sRet;
        }

       private void cmdCari_Click(object sender, EventArgs e)
       {
try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridAnggaranKas.Rows)
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
                    gridAnggaranKas.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridAnggaranKas.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void gridAnggaranKas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
    }
}
