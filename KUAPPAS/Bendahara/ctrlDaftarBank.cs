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
using DTO.Bendahara;
using BP.Bendahara;


namespace KUAPPAS.Bendahara
{
    public partial class ctrlDaftarBank : UserControl
    {
        public ctrlDaftarBank()
        {
            InitializeComponent();
        }

        private void ctrlDaftarBank_Load(object sender, EventArgs e)
        {

        }
        public bool Create(string kode="")
        {
            try
            {



                int idx = 0;
                int selectedIdx = 0;
                cmbDaftarBank.Items.Clear();
                DaftarBankLogic oLogic = new DaftarBankLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gLstBanks = oLogic.GetBanks();
                foreach (DaftarBank db in GlobalVar.gLstBanks)
                {
                    ListItemData itemc = new ListItemData(db.Nama, db.bankCode);
                    cmbDaftarBank.Items.Add(itemc);

                    if (db.bankCode.Trim() == kode.Trim())
                    {
                        selectedIdx = idx;
                    }
                    idx++;
                }
                cmbDaftarBank.SelectedIndex = selectedIdx;


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public void SetKode(string kode, string NoRekening="")
        {
            for (int idx = 0; idx < cmbDaftarBank.Items.Count; idx++)
            {
                ListItemData it = (ListItemData)cmbDaftarBank.Items[idx];

                if (it.Kode == kode)
                {
                    cmbDaftarBank.SelectedIndex = idx;
                    if (kode == "123" && NoRekening.Trim().Replace(" ", "").Replace(".", "").Replace("-", "").Length != 10)
                    {
                        if (cmbDaftarBank.Text.Contains("Syariah") == true)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (cmbDaftarBank.Text.Contains("Syariah") == false )
                        {
                            break;
                        };
                    }
                    
                }
            }
                
        }

        public string Kode
        {
            
            get{
                ListItemData it = (ListItemData)cmbDaftarBank.Items[cmbDaftarBank.SelectedIndex];
                return it.Kode;
            }
        }
        public string KeteranganNamaBank
        {

            get
            {
                return txtKeteranganBank.Text.Trim()+ " ";
            }
            set
            {
                txtKeteranganBank.Text = value;
            }
        }
        public string NamaBank
        {
            get {
                ListItemData it = (ListItemData)cmbDaftarBank.Items[cmbDaftarBank.SelectedIndex];
                if (it != null)
                    return it.ItemText;
                else
                {
                    MessageBox.Show("Ada maslaah pembacaan nama bank");
                    return "";
                }
            }
        }

        private void cmbDaftarBank_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
