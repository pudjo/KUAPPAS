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
using DTO;
using DTO.Bendahara;
using Formatting;


namespace KUAPPAS.Bendahara
{
    public partial class frmListBAST : ChildForm
    {
        private int m_iIDDInas;
        private List<BAST> m_lstBAST;
        private bool mbOnlyDisplay;
        private bool m_bForJurnal;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        List<Kontrak> m_lstKontrak;


        public frmListBAST(bool bOnlyDisplay = false)
        {
            InitializeComponent();
            mbOnlyDisplay = bOnlyDisplay;
            if (bOnlyDisplay == true)
            {
                gridBAST.Top = 0;
                gridBAST.Left = 0;
                ctrlPanelPencarian1.Visible = false;
                
            }
            m_bForJurnal = false;

        }
        public bool ForJurnal
        {
            set { m_bForJurnal = value;
            if (m_bForJurnal)
            {
                ctrlPanelPencarian1.Visible = false;

            }
            }
        }
        private void frmListBAST_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Daftar Berita Acara Pembayaran");
            this.WindowState = FormWindowState.Maximized;
            if (mbOnlyDisplay == false)
                ctrlPanelPencarian1.Create();
            gridBAST.FormatHeader();
            if (GlobalVar.Pengguna.SKPD > 0)
            {

                m_iIDDInas = GlobalVar.Pengguna.SKPD;
            }
  
        }
        public bool SetAddOrUpdateBAST(BAST oBAST)
        {

            return true;

        }
        private void cmdtampilkan_Click(object sender, EventArgs e)
        {
            
        }
        public bool  LoadData (int iddinas, DateTime tanggalAwal, DateTime tanggalAkhir ){
            try
            {
                

                //m_lstBAST = GlobalVar.gListBAST.FindAll(bast => bast.IDDInas == m_iIDDInas &&
                //                                 bast.dtBAST >= tanggalAwal && bast.dtBAST <= tanggalAkhir);


                


                if (m_lstBAST != null)
                {
                    foreach (BAST b in m_lstBAST)
                    {
                        string[] row = { b.NoUrut.ToString(), "Detail", "false", b.NoBAST, b.dtBAST.ToString("dd MMM"), b.Uraian, b.NOKontrak, b.NamaPihakKetiga, "" };
                        gridBAST.Rows.Add(row);

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        
        }

        private void gridBAST_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridBAST.Rows.Count)
            {
                if (e.ColumnIndex == 1)
                {
                    BAST oBAST = new BAST();
                    oBAST = m_lstBAST[e.RowIndex];
                    frmBAST fBAST = new frmBAST();
                    fBAST.SetBAST(oBAST);
                    fBAST.Show();



                }
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {

        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            m_iIDDInas = ctrlPanelPencarian1.Dinas;
            DateTime tanggalAwal = ctrlPanelPencarian1.TanggalAwal;
            DateTime tanggalAkhir = ctrlPanelPencarian1.TanggalAkhir;
            BASTLogic oLogic = new BASTLogic((int)GlobalVar.TahunAnggaran);
            gridBAST.Rows.Clear();
            m_lstBAST = new List<BAST>();
           
                GlobalVar.gListBAST = new List<BAST>();
                m_lstBAST = oLogic.GetByIDDInasDanBatas(m_iIDDInas,tanggalAwal,tanggalAkhir);
                //foreach (BAST b in m_lstBAST){
                //    GlobalVar.gListBAST.Add(b);
                //}
                               
           

            //if (GlobalVar.gListBAST.FindAll(bast => bast.IDDInas == m_iIDDInas &&
            //                                 bast.dtBAST >= tanggalAwal && bast.dtBAST <= tanggalAkhir).Count == 0)
            //{
            //    m_lstBAST = oLogic.GetByIDDInas(m_iIDDInas);
            //    foreach (BAST b in m_lstBAST)
            //    {

            //        GlobalVar.gListBAST.Add(b);
            //    }

            //}


                if (LoadData(m_iIDDInas, tanggalAwal, tanggalAkhir) == true)
                {
                    lblPencarian.Visible = true;
                    txtCari.Visible = true;
                    cmdCari.Visible = true;
                    cmdCariLagi.Visible = true;

                    m_lstKontrak = new List<Kontrak>();
                    if (GlobalVar.gListKontrak == null)
                    {
                        //    KontrakLogic  oKontrakLogic = new KontrakLogic(GlobalVar.TahunAnggaran);
                        //    GlobalVar.gListKontrak = new List<Kontrak>();

                        //    m_lstKontrak  = oKontrakLogic.GetByIDDinas(m_iIDDInas);

                        //    foreach (Kontrak kon in m_lstKontrak)
                        //    {
                        //        GlobalVar.gListKontrak.Add(kon);
                        //    }

                        //}
                        //if (GlobalVar.gListKontrak.FindAll(k => k.IDDInas == m_iIDDInas).Count==0)
                        //{
                        //    KontrakLogic oKontrakLogic = new KontrakLogic(GlobalVar.TahunAnggaran);

                        //    m_lstKontrak = oKontrakLogic.GetByIDDinas(m_iIDDInas);
                        //    foreach (Kontrak kon in m_lstKontrak)
                        //    {
                        //        GlobalVar.gListKontrak.Add(kon);
                        //    }
                        //}
                        //else
                        //{
                        //    m_lstKontrak = GlobalVar.gListKontrak.FindAll(k => k.IDDInas == m_iIDDInas);
                        //}

                    }

                }

        }

        private void cmdCari_Click(object sender, EventArgs e)
        {try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridBAST.Rows)
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
                    gridBAST.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridBAST.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        

        }

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {

        }
        private void ctrlPanelPencarian_Load(object sender, EventArgs e)
        {

        }

        private void gridBAST_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {

                    DataGridViewRow row = gridBAST.Rows[e.RowIndex];
                    if (row.Cells[0].Value != null)
                    {
                        long NoUrut = DataFormat.GetLong(row.Cells[0].Value );
                        BAST oBAST = new BAST();
                        

                        oBAST = m_lstBAST.FirstOrDefault(b => b.NoUrut == NoUrut);
                        Kontrak k = new Kontrak();
                        KontrakLogic oLogicc = new KontrakLogic(GlobalVar.TahunAnggaran);
                        k = oLogicc.Get(oBAST.NoUrutKontrak);
                        if (k != null)
                        {
                            oBAST.oKontrak = k;
                            frmBAST fBAST = new frmBAST();
                            fBAST.SetBAST(oBAST);

                            fBAST.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void ctrlPanelPencarian1_OnAdd()
        {
            frmBAST fBast = new frmBAST();
            fBast.OnNew();
            fBast.Show();
        }
    }
}
