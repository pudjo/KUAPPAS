using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Formatting;


namespace KUAPPAS.Bendahara
{
    public partial class ctrlJenisSKRSKPD : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        public ctrlJenisSKRSKPD()
        {
            InitializeComponent();
        }

        private void ctrlJenisSKRSKPD_Load(object sender, EventArgs e)
        {

        }
          public void Create()
        {

            ListItemData item = new ListItemData("Retribusi", 0);
            cmbJenisSKRSKPD.Items.Add(item);

            ListItemData itemx = new ListItemData("Pajak", 1);
            cmbJenisSKRSKPD.Items.Add(itemx);


        }
        public void SetID(int pID)
        {
            cmbJenisSKRSKPD.SelectedIndex = pID;

        }
        public int GetID()
        {
            return cmbJenisSKRSKPD.SelectedIndex;

        }
    }
    
}
