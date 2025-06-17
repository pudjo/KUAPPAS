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
    public partial class ctrlBendahara : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        List<Pejabat> m_ListPejabat;

        public ctrlBendahara()
        {
            InitializeComponent();
            m_SelectedID = 0;
            m_ListPejabat = new List<Pejabat>();

        }

        private void ctrlBendahara_Load(object sender, EventArgs e)
        {

        }

        public int Create(int IDDinas, int KodeUK )
        {
            try
            {
                //List<Pejabat> mListUnit = new List<Pejabat>();
                PejabatLogic oLogic = new PejabatLogic(GlobalVar.TahunAnggaran);
                m_ListPejabat = oLogic.GetByJenisAndDinas(7, IDDinas, KodeUK);
                cmbBendahara.Items.Clear();

                if (m_ListPejabat != null)
                {
                    foreach (Pejabat p in m_ListPejabat)
                    {
                        ListItemData item = new ListItemData(p.Nama, p.ID,p);
                        cmbBendahara.Items.Add(item);

                    }
                    return m_ListPejabat.Count;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan pemanggilan data"+ ex.Message);
                return 0;
            }


        }
        public int CreatePenandaTanganSP2D()
        {
            try
            {
                List<Pejabat> _lst = new List<Pejabat>();
                PejabatLogic oLogic = new PejabatLogic(GlobalVar.TahunAnggaran);

                _lst = oLogic.GetKabidPerbend();// GetByJenisAndDinas(7, IDDinas);
                cmbBendahara.Items.Clear();

                if (_lst != null)
                {
                    foreach (Pejabat p in _lst)
                    {
                        ListItemData item = new ListItemData(p.Nama, p.ID,p);
                        cmbBendahara.Items.Add(item);

                    }
                    return _lst.Count;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan pemanggilan data" + ex.Message);
                return 0;
            }


        }

        private void cmbBendahara_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbDusun.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbBendahara.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbBendahara.Items[i];
                if (li.ItemText == cmbBendahara.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }

            return m_SelectedID;
        }

        public Pejabat GetPejabat()
        {
            GetID();

            Pejabat retVal = new Pejabat();
            PejabatLogic oLogic = new PejabatLogic(GlobalVar.TahunAnggaran);
            retVal = oLogic.GetByID(m_SelectedID);

            //for (int i = 0; i < cmbBendahara.Items.Count; i++)
            //{
            //    ListItemData li = (ListItemData)cmbBendahara.Items[i];
            //    if (li.ItemText == cmbBendahara.Text)
            //    {
            //        retVal = (Pejabat)li.something;
            //        return retVal;
            //        break;
            //    }

            //}

            //foreach (Pejabat p in m_ListPejabat){
            //    if (p.ID == m_SelectedID)
            //        return p;
            //}
            return retVal;

        }
        

        public void SetID(int pID)
        {
            int i;
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbBendahara.Items.Count; i++)
            {
                li = (ListItemData)cmbBendahara.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbBendahara.SelectedIndex = i;
                    cmbBendahara.SelectedItem = li;
                    break;
                }
            }
        }
        public bool SetFirst()
        {
            if (cmbBendahara.Items.Count > 0)
            {
                cmbBendahara.SelectedIndex = 0;
                return true;
            }
            return false;
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
