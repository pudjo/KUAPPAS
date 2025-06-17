using BP.Bendahara;
using DTO.Akuntansi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Akunting
{
    public partial class frmRegisterSP2DBPK : Form
    {
        public frmRegisterSP2DBPK()
        {
            InitializeComponent();
        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
            SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
            List<RegisterSP2DBPK> lst = oLogic.GetRegisterSP2DBPK();

        }
    }
}
