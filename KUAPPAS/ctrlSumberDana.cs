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
    public partial class ctrlSumberDana : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private int mProfile;
        
        public ctrlSumberDana()
        {
            InitializeComponent();
            mProfile = 3;
        }

        private void ctrlSumberDana_Load(object sender, EventArgs e)
        {

        }
        public int Profile {
            set { mProfile = value; }
        }
    
        
      
             



        private void cmbSumberDana_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();
        }
        public string NamaSumberDana
        {
            get
            {
                return cmbSumberDana.Text;

            }
        }
        public void Create()
        {
            try
            {

                cmbSumberDana.Items.Clear();

                SumberDanaLogic o = new SumberDanaLogic(GlobalVar.TahunAnggaran);
                List<SumberDana> lst = o.Get();
                var query = from sk in lst.Where (x=>x.Leaf==1)
                            orderby sk.ID 
                            select sk;
                foreach (SumberDana p in query)
                {
                    ListItemData item = new ListItemData(p.Nama, p.ID);
                    cmbSumberDana.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbSumberDana.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbSumberDana.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSumberDana.Items[i];
                
                if (li.ItemText == cmbSumberDana.Text)
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
            for (i = 0; i < cmbSumberDana.Items.Count; i++)
            {
                li = (ListItemData)cmbSumberDana.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbSumberDana.SelectedIndex = i;
                    cmbSumberDana.SelectedItem = li;
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

        

        private void cmbSumberDana_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
