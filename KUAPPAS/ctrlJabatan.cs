using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS
{
    public partial class ctrlJabatan : UserControl
    {
        private int m_SelectedID;
        public ctrlJabatan()
        {
            InitializeComponent();
            m_SelectedID = 0;

        }

        private void cmbJabatan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void Create()
        {
            ListItemData item = new ListItemData("Kepala Daerah", 1);
            cmbJabatan.Items.Add(item);
            item = new ListItemData("Sekretaris Daerah", 2);
            cmbJabatan.Items.Add(item);
            item = new ListItemData("Kepala PPKD", 3);
            cmbJabatan.Items.Add(item);
            item = new ListItemData("Kepala BUD", 4);
            cmbJabatan.Items.Add(item);
            item = new ListItemData("Bendahara PPKD", 5);
            cmbJabatan.Items.Add(item);
            item = new ListItemData("Kepala Dinas/Badan", 6);
            cmbJabatan.Items.Add(item);
            item = new ListItemData("PPK", 7);
            cmbJabatan.Items.Add(item);
            item = new ListItemData("Bendahara Pengeluaran", 8);
            cmbJabatan.Items.Add(item);
            item = new ListItemData("Bendahara Penerimaan", 9);
            cmbJabatan.Items.Add(item);
            item = new ListItemData("Bendahara Pengeluaran Pembantu", 10);
            cmbJabatan.Items.Add(item);
            item = new ListItemData("Kuasa Pengguna Anggaran Penerimaan ", 11);
            cmbJabatan.Items.Add(item);

        }
        public void CreatePenandaTangan()
        {
           



            ListItemData   item = new ListItemData("Pengguna Anggaran", 6);
            cmbJabatan.Items.Add(item);
            item = new ListItemData("Kuasa Pengguna Anggaran", 7);
            cmbJabatan.Items.Add(item);

        }
        public int ID
        {
            set{

                int i;
                ListItemData li = new ListItemData("", 0);
                for (i = 0; i < cmbJabatan.Items.Count; i++)
                {
                    li = (ListItemData)cmbJabatan.Items[i];
                    if (li.Itemdata == Convert.ToInt32(value))
                    {
                        cmbJabatan.SelectedIndex = i;
                        cmbJabatan.SelectedItem = li;
                        break;
                    }
                }
              }
            get
            {
                m_SelectedID = 0;
                for (int i = 0; i < cmbJabatan.Items.Count; i++)
                {
                    ListItemData li = (ListItemData)cmbJabatan.Items[i];
                    if (li.ItemText == cmbJabatan.Text)
                    {
                        m_SelectedID = li.Itemdata;

                        break;
                    }

                }

                return m_SelectedID;
            }
        }
    }
}
