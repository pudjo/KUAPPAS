using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP.Bendahara;
using BP;

//using DTO;
using Formatting;
using DTO.Bendahara;
using DTO;

namespace KUAPPAS.Bendahara
{
    public partial class frmListTerimaSTSKasda : ChildForm
    {
        private int m_iIDDInas;//m_iSKPD;

        private List<Setor> m_lstSetorPenerimaan;
        private List<SetorRekening> m_listSetorRekening;
        public frmListTerimaSTSKasda()
        {
            InitializeComponent();
            m_lstSetorPenerimaan = new List<Setor>();
            m_listSetorRekening = new List<SetorRekening>();
            gridSetorSTS.FormatHeader();
            
        }

        private void frmListTerimaSTSKasda_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Daftar Penerimaan Penyetoran di Kasda");
            ctrlPanelPencarian1.Create();
            m_iIDDInas = ctrlPanelPencarian1.Dinas;

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

        private void ctrlPanelPencarian1_OnAdd()
        {
            frmTerimaSetorKasda fSetor = new frmTerimaSetorKasda();
           // fSetor.SetNew();
            fSetor.Show();
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

         private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
         {

         }
    }
}
