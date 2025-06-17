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
    public partial class frmSP2DOnlineMasukkanOTP : Form
    {
        private bool mOK;
        private string mOTP;
        public frmSP2DOnlineMasukkanOTP()
        {
            InitializeComponent();
            mOK = false;
            mOTP= "";

        }

        private void frmSP2DOnlineMasukkanOTP_Load(object sender, EventArgs e)
        {

        }
        public bool OK
        {
            set { mOK = value; }
            get { return mOK; }

        }
        public string OTP{
            get{ return mOTP;}
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
           mOTP= txtOTP.Text.Trim();
           mOK= true;
           this.Hide();

        }

        private void cmdBatal_Click(object sender, EventArgs e)
        {
            mOK= false;
            this.Hide();

        }
    }
}
