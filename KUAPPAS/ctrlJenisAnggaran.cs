using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using BP;

namespace KUAPPAS
{
    public partial class ctrlJenisAnggaran : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private int m_iTahap=0;

        public ctrlJenisAnggaran()
        {
            InitializeComponent();
        }

        private void ctrlJenisAnggaran_Load(object sender, EventArgs e)
        {
           
        }
        public void Create(int Tahap)
        {
            m_iTahap= Tahap;
            cmbJenisAnggaran.Items.Clear();

            ListItemData item = new ListItemData("Pendapatan", 1);
            cmbJenisAnggaran.Items.Add(item);
            ListItemData item5 = new ListItemData("Belanja ", 3);
            cmbJenisAnggaran.Items.Add(item5);
            ListItemData item61 = new ListItemData("Penerimaan Pembiayaan", 4);
            cmbJenisAnggaran.Items.Add(item61);
            ListItemData item62 = new ListItemData("Pengeluaran Pembiayaan", 5);
            cmbJenisAnggaran.Items.Add(item62);

            
        }

        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbDesa.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbJenisAnggaran.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbJenisAnggaran.Items[i];
                if (li.ItemText == cmbJenisAnggaran.Text)
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
            Create(m_iTahap);
           
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbJenisAnggaran.Items.Count; i++)
            {
                li = (ListItemData)cmbJenisAnggaran.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbJenisAnggaran.SelectedItem = li;
                    cmbJenisAnggaran.SelectedIndex = i;
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

        private void cmbJenisAnggaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
