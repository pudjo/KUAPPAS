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
using Formatting;
using BP;


namespace KUAPPAS
{
    public partial class ctrlProgram : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private int m_iDinas;
        private int m_iUrusan;
        private bool OnLoad;

        public ctrlProgram()
        {
            InitializeComponent();
            OnLoad = false;
        }

        private void ctrlProgram_Load(object sender, EventArgs e)
        {

        }
        
        
        private void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();

        }

        
        public void Create( int Tahun, int dinas, int urusan, int idprogram )
        {
            try
            {
                OnLoad = true;
                cmbProgram.Items.Clear();
                TProgramAPBDLogic oLOgic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran);        


                List<TProgramAPBD> _lst = new List<TProgramAPBD>();
                _lst = oLOgic.GetByDinasAndUrusan(Tahun, dinas, urusan);

                if (_lst != null)
                {
                    foreach (TProgramAPBD p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDProgram.ToKodeProgram() + " " + p.Nama, p.IDProgram);

                        if (idprogram == 0)
                        {
                            cmbProgram.Items.Add(item);
                        }
                        else
                        {
                            if (idprogram == p.IDProgram)
                            {
                                cmbProgram.Items.Add(item);
                            }
                        }
                    }
                    cmbProgram.SelectedIndex = 0; 

                }
                OnLoad = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void Clear()
        {
            cmbProgram.Text = "";
            cmbProgram.SelectedIndex = -1;

        }
        public void Create(int Tahun, int dinas, int idUrusan , List<SPPRekening> lstSPPRekeing=null)
        {
            try
            {
                OnLoad = true;
                cmbProgram.Items.Clear();
                TProgramAPBDLogic oLOgic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran);


                    if (lstSPPRekeing != null)
                    {
                        var query = from p in GlobalVar.gListProgram
                                    join pts in lstSPPRekeing
                                    on p.IDProgram equals pts.IDProgram 
                                    
                                    select new { IDProg = p.IDProgram , Nama= p.Nama };

                        int oldIDrog = 0;
                        foreach (var p in query)
                        {
                            if (p.IDProg != oldIDrog)
                            {
                                
                                    ListItemData item = new ListItemData(p.IDProg.ToKodeProgram() + " " + p.Nama, p.IDProg);
                                    cmbProgram.Items.Add(item);
                                    oldIDrog = p.IDProg;
                                
                            }

                            if (cmbProgram.Items.Count>0)
                                cmbProgram.SelectedIndex = 0;
                        }

                    }
                    else
                    {
                        

                        List<TProgramAPBD> lstrogram = new List<TProgramAPBD>();
                        

                        lstrogram = GlobalVar.gListProgram.FindAll(x => x.IDDinas == dinas && x.IDUrusan == idUrusan);
                        int oldIDrog = 0;
                        foreach (TProgramAPBD p in lstrogram)
                        {
                            if (p.IDProgram != oldIDrog)
                            {
                               
                                  
                                        ListItemData itemx = new ListItemData(p.IDProgram.ToKodeProgram() + " " + p.Nama, p.IDProgram);
                                        cmbProgram.Items.Add(itemx);
                                        oldIDrog = p.IDProgram;
                                   
                                
                            }

                        }

                    }


                    //if (cmbProgram.Items.Count > 0)
                    //    cmbProgram.SelectedIndex = 0;
                    OnLoad = false;
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
                cmbProgram.Items.Clear();
                foreach (SPPRekening sp in lst)
                {
                    ListItemData item = new ListItemData(sp.IDProgram.ToString().Substring(0, 1) + "." + sp.IDProgram.ToString().Substring(1,2) + "." + sp.IDProgram.ToString().Substring(3,2) + "   " + sp.NamaProgram, sp.IDProgram);
                    cmbProgram.Items.Add(item);


                }
                cmbProgram.SelectedIndex = 0;
      
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
                OnLoad = true;
                cmbProgram.Items.Clear();

                UrusanLogic o = new UrusanLogic(GlobalVar.TahunAnggaran);
                List<Urusan> lst = o.Get();
                var query = from sk in lst
                            orderby sk.KodeKategori, sk.KodeUrusan
                            select sk;
                ListItemData _item = new ListItemData("Program Untuk Semua Urusan" , 0);
                cmbProgram.Items.Add(_item);
                foreach (Urusan p in query)
                {
                    ListItemData item = new ListItemData(p.Tampilan + " " + p.Nama, p.ID);
                    cmbProgram.Items.Add(item);
                }
                OnLoad = false ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public string GetNamaProgram()
        {
            return DataFormat.RemoveDigits(cmbProgram.Text.Trim());
        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbProgram.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbProgram.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbProgram.Items[i];
                if (li.ItemText == cmbProgram.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }
          
            return m_SelectedID;
        }

        public void SetID(int pID)
        {
            int i;
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbProgram.Items.Count; i++)
            {
                li = (ListItemData)cmbProgram.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbProgram.SelectedItem = li;
                    cmbProgram.SelectedIndex = i;
                    break;
                }
            }
        }
        private void FireChangeEvent()
        {
            if (OnLoad == false)
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

        public int KodeKategori()
        {
            if (m_SelectedID == 0)
                GetID();
            if (m_SelectedID.ToString().Length >= 3)
            {
                return DataFormat.GetInteger(m_SelectedID.ToString().Substring(0, 1));
            }
            else return 0;

        }
        public int KodeUrusan()
        {
            if (m_SelectedID == 0)
                GetID();
            if (m_SelectedID.ToString().Length == 3)
            {
                return DataFormat.GetInteger(m_SelectedID.ToString().Substring(1, 2));
            }
            else return 0;

        }
        public int KodeProgram()
        {
            if (m_SelectedID == 0)
                GetID();
            if (m_SelectedID.ToString().Length > 3)
            {
                return DataFormat.GetInteger(m_SelectedID.ToString().Substring(3, 2));
            }
            else return 0;

        }
        public void CreateBasedSPD(int Tahun, int dinas, int urusan, long inourutSPD)
        {
            try
            {
                OnLoad = true;
                cmbProgram.Items.Clear();
                TProgramAPBDLogic oLOgic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran);



                List<TProgramAPBD> _lst = new List<TProgramAPBD>();
                _lst = oLOgic.GetByDinasAndUrusanAndSPD(Tahun, dinas, urusan, inourutSPD);

                if (_lst != null)
                {
                    foreach (TProgramAPBD p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDProgram.ToKodeProgram() + " " + p.Nama, p.IDProgram);
                        cmbProgram.Items.Add(item);
                    }
                }
                OnLoad =   false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateBasedBAST(int Tahun, int dinas, int urusan, long inourutSPD)
        {
            try
            {
                OnLoad = true;

                cmbProgram.Items.Clear();
                TProgramAPBDLogic oLOgic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran);



                List<TProgramAPBD> _lst = new List<TProgramAPBD>();
                _lst = oLOgic.GetByDinasAndUrusanAndBAST(Tahun, dinas, urusan, inourutSPD);

                if (_lst != null)
                {
                    foreach (TProgramAPBD p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDProgram.ToKodeProgram() + " " + p.Nama, p.IDProgram);
                        cmbProgram.Items.Add(item);
                    }
                }
                OnLoad = false ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateBasedSPJ(int Tahun, int dinas, int urusan, long inourutSPJ)
        {
            try
            {
                OnLoad = true;
                cmbProgram.Items.Clear();
                TProgramAPBDLogic oLOgic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran);



                List<TProgramAPBD> _lst = new List<TProgramAPBD>();
                _lst = oLOgic.GetByDinasAndUrusanAndSPJ(Tahun, dinas, urusan, inourutSPJ);

                if (_lst != null)
                {
                    foreach (TProgramAPBD p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDProgram.ToKodeProgram() + " " + p.Nama, p.IDProgram);
                        cmbProgram.Items.Add(item);
                    }
                }
                OnLoad = false ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateBasedSP2D(int Tahun, int dinas, int urusan, long inourutSP2D)
        {
            try
            {
                OnLoad = true;
                cmbProgram.Items.Clear();
                TProgramAPBDLogic oLOgic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran);



                List<TProgramAPBD> _lst = new List<TProgramAPBD>();
                _lst = oLOgic.GetByDinasAndUrusanAndSP2D(Tahun, dinas, urusan, inourutSP2D);

                if (_lst != null)
                {
                    foreach (TProgramAPBD p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDProgram.ToKodeProgram() + " " + p.Nama, p.IDProgram);
                        cmbProgram.Items.Add(item);
                    }
                }
                OnLoad = false ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
