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
    public partial class ctrlKontrak : UserControl
    {
        public int m_idUrusan = 0;
        public int m_idProgram = 0;
        public long m_idKegiatan = 0;
        public long m_iNoUrut;
        public int m_idDinas;
        public delegate void ValueChangedEventHandler(long pID);
        public event ValueChangedEventHandler OnChanged;
        private long m_SelectedID;
        private List<Kontrak> m_lstKontrak;

        public ctrlKontrak()
        {
            InitializeComponent();
            m_lstKontrak = new List<Kontrak>();
        }

        private void ctrlKontrak_Load(object sender, EventArgs e)
        {

        }
        public void Clear()
        {
            cmbKontrak.SelectedIndex = -1;

        }
        public bool  Create(int idDinas, DateTime batas)
        {
            try
            {
                //List<Kontrak> lst = new List<Kontrak>();
                KontrakLogic oLogic = new KontrakLogic((int)GlobalVar.TahunAnggaran);
                DateTime awalTahun = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                List<Kontrak> lst = new List<Kontrak>();
             
                    GlobalVar.gListKontrak = new List<Kontrak>();
             m_lstKontrak= oLogic.GetByIDDinasDanBatas(idDinas, batas);

                


              //  m_lstKontrak = GlobalVar.gListKontrak.FindAll(k => k.IDDInas == idDinas && k.DtKontrak <= batas);


                cmbKontrak.Items.Clear();
                if (m_lstKontrak != null)
                {
                    foreach (Kontrak k in m_lstKontrak)
                    {
                        ListItemData item = new ListItemData(k.NoKontrak + " " + k.NamaPerusahaan, k.NoUrut);
                        cmbKontrak.Items.Add(item);

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan memanggil data SPKKontrak "+ ex.Message);
                return false;
            }
        }
        public void Create(int idDinas, DateTime d, string sNoUrut ="")
        {
            //List<Kontrak> lst = new List<Kontrak>();
            KontrakLogic oLogic = new KontrakLogic((int)GlobalVar.TahunAnggaran);
            DateTime awalTahun = new DateTime(GlobalVar.TahunAnggaran, 1, 1);

            m_lstKontrak = oLogic.GetByIDDinas(idDinas, awalTahun, d);
            
            //mListUnit = oLogic.GetByIDDinas(idDinas,d);
            cmbKontrak.Items.Clear();
            if (m_lstKontrak != null)
            {

                foreach (Kontrak k in m_lstKontrak)
                {
                    ListItemData item = new ListItemData(k.NoKontrak + " " + k.NamaPerusahaan, k.NoUrut);
                    //if (sNoUrut ==""){

                        cmbKontrak.Items.Add(item);
                    //} else {

                    //    if (sNoUrut.Trim()== k.NoUrut.ToString ().Trim()){
                    //       cmbKontrak.Items.Add(item);


                    //       break;
                    //    } 

                    //}

                }
            }
            if (sNoUrut != "")
            {
                
                for (int idx = 0; idx < cmbKontrak.Items.Count; idx++)
                {
                    ListItemData li=(ListItemData)cmbKontrak.Items[idx];
                    if (sNoUrut== li.lItemData.ToString()){
                        cmbKontrak.SelectedIndex = idx;
                        break;
                    }
                }
            }
        }
        public void SetKontrak(int idDinas, Kontrak oKontrak)
        {
            if (oKontrak != null){
                cmbKontrak.Items.Clear();
                ListItemData item = new ListItemData(oKontrak.NoKontrak + " " + oKontrak.NamaPerusahaan, oKontrak.NoUrut);
                cmbKontrak.Items.Add(item);
            }
            
        }
        public long GetID()
        {
            m_SelectedID = 0;
            for (int i = 0; i < cmbKontrak.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbKontrak.Items[i];
                if (li.ItemText == cmbKontrak.Text)
                {
                    m_SelectedID = li.lItemData;
                    break;
                }

            }

            return m_SelectedID;
        }


       

        public void SetID(long  pID)
        {
          
            m_SelectedID = pID;

           // ListItemData li = new ListItemData("",0);
            for (  int i = 0; i < cmbKontrak.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbKontrak.Items[i];

                if (li.lItemData == pID)
                {
                    cmbKontrak.SelectedItem = li;

                    cmbKontrak.SelectedIndex = i;
                    
                    break;
                }
            }
        }
        public Kontrak GetKontrak()
        {
            if (m_SelectedID == 0)
                GetID();
            KontrakLogic oLogic=new KontrakLogic(GlobalVar.TahunAnggaran);
            Kontrak k = oLogic.Get(m_SelectedID);
            if (k != null)
                return k;

            return null;
   
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

        

        private void cmbKontrak_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        private void cmbKontrak_SelectedIndexChanged(object sender, EventArgs e)
        {
 
        }

        private void cmbKontrak_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
    }
}
