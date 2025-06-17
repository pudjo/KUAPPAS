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
    public partial class frmKecamatan : Form
    {
        public frmKecamatan()
        {
            InitializeComponent();
        }

        private void cmdKeluar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            frmKecamatanDetail fDetail = new frmKecamatanDetail();
            fDetail.ShowDialog();
            LoadData();
        }
        private void LoadData()
        {
            KecamatanLogic oLogic = new KecamatanLogic(GlobalVar.TahunAnggaran);
            List<Kecamatan> _lst = new List<Kecamatan>();
            _lst = oLogic.Get();
            gridKecamatan.Rows.Clear();
            if (_lst != null)
            {
                foreach (Kecamatan k in _lst)
                {
                    string[] row = { "Detail", k.ID.ToString(), k.Nama };
                    gridKecamatan.Rows.Add(row);
                }
            }
        }

        private void frmKecamatan_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ctrlHeader1.SetCaption("Daftar Kecamatan", "Klik Tambah untuk menambah, Tombol Detail pada setiap baris untuk melihat detail.");
            gridKecamatan.FormatGridView();
            LoadData();
        }

        private void gridKecamatan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int idKecamatan = DataFormat.GetInteger(gridKecamatan.Rows[e.RowIndex].Cells[1].Value.ToString());
                frmKecamatanDetail fDetail = new frmKecamatanDetail();
                fDetail.SetID(idKecamatan);
                fDetail.ShowDialog();
                LoadData();

            }
        }
    }
}
