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
using Formatting;

namespace KUAPPAS
{
    public partial class ctrlUrusanBaru : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        public ctrlUrusanBaru()
        {
            InitializeComponent();
        }

        private void ctrlUrusanBaru_Load(object sender, EventArgs e)
        {

        }
         
        private void cmbUrusanBaru_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        public void Create()
        {
            try
            {
                cmbUrusanBaru.Items.Clear();

                UrusanBaruLogic o = new UrusanBaruLogic(GlobalVar.TahunAnggaran);
                List<UrusanBaru> lst = o.Get();
                if (lst != null)
                {
                    foreach (UrusanBaru kb in lst)
                    {
                        ListItemData item = new ListItemData(kb.Tampilan + "  " + kb.Nama, kb.ID);
                        cmbUrusanBaru.Items.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateByIUrusanLama(int _urusanLama)
        {
            try
            {
                cmbUrusanBaru.Items.Clear();
                UrusanBaruLogic o = new UrusanBaruLogic(GlobalVar.TahunAnggaran);
                List<UrusanBaru> lst = o.GetDariMapping(_urusanLama);
                if (lst != null)
                {
                    foreach (UrusanBaru kb in lst)
                    {
                        ListItemData item = new ListItemData(kb.Tampilan + "  " + kb.Nama, kb.ID);
                        cmbUrusanBaru.Items.Add(item);
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
            
            m_SelectedID = 0;
            for (int i = 0; i < cmbUrusanBaru.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbUrusanBaru.Items[i];
                if (li.ItemText == cmbUrusanBaru.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }

            return m_SelectedID;
        }

        public void SetID(int pID)
        {
            if (cmbUrusanBaru.Items.Count == 0)
                Create();
            int i;
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbUrusanBaru.Items.Count; i++)
            {
                li = (ListItemData)cmbUrusanBaru.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbUrusanBaru.SelectedIndex = i;
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

        

        private void cmbUrusanBaru_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
