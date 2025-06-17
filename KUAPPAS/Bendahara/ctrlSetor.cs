using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP;
using BP.Bendahara;
using DTO;
using DTO.Bendahara;
using Formatting;
namespace KUAPPAS.Bendahara
{
    public partial class ctrlSetor : UserControl
    {
             public int m_idUrusan = 0;
        public int m_idProgram = 0;
        public long m_idKegiatan = 0;
        public long m_iNoUrut;
        public int m_idDinas;
        public delegate void ValueChangedEventHandler(long pID);
        public event ValueChangedEventHandler OnChanged;
        private long m_SelectedID;
        private List<Setor> m_listSetor;

        public ctrlSetor()
        {
            InitializeComponent();
         
            m_listSetor = new List<Setor>();
        }
    


        

        private void ctrlSetor_Load(object sender, EventArgs e)
        {

        }
      
        
        
       
        public void Create(int iDDinas, int KodeUK, int jenis= -1, int Status=0)
        {
            //List<Setor> lst = new List<Setor>();
            bool bAlreadyLoad = false;
            SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
            ParameterBendahara oParemeter = new ParameterBendahara(GlobalVar.TahunAnggaran);

            oParemeter.IDDInas = iDDinas;
            oParemeter.Jenis = -1; // SUpaya terpanggil semua 
            if (jenis > -1)
            {
                oParemeter.Jenis = jenis;

            }
            DateTime tanggalawal= new DateTime(GlobalVar.TahunAnggaran,1,1);
DateTime tanggalakhir= new DateTime(GlobalVar.TahunAnggaran,12,31);

            oParemeter.Status = Status;
            m_listSetor= new List<Setor>();
             m_listSetor = oLogic.Get(oParemeter);
             bAlreadyLoad = true;
            //}

            List<long> lstNoUrut = new List<long>();
            foreach (Setor Setor in m_listSetor)
            {
                lstNoUrut.Add(Setor.NoUrut);

            }


            
            cmbSetor.Items.Clear();

            if (m_listSetor != null)
            {
                ListItemData item;
                foreach (Setor k in m_listSetor)
                {
                    if (Status == 1)
                    {
                         item = new ListItemData(
                            k.NoBukti + "- (" + k.Jumlah.ToRupiahInReport() + ")", k.NoUrut);
                    }
                    else
                    {
                        item = new ListItemData(
                            k.NoBukti + "- (" + k.Jumlah.ToRupiahInReport() + ")", k.NoUrut);
                    }
                    cmbSetor.Items.Add(item);
                    
                }
            }
        }
        public long GetID()
        {
            //ListItemData li = (ListItemData)cmbSetor.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbSetor.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSetor.Items[i];
                if (li.ItemText == cmbSetor.Text)
                {
                    m_SelectedID = li.lItemData;
                    break;
                }

            }

            return m_SelectedID;
        }
        public void Clear()
        {
            cmbSetor.Text = "";
            m_SelectedID = -1;
            cmbSetor.Items.Clear();

        }
        public void SetID(long  pID)
        {
            int i;

            m_SelectedID = pID;
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbSetor.Items.Count; i++)
            {
                li = (ListItemData)cmbSetor.Items[i];
                if (li.lItemData == Convert.ToInt64(pID))
                {
                    cmbSetor.SelectedIndex = i;
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

        public Setor GetSetor(bool withDetail = true)
        {
            if (m_SelectedID == 0)
                GetID();

            Setor Setor = m_listSetor.FirstOrDefault(x => x.NoUrut == m_SelectedID);
            if (Setor != null)
            {
                
            }

            return Setor;
   
        }
        public Setor DataSetor
        {
            get
            {
                Setor oSetor;
                oSetor = null;
                for (int i = 0; i < cmbSetor.Items.Count; i++)
                {
                    ListItemData li = (ListItemData)cmbSetor.Items[i];
                    if (li.ItemText == cmbSetor.Text)
                    {
                        oSetor = new Setor();
                        oSetor = (Setor)li.something;
                        break;
                    }

                }

                return oSetor;
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

        

        private void cmbSetor_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        private void cmbSetor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        private void ctrlSetor_Load_1(object sender, EventArgs e)
        {
        
        }
    }
    
}
