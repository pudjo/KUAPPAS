using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formatting;

namespace KUAPPAS
{
    public partial class TanggalTandaTangan: UserControl
    {
        public TanggalTandaTangan()
        {
            InitializeComponent();
        }

        private void TanggalTandaTangan_Load(object sender, EventArgs e)
        {

        }
        public string Tanggal
        {
            get
            {
                return tanggal.Value.Date.ToTanggalIndonesiaLengkap();
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
