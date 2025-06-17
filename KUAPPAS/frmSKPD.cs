using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using DTO;
using Formatting;


namespace KUAPPAS
{
    public partial class frmSKPD : Form
    {
        public SKPD m_oSKPD;
        private string sTextAseli;
        private int m_Parent;
        private int JenisForm;
        private int mProfile;
        
        public frmSKPD()
        {
            InitializeComponent();
            m_oSKPD = new SKPD();
            sTextAseli = chkTampilkanTerkait.Text;
            m_Parent = 0;
            JenisForm = 0;
            mProfile = 3;
        }

        public int Profile
        {
            set { mProfile = value; }
        }
        public frmSKPD(Single _bMode)
        {
            if (_bMode == 1)
            {
              //  ctrlNavigation1.Enabled = false;
              //  ctrlNavigation3.Enabled = false;
               // cmdSimpan.Enabled = false;

            }
        }
        public void SetJenisForm(int bJenis){
            JenisForm = bJenis;
           // if (JenisForm == 0)  // SKPD
            //{
                ctrlSKPD1.Visible = false;
                lblSKPD.Visible = false;
                lblUK.Visible = false;
                txtKodeUnit.Text = "0";
                txtKodeUnit.Visible = false;
                tabControl1.Visible = false;
                tabControl1.Visible = true;
                


            //}
            //else
           // {
            //    ctrlSKPD1.Visible = true;
            //    lblSKPD.Visible = true;
            //    lblUK.Visible = true;
            //    txtKodeUnit.Text = "0";
            //    txtKodeUnit.Visible = true;
            //    tabControl1.Visible = true;
            //   // ctrlUrusan1.Create();
                

            //}
        }
        private void frmSKPD_Load(object sender, EventArgs e)
        {
            if (m_oSKPD.ID == 0)
            {
                ctrlNavigation3.SetNew();
            }
            if (gridUrusan.Rows.Count ==0)
                LoadUrusanPemerintahan();
            
            gridUrusan.FormatHeader();

        }
        private void LoadUrusanPemerintahan()
        {


            UrusanLogic o = new UrusanLogic(GlobalVar.TahunAnggaran);
            List<Urusan> lst = o.Get();
            gridUrusan.Rows.Clear();
            var query = from sk in lst
                        orderby sk.KodeKategori, sk.KodeUrusan
                        select sk;

            foreach (Urusan p in query)
            {
                string[] row = { "false", p.ID.ToString(),"", p.ID.ToKodeUrusan() + " " + p.Nama };
                gridUrusan.Rows.Add(row);                
            }
        }

        private bool LoadSKPD()
        {
            SKPDLogic oLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
            m_oSKPD = oLogic.GetByID(m_oSKPD.ID);

           // if (m_oSKPD.Parent == 0 && _oSKPD.ID>0 && JenisForm==1)
                if (m_oSKPD.Parent == 0    && JenisForm == 0)
            {   
                lblSKPD.Visible = false;
                ctrlSKPD1.Visible = false;
                m_Parent = 0;
               
            }
            else
            {
                lblSKPD.Visible = true;
                ctrlSKPD1.Visible = true;
                if (m_Parent > 0)
                {
                    ctrlSKPD1.Create(0);
                    ctrlSKPD1.SetID(m_Parent, m_oSKPD.ID);
                }
            }
            if (m_oSKPD != null)
            {
                ctrlUrusan2.Create();
                int idUrusan = 0;
                idUrusan = m_oSKPD.IDUrusan;// DataFormat.GetInteger(DataFormat.IntToStringWithLeftPad(m_oSKPD.KodeKategori, 1) + DataFormat.IntToStringWithLeftPad(m_oSKPD.KodeUrusan, 2));
                ctrlUrusan2.SetID(idUrusan);

                
                txtID.Text = m_oSKPD.ID.ToString();
                txtNamaSKPD.Text = m_oSKPD.Nama;
                chkTampilkanTerkait.Text = sTextAseli + " " + m_oSKPD.Nama;

                txtKode.Text = m_oSKPD.Kode.ToString();
                ctrlUrusan2.SetID (m_oSKPD.KodeUrusan);
                txtKodeUnit.Text = m_oSKPD.KodeUnit.ToString();
                

                LoadUrusanPemerintahan();

                UrusanDinasLogic oUDLogic = new UrusanDinasLogic(GlobalVar.TahunAnggaran, mProfile);
                List<UrusanDinas> _lst = new List<UrusanDinas>();
                _lst = oUDLogic.GetByIDDinas(m_oSKPD.ID, GlobalVar.TahunAnggaran);
                if (_lst != null)
                {
                    foreach (UrusanDinas u in _lst)
                    {
                        for (int i = 0; i < gridUrusan.Rows.Count; i++)
                        {
                            int _lIDUrusan =DataFormat.GetInteger( gridUrusan.Rows[i].Cells[1].Value);
                            DataGridViewRow row = gridUrusan.Rows[i];
                            if (u.IDUrusan == _lIDUrusan)
                            {
                                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];

                                chk.Value = true;// chk.TrueValue;                               

                            }
                        }
                    }
                }

                return true;

            }
            else return false;

        }
        public bool SetID(int _pID, int Parent=0)
        {
            m_oSKPD.ID = _pID;

            m_Parent = Parent;
            return LoadSKPD();
        }
        public bool SetBaru( int Parent)
        {
            m_oSKPD.ID = 0;
            m_Parent = Parent;
            ctrlSKPD1.Create(0);
            ctrlSKPD1.SetID(Parent);
            SKPD oSKPD = new SKPD();
            SKPDLogic oLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
            oSKPD = oLogic.GetByID(Parent);
            if (Parent > 0)
            {
              
                //ctrlSKPD1.Create(0);
                ctrlUrusan2.Create();
                ctrlUrusan2.Enabled = false;
                txtKode.Enabled = false;

                txtKode.Text = oSKPD.Kode.ToString();
                ctrlUrusan2.SetID(oSKPD.IDUrusan );
                ctrlSKPD1.SetID(m_Parent, m_oSKPD.ID);
                
                
            }

            return true;
        }
        private void frmSKPD_Load_1(object sender, EventArgs e)
        {

        }
        private void frmSKPD_Load_2(object sender, EventArgs e)
        {

        }

        private void chkTampilkanTerkait_CheckedChanged(object sender, EventArgs e)
        {
            RefreshUrusan();
        }
        private void RefreshUrusan()
        {

            if (chkTampilkanTerkait.Checked == true)
            {

                for (int i = 0; i < gridUrusan.Rows.Count; i++)
                {
                    //int _lIDUrusan = DataFormat.GetInteger(gridUrusan.Rows[i].Cells[1].Value);


                    DataGridViewRow row = gridUrusan.Rows[i];
                    //DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                    //if (row.Cells[0].Value != null)
                    if (chk.Value != null)
                    {
                        if (row.IsNewRow == false && DataFormat.GetBoolean(row.Cells[0].Value) == false)
                            row.Visible = false;// !(chkTampilkanTerkait.Checked);// false;

                    }
                    //      }
                    //}
                }
            }
            else
            {
                for (int i = 0; i < gridUrusan.Rows.Count; i++)
                {
                    DataGridViewRow row = gridUrusan.Rows[i];
                    row.Visible = true;// !(chkTampilkanTerkait.Checked);// false;                 
                }
            }            
        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        //private void Simpan()
        {

            List<UrusanDinas> _lst = new List<UrusanDinas>();
            for (int i = 0; i < gridUrusan.Rows.Count; i++)
            {
                    
               UrusanDinas oUrusanDinas = new UrusanDinas();
               oUrusanDinas.IDDinas = m_oSKPD.ID;
               oUrusanDinas.Tahun= GlobalVar.TahunAnggaran;
                        
               DataGridViewRow row = gridUrusan.Rows[i];
               DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
               if (chk.Value != null)
               {
                   if (DataFormat.GetBoolean(row.Cells[0].Value) == true){
                            oUrusanDinas.IDUrusan =DataFormat.GetInteger(row.Cells[1].Value);
                            _lst.Add(oUrusanDinas);

                       
                    }
                }                   
            }
            SKPDLogic oLOgic = new SKPDLogic(GlobalVar.TahunAnggaran);
            if (oLOgic.SimpanUrusanPemerintahan(_lst) == true)
            {
                MessageBox.Show("Penyimpanan berhasil");

            }
            else
            {
                MessageBox.Show("Kesalahan menyimpan. " + oLOgic.LastError());
            }

        }

        private EventResponseMessage ctrlNavigation3_OnAdd()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            txtID.Text = "0";
            txtNamaSKPD.Text = "";
            if (JenisForm == 0)
            {
                txtKode.Text = "0";
                ctrlUrusan2.Create();
            }
            m_oSKPD.ID = 0;
           // LoadSKPD();
            LoadUrusanPemerintahan();
            return lRet;

        }

        private EventResponseMessage ctrlNavigation3_OnSave()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            SKPDLogic oSKPD = new SKPDLogic(GlobalVar.TahunAnggaran);
            m_oSKPD.Kode = DataFormat.GetInteger(txtKode.Text);
            m_oSKPD.IDUrusan = ctrlUrusan2.GetID();
            m_oSKPD.Nama = txtNamaSKPD.Text;
            m_oSKPD.KodeKategori = ctrlUrusan2.KodeKategori();
            m_oSKPD.KodeUrusan = ctrlUrusan2.KodeUrusan();
            m_oSKPD.KodeUnit = DataFormat.GetInteger(txtKodeUnit.Text);
            m_oSKPD.Parent = m_Parent;
            m_oSKPD.Tahun = GlobalVar.TahunAnggaran;
            m_oSKPD.Level = 1;
            m_oSKPD.Root = 1;
            if (m_Parent > 0)
            {
                m_oSKPD.Level = 1;
                m_oSKPD.Root = 1;

            }
            if (oSKPD.Simpan(ref m_oSKPD) == true)
            {
               
            }
            else
            {
                lRet.ResponseStatus = false;
                MessageBox.Show(oSKPD.LastError());

            }
            return lRet;

            
        }

        private EventResponseMessage ctrlNavigation3_OnDelete()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            //if (m_oSKPD.ID > 0)
            //{
                if (MessageBox.Show("Apakah benar akan menghapus SKPD ini?", "Hapus SKPD", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    SKPDLogic oLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
                    if (oLogic.Hapus(m_oSKPD.ID, (int)GlobalVar.TahunAnggaran)==true){
                        m_oSKPD.ID = 0;
                        txtKode.Text = "0";
                        txtNamaSKPD.Text = "";
                        txtID.Text = "0";

                        MessageBox.Show("Penghapusan selesai.");
                    }
                    else
                    {
                        lRet.ResponseStatus = false;
                        MessageBox.Show(oLogic.LastError());
                    }
                }
            //}
            return lRet;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EventResponseMessage lRet = new EventResponseMessage();
            SKPDLogic oSKPD = new SKPDLogic(GlobalVar.TahunAnggaran);
            m_oSKPD.Kode = DataFormat.GetInteger(txtKode.Text);
            m_oSKPD.IDUrusan = ctrlUrusan2.GetID();
            m_oSKPD.Nama = txtNamaSKPD.Text;
            m_oSKPD.KodeKategori = ctrlUrusan2.KodeKategori();
            m_oSKPD.KodeUrusan = ctrlUrusan2.KodeUrusan();
            
            m_oSKPD.Parent = m_Parent;
            m_oSKPD.KodeUnit = DataFormat.GetInteger(txtKodeUnit.Text);
            m_oSKPD.Level = 0;
            m_oSKPD.Tahun = GlobalVar.TahunAnggaran;

            if (DataFormat.GetInteger(txtKodeUnit.Text) > 0)
            {
                m_oSKPD.Level = 1;
            }
            if (oSKPD.Simpan(ref m_oSKPD) == true)
            {
                MessageBox.Show("Penyimpanan berhasil.");
            }
            else
            {
                MessageBox.Show("Penyimoanan gagal.\n" + oSKPD.LastError());
            }
            

        }

        private void cmdSimpanUrusanBAru_Click(object sender, EventArgs e)
        {
            
        }

        private void ctrlNavigation3_Load(object sender, EventArgs e)
        {

        }
    }
}
