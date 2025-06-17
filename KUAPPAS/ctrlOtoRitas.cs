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
using DTO;

namespace KUAPPAS
{
    public partial class ctrlOtoRitas : UserControl
    {
        public ctrlOtoRitas()
        {
            InitializeComponent();
        }

        public void Create()
        {
            cmbOtoritas.Items.Clear();

            List<cOtoritas> m_listOtoritas = new List<cOtoritas>();
            OtoritasLogic oLogic = new OtoritasLogic();

            m_listOtoritas = oLogic.GetListOtoritas(GlobalVar.Pengguna.Kelompok);
            
            foreach (cOtoritas o in m_listOtoritas)
            {
                ListItemData li = new ListItemData(o.Nama, o.ID);
                cmbOtoritas.Items.Add(li);
            }
        }
        public int Otoritas
        {
            get
            {
                if (cmbOtoritas.SelectedItem == null)
                    return 0;
                ListItemData li = (ListItemData)cmbOtoritas.SelectedItem;
                return (int)li.Itemdata;

            }
            set
            {
                for (int idx = 0; idx < cmbOtoritas.Items.Count; idx++)
                {
                    ListItemData li = (ListItemData)cmbOtoritas.Items[idx];
                    if ((int)li.Itemdata == value)
                    {
                        cmbOtoritas.SelectedIndex = idx;
                        break;
                    }
                }
            }

        }
        public string KeteranganStatus(int Status)
        {
            for (int idx = 0; idx < cmbOtoritas.Items.Count; idx++)
            {
                ListItemData li = (ListItemData)cmbOtoritas.Items[idx];
                if ((int)li.Itemdata == Status)
                {
                    return li.ItemText;

                }
            }
            return "";
        }

        private void cmbOtoritas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        
    }
}
