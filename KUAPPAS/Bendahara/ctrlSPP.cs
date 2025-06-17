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
    public partial class ctrlSPP : UserControl
    {
        public int m_idUrusan = 0;
        public int m_idProgram = 0;
        public long m_idKegiatan = 0;
        public long m_iNoUrut;
        public int m_idDinas;
        public delegate void ValueChangedEventHandler(long pID);
        public event ValueChangedEventHandler OnChanged;
        private long m_SelectedID;
        private List<SPP> m_listSPP;


        public ctrlSPP()
        {
            InitializeComponent();
            m_listSPP = new List<SPP>();
        }

        private void ctrlSPP_Load(object sender, EventArgs e)
        {

        }
      
        
        
       
        public void Create(int iDDinas, int KodeUK, int jenis= -1, int Status=0)
        {
            //List<SPP> lst = new List<SPP>();
            bool bAlreadyLoad = false;
            SPPLogic oLogic = new SPPLogic((int)GlobalVar.TahunAnggaran);
            ParameterBendahara oParemeter = new ParameterBendahara(GlobalVar.TahunAnggaran);
            oParemeter.IDDInas = iDDinas;
            oParemeter.Jenis = -1; // SUpaya terpanggil semua 
            if (jenis > -1)
            {
                oParemeter.Jenis = jenis;

            }
            oParemeter.Status = Status;

            //if (GlobalVar.gListSPP==null){
            //    GlobalVar.gListSPP= new List<SPP>();
            //    bAlreadyLoad = false;
            //}
            //if (GlobalVar.gListSPP.FindAll(x=>x.IDDInas== iDDinas).Count==  0){
            //    bAlreadyLoad = false;
            //    GlobalVar.gListSPP =oLogic.Get(oParemeter);
            //}
            //else
            //{
             GlobalVar.gListSPP = oLogic.Get(oParemeter);
             bAlreadyLoad = true;
            //}

            List<long> lstNoUrut = new List<long>();
            foreach (SPP spp in GlobalVar.gListSPP)
            {
                lstNoUrut.Add(spp.NoUrut);

            }

            if (GlobalVar.gListSPPRekening == null)
            {
                GlobalVar.gListSPPRekening = new List<SPPRekening>();
            }

            if (bAlreadyLoad == false)
            {
                GlobalVar.gListSPPRekening = oLogic.GetDetail(lstNoUrut);
            }


            if (jenis != -1)
            {
                m_listSPP = GlobalVar.gListSPP.FindAll(x => x.IDDInas == iDDinas && x.Jenis == jenis && x.Status == Status);
            }
            else
            {
                m_listSPP = GlobalVar.gListSPP.FindAll(x => x.IDDInas == iDDinas && x.Status == Status);
            }
            cmbSPP.Items.Clear();

            if (m_listSPP != null)
            {
                ListItemData item;
                foreach (SPP k in m_listSPP)
                {
                    if (Status == 1)
                    {
                         item = new ListItemData(
                            k.NoSPM+ "- (" + k.Jumlah.ToRupiahInReport() + ")", k.NoUrut, k);
                    }
                    else
                    {
                        item = new ListItemData(
                            k.NoSP2D + "- (" + k.Jumlah.ToRupiahInReport() + ")", k.NoUrut, k);
                    }
                    cmbSPP.Items.Add(item);
                    
                }
            }
        }
        public long GetID()
        {
            //ListItemData li = (ListItemData)cmbSPP.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbSPP.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSPP.Items[i];
                if (li.ItemText == cmbSPP.Text)
                {
                    m_SelectedID = li.lItemData;
                    break;
                }

            }

            return m_SelectedID;
        }
        public void Clear()
        {
            cmbSPP.Text = "";
            m_SelectedID = -1;
            cmbSPP.Items.Clear();

        }
        public void SetID(long  pID)
        {
            int i;

            m_SelectedID = pID;
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbSPP.Items.Count; i++)
            {
                li = (ListItemData)cmbSPP.Items[i];
                if (li.lItemData == Convert.ToInt64(pID))
                {
                    cmbSPP.SelectedIndex = i;
                    break;
                }
            }
        }
        public long  ID
        {
            set
            {
                SetID(value);
            }
            get
            {
                return GetID();
            }
        }

        public SPP GetSPP(bool withDetail = true)
        {
            if (m_SelectedID == 0)
                GetID();

            SPP spp = m_listSPP.FirstOrDefault(x => x.NoUrut == m_SelectedID);
            if (spp != null)
            {
                if (withDetail == true)
                {
                    SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                    spp.Rekenings = oLogic.GetDetail(m_SelectedID);
                }
                else
                {
                    spp.Rekenings = new List<SPPRekening>();
                }
            }

            return spp;
   
        }
        public SPP DataSPP
        {
            get
            {
                SPP oSPP;
                oSPP = null;
                for (int i = 0; i < cmbSPP.Items.Count; i++)
                {
                    ListItemData li = (ListItemData)cmbSPP.Items[i];
                    if (li.ItemText == cmbSPP.Text)
                    {
                        oSPP = new SPP();
                        oSPP = (SPP)li.something;
                        break;
                    }

                }

                return oSPP;
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
            FireChangeEvent();
        }
    }
}
