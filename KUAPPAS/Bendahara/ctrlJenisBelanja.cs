using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS.Bendahara
{
    public partial class ctrlJenisBelanja : UserControl
    {
        public delegate void ValueChangedEventHandler(long pID);
        public event ValueChangedEventHandler OnChanged;
        private long m_SelectedID;
        
        public ctrlJenisBelanja()
        {
            InitializeComponent();
        }

        private void ctrlJenisBelanja_Load(object sender, EventArgs e)
        {

        }
        
        
        public  void Create()
        {
            
                    ListItemData item = new ListItemData("SP2D Gaji" ,0);

                    cmbJenisBelanja.Items.Add(item);
                    ListItemData itemb = new ListItemData("B L U D", 1);

                    cmbJenisBelanja.Items.Add(itemb);
                    ListItemData itemt = new ListItemData("SP2D LS", 2);

                    cmbJenisBelanja.Items.Add(itemt);
                    ListItemData itemc = new ListItemData("UP/GU", 3);

        }
        public long GetID()
        {
            //ListItemData li = (ListItemData)cmbKontrak.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbJenisBelanja.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbJenisBelanja.Items[i];
                if (li.ItemText == cmbJenisBelanja.Text)
                {
                    m_SelectedID = li.lItemData;
                    break;
                }

            }

            return m_SelectedID;
        }
        public void SetID(long pID)
        {
            int i;
            m_SelectedID = pID;

            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbJenisBelanja.Items.Count; i++)
            {
                li = (ListItemData)cmbJenisBelanja.Items[i];
                if (li.lItemData == Convert.ToInt64(pID))
                {
                    cmbJenisBelanja.SelectedIndex = i;
                    break;
                }
            }
        }

        private void cmbJenisBelanja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
