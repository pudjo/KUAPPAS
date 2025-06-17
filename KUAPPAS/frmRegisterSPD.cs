using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using Formatting;
using DTO;


namespace KUAPPAS
{
    public partial class frmRegisterSPD : Form
    {
        public frmRegisterSPD()
        {
            InitializeComponent();
        }

        private void frmRegisterSPD_Load(object sender, EventArgs e)
        {
           // ctrlHeader1.SetCaption("Register SPD","Pilih Bulan dan klik Tombol Cetak");
            ctrlBulan1.Create();
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            ctrlHeader1.SetCaption("Register SPD", "");


        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            DateTime dAwal;
            DateTime dAkhir;
            int _iJenis = cmbJenis.SelectedIndex == 0 ? -1 : cmbJenis.SelectedIndex+2;

            int idDInas = ctrlSKPD1.GetID();

            dAwal = ctrlBulan1.GetFirstDay();
            dAkhir = ctrlBulan1.GetLastDay();
            //frmReportViewer fW = new frmReportViewer();

            //fW.RegisterSPD(dAwal, dAkhir, idDInas, _iJenis);
            //fW.Show();


        }

        private void cmdExcell_Click(object sender, EventArgs e)
        {
            DateTime dAwal;
            DateTime dAkhir;
            int _iJenis = cmbJenis.SelectedIndex == 0 ? -1 : cmbJenis.SelectedIndex + 2;

            int idDInas = ctrlSKPD1.GetID();

            dAwal = ctrlBulan1.GetFirstDay();
            dAkhir = ctrlBulan1.GetLastDay();
            ////frmReportViewer fW = new frmReportViewer();

            ////fW.RegisterSPD(dAwal, dAkhir, idDInas, _iJenis, true);


        }

        
    }
}
