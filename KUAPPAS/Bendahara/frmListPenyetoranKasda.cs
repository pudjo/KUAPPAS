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
    public partial class frmListPenyetoranKasda : ChildForm
    {
        private int m_iIDDInas;//m_iSKPD;

        private List<Setor> m_lstSetorPenerimaan;
        private List<SetorRekening> m_listSetorRekening;

   List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        public frmListPenyetoranKasda(bool bOnlyDisplay= false)
        {
            InitializeComponent();
            m_lstSetorPenerimaan = new List<Setor>();
            m_listSetorRekening = new List<SetorRekening>();
            gridSetorSTS.FormatHeader();
            if (bOnlyDisplay == true)
            {
                gridSetorSTS.Top = 0;
                gridSetorSTS.Left = 0;
                ctrlPanelPencarian1.Visible = false;

            }


        }
        public void LoadData(int idDinas, DateTime tanggalAwal, DateTime tanggalAkhir){

            SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
            m_lstSetorPenerimaan = new List<Setor>();
            gridSetorSTS.Rows.Clear();
            m_iIDDInas=idDinas;
            m_lstSetorPenerimaan = oLogic.GetByDinas(m_iIDDInas, E_JENIS_SETOR.E_SETOR_STS, tanggalAwal, tanggalAkhir);
            decimal Jumlah = 0L;
            if (m_lstSetorPenerimaan != null)
            {
                foreach (Setor s in m_lstSetorPenerimaan)
                {
                    string[] row = { s.NoUrut.ToString(), "Detail", "BKU Kan", s.dtBukuKas.ToString("dd MMM"), s.NoBukti, s.Keterangan, s.Jumlah.ToRupiahInReport() };
                    gridSetorSTS.Rows.Add(row);
                    Jumlah = Jumlah + s.Jumlah;

                }
                txtJumlah.Text = Jumlah.ToRupiahInReport();

            }

            else
            {
                if (oLogic.IsError() == true)
                {
                    MessageBox.Show(oLogic.LastError());
                }
            }
            LoadDataDetail();
        }

        public void LoadDataDetail()
        {
            DateTime tanggalAwal = ctrlPanelPencarian1.TanggalAwal;
            DateTime tanggalAkhir = ctrlPanelPencarian1.TanggalAkhir;
            m_iIDDInas = ctrlPanelPencarian1.Dinas;
            SetorLogic oLogic = new SetorLogic((int)GlobalVar.TahunAnggaran);
            m_listSetorRekening = oLogic.GetDetailByDinas(m_iIDDInas, E_JENIS_SETOR.E_SETOR_STS, tanggalAwal, tanggalAkhir);

        }
        private void frmListPenyetoranKasda_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Daftar Penyetoran ke Kasda");
            ctrlPanelPencarian1.Create();
            m_iIDDInas = ctrlPanelPencarian1.Dinas;

        }

        private void ctrlSKPD1_OnChanged(int pID)
        {
            m_iIDDInas = pID;

        }

        private void gridSetorSTS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridSetorSTS.Rows.Count)
            {

                if (e.ColumnIndex == 1)
                {
                    Setor st = new Setor();
                    st = m_lstSetorPenerimaan[e.RowIndex];
                    if (m_listSetorRekening == null)
                    {
                        m_listSetorRekening = new List<SetorRekening>();

                    }
                    if (m_listSetorRekening.Count == 0)
                    {
                        LoadDataDetail();
                    }

                    st.Details = m_listSetorRekening.FindAll(x => x.NoUrut == st.NoUrut);

                    frmSetorPenerimaan fSetor = new frmSetorPenerimaan();

                    fSetor.SetSetor(st);
                    fSetor.ShowDialog();
                }
                if (e.ColumnIndex == 2)
                {
                    Setor st = new Setor();
                    st = m_lstSetorPenerimaan[e.RowIndex];
                    SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                    if (m_listSetorRekening == null)
                    {
                        m_listSetorRekening = new List<SetorRekening>();

                    }
                    if (m_listSetorRekening.Count == 0)
                    {
                        LoadDataDetail();
                    }

                    st.Details = m_listSetorRekening.FindAll(x => x.NoUrut == st.NoUrut);


                    if (oLogic.CatatBKU(st, 1) == true)
                    {
                        MessageBox.Show("Proses BKU selesai");

                    }
                    else
                    {
                        MessageBox.Show("Terjadi kesalahan proses BKU");
                    }


                }

            }

        }

        private void cmTampilkanPenyetoran_Click(object sender, EventArgs e)
        {
           // DateTimd dAwal = ctrl
        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            try
            {
                DateTime tanggalAwal = ctrlPanelPencarian1.TanggalAwal;
                DateTime tanggalAkhir = ctrlPanelPencarian1.TanggalAkhir;
                m_iIDDInas = ctrlPanelPencarian1.Dinas;
                LoadData(m_iIDDInas, tanggalAwal, tanggalAkhir);
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
            frmSetorPenerimaan fSetor = new frmSetorPenerimaan();
            fSetor.SetNew();
            fSetor.Show();
        }

        private void cmdPilihSemua_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridSetorSTS.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    row.Cells[2].Value = true;
                }
            }
        }

        private void cmdBKU_Click(object sender, EventArgs e)
        {
            try
            {
                for (int row = 0; row < m_lstSetorPenerimaan.Count; row++)
                {
                    Setor st = new Setor();
                    st = m_lstSetorPenerimaan[row];
                    SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                    oLogic.CatatBKU(st, 1);

                }
                MessageBox.Show("Proses BKU selesai");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan Prosess BKU " + ex.Message);

            }
        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridSetorSTS.Rows)
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
                    gridSetorSTS.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridSetorSTS.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }
        
    }
}
