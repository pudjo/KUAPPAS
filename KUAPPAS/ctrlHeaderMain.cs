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
namespace KUAPPAS
{
    public partial class ctrlHeaderMain : UserControl
    {
        public ctrlHeaderMain()
        {
            InitializeComponent();
        }
        public void SetTahun(int pTahun)
        {
            label3.Text = GlobalVar.TahunAnggaran.ToString();
          //  label2.Text = "Pemerintah " + GlobalVar.gPemda.NamaPanjang;


        }
      //  public string Tahun 
        private void ctrlHeaderMain_Load(object sender, EventArgs e)
        {
            label3.Text = GlobalVar.TahunAnggaran.ToString();
#if DEBUG
            pictureBox1.Visible= false;
#else
            pictureBox1.Visible = true ;
#endif
        }
    }
}
