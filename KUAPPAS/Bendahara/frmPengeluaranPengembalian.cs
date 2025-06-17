using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DTO.Bendahara;
using BP;
using BP.Bendahara;
using Formatting;
namespace KUAPPAS.Bendahara
{
    public partial class frmPengeluaranPengembalian : Form
    {
        private const int CON_COL_JUMLAH =6;

        private int m_IDDinas;
        private int m_iKodeUK;
        private SPP m_oSPP;
        private int m_IDUrusan;
        private int m_IDProgram;

        private int m_IDKegiatan;
        private long m_IDSubKegiatan;
        private long m_NoUrut;
        private Setor m_oSetor;
        private bool m_bNew;
        private decimal m_cJumlah;
        private decimal m_cJumlahSPP;
        private int idUnitEdited;
        private string subUnitEdited;

        public frmPengeluaranPengembalian()
        {
            InitializeComponent();
            m_IDDinas=0;
            m_iKodeUK=0;
            m_oSPP= new SPP();
            m_NoUrut = 0;
            m_bNew = false;
            idUnitEdited=0;
            subUnitEdited="";
        }

        private void frmPengembalian_Load(object sender, EventArgs e)
        {
         
            ctrlJenisSPP1.Create();
            ctrlDinas1.Create();
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlDinas1.SetID(GlobalVar.Pengguna.SKPD);
                m_IDDinas = GlobalVar.Pengguna.SKPD;
            }
            gridSPPRekening.FormatHeader();

        }

        private void ctrlProgram1_OnChanged(int pID)
        {
          m_IDProgram=pID;
        
        }

        private void ctrlProgram1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlSubKegiatan1_OnChanged(long pID)
        {
            m_IDSubKegiatan = pID;
        }

        private void ctrlSubKegiatan1_Load(object sender, EventArgs e)
        {
            
        }

        private void ctrlKegiatanAPBD1_OnChanged(int pID)
        {
            m_IDKegiatan = pID;
        }

        private void ctrlKegiatanAPBD1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlUrusanPemerintahan1_OnChanged(int pID)
        {
            m_IDUrusan = pID;
        }

        private void ctrlUrusanPemerintahan1_Load(object sender, EventArgs e)
        {

        }

    

        private void cmdAdd_Click(object sender, EventArgs e)
        {

        }

        private void ctrlJenisSPP1_OnChanged(int pID)
        {
            chkUPTahunLalu.Enabled  = false;
            switch (pID)
            {

                case 0:
                    lblNoSP2D.Text = "No SP2D";
                    txtJumlah.Visible = true;
                    ctrlSPP1.Enabled = false;
                    chkUPTahunLalu.Visible = true;
                    txtJumlah.Enabled  = true;
                    chkUPTahunLalu.Enabled = true;
                    groupRekening.Visible = false;
                    lblKeteranganAPP.Visible = false;
                    lblKeteranganAPP.Text = "";
                   if (m_bNew ==true )
                    txtJumlah.Text = "0";

                    ctrlSPP1.Clear();

                    break;
                case 1:
                    lblNoSP2D.Text = "No SP2D";
                    txtJumlah.Enabled  = false ;
                    ctrlSPP1.Visible = true;
                    ctrlSPP1.Enabled   = true;
                    ctrlSPP1.Create(m_IDDinas, m_iKodeUK, pID, 4);
                    groupRekening.Visible = true;
                    break;
                case 2:
                    lblNoSP2D.Text = "No SP2D";
                    txtJumlah.Enabled  = true ;
                    ctrlSPP1.Visible = true;
                    ctrlSPP1.Enabled   = true;
                    ctrlSPP1.Create(m_IDDinas, m_iKodeUK, pID, 4);
                    groupRekening.Visible = false;
                    break;
                case 3:
                    lblNoSP2D.Text = "No SP2D";
                    txtJumlah.Enabled  = false ;
                    ctrlSPP1.Visible = true;
                    ctrlSPP1.Enabled   = true;
                    ctrlSPP1.Create(m_IDDinas, m_iKodeUK, pID, 4);
                    groupRekening.Visible = true;
                    break;
                case 4:
                    lblNoSP2D.Text = "No SP2D";
                    txtJumlah.Enabled  = false ;
                    ctrlSPP1.Visible = true;
                    ctrlSPP1.Enabled   = true;
                    ctrlSPP1.Create(m_IDDinas, m_iKodeUK, pID, 4);
                    groupRekening.Visible = true;

                    break;
                case 5:
                    break;

            }
        }
        public bool SetSetor(Setor oSetor)
        {
            try
            {
                if (oSetor == null)
                {
                    return false;
                }
                m_oSetor = new Setor();
                m_oSetor = oSetor;
                m_bNew = false;
                m_NoUrut = m_oSetor.NoUrut;
                ctrlDinas1.Create();
                m_IDDinas = m_oSetor.IDDinas;
                m_iKodeUK = m_oSetor.KodeUK;
                ctrlDinas1.SetID(m_IDDinas, m_iKodeUK);
                txtJumlah.Text = oSetor.Jumlah.ToRupiahInReport();
                txtNoBukti.Text = m_oSetor.NoBukti;
                txtKeterangan.Text = m_oSetor.Keterangan;
                ctrlTanggal1.Tanggal = m_oSetor.dtBukuKas;
                chkBank.Checked = true;// m_oSetor.Kodebank == 1 ? true : false;
                chkUPTahunLalu.Checked = m_oSetor.TahunLalu == 1 ? true : false;
                ctrlJenisSPP1.SetValue(m_oSetor.JenisSP2D);
                if (m_oSetor.Jenis == 1 || m_oSetor.Jenis == 3 || m_oSetor.Jenis == 4)
                {
                    ctrlJenisSPP1.SetValue(m_oSetor.JenisSP2D);

                    ctrlSPP1.SetID(Convert.ToInt64(m_oSetor.NoUrutClient));


                    for (int nRow = 0; nRow < gridSPPRekening.Rows.Count; nRow++)
                    {
                        foreach (SetorRekening sr in oSetor.Details)
                        {
                            if (sr.IDRekening == DataFormat.GetLong(gridSPPRekening.Rows[nRow].Cells[2].Value) &&
                                sr.IDSubKegiatan == DataFormat.GetLong(gridSPPRekening.Rows[nRow].Cells[1].Value))
                            {
                                gridSPPRekening.Rows[nRow].Cells[CON_COL_JUMLAH].Value = sr.Jumlah.ToRupiahInReport();
                            }
                        }
                    }
                    HitungJumlah();
                }
                else
                {
                    txtJumlah.Text = m_oSetor.Jumlah.ToRupiahInReport();
                }
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Pengembalian: On Set Setor:" + ex.Message);
                return false;
            }
        }
        private void ctrlSPP1_OnChanged(long pID)
        {
            try
            {
               
                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                m_oSPP = new SPP();
                m_iKodeUK = m_oSPP.KodeUK;
                if (ctrlJenisSPP1.GetID() > 2)
                {
                    m_oSPP = ctrlSPP1.GetSPP();
                }
                else
                {
                    m_oSPP = ctrlSPP1.GetSPP(false);
                }
                m_iKodeUK = m_oSPP.KodeUK;

                if (m_oSPP != null)
                {
                    if (m_oSPP.Jenis > 2)
                    {

                        if (m_bNew == false)
                        {
                            lblKeteranganAPP.Text = m_oSPP.Keterangan;
                            ctrlUrusanPemerintahan1.Create(m_IDDinas, GlobalVar.TahunAnggaran);
                            m_IDUrusan = m_oSetor.IDUrusan;
                            ctrlUrusanPemerintahan1.Create(m_IDDinas, GlobalVar.TahunAnggaran);
                            ctrlUrusanPemerintahan1.SetID(m_IDUrusan);
                            ctrlProgram1.Create(m_oSPP.Tahun, m_IDDinas, m_IDUrusan);
                            m_IDProgram = m_oSetor.IDProgram;
                            ctrlProgram1.SetID(m_IDProgram);
                            ctrlKegiatanAPBD1.CreateWIthUK(m_oSPP.Tahun, m_IDDinas, m_iKodeUK, m_IDProgram);
                            m_IDKegiatan = m_oSetor.IDKegiatan;
                            ctrlKegiatanAPBD1.SetID(m_IDKegiatan);
                            ctrlSubKegiatan1.CreateWithUK(m_oSPP.Tahun, m_IDDinas, m_iKodeUK, m_IDKegiatan);
                            m_IDSubKegiatan = m_oSetor.IDSubKegiatan;
                            ctrlSubKegiatan1.SetID(m_IDSubKegiatan);


                        }
                        else
                        {
                            //ctrlUrusanPemerintahan1.Create (m_IDDinas,GlobalVar.TahunAnggaran);
                            ctrlUrusanPemerintahan1.Show(m_oSPP.Rekenings);
                            m_IDUrusan = ctrlUrusanPemerintahan1.GetID();

                            ctrlUrusanPemerintahan1.SetID(m_IDUrusan);
                            ctrlProgram1.Create(m_oSPP.Tahun, m_IDDinas, m_IDUrusan, m_oSPP.Rekenings);
                            m_IDProgram = ctrlProgram1.GetID();

                            ctrlKegiatanAPBD1.CreateWIthUK(m_oSPP.Tahun, m_IDDinas, m_iKodeUK, m_IDProgram, m_oSPP.Rekenings);
                            m_IDKegiatan = ctrlKegiatanAPBD1.GetID();

                            ctrlSubKegiatan1.CreateWithUK(m_oSPP.Tahun, m_IDDinas, m_iKodeUK, m_IDKegiatan, m_oSPP.Rekenings);
                            m_IDSubKegiatan = ctrlSubKegiatan1.GetID();


                        }
                        lblKeteranganAPP.Text = m_oSPP.Keterangan;

                        gridSPPRekening.Rows.Clear();
                        foreach (SPPRekening sr in m_oSPP.Rekenings)
                        {
                            string[] row = { sr.UnitKerja.ToString(), 
                                           sr.IDSubKegiatan.ToString(), 
                                           sr.IDRekening.ToString(), 
                                           sr.IDRekening.ToKodeRekening(), 
                                           sr.NamaRekening, 
                                           sr.Jumlah.ToRupiahInReport(), 
                                           "0" , 
                                           NamaUnit(sr.UnitKerja) ,
                                           sr.NamaSubKegiatan};

                            gridSPPRekening.Rows.Add(row);


                        }
                    }
                    else
                    {
                        txtJumlah.Text = m_oSPP.Jumlah.ToRupiahInReport();
                        lblKeteranganAPP.Text = m_oSPP.Keterangan;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pengembalian: On Set SP2D:" + ex.Message);
            }
        }
        private string NamaUnit(int KodeUK)
        {
            if (KodeUK==0)
            {
                return ctrlDinas1.GetNamaSKPD();
            }
            else
            {
                Unit unit = new Unit();
                unit = GlobalVar.gListOrganisasi.First(u => u.SKPD == m_IDDinas && u.Kode == KodeUK);
                return unit.Nama;

            }
        }
        private void ctrlProgram1_Load_1(object sender, EventArgs e)
        {

        }

   

       

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_IDDinas = pIDSKPD;
            m_iKodeUK = pIDUK;
            ctrlSPP1.Clear();
            lblKeteranganAPP.Text = "";
            txtJumlah.Text = "0";
            ctrlJenisSPP1.Clear();
            ctrlUrusanPemerintahan1.Clear();
            ctrlProgram1.Clear();
            ctrlKegiatanAPBD1.Clear();
            ctrlSubKegiatan1.Clear();
            gridSPPRekening.Rows.Clear();


        }

        private void ctrlSPP1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlJenisSPP1_Load(object sender, EventArgs e)
        {

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
            if (txtNoBukti.Text.Length == 0)
            {
                MessageBox.Show("Belum mengisi No Bukti");
                return false;
            }
            if (txtKeterangan.Text.Length == 0)
            {
                MessageBox.Show("Belum mengisi Keterangan");
                return false;
            }
            if (ctrlJenisSPP1.ID< 0)
            {
                MessageBox.Show("Belum memilih Jenis SP2D/Belanja");
                return false;
            }
            if (ctrlJenisSPP1.ID >0 && ctrlSPP1.DataSPP== null){
                MessageBox.Show("Belum Pilih SP2D");
                return false;
            }
            if (ctrlJenisSPP1.ID > 2 && (m_IDUrusan == 0 || m_IDProgram == 0 || m_IDKegiatan == 0 || m_IDSubKegiatan == 0))
            {
                MessageBox.Show("Program Kegiatan Salah.");
                return false;
            }
           //' if (DataFormat.FormatUangReportKeDecimal(txtJumlah.Text)==0)
           //' {
             //   MessageBox.Show("Belum memasukkan nilai Pengembalian.");
              //  return false;
            //}

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
                oSetor.IDDinas = m_IDDinas;
                oSetor.KodeUK = m_iKodeUK;
                if (ctrlJenisSPP1.ID > 2)
                {
                    oSetor.IDUrusan = m_IDUrusan;
                    oSetor.IDProgram = m_IDProgram;
                    oSetor.IDKegiatan = m_IDKegiatan;
                    oSetor.IDSubKegiatan = m_IDSubKegiatan;
                    oSetor.KodeKategori = m_IDDinas.ToString().ToKodeKategori();
                    oSetor.KodeUrusan = m_IDDinas.ToString().ToKodeUrusan();
                    oSetor.KodeSKPD = m_IDDinas.ToString().ToKodeSKPD();
                    oSetor.KodekategoriPelaksana = m_IDUrusan.ToString().ToKodeKategoriPelaksana();

                    oSetor.KodeUrusanPelaksana = m_IDUrusan.ToString().ToKodeUrusanPelaksana();
                    oSetor.KodeProgram = m_IDProgram.ToString().ToKodeProgram();
                    oSetor.KodeKegiatan = m_IDKegiatan.ToString().ToKodeKegiatan();
                    oSetor.KodeSubKegiatan = m_IDSubKegiatan.ToString().ToKodeSubKegiatan();
                    oSetor.KodeSKPD = m_IDDinas.ToString().ToKodeSKPD();


                    oSetor.NoBukti = txtNoBukti.Text.Trim();
                    oSetor.dtBukuKas = ctrlTanggal1.Tanggal;
                    oSetor.Keterangan = txtKeterangan.Text;
                    oSetor.NoUrutClient = ctrlSPP1.ID;
                    oSetor.JenisSP2D = ctrlJenisSPP1.ID;
                    oSetor.Jenis = (int)E_JENIS_SETOR.E_SETOR_CP;
                }
                else
                {
                    oSetor.IDUrusan = 0;
                    oSetor.IDProgram = 0;
                    oSetor.IDKegiatan = 0;
                    oSetor.IDSubKegiatan = 0;

                    oSetor.KodeUrusanPelaksana = 0;
                    oSetor.KodeProgram = 0;
                    oSetor.KodeKegiatan = 0;
                    oSetor.KodeSubKegiatan = 0;

                    oSetor.KodeKategori = m_IDDinas.ToString().ToKodeKategori();
                    oSetor.KodeUrusan = m_IDDinas.ToString().ToKodeUrusan();
                    oSetor.KodeSKPD = m_IDDinas.ToString().ToKodeSKPD();



                    oSetor.NoBukti = txtNoBukti.Text.Trim();
                    oSetor.dtBukuKas = ctrlTanggal1.Tanggal;
                    oSetor.Keterangan = txtKeterangan.Text;
                    
                    oSetor.JenisSP2D = ctrlJenisSPP1.GetID();
                    if (oSetor.JenisSP2D == 0)
                    {
                        oSetor.Jenis = (int)E_JENIS_SETOR.E_SETOR_UYHD;
                        oSetor.NoUrutClient = 0;
                    }
                    else
                    {
                        oSetor.NoUrutClient = ctrlSPP1.ID;
                        if (oSetor.JenisSP2D == 2)
                        {
                            oSetor.Jenis = (int)E_JENIS_SETOR.E_SETOR_SISATU;
                        }
                        else
                        {
                            oSetor.Jenis = (int)E_JENIS_SETOR.E_SETOR_CP;
                        }

                    }
                    
                }
                oSetor.JenisBendahara = 2;

                oSetor.TahunLalu = chkUPTahunLalu.Checked ? 1 : 0;
                oSetor.Kodebank = 1;// chkBank.Checked ? 1 : 0;
                oSetor.Tahun = GlobalVar.TahunAnggaran;
         
                oSetor.NobuktiClient = "";
                oSetor.NamaBank = "";
                oSetor.NoNTPN = "";
                oSetor.NoRekening = "";
                oSetor.NourutBayangan = "";
                oSetor.NPWP = "";
                oSetor.Penerima = "";
                oSetor.PPKD = 0;
                oSetor.SetorKeKasda = 1;
                oSetor.Sumber = 1;
                oSetor.Alamat = "";
                oSetor.KodeBilling = "";
                oSetor.UnitAnggaran = ctrlDinas1.UnitAnggaran;
                oSetor.Jumlah = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
                if (ctrlJenisSPP1.ID > 2)
                {
                    oSetor.Jumlah = 0;
                    oSetor.Details = GetDetail();
                    foreach (SetorRekening s in oSetor.Details)
                    {
                        oSetor.Jumlah = oSetor.Jumlah + s.Jumlah;
                    }
                }


                SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);

                m_NoUrut= oLogic.Simpan(oSetor);
                if (oLogic.IsError())
                {
                    MessageBox.Show("Kesalahan menyimpan pengembalian Belanja" + oLogic.LastError());
                }
                else
                {
                
                    MessageBox.Show("Penyimpanan selesai..");
                }

                ret.ResponseStatus = true;
                

            }
            catch (Exception ex)
            {
                ret.ResponseStatus = false;
                MessageBox.Show(ex.Message);
            }
            return ret;
        }


        private List<SetorRekening> GetDetail()
        {
            List<SetorRekening> lst = new List<SetorRekening>();
            foreach (DataGridViewRow row in gridSPPRekening.Rows)
            {
                if ( row.Cells[2].Value !=null && 
                    row.Cells[CON_COL_JUMLAH].Value !=null ){
                    SetorRekening sr = new SetorRekening();
                    sr.KodeuK = DataFormat.GetInteger(row.Cells[0].Value);
                    sr.IDSubKegiatan =  DataFormat.GetLong(row.Cells[1].Value.ToString().Replace(".", ""));
                    sr.IDRekening = DataFormat.GetLong(row.Cells[2].Value.ToString().Replace(".", ""));
                    sr.Jumlah = DataFormat.FormatUangReportKeDecimal(row.Cells[CON_COL_JUMLAH].Value.ToString());
                    if (sr.Jumlah>0)
                    lst.Add (sr);
                }
                

            }
            return lst;

        }
        private void ctrlSubKegiatan1_Load_1(object sender, EventArgs e)
        {

        }

        private void gridSPPRekening_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == CON_COL_JUMLAH)
            {
                decimal h = DataFormat.GetDecimal(gridSPPRekening.Rows[e.RowIndex].Cells[CON_COL_JUMLAH].Value);

                //idUnitEdited = DataFormat.GetInteger(gridSPPRekening.Rows[e.RowIndex].Cells[0].Value);
                //subUnitEdited = DataFormat.GetString(gridSPPRekening.Rows[e.RowIndex].Cells[1].Value);

                gridSPPRekening.Rows[e.RowIndex].Cells[CON_COL_JUMLAH].Value = h.ToRupiahInReport();

                HitungJumlah();

                //for (int r = 0; r < gridSPPRekening.Rows.Count; r++)
                //{
                //    //if (
                //    //    idUnitEdited == DataFormat.GetInteger(gridSPPRekening.Rows[e.RowIndex].Cells[0].Value) &&
                //    //    subUnitEdited == DataFormat.GetString(gridSPPRekening.Rows[e.RowIndex].Cells[1].Value))
                                          
                //    //{

                //    //}
                    


                //}

            }
        
        }
         private void HitungJumlah()
          {
                    m_cJumlah = 0;
                    foreach (DataGridViewRow row in gridSPPRekening.Rows)
                    {
                        decimal nilai;
                        nilai = DataFormat.GetString(row.Cells[CON_COL_JUMLAH].Value).FormatUangReportKeDecimal();
                        m_cJumlah = m_cJumlah + nilai;
                    }
                    txtJumlah.Text = m_cJumlah.ToRupiahInReport();
         
          }
         private void HitungJumlahSPP()
         {
             m_cJumlahSPP = 0;
             foreach (DataGridViewRow row in gridSPPRekening.Rows)
             {
                 decimal nilai;
                 nilai = DataFormat.GetString(row.Cells[CON_COL_JUMLAH-1].Value).FormatUangReportKeDecimal();
                 m_cJumlahSPP = m_cJumlahSPP + nilai;
             }

         }

         private EventResponseMessage ctrlNavigation1_OnAdd()
         {
             EventResponseMessage ret = new EventResponseMessage();
             ret.ResponseStatus = true;
             try
             {
                 OnNew ();
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
             return ret;
             
         }
         public void SetNew()
         {
             ctrlNavigation1.SetNew();
          
         }
         public bool OnNew()
         {
             try
             {
                 ctrlDinas1.Create();
                 txtKeterangan.Text = "";
                 ctrlJenisSPP1.Create();
                 txtNoBukti.Text = "";
                 DateTime d = DateTime.Now.Date;
                 ctrlTanggal1.Tanggal = d;
                 txtJumlah.Text = "0";
                 groupRekening.Visible = false;
                 m_IDUrusan = 0;
                 m_IDProgram = 0;
                 m_IDKegiatan = 0;
                 m_IDSubKegiatan = 0;
                 gridSPPRekening.Rows.Clear();
                 m_bNew = true;
                 m_NoUrut = 0;
                 idUnitEdited = 0;
                 subUnitEdited = "";
                 return true;
             }
             catch (Exception ex)
             {

                 MessageBox.Show(ex.Message);
                 return false;
             }
         }

         private void ctrlUrusanPemerintahan1_OnChanged_1(int pID)
         {
             m_IDUrusan = pID;
         }

         private void ctrlUrusanPemerintahan1_Load_1(object sender, EventArgs e)
         {

         }

         private void gridSPPRekening_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
         {
             if (e.ColumnIndex == CON_COL_JUMLAH)
             {
                 idUnitEdited = DataFormat.GetInteger(gridSPPRekening.Rows[e.RowIndex].Cells[0].Value);
                 subUnitEdited = DataFormat.GetString(gridSPPRekening.Rows[e.RowIndex].Cells[1].Value);
                

             }

             
         }

         private void ctrlNavigation1_Load(object sender, EventArgs e)
         {

         }

         private void chkUPTahunLalu_CheckedChanged(object sender, EventArgs e)
         {
             if (chkUPTahunLalu.Checked == true)
             {
                 GetSaldoAwal();
             }
         }
         private void GetSaldoAwal()
         {
             try
             {
                 int iddinas = ctrlDinas1.GetID();

                 BKULogic oBKULogic = new BKULogic(GlobalVar.TahunAnggaran);
                 BKU saldoBKU = new BKU();
                 saldoBKU = oBKULogic.GetBKUSaldoAwal(iddinas, 2);
                 if (saldoBKU != null)
                 {
                    txtJumlah.Text = saldoBKU.Jumlah.ToRupiahInReport();
                    if (saldoBKU.Kodebank == 1)
                    {
                        chkBank.Checked = true;
                    }
                    else
                    {
                        chkBank.Checked = false;
                    }

                 }
                 else
                 {
                     MessageBox.Show("Saldo Awal Tidak ada / Belum diinput..");
                     txtJumlah.Text = "0";

                 }
                 return ;
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
                 return ;
             }
         }

         private EventResponseMessage ctrlNavigation1_OnDelete()
         {
             EventResponseMessage ret = new EventResponseMessage();
             try
             {
                 if (MessageBox.Show("APakah benar akan menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                 {
                     Setor oSetor = new Setor();
                     oSetor.NoUrut = m_NoUrut;


                     SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);

                     oLogic.Hapus(m_NoUrut, E_JENIS_SETOR.E_SETOR_CP);
                     if (oLogic.IsError())
                     {
                         MessageBox.Show("Kesalahan Penghapusan data" + oLogic.LastError());
                     }
                     else
                     {

                         MessageBox.Show("Data SUdah dihapus ");
                     }
                 }
                 ret.ResponseStatus = true;


             }
             catch (Exception ex)
             {
                 ret.ResponseStatus = false;
                 MessageBox.Show(ex.Message);
             }
             return ret;

         }
    }
}
