using DTO.SP2DOnLine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO.SP2DOnLine._511;

namespace KUAPPAS.SP2DOnline
{
    public partial class frmSP2DOnlne510Respond : Form
    {
        public frmSP2DOnlne510Respond()
        {
            InitializeComponent();
        }

        private void frmSP2DOnlne510Respond_Load(object sender, EventArgs e)
        {

        }
        //public void SetData(DataInformasi511ResponseEx data)
        //{
            
        //    txtNoSP2D.Text = data.nomorSP2D;
        //    txtNoSPM.Text = data.nomorSPM;
        //    txtRefNo.Text = data.referenceNo;
            
            
        //    //foreach (DetailPotonganMpnResponse pot in data.detailPotonganMpn)
        //    //{
        //    //    string[] row = { pot.idBilling, pot.referenceNo, pot.nominalPotongan, pot.statusPaymentMpn, pot.ntpn };


        //    //   gridPotongan.Rows.Add(row);


        //    //}

        //}

        private void cmdTutup_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
