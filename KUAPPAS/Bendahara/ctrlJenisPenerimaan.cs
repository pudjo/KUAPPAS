using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Formatting;
using DTO;



namespace KUAPPAS.Bendahara
{
    public partial class ctrlJenisPenerimaan : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
       // private int m_SelectedID;

        public ctrlJenisPenerimaan()
        {
            InitializeComponent();
        }

        private void ctrlJenisPenerimaan_Load(object sender, EventArgs e)
        {
           
        }
        public void Create()
        {
           // ListItemData item = new ListItemData("BLUD", 5);

            cmbJenisPenerimaan.Items.Clear();
            ListItemData item = new ListItemData("Semua Jenis", -1);
                
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENERIMAAN_SKPD ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_KASDA ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_PPK ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_AUDIT ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_SUPPORT ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_AKUNTANSI)
            {

                item = new ListItemData("Semua Jenis", -1);
                cmbJenisPenerimaan.Items.Add(item);

                item = new ListItemData("Penerimaan Melalui Bendahara Penerimaan", 0);
                cmbJenisPenerimaan.Items.Add(item);

                item = new ListItemData("Penerimaan Langsung ke KASDA", 1);
                cmbJenisPenerimaan.Items.Add(item);

            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BLUD ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_PPK ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_AUDIT ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_SUPPORT ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_AKUNTANSI)
            {
              
                item = new ListItemData("BLUD", 6);
                cmbJenisPenerimaan.Items.Add(item);
            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BOS ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_PPK ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_AUDIT ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_SUPPORT ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_AKUNTANSI)
            {
                item = new ListItemData("BLUD", 6);
                cmbJenisPenerimaan.Items.Add(item);
               
            }
            


        }
        public void SetID(int pID)
        {
            //List<ListItemData> lstli = (List<ListItemData>)cmbJenisPenerimaan.Items;
            int idx = 0;
            foreach (ListItemData li in cmbJenisPenerimaan.Items)
            {
                if (li.Itemdata == pID)
                {
                    cmbJenisPenerimaan.SelectedIndex = idx;
                    break;
                }

            }



        }
        public int GetID()
        {
            ListItemData li = (ListItemData)cmbJenisPenerimaan.SelectedItem;

            if (li != null)
            {
                if (cmbJenisPenerimaan.Text.Trim().Length > 0)
                {
                   
                        return li.Itemdata;
                }

            }
            return -1;



        }

        private void cmbJenisPenerimaan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
