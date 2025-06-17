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
using Formatting;
namespace KUAPPAS
{
    public partial class ctrlUrusan : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        
        public ctrlUrusan()
        {
            InitializeComponent();
        }

        private void cmbUrusan_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();

        }

        
        public void Create()
        {
            try
            {
                cmbUrusan.Items.Clear();

                UrusanLogic o = new UrusanLogic(GlobalVar.TahunAnggaran);
                List<Urusan> lst = o.Get();
                var query = from sk in lst
                            orderby sk.KodeKategori, sk.KodeUrusan
                            select sk;
                foreach (Urusan p in query)
                {
                    ListItemData item = new ListItemData(p.Tampilan + " " + p.Nama, p.ID);
                    cmbUrusan.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateWithAll()
        {
            try
            {
                cmbUrusan.Items.Clear();

                UrusanLogic o = new UrusanLogic(GlobalVar.TahunAnggaran);
                List<Urusan> lst = o.Get();
                var query = from sk in lst
                            orderby sk.KodeKategori, sk.KodeUrusan
                            select sk;
                ListItemData _item = new ListItemData("Program Untuk Semua Urusan" , 0);
                cmbUrusan.Items.Add(_item);
                foreach (Urusan p in query)
                {
                    ListItemData item = new ListItemData(p.Tampilan + " " + p.Nama, p.ID);
                    cmbUrusan.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbUrusan.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbUrusan.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbUrusan.Items[i];
                if (li.ItemText == cmbUrusan.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }
            //if (li != null)
            //{
            //    m_SelectedID = li.Itemdata;
            //}
            //else
            //{
            //    m_SelectedID = 0;
            //}

            return m_SelectedID;
        }

        public void SetID(int pID)
        {
            int i;
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbUrusan.Items.Count; i++)
            {
                li = (ListItemData)cmbUrusan.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbUrusan.SelectedIndex = i;
                    break;
                }
            }
        }
        private void FireChangeEvent()
        {
            if (OnChanged != null)
            {
                OnChanged(m_SelectedID);
            }
        }
        public int GetSelectedID()
        {
            return m_SelectedID;
        }

        private void ctrlUrusan_Load(object sender, EventArgs e)
        {

        }
        public int KodeKategori()
        {
            if (m_SelectedID == 0)
                GetID();
            if (m_SelectedID.ToString().Length >= 3)
            {
                return DataFormat.GetInteger(m_SelectedID.ToString().Substring(0, 1));
            }
            else return 0;

        }
        public int KodeUrusan()
        {
            if (m_SelectedID == 0)
                GetID();
            if (m_SelectedID.ToString().Length == 3)
            {
                return DataFormat.GetInteger(m_SelectedID.ToString().Substring(1, 2));
            }
            else return 0;

        }
        /*
         * 
         *  */
    }
}
