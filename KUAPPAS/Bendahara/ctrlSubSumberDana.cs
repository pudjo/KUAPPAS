using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO.Bendahara;
using DataAccess;
using Formatting;
using BP;
using BP.Bendahara;

namespace KUAPPAS.Bendahara
{
    public partial class ctrlSubSumberDana : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;

        public ctrlSubSumberDana()
        {
            InitializeComponent();
        }

        private void ctrlSubSumberDana_Load(object sender, EventArgs e)
        {

        }

        
     

        private void cmbSubSumberDana_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();
        }

        public void Create(int iSumberDana)
        {
            try
            {
                cmbSubSumberDana.Items.Clear();

                SubSumberDanaLogic o = new SubSumberDanaLogic((int)GlobalVar.TahunAnggaran);
                List<SubSumberDana> lst = o.Get(iSumberDana);
                var query = from sk in lst
                            orderby sk.IDDetail 
                            select sk;
                foreach (SubSumberDana p in query)
                {
                    ListItemData item = new ListItemData(p.Nama, p.IDDetail);
                    cmbSubSumberDana.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbSubSumberDana.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbSubSumberDana.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSubSumberDana.Items[i];
                if (li.ItemText == cmbSubSumberDana.Text)
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
            for (i = 0; i < cmbSubSumberDana.Items.Count; i++)
            {
                li = (ListItemData)cmbSubSumberDana.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbSubSumberDana.SelectedIndex = i;
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

        

        private void cmbSubSumberDana_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
