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
using Formatting;


namespace KUAPPAS
{
    public partial class ctrlUrusanPemerintahan : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private bool m_bInLoad;
        private int mprofile;

        public ctrlUrusanPemerintahan()
        {
            InitializeComponent();
            m_bInLoad = true;
            mprofile = 2;
           
        }
        public int Proffile
        {
            set { mprofile = value; }
        }
        private void ctrUrusanPemerintahan_Load(object sender, EventArgs e)
        {

        }

        public void Clear()
        {
            cmbUrusanPemerintahan.SelectedIndex = -1;

        }
        private void cmbUrusanPemerintahan_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();

        }

        
        public void Create(int _pIDDInas, int _pTahun, int idurusan=0 )
        {
            try
            {
                m_bInLoad = true;
                cmbUrusanPemerintahan.Items.Clear();

                UrusanDinasLogic o = new UrusanDinasLogic(GlobalVar.TahunAnggaran, mprofile);
                
                List<UrusanDinas> lst = o.GetByIDDinas(_pIDDInas,_pTahun);
                
                var query = from sk in lst
                            orderby sk.IDUrusan
                            select sk;
                
                
                
                cmbUrusanPemerintahan.Items.Clear();
                foreach (UrusanDinas p in query)

                {
                    ListItemData item = new ListItemData(p.IDUrusan.ToString().Substring(0, 1) + "." + p.IDUrusan.ToString().Substring(1)  + "  " + p.NamaUrusan, p.IDUrusan);
                    if (idurusan == 0)
                    {

                        cmbUrusanPemerintahan.Items.Add(item);
                    }
                    else
                    {
                        if (idurusan == p.IDUrusan)
                        {
                            cmbUrusanPemerintahan.Items.Add(item);
                        }
                    } 

                }
                cmbUrusanPemerintahan.SelectedIndex = 0; 
                m_bInLoad = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void Show(List<SPPRekening> lst)
        {
            try
            {
                m_bInLoad = true;
               cmbUrusanPemerintahan.Items.Clear();
               int oldID = 0;
               if (lst.Count != 0)
               {
                   foreach (SPPRekening sp in lst)
                   {

                       if (oldID != sp.IDUrusan)
                       {
                           ListItemData item = new ListItemData(sp.IDUrusan.ToString().Substring(0, 1) + "." + sp.IDUrusan.ToString().Substring(1) + "  " + sp.NamaUrusan, sp.IDUrusan);
                           cmbUrusanPemerintahan.Items.Add(item);
                           oldID = sp.IDUrusan;
                       }


                   }
               }

               if (cmbUrusanPemerintahan.Items.Count > 0)
               {
                   cmbUrusanPemerintahan.SelectedIndex = 0;
               }
                m_bInLoad = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateBasedSPD(int _pIDDInas, int _pTahun, long inourut)
        {
            try
            {
                m_bInLoad = true;
                cmbUrusanPemerintahan.Items.Clear();

                UrusanDinasLogic o = new UrusanDinasLogic(GlobalVar.TahunAnggaran, mprofile);
                List<UrusanDinas> lst = o.GetByIDDinasAndSPD(_pIDDInas, _pTahun, inourut);
                var query = from sk in lst
                            orderby sk.IDUrusan
                            select sk;
                cmbUrusanPemerintahan.Items.Clear();
                foreach (UrusanDinas p in query)
                {
                    ListItemData item = new ListItemData(p.IDUrusan.ToString().Substring(0, 1) + "." + p.IDUrusan.ToString().Substring(1) + "  " + p.NamaUrusan, p.IDUrusan);

                    cmbUrusanPemerintahan.Items.Add(item);
                }
                cmbUrusanPemerintahan.SelectedIndex = 0;

                m_bInLoad = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateBasedBAST(int _pIDDInas, int _pTahun, long inourutBAST)
        {
            try
            {
                m_bInLoad = true;
                cmbUrusanPemerintahan.Items.Clear();

                UrusanDinasLogic o = new UrusanDinasLogic(GlobalVar.TahunAnggaran,3);
                List<UrusanDinas> lst = o.GetByIDDinasAndBAST(_pIDDInas, _pTahun, inourutBAST);
                var query = from sk in lst
                            orderby sk.IDUrusan
                            select sk;
                cmbUrusanPemerintahan.Items.Clear();
                foreach (UrusanDinas p in query)
                {
                    ListItemData item = new ListItemData(p.IDUrusan.ToString().Substring(0, 1) + "." + p.IDUrusan.ToString().Substring(1) + "  " + p.NamaUrusan, p.IDUrusan);

                    cmbUrusanPemerintahan.Items.Add(item);
                }
                cmbUrusanPemerintahan.SelectedIndex = 0;

                m_bInLoad = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateBasedSPJ(int _pIDDInas, int _pTahun, long inourutSPJ)
        {
            try
            {
                m_bInLoad = true;
                cmbUrusanPemerintahan.Items.Clear();

                UrusanDinasLogic o = new UrusanDinasLogic(GlobalVar.TahunAnggaran,3);
                List<UrusanDinas> lst = o.GetByIDDinasAndSPJ(_pIDDInas, _pTahun, inourutSPJ);
                var query = from sk in lst
                            orderby sk.IDUrusan
                            select sk;
                cmbUrusanPemerintahan.Items.Clear();
                foreach (UrusanDinas p in query)
                {
                    ListItemData item = new ListItemData(p.IDUrusan.ToString().Substring(0, 1) + "." + p.IDUrusan.ToString().Substring(1) + "  " + p.NamaUrusan, p.IDUrusan);

                    cmbUrusanPemerintahan.Items.Add(item);
                }
                cmbUrusanPemerintahan.SelectedIndex = 0;

                m_bInLoad = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateBasedSP2D(int _pIDDInas, int _pTahun, long inourutSP2D)
        {
            try
            {
                m_bInLoad = true;
                cmbUrusanPemerintahan.Items.Clear();

                UrusanDinasLogic o = new UrusanDinasLogic(GlobalVar.TahunAnggaran, mprofile);
                List<UrusanDinas> lst = o.GetByIDDinasAndSP2D(_pIDDInas, _pTahun, inourutSP2D);
                var query = from sk in lst
                            orderby sk.IDUrusan
                            select sk;
                cmbUrusanPemerintahan.Items.Clear();
                foreach (UrusanDinas p in query)
                {
                    ListItemData item = new ListItemData(p.IDUrusan.ToString().Substring(0, 1) + "." + p.IDUrusan.ToString().Substring(1) + "  " + p.NamaUrusan, p.IDUrusan);

                    cmbUrusanPemerintahan.Items.Add(item);
                }
                if (cmbUrusanPemerintahan.Items.Count>0)
                cmbUrusanPemerintahan.SelectedIndex = 0;

                m_bInLoad = false;
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
                cmbUrusanPemerintahan.Items.Clear();

                UrusanLogic o = new UrusanLogic(GlobalVar.TahunAnggaran);
                List<Urusan> lst = o.Get();
                var query = from sk in lst
                            orderby sk.KodeKategori, sk.KodeUrusan
                            select sk;
                ListItemData _item = new ListItemData("Program Untuk Semua Urusan" , 0);
                cmbUrusanPemerintahan.Items.Add(_item);
                foreach (Urusan p in query)
                {
                    ListItemData item = new ListItemData(p.Tampilan + " " + p.Nama, p.ID);
                    cmbUrusanPemerintahan.Items.Add(item);
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
            for (int i = 0; i < cmbUrusanPemerintahan.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbUrusanPemerintahan.Items[i];
                if (li.ItemText == cmbUrusanPemerintahan.Text)
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
        public string GetNama()
        {


            return  cmbUrusanPemerintahan.Text; 
        }

        public void SetID(int pID)
        {
            int i;
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbUrusanPemerintahan.Items.Count; i++)
            {
                li = (ListItemData)cmbUrusanPemerintahan.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbUrusanPemerintahan.SelectedIndex = i;
                    cmbUrusanPemerintahan.SelectedItem = li;

                    if (m_bInLoad==false)
                        FireChangeEvent();

                    break;
                }
            }
        }
        private void FireChangeEvent()
        {
            if (m_bInLoad == false)
            {
                if (OnChanged != null)
                {
                    OnChanged(m_SelectedID);
                }
            }
        }
        public int GetSelectedID()
        {
            return m_SelectedID;
        }

        private void ctrlUrusan_Load(object sender, EventArgs e)
        {

        }

        private void cmbUrusanPemerintahan_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();
        }
    }
}
