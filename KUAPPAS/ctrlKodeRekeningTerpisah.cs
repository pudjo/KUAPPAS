using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using BP;
using Formatting;

namespace KUAPPAS
{
    public partial class ctrlKodeRekeningTerpisah : UserControl
    {
        Rekening m_oRekening;

        public ctrlKodeRekeningTerpisah()
        {
            InitializeComponent();
            m_oRekening = null;
        }

        private void ctrlKodeRekeningTerpisah_Load(object sender, EventArgs e)
        {
            Clear();

        }
        public void Clear()
        {
            txtKode1.Text = "";
            txtKode2.Text = "";
            txtKode3.Text = "";
            txtKode4.Text = "";
            txtKode5.Text = "";
            txtKode6.Text = "";
            lblNama.Text = "";
        }
        public bool Create(Rekening oRekening)
        {
            m_oRekening = new Rekening(oRekening, GlobalVar.ProfileRekening);
            txtKode1.Text = m_oRekening.KodeLevel1;
            txtKode2.Text = m_oRekening.KodeLevel2;
            txtKode3.Text = m_oRekening.KodeLevel3;
            txtKode4.Text = m_oRekening.KodeLevel4;
            txtKode5.Text = m_oRekening.KodeLevel5;
            txtKode6.Text = m_oRekening.KodeLevel6;

            txtKode1.Enabled = false;
            txtKode2.Enabled = false;
            txtKode3.Enabled = false;
            txtKode4.Enabled = false;
            txtKode5.Enabled = false;
            txtKode6.Enabled = false;
            lblNama.Text = m_oRekening.Nama;
            RefreshTampilanBasedRoot();


            return true;


        }
        public void AllowEdit(bool bTrue = true)
        {
            switch ((int)m_oRekening.Root)
            {
                case 1:
                    txtKode1.Enabled = bTrue;
                    break;
                case 2:
                    txtKode2.Enabled = bTrue;
                    break;
                case 3:
                    txtKode3.Enabled = bTrue;
                    break;
                case 4:
                    txtKode4.Enabled = bTrue;
                    break;
                case 5:
                    txtKode5.Enabled = bTrue;
                    break;
                case 6:
                    txtKode6.Enabled = bTrue;
                    break;

            }
            

        }
        private void RefreshTampilanBasedRoot() {
            
            txtKode1.Visible = true ;
            txtKode2.Visible = false;
            txtKode3.Visible = false;
            txtKode4.Visible = false;
            txtKode5.Visible = false;
            txtKode6.Visible = false;

            if (m_oRekening.Root > 1)
            {
                txtKode2.Visible = true;
                if (m_oRekening.Root > 2)
                {
                    txtKode3.Visible = true;
                    if (m_oRekening.Root > 3)
                    {
                        txtKode4.Visible = true;
                        if (m_oRekening.Root > 4)
                        {
                            txtKode5.Visible = true;
                            if (m_oRekening.Root > 5)
                            {
                                txtKode6.Visible = true;
                            }
                        }
                    }
                }
            }
        }
        public void SetNewWithParent(Rekening oRek)
        {
            Create(oRek);
            if (oRek.Root == 4)
            {
                txtKode5.Visible = true;
                txtKode5.Enabled = true;
                txtKode5.Focus();
                

            }
            if (oRek.Root == 5)
            {
                txtKode6.Visible = true;
                txtKode6.Enabled = true;
                txtKode6.Focus();


            }
            if (oRek.Root == 3)
            {
                txtKode4.Visible = true;
                txtKode4.Enabled = true;
                txtKode4.Focus();


            }
        }
        private void ctrlKodeRekeningTerpisah_Enter(object sender, EventArgs e)
        {
            if (m_oRekening == null)
                return;
            switch ((int)m_oRekening.Root)
            {
                case 1:
                    txtKode1.Focus();
                    break;
                case 2:
                    txtKode2.Focus();
                    break;
                case 3:
                    txtKode3.Focus();
                    break;
                case 4:
                    txtKode4.Focus();
                    break;
                case 5:
                    txtKode5.Focus();
                    break;
                case 6:
                    txtKode6.Focus();
                    break;

            }
        }
        public long  GetID(bool bBaru = false)
        {
            long lRet;
            if (bBaru == false ){
                return m_oRekening == null ? 0 : m_oRekening.ID;
    } else {


            string s1 = DataFormat.GetInteger(txtKode1.Text).IntToStringWithLeftPad(GlobalVar.ProfileRekening.Kode1);
            string s2 = DataFormat.GetInteger(txtKode2.Text).IntToStringWithLeftPad(GlobalVar.ProfileRekening.Kode2);
            string s3 = DataFormat.GetInteger(txtKode3.Text).IntToStringWithLeftPad(GlobalVar.ProfileRekening.Kode3);
            string s4 = DataFormat.GetInteger(txtKode4.Text).IntToStringWithLeftPad(GlobalVar.ProfileRekening.Kode4);
            string s5 = DataFormat.GetInteger(txtKode5.Text).IntToStringWithLeftPad(GlobalVar.ProfileRekening.Kode5);
            string s6 = DataFormat.GetInteger(txtKode6.Text).IntToStringWithLeftPad(GlobalVar.ProfileRekening.Kode6);



            lRet= DataFormat.GetLong(s1+ s2+ s3+s4+s5+s6);

    return lRet;
}

            //lRet = DataFormat.GetLong(s1 + s2 + s3 + s4 + s5);

        //    


        }
        public string GetNama()
        {
            return m_oRekening.Nama;

        }
        public Single GetRoot()
        {
            return m_oRekening.Root;
        }
        public Single Leaf()
        {
            return m_oRekening.Leaf;
        }
        public Single Debet()
        {
            return m_oRekening.Debet;
        }

    }
}
