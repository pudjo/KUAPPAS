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
using DTO.Bendahara;

namespace KUAPPAS.Bendahara
{
    public partial class frmPerbaikiNoBKU : Form
    {
        int m_IDDInas;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;
        int m_iJenisBendahara;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        public int miBulan ;
        public int JenisTanggal ;
        public frmPerbaikiNoBKU()
        {
            InitializeComponent();
        }
        public int Dinas
        {
            set
            {
                m_IDDInas = value;
                ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
                ctrlSKPD1.SetID(m_IDDInas);
            }
        }
        public int Bulan
        {
            set
            {
                miBulan = value;
                ctrlTanggalBulanVertikal1.Bulan = value;
            }
        }
        public int JenisPeriode
        {
            set
            {

                JenisTanggal = value;
                ctrlTanggalBulanVertikal1.JenisPeriode = value;
            }
        }
        
        public DateTime TanggalAwal
        {
            set { 
                mTanggalAwal = value;
                ctrlTanggalBulanVertikal1.TanggalAwal = value;
            }
        }
        public DateTime TanggalAkhir
        {
            set { 
                mTanggalAkhir = value;
                ctrlTanggalBulanVertikal1.TanggalAkhir = value;
            }
        }
        public int JenisBendahara
        {
            set { 
                m_iJenisBendahara = value;
                lblJenisBendahara.Text = m_iJenisBendahara == 1 ? "Bendahara Penerimaan" : "Bendahara Pengeluaran";

            }
        }
        private void cmdTampilkan_Click(object sender, EventArgs e)
        {



            gridBKU.Rows.Clear();
            BKULogic oLogic = new BKULogic(GlobalVar.TahunAnggaran);
            int i = 0;
            if (chkMulaidariNol.Checked == false)
            {
                BKU MaxNoBKU = new BKU();
             
                List<long> lstNoUrut = new List<long>();

                MaxNoBKU = oLogic.GetBKUDenganMaxNoBKU(m_IDDInas, 0, m_iJenisBendahara, mTanggalAwal.AddDays(-1));
                i = MaxNoBKU.NoBKUSKPD;

            }

            if (oLogic.UpdateKelompok(m_IDDInas, m_iJenisBendahara, mTanggalAwal, mTanggalAkhir) == true)
            {
                List<BKUDISPLAY> lst = new List<BKUDISPLAY>();
                
                lst = oLogic.GetBKUUntukDiUrutkan(m_IDDInas, m_iJenisBendahara, mTanggalAwal, mTanggalAkhir);
                if (lst != null)
                {
                    foreach(BKUDISPLAY bd in lst)
                    {
                        if (bd.Level== 1)
                        {
                            string[] row ={ bd.NoUrut.ToString(),
                                         "false",
                                         (++i).ToString(),
                                         bd.Tanggal.ToTanggalIndonesia(),
                                         bd.NoBukti, 
                                         bd.Uraian,
                                         bd.Penerimaan.ToRupiahInReport(), 
                                         bd.Pengeluaran.ToRupiahInReport(),
                                         bd.JenisSumber.ToString(),
                                         bd.NoUrutSumber.ToString()};

                            gridBKU.Rows.Add(row);
                        }
                    }
                }
            
            }
        }

        private void cmdHapus_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah benar akan menghapus BKU yang dipilih?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                foreach (DataGridViewRow row in gridBKU.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                        bool bDipilih = Convert.ToBoolean(row.Cells[1].Value);
                        long NoUrutSumber = DataFormat.GetLong(row.Cells[9].Value);
                        int JenisSumber = DataFormat.GetInteger(row.Cells[8].Value);
                        if (bDipilih == true)
                        {

                            BKULogic oLogic = new BKULogic(GlobalVar.TahunAnggaran);
                            oLogic.Hapus(NoUrut, NoUrutSumber, JenisSumber, m_iJenisBendahara);
                      

                        }

                    }
                }
                MessageBox.Show("BKU Sudah dihapus");
                cmdTampilkaAsli.PerformClick();
            }
        }

        private void cmdPerbaiki_Click(object sender, EventArgs e)
        {
            BKULogic oLogic = new BKULogic(GlobalVar.TahunAnggaran);
            bool retVal = true;
            foreach (DataGridViewRow row in gridBKU.Rows)
            {
               
                long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                bool bDipilih = Convert.ToBoolean(row.Cells[1].Value);
                long NoUrutSumber = DataFormat.GetLong(row.Cells[9].Value);
                int JenisSumber = DataFormat.GetInteger(row.Cells[8].Value);
                int NoBKUSKPD =  DataFormat.GetInteger(row.Cells[2].Value);
                if (oLogic.PerbaikiNoBKU(NoUrut,NoBKUSKPD, NoUrutSumber, JenisSumber, m_iJenisBendahara) == false)
                {
                    MessageBox.Show("Kesalahan pemberian No BKU.");
                    return ;

                
                }

            }
            MessageBox.Show("No BKU SUdah dirapikaan");



        }

        private void cmdTampilkaAsli_Click(object sender, EventArgs e)
        {
            
            BKULogic oLogic = new BKULogic((int)GlobalVar.TahunAnggaran);
            gridBKU.Rows.Clear();
            List<BKUDISPLAY> lst = new List<BKUDISPLAY>();
            List<int> lstJenisSumber = new List<int>();
            lst = oLogic.GetBKU(m_IDDInas, m_iJenisBendahara, mTanggalAwal, mTanggalAkhir, 0, lstJenisSumber);
            int i = 0;

            if (lst != null)

            {
                foreach (BKUDISPLAY bd in lst)
                {
                    if (bd.Level  == 1)
                    {
                        string[] row ={ bd.NoUrut.ToString(),
                                         "false",
                                         bd.NoBkUSKPD.ToString(),
                                         bd.Tanggal.ToTanggalIndonesia(),
                                         bd.NoBukti, 
                                         bd.Uraian,
                                         bd.Penerimaan.ToRupiahInReport(), 
                                         bd.Pengeluaran.ToRupiahInReport(),
                                         bd.JenisSumber.ToString(),
                                         bd.NoUrutSumber.ToString()};

                        gridBKU.Rows.Add(row);
                    }

                }
            }
        }

        private void frmPerbaikiNoBKU_Load(object sender, EventArgs e)
        {

        }

        private void cmdPilihSemua_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridBKU.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    row.Cells[1].Value = true;
                }
            }
        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridBKU.Rows)
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
                    gridBKU.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridBKU.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }
    }
}
