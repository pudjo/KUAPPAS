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
    public partial class frmListPengeluaran : ChildForm
    {

        private int m_iIDDInas;//m_iSKPD;

        //private int m_iPPKD;
        //private int m_iKodeUK;
        //private DateTime tanggalAwal;
        //private DateTime tanggalAkhir;

        //private decimal m_sJumlahPenerimaan;
        //private decimal m_sJumlahPengeluaran;

        private List<Pengeluaran> m_lstPengeluaran;
        private bool mbOnlyDisplay;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;

        public frmListPengeluaran(bool bOnlyDisplay = false)
        {
            InitializeComponent();
             mbOnlyDisplay = bOnlyDisplay;
             if (bOnlyDisplay == true)
             {
                 gridBPK.Top = 0;
                 gridBPK.Left = 0;
               //  ctrlPanelPencarian1.Visible = false;
             }
        }

        private void frmListBPK_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ctrlDinas1.Create();
            ctrlHeader1.SetCaption("Daftar Belanja/Bukti Pengeluaran Kas/SPJ/Panjar dan Pertanggungjawabannya..");

            //if (mbOnlyDisplay == false)
            ////    ctrlPanelPencarian1.Create();
            ////ctrlPanelPencarian1.SetVisible(4, false);

            ListItemData li = new ListItemData("Semua", -1);
            cmbJenis.Items.Add(li);
            
            li = new ListItemData("Panjar", (int)E_JENISPENGELUARAN.PENGELUARAN_PANJAR);
            cmbJenis.Items.Add(li);
             li = new ListItemData("Pengembalian Panjar", (int)E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR);
            cmbJenis.Items.Add(li);

            li = new ListItemData("SPJ/BPK", (int)E_JENISPENGELUARAN.PENGELUARAN_LANGSUNG);
            cmbJenis.Items.Add(li);
            li = new ListItemData("Pertanggung Jawaban Panjar", (int)E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR);
            cmbJenis.Items.Add(li);
            li = new ListItemData("ADD", (int)E_JENISPENGELUARAN.PENGELUARAN_ADD);
            cmbJenis.Items.Add(li);
            li = new ListItemData("BLUD", (int)E_JENISPENGELUARAN.PENGELUARAN_BLUD);
            cmbJenis.Items.Add(li);
            li = new ListItemData("BOS", (int)E_JENISPENGELUARAN.PENGELUARAN_BOS);
            cmbJenis.Items.Add(li);

            ctrlTanggalBulanVertikal1.Create();
            gridBPK.FormatHeader();

        }

        private void cmdPanggilBPK_Click(object sender, EventArgs e)
        {
        }


        public void LoadData(int iddinas, DateTime tanggalAwal, DateTime tanggalAkhr){

            if (GlobalVar.gListPengeluaran != null)
            {
                m_lstPengeluaran = GlobalVar.gListPengeluaran.FindAll(x => x.IDDInas == m_iIDDInas && x.Tanggal >= tanggalAwal && x.Tanggal <= tanggalAkhr);
                decimal cJumlah = 0l;
                gridBPK.Rows.Clear();
                if (m_lstPengeluaran.Count > 0)
                {
                    foreach (Pengeluaran p in m_lstPengeluaran)
                    {
                        string[] row = { p.NoUrut.ToString(), "Detail", "false", p.NoBukti, p.Tanggal.ToString("dd MMM"), p.Penerima, p.Uraian, p.Jumlah.ToRupiahInReport() };
                        cJumlah = cJumlah + p.Jumlah;
                        gridBPK.Rows.Add(row);

                    }
                }
                txtJumlah.Text = cJumlah.ToRupiahInReport();
            }
        }

        private void gridBPK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridBPK.Rows.Count)
            {
                if (e.ColumnIndex == 1)
                {
                    //long noUrut = DataFormat.GetLong(gridBPK.Rows[e.RowIndex].Cells[0].Value);
                    DataGridViewRow row= gridBPK.Rows[e.RowIndex];

                    long noUrut = DataFormat.GetLong(row.Cells[0].Value);

                    Pengeluaran p = m_lstPengeluaran.FirstOrDefault(x=>x.NoUrut== noUrut);
                    if (p != null)
                    {
                        frmPengeluaran f = new frmPengeluaran();
                        f.Jenis = p.Jenis;
                        f.SetPengeluaran(p);
                        f.ShowDialog();
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    DataGridViewRow row = gridBPK.Rows[e.RowIndex];

                    long noUrut = DataFormat.GetLong(row.Cells[0].Value);

                    Pengeluaran p = m_lstPengeluaran.FirstOrDefault(x => x.NoUrut == noUrut);
                    PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
                    if (oLogic.CatatBKU(ref p) == true)
                    {
                        MessageBox.Show("Selesai Catatan BKU");
                    }
                    else
                    {
                        MessageBox.Show("**GAGA**L Catatan BKU"+ oLogic.LastError());
                    }
                    
                }

            }
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_iIDDInas = pIDSKPD;
        }

        
            private void LoadData(){
            try
            {
                m_iIDDInas = ctrlDinas1.GetID();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
                PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);

                if (GlobalVar.gListPengeluaran == null)
                {
                    GlobalVar.gListPengeluaran = new List<Pengeluaran>();
                }
                //if (GlobalVar.gListPengeluaran.FindAll(x => x.IDDInas == m_iIDDInas).Count == 0)
                //{
                   m_lstPengeluaran = new List<Pengeluaran>();
                    ParameterBendahara pb = new ParameterBendahara(GlobalVar.TahunAnggaran);
                    pb.IDDInas = m_iIDDInas;// ctrlSKPD1.GetID();
                    pb.TanggalAwal = tanggalAwal;
                    pb.TanggalAkhir = tanggalAkhir;
                    int Jenis=-1;
                    if (chkBLUD.Checked == true) { 
                        //Jenis = 1;
                    }
                    if (chkTU.Checked == true)
                    {
                        //Jenis = 2;
                    }
                    if (chkBLUD.Checked == true)
                    {
                        //Jenis = 3;
                    }

                    if (cmbJenis.Text.Length > 0)
                    {
                        ListItemData li = (ListItemData)cmbJenis.SelectedItem;
                        if (li.ItemText == cmbJenis.Text)
                        {
                            Jenis = li.Itemdata;
                        }
                    }
                    pb.Jenis = Jenis;
                    m_lstPengeluaran = oLogic.Get(pb);
                    if (oLogic.IsError() == true)
                    {
                        MessageBox.Show(oLogic.LastError());
                        return;
                    } 
                    foreach (Pengeluaran p in m_lstPengeluaran)
                    {
                        GlobalVar.gListPengeluaran.Add(p);

                    }
                

                
                decimal cJumlah = 0l;
                gridBPK.Rows.Clear();
                if (m_lstPengeluaran.Count > 0)
                {
                    foreach (Pengeluaran p in m_lstPengeluaran)
                    {
                        string[] row = { p.NoUrut.ToString(), "Detail", "BKU kan", p.NoBukti, p.Tanggal.ToString("dd MMM"), p.Uraian, p.Penerima, p.Jumlah.ToRupiahInReport() };
                        cJumlah = cJumlah + p.Jumlah;
                        gridBPK.Rows.Add(row);

                    }
                }
                txtJumlah.Text = cJumlah.ToRupiahInReport();

                txtCari.Visible = true;
                cmdCari.Visible = true;
                cmdCariLagi.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

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
                foreach (DataGridViewRow row in gridBPK.Rows)
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
                    gridBPK.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridBPK.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void ctrlPanelPencarian1_OnAdd()
        {
            

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            mnuSPJ.Show(ptLowerLeft);
        }

        private void mnuBPK_Click(object sender, EventArgs e)
        {
            try
            {
                frmPengeluaran fPengeluaran = new frmPengeluaran();
                fPengeluaran.Jenis = E_JENISPENGELUARAN.PENGELUARAN_LANGSUNG;
                fPengeluaran.SetNew();
                fPengeluaran.Show();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;

                LoadData(m_iIDDInas, tanggalAwal, tanggalAkhir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuSPJPanjar_Click(object sender, EventArgs e)
        {
            frmPengeluaran fPengeluaran = new frmPengeluaran();
            fPengeluaran.Jenis = E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR;
            fPengeluaran.SetNew();
            fPengeluaran.Show();
            DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
            DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;

     

            LoadData(m_iIDDInas, tanggalAwal, tanggalAkhir);
        }

        private void Panjar_Click(object sender, EventArgs e)
        {
            frmPengeluaran fPengeluaran = new frmPengeluaran();
            fPengeluaran.Jenis = E_JENISPENGELUARAN.PENGELUARAN_PANJAR;
            fPengeluaran.SetNew();
            fPengeluaran.Show();
            DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
            DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;



            
        }

        private void cmdLoadData_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmdBKU_Click(object sender, EventArgs e)
        {
            bool berhasilcatatBKU = true;
            PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
            foreach (DataGridViewRow row in gridBPK.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    long noUrut = DataFormat.GetLong(row.Cells[0].Value);
                    Pengeluaran p = m_lstPengeluaran.FirstOrDefault(x => x.NoUrut == noUrut);
                    if (p != null)
                    {
                        if (oLogic.CatatBKU(ref p) == false)
                        {
                            berhasilcatatBKU = berhasilcatatBKU && false;

                        }
                    }
                }

            }
            if (berhasilcatatBKU == true)
            {
                MessageBox.Show("Pencatatan BKU selesai.");
            }
            else
            {
                MessageBox.Show("Ada kesalahan Pencatatan BKU ");
            }
        }

        private void mnuPengembalianPanjar_Click(object sender, EventArgs e)
        {
            frmPengeluaran fPengeluaran = new frmPengeluaran();
            fPengeluaran.Jenis = E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR;
            fPengeluaran.SetNew();
            fPengeluaran.Show();
            DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
            DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;



        }

        private void mnuADD_Click(object sender, EventArgs e)
        {
            try
            {
                frmPengeluaran fPengeluaran = new frmPengeluaran();
                fPengeluaran.Jenis = E_JENISPENGELUARAN.PENGELUARAN_ADD;
                fPengeluaran.SetNew();
                fPengeluaran.Show();
                DateTime tanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;

                LoadData(m_iIDDInas, tanggalAwal, tanggalAkhir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
