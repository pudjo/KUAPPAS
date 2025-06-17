using BP.Akuntansi;
using DTO.Akuntansi;
using Formatting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Akunting
{
    public partial class frmListJurnal : ChildForm
    {
        public JENIS_JURNAL m_iJenisJurnal;
        public JENIS_SUMBERJURNAL m_iSumberJurnal;
        public int m_idDInas;
        List<Jurnal> m_lst = new List<Jurnal>();
        public frmListJurnal()
        {
            InitializeComponent();
            m_idDInas = 0;
        }

        private void frmListJurnal_Load(object sender, EventArgs e)
        {
            ctrlPanelPencarian1.Create();
            //ctrlHeader1.SetCaption("Daftar Jurnal Manual");
            //this.Text = "Daftar Jurnal Manual";
            gridListJurnal.FormatHeader();
            ctrlPencarian1.setGrid(ref gridListJurnal);
            if (m_iJenisJurnal == JENIS_JURNAL.JENIS_JURNALUMUM)
            {
                ctrlHeader1.SetCaption("Daftar Jurnal Umun");
                this.Text = "Daftar Jurnal Umun";
            }
            else
            {
                ctrlHeader1.SetCaption("Daftar Jurnal Penyesuaian");
                this.Text = "Daftar Jurnal Penyesuaian";
            }
        }
        public JENIS_JURNAL Jenis{
            set {
                m_iJenisJurnal= value;
                if (m_iJenisJurnal == JENIS_JURNAL.JENIS_JURNALUMUM)
                {
                    ctrlHeader1.SetCaption("Daftar Jurnal Umun");
                    this.Text = "Daftar Jurnal Umun";
                }
                else
                {
                    ctrlHeader1.SetCaption("Daftar Jurnal Penyesuaian");
                    this.Text = "Daftar Jurnal Penyesuaian";
                }

            }
        }
        public JENIS_SUMBERJURNAL Sumber
        {
            set { m_iSumberJurnal = value; }
        }

        private void ctrlPanelPencarian1_Load(object sender, EventArgs e)
        {
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlPanelPencarian1.Create();
                m_idDInas = GlobalVar.Pengguna.SKPD;

            }

        }
        private void LoadData()
        {

            try
            {

                gridListJurnal.Rows.Clear();
                m_idDInas = ctrlPanelPencarian1.Dinas;
                JurnalLogic oLogic = new JurnalLogic(GlobalVar.TahunAnggaran);
                m_lst = new List<Jurnal>();
                m_lst = oLogic.GetByJenis(m_idDInas, (int)m_iJenisJurnal, ctrlPanelPencarian1.TanggalAwal, ctrlPanelPencarian1.TanggalAkhir);
                if (m_lst != null)
                {
                    foreach (Jurnal jr in m_lst)
                    {
                        string[] row = { jr.NoJurnal.ToString(),"Detail", jr.NoBukti, jr.TanggalBukti.ToTanggalIndonesia() ,jr.Uraian};
                        gridListJurnal.Rows.Add(row);

                    }
                }
                return ;
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
                
                return ;
            }
        
        }

        private void ctrlPanelPencarian1_OnAdd()
        {
            frmJurnal f = new frmJurnal();
            f.Jenis = m_iJenisJurnal;
            f.Sumber = m_iSumberJurnal;
            f.ShowDialog();
        }

        private void gridListJurnal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridListJurnal.Rows.Count)
            {
                if (e.ColumnIndex > 1)
                {
                    long NoJurnal = DataFormat.GetLong(gridListJurnal.Rows[e.RowIndex].Cells[0].Value);


                    List<JurnalRekeningShow> lst = new List<JurnalRekeningShow>();
                    JurnalLogic oLogic = new JurnalLogic(GlobalVar.TahunAnggaran);
                    lst = oLogic.GetByNoJurnal(NoJurnal);
                    if (lst != null)
                    {
                        ctrlJurnalRekening1.SetJurnal(NoJurnal,lst);

                    }
                }
                else
                {
                    long NoJurnal = DataFormat.GetLong(gridListJurnal.Rows[e.RowIndex].Cells[0].Value);
                    frmJurnal f = new frmJurnal();
                    f.Jenis = m_iJenisJurnal;
                    f.Sumber = m_iSumberJurnal;
                    Jurnal j = new Jurnal();
                    j = m_lst.FirstOrDefault(x => x.NoJurnal == NoJurnal);
                    f.SetJurnal(j);
                    f.ShowDialog();
                }
            }
        }

        private void ctrlPanelPencarian1_OnDisplay()
        {
            LoadData();
        }
    }
}
