using DTO.Bendahara;
using BP.Bendahara;
using Formatting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Bendahara
{
    public partial class frmSaldoAwalBendahara : Form
    {
        public frmSaldoAwalBendahara()
        {
            InitializeComponent();
        }
        private bool GetSaldoAwal()
        {
            try
            {
                int iddinas= ctrlDinas1.GetID();
                SaldoAwal sa = new SaldoAwal();
                sa.IDDInas = ctrlDinas1.GetID();
                sa.IDRekening = 0;
                sa.Jenis = 1;
                sa.Jumlah = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
                sa.Bank = chkBank.Checked ? 1 : 0;

                SaldoAwalLogic oLogic = new SaldoAwalLogic(GlobalVar.TahunAnggaran);
                List<SaldoAwal> lst = oLogic.Get(GlobalVar.TahunAnggaran, iddinas);
                if (lst != null)
                {
                    sa = lst[0];
                    txtJumlah.Text = sa.Jumlah.ToRupiahInReport();
                    chkBank.Checked = sa.Bank == 1 ? true : false;

                }
                else
                {
                    MessageBox.Show(oLogic.LastError());
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
                SaldoAwal sa = new SaldoAwal();
                sa.Tahun = GlobalVar.TahunAnggaran;
                sa.IDDInas = ctrlDinas1.GetID();
                sa.IDRekening = 0;
                sa.Jenis = 1;
                sa.Jumlah=DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
                sa.Bank = chkBank.Checked ? 1 : 0;
                SaldoAwalLogic oLogic = new SaldoAwalLogic(GlobalVar.TahunAnggaran);
                if (oLogic.Simpan(sa)){
              
                    MessageBox.Show("Berhasil menyimpan saldo Awal");
                } else {
                    MessageBox.Show(oLogic.LastError());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
           
        }

        private void frmSaldoAwalBendahara_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Setting Saldoo Awal");

            ctrlDinas1.Create();
            GetSaldoAwal();
        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {
            
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            GetSaldoAwal();
        }

        private void ctrlDinas1_OnChanged_1(int pIDSKPD, int pIDUK)
        {
            GetSaldoAwal();
        }
    }
}
