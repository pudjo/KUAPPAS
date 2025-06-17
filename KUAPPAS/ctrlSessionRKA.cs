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
    public partial class ctrlSessionRKA : UserControl
    {
        public delegate void ValueChangedEventHandler(SessionInputRKA sID);
        public event ValueChangedEventHandler OnChanged;
        private SessionInputRKA m_Selected;
        List<SessionInputRKA> mlst;
        private bool bIsOn;

        public ctrlSessionRKA()
        {
            InitializeComponent();
            mlst= new List<SessionInputRKA> ();
        }

        private void ctrlSessionRKA_Load(object sender, EventArgs e)
        {

        }

        private void cmbSessionRKA_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        public void Create(int IDDInas , long idKegiatan)
        {
            try
            {
                cmbSessionRKA.Items.Clear();
                bIsOn = false;

                SessionInputRKALogic oLogic = new SessionInputRKALogic((int)GlobalVar.TahunAnggaran);
                mlst = new List<SessionInputRKA>();
                mlst = oLogic.Get(IDDInas, idKegiatan);
                
                cmbSessionRKA.Items.Clear();

                ListItemData item0 = new ListItemData("Semua Pengguna..", 0);
                cmbSessionRKA.Items.Add(item0);

                foreach (SessionInputRKA p in mlst)
                {
                    ListItemData item = new ListItemData(p.Nama, p.ID );
                    cmbSessionRKA.Items.Add(item);
                    bIsOn = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public bool IsOn()
        {
            return bIsOn;

        }
        public SessionInputRKA GetID()
        {
            //ListItemData li = (ListItemData)cmbSessionRKA.SelectedItem;
            m_Selected = new SessionInputRKA();

            for (int i = 0; i < cmbSessionRKA.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSessionRKA.Items[i];
                if (li.ItemText == cmbSessionRKA.Text)
                {
                    m_Selected = mlst[i];
                    break;
                }

            }

            return m_Selected;
        }

        public void SetID(int pID)
        {
            int i;
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbSessionRKA.Items.Count; i++)
            {
                li = (ListItemData)cmbSessionRKA.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbSessionRKA.SelectedIndex = i;
                    break;
                }
            }
        }
        private void FireChangeEvent()
        {
            if (OnChanged != null)
            {
                GetID();
                OnChanged(m_Selected);
            }
        }
        public SessionInputRKA GetSelectedID()
        {
            return m_Selected;
        }
        public List<SessionInputRKA> GetList()
        {
            return mlst;
        }



        private void cmbSessionRKA_Click(object sender, EventArgs e)
        {
            //  FireChangeEvent();
        }
    }
}
