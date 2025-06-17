using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Bendahara
{
    public partial class ctrlJenisBelanjaSPP : UserControl
    {
        private int m_ID;
        public ctrlJenisBelanjaSPP()
        {
            InitializeComponent();
        }

        private void ctrlJenisBelanjaSPPcs_Load(object sender, EventArgs e)
        {

        }
        public int ID
        {
            set {
                Create();
                SetID(value);
            }
            
        }
        public void SetID(int id)
        {
            m_ID = id;
            Create();
            switch (m_ID)
            {
                case 51:
                    cmbJenisKegiatan.SelectedIndex = 0;
                    break;
                case 52:
                    cmbJenisKegiatan.SelectedIndex = 1;
                    break;

                case 53:
                    cmbJenisKegiatan.SelectedIndex = 2;
                    break;

                case 54:
                    cmbJenisKegiatan.SelectedIndex = 3;
                    break;


            }
        }

        public int GetID()
        {

            m_ID = 0;
            switch (cmbJenisKegiatan.Text)
            {
                case "BELANJA OPERASI":
                    m_ID = 51;
                    break;

                case "BELANJA MODAL":
                  m_ID= 52;
                  break;
                case "BELANJA TAK TERDUGA":
                    m_ID= 53;
                    break;
                case "BELANJA TRANSFER":
                    m_ID = 54;
                    break;
                case "PEMBIAYAAN":
                    m_ID = 62;
                    break;
            }

            return m_ID;
        }

        public void Create()
        {
            ListItemData itemc = new ListItemData("BELANJA OPERASI", 51);
            ListItemData item1 = new ListItemData("BELANJA MODAL", 52);
            ListItemData item2 = new ListItemData("BELANJA TAK TERDUGA", 53);
            ListItemData item3 = new ListItemData("BELANJA TRANSFER", 54);
            ListItemData item4 = new ListItemData("PEMBIAYAAN", 62);

            cmbJenisKegiatan.Items.Clear();
            cmbJenisKegiatan.Items.Add(itemc);
            cmbJenisKegiatan.Items.Add(item1);
            cmbJenisKegiatan.Items.Add(item2);
            cmbJenisKegiatan.Items.Add(item3);
            cmbJenisKegiatan.Items.Add(item4);
        }
    }
}
