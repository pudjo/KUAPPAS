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


using Formatting;
using DTO.Bendahara;
using DTO;
using System.IO;

namespace KUAPPAS.Bendahara
{
    public partial class frmCekBKU : Form
    {
        private int m_iIDDInas;
        private List<BKUDISPLAY> mListBKU;
        private List<STS> m_lstSTS;
        private List<Pengeluaran> m_lstPengeluaran;
        private List<Setor> m_lstSetor ;
        private List<Koreksi>  mlstKoreksi ; 
         private decimal m_sJumlahPenerimaan;
        private decimal m_sJumlahPengeluaran;
        private int m_iKodeUK;
        List<clsObject> listObject = new List<clsObject>();

        List<clsObject> lstBKU = new List<clsObject>();
        int columnCount;
        int count = 0;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;
         bool isNewPage;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
    
        private int m_iJenisBendahara;
        private decimal m_cSaldoAwal;
        private int m_iBankSaldoAwal;
        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        List<SPP> m_ListSPP;
        private List<Setor> m_lstSetorPenerimaan;
        private List<SetorRekening> m_listSetorRekening;

        public frmCekBKU()
        {
            InitializeComponent();
        }
        public int Dinas
        {
            set
            {
                m_iIDDInas = value;
                ctrlDinas1.Create();
                ctrlDinas1.SetID(m_iIDDInas);
            }
        }
        public DateTime TanggalAwal
        {
            set { 
                mTanggalAwal = value;
                ctrlTanggalBulanVertikal1.TanggalAwal = mTanggalAwal;
            }
        }
        public DateTime TanggalAkhir
        {
            set { 
                mTanggalAkhir = value;
                ctrlTanggalBulanVertikal1.TanggalAkhir = mTanggalAkhir;

            }
        }
        public int JenisBendahara
        {
            set
            {
                m_iJenisBendahara = value;
                lblJenisBendahara.Text = m_iJenisBendahara == 1 ? "Bendahara Penerimaan" : "Bendahara Pengeluaran";

            }
        }
        private void frmCekBKU_Load(object sender, EventArgs e)
        {
            try
            {
                gridBKU1.FormatHeader();
                gridBKU2.FormatHeader();
                cmbSUmber.Items.Clear();
                ListItemData li = new ListItemData("Semua", 0);
                cmbSUmber.Items.Add(li);
                if (GlobalVar.Pengguna.SKPD > 0)
                {
                    m_iIDDInas= GlobalVar.Pengguna.SKPD;
                    ctrlDinas1.SetID(m_iIDDInas);
                }
                li = new ListItemData("S P 2 D", (int)E_JENIS_REFERENSIBKU.REFERENSI_SP2D);
                cmbSUmber.Items.Add(li);

                li = new ListItemData("Penerimaan/STS", (int)E_JENIS_REFERENSIBKU.REFERENSI_TSTS);
                cmbSUmber.Items.Add(li);

                li = new ListItemData("Penyetoran Penerimaan/STS Ke Kasda", (int)E_JENIS_REFERENSIBKU.REFERENSI_SETORSTS);
                cmbSUmber.Items.Add(li);
                li = new ListItemData("Potongan Pajak SP2D", (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSP2D);
                cmbSUmber.Items.Add(li);

                li = new ListItemData("Belanja Panjar/BPK/Pertanggungjawaban Panjar", (int)E_JENIS_REFERENSIBKU.REFERENSI_PENGELUARANLANGSUNG);
                cmbSUmber.Items.Add(li);

                li = new ListItemData("Pungutan Pajak SPJ/BPK", (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSPJPANJAR);
                cmbSUmber.Items.Add(li);

                li = new ListItemData("Penyetoran Pajak", (int)E_JENIS_REFERENSIBKU.REFERENSI_PENYETORANPAJAK);
                cmbSUmber.Items.Add(li);
                li = new ListItemData("Pengembalian Belanja", (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP);
                cmbSUmber.Items.Add(li);
                li = new ListItemData("Koreksi Belanja", (int)E_JENIS_REFERENSIBKU.REFERENSI_KOREKSI);
                cmbSUmber.Items.Add(li);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void GetTanggal()
        {
            mTanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
            mTanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;

        }
        private void cmdPanggilBKU_Click(object sender, EventArgs e)
        {
            
            if (ctrlTanggalBulanVertikal1.CekPilihan() == true)
            {
                GetTanggal();
            }
            else
                return;

            //  Bandingin Jumlah
            BandingkanJumlah();
            BandingkanJumlaDetail();
            Panggil();
        }
        
        private void BandingkanJumlah()
        {
            try

            {
                int isumberData = GetSumberData();
                int iddinas= ctrlDinas1.GetID();
                
                BKULogic bkulogic = new BKULogic(GlobalVar.TahunAnggaran);
                lstBKU = new List<clsObject>();
                lstBKU = bkulogic.GetJumlah(iddinas, isumberData, mTanggalAwal, mTanggalAkhir);

                decimal jumlahBKU = 0; //bkulogic.GetJumlah(iddinas, isumberData, mTanggalAwal, mTanggalAkhir);
                if (lstBKU  != null)
                {
                    jumlahBKU = lstBKU.Sum(x => x.Jumlah);


                    txtJunlahBKU.Text = jumlahBKU.ToRupiahInReport();
                }
                else
                {
                    MessageBox.Show(bkulogic.LastError()); 
                }
                decimal jumlahBelanja = 0;
                switch (isumberData)
                {
                    case 1:
                        jumlahBelanja =SPP();
                        break;
                    case 5:
                        jumlahBelanja = GetJumlahBelanja();
                        break;
                    case 9:
                        jumlahBelanja = PungutPajak();
                        break;

                    case 17:
                        jumlahBelanja = PenyetoranPajak();
                        break;
                }
                txtJumlahTrx.Text = jumlahBelanja.ToRupiahInReport();

                if (jumlahBelanja != jumlahBKU)
                {
                    txtJumlahTrx.BackColor = Color.Red;
                    txtJunlahBKU.BackColor = Color.Red;
                }
                else
                {
                    txtJumlahTrx.BackColor = Color.LightSeaGreen ;
                    txtJunlahBKU.BackColor = Color.LightSeaGreen;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BandingkanJumlaDetail()
        {
            try
            {
                int isumberData = GetSumberData();
                int iddinas = ctrlDinas1.GetID();

                BKULogic bkulogic = new BKULogic(GlobalVar.TahunAnggaran);
                decimal jumlahBKU = bkulogic.GetJumlahDetail(iddinas, isumberData, mTanggalAwal, mTanggalAkhir);
                if (bkulogic.IsError() == false)
                {
                    txtJumlahBKUDetail.Text = jumlahBKU.ToRupiahInReport();
                }
                else
                {
                    MessageBox.Show(bkulogic.LastError());
                }
                decimal jumlahBelanja = 0;
                switch (isumberData)
                {
                    case 1:
                        
                        jumlahBelanja = SPPDetail();
                        
                        break;
                    case 5:
                        jumlahBelanja = GetJumlahBelanjaDetail();
                        break;
                    case 9:
                        jumlahBelanja = PungutPajakDetail();
                        break;
                        
                    case 17:
                        jumlahBelanja = PenyetoranPajakDetail();
                        break;
                }
                txtJumlahTrxDetail.Text = jumlahBelanja.ToRupiahInReport();

                if (jumlahBelanja != jumlahBKU)
                {
                    txtJumlahTrx.BackColor = Color.Red;
                    txtJunlahBKU.BackColor = Color.Red;
                }
                else
                {
                    txtJumlahTrx.BackColor = Color.LightSeaGreen;
                    txtJunlahBKU.BackColor = Color.LightSeaGreen;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private decimal GetJumlahBelanja()
        {
            try
            {
                int isumberData = GetSumberData();
                int iddinas = ctrlDinas1.GetID();

                PengeluaranLogic belanjalogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
                listObject = new List<clsObject>();

                decimal jumlahBelanja = listObject.Sum(x => x.Jumlah);
                return jumlahBelanja;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }
        private decimal GetJumlahBelanjaDetail()
        {
            try
            {
                int isumberData = GetSumberData();
                int iddinas = ctrlDinas1.GetID();

                PengeluaranLogic belanjalogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
                decimal jumlahBelanja = belanjalogic.GetJumlahDetail(iddinas, isumberData, mTanggalAwal, mTanggalAkhir);
                return jumlahBelanja;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }
        private decimal PungutPajak()
        {
            try
            {
                int isumberData = GetSumberData();
                int iddinas = ctrlDinas1.GetID();

                PengeluaranLogic belanjalogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
                decimal jumlahBelanja = belanjalogic.GetJumlahPungutPajak(iddinas, isumberData, mTanggalAwal, mTanggalAkhir);
                return jumlahBelanja;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }
        private decimal PungutPajakDetail()
        {
            try
            {
                int isumberData = GetSumberData();
                int iddinas = ctrlDinas1.GetID();

                PengeluaranLogic belanjalogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
                decimal jumlahBelanja = belanjalogic.GetJumlahPungutPajak(iddinas, isumberData, mTanggalAwal, mTanggalAkhir);
                return jumlahBelanja;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }

        private decimal PenyetoranPajak()
        {
            try
            {
                int isumberData = GetSumberData();
                int iddinas = ctrlDinas1.GetID();

                SetorLogic belanjalogic = new SetorLogic(GlobalVar.TahunAnggaran);
                decimal jumlahBelanja = belanjalogic.GeJumlahSetor(iddinas, isumberData, mTanggalAwal, mTanggalAkhir);
                return jumlahBelanja;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }

        private decimal SPP()
        {
            try
            {
                int isumberData = GetSumberData();
                int iddinas = ctrlDinas1.GetID();

                SPPLogic belanjalogic = new SPPLogic(GlobalVar.TahunAnggaran);
                decimal jumlahBelanja = belanjalogic.GetJumlah(iddinas,0, isumberData, mTanggalAwal, mTanggalAkhir);
                return jumlahBelanja;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }
        private decimal SPPDetail()
        {
            try
            {
                int isumberData = GetSumberData();
                int iddinas = ctrlDinas1.GetID();

                SPPLogic belanjalogic = new SPPLogic(GlobalVar.TahunAnggaran);
                decimal jumlahBelanja = belanjalogic.GetJumlahDetail(iddinas, 0, isumberData, mTanggalAwal, mTanggalAkhir);
                return jumlahBelanja;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }
        private decimal PenyetoranPajakDetail()
        {
            try
            {
                int isumberData = GetSumberData();
                int iddinas = ctrlDinas1.GetID();

                SetorLogic belanjalogic = new SetorLogic(GlobalVar.TahunAnggaran);
                decimal jumlahBelanja = belanjalogic.GeJumlahSetorDetail(iddinas, isumberData, mTanggalAwal, mTanggalAkhir);
                return jumlahBelanja;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }
            private int GetSumberData()
        {
            try
            {
                ListItemData li = (ListItemData)cmbSUmber.SelectedItem;
                if (li != null)
                {
                    if (cmbSUmber.Text.Trim().Length > 0)
                    {
                        if (li.Itemdata > 0)
                            return li.Itemdata;
                    }

                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;


            }


        }
         private void Panggil()
         {

                try
                {
                }
                catch (Exception ex)
                {

                }
         }

        private void Panggil2()
        {

            try
            {

                gridBKU1.Rows.Clear();
                BKULogic oLogic = new BKULogic((int)GlobalVar.TahunAnggaran);
                m_iIDDInas = ctrlDinas1.GetID();
                m_iKodeUK = ctrlDinas1.GetKodeUK();
                GetTanggal();
                DateTime tanggalawal = mTanggalAwal;
                DateTime tanggalakhir = mTanggalAkhir;
                int isumberData = GetSumberData();
                mListBKU= new List<BKUDISPLAY>();
                if (isumberData == 0)
                {
                    MessageBox.Show("Belum memilih Sumber Data...");
                    return;
                }
                List<int> lstJenisSumber = new List<int>();
                lstJenisSumber.Add(isumberData);
                if (isumberData == 1 || isumberData == 3 || isumberData == 5 || isumberData == 6
                    || isumberData == 9 || isumberData == 10 || isumberData == 12
                    || isumberData == 14 || isumberData == 16 || isumberData == 17 || isumberData == 25)
                {
                    m_iJenisBendahara = 2;

                }

                if (isumberData == 2 || isumberData == 24)
                {
                    m_iJenisBendahara = 1;

                }

                DateTime tanggalAwalTahun = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                             mListBKU = oLogic.GetBKU(m_iIDDInas, m_iJenisBendahara, tanggalAwalTahun, tanggalakhir, m_iKodeUK, lstJenisSumber);
                if (mListBKU != null)
                {

                    if (isumberData == 1)
                    {
                        CekBKUSP2D();

                    }
                    if (isumberData == (int)E_JENIS_REFERENSIBKU.REFERENSI_TSTS)
                    {
                        CekBKUSTS();

                    }
                    if (isumberData == (int)E_JENIS_REFERENSIBKU.REFERENSI_SETORSTS)
                    {
                        CekBKUSetorSTS();

                    }
                    if (isumberData == (int)E_JENIS_REFERENSIBKU.REFERENSI_PENGELUARANLANGSUNG)
                    {
                        CekBelanjaSPJ((int)E_JENIS_REFERENSIBKU.REFERENSI_PENGELUARANLANGSUNG);
                    }
                    if (isumberData == (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSPJPANJAR)
                    {
                        CekBelanjaSPJ((int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSPJPANJAR);
                    }

                    if (isumberData == (int)E_JENIS_REFERENSIBKU.REFERENSI_PENYETORANPAJAK)
                    {
                        CekSetorPajak();
                    }
                    if (isumberData == (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP)
                    {
                        CekPengembalianBelanja();
                    }
                    if (isumberData == (int)E_JENIS_REFERENSIBKU.REFERENSI_KOREKSI)
                    {
                        CekKoreksiBelanja();
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void CekBKUSP2D()
        {
            if (PanggilSP2D() == true)
            {
                List<long> lstNoUrutSumberInBKU = new List<long>();
                List<long> lstNoUrutinSP2D = new List<long>();
         
                foreach (BKUDISPLAY bku in mListBKU)
                {
                    lstNoUrutSumberInBKU.Add(bku.NoUrutSumber);
                }
                foreach (SPP spp in m_ListSPP)
                {
                    lstNoUrutinSP2D.Add(spp.NoUrut);
                }

                var lstNoUrutinSP2DNoInBKU = lstNoUrutinSP2D.Except(lstNoUrutSumberInBKU);
                foreach (var item in lstNoUrutinSP2DNoInBKU)
                {
                    SPP oSPP = new SPP();
                    oSPP = m_ListSPP.FirstOrDefault(x => x.NoUrut == (long)item);
                    if (oSPP != null)
                    {
                        string[] row = {oSPP.NoUrut.ToString(),oSPP.NoSP2D, oSPP.dtCair.ToTanggalIndonesia(),
                                                   oSPP.Keterangan.Substring(1,20) + "...", oSPP.Jumlah.ToRupiahInReport(),"BKUkan"};
                        gridBKU1.Rows.Add(row);

                    }
               }

               var lstNoUrutNoInTrx = lstNoUrutSumberInBKU.Except(lstNoUrutinSP2D);
               foreach (var item in lstNoUrutNoInTrx)
                {
                    BKUDISPLAY oBKU = new BKUDISPLAY();
                    oBKU = mListBKU.FirstOrDefault(x => x.NoUrutSumber== (long)item && x.JenisSumber==1 );
                    if (oBKU != null)
                    {
                        string[] row = {oBKU.NoUrut.ToString(),oBKU.NoBukti, oBKU.Tanggal.ToTanggalIndonesia(),
                                                   oBKU.Uraian, oBKU.Jumlah.ToRupiahInReport(),"Hapus"};
                        gridBKU2.Rows.Add(row);

                    }
                }


            }
        }
        private bool  PanggilSP2D()
        {
            try
            {


                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);

                m_iIDDInas = ctrlDinas1.GetID();
                p.IDDInas = m_iIDDInas;// ctrlSKPD1.GetID();
                
                p.LstStatus = new List<int>();
                p.Jenis = -1;
                
                List<int> lstStatus = new List<int>();
                lstStatus.Add(4);
                p.LstStatus = lstStatus;
                p.TidakdiBKU = 1;
                p.Jenis = -1;
                p.NoSP2D = "";
                p.NoSPM = "";
                p.NoSPP = "";
                p.Status = 4;
                m_ListSPP = new List<SPP>();

                 m_ListSPP = oLogic.GetSPP(p);

              
                if (m_ListSPP == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void CekBKUSTS()
        {
            if (PanggilSTS() == true)
            {
                List<long> lstNoUrutSumberInBKU = new List<long>();
                List<long> lstNoUrutinTrx = new List<long>();
                // List<long> lstNoUrutinSP2DNoInBKU = new List<long>();

                foreach (BKUDISPLAY bku in mListBKU)
                {
                    lstNoUrutSumberInBKU.Add(bku.NoUrutSumber);
                }
                foreach (STS spp in m_lstSTS)
                {
                    lstNoUrutinTrx.Add(spp.NoUrut);
                }

                var lstNoUrutinSP2DNoInBKU = lstNoUrutinTrx.Except(lstNoUrutSumberInBKU);
                foreach (var item in lstNoUrutinSP2DNoInBKU)
                {
                    STS o = new STS();
                    o = m_lstSTS.FirstOrDefault(x => x.NoUrut == (long)item);
                    if (o != null)
                    {
                        string[] row = {o.NoUrut.ToString(),o.NoSTS, o.dtBukuKas.ToTanggalIndonesia(),
                                                   o.Keterangan, o.Jumlah.ToRupiahInReport(),"BKUkan"};
                        gridBKU1.Rows.Add(row);

                    }


                }
                var lstNoUrutNoInTrx = lstNoUrutSumberInBKU.Except(lstNoUrutinTrx);
                foreach (var item in lstNoUrutNoInTrx)
                {
                    BKUDISPLAY oBKU = new BKUDISPLAY();
                    oBKU = mListBKU.FirstOrDefault(x => x.NoUrut == (long)item);
                    if (oBKU != null)
                    {
                        string[] row = {oBKU.NoUrut.ToString(),oBKU.NoBukti, oBKU.Tanggal.ToTanggalIndonesia(),
                                                   oBKU.Uraian, oBKU.Jumlah.ToRupiahInReport(),"Hapus"};
                        gridBKU2.Rows.Add(row);

                    }
                }




            }
        }
        private void CekBKUSetorSTS()
        {
            if (PanggilSetorSTS() == true)
            {
                List<long> lstNoUrutSumberInBKU = new List<long>();
                List<long> lstNoUrutinTrx = new List<long>();
                // List<long> lstNoUrutinSP2DNoInBKU = new List<long>();

                foreach (BKUDISPLAY bku in mListBKU)
                {
                    lstNoUrutSumberInBKU.Add(bku.NoUrutSumber);
                }
                foreach (Setor setor in m_lstSetorPenerimaan)
                {
                    lstNoUrutinTrx.Add(setor.NoUrut);
                }

                var lstNoUrutinTrxNoInBKU = lstNoUrutinTrx.Except(lstNoUrutSumberInBKU);
                foreach (var item in lstNoUrutinTrxNoInBKU)
                {
                    Setor o = new Setor ();
                    o = m_lstSetorPenerimaan.FirstOrDefault(x => x.NoUrut == (long)item);
                    if (o != null)
                    {
                        string[] row = {o.NoUrut.ToString(),o.NoBukti, o.dtBukuKas.ToTanggalIndonesia(),
                                                   o.Keterangan, o.Jumlah.ToRupiahInReport(),"BKUkan"};
                        gridBKU1.Rows.Add(row);

                    }


                }
                var lstNoUrutNoInTrx = lstNoUrutSumberInBKU.Except(lstNoUrutinTrx);
                foreach (var item in lstNoUrutNoInTrx)
                {
                    BKUDISPLAY oBKU = new BKUDISPLAY();
                    oBKU = mListBKU.FirstOrDefault(x => x.NoUrut == (long)item);
                    if (oBKU != null)
                    {
                        string[] row = {oBKU.NoUrut.ToString(),oBKU.NoBukti, oBKU.Tanggal.ToTanggalIndonesia(),
                                                   oBKU.Uraian, oBKU.Jumlah.ToRupiahInReport(),"Hapus"};
                        gridBKU2.Rows.Add(row);

                    }
                }


            }
        }
        private bool PanggilSTS()
        {
            try
            {


                STSLogic oLogic = new STSLogic((int)GlobalVar.TahunAnggaran);
                GetTanggal();
                DateTime tanggalawal = mTanggalAwal;
                DateTime tanggalakhir = mTanggalAkhir;

                m_iIDDInas = ctrlDinas1.GetID();
                m_lstSTS = oLogic.GetByDinas(m_iIDDInas, tanggalawal, tanggalakhir,-1);//, E_JENIS_SETOR.E_SETOR_PAJAK);
                return true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan memanggil data Pendapatan" +  ex.Message);
                return false;
            }
        }

      
        private bool PanggilSetorSTS()
        {
            try
            {
                GetTanggal();
                m_iIDDInas = ctrlDinas1.GetID();
                m_iKodeUK = ctrlDinas1.GetKodeUK();
                DateTime tanggalawal = mTanggalAwal;
                DateTime tanggalakhir = mTanggalAkhir;
                SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
                m_lstSetorPenerimaan = new List<Setor>();
                m_lstSetorPenerimaan = oLogic.GetByDinas(m_iIDDInas, E_JENIS_SETOR.E_SETOR_STS, tanggalawal, tanggalakhir);

            //    m_listSetorRekening = oLogic.GetDetailByDinas(m_iIDDInas, E_JENIS_SETOR.E_SETOR_STS, tanggalawal, tanggalakhir);

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan memanggil data Penyetoran" + ex.Message);
                return false;

            }
        }

        private void CekBelanjaSPJ( int JenisSumber)
        {
            if (PanggilBelanja() == true)
            {
                List<long> lstNoUrutSumberInBKU = new List<long>();
                List<long> lstNoUrutinTrx = new List<long>();
                // List<long> lstNoUrutinSP2DNoInBKU = new List<long>();

                foreach (BKUDISPLAY bku in mListBKU)
                {
                    if (JenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSPJPANJAR)
                    {
                        if (bku.JenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSPJPANJAR)
                        {
                            lstNoUrutSumberInBKU.Add(bku.NoUrutSumber);
                        }

                    }
                    else
                    {
                       // lstNoUrutSumberInBKU.Add(bku.NoUrutSumber);
                    }
                }
                foreach (Pengeluaran p in m_lstPengeluaran)
                {
                    if (JenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSPJPANJAR){
                        if (p.Potongans.Count > 0)
                        {
                            lstNoUrutinTrx.Add(p.NoUrut);
                        }
                
                    } else {
                        //lstNoUrutinTrx.Add(p.NoUrut);
                    }
                    
                }

                var lstNoUrutinTrxNoInBKU = lstNoUrutinTrx.Except(lstNoUrutSumberInBKU);

                gridBKU1.Rows.Clear();
                foreach (var item in lstNoUrutinTrxNoInBKU)
                {
                    Pengeluaran o = new Pengeluaran();
                    
                    o = m_lstPengeluaran.FirstOrDefault(x => x.NoUrut == (long)item);
                    decimal jumlah = o.Jumlah;
                    if (JenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSPJPANJAR)
                    {
                        jumlah = 0;
                        foreach (PotonganPanjar pp in o.Potongans)
                        {
                            jumlah = jumlah + pp.Jumlah;
                        }
                    }

                    if (o != null)
                    {
                        
                        string[] row = {o.NoUrut.ToString(),o.NoBukti, o.Tanggal.ToTanggalIndonesia(),
                                                   o.Uraian.Substring(0,150)+ "...", jumlah.ToRupiahInReport(),"BKUkan"};
                        gridBKU1.Rows.Add(row);

                        
                    }


                }

                var lstNoUrutNoInTrx = lstNoUrutSumberInBKU.Except(lstNoUrutinTrx);

                gridBKU2.Rows.Clear();
                foreach (var item in lstNoUrutNoInTrx)
                {
                    BKUDISPLAY oBKU = new BKUDISPLAY();
                    oBKU = mListBKU.FirstOrDefault(x => x.NoUrutSumber == (long)item 
                                                          && x.JenisSumber == JenisSumber 
                                                           && x.Tanggal >= mTanggalAwal && x.Tanggal<= mTanggalAkhir);

                    if (oBKU != null)
                    {
                        string[] row = {oBKU.NoUrut.ToString(),oBKU.NoBukti, oBKU.Tanggal.ToTanggalIndonesia(),
                                                   oBKU.Uraian, oBKU.Jumlah.ToRupiahInReport(),"Hapus"};
                        gridBKU2.Rows.Add(row);

                    }
                }


            }
        }
        private void CekSetorPajak()
        {
            if (PanggilSetorPajak() == true)
            {
                List<long> lstNoUrutSumberInBKU = new List<long>();
                List<long> lstNoUrutinTrx = new List<long>();
                // List<long> lstNoUrutinSP2DNoInBKU = new List<long>();

                foreach (BKUDISPLAY bku in mListBKU)
                {

                    if (bku.JenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_PENYETORANPAJAK)
                    {
                        lstNoUrutSumberInBKU.Add(bku.NoUrutSumber);
                    }
                    
                }
                foreach (Setor p in m_lstSetor)
                {
                    lstNoUrutinTrx.Add(p.NoUrut);
                    
                }

                var lstNoUrutinTrxNoInBKU = lstNoUrutinTrx.Except(lstNoUrutSumberInBKU);

                gridBKU1.Rows.Clear();
                foreach (var item in lstNoUrutinTrxNoInBKU)
                {
                    Setor o = new Setor();
                    o = m_lstSetor.FirstOrDefault(x => x.NoUrut == (long)item 
                        && x.dtBukuKas>=mTanggalAwal
                        && x.dtBukuKas <= mTanggalAkhir);
                    if (o != null)
                    {

                        string[] row = {o.NoUrut.ToString(),o.NoBukti, o.dtBukuKas.ToTanggalIndonesia(),
                                                   o.Keterangan.Substring(0,150)+ "...", o.Jumlah.ToRupiahInReport(),"BKUkan"};
                        gridBKU1.Rows.Add(row);
                    }
                }

                var lstNoUrutNoInTrx = lstNoUrutSumberInBKU.Except(lstNoUrutinTrx);

                gridBKU2.Rows.Clear();
                foreach (var item in lstNoUrutNoInTrx)
                {
                    BKUDISPLAY oBKU = new BKUDISPLAY();
                    oBKU = mListBKU.FirstOrDefault(x => x.NoUrutSumber == (long)item
                         && x.Tanggal>=mTanggalAwal
                        && x.Tanggal <= mTanggalAkhir);

                 
                    if (oBKU != null)
                    {
                        string[] row = {oBKU.NoUrut.ToString(),oBKU.NoBukti, oBKU.Tanggal.ToTanggalIndonesia(),
                                                   oBKU.Uraian, oBKU.Jumlah.ToRupiahInReport(),"Hapus"};
                        gridBKU2.Rows.Add(row);

                    }
                }
            }
        }
        private void CekPengembalianBelanja()
        {
            if (PanggilPengembalianBelanja() == true)
            {
                List<long> lstNoUrutSumberInBKU = new List<long>();
                List<long> lstNoUrutinTrx = new List<long>();
                // List<long> lstNoUrutinSP2DNoInBKU = new List<long>();

                foreach (BKUDISPLAY bku in mListBKU)
                {

                    if (bku.JenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP)
                    {
                        lstNoUrutSumberInBKU.Add(bku.NoUrutSumber);
                    }

                }
                foreach (Setor p in m_lstSetor)
                {
                    lstNoUrutinTrx.Add(p.NoUrut);

                }

                var lstNoUrutinTrxNoInBKU = lstNoUrutinTrx.Except(lstNoUrutSumberInBKU);

                gridBKU1.Rows.Clear();
                foreach (var item in lstNoUrutinTrxNoInBKU)
                {
                    Setor o = new Setor();
                    o = m_lstSetor.FirstOrDefault(x => x.NoUrut == (long)item
                        && x.dtBukuKas >= mTanggalAwal
                        && x.dtBukuKas <= mTanggalAkhir);
                    if (o != null)
                    {

                        string[] row = {o.NoUrut.ToString(),o.NoBukti, o.dtBukuKas.ToTanggalIndonesia(),
                                                   o.Keterangan.Substring(0,150)+ "...", o.Jumlah.ToRupiahInReport(),"BKUkan"};
                        gridBKU1.Rows.Add(row);
                    }
                }

                var lstNoUrutNoInTrx = lstNoUrutSumberInBKU.Except(lstNoUrutinTrx);

                gridBKU2.Rows.Clear();
                foreach (var item in lstNoUrutNoInTrx)
                {
                    BKUDISPLAY oBKU = new BKUDISPLAY();
                    oBKU = mListBKU.FirstOrDefault(x => x.NoUrutSumber == (long)item
                         && x.Tanggal >= mTanggalAwal
                        && x.Tanggal <= mTanggalAkhir);


                    if (oBKU != null)
                    {
                        string[] row = {oBKU.NoUrut.ToString(),oBKU.NoBukti, oBKU.Tanggal.ToTanggalIndonesia(),
                                                   oBKU.Uraian, oBKU.Jumlah.ToRupiahInReport(),"Hapus"};
                        gridBKU2.Rows.Add(row);

                    }
                }
            }
        }
        private void CekKoreksiBelanja()
        {
            if (PanggilKoreksi() == true)
            {
                List<long> lstNoUrutSumberInBKU = new List<long>();
                List<long> lstNoUrutinTrx = new List<long>();
                // List<long> lstNoUrutinSP2DNoInBKU = new List<long>();

                foreach (BKUDISPLAY bku in mListBKU)
                {

                    if (bku.JenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_KOREKSI)
                    {
                        lstNoUrutSumberInBKU.Add(bku.NoUrutSumber);
                    }

                }
                foreach (Koreksi p in mlstKoreksi)
                {
                    lstNoUrutinTrx.Add(p.NoUrut);

                }

                var lstNoUrutinTrxNoInBKU = lstNoUrutinTrx.Except(lstNoUrutSumberInBKU);

                gridBKU1.Rows.Clear();
                foreach (var item in lstNoUrutinTrxNoInBKU)
                {
                    Koreksi o = new Koreksi();
                    o = mlstKoreksi.FirstOrDefault(x => x.NoUrut == (long)item
                        && x.DtKoreksi >= mTanggalAwal
                        && x.DtKoreksi <= mTanggalAkhir);
                    if (o != null)
                    {

                        string[] row = {o.NoUrut.ToString(),o.NoBukti, o.DtKoreksi.ToTanggalIndonesia(),
                                                   o.Uraian.Substring(0,150)+ "...", o.Jumlah.ToRupiahInReport(),"BKUkan"};
                        gridBKU1.Rows.Add(row);
                    }
                }

                var lstNoUrutNoInTrx = lstNoUrutSumberInBKU.Except(lstNoUrutinTrx);

                gridBKU2.Rows.Clear();
                foreach (var item in lstNoUrutNoInTrx)
                {
                    BKUDISPLAY oBKU = new BKUDISPLAY();
                    oBKU = mListBKU.FirstOrDefault(x => x.NoUrutSumber == (long)item
                         && x.Tanggal >= mTanggalAwal
                        && x.Tanggal <= mTanggalAkhir);


                    if (oBKU != null)
                    {
                        string[] row = {oBKU.NoUrut.ToString(),oBKU.NoBukti, oBKU.Tanggal.ToTanggalIndonesia(),
                                                   oBKU.Uraian, oBKU.Jumlah.ToRupiahInReport(),"Hapus"};
                        gridBKU2.Rows.Add(row);

                    }
                }
            }
        }

        private bool PanggilKoreksi()
        {
            try
            {
                GetTanggal();
                m_iIDDInas = ctrlDinas1.GetID();

                KoreksiLogic oKoreksiLogic = new KoreksiLogic(GlobalVar.TahunAnggaran);
                mlstKoreksi = new List<Koreksi>();

              
                mlstKoreksi = oKoreksiLogic.GetByDinas(m_iIDDInas, GlobalVar.TahunAnggaran);



                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan memanggil data Penyetoran" + ex.Message);
                return false;

            }

        }
        private bool PanggilSetorPajak()
        {
            try
            {
                GetTanggal();
                m_iIDDInas = ctrlDinas1.GetID();
                
                SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
                m_lstSetor = new List<Setor>();
                m_lstSetor = oLogic.GetByDinas(m_iIDDInas, E_JENIS_SETOR.E_SETOR_PAJAK, mTanggalAwal, mTanggalAkhir);


                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan memanggil data Penyetoran" + ex.Message);
                return false;

            }
        }
        private bool PanggilPengembalianBelanja()
        {
            try
            {
                GetTanggal();
                m_iIDDInas = ctrlDinas1.GetID();

                SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
                m_lstSetor = new List<Setor>();
                m_lstSetor = oLogic.GetByDinas(m_iIDDInas, E_JENIS_SETOR.E_SETOR_CP, mTanggalAwal, mTanggalAkhir);


                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan memanggil data Penyetoran" + ex.Message);
                return false;

            }
        }


        
        private bool PanggilBelanja()
        {
            try
            {
                GetTanggal();
                m_iIDDInas = ctrlDinas1.GetID();
                m_iKodeUK = ctrlDinas1.GetKodeUK();
                DateTime tanggalawal = mTanggalAwal;
                DateTime tanggalakhir = mTanggalAkhir;
                PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
   
                m_lstPengeluaran = new List<Pengeluaran>();
                ParameterBendahara pb = new ParameterBendahara(GlobalVar.TahunAnggaran);
                pb.IDDInas = m_iIDDInas;// ctrlSKPD1.GetID();
                pb.TanggalAwal = tanggalawal;
                pb.TanggalAkhir = tanggalakhir;
                
                pb.Jenis = -1;
                pb.LstJenis = new List<int>();
                pb.LstJenis.Add(1);
                pb.LstJenis.Add(3);
                pb.LstJenis.Add(4);
                m_lstPengeluaran=oLogic.Get(pb,true );// true with potongan


                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan memanggil data Penyetoran" + ex.Message);
                return false;

            }
        }

    }
}
