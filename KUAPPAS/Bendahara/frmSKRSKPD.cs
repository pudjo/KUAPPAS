using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS.Bendahara
{
    public partial class frmSKRSKPD : Form
    {
        public frmSKRSKPD()
        {
            InitializeComponent();
        }

        private void frmSKRSKPD_Load(object sender, EventArgs e)
        {
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlDinas1.SetID(GlobalVar.Pengguna.SKPD);
                
            }
        }
    }
}
