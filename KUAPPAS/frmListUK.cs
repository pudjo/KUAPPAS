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
    public partial class frmListUK : ChildForm
    {
        List<Unit> mListUnit = new List<Unit>();
        public frmListUK()
        {
            InitializeComponent();
        }
        
        private void ctrlSKPD1_Load(object sender, EventArgs e)
        {

        }

        private void frmListUK_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Daftar Unit Kerja/Bidang", "Pilih SKPD.");
            this.WindowState = FormWindowState.Maximized;
            gridSKPD.FormatHeader();

            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);

        }

        private void ctrlSKPD1_OnChanged(int pID)
        {
            UnitKerjaLogic ukLogic = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
            mListUnit = new List<Unit>();
            mListUnit = ukLogic.GetBySKPD(pID);
            gridSKPD.Rows.Clear();
            foreach (Unit uk in mListUnit)
            {
                string[] row = { uk.ID.ToString(), "Detail", uk.Kode.IntToStringWithLeftPad(2), uk.Nama };
                gridSKPD.Rows.Add(row);

            }
            
            
        }

        private void cmdTambah_Click(object sender, EventArgs e)
        {
            frmUK fUK = new frmUK();
            fUK.UnitYangSudahAda = mListUnit;

            fUK.SetNew();
            fUK.Show();

         
        }

        private void gridUnitKerja_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ctrlSKPD1_Load_1(object sender, EventArgs e)
        {

        }

        private void gridSKPD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridSKPD.Rows.Count)
            {
                int _selectedID = DataFormat.GetInteger(gridSKPD.Rows[e.RowIndex].Cells[0].Value);

                frmUK fUK = new frmUK();
                fUK.UnitYangSudahAda = mListUnit;
                fUK.SetID(_selectedID);
                fUK.Show();
        
            }
        }
    }
}
