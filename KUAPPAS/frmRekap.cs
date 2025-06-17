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
    public partial class frmRekap : Form
    {
        public frmRekap()
        {
            InitializeComponent();
        }

        private void frmRekap_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            gridRekap.FormatHeader();
            LoadRekap();
        }
        private void LoadRekap()
        {
            PlafonByUrusanLogic oLogic = new PlafonByUrusanLogic(GlobalVar.TahunAnggaran);
            List<PlafonByUrusan> _lst = new List<PlafonByUrusan>();
            decimal JBTL = 0L;
            decimal JBL = 0L;
            decimal J = 0L;

            _lst = oLogic.Get();
            gridRekap.Rows.Clear();
            if (_lst != null)
            {
                foreach (PlafonByUrusan p in _lst)
                {
                    string[] row = { p.IDDinas.ToString(), p.Nama, p.BTL.FormatUang(), p.BL.FormatUang(), p.Jumlah.FormatUang() };
                    gridRekap.Rows.Add(row);
                    JBTL +=p.BTL;
                    JBL += p.BL;
                    J += p.Jumlah;

                }

            }
            txtBTL.Text = JBTL.FormatUang();
            txtBL.Text = JBL.FormatUang();
            txtJ.Text = J.FormatUang();

        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            LoadRekap();
        }
    }
}
