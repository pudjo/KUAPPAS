using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using BP;
using Formatting;

namespace KUAPPAS
{
    public partial class frmPengguna : ChildForm
    {
        private List<Pengguna> mlst = new List<Pengguna>();
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;

        public frmPengguna()
        {
            InitializeComponent();
            mlst = new List<Pengguna>();
        }

        private void frmPengguna_Load(object sender, EventArgs e)
        {
           // ctrlHeader1.SetCaption("Daftar Pengguna", "Setting pengguna aplikasi, dinas dan fungsi");
            this.WindowState = FormWindowState.Maximized;
            ctrlHeader1.SetCaption("Daftar Pengguna");

            gridPengguna.FormatHeader();
            ctrlDinas1.Create();
            ctrlOtoRitas1.Create();
        }
        private void LoadPengguna()
        {
            PenggunaLogic oLOgic = new PenggunaLogic(GlobalVar.TahunAnggaran);
            List<Pengguna> lstPengguna = new List<Pengguna>();

            mlst = oLOgic.Get(GlobalVar.Pengguna.Kelompok);

            if (chkSemuaDinas.Checked == true)
            {
                lstPengguna = mlst.FindAll(x => x.SKPD == 0 || x.SKPD > 0 );
            }
            else
            {
                int skpd = ctrlDinas1.GetID();
                lstPengguna = mlst.FindAll(x => x.SKPD == skpd);

            }

            if (ctrlOtoRitas1.Otoritas > 0)
            {
                lstPengguna = lstPengguna.FindAll(x => x.Kelompok == ctrlOtoRitas1.Otoritas);
            }


            
            gridPengguna.Rows.Clear();
            string namaSKPD = "";
            string NamaKelompok = "";
            if (mlst  != null)
            {
                foreach (Pengguna p in lstPengguna)
                {
                    if (p.SKPD > 0)
                    {
                        SKPD oSKPD= GlobalVar.gListSKPD.FirstOrDefault(x=>x.ID == p.SKPD);
                        if (oSKPD != null)
                            namaSKPD = oSKPD.Nama;


                    }
                    if (p.Kelompok > 0)
                    {
                        NamaKelompok = "";
                        cOtoritas o = GlobalVar.gListOtoritas.FirstOrDefault(x => x.ID == p.Kelompok);
                        if (o != null)
                        {
                            NamaKelompok = o.Nama;
                        }
                    }

                    string[] row = { p.ID.ToString(), "Detail", p.Nama, 
                                     p.UserID, p.NIK, namaSKPD, NamaKelompok,"Ref Password"};
                    gridPengguna.Rows.Add(row);
                }
            }
            else
            {
                MessageBox.Show(oLOgic.LastError());

            }

        }
        
        
        private void gridPengguna_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridPengguna.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    return;
                }
                Pengguna oPengguna = new Pengguna();
                if (e.ColumnIndex == 1)
                {

                    int ID = DataFormat.GetInteger(gridPengguna.Rows[e.RowIndex].Cells[0].Value);
                    if (ID > 0)
                    {
                        oPengguna = mlst.FirstOrDefault(x=>x.ID == ID);
                        if (oPengguna != null)
                        {
                            frmPenggunaRinci fpD = new frmPenggunaRinci();
                            fpD.SetPengguna(oPengguna);
                            fpD.ShowDialog();
                        }
                    }

                }
                if (e.ColumnIndex == 7)
                {
                    int ID = DataFormat.GetInteger(gridPengguna.Rows[e.RowIndex].Cells[0].Value);
                    if (MessageBox.Show("Apakah benar akan mesetting pengguna ini , untuk membuat password baru?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        PenggunaLogic oPenggunaLogic = new PenggunaLogic(GlobalVar.TahunAnggaran);
                       
                        oPengguna.ID = ID;
                        oPengguna.Status = 0;

                        if (oPenggunaLogic.SetNewStatus(oPengguna) == true)
                        {
                            MessageBox.Show("Setting Status berhasil. Pengguna harue msmbuat password baru");
                        }
                        else {
                            MessageBox.Show("Setting Status GAGAL. Hubungi Admin..");
                        }

                    }
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            
            

        }


        private void gridPengguna_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

            
           
        }

        
        private void cmdCari_Click(object sender, EventArgs e)
        {
            containingCells.Clear();
            currentContainingCellListIndex = 0;
            foreach (DataGridViewRow row in gridPengguna.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == DBNull.Value || cell.Value == null)
                        continue;
                    if (cell.Value.ToString().ToUpper().Contains(txtCari.Text.Trim().ToUpper()))
                    {
                        containingCells.Add(cell);
                    }
                }
            }
            if (containingCells.Count > 0)
                gridPengguna.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }
        private void CariLagi(){
             if (containingCells.Count > 0 && currentContainingCellListIndex < containingCells.Count)
                 gridPengguna.CurrentCell =
                         containingCells[currentContainingCellListIndex++];
        }

        private void cmdCariLagi_Click(object sender, EventArgs e)
        {
            CariLagi();

        }

        private void cmdTambah_Click(object sender, EventArgs e)
        {
            frmPenggunaRinci fPenggunadetail = new frmPenggunaRinci();
            fPenggunadetail.SetNew();

            fPenggunadetail.Show();
        }

        private void cmdLoadData_Click(object sender, EventArgs e)
        {
            LoadPengguna();
        }
    }
}
