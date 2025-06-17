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
    public partial class frmListDusun : Form
    {
        public frmListDusun()
        {
            InitializeComponent();
        }

        private void cmdTambah_Click(object sender, EventArgs e)
        {
            frmDusunDetail fDusunDetail = new frmDusunDetail();
            fDusunDetail.ShowDialog();
            LoadDusun();
        }
        private void LoadDusun()
        {
            DusunLogic oLogic = new DusunLogic(GlobalVar.TahunAnggaran);
            List<Dusun> _lst = new List<Dusun>();
            gridDusun.Rows.Clear();
            _lst = oLogic.Get();
            if (_lst != null)
            {
                foreach (Dusun d in _lst)
                {
                    string[] row = {"Detail",d.ID.ToString(), d.Tampilan,d.Nama,d.NamaDesa,d.NamaKecamatan };
                    gridDusun.Rows.Add(row);

                }
            }
            else
            {
                if (oLogic.IsError())
                {
                    MessageBox.Show(oLogic.LastError());
                }
            }
        }

        private void frmListDusun_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ctrlHeader1.SetCaption("Daftar Dusun", "Klik Tambah untuk menambah, Tombol Detail pada setiap baris untuk melihat detail.");
            gridDusun.FormatGridView();
            gridDusun.FormatHeader();
            LoadDusun();

        }

        private void gridDusun_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string sID = DataFormat.GetString(gridDusun.Rows[e.RowIndex].Cells[1].Value.ToString());
                int pID = DataFormat.GetInteger(sID);
                frmDusunDetail fDetail = new frmDusunDetail();
                fDetail.SetID(pID);
                fDetail.ShowDialog();
                LoadDusun();
            }
        }
    }
}
