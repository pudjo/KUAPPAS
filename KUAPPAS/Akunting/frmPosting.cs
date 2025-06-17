using BP.Akuntansi;
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
    public partial class frmPosting : Form
    {
        public frmPosting()
        {
            InitializeComponent();
        }

        private void chkSemuaDinas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSemuaDinas.Checked == true)
                ctrlSKPD1.Enabled = false;
            else
                ctrlSKPD1.Enabled = true;

        }

        private void frmPosting_Load(object sender, EventArgs e)
        {
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            ctrlHeader1.SetCaption("Posting Data Jurnal");

            if (GlobalVar.Pengguna.SKPD > 0)
            {
                chkSemuaDinas.Visible = false;
               
                   
                
            }
            else
            {
                chkSemuaDinas.Visible = true;
            }
        }

        private void cmdPosting_Click(object sender, EventArgs e)
        {

            try
            {
                int iddinas = 0;
                if (chkSemuaDinas.Checked == false)
                    iddinas = ctrlSKPD1.GetID();

                if (iddinas == 0)
                {
                    MessageBox.Show("Belum Pilih  OPD");
                    return;
                }
                ProsesJurnalLogic JurnalLogic = new ProsesJurnalLogic(GlobalVar.TahunAnggaran, iddinas);
                if (JurnalLogic.Posting() == true)
                {
                    MessageBox.Show("Proses Posting sudah selesai ");

                }
                else
                {
                    MessageBox.Show("Proses Posting Gagal " + JurnalLogic.LastError());

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


        }

        private void cmdTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
