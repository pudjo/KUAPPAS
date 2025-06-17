using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BP;
using DTO.Bendahara;
using BP.Bendahara;
namespace KUAPPAS.Bendahara
{
    public partial class frmBank : Form
    {
        public frmBank()
        {
            InitializeComponent();
        }

        private void frmBank_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Bank");
            this.Text = "Bank";
        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            DaftarBankLogic oLogic = new DaftarBankLogic(GlobalVar.TahunAnggaran);
            GlobalVar.gLstBanks = oLogic.GetBanks();
            DaftarBank bank = new DaftarBank();
            bank.bankCode = txtKode.Text;
            bank.Nama = txtNama.Text;
            if (oLogic.ApaBankSudahAda(bank) == false)
            {
                if (oLogic.Simpan(bank))
                {
                    MessageBox.Show("Data Bank sudah disimpan.");
                }
                else
                {
                    MessageBox.Show("Ada kesalahan penyimpanan Bank.");
                }


            }
            else
            {
                MessageBox.Show("Bank dengan Kode Bank tersebut sudah ada.");
            }
            
        }

        private void cmdBaru_Click(object sender, EventArgs e)
        {
            txtKode.Text = "";
            txtNama.Text = "";


        }
    }
}
