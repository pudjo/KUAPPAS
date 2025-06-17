using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using DTO;

namespace KUAPPAS
{
    public partial class ctrlMisi : UserControl
    {
        
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;

        public ctrlMisi()
        {
            InitializeComponent();
        }

        private void ctrlMisi_Load(object sender, EventArgs e)
        {

        }


        private void cmbMisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        public void Create()
        {
            try
            {
                cmbMisi.Items.Clear();

                RPJMDMisiLogic o = new RPJMDMisiLogic(GlobalVar.TahunAnggaran);
                List<RPJMDMisi> lst = o.Get();
                var query = from sk in lst
                            orderby sk.No
                            select sk;
                foreach (RPJMDMisi p in query)
                {
                    ListItemData item = new ListItemData(p.Misi, p.ID);
                    cmbMisi.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbMisi.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbMisi.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbMisi.Items[i];
                if (li.ItemText == cmbMisi.Text)
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
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbMisi.Items.Count; i++)
            {
                li = (ListItemData)cmbMisi.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbMisi.SelectedIndex = i;
                    break;
                }
            }
        }
        private void FireChangeEvent()
        {
            if (OnChanged != null)
            {
                GetID();
                OnChanged(m_SelectedID);
            }
        }
        public int GetSelectedID()
        {
            return m_SelectedID;
        }



        private void cmbMisi_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
