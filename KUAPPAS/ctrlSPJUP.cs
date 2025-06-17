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
using BP.Bendahara;

namespace KUAPPAS
{
    public partial class ctrlSPJUP : UserControl
    {
        public int m_idUrusan = 0;
        public int m_idProgram = 0;
        public long m_idKegiatan = 0;
        public long m_iNoUrut;
        public int m_idDinas;
        public delegate void ValueChangedEventHandler(long pID);
        public event ValueChangedEventHandler OnChanged;
        private long m_SelectedID;
        private List<SPJ> mlst;
        private bool OnCreate;
        public ctrlSPJUP()
        {
            InitializeComponent();
            List<Fungsional> lst = new List<Fungsional>();
            OnCreate = true;
        }

        private void ctrlSPJUP_Load(object sender, EventArgs e)
        {

        }
       
        

        private void ctrlSPJ_Load(object sender, EventArgs e)
        {

        }
        
       
        public void Create(int iDDinas, int iJenisi,long iNourut =0)
        {

            try
            {
                SPJLogic oLogic = new SPJLogic((int)GlobalVar.TahunAnggaran);

                mlst = oLogic.GetByDInasAndJenis(iDDinas, iJenisi);

                if (oLogic.IsError() == true || mlst == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return;
                }
                cmbSPJ.Items.Clear();

                int selectedIndex = 0;
                int idx = 0;
                if (mlst != null)
                {
                    foreach (SPJ k in mlst)
                    {
                        ListItemData item = new ListItemData(k.NoSPJ, k.NoUrut);
                        cmbSPJ.Items.Add(item);

                        if (iNourut > 0)
                        {
                            if (k.NoUrut == iNourut)
                            {
                                selectedIndex = idx;
                            }
                        }
                        idx++;

                    }
                    if (cmbSPJ.Items.Count>0 && selectedIndex >= 0)
                    {
                        cmbSPJ.SelectedIndex = selectedIndex;
                    }
                }
                OnCreate = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public long GetID()
        {
            //ListItemData li = (ListItemData)cmbSPJ.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbSPJ.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSPJ.Items[i];
                if (li.ItemText == cmbSPJ.Text)
                {
                    m_SelectedID = li.lItemData;
                    break;
                }

            }

            return m_SelectedID;
        }

        public void SetID(long  pID)
        {
            int i;
            m_SelectedID = pID;

            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbSPJ.Items.Count; i++)
            {
                li = (ListItemData)cmbSPJ.Items[i];
                if (li.lItemData == Convert.ToInt64(pID))
                {
                    cmbSPJ.SelectedIndex = i;
                    break;
                }
            }
        }
        public SPJ GetSPJ()
        {
            if (m_SelectedID == 0)
                GetID();
            foreach (SPJ k in mlst)
            {
                if (k.NoUrut== m_SelectedID){
                    return k;
                }
            }
            return null;
   
        }
        private void FireChangeEvent()
        {
            if (OnCreate == false)
            {
                if (OnChanged != null)
                {
                    GetID();
                    OnChanged(m_SelectedID);
                }
            }
        }
        public long GetSelectedID()
        {
            return m_SelectedID;
        }

        

        private void cmbSPJ_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        private void cmbSPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
        public bool CekApakahSPJUPSudahDipakai()
        {
            SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);

            GetSelectedID();
            return oLogic.CekApakahSPJUPSudahDipakai(m_SelectedID);



        }
    }
}
