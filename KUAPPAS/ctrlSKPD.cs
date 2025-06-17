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
using Formatting;

namespace KUAPPAS
{
    public partial class ctrlSKPD : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private bool bOnLoading;
        public ctrlSKPD()
        {
            InitializeComponent();
            bOnLoading = false;
        }
        private void ctrSKPD_Load(object sender, EventArgs e)
        {
        }
        
        private void cmbSKPD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
        public bool GetStatusInput()
        {
            TahapanAnggaran ta = new TahapanAnggaran();
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            m_SelectedID = GetID();

            ta = oLogic.GetByDinas(m_SelectedID, (int)GlobalVar.TahunAnggaran);
            if (ta.StatusInput == 9)
                return false;
            return true;



        }

        public  Pejabat  GetKepalaDinas (DateTime d)
        {
            try
            {
                PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                Pejabat oKepelaDinas = new Pejabat();
                oKepelaDinas = oLogic.GetKepalaDinas(m_SelectedID, 0,d, 0);
                return oKepelaDinas;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Kesalahan mengambil Data Keapal dinas " + ex.Message);
                return null;
            }

        }
        public Pejabat GetKuaaPenggunaAnggaranPenerimaan(DateTime d)
        {
            try
            {
                PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                Pejabat oKepelaDinas = new Pejabat();
                oKepelaDinas = oLogic.GetKuaaPenggunaAnggaranPenerimaan(m_SelectedID, 0, d, -1);
                return oKepelaDinas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan mengambil Data Keapal dinas " + ex.Message);
                return null;
            }

        }
        public  Pejabat GetBendahara(DateTime d)
        {
            try
            {
                PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                Pejabat oBendahara = new Pejabat();
                oBendahara = oLogic.GetBendaharaDinas(m_SelectedID, 0, d,0);
                return oBendahara;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Kesalahan mengambil Data Keapal dinas " + ex.Message);
                return null;
            }
        }

        public  Pejabat GetBendaharaPenerimaan(DateTime d){
            try
            {
                PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                Pejabat oBendaharaPenerimaan = new Pejabat();
                oBendaharaPenerimaan = oLogic.GetBendaharaPenerimaan(m_SelectedID, 0,d, -1);
                return oBendaharaPenerimaan;
             }
             catch(Exception ex)
             {
                    MessageBox.Show("Kesalahan mengambil Data Keapal dinas " + ex.Message);
                    return null;
             }

        }
         
        public  Pejabat GetPPK(DateTime d){

            try
            {
                PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                Pejabat oPPK = new Pejabat();
                oPPK = oLogic.GetPPKSKPD(m_SelectedID, 0,d);
                return oPPK;
}
            catch(Exception ex)
            {
                MessageBox.Show("Kesalahan mengambil Data Keapal dinas " + ex.Message);
                return null;
            }

        }
        public void Clear()
        {
            cmbSKPD.Text = "";
     
        }
        public void Create( int idskpd=0)
        {
            try
            {
                bOnLoading = true;
                cmbSKPD.Items.Clear();
                cmbSKPD.Enabled = true;
                
                cmbLevel.Items.Clear();

                SKPDLogic o = new SKPDLogic(GlobalVar.TahunAnggaran);
                if (GlobalVar.gListSKPD == null)
                {
                    GlobalVar.gListSKPD = o.Get((int)GlobalVar.TahunAnggaran);
                }

                if (GlobalVar.gListSKPD.Count==0)
                {
                    GlobalVar.gListSKPD = o.Get((int)GlobalVar.TahunAnggaran);
                }

                   
                
                var query = from sk in GlobalVar.gListSKPD
                            orderby sk.ID
                            select sk;

                foreach (SKPD p in query)
                {
                    //if (p.Root == 1)
                    //{
                    //if (GlobalVar.Pengguna.SKPD == 0)
                        if (idskpd==0)
                    {

                        ListItemData item = new ListItemData(p.Tampilan + "  " + p.Nama, p.ID);
                        cmbSKPD.Items.Add(item);
                        cmbLevel.Items.Add(p.Level);

                    }
                    else
                    {

                        if (p.Parent == GlobalVar.Pengguna.SKPD || 
                            p.ID == idskpd)
                        {
                            ListItemData item = new ListItemData(p.Tampilan + "  " + p.Nama, p.ID);
                            cmbSKPD.Items.Add(item);
                            cmbLevel.Items.Add(p.Level);
                        }

                    }
                }
                if (GlobalVar.Pengguna.SKPD > 0)
                {  
                    // Jika Parent 
                                       
                    SetID(GlobalVar.Pengguna.SKPD);
                    //cmbSKPD.Enabled = false;

                }
                bOnLoading = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public void CreateLama()
        {
            try
            {
                cmbSKPD.Items.Clear();
                cmbSKPD.Enabled = true;

                cmbLevel.Items.Clear();


                SKPDLogic o = new SKPDLogic(GlobalVar.TahunAnggaran);

                List<SKPD> lst = o.Get((int)GlobalVar.TahunAnggaran);
                
                var query = from sk in lst
                            orderby sk.ID
                            select sk;
                foreach (SKPD p in query)
                {
                    if (p.Root == 1)
                    {
                        ListItemData item = new ListItemData(p.Tampilan + "  " + p.Nama, p.ID);
                        cmbSKPD.Items.Add(item);
                        cmbLevel.Items.Add(p.Level);

                    }
                }
                if (GlobalVar.Pengguna.SKPD > 0)
                {
                    // Jika Parent 




                    SetID(GlobalVar.Pengguna.SKPD);
                    cmbSKPD.Enabled = false;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }



        public void Create(Single Root)
        {
            try
            {
                cmbSKPD.Items.Clear();
                cmbSKPD.Enabled = true;
                SKPDLogic o = new SKPDLogic(GlobalVar.TahunAnggaran);
                if (GlobalVar.gListSKPD == null)
                {

                    GlobalVar.gListSKPD = o.Get((int)GlobalVar.TahunAnggaran);
                }

                var query = from sk in GlobalVar.gListSKPD
                            where sk.Root == Root
                            orderby sk.ID
                            select sk;

                foreach (SKPD p in query)
                {
                    ListItemData item = new ListItemData(p.Tampilan + "  " + p.Nama, p.ID);
                    cmbSKPD.Items.Add(item);
                }
                if (GlobalVar.Pengguna.SKPD > 0)
                {
                    SetID(GlobalVar.Pengguna.SKPD);
                    cmbSKPD.Enabled = false;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateByParent(int Parent)
        {
            try
            {
                cmbSKPD.Items.Clear();
                cmbSKPD.Enabled = true;
                
                if (GlobalVar.gListSKPD == null)
                {
                    SKPDLogic o = new SKPDLogic(GlobalVar.TahunAnggaran);
                    GlobalVar.gListSKPD = o.Get((int)GlobalVar.TahunAnggaran);
                }

                var query = from sk in GlobalVar.gListSKPD
                            where sk.Parent== Parent
                            orderby sk.ID
                            select sk;
                foreach (SKPD p in query)
                {
                    ListItemData item = new ListItemData(p.Tampilan + "  " + p.Nama, p.ID);
                    cmbSKPD.Items.Add(item);
                }
                if (GlobalVar.Pengguna.SKPD > 0)
                {
                    SetID(GlobalVar.Pengguna.SKPD);
                    cmbSKPD.Enabled = false;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbSKPD.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbSKPD.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSKPD.Items[i];
                if (li.ItemText == cmbSKPD.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }

            return m_SelectedID;
        }

        public void SetID(int pID, int realID =0)
        {
            int i;
            ListItemData li = new ListItemData("", 0);
            if (pID == 0 && realID>0)
            {
                int localKode = pID + 1;
                SKPD oSKPD = new SKPD();
                SKPDLogic oLogic = new SKPDLogic(GlobalVar.TahunAnggaran);

                oSKPD = oLogic.GetByID(realID);
                pID = oSKPD.Parent;
            }
            for (i = 0; i < cmbSKPD.Items.Count; i++)
            {
                li = (ListItemData)cmbSKPD.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbSKPD.SelectedIndex = i;
                    cmbSKPD.SelectedItem = li;
                    m_SelectedID = Convert.ToInt32(pID);
                    
                    break;
                }
            }
        }
        private void FireChangeEvent()
        {
            if (bOnLoading == false){
                GetID();
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


        public int GetTahapanAnggaran( int _pTahun )
        {
            TahapanAnggaran oTahap = new TahapanAnggaran();
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            oTahap = oLogic.GetByDinas(m_SelectedID, _pTahun);
            return oTahap.Tahap;

        }
        private void cmbSKPD_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
        public int IDUrusan()
        {
            return DataFormat.GetInteger(GetID().ToString().Substring(0, 3));
        }
        public int KodeKategori()
        {
            return DataFormat.GetInteger( GetID().ToString().Substring(0, 1));
        }
        public int KodeUrusan()
        {
            return DataFormat.GetInteger(GetID().ToString().Substring(1, 2));
        }

        public string KodeUrusanPemerintahan()
        {
            int _ID = GetID();
            if (_ID> 100){

                string sID = _ID.ToString();
            
            return sID.Substring(0, 1) + "." + sID.Substring(1, 2);
            }  else 
            return "";


            
        }
        public string GetNamaSKPD()
        {
            if (cmbSKPD.Text.Length > 8)
                return cmbSKPD.Text.Substring(8).Trim();
            else
                return "";
        }
        public string KodeOrganisasi()
        {
            int  sID = GetID();//.ToString();
           if (sID> 100)
            return sID.ToString().Substring(0, 1) + "." + sID.ToString().Substring(1, 2) + "." + sID.ToString().Substring(3, 2);
           else return "";


        }
        public string NamaUrusanPemerintahan()
        {
            
            int sID = GetID();//.ToString();
            if (sID > 100)
            {
                int _idUrusan = DataFormat.GetInteger(sID.ToString().Substring(0, 3));
                UrusanLogic oLogic = new UrusanLogic(GlobalVar.TahunAnggaran);
                Urusan u = new Urusan();
                u = oLogic.GetByID(_idUrusan);
                return u.Nama;
            }
            else
            {
                return "";

            }
            
        }
        public int GetLevel()
        {
            //ListItemData li = (ListItemData)cmbSKPD.SelectedItem;
            int iLevel = 0;
            for (int i = 0; i < cmbSKPD.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSKPD.Items[i];
                if (li.ItemText == cmbSKPD.Text)
                {
                    iLevel = (int)cmbLevel.Items[i];
                    break;
                }

            }

            return m_SelectedID;

        }
        public int KodeSKPD()
        {
            return DataFormat.GetInteger(GetID().ToString().Substring(3, 2));
        }

        private void ctrlSKPD_Resize(object sender, EventArgs e)
        {
            cmbSKPD.Width = cmbSKPD.Parent.Width-100;
        }
    }
}
