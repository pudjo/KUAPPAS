using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Text.RegularExpressions;

using System.Windows.Forms;
using DTO;
using BP;
using Formatting;

namespace KUAPPAS
{
    public partial class frmBuatPassword : Form
    {
        private Pengguna m_oPengguna;
        private bool IsOk;

        public frmBuatPassword()
        {
            InitializeComponent();
            m_oPengguna = new Pengguna();
            IsOk = false;
        }
        private void frmBuatPassword_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption ("Ubah Password");
        }
        public void SetPengguna(Pengguna oPengguna)
        {
            m_oPengguna = oPengguna;
            if (oPengguna != null)
            {
                txtID.Text = oPengguna.ID.ToString();
                txtID.Enabled = false;
                txtNama.Text = oPengguna.Nama;
                txtNIK.Text = oPengguna.NIK;
                txtUserID.Text = oPengguna.UserID;
            }
        }
        public bool ValidatePassword(string pwd, int minLength = 8, int numUpper = 2, int numLower = 2, int numNumbers = 2, int numSpecial = 2)
        {

            // Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
            System.Text.RegularExpressions.Regex upper = new System.Text.RegularExpressions.Regex("[A-Z]");
            System.Text.RegularExpressions.Regex lower = new System.Text.RegularExpressions.Regex("[a-z]");
            System.Text.RegularExpressions.Regex number = new System.Text.RegularExpressions.Regex("[0-9]");
            // Special is "none of the above".
            System.Text.RegularExpressions.Regex special = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");
            string Message="";
            bool retVal = true;
            // Check the length.
            if (pwd.Length < minLength)
            {
                Message = Message + " Password Minimal " + minLength.ToString() + " huruf";
                
                retVal = retVal && false;
                //return false;
            }
            MatchCollection matches = upper.Matches(pwd);
            if (matches.Count < numUpper)
            {
                Message = Message + " - Password Minimal " + numUpper.ToString() + " Huruf Besar";
                retVal = retVal && false;
                //return false;

            }

            matches = lower.Matches(pwd);
            if (matches.Count < numLower)
            {
                Message = Message + " Password Minimal " + numLower.ToString() + " Huruf Kecil";
              //  return false;
                retVal = retVal && false;
            }
            matches = number.Matches(pwd);
          
            if (matches.Count < numNumbers)
            {
                Message = Message + " Password Minimal " + numNumbers.ToString() + " angka";
                retVal = retVal && false;
            }
            lblMessage.Text = Message;
            lblMessage.BackColor = Color.White;

            return retVal;

           // matches = special.Matches(pwd);

            //if (matches.Count < numSpecial)
            //{
            //    MessageBox.Show("Password Minimal " + numNumbers.ToString() + " Kharakter spesial");
            //    return false;
            //}

            // Passed all checks.

           
        }
        private bool CekInput()
        {
            if (txtPassword.Text.Trim().Length < 8)
            {
                MessageBox.Show("Password harus minimal 8 huruf");
                return false;
            }
            if (txtPassword.Text.Trim() != txtConfirm.Text.Trim())
            {
                MessageBox.Show("Password dan Konfirm Tidak Sama. SIla disamakan");
                return false;
            }
            return true;


        }
        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (CekInput() == false)
                {
                    IsOk = false;
                    return;
                }
                if (ValidatePassword( txtPassword.Text,8,1,1,1,1) == false)
                {
                    return;
                }
                if (m_oPengguna.Sumber == 2)
                {
                    PenggunaLogic oLOgic = new PenggunaLogic(GlobalVar.TahunAnggaran);
                    GlobalVar.Pengguna.NIK = txtNIK.Text;
                    GlobalVar.Pengguna.Level = 1;
                    GlobalVar.Pengguna.Nama = txtNama.Text;
                    GlobalVar.Pengguna.PPKD = false;
                    GlobalVar.Pengguna.Status = 1;
                    

                    oLOgic.Simpan(ref GlobalVar.Pengguna);
                    txtID.Text = GlobalVar.Pengguna.ID.ToString();



                }


                string savedPassword = "";
                savedPassword = PasswordUtility.Generate(txtPassword.Text);
                m_oPengguna.Password = savedPassword;

                PenggunaLogic ologic = new PenggunaLogic(GlobalVar.TahunAnggaran);
                if (ologic.Simpan(ref m_oPengguna) == false)
                {
                    MessageBox.Show(ologic.LastError());
                    return;

                }

                if (ologic.SetPassword2(m_oPengguna) == true)
                {
                    MessageBox.Show("Password tersimoan");
                    IsOk = true;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Penyimpanan pasword gagal..");
                    IsOk = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool IsOK()
        {
            return IsOk;

        }

        private void cmdBatal_Click(object sender, EventArgs e)
        {
            IsOk = false;
            this.Hide();

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
           if (ValidatePassword(txtPassword.Text, 8, 1, 1, 1, 1) == false)
            {

                cmdSimpan.Enabled = false;
            }
            else
            {
                cmdSimpan.Enabled = true;
                lblMessage.Text = "Password Valid";
                lblMessage.ForeColor = Color.Green;
            }
        }
    }
}
