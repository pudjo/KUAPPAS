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
using System.Globalization;


namespace KUAPPAS.Bendahara
{
    public partial class frmPenerimaan : Form
    {
        private int m_IDDInas;
        private int m_KodeUK;
        private long m_nNourut;
        private bool m_bLoad;
        private int m_TahapAnggaran;
        private bool m_bNew ;
        private long m_NourutSetor;
        private Single  m_iStatus;

        public frmPenerimaan()
        {
            InitializeComponent();
            m_nNourut = 0;
            m_IDDInas = 0;
            m_TahapAnggaran = 0;
        }

        private void frmSTS_Load(object sender, EventArgs e)
        {
            gridPenerimaan.FormatHeader();
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlDinas1.Create();
                m_IDDInas = GlobalVar.Pengguna.SKPD;
                ctrlDinas1.SetID(m_IDDInas);

            }


        }
        public bool SetSTS(STS oSTS)
        {
            try
            {
                m_bLoad = true;

                m_IDDInas = oSTS.IDDinas;
                m_nNourut= oSTS.NoUrut;
                ctrlDinas1.Create();
                ctrlDinas1.SetID(oSTS.IDDinas, oSTS.KodeUK);
                //ctrlJenisPenerimaan1.Create();
                //ctrlJenisPenerimaan1.SetID((int)oSTS.Jenis);
                if (oSTS.Jenis == (int)E_JENIS_PENERIMAAN.E_JENIS_PENERIMAAN_KE_BUD)
                {
                    chkTransferLangsungKasda.Checked = true;
                }
                else
                {
                    chkTransferLangsungKasda.Checked = false;
                }
                 
                txtKeterangan.Text = oSTS.Keterangan;
                txtNoBukti.Text = oSTS.NoSTS;
                txtPenyetor.Text = oSTS.Penyetor;
                ctrlSKPDSKPD1.Create(oSTS.IDDinas);
                ctrlSKPDSKPD1.SetID(oSTS.NoSKR);
                ctrlVia1.SetBank(oSTS.Bank);
                Tanggal.Tanggal = oSTS.TanggalSTS;

                m_NourutSetor=oSTS.NoUrutSetor;
                m_iStatus= oSTS.Status;



            //    IsiRekening();
                decimal cJumlah = 0L;
                txtJumlah.Text = cJumlah.ToRupiahInReport();
                ctrlComboAnggaran1.Create(m_IDDInas,0,0, 0, 0, 0, 2, 1);

                foreach (STSRekening sr in oSTS.Rekenings)
                {
                    //for (int idx = 0; idx < gridPenerimaan.Rows.Count; idx++)
                    //{
                    //    if (gridPenerimaan.Rows[idx].Cells[0].Value !=null){

                      //  if (DataFormat.GetLong(gridPenerimaan.Rows[idx].Cells[0].Value.ToString()) == sr.IDRekening)
                       // {
                    string[] row = { sr.IDRekening.ToKodeRekening(GlobalVar.ProfileRekening), sr.Nama,"" , sr.Jumlah.ToRupiahInReport(),sr.IDRekening.ToString() };
                    gridPenerimaan.Rows.Add(row);

                            //gridPenerimaan.Rows[idx].Cells[3].Value = sr.Jumlah.ToRupiahInReport();
                            cJumlah = cJumlah + sr.Jumlah;
                        //}
                    //    }

                    //}

                }
                txtJumlah.Text = cJumlah.ToRupiahInReport();
                //if (oSTS.Status > 0)
                //{
                //    ctrlNavigation1.Enabled = false;

                //}

                ctrlFooter1.IDCrt = oSTS.idcrt;
                ctrlFooter1.WaktuBuat = oSTS.tcrt;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        private void IsiRekening()
        {
            gridPenerimaan.Rows.Clear();
            List<TAnggaranRekening> lta = new List<TAnggaranRekening>();
            TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic((int)GlobalVar.TahunAnggaran);
            if (m_IDDInas == 0)
            {
                return;
            }
            int idUrusan = DataFormat.GetInteger(DataFormat.GetString(m_IDDInas).Substring(0, 3));

            lta = oLogic.Get((int)GlobalVar.TahunAnggaran, m_IDDInas,idUrusan, 0, 0, 1, 0,m_TahapAnggaran,0,0);
            if (lta != null)
            {
                foreach (TAnggaranRekening ta in lta)
                {
                    string[] row = { ta.IDRekening.ToString(), ta.Nama, ta.JumlahDPA.ToRupiahInReport(), "0" };
                    gridPenerimaan.Rows.Add(row);

                }
            }
        }

        private void ctrlComboAnggaran1_OnChanged(long pID)
        {
            TAnggaranRekening ta = new TAnggaranRekening();
            //if (pID != null)
            //{
                ta = ctrlComboAnggaran1.GetAnggaranRekening();
                if (ta != null)
                {

                    txtIDRekening.Text = ta.IDRekening.ToKodeRekening(GlobalVar.ProfileRekening);
                    txtNamaRekening.Text = ta.Nama;

                    AddToList();
                }
                else
                {
                    txtIDRekening.Text = "";
                    txtNamaRekening.Text = "";
                }
           // }
        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            frmCariRekening fcr = new frmCariRekening();
            fcr.ShowDialog();
            if (fcr.IsOK() == true)
            {
                Rekening oRekening = fcr.GetRekening();
                txtIDRekening.Text = oRekening.ID.ToString();
                txtNamaRekening.Text = oRekening.Nama;

            }
        }

        private void cmdAddToList_Click(object sender, EventArgs e)
        {
        }
        private void AddToList(){
            try
            {
                long idRekeningToAdd = DataFormat.GetLong(txtIDRekening.Text.Replace(".", ""));
                bool bfound = false;
                for (int idx = 0; idx < gridPenerimaan.Rows.Count; idx++)
                {
                    //if (gridPenerimaan.Rows[idx].Cells[0].Value != null)
                    //{
                    //    if (DataFormat.GetLong(gridPenerimaan.Rows[idx].Cells[0].Value.ToString().Replace(".", "")) == idRekeningToAdd)
                    //    {
                    //        bfound = bfound || true;
                    //    }
                    //}

                }
                if (bfound == false)
                {

                    string[] row = { txtIDRekening.Text, txtNamaRekening.Text, "0", "0", txtIDRekening.Text.Replace(".", "") };

                    gridPenerimaan.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void gridPenerimaan_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridPenerimaan.Rows.Count)
            {
                if (e.ColumnIndex == 3)
                {
                    decimal cJumlah = 0L;
                    decimal nilai = DataFormat.GetDecimal (gridPenerimaan.Rows[e.RowIndex].Cells[3].Value);
                  //  textBox1.Text = String.Format(CultureInfo.GetCultureInfo("id-ID"), "{0:C2}", nilai);

                    decimal dj = DataFormat.GetDecimal(gridPenerimaan.Rows[e.RowIndex].Cells[3].Value); 
                    //(DataFormat.GetString(gridPenerimaan.Rows[e.RowIndex].Cells[3].Value)).FormatUangReportKeDecimal();
                  //  textBox1.Text = dj.ToString();
                    gridPenerimaan.Rows[e.RowIndex].Cells[3].Value = dj.ToRupiahInReport();
                    for (int irow = 0; irow < gridPenerimaan.Rows.Count; irow++)
                    {

                  //      DataFormat.FormatUangReportKeDecimal(gridRekening.Rows[idx].Cells[5].Value);

                        dj = DataFormat.FormatUangReportKeDecimal(gridPenerimaan.Rows[irow].Cells[3].Value);//).FormatUangReportKeDecimal(); 
                        if (dj != 0)
                        {
                            //textBox2.Text = dj.ToString();
                            cJumlah = cJumlah + dj;
                        }
                    }
                    txtJumlah.Text = cJumlah.ToRupiahInReport();
                }
            }

        }

        private bool CekInput()
        {
            if (GlobalVar.Pengguna.Status >= 2)
            {
                MessageBox.Show("Pengguna sudah di nonaktifkan untuk input");
                return false;

            }
            if (ctrlDinas1.PilihanValid == false)
            {
                return false;
            }
            if (GlobalVar.Pengguna.Status >= 2)
            {
                MessageBox.Show("Pengguna sudah di nonaktifkan untuk input");
                return false;

            }

            if (txtNoBukti.Text.Trim().Length == 0)
            {
                MessageBox.Show("No Bukti/STS belum diisi ");
                return false;

            }
            if (txtKeterangan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Keterangan belum diisi ");
                return false;

            }

           

            if (txtJumlah.Text.UangToDecimal()== 0)
            {
                MessageBox.Show("Belum mengisi Detail penerimaan..");
                return false;

            }

            return true;
        }
        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage ret = new EventResponseMessage();
            
            ret.ResponseStatus = true;
            try
            {
                STSLogic oLogic = new STSLogic(GlobalVar.TahunAnggaran);

                STS oSTS = new STS();
                m_bLoad = true;
                if (CekInput() == false)
                {
                    ret.ResponseStatus = false;
                    return ret;
                }
                oSTS.IDDinas = m_IDDInas;
                oSTS.KodeUK = m_KodeUK;
                oSTS.NoUrut = m_nNourut;
                oSTS.NamaFile = "";
                if (chkTransferLangsungKasda.Checked == true)
                {
                    oSTS.Jenis = (int)E_JENIS_PENERIMAAN.E_JENIS_PENERIMAAN_KE_BUD;
                    oSTS.Bank = 1;
                }
                else
                {
                    oSTS.Jenis = (int)E_JENIS_PENERIMAAN.E_JENIS_PENERIMAAN_KE_REK_BENDAHARA;
                    oSTS.Bank = 0;
                }

                
                oSTS.Keterangan = txtKeterangan.Text;
                oSTS.KodeKategori = m_IDDInas.ToString().ToKodeKategori();
                oSTS.KodeUrusan = m_IDDInas.ToString().ToKodeUrusan();
                oSTS.KodeSKPD = m_IDDInas.ToString().ToKodeSKPD();

                oSTS.NoSTS = txtNoBukti.Text;
                oSTS.Penyetor = txtPenyetor.Text;
                oSTS.NoSKR = ctrlSKPDSKPD1.GetID();
               

                oSTS.TanggalSTS = Tanggal.Tanggal;
                oSTS.NamaBank = "";
                oSTS.NoRekening = "";
                oSTS.Alamat = "";
                oSTS.NoBukti = txtNoBukti.Text;
                oSTS.JabatanPenyetor = "";
                oSTS.InstitusiPenyetor = 0;
                oSTS.NPWP = "";
                oSTS.Tahun = GlobalVar.TahunAnggaran;
                oSTS.idcrt = GlobalVar.Pengguna.ID;
                oSTS.idupdate = GlobalVar.Pengguna.ID;
                //oSTS.NoUrutSetor=

                //     SSQL = "UPDATE tSTS SET inourutSetor =@NOURUTSETOR, iStatus= 3 where inourut =@NOURUTSTS";
                //DBParameterCollection paramUpdateSTS = new DBParameterCollection();
                //paramUpdateSTS.Add(new DBParameter("@NOURUTSETOR", str.NoUrut));
                //paramUpdateSTS.Add(new DBParameter("@NOURUTSTS", stsdisetor.NoUrut));


                oSTS.Rekenings = new List<STSRekening>();
                //List<STSRekening> lstSTSRekening = new List<STSRekening>();
                decimal cJumlah = 0L;

                txtJumlah.Text = cJumlah.ToRupiahInReport();

                for (int irow = 0; irow < gridPenerimaan.Rows.Count-1; irow++)
                {
                    STSRekening sr = new STSRekening();
                    sr.IDRekening = DataFormat.GetLong(gridPenerimaan.Rows[irow].Cells[4].Value);
                    //sr.Jumlah = DataFormat.FormatUangReportKeDecimal(gridPenerimaan.Rows[irow].Cells[3].Value);
                    sr.Jumlah = DataFormat.GetString(gridPenerimaan.Rows[irow].Cells[3].Value).FormatUangReportKeDecimal();
                    if (sr.IDRekening == 0)
                    {
                        sr.IDRekening = DataFormat.GetLong(DataFormat.GetString(gridPenerimaan.Rows[irow].Cells[0].Value).Replace(".",""));
                        if (sr.IDRekening == 0)
                        {
                            MessageBox.Show("Kesalahan kode Rekening. Sila ulangi pilih lagi..");
                            ret.ResponseStatus = false;
                            return ret;
                        }
                    }
                    
                    cJumlah = cJumlah + sr.Jumlah;
                    if (sr.Jumlah>0)
                        oSTS.Rekenings.Add(sr);
                }
                oSTS.Jumlah = cJumlah;
                long nourutHasil = 0;
                 txtJumlah.Text = cJumlah.ToRupiahInReport();
                 nourutHasil = oLogic.Simpan(oSTS);

                if (nourutHasil == 0)
                {
                    MessageBox.Show(oLogic.LastError());
                    


                } else {
                    m_bNew = false;
                    m_nNourut = nourutHasil;
                    string sPesan = "Penyimpanan Berhasil.";
                    if (chkTransferLangsungKasda.Checked == false)
                    {
                        sPesan= sPesan+" ***** Penerimaan di terima Bendahara Penerimaan.Jangan lupa untuk buat Setoran..****";
                    }
                    MessageBox.Show(sPesan);


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret.ResponseStatus = false;
                
            }

            return ret;

        }
        
        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            
            ctrlDinas1.Create();

            txtJumlah.Text = "0";
            txtKeterangan.Text = "";
            txtNoBukti.Text = "";
            txtPenyetor.Text = "";
            m_nNourut = 0;
            chkTransferLangsungKasda.Checked = false;
            
            Tanggal.Tanggal = DateTime.Now.Date;
            gridPenerimaan.Rows.Clear();




            //if (GlobalVar.Pengguna.IsUserDinas == 1)
            //{
                int iPPKD = (int)ctrlDinas1.PPKD();
                if (m_IDDInas > 0)
                {
                    int idUrusan = DataFormat.GetInteger(m_IDDInas.ToString().Substring(0, 3));
                    ctrlComboAnggaran1.Create(m_IDDInas, 0, 0, 0, 0, 0, m_TahapAnggaran,1);
                }
         //   }
            return ret;

        }
        public void SetNew()
        {

            ctrlNavigation1.SetNew();
        }
        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_IDDInas = pIDSKPD;
            m_KodeUK = pIDUK;
            m_TahapAnggaran = ctrlDinas1.GetTahapAnggaran();
            if (m_bLoad == false)
            {
                int iPPKD = (int)ctrlDinas1.PPKD();
                int idUrusan = 0;// DataFormat.GetInteger(m_IDDInas.ToString().Substring(0, 3));
                ctrlComboAnggaran1.Create(m_IDDInas, idUrusan, 0, 0, 0,0,m_TahapAnggaran, 1);
            }
        }

        private void Tanggal_OnChanged(DateTime pTanggal)
        {
            ctrlSKPDSKPD1.Create(m_IDDInas);

        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlComboAnggaran1_Load(object sender, EventArgs e)
        {

        }

        private void gridPenerimaan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            try
            {
                if (MessageBox.Show("Apakah benar akan menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return ret;
                }
                if (m_nNourut == 0)
                {
                    MessageBox.Show("Tidak ada penerimaan yang dihapus");
                    ret.ResponseStatus = false;
                    return ret;

                }
                STS oSTS = new STS();
                m_bLoad = true;

                oSTS.IDDinas = m_IDDInas;
                oSTS.KodeUK = m_KodeUK;
                oSTS.NoUrut = m_nNourut;
                STSLogic oLogic = new STSLogic(GlobalVar.TahunAnggaran);
                if (oLogic.Hapus(m_nNourut) == true)
                {
                    MessageBox.Show("Berhasil menghapus dat apenerimaan.");
                
                    m_nNourut = 0;
                   
                } else{

                    MessageBox.Show(oLogic.LastError());

//                    m_nNourut = 0;
                    ret.ResponseStatus = false;

                }
                return ret;
            }
            catch (Exception ex)
            {
                ret.ResponseStatus = false;
                MessageBox.Show("Gagal melakukan penghapusan penerimaan." + ex.Message);
                return ret;
            }

            
        }

        private void chkTransferLangsungKasda_CheckedChanged(object sender, EventArgs e)
        {
            
        }

    }
}
