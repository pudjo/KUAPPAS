using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP.Bendahara;
using DTO.Bendahara;
using Formatting;
namespace KUAPPAS.Bendahara
{
    public partial class ctrlJenisGaji : UserControl
    {
        public ctrlJenisGaji()
        {
            InitializeComponent();
        }

        private void cmbJenisGaji_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public bool Create()
        {
            try
            {
                           
                cmbJenisGaji.Items.Clear();
                JenisGajiLogic oLogic = new JenisGajiLogic(GlobalVar.TahunAnggaran);
                List<JenisGaji> lst = new List<JenisGaji>();
                lst = oLogic.Get();
                if (lst != null)
                {

                    foreach (JenisGaji db in lst)
                        {
                            ListItemData itemc = new ListItemData(db.Kode + " " + db.Nama, db.Kode);
                            cmbJenisGaji.Items.Add(itemc);
                        }
                      
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public string GetKode()
        {
            ListItemData li = (ListItemData)cmbJenisGaji.SelectedItem;
            if (li != null)
            {
                return li.Kode;
            }
            return "";
        }
    }
}
