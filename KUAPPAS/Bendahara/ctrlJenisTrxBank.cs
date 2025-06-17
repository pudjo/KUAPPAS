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
    public partial class ctrlJenisTrxBank : UserControl
    {
        public delegate void ValueChangedEventHandler(long pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        
        public ctrlJenisTrxBank()
        {
            InitializeComponent();
        }

        private void ctrlJenisTrxBank_Load(object sender, EventArgs e)
        {
        }
        public void Create(){
            
                    //ListItemData item = new ListItemData("Penerimaan" ,0);

                    //cmbJenisTrxBank.Items.Add(item);
                    //ListItemData itemb = new ListItemData("Pembayaran", 1);

                    //cmbJenisTrxBank.Items.Add(itemb);
                    //ListItemData itemt = new ListItemData("Transfer Antar Rekening", 2);
                    //cmbJenisTrxBank.Items.Add(itemt);

                    ListItemData itemc = new ListItemData("Pencairan", 3);
                    cmbJenisTrxBank.Items.Add(itemc);
                    //ListItemData itemo = new ListItemData("Penyetoran", 4);

                    //cmbJenisTrxBank.Items.Add(itemo);

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbKontrak.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbJenisTrxBank.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbJenisTrxBank.Items[i];
                if (li.ItemText == cmbJenisTrxBank.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }

            return m_SelectedID;
        }
        public void SetID(int pID)
        {
            int i;
            m_SelectedID = pID;

            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbJenisTrxBank.Items.Count; i++)
            {
                li = (ListItemData)cmbJenisTrxBank.Items[i];
                if (li.Itemdata == Convert.ToInt64(pID))
                {
                    cmbJenisTrxBank.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
