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
using Formatting;


namespace KUAPPAS
{
    public partial class ctrlKategoriBaru : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        

        public ctrlKategoriBaru()
        {
            InitializeComponent();
        }
        

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        public void Create()
        {
            try
            {
                cmbKategori.Items.Clear();

                KategoriBaruLogic o = new KategoriBaruLogic(GlobalVar.TahunAnggaran);
                List<KategoriBaru> lst = o.Get();
                if (lst != null)
                {
                    foreach (KategoriBaru kb in lst)
                    {
                        ListItemData item = new ListItemData(kb.ID.ToString() + "  " + kb.Nama, kb.ID);
                        cmbKategori.Items.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public int GetID()
        {
            
            m_SelectedID = 0;
            for (int i = 0; i < cmbKategori.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbKategori.Items[i];
                if (li.ItemText == cmbKategori.Text)
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
            for (i = 0; i < cmbKategori.Items.Count; i++)
            {
                li = (ListItemData)cmbKategori.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbKategori.SelectedIndex = i;
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

        

        private void cmbKategori_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
