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
    public partial class frmListSKRSKPD : ChildForm
    {

        private int m_iIDDInas;//m_iSKPD;

        private int m_iPPKD;
    

        private decimal m_sJumlahPenerimaan;
        private decimal m_sJumlahPengeluaran;

        private List<Pengeluaran> m_lstPengeluaran;

        DataGridViewCellStyle _merah = new DataGridViewCellStyle();
        DataGridViewCellStyle _pink = new DataGridViewCellStyle();
        DataGridViewCellStyle _ijo = new DataGridViewCellStyle();

        public frmListSKRSKPD()
        {
            InitializeComponent();
        }

        private void frmListSKRSKPD_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Surat Ketetapan Pajak/Retribusi Daerah");

            gridSKRSKPD.FormatHeader();
            ctrlPanelPencarian1.Create();

        }

        private void cmdTampilkanSKRSKPD_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData(){
            //SKRSKPDLogic oLogic = new SKRSKPDLogic((int)GlobalVar.TahunAnggaran);
            //List<SKRSKPD> lst = new List<SKRSKPD>();
            //gridSKRSKPD.Rows.Clear();
            //lst = oLogic.GetByDinas(m_iIDDInas);//, E_JENIS_SETOR.E_SETOR_PAJAK);
            //if (lst != null)
            //{
            //    foreach (SKRSKPD s in lst)
            //    {
            //        string[] row = { s.NoUrut.ToString(), "Detail", "false", s.NoBukti, s.TanggalSKRSKPD.ToString("dd MMM"), s.Keterangan, s.Nama + s.Alamat, s.Jumlah.ToRupiahInReport() };
            //        gridSKRSKPD.Rows.Add(row);

            //    }
            //}
        }

        private void ctrlSKPD1_Load(object sender, EventArgs e)
        {
          

        }

        private void ctrlSKPD1_OnChanged(int pID)
        {
            m_iIDDInas = pID;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            LoadData();
        }
    }
}
