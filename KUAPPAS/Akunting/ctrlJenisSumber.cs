using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Akunting
{
    public partial class ctrlJenisSumber : UserControl
    {
        private int m_SelectedID;
        public ctrlJenisSumber()
        {
            InitializeComponent();
        }
        public string NamaSumber
        {
            get
            {
                return cmbJenisSumber.Text;
            }
        }
        private void ctrlJenisSumber_Load(object sender, EventArgs e)
        {

        }
        public void Create()
        {
        
                //public enum JENIS_SUMBERJURNAL{
        //E_SUMBER_DPA = 0,
        //E_SUMBER_SKR = 1,
        //E_SUMBER_STS = 2,
        //E_SUMBER_SETOR = 3,
        //S_SUMBER_BAST = 4,
        //E_SUMBER_SP2D = 5,
        //E_SUMBER_PANJAR = 6,
        //E_SUMBER_MANUAL = 7,
        //E_JURNAL_PENYESUAIAN = 9,
        //E_JURNAL_PENYUSUTAN = 10,
        //E_SUMBER_TRXASET = 11,
        //E_SUMBER_INVESTASI = 12,
        //E_SUMBER_UTANG = 13,
        //E_SUMBER_KOREKSI = 14,
        //E_SUMBER_PENUTUP = 20
            ListItemData li = new ListItemData("Semua Sumber", 0);
                   cmbJenisSumber.Items.Add(li);
            li = new  ListItemData( "SKR/SKP",1);
            cmbJenisSumber.Items.Add(li);
                li = new  ListItemData("STS",2);
                cmbJenisSumber.Items.Add(li);
                li = new ListItemData("Penyetoran Penerimaan dan Pengembalian Belanja", 3);
                cmbJenisSumber.Items.Add(li);
                li = new  ListItemData("BAST",4);
                cmbJenisSumber.Items.Add(li);
                li = new  ListItemData("SP2D",5);
                cmbJenisSumber.Items.Add(li);
                li = new  ListItemData("SPJ/BPK",6);
                cmbJenisSumber.Items.Add(li);
                //li = new  ListItemData("Pengembalian",6);
                //cmbJenisSumber.Items.Add(li);
               li = new ListItemData("Koreksi", 14);
               cmbJenisSumber.Items.Add(li);
               li = new ListItemData("Pajak", 19);
               cmbJenisSumber.Items.Add(li);
            li = new ListItemData("Penyesuaian", 8);
            cmbJenisSumber.Items.Add(li);
            li = new ListItemData("Jurnal Umum", 3);
            cmbJenisSumber.Items.Add(li);

            li = new ListItemData("Penutup", 20);
               cmbJenisSumber.Items.Add(li);
        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbKontrak.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbJenisSumber.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbJenisSumber.Items[i];
                if (li.ItemText == cmbJenisSumber.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }

            return m_SelectedID;
        }
    }
}
