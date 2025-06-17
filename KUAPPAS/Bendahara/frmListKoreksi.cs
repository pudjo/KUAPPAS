using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using BP.Bendahara;

using Formatting;
using DTO;
using DTO.Bendahara;


namespace KUAPPAS.Bendahara
{
    public partial class frmListKoreksi :  ChildForm
    {
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        List<Koreksi> mlstKoreksi; 

        public frmListKoreksi()
        {
            InitializeComponent();
        }

        private void frmListKoreksi_Load(object sender, EventArgs e)
        {
            gridkoreksi.FormatHeader();
            ctrlHeader1.SetCaption("Koreksi Belanja");
            ctrlPanelPencarian1.Create();

        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            try
            {

                KoreksiLogic oKoreksiLogic = new KoreksiLogic(GlobalVar.TahunAnggaran);
                mlstKoreksi  = new List<Koreksi>();

                int iddinas = ctrlPanelPencarian1.Dinas;
                gridkoreksi.Rows.Clear();
                mlstKoreksi = oKoreksiLogic.GetByDinas(iddinas, GlobalVar.TahunAnggaran);

                if (mlstKoreksi  != null)
                {
                    foreach (Koreksi k in mlstKoreksi)
                    {
                        string[] row = { k.NoUrut.ToString(), "Detail", k.NoBukti, k.DtKoreksi.ToString("dd MMM"),  k.Uraian,k.NourutSPJUP>0?"Sudah LOJ":"Belum LPJ" };

                        gridkoreksi.Rows.Add(row);

                    }
                    groupPencarian.Visible = true;
                    txtCari.Visible = true;
                    cmdCari.Visible = true;
                    cmdCariLagi.Visible = true;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlPanelPencarian1_OnAdd()
        {
            frmKoreksi fKoreksi = new frmKoreksi();
            fKoreksi.SetNew();
            fKoreksi.Show();

        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridkoreksi.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value == DBNull.Value || cell.Value == null)
                            continue;
                        if (cell.Value.ToString().ToUpper().Contains(txtCari.Text.Trim().ToUpper()) && cell.Visible == true)
                        {
                            containingCells.Add(cell);
                        }
                    }
                }
                if (containingCells.Count > 0)
                    gridkoreksi.CurrentCell = containingCells[currentContainingCellListIndex++];
                else
                    MessageBox.Show("Tidak diketemukan");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmdCariLagi_Click(object sender, EventArgs e)
        {
            if (containingCells.Count > 0 && currentContainingCellListIndex < containingCells.Count)
                gridkoreksi.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void gridkoreksi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                Koreksi k = mlstKoreksi[e.RowIndex];
                frmKoreksi fKoreksi = new frmKoreksi();
                fKoreksi.SetKoreksi(k);
                fKoreksi.Show();

            }
        }
        

    }
}
