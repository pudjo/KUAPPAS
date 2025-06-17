using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS
{
    public partial class frmRekapKUAPPAS : Form
    {
        private int m_iJenis;
        public frmRekapKUAPPAS()
        {
            InitializeComponent();
        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            ////frmReportViewer f = new frmReportViewer();

            ////if (chkPendapatan.Checked == true)
            ////    m_iJenis = 1;

            ////if (chkBelanjaTidakLangsung.Checked == true)
            ////    m_iJenis = 2;
            ////if (chkBelanjaLangsung.Checked == true)
            ////    m_iJenis = 3;


            ////f.CetakRekapKUA(m_iJenis);
            ////f.Show();
        }

        private void frmRekapKUAPPAS_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption ("Rekep Pagu Per Dinas","");

        }
    }
}
