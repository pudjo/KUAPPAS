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
    public partial class frmListSPP :  ChildForm
    {
        private List<SPP> m_lstSPP;
        private List<SPPRekening> m_DetailSPP;
        private int m_iIDDInas;
        private int mTahap;

        DataGridViewCellStyle _merah = new DataGridViewCellStyle();
        DataGridViewCellStyle _pink = new DataGridViewCellStyle();
        DataGridViewCellStyle _ijo = new DataGridViewCellStyle();

        DataGridViewCellStyle _ditolakStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _didiskusikanStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _diTerimaStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _diTangguhkanStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _baruStyle = new DataGridViewCellStyle();

        private bool mbOnlyDisplay;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        int m_iMode;
        int m_iKodeUk;

        public frmListSPP(int mode, bool bOnlyDisplay = false)
        {
            InitializeComponent();
            m_lstSPP = new List<SPP>();
            m_DetailSPP= new List<SPPRekening>() ;
            m_iIDDInas= 0 ;
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                m_iIDDInas = GlobalVar.Pengguna.SKPD;
            }
            m_iMode = mode;
            mbOnlyDisplay = bOnlyDisplay;
            if (bOnlyDisplay == true)
            {
                gridSPP.Top = 0;
                gridSPP.Left = 0;
                ctrlPanelPencarian1.Visible = false;
                ctrlJenisSPP1.Visible = false;
                txtNoSP2D.Visible = false;
                txtNoSPM.Visible = false;
                txtNoSPP.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label4.Visible = false;

            }

        }

        public int Tahap
        {
            set {  mTahap = value; }
            get { return mTahap; }
        }


        private void frListSPP_Load(object sender, EventArgs e)
        {
            
            ctrlHeader1.SetCaption("SPP/SPM/SP2D");
            cmdBKU.Visible = false;
            chkBelumBKU.Visible = false;
            cmdPiliSemua.Visible = false;
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                m_iIDDInas = GlobalVar.Pengguna.SKPD;
            }

            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_SKPD ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_PEMBANTU_SKPD ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_SUPPORT )
            {
                if (m_iMode == 0)
                {
                    ctrlHeader1.SetCaption("Pembuatan SPP");
                    cmdPiliSemua.Visible = false;
                    cmdBKU.Visible = false;
                    chkBelumBKU.Checked = false;

                    chkBelumBKU.Visible = false;
                }
                if (m_iMode == 4)
                {
                    ctrlHeader1.SetCaption("Pencatatan BKU SP2D");
                    ctrlPanelPencarian1.SetVisible(2,false );
                    cmdBKU.Visible = true;
                    chkBelumBKU.Visible = true ;
                    chkSPP.Enabled = false;
                    chkSPM.Enabled = false;
                    chkSPMTerima.Enabled = false;
                    chkSPMVerifikasi.Enabled = false;
                    chkSPMDitolakVerifikasi.Enabled = false;
                    chkTerbit.Enabled = false;
                    chkCair.Enabled = false;
                    chkCair.Checked = true;
                    cmdPiliSemua.Visible = true;

                } 

            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_PPK)
            {
                
                ctrlHeader1.SetCaption("Pembuatan SPM");
                cmdPiliSemua.Visible = false;
            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUDVERIFIKASISPM )
            {

                ctrlHeader1.SetCaption("Verifikasi  SPM");
                cmdPiliSemua.Visible = false;
                chkSPMTerima.Checked = true;
            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUDCETAKSP2D)
            {

                ctrlHeader1.SetCaption("Cetak  SP2D");
                cmdPiliSemua.Visible = false;
                chkTerbit.Checked = true;
            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_PPK)
            {
                ctrlHeader1.SetCaption("Pembuatan SP2D");
                cmdPiliSemua.Visible = false;
            }
            
           
            
            ctrlJenisSPP1.Clear();
            ctrlJenisSPP1.Create(true);
            ctrlJenisSPP1.ID = -1;

            if (m_iMode == 0)
                ctrlHeader1.SetCaption("Pembuatan SPP");
            if (m_iMode == 4)
            {
                ctrlHeader1.SetCaption("Pencatatan BKU SP2D");
                ctrlPanelPencarian1.SetVisible(2, false);
            } 
            ctrlPanelPencarian1.MustLoad = true;
            if (mbOnlyDisplay == false)
               ctrlPanelPencarian1.Create();
            ctrlPanelPencarian1.SetVisible(4, false);
            
            gridSPP.FormatHeader();

           


        }

        private void cmdAddSPP_Click(object sender, EventArgs e)
        {

        }

        private void cmdLoadSPP_Click(object sender, EventArgs e)
        {

        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {

        }

        private void cmdLoad_Click_1(object sender, EventArgs e)
        {

            

        }
        public  void LoadData ( int pIDDinas, DateTime tanggalAwal, DateTime tanggalAkhir, int JenisTanggal=1){
            try
            {


                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);
                m_iKodeUk = ctrlPanelPencarian1.UnitKerja;
                m_iIDDInas = ctrlPanelPencarian1.Dinas;
                p.IDDInas = m_iIDDInas;
                p.KodeUK = m_iKodeUk;
                p.LstStatus = new List<int>();
                
                p.Jenis = ctrlJenisSPP1.GetID();

                if (p.Jenis == 8)
                {
                    MessageBox.Show("Belum pilih jenis..");
                    return;
                }
               
                List<int> lstStatus = new List<int>();

                if (chkSPP.Checked == true)
                    lstStatus.Add(0);
                if (chkSPM.Checked == true)
                    lstStatus.Add(1);
                if (chkSPMTerima.Checked == true)
                    lstStatus.Add(2);
                if (chkSPMDitolakVerifikasi.Checked == true)
                    lstStatus.Add(10);
                if (chkSPMVerifikasi.Checked == true)
                    lstStatus.Add(6);
                if (chkTerbit.Checked == true)
                    lstStatus.Add(3);
                if (chkCair.Checked == true)
                    lstStatus.Add(4);

                if (chkSPMVerifikasi.Checked == true)
                    lstStatus.Add(6);
                p.Status = -1;
                if (lstStatus.Count == 0)
                {
                    MessageBox.Show("Belum ada status dipilih");
                    return;


                }
                p.LstStatus = lstStatus;
                if (GlobalVar.gListSPP == null)
                {
                    GlobalVar.gListSPP = new List<SPP>();

                }
               
                
          
                
                
   
                p.Jenis = ctrlJenisSPP1.GetID();
                p.NoSP2D = txtNoSP2D.Text;
                p.NoSPM = txtNoSPM.Text;
                p.NoSPP = txtNoSPP.Text;

                if (chkSPP.Checked == true)
                {
                    p.LstStatus.Add(0);


                }
                if (chkSPM.Checked == true)
                {
                    p.LstStatus.Add(1);


                }
           
               // MessageBox.Show("toDatabase");
                // Get Data From Database
                p.TanggalAwal = ctrlPanelPencarian1.TanggalAwal;
                p.TanggalAkhir = ctrlPanelPencarian1.TanggalAkhir;
        
                GlobalVar.gListSPP = oLogic.GetSPP(p);
                m_lstSPP = new List<SPP>();

               // MessageBox.Show("end toDatabase");

                m_lstSPP = GlobalVar.gListSPP;//.FindAll(spp=>)
                if (m_lstSPP == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return;
                }
                //MessageBox.Show("Get Detail");
                if (GlobalVar.gListSPPRekening == null)
                {
                    GlobalVar.gListSPPRekening = new List<SPPRekening>();
                }
                //if (GlobalVar.gListSPPRekening.FindAll(x => x.IDDinas == m_iIDDInas).Count == 0)
                //{
                //    GlobalVar.gListSPPRekening = oLogic.GetSPPDetail(m_iIDDInas);
                //}
                m_DetailSPP= new List<SPPRekening>() ;
               // MessageBox.Show("End  Detail");
              //  m_DetailSPP = GlobalVar.gListSPPRekening.FindAll(x => x.IDDinas == m_iIDDInas);



                gridSPP.Rows.Clear();

                int iRow = 0;
                decimal cJumlah = 0L;
                string sAction = "";
                string Status = "";
                if (m_iMode == 4)
                {
                    sAction = "Catat BKU";
                }
                else
                {
                    sAction = "Detail";
                }
                
                foreach (SPP spp in m_lstSPP)
                {


                    if (spp.diBKU > 0)
                    {
                        Status = "Sudah BKU";

                    }
                    else
                    {
                        Status = "Belum BKU";
                    }

                    string[] row = { spp.NoUrut.ToString(), sAction, "false", spp.NoSPP.Replace(Environment.NewLine ,"") +  " " +"Tanggal :" + spp.dtSPP.ToString("dd MMM"),
                                        spp.NoSPM.Replace(Environment.NewLine ,"") +  " "+ "Tanggal :" +  spp.dtSPM.ToString("dd MMM"), 
                                        spp.NoSP2D.Replace(Environment.NewLine ,"") +  " " + "Tanggal :" +   spp.dtTerbit.ToString("dd MMM"), "",
                                        spp.Keterangan, spp.Jumlah.ToRupiahInReport(),Status };

                    if (chkBelumBKU.Checked == true)
                    {
                        if (spp.diBKU ==0)
                        {
                            gridSPP.Rows.Add(row);
                            cJumlah = cJumlah + spp.Jumlah;        
                            iRow++;
                        }
                    }
                    else
                    {
                        gridSPP.Rows.Add(row);
                        cJumlah = cJumlah + spp.Jumlah;
                        iRow++;
                    }
                    

                    if (spp.Status == 1)
                        gridSPP.Rows[iRow-1].DefaultCellStyle.BackColor = Color.AliceBlue;
                    if (spp.Status == 2)
                        gridSPP.Rows[iRow-1].DefaultCellStyle.BackColor = Color.LightBlue;

                    if (spp.Status == 3)
                        gridSPP.Rows[iRow-1].DefaultCellStyle.BackColor = Color.AntiqueWhite;// Red;
                    if (spp.Status == 4)
                    {
                        if (spp.diBKU > 0)
                        {
                            if (chkBelumBKU.Checked==false)
                              gridSPP.Rows[iRow-1].DefaultCellStyle.BackColor = Color.LightSalmon;// PaleVioletRed;// IndianRed;// Red;
                        }
                        else
                        {
                            gridSPP.Rows[iRow-1].DefaultCellStyle.BackColor = Color.LightPink;// PaleVioletRed;// IndianRed;// Red;
                        }
                    }
                    if (spp.Status == 10)
                        gridSPP.Rows[iRow - 1].DefaultCellStyle.BackColor = Color.DeepPink;
                    if (spp.Status == 6)
                        gridSPP.Rows[iRow - 1].DefaultCellStyle.BackColor = Color.DodgerBlue ;
                        

                    
                   


                }
                txtJumlah.Text = cJumlah.ToRupiahInReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
       

        private void cmdAddSPP_Click_1(object sender, EventArgs e)
        {
            frmSPP fSPP = new frmSPP();
            fSPP.ShowDialog();
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
                    
                    oSPP = m_lstSPP.FirstOrDefault(x => x.NoUrut == lNoUrut);
                    if (oSPP == null)
                    {
                        MessageBox.Show("Ada kesalahan. Sila hubung admin.. ");
                    }
                }
                //        ctrlRekeningKegiatan1.CreateSPP(oSPP);
                if (e.ColumnIndex == 1)
                {
                    if (m_iMode == 4)
                    {
                        SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                        oLogic.CatatBKU(oSPP);
                        
                        MessageBox.Show("Catat BKU Selesai...");

                    }
                    else
                    {
                        frmSPP fSPP = new frmSPP();
                        fSPP.SetModeForm(0);

                        //if (oSPP == null)
                        //{
                        //    fSPP.SetID(oSPP.NoUrut);
                        //}
                        //else
                        //{
                            List<SPPRekening> lstDetail = new List<SPPRekening>();
                            //SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                            //lstDetail = m_DetailSPP.FindAll(x => x.NoUrut == oSPP.NoUrut);
                            //oLogic.GetDetail()
                            //if (lstDetail.Count == 0)
                            //{
                                SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                                lstDetail = oSPPLogic.GetDetail(oSPP.NoUrut);

                           //// }
                                if (lstDetail != null)
                                {
                                    oSPP.Rekenings = lstDetail;
                                }
                            fSPP.SetSPP(oSPP);

                      //  }

                        //fSPP.SetID(lNoUrut);
                        fSPP.Show();
                    }

                }
                else
                {
                    return;

                }
                
            }
        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_iIDDInas = pIDSKPD;
        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            try
            {
                m_iIDDInas = ctrlPanelPencarian1.Dinas;
                DateTime tanggalAwal = ctrlPanelPencarian1.TanggalAwal;
                DateTime tanggalAkhir = ctrlPanelPencarian1.TanggalAkhir;

             
               
                LoadData(m_iIDDInas, tanggalAwal, tanggalAkhir);
                //MessageBox.Show("End of load");
                int jumlah = m_lstSPP.Count;
                lblJumlah.Text = jumlah.ToString() ;
                lblPencarian.Visible = true;
                txtCari.Visible = true;
                cmdCari.Visible = true;
                cmdCariLagi.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void ctrlPanelPencarian1_OnAdd()
        {
             frmSPP fSPP = new frmSPP();
             fSPP.OnNew();
             fSPP.ShowDialog();
        
        }

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {

        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridSPP.Rows)
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
                    gridSPP.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridSPP.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void cmdPiliSemua_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridSPP.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    row.Cells[2].Value = true;
                }
            }
        }

        private void cmdBKU_Click(object sender, EventArgs e)
        {
            SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                
            for (int row = 0; row < gridSPP.Rows.Count; row++)
            {
                bool bDipilih = Convert.ToBoolean(gridSPP.Rows[row].Cells[2].Value);
                if (bDipilih == true)
                {
                    SPP oSPP = new SPP();
                    string sNoUrut = gridSPP.Rows[row].Cells[0].Value.ToString();
                    
                    long lNoUrut = DataFormat.GetLong(sNoUrut);

                    
                    oSPP = m_lstSPP.FirstOrDefault(x=>x.NoUrut==lNoUrut);
                    if (oSPP == null)
                    {
                        MessageBox.Show("Kesalahan . Coba Klik tombol BKU pada kolom pertama baris ini. ");
                       // return;

                    }
                    
                    
                    oLogic.CatatBKU(oSPP);
                    
                }
            }
            MessageBox.Show("Catat BKU Selesai...");


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblPencarian_Click(object sender, EventArgs e)
        {

        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }
    }
}
