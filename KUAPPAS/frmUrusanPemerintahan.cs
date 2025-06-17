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



namespace KUAPPAS
{
    public partial class frmUrusanPemerintahan : Form
    {
        public frmUrusanPemerintahan()
        {
            InitializeComponent();
        }

        private void frmUrusanPemerintahan_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            gridUrusan.FormatHeader();
            LoadUrusan();
            
        }
        private void LoadUrusan()
        {
            UrusanLogic olOgic = new UrusanLogic(GlobalVar.TahunAnggaran);
            List<Urusan> _lst = new List<Urusan>();
            _lst = olOgic.Get();
            gridUrusan.Rows.Clear();
            if (_lst != null)
            {
                foreach (Urusan u in _lst)
                {
                    string[] row = { u.ID.ToString(), u.Tampilan, u.Nama, u.NamaFungsi };
                    gridUrusan.Rows.Add(row);
                }
            }
        }

        private void gridUrusan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
