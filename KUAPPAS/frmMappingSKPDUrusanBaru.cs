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
    public partial class frmMappingSKPDUrusanBaru : Form
    {
        public frmMappingSKPDUrusanBaru()
        {
            InitializeComponent();
        }

        private void frmMappingSKPDUrusanBaru_Load(object sender, EventArgs e)
        {

        }
        private void CreateCOmboUrusanBaru()
        {
            UrusanBaruLogic oLogic = new UrusanBaruLogic(GlobalVar.TahunAnggaran);
            List<UrusanBaru> _lst = new List<UrusanBaru>();
            _lst = oLogic.Get();
            DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
            if (_lst != null)
            {
                foreach(UrusanBaru ub in _lst){
                    cmb.Items.Add(new ListItemData(ub.Tampilan  + " " + ub.Nama, ub.ID));                
                }
                
            }
            

        }
    }
}
