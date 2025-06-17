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
    public partial class frmListPenyetoranPajak : ChildForm
    {
       List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        private int m_iIDDInas;
        private int m_KodeUK;
        private List<Setor> m_lstSetor;
        private List<SetorRekening> m_lstSetorDetail;
        public frmListPenyetoranPajak()
        {
            InitializeComponent();
            m_iIDDInas = 0;
        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            try
            {
                SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
                m_lstSetor = new List<Setor>();
                m_lstSetorDetail = new List<SetorRekening>();
                gridSetorPajak.Rows.Clear();
                m_iIDDInas = ctrlPanelPencarian1.Dinas;
                DateTime tanggalAwal = ctrlPanelPencarian1.TanggalAwal;
                DateTime tanggalAkhir = ctrlPanelPencarian1.TanggalAkhir;
                m_lstSetor = oLogic.GetByDinas(m_iIDDInas, E_JENIS_SETOR.E_SETOR_PAJAK, tanggalAwal, tanggalAkhir);
                if (m_lstSetor != null)
                {
                    foreach (Setor s in m_lstSetor)
                    {
                        string[] row = { s.NoUrut.ToString(), s.NoUrutClient.ToString(), "Detail", "BKU", s.dtBukuKas.ToString("dd MMM"), s.NoBukti, s.Keterangan, s.NoNTPN, s.KodeBilling, s.Jumlah.ToRupiahInReport() };
                        gridSetorPajak.Rows.Add(row);

                    }
                }
                m_lstSetorDetail = oLogic.GetDetailByDinas(m_iIDDInas, E_JENIS_SETOR.E_SETOR_PAJAK, tanggalAwal, tanggalAkhir);

                groupPencarian.Visible = true;
                txtCari.Visible = true;
                cmdCari.Visible = true;
                cmdCariLagi.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void frmListPenyetoranPajak_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Daftar Penyetoran Pajak");
            ctrlPanelPencarian1.Create();
            gridSetorPajak.FormatHeader();


        }

        private void ctrlPanelPencarian1_OnAdd()
        {
            frmPajak fPajak = new frmPajak();
            fPajak.SetNew();

            fPajak.ShowDialog();

        }

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {
            
        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridSetorPajak.Rows)
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
                    gridSetorPajak.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridSetorPajak.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void gridSetorPajak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {

                long NoUrut = DataFormat.GetLong(gridSetorPajak.Rows[e.RowIndex].Cells[0].Value);
                Setor oSetor = new Setor();
                oSetor = m_lstSetor.First(x => x.NoUrut == NoUrut);
                if (oSetor.Jenis == 4)
                {

                    frmPajak fPajak = new frmPajak();
                    fPajak.SetPenyetoranPajak(oSetor);

                    fPajak.Show();
                }

            }
            if (e.ColumnIndex == 3)
            {

                long NoUrut = DataFormat.GetLong(gridSetorPajak.Rows[e.RowIndex].Cells[0].Value);
                Setor oSetor = new Setor();
                oSetor = m_lstSetor.First(x => x.NoUrut == NoUrut);
                if (oSetor.Jenis == 4)
                {
                    SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                    oSetor.Details=oLogic.GetDetail(oSetor.NoUrut);
                    if (oLogic.CatatBKU(oSetor, 2) == true)
                    {
                        MessageBox.Show("Pajak sudah di BKU kan");

                    }
                    else
                    {
                        MessageBox.Show("Kesalahan dalam pencatatan BKU Pajak" + oLogic.LastError());
                    }
                    
                }

            }

        }

        private void cmdLanjut_Click(object sender, EventArgs e)
        {
            frmPajakDanSetorannya fPajakSetor = new frmPajakDanSetorannya();
            fPajakSetor.Show();

        }

        private void cmdBKU_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gridSetorPajak.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                        Setor oSetor = new Setor();
                        oSetor = m_lstSetor.First(x => x.NoUrut == NoUrut);
                        oSetor.Details = m_lstSetorDetail.FindAll(x => x.NoUrut == NoUrut);
                        if (oSetor != null)
                        {
                            if (oSetor.Jenis == 4)
                            {
                                SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                                if (oLogic.CatatBKU(oSetor, 2) == true)
                                {

                                }
                                else
                                {
                                    MessageBox.Show("Kesalahan dalam pencatatan BKU Pajak No " + oSetor.NoBukti);
                                }

                            }
                        }
                    }
                }
                MessageBox.Show("Pajak sudah di BKU kan ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
    }
}
