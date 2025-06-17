using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using DTO.Bendahara;

using BP;
using BP.Bendahara;
using Formatting;



namespace KUAPPAS.Bendahara
{
    public partial class frmPengeluaran : Form
    {
        private int m_IDDINas;
        private int m_iKodeUk;
        private long m_iNoUrut;
        private int m_iJenisBelanja;
        private int m_IDUrusan;
        private int m_IDProgram;
        private int m_IDKegiatan;
        private long m_IDSubKegiatan;
        private int m_UnitAnggaran;
        private int m_iTahapANggaaran;
        private E_JENISPENGELUARAN m_iJenisPengeluaran;
        private long m_iNoUrutPanjar;
        private bool m_bNew;
        private Pengeluaran oPengeluaran;
        private List<BKU> m_lstBKU;
        private int m_iStatus;
        private long m_iNoUrutSPP;
        public frmPengeluaran()
        {
            InitializeComponent();
            m_iNoUrut = 0;
            m_IDDINas = 0;
            m_iJenisBelanja = 0;
            m_IDUrusan = 0;
            m_IDProgram = 0;
            m_IDKegiatan = 0;
            m_IDSubKegiatan = 0;
            m_iTahapANggaaran = 0;
            m_iNoUrutPanjar = 0;
            m_bNew = false;
            m_lstBKU= new List<BKU> ();
            m_iStatus = 0;
            m_iJenisBelanja = 1;
        }

        private void frmPengeluaran_Load(object sender, EventArgs e)
        {
            if (m_iNoUrut==0)
             ctrlDinas1.Create();

            m_IDDINas=ctrlDinas1.GetIDSKPD ();
            m_iKodeUk = ctrlDinas1.GetKodeUK();

            
            ctrlRekeningKegiatan1.Keperluan = 2;

            
           // ctrlTanggal1.Tanggal = nowDay;
                       
            //ctrlProgramKegiatan1.SetSumber(1);
            //if (m_iJenisPengeluaran == 4)
            //{
            //    ctrlPanjar1.Create(m_IDDINas, m_iKodeUk, 1, ctrlTanggal1.Tanggal);
            //}
            //else
            //{
                ctrlProgramKegiatan1.SetIDinas(m_IDDINas, m_iKodeUk);
            //}
 




        }

        private void chkUPGU_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUPGU.Checked == true)
            {
                ctrlSPP1.Visible = false;
                ctrlProgramKegiatan1.SetIDinas(m_IDDINas,m_iKodeUk);
                ctrlProgramKegiatan1.SetSumber(1);

     

                ctrlProgramKegiatan1.CreateUrusan();
                
                m_iJenisBelanja = 1;
                ctrlSPP1.Visible = false;
                lblTU.Visible = true;

            }
        }

        private void chkTU_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTU.Checked == true)
            {
                ctrlSPP1.Visible = true;
                m_iJenisBelanja=2;
                ctrlProgramKegiatan1.SetSumber(3);
                
                ctrlSPP1.Create(m_IDDINas,m_iKodeUk, 2, 4);
                ctrlSPP1.Visible = true;
                lblTU.Visible = true;

            }
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_IDDINas = pIDSKPD;
            m_iKodeUk = pIDUK;
            m_iTahapANggaaran = ctrlDinas1.GetTahapAnggaran();
            if (ctrlDinas1.Unit != null)
                m_UnitAnggaran = ctrlDinas1.Unit.UntAnggaran;
            else
                m_UnitAnggaran = 0;
            if (m_iJenisPengeluaran == E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR ||
                m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR )
            {
                ctrlPanjar1.CreateUntukDIpertanggungjawabkan(m_IDDINas);
            }
            else
            {
                ctrlProgramKegiatan1.SetIDinas(m_IDDINas, m_UnitAnggaran);
            }
           
        }

        private void ctrlSPP1_OnChanged(long pID)
        {
            try
            {
                m_iNoUrutSPP = pID;
                SPPLogic SPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                SPP oSPP = new SPP();
                oSPP = SPPLogic.GetByID(pID);
                if (oSPP != null)
                {
                    if (oSPP.Rekenings != null)
                    {
                        if (oSPP.Rekenings.Count > 0)
                        {

                            m_IDUrusan = oSPP.Rekenings[0].IDUrusan;
                            m_IDProgram = oSPP.Rekenings[0].IDProgram;
                            m_IDKegiatan = oSPP.Rekenings[0].IDKegiatan;
                            m_IDSubKegiatan = oSPP.Rekenings[0].IDSubKegiatan;
                            ctrlProgramKegiatan1.SetNoUrutSP2D( pID);
                            ctrlProgramKegiatan1.SetValue(oSPP.IDDInas, oSPP.KodeUK, m_IDUrusan, m_IDProgram, m_IDKegiatan, m_IDSubKegiatan);
                            ctrlRekeningKegiatan1.SetProgramKegiatan(oSPP.IDDInas, oSPP.KodeUK, m_IDUrusan, m_IDProgram, m_IDKegiatan, 2, 0, m_IDSubKegiatan);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        public bool SetPengeluaran(Pengeluaran p, bool dariPanjar= false)
        {
            try
            {
                if (dariPanjar == false)
                {
                    // ctrlPotongan2.CreateUntukSPJ();

                    PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahapAnggaran);
                    ctrlDinas1.Create();



                    if (p == null)
                        return false;

                    if (p.Jenis == E_JENISPENGELUARAN.PENGELUARAN_LANGSUNG)
                    {
                        ctrlPanjar1.Visible = false;
                    }
                    
                    ctrlDinas1.SetID(p.IDDInas, p.KodeUK);

                    m_IDDINas = p.IDDInas;
                    m_iKodeUk = p.KodeUK;
                    ctrlTanggal1.Tanggal = p.Tanggal;
                    if (p.Jenis == E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR||
                        m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR)
                    {
                        ctrlPanjar1.Visible = true;
                        ctrlPanjar1.CreateUntukDIpertanggungjawabkan(m_IDDINas);
                        ctrlPanjar1.SetID(p.NoReferensi);
                    }

                    txtNoBukti.Text = p.NoBukti;
                    ctrlTanggal1.Tanggal = p.Tanggal;

                    m_iNoUrut = p.NoUrut;
                    m_iJenisPengeluaran = p.Jenis;
                    m_iJenisBelanja = p.JenisBelanja;

                    m_UnitAnggaran = p.UnitAnggaran;
                    txtJumlahPanjar.Text = p.Jumlah.ToRupiahInReport();

                    chkUPGU.Checked = p.JenisBelanja <= 1 ? true : false;
                    chkTU.Checked = p.JenisBelanja == 2 ? true : false;
  
                    if (m_bNew == false)
                    {
                        if (m_iJenisPengeluaran == E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR ||
                            m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR)
                        {
                            ctrlPanjar1.Visible = true;
                            ctrlPanjar1.CreateUntukDIpertanggungjawabkan(m_IDDINas);
                            ctrlPanjar1.SetID(p.NoReferensi);
                            lblPanjar.Visible = true;
                        }
                        else
                        {
                            ctrlPanjar1.Visible = false;
                            lblPanjar.Visible = false;
                            if (p.JenisBelanja == 2)
                            {
                                ctrlSPP1.Create(m_IDDINas,m_iKodeUk, 2, 4);
                                ctrlSPP1.SetID(p.NoUrutSPP);
                        
                           
                            }
                        }
                    }
                }


                ctrlProgramKegiatan1.SetIDinas(m_IDDINas, m_UnitAnggaran);
                ctrlProgramKegiatan1.SetSumber(1);

                if (p.JenisBelanja == 2)
                {

                    ctrlProgramKegiatan1.SetSumber(3);
                    ctrlProgramKegiatan1.SetNoUrutSP2D(p.NoUrutSPP);
                    ctrlProgramKegiatan1.CreateUrusan();

                }
                else
                {

                    ctrlProgramKegiatan1.SetSumber(1);
                    ctrlProgramKegiatan1.CreateUrusan();

                }
                if (p.JenisBelanja <= 1)
                {
                    ctrlProgramKegiatan1.SetValue(m_IDDINas, m_UnitAnggaran, p.IDUrusan, p.IDProgram, p.IDKegiatan, p.IDSUbKegiatan);//IDSUbKegiatan);
                    if (dariPanjar == false)
                    {
                        txtKeterangan.Text = p.Uraian;
                        txtPenerima.Text = p.Penerima;

                    }
                }
                else
                {
                    ctrlProgramKegiatan1.SetValueFromSP2D(m_IDDINas, m_UnitAnggaran, p.IDUrusan, p.IDProgram, p.IDKegiatan, p.IDSUbKegiatan,p.NoUrutSPP);//IDSUbKegiatan);
                    if (dariPanjar == false)
                    {
                        txtKeterangan.Text = p.Uraian;
                        txtPenerima.Text = p.Penerima;

                    }
                }
                ctrlRekeningKegiatan1.Clear();
                
                DateTime d = DateTime.Now.Date;
                ctrlVia1.SetBank(p.IDBank);
                if (p.JenisBelanja < 2)
                {
                    if (ctrlRekeningKegiatan1.CreateDariAnggaranKas(m_IDDINas, p.IDProgram, p.IDKegiatan, p.IDSUbKegiatan, d, p.tahap, p.UnitAnggaran) == true)
                    {

                        decimal cJumlah = ctrlRekeningKegiatan1.CreatePanjar(p.NoUrut);
                        txtJumlah.Text = cJumlah.ToRupiahInReport();
                    }
                }
                else
                {
                    if (ctrlRekeningKegiatan1.CreateDariSP2DTU(m_IDDINas,  p.IDSUbKegiatan, p.NoUrutSPP,p.Tanggal, p.UnitAnggaran) == true)
                    {

                        decimal cJumlah = ctrlRekeningKegiatan1.CreatePanjar(p.NoUrut);
                        txtJumlah.Text = cJumlah.ToRupiahInReport();
                    }
                }

                // Set BKU
            //    m_lstBKU = GlobalVar.gListBKU.FindAll(x => x.NourutSumber == m_iNoUrut);
                txtPenerima.Text = p.Penerima;
                ctrlFooter1.WaktuBuat = p.tcrt;
                ctrlFooter1.IDCrt = p.idcrt;
                m_iStatus = p.Status;
                if (m_iStatus > 0)
                {
                    ctrlNavigation1.AllowDelete = false;
                }
                decimal JumlahPotongan = 0L;
                JumlahPotongan=ctrlPotongan2.SetNoUrutSPJ(m_iNoUrut);
                txtJumlahPotongan.Text = JumlahPotongan.ToRupiahInReport();
                m_bNew = false;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        private void ctrlPanjar1_OnChanged(long pID)
        {
            Pengeluaran p = new Pengeluaran();
            p = ctrlPanjar1.GetPengeluaran(pID);
            if (p != null)
            {
                txtKeteranganPanjar.Text = p.Uraian + " ( " + p.Jumlah.ToRupiahInReport() + ")";
                SetPengeluaran(p, true);

            }
            else txtKeteranganPanjar.Text = "";
        }

        //private void ctrlProgramKegiatan1_OnChanged(int pIDurusan, int pIDProgram, int  pIDKegiaan, int pIDSubKegiatan)
        //{
           
        //    m_IDProgram= pIDProgram;
        //    m_IDUrusan = pIDurusan;
        //    m_IDKegiatan= pIDKegiaan;
        //  //  ctrlRekeningKegiatan1.SetProgramKegiatan(m_IDDINas,m_I)
        //}

        private void ctrlProgramKegiatan1_OnChanged(int pIDurusan, int pIDProgram, int pIDKegiaan, long pIDSubKegiatan)
        {
            m_IDProgram = pIDProgram;
            m_IDUrusan = pIDurusan;
            m_IDKegiatan = pIDKegiaan;
            m_IDSubKegiatan = pIDSubKegiatan;

            if (m_iJenisPengeluaran > E_JENISPENGELUARAN.PENGELUARAN_PANJAR)
            {
                ctrlRekeningKegiatan1.Clear();
                if (m_iJenisBelanja < 2)
                {
                    m_iTahapANggaaran = ctrlDinas1.GetTahapAnggaran();


                    ctrlRekeningKegiatan1.CreateDariAnggaranKas(m_IDDINas, m_IDProgram, m_IDKegiatan, m_IDSubKegiatan, ctrlTanggal1.Tanggal, m_iTahapANggaaran, m_UnitAnggaran);
                }
                else
                {
                    long nourut = ctrlSPP1.GetID();
                    if (nourut>0)
                    ctrlRekeningKegiatan1.CreateDariSP2DTU(m_IDDINas, m_IDSubKegiatan, ctrlSPP1.GetID() ,ctrlTanggal1.Tanggal, m_UnitAnggaran);
                }
            }

        }

        private void txtKeteranganPanjar_TextChanged(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private bool CekInput()
        {
            if (GlobalVar.Pengguna.Status == 2 )
            {
                MessageBox.Show("Pengguna Tidak bisa melakukan Transaksi");
                return false;

            }

            if (ctrlDinas1.GetID() == 0)
            {
                MessageBox.Show("Belum Pilih DInas");
                return false;

            }
             if (ctrlDinas1.GetID() > 0 && ctrlDinas1.WithUnitKerja()== true && ctrlDinas1.GetKodeUK ()==0)
            {
                MessageBox.Show("Belum Pilih Unit..");
                return false;

            }
             if (txtNoBukti.Text.Trim().Length ==0)
             {
                 MessageBox.Show("Belum mengisi No Bukti");
                 return false;

             }
             if (ctrlTanggal1.Tanggal.Year != GlobalVar.TahunAnggaran)
             {
                 MessageBox.Show("Tanggal salah Tahun ");
                 return false;
             }
             if (m_IDUrusan == 0 || m_IDProgram == 0 || m_IDKegiatan == 0 || m_IDSubKegiatan == 0)
             {
                 MessageBox.Show("Belum Piilih Program/Kegiatan/SubKegiatan...");
                 return false;
             }
             //if (m_IDProgram / 100 != m_IDUrusan)
             //{
             //    MessageBox.Show("Salah Pengkodean Program/Urusan. Sila Hubungi Admin Support.");
             //}
             if (m_IDKegiatan / 1000 != m_IDProgram)
             {
                 MessageBox.Show("Salah Pengkodean Program/Kegiatan. Sila Hubungi Admin Support.");
             }

             //if (m_IDSubKegiatan / 10000 != Convert.ToInt64(m_IDKegiatan))
             //{
             //    MessageBox.Show("Salah Pengkodean Kegiatan/SubKegiatan. Sila Hubungi Admin Support.");
             //}

             if (txtJumlah.Text.Trim().FormatUangReportKeDecimal() == 0 &&  m_iJenisPengeluaran > E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR)
             {
                 MessageBox.Show("Belum mengisi Nilai Belanja..");
                 return false;
             }
             if (txtJumlahPanjar.Text.Trim().FormatUangReportKeDecimal() == 0
                 && (m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGELUARAN_PANJAR|| 
                 m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR))
             {
                 MessageBox.Show("Belum mengisi Nilai Panjar..");
                 return false;
             }
            

             if (m_iJenisBelanja <= 1)
             {

             }
             if (m_iJenisBelanja == 2)
             {
                 if (ctrlSPP1.GetID() == 0)
                 {
                     MessageBox.Show("Belum pilih SP2D TU..");
                     return false;

                 }
             }


            return true;
            
        }
        public void SetNew()
        {
            ctrlNavigation1.SetNew();
        }
        public E_JENISPENGELUARAN Jenis
        {
            
            set
            {
                m_iJenisPengeluaran= value;
                switch (value)
                {
                    case E_JENISPENGELUARAN.PENGELUARAN_PANJAR:
                        panelPanjar.Visible = false;
                        lblPanjar.Visible = false;
                        panelProgram.Top = panelPanjar.Top;
                        TabRekening.Visible = false;
                        txtJumlahPanjar.Visible = true;
                        lblJumlahPanjar.Visible = true;
                        m_iJenisPengeluaran = E_JENISPENGELUARAN.PENGELUARAN_PANJAR;
                        panelJenisBelanja.Visible = false;
                        this.Text = "Pengeluaran Panjar";


                        break;
                    case E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR:
                        panelPanjar.Visible = true;
                        lblPanjar.Visible = true;
                        panelProgram.Top = panelPanjar.Height + panelPanjar.Top;
                        panelProgram.Visible = true;
                        TabRekening.Visible = false;
                        txtJumlahPanjar.Visible = true;
                        lblJumlahPanjar.Visible = true;
                        m_iJenisPengeluaran = E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR;
                        panelJenisBelanja.Visible = false;
                        lblJumlahPanjar.Text = "Jumlah Pengembalian";
                        this.Text = "Pengembbalian Panjar";


                        break;
                    case E_JENISPENGELUARAN.PENGELUARAN_LANGSUNG:
                        panelPanjar.Visible = false;
                        lblPanjar.Text ="Jenis Belanja";
                        ctrlPanjar1.Visible = false;
                        lblPanjar.Visible = false;
                        txtJumlahPanjar.Visible = false  ;
                        lblJumlahPanjar.Visible = false;
                        TabRekening.Visible = true ;
                        txtJumlahPanjar.Visible = false  ;
                        lblJumlahPanjar.Visible = false;
                        panelJenisBelanja.Visible = true;
                        panelJenisBelanja.Left = panelPanjar.Left;
                        this.Text = "Pengeluaran Belanja/SPJ/Bukti Pengeluaran Kas.";
                        ctrlPotongan2.CreateUntukSPJ();

                        break;
                    case E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR:
                        panelPanjar.Visible = true;
                        lblPanjar.Visible = true;
                        panelProgram.Top = panelPanjar.Height +  panelPanjar.Top;
                        TabRekening.Visible = true ;
                        txtJumlahPanjar.Visible = false  ;
                        lblJumlahPanjar.Visible = false;
                       
                        panelJenisBelanja.Visible = false ;
                        this.Text = "Pertanggungjawaban Panjar.";
                        ctrlPotongan2.CreateUntukSPJ();
                        break;
                    case E_JENISPENGELUARAN.PENGELUARAN_ADD:
                        panelPanjar.Visible = false;
                        lblPanjar.Text = "Jenis Belanja";
                        ctrlPanjar1.Visible = false;
                        lblPanjar.Visible = false;
                        txtJumlahPanjar.Visible = false;
                        lblJumlahPanjar.Visible = false;
                        TabRekening.Visible = true;
                        txtJumlahPanjar.Visible = false;
                        lblJumlahPanjar.Visible = false;
                        panelJenisBelanja.Visible = false;
                        panelJenisBelanja.Left = panelPanjar.Left;
                        this.Text = "Pengeluaran ADD melalui KPPN .";
                        ctrlPotongan2.CreateUntukSPJ();

                        break;
                    case E_JENISPENGELUARAN.PENGELUARAN_BLUD:
                        panelPanjar.Visible = false;
                        lblPanjar.Text = "Jenis Belanja";
                        ctrlPanjar1.Visible = false;
                        lblPanjar.Visible = false;
                        txtJumlahPanjar.Visible = false;
                        lblJumlahPanjar.Visible = false;
                        TabRekening.Visible = true;
                        txtJumlahPanjar.Visible = false;
                        lblJumlahPanjar.Visible = false;
                        panelJenisBelanja.Visible = false;
                        panelJenisBelanja.Left = panelPanjar.Left;
                        this.Text = "Pengeluaran ADD melalui KPPN .";
                        ctrlPotongan2.CreateUntukSPJ();

                        break;
                    case E_JENISPENGELUARAN.PENGELUARAN_BOS:
                        panelPanjar.Visible = false;
                        lblPanjar.Text = "Jenis Belanja";
                        ctrlPanjar1.Visible = false;
                        lblPanjar.Visible = false;
                        txtJumlahPanjar.Visible = false;
                        lblJumlahPanjar.Visible = false;
                        TabRekening.Visible = true;
                        txtJumlahPanjar.Visible = false;
                        lblJumlahPanjar.Visible = false;
                        panelJenisBelanja.Visible = false;
                        panelJenisBelanja.Left = panelPanjar.Left;
                        this.Text = "Pengeluaran ADD melalui KPPN .";
                        ctrlPotongan2.CreateUntukSPJ();

                        break;

                }

            }
            get { return m_iJenisPengeluaran; }


        }

        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage ret = new EventResponseMessage();
            try
            {

                m_IDUrusan = ctrlProgramKegiatan1.IdUrusan;
                m_IDProgram = ctrlProgramKegiatan1.IdProgram;
                m_IDKegiatan = ctrlProgramKegiatan1.IdKegiatan;
                m_IDSubKegiatan = ctrlProgramKegiatan1.IdSubKegiatan;



                if (CekInput() == false)
                {
                    ret.ResponseStatus = false;
                    return ret;
                }


                Pengeluaran pengeluaran = new Pengeluaran();
                PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
                pengeluaran.Tahun = GlobalVar.TahunAnggaran;
                pengeluaran.NoUrut = m_iNoUrut;
                pengeluaran.IDDInas = m_IDDINas;
                pengeluaran.KodeUK = m_iKodeUk;
                pengeluaran.IDUrusan = m_IDUrusan;
                pengeluaran.IDProgram = m_IDProgram;
                pengeluaran.IDKegiatan = m_IDKegiatan;
                pengeluaran.IDSUbKegiatan = m_IDSubKegiatan;

                pengeluaran.KodeUrusan = m_IDDINas.ToString().ToKodeUrusan();
                pengeluaran.Kodekategori = m_IDDINas.ToString().ToKodeKategori();
                pengeluaran.KodeSKPD = m_IDDINas.ToString().ToKodeSKPD();

                pengeluaran.KodekategoriPelaksana = m_IDUrusan.ToString().ToKodeKategoriPelaksana();
                pengeluaran.KodeurusanPelaksana = m_IDUrusan.ToString().ToKodeUrusanPelaksana();
                pengeluaran.KodeProgram = m_IDProgram.ToString().ToKodeProgram();
                pengeluaran.Kodekegiatan = m_IDKegiatan.ToString().ToKodeKegiatan();
                pengeluaran.KodeSubKegiatan = m_IDSubKegiatan.ToString().ToKodeSubKegiatan();
                pengeluaran.tahap = m_iTahapANggaaran;
                pengeluaran.Jenis = (E_JENISPENGELUARAN)m_iJenisPengeluaran;


                pengeluaran.JenisBelanja = m_iJenisBelanja;

                pengeluaran.IDBank = ctrlVia1.Bank;
                pengeluaran.Tanggal = ctrlTanggal1.Tanggal;
                pengeluaran.Uraian = txtKeterangan.Text;
                pengeluaran.Penerima = txtPenerima.Text;
              
                pengeluaran.NoBukti = txtNoBukti.Text;
                pengeluaran.NoUrutSPP = ctrlSPP1.GetID();
                pengeluaran.IDBank = ctrlVia1.IsBank();
                pengeluaran.NoPungut = txtNoBuktiPungut.Text;
                pengeluaran.UnitAnggaran = ctrlDinas1.UnitAnggaran;
                if (m_iJenisPengeluaran == E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR ||
                    m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR )
                {
                    pengeluaran.NoReferensi = ctrlPanjar1.GetID();
                    pengeluaran.Debet = 1;
                    pengeluaran.JumlahDikembalikan = ctrlPanjar1.Jumlah;
         

                }
                else
                {
                    pengeluaran.Debet = -1;
                    pengeluaran.NoReferensi = 0;
                    pengeluaran.JumlahDikembalikan = 0;
                }

                if (m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGELUARAN_LANGSUNG ||
                    m_iJenisPengeluaran == E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR ||
                    m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGELUARAN_ADD ||
                    m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGELUARAN_BLUD ||
                    m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGELUARAN_BOS )
                {
                    pengeluaran.Details = GetRekening();
                    pengeluaran.Potongans = GetPotongan();
                }
                if (m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGELUARAN_PANJAR || m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR)
                {
                    pengeluaran.Jumlah = txtJumlahPanjar.Text.FormatUangReportKeDecimal();

                }
                else
                {
                    pengeluaran.Jumlah = 0;
                    foreach (PengeluaranRekening pr in pengeluaran.Details)
                    {
                        pengeluaran.Jumlah = pengeluaran.Jumlah + pr.Jumlah;
                    }
                }
                pengeluaran.idcrt = GlobalVar.Pengguna.ID;
       

                long noUrutReturned = 0;
                noUrutReturned = oLogic.Simpan(ref pengeluaran);
                
                if (oLogic.IsError())
                {
                    MessageBox.Show(oLogic.LastError());
                    ret.ResponseStatus = false;

                }
                else
                {
                    ret.ResponseStatus = true;
                    //m_lstBKU = pengeluaran.ListBKU;
                    m_iNoUrut = noUrutReturned;
                    m_bNew = false;

                    if (GlobalVar.gListPengeluaran != null)
                    {
                        Pengeluaran peng = GlobalVar.gListPengeluaran.FirstOrDefault(x => x.NoUrut == pengeluaran.NoUrut);

                        if (peng != null)
                        {
                            int idx = GlobalVar.gListPengeluaran.IndexOf(peng);
                            GlobalVar.gListPengeluaran[idx] = pengeluaran;
                        }
                        else
                        {
                            GlobalVar.gListPengeluaran.Add(pengeluaran);
                        }
                    }

                }


                return ret;
            }
            catch (Exception ex)
            {
                ret.ResponseStatus = false;
                return ret;
            }
        }

        private List<PengeluaranRekening> GetRekening()
        {
            try
            {
                List<PengeluaranRekening> lstRet = new List<PengeluaranRekening>();
                lstRet = ctrlRekeningKegiatan1.GetPengeluaranRekening();

                return lstRet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private List<PotonganPanjar> GetPotongan()
        {

            return ctrlPotongan2.getPenjarPotongan();

        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkBLUD_CheckedChanged(object sender, EventArgs e)
        {
            
                
            
        }

        private void ctrlProgramKegiatan1_Load(object sender, EventArgs e)
        {

        }
        
        private void chkUPGU_CheckedChanged_1(object sender, EventArgs e)
        {
            panelPanjar.Visible = false;
            lblPanjar.Visible = false;

        }

        private void ctrlRekeningKegiatan1_OnChanged(decimal pJumlah)
        {
            txtJumlah.Text = pJumlah.ToRupiahInReport();
        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }

       

        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage ret = new EventResponseMessage();

            try
            {
                m_bNew = true;
                txtNoBukti.Text = "";
                DateTime nowDay = DateTime.Now;
                ctrlTanggal1.Tanggal = nowDay;
                
                
                ctrlProgramKegiatan1.Clear();
                ctrlProgramKegiatan1.SetSumber(1);

                ctrlRekeningKegiatan1.Clear();
                ctrlPotongan2.CreateUntukSPJ();
                
                txtKeterangan.Text = "";
                txtJumlah.Text = "0";
                txtJumlahPanjar.Text = "0";
                txtJumlahPotongan.Text = "0";


                m_iNoUrut = 0;
                m_lstBKU = new List<BKU>();
                if (m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGELUARAN_ADD)
                {
                    m_IDDINas= 5020200;
                    ctrlDinas1.SetID(m_IDDINas, 1);
                    ctrlProgramKegiatan1.SetIDinas(m_IDDINas, 1);
                    ctrlProgramKegiatan1.SetValue(m_IDDINas, 1, 502, 50202, 50202204, 502022040008);

                }
                if (m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGELUARAN_BLUD)
                {
                    m_IDDINas = 1020100;
                    ctrlDinas1.SetID(m_IDDINas, 1);
                    //ctrlProgramKegiatan1.SetIDinas(m_IDDINas, 1);
                    //ctrlProgramKegiatan1.SetValue(m_IDDINas, 1, 502, 50202, 50202204, 502022040008);

                }
                if (m_iJenisPengeluaran == E_JENISPENGELUARAN.PENGELUARAN_BOS)
                {
                    m_IDDINas = 1010100;
                    ctrlDinas1.SetID(m_IDDINas, 1);
                    //ctrlProgramKegiatan1.SetIDinas(m_IDDINas, 1);
                    //ctrlProgramKegiatan1.SetValue(m_IDDINas, 1, 502, 50202, 50202204, 502022040008);

                }
                
                ret.ResponseStatus = true;
            }
            catch (Exception ex)
            {
                ret.ResponseStatus = false;
            }
            return ret;

        }
       
        private bool CekDataUntukDIHapus()
        {
            if (m_iStatus > 0)
            {
                MessageBox.Show("Sudah proses LPJ UP..");
                return false;
            }




            return true;
        }
        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage ret = new EventResponseMessage();
            try
            {
                PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
                if (MessageBox.Show("Apakah benar akan menghapus data ini?", "Confirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (CekDataUntukDIHapus() == false)
                    {
                        ret.ResponseStatus = false;
                        return ret;

                    }
                    if (ctrlPotongan2.PajakSudahDisetor() == true)
                    {
                        MessageBox.Show("Pajak sudah disetor...Hapus dulu setorannya. Sila Hapus penyetorannya dulu..");
                        ret.ResponseStatus = false;
                        return ret;
                    }

                    if (oLogic.Hapus(m_iNoUrut) == true)
                    {
                        MessageBox.Show("Berhasil menhapus data");

                        ret.ResponseStatus = true;
                    }
                    else
                    {
                        MessageBox.Show("Gagal menhapus data");
                        ret.ResponseStatus = false;
                    }

                }

                
            } catch(Exception ex){
                ret.ResponseStatus = false;
                MessageBox.Show(ex.Message);
            }
            return ret;

        }

        private void ctrlPanjar1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlProgramKegiatan1_Load_1(object sender, EventArgs e)
        {

        }

        private void ctrlSPP1_Load(object sender, EventArgs e)
        {

        }

        private void panelProgram_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
