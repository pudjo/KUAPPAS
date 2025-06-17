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
using Formatting;

namespace KUAPPAS
{
    public partial class frmInputKodeRekening : Form
    {
        private bool mbOK;
        long m_lParent;
        public frmInputKodeRekening()
        {
            InitializeComponent();
        }

        private void cmdCek_Click(object sender, EventArgs e)
        {
            long idinputted= DataFormat.GetLong(txtKodeRekening.Text.Replace(".",""));
            if (idinputted.ToString().Length < 12)
            {
                MessageBox.Show("KOde Rekening Tidak benar. Harus 12 angka..");
                return;
            }
            Rekening rek =GlobalVar.gListRekening.FirstOrDefault(x=>x.ID==idinputted);
            if (rek == null)
            {
                MessageBox.Show("Kode Rekening Tidak ada...");
            }
            else
            {
                if (rek.Root < 6)
                {
                    MessageBox.Show("Kode Rekening Harus yang paling rinci");
                    return;
                }

                MessageBox.Show(rek.Nama);
            }
        }
        public long ParentRekening{
            set
            {
                m_lParent = value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            mbOK = true;
            this.Hide();

        }
        public long IDrekening
        {
            get
            {
                 long idinputted= DataFormat.GetLong(txtKodeRekening.Text.Replace(".",""));

                 return idinputted;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mbOK = false;
            this.Hide();
        }

        private void frmInputKodeRekening_Load(object sender, EventArgs e)
        {

            treeRekening1.CreateForJurnal (m_lParent);

        }
        public bool IsOK{
            get
            {
                return mbOK;
            }
        }

        private void treeRekening1_DoubleClicking(Rekening rek)
        {
            if (rek.Root < 6)
            {
                MessageBox.Show("Harus paling rinci");
                return;
            }
            txtKodeRekening.Text = rek.ID.ToString();
            lblNama.Text = rek.Nama;
        }

        private void treeRekening1_Load(object sender, EventArgs e)
        {

        }
    }
}
