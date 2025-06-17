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

namespace KUAPPAS
{
    public partial class ctrlStatusPengguna : UserControl
    {
        public ctrlStatusPengguna()
        {
            InitializeComponent();
        }

        private void ctrlStatusPengguna_Load(object sender, EventArgs e)
        {

        }
      

        public void Create()
        {
            cmbStatus.Items.Clear();

            List<ListItemData> m_lstStatus = new List<ListItemData>();
            StatusPenggunaLogic oLogic = new StatusPenggunaLogic();
            m_lstStatus = oLogic.GetListStatus();
            foreach (ListItemData li in m_lstStatus)
            {
                cmbStatus.Items.Add(li);
            }
        }
        public int Status
        {
            get
            {
                ListItemData li = (ListItemData)cmbStatus.SelectedItem;
                return (int)li.lItemData;

            }
            set
            {
                for (int idx = 0; idx < cmbStatus.Items.Count; idx++)
                {
                    ListItemData li = (ListItemData)cmbStatus.Items[idx];
                    if ((int)li.Itemdata == value)
                    {
                        cmbStatus.SelectedIndex = idx;
                        break;
                    }
                }
            }

        }
        public string KeteranganStatus(int Status)
        {
            for (int idx = 0; idx < cmbStatus.Items.Count; idx++)
            {
                ListItemData li = (ListItemData)cmbStatus.Items[idx];
                if ((int)li.Itemdata == Status)
                {
                    return li.ItemText;

                }
            }
            return "";
        }
      

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
