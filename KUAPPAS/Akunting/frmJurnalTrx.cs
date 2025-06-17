using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP.Bendahara;
using BP;
using BP.Akuntansi;
//using DTO;
using Formatting;
using DTO.Bendahara;
using DTO;
using DTO.Akuntansi;


namespace KUAPPAS.Akunting
{
    public partial class frmJurnalTrx : ChildForm
    {
        private int m_iIDDInas;//m_iSKPD;
        private ProsesJurnalLogic JurnalLogic ;

        private List<Pengeluaran> m_lstPengeluaran;
        private List<STS> m_lstSTS;
        private List<Setor> m_lstSetorPenerimaan;
        private List<BAST> m_lstBAST;
        private List<SPP> m_lstSPP;
        private List<Setor> m_lstSetor;
        private List<Koreksi> mlstKoreksi;
        private List<JurnalPajak> m_lstPajak;


        DataGridViewCellStyle _merah = new DataGridViewCellStyle();
        DataGridViewCellStyle _pink = new DataGridViewCellStyle();
        DataGridViewCellStyle _ijo = new DataGridViewCellStyle();
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();

        
        private List<NoUrutDanJenis> m_lstNoUrutSPP;




        public frmJurnalTrx()
        {
            InitializeComponent();
        }

        private void frmJurnalTrx_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            ctrlTanggalBulanVertikal1.TanggalAkhir = DateTime.Now.Date;

            ctrlPencarianSP2D.setGrid(ref gridSPP);
            ctrlPencarianSPJ.setGrid(ref gridBPK);
            ctrlPencarian1STS.setGrid(ref gridSTS);
            ctrlPencarian1SKR.setGrid(ref gridSKRSKPD);
            ctrlPencarian1SetorSTS.setGrid(ref gridSetorSTS);
            ctrlPencarian1BAST.setGrid(ref gridBAST);
            ctrlPencarian1PengembalianBelanja.setGrid(ref gridPengembalian);
            ctrlPencarian1Koreksi.setGrid(ref  gridKoreksi );
            ctrlPencarian1Pajak.setGrid(ref gridPajak);

            gridBAST.FormatHeader();
            gridSTS.FormatHeader();
            gridSKRSKPD.FormatHeader();
            gridPengembalian.FormatHeader();
            gridSTS.FormatHeader();
            gridSPP.FormatHeader();
            gridPajak.FormatHeader();
            gridKoreksi.FormatHeader();
            gridSetorSTS.FormatHeader();
            gridBPK.FormatHeader();

        }
        private void LoadPenerimaan()
        {
            try{

            m_iIDDInas = ctrlDinas1.GetID();
            DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
            DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
       
            STSLogic oLogic = new STSLogic((int)GlobalVar.TahunAnggaran);
             gridSTS.Rows.Clear();
             //GetByDinas
             
             m_lstSTS = oLogic.GetByDinasToJurnal(m_iIDDInas, tanggalAwal, tanggalAkhir, -1);//, E_JENIS_SETOR.E_SETOR_PAJAK);
             if (oLogic.IsError())
             {
                 MessageBox.Show(oLogic.LastError());
                 return;
             }
            if (m_lstSTS != null)
            {

                int i = 0;
                foreach (STS s in m_lstSTS)
                {
                    string[] row = { s.NoUrut.ToString(), "Detail", "false", s.TanggalSTS.ToString("dd MMM"), s.NoSTS,
                                       s.Keterangan, s.Penyetor, 
                                       s.Jumlah.ToRupiahInReport(), s.Jenis.ToString(),s.NoSKR.ToString(),s.Status.ToString() };
                    gridSTS.Rows.Add(row);
                    if (s.Status != 0)
                    {

                        gridSTS.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                    i++;

                }
            }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void LoadPenyetoranPenerimaan()
        {
            try
            {
                m_iIDDInas = ctrlDinas1.GetID();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
                SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
                m_lstSetorPenerimaan = new List<Setor>();
                gridSetorSTS.Rows.Clear();

                m_lstSetorPenerimaan = oLogic.GetByDinasUntukJurnal(m_iIDDInas, E_JENIS_SETOR.E_SETOR_STS, tanggalAwal, tanggalAkhir);

                if (m_lstSetorPenerimaan != null)
                {
                    int i = 0;
                    foreach (Setor s in m_lstSetorPenerimaan)
                    {
                        string[] row = { s.NoUrut.ToString(), "Detail", "false", s.dtBukuKas.ToString("dd MMM"), s.NoBukti, s.Keterangan, s.Jumlah.ToRupiahInReport() };
                        gridSetorSTS.Rows.Add(row);
                        if (s.Status != 0)
                        {

                            gridSetorSTS.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                        }
                        i++;

                    }
                }

                else
                {
                    if (oLogic.IsError() == true)
                    {
                        MessageBox.Show(oLogic.LastError());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadBAST()
        {
            try
            {
                m_iIDDInas = ctrlDinas1.GetID();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
                BASTLogic oLogic = new BASTLogic((int)GlobalVar.TahunAnggaran);
                gridBAST.Rows.Clear();
                m_lstBAST = new List<BAST>();
                m_lstBAST = oLogic.GetByIDDInasForJurnal(m_iIDDInas);

                if (m_lstBAST != null)
                {
                    m_lstBAST = m_lstBAST.FindAll(bast => bast.dtBAST >= tanggalAwal && bast.dtBAST <= tanggalAkhir);
                    
                }
                else
                {
                    MessageBox.Show("Gagal memanggil data BAST " + oLogic.LastError());
                    return;
                }
                if (m_lstBAST != null)
                {
                    int idx = 0;
                    foreach (BAST b in m_lstBAST)
                    {
                        string[] row = { b.NoUrut.ToString(), "Detail", "false", b.NoBAST, b.dtBAST.ToString("dd MMM"), b.Uraian, b.NOKontrak, b.NamaPihakKetiga, b.NoSP2D };
                        gridBAST.Rows.Add(row);

                        if (b.Status > 0)
                        {

                            gridBAST.Rows[idx].DefaultCellStyle.BackColor = Color.LightBlue;
                          

                        }
                        idx++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
               
        }
        private void LoadSP2D()
        {
            try { 
                m_iIDDInas = ctrlDinas1.GetID();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);

                p.LstStatus = new List<int>();

                p.Jenis = -1;

                p.IDDInas = m_iIDDInas;

                List<int> lstStatus = new List<int>();

                lstStatus.Add(4);
            
                p.Status = 4;
            
                p.LstStatus = lstStatus;
                p.OrderBy = " tSPP.dtBukukas, tSPP.inourut ";
                p.TanggalAwal = tanggalAwal;
                p.TanggalAkhir = tanggalAkhir;
                m_lstSPP = new List<SPP>();
                m_lstSPP = oLogic.GetSPPForJurnal(p);         
                gridSPP.Rows.Clear();
                int iRow = 0;
                foreach (SPP spp in m_lstSPP)
                {
                    //if (spp.NoBAST != "" )
                    //{
                    if (spp.NoSP2D.Contains("1016"))
                    {
                        Console.WriteLine(spp.NoSP2D);
                    }
                        string[] row = { spp.NoUrut.ToString(), 
                                       "Detail", 
                                       "false", 
                                       spp.NoSPP.Trim().Replace(Environment.NewLine ,"") +  "->" +"Tgl :" + spp.dtSPP.ToString("dd MMM"),
                                            spp.NoSPM.Trim().Replace(Environment.NewLine ,"") +  "->"+ "Tgl :" +  spp.dtSPM.ToString("dd MMM"), 
                                            spp.NoSP2D.Trim().Replace(Environment.NewLine ,"") +  "->" + "Tgl :" +   spp.dtTerbit.ToString("dd MMM"), "",
                                            spp.Keterangan, spp.Jumlah.ToRupiahInReport(),spp.Jenis.ToString() };


                        gridSPP.Rows.Add(row);

                        if (spp.Status != 0)
                        {

                            gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.LightBlue;
                        }

                        iRow++;
                    }


                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
               

        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {


            if (chkSKRSKPD.Checked == true)
            {
               
            }
            if (chkSTS.Checked == true)
            {
                LoadPenerimaan();
            }
            if (chkSetorKasda.Checked == true)
            {
                LoadPenyetoranPenerimaan();
            }
            if (chkBAST.Checked == true)
            {
                LoadBAST();
            }
            if (chkSP2D.Checked== true){
                LoadSP2D();

            }
            if (chkBelanjaSPJ.Checked == true)
            {
                LoadPengeluaran();

            }
            if (chkPengembalian.Checked == true)
            {
                LoadPengembalianBelanja();

            }
            if (chkKoreksi.Checked == true)
            {
                LoadKoreksi();
            }
            if (chkPajak.Checked == true)
            {
                LoadPajak();
            }
            MessageBox.Show("Data sudah selesai dipanggil..");

        }
        private void LoadPengeluaran()
        {
            try
            {
                m_iIDDInas = ctrlDinas1.GetID();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
                PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);

               
                //if (GlobalVar.gListPengeluaran.FindAll(x => x.IDDInas == m_iIDDInas).Count == 0)
                //{
                m_lstPengeluaran = new List<Pengeluaran>();
                ParameterBendahara pb = new ParameterBendahara(GlobalVar.TahunAnggaran);
                pb.IDDInas = m_iIDDInas;// ctrlSKPD1.GetID();
                pb.TanggalAwal = tanggalAwal;
                pb.TanggalAkhir = tanggalAkhir;
                int Jenis = -1;
                
                pb.Jenis = Jenis;;
                pb.LstJenis = new List<int>();
        
                pb.LstJenis.Add(3);
                pb.LstJenis.Add(4);
                pb.LstJenis.Add(5);
                pb.LstJenis.Add(6);
                pb.LstJenis.Add(7);

                m_lstPengeluaran = oLogic.GetForJurnal(pb);
                gridBPK.Rows.Clear();
                if (m_lstPengeluaran.Count > 0)
                {
                    int iRow = 0;
                    foreach (Pengeluaran p in m_lstPengeluaran)
                    {
                        if (p.Jenis == E_JENISPENGELUARAN.PENGELUARAN_LANGSUNG ||
                            p.Jenis == E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR ||
                                   p.Jenis == E_JENISPENGELUARAN.PENGELUARAN_BLUD ||
                            p.Jenis == E_JENISPENGELUARAN.PENGELUARAN_ADD ||
                             p.Jenis == E_JENISPENGELUARAN.PENGELUARAN_BOS)
                        {
                            string[] row = { p.NoUrut.ToString(), "Detail", "false", p.NoBukti, p.Tanggal.ToString("dd MMM"), p.Uraian, p.Penerima, p.Jumlah.ToRupiahInReport() };
                            gridBPK.Rows.Add(row);

                            if (p.Status != 0)
                            {

                                gridBPK.Rows[iRow].DefaultCellStyle.BackColor = Color.LightBlue;
                            }
                            iRow++;

                        
                        }
                        

                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void LoadPengembalianBelanja()
        {
            try
            {

                SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);

                m_lstSetor = new List<Setor>();
                gridPengembalian.Rows.Clear();
                m_iIDDInas = ctrlDinas1.GetID();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
                m_lstSetor = oLogic.GetByDinasForJurnal(m_iIDDInas, E_JENIS_SETOR.E_ALL, tanggalAwal, tanggalAkhir);
                int idx = 0;
                if (m_lstSetor != null)
                {
                    foreach (Setor s in m_lstSetor)
                    {

                        string[] row = { s.NoUrut.ToString(), 
                                           "Detail", 
                                           "false", 
                                           s.NoBukti, 
                                           s.dtBukuKas.ToString("dd MMM"), 
                                           s.Keterangan, 
                                           s.Jenis.ToString(), 
                                           s.NobuktiClient, 
                                           s.Jumlah.ToRupiahInReport() };
                        gridPengembalian.Rows.Add(row);
                        if (s.Status > 0)
                        {

                            gridPengembalian.Rows[idx].DefaultCellStyle.BackColor = Color.LightBlue;
                            

                        }
                        idx++;

                    }
                 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void LoadKoreksi()
        {
            try
            {

                KoreksiLogic oKoreksiLogic = new KoreksiLogic(GlobalVar.TahunAnggaran);
                mlstKoreksi = new List<Koreksi>();

                m_iIDDInas = ctrlDinas1.GetID();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;

                gridKoreksi.Rows.Clear();
                mlstKoreksi = oKoreksiLogic.GetByDinas(m_iIDDInas, GlobalVar.TahunAnggaran);

                if (mlstKoreksi != null)
                {
                    foreach (Koreksi k in mlstKoreksi)
                    {
                        string[] row = { k.NoUrut.ToString(), "Detail","false", k.NoBukti, k.DtKoreksi.ToString("dd MMM"), k.Uraian, k.NourutSPJUP > 0 ? "Sudah LOJ" : "Belum LPJ" };

                        gridKoreksi.Rows.Add(row);

                    }
              
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void LoadPajak()
        {
            try
            {

                gridPajak.Rows.Clear();
                JurnalLogic oLogic = new JurnalLogic(GlobalVar.TahunAnggaran);
                m_lstPajak = new List<JurnalPajak>();
                decimal JumlahPungut = 0;
                decimal JumlahSetor = 0;


                m_iIDDInas = ctrlDinas1.GetID();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;

                gridKoreksi.Rows.Clear();
                m_lstPajak = oLogic.GetPajakUntukJurnal(GlobalVar.TahunAnggaran, m_iIDDInas,
                    tanggalAwal, tanggalAkhir);
                int iRow = 0;
                if (m_lstPajak != null)
                {
                    foreach (JurnalPajak k in m_lstPajak)
                    {
                        string[] row = { k.NoUrut.ToString(), "Detail", "false",
                                           k.Kelompok.ToString(), 
                                           k.NoBukti, 
                                           k.Tanggal.ToTanggalIndonesia(),
                                           k.Keterangan,
                                           k.Pungut.ToRupiahInReport(),
                                               k.Setor.ToRupiahInReport()};

                        JumlahPungut = JumlahPungut+k.Pungut;
                        JumlahSetor = JumlahSetor+k.Setor;
                        gridPajak.Rows.Add(row);

                        if (k.Dijurnal> 1)
                        {

                            gridPajak.Rows[iRow].DefaultCellStyle.BackColor = Color.LightBlue;
                        }
                        iRow++;

                    }
                    txtPungut.Text = JumlahPungut.ToRupiahInReport();
                    txtSetor.Text = JumlahSetor.ToRupiahInReport();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void cmdProses_Click(object sender, EventArgs e)
        {
            if (chkSP2D.Checked == true)
            {

                
                ProsesJurnalSP2D();

               // MessageBox.Show("Selesai Penjurnalan SP2D");
            }
            if (chkBelanjaSPJ.Checked == true)
            {
                ProcessJurnalPanjar();
          //      MessageBox.Show("Selesai Penjurnalan SPJ");
            }

            if (chkPengembalian.Checked == true)
            {
                ProcessJurnalPengembalianBelannja();
              //  MessageBox.Show("Selesai Penjurnalan Pengembalian Belanja");
            }
            if (chkKoreksi.Checked == true)
            {
                ProcessJurnalKoreksi();
          //      MessageBox.Show("Selesai Penjurnalan Koreksi Belanja");
            }
            if (chkBAST.Checked == true)
            {
                ProcessJurnalBAST();
            //    MessageBox.Show("Selesai Penjurnalan BAST.");
            }

            if (chkSTS.Checked == true)
            {
                ProsesJurnalPenerimaan();
           //     MessageBox.Show("Selesai Penjurnalan Penerimaan.");
            }

            if (chkSetorKasda.Checked == true)
            {
                ProsesSetorPenerimaan();
           //     MessageBox.Show("Selesai Penjurnalan Penyetoran Penerimaan ke Kasda");
            }
            if (chkPajak.Checked == true)
            {


                ProsesJurnalPajak();

            }

            MessageBox.Show("Selesai Penjurnalan...");
        }

        private void ProsesSetorPenerimaan()
        {
            foreach (DataGridViewRow row in gridSetorSTS.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    bool bDipilih = Convert.ToBoolean(row.Cells[2].Value);
                    int jenis = 1;

                    if (bDipilih == true)
                    {
                        JurnalLogic.Jurnal(NoUrut.ToString(), JENIS_TABLE.TABLE_SETOR, jenis);



                    }
                }
            }
        }
        private void ProsesJurnalPajak()
        {
            foreach (DataGridViewRow row in gridPajak.Rows)
            {
                int pungutAtauSetoran = 0;
                if (row.Cells[0].Value != null)
                {
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    bool bDipilih = Convert.ToBoolean(row.Cells[2].Value);
                    pungutAtauSetoran= DataFormat.GetInteger(row.Cells[3].Value);
                    
                    int jenis = 1;
                    
                    if (bDipilih == true)
                    {

                        if (pungutAtauSetoran==1)
                        JurnalLogic.Jurnal(NoUrut.ToString(), JENIS_TABLE.TABLE_PANJAR, jenis,1);
                        else
                         JurnalLogic.Jurnal(NoUrut.ToString(), JENIS_TABLE.TABLE_SETOR, jenis,1);



                    }
                }
            }
        }
        private void ProsesJurnalPenerimaan()
        {
            foreach (DataGridViewRow row in gridSTS.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    bool bDipilih = Convert.ToBoolean(row.Cells[2].Value);
                    // Jenis
                    int jenis = DataFormat.GetInteger(row.Cells[8].Value);
                    
                    if (bDipilih == true)
                    {
                        JurnalLogic.Jurnal(NoUrut.ToString(), JENIS_TABLE.TABLE_STS,jenis );



                    }
                }
            }
        }

        private void ProcessJurnalBAST()
        {
            foreach (DataGridViewRow row in gridBAST.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    bool bDipilih = Convert.ToBoolean(row.Cells[2].Value);
                    if (bDipilih == true)
                    {
                        JurnalLogic.Jurnal(NoUrut.ToString(), JENIS_TABLE.TABLE_BAST);



                    }
                }
            }
        }
        private void ProcessJurnalKoreksi()
        {
            foreach (DataGridViewRow row in gridKoreksi.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    bool bDipilih = Convert.ToBoolean(row.Cells[2].Value);
                    if (bDipilih == true)
                    {
                        JurnalLogic.Jurnal(NoUrut.ToString(), JENIS_TABLE.TABLE_KOREKSI);



                    }
                }
            }
        }
        private void ProcessJurnalPanjar()
        {
            foreach (DataGridViewRow row in gridBPK.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    bool bDipilih = Convert.ToBoolean(row.Cells[2].Value);
                    if (bDipilih == true)
                    {
                        JurnalLogic.Jurnal(NoUrut.ToString(), JENIS_TABLE.TABLE_PANJAR);



                    }
                }
            }
        }
        private void ProsesJurnalSP2D()
        {
           

       
            long NoUrut =0;
            int jenis = 0 ;
            foreach (DataGridViewRow row in gridSPP.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    bool bDipilih = Convert.ToBoolean(row.Cells[2].Value);
                    if (bDipilih == true)
                    {
                        

                           NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                           jenis = DataFormat.GetInteger(row.Cells[9].Value);
                        
                           JurnalLogic.Jurnal(NoUrut.ToString(), JENIS_TABLE.TABLE_SPP, jenis);
                        
                    }
                }
            }



        }

        


        private void ProcessJurnalPengembalianBelannja()
        {

            foreach (DataGridViewRow row in gridPengembalian.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    bool bDipilih = Convert.ToBoolean(row.Cells[2].Value);
                    int jenisSetor = DataFormat.GetInteger(row.Cells[6].Value);
                    if (bDipilih == true)
                    {
                        JurnalLogic.Jurnal(NoUrut.ToString(), JENIS_TABLE.TABLE_SETOR, jenisSetor);



                    }
                }
            }

        }
        private void ctrlDinas1_Load(object sender, EventArgs e)
        {
            if (GlobalVar.Pengguna.SKPD == 0)
            {
                JurnalLogic = new ProsesJurnalLogic(GlobalVar.TahunAnggaran, 0);
            }
            else
            {
                JurnalLogic = new ProsesJurnalLogic(GlobalVar.TahunAnggaran, GlobalVar.Pengguna.SKPD);
            }

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_iIDDInas = pIDSKPD;
            JurnalLogic = new ProsesJurnalLogic(GlobalVar.TahunAnggaran, m_iIDDInas);
            BersihkanTampilanData();


        }
        private void BersihkanTampilanData()
        {
            gridBAST.Rows.Clear();
            gridBPK.Rows.Clear();
            gridKoreksi.Rows.Clear();
            gridPengembalian.Rows.Clear();
            gridSetorSTS.Rows.Clear();
            gridSKRSKPD.Rows.Clear();
            gridSPP.Rows.Clear();
            gridSTS.Rows.Clear();
           
        }
        private void gridSPP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                if (e.RowIndex >= 0 && e.RowIndex < gridSPP.Rows.Count)
                {
                    DataGridViewRow row = gridSPP.Rows[e.RowIndex];
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    tabJurnalRekening1.NoUrutSumber = NoUrut;

                }
            }
        }

        private void cmdPilihSemuaSP2D_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in gridSPP.Rows)
            {
                
                    if (row.Cells[1].Value != null)
                    {
                        row.Cells[1].Value = true;
                    }
                

            }
        }

        private void chkPilihSP2D_CheckedChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in gridSPP.Rows)
            {

                if (row.Cells[1].Value != null)
                {
                    row.Cells[2].Value = chkPilihSP2D.Checked;
                }


            }
        }

        private void gridSKRSKPD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkilihSemuaSPJ_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridBPK.Rows)
            {

                if (row.Cells[1].Value != null)
                {
                    row.Cells[2].Value = chkilihSemuaSPJ.Checked;
                }


            }
        }

        private void gridBPK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                if (e.RowIndex >= 0 && e.RowIndex < gridBPK.Rows.Count)
                {
                    DataGridViewRow row = gridBPK.Rows[e.RowIndex];
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    tabJurnalRekening1.Pajak = 0;
                    tabJurnalRekening1.NoUrutSumber = NoUrut;

                }
            }
        }

        private void chkPengembalianBelanja_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridPengembalian.Rows)
            {

                if (row.Cells[1].Value != null)
                {
                    row.Cells[2].Value = chkPengembalianBelanja.Checked;
                }


            }
        }

        private void gridPengembalian_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                if (e.RowIndex >= 0 && e.RowIndex < gridPengembalian.Rows.Count)
                {
                    DataGridViewRow row = gridPengembalian.Rows[e.RowIndex];
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    tabJurnalRekening1.NoUrutSumber = NoUrut;

                }
            }
        }

        private void chkPilihSemuaKoreksi_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridKoreksi.Rows)
            {

                if (row.Cells[1].Value != null)
                {
                    row.Cells[2].Value = chkPilihSemuaKoreksi.Checked;
                }


            }

        }

        private void gridKoreksi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                if (e.RowIndex >= 0 && e.RowIndex < gridKoreksi.Rows.Count)
                {
                    DataGridViewRow row = gridKoreksi.Rows[e.RowIndex];
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    tabJurnalRekening1.NoUrutSumber = NoUrut;

                }
            }
        }

        private void chkPiliSemuaBAST_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridBAST.Rows)
            {

                if (row.Cells[1].Value != null)
                {
                    row.Cells[2].Value = chkPiliSemuaBAST.Checked;
                }


            }
        }

        private void gridBAST_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                if (e.RowIndex >= 0 && e.RowIndex < gridBAST.Rows.Count)
                {
                    DataGridViewRow row = gridBAST.Rows[e.RowIndex];
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    tabJurnalRekening1.NoUrutSumber = NoUrut;

                }
            }
        }

        private void gridSTS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                if (e.RowIndex >= 0 && e.RowIndex < gridSTS.Rows.Count)
                {
                    DataGridViewRow row = gridSTS.Rows[e.RowIndex];
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    tabJurnalRekening1.NoUrutSumber = NoUrut;

                }
            }
        }

        private void chkPiliSemuaSTS_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridSTS.Rows)
            {

                if (row.Cells[1].Value != null)
                {
                    row.Cells[2].Value = chkPiliSemuaSTS.Checked;
                }


            }
        }

        private void chkPiliSemuaPenyetorankeKasda_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridSetorSTS.Rows)
            {

                if (row.Cells[1].Value != null)
                {
                    row.Cells[2].Value = chkPiliSemuaPenyetorankeKasda.Checked;
                }


            }


            
        }

        private void gridSetorSTS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                if (e.RowIndex >= 0 && e.RowIndex < gridSetorSTS.Rows.Count)
                {
                    DataGridViewRow row = gridSetorSTS.Rows[e.RowIndex];
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    tabJurnalRekening1.NoUrutSumber = NoUrut;

                }
            }

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_DoubleClick(object sender, EventArgs e)
        {
            gridSPP.Columns[0].Visible = true;

        }

        private void bcSP2D_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            List<NoUrutDanJenis> lst = e.Argument as List<NoUrutDanJenis>;

            int i = 0;
            foreach (NoUrutDanJenis nj in lst)
            {
                JurnalLogic.Jurnal(nj.NoUrut.ToString(), JENIS_TABLE.TABLE_SPP, nj.jenis);
                backgroundWorker.ReportProgress((i * 100) / lst.Count);
                i++;

            }
            


        }

        private void bcSP2D_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbSP2D.Value = e.ProgressPercentage;

        }

        private void cmdBelumJurnalSP2D_Click(object sender, EventArgs e)
        {
            
            gridSPP.Rows.Clear();
            foreach (SPP spp in m_lstSPP)
            {
                if (spp.Status == 0)
                {

                    string[] row = { spp.NoUrut.ToString(), 
                                       "Detail", 
                                       "false", 
                                       spp.NoSPP.Trim().Replace(Environment.NewLine ,"") +  "->" +"Tgl :" + spp.dtSPP.ToString("dd MMM"),
                                            spp.NoSPM.Trim().Replace(Environment.NewLine ,"") +  "->"+ "Tgl :" +  spp.dtSPM.ToString("dd MMM"), 
                                            spp.NoSP2D.Trim().Replace(Environment.NewLine ,"") +  "->" + "Tgl :" +   spp.dtTerbit.ToString("dd MMM"), "",
                                            spp.Keterangan, spp.Jumlah.ToRupiahInReport(),spp.Jenis.ToString() };


                    gridSPP.Rows.Add(row);


                }
         }


        }

        private void cmdBelumJurnalSPJ_Click(object sender, EventArgs e)
        {
            gridBPK.Rows.Clear();
            if (m_lstPengeluaran.Count > 0)
            {
                int iRow = 0;
                foreach (Pengeluaran p in m_lstPengeluaran)
                {
                    if (p.Status == 0)
                    {
                        if (p.Jenis == E_JENISPENGELUARAN.PENGELUARAN_LANGSUNG || p.Jenis == E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR)
                        {
                            string[] row = { p.NoUrut.ToString(), "Detail", "false", p.NoBukti, p.Tanggal.ToString("dd MMM"), p.Uraian, p.Penerima, p.Jumlah.ToRupiahInReport() };
                            gridBPK.Rows.Add(row);

                           


                        }

                    }

                }
            }

        }

        private void cmdBelumJurnalSTS_Click(object sender, EventArgs e)
        {
            gridSTS.Rows.Clear();
            if (m_lstSTS != null)
            {

                foreach (STS s in m_lstSTS)
                {
                    if (s.Status == 0)
                    {
                        string[] row = { s.NoUrut.ToString(), "Detail", "false", s.TanggalSTS.ToString("dd MMM"), s.NoSTS,
                                       s.Keterangan, s.Penyetor, 
                                       s.Jumlah.ToRupiahInReport(), s.Jenis.ToString(),s.NoSKR.ToString(),s.Status.ToString() };
                        gridSTS.Rows.Add(row);
                    }

                }
            }

        }

        private void cmdPosting_Click(object sender, EventArgs e)
        {
            try
            {
                int iddinas = 0;
                if (chkSemuaDinas.Checked == false)
                    iddinas = ctrlDinas1.GetID();

                if (iddinas == 0)
                {
                    MessageBox.Show("Belum Pilih  OPD");
                    return;
                }
                ProsesJurnalLogic JurnalLogic = new ProsesJurnalLogic(GlobalVar.TahunAnggaran, iddinas);
                if (JurnalLogic.Posting() == true)
                {
                    MessageBox.Show("Proses Posting sudah selesai ");

                }
                else
                {
                    MessageBox.Show("Proses Posting Gagal " + JurnalLogic.LastError ());

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void tabPage9_Click(object sender, EventArgs e)
        {

        }

        private void gridPajak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                if (e.RowIndex >= 0 && e.RowIndex < gridPajak.Rows.Count)
                {
                    DataGridViewRow row = gridPajak.Rows[e.RowIndex];
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    tabJurnalRekening1.Pajak = 1;
                    tabJurnalRekening1.NoUrutSumber = NoUrut;

                }
            }
        }

        private void chkPiliSemuaPajak_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridPajak.Rows)
            {

                if (row.Cells[1].Value != null)
                {
                    row.Cells[2].Value = chkPiliSemuaPajak.Checked;
                }


            }
        }

        private void tabJurnalRekening1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void ctrlPencarian1STS_Load(object sender, EventArgs e)
        {

        }

        private void cmdJurnalUtangTahunLalu_Click(object sender, EventArgs e)
        {
            long NoUrut = 0;
            int jenis = 0;
            foreach (DataGridViewRow row in gridSPP.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    bool bDipilih = Convert.ToBoolean(row.Cells[2].Value);
                    if (bDipilih == true)
                    {


                        NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                        jenis = DataFormat.GetInteger(row.Cells[9].Value);

                        JurnalLogic.Jurnal(NoUrut.ToString(), JENIS_TABLE.TABLE_SPP, jenis,0,0,true);

                    }
                }
            }
        }
       

    }
   
}
