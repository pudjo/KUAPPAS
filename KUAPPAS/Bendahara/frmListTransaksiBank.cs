using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using BP.Bendahara;
using BP;

using Formatting;
using DTO.Bendahara;
using DTO;

namespace KUAPPAS.Bendahara
{
    public partial class frmListTransaksiBank : ChildForm
    {

        private List<TrxBank> m_lstTrxBank;

        public frmListTransaksiBank()
        {
            InitializeComponent();
            m_lstTrxBank = new List<TrxBank>();
        }

        private void frmListTransaksiBank_Load(object sender, EventArgs e)
        {
            gridTransaksiBank.FormatHeader();
            ctrlHeader1.SetCaption("Transaksi Bank");
            ctrlPanelPencarian1.MustLoad = true;

            ctrlPanelPencarian1.Create();

    
        }

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            TrxBankLogic oLogic = new TrxBankLogic(GlobalVar.TahunAnggaran);
            int iddinas = ctrlPanelPencarian1.Dinas;
            m_lstTrxBank = new List<TrxBank>();
            gridTransaksiBank.Rows.Clear();
            m_lstTrxBank = oLogic.GetByDinas(iddinas, GlobalVar.TahunAnggaran, 0);


            if (m_lstTrxBank != null)
            {
                foreach (TrxBank t in m_lstTrxBank)
                {
                    string[] row = { t.ID.ToString(), "Detail", t.jenis.ToString(), t.DTrx.FormatTanggal(), t.NoBukti, t.Uraian, t.Jumlah.ToRupiahInReport() };
                    gridTransaksiBank.Rows.Add(row);

                }
                
            }
        }

        private void gridTransaksiBank_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                long noUrut = DataFormat.GetLong(gridTransaksiBank.Rows[e.RowIndex].Cells[0].Value);
                frmTrxBank fTrx = new frmTrxBank();
                TrxBank oTrx = m_lstTrxBank.FirstOrDefault(x => x.ID == noUrut);
                if (oTrx != null)
                {
                    fTrx.Settrx ( oTrx);
                }
                fTrx.Show();

            }
        }

        private void ctrlPanelPencarian1_OnAdd()
        {
            frmTrxBank fTrx = new frmTrxBank();
            fTrx.Show();
        }
    }
}
