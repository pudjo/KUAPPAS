//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;

//namespace KUAPPAS
//{
//    public partial class ctrlSPD : UserControl
//    {
//        public ctrlSPD()
//        {
//            InitializeComponent();
//        }

//        private void ctrlSPD_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}
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


namespace KUAPPAS
{
    public partial class ctrlSPD : UserControl
    {
        public delegate void ValueChangedEventHandler(long  pID);
        public delegate void OnFocusEventHandler();

        public event ValueChangedEventHandler OnChanged;
        public event OnFocusEventHandler OnFocus;
        private long  m_SelectedID;
        private List<SPD> mlstSPD;
        public ctrlSPD()
        {
            InitializeComponent();
            mlstSPD = new List<SPD>();
        }

        private void ctrlSPD_Load(object sender, EventArgs e)
        {

        }
        public void Clear()
        {
            cmbSPD.SelectedIndex = -1;

        }
        private void cmbSPD_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();
        }
        
        public void Create(int _idDinas, DateTime dtBatas, int iJenis, long nourut=0)
        {
            try
            {

                cmbSPD.Items.Clear();
                mlstSPD = new List<SPD>();
                SPDLogic o = new SPDLogic(GlobalVar.TahunAnggaran);
                
                    GlobalVar.gListSPD =new List<SPD>();
                
                    mlstSPD = new List<SPD>();
                    if (nourut == 0)
                    {
                        mlstSPD = o.GetUntukSPP(_idDinas, GlobalVar.TahunAnggaran, dtBatas);
                    }
                    else
                    {
                        mlstSPD = o.GetUntukSPP(_idDinas, GlobalVar.TahunAnggaran, dtBatas, nourut);

                    }
                    
                    GlobalVar.gListSPD = mlstSPD;//.AddRange(mlstSPD);
                    
                    
                 

               
                

                foreach (SPD p in mlstSPD)
                {

                    
                        if (p.NoUrut == 24062170100000813)
                        {
                            p.NoUrut = 24062170100000813;
                        }
                        ListItemData item = new ListItemData(p.NoSPD + "- .... "+p.Jumlah.ToRupiah()   , p.NoUrut);
                        cmbSPD.Items.Add(item);
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public List<SPD> GetListSPDBefore()
        {

            List<SPD> lst = new List<SPD>();
            for (int idx = 0; idx <= cmbSPD.SelectedIndex; idx++)
            {
                SPD oSPD= mlstSPD[idx];
                lst.Add(oSPD); 
            }
            return lst;

        }
        public SPD GetSPD()
        {
            if (cmbSPD.SelectedIndex < 0 || cmbSPD.SelectedIndex > cmbSPD.Items.Count)
            {
                return null;
            }
            GetID();
            SPD oSPD = mlstSPD.FirstOrDefault(x => x.NoUrut == m_SelectedID);

            return oSPD;
        }
 
        public long  GetID()
        {
            //ListItemData li = (ListItemData)cmbSPD.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbSPD.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSPD.Items[i];
                if (li.ItemText  == cmbSPD.Text)
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
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbSPD.Items.Count; i++)
            {
                li = (ListItemData)cmbSPD.Items[i];
                if (li.lItemData == Convert.ToInt64(pID))
                {
                    cmbSPD.SelectedIndex = i;
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



        private void cmbSPD_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        private void ctrlSPD_Enter(object sender, EventArgs e)
        {
            if (OnFocus != null)
                OnFocus();
        }
    }
}

