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
    public partial class frmMasterSumberDana : Form
    {
        private int mprofile;
        private int m_ID;

        public frmMasterSumberDana()
        {
            m_ID = 0;

            InitializeComponent();
            mprofile = 3;
        }
        public int Profile
        {
            set { mprofile = value; }
        }

        private void frmMasterSumberDana_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ctrlHeader1.SetCaption("Daftar Jenis Sumber Dana", "");
            //gridSumberDana.FormatHeader();
            treeSumberDana1.Create();
            //LoadData();
        }
        private void LoadData()
        {
            SumberDanaLogic oLogic = new SumberDanaLogic(GlobalVar.TahunAnggaran);
            List<SumberDana> _lst = new List<SumberDana>();
            _lst = oLogic.Get();
            gridSumberDana.Rows.Clear();

            if (_lst != null)
            {
                foreach (SumberDana sd in _lst)
                {
                    string[] row = { sd.ID.ToString(), sd.IDRekening.ToString(), sd.Nama };
                    gridSumberDana.Rows.Add(row);


                }
            }
            else
            {
                MessageBox.Show(oLogic.LastError());
            }

        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            SumberDanaLogic oLogic = new SumberDanaLogic(GlobalVar.TahunAnggaran);
            
                SumberDana sd = new SumberDana();
                sd.IDRekening = DataFormat.GetLong(txtID.Text );
                sd.IIDParent = DataFormat.GetLong(txtIDParent.Text ); 
                //sd.StatusUpdate = DataFormat.GetSingle(gridSumberDana.Rows[i].Cells[1].Value);
                sd.Nama= txtNama.Text;
                sd.Root = DataFormat.GetInteger(txtRoot.Text );
                sd.Leaf = chkLeaf.Checked == true ? 0 : 1;
                sd.ID = m_ID;

                if (oLogic.Simpan(sd) == true)
                {
                    MessageBox.Show("Data Tersimpan");
                }
                else
                {
                    MessageBox.Show("Kesalahan Menyimppan Data" + oLogic.LastError());
                }

            
            

        }

        private void cmdTambah_Click(object sender, EventArgs e)
        {
            //gridSumberDana.Rows.Add();
            txtIDParent.Text = txtID.Text;
            txtNama.Text = "";
            txtID.Text = "";
            txtRoot.Text = (DataFormat.GetInteger(txtRoot.Text) + 1).ToString();

        }

        private void cmdHapus_Click(object sender, EventArgs e)
        {
            if (gridSumberDana.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = gridSumberDana.SelectedRows[0];

            int i = row.Index;
            if (i >= 0 && i < gridSumberDana.Rows.Count)
            {
                if (MessageBox.Show("Apakah benar akan menghapus data?", "Konfirnasi Hapus", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SumberDana sd = new SumberDana();
                    SumberDanaLogic oLogic = new SumberDanaLogic(GlobalVar.TahunAnggaran);
                    sd.ID = DataFormat.GetInteger(row.Cells[0].Value);
                    if (oLogic.Hapus(sd) == true)
                    {
                        MessageBox.Show("Data sudah dihapus");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Data gagal di hapus" + oLogic.LastError());

                    }

                }
            }

        }

        private void treeSumberDana1_Changed(SumberDana rek)
        {
            if (rek != null)
            {
                txtID.Text = rek.IDRekening.ToString();
                txtIDParent.Text = rek.IIDParent.ToString();
                txtNama.Text = rek.Nama;
                txtRoot.Text = rek.Root.ToString();
                chkLeaf.Checked = rek.Leaf == 0;
                m_ID = rek.ID;


            }
            else
            {

                txtID.Text ="";
                txtIDParent.Text = "";
                txtNama.Text = "";
                txtRoot.Text = "";
                chkLeaf.Checked = false;
                m_ID = 0;


            }
        }

        private void treeSumberDana1_Load(object sender, EventArgs e)
        {

        }
    }
}
