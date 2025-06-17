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


using BP;
using BP.Bendahara;
using DTO;
using DTO.Bendahara;
namespace KUAPPAS.Bendahara
{
    public partial class frmCariKontrak : Form
    {
        private int m_iIDDInas = 0;
        private DateTime mTanggalAwal;
        private DateTime mTanggalAkhir;
        private bool m_bOK;
        private Kontrak mKontrakdipilih;
        List<Kontrak> m_ListKontrak = new List<Kontrak>();
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;

        public frmCariKontrak()
        {
            InitializeComponent();
            m_bOK = false;

        }

        private void frmCariKontrak_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption ("Pencarian Kontrak");
            gridKontrak.FormatHeader();

            if (m_iIDDInas == 0)
            {
                ctrlDinas1.Create();
            }
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                m_iIDDInas = GlobalVar.Pengguna.SKPD;
                ctrlDinas1.SetID(m_iIDDInas);
            }
            mKontrakdipilih = new Kontrak();

            mTanggalAwal= new DateTime(GlobalVar.TahunAnggaran,1,1);
            mTanggalAwal= new DateTime(GlobalVar.TahunAnggaran,12,31);
            ctrlTanggalBulan1.TanggalAwal = mTanggalAwal;
            ctrlTanggalBulan1.TanggalAkhir = mTanggalAkhir;

        }
        private void GetTanggal()
        {
            mTanggalAwal= ctrlTanggalBulan1.TanggalAwal;
            mTanggalAkhir= ctrlTanggalBulan1.TanggalAkhir;
        }

        private void cmdtampilkan_Click(object sender, EventArgs e)
        {

            try
            {

                KontrakLogic oLogic = new KontrakLogic((int)GlobalVar.TahunAnggaran);
                DateTime tanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                DateTime tanggalAkhir = new DateTime(GlobalVar.TahunAnggaran, 12, 31);


                gridKontrak.Rows.Clear();

                              
               m_ListKontrak = oLogic.GetByIDDinas(m_iIDDInas);
              
                   
                
               
                if (LoadData() == true)
                {
                    lblPencarian.Visible = true;
                    txtCari.Visible = true;
                    cmdCari.Visible = true;
                    cmdCariLagi.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        public int IDDinas
        {
            set
            {
                m_iIDDInas = value;
                ctrlDinas1.Create();
                ctrlDinas1.SetID(value);
            }
        } 
        private bool LoadData()
        {

            try
            {
                lblPencarian.Visible = false;
                txtCari.Visible = false;
                cmdCari.Visible = false;
                cmdCariLagi.Visible = false;

                if (m_ListKontrak != null)
                {
                    if (txtNoKontrak.Text.Trim().Length > 0)
                    {
                        foreach (Kontrak k in m_ListKontrak)
                        {
                            if (k.NoKontrak.ToUpper().Contains(txtNoKontrak.Text.Trim().ToUpper()))
                            {
                                string[] row = { k.NoUrut.ToString(), k.NoKontrak, k.DtKontrak.ToString("dd MMM "), k.Uraian, k.NamaPerusahaan, k.Jumlah.ToRupiahInReport() };
                                gridKontrak.Rows.Add(row);
                            }

                        }
                    }
                    else
                    {
                        foreach (Kontrak k in m_ListKontrak)
                        {
                            string[] row = { k.NoUrut.ToString(), k.NoKontrak, k.DtKontrak.ToString("dd MMM "), k.Uraian, k.NamaPerusahaan, k.Jumlah.ToRupiahInReport() };
                            gridKontrak.Rows.Add(row);

                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }



        }

        private void cmdbatal_Click(object sender, EventArgs e)
        {
            m_bOK = false;
            this.Hide();
        }

        private void gridKontrak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex> 0  && e.ColumnIndex < gridKontrak.ColumnCount && 
                e.RowIndex >= 0 && e.RowIndex < gridKontrak.Rows.Count)
            {
                long noUrut = DataFormat.GetLong(gridKontrak.Rows[e.RowIndex].Cells[0].Value);
                mKontrakdipilih = new Kontrak();

                mKontrakdipilih = m_ListKontrak.FirstOrDefault(k => k.NoUrut == noUrut);
                if (mKontrakdipilih != null)                {
                    lblNo.Text = mKontrakdipilih.NoKontrak + " ( "+ DataFormat.GetString(gridKontrak.Rows[e.RowIndex].Cells[4].Value) + ")";
                    lblKeterangan.Text = mKontrakdipilih.Uraian;
                    lblJumlah.Text = mKontrakdipilih.Jumlah.ToRupiahInReport();


                }
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            m_bOK = true;
            this.Hide();
        }
        public bool IsOk()
        {
            return m_bOK;
        }
        public Kontrak GetKontrakDipilih()
        {
            if (mKontrakdipilih != null)
            {
                return mKontrakdipilih;
            }
            else
            {
                MessageBox.Show("Belum ada kontrak dipilih");
                return null;
            }
        }
    }
}
