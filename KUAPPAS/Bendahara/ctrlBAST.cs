using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using BP.Bendahara;
using DTO;
using DTO.Bendahara;
using Formatting;


namespace KUAPPAS.Bendahara
{
    public partial class ctrlBAST : UserControl
    {
        public int m_idUrusan = 0;
        public int m_idProgram = 0;
        public long m_idKegiatan = 0;
        public long m_iNoUrut;
        public int m_idDinas;
        public delegate void ValueChangedEventHandler(long pID);
        public event ValueChangedEventHandler OnChanged;
        private long m_SelectedID;
        private List<BAST> _lst;

        public ctrlBAST()
        {
            InitializeComponent();
            _lst = new List<BAST>();
        }

        private void ctrlBAST_Load(object sender, EventArgs e)
        {

        }
        
       
        public void Create(int iDDinas, long  iKontrak, long  iNoBAST =0)
        {
            _lst = new List<BAST>();
            BASTLogic oLogic = new BASTLogic((int)GlobalVar.TahunAnggaran);
            if (iKontrak > 0) { 
                _lst = oLogic.GetByKontrak(iDDinas, iKontrak);
                cmbBAST.Items.Clear();
                if (_lst != null)
                {
                    foreach (BAST k in _lst)
                    {
                        ListItemData item = new ListItemData(k.NoBAST, k.NoUrut,k);
                        //if (iNoBAST == "")
                        //{
                            cmbBAST.Items.Add(item);
                        //}
                        //else
                        //{
                        //    if (iNoBAST == k.NoUrut.ToString().Trim())
                        //    {

                        //        cmbBAST.Items.Add(item);

                        //    }
                        //}

                    }
                }
                if (iNoBAST != 0)
                {

                    for (int idx = 0; idx < cmbBAST.Items.Count; idx++)
                    {
                        ListItemData li = (ListItemData)cmbBAST.Items[idx];
                        if (iNoBAST == li.lItemData)
                        {
                            cmbBAST.SelectedIndex = idx;
                            break;
                        }
                    }
                }
            }
        }
        public bool  CreateByNoKontrak(long iKontrak, int iddinas)
        {
            try
            {
                _lst = new List<BAST>();

                BASTLogic oLogic = new BASTLogic((int)GlobalVar.TahunAnggaran);
                if (iKontrak > 0)
                {
                    //if (GlobalVar.gListBAST == null)
                    //{
                    //    GlobalVar.gListBAST = oLogic.GetByIDDInas(iddinas);

                    //}
                    //if (GlobalVar.gListBAST.FindAll(b => b.IDDInas == iddinas).Count == 0)
                    //{
                    //    _lst = oLogic.GetByIDDInas(iddinas);
                    //    foreach (BAST b in _lst)
                    //    {
                    //        GlobalVar.gListBAST.Add(b);
                    //    }

                    //}

                    _lst = oLogic.GetByIDDInasAndNoKonrak(iddinas, iKontrak);
                  //  _lst = GlobalVar.gListBAST.FindAll(b => b.IDDInas == iddinas && b.NoUrutKontrak == iKontrak);

                    cmbBAST.Items.Clear();
                    if (_lst != null)
                    {
                        foreach (BAST k in _lst)
                        {
                            ListItemData item = new ListItemData(k.NoBAST, k.NoUrut, k);
                            cmbBAST.Items.Add(item);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kesalahan membuat pilihan BAST.." +ex.Message);
                return false;
            }
        }
        public long GetID()
        {
            m_SelectedID = 0;
            for (int i = 0; i < cmbBAST.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbBAST.Items[i];
                if (li.ItemText == cmbBAST.Text)
                {
                    m_SelectedID = li.lItemData;
                    break;
                }

            }

            return m_SelectedID;
        }

        public BAST GetBAST(long noUrut)
        {
            BAST bast = new BAST();
            
            for (int i = 0; i < cmbBAST.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbBAST.Items[i];
                if (li.lItemData == noUrut)
                {
                    bast = (BAST)li.something;
                    break;
                }

            }

            return bast;
        }
        public void SetID(long  pID)
        {
            int i;
            m_SelectedID = pID;

            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbBAST.Items.Count; i++)
            {
                li = (ListItemData)cmbBAST.Items[i];
                if (li.lItemData == Convert.ToInt64(pID))
                {
                    cmbBAST.SelectedIndex = i;
                    FireChangeEvent();
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
        public long GetSelectedID()
        {
            return m_SelectedID;
        }

        

        private void cmbBAST_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        private void cmbBAST_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
