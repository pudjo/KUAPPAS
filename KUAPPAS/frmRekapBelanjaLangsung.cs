using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using DTO;
using Formatting;

namespace KUAPPAS
{
    public partial class frmRekapBelanjaLangsung : Form
    {
        public frmRekapBelanjaLangsung()
        {
            InitializeComponent();
        }

        private void frmRekapBelanjaLangsung_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Cek Rekap Belanja Langsung(per Program/Kegiatan");

            gridRekap.FormatHeader();

            ctrlDinas1.Create();
            LoadData();
        }
        private void LoadData()
        {
            int _pDinas = ctrlDinas1.GetID();
            rptRKALogic oLogic = new rptRKALogic(GlobalVar.TahunAnggaran);
            gridRekap.Rows.Clear();
            List<RKA22Murni> _lst = oLogic.GetRekapRKA22Murni(GlobalVar.TahunAnggaran, _pDinas,4);
            
            foreach (RKA22Murni r in _lst)
            {
                string[] row = { r.IDUrusan.ToString(), r.IDProgram.ToString(), r.IDkegiatan.ToString(), r.IDUrusan.ToString(), r.KodeProgram, r.KodeKegiatan, r.Nama, r.Jumlah };
                gridRekap.Rows.Add(row);
            }


            if (oLogic.IsError())
            {
                MessageBox.Show(oLogic.LastError());
                return;
            }

            
            
        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            ParameterLaporan p = new ParameterLaporan();
            p.KodeUrusan = ctrlDinas1.KodeUrusanPemerintahan();
            p.KodeOrganisasi = ctrlDinas1.KodeOrganisasi();
            p.NamaUrusan = ctrlDinas1.NamaUrusanPemerintahan();
            p.NamaDinas = ctrlDinas1.GetNamaSKPD();
            p.Tanggal = dtCetak.Value.ToString("DD MMM yyyy");
            p.dTanggal = dtCetak.Value.Date;

            if (ctrlDinas1.GetID() == 0)
            {
                MessageBox.Show("Dinas Belum dipilih");
                return;
            }

            //frmReportViewer f = new frmReportViewer();

            //f.CetakRKA22Murni(p, GlobalVar.TahunAnggaran, ctrlDinas1.GetID());
            //f.Show();
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            LoadData();
        }
    }
}
