using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS.SP2DOnline
{
    public partial class frmInputOTP : Form
    {
        private bool m_bOK;
        private string m_sOTP;
        public frmInputOTP()
        {
            InitializeComponent();
            m_bOK = false;
            m_sOTP = "";
        }

        private void frmInputOTP_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Masukkan OTP");
            m_bOK = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_sOTP = txtOTP.Text.Trim();
            if (m_sOTP.Length != 6)
            {
                MessageBox.Show("Kode Salah. Silakan periksa. Harus 6 angka");
                return;
            }


            if (MessageBox.Show("Apakah anda yakin?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_bOK = true;
                this.Hide();

            };
            
        }
        public string KodeOTP
        {

            get { return m_sOTP; }
        }
        private void cmdBatal_Click(object sender, EventArgs e)
        {
            m_bOK = false;
            this.Hide();
        }
        public bool OK
        {
            get { return m_bOK; }
             
        }
    }
}
