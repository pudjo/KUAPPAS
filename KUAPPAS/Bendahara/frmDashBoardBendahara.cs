using BP.Bendahara;
using DTO.Bendahara;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formatting;
namespace KUAPPAS.Bendahara
{
    public partial class frmDashBoardBendahara : ChildForm
    {
        private int m_iIDDInas;
        List<SPP>  m_iListSPP;
        public frmDashBoardBendahara()
        {
            InitializeComponent();
        }

        private void frmDashBoardBendahara_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            gridSP2D.FormatHeader();
        }
        private void LoadDataSP2DTidakdiBKU() {
        
            try
            {
                m_iIDDInas = ctrlDinas1.GetID();
                
               SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                m_iListSPP = new List<SPP>();
                m_iListSPP= oLogic.GetSPPBelumBKU(m_iIDDInas);
                if (oLogic.IsError()== true){
                    MessageBox.Show(oLogic.LastError());
                    return;

                }
                int no =0;
                gridSP2D.Rows.Clear();

                foreach(SPP spp in m_iListSPP){
                    string [] row = {spp.NoUrut.ToString(),(++no).ToString(), spp.NoSP2D,spp.dtCair.ToTanggalIndonesia(true),spp.Jumlah.ToRupiahInReport() };
                    gridSP2D.Rows.Add(row);

                
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        
        }

        private void cmdPanggil_Click(object sender, EventArgs e)
        {
            LoadDataSP2DTidakdiBKU();
        }
     }
}
