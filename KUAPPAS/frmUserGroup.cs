using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using BP;
using Formatting;


namespace KUAPPAS
{
    public partial class frmUserGroup : Form
    {
        private UserGroup mUg;
        public frmUserGroup()
        {
            InitializeComponent();
            mUg = new UserGroup();
            cmbStatus.Items.Add(new ListItemData("Baru/Belum AKtif", 0));
            cmbStatus.Items.Add(new ListItemData("AKtif", 1));
            cmbStatus.Items.Add(new ListItemData("Sudah tidak AKtif",9));


        }

        private void frmUserGroup_Load(object sender, EventArgs e)
        {

        }
        public void  SetGroup(UserGroup ug){
            if (ug != null)
            {
                mUg = ug;
                txtNamaGroup.Text = mUg.Nama;
                int idx = 0;
                foreach (ListItemData li in cmbStatus.Items){
                    if (li.Itemdata == mUg.Status)
                    {
                        cmbStatus.SelectedIndex = idx;
                        break;
                    }
                    idx++;

                }
            }

        }
     }
}
