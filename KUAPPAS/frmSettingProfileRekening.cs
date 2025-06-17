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

namespace KUAPPAS
{
    public partial class frmSettingProfileRekening : Form
    {
        private int m_id = 0;
        List<ProfileRekening> lst;
        private int mProfile;
        public frmSettingProfileRekening()
        {
            InitializeComponent();
            m_id = 0;
            lst = new List<ProfileRekening>();
            mProfile = 2;
        }

        public int Profile
        {
            set { mProfile = value; }
            get { return mProfile; }
        }
        private void frmSettingProfileRekening_Load(object sender, EventArgs e)
        {

            LoadProfileRekening();
        }
        private void LoadProfileRekening()
        {
            List<ProfileRekening> lst = new List<ProfileRekening>();

            ProfileRekeningLogic oLogic = new ProfileRekeningLogic((int)GlobalVar.TahunAnggaran, mProfile);
            gridProfile.Rows.Clear();
            lst = oLogic.Get();
            foreach (ProfileRekening p in lst)
            {
                string[] row = { p.ID.ToString(), p.Nama, p.Keterangan };
                gridProfile.Rows.Add(row);

            }
        }

        private void gridProfile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridProfile.Rows.Count)
            {
                ProfileRekening p = new ProfileRekening();
                p = lst[e.RowIndex];
                m_id = p.ID;
                txtNama.Text = p.Nama;
                txtKeterangan.Text = p.Keterangan;
                txtNumSegment.Text = p.NumSegment.ToString();
                txtSegmen1.Text = p.Kode1.ToString();
                txtSegmen2.Text = p.Kode2.ToString();
                txtSegmen3.Text = p.Kode3.ToString();
                txtSegmen4.Text = p.Kode4.ToString();
                txtSegmen5.Text = p.Kode5.ToString();
                txtSegmen6.Text = p.Kode6.ToString();
            }
        }
    }
}
