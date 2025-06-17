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
    public partial class ctrlSKPDSKPD : UserControl
    {
        public delegate void ValueChangedEventHandler(long pID);
        public event ValueChangedEventHandler OnChanged;
        public List<SKRSKPD> m_lst;
        private long m_SelectedID;
        public ctrlSKPDSKPD()
        {
            InitializeComponent();
            m_lst = new List<SKRSKPD>();
        }

        private void ctrlSKPDSKPD_Load(object sender, EventArgs e)
        {

        }
        public void Create(int iDDinas)
        {
            //List<SPP> lst = new List<SPP>();
            SKRSKPDLogic oLogic = new SKRSKPDLogic((int)GlobalVar.TahunAnggaran);
            //List<SKRSKPD> mListUnit = new List<SKRSKPD>();


        

            //m_lst = oLogic.GetByDinas(iDDinas);
            //cmbSKRSKPD.Items.Clear();
            //if (m_lst != null)
            //{
            //    foreach (SKRSKPD k in m_lst)
            //    {
            //        ListItemData item = new ListItemData(k.NoBukti, k.NoUrut);
            //        cmbSKRSKPD.Items.Add(item);

            //    }
            //}
        }
        public long GetID()
        {
            //ListItemData li = (ListItemData)cmbSPP.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbSKRSKPD.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSKRSKPD.Items[i];
                if (li.ItemText == cmbSKRSKPD.Text)
                {
                    m_SelectedID = li.lItemData;
                    break;
                }

            }

            return m_SelectedID;
        }
        public SKRSKPD GetSKRSKPD()
        {
            if (m_SelectedID == 0)
                GetID();
            foreach (SKRSKPD k in m_lst)
            {
                if (k.NoUrut== m_SelectedID)
                {
                    return k;
                }
            }
            return null;

        }
        public void SetID(long pID)
        {
            int i;
            m_SelectedID = pID;

            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbSKRSKPD.Items.Count; i++)
            {
                li = (ListItemData)cmbSKRSKPD.Items[i];
                if (li.lItemData == Convert.ToInt64(pID))
                {
                    cmbSKRSKPD.SelectedIndex = i;
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



        private void cmbSPP_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        private void cmbSPP_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbSKRSKPD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
