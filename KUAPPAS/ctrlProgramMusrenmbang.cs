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
    public partial class ctrlProgramMusrenmbang : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        public ctrlProgramMusrenmbang()
        {
            InitializeComponent();
        }

        private void ctrlProgramMusrenmbang_Load(object sender, EventArgs e)
        {

        }
    
        
        
        
        private void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();

        }

        
        public void CreateByIDDInasUrusan(int _pIDDInas , int _pUrusan)
        {
            try
            {
                cmbProgram.Items.Clear();

                ProgramMusrenmbangLogic o = new ProgramMusrenmbangLogic(GlobalVar.TahunAnggaran);
                List<ProgramMusrenmbang> lst = o.GetByIDDInasByUrusan (_pIDDInas, _pUrusan);
                var query = from sk in lst
                            where sk.IDUrusan == _pUrusan 
                            orderby sk.IDProgram
                            select sk;
                foreach (ProgramMusrenmbang p in query)
                {
                    ListItemData item = new ListItemData(p.IDProgram.ToString() +". " +p.NamaProgram , p.IDProgram );
                    cmbProgram.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbProgram.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbProgram.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbProgram.Items[i];
                if (li.ItemText == cmbProgram.Text)
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
            for (i = 0; i < cmbProgram.Items.Count; i++)
            {
                li = (ListItemData)cmbProgram.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbProgram.SelectedIndex = i;
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

    }
}
