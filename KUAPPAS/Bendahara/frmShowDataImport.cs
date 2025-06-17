using BP.Bendahara;
using DTO.Bendahara;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formatting;

namespace KUAPPAS.Bendahara
{
    public partial class frmShowDataImport : Form
    {
        public int dinas;
        public string sheet;

        public frmShowDataImport()
        {
            InitializeComponent();
        }

        private void frmShowDataImport_Load(object sender, EventArgs e)
        {

        }
        public int Dinas
        {
            set
            {
                dinas = value;
            }
        }
        public string Sheet
        {
            set
            {
                sheet = value;
                NamaSheet.Text = sheet;
            }
        }
        public int LoadData()
        {
            STSLogic oLogic = new STSLogic(GlobalVar.TahunAnggaran);
            gridSTS.Rows.Clear();
            try{
                List<STS> lst = new List<STS>();
                decimal Jumlah = 0;
                lst = oLogic.GetDataImport(dinas, sheet);
                {
                    if (lst != null)
                    {
                        foreach (STS sts in lst)
                        {
                            string[] row = {sts.NoUrut.ToString(), sts.TanggalSTS.ToTanggalIndonesia(),
                                               sts.NoSTS, 
                                               sts.Keterangan, 
                                               sts.Jumlah.ToRupiahInReport() };
                            gridSTS.Rows.Add(row);
                            Jumlah = Jumlah + sts.Jumlah;
                        }
                        txtJumlah.Text = Jumlah.ToRupiahInReport();

                    }
                 }
                return lst.Count;
            }catch(Exception ex){
                MessageBox.Show(ex.Message);
                return 0;

            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdBersihkan_Click(object sender, EventArgs e)
        {
            BKULogic oBKULogic = new BKULogic(GlobalVar.TahunAnggaran);

            if (oBKULogic.BersihkanBKUPendapatanImport(dinas, sheet))
            {
                STSLogic oSTSLogic = new STSLogic(GlobalVar.TahunAnggaran);
                if (oSTSLogic.BersihkanBKUPendapatanImport(dinas, sheet))
                {
                    MessageBox.Show("Data Sudah di bersihkan ");

                }
            }
               
        }
        

    }
}
