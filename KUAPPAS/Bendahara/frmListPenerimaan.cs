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
    public partial class frmListPenerimaan : ChildForm
    {
        private int m_iIDDInas;//m_iSKPD;

  


     
        private List<STS> m_lstSTS;
        DataGridViewCellStyle _merah = new DataGridViewCellStyle();
        DataGridViewCellStyle _pink = new DataGridViewCellStyle();
        DataGridViewCellStyle _ijo = new DataGridViewCellStyle();
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;

        public frmListPenerimaan(bool bOnlyDisplay= false )
        {
            InitializeComponent();
            m_lstSTS = new List<STS>();
            if (bOnlyDisplay == true)
            {
                gridSTS.Top = 0;
                gridSTS.Left = 0;
                ctrlPanelPencarian1.Visible = false;
                
            }
            DateTime dAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
            DateTime dAkhir = new DateTime(GlobalVar.TahunAnggaran, 12, 31);

            ctrlNamaFileImportSTS1.Create(m_iIDDInas, dAwal, dAkhir);
          }

        private void frmListPenerimaan_Load(object sender, EventArgs e)
        {

            ctrlHeader1.SetCaption("Daftar Penerimaan ");
            ctrlPanelPencarian1.MustLoad = true;
            ctrlPanelPencarian1.Create();
            gridSTS.FormatHeader();
            ctrlJenisPenerimaan1.Create();
            DateTime dAwal = new DateTime (GlobalVar.TahunAnggaran,1,1);
            DateTime dAkhir = new DateTime(GlobalVar.TahunAnggaran, 12, 31);

            ctrlNamaFileImportSTS1.Create(m_iIDDInas, dAwal, dAkhir);
   
            
        }

        private void ctrlSKPD1_OnChanged(int pID)
        {
            m_iIDDInas = pID;

        }

        private void cmdTampilkanSTS_Click(object sender, EventArgs e)
        {

           

        }

        public void LoadData(int IDDInas,DateTime tanggalAwal, DateTime tanggalAkhir)
        {

            STSLogic oLogic = new STSLogic((int)GlobalVar.TahunAnggaran);
            gridSTS.Rows.Clear();
            txtJumlah.Text = "0";

            m_iIDDInas = IDDInas;
            int Jenis = ctrlJenisPenerimaan1.GetID();
            m_lstSTS = oLogic.GetByDinas(m_iIDDInas, tanggalAwal, tanggalAkhir,Jenis);

            decimal Jumlah = 0l;
            if (m_lstSTS != null)
            {
                foreach (STS s in m_lstSTS)
                {
                    if (ctrlNamaFileImportSTS1.namaFile != "")
                    {
                        if (s.NamaFile == ctrlNamaFileImportSTS1.namaFile)
                        {
                            string[] row = { s.NoUrut.ToString(), "Detail", "BKU kan", "false", s.TanggalSTS.ToString("dd MMM"), s.NoSTS, s.Keterangan, s.Penyetor, s.Jumlah.ToRupiahInReport() };
                            gridSTS.Rows.Add(row);
                            Jumlah = Jumlah + s.Jumlah;
                        }
                    }
                    else
                    {
                        if (s.TanggalSTS >= tanggalAwal)
                        {
                            string[] row = { s.NoUrut.ToString(), "Detail", "BKU kan", "false", s.TanggalSTS.ToString("dd MMM"), s.NoSTS, s.Keterangan, s.Penyetor, s.Jumlah.ToRupiahInReport() };
                            gridSTS.Rows.Add(row);
                            Jumlah = Jumlah + s.Jumlah;
                        }
                    }
                }
                txtJumlah.Text = Jumlah.ToRupiahInReport();
            }
        }
        private void gridSTS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && (e.RowIndex >= 0 && e.RowIndex < gridSTS.Rows.Count))
            {

                     STSLogic oLogic = new STSLogic((int)GlobalVar.TahunAnggaran);
                     long nourut = DataFormat.GetLong(gridSTS.Rows[e.RowIndex].Cells[0].Value);
                     if (nourut == 0)
                     {
                         return;
                     }
                     STS oSTS = new STS();
                     oSTS = oLogic.GetByID(nourut);
                     if (oSTS == null)
                     {
                         return;

                     }
                        
                
                
               // oSTS = m_lstSTS[e.RowIndex];
                if (oSTS != null)
                {
                    frmPenerimaan f = new frmPenerimaan();
                    f.SetSTS(oSTS);
                    f.Show();
                }
            }
            if (e.ColumnIndex == 2 && (e.RowIndex >= 0 && e.RowIndex < gridSTS.Rows.Count))
            {

                STSLogic oLogic = new STSLogic((int)GlobalVar.TahunAnggaran);
                long nourut = DataFormat.GetLong(gridSTS.Rows[e.RowIndex].Cells[0].Value);
                if (nourut == 0)
                {
                    return;
                }
                STS oSTS = new STS();
                oSTS = oLogic.GetByID(nourut);
                if (oSTS == null)
                {
                    return; 
                }
                
                STSLogic oSTSLogic = new STSLogic(GlobalVar.TahunAnggaran);
                List<STS> lst = new List<STS>();
                lst.Add(oSTS);

                if (
                  oSTSLogic.BKUKanSTS(oSTS, m_iIDDInas) == false)
                {
                    MessageBox.Show(oSTSLogic.LastError());
                    txtCari.Text = oSTSLogic.LastError();

                }

            }
        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            m_iIDDInas= ctrlPanelPencarian1.Dinas;
            DateTime tanggalAwal = ctrlPanelPencarian1.TanggalAwal;
            DateTime tanggalAkhir = ctrlPanelPencarian1.TanggalAkhir;
            LoadData(m_iIDDInas, tanggalAwal, tanggalAkhir);

        }

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlPanelPencarian1_OnAdd()
        {
            frmPenerimaan fTST = new frmPenerimaan();
            fTST.SetNew();
            fTST.Show();
        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridSTS.Rows)
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
                    gridSTS.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridSTS.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void cmdBKU_Click(object sender, EventArgs e)
        {
            List<long> lstNoUrutTdkDIproses = new List<long>();
            try
            {

                STSLogic oSTSLogic = new STSLogic(GlobalVar.TahunAnggaran);
                foreach(DataGridViewRow row in gridSTS.Rows){
                    STS oSTS = new STS();
                    long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                    oSTS = m_lstSTS.FirstOrDefault(x => x.NoUrut == NoUrut);
                    if (oSTS != null)
                    {
                        oSTSLogic.BKUKanSTS(oSTS, m_iIDDInas);
                        if (oSTSLogic.IsError() == true)
                        {
                            Console.WriteLine(oSTSLogic.LastError());
                        }
                    }
                    else
                    {
                        lstNoUrutTdkDIproses.Add(NoUrut); 
                    }
                }
                MessageBox.Show("Proses BKU Selesai");
                foreach (long l in lstNoUrutTdkDIproses)
                {
                    Console.WriteLine(l.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdPilihSemua_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridSTS.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    row.Cells[1].Value = true;
                }
            }
        }

        private void ctrlPanelPencarian1_TanggalBerubah()
        {

            m_iIDDInas = ctrlPanelPencarian1.Dinas;
            ctrlNamaFileImportSTS1.Create(m_iIDDInas, ctrlPanelPencarian1.TanggalAwal, ctrlPanelPencarian1.TanggalAkhir);
        }

        private void ctrlPanelPencarian1_DinasBerubah()
        {
            m_iIDDInas = ctrlPanelPencarian1.Dinas;

            DateTime dAwal = ctrlPanelPencarian1.TanggalAwal;
            DateTime dAkhir = ctrlPanelPencarian1.TanggalAkhir;

            ctrlNamaFileImportSTS1.Create(m_iIDDInas, dAwal, dAkhir);

        }
    }
}
