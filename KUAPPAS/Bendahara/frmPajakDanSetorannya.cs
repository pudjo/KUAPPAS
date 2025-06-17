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

using System.Data.OleDb;
using DTO.Bendahara;
using BP.Bendahara;
namespace KUAPPAS.Bendahara
{
    public partial class frmPajakDanSetorannya : ChildForm
    {
        private int m_IDDinas;
        private DateTime mTanggalAwal;
        private DateTime mTanggalAkhir;
        List<PajakdanPenyetoran> mlstPajakDanSetoran = new List<PajakdanPenyetoran>();
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;

        public frmPajakDanSetorannya()
        {
            InitializeComponent();
        }

        private void cmdTampilkan_Click(object sender, EventArgs e)
        {
            try
            {
                GetTanggal();
                m_IDDinas = ctrlDinas1.GetID();
                gridPajakDanSetor.Rows.Clear();

                decimal JumlahPungut = 0L;
                decimal JumlahSetor = 0L;

                mlstPajakDanSetoran = new List<PajakdanPenyetoran>();
                PajakDanPenyetoranLogic oLogic = new PajakDanPenyetoranLogic(GlobalVar.TahunAnggaran);
                mlstPajakDanSetoran = oLogic.GetPajakPenyetoran(m_IDDinas, mTanggalAwal, mTanggalAkhir, cmbStatusSetor.SelectedIndex);
                if (mlstPajakDanSetoran != null)
                {
                    int irow = 0;
                    foreach (PajakdanPenyetoran ps in mlstPajakDanSetoran)
                    {
                        string[] row ={
                                           ps.inourutPanjar.ToString(),
                                           ps.NoBuktiBelanja,
                                           ps.TanggalBelanja.ToTanggalIndonesia(true),
                                           ps.KeteranganBelanja,
                                           ps.NamaPungut,
                                           ps.JumlahPungut.ToRupiahInReport(),
                                           ps.inourutSetor.ToString(),
                                           ps.NoBuktiSetor,

                                           ps.inourutSetor>0? ps.TanggalSetor.ToTanggalIndonesia(true):"",
                                           ps.KeterangabSetor,
                                           ps.NamaPungut,
                                           ps.JumlahSetor.ToRupiahInReport(),
                                           "Setor ",
                                           "Hapus"


                                       };

                        JumlahPungut = JumlahPungut+ ps.JumlahPungut;
                        JumlahSetor = JumlahSetor + ps.JumlahSetor;


                        gridPajakDanSetor.Rows.Add(row);
                        if (ps.inourutSetor == 0)
                        {

                            gridPajakDanSetor.Rows[irow].DefaultCellStyle.BackColor = Color.AntiqueWhite;// Red;
                        }
                        else
                        {
                            if (ps.inourutPanjar == 0)
                            {
                                gridPajakDanSetor.Rows[irow].DefaultCellStyle.BackColor = Color.LightSalmon;// Red;
                            }
                            else
                            {
                                gridPajakDanSetor.Rows[irow].DefaultCellStyle.BackColor = Color.LightBlue;// Red;
                            }
                            

                        }

                        irow++;
                    }
                    groupPencarian.Visible = true;
                    gridPajakDanSetor.Columns[12].DefaultCellStyle.BackColor = Color.DarkBlue;

                    txtJumlahPungut.Text = JumlahPungut.ToRupiahInReport();
                    txtJumlahSetor.Text = JumlahSetor.ToRupiahInReport();

                //    gridPajakDanSetor.Columns[12].DefaultCellStyle.ForeColor= Color.White;

                }
            }
            catch (Exception ex)
            {


            }


        }
        private void GetTanggal()
        {
            mTanggalAwal = ctrlTanggalBulan1.TanggalAwal;
            mTanggalAkhir = ctrlTanggalBulan1.TanggalAkhir;
        }
        private void frmPajakDanSetorannya_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            this.Text ="Pajak dan Penyetorannya";
            ctrlHeader1.SetCaption("Penyetoran Pajak (Baru)");
            gridPajakDanSetor.FormatHeader();
            cmbStatusSetor.Items.Add("Semua");
            cmbStatusSetor.Items.Add("Pajak BELUM disetor");
            cmbStatusSetor.Items.Add("Pajak SUDAH disetor");
            picBelumSetor.BackColor = Color.AntiqueWhite;
            picSudahSetor.BackColor = Color.LightBlue;
            picSampaj.BackColor = Color.LightSalmon;
        }

        private void gridPajakDanSetor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==12){
                PajakdanPenyetoran ps= new PajakdanPenyetoran();
                ps= mlstPajakDanSetoran[e.RowIndex];
                frmPajak2 fpajak= new frmPajak2();
                fpajak.SetPajak(ps);
                fpajak.Show ();

            }
            if (e.ColumnIndex == 13)
            {
                if (MessageBox.Show("Apakah benar akan menghapus Penyetoran Pajak ini?","Konfirmasi",MessageBoxButtons.YesNo)==DialogResult.Yes){

                        PajakdanPenyetoran ps = new PajakdanPenyetoran();
                        ps = mlstPajakDanSetoran[e.RowIndex];
                        SetorLogic oLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                        if (oLogic.Hapus(ps.inourutSetor, E_JENIS_SETOR.E_SETOR_PAJAK) == true)
                        {
                            MessageBox.Show("Data Penyetoran Pajak sudah dihapus");
                        }
                        else
                        {
                            MessageBox.Show("Gagal menghapus data penyetoran Pajak");
                        }

                }

            }
        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridPajakDanSetor.Rows)
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
                    gridPajakDanSetor.CurrentCell = containingCells[currentContainingCellListIndex++];
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
                gridPajakDanSetor.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void cmdCekBKU_Click(object sender, EventArgs e)
        {
            BKULogic oLogic = new BKULogic(GlobalVar.TahunAnggaran);

            foreach (DataGridViewRow row in gridPajakDanSetor.Rows)
            {
                long noUrutPungut = DataFormat.GetLong(row.Cells[0].Value);
                long noUrutSetor = DataFormat.GetLong(row.Cells[6].Value);
                if (oLogic.CekNoUrutSumberDiBKU(noUrutPungut, 9, 2) == 0)
                {
                    row.Cells[1].Style.BackColor = Color.Red;
                }
                if (oLogic.CekNoUrutSumberDiBKU(noUrutSetor, 17, 2) == 0)
                {
                    row.Cells[6].Style.BackColor = Color.Red;
                }

            }
        }
    }
}
