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
    public partial class ctrlDusun : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        
        public ctrlDusun()
        {
            InitializeComponent();
        }

        private void cmbDusun_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
        
        public void Create()
        {
            try
            {
                cmbDusun.Items.Clear();

                DusunLogic o = new DusunLogic(GlobalVar.TahunAnggaran);
                List<Dusun> lst = o.Get();
                var query = from sk in lst
                            orderby sk.Kode
                            select sk;
                foreach (Dusun p in query)
                {
                    ListItemData item = new ListItemData(p.Kode + " " + p.Nama, p.ID);
                    cmbDusun.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void Create(int pIDKecamatan, int pIDDesa)
        {
            try
            {
                cmbDusun.Items.Clear();

                DusunLogic o = new DusunLogic(GlobalVar.TahunAnggaran);
                List<Dusun> lst = o.Get(pIDKecamatan, pIDDesa);
                var query = from sk in lst
                            orderby sk.Kode
                            select sk;
                foreach (Dusun p in query)
                {
                    ListItemData item = new ListItemData(p.Kode + " " + p.Nama, p.ID);
                    cmbDusun.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbDusun.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbDusun.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbDusun.Items[i];
                if (li.ItemText == cmbDusun.Text)
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
            for (i = 0; i < cmbDusun.Items.Count; i++)
            {
                li = (ListItemData)cmbDusun.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbDusun.SelectedIndex = i;
                    cmbDusun.SelectedItem = li;

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

        private void ctrlDusun_Load(object sender, EventArgs e)
        {

        }

        private void cmbDusun_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
