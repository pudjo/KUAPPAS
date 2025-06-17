using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DTO;
using DTO.Bendahara;

using BP;
using BP.Bendahara;
using Formatting;
using KUAPPAS.Bendahara;


namespace KUAPPAS
{
    public partial class ctrlRekeningKegiatan : UserControl
    {

        public delegate void ValueChangedEventHandler(decimal pJumlah);
        public event ValueChangedEventHandler OnChanged;

        private int m_idDinas;
        private int m_IDUrusan;
        private int m_IDProgram;
        private int m_IDKegiatan;
        private int m_iJenis;
        private int m_bPPKD;
        private long m_IDSubKegiatan;
        private decimal m_dJumah;
        private int m_KodeUK;
        private int m_iTahap;
        private int m_UnitKerja;
        private int m_iKeperluan;// 1 SPP
                                // dua panjar
                                // 3 Koreksi


        public ctrlRekeningKegiatan()
        {
            InitializeComponent();
            m_dJumah = 0L;
            m_KodeUK = 0;
        }

        public bool SetProgramKegiatan(int idDinas, int KodeUK,int idUrusan, int idProgram, int idKegiatan, int pJenis, int bPPKD, long idSubKegiatan = 0)
        {

            m_idDinas = idDinas;
            m_IDUrusan = idUrusan;
            m_KodeUK = KodeUK;
            m_IDProgram = idProgram;
            m_IDKegiatan = idKegiatan;
            m_iJenis = pJenis;
            m_bPPKD = bPPKD;
            m_IDSubKegiatan = idSubKegiatan;

            return true;


        }
        // mengisi pada kolom belanja dengan nilaikontrak
        public bool SetKontrak(Kontrak oKontrak)
        {

            foreach (KontrakRekening kr in oKontrak.Rekening)
            {

                for (int irow = 0; irow < gridRekening.Rows.Count; irow++)
                {
                    if (gridRekening.Rows[irow].Cells[0].Value != null)
                    {
                        if (DataFormat.GetLong(gridRekening.Rows[irow].Cells[0].Value) == kr.IDRekening)
                        {
                            gridRekening.Rows[irow].Cells[5].Value = kr.Jumlah.ToRupiahInReport();
                        }
                    }
                }
            }
            HitungJumlah();
            return true;
        }


        public bool SetBAST(BAST oBAST)
        {
            // mengisi pada kolom belanja dengan nilaiBAST
            try
            {
                List<BASTRekening> lstBR = new List<BASTRekening>();

                if (oBAST.Rekening == null)
                {
                    BASTLogic oLogic = new BASTLogic(GlobalVar.TahunAnggaran);
                    lstBR = oLogic.GetDetail(oBAST.NoUrut);

                }
                else
                    lstBR = oBAST.Rekening;

                foreach (BASTRekening br in lstBR)
                {
                    for (int irow = 0; irow < gridRekening.Rows.Count; irow++)
                    {
                        if (gridRekening.Rows[irow].Cells[0].Value != null)
                        {
                            if (DataFormat.GetLong(gridRekening.Rows[irow].Cells[0].Value) == br.IDRekening)
                            {
                                gridRekening.Rows[irow].Cells[5].Value = br.Jumlah.ToRupiahInReport();
                            }
                        }
                    }

                }
                HitungJumlah();
                return true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        
        public bool Clear()
        {
            gridRekening.Rows.Clear();

            return true;
        }

        public bool LoadAnggaranKas(int idDinas,int iKodeuk,  int tahap,DateTime dBatas, long idSubkegiatan = 0)
        {

            m_idDinas = idDinas;
   
            m_iJenis = 1;
            m_bPPKD = 0;
            m_IDSubKegiatan = idSubkegiatan;

            TSubKegiatanLogic oLogic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, 3);
            List<AnggaranKas> lst = new List<AnggaranKas>();
            
                lst = oLogic.GetRekeningBasedAnggaranKas(m_idDinas, iKodeuk, tahap, idSubkegiatan, dBatas);
            


                foreach (AnggaranKas rd in lst)
                {
                    string[] row = { rd.IDRekening.ToString(),
                                   "...",
                                   rd.IDRekening.ToKodeRekening(6),
                                   rd.Nama,
                                   rd.Bulan1.ToRupiahInReport(),
                                   "0",
                                   rd.IDUrusan.ToString(),
                                   rd.IDProgram.ToString(),
                                   rd.IDKegiatan.ToString(),
                                   idSubkegiatan.ToString(),rd.IDUnit.ToString(), "0","0" };

                    gridRekening.Rows.Add(row);

                }
            return true;


        }
        public int Keperluan
        {
            set { 
                m_iKeperluan = value;
                RefreshTampilan();
            }
        }
        private void RefreshTampilan(){
            if (m_iKeperluan==1){
                gridRekening.Columns[4].HeaderText = "Sisa SPD";
                for (int c = 6; c < gridRekening.Columns.Count; c++)
                {
                    gridRekening.Columns[c].Visible = false;
                }

            }
            if (m_iKeperluan==2){
                gridRekening.Columns[4].HeaderText = "Sisa Anggarab Kas ";
                for (int c = 6; c < gridRekening.Columns.Count; c++)
                {
                    gridRekening.Columns[c].Visible = false;
                }

            }
            if (m_iKeperluan==3){
                gridRekening.Columns[4].HeaderText = "Sisa Anggarab Kas ";
                for (int c = 6; c < gridRekening.Columns.Count - 2; c++)
                {
                    gridRekening.Columns[c].Visible = false;
                }
                gridRekening.Columns[4].Visible = false;

                for (int c = 0; c < gridRekening.Columns.Count - 3; c++)
                {
                    gridRekening.Columns[c].ReadOnly = true;
                }

            
            }
            if (m_iKeperluan == 4)
            {
                gridRekening.Columns[4].HeaderText = "Sisa Anggaran";
                for (int c = 6; c < gridRekening.Columns.Count; c++)
                {
                    gridRekening.Columns[c].Visible = false;
                }

            }

            if (m_iKeperluan == 5)
            {
                gridRekening.Columns[4].HeaderText = "Sisa Anggaran";
                gridRekening.Columns[5].HeaderText = "Nilai Kontrak/SPK";

                for (int c = 6; c < gridRekening.Columns.Count; c++)
                {
                    gridRekening.Columns[c].Visible = false;
                }

            }
            if (m_iKeperluan == 6)
            {
                gridRekening.Columns[4].HeaderText = "Nilai Kontrak/SPK";
                gridRekening.Columns[5].HeaderText = "Nilai BAST";

                for (int c = 6; c < gridRekening.Columns.Count; c++)
                {
                    gridRekening.Columns[c].Visible = false;
                }

            }


            
        }
        private void gridRekening_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex< gridRekening.Rows.Count)
            {
                string srek = DataFormat.GetString(gridRekening.Rows[e.RowIndex].Cells[2].Value).Replace(".", "");
                int posDash = srek.IndexOf("-");
                srek = srek.Substring(posDash+1).Trim();
                long iDRekenig= DataFormat.GetLong(srek);
                frmSPDdanRealissasi fSPDRealisasi = new frmSPDdanRealissasi();
                int kodeuk= DataFormat.GetInteger(gridRekening.Rows[e.RowIndex].Cells[10].Value);
           
                int idUrusan = DataFormat.GetInteger(gridRekening.Rows[e.RowIndex].Cells[6].Value);
                int idprogram = DataFormat.GetInteger(gridRekening.Rows[e.RowIndex].Cells[7].Value);
                int idKegiatan = DataFormat.GetInteger(gridRekening.Rows[e.RowIndex].Cells[8].Value);
                long idsubkegiatan = DataFormat.GetLong(gridRekening.Rows[e.RowIndex].Cells[9].Value);
                
                
                fSPDRealisasi.SetProgramKegiatan(
                    m_idDinas,  
            m_KodeUK,
                  idUrusan,
            idprogram,
            idKegiatan,
            idsubkegiatan,
            iDRekenig
                    );

                fSPDRealisasi.Show();
            
            }
        }
        public bool SetDataKoreksi(List<KoreksiDetail> lst, int kedua=0)
        {
            try
            {
                //KoreksiDetail kd= new KoreksiDetail();
                
                    //if (kedua == 0) {
                    //    kd = lst[0];
                    //} else{
                    //    kd= lst[1];
                    //}
                foreach (KoreksiDetail kd in lst)
                {
                    foreach (DataGridViewRow row in gridRekening.Rows)
                    {
                        if (row.Cells[9].Value != null)
                        {
                            if (kd.IDSubKegiatan == DataFormat.GetLong(row.Cells[9].Value.ToString().Replace(".", "")) &&
                                kd.IDRekening1 == DataFormat.GetLong(row.Cells[0].Value.ToString().Replace(".", "")))
                            {

                                decimal nilaiBelanja = DataFormat.FormatUangReportKeDecimal(row.Cells[5].Value.ToString());
                               // decimal input = DataFormat.FormatUangReportKeDecimal(row.Cells[11].Value);
                                decimal koreksi = kd.Jumlah1;

                             //   row.Cells[11].Value = input.ToRupiahInReport();
                                //januari row.Cells[12].Value = koreksi.ToRupiahInReport();
                                row.Cells[12].Value = (kd.Debet1 * koreksi).ToRupiahInReport();
                                // Menjadi negatif jika 11> nilai belanja
                                row.Cells[11].Value = (nilaiBelanja - (( kd.Debet1 *  koreksi))).ToRupiahInReport();
                                // //januari 
                            /*    
                                if (kd.Debet1 == 1) 
                                {
                                    //row.Cells[12].Value = (kd.Jumlah1).ToRupiahInReport();
                                    row.Cells[11].Value = (kd.Jumlah1 - nilaiBelanja).ToRupiahInReport();

                                }
                                else
                                {
                                    //kd= belanja- koreksi 
                                    // k= jml-belanja
                                    // jml= k-belanja
                                    // jml= k+ Belanja
                                    row.Cells[12].Value = (-1 * kd.Jumlah1).ToRupiahInReport();
                                    row.Cells[11].Value = (nilaiBelanja- (-1 * kd.Jumlah1)).ToRupiahInReport();
                                  //  gridRekening1.TextMatrix(i, 4) = fMoney(CCUrEx(gridRekening1.TextMatrix(i, 3)) + rsDetail!cJumlah1)
                                }
                              */  //
                            }

                        }
                    }

                } 
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public void DisplaySPP(int idDinas, int idProgram, long iDKegiatan, long idSubKegiatan, long inoUrutSPD, DateTime tanggal, int iJenis, int KodeUK, bool addMode = false, long iidrekning = 0, decimal cJumlahSPP = 0, long noUrutSPP = 0)
        {


            SPDLogic oSPDLogic = new SPDLogic((int)GlobalVar.TahunAnggaran);
            List<SPDDetail> lstSPD = oSPDLogic.GetDetailSebelumNoUrutEx(inoUrutSPD, idDinas, idSubKegiatan, KodeUK);

            RealisasiLogic oRLogic = new RealisasiLogic((int)GlobalVar.TahunAnggaran);
            List<Realisasi> lstRealisasi = new List<Realisasi>();

            lstRealisasi = oRLogic.GetSebelum(idDinas, KodeUK, idSubKegiatan, tanggal, noUrutSPP, 0);
            if (addMode == false)
                gridRekening.Rows.Clear();

            decimal decReturn;
            decReturn = 0l;


            if (lstSPD != null)
            {
                bool bFound = false;
                foreach (SPDDetail r in lstSPD)
                {
                    bFound = false;
                    decimal nilaiSisa = r.Jumlah;

                    foreach (Realisasi realisasi in lstRealisasi)
                    {
                        if (iidrekning > 0)
                        {
                            if (r.IDRekening == iidrekning && r.IDSubkegiatan == idSubKegiatan && r.IDRekening == realisasi.IIDRekening && r.IDSubkegiatan == realisasi.IDSubKegiatan)
                            {
                                nilaiSisa = r.Jumlah - realisasi.cJumlah;
                                bFound = true;
                            }



                            if (r.IDRekening == iidrekning && r.IDSubkegiatan == idSubKegiatan && r.IDRekening == realisasi.IIDRekening && r.IDSubkegiatan == realisasi.IDSubKegiatan)
                            {
                                string[] row = { r.IDRekening.ToString(), "RO", r.IDRekening.ToString().ToKodeRekening(), r.NamaRekening, nilaiSisa.ToRupiahInReport(), cJumlahSPP.ToRupiahInReport(), r.IDUrusan.ToString(), r.IDProgram.ToString(), r.IDKegiatan.ToString(), r.IDSubkegiatan.ToString(), KodeUK.ToString() };
                                gridRekening.Rows.Add(row);
                            }
                        }
                        else
                        {
                            if (r.IDRekening == realisasi.IIDRekening && r.IDSubkegiatan == realisasi.IDSubKegiatan)
                            {
                                nilaiSisa = r.Jumlah - realisasi.cJumlah;
                                bFound = true;
                            }



                            if (r.IDRekening == realisasi.IIDRekening && r.IDSubkegiatan == realisasi.IDSubKegiatan)
                            {
                                string[] row = { r.IDRekening.ToString(), "RO", r.IDRekening.ToString().ToKodeRekening(), r.NamaRekening, nilaiSisa.ToRupiahInReport(), cJumlahSPP.ToRupiahInReport(), r.IDUrusan.ToString(), r.IDProgram.ToString(), r.IDKegiatan.ToString(), r.IDSubkegiatan.ToString(), KodeUK.ToString() };
                                gridRekening.Rows.Add(row);
                            }

                        }


                    }
                    if (bFound == false)
                    {
                        if (r.IDRekening == iidrekning)
                        {
                            string[] row = { r.IDRekening.ToString(), "RO", r.IDRekening.ToString().ToKodeRekening(),
                                               r.NamaRekening, nilaiSisa.ToRupiahInReport(),
                                               cJumlahSPP.ToRupiahInReport(), r.IDUrusan.ToString(), 
                                               r.IDProgram.ToString(), r.IDKegiatan.ToString(), r.IDSubkegiatan.ToString(), KodeUK.ToString() };
                            gridRekening.Rows.Add(row);
                        }
                    }

                }

            }

        }
        public int Dinas
        {
            set
            {
                m_idDinas = value;
            }
        }
        public void CreateNewSPP(int idDinas, int idProgram, long iDKegiatan, long idSubKegiatan, long inoUrutSPD, DateTime tanggal, int iJenis,int KodeUK, bool addMode = false,long iidrekning=0,decimal cJumlahSPP=0, long noUrutSPP =0 )
        {


            SPDLogic oSPDLogic = new SPDLogic((int)GlobalVar.TahunAnggaran);
            //List<SPDDetail> lstSPD = oSPDLogic.GetDetailSebelumNoUrutEx(inoUrutSPD, idDinas,  idSubKegiatan,KodeUK);
            List<SPDDetail> lstSPD = oSPDLogic.GetDetailSebelumTanggalEx(tanggal, idDinas, idSubKegiatan, KodeUK);

            m_idDinas = idDinas;

            RealisasiLogic oRLogic = new RealisasiLogic((int)GlobalVar.TahunAnggaran);
            List<Realisasi> lstRealisasi = new List<Realisasi>();

            lstRealisasi = oRLogic.Get(idDinas, KodeUK, idSubKegiatan, tanggal, noUrutSPP, 0);
            if (addMode == false )
            gridRekening.Rows.Clear();

            decimal decReturn;
            decReturn = 0l;


            if (lstSPD != null)
            {
                bool bFound = false;
                foreach (SPDDetail  r in lstSPD)
                {
                    bFound = false;
                    decimal nilaiSisa = r.Jumlah;
                    
                    foreach (Realisasi realisasi in lstRealisasi)
                    {
                        if (iidrekning > 0)
                        {
                            if (r.IDRekening == iidrekning && r.IDSubkegiatan == idSubKegiatan && r.IDRekening == realisasi.IIDRekening && r.IDSubkegiatan == realisasi.IDSubKegiatan)
                            
                            {
                                nilaiSisa = r.Jumlah - realisasi.cJumlah;
                                bFound = true;
                            }


                        
                            if (r.IDRekening == iidrekning && r.IDSubkegiatan == idSubKegiatan && r.IDRekening == realisasi.IIDRekening && r.IDSubkegiatan == realisasi.IDSubKegiatan)
                            {
                                bFound = true;

                                string[] row = { r.IDRekening.ToString(), "RO", r.IDRekening.ToString().ToKodeRekening(), r.NamaRekening, nilaiSisa.ToRupiahInReport(), cJumlahSPP.ToRupiahInReport(), r.IDUrusan.ToString(), r.IDProgram.ToString(), r.IDKegiatan.ToString(), r.IDSubkegiatan.ToString(), KodeUK.ToString() };
                                gridRekening.Rows.Add(row);
                            }
                        }
                        else
                        {
                            if (r.IDRekening == realisasi.IIDRekening && r.IDSubkegiatan == realisasi.IDSubKegiatan)
                            {
                                    nilaiSisa = r.Jumlah - realisasi.cJumlah;
                                    bFound = true;
                            }



                            if (r.IDRekening == realisasi.IIDRekening && r.IDSubkegiatan == realisasi.IDSubKegiatan)
                            {
                                string[] row = { r.IDRekening.ToString(), "RO", r.IDRekening.ToString().ToKodeRekening(), r.NamaRekening, nilaiSisa.ToRupiahInReport(), cJumlahSPP.ToRupiahInReport(), r.IDUrusan.ToString(), r.IDProgram.ToString(), r.IDKegiatan.ToString(), r.IDSubkegiatan.ToString(), KodeUK.ToString() };
                                gridRekening.Rows.Add(row);
                            }

                        }


                    }
                    if (bFound == false)
                    {
                        string[] row = { r.IDRekening.ToString(), "RO", r.IDRekening.ToString().ToKodeRekening(), r.NamaRekening, nilaiSisa.ToRupiahInReport(), cJumlahSPP.ToRupiahInReport(), r.IDUrusan.ToString(), r.IDProgram.ToString(), r.IDKegiatan.ToString(), r.IDSubkegiatan.ToString(), KodeUK.ToString() };
                        gridRekening.Rows.Add(row);

                    }
                    
                }

            }
            
        }
        public void CreateNewSPPSariSPJUP(int idDinas, long NoUrutSPJUP, long noUrutSPD)
        {


           


            decimal decReturn;
            decReturn = 0l;


            List<SPJRekening> lstSPJ = new List<SPJRekening>();
            SPJLogic oLogic = new SPJLogic(GlobalVar.TahunAnggaran);


            lstSPJ = oLogic.GetSPJRekening(NoUrutSPJUP, noUrutSPD);

            if (oLogic.IsError() == true)
            {
                MessageBox.Show(oLogic.LastError());
                return;

            }
            gridRekening.Rows.Clear();
            m_dJumah = 0;
            
            foreach (SPJRekening r in lstSPJ)
            {
                        string[] row = { r.IDRekening.ToString(), "RO", r.IDRekening.ToString().ToKodeRekening(), r.NamaRekening, r.JumlahSPD.ToRupiahInReport(), r.Jumlah.ToRupiahInReport(), r.IDUrusan.ToString(), r.IDProgram.ToString(), r.IDKegiatan.ToString(), r.IDSubKegiatan.ToString(), KodeUK.ToString() };
                        gridRekening.Rows.Add(row);

                        m_dJumah = m_dJumah + r.Jumlah;
            }


            HitungJumlah();
            if (OnChanged != null)
            {
                OnChanged(m_dJumah);
            }

            

        }

        private int GetJenisSPDBasedJenisSPP(SPP oSPP)
        {
            if (oSPP.Jenis == 4)
            {
                return 3;
            }
            else
            {
                if (oSPP.PPKD == 0 && (oSPP.Jenis == 3 || oSPP.Jenis == 1 || oSPP.Jenis == 2 || oSPP.Jenis == 0))
                {
                    return 4;
                }
                else
                    return 5;
            }
        }

        public decimal JumlahRekening
        {
            get { return m_dJumah; }
        }

        public void CreateSPP(SPP oSPP)
        {

            decimal cJumlahSPP = 0L;

            SPDLogic oSPDLogic = new SPDLogic((int)GlobalVar.TahunAnggaran);
            List<SPDDetail> lstSPD = new List<SPDDetail>();
         
            List<SPPRekening> _lstSPPRekening = new List<SPPRekening>();            
            _lstSPPRekening = oSPP.Rekenings.FindAll(x=>x.Jumlah >0);

            var  _lstProgramKegiatanSUbKegiatanSPPRekening = _lstSPPRekening.GroupBy(x => new { x.UnitKerja, x.IDUrusan, x.IDProgram, x.IDKegiatan, x.IDSubKegiatan })
                                                                       .Select ( y => new SPPRekening
                                                                       {
                                                                           UnitKerja= y.Key.UnitKerja,
                                                                           IDSubKegiatan = y.Key.IDSubKegiatan,
                                                                           IDUrusan = y.Key.IDUrusan,
                                                                           IDProgram = y.Key.IDProgram,
                                                                           IDKegiatan = y.Key.IDKegiatan


                                                                       }); 



            RealisasiLogic oRLogic = new RealisasiLogic((int)GlobalVar.TahunAnggaran);
            
            List<Realisasi> lstRealisasi = new List<Realisasi>();
            lstRealisasi = oRLogic.GetgroupedByKegiatanAndRekening(oSPP.IDDInas, oSPP.dtSPP, 0, oSPP.NoUrut);
            if (oRLogic.IsError() == true)
            {
                MessageBox.Show(oRLogic.LastError());
            }
            // untuk tiap program Kegiatan 
            foreach (var sr in _lstProgramKegiatanSUbKegiatanSPPRekening)
            {
                /// dicari spdnya 
                /// 

                lstSPD = oSPDLogic.GetDetailSebelumNoUrutEx(oSPP.NoUrutSPD, oSPP.IDDInas, sr.IDSubKegiatan, oSPP.UnitAnggaran);
                decimal decReturn;
                decReturn = 0l;
                gridRekening.Rows.Clear();
                //  gridRekening.Rows.Clear();

                decimal sisa = 0l;
                decimal cspp = 0L;
                if (lstSPD != null)
                {

                    foreach (SPDDetail sk in lstSPD)
                    {

                        sisa = sk.Jumlah;
                        cspp = 0;
                        if (sk.IDRekening == 510507010001)
                        {
                            cspp = 0;
                        }
                        foreach (Realisasi rl in lstRealisasi)
                        {

                            if (oSPP.Jenis == 4 || oSPP.Jenis == 3 || oSPP.Jenis == 1 || oSPP.Jenis == 2)
                            {


                                if (rl.IDKegiatan == sk.IDKegiatan && rl.IDSubKegiatan == sk.IDSubkegiatan && rl.IIDRekening == sk.IDRekening)
                                {
                                    sisa = sk.Jumlah - rl.cJumlah;

                                }
                            }
                            else
                            {
                                if (rl.IDKegiatan == sk.IDKegiatan && rl.IDSubKegiatan == sk.IDSubkegiatan && rl.IIDRekening == sk.IDRekening)
                                {
                                    sisa = sk.Jumlah - rl.cJumlah;

                                }
                            }

                        }


                        foreach (SPPRekening sxr in _lstSPPRekening)
                        {

                            if (oSPP.Jenis == 3 || oSPP.Jenis == 1 || oSPP.Jenis == 2)
                            {
                                if ((sr.IDKegiatan == sxr.IDKegiatan) && (sr.IDSubKegiatan == sxr.IDSubKegiatan) && sk.IDRekening == sxr.IDRekening)
                                {
                                    cspp = sxr.Jumlah;

                                }
                            }
                            else
                            {
                                if (sxr.IDRekening == sk.IDRekening)
                                {
                                    cspp = sxr.Jumlah;

                                }
                            }
                        }

                        string[] row = { sk.IDRekening.ToString(), "RO", sk.IDKegiatan.ToKodeKegiatan() + "- " + sk.IDRekening.ToString().ToKodeRekening(), sk.NamaRekening, sisa.ToRupiahInReport(), cspp.ToRupiahInReport(), sk.IDUrusan.ToString(), sk.IDProgram.ToString(), sk.IDKegiatan.ToString(), sk.IDSubkegiatan.ToString(), sr.UnitKerja.ToString() };
                        gridRekening.Rows.Add(row);
                        cJumlahSPP = cJumlahSPP + cspp;


                    }

                }
            }

         
            m_dJumah = cJumlahSPP;
            if (OnChanged != null)
            {
                OnChanged(m_dJumah);
            }



        }
        public Decimal JumlahSPP
        {
            get { return m_dJumah; }
        }
        

        private List<SPDDetail> GetSPDBefore(int idDinas, int IDProgram, long iDKegiatan, long idSubKegiatan, long inoUrutSPD,
                              DateTime tanggal, int iJenis,int KodeUK, bool addMode = false)
        {

            List<SPDDetail> lst = new List<SPDDetail>();
     

            if (addMode == false)
                gridRekening.Rows.Clear();
            // dari spd
            SPDLogic oSPDLogic = new SPDLogic((int)GlobalVar.TahunAnggaran);
            lst = oSPDLogic.GetDetailSebelumNoUrutEx(inoUrutSPD, idDinas,idSubKegiatan,KodeUK);

            return lst;


        }


        public decimal CreatePanjar(long inoUrut)
        {

            PengeluaranLogic oLogic = new PengeluaranLogic((int)GlobalVar.TahunAnggaran);
            Pengeluaran oSPP = new Pengeluaran();

            List<PengeluaranRekening> lst = new List<PengeluaranRekening>();
            
            lst = oLogic.GetDetail(inoUrut);

            decimal dRet = 0l;

   
            if (lst == null)
                return dRet;


            decimal cJumlah = 0L;

            if (lst != null)
            {
                for (int row = 0; row < gridRekening.Rows.Count; row++)
                {
                    long idRek = DataFormat.GetLong(gridRekening.Rows[row].Cells[0].Value);
                    long idsub = DataFormat.GetLong(gridRekening.Rows[row].Cells[9].Value);
                    foreach (PengeluaranRekening r in lst)
                    {
                        if (r.IDRekening == idRek  && r.IDSUbKegiatan== idsub)
                        {
                            gridRekening.Rows[row].Cells[5].Value = r.Jumlah.ToRupiahInReport();
                            dRet = dRet + r.Jumlah;
                        }
                    }

                }

            }
  
            m_dJumah = dRet;
            return dRet;
        }

        public bool CreateDariAnggaranKas(int idDinas, int idProgram, long iDKegiatan, long idSubKegiatan,  DateTime tanggal, int tahap, int KodeUK)
        {
            try
            {

                AnggaranKasLogic akLogic = new AnggaranKasLogic((int)GlobalVar.TahunAnggaran);

                // 3 Jenis Belanja 
                List<AnggaranKas> lstAnggaranKas = akLogic.GetAKumulasiAnggaranKas(GlobalVar.TahunAnggaran,  idDinas,KodeUK, 3,tanggal,tahap, idSubKegiatan);


                RealisasiLogic oRLogic = new RealisasiLogic((int)GlobalVar.TahunAnggaran);

                List<Realisasi> lst = new List<Realisasi>();

                lst = oRLogic.GetUntukSPJ(idDinas, KodeUK, idSubKegiatan, tanggal, 0);

                decimal decReturn;
                decReturn = 0l;


                //if (addMode == false)
                //    gridRekening.Rows.Clear();


                if (lstAnggaranKas != null)
                {
                    decimal nilai = 0;
                    foreach (AnggaranKas r in lstAnggaranKas)
                    {
                        nilai=r.Bulan1;
                        foreach (Realisasi realisasi in lst)
                        {
                            if (realisasi.IDSubKegiatan == r.IdSubKegiatan && realisasi.IIDRekening == r.IDRekening)
                            {
                                nilai= nilai- realisasi.cJumlah;
                            }
                        }

                        string[] row = { r.IDRekening.ToString(), "RO", r.IDRekening.ToString().ToKodeRekening(), r.Nama, nilai.ToRupiahInReport(), "0", r.IDUrusan.ToString(), r.IDProgram.ToString(), r.IDKegiatan.ToString(), r.IdSubKegiatan.ToString(), KodeUK.ToString(), "0", "0" };
                        
                        gridRekening.Rows.Add(row);


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
        public bool CreateDariSP2DTU(int idDinas,
            long idSubKegiatan, 
            long inourutSP2d , DateTime tanggal, int KodeUK)
        {
            try
            {

                SPPLogic sppLogic = new SPPLogic((int)GlobalVar.TahunAnggaran);

                // 3 Jenis Belanja 
                List<SPPRekening> lstSPPRekening = sppLogic.GetDetail(inourutSP2d, idSubKegiatan);
                RealisasiLogic oRLogic = new RealisasiLogic((int)GlobalVar.TahunAnggaran);

                List<Realisasi> lst = new List<Realisasi>();

                lst = oRLogic.GetUntukSPJTU(idDinas, KodeUK, idSubKegiatan, tanggal, inourutSP2d);

                decimal decReturn;
                decReturn = 0l;


                if (lstSPPRekening  != null)
                {
                    decimal nilai = 0;
                    foreach (SPPRekening spprekening in lstSPPRekening)
                    {
                        nilai = spprekening.Jumlah;
                        foreach (Realisasi realisasi in lst)
                        {
                            if (realisasi.IDSubKegiatan == spprekening.IDSubKegiatan
                                && realisasi.IIDRekening == spprekening.IDRekening)
                            {
                                nilai = nilai - realisasi.cJumlah;
                            }
                        }

                        string[] row = { spprekening.IDRekening.ToString(), 
                                           "RO", spprekening.IDRekening.ToString().ToKodeRekening(), 
                                           spprekening.NamaRekening, nilai.ToRupiahInReport(), "0", 
                                           spprekening.IDUrusan.ToString(), 
                                           spprekening.IDProgram.ToString(), 
                                           spprekening.IDKegiatan.ToString(), 
                                           spprekening.IDSubKegiatan.ToString(),
 KodeUK.ToString(), "0", "0" };

                        gridRekening.Rows.Add(row);


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

        public  void LoadAnggaran( int Tahap, DateTime tanggalBatas )
        {
            TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
            List<TAnggaranRekening> lstRekening = new List<TAnggaranRekening>();
            lstRekening = oLogic.GetMinRealisasi(GlobalVar.TahunAnggaran, m_idDinas, m_IDUrusan, m_IDProgram, m_IDKegiatan,m_IDSubKegiatan, Tahap, tanggalBatas,m_KodeUK );

            decimal cJumlah=0l;
            
            // int _barisRekening = 0;
            foreach (TAnggaranRekening ta in lstRekening)
            {
                cJumlah=0l;
                

                //              '0                       ,1   ,2       ,3   ,4   ,5   ,6   ,7  ,8  ,9                                          ,10  ,11      ,12  ,13  ,14 ,15                         ,16  ,17 ,18  ,19  ,20  ,21 ,22
                string[] row = { ta.IDRekening.ToString(), "", ta.IDRekening.ToString().ToKodeRekening(), ta.Nama, (ta.Jumlah- ta.Realisasi).ToRupiahInReport(), "0", m_IDUrusan.ToString(), m_IDProgram.ToString().ToString(), m_IDKegiatan.ToString(), m_IDSubKegiatan.ToString() };
                gridRekening.Rows.Add(row);

            }


           

        }
        public  void LoadKontrak(long inoKontrak)
        {
            KontrakLogic oLogic = new KontrakLogic(GlobalVar.TahunAnggaran);
            Kontrak oKontrak = new Kontrak();
            oKontrak = oLogic.Get(inoKontrak);
            if (oKontrak.IDDInas != m_idDinas ||
                oKontrak.IDProgram != m_IDProgram ||
                    oKontrak.IDKegiatan != m_IDKegiatan || 
                    oKontrak.IDSubKegiatan != m_IDSubKegiatan)
            {
                MessageBox.Show ("Kontrak Salah kegiatan");
                return;
            }



            //gridRekening.Rows.Clear();

            if (oKontrak != null)
            {
                // int _barisRekening = 0;
                foreach (KontrakRekening kr in oKontrak.Rekening)
                {
                    //              '0                       ,1   ,2       ,3   ,4   ,5   ,6   ,7  ,8  ,9                                          ,10  ,11      ,12  ,13  ,14 ,15                         ,16  ,17 ,18  ,19  ,20  ,21 ,22
                    string[] row = { kr.IDRekening.ToString(), "", kr.IDRekening.ToString().ToKodeRekening(), kr.NamaRekening, kr.Jumlah.ToRupiahInReport(), "0",m_IDUrusan.ToString(), m_IDProgram.ToString(), m_IDKegiatan.ToString(), m_IDSubKegiatan.ToString() };
              
                    gridRekening.Rows.Add(row);

                }
            }


            //HitungJumlahRKA();

        }
        private void  HitungJumlah()
        {
            try
            {

                m_dJumah = 0L;
                for (int idx = 0; idx < gridRekening.Rows.Count; idx++)
                {
                    if (gridRekening.Rows[idx].Cells[2].Value != null)
                    {
                        m_dJumah = m_dJumah + DataFormat.FormatUangReportKeDecimal(gridRekening.Rows[idx].Cells[5].Value);

                     
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void ctrlRekeningKegiatan_Load(object sender, EventArgs e)
        {
            gridRekening.FormatHeader();

        }
        
        public List<SPPRekening> getDisplayRekening()
        {
            List<SPPRekening> lst = new List<SPPRekening>();

            for (int idx = 0; idx < gridRekening.Rows.Count; idx++)
            {
                if (gridRekening.Rows[idx].Cells[2].Value != null){
                SPPRekening sr = new SPPRekening();
                sr.NamaRekening = gridRekening.Rows[idx].Cells[3].Value.ToString();
                sr.KodeRekening = gridRekening.Rows[idx].Cells[2].Value.ToString();
                sr.JumlahInString = gridRekening.Rows[idx].Cells[5].Value.ToString();
                
                    sr.IDSubKegiatan = DataFormat.GetLong(gridRekening.Rows[idx].Cells[9].Value.ToString());

                sr.IDKegiatan = DataFormat.GetInteger(gridRekening.Rows[idx].Cells[8].Value.ToString());
                sr.IDRekening = DataFormat.GetLong (gridRekening.Rows[idx].Cells[0].Value.ToString().Replace(".",""));
                sr.IDProgram = DataFormat.GetInteger(gridRekening.Rows[idx].Cells[7].Value.ToString()); 
                sr.IDUrusan = DataFormat.GetInteger(gridRekening.Rows[idx].Cells[6].Value.ToString());
                sr.UnitKerja = DataFormat.GetInteger(gridRekening.Rows[idx].Cells[10].Value.ToString());
                sr.KodekategoriPelaksana = DataFormat.GetString(gridRekening.Rows[idx].Cells[6].Value).ToKodeKategoriPelaksana ();
                sr.KodeUrusanPelaksana = DataFormat.GetString(gridRekening.Rows[idx].Cells[6].Value).ToKodeUrusanPelaksana();
                sr.KodeProgram = DataFormat.GetString(gridRekening.Rows[idx].Cells[6].Value).ToKodeProgram ();
                sr.KodeKegiatan = DataFormat.GetString(gridRekening.Rows[idx].Cells[8].Value).ToKodeKegiatan ();
                sr.KodeSubKegiatan = DataFormat.GetString(gridRekening.Rows[idx].Cells[9].Value).ToKodeSubKegiatan ();
                
                    string h = gridRekening.Rows[idx].Cells[5].Value.ToString();

                    sr.Jumlah = Decimal.Round(DataFormat.FormatUangReportKeDecimal(h));

                    if (sr.Jumlah !=0 )
                        lst.Add(sr);
                }

            }
            return lst;
        }
        public List<BASTRekening> GetBASTRekening()
        {
            List<BASTRekening> lst = new List<BASTRekening>();
            for (int idx = 0; idx < gridRekening.Rows.Count; idx++)
            {
                if (gridRekening.Rows[idx].Cells[2].Value != null)
                {
                    BASTRekening sr = new BASTRekening();
                    sr.NamaRekening = gridRekening.Rows[idx].Cells[3].Value.ToString();
                    
                    sr.IDRekening = DataFormat.GetLong(gridRekening.Rows[idx].Cells[0].Value.ToString().Replace(".", ""));
                    string h = gridRekening.Rows[idx].Cells[5].Value.ToString();

                    sr.Jumlah = Decimal.Round(DataFormat.FormatUangReportKeDecimal(h));


                    lst.Add(sr);
                }

            }
            return lst;
        }
        public List<PengeluaranRekening> GetPengeluaranRekening()
        {
            List<PengeluaranRekening> lst = new List<PengeluaranRekening>();
            for (int idx = 0; idx < gridRekening.Rows.Count; idx++)
            {
                if (gridRekening.Rows[idx].Cells[2].Value != null)
                {
                    PengeluaranRekening sr = new PengeluaranRekening();
                    sr.Nama= gridRekening.Rows[idx].Cells[3].Value.ToString();
                    sr.IDRekening= DataFormat.GetLong(gridRekening.Rows[idx].Cells[0].Value);
                    string h = gridRekening.Rows[idx].Cells[5].Value.ToString();

                    sr.Jumlah = Decimal.Round(DataFormat.FormatUangReportKeDecimal(h));


                    lst.Add(sr);
                }

            }
            return lst;
        }
        public List<KontrakRekening> getKontrakRekening()
        {
            List<KontrakRekening> lst = new List<KontrakRekening>();
            for (int idx = 0; idx < gridRekening.Rows.Count; idx++)
            {
                if (gridRekening.Rows[idx].Cells[2].Value != null)
                {
                    if (DataFormat.FormatUangReportKeDecimal(gridRekening.Rows[idx].Cells[5].Value) > 0)
                    {
                        KontrakRekening sr = new KontrakRekening();
                        sr.NamaRekening = gridRekening.Rows[idx].Cells[3].Value.ToString();
                        sr.IDRekening = DataFormat.GetLong(gridRekening.Rows[idx].Cells[0].Value);
                        sr.Jumlah = DataFormat.FormatUangReportKeDecimal(gridRekening.Rows[idx].Cells[5].Value); //gridRekening.Rows[idx].Cells[4].Value.ToString().Ru;
                        lst.Add(sr);
                    }
                }

            }
            return lst;
        }
      
        public string JumlahTerbilang
        {
            get
            {
                return m_dJumah.Terbilang();

            }
        }

        private void gridRekening_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                decimal h = DataFormat.GetDecimal(gridRekening.Rows[e.RowIndex].Cells[5].Value);// DataFormat.FormatUangReportKeDecimal(gridRekening.Rows[e.RowIndex].Cells[5].Value);
                decimal pagu = DataFormat.GetString((gridRekening.Rows[e.RowIndex].Cells[4].Value)).FormatUangReportKeDecimal();
                if (h > pagu)
                {
                    MessageBox.Show("Tidak boleh melebihi Sisa Anggaran/SPD/ANggaranKas");
                    gridRekening.Rows[e.RowIndex].Cells[5].Value="0";
                    h = 0;

                }
                gridRekening.Rows[e.RowIndex].Cells[5].Value = h.ToRupiahInReport();
                HitungJumlah();
                if (OnChanged != null)
                {
                    OnChanged(m_dJumah);
                }
            }
            if (e.ColumnIndex == 11)
            {
                DataGridViewRow row = gridRekening.Rows[e.RowIndex];
                if (row.Cells[0].Value == null)
                {
                    return;
                }
                decimal nilaiBelanja = DataFormat.FormatUangReportKeDecimal(row.Cells[5].Value.ToString());
                decimal input = DataFormat.FormatUangReportKeDecimal(gridRekening.Rows[e.RowIndex].Cells[11].Value);
                decimal koreksi = nilaiBelanja - input ;
                row.Cells[11].Value = input.ToRupiahInReport();
                row.Cells[12].Value = koreksi.ToRupiahInReport();

            }
         }
        public List<KoreksiDetail> GetKoreksiDetail()
        {
            try
            {
                List<KoreksiDetail> lst = new List<KoreksiDetail>();
                for (int idx = 0; idx < gridRekening.Rows.Count; idx++)
                {
                    if (gridRekening.Rows[idx].Cells[2].Value != null)
                    {
                        KoreksiDetail sr = new KoreksiDetail();
                      
                        sr.IDSubKegiatan = DataFormat.GetLong(gridRekening.Rows[idx].Cells[9].Value.ToString());
                        sr.IDKegiatan = DataFormat.GetInteger(gridRekening.Rows[idx].Cells[8].Value.ToString());
                        sr.IDRekening1= DataFormat.GetLong(gridRekening.Rows[idx].Cells[0].Value.ToString().Replace(".", ""));
                        sr.IDProgram = DataFormat.GetInteger(gridRekening.Rows[idx].Cells[7].Value.ToString());
                        sr.IDurusan= DataFormat.GetInteger(gridRekening.Rows[idx].Cells[6].Value.ToString());
                        sr.KodeUK1 = DataFormat.GetInteger(gridRekening.Rows[idx].Cells[10].Value.ToString());
                        sr.KodeKategoriPelaksana = DataFormat.GetString(gridRekening.Rows[idx].Cells[6].Value).ToKodeKategoriPelaksana();
                        sr.KodeUrusanPelaksana = DataFormat.GetString(gridRekening.Rows[idx].Cells[6].Value).ToKodeUrusanPelaksana();
                        sr.KodeProgram = DataFormat.GetString(gridRekening.Rows[idx].Cells[7].Value).ToKodeProgram();
                        sr.KodeKegiatan = DataFormat.GetString(gridRekening.Rows[idx].Cells[8].Value).ToKodeKegiatan();
                        sr.KodeSubKegiatan = DataFormat.GetString(gridRekening.Rows[idx].Cells[9].Value).ToKodeSubKegiatan();
                        
                        string nilai = gridRekening.Rows[idx].Cells[12].Value.ToString();
                        sr.Jumlah1 = DataFormat.FormatUangReportKeDecimal(nilai);
                        sr.Debet1 = sr.Jumlah1 < 0 ? -1 : 1;
                        //positifkan
                        sr.Jumlah1 = sr.Jumlah1 < 0 ? -1 * sr.Jumlah1 : sr.Jumlah1;
            //             rsKoreksiDetail.Fields("cJumlah1") = CCUrEx(gridRekening1.TextMatrix(i, 13))
            //rsKoreksiDetail.Fields("iDebet1") = IIf(CCUrEx(gridRekening1.TextMatrix(i, 13)) < 0, -1, 1)
            //rsKoreksiDetail.Fields("cJumlah1") = IIf(CCUrEx(gridRekening1.TextMatrix(i, 13)) < 0, -1 * CCUrEx(gridRekening1.TextMatrix(i, 13)), CCUrEx(gridRekening1.TextMatrix(i, 13)))
            

                        if (sr.Jumlah1 != 0)
                        {

                            lst.Add(sr);
                        }
                    }

                }
                return lst;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesahan mengambil data Koreksi...");
                return null;
            }
        }

    }
}
