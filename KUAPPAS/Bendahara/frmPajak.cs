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

using Formatting;
using DTO.Bendahara;
using DTO;

namespace KUAPPAS.Bendahara
{
    public partial class frmPajak : Form
    {

        private int m_IDDInas;
        private int m_KodeUk;
        private bool m_bNew ;
        private long m_NoUrut;
        List<PembayaranPajak> m_lstPembayaranPotongan;
        List<PembayaranPajak> m_lstPembayaranPotonganUntukDIbayar;
        decimal m_cJumlah;
        decimal m_cJumlah2;
      
        public frmPajak()
        {
            InitializeComponent();
            
            m_IDDInas=0;
            m_KodeUk = 0;
        }

        private void frmPajak_Load(object sender, EventArgs e)
        {
            gridPajak.FormatHeader();
            ctrlDinas1.Create();
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                m_IDDInas= GlobalVar.Pengguna.SKPD;
                ctrlDinas1.SetID(m_IDDInas);
            }

        }

      

        public void SetNew()
        {
            ctrlNavigation1.SetNew();
            gridPajak.Rows.Clear();
           
        }
        private bool CekInput()
        {
            if (GlobalVar.Pengguna.Status == 2)
            {
                MessageBox.Show("Pengguna Tidak bisa melakukan Transaksi");
                return false;

            }
            if (ctrlDinas1.PilihanValid == false)
            {
                return false;

            }
            if (txtNoBukti.Text.Trim().Length == 0)
            {
                MessageBox.Show("Belum mengisi No Bukti");
                return false;
            }
            if (txtKeterangan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Belum mengisi Keterangan..");
                return false;
            }
            if (ctrlTanggal1.Tanggal.Year != GlobalVar.TahunAnggaran)
            {
                MessageBox.Show("Tanggal Salah..");
                return false;
            }
            if (DataFormat.FormatUangReportKeDecimal(txtJumlah.Text)==0)
            {
                MessageBox.Show("Belum ada pilihan Pajak..");
                return false;
            }

            return true;

        }
        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage ret = new EventResponseMessage();
            if (CekInput() == false)
            {
                ret.ResponseStatus = false;
                return ret;
            }
            Setor oSetor = new Setor();
            oSetor.NoUrut = m_NoUrut;
            oSetor.IDDinas =m_IDDInas;
            oSetor.KodeUK = m_KodeUk;
           
            oSetor.IDUrusan = DataFormat.GetInteger(m_IDDInas.ToString().Substring(0,3));
            oSetor.IDProgram = 0;
            oSetor.IDKegiatan = 0;
            oSetor.IDSubKegiatan = 0;
            oSetor.KodeKategori = m_IDDInas.ToString().ToKodeKategori();
            oSetor.KodeUrusan = m_IDDInas.ToString().ToKodeUrusan();
            oSetor.KodeSKPD = m_IDDInas.ToString().ToKodeSKPD();
            oSetor.KodekategoriPelaksana = m_IDDInas.ToString().ToKodeKategoriPelaksana();

            oSetor.KodeUrusanPelaksana = m_IDDInas.ToString().ToKodeUrusanPelaksana();
            oSetor.KodeProgram = 0;
            oSetor.KodeKegiatan = 0;
            oSetor.KodeSubKegiatan = 0;
                

            oSetor.NoBukti = txtNoBukti.Text.Trim();
            oSetor.dtBukuKas = ctrlTanggal1.Tanggal;
            oSetor.Keterangan = txtKeterangan.Text;
            oSetor.NoUrutClient = 0;
            oSetor.JenisSP2D = 1;
            oSetor.Jenis = (int)E_JENIS_SETOR.E_SETOR_PAJAK;
            
            oSetor.JenisBendahara = 2;

            oSetor.TahunLalu = 0;
            oSetor.Kodebank = chkBank.Checked ? 1 : 0;
            oSetor.Tahun = GlobalVar.TahunAnggaran;
            oSetor.NobuktiClient = "";
            oSetor.NamaBank = "";
            oSetor.NoNTPN = txtNTPN.Text.Trim();
            oSetor.KodeBilling = txtKodeBILLING.Text.Trim();
            oSetor.NoRekening = "";
            oSetor.KodeBilling = txtKodeBILLING.Text;
            oSetor.NourutBayangan = "";
            oSetor.NPWP = "";
            oSetor.Penerima = "";
            oSetor.PPKD = 0;
            oSetor.SetorKeKasda = 1;
            oSetor.Sumber = 1;
            oSetor.Alamat = "";
            oSetor.UnitAnggaran = ctrlDinas1.UnitAnggaran;
            oSetor.Jumlah = 0;
           
            oSetor.Details =GetDetail();
            foreach (SetorRekening sr in oSetor.Details)
            {
                oSetor.Jumlah = oSetor.Jumlah + sr.Jumlah;
            }

            if (oSetor.Jumlah == 0)
            {
                MessageBox.Show("Belum ada data pungut yang dipilih");
                ret.ResponseStatus = false;
                return ret;
            
            }
            


            SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
            long noUrut=0;
            noUrut =oLogic.Simpan(oSetor);

            if (oLogic.IsError())
            {
                MessageBox.Show("Kesalahan menyimpan pengembalian Belanja" + oLogic.LastError());
            }
            else
            {
                m_NoUrut = noUrut;
                m_bNew = false;

                MessageBox.Show("Penyimpanan selesai..");
            }

            ret.ResponseStatus = true;



            UpdateStatusList();
            return ret;
        }
        private new List<PembayaranPajak> GetPajakDibayar()
        {
            try
            {
                List<PembayaranPajak> lst = new List<PembayaranPajak>(); 
                foreach (DataGridViewRow row in gridPajak.Rows)
                {
                    if (row.Cells[2].Value != null)
                    {
                        bool dipilih;
                        dipilih = Convert.ToBoolean(row.Cells[1].Value);
                        if (dipilih == true)
                        {
                            PembayaranPajak pdb = new PembayaranPajak();
                            pdb.NoUrutBelanja = DataFormat.GetLong(row.Cells[0].Value.ToString());
                            pdb.IDPotongan = DataFormat.GetLong(row.Cells[4].Value.ToString());
                            lst.Add(pdb);
                        }
                    }

                }
                return lst;
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }
        private List<SetorRekening> GetDetail()
        {
            List<SetorRekening> lst = new List<SetorRekening>();
            foreach (DataGridViewRow row in gridPajak.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                       bool dipilih;
                        dipilih = Convert.ToBoolean(row.Cells[1].Value);
                        if (dipilih == true)
                        {
                            SetorRekening sr = new SetorRekening();
                           
                            sr.KodeuK = m_KodeUk;
                            sr.IDUrusan = DataFormat.GetInteger(row.Cells[10].Value.ToString());
                            sr.IDProgram = DataFormat.GetInteger(row.Cells[11].Value.ToString());
                            sr.IdKegiatan = DataFormat.GetInteger(row.Cells[12].Value.ToString());
                            sr.IDSubKegiatan = DataFormat.GetLong(row.Cells[13].Value.ToString().Replace(".", ""));
                            sr.NoUrutBelanja = DataFormat.GetLong(row.Cells[0].Value.ToString());
                            sr.IDRekening   = DataFormat.GetLong(row.Cells[4].Value.ToString());
                            sr.Jumlah = DataFormat.FormatUangReportKeDecimal(row.Cells[6].Value.ToString());
                           
                            lst.Add(sr);
                        }
                    
                }


            }
            return lst;

        }
        private bool UpdateStatusList()
        {
            try
            {
                List<PembayaranPajak> lst = new List<PembayaranPajak>();
                lst = GetPajakDibayar();
                if (lst == null)
                {

                    return false;
                }
                foreach (PembayaranPajak pp in lst)
                {
                    for(int idx=0; idx< m_lstPembayaranPotongan.Count;idx++){
                        if (m_lstPembayaranPotongan[idx].NoUrutBelanja == pp.NoUrutBelanja &&
                            m_lstPembayaranPotongan[idx].IDPotongan == pp.IDPotongan &&
                            m_lstPembayaranPotongan[idx].StatusPajak == 0
                            )

                            m_lstPembayaranPotongan[idx].StatusPajak = 1;
                     
                    }
               //     MessageBox.Show((m_lstPembayaranPotongan.FindAll(x => x.StatusPajak == 0)).Count.ToString());
                }
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool GetPembayaranPajak()
        {
            ///*
            ///Untuk semua yang harus dibayar
            ///


           
            try
            {
                PotonganPanjarLogic oLogic = new PotonganPanjarLogic(GlobalVar.TahunAnggaran);
                m_lstPembayaranPotongan = new List<PembayaranPajak>();
                
                if (ctrlDinas1.PilihanValid== false  ){
                    return true;
                }

                m_lstPembayaranPotongan = oLogic.GetPajakUntukPembayaran(m_IDDInas,m_KodeUk);

                if (m_lstPembayaranPotongan == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return false;
                }
                if (m_lstPembayaranPotongan.Count == 0)
                {
                    MessageBox.Show("Tidak ada data");
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        public bool GetPembayaranPajakUntukDbayar()
        {
            ///*
            ///Untuk yang akan dibayar 
            ///



            try
            {
                gridPajak.Rows.Clear();
                if (m_IDDInas == 0)
                {
                    return true;
                }
                if (m_lstPembayaranPotongan == null)
                {
                    GetPembayaranPajak();
                }

               

                List<PembayaranPajak> lst = m_lstPembayaranPotongan.FindAll(p => p.IDDInas == m_IDDInas );
                if (lst.Count==0)
                {
                    GetPembayaranPajak();
                }


                m_lstPembayaranPotonganUntukDIbayar = new List<PembayaranPajak>();
                m_lstPembayaranPotonganUntukDIbayar= m_lstPembayaranPotongan.FindAll
                    (p=>p.TanggalBelanja <= ctrlTanggal1.Tanggal && p.StatusPajak==0 && 
                        p.IDDInas== m_IDDInas && p.KodeUK == m_KodeUk);
                    
                                    
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        public bool GetPembayaranPajakByNoUrut()
        {
            ///*
            ///Untuk yang akan dibayar 
            ///



            try
            {
                gridPajak.Rows.Clear();
                if (m_NoUrut == 0)
                {
                    return true;
                }

                if (m_lstPembayaranPotongan == null)
                {
                    GetPembayaranPajak();
                }


                List<PembayaranPajak> lst = m_lstPembayaranPotongan.FindAll(p => p.IDDInas == m_IDDInas && p.KodeUK == m_KodeUk);
                if (lst.Count == 0)
                {
                    GetPembayaranPajak();
                }


                m_lstPembayaranPotonganUntukDIbayar = new List<PembayaranPajak>();
                
                m_lstPembayaranPotonganUntukDIbayar = m_lstPembayaranPotongan.FindAll
                    (p => p.NoUrutSetorPajak== m_NoUrut);



                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            try
            {

                ctrlDinas1.Create();
                ctrlTanggal1.Tanggal = DateTime.Now.Date;
                txtNoBukti.Text = "";
                txtKeterangan.Text = "";
                txtNTPN.Text = "";
                txtKodeBILLING.Text = "";
                gridPajak.Rows.Clear();
                m_bNew = true;
                m_NoUrut = 0;
                txtJumlah.Text = "0";
                label7.Visible = false;
                txtJumlah2.Visible = false;

          

         


                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret.ResponseStatus = false;
                return ret;
            }
        }

        private void ctrlTanggal1_OnChanged(DateTime pTanggal)
        {
            
            if (GetPembayaranPajakUntukDbayar())
            {
                DisplayDaftarPembayaran(false);
            }
        }

        private bool DisplayDaftarPembayaran(bool dipilih)
        {
            try
            {
                if (m_lstPembayaranPotonganUntukDIbayar == null)
                {
                    return true;
                }
                foreach (PembayaranPajak pem in m_lstPembayaranPotonganUntukDIbayar)
                {
                    string[] row = {
                                   pem.NoUrutBelanja.ToString(),
                                   dipilih== true?"true":"false",
                                   pem.NoBuktiBelanja,
                                   pem.TanggalBelanja.ToString("dd MMM"),
                                   pem.IDPotongan.ToString(),
                                   pem.NamaPotongan,
                                   pem.Jumlah.ToRupiahInReport(),
                                   pem.KeteranganBelanja, 
                                   pem.KodeBilling,
                                   pem.NTPN,
                                   pem.IDUrusan.ToString(),
                                   pem.IDProgram.ToString(),
                                   pem.IDKegiatan.ToString(),
                                   pem.IDSubKegiatan.ToString()};
                                  
                    gridPajak.Rows.Add(row);

                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool HitungJumlah()
        {
            try
            {
                bool dipilih;
                decimal nilai;
                m_cJumlah = 0;
                foreach (DataGridViewRow row in gridPajak.Rows)
                {
                    if (row.Cells[2].Value != null)
                    {
                        nilai = 0;
                        //dipilih = bool(row.Cells[1].Value);
                        DataGridViewCheckBoxCell chkchecking = row.Cells["PIlih"] as DataGridViewCheckBoxCell;

                        dipilih=Convert.ToBoolean(chkchecking.Value) ;
                        nilai = DataFormat.FormatUangReportKeDecimal(row.Cells[6].Value.ToString());
                        if (dipilih == true)
                        {
                            m_cJumlah = m_cJumlah + nilai;
                        } 
                    }

                }

                txtJumlah.Text = m_cJumlah.ToRupiahInReport();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show ("Ada kesalahan menghitung Pajak " + ex.Message);
                return false;

            }

        }
        private void ctrlTanggal1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }
        public bool SetPenyetoranPajak(Setor oSetor)
        {
            try
            {
                

                m_NoUrut = oSetor.NoUrut;
                m_IDDInas = oSetor.IDDinas;
                m_KodeUk = oSetor.KodeUK;
                ctrlDinas1.SetID(oSetor.IDDinas, oSetor.KodeUK);
                txtNoBukti.Text = oSetor.NoBukti;
                txtKeterangan.Text = oSetor.Keterangan;
                ctrlTanggal1.Tanggal = oSetor.dtBukuKas;
                txtJumlah2.Text = oSetor.Jumlah.ToRupiahInReport();
                txtNTPN.Text = oSetor.NoNTPN;
                txtKodeBILLING.Text = oSetor.KodeBilling;
                m_cJumlah = oSetor.Jumlah;
                m_cJumlah2 = oSetor.Jumlah;
                txtJumlah2.Visible = true;
                label7.Visible = true;
                txtJumlah2.Enabled = false;

                chkBank.Checked = oSetor.Kodebank == 1 ? true : false;
                GetPembayaranPajak();
                GetPembayaranPajakByNoUrut();
                DisplayDaftarPembayaran(true );
                HitungJumlah();
                cmdUpdateHeaderOnly.Visible = true;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_IDDInas = pIDSKPD;
            m_KodeUk = pIDUK;

            //if (m_lstPembayaranPotongan == null)
            //{
                GetPembayaranPajak();
           // }
            if (m_lstPembayaranPotongan.FindAll(x => x.IDDInas == m_IDDInas ).Count == 0)
            {
                GetPembayaranPajak();
            }
            if (GetPembayaranPajakUntukDbayar())
            {
                DisplayDaftarPembayaran(false);
            }
        }

        private void gridPajak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 1)
            //{
            //    gridPajak.RefreshEdit();
            //}
        }

        private void gridPajak_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    gridPajak.RefreshEdit();

                    gridPajak.CurrentCell.Value = Convert.ToBoolean(gridPajak.CurrentCell.Value) == true ? false : true;

                    //gridPajak.NotifyCurrentCellDirty(true);
                    HitungJumlah();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void gridPajak_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 1)
            //{
            //    HitungJumlah();
            //}
        }

        private void gridPajak_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            //if (e.ColumnIndex == 1)
            //{
            //    HitungJumlah();
            //}
        }

        private void gridPajak_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 1)
            //{
            //    HitungJumlah();
            //}
        }

        private void gridPajak_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex == 1)
            //{
            //    HitungJumlah();
            //}
        }

        private void gridPajak_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            

           
        }

        private void gridPajak_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 1)
            //{
            //    HitungJumlah();
            //}
        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }

        private EventResponseMessage ctrlNavigation1_OnEdit()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            try
            {
                
                lRet.ResponseStatus = true;

                if (m_lstPembayaranPotongan == null)
                {
                    GetPembayaranPajak();
                }
                if (m_lstPembayaranPotongan.FindAll(x => x.IDDInas == m_IDDInas && x.KodeUK == m_KodeUk).Count == 0)
                {
                    GetPembayaranPajak();
                }
                if (GetPembayaranPajakUntukDbayar())
                {
                    DisplayDaftarPembayaran(false);
                }
                return lRet;
            }
            catch (Exception ex)
            {
                lRet.ResponseStatus = false;
                return lRet;

            }
        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage ret = new EventResponseMessage();

            ret.ResponseStatus = true;
            if (MessageBox.Show("Apakah bendar akan menghapus data ini?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Setor oSetor = new Setor();
                oSetor.NoUrut = m_NoUrut;
                oSetor.IDDinas = m_IDDInas;
                oSetor.KodeUK = m_KodeUk;

                SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                bool Returned = false;
                Returned = oLogic.Hapus(m_NoUrut, E_JENIS_SETOR.E_SETOR_PAJAK);

                if (Returned == false)
                {
                    MessageBox.Show("Kesalahan menghapus data ini " + oLogic.LastError());
                }
                else
                {
                    MessageBox.Show("Penghapusan selesai ");

                    SetNew();
                }
            }

            ret.ResponseStatus = true;
            
            return ret;

        }

        private void cmdUpdateHeaderOnly_Click(object sender, EventArgs e)
        {
            Setor oSetor = new Setor();
            oSetor.NoUrut = m_NoUrut;
            oSetor.IDDinas = m_IDDInas;
            oSetor.KodeUK = m_KodeUk;

            oSetor.IDUrusan = DataFormat.GetInteger(m_IDDInas.ToString().Substring(0, 3));
            oSetor.IDProgram = 0;
            oSetor.IDKegiatan = 0;
            oSetor.IDSubKegiatan = 0;
            oSetor.KodeKategori = m_IDDInas.ToString().ToKodeKategori();
            oSetor.KodeUrusan = m_IDDInas.ToString().ToKodeUrusan();
            oSetor.KodeSKPD = m_IDDInas.ToString().ToKodeSKPD();
            oSetor.KodekategoriPelaksana = m_IDDInas.ToString().ToKodeKategoriPelaksana();

            oSetor.KodeUrusanPelaksana = m_IDDInas.ToString().ToKodeUrusanPelaksana();
            oSetor.KodeProgram = 0;
            oSetor.KodeKegiatan = 0;
            oSetor.KodeSubKegiatan = 0;


            oSetor.NoBukti = txtNoBukti.Text.Trim();
            oSetor.dtBukuKas = ctrlTanggal1.Tanggal;
            oSetor.Keterangan = txtKeterangan.Text;
            oSetor.NoUrutClient = 0;
            oSetor.JenisSP2D = 1;
            oSetor.Jenis = (int)E_JENIS_SETOR.E_SETOR_PAJAK;

            oSetor.JenisBendahara = 2;

            oSetor.TahunLalu = 0;
            oSetor.Kodebank = chkBank.Checked ? 1 : 0;
            oSetor.Tahun = GlobalVar.TahunAnggaran;
            oSetor.NobuktiClient = "";
            oSetor.NamaBank = "";
            oSetor.NoNTPN = txtNTPN.Text.Trim();
            oSetor.KodeBilling = txtKodeBILLING.Text.Trim();
            oSetor.NoRekening = "";
            oSetor.KodeBilling = txtKodeBILLING.Text;
            oSetor.NourutBayangan = "";
            oSetor.NPWP = "";
            oSetor.Penerima = "";
            oSetor.PPKD = 0;
            oSetor.SetorKeKasda = 1;
            oSetor.Sumber = 1;
            oSetor.Alamat = "";
            oSetor.UnitAnggaran = ctrlDinas1.UnitAnggaran;
            oSetor.Jumlah = m_cJumlah2;

           
       


            SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
            long noUrut = 0;
            noUrut = oLogic.Simpan(oSetor,2,true);

            if (oLogic.IsError())
            {
                MessageBox.Show("Kesalahan menyimpan pengembalian Belanja" + oLogic.LastError());
            }
            else
            {
                m_NoUrut = noUrut;
                m_bNew = false;

                MessageBox.Show("Penyimpanan selesai..");
            }

        }
    }
}
