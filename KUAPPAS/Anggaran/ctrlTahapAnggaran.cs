using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Anggaran
{
    public partial class ctrlTahapAnggaran : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        public ctrlTahapAnggaran()
        {
            InitializeComponent();
        }

        private void ctrlTahapAnggaran_Load(object sender, EventArgs e)
        {

        }
        public void Create()
        {
            cmbTahapanAnggaran.Items.Clear();


            cmbTahapanAnggaran.Items.Add(new ListItemData("Anggaran Murni", 2));
            cmbTahapanAnggaran.Items.Add(new ListItemData("Anggaran Pergeseran", 3));
            cmbTahapanAnggaran.Items.Add(new ListItemData("Anggaran Perubahan", 4));
            cmbTahapanAnggaran.Items.Add(new ListItemData("Anggaran Pergeseran Perubahan)", 5));
           
        }
        public string Nama
        {
            get
            {
                return cmbTahapanAnggaran.Text;

            }
        }
        public int ID
        {
            set
            {
               
                Create();
                m_SelectedID = value;
                for (int idx = 0; idx < cmbTahapanAnggaran.Items.Count; idx++)
                {
                    ListItemData it = (ListItemData)cmbTahapanAnggaran.Items[idx];

                    if (it.Itemdata == m_SelectedID)
                    {
                        cmbTahapanAnggaran.SelectedIndex = idx;
                        break;
                    }
                }
                

            }
            get
            {
                GetID();
                return m_SelectedID;
            }
        }
        public int GetID()
        {
            if (cmbTahapanAnggaran.Text == "")
            {
                return 0;
            }
            foreach (ListItemData li in cmbTahapanAnggaran.Items)
            {
                if (li.ItemText == cmbTahapanAnggaran.Text)
                {
                    m_SelectedID = li.Itemdata;
                    return li.Itemdata;
                }
            }
            return -1;

        }

    }
}
