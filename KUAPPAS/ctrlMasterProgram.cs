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
    public partial class ctrlMasterProgram : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;

        public ctrlMasterProgram()
        {
            InitializeComponent();
        }

        private void ctrlMasterProgram_Load(object sender, EventArgs e)
        {

        }
        public void Clear()
        {
            cmbMasterProgram.Items.Clear();
            cmbMasterProgram.SelectedIndex = -1;

        }

        private void cmbMasterProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();

        }


        public void Create(int _pUrusan)
        {
            try
            {
                cmbMasterProgram.Items.Clear();

                MasterProgramLogic o = new MasterProgramLogic(GlobalVar.TahunAnggaran);
                List<MasterProgram> lst = o.Get();
                var query = from sk in lst
                            where sk.IDUrusan == _pUrusan || sk.IDUrusan == 0
                            orderby sk.Kode
                            select sk;
                foreach (MasterProgram p in query)
                {
                    ListItemData item = new ListItemData(p.Kode.ToString("00") + " " + p.Nama, p.ID);
                    cmbMasterProgram.Items.Add(item);
                }
                cmbMasterProgram.SelectedIndex = -1;
                cmbMasterProgram.Text = "";

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public string GetNamaProgram()
        {
            if (cmbMasterProgram.Text.Trim().Length > 2)
            {
                return cmbMasterProgram.Text.Trim().Substring(2);

            }
            return "";

        }
        public void CreateWithPilihan(int _pUrusan)
        {
            try
            {
                cmbMasterProgram.Items.Clear();

                MasterProgramLogic o = new MasterProgramLogic(GlobalVar.TahunAnggaran);
                List<MasterProgram> lst = o.GetByUrusanAndAll(_pUrusan);

                var query = from sk in lst
                            orderby sk.Kode
                            select sk;
                foreach (MasterProgram p in query)
                {
                    ListItemData item = new ListItemData(p.Kode.ToString("00") + " " + p.Nama, p.ID);
                    cmbMasterProgram.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public string NamaProgram()
        {
            return "";// cmbMasterProgram.Text.Substring(2);

        }
        public int GetKode()
        {
            if (cmbMasterProgram.Text.Length > 2)
            {
                return DataFormat.GetInteger(cmbMasterProgram.Text.Substring(0, 2));
            }
            return 0;
        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbMasterProgram.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbMasterProgram.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbMasterProgram.Items[i];
                if (li.ItemText == cmbMasterProgram.Text)
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
            for (i = 0; i < cmbMasterProgram.Items.Count; i++)
            {
                li = (ListItemData)cmbMasterProgram.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbMasterProgram.SelectedIndex = i;
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

//        public delegate void ValueChangedEventHandler(int pID);
//        public event ValueChangedEventHandler OnChanged;
//        private int m_SelectedID;
        
//        public ctrlMasterProgram()
//        {
//            InitializeComponent();
//        }

//        private void ctrlMasterProgram_Load(object sender, EventArgs e)
//        {

//        }
//        public void Clear()
//        {
//            cmbMasterProgram.Items.Clear();
//            cmbMasterProgram.SelectedIndex = -1;
 
//        }
        
//        private void cmbMasterProgram_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            GetID();
//            FireChangeEvent();

//        }

        
//        public void Create(int _pUrusan)
//        {
//            try
//            {
//                cmbMasterProgram.Items.Clear();

//                MasterProgramLogic o = new MasterProgramLogic(GlobalVar.TahunAnggaran);
//                List<MasterProgram> lst = o.Get();
//                var query = from sk in lst
//                            where sk.IDUrusan == _pUrusan || sk.IDUrusan ==0
//                            orderby sk.Kode
//                            select sk;
//                foreach (MasterProgram p in query)
//                {
//                    ListItemData item = new ListItemData(p.Kode.ToString("00")+ " " + p.Nama, p.ID);
//                    cmbMasterProgram.Items.Add(item);
//                }
//                cmbMasterProgram.SelectedIndex = -1;
//                cmbMasterProgram.Text = "";

//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }

//        }
//        public void CreateWithPilihan(int _pUrusan)
//        {
//            try
//            {
//                cmbMasterProgram.Items.Clear();

//                MasterProgramLogic o = new MasterProgramLogic(GlobalVar.TahunAnggaran);
//                List<MasterProgram> lst = o.GetByUrusanAndAll(_pUrusan);

//                var query = from sk in lst                            
//                            orderby sk.Kode
//                            select sk;
//                foreach (MasterProgram p in query)
//                {
//                    ListItemData item = new ListItemData(p.Kode.ToString("00") + " " + p.Nama, p.ID);
//                    cmbMasterProgram.Items.Add(item);
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }

//        }
//        public string NamaProgram()
//        {
//            return "";// cmbMasterProgram.Text.Substring(2);

//        }
//        public int GetKode()
//        {
//            if (cmbMasterProgram.Text.Length > 2)
//            {
//                return DataFormat.GetInteger(cmbMasterProgram.Text.Substring(0, 2));
//            }
//            return 0;
//        }
//        public int GetID()
//        {
//            //ListItemData li = (ListItemData)cmbMasterProgram.SelectedItem;
//            m_SelectedID = 0;
//            for (int i = 0; i < cmbMasterProgram.Items.Count; i++)
//            {
//                ListItemData li = (ListItemData)cmbMasterProgram.Items[i];
//                if (li.ItemText == cmbMasterProgram.Text)
//                {
//                    m_SelectedID = li.Itemdata;
//                    break;
//                }

//            }
//            //if (li != null)
//            //{
//            //    m_SelectedID = li.Itemdata;
//            //}
//            //else
//            //{
//            //    m_SelectedID = 0;
//            //}

//            return m_SelectedID;
//        }

//        public void SetID(int pID)
//        {
//            int i;
//            ListItemData li = new ListItemData("", 0);
//            for (i = 0; i < cmbMasterProgram.Items.Count; i++)
//            {
//                li = (ListItemData)cmbMasterProgram.Items[i];
//                if (li.Itemdata == Convert.ToInt32(pID))
//                {
//                    cmbMasterProgram.SelectedIndex = i;
//                    break;
//                }
//            }
//        }
//        private void FireChangeEvent()
//        {
//            if (OnChanged != null)
//            {
//                OnChanged(m_SelectedID);
//            }
//        }
//        public int GetSelectedID()
//        {
//            return m_SelectedID;
//        }

//    }
//}
