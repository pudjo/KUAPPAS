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
    public partial class ctrlJenisJurnalPenyesuaian : UserControl
    {
        private int m_SelectedID;
        public ctrlJenisJurnalPenyesuaian()
        {
            InitializeComponent();
            m_SelectedID=0;
        }

        private void cmbJenisJurnalPenyesuaian_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void Create()
        {
            cmbJenisJurnalPenyesuaian.Items.Clear();

            ListItemData li = new ListItemData("Umum", 0);
            cmbJenisJurnalPenyesuaian.Items.Add(li);
            li = new ListItemData("Persediaan", 1);

            cmbJenisJurnalPenyesuaian.Items.Add(li);
            li = new ListItemData("Penyusutan", 2);
            cmbJenisJurnalPenyesuaian.Items.Add(li);
            li = new ListItemData("Utang Belanja Pegawai", 3);
            cmbJenisJurnalPenyesuaian.Items.Add(li);
            li = new ListItemData("Penyisihan Piutang", 4);
            cmbJenisJurnalPenyesuaian.Items.Add(li);
            li = new ListItemData("Amortisasi",5);
            cmbJenisJurnalPenyesuaian.Items.Add(li);

        }
        public string  GetParent()
        {
           
                GetID();
                switch (m_SelectedID){
                    case 1:
                        return "11120";
                    case 2:
                        return "820";
                    case 3:
                        return "81";
                    case 4:
                        return "918";
                    case 5:
                        return "91704";
                   }
                return "";

                
                   
        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbSatuan.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbJenisJurnalPenyesuaian.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbJenisJurnalPenyesuaian.Items[i];
                if (li.ItemText == cmbJenisJurnalPenyesuaian.Text)
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
            for (i = 0; i < cmbJenisJurnalPenyesuaian.Items.Count; i++)
            {
                li = (ListItemData)cmbJenisJurnalPenyesuaian.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbJenisJurnalPenyesuaian.SelectedItem = li;
                    cmbJenisJurnalPenyesuaian.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
