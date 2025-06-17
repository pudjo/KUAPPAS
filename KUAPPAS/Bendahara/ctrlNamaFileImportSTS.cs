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

namespace KUAPPAS.Bendahara
{
    public partial class ctrlNamaFileImportSTS : UserControl
    {
        public ctrlNamaFileImportSTS()
        {
            InitializeComponent();
        }

        private void cmbNamaFile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public string namaFile
        {
            get
            {
                return cmbNamaFile.Text.Trim();

            }
        }
        public void Create(int IDdinas, DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            STSLogic oLogic = new STSLogic(GlobalVar.TahunAnggaran);
            cmbNamaFile.Items.Clear();
            List<FileSTS> lstNamaFile = oLogic.GetNamaFileByDinas(IDdinas, tanggalAwal, tanggalAkhir);
            if (lstNamaFile != null)
            {
                foreach (FileSTS f in lstNamaFile)
                {
                    cmbNamaFile.Items.Add(f.NamaFIle);
                }

            }
            else
            {
                MessageBox.Show(oLogic.LastError());
            }

        }
    }
}
