using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP.Bendahara;
using DTO.Bendahara;
using Formatting;
namespace KUAPPAS.Bendahara
{
    public partial class frmbataUP : ChildForm
    {
        public frmbataUP()
        {
            InitializeComponent();
        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {
            gridBataUP.FormatHeader();
            ctrlHeader1.SetCaption("Batas UP");
            LoadData();
        }
        private bool LoadData()
        {
            try
            {
                int i=0;
                BatasUPLogic oLogic = new BatasUPLogic(GlobalVar.TahunAnggaran);
                List<BatasUP> lst = new List<BatasUP>();
                lst = oLogic.Get(GlobalVar.TahunAnggaran);
                foreach (BatasUP bu in lst)
                {
                    string[] row = { (++i).ToString(), bu.IDDinas.ToString(), bu.NamaSKPD, bu.Jumlah.ToRupiahInReport(), "Ubah" };
                    gridBataUP.Rows.Add(row);

                }
                
                
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (DataGridViewRow row in gridBataUP.Rows)
                {
                    if (row.Cells[1].Value != null)
                    {
                        BatasUP bu = new BatasUP();
                        bu.Tahun = GlobalVar.TahunAnggaran;
                        bu.IDDinas = DataFormat.GetInteger(row.Cells[1].Value);
                        bu.Jumlah = DataFormat.FormatUangReportKeDecimal(row.Cells[3].Value);
                        bu.KodeUK = 0;

                        BatasUPLogic oLogic = new BatasUPLogic(GlobalVar.TahunAnggaran);
                        oLogic.Simpan(bu);



                    }
                }
                MessageBox.Show("Penyimpanan sudah selesai..");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridBataUP_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
             decimal jumlahUP = DataFormat.FormatUangReportKeDecimal(gridBataUP.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
             gridBataUP.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = jumlahUP.ToRupiahInReport();

        }
    }
}
