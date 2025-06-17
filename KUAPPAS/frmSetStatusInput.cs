using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using BP;
using Formatting;
using BP.Bendahara;
namespace KUAPPAS
{
    public partial class frmSetStatusInput : Form

    {
        private int m_currentStatus = 0;
        private bool m_bStatusInput;
        private bool m_iStatusAK;
        public frmSetStatusInput()
        {
            InitializeComponent();
            m_bStatusInput = true;
            m_iStatusAK = true;
        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }

        private void frmSetStatusInput_Load(object sender, EventArgs e)
        {


            gridTahap.FormatHeader(false);
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            LoadTahapanAnggaran();


        }
        private void LoadTahapanAnggaran()
        {
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            List<TahapanAnggaran> lst = new List<TahapanAnggaran>();
            gridTahap.Rows.Clear();
            lst = oLogic.Get();

            int i = 0;
            foreach (TahapanAnggaran ta in lst)
            {
                string Tahap = GetNamaTahap(ta.Tahap);
                string sSatusInput  =ta.StatusInput==9? "Terkunci":"Tidak Terkuncu";
                string[] row = { ta.NamaDinas, Tahap, sSatusInput ,ta.IDDInas.ToString()};
                gridTahap.Rows.Add(row);
               if (ta.StatusInput==9)
                   gridTahap.Rows[i].Cells[2].Style.BackColor = Color.Red;
               //else 
               i++;

            }

        }
        private string GetNamaTahap(int iTahap)
        {
            string sret = "";
            switch (iTahap)
            {
                case 1:
                    sret = "Input RKA";
                    break;
                case 2:
                    sret = "DPA";
                    break;

                case 3:
                    sret = "Input Pergeseran";
                    break;

                case 4:
                    sret = "Input Anggaran Perubahan ";
                    break;

                case 5:
                    sret = "Input Penyempurnaan Anggaran Perubahan ";
                    break;

            }
            return sret;

            
        }


        private void cmdKunciRKA_Click(object sender, EventArgs e)
        {
            
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            int skpd = 0;
            if (chkSemuaDinas.Checked == true)
                skpd = 0;
            else
                skpd = ctrlSKPD1.GetID();
            if (oLogic.SetTahapRKA(skpd, GlobalVar.TahunAnggaran, 2) == false)
            {
                MessageBox.Show(oLogic.LastError());
            }
            else
            {
                MessageBox.Show("Data DPA sudah dikunci.");
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            TahapanAnggaran tA = new TahapanAnggaran();
            tA = oLogic.GetByDinas(pIDSKPD, (int)GlobalVar.TahunAnggaran);
            m_currentStatus = tA.Tahap ;
            cmdKunciRKA.Enabled = false;
            cmdInputPenyempurnaan.Enabled = false;
            switch (tA.Tahap)
            {
                case 0:
                    rbRKA.Checked = false;
                    cmdKunciRKA.Enabled = false;
                    rbDPA.Checked = false;
                    rbPenyempurnaan.Checked = false ;
                    rbRKAPErubahan.Checked = false;
                    rbRKAPErubahan.Checked = false;
                    rbRKAPErubahan.Checked = false;
                    break;
                case 1:
                    rbRKA.Checked = true;
                    cmdKunciRKA.Enabled = true;
                    //cmdKucniInput.Enabled = true;
                    //cmdKucniInput.Text = "Kunci Input RKA";
                    
                    break;
               case 2:
                    rbDPA.Checked = true;
                    cmdInputPenyempurnaan.Enabled = true ;
                    cmdInputRKAP.Enabled = true ;
                    break;
                case 3:
                    rbPenyempurnaan.Checked = true;
                    break;
                case 4:
                    rbRKAPErubahan.Checked = true;
                    break;
                case 5:
                    rbRKAPErubahan.Checked = true;
                    break;
                case 6:
                    rbRKAPErubahan.Checked = true;
                    break;
                case 9:
                    cmdKucniInput.Text = "Buka Input RKA";
                    break;



            }

            if (ctrlSKPD1.GetStatusInput() == false)
            {
                cmdKucniInput.Text = "Buka Kunci Input";
                m_bStatusInput = false;
            }
            else
            {
                m_bStatusInput = true;
                cmdKucniInput.Text = "Kunci Input";
            }

            if (tA.StatusAnggaranKas <= 1 )
            {
                button1.Text = "Kunci Input Anggaran Kas";
                m_iStatusAK = true;
            }
            else
            {

                button1.Text = "Buka Kunci Input Anggaran Kas";
                m_iStatusAK = false;

            }
        }

        private void cmdInputPenyempurnaan_Click(object sender, EventArgs e)
        {
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            if (oLogic.SetTahapRKA(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran, 3) == false)
            {
                MessageBox.Show(oLogic.LastError());
            }
            else
            {
                MessageBox.Show("Silakan Input sudah dikunci.");
            }
            LoadTahapanAnggaran();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int iskpd = ctrlSKPD1.GetID();
             List<int> lstIDSKPD = new List<int>();

            if (chkSemuaDinas.Checked== true)
            {

                List<SKPD> lstSKPD = new List<SKPD>();
                SKPDLogic oSKPDLogic = new SKPDLogic((int)GlobalVar.TahunAnggaran);
                lstSKPD = oSKPDLogic.Get((int)GlobalVar.TahunAnggaran);
                if (lstSKPD != null)
                {
                    foreach (SKPD oskpd in lstSKPD)
                    {
                        lstIDSKPD.Add(oskpd.ID);
                    }
                }
                else
                {
                    MessageBox.Show(oSKPDLogic.LastError());
                    return;

                }

            }
            else
            {

                lstIDSKPD.Add(iskpd);
            }
            AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
            if (oLogic.PersiapkanAKPerubahan(3, 4, lstIDSKPD) == true)
            {
                MessageBox.Show("Anggaran Kas Perubahan sudah disiapkan.... ");

            }
            else
            {
                MessageBox.Show(oLogic.LastError());
            }

        }

        private void cmdInputRKAP_Click(object sender, EventArgs e)
        {
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            if (oLogic.SetTahapRKA(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran, 4) == false)
            {
                MessageBox.Show(oLogic.LastError());
            }
            else
            {
                MessageBox.Show("Silakan RKA Perubahan.");
            }
            LoadTahapanAnggaran();
        }

        private void cmdTetapkanABT_Click(object sender, EventArgs e)
        {
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            if (oLogic.SetTahapRKA(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran, 5) == false)
            {
                MessageBox.Show(oLogic.LastError());
            }
            else
            {
                MessageBox.Show("Silakan RKA Perubahan.");
            }
        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            if (oLogic.SetTahapRKA(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran, 1) == false)
            {
                MessageBox.Show(oLogic.LastError());
            }
            else
            {
                MessageBox.Show("Silakan input RKA.");
            }
        }

        private void cmdKucniInput_Click(object sender, EventArgs e)
        {
                 TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);

        
              //    oLogic.BukaKunciInput(0, GlobalVar.TahunAnggaran);
                 if (cmdKucniInput.Text.Contains("Buka") == true)
                 {
                     oLogic.BukaKunciInput(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran);
                     m_bStatusInput = true ;
                     cmdSimpan.Text = "Kunci Input";
                 }
                 else
                 {
                     oLogic.KunciInput(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran);
                     m_bStatusInput = false;
                     cmdSimpan.Text = "Buka Kunci Input";
                 }
                   
                    

            
            LoadTahapanAnggaran();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_iStatusAK == true )
            {
                if (MessageBox.Show("Apakah benar akan membolehkan perubahan data Anggaran Kas sehingga pengguna tidak bisamerubahlagi?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
                    if (oLogic.SetStatusAK(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran, 2) == false)
                    {
                        MessageBox.Show(oLogic.LastError());
                    }
                    else
                    {
                        MessageBox.Show("Data Anggaran Kas Bisa di input .");
                        m_iStatusAK = true;
                        button1.Text = "Kunci Input Anggaran Kas";
                    }
                }

            }
            else
            {
                
                if (MessageBox.Show("Apakah benar akan mengunci data Anggaran Kas sehingga pengguna tidak bisamerubahlagi?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
                    if (oLogic.SetStatusAK(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran, 1) == false)
                    {
                        MessageBox.Show(oLogic.LastError());
                    }
                    else
                    {
                        MessageBox.Show("Data Anggaran Kas sudah dibuka kunci input.");
                        m_iStatusAK = false;

                        button1.Text = "Buka Kunci Input Anggaran Kas";
                    }
                }
            }
        }

        private void ctrlSKPD1_OnChanged(int pID)
        {
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            TahapanAnggaran tA = new TahapanAnggaran();
            tA = oLogic.GetByDinas(pID, (int)GlobalVar.TahunAnggaran);
            m_currentStatus = tA.Tahap;
            cmdKunciRKA.Enabled = false;
            cmdInputPenyempurnaan.Enabled = false;
            switch (tA.Tahap)
            {
                case 0:
                    rbRKA.Checked = false;
                    cmdKunciRKA.Enabled = false;
                    rbDPA.Checked = false;
                    rbPenyempurnaan.Checked = false;
                    rbRKAPErubahan.Checked = false;
                    rbRKAPErubahan.Checked = false;
                    rbRKAPErubahan.Checked = false;
                    break;
                case 1:
                    rbRKA.Checked = true;
                    //cmdKunciRKA.Enabled = true;
                    //cmdKucniInput.Enabled = true;
                   // cmdKucniInput.Text = "Kunci Input RKA";

                    break;
                case 2:
                    rbRKA.Checked = false;
                    rbDPA.Checked = true;
                    cmdInputPenyempurnaan.Enabled = true;
                    //cmdInputRKAP.Enabled = true;
                    break;
                case 3:
                    
                    rbPenyempurnaan.Checked = true;
                    cmdInputRKAP.Enabled = true;

                    break;
                case 4:
                    rbRKAPErubahan.Checked = true;
                    break;
                case 5:
                    rbRKAPergeseranPerubahan.Checked = true;
                    break;
                case 6:
                    rbRKAPErubahan.Checked = true;
                    break;
                case 9:
                    //cmdKucniInput.Text = "Buka Input RKA";
                    break;



            }
         

            if (ctrlSKPD1.GetStatusInput() == false)
            {
                cmdKucniInput.Text = "Buka Kunci Input";
                m_bStatusInput = false;
            }
            else
            {
                m_bStatusInput = true;
                cmdKucniInput.Text = "Kunci Input";
            }

            if (tA.StatusAnggaranKas <= 1)
            {
                button1.Text = "Kunci Input Anggaran Kas";
                m_iStatusAK = true;
            }
            else
            {

                button1.Text = "Buka Kunci Input Anggaran Kas";
                m_iStatusAK = false;

            }
        }

        private void btnMulaiTahunANggaranBaru_Click(object sender, EventArgs e)
        {
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            if (oLogic.SetTahapRKA(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran, 1) == false)
            {
                MessageBox.Show(oLogic.LastError());
            }
            else
            {
                MessageBox.Show("Silakan input RKA.");
            }
        }

        private void cmdBukaKunci_Click(object sender, EventArgs e)
        {

            SetTahap(1);
        }
        private void SetTahap(int iTahap)
        {
            if (MessageBox.Show("Apakah yakin akan set Tahap?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
                int skpd = 0;
                if (chkSemuaDinas.Checked == true)
                    skpd = 0;
                else
                    skpd = ctrlSKPD1.GetID();
                if (oLogic.SetTahapRKA(skpd, GlobalVar.TahunAnggaran, iTahap) == false)
                {
                    MessageBox.Show(oLogic.LastError());
                }
                else
                {
                    MessageBox.Show("Input Data di buka.");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SetTahap(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            SetTahap(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            SetTahap(4);
        }

        private void cmdBukaInput_Click(object sender, EventArgs e)
        {
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);



            
                if (chkSemuaDinas.Checked== true)
                {
                    oLogic.BukaKunciInput(0, GlobalVar.TahunAnggaran);
                    m_bStatusInput = true;
                  //  cmdSimpan.Text = "Kunci Input";

                } else {
                        oLogic.BukaKunciInput(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran);
                        m_bStatusInput = true;
                  //      cmdSimpan.Text = "Kunci Input";
                }
            
           
            LoadTahapanAnggaran();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            int iskpd = ctrlSKPD1.GetID();
            
            List<int> lstIDSKPD = new List<int>();
            if (iskpd == 0)
            {

                List<SKPD> lstSKPD = new List<SKPD>();
                SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
                lstSKPD = oSKPDLogic.Get(GlobalVar.TahunAnggaran);
                foreach (SKPD oskpd in lstSKPD)
                {
                    lstIDSKPD.Add(oskpd.ID);
                }

            }
            else
            {

                lstIDSKPD.Add(iskpd);
            }
            AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
            if (oLogic.PersiapkanAKPerubahan(2, 3, lstIDSKPD) == true)
            {
                MessageBox.Show("Anggaran Kas Perubahan sudah disiapkan.... ");

            }
            else
            {
                MessageBox.Show(oLogic.LastError());
            }
            
        }

        private void ctrlSKPD1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            if (oLogic.SetTahapRKA(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran, 5) == false)
            {
                MessageBox.Show(oLogic.LastError());
            }
            else
            {
                MessageBox.Show("Silakan  DPA Pergeseran Perubahan.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int iskpd = ctrlSKPD1.GetID();
            List<int> lstIDSKPD = new List<int>();
            if (iskpd == 0)
            {

                List<SKPD> lstSKPD = new List<SKPD>();
                SKPDLogic oSKPDLogic = new SKPDLogic(2021);
                foreach (SKPD oskpd in lstSKPD)
                {
                    lstIDSKPD.Add(oskpd.ID);
                }

            }
            else
            {

                lstIDSKPD.Add(iskpd);
            }
            AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
            if (oLogic.PersiapkanAKPerubahan(4, 5, lstIDSKPD) == true)
            {
                MessageBox.Show("Anggaran Kas Pergeseran Perubahan sudah disiapkan.... ");

            }
            else
            {
                MessageBox.Show(oLogic.LastError());
            }
        }
    }
}
