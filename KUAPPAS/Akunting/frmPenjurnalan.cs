using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KUAPPAS.Bendahara;


namespace KUAPPAS.Akunting
{
    public partial class frmPenjurnalan : Form
    {
        private int m_idDInas ;
        private DateTime tanggalAwal;
        private DateTime tanggalAkhir;
            

        public frmPenjurnalan()
        {
            InitializeComponent();
        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            m_idDInas = ctrlPanelPencarian1.Dinas;
            tanggalAwal = ctrlPanelPencarian1.TanggalAwal;
            tanggalAkhir = ctrlPanelPencarian1.TanggalAkhir;
            
            if (chkSKRSKPD.Checked)
            LoadPenerimaan();

            if (chkSetor.Checked)
            LoadPenyrtoran();
            if (chkSP2D.Checked)
            LoadSPP();
            if (chkBAST.Checked)
            LoadBAST();

            if (chkBPK.Checked)
                LoadPengeluaran();

        }

        private void LoadPenerimaan(){
            frmListPenerimaan fListPenrimaan = new frmListPenerimaan();
            fListPenrimaan = (frmListPenerimaan)Application.OpenForms["frmListPenerimaan"]; ;

            if (fListPenrimaan == null)
            {

                frmListPenerimaan fLstPenrimaan = new frmListPenerimaan(true);
                //  fRKA.SetModeAndTahap(0, 4);
                tabMDI.TabPages.Add(fLstPenrimaan);
                fLstPenrimaan.LoadData(m_idDInas, tanggalAwal,tanggalAkhir);

            }
            else
            {
                fListPenrimaan.LoadData(m_idDInas, tanggalAwal, tanggalAkhir);
                tabMDI.TabPages[fListPenrimaan].Select();
            }
        }

        private void LoadPenyrtoran()
        {
        
            // Penyetoran 
            frmListPenyetoranKasda fListSetor = new frmListPenyetoranKasda();
            fListSetor = (frmListPenyetoranKasda)Application.OpenForms["frmListPenyetoranKasda"]; ;

            if (fListSetor == null)
            {

                frmListPenyetoranKasda fLstPenrimaan = new frmListPenyetoranKasda(true);
                tabMDI.TabPages.Add(fLstPenrimaan);
                fLstPenrimaan.LoadData(m_idDInas, tanggalAwal, tanggalAkhir);

            }
            else
            {
                fListSetor.LoadData(m_idDInas, tanggalAwal, tanggalAkhir);
                tabMDI.TabPages[fListSetor].Select();
            }




        }
        private void LoadSPP()
        {
            frmListSPP fListSPP = new frmListSPP(true);
            fListSPP = (frmListSPP)Application.OpenForms["frmListSPP"]; ;

            if (fListSPP == null)
            {

                frmListSPP fLstSPP = new frmListSPP(true);
                tabMDI.TabPages.Add(fLstSPP);
                fLstSPP.LoadData(m_idDInas, tanggalAwal, tanggalAkhir);

            }
            else
            {
                fListSPP.LoadData(m_idDInas, tanggalAwal, tanggalAkhir);
                tabMDI.TabPages[fListSPP].Select();
            }
        }
        private void LoadBAST()
        {
            frmListBAST fListBAST = new frmListBAST(true);
            fListBAST = (frmListBAST)Application.OpenForms["frmListBAST"]; ;

            if (fListBAST == null)
            {

                fListBAST = new frmListBAST(true);
                tabMDI.TabPages.Add(fListBAST);
                fListBAST.LoadData(m_idDInas, tanggalAwal, tanggalAkhir);

            }
            else
            {
                fListBAST.LoadData(m_idDInas, tanggalAwal, tanggalAkhir);
                tabMDI.TabPages[fListBAST].Select();
            }
        }
        private void LoadPengeluaran()
        {
            frmListBPK fListBPK = new frmListBPK(true);
            fListBPK = (frmListBPK)Application.OpenForms["frmListBPK"]; ;

            if (fListBPK == null)
            {

                fListBPK = new frmListBPK(true);
                tabMDI.TabPages.Add(fListBPK);
                fListBPK.LoadData(m_idDInas, tanggalAwal, tanggalAkhir);

            }
            else
            {
                fListBPK.LoadData(m_idDInas, tanggalAwal, tanggalAkhir);
                tabMDI.TabPages[fListBPK].Select();
            }
        }
        private void frmPenjurnalan_Load(object sender, EventArgs e)
        {
            ctrlPanelPencarian1.Create();
            ctrlPanelPencarian1.SetVisible(3, false);
            ctrlPanelPencarian1.SetVisible(4, false);
            ctrlPanelPencarian1.SetCaption(2, "Proses Jurnal");

        }

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {

        }
    }
}
