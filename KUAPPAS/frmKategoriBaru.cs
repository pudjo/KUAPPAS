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
    public partial class frmKategoriBaru : Form
    {
        public frmKategoriBaru()
        {
            InitializeComponent();
        }

        private void frmKategoriBaru_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("DaftarKelompok Urusan Baru", "");
            LoadData();
        }
        private void LoadData()
        {
            KategoriBaruLogic oLogic = new KategoriBaruLogic(GlobalVar.TahunAnggaran);
            List<KategoriBaru> _lst = new List<KategoriBaru>();
            _lst = oLogic.Get();
            gridKategori.Rows.Clear();

            if (_lst != null)
            {
                foreach (KategoriBaru kb in _lst)
                {

                    string[] row = { "Hapus",kb.ID.ToString(), kb.Nama };
                    gridKategori.Rows.Add(row);
                }
            }
            else
            {
                MessageBox.Show(oLogic.LastError());
            }
        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            KategoriBaruLogic oLogic = new KategoriBaruLogic(GlobalVar.TahunAnggaran);

            for (int i=0; i < gridKategori.Rows.Count; i++)
            {
                if (gridKategori.Rows[i].Cells[1].Value != null)
                {

                    if (gridKategori.Rows[i].Cells[1].Value.ToString() != "")
                    {
                        KategoriBaru kb = new KategoriBaru();
                        kb.ID = DataFormat.GetInteger(gridKategori.Rows[i].Cells[1].Value.ToString());
                        kb.Nama= DataFormat.GetString(gridKategori.Rows[i].Cells[2].Value.ToString());
                        oLogic.Simpan(ref kb);
                    }
                }
            }
            MessageBox.Show("Penyimpanan Selesai.");

        }

        private void gridKategori_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (gridKategori.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    if (gridKategori.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
                    {
                        if (MessageBox.Show("Apakah benara akan menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            int _ID = DataFormat.GetInteger(gridKategori.Rows[e.RowIndex].Cells[1].Value.ToString());
                            KategoriBaruLogic oLogic = new KategoriBaruLogic(GlobalVar.TahunAnggaran);
                            KategoriBaru kb = new KategoriBaru();
                            kb.ID = _ID;
                            if (oLogic.Hapus(_ID) == true)
                            {
                                MessageBox.Show("Data Suhdah dihapus.");

                            }
                            else
                            {
                                MessageBox.Show("Data gagal dihapus." + oLogic.LastError());
                            }
                        }
                    }

                }
            }
        }
    }
}
