using BP;
using BP.Akuntansi;
using DTO;
using DTO.Akuntansi;
using DTO.Laporan;
using Formatting;
using Microsoft.Office.Interop.Word;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace KUAPPAS.Akunting
{
    public partial class frmLRA : ChildForm
    {

        private const long CON_BATAS_ATAS_PENDAPATAN = 500000000000;
        private const long CON_BATAS_ATAS_BELANJA = 600000000000;
        private const long CON_BATAS_ATAS_PENERIMAANPEMBIAYAAN = 620000000000;


        private const int COL_REALISASITRX = 3;
        private const int COL_SELISIHTRX = 4;
        private const int COL_PROSENTASETRX = 5;

        private const int COL_REALISASIBB = 7;
        private const int COL_SELISIHBB = 8;
        private const int COL_PROSENTASEBB = 9;
        private const int COL_COMMANDBB = 10;



        private List<Realisasi04AK> m_listRealisasiTrx;
        private List<Realisasi04AK> m_listRealisasiBukuBesar;

        private List<Realisasi04AK> m_listRealisasiPendapatanTrx;
        private List<Realisasi04AK> m_listRealisasiBelanjaTrx;
        private List<Realisasi04AK> m_listRealisasiPenerimaanPembiayaanTrx;
        private List<Realisasi04AK> m_listRealisasiPengeluaranPembiayaanTrx;

        private List<Realisasi04AK> m_listRealisasiPendapatanBB;
        private List<Realisasi04AK> m_listRealisasiBelanjaBB;
        private List<Realisasi04AK> m_listRealisasiPenerimaanPembiayaanBB;
        private List<Realisasi04AK> m_listRealisasiPengeluaranPembiayaanBB;



        // private List<Realisasi04AK> m_listRealisasiBukubesar;

        DateTime mTanggalAkhir;
        DateTime mTanggalAwal;

        private List<Rekening> m_lstRekening;

        private List<Rekening> m_lstRekeningPendapatan;
        private List<Rekening> m_lstRekeningBelanja;
        private List<Rekening> m_lstRekeningPenerimaanPembiayaan;
        private List<Rekening> m_lstRekeningPengeluaranPembiayaan;

        private List<TAnggaranRekening> m_lstAnggaran;

        private List<TAnggaranRekening> m_lstAnggaranPendapatan;
        private List<TAnggaranRekening> m_lstAnggaranBelanja;
        private List<TAnggaranRekening> m_lstAnggaranPenerimaanPembiayaan;
        private List<TAnggaranRekening> m_lstAnggaranPengeluaranPembiayaan;


        private int m_iSKPD;
        private int m_iKodeUK;


        CetakPDF oCetakPDF;
        PdfPage previousPage;
        int m_iJenisBendahara;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;

        decimal JumlahAnggaranBelanja = 0l;
        decimal JumlahAnggaranPendapatan = 0L;

        decimal JumlahAnggaranPenerimaan = 0l;
        decimal JumlahAnggaranPengeluaran = 0L;


        decimal JumlahBelanja = 0l;
        decimal JumlahPendapatan = 0L;
        decimal JumlahPenerimaan = 0l;
        decimal JumlahPengeluaran = 0L;

        public frmLRA()
        {
            InitializeComponent();
            m_listRealisasiTrx = new List<Realisasi04AK>();
            m_listRealisasiPendapatanTrx = new List<Realisasi04AK>();
            m_listRealisasiBelanjaTrx = new List<Realisasi04AK>();
            m_listRealisasiPenerimaanPembiayaanTrx = new List<Realisasi04AK>();
            m_listRealisasiPengeluaranPembiayaanTrx = new List<Realisasi04AK>();

            m_listRealisasiPendapatanBB = new List<Realisasi04AK>();
            m_listRealisasiBelanjaBB = new List<Realisasi04AK>();
            m_listRealisasiPenerimaanPembiayaanBB = new List<Realisasi04AK>();
            m_listRealisasiPengeluaranPembiayaanBB = new List<Realisasi04AK>();



        }

        private int GetLevelRekening()
        {

            int SelectedIndex = cmbLevelRekening.SelectedIndex;
            if (SelectedIndex > -1)
            {
                ListItemData li = (ListItemData)cmbLevelRekening.Items[SelectedIndex];
                return li.Itemdata;
            }
            else
            {
                return 0;
            }


        }
        private void frmLRA_Load(object sender, EventArgs e)
        {

            ctrlHeader1.SetCaption("Laporan Realisasi Anggaran");
            ctrlDinas1.Create();
            ctrlTanggalBulan1.TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
            if (DateTime.Now.Year > GlobalVar.TahunAnggaran)
            {
                ctrlTanggalBulan1.TanggalAkhir = new DateTime(GlobalVar.TahunAnggaran, 12, 31);
            }
            ListItemData li = new ListItemData("Akun Utama", 1);
            cmbLevelRekening.Items.Add(li);
            if (GlobalVar.Pengguna.UserID == "pudjo")
            {
                cjkAnggaranByPass.Visible = true;
            }
            li = new ListItemData("Akun Kelompok", 2);
            cmbLevelRekening.Items.Add(li);
            li = new ListItemData("Akun Jenis", 3);
            cmbLevelRekening.Items.Add(li);
            li = new ListItemData("Akun Objek", 4);
            cmbLevelRekening.Items.Add(li);
            li = new ListItemData("Akun Rincian Objek", 5);
            cmbLevelRekening.Items.Add(li);
            li = new ListItemData("Akun Sub Rincian Objek", 6);
            cmbLevelRekening.Items.Add(li);

            ctrlTanggalBulan1.Create();

            gridLRA.FormatHeader();
            SembunyikanTampilkanKolomNukuBesar(false);
            ctrlPencarian1.setGrid(ref gridLRA);

        }

        private void SembunyikanTampilkanKolomNukuBesar(bool tampil)
        {

            gridLRA.Columns[COL_REALISASIBB].Visible = tampil;
            gridLRA.Columns[COL_SELISIHBB].Visible = tampil;
            gridLRA.Columns[COL_PROSENTASEBB].Visible = tampil;
            gridLRA.Columns[COL_COMMANDBB].Visible = tampil;
            cmdCetakDariBukubesar.Visible = tampil;
            cmdExcellBukuBesar.Visible = tampil;
            for (int i = 0; i < gridLRA.Rows.Count; i++)
            {
                gridLRA.Rows[i].Cells[COL_COMMANDBB].Value= "Buku Besar";
            }




        }
        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {

        }
        private long GetMaximalIDRekengBelanja(List<Rekening> semuarekenings)
        {
            List<Rekening> rekenings = new List<Rekening>();
            rekenings = semuarekenings.FindAll(x => x.ID < CON_BATAS_ATAS_BELANJA).OrderByDescending(x => x.ID ).ToList();
            long rekening = rekenings[0].ID;
            int level = GetLevelRekening();
            string sID = rekening.ToString();
            switch (level)
            {
                case 2:
                    return DataFormat.ToLong(sID.Substring(0, 2) + "0000000000");
                case 3:
                    return DataFormat.ToLong(sID.Substring(0, 4) + "00000000");
                case 4:
                    return DataFormat.ToLong(sID.Substring(0, 6) + "000000");
                case 5:
                    return DataFormat.ToLong(sID.Substring(0, 8) +"0000");
                case 12:
                    return DataFormat.ToLong(sID.Substring(0, 8));

            }

            return 0;

        }
        private bool LoadRekening()
        {
            try
            {
                int iddinas = 0;
                if (chkSemuaAnggaran.Checked == false)
                {
                    iddinas=ctrlDinas1.GetID();
                }
                
                m_lstRekening = new List<Rekening>();
                RekeningLogic oRekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran);

                m_lstRekening = oRekeningLogic.GetInAnggaran(iddinas).Where(r => r.ID >= 400000000000 && r.ID < 700000000000).ToList();

                // Rekening MaxRekeningBelanja= m_lstRekening.Where(x=>x.ID < CON_BATAS_ATAS_BELANJA);
                if (m_lstRekening == null)
                {
                    MessageBox.Show(oRekeningLogic.LastError());
                    return false;
                }
                long rekeningMaksimalBelanja = GetMaximalIDRekengBelanja(m_lstRekening);
                m_lstRekeningPendapatan = new List<Rekening>();
                m_lstRekeningBelanja = new List<Rekening>();
                m_lstRekeningPenerimaanPembiayaan = new List<Rekening>();
                m_lstRekeningPengeluaranPembiayaan = new List<Rekening>();
                m_lstRekeningPendapatan = m_lstRekening.FindAll(x => x.ID < CON_BATAS_ATAS_PENDAPATAN);
                m_lstRekeningBelanja = m_lstRekening.FindAll(x => x.ID > CON_BATAS_ATAS_PENDAPATAN && x.ID < CON_BATAS_ATAS_BELANJA);
                m_lstRekeningPenerimaanPembiayaan = m_lstRekening.FindAll(x => x.ID > CON_BATAS_ATAS_BELANJA && x.ID < CON_BATAS_ATAS_PENERIMAANPEMBIAYAAN);
                m_lstRekeningPengeluaranPembiayaan = m_lstRekening.FindAll(x => x.ID > CON_BATAS_ATAS_PENERIMAANPEMBIAYAAN);
                int levelanggaran = GetLevelRekening();
                Rekening rek0 = new Rekening();
                rek0 = m_lstRekeningPendapatan.FirstOrDefault(x => x.ID == 410412150001);

                gridLRA.Rows.Clear();
                bool bFound = false; // ada pembiayaan untuk pencingsurplus defisit
                foreach (Rekening r in m_lstRekening)
                {
                    if (r.Root <= levelanggaran)
                    {
                        string[] row = { r.ID.ToKodeRekening((int)r.Root), r.Nama };
                        gridLRA.Rows.Add(row);
                    }

                    if (r.ID == rekeningMaksimalBelanja )
                    {
                        bFound = true;
                        string[] rowsd = { "SD", "SURPLUS DEFISIT"};
                        gridLRA.Rows.Add(rowsd);
                    }

                    

                }

                if (bFound == false)
                {
                    string[] rowsd = { "SD", "Surplus Defisit" };
                    gridLRA.Rows.Add(rowsd);
                }
                string[] dataPembiayaanNetto = { "PN", "PEMBIAYAAN NETTO" };
                gridLRA.Rows.Add(dataPembiayaanNetto);
                string[] dataSIlpa = { "SILPA", "SISA LEBIH PEMBIAYAAN ANGGARAN " };
                gridLRA.Rows.Add(dataSIlpa);


                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }

        }
        private void Tulis (string kode , int col, string nilai)
        {

            int baris = TemukanBaris(kode);
     
            try
            {
                if (baris >= 0 && baris < gridLRA.Rows.Count)
                {
                    gridLRA.Rows[baris].Cells[col].Value = nilai;
                }
            } catch(Exception ex)
            {
                MessageBox.Show("Baris " + baris.ToString() + " " + ex.Message );
            }

        }
        private bool DisplayAnggaran(int iddinas)
        {
            //return true;
            try
            {
                
                if (cjkAnggaranByPass.Checked == true)
                {
                    GlobalVar.gListRekeningAnggaran = new List<TAnggaranRekening>();
                    TAnggaranRekeningLogic oAnggaranRekeningLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
                    GlobalVar.gListRekeningAnggaran = oAnggaranRekeningLogic.Get(GlobalVar.TahunAnggaran, true);

                }
                //  m_
                //  m_lstNeracaAwal= new List
                m_lstAnggaran = new List<TAnggaranRekening>();

                m_lstAnggaranPendapatan = new List<TAnggaranRekening>();
                m_lstAnggaranBelanja = new List<TAnggaranRekening>();
                m_lstAnggaranPenerimaanPembiayaan = new List<TAnggaranRekening>();
                m_lstAnggaranPengeluaranPembiayaan = new List<TAnggaranRekening>();


                if (iddinas > 0)
                {
                    //hanya anggaran dinas itu
                    m_lstAnggaran = GlobalVar.gListRekeningAnggaran.FindAll(x => x.IDDinas == iddinas);
                }
                else
                {
                    m_lstAnggaran = GlobalVar.gListRekeningAnggaran;

                }








                m_lstAnggaranPendapatan = m_lstAnggaran.FindAll(x => x.IDRekening < CON_BATAS_ATAS_PENDAPATAN);
                m_lstAnggaranBelanja = m_lstAnggaran.FindAll(x => x.IDRekening > CON_BATAS_ATAS_PENDAPATAN && x.IDRekening < CON_BATAS_ATAS_BELANJA);
                m_lstAnggaranPenerimaanPembiayaan = m_lstAnggaran.FindAll(x => x.IDRekening > CON_BATAS_ATAS_BELANJA && x.IDRekening < CON_BATAS_ATAS_PENERIMAANPEMBIAYAAN);
                m_lstAnggaranPengeluaranPembiayaan = m_lstAnggaran.FindAll(x => x.IDRekening > CON_BATAS_ATAS_PENERIMAANPEMBIAYAAN);






                var lstJumlah = m_lstAnggaran.Where(x => x.IDRekening > 400000000000).GroupBy(x => x.IDRekening.ToString().Substring(0, 1))
                 .Select(x => new
                 {
                     IDRekening = x.Key,
                     JumlahMurni = x.Sum(y => y.JumlahMurni),
                     JumlahGeser = x.Sum(y => y.JumlahPergeseran),
                     JumlahRKAP = x.Sum(y => y.JumlahRKAP),
                     JumlahABT = x.Sum(y => y.JumlahABT),

                 }).ToList();




                switch (ctrlTahapAnggaran1.ID)
                {
                    case 2:
                        JumlahAnggaranBelanja = m_lstAnggaranBelanja.Sum(s => s.JumlahMurni);
                        JumlahAnggaranPendapatan = m_lstAnggaranPendapatan.Sum(s => s.JumlahMurni);
                        JumlahAnggaranPenerimaan = m_lstAnggaranPenerimaanPembiayaan.Sum(s => s.JumlahMurni);
                        JumlahAnggaranPengeluaran = m_lstAnggaranPengeluaranPembiayaan.Sum(s => s.JumlahMurni);
                        break;
                    case 3:
                        JumlahAnggaranBelanja = m_lstAnggaranBelanja.Sum(s => s.JumlahPergeseran);
                        JumlahAnggaranPendapatan = m_lstAnggaranPendapatan.Sum(s => s.JumlahPergeseran);
                        JumlahAnggaranPenerimaan = m_lstAnggaranPenerimaanPembiayaan.Sum(s => s.JumlahPergeseran);
                        JumlahAnggaranPengeluaran = m_lstAnggaranPengeluaranPembiayaan.Sum(s => s.JumlahPergeseran);
                        break;
                    case 4:
                        JumlahAnggaranBelanja = m_lstAnggaranBelanja.Sum(s => s.JumlahRKAP);
                        JumlahAnggaranPendapatan = m_lstAnggaranPendapatan.Sum(s => s.JumlahRKAP);
                        JumlahAnggaranPenerimaan = m_lstAnggaranPenerimaanPembiayaan.Sum(s => s.JumlahRKAP);
                        JumlahAnggaranPengeluaran = m_lstAnggaranPengeluaranPembiayaan.Sum(s => s.JumlahRKAP);
                        break;

                    case 5:
                        JumlahAnggaranBelanja = m_lstAnggaranBelanja.Sum(s => s.JumlahABT);
                        JumlahAnggaranPendapatan = m_lstAnggaranPendapatan.Sum(s => s.JumlahABT);
                        JumlahAnggaranPenerimaan = m_lstAnggaranPenerimaanPembiayaan.Sum(s => s.JumlahABT);
                        JumlahAnggaranPengeluaran = m_lstAnggaranPengeluaranPembiayaan.Sum(s => s.JumlahABT);
                        break;

                }

                //var query = from person in people
                //            where person.ID == 4
                //            join pet in pets on person equals pet.Owner into personpets
                //            from petOrNull in personpets.DefaultIfEmpty()
                //            select new { Person = person, Pet = petOrNull };


                List<Laporan> lstUrusanDanAnggaran = (from t in m_lstRekening
                                                      join j in lstJumlah
                                                     on t.ID.ToString().Substring(0, 1) equals j.IDRekening
                                                      where t.Root == 1

                                                      select new Laporan
                                                      {
                                                          Kode = t.ID.ToKodeRekening(1),
                                                          Level = 1,
                                                          Nama = t.Nama,
                                                          IDRekening = t.ID,
                                                          AnggaranMurni = j.JumlahMurni,
                                                          AnggaranGeser = j.JumlahGeser,
                                                          AnggaranRKAP = j.JumlahRKAP,
                                                          AnggaranABT = j.JumlahABT,


                                                      }).ToList<Laporan>();

                
                int maxLevel = GetLevelRekening();

                foreach (Laporan lp in lstUrusanDanAnggaran)
                {
                    if (GetLevelRekening() >= 1)
                    {

                        // JP, JB//SD//TB KB
                        if (JumlahAnggaranPendapatan > 0)
                        {
                            if (lp.IDRekening == 400000000000)
                            {
                                if (JumlahAnggaranPendapatan >= 0)
                                {
                                    //string[] dataLRA = { "4", "JUMLAH PENDAPATAAN", JumlahAnggaranPendapatan.ToRupiahInReport(), "0" };
                                    //int baris = TemukanBaris("4");
                                    Tulis("4", 2, JumlahAnggaranPendapatan.ToRupiahInReport());
                                    //gridLRA.Rows.Add(dataLRA);
                                    DisplayAnggaranOnLevel(m_lstAnggaranPendapatan, 2, lp.IDRekening);

                                }
                            }
                        }
                        else
                        {
                            //string[] dataLRA = { "4", "JUMLAH PENDAPATAAN", JumlahAnggaranPendapatan.ToRupiahInReport(), "0" };
                            Tulis("4", 2, JumlahAnggaranPendapatan.ToRupiahInReport());
                            //Tulis("4", 1, "JUMLAH PENDAPATAN");
                            //gridLRA.Rows.Add(dataLRA);
                        }

                        if (lp.IDRekening == 500000000000)
                        {
                            //string[] strJumlahBelanja = { "5", "JUMLAH BELANJA DAERAH", JumlahAnggaranBelanja.ToRupiahInReport(), "0" };
                            //gridLRA.Rows.Add(strJumlahBelanja);
                            Tulis("5",2, JumlahAnggaranBelanja.ToRupiahInReport());
                            DisplayAnggaranOnLevel(m_lstAnggaranBelanja, 2, lp.IDRekening);


                            //Tulis("SO", 2, (JumlahAnggaranPendapatan - JumlahAnggaranBelanja).ToRupiahInReport());
                            //Tulis("SO", 1, "SURPLUS DEFISIT");
                            Tulis("SD", 2, (JumlahAnggaranPendapatan - JumlahAnggaranBelanja).ToRupiahInReport());
                            //string[] strSurplusdefisit = { "SD", "SURPLUS/DEFISIT", (JumlahAnggaranPendapatan - JumlahAnggaranBelanja).ToRupiahInReport(), "0" };
                            //gridLRA.Rows.Add(strSurplusdefisit);

                            //if (JumlahAnggaranPenerimaan == 0 || JumlahAnggaranPengeluaran == 0)
                            //{
                    

                            //}

                        }

                     //   if (lp.IDRekening == 600000000000)
                       // {

                            if (m_lstAnggaranPenerimaanPembiayaan.Count > 0)
                                DisplayAnggaranOnLevel(m_lstAnggaranPenerimaanPembiayaan, 2, lp.IDRekening);

                            Tulis("61", 2, JumlahAnggaranPenerimaan.ToRupiahInReport());
                            //string[] strPenerimaanPembiayaan = { "6.1", "JUMLAH PENERIMAAN PEMBIAYAAN", JumlahAnggaranPenerimaan.ToRupiahInReport(), "0" };

                            //gridLRA.Rows.Add(strPenerimaanPembiayaan);

                            

                            if (m_lstAnggaranPengeluaranPembiayaan.Count > 0)
                                DisplayAnggaranOnLevel(m_lstAnggaranPengeluaranPembiayaan, 2, lp.IDRekening);
                            //string[] strPenngeluaranPembiayaan = { "6.2", "PENGELUARAN PEMBIAYAAN", JumlahAnggaranPengeluaran.ToRupiahInReport(), "0" };
                            //gridLRA.Rows.Add(strPenngeluaranPembiayaan);

                            Tulis("62", 2, JumlahAnggaranPengeluaran.ToRupiahInReport());
                            Tulis("PN", 2, (JumlahAnggaranPenerimaan - JumlahAnggaranPengeluaran).ToRupiahInReport());
                            Tulis("SILPA", 2, ((JumlahAnggaranPendapatan - JumlahAnggaranBelanja) + (JumlahAnggaranPenerimaan - JumlahAnggaranPengeluaran)).ToRupiahInReport());
                            ////string[] dataSIlpa = { "SILPA", "SISA LEBIH PEMBIAYAAN ANGGARAN " };
                            ////gridLRA.Rows.Add(dataSIlpa);

                        //}

                    }

                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }

        }
        private int TemukanBaris(string kode)
        {
            for (int idx=0; idx< gridLRA.Rows.Count; idx++)
            {
                if (DataFormat.GetString(gridLRA.Rows[idx].Cells[0].Value).Replace(".","") == kode.Replace(".", ""))
                {
                    return idx;
                }
            }
            return gridLRA.Rows.Count;

        }
        private bool DisplayAnggaranOnLevel(List<TAnggaranRekening> lstAnggaran, int Level, long IdParent)
        {
            //return true;

            int lenKode = 12;
            switch (Level)
            {
                case 1:
                    lenKode = 1;
                    break;
                case 2:
                    lenKode = 2;
                    break;
                case 3:
                    lenKode = 4;
                    break;
                case 4:
                    lenKode = 6;
                    break;
                case 5:
                    lenKode = 8;
                    break;
                case 6:
                    lenKode = 12;
                    break;
            }
            var lstJumlah = lstAnggaran.Where(l => l.IDRekening.ToString().Length == 12).GroupBy(x => x.IDRekening.ToString().Substring(0, lenKode))
              .Select(x => new
              {
                  IDRekening = x.Key,
                  JumlahMurni = x.Sum(y => y.JumlahMurni),
                  JumlahGeser = x.Sum(y => y.JumlahPergeseran),
                  JumlahRKAP = x.Sum(y => y.JumlahRKAP),
                  JumlahABT = x.Sum(y => y.JumlahABT),

              }).ToList();
            List<Laporan> lstUrusanDanAnggaran = (from t in m_lstRekening
                                                  join j in lstJumlah
                                                 on t.ID.ToString().Substring(0, lenKode) equals j.IDRekening
                                                  where t.Root == Level && t.IDParent == IdParent
                                                  select new Laporan
                                                  {
                                                      Kode = t.ID.ToKodeRekening(Level),
                                                      Level = Level,
                                                      Nama = t.Nama,
                                                      IDRekening = t.ID,
                                                      AnggaranMurni = j.JumlahMurni,
                                                      AnggaranGeser = j.JumlahGeser,
                                                      AnggaranRKAP = j.JumlahRKAP,
                                                      AnggaranABT = j.JumlahABT,


                                                  }).ToList<Laporan>();

            foreach (Laporan lp in lstUrusanDanAnggaran)
            {
                decimal anggaran = 0L;
                switch (ctrlTahapAnggaran1.ID)
                {
                    case 2:
                        anggaran = lp.AnggaranMurni;
                        break;
                    case 3:
                        anggaran = lp.AnggaranGeser;
                        break;
                    case 4:
                        anggaran = lp.AnggaranRKAP;
                        break;

                    case 5:
                        anggaran = lp.AnggaranABT;
                        break;

                }

                //anggaran > 0 &&

                if ((anggaran >= 0 && lp.Kode.Substring(0, 1) == "4") || (lp.Kode.Substring(0, 1) == "5") ||
                    (anggaran > 0 && lp.Kode.Substring(0, 1) == "6"))
                {
                    Tulis(lp.Kode, 2, anggaran.ToRupiahInReport());

                    //string[] dataLRA = { lp.Kode, lp.Nama, anggaran.ToRupiahInReport(), "0" };

                    //gridLRA.Rows.Add(dataLRA);

                    if (GetLevelRekening() > Level)
                    {
                        DisplayAnggaranOnLevel(lstAnggaran, Level + 1, lp.IDRekening);
                    }

                }


            }


            return true;

        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            try
            {

                if (GetLevelRekening() == 0)
                {
                    MessageBox.Show("Belum memilih Level Rekening.");
                    return;
                }
                if (ctrlTahapAnggaran1.ID == 0)
                {
                    MessageBox.Show("Belum memilih Tahap Anggaran");
                    return;
                }
                if (chkSemuaDinas.Checked == false)
                {
                    if (ctrlDinas1.GetID() == 0)
                    {
                        MessageBox.Show("Belum memilih OPD.");
                        return;
                    }
                }
                int iddinas = ctrlDinas1.GetID();

                if (LoadRekening() == true)
                {
                    DisplayAnggaran(iddinas);
                }



                m_listRealisasiPendapatanTrx = new List<Realisasi04AK>();
                m_listRealisasiBelanjaTrx = new List<Realisasi04AK>();
                m_listRealisasiPenerimaanPembiayaanTrx = new List<Realisasi04AK>();
                m_listRealisasiPengeluaranPembiayaanTrx = new List<Realisasi04AK>();

                m_listRealisasiPendapatanBB = new List<Realisasi04AK>();
                m_listRealisasiBelanjaBB = new List<Realisasi04AK>();
                m_listRealisasiPenerimaanPembiayaanBB = new List<Realisasi04AK>();
                m_listRealisasiPengeluaranPembiayaanBB = new List<Realisasi04AK>();
                mTanggalAwal = ctrlTanggalBulan1.TanggalAwal;
                mTanggalAkhir = ctrlTanggalBulan1.TanggalAkhir;

                RealisasiAKLogic oLogic = new RealisasiAKLogic(GlobalVar.TahunAnggaran);
                if (chkSemuaDinas.Checked == true)
                {
                    m_iSKPD = 0;
                }
                else
                {
                    m_iSKPD = ctrlDinas1.GetID();
                    if (m_iSKPD == 0)
                    {
                        MessageBox.Show("Belum pilih dinas.");
                        return;

                    }
                }
                List<Realisasi04AK> lst = new List<Realisasi04AK>();
                lst = oLogic.GetRealisasi(m_iSKPD, mTanggalAkhir, chkBelanjaModal.Checked);

                m_listRealisasiTrx = oLogic.GetRealisasi(m_iSKPD, mTanggalAkhir, chkBelanjaModal.Checked);

                if (chkSemuaJenis.Checked == false)
                {
                    if (chkGaji.Checked == true)
                    {
                        m_listRealisasiTrx = lst.FindAll(x => x.Tabel == 1 && x.JenisSP2D == 4);
                    }
                    if (chkLS.Checked)
                    {
                        m_listRealisasiTrx = lst.FindAll(x => x.Tabel == 1 && x.JenisSP2D == 3);
                    }
                    if (chkUPGU.Checked == true)
                    {
                        m_listRealisasiTrx = lst.FindAll(x => x.Tabel == 2);
                    }
                    if (chkPengembalian.Checked == true)
                    {
                        m_listRealisasiTrx = lst.FindAll(x => x.Tabel == 3);
                    }
                    if (chkKoreksi.Checked == true)
                    {
                        m_listRealisasiTrx = lst.FindAll(x => x.Tabel == 4);
                    }

                }
                else
                {
                    m_listRealisasiTrx = lst;
                }


                if (m_listRealisasiTrx == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return;
                }
                m_listRealisasiPendapatanTrx = m_listRealisasiTrx.FindAll(x => x.idRekening < 500000000000);
                m_listRealisasiBelanjaTrx = m_listRealisasiTrx.FindAll(x => x.idRekening > 500000000000 &&
                                                                            x.idRekening < 600000000000);
                m_listRealisasiPenerimaanPembiayaanTrx = m_listRealisasiTrx.FindAll(x => x.idRekening > 610000000000 &&
                                                                            x.idRekening < 620000000000);
                m_listRealisasiPengeluaranPembiayaanTrx = m_listRealisasiTrx.FindAll(x => x.idRekening > 620000000000);


                //  = oLogic.GetRealisasiBukubesar(m_iSKPD, mTanggalAkhir);
                JumlahPendapatan = 0;
                JumlahBelanja = 0;
                JumlahPenerimaan = 0;
                JumlahPengeluaran = 0;
                int iLevelRekening = GetLevelRekening();
                if (m_listRealisasiTrx != null)
                {

                    if (m_listRealisasiPendapatanTrx != null)
                        JumlahPendapatan = m_listRealisasiPendapatanTrx.Sum(s => s.Jumlah * s.Debet);

                    if (m_listRealisasiBelanjaTrx != null)
                        JumlahBelanja = m_listRealisasiBelanjaTrx.Sum(s => s.Jumlah * s.Debet);

                    if (m_listRealisasiPenerimaanPembiayaanTrx != null)
                        JumlahPenerimaan = m_listRealisasiPenerimaanPembiayaanTrx.Sum(s => s.Jumlah * s.Debet);
                    if (m_listRealisasiPengeluaranPembiayaanTrx != null)
                        JumlahPengeluaran = m_listRealisasiPengeluaranPembiayaanTrx.Sum(s => s.Jumlah * s.Debet);

                    for (int level = 0; level <= iLevelRekening; level++)
                    {
                        ProsesLRALevel1(m_listRealisasiPendapatanTrx, level);
                    }
                    for (int level = 0; level <= iLevelRekening; level++)
                    {
                        ProsesLRALevel1(m_listRealisasiBelanjaTrx, level);
                    }



                    ////// JP, JB//SD//TB KB
                    //// gridLRA.Rows[row].Cells[col].Value = r.Realisasi.ToRupiahInReport();
                    for (int row = 0; row < gridLRA.Rows.Count; row++)
                    {
                        string kode = DataFormat.GetString(gridLRA.Rows[row].Cells[0].Value);
                        if (kode == "4")
                        {
                            gridLRA.Rows[row].Cells[3].Value = JumlahPendapatan.ToRupiahInReport();
                        }
                        if (kode == "5")
                        {
                            gridLRA.Rows[row].Cells[3].Value = JumlahBelanja.ToRupiahInReport();
                        }

                        if (kode == "SD")
                        {
                            gridLRA.Rows[row].Cells[3].Value = (JumlahPendapatan - JumlahBelanja).ToRupiahInReport();
                        }

                        if (kode == "6.1")
                        {
                            gridLRA.Rows[row].Cells[3].Value = (JumlahPenerimaan).ToRupiahInReport();
                        }
                        if (kode == "6.2")
                        {
                            gridLRA.Rows[row].Cells[3].Value = (JumlahPengeluaran).ToRupiahInReport();
                        }
                        if (kode == "PN")
                        {
                            gridLRA.Rows[row].Cells[3].Value = (JumlahPenerimaan - JumlahPengeluaran).ToRupiahInReport();
                        }
                        if (kode == "SILPA")
                        {
                            gridLRA.Rows[row].Cells[3].Value = ((JumlahPendapatan - JumlahBelanja) + (JumlahPenerimaan - JumlahPengeluaran)).ToRupiahInReport();
                        }



                    }
                    RefreshSelisih(1);
                    cmdCetak.Visible = true;
                    ctrlPencarian1.Visible = true;
                    cmdPanggilLRABukuBesar.Visible = true;
                    cmdExcell.Visible = true;
                    txtSpasi.Visible = true;
                    label12.Visible = true;
                    label12.Visible = true;

                    for (int i = 0; i < gridLRA.Rows.Count-1; i++)
                    {
                        for (int c = 0; c < gridLRA.Columns.Count; c++)
                        {
                            if (DataFormat.GetString(gridLRA.Rows[i].Cells[c].Value) == "" &&
                                DataFormat.GetString(gridLRA.Rows[i].Cells[1].Value) != "")
                            {
                                gridLRA.Rows[i].Cells[c].Value = "0";
                            }
                        }
                    }
                }





                else
                {
                    MessageBox.Show(oLogic.LastError());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void RefreshSelisih( int sumber)
        {
            try
            {
                decimal anggaran = 0;
                decimal realisasi = 0;
                decimal selisih = 0;

                int colRelisasi = COL_REALISASITRX;
                int colSelisih = COL_SELISIHTRX;
                int colProsentase = COL_PROSENTASETRX;

                if (sumber == 2)
                {
                    colRelisasi = COL_REALISASIBB;
                    colSelisih = COL_SELISIHBB;
                    colProsentase = COL_PROSENTASEBB;
                }

                
                string kode = "";
                foreach (DataGridViewRow row in gridLRA.Rows)
                {
                    anggaran = 0;
                    realisasi = 0;
                    selisih = 0;
                    //row.Cells[5].Value = "0.00";

                    if (row.Cells[0].Value != null)
                    {
                        anggaran = DataFormat.FormatUangReportKeDecimal(row.Cells[2].Value);
                        realisasi = DataFormat.FormatUangReportKeDecimal(row.Cells[colRelisasi].Value);
                        kode = DataFormat.GetString(row.Cells[1].Value);
                        if (kode.Substring(0, 1) == "4")
                        {
                            selisih = realisasi - anggaran;
                        }
                        else
                        {
                            selisih = anggaran - realisasi;
                        }
                        row.Cells[colSelisih].Value = selisih.ToRupiahInReport();
                        
                        if (anggaran > 0)
                        {
                            row.Cells[colProsentase].Value = (100 * (realisasi / anggaran)).ToString("##.##");
                        }
                        if (realisasi == 0)
                        {
                            row.Cells[colProsentase].Value = "00.00";
                        }

                    }

                }

            }
            catch (Exception ex)
            {

            }
        }
        private bool ProsesLRALevel1(List<Realisasi04AK> lstLRA, int level, int col = 3)
        {
            string idRekening = "";
            try
            {
                int lenKode = 12;
                switch (level)
                {
                    case 1:
                        lenKode = 1;
                        break;
                    case 2:
                        lenKode = 2;
                        break;
                    case 3:
                        lenKode = 4;
                        break;
                    case 4:
                        lenKode = 6;
                        break;
                    case 5:
                        lenKode = 8;
                        break;
                    case 6:
                        lenKode = 12;
                        break;
                }
                // var lstJumlah = m_lstNeracaAwal.GroupBy(x => x.IDRekening.ToString().Substring(0, lenKode));

                var lstJumlah = lstLRA.Where(l => l.idRekening.ToString().Length == 12).GroupBy(x => x.idRekening.ToString().Substring(0, lenKode))
                  .Select(x => new
                  {
                      IDRekening = x.Key,
                      Level = level,
                      Realisasi = x.Sum(y => y.Debet * y.Jumlah),


                  }).ToList();




                foreach (var r in lstJumlah)
                {
                    for (int row = 0; row < gridLRA.Rows.Count; row++)
                    {
                        if (gridLRA.Rows[row].Cells[0].Value != null)
                        {
                            if (DataFormat.GetString(gridLRA.Rows[row].Cells[0].Value).Replace(".", "") == r.IDRekening)
                            {
                                gridLRA.Rows[row].Cells[col].Value = r.Realisasi.ToRupiahInReport();
                              

                                /*
                                decimal anggaran = DataFormat.FormatUangReportKeDecimal(gridLRA.Rows[row].Cells[2].Value);

                                if (r.IDRekening.Trim().Length == 1)
                                {
                                    if (r.IDRekening.ToString().Substring(0, 1) == "5")
                                    {
                                        gridLRA.Rows[row].Cells[col + 1].Value = (anggaran - r.Realisasi).ToRupiahInReport();
                                    }
                                    else
                                    {
                                        gridLRA.Rows[row].Cells[col + 1].Value = (r.Realisasi - anggaran).ToRupiahInReport();
                                    }
                                }
                                if (r.IDRekening.Trim().Length > 1)
                                {
                                    if (r.IDRekening.ToString().Substring(0, 1) == "5" || r.IDRekening.ToString().Substring(0, 2) == "62")
                                    {
                                        gridLRA.Rows[row].Cells[col + 1].Value = (anggaran - r.Realisasi).ToRupiahInReport();
                                    }
                                    else
                                    {
                                        gridLRA.Rows[row].Cells[col + 1].Value = (r.Realisasi - anggaran).ToRupiahInReport();
                                    }
                                }



                                if (anggaran > 0 && col == COL_REALISASITRX)
                                {
                                    gridLRA.Rows[row].Cells[col + 2].Value = (r.Realisasi / anggaran).ToString("#,##");
                                }
                                if (col == COL_REALISASIBB)
                                {
                                    if (DataFormat.FormatUangReportKeDecimal(gridLRA.Rows[row].Cells[COL_REALISASITRX].Value) != DataFormat.FormatUangReportKeDecimal(gridLRA.Rows[row].Cells[COL_REALISASIBB].Value))
                                    {

                                        gridLRA.Rows[row].DefaultCellStyle.BackColor = Color.LightBlue;
                                        //gridLRA.Rows[row].DefaultCellStyle.BackColor = Color.LightBlue;

                                    }
                                }*/
                            }


                        }

                    }

                    for (int row = 0; row < gridLRA.Rows.Count; row++)
                    {
                        decimal anggaran = DataFormat.FormatUangReportKeDecimal(gridLRA.Rows[row].Cells[2].Value);
                        decimal realisasi = DataFormat.FormatUangReportKeDecimal(gridLRA.Rows[row].Cells[3].Value);


                        //gridLRA.Rows[row].Cells[4].Value = anggaran.ToRupiahInReport();
                        if (anggaran > 0)
                        {
                            gridLRA.Rows[row].Cells[5].Value = (realisasi / anggaran).ToString("#,##");
                        }

                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Rek " + idRekening);
                return false;
            }

        }

        private void chkSemuaDinas_CheckedChanged(object sender, EventArgs e)
        {

            ctrlDinas1.Enabled = !chkSemuaDinas.Checked;
        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            Cetak(1);
        }
        private void Cetak(int sumber) { 
            try
            {

                int colRelisasi = COL_REALISASITRX;
                int colSelisih = COL_SELISIHTRX;
                int colProsentase = COL_PROSENTASETRX;

                if (sumber == 2)
                {
                    colRelisasi = COL_REALISASIBB;
                    colSelisih = COL_SELISIHBB;
                    colProsentase = COL_PROSENTASEBB;
                }
                PdfDocument document = new PdfDocument();
                PdfSection section = document.Sections.Add();
                document.PageSettings.Margins.Left = 8;
                document.PageSettings.Margins.Top = 5;
                document.PageSettings.Margins.Right = 2;
                document.PageSettings.Margins.Bottom = 8;
                section.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section.PageSettings.Height = 935;// = PdfPageSize.Legal;
                section.PageSettings.Orientation = PdfPageOrientation.Portrait;

                float yPos = 0;
                SaatnyacetakKesimpulan = false;
                PdfPage page = section.Pages.Add();

                PdfGraphics graphics = page.Graphics;
                previousPage = page;
                document.Pages.PageAdded += Pages_PageAdded;

                yPos = 10;
                PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);


                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;

                //stringFormat.CharacterSpacing = 2f;
                oCetakPDF = new CetakPDF();
                //SizeF size = font12.MeasureString("xxx");


                Pejabat pimpinan = new Pejabat();
                Pejabat bendahara = new Pejabat();
                //pimpinan = ctrlDinas1.GetPimpinan(mTanggalAkhir);
                //bendahara = ctrlDinas1.GetBendaharaPengeluaran(mTanggalAkhir);

                //if (pimpinan == null)
                //{
                //    MessageBox.Show("Data Pimpinan belum di setting di master pejabat");
                //    return ;

                //}

                //if (bendahara == null)
                //{
                //    MessageBox.Show("Data Bndahara belum di setting di master pejabat");
                //    return ;

                //}


                float kiri = 15;
                float posTitikdua = 150;
                float posNama = 155;

                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "LAPORAN REALISASI PENDAPATAN DAN BELANJA", 10, kiri, yPos,
                page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "UNTUK TAHUN YANG BERAKHIR SAMPAI DENGAN " + ctrlTanggalBulan1.TanggalAkhir.ToTanggalIndonesia(), 10, kiri, yPos,
                page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = yPos + 20;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "LAPORAN REALISASI ANGGARAN ", 12, kiri, yPos,
                page.GetClientSize().Width, stringFormat, true, false, true);



                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = yPos + 20;
                if (m_iSKPD > 0)
                {
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD "
                              , 10, kiri, yPos,
                              page.GetClientSize().Width, stringFormat, false, false, true);
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                              page.GetClientSize().Width, stringFormat, false, false, true);
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                              ctrlDinas1.GetNamaSKPD(), 10, 155, yPos,
                              page.GetClientSize().Width, stringFormat, true, false, true);
                }


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
                #region gridKas
                PdfGrid pdfGrid = new PdfGrid();

                int count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("Kode Rekening");
                table.Columns.Add("Uraian");
                table.Columns.Add("Anggaran");
                table.Columns.Add("Realisasi");
                table.Columns.Add("Lebih/(Kurang)");
                table.Columns.Add("%");


                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();


                decimal akumulasi = 0L;
                decimal sisa = 0;
                string kode = "";

                for (int idx = 0; idx < gridLRA.Rows.Count; idx++)
                {
                    kode = DataFormat.GetString(gridLRA.Rows[idx].Cells[0].Value);
                    if (kode == "JP" || kode == "JB" || kode == "SD" || kode == "TB" || kode == "KB" || kode == "SILPA" || kode == "PN")
                    {
                        kode = "";
                    }
                    table.Rows.Add(new string[]
                    {
                        kode ,
                       DataFormat.GetString(gridLRA.Rows[idx].Cells[1].Value),
                       DataFormat.GetString(gridLRA.Rows[idx].Cells[2].Value),
                    
                       DataFormat.GetString(gridLRA.Rows[idx].Cells[colRelisasi].Value),
                       DataFormat.GetString(gridLRA.Rows[idx].Cells[colSelisih].Value),
                       DataFormat.GetString(gridLRA.Rows[idx].Cells[colProsentase].Value),

                    });
                }

                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 80;
                pdfGrid.Columns[1].Width = 160;
                pdfGrid.Columns[2].Width = 80;
                pdfGrid.Columns[3].Width = 80;
                pdfGrid.Columns[4].Width = 74;
                pdfGrid.Columns[5].Width = 30;

                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                //gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                PdfStringFormat formatKolomTengah = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGrid.Columns[2].Format = formatKolomAngka;
                pdfGrid.Columns[3].Format = formatKolomAngka;
                pdfGrid.Columns[4].Format = formatKolomAngka;

                formatKolomTengah.Alignment = PdfTextAlignment.Center;
                pdfGrid.Columns[5].Format = formatKolomTengah;










                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
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
                    pdfGrid.Headers[c].Height = 30;

                }


                for (int idx = 0; idx < pdfGrid.Rows.Count; idx++)
                {
                    pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 7,
                        FontStyle.Regular)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);

                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        if (c == 4)
                        {
                            pdfGrid.Rows[idx].Cells[c].Style.CellPadding = new PdfPaddings(1, 1, 1, 0);
                        }
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.05F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.05F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.05F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.05F;

                    }

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos));
                #endregion gridKas
                //  pdfGridLayoutResult = pdfGridRingkasan.Draw(pdfGridLayoutResult.Page, new PointF(kiri, pdfGridLayoutResult.Bounds.Bottom));

                yPos = pdfGridLayoutResult.Bounds.Bottom + 10;

                PejabatLogic oLogicPejabat = new PejabatLogic(GlobalVar.TahunAnggaran);

                Pejabat oKada = new Pejabat();

                DateTime d = DateTime.Now;
                if (chkSemuaDinas.Checked == true)
                {
                    oKada = oLogicPejabat.GetKepalaDaerah();
                }
                else
                {
                    oKada = ctrlDinas1.GetPimpinan(d);
                }

                float setengah = page.GetClientSize().Width / 2;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                           "Ketapang, " + tanggalTandaTangan1.Tanggal, 9, kiri + setengah, yPos,
                           setengah, stringFormat, true, false, true);
               
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          oKada.Jabatan, 9, kiri + setengah, yPos,
                          setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                         oKada.Nama, 9, kiri + setengah, yPos,
                         setengah, stringFormat, true, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                         oKada.NIP,  9, kiri + setengah, yPos,
                         setengah, stringFormat, true, false, true);





                //PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                //SaatnyacetakKesimpulan = true;
                //page = document.Pages.Add();

                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../LRA.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../LRA.pdf");
                pV.Show();


                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
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
                if (m_iJenisBendahara == 2)
                {
                    bendahara = ctrlDinas1.GetBendaharaPengeluaran(ctrlTanggalBulan1.TanggalAkhir);
                }
                else
                {
                    bendahara = ctrlDinas1.GetBendaharaPenerimaan(ctrlTanggalBulan1.TanggalAkhir);
                }
                pimpinan = ctrlDinas1.GetPimpinan(ctrlTanggalBulan1.TanggalAkhir);
                yPos = yPos + 10;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.gPemda.Ibukota + "," + ctrlTanggalBulan1.TanggalAkhir.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Jabatan, 10, 30, yPos, setengah, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Jabatan, 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Nama, 10, 30, yPos, setengah, stringFormat, false, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + pimpinan.NIP, 10, 30, yPos, setengah, stringFormat, false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + bendahara.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);




            }






            previousPage = args.Page;
        }

        private void cmdExcell_Click(object sender, EventArgs e)
        {
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
                // excel.DisplayAlerts = false;

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                if (chkSemuaDinas.Checked == true)
                {
                    excelSheet.Name = "LRA" + ctrlDinas1.GetNamaSKPD();
                }
                else
                {
                    excelSheet.Name = "LRA";
                }

                // header
                excelSheet.Cells[1, 2] = "Laporan Realisasi Anggaran";
                excelSheet.Cells[2, 1] = "SKPD";
                if (chkSemuaDinas.Checked == true)
                {
                    excelSheet.Cells[2, 3] = "Semua Dinas";
                }
                else
                {
                    excelSheet.Cells[2, 4] = ctrlDinas1.GetNamaSKPD();
                }
                excelSheet.Cells[3, 1] = "Tanggal ";
                excelSheet.Cells[3, 2] = "";




                for (int i = 1; i < 7; i++)
                {
                    excelSheet.Cells[4, i] = gridLRA.Columns[i - 1].HeaderText;
                }
                //gridLRA.Columns.Count + 1
                for (int row = 0; row < gridLRA.Rows.Count; row++)
                {
                    for (int col = 0; col < 7; col++)
                    {
                        if (col > 1)
                        {


                            excelSheet.Cells[row + 5, col + 1] = DataFormat.FormatUangReportKeDecimal(gridLRA.Rows[row].Cells[col].Value);


                        }
                        else
                        {
                            excelSheet.Cells[row + 5, col + 1] = DataFormat.GetString(gridLRA.Rows[row].Cells[col].Value).ReplaceUnicode();
                        }


                    }
                }


                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];
                //excelCellrange.EntireColumn.AutoFit();
                //excelSheet.Range (“G:G”).NumberFormat = “0.00”;
                //Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                //border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //border.Weight = 2d;
                string namaFile = BuatFile();
                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Masih Kosong ");
                    return;
                }

                excelworkBook.SaveAs(namaFile);
                MessageBox.Show("File sudah disimpan di " + namaFile);


                excelworkBook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke excell" + ex.Message);
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




            //if (fdlg.FileName != "")
            //{
            // Saves the Image via a FileStream created by the OpenFile method.  

            sRet = fdlg.FileName;


            //  }
            return sRet;
        }

        private void contextMenuLRA_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Copy" && gridLRA.CurrentCell.Value != null)
            {
                Clipboard.SetDataObject(gridLRA.CurrentCell.Value.ToString(), false);
            }
        }

        private void cmdPanggilLRABukuBesar_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < gridLRA.Rows.Count; i++)
                {

                    gridLRA.Rows[i].DefaultCellStyle.BackColor = Color.White;

                }


                SembunyikanTampilkanKolomNukuBesar(true);



                m_listRealisasiPendapatanBB = new List<Realisasi04AK>();
                m_listRealisasiBelanjaBB = new List<Realisasi04AK>();
                m_listRealisasiPenerimaanPembiayaanBB = new List<Realisasi04AK>();
                m_listRealisasiPengeluaranPembiayaanBB = new List<Realisasi04AK>();
                mTanggalAwal = ctrlTanggalBulan1.TanggalAwal;
                mTanggalAkhir = ctrlTanggalBulan1.TanggalAkhir;

                RealisasiAKLogic oLogic = new RealisasiAKLogic(GlobalVar.TahunAnggaran);
                if (chkSemuaDinas.Checked == true)
                {
                    m_iSKPD = 0;
                }
                else
                {
                    m_iSKPD = ctrlDinas1.GetID();
                    if (m_iSKPD == 0)
                    {
                        MessageBox.Show("Belum pilih dinas.");
                        return;

                    }
                }
                List<Realisasi04AK> lst = new List<Realisasi04AK>();
                lst = oLogic.GetRealisasiBukubesar(m_iSKPD, mTanggalAkhir);

                //m_listRealisasiBukuBesar = oLogic.GetRealisasiBukubesar(m_iSKPD, mTanggalAkhir);

                m_listRealisasiBukuBesar = lst;



                if (m_listRealisasiBukuBesar == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return;
                }
                m_listRealisasiPendapatanBB = m_listRealisasiBukuBesar.FindAll(x => x.idRekening < 500000000000);
                m_listRealisasiBelanjaBB = m_listRealisasiBukuBesar.FindAll(x => x.idRekening > 500000000000 &&
                                                                            x.idRekening < 600000000000);
                m_listRealisasiPenerimaanPembiayaanBB = m_listRealisasiBukuBesar.FindAll(x => x.idRekening > 610000000000 &&
                                                                            x.idRekening < 620000000000);
                m_listRealisasiPengeluaranPembiayaanBB = m_listRealisasiBukuBesar.FindAll(x => x.idRekening > 620000000000);


                //  = oLogic.GetRealisasiBukubesar(m_iSKPD, mTanggalAkhir);
                JumlahPendapatan = 0;
                JumlahBelanja = 0;
                JumlahPenerimaan = 0;
                JumlahPengeluaran = 0;

                int iLevelRekening = GetLevelRekening();
                if (m_listRealisasiBukuBesar != null)
                {

                    if (m_listRealisasiPendapatanBB != null)
                        JumlahPendapatan = m_listRealisasiPendapatanBB.Sum(s => s.Jumlah * s.Debet);

                    if (m_listRealisasiBelanjaTrx != null)
                        JumlahBelanja = m_listRealisasiBelanjaBB.Sum(s => s.Jumlah * s.Debet);

                    if (m_listRealisasiPenerimaanPembiayaanBB != null)
                        JumlahPenerimaan = m_listRealisasiPenerimaanPembiayaanBB.Sum(s => s.Jumlah * s.Debet);
                    if (m_listRealisasiPengeluaranPembiayaanBB != null)
                        JumlahPengeluaran = m_listRealisasiPengeluaranPembiayaanTrx.Sum(s => s.Jumlah * s.Debet);

                    for (int level = 0; level <= iLevelRekening; level++)
                    {
                        ProsesLRALevel1(m_listRealisasiPendapatanBB, level, COL_REALISASIBB);
                    }
                    for (int level = 0; level <= iLevelRekening; level++)
                    {
                        ProsesLRALevel1(m_listRealisasiBelanjaBB, level, COL_REALISASIBB);
                    }

                    for (int level = 0; level <= iLevelRekening; level++)
                    {
                        ProsesLRALevel1(m_listRealisasiPenerimaanPembiayaanBB, level, COL_REALISASIBB);
                    }

                    for (int level = 0; level <= iLevelRekening; level++)
                    {
                        ProsesLRALevel1(m_listRealisasiPengeluaranPembiayaanBB, level, COL_REALISASIBB);
                    }

                    ////// JP, JB//SD//TB KB
                    //// gridLRA.Rows[row].Cells[col].Value = r.Realisasi.ToRupiahInReport();
                    for (int row = 0; row < gridLRA.Rows.Count; row++)
                    {
                        string kode = DataFormat.GetString(gridLRA.Rows[row].Cells[0].Value);
                        if (kode == "4")
                        {
                            gridLRA.Rows[row].Cells[COL_REALISASIBB].Value = JumlahPendapatan.ToRupiahInReport();
                        }
                        if (kode == "5")
                        {
                            gridLRA.Rows[row].Cells[COL_REALISASIBB].Value = JumlahBelanja.ToRupiahInReport();
                        }

                        if (kode == "SD")
                        {
                            gridLRA.Rows[row].Cells[COL_REALISASIBB].Value = (JumlahPendapatan - JumlahBelanja).ToRupiahInReport();
                        }

                        if (kode == "6.1")
                        {
                            gridLRA.Rows[row].Cells[COL_REALISASIBB].Value = (JumlahPenerimaan).ToRupiahInReport();
                        }
                        if (kode == "6.2")
                        {
                            gridLRA.Rows[row].Cells[COL_REALISASIBB].Value = (JumlahPengeluaran).ToRupiahInReport();
                        }
                        if (kode == "PN")
                        {
                            gridLRA.Rows[row].Cells[COL_REALISASIBB].Value = (JumlahPenerimaan - JumlahPengeluaran).ToRupiahInReport();
                        }
                        if (kode == "SILPA")
                        {
                            gridLRA.Rows[row].Cells[COL_REALISASIBB].Value = ((JumlahPendapatan - JumlahBelanja) + (JumlahPenerimaan - JumlahPengeluaran)).ToRupiahInReport();
                        }

                        gridLRA.Rows[row].Cells[COL_REALISASIBB + 2].Value = "Cek";

                    }
                    RefreshSelisih(2);

                }

                for (int i = 0; i < gridLRA.Rows.Count; i++)
                {
                    if (DataFormat.GetString(gridLRA.Rows[i].Cells[0].Value).Trim().Length > 14)
                    {
                        gridLRA.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                    }

                }
          //      '1,11,11,11.1111'
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void gridLRA_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < gridLRA.RowCount)
                {

                    if (e.ColumnIndex == 9)
                    {
                        frmSelisihTrxBB fSelisih = new frmSelisihTrxBB();
                        fSelisih.Dinas = m_iSKPD;
                        fSelisih.TanggalBatas = mTanggalAkhir;
                        fSelisih.IDRekening = DataFormat.GetLong(DataFormat.GetString(gridLRA.Rows[e.RowIndex].Cells[0].Value).Replace(".", ""));
                        fSelisih.NamaRekening = DataFormat.GetString(gridLRA.Rows[e.RowIndex].Cells[1].Value);
                        fSelisih.Show();


                    }
                    if (e.ColumnIndex == 10)
                    {
                        frmBukuBesarDlg fBukubesarDlg = new frmBukuBesarDlg();
                        fBukubesarDlg.SKPD = m_iSKPD;
                        fBukubesarDlg.Tanggal = mTanggalAkhir;
                        fBukubesarDlg.IDRekening = DataFormat.GetLong(DataFormat.GetString(gridLRA.Rows[e.RowIndex].Cells[0].Value).Replace(".", ""));
                        fBukubesarDlg.NamaRekening = DataFormat.GetString(gridLRA.Rows[e.RowIndex].Cells[1].Value);
                        fBukubesarDlg.LoadData();
                        fBukubesarDlg.Excell = chkExcelkanBB.Checked;

                        if (fBukubesarDlg.IsDisposed == false)
                        {
                            fBukubesarDlg.Show();
                        }



                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ctrlTahapAnggaran1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }

        private void cmdCetakDariBukubesar_Click(object sender, EventArgs e)
        {
            Cetak(2);
        }

        private void cmdExcellBukuBesar_Click(object sender, EventArgs e)
        {
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
                // excel.DisplayAlerts = false;

                

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                if (chkSemuaDinas.Checked == true)
                {
                    excelSheet.Name = "LRA" + ctrlDinas1.GetNamaSKPD();
                }
                else
                {
                    excelSheet.Name = "LRA";
                }

                // header
                excelSheet.Cells[1, 2] = "Laporan Realisasi Anggaran Berdasar Buku Besar";
                excelSheet.Cells[2, 1] = "SKPD";
                if (chkSemuaDinas.Checked == true)
                {
                    excelSheet.Cells[2, 3] = "Semua Dinas";
                }
                else
                {
                    excelSheet.Cells[2, 4] = ctrlDinas1.GetNamaSKPD();
                }
                excelSheet.Cells[3, 1] = "Tanggal ";
                excelSheet.Cells[3, 2] = "";


                int[] columns = { 1, 2, 3, 8 };
                int columnExcell = 1;
                foreach (int i in columns)
                {    
                    
                    excelSheet.Cells[4, columnExcell] = gridLRA.Columns[i - 1].HeaderText;
                    columnExcell++;
                }
                //gridLRA.Columns.Count + 1
                for (int row = 0; row < gridLRA.Rows.Count; row++)
                {
                    //for (int col = 0; col < 7; col++)
                    columnExcell = 1;
                    foreach (int col in columns)
                     {

                        if (col > 2)
                        {


                            excelSheet.Cells[row + 5, columnExcell] = DataFormat.FormatUangReportKeDecimal(gridLRA.Rows[row].Cells[col-1].Value);


                        }
                        else
                        {
                            excelSheet.Cells[row + 5, columnExcell] = DataFormat.GetString(gridLRA.Rows[row].Cells[col-1].Value).ReplaceUnicode();
                        }


                        columnExcell++;
                    }

                }

                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];
                string namaFile = BuatFile();
                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Masih Kosong ");
                    return;
                }

                excelworkBook.SaveAs(namaFile);
                MessageBox.Show("File sudah disimpan di " + namaFile);


                excelworkBook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke excell" + ex.Message);
            }
        }

        private void txtSpasi_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            try
            {

            }catch(Exception ex)
            {


            }


        }
    }
}

/*
RealisasiAKLogic oLogic = new RealisasiAKLogic(GlobalVar.TahunAnggaran);
                List<Realisasi04AK> lst = new List<Realisasi04AK>();
                if (lst == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return;
                }
                lst = oLogic.GetRealisasi(m_iSKPD, mTanggalAkhir);

                if (oLogic.IsError())
                {
                    MessageBox.Show("Kesalahan dalam memanggil data Realisasi Bukubesar..");
                    return;
                }
                if (chkSemuaJenis.Checked == false)
                {
                    if (chkGaji.Checked == true)
                    {
                        m_listRealisasiBukuBesar = lst.FindAll(x => x.Tabel == 1 && x.JenisSP2D == 4);
                    }
                    if (chkLS.Checked)
                    {
                        m_listRealisasiBukuBesar = lst.FindAll(x => x.Tabel == 1 && x.JenisSP2D == 3);
                    }
                    if (chkUPGU.Checked == true)
                    {
                        m_listRealisasiBukuBesar = lst.FindAll(x => x.Tabel == 2);
                    }
                    if (chkPengembalian.Checked == true)
                    {
                        m_listRealisasiBukuBesar = lst.FindAll(x => x.Tabel == 3);
                    }
                    if (chkKoreksi.Checked == true)
                    {
                        m_listRealisasiBukuBesar = lst.FindAll(x => x.Tabel == 4);
                    }

                }
                else
                {
                    m_listRealisasiBukuBesar = lst;
                }


                m_listRealisasiPendapatanBB = m_listRealisasiBukuBesar.FindAll(x => x.idRekening < 500000000000);
                m_listRealisasiBelanjaBB = m_listRealisasiBukuBesar.FindAll(x => x.idRekening > 500000000000 &&
                                                                            x.idRekening < 600000000000);
                m_listRealisasiPenerimaanPembiayaanBB = m_listRealisasiBukuBesar.FindAll(x => x.idRekening > 610000000000 &&
                                                                            x.idRekening < 620000000000);
                m_listRealisasiPengeluaranPembiayaanBB = m_listRealisasiBukuBesar.FindAll(x => x.idRekening > 620000000000);
                //  = oLogic.GetRealisasiBukubesar(m_iSKPD, mTanggalAkhir);
                JumlahPendapatan = 0;
                JumlahBelanja = 0;
                JumlahPenerimaan = 0;
                JumlahPengeluaran = 0;
                int iLevelRekening = GetLevelRekening();
                if (m_listRealisasiBukuBesar != null)
                {

                    if (m_listRealisasiPendapatanBB != null)
                        JumlahPendapatan = m_listRealisasiPendapatanBB.Sum(s => s.Jumlah * s.Debet);

                    if (m_listRealisasiBelanjaBB != null)
                        JumlahBelanja = m_listRealisasiBelanjaBB.Sum(s => s.Jumlah * s.Debet);

                    if (m_listRealisasiPenerimaanPembiayaanBB != null)
                        JumlahPenerimaan = m_listRealisasiPenerimaanPembiayaanBB.Sum(s => s.Jumlah * s.Debet);
                    if (m_listRealisasiPengeluaranPembiayaanBB != null)
                        JumlahPengeluaran = m_listRealisasiPengeluaranPembiayaanBB.Sum(s => s.Jumlah * s.Debet);

                    for (int level = 0; level <= iLevelRekening; level++)
                    {
                        ProsesLRALevel1(m_listRealisasiPendapatanBB, level);
                    }




                    ////// JP, JB//SD//TB KB
                    //// gridLRA.Rows[row].Cells[col].Value = r.Realisasi.ToRupiahInReport();
                    for (int row = 0; row < gridLRA.Rows.Count; row++)
                    {
                        string kode = DataFormat.GetString(gridLRA.Rows[row].Cells[0].Value);
                        if (kode == "4")
                        {
                            gridLRA.Rows[row].Cells[3].Value = JumlahPendapatan.ToRupiahInReport();
                        }
                        if (kode == "5")
                        {
                            gridLRA.Rows[row].Cells[3].Value = JumlahBelanja.ToRupiahInReport();
                        }

                        if (kode == "SD")
                        {
                            gridLRA.Rows[row].Cells[3].Value = (JumlahPendapatan - JumlahBelanja).ToRupiahInReport();
                        }

                        if (kode == "6.1")
                        {
                            gridLRA.Rows[row].Cells[3].Value = (JumlahPenerimaan).ToRupiahInReport();
                        }
                        if (kode == "6.2")
                        {
                            gridLRA.Rows[row].Cells[3].Value = (JumlahPengeluaran).ToRupiahInReport();
                        }
                        if (kode == "PN")
                        {
                            gridLRA.Rows[row].Cells[3].Value = (JumlahPenerimaan - JumlahPengeluaran).ToRupiahInReport();
                        }
                        if (kode == "SILPA")
                        {
                            gridLRA.Rows[row].Cells[3].Value = ((JumlahPendapatan - JumlahBelanja) + (JumlahPenerimaan - JumlahPengeluaran)).ToRupiahInReport();
                        }



                    }
                    RefreshSelisih();

*/