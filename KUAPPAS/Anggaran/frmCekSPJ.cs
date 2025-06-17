using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Anggaran
{
    public partial class frmCekSPJ : Form
    {
        public frmCekSPJ()
        {
            InitializeComponent();
        }

        private void frmCekSPJ_Load(object sender, EventArgs e)
        {

        }
        private bool Bandingkan (){
            try
            {

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        
        }
    }
}
