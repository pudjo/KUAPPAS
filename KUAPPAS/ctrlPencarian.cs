using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS
{
    public partial class ctrlPencarian : UserControl
    {
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        DataGridViewCell cell;
        int currentContainingCellListIndex;
        DataGridView m_Grid;
        public ctrlPencarian()
        {
            InitializeComponent();
        }

        private void ctrlPencarian_Load(object sender, EventArgs e)
        {

        }
        public void setGrid(ref DataGridView grid)
        {
            m_Grid = grid;
            currentContainingCellListIndex = 0;
            containingCells = new List<DataGridViewCell>();
        }
        public DataGridViewCell Cell
        {
            get { return cell; }
        }
        private void cmdCari_Click(object sender, EventArgs e)
        {
            try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in m_Grid.Rows)
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
                    m_Grid.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                m_Grid.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }
      
    }
}
