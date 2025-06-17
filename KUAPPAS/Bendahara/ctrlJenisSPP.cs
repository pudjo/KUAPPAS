using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS
{
    public partial class ctrlJenisSPP : UserControl
    {

        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private bool mbLoading;

        public ctrlJenisSPP()
        {
            InitializeComponent();
            cmbJenisSPP.Items.Clear();
            mbLoading = false;

            //cmbJenisSPP.Items.Add(new ListItemData( "UP  Uang Persediaan",0));
            //cmbJenisSPP.Items.Add(new ListItemData("GU  Ganti Uang",1));
            //cmbJenisSPP.Items.Add(new ListItemData("TU  Tambahan Uang",2));
            //cmbJenisSPP.Items.Add(new ListItemData("LS  LS Barang dan Jasa",3));
            //cmbJenisSPP.Items.Add(new ListItemData("Gaji/Tunjangan(Gaji dan Tunjangan)",4));
            //cmbJenisSPP.Items.Add(new ListItemData("LS PPKD(Pejabat Pengelola Keuangan Daerah)",5));
        

        }
        public void Clear()
        {
            cmbJenisSPP.SelectedIndex = -1;

        }
        private void cmbJenisSPP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mbLoading == false)
            {
                m_SelectedID = cmbJenisSPP.SelectedIndex;//.Items.IndexOf(cmbJenisSPP.Text);
                if (OnChanged != null)
                    OnChanged(m_SelectedID);
            }

        }
        public void Create(bool WithAllChoice=false,bool bendaharaPenerimaanPembantu= false )
        {
            mbLoading = false;
            cmbJenisSPP.Items.Clear();

            if (bendaharaPenerimaanPembantu == false)
            {
                if (WithAllChoice == true)
                {
                    cmbJenisSPP.Items.Add(new ListItemData("  * (Semua Jenis)", -1));

                }

                cmbJenisSPP.Items.Add(new ListItemData("UP   (Uang Persediaan)", 0));
                cmbJenisSPP.Items.Add(new ListItemData("GU   (Ganti Uang)", 1));
                cmbJenisSPP.Items.Add(new ListItemData("TU   (Tambahan Uang)", 2));
                cmbJenisSPP.Items.Add(new ListItemData("LS   (LS Barang dan Jasa)", 3));
                cmbJenisSPP.Items.Add(new ListItemData("Gaji/Tunjangan(Gaji dan Tunjangan)", 4));
                cmbJenisSPP.Items.Add(new ListItemData("LS PPKD(Pejabat Pengelola Keuangan Daerah)", 5));
            }
            else
            {
                cmbJenisSPP.Items.Add(new ListItemData("LS   (LS Barang dan Jasa)", 3));
                
            }
            mbLoading = false;
        }
        public void SetValue(int value)
        {
            try
            {
               // List<ListItemData> lit = List < ListItemData > cmbJenisSPP.Items;
                if (value ==-1)
                   Create(true );
                else
                    Create();
                m_SelectedID = value;
                for (int idx = 0; idx < cmbJenisSPP.Items.Count; idx++)
                {
                    ListItemData it = (ListItemData)cmbJenisSPP.Items[idx];

                    if (it.Itemdata == m_SelectedID)
                    {
                        cmbJenisSPP.SelectedIndex= idx;
                        break;
                    }
                }


                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan set value Jenis SPP/SPM/SP2D.."+ ex.Message);

            }
        }
        public int GetID()
        {
            if (cmbJenisSPP.Text == "")
            {
                return 8;
            }
            foreach (ListItemData li in cmbJenisSPP.Items)
            {
                if (li.ItemText == cmbJenisSPP.Text)
                {
                    m_SelectedID = li.Itemdata;
                    return li.Itemdata;
                }
            }
            return -1;

        }
        public int ID
        {
            get{
                //ListItemData it = (ListItemData)cmbJenisSPP.SelectedItem;
                //return cmbJenisSPP.SelectedIndex;
                return m_SelectedID;
            }
            set
            {
                SetValue(value);
            }
            
        }

        private void ctrlJenisSPP_Load(object sender, EventArgs e)
        {

        }


    }
}
