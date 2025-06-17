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
    public partial class ctrlRPJMDTujuan : UserControl
    {
        
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;

        public ctrlRPJMDTujuan()
        {
            InitializeComponent();
        }

        private void ctrlRPJMDTujuan_Load(object sender, EventArgs e)
        {

        }

        


        private void cmbTujuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        public void Create(int _pIDKecamatan)
        {
            try
            {
                cmbTujuan.Items.Clear();

                DesaLogic o = new DesaLogic(GlobalVar.TahunAnggaran);
                List<Desa> lst = o.Get();
                var query = from sk in lst
                            where sk.Kecamatan == _pIDKecamatan
                            orderby sk.Kode
                            select sk;
                foreach (Desa p in query)
                {
                    ListItemData item = new ListItemData(p.Tampilan, p.ID);
                    cmbTujuan.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbTujuan.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbTujuan.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbTujuan.Items[i];
                if (li.ItemText == cmbTujuan.Text)
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
            for (i = 0; i < cmbTujuan.Items.Count; i++)
            {
                li = (ListItemData)cmbTujuan.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbTujuan.SelectedIndex = i;
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

        

        private void cmbTujuan_Click(object sender, EventArgs e)
        {
          //  FireChangeEvent();
        }
    }
}
