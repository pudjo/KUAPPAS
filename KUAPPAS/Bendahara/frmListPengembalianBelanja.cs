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

//using DTO;
using Formatting;
using DTO.Bendahara;
using DTO;

namespace KUAPPAS.Bendahara
{
    public partial class frmListPengembalianBelanja : ChildForm

    {
        List<Setor> m_lstSetor;
        List<SetorRekening> m_listSetorDetail;
        private int m_iIDDInas;

        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        public frmListPengembalianBelanja()
        {
            InitializeComponent();
            m_iIDDInas = 0;
            m_lstSetor = new List<Setor>();
            m_listSetorDetail = new List<SetorRekening>();
        }

        private void frmListPengembalianBelanja_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Pengembalian Belanja");
            ctrlPanelPencarian1.Create();

            gridPengembalian.FormatHeader();
        }

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {
           

        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            
            SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
            
            m_lstSetor = new List<Setor>();
            gridPengembalian.Rows.Clear();
            m_iIDDInas = ctrlPanelPencarian1.Dinas;
            decimal jumlah = 0L;

            DateTime tanggalAwal = ctrlPanelPencarian1.TanggalAwal;
            DateTime tanggalAkhir = ctrlPanelPencarian1.TanggalAkhir;
            m_lstSetor = oLogic.GetByDinas(m_iIDDInas, E_JENIS_SETOR.E_ALL , tanggalAwal, tanggalAkhir);
            m_listSetorDetail = oLogic.GetDetailByDinas(m_iIDDInas, E_JENIS_SETOR.E_ALL , tanggalAwal, tanggalAkhir);
            
            if (m_lstSetor != null)
            {
                foreach (Setor s in m_lstSetor)
                {
                    
                        string[] row = { s.NoUrut.ToString(), 
                                           "Detail", 
                                           "", 
                                           s.NoBukti, 
                                           s.dtBukuKas.ToString("dd MMM"), 
                                           s.Keterangan, 
                                           s.JenisSP2D.ToString(), 
                                           s.NobuktiClient, 
                                           s.Jumlah.ToRupiahInReport() };
                        gridPengembalian.Rows.Add(row);
                        jumlah = jumlah + s.Jumlah;

                }
                groupPencarian.Visible = true;
                
                txtCari.Visible = true;
                cmdCari.Visible = true;
                cmdCariLagi.Visible = true;
            }
            txtJumlah.Text = jumlah.ToRupiahInReport();

        }

        private void ctrlPanelPencarian1_OnAdd()
        {
            frmPengeluaranPengembalian fPengembalian = new frmPengeluaranPengembalian();
            fPengembalian.SetNew();
            fPengembalian.Show();

        }

        private void gridPengembalian_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    
                    long NoUrut = DataFormat.GetLong(gridPengembalian.Rows[e.RowIndex].Cells[0].Value);
                    Setor oSetor = new Setor();
                    oSetor = m_lstSetor.First(x => x.NoUrut == NoUrut);
                    if (oSetor.Jenis > 2)
                    {
                        if (m_listSetorDetail == null)
                        {
                            SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                            m_listSetorDetail = oLogic.GetDetailByDinas(m_iIDDInas, E_JENIS_SETOR.E_ALL, ctrlPanelPencarian1.TanggalAwal, ctrlPanelPencarian1.TanggalAkhir);
                        }
                        List<SetorRekening> lstDetail = m_listSetorDetail.FindAll(x => x.NoUrut == NoUrut);
                        oSetor.Details = lstDetail;

                    }
                    frmPengeluaranPengembalian fPengembalian = new frmPengeluaranPengembalian();
                    fPengembalian.SetSetor(oSetor);
                    fPengembalian.Show();
                }
                if (e.ColumnIndex == 2)
                {
                    SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                    for (int row = 0; row < m_lstSetor.Count; row++)
                    {

                        long NoUrut = DataFormat.GetLong(gridPengembalian.Rows[e.RowIndex].Cells[0].Value);
                        Setor oSetor = new Setor();
                        oSetor = m_lstSetor.First(x => x.NoUrut == NoUrut);
                        Setor st = new Setor();
                        st = m_lstSetor[row];
              
                        oLogic.CatatBKU(st, 2);

                    }
                    MessageBox.Show("Proses BKU selesai");

                   
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan menampilkan data Pengembalian.  " + ex.Message);
            }
        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridPengembalian.Rows)
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
                    gridPengembalian.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridPengembalian.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }
        


    }
}
