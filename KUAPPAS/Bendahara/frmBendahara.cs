using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BP.Bendahara;
using BP;

//using DTO;
using Formatting;
using DTO.Bendahara;
using DTO;



namespace KUAPPAS.Bendahara
{
    public partial class frmBendahara : Form
    {
        private int m_iIDDInas;//m_iSKPD;

        private int m_iPPKD;
        // *******************
        private List<SPP> m_lstSPP;
        private List<TrxBank> m_lstTrxBank;
        private List<STS> m_lstSTS ;
        private List<Setor> m_lstSetorPenerimaan;
        private List<Setor> m_lstSetorPajak;
        private List<Kontrak> m_lstKontrak ;
        private List<BAST> m_lstBAST;
        private List<Pengeluaran> m_lstPanjar;
             
         private decimal m_sJumlahPenerimaan ;
         private decimal m_sJumlahPengeluaran;

        private List<Pengeluaran> m_lstPengeluaran;

        DataGridViewCellStyle _merah = new DataGridViewCellStyle();
        DataGridViewCellStyle _pink = new DataGridViewCellStyle();
        DataGridViewCellStyle _ijo = new DataGridViewCellStyle();
        //DataGridViewCellStyle _= new DataGridViewCellStyle();

        private Point _imageLocation = new Point(13, 5);
        private Point _imgHitArea = new Point(13, 2);

        Image CloseImage;


        private int m_iHakUser;
        // 1-> Anggaran 
        // 2-> Bendahara Penerimaan
        // 3 -> Benahara Pengeluaran
        // 43 -> PPK 
        // 5 -> Perbend
        // 6 -> Akuntansi
        // 7 -> Semua 

        DataGridViewCellStyle _ditolakStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _didiskusikanStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _diTerimaStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _diTangguhkanStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _baruStyle = new DataGridViewCellStyle();


        
        public frmBendahara()
        {
            InitializeComponent();
            m_lstSPP=new List<SPP>() ;
            m_lstPengeluaran= new List<Pengeluaran>();
            m_lstPanjar = new List<Pengeluaran>();
            m_lstTrxBank = new List<TrxBank>();
            m_lstSTS = new List<STS>();
            m_lstSetorPenerimaan = new List<Setor>();
            m_lstSetorPajak = new List<Setor>();
            m_lstKontrak = new List<Kontrak>();
            m_lstBAST = new List<BAST>();
        }

        private void frmBendahara_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            //foreach (TabPage t in tabControl1.TabPages){
            //    if (t.Name != "tabHalamanDepan")
            //    {
            //        tabControl1.TabPages.Remove(t);
            //    }
            
            //}
            gridSetorSTS.FormatHeader();
            gridSTS.FormatHeader();
            gridSPP.FormatHeader();
            gridBAST.FormatHeader();
 //           gridBKU.FormatHeader();
            gridBPK.FormatHeader();
            gridKontrak.FormatHeader();
            gridPanjar.FormatHeader();
            gridTrxBank.FormatHeader();
            gridPengembalian.FormatHeader();
            gridSetorPajak.FormatHeader();
            gridSetorSTS.FormatHeader();
            gridSKRSKPD.FormatHeader();
            _diTerimaStyle.BackColor = Color.GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);

            _diTangguhkanStyle.BackColor = Color.Cyan;

            //_didiskusikanStyle.Font = new Font(gridMusrenbang2.Font, FontStyle.Bold);
            _didiskusikanStyle.BackColor = Color.LightSkyBlue;// LightGray;

            //_ditolakStyle.Font = new Font(gridMusrenbang2.Font, FontStyle.Bold);
            _ditolakStyle.BackColor = Color.OrangeRed;
            _baruStyle.BackColor = Color.White;


            //tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            //tabControl1.DrawItem += TabControl1_DrawItem;
            //CloseImage = KUAPPAS.Properties.Resources.clear;
            //tabControl1.Padding = new Point(10, 3);
            splitContainer1.Panel2Collapsed = true;

            ctrlDinas1.Create();

        }
        //private void TabControl1_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        //{
        //    try
        //    {
        //        Image img = new Bitmap(CloseImage);
        //        Rectangle r = e.Bounds;
        //        r = this.tabControl1.GetTabRect(e.Index);
        //        r.Offset(2, 2);
        //        Brush TitleBrush = new SolidBrush(Color.Black);
        //        Font f = this.Font;
        //        string title = this.tabControl1.TabPages[e.Index].Text;

        //        e.Graphics.DrawString(title, f, TitleBrush, new PointF(r.X, r.Y));

        //        if (tabControl1.SelectedIndex >= 1)
        //        {
        //            e.Graphics.DrawImage(img, new Point(r.X + (this.tabControl1.GetTabRect(e.Index).Width - _imageLocation.X), _imageLocation.Y));
        //        }
        //    }
        //    catch (Exception) { }
        //}
        private void cmdLoadSPP_Click(object sender, EventArgs e)
        {
            
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {

            SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
            ParameterBendahara p = new ParameterBendahara();
            p.IDDInas = m_iIDDInas;// ctrlSKPD1.GetID();
            p.Jenis = ctrlJenisSPP1.GetID();
            p.NoSP2D = txtNoSP2D.Text;
            p.NoSPM = txtNoSPM.Text;
            p.NoSPP = txtNoSPP.Text;
           // p.Status 
            m_lstSPP = new List<SPP>();
            m_lstSPP = oLogic.Get(p);
            gridSPP.Rows.Clear();
            int iRow = 0;

            foreach (SPP spp in m_lstSPP)
            {
                string[] row = { spp.NoUrut.ToString(), "Detail","false",spp.NoSPP, spp.dtSPP.ToString("dd MMM"), spp.NoSPM, spp.dtSPM.ToString("dd MMM"), spp.NoSP2D, spp.dtCair.ToString("dd MMM"), spp.Keterangan, spp.Jumlah.ToRupiahInReport() };
             
    
                gridSPP.Rows.Add(row);
                if (spp.Status == 1)
                    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.Aqua;
                if (spp.Status == 2)
                    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.Pink;

                if (spp.Status == 3)
                    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.Purple;// Red;
                if (spp.Status == 4)
                    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.OrangeRed;// PaleVioletRed;// IndianRed;// Red;


                iRow++;


            }


        }

        private void gridSPP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < gridSPP.Rows.Count)
            {

                string sNoUrut = gridSPP.Rows[e.RowIndex].Cells[0].Value.ToString();
                long lNoUrut = DataFormat.GetLong(sNoUrut);
                SPP oSPP = new SPP();
                if (m_lstSPP != null)
                {
                    oSPP = m_lstSPP[e.RowIndex];
                }
        //        ctrlRekeningKegiatan1.CreateSPP(oSPP);
                if (e.ColumnIndex == 1)
                {
                    frmSPP fSPP = new frmSPP();
                    fSPP.SetModeForm(0);
                    fSPP.SetSPP(oSPP);
                    //fSPP.SetID(lNoUrut);
                    fSPP.Show();

                }

            }
            
        }

        private void cmdPanggilBPK_Click(object sender, EventArgs e)
        {
            PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
            ParameterBendahara pb = new ParameterBendahara();
            pb.IDDInas = m_iIDDInas;// ctrlSKPD1.GetID();
            pb.Jenis = 3;
            pb.TanggalAwal = ctrlPeriodeBPK.GetDateAwal();
            pb.TanggalAkhir = ctrlPeriodeBPK.GetDateAkhir();

            //List<Pengeluaran> _lst = oLogic.Get(pb);
            m_lstPengeluaran = oLogic.Get(pb);
            gridBPK.Rows.Clear();
            foreach (Pengeluaran p in m_lstPengeluaran)
            {
                string[] row = { p.NoUrut.ToString(), "Detail","false",p.NoBukti, p.Tanggal.ToString("dd MMM"), p.Penerima, p.Uraian, p.Jumlah.ToRupiahInReport() };
                gridBPK.Rows.Add(row);

            }
        }

        private void cmdPanggilPanjar_Click(object sender, EventArgs e)
        {
            
            PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
            ParameterBendahara pb = new ParameterBendahara();
            
            pb.IDDInas = m_iIDDInas;// ctrlSKPD1.GetID();
            pb.Jenis = 1;
            m_lstPanjar = oLogic.GetPanjarDanPertanggungJawabannya(pb);
             //= new List<Pengeluaran>();
            int i = 0;
            gridPanjar.Rows.Clear();
            foreach (Pengeluaran p in m_lstPanjar)
            {
                string[] row = { p.NoUrut.ToString(), "Detail", "false", p.NoBukti, p.Tanggal.ToFriendlyDateString(), p.Penerima, p.Uraian, p.Jumlah.ToRupiahInReport() ,p.NoReferensi.ToString()};
                gridPanjar.Rows.Add(row);
                if (p.NoReferensi == p.NoUrut)
                {
                    //DataGridViewCellStyle _diTerimaStyle = new DataGridViewCellStyle();
                    //DataGridViewCellStyle _diTangguhkanStyle = new DataGridViewCellStyle();

                    gridPanjar.Rows[i].DefaultCellStyle = _baruStyle;
                }
                else
                {
                    gridPanjar.Rows[i].DefaultCellStyle = _diTangguhkanStyle;
                }

                i++;

            }
        }

        private void cmdtampilkanKontrak_Click(object sender, EventArgs e)
        {
            LoadKontrak();
        }
        private void LoadKontrak(){
            KontrakLogic oLogic = new KontrakLogic((int)GlobalVar.TahunAnggaran);
            DateTime tanggalAwal = ctrlPeriodeKontrak.GetDateAwal();
            DateTime tanggalAkhir = ctrlPeriodeKontrak.GetDateAkhir();


            m_lstKontrak = oLogic.GetByIDDinas(m_iIDDInas, tanggalAwal, tanggalAkhir);
            
            gridKontrak.Rows.Clear();
            if (m_lstKontrak != null)
            {
                foreach (Kontrak k in m_lstKontrak)
                {
                    string[] row = { k.NoUrut.ToString(), "Ubah", k.NoKontrak, k.DtKontrak.ToString("dd MMM "), k.Uraian, k.NamaPerusahaan };
                    gridKontrak.Rows.Add(row);

                }
            }
        }
        public void  SetIDDInas(int pIDDInas)
        {
            m_iIDDInas = pIDDInas;

        }

        //private void ctrlSKPD1_OnChanged(int pID)
        //{
        //    m_iIDDInas = pID;

        //}

        private void cmdTampilkanBAST_Click(object sender, EventArgs e)
        {
            BASTLogic oLogic = new BASTLogic((int)GlobalVar.TahunAnggaran);
           // List<BAST> _lst = new List<BAST>();
            gridBAST.Rows.Clear();
            m_lstBAST = oLogic.GetByIDDInas(m_iIDDInas);
            if (m_lstBAST  != null)
            {
                foreach (BAST b in m_lstBAST)
                {
                    string[] row = { b.NoUrut.ToString(), "Detail","false", b.NoBAST, b.dtBAST.ToString("dd MMM"), b.Uraian, b.NOKontrak , b.NamaPihakKetiga, b.NoSP2D };
                    gridBAST.Rows.Add(row);

                }
            }
        }

        private void cmdTampilkanPenyetoranPajak_Click(object sender, EventArgs e)
        {
            SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
            List<Setor> lst = new List<Setor>();
            gridSetorPajak.Rows.Clear();
            DateTime tanggalAwal = ctrlPeriodeSetorPajak.GetDateAwal();
            DateTime tanggalAkhir = ctrlPeriodeSetorPajak.GetDateAkhir();
            lst = oLogic.GetByDinsd(m_iIDDInas, E_JENIS_SETOR.E_SETOR_PAJAK, tanggalAwal, tanggalAkhir);
            if (lst != null)
            {
                foreach (Setor s in lst)
                {
                    string[] row = { s.NoUrut.ToString(), s.NoUrutClient.ToString(), "Detail", s.dtBukuKas.ToString("dd MMM"), s.NoBukti, s.Keterangan, s.NoNTPN, s.Jumlah.ToRupiahInReport() };
                    gridSetorPajak.Rows.Add(row);

                }
            }
        }

        private void cmdTampilkanSTS_Click(object sender, EventArgs e)
        {
            STSLogic oLogic = new STSLogic((int)GlobalVar.TahunAnggaran);
            //List<STS> lst = new List<STS>();

            gridSTS.Rows.Clear();
            DateTime tanggalAwal = ctrlPeriodeSTS.GetDateAwal();
            DateTime tanggalAkhir = ctrlPeriodeSTS.GetDateAkhir();

            //m_lstSTS ;= new List<STS>();
          //  m_lstSTS = oLogic.GetByDinas(m_iIDDInas);//, E_JENIS_SETOR.E_SETOR_PAJAK);
            m_lstSTS = oLogic.GetByDinas(m_iIDDInas, tanggalAwal, tanggalAkhir);//, E_JENIS_SETOR.E_SETOR_PAJAK);
            if (m_lstSTS != null)
            {
                foreach (STS s in m_lstSTS)
                {
                    string[] row = { s.NoUrut.ToString(), "Detail", "false",s.TanggalSTS.ToString("dd MMM"), s.NoSTS, s.Keterangan, s.Penyetor, s.Jumlah.ToRupiahInReport() };
                    gridSTS.Rows.Add(row);

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
            m_lstSetorPenerimaan = new List<Setor>();
            gridSetorSTS.Rows.Clear();
            DateTime tanggalAwal = ctrlPeriodeSetorPenerimaan.GetDateAwal();
            DateTime tanggalAkhir = ctrlPeriodeSetorPenerimaan.GetDateAkhir();

            m_lstSetorPenerimaan = oLogic.GetByDinsd(m_iIDDInas, E_JENIS_SETOR.E_SETOR_STS,tanggalAwal, tanggalAkhir);
            if (m_lstSetorPenerimaan != null)
            {
                foreach (Setor s in m_lstSetorPenerimaan)
                {
                    string[] row = { s.NoUrut.ToString(), "Detail", "false",s.dtBukuKas.ToString("dd MMM"), s.NoBukti, s.Keterangan, s.Jumlah.ToRupiahInReport() };
                    gridSetorSTS.Rows.Add(row);

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //SKRSKPDLogic oLogic = new SKRSKPDLogic((int)GlobalVar.TahunAnggaran);
            //List<SKRSKPD> lst = new List<SKRSKPD>();
            //gridSKRSKPD.Rows.Clear();
            //lst = oLogic.GetByDinas(m_iIDDInas);//, E_JENIS_SETOR.E_SETOR_PAJAK);
            //if (lst != null)
            //{
            //    foreach (SKRSKPD s in lst)
            //    {
            //        string[] row = { s.NoUrut.ToString(), "Detail", "false", s.NoBukti, s.TanggalSKRSKPD.ToString("dd MMM"), s.Keterangan, s.Nama +  s.Alamat, s.Jumlah.ToRupiahInReport() };
            //        gridSKRSKPD.Rows.Add(row);

            //    }
            //}
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedTab.Name == "tabBKU")
            //if (tabControl1.Tabs"tabBKU")
          
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabBKU"])
            {
                ////splitContainer1.SplitterDistance = 100;
                //this.splitContainer1.Panel2.Hide();

                //this.splitContainer1.SplitterDistance = this.splitContainer1.Width;

            }
        }

        private void cmdPanggilBKU_Click(object sender, EventArgs e)
        {
            BKULogic oLogic = new BKULogic((int)GlobalVar.TahunAnggaran);
            List<BKUDISPLAY> lst = new List<BKUDISPLAY>();
            lst= oLogic.GetBKu(m_iIDDInas, E_JENISBENDAHARA.BENDAHARA_PENGELUARAN, ctrlPeriode1.GetDateAwal(), ctrlPeriode1.GetDateAkhir(), 0, -1);
            gridBKU.Rows.Clear();
            txtPenerimaanKini.Text = "0";
            txtPenerimaanLalu.Text = "0";
            txtPengeluaranLalu.Text = "0";
            txtPengeluaranKini.Text = "0";
            txtTunai.Text = "0";
            txtBank.Text = "0";
            txtSaldoAkhir.Text = "0";
            txtSaldo.Text = "0";






            if (lst != null)
            {
                
     
            //gridRekapProgram.Rows.Clear();
            //Font boldFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Bold);


                foreach (BKUDISPLAY b in lst)
                {

                    if (b.Level == 1)
                    {
                       

                        TreeGridNode node = gridBKU.Nodes.Add("Induk","Detail",b.NoBkU.ToString(),b.NoBukti, b.Tanggal.ToString("dd MMM"),
                            b.IDKegiatan.ToKodeKegiatan(GlobalVar.ProfileProgramKegiatan), b.Uraian, b.SPenerimaan, b.SPengeluaran, "", b.NoUrut.ToString(), b.NoUrutSumber.ToString());// ID.Kode + "-" + r.Nama, r.PENDAPATANINPUT, r.BTLINPUT, r.BLINPUT, r.PENDAPATANPAGU, r.BTLPAGU, r.BLPAGU,r.SelisihBelanja);
                        
                        LoadDetail(node, b.NoUrut, ref lst);
                                        
            

                    }

                }
            }
            BKUINFO bkuinfo = new BKUINFO();
            bkuinfo = oLogic.GetInfo(m_iIDDInas, E_JENISBENDAHARA.BENDAHARA_PENGELUARAN, ctrlPeriode1.GetDateAwal(), ctrlPeriode1.GetDateAkhir(), 0, -1);
            if (bkuinfo != null)
            {
                txtSaldo.Text = bkuinfo.SaldoAwal.ToRupiahInReport();
                txtPenerimaanKini.Text = bkuinfo.JumlahTerima.ToRupiahInReport();
                txtPenerimaanLalu.Text = bkuinfo.JumlahTerimalalu.ToRupiahInReport();
                txtPengeluaranLalu.Text = bkuinfo.JumlahKeluarLalu.ToRupiahInReport();
                txtPengeluaranKini.Text = bkuinfo.JumlahKeluar.ToRupiahInReport();

                m_sJumlahPenerimaan = (bkuinfo.JumlahTerima + bkuinfo.JumlahTerimalalu);
                m_sJumlahPengeluaran = (bkuinfo.JumlahKeluarLalu + bkuinfo.JumlahKeluar);

                txtTunai.Text = bkuinfo.JumlahTunai.ToRupiahInReport();
                txtBank.Text = bkuinfo.JumlahBank.ToRupiahInReport();
                txtSaldoAkhir.Text = (bkuinfo.SaldoAwal + (bkuinfo.JumlahTerima + bkuinfo.JumlahTerimalalu) - (bkuinfo.JumlahKeluar + bkuinfo.JumlahKeluarLalu)).ToRupiahInReport();

            }
        }

        private void LoadDetail(TreeGridNode nodeParent, long noUrut, ref List<BKUDISPLAY>lst)
        {
            foreach (BKUDISPLAY b in lst)
            {

                if (b.Level == 2 && b.NoUrut== noUrut )
                {


                    TreeGridNode node = nodeParent.Nodes.Add("-", "Detail", "", "", "",
                        b.IDKegiatan.ToKodeKegiatan(GlobalVar.ProfileProgramKegiatan) + "." + b.IDRekening.ToKodeRekening(GlobalVar.ProfileRekening), 
                        b.Uraian, b.SPenerimaan, b.SPengeluaran, "", b.NoUrut.ToString(), b.NoUrutSumber.ToString());// ID.Kode + "-" + r.Nama, r.PENDAPATANINPUT, r.BTLINPUT, r.BLINPUT, r.PENDAPATANPAGU, r.BTLPAGU, r.BLPAGU,r.SelisihBelanja);

                    



                }

            }

        }

        private void cmdPanggilDataCP_Click(object sender, EventArgs e)
        {
            SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
            List<Setor> lst = new List<Setor>();
            gridPengembalian.Rows.Clear();
            DateTime tanggalAwal = ctrlPeriodePengembalian.GetDateAwal();
            DateTime tanggalAkhir = ctrlPeriodePengembalian.GetDateAkhir();
            lst = oLogic.GetByDinsd(m_iIDDInas, E_JENIS_SETOR.E_SETOR_CP, tanggalAwal, tanggalAkhir);
            if (lst != null)
            {
                foreach (Setor s in lst)
                {
                    string[] row = { s.NoUrut.ToString(), "Detail", "false", s.NoBukti, s.dtBukuKas.ToString("dd MMM"), s.Keterangan,s.JenisSP2D .ToString(), s.NobuktiClient ,  s.Jumlah.ToRupiahInReport() };
                    gridPengembalian.Rows.Add(row);

                }
            }
        }

        private void gridBPK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridBPK.Rows.Count)
            {
                if (e.ColumnIndex == 1)
                {
                    //long noUrut = DataFormat.GetLong(gridBPK.Rows[e.RowIndex].Cells[0].Value);
                    Pengeluaran p = m_lstPengeluaran[e.RowIndex];
                    frmPengeluaran f = new frmPengeluaran();
                    f.SetPengeluaran(p);
                    f.Show();
                }

            }
        }

        private void cmdAddSPP_Click(object sender, EventArgs e)
        {
            frmSPP fSPP = new frmSPP();
            fSPP.ShowDialog();

        }

        private void cmdLoadTrxBank_Click(object sender, EventArgs e)
        {
            TrxBankLogic oLogic = new TrxBankLogic((int)GlobalVar.TahunAnggaran);
            List<TrxBank> lst = new List<TrxBank>();
            m_lstTrxBank = new List<TrxBank>();

            m_lstTrxBank = oLogic.GetByDinas(m_iIDDInas, (int)GlobalVar.TahunAnggaran,m_iPPKD);
            gridTrxBank.Rows.Clear();

            if (lst != null)
            {
                foreach (TrxBank tb in m_lstTrxBank)
                {
                    string[] row = { tb.ID.ToString(), "Detail", tb.jenis == 0 ? "Peyetoran" : "Pencairan", tb.NoBukti, tb.DTrx.ToString("dd MMM yyyy"), tb.Uraian, tb.Jumlah.ToRupiahInReport() };
                    gridTrxBank.Rows.Add(row);

                }
            }
        }

        private void gridTrxBank_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && (e.RowIndex >= 0 && e.RowIndex < gridTrxBank.Rows.Count))
            {
                TrxBank tBak = new TrxBank();

                tBak = m_lstTrxBank[e.RowIndex];
                frmTrxBank f = new frmTrxBank();
                f.Settrx(tBak);
                f.Show();
            }
        }

        private void gridSTS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && (e.RowIndex >= 0 && e.RowIndex < gridSTS.Rows.Count))
            {
                STS oSTS = new STS();

                oSTS = m_lstSTS[e.RowIndex];
                if (oSTS != null)
                {
                    frmSTS f = new frmSTS();
                    f.SetSTS(oSTS);
                    f.Show();
                }
            }

        }

        private void cmdAddPemyetoran_Click(object sender, EventArgs e)
        {
            frmSetorPenerimaan fsetor = new frmSetorPenerimaan();
            fsetor.ShowDialog();


        }

        private void gridSetorSTS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex  < gridSetorSTS.Rows.Count){
            
                Setor st = new Setor();
                st = m_lstSetorPenerimaan[e.RowIndex];
                frmSetorPenerimaan fSetor = new frmSetorPenerimaan();
                fSetor.Set(st);
                fSetor.ShowDialog();
            }
        }

        private void tabBAST_Click(object sender, EventArgs e)
        {

        }

        private void cmdTambahBAST_Click(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_iIDDInas = pIDSKPD;
            ctrlSPJFungsional1.SetDInas(m_iIDDInas);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridKontrak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridKontrak.Rows.Count)
            {
                if (e.ColumnIndex == 1)
                {
                    Kontrak oktr = new Kontrak();
                    oktr = m_lstKontrak[e.RowIndex];
                    frmKontrak fKontrak = new frmKontrak();
                    fKontrak.SetKontrak(oktr);
                    fKontrak.Show();
                }
            }
        }

        private void gridBAST_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridBAST.Rows.Count)
            {
                if (e.ColumnIndex == 1)
                {
                    BAST oBAST = new BAST();
                    oBAST = m_lstBAST[e.RowIndex];
                    frmBAST fBAST = new frmBAST();
                    fBAST.SetBAST(oBAST);
                    fBAST.Show();

                   

                }
            }
        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void gridPanjar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridPanjar.Rows.Count)
            {
                if (e.ColumnIndex == 1)
                {
                    Pengeluaran p = m_lstPanjar[e.RowIndex];
                    frmPengeluaran f = new frmPengeluaran();
                    f.SetPengeluaran(p);
                    f.Show();

                }
            }
        }

        private void cmdCetakBKU_Click(object sender, EventArgs e)
        {
            CetakBKU cetak = new CetakBKU();
            ParameterLaporanBKU parameter = new ParameterLaporanBKU();
               SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
                PemdaLogic oPemdaLogic = new PemdaLogic(GlobalVar.TahunAnggaran);
                SKPD oSKPD = oSKPDLogic.GetByID(m_iIDDInas, true );
                Pemda oPemda = new Pemda();
                SPP oSPP = new SPP();
                //SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                //oSPP = oSPPLogic.GetByID(m_NoUrut, true );
                oPemda = oPemdaLogic.Get();

            parameter.Skpd = oSKPD;
            parameter.Height = 8.5;
            parameter.Width= 14;
            parameter.SaldoAwal = txtSaldo.Text.FormatUangReportKeDecimal();
            parameter.Penerimaanlalu = txtPenerimaanLalu.Text.FormatUangReportKeDecimal();
            parameter.PenerimaanKini = txtPenerimaanKini.Text.FormatUangReportKeDecimal();
            parameter.PengeluaranKini = txtPengeluaranKini.Text.FormatUangReportKeDecimal();
            parameter.PengeluaranLalu = txtPenerimaanLalu.Text.FormatUangReportKeDecimal();
            parameter.Penerimaan = m_sJumlahPenerimaan;
            parameter.Pengeluaran = m_sJumlahPengeluaran;
            parameter.SaldoAkhir = txtSaldoAkhir.Text.Trim().FormatUangReportKeDecimal();
           
            parameter.PEMDA = oPemda;

            cetak.Cetak(false, "bku2.pdf", gridBKU, parameter);
        }

        private void menuBendahara1_OnSPJ()
        {
            DisplayTab(tabSPJ);
            splitContainer1.Panel2Collapsed = true;
            ctrlSPJFungsional1.Size= tabSPJ.Size;
            ctrlSPJFungsional1.SetDInas(m_iIDDInas);


            //// Get the location in this form's coordinate system.
            //Point form_pt = new Point(menuBendahara1.Width + tabSPJ.Left, tabSPJ.Top );

            //// Translate into the screen coordinate system.
            //Point screen_pt = this.PointToScreen(form_pt);

            //// Create and position the form.
            ////Form1 frm = new Form1();
            //frmSPJFungsional fSPJ = new frmSPJFungsional();
            //fSPJ.StartPosition = FormStartPosition.Manual;
            //fSPJ.Location = screen_pt;
            //fSPJ.Show();

            
            //fSPJ.Show();
        }

        private void menuBendahara1_OnKasTunai()
        {
            frmLaporanRegister fLap = new frmLaporanRegister();
            fLap.JenisLaporan(0);
            fLap.Show();

        }

        private void menuBendahara1_OnKasBank()
        {
            frmLaporanRegister fLap = new frmLaporanRegister();
            fLap.JenisLaporan(1);
            fLap.Show();
        }

        private void menuBendahara1_OnRegisterSPP()
        {
            frmLaporanRegister fLap = new frmLaporanRegister();
            fLap.JenisLaporan(3);
            fLap.Show();
        }

        private void menuBendahara1_Load(object sender, EventArgs e)
        {
            DisplayTab(tabSetorKasda);
        }

        private void menuBendahara1_OnRegisterSP2D()
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmLaporanRegister")
                {
                    frm.Focus();
                    bFormNameOpen = true;
                }
            }
            if (bFormNameOpen == false)
            {
                frmLaporanRegister fLap = new frmLaporanRegister();

                fLap.JenisLaporan(4);
                fLap.Show();
            }
        }

        private void menuBendahara1_OnKontrak()
        {
            DisplayTab(tabKontrak);

            //bool isOpened = false;
            //foreach (TabPage p in tabControl1.TabPages)
            //{
            //    if (p == tabKontrak)
            //    {
            //        isOpened = true;
            //        p.Focus();
            //        tabControl1.SelectedTab = p;
            //    }
            //}

            //if (isOpened==false)
            //    tabControl1.TabPages.Add(tabKontrak);
            
        }

        private void menuBendahara1_OnPanjar()
        {
           // tabControl1.TabPages.Add(tabPanjar);
            DisplayTab(tabPanjar);

        }

        private void menuBendahara1_OnKoreksi()
        {
            //tabControl1.TabPages.Add(tab)
           
        }

        private void menuBendahara1_OnSPP()
        {
            DisplayTab(tabSPP);

            //tabControl1.TabPages.Add(tabSPP);
        }

        private void menuBendahara1_OnSTS()
        {
            DisplayTab(tabSTS);
        }
        private void DisplayTab(TabPage t)
        {
            bool isOpened = false;
            foreach (TabPage p in tabControl1.TabPages)
            {
                if (p == t)
                {
                    isOpened = true;
                    p.Focus();
                    tabControl1.SelectedTab = p;
                }
            }

            if (isOpened == false)
            {
                tabControl1.TabPages.Add(t);
                t.Focus();
                tabControl1.SelectedTab = t;
            }

        }

        private void menuBendahara1_OnSKRSKPD()
        {
            DisplayTab(tabSKRSKPD);
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            //TabControl tc = (TabControl)sender;
            //Point p = e.Location;
            //int _tabWidth = 0;
            //_tabWidth = this.tabControl1.GetTabRect(tc.SelectedIndex).Width - (_imgHitArea.X);
            //Rectangle r = this.tabControl1.GetTabRect(tc.SelectedIndex);
            //r.Offset(_tabWidth, _imgHitArea.Y);
            //r.Width = 16;
            //r.Height = 16;
            //if (tabControl1.SelectedIndex >= 1)
            //{
            //    if (r.Contains(p))
            //    {
            //        TabPage TabP = (TabPage)tc.TabPages[tc.SelectedIndex];
            //        tc.TabPages.Remove(TabP);
            //    }
            //}
        }

        private void menuBendahara1_OnPengembalianBelanja()
        {
            DisplayTab(tabPengembalianBelannja);
            

        }

        private void menuBendahara1_OnKasBukuKartuKendali()
        {
            //DisplayTab()
        }

        private void menuBendahara1_OnBPK()
        {
            DisplayTab(tabBPK);
        }

        private void menuBendahara1_OnBAST()
        {
            DisplayTab(tabBAST);
        }

        private void menuBendahara1_OnBKUSP2D()
        {
            DisplayTab(tabSPP);
        }

        private void menuBendahara1_OnKasBukuPaanjar()
        {

        }

        private void menuBendahara1_OnSPM()
        {
            DisplayTab(tabSPP);
        }

        private void menuBendahara1_OnTrxBank()
        {
            DisplayTab(tabBank);
        }

        private void menuBendahara1_OnBKU()
        {
            DisplayTab(tabBKU);
          //  splitContainer1.Panel2Collapsed = true;
            //ctrlSPJFungsional1.Size= tabSPJ.Size;
        }

        private void tabBKU_Click(object sender, EventArgs e)
        {

        }

        private void cmdAddKontrak_Click(object sender, EventArgs e)
        {
            frmKontrak fKonntrak = new frmKontrak();
            fKonntrak.CreateNew();
            fKonntrak.ShowDialog();
            LoadKontrak();
            
        }

        private void gridSKRSKPD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdtambahSTS_Click(object sender, EventArgs e)
        {

        }

        private void cmdAddSBPK_Click(object sender, EventArgs e)
        {

        }

        private void gridPengembalian_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
