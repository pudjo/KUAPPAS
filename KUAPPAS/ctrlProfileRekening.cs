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
    public partial class ctrlProfileRekening : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private int mProfile;
        public ctrlProfileRekening()
        {
            InitializeComponent();
            mProfile = 2;
        }
        public int Profile
        {
            set { mProfile = value; }
            get { return mProfile; }
        }
        private void ctrlProfileRekening_Load(object sender, EventArgs e)
        {

        }
        private void cmbProfileRekening_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        public void Create()
        {
            try
            {
                cmbProfileRekening.Items.Clear();

                ProfileRekeningLogic o = new ProfileRekeningLogic(GlobalVar.TahunAnggaran, mProfile);
                List<ProfileRekening> lst = o.Get();
                cmbProfileRekening.Items.Clear();
                if (lst != null)
                {
                    foreach (ProfileRekening p in lst)
                    {
                        ListItemData item = new ListItemData(p.Nama, p.ID);
                        cmbProfileRekening.Items.Add(item);
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
            //ListItemData li = (ListItemData)cmbProfileRekening.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbProfileRekening.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbProfileRekening.Items[i];
                if (li.ItemText == cmbProfileRekening.Text)
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
            for (i = 0; i < cmbProfileRekening.Items.Count; i++)
            {
                li = (ListItemData)cmbProfileRekening.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbProfileRekening.SelectedIndex = i;
                    cmbProfileRekening.SelectedItem = li;
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



        private void cmbProfileRekening_Click(object sender, EventArgs e)
        {
            //  FireChangeEvent();
        }
    }
}
