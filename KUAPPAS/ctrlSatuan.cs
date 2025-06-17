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
    public partial class ctrlSatuan : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        public ctrlSatuan()
        {
            InitializeComponent();
        }

        private void ctrlSatuan_Load(object sender, EventArgs e)
        {

        }
    
        
      
             



        private void cmbSatuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();
        }

        public void Create()
        {
            try
            {
                cmbSatuan.Items.Clear();

                SatuanLogic o = new SatuanLogic(GlobalVar.TahunAnggaran);
                List<Satuan> lst = o.Get();
                var query = from sk in lst
                            orderby sk.ID 
                            select sk;
                foreach (Satuan p in query)
                {
                    ListItemData item = new ListItemData(p.Nama, p.ID);
                    cmbSatuan.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbSatuan.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbSatuan.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSatuan.Items[i];
                if (li.ItemText == cmbSatuan.Text)
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
            for (i = 0; i < cmbSatuan.Items.Count; i++)
            {
                li = (ListItemData)cmbSatuan.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbSatuan.SelectedItem = li;
                    cmbSatuan.SelectedIndex = i;
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

        

        private void cmbSatuan_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
