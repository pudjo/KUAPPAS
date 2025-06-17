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
    public partial class ctrlVia : UserControl
    {
        public delegate void ValueChangedEventHandler(Single pID);
        public event ValueChangedEventHandler OnChanged;

        public ctrlVia()
        {
            InitializeComponent();
        }

        private void ctrlVia_Load(object sender, EventArgs e)
        {

        }
        public int  IsBank()
        {
            return chkBank.Checked == true ? 1 : 0;

        }
        public int Bank
        {
            set { chkBank.Checked= value== 1 ? true : false; }
            get { if (chkBank.Checked) return 1; else return 0; }
        }

        public void SetBank(int  iBank)
        {
            chkBank.Checked = iBank == 1 ? true : false;

        }

        private void chkBank_CheckedChanged(object sender, EventArgs e)
        {
            if (OnChanged != null)
                OnChanged(IsBank());

        }


    }
}
