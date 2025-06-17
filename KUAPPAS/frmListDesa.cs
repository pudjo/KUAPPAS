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
    public partial class frmListDesa : Form
    {
        public frmListDesa()
        {
            InitializeComponent();
        }

        private void cmdKeluar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void cmdTambah_Click(object sender, EventArgs e)
        {
            frmDesaDetail fDesa = new frmDesaDetail();
            fDesa.ShowDialog();
            LoadDesa();

            
        }
        private void LoadDesa()
        {
            DesaLogic oLogic = new DesaLogic(GlobalVar.TahunAnggaran);
            List<Desa> _lst = new List<Desa>();
            _lst = oLogic.Get();
            gridDesa.Rows.Clear();
            if (_lst != null)
            {
                foreach (Desa d in _lst){
                    string[] row = { "Detail", d.ID.ToString(), d.Kecamatan.ToString(), d.Kode.ToString(), d.Nama, d.NamaKecamatan };

                    gridDesa.Rows.Add(row);
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

        private void frmListDesa_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ctrlHeader1.SetCaption("Daftar Desa", "Klik Tambah untuk menambah, Tombol Detail pada setiap baris untuk melihat detail.");
            gridDesa.FormatHeader();
            gridDesa.FormatGridView();
            LoadDesa();
        }

        private void gridDesa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex < gridDesa.Rows.Count)
            {
                string sID = DataFormat.GetString(gridDesa.Rows[e.RowIndex].Cells[1].Value.ToString());
                if (sID.Trim().Length > 0)
                {
                    int _pID = DataFormat.GetInteger(sID);
                    frmDesaDetail fDetail = new frmDesaDetail();
                    fDetail.SetID(_pID);
                    fDetail.ShowDialog();

                }
            }
        }
    }
}
