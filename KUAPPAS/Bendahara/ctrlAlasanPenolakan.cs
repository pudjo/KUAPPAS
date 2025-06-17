using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO.Bendahara;
using BP.Bendahara;

namespace KUAPPAS.Bendahara
{
    public partial class ctrlAlasanPenolakan : UserControl
    {

        public delegate void ValueChangedEventHandler(string alasan);
        public event ValueChangedEventHandler Changed;
        public event ValueChangedEventHandler DoubleClicking;
        public ctrlAlasanPenolakan()
        {
            InitializeComponent();
        }

        private void ctrlAlasanPenolakan_Load(object sender, EventArgs e)
        {

        }
        public bool Create()
        {
            try
            {
                cmbAlasan.Items.Clear();
                TemplateAlasanLogic oLogic = new TemplateAlasanLogic((int)GlobalVar.TahunAnggaran);
                List<TemplateAlasan> lst = new List<TemplateAlasan>();
                lst = oLogic.Get();
                if (lst != null)
                {
                    foreach (TemplateAlasan ta in lst)
                    {
                        cmbAlasan.Items.Add(ta.Alasan);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show ( ex.Message);
                return false;
            }

        }

        private void cmbAlasan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public string Text
        {
            get
            {
                return cmbAlasan.Text.Trim();
            }
        }
        public void Clear()
        {
            cmbAlasan.Items.Clear();
        }
        private void cmbAlasan_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
