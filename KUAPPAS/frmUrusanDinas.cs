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
    public partial class frmUrusanDinas : Form
    {
         public SKPD m_oSKPD;
        private string sTextAseli;
        private int mProfile;
        
        public frmUrusanDinas()
        {
            InitializeComponent();
            m_oSKPD = new SKPD();
            sTextAseli = chkTampilkanTerkait.Text;
            mProfile = 3;
        }
        public int Profile
        {
            set { mProfile = value; }
        }

        private void chkTampilkanTerkait_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {

        }

        private void frmUrusanDinas_Load(object sender, EventArgs e)
        {
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            if (gridUrusan.Rows.Count == 0)
                LoadUrusanPemerintahan();

          
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
                string[] row = { "false", p.ID.ToString(), p.Tampilan, p.Nama };
                gridUrusan.Rows.Add(row);
            }
        }
        private bool LoadSKPD()
        {
            SKPDLogic oLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
            m_oSKPD = oLogic.GetByID(m_oSKPD.ID);
            if (m_oSKPD != null)
            {
                ctrlUrusan2.Create();
                int idUrusan = 0;
                idUrusan = DataFormat.GetInteger(DataFormat.IntToStringWithLeftPad(m_oSKPD.KodeKategori, 1) + DataFormat.IntToStringWithLeftPad(m_oSKPD.KodeUrusan, 2));
                ctrlUrusan2.SetID(idUrusan);

                txtID.Text = m_oSKPD.ID.ToString();
                txtNamaSKPD.Text = m_oSKPD.Nama;
                chkTampilkanTerkait.Text = sTextAseli + " " + m_oSKPD.Nama;                
                ctrlUrusan2.SetID(m_oSKPD.KodeUrusan);                
                LoadUrusanPemerintahan();
                UrusanDinasLogic oUDLogic = new UrusanDinasLogic(GlobalVar.TahunAnggaran,mProfile);
                List<UrusanDinas> _lst = new List<UrusanDinas>();
                _lst = oUDLogic.GetByIDDinas(m_oSKPD.ID, GlobalVar.TahunAnggaran);
                if (_lst != null)
                {
                    foreach (UrusanDinas u in _lst)
                    {
                        for (int i = 0; i < gridUrusan.Rows.Count; i++)
                        {
                            int _lIDUrusan = DataFormat.GetInteger(gridUrusan.Rows[i].Cells[1].Value);
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
        //public bool SetID(int _pID)
        //{
            
        //}

        private void ctrlSKPD1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlSKPD1_OnChanged(int pID)
        {
            m_oSKPD.ID = pID;
            LoadSKPD();


        }

    }
}
