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
using DataAccess;
using Formatting;

namespace KUAPPAS
{
    public partial class ctrlBulan : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;

        public ctrlBulan()
        {
            InitializeComponent();
        }

        private void cmbBulan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public bool Create()
        {
            bool lRet=true;
            try
            {
                //BulanLogic oLogic = new BulanLogic(GlobalVar.TahunAnggaran);
                //List<Bulan> _lst = new List<Bulan>();
                //_lst = oLogic.Get();
                cmbBulan.Items.Clear();

                ListItemData iJanuari = new ListItemData("Januari", 1);
                cmbBulan.Items.Add(iJanuari);
                ListItemData iFeb = new ListItemData("Februari", 2);
                cmbBulan.Items.Add(iFeb);
                ListItemData iMaret = new ListItemData("Maret", 3);
                cmbBulan.Items.Add(iMaret);
                ListItemData iApril = new ListItemData("April", 4);
                cmbBulan.Items.Add(iApril);
                ListItemData iMei = new ListItemData("Mei", 5);
                cmbBulan.Items.Add(iMei);
                ListItemData iJuni = new ListItemData("Juni", 6);
                cmbBulan.Items.Add(iJuni);
                ListItemData iJuli = new ListItemData("Juli", 7);
                cmbBulan.Items.Add(iJuli);
                ListItemData iAgustus= new ListItemData("Agustus", 8);
                cmbBulan.Items.Add(iAgustus);
                ListItemData iSeptember = new ListItemData("September", 9);
                cmbBulan.Items.Add(iSeptember);
                ListItemData iOktober = new ListItemData("Oktober", 10);
                cmbBulan.Items.Add(iOktober);
                ListItemData iNovember = new ListItemData("November", 11);
                cmbBulan.Items.Add(iNovember);
                ListItemData iDesember = new ListItemData("Desember", 12);
                cmbBulan.Items.Add(iDesember);
      


            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal pembuatan control Bulan." + ex.Message);
                lRet=false;

            }
            return lRet;

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbBulan.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbBulan.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbBulan.Items[i];
                if (li.ItemText == cmbBulan.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }

            return m_SelectedID;
        }
        public void SetBlank()
        {
            cmbBulan.Text = "";
        }

        public DateTime TanggalAwal
        {
            get
            {
                 GetID();
                return new DateTime(GlobalVar.TahunAnggaran,m_SelectedID,1);
            }
        }
        public DateTime TanggalAkhir
        {
            get
            {
                GetID();
                if ( m_SelectedID ==1 ||
                    m_SelectedID == 3 ||
                    m_SelectedID == 5 ||
                    m_SelectedID == 7 ||
                    m_SelectedID == 8 ||
                    m_SelectedID == 10 ||
                    m_SelectedID == 12)
                        return new DateTime(GlobalVar.TahunAnggaran, m_SelectedID, 31);
                        
                  if(    m_SelectedID == 4 ||
                    m_SelectedID == 6 ||
                    m_SelectedID == 9 ||
                    m_SelectedID == 11 )
                        return new DateTime(GlobalVar.TahunAnggaran, m_SelectedID, 30);
            
                     if(    m_SelectedID == 2){
                          if (GlobalVar.TahunAnggaran % 4 ==0)
                              return new DateTime(GlobalVar.TahunAnggaran, m_SelectedID, 29);
                          else 
                              return new DateTime(GlobalVar.TahunAnggaran, m_SelectedID, 28);
                     }

                     return new DateTime(GlobalVar.TahunAnggaran, 12, 31);       

                } 

            
        }
        public bool IsValid()
        {
            if (cmbBulan.Text == "")
                return false;
            return true;

        }
        public string NamaBulan
        {
            get { return cmbBulan.Text; }
        }
        public DateTime GetLastDay()
        {
            GetID();
            if (m_SelectedID == 0)
                return new DateTime((int)GlobalVar.TahunAnggaran, 12, 31);

            else
            
            {
                DateTime d = new DateTime((int)GlobalVar.TahunAnggaran, m_SelectedID, 1);
            
                DateTime retDate = d.GetLastDayOfMonth();//m_SelectedID);// DateFormat.GetLastDayOfMonth()
                return retDate;
            }

        }
        public DateTime GetFirstDay()

        {
            GetID();
            if (m_SelectedID==0 )
                return new DateTime((int)GlobalVar.TahunAnggaran, 1, 1);
            else 
                return new DateTime((int)GlobalVar.TahunAnggaran, m_SelectedID, 1);
        }

        public void SetID(int pID)
        {
            int i;
         //   cmbBulan.Text = "";
            Create();
            ListItemData li = new ListItemData("", 0);

            for (i = 0; i < cmbBulan.Items.Count; i++)
            {
                li = (ListItemData)cmbBulan.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbBulan.SelectedIndex = i;
                    cmbBulan.SelectedItem = li;

                    break;
                }
            }
        }
        private void FireChangeEvent()
        {
            GetID();
            if (OnChanged != null)
            {
                OnChanged(m_SelectedID);
            }
        }
        public int GetSelectedID()
        {
            return m_SelectedID;
        }



        private void cmbBulan_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
        
        public string GetNama()
        {
            return cmbBulan.Text.Trim();
        }
        

    }
}
