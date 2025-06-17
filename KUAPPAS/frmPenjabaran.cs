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
    public partial class frmPenjabaran : Form
    {
        private int m_iTahap;
        public frmPenjabaran()
        {
            InitializeComponent();
            m_iTahap = 0;
        }

        private void frmPenjabaran_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            SetJudul();

        }
        private void SetJudul()
        {
             
            string sJudul = "";

            switch (m_iTahap)
            {
                case 0:
                    sJudul = "Rancangan Penjabaran APBD";
                    break;
                case 1:
                    sJudul = "Penjabaran APBD";
                    break;
                case 2:
                    sJudul = "Rancangan Penjabaran Perubahan APBD";
                    break;
                case 3:
                    sJudul = "Penjabaran Perubahan APBD";
                    break;

            }
            ctrlHeader1.SetCaption(sJudul, "");

        }
        public void SetTahap(int _pTahap)
        {
            m_iTahap = _pTahap;
            SetJudul();

        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            ParameterLaporan _p = new ParameterLaporan();
            _p.Tahun = GlobalVar.TahunAnggaran;
            _p.Tahap = GlobalVar.TahapAnggaran;
            _p.IDDinas = ctrlDinas1.GetID();
            //_p.Jenis="(";

            //if (chkKhususGaji.Checked == true)
            //{
              //  _p.Jenis = _p.Jenis + "2";
            //}
            //else
            //{
              //  _p.Jenis = _p.Jenis + "1,2,3,4,5";
            //}
            //_p.Jenis = _p.Jenis + ")";

            
            //frmReportViewer frv = new frmReportViewer();
            //frv.Penjabaran(_p);
            //frv.Show();

        }
    }
}
