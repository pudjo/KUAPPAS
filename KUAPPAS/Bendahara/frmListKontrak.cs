using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Formatting;


using BP;
using BP.Bendahara;
using DTO;
using DTO.Bendahara;


namespace KUAPPAS.Bendahara
{
   public partial class frmListKontrak : ChildForm

    {
        private int m_iIDDInas;
        List<Kontrak> m_ListKontrak = new List<Kontrak>();
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
            
        public frmListKontrak()
        {
            InitializeComponent();
        }

        private void frmListKontrak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            ctrlDinas1.Create();
            m_iIDDInas = ctrlDinas1.GetID();
            ctrlHeader1.SetCaption("Daftar Kontrak");
            gridKontrak.FormatHeader();

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            frmKontrak fKontrak = new frmKontrak();
            fKontrak.SetNew();
            fKontrak.Show();

        }

        private void cmdtampilkan_Click(object sender, EventArgs e)
        {
            try
            {

                KontrakLogic oLogic = new KontrakLogic((int)GlobalVar.TahunAnggaran);
                DateTime tanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                DateTime tanggalAkhir = new DateTime(GlobalVar.TahunAnggaran, 12, 31);


                gridKontrak.Rows.Clear();

                
                   

             
                    m_ListKontrak = oLogic.GetByIDDinas(m_iIDDInas);

                   

             
                if (LoadData() == true)
                {
                    lblPencarian.Visible = true;
                    txtCari.Visible = true;
                    cmdCari.Visible = true;
                    cmdCariLagi.Visible = true;
                }
            } catch(Exception ex){
                MessageBox.Show(ex.Message);
            }



        }  
       private bool LoadData(){

        try
            {
                lblPencarian.Visible = false;
                txtCari.Visible = false;
                cmdCari.Visible = false;
                cmdCariLagi.Visible = false;
                if (m_ListKontrak != null)
                {
                    foreach (Kontrak k in m_ListKontrak)
                    {
                        string[] row = { k.NoUrut.ToString(), "Ubah", k.NoKontrak, k.DtKontrak.ToString("dd MMM "), k.Uraian, k.NamaPerusahaan , k.Jumlah.ToRupiahInReport()};
                        gridKontrak.Rows.Add(row);

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

           
           
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_iIDDInas = pIDSKPD;

        }

        private void gridKontrak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                Kontrak oKontrak = new Kontrak();
                oKontrak = m_ListKontrak[e.RowIndex];
                frmKontrak fKontrak = new frmKontrak();
                fKontrak.SetKontrak(oKontrak);
                fKontrak.Show();


            }
        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridKontrak.Rows)
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
                    gridKontrak.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridKontrak.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }
    }
}
