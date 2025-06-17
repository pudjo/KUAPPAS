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
    public partial class ctrlKecamatan : UserControl
    {   public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        
        public ctrlKecamatan()
        {
            InitializeComponent();
        }

        private void ctrlKecamatan_Load(object sender, EventArgs e)
        {

        }
             



        private void cmbKecamatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        public void Create()
        {
            try
            {
                cmbKecamatan.Items.Clear();

                KecamatanLogic o = new KecamatanLogic(GlobalVar.TahunAnggaran);
                List<Kecamatan> lst = o.Get();
                var query = from sk in lst
                            orderby sk.Kode
                            select sk;
                foreach (Kecamatan p in query)
                {
                    ListItemData item = new ListItemData(p.Nama, p.ID);
                    cmbKecamatan.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbKecamatan.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbKecamatan.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbKecamatan.Items[i];
                if (li.ItemText == cmbKecamatan.Text)
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
            for (i = 0; i < cmbKecamatan.Items.Count; i++)
            {
                li = (ListItemData)cmbKecamatan.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbKecamatan.SelectedIndex = i;
                    cmbKecamatan.SelectedItem = li;
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
        public string NamaKecamatan
        {
            get { return cmbKecamatan.Text; }
        }
        

        private void cmbKecamatan_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
