using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using DTO.Bendahara;
using BP;
using BP.Bendahara;
using Formatting;

namespace KUAPPAS.Bendahara
{
    public partial class ctrlCombPotongan : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private List<Potongan> _lst;

        public ctrlCombPotongan()
        {
            InitializeComponent();
            _lst = new List<Potongan>();
        }

        private void ctrlCombPotongan_Load(object sender, EventArgs e)
        {

        }
        public void Create()
        {
            //List<Potongan> lst = new List<Potongan>();
            PotonganLogic oLogic = new PotonganLogic((int)GlobalVar.TahunAnggaran);
            _lst = oLogic.Get();
            cmbPotongan.Items.Clear();
            if (_lst != null)
            {
                foreach (Potongan k in _lst)
                {
                    ListItemData item = new ListItemData(k.Nama, k.IDPotongan);
                    cmbPotongan.Items.Add(item);

                }
            }
        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbPotongan.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbPotongan.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbPotongan.Items[i];
                if (li.ItemText == cmbPotongan.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }

            return m_SelectedID;
        }

        public void SetID(int  pID)
        {
            int i;
            m_SelectedID = pID;

            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbPotongan.Items.Count; i++)
            {
                li = (ListItemData)cmbPotongan.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbPotongan.SelectedIndex = i;
                    break;
                }
            }
        }
        public Potongan GetPotongan()
        {
            if (m_SelectedID == 0)
                GetID();
            foreach (Potongan k in _lst)
            {
                if (k.IDPotongan== m_SelectedID)
                {
                    return k;
                }
            }
            return null;

        }
        private void FireChangeEvent()
        {
            if (OnChanged != null)
            {
                GetID();
                OnChanged(m_SelectedID);
            }
        }
        public long GetSelectedID()
        {
            return m_SelectedID;
        }



        private void cmbPotongan_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        private void cmbPotongan_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }


    }
}
