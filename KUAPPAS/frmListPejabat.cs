using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BP;


namespace KUAPPAS
{
    public partial class frmListPejabat : ChildForm
    {
        private int m_IDDInas;
        private int m_KodeUK;
        private List<Pejabat> mlstPejabat;

        public frmListPejabat()
        {
            InitializeComponent();
            m_IDDInas = 0;
            m_KodeUK = 0;

        }

        private void cmdTampilkan_Click(object sender, EventArgs e)
        {
            try
            {
                int jenis = ctrlJabatan1.ID;


                PejabatLogic oLogic = new PejabatLogic(GlobalVar.TahunAnggaran);
                mlstPejabat = new List<Pejabat>();
                gridPejabat.Rows.Clear();
                m_IDDInas = ctrlDinas1.GetID();
                m_KodeUK = ctrlDinas1.GetKodeUK();

                mlstPejabat = oLogic.GetByJenisAndDinas(jenis, m_IDDInas, m_KodeUK);

                if (mlstPejabat != null)
                {
                    foreach (Pejabat p in mlstPejabat)
                    {
                        string[] row = { p.ID.ToString(), "Detail", p.Nama, p.NIP, p.NPWP,p.Jabatan,p.TanggalAktiv.ToString("dd MMM yyyy") };
                        gridPejabat.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void frmListPejabat_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            gridPejabat.FormatHeader();
            ctrlJabatan1.Create();
            ctrlHeader1.SetCaption("Daftar Pejabat..");


        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_IDDInas = pIDSKPD;
            m_KodeUK = pIDUK;
        }

        private void gridPejabat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                Pejabat p = mlstPejabat[e.RowIndex];
                frmPejabatDetail fpFetail = new frmPejabatDetail();
                fpFetail.SetPejabat(p);
                fpFetail.Show();

            }
        }

        private void cmdTambah_Click(object sender, EventArgs e)
        {
            frmPejabatDetail fpFetail = new frmPejabatDetail();
            fpFetail.SetNew();
            fpFetail.Show();
        }
    }
}
