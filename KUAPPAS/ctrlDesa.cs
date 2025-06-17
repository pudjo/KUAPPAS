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
    public partial class ctrlDesa : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        public ctrlDesa()
        {
            InitializeComponent();
        }

        private void ctrlDesa_Load(object sender, EventArgs e)
        {

        }



        private void cmbDesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        public void Create(int _pIDKecamatan)
        {
            try
            {
                cmbDesa.Items.Clear();

                DesaLogic o = new DesaLogic(GlobalVar.TahunAnggaran);
                List<Desa> lst = o.Get();
                var query = from sk in lst
                            where sk.Kecamatan == _pIDKecamatan
                            orderby sk.Kode
                            select sk;
                foreach (Desa p in query)
                {
                    ListItemData item = new ListItemData(p.Tampilan, p.ID);
                    cmbDesa.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbDesa.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbDesa.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbDesa.Items[i];
                if (li.ItemText == cmbDesa.Text)
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
            for (i = 0; i < cmbDesa.Items.Count; i++)
            {
                li = (ListItemData)cmbDesa.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbDesa.SelectedIndex = i;
                    cmbDesa.SelectedItem = li;
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

        public string NamaDesa
        {
            get { return cmbDesa.Text; }
        }

        private void cmbDesa_Click(object sender, EventArgs e)
        {
          //  FireChangeEvent();
        }
    }
}
