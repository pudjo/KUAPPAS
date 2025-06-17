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
    public partial class ctrlBank : UserControl
    {
        public delegate void ValueChangedEventHandler(string  Kode);
        public event ValueChangedEventHandler OnChanged;
        private string  m_SelectedID;

        public ctrlBank()
        {
            InitializeComponent();
        }

        private void ctrlBank_Load(object sender, EventArgs e)
        {

        }
        public int Create()
        {
            try
            {
                cmbBank.Items.Clear();

                BanksLogic o = new BanksLogic(GlobalVar.TahunAnggaran,0);
                List<Banks> lst = o.Get();


                foreach (Banks b in lst)
                {
                    ListItemData item = new ListItemData(b.Nama,b.bankCode);
                    cmbBank.Items.Add(item);
                }
                //  lblUK.Visible = cmbUnitKerja.Items.Count == 0 ? false : true;
                cmbBank.Visible = cmbBank.Items.Count == 0 ? false : true;
                return cmbBank.Items.Count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 0;
            }

        }
        public string KodeBank{
            get { return GetID(); }
            set { SetID(value); }

        }
        public string KodeBic 
        {
            get { return GetBIC(); }
            //set { SetID(value); }

        }

        public string  GetID()
        {
            //ListItemData li = (ListItemData)cmbDesa.SelectedItem;
            m_SelectedID = "";
            for (int i = 0; i < cmbBank.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbBank.Items[i];
                if (li.ItemText == cmbBank.Text)
                {
                    m_SelectedID = li.Kode;

                    break;
                }

            }

            return m_SelectedID;
        }

        public string GetBIC()
        {
            //ListItemData li = (ListItemData)cmbDesa.SelectedItem;
      
            for (int i = 0; i < cmbBank.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbBank.Items[i];
                if (li.ItemText == cmbBank.Text)
                {
                    return li.Kodetambahan;

                    break;
                }

            }

            return "";
        }
        public void SetID(string pID)
        {
            int i;
            ListItemData li = new ListItemData("", "","");
            for (i = 0; i < cmbBank.Items.Count; i++)
            {
                li = (ListItemData)cmbBank.Items[i];
                if (li.Kode == pID)
                {
                    cmbBank.SelectedIndex = i;
                    cmbBank.SelectedItem = li;
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

        private void cmbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
