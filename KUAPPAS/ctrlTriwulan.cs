using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS
{
    public partial class ctrlTriwulan : UserControl
    {
        public DateTime TanggalAwal;
        public DateTime TanggalAkhir;

        public ctrlTriwulan()
        {
            InitializeComponent();
            cmbTriwulan.Items.Add("TRIWULAN I");
            cmbTriwulan.Items.Add("TRIWULAN II");
            cmbTriwulan.Items.Add("TRIWULAN III");
            cmbTriwulan.Items.Add("TRIWULAN IV");
        }

        private void cmbTriwulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cmbTriwulan.SelectedIndex){
                case 0:
                    TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                    TanggalAkhir= new DateTime(GlobalVar.TahunAnggaran, 3, 31);
                    break;
                case 1:
                    TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 4, 1);
                    TanggalAkhir= new DateTime(GlobalVar.TahunAnggaran, 6, 30);
                    break;
                case 2:
                    TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 6, 1);
                    TanggalAkhir= new DateTime(GlobalVar.TahunAnggaran, 9, 30);
                    break;
                case 3:
                    TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 10, 1);
                    TanggalAkhir= new DateTime(GlobalVar.TahunAnggaran, 12, 31);
                    break;

            }
 
            
        }
    
    }
}
