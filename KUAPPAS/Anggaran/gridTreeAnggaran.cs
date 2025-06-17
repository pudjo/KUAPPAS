using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BP.Bendahara;
using DTO.Anggaran;
using BP.Anggaran;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using DTO;

using BP;
using Formatting;
namespace KUAPPAS.Anggaran
{
    public partial class gridTreeAnggaran : UserControl
    {
        List<RekapAnggaran> _lstRekap2 = new List<RekapAnggaran>();
        private List<ProgramKegiatanAnggaran> m_lstProgramKegiatan;

        private List<DisplaySPD> m_lstDisplaySPD;
        List<SPD> mListSPD;
        List<SPDDetail> m_lstSPDDetail;
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
        private const int COL_SPDINI = 7;
        private const int COL_SISA = 8;

        private const int COL_LEVEL = 9;
        private const int COL_IDURUSAN = 10;
        private const int COL_IDPROGRAM = 11;
        private const int COL_IDKEGIATAN = 12;
        private const int COL_IDSUBKEGIATAN = 13;
        private const int COL_IDREKENING = 14;
        private const int COL_IDDINAS = 0;
        private const int COL_KODEUK = 15;


        private const int LEVEL_DINAS = 0;
        private const int LEVEL_UNIT = 1;
        private const int LEVEL_URUSAN = 2;
        private const int LEVEL_PROGRAM = 3;
        private const int LEVEL_KEGIATAN = 4;
        private const int LEVEL_SUBKEGIATAN = 5;
        private const int LEVEL_REKANING = 6;




        private decimal m_cJumlahDPA;
        private decimal m_cJumlahSPDSebelum;
        private decimal m_cJumlahSISADANASEBELUM;
        private decimal m_cJumlahSPD;
        private decimal m_cJumlahSISADPA;
        private decimal m_cJumlahTotalSPD;
        SKPD oAKPD;
        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _level2style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level3style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level4style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level5style = new DataGridViewCellStyle();

        DataGridViewCellStyle _redstyle = new DataGridViewCellStyle();

        DataGridViewCellStyle _normalstyle = new DataGridViewCellStyle();

        //public delegate void GetKegiatan();
        //public GetKegiatan getKegiatan;
        private int prevJenis = 0;
        private int preDinas = 0;

        private int m_IDSKPD;
        private Single m_iStatus;

        public gridTreeAnggaran()
        {
            InitializeComponent();
            oAKPD = new SKPD();

        }

        private void gridTreeAnggaran_Load(object sender, EventArgs e)
        {

        }
        public bool LoadProgramKegiatan()
        {
            //  m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
            ProgramKegiatanAnggaranLogic oLogic = new ProgramKegiatanAnggaranLogic(GlobalVar.TahunAnggaran);

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
        private void DisplayProgramKegiatanSubKegiatan()
        {
            try
            {

                gridSPDDetail.Rows.Clear();
                // ***********************************

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
                             m_IDSKPD.ToString(), oAKPD.Nama, 
                                                                                            totalOPD.AnggaranMurni.ToRupiahInReport(), 
                                                                                            totalOPD.AnggaranGeser.ToRupiahInReport(), 
                                                                                            totalOPD.AnggaranRKAP.ToRupiahInReport(), 
                                                                                            totalOPD.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_DINAS.ToString(),
                                                                                            "0",
                                                                                            "0",
                                                                                            "0",
                                                                                            "0",
                                                                                            "0" ,"0" };
                gridSPDDetail.Rows.Add(rowOPD);
                ProcessUnit();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

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
                            gridSPDDetail.Rows.Add(rowOPD);
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
                int oldIdProgram = 0;
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
                        gridSPDDetail.Rows.Add(row);

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
                        gridSPDDetail.Rows.Add(rowProgram);

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

                        gridSPDDetail.Rows.Add(rowkegiatan);

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
                IdSubKegiatan = x.Key,
                JumlahMurni = x.Sum(y => y.AnggaranMurni),
                JumlahGeser = x.Sum(y => y.AnggaranGeser),
                JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                JumlahABT = x.Sum(y => y.AnggaranABT),

            }).ToList();

            List<ProgramKegiatanAnggaran> lstPSubKegiatanDanAnggaran = (from t in lstDistinctSubKegiatan
                                                                        join j in lstJumlahSubKegiatan
                                                                        on t.IDSubKegiatan equals j.IdSubKegiatan
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
                    gridSPDDetail.Rows.Add(strSubkegiatan);
                    oldSubKegiatan = subkeg.IDSubKegiatan;
                    ProcessRekening(oldSubKegiatan, KodeUK);


                }
            }
        }
        private void ProcessRekening(long idSubKegiatan, int KodeUK)
        {
            try
            {
                List<ProgramKegiatanAnggaran> lstRekening = new List<ProgramKegiatanAnggaran>();
                List<ProgramKegiatanAnggaran> lstDistinctSubKegiatan = new List<ProgramKegiatanAnggaran>();
                lstRekening = m_lstProgramKegiatan.FindAll(rek => rek.IDSubKegiatan == idSubKegiatan
                                              && rek.IDDInas == m_IDSKPD
                                              && rek.KodeUK == KodeUK);

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
                                                                                            rek.IIDRekening.ToString(), KodeUK.ToString()};
                    gridSPDDetail.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
