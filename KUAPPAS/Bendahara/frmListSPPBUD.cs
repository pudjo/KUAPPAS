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
    public partial class frmListSPPBUD : ChildForm
    {
        private int m_iIDDInas;//m_iSKPD;

        private int m_iPPKD;
        // *******************
        private List<SPP> m_lstSPP;


        public frmListSPPBUD()
        {
            InitializeComponent();
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
            ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);
            p.IDDInas = m_iIDDInas;// ctrlSKPD1.GetID();
            p.Jenis = ctrlJenisSPP1.GetID();
            p.NoSP2D = txtNoSP2D.Text;
            p.NoSPM = txtNoSPM.Text;
            p.NoSPP = txtNoSPP.Text;
            // p.Status 
            m_lstSPP = new List<SPP>();
            m_lstSPP = oLogic.Get(p);
            gridSPP.Rows.Clear();
            int iRow = 0;

            foreach (SPP spp in m_lstSPP)
            {
                string[] row = { spp.NoUrut.ToString(), "Detail", "false", spp.NoSPP, spp.dtSPP.ToString("dd MMM"), spp.NoSPM, spp.dtSPM.ToString("dd MMM"), spp.NoSP2D, spp.dtCair.ToString("dd MMM"), spp.Keterangan, spp.Jumlah.ToRupiahInReport() };


                gridSPP.Rows.Add(row);
                if (spp.Status == 1)
                    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.Aqua;
                if (spp.Status == 2)
                    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.Pink;

                if (spp.Status == 3)
                    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.Purple;// Red;
                if (spp.Status == 4)
                    gridSPP.Rows[iRow].DefaultCellStyle.BackColor = Color.OrangeRed;// PaleVioletRed;// IndianRed;// Red;


                iRow++;


            }
        }

        private void cmdAddSPP_Click(object sender, EventArgs e)
        {
            frmSPP fSPP = new frmSPP();
            fSPP.ShowDialog();

        }

        private void gridSPP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < gridSPP.Rows.Count)
            {

                string sNoUrut = gridSPP.Rows[e.RowIndex].Cells[0].Value.ToString();
                long lNoUrut = DataFormat.GetLong(sNoUrut);
                SPP oSPP = new SPP();
                if (m_lstSPP != null)
                {
                    oSPP = m_lstSPP[e.RowIndex];
                }
                //        ctrlRekeningKegiatan1.CreateSPP(oSPP);
                if (e.ColumnIndex == 1)
                {
                    frmSPP fSPP = new frmSPP();
                    fSPP.SetModeForm(0);
                    fSPP.SetSPP(oSPP);
                    //fSPP.SetID(lNoUrut);
                    fSPP.Show();

                }

            }
        }

        private void frmListSPPBUD_Load(object sender, EventArgs e)
        {

        }
    }
}
