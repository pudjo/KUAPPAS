using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS
{
    public partial class ctrlHeader : UserControl
    {
        public ctrlHeader()
        {
            InitializeComponent();
        }

        private void ctrlHeader_Load(object sender, EventArgs e)
        {
            this.lblCaption.BackColor= this.lblCaption.Parent.BackColor;
            this.lblCaption2.BackColor = this.lblCaption2.Parent.BackColor;
            this.lblCaption.Text = "";
            this.lblCaption2.Text = "";
        }
        public void SetCaption(string sCaption1, string  sCaption2="")
        {
            this.BackColor = Color.Blue;// LightSteelBlue;

            this.lblCaption.BackColor = this.lblCaption.Parent.BackColor;
            this.lblCaption2.BackColor = this.lblCaption2.Parent.BackColor;

            this.lblCaption.Text = sCaption1;
            this.lblCaption2.Text = sCaption2;
            ;




        }
        
    }
}
