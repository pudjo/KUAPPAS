using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS
{
    public partial class ctrlWaktu : UserControl
    {
        public ctrlWaktu()
        {
            InitializeComponent();
        }

        private void ctrlWaktu_Load(object sender, EventArgs e)
        {

        }
        public DateTime TanggalAwal
        {
            get {
                return ctrlPeriode1.GetDateAwal();
            }
        }
        public DateTime TanggalAkhir
        {
            get
            {
                return ctrlPeriode1.GetDateAkhir();
            }
        }
    }
}
