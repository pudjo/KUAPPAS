using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DTO;
using DTO.Anggaran;
using BP;
using BP.Anggaran;
using DTO.Laporan;
using Formatting;

namespace KUAPPAS.Anggaran
{
    public partial class treeAnggaran : UserControl
    {
        private List<ProgramKegiatanAnggaran> m_lstProgramKegiatan;  
        private int m_IDDInas = 0;
        private int m_iKodeUK = 0;
        private int m_iTahap;
        private string m_sNamaDinas;
        public treeAnggaran()
        {
            InitializeComponent();
            m_IDDInas = 0;
            m_iKodeUK = 0;
            m_iTahap = 2;
            m_sNamaDinas = "";

        }

        private void treeAnggaran_Load(object sender, EventArgs e)
        {
            treeGridProgram.FormatHeader();
        }

        public int DInas
        {
            set
            {
                m_IDDInas = value;
            }

        }
        public int KodeUK
        {
            set
            {
                m_iKodeUK = value;
            }
        }
        public int Tahap
        {
            set
            {
                m_iTahap = value;
            }
        }
        public string NamaDinas
        {
            set
            {
                m_sNamaDinas = value;
            }
        }
        public bool SetData(ref List<ProgramKegiatanAnggaran> listProgramKegiatan)
        {
            try
            {
                m_lstProgramKegiatan = listProgramKegiatan;

                return true;
            }
            catch (Exception ex)
            {

                return false;

            }

        }
        public bool Create()
        {
            try
            {
                if (GlobalVar.Pengguna.SKPD > 0)
                {
                    if (GlobalVar.Pengguna.SKPD != m_IDDInas)
                    {
                        return false;
                    }

                }
                if (GlobalVar.Pengguna.SKPD > 0)
                {
                    if (GlobalVar.Pengguna.SKPD != m_IDDInas)
                    {
                        return false;
                    }

                }

                if (m_lstProgramKegiatan.Count == 0)
                {
                    if (LoadProgramKegiatan() == false)
                    {
                        return false;
                    }

                }            
               

                DisplayProgramKegiatanSubKegiatan();
                SembunyikanKolomBasdTahap();
                if (GlobalVar.Pengguna.KodeUK <= 1 || GlobalVar.Pengguna.Kelompok == 1 ||
                    GlobalVar.Pengguna.Kelompok == 1000
                    )
                {
                    treeGridProgram.Columns[9].Visible = true  ;
                }
                else
                {
                    treeGridProgram.Columns[9].Visible = false ;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuat tampilan anggaran");
                return false;
            }
        }
        private void SembunyikanKolomBasdTahap()
        {
            
            switch (m_iTahap)
            {
                case 2:
                 

                    treeGridProgram.Columns[2].Visible = true;
                    treeGridProgram.Columns[3].Visible = false;
                    treeGridProgram.Columns[4].Visible = false;
                    treeGridProgram.Columns[5].Visible = false;
         


                    break;
                case 3:
    
                    treeGridProgram.Columns[2].Visible = true;
                    treeGridProgram.Columns[3].Visible = true;
                    treeGridProgram.Columns[4].Visible = false;
                    treeGridProgram.Columns[5].Visible = false;
              

                    break;
                case 4:
                    
                    treeGridProgram.Columns[2].Visible = true;
                    treeGridProgram.Columns[3].Visible = true;
                    treeGridProgram.Columns[4].Visible = true;
                    treeGridProgram.Columns[5].Visible = false;
             
                    break;
                case 5:
                 
                    treeGridProgram.Columns[2].Visible = true;
                    treeGridProgram.Columns[3].Visible = true;
                    treeGridProgram.Columns[4].Visible = true;
                    treeGridProgram.Columns[5].Visible = true;
                  
                    break;

            }
        }

        public bool LoadProgramKegiatan()
        {

            try
            {

                m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
                ProgramKegiatanAnggaranLogic oLogic = new ProgramKegiatanAnggaranLogic(GlobalVar.TahunAnggaran);
                m_lstProgramKegiatan = oLogic.GetByDInas(m_IDDInas, m_iKodeUK);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }




        }
        private void DisplayProgramKegiatanSubKegiatan()
        {
            try
            {
                System.Drawing.Font boldFont = new System.Drawing.Font(treeGridProgram.DefaultCellStyle.Font, FontStyle.Bold);



                int oldKodeUK = -1;
                treeGridProgram.Rows.Clear();

                List<ProgramKegiatanAnggaran> lstUnit = new List<ProgramKegiatanAnggaran>();

                m_lstProgramKegiatan.OrderBy(x => x.KodeUK);
                // ***********************************
                foreach (ProgramKegiatanAnggaran uk in m_lstProgramKegiatan)
                {
                    if (oldKodeUK != uk.KodeUK)
                    {
                        lstUnit.Add(uk);
                        oldKodeUK = uk.KodeUK;
                    }
                }

                var lstJumlahUK = m_lstProgramKegiatan.GroupBy(x => x.KodeUK)
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


                foreach (ProgramKegiatanAnggaran p in lstUKDanAnggaran)
                {
                    if ((p.KodeUK != oldKodeUK))
                    {
                        TreeGridNode ukNode = treeGridProgram.Nodes.Add("", p.KodeUK.ToString() + "-" + p.NamaUK, p.AnggaranMurni.ToRupiahInReport(), p.AnggaranGeser.ToRupiahInReport(), p.AnggaranRKAP.ToRupiahInReport(), p.AnggaranABT.ToRupiahInReport(), "1");
                        ukNode.DefaultCellStyle.Font = boldFont;
                        LoadThisUK(ukNode, p.KodeUK);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadThisUK(TreeGridNode node, int KodeUK)
        {

            try
            {
                //System.Drawing.Font boldFont = new System.Drawing.Font(node.DefaultCellStyle.Font, FontStyle.Bold);
                System.Drawing.Font boldFont = new System.Drawing.Font(treeGridProgram.DefaultCellStyle.Font, FontStyle.Bold);
                int oldUK = -1;
                int oldIdUrusan = 0;
                int oldIdProgram = 0;
                int oldKodeUK = -1;
                List<ProgramKegiatanAnggaran> lstUrusan = new List<ProgramKegiatanAnggaran>();
                List<ProgramKegiatanAnggaran> lstProgramKegiatanThisUK = new List<ProgramKegiatanAnggaran>();

                lstProgramKegiatanThisUK = m_lstProgramKegiatan.FindAll(x => x.KodeUK == KodeUK);

                // ***********************************
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
                        TreeGridNode urusannode = node.Nodes.Add("", p.StrIDUrusan + "-" + p.NamaUrusan, p.AnggaranMurni.ToRupiahInReport(), p.AnggaranGeser.ToRupiahInReport(), p.AnggaranRKAP.ToRupiahInReport(), p.AnggaranABT.ToRupiahInReport(), "1");


                        List<ProgramKegiatanAnggaran> lstProgram = new List<ProgramKegiatanAnggaran>();
                        List<ProgramKegiatanAnggaran> lstPDistinctrogram = new List<ProgramKegiatanAnggaran>();
                        oldIdUrusan = p.IDUrusan;
                        urusannode.DefaultCellStyle.Font = boldFont;
                        // ***********************
                        lstProgram = lstProgramKegiatanThisUK.FindAll(prog => prog.IDUrusan == oldIdUrusan);
                        foreach (ProgramKegiatanAnggaran program in lstProgram)
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
                                TreeGridNode nodeprogram = urusannode.Nodes.Add("", pr.StrIDProgram + "-" + pr.NamaProgram, pr.AnggaranMurni.ToRupiahInReport(), pr.AnggaranGeser.ToRupiahInReport(), pr.AnggaranRKAP.ToRupiahInReport(), pr.AnggaranABT.ToRupiahInReport(), "2");
                                oldIdProgram = pr.IDProgram;
                                ProcessKegiatan(nodeprogram, KodeUK, oldIdProgram);
                                nodeprogram.DefaultCellStyle.Font = boldFont;
                            }
                        }
                    }

                }
            }




            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        private void ProcessKegiatan(TreeGridNode node, int KodeUK, int idProgram)
        {
            int oldKegiatan;


            oldKegiatan = 0;
            List<ProgramKegiatanAnggaran> lstKegiatan = new List<ProgramKegiatanAnggaran>();
            List<ProgramKegiatanAnggaran> lstDistinctKegiatan = new List<ProgramKegiatanAnggaran>();
            List<ProgramKegiatanAnggaran> lstProgramKegiatanThisUK = new List<ProgramKegiatanAnggaran>();

            lstProgramKegiatanThisUK = m_lstProgramKegiatan.FindAll(x => x.KodeUK == KodeUK);

            lstKegiatan = lstProgramKegiatanThisUK.FindAll(keg => keg.IDProgram == idProgram);
            foreach (ProgramKegiatanAnggaran kegiatan in lstKegiatan)
            {
                if (oldKegiatan != kegiatan.IDKegiatan)
                {
                    lstDistinctKegiatan.Add(kegiatan);
                    oldKegiatan = kegiatan.IDKegiatan;
                }
            }

            var lstJumlahKegiatan = lstProgramKegiatanThisUK.GroupBy(x => x.IDKegiatan)
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

                    TreeGridNode nodekegiatan = node.Nodes.Add("", keg.StrIDKegiatan + "-" + keg.NamaKegiatan,
                                        keg.AnggaranMurni.ToRupiahInReport(), keg.AnggaranGeser.ToRupiahInReport(), keg.AnggaranRKAP.ToRupiahInReport(), keg.AnggaranABT.ToRupiahInReport(), "3");
                    oldKegiatan = keg.IDKegiatan;
                    ProcessSubKegiatan(nodekegiatan, KodeUK, oldKegiatan);


                }
            }
        }
        private void ProcessSubKegiatan(TreeGridNode node, int KodeUK, int idKegiatan)
        {
            long oldSubKegiatan;
            oldSubKegiatan = 0;

            List<ProgramKegiatanAnggaran> lstKSubegiatan = new List<ProgramKegiatanAnggaran>();
            List<ProgramKegiatanAnggaran> lstDistinctSubKegiatan = new List<ProgramKegiatanAnggaran>();

            List<ProgramKegiatanAnggaran> lstProgramKegiatanThisUK = new List<ProgramKegiatanAnggaran>();
            lstProgramKegiatanThisUK = m_lstProgramKegiatan.FindAll(x => x.KodeUK == KodeUK);


            lstKSubegiatan = m_lstProgramKegiatan.FindAll(keg => keg.IDKegiatan == idKegiatan && keg.KodeUK==KodeUK);
            foreach (ProgramKegiatanAnggaran subkegiatan in lstKSubegiatan)
            {
                if (oldSubKegiatan != subkegiatan.IDSubKegiatan)
                {
                    lstDistinctSubKegiatan.Add(subkegiatan);
                    oldSubKegiatan = subkegiatan.IDSubKegiatan;
                }
            }

            var lstJumlahSubKegiatan = lstProgramKegiatanThisUK.GroupBy(x => x.IDSubKegiatan)
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

                    TreeGridNode nodeSubkegiatan = node.Nodes.Add("Detail..", subkeg.IDSubKegiatan.TampilanSubKegiatan() + "-" + subkeg.NamaSubKegiatan,
                                        subkeg.AnggaranMurni.ToRupiahInReport(), subkeg.AnggaranGeser.ToRupiahInReport(),
                                        subkeg.AnggaranRKAP.ToRupiahInReport(), subkeg.AnggaranABT.ToRupiahInReport(), "4", subkeg.IDSubKegiatan.ToString(), subkeg.KodeUK.ToString(),"Set Unit/Bagian");
                    oldSubKegiatan = subkeg.IDSubKegiatan;
                    ProcessRekening(nodeSubkegiatan, KodeUK, oldSubKegiatan);


                }
            }
        }
        private void ProcessRekening(TreeGridNode node, int KodeUK, long idSubKegiatan)
        {

            List<ProgramKegiatanAnggaran> lstRekening = new List<ProgramKegiatanAnggaran>();
            List<ProgramKegiatanAnggaran> lstDistinctSubKegiatan = new List<ProgramKegiatanAnggaran>();
            List<ProgramKegiatanAnggaran> lstProgramKegiatanThisUK = new List<ProgramKegiatanAnggaran>();
            lstProgramKegiatanThisUK = m_lstProgramKegiatan.FindAll (x => x.KodeUK == KodeUK);

            if (KodeUK == 29)
            {
                KodeUK = 29;
            }

            lstRekening = lstProgramKegiatanThisUK.FindAll(x=>x.IIDRekening>0 && x.IDSubKegiatan == idSubKegiatan &&
                                                            x.KodeUK == KodeUK);



            foreach (ProgramKegiatanAnggaran rek in lstRekening)
            {

                if (rek.IIDRekening > 0)
                {
                    TreeGridNode nodekegiatan = node.Nodes.Add("", rek.IIDRekening.ToKodeRekening() + " - " + rek.NamaRekening,
                                        rek.AnggaranMurni.ToRupiahInReport(), rek.AnggaranGeser.ToRupiahInReport(),
                                        rek.AnggaranRKAP.ToRupiahInReport(), rek.AnggaranABT.ToRupiahInReport(), "5", rek.IDSubKegiatan.ToString(), rek.KodeUK.ToString());



                }
            }
        }

        private void treeGridProgram_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (DataFormat.GetInteger(treeGridProgram.Rows[e.RowIndex].Cells[6].Value) == 4)
                    {
                        List<ProgramKegiatanAnggaran> lstpk = new List<ProgramKegiatanAnggaran>();
                        foreach (ProgramKegiatanAnggaran p in m_lstProgramKegiatan)
                        {
                            if (p.IDSubKegiatan == DataFormat.GetLong(treeGridProgram.Rows[e.RowIndex].Cells[7].Value) &&
                                 p.KodeUK == DataFormat.GetInteger(treeGridProgram.Rows[e.RowIndex].Cells[8].Value))
                            {

                                lstpk.Add(p);
                            }
                            Console.WriteLine(p.IDSubKegiatan.ToString() + "   " + p.KodeUK.ToString());
                        }

                        frmEditAnggaran fEditAnggaran = new frmEditAnggaran();
                        fEditAnggaran.IDDInas = m_IDDInas;
                        fEditAnggaran.KodeUk = DataFormat.GetInteger(treeGridProgram.Rows[e.RowIndex].Cells[8].Value);
                        fEditAnggaran.NamaDinas = m_sNamaDinas;
                        
                        if (lstpk.Count == 0)
                        {
                            MessageBox.Show("Ada kesalahan pembacaan data", "Hubungi support..");
                            return;

                        }
                        fEditAnggaran.SetProgramKegiatanAnggaran(lstpk);


                        fEditAnggaran.ShowDialog();

                    }
                }
                if (e.ColumnIndex == 9)
                {
                    

                    frmPilihUnitKerja fPilihUK = new frmPilihUnitKerja();
                    fPilihUK.Dinas = m_IDDInas;
                    long idSUbKegiatan = DataFormat.GetLong(treeGridProgram.Rows[e.RowIndex].Cells[7].Value);
                    long idRekening = DataFormat.GetLong(treeGridProgram.Rows[e.RowIndex].Cells[8].Value);
                    int Kodeuklama = DataFormat.GetInteger(treeGridProgram.Rows[e.RowIndex].Cells[8].Value);

                    if (idSUbKegiatan > 0 && idRekening < 500000000000) { 
                    fPilihUK.ShowDialog();
                    if (fPilihUK.OK == true)
                        {


                            int kodeUK = fPilihUK.KodeUK;
                            string NamaUK = fPilihUK.NamaUnit;
                            TSubKegiatanLogic oSubKegiatanLogic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, 1);
                            if (MessageBox.Show("Akan menyeting Sub Kegiatan ini ke Unit/bagian " + NamaUK, "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                if (oSubKegiatanLogic.UpdateUK(Kodeuklama,idSUbKegiatan, m_IDDInas, kodeUK, NamaUK) == true)
                                {
                                    MessageBox.Show("Sub Kegiatan sudah di set Unit Kerjanya...");
                                }
                                else
                                {
                                    MessageBox.Show("Kesalahan Setting Unit/bagian");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Batal set Unit Kerja Sub Kegiatan...");
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
