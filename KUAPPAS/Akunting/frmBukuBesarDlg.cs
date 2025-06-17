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
    public partial class frmBukuBesarDlg: Form
    {
        public frmBukuBesarDlg()
        {
            InitializeComponent();
        }

        private void frmBukuBesarDlg_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Buku Besar");
        }
        public int SKPD
        {
            set
            {
                ctrlBukuBesar1.OPD = value;
            }
        }
        public DateTime Tanggal
        {
            set
            {
                ctrlBukuBesar1.TanggalAkhir= value;
            }
        }
        public long IDRekening
        {
            set
            {
                ctrlBukuBesar1.IDRekening = value;

            }
        }
        public string NamaRekening
        {
            set { 
                ctrlBukuBesar1.NamaRekening = value; 
            }
        }
        public void LoadData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (ctrlBukuBesar1.LoadData()== true)
                {
                    Cursor.Current = Cursors.Default;
                    this.Close();
                } else
                {
                    Cursor.Current = Cursors.Default;
                }
                
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool Excell
        {
            set
            {
                ctrlBukuBesar1.Excell = value;

            }
        }
        public string Direktori

        {
            set
            {
                ctrlBukuBesar1.Direktori = value;
            }

        }
        private void ctrlBukuBesar1_Load(object sender, EventArgs e)
        {

        }
    }
}
