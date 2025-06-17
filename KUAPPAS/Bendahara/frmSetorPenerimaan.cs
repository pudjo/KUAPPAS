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
using Formatting;
using BP;
using BP.Bendahara;


namespace KUAPPAS.Bendahara
{
    public partial class frmSetorPenerimaan : Form
    {
        private int m_IDDInas;
        private bool OnLoading;
        private long  m_NoUrut;
        private int m_iKodeUK;
        private bool mbOnlyDisplay;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        List<SetorRekening> m_lstSetorRekening;
        private List<STSDisetor> mlstSTSDisetor;
        Setor m_osetor;
        bool m_bNew;
        bool m_bEdit;
        public frmSetorPenerimaan()
        {
            InitializeComponent();
            m_lstSetorRekening= new List<SetorRekening> ();
            mlstSTSDisetor = new List<STSDisetor>();
            OnLoading = false;
            m_bNew = false;
            m_bEdit = false;
        }

        private void CatatKeRingkasan()
        {
            try
            {
               m_lstSetorRekening= new List<SetorRekening> ();
               mlstSTSDisetor = new List<STSDisetor>();
               foreach (DataGridViewRow row in gridSetor.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        long NoUrut = DataFormat.GetLong(row.Cells[0].Value);

                        //bool bDipilih = (DataGridViewCheckBoxCell)row.Cells[1].Selected;
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[1];
                        gridRekening.Refresh();
                        bool bDipilih = Convert.ToBoolean(row.Cells[1].Value);


                        long idRekening = DataFormat.GetLong(row.Cells[4].Value.ToString().Replace(".",""));
                        int idx = 0;
                        decimal JumlahBarisIni = DataFormat.FormatUangReportKeDecimal(row.Cells[6].Value); ;

                        if (bDipilih == true)
                        {

                            SetorRekening sr = new SetorRekening();
                            sr= m_lstSetorRekening.FirstOrDefault(x=>x.IDRekening== idRekening );
                            if (sr !=null){
                                idx = m_lstSetorRekening.IndexOf(sr);
                                m_lstSetorRekening[idx].Jumlah = m_lstSetorRekening[idx].Jumlah + JumlahBarisIni;
                            } else {
                                SetorRekening srToAdd = new SetorRekening();
                                srToAdd.IDRekening = idRekening;
                                srToAdd.Jumlah = JumlahBarisIni;

                                srToAdd.KodeuK = m_iKodeUK;
                                srToAdd.IDSubKegiatan = 0;
                                srToAdd.IDUrusan = 0;
                                srToAdd.IDProgram = 0;
                                srToAdd.IdKegiatan = 0;
                                srToAdd.NamaRekening = DataFormat.GetString(row.Cells[7].Value);
                                m_lstSetorRekening.Add(srToAdd);

                            }

                            STSDisetor stsdisetor = new STSDisetor();
                            stsdisetor.IIDRekening = idRekening;
                            stsdisetor.NoUrut = NoUrut;
                      


                            mlstSTSDisetor.Add(stsdisetor);


                            
                          

                        }

                    }

                }

                decimal Jumlah=0L;
                gridRekening.Rows.Clear();

                foreach(SetorRekening sr in m_lstSetorRekening){

                    string[] row= {sr.IDRekening.ToKodeRekening(), sr.NamaRekening, sr.Jumlah.ToRupiahInReport()};
                    gridRekening.Rows.Add(row);
                    Jumlah=Jumlah+ sr.Jumlah;
                }
                txtJumlah.Text = Jumlah.ToRupiahInReport();

               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmSetorPenerimaan_Load(object sender, EventArgs e)
        {
           
             
             gridSetor.FormatHeader();
             gridRekening.FormatHeader();

             

        }

        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            m_osetor = new Setor();
            m_bNew = true;
            m_bEdit = false;
            ctrlDinas1.Create();
            m_IDDInas = ctrlDinas1.GetID();
            txtNoBukti.Text = "";
            txtKeterangan.Text = "";
            dtSetor.Tanggal = DateTime.Now.Date;
            OnLoading = false;
            gridSetor.Rows.Clear();

            gridRekening.Rows.Clear();
            m_NoUrut = 0;
            LoadTBP();
            return lRet;


        }
        public bool SetSetor(Setor oSetor)
        {
            m_osetor = new Setor();
            m_osetor = oSetor;
            if (m_osetor != null)
            {
                ctrlDinas1.SetID(m_osetor.IDDinas, m_osetor.KodeUK);
                m_NoUrut = m_osetor.NoUrut;
                txtNoBukti.Text = m_osetor.NoBukti;
                m_IDDInas = m_osetor.IDDinas;
                m_iKodeUK = m_osetor.KodeUK;
                txtKeterangan.Text = m_osetor.Keterangan;
                dtSetor.Tanggal = m_osetor.dtBukuKas;
                m_bNew = false;
                m_bEdit = false;
                OnLoading = true;
                LoadTBP();
                decimal Jumlah = 0L;
                gridRekening.Rows.Clear();
                m_lstSetorRekening = m_osetor.Details;
                foreach (SetorRekening sr in m_lstSetorRekening)
                {

                    string[] row = { sr.IDRekening.ToKodeRekening(), sr.NamaRekening, sr.Jumlah.ToRupiahInReport() };
                    gridRekening.Rows.Add(row);
                    Jumlah = Jumlah + sr.Jumlah;
                }
                txtJumlah.Text = Jumlah.ToRupiahInReport();


                return true;
            }
            else
            {
                return false;
            }

        }

        private void dtSetor_OnChanged(DateTime pTanggal)
        {
            if (m_bNew = true )
            LoadTBP();
        }
        private bool LoadTBP()
        {
            try
            {
                gridSetor.Rows.Clear();
                STSLogic oSTSLogic = new STSLogic((int)GlobalVar.TahunAnggaran);
                List<STSDisetor> lst = new List<STSDisetor>();
                
                lst = oSTSLogic.GetTBPDinas(m_IDDInas, dtSetor.Tanggal, m_osetor.NoUrut);

                if (m_bNew == true &&             m_bEdit == false)
                {
                    lst = lst.FindAll(x => x.NoUrutSetor == 0);
                }
                
                if (m_bNew == false  && m_bEdit == false )
                {
                    lst = lst.FindAll(x => x.NoUrutSetor == m_NoUrut);
                }
                if (m_bEdit == true)
                {
                    lst = lst.FindAll(x => x.NoUrutSetor == m_NoUrut || x.NoUrutSetor ==0 );
                }


                if (oSTSLogic.IsError() == true)
                {
                    MessageBox.Show(oSTSLogic.LastError());
                    return false;
                }
               
                var query = from sk in lst
                            orderby sk.NoUrut
                            select sk;
                
                bool shoudChecked = m_osetor.NoUrut >0? true:false;
                decimal dJumlah = 0L;
                foreach (STSDisetor s in query)
                {
                   // bool shoudChecked = m_osetor.NoUrut > 0 ? true : false;
                    shoudChecked = m_NoUrut >0 && s.NoUrutSetor == m_NoUrut ? true : false;
                    string[] row = { s.NoUrut.ToString(), 
                                      shoudChecked.ToString(), 
                                      s.NoSTS, 
                                      s.TanggalSTS.FormatTanggal(), 
                                      s.IIDRekening.ToKodeRekening(), 
                                      s.NamaRekneing, 
                                      s.Jumlah.ToRupiahInReport(), 
                                      s.Keterangan };

                    gridSetor.Rows.Add(row);
                    if(shoudChecked == true) 
                      dJumlah = dJumlah + s.Jumlah;
                }
                //if (shoudChecked == true )

             txtJumlah.Text = dJumlah.ToRupiahInReport();
                //else
                  //  txtJumlah.Text = "0";

                

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
            m_iKodeUK = pIDUK;
            if (m_bNew == true)
               LoadTBP();
        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }
        public void SetNew()
        {
            ctrlNavigation1.SetNew();
        }

        private bool CekInput()
        {
            if (GlobalVar.Pengguna.Status >= 2)
            {
                MessageBox.Show("Status pengguna tidak boleh melakukan penyimpanan data.");
                return false;
            }
            if (ctrlDinas1.PilihanValid == false)
            {
                return false;
            }
            if (txtNoBukti.Text.Trim().Length == 0)
            {
                MessageBox.Show("Belum mengisi No bukti");
                return false;

            }
            if (txtKeterangan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Belum Keterangan");
                return false;

            }

            return true;
        }
        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage ret = new EventResponseMessage();
            try
            {
                if (CekInput() == false)
                {
                    ret.ResponseStatus = false;
                    return ret;

                }
                Setor oSetor = new Setor();
                oSetor.NoUrut = m_NoUrut;
                oSetor.IDDinas = m_IDDInas;
                oSetor.KodeUK = m_iKodeUK;
                oSetor.IDUrusan = 0;
                oSetor.IDProgram = 0;
                oSetor.IDKegiatan = 0;
                oSetor.IDSubKegiatan = 0;

                oSetor.KodeUrusanPelaksana = 0;
                oSetor.KodeProgram = 0;
                oSetor.KodeKegiatan = 0;
                oSetor.KodeSubKegiatan = 0;

                    oSetor.KodeKategori = m_IDDInas.ToString().ToKodeKategori();
                    oSetor.KodeUrusan = m_IDDInas.ToString().ToKodeUrusan();
                    oSetor.KodeSKPD = m_IDDInas.ToString().ToKodeSKPD();



                    oSetor.NoBukti = txtNoBukti.Text.Trim();
                    oSetor.dtBukuKas = dtSetor.Tanggal;
                    oSetor.Keterangan = txtKeterangan.Text;
                    oSetor.Jenis = (int)E_JENIS_SETOR.E_SETOR_STS;
                    oSetor.JenisSP2D = 0;
                    oSetor.NoUrutClient = 0;
                
                oSetor.JenisBendahara = 1;

                oSetor.TahunLalu = 0;
                oSetor.Kodebank = 0;
                oSetor.Tahun = GlobalVar.TahunAnggaran;
                oSetor.Jumlah = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
                oSetor.NobuktiClient = "";
                oSetor.NamaBank = "";
                oSetor.NoNTPN = "";
                oSetor.KodeBilling = "";
                oSetor.UnitAnggaran = ctrlDinas1.UnitAnggaran;
                oSetor.NoRekening = "";
                oSetor.NourutBayangan = "";
                oSetor.NPWP = "";
                oSetor.Penerima = "";
                oSetor.PPKD = 0;
                oSetor.SetorKeKasda = 1;
                oSetor.Sumber = 1;
                oSetor.Alamat = "";
                CatatKeRingkasan();
                oSetor.Details = m_lstSetorRekening;
                oSetor.STSDisetors= mlstSTSDisetor;

                SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                m_NoUrut = oLogic.Simpan(oSetor,1);
                if (oLogic.IsError())
                {
                    MessageBox.Show("Kesalahan menyimpan pengembalian Belanja" + oLogic.LastError());
                    ret.ResponseStatus = false;
                }
                else
                {

                    m_bEdit = false;
                    m_bNew = false;
                    MessageBox.Show("Penyimpanan selesai..");
                    ret.ResponseStatus = true;
                }

                


            }
            catch (Exception ex)
            {
                ret.ResponseStatus = false;
                MessageBox.Show(ex.Message);
            }
            return ret;
        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void cmdCari_Click(object sender, EventArgs e)
        {

            try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridSetor.Rows)
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
                    gridSetor.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridSetor.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void gridSetor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                cmdRefresh.PerformClick();

                
            }
        }

        private void dtSetor_Load(object sender, EventArgs e)
        {

        }

        private void gridSetor_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
      
                cmdRefresh.PerformClick();

            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            CatatKeRingkasan();
        }

        private EventResponseMessage ctrlNavigation1_OnEdit()
        {
            EventResponseMessage ret = new EventResponseMessage();
            m_bEdit = true;
            LoadTBP();
            ret.ResponseStatus = true;

            return ret; ;
        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage ret = new EventResponseMessage();
            try
            {
                if (MessageBox.Show("APakah benar akan menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                    if( oLogic.Hapus(m_NoUrut,E_JENIS_SETOR.E_SETOR_STS)== true){
                        ret.ResponseStatus = true;
                    
                    } else {
                        ret.ResponseStatus = false ;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show ("Penghapusan gagal.");
                ret.ResponseStatus = false ;
            }
            return ret;
        }
    }
}
