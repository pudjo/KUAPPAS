using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Formatting;
using DTO.Bendahara;
using DTO;



namespace KUAPPAS.Bendahara
{
    public partial class frmListLPJUP : Form
    {
        public frmListLPJUP()
        {
            InitializeComponent();
        }

        private void frmListLPJUP_Load(object sender, EventArgs e)
        {
            ctrlPanelPencarian1.Create();


        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            
        }

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {

        }
    }
}
