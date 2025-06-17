using BP;
using DTO;
using Formatting;
using Menu;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Z80NavBarControl.Z80NavBar.Themes;
using Z80NavBarControl;


namespace KUAPPAS
{
    public partial class frmListDinas : Form
    {
        public frmListDinas()
        {
            InitializeComponent();
            
        }

        private void frmListDinas_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Daftar Dinas (SKPD/Unit Kerja)", "Klik Detail untuk melihat setting detail dari Dinas Bersangkutan.");
            this.WindowState = FormWindowState.Maximized;

            gridSKPD.FormatGridView();
            gridSKPD.FormatHeader();
            ctrlUrusan1.Create();

            LoadData();

        }
        private void LoadData()
        {

            List<SKPD> _lst = new List<SKPD>();
            SKPDLogic oLogic = new SKPDLogic(GlobalVar.TahunAnggaran);

            _lst = oLogic.Get((int)GlobalVar.TahunAnggaran);

            gridSKPD.Rows.Clear();

            if (_lst != null)
            {
                foreach (SKPD o in _lst)
                {
                    if (GlobalVar.Pengguna.SKPD > 0)
                    {
                        if (o.ID == GlobalVar.Pengguna.SKPD)
                        {
                            string[] row = { "Detail", o.ID.ToString(), o.Tampilan, o.Nama, o.KodeKategori.ToString(), o.KodeUrusan.ToString(), o.Kode.ToString() };
                            gridSKPD.Rows.Add(row);
                        }
                    }
                    else
                    {
                        int _urusanDipilih = ctrlUrusan1.GetID();
                        if (_urusanDipilih > 0 && chkSemuaUrusan.Checked== false )
                        {
                            if (_urusanDipilih == o.IDUrusan)
                            {
                                
                                    string[] rowx = { "Detail", o.ID.ToString(), o.Tampilan, o.Nama, o.KodeKategori.ToString(), o.KodeUrusan.ToString(), o.Kode.ToString() };
                                    gridSKPD.Rows.Add(rowx);
                                
                            }
                        }
                        else
                        {
                                string[] rowx = { "Detail", o.ID.ToString(), o.Tampilan, o.Nama, o.KodeKategori.ToString(), o.KodeUrusan.ToString(), o.Kode.ToString() };
                                gridSKPD.Rows.Add(rowx);
                            
                        }
                    }



                }

            }


        }
        private void LoadDataLama()
        {

            List<Dinas> _lst = new List<Dinas>();
            DinasLogic oLogic = new DinasLogic(GlobalVar.TahunAnggaran);
            _lst = oLogic.Get();
            gridSKPD.Rows.Clear();

            if (_lst != null)
            {
                foreach (Dinas o in _lst)
                {
                    string[] row = { "Detail", o.ID.ToString(), o.Tampilan, o.Nama, o.KodeKategori.ToString(), o.KodeUrusan.ToString(), o.KodeSKPD.ToString() };
                    gridSKPD.Rows.Add(row);

                }

            }


        }

        private void gridSKPD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridSKPD.Rows.Count)
            {
                if (e.ColumnIndex == 0)
                {
                    if (gridSKPD.Rows[e.RowIndex].Cells[1].Value != null)
                    {
                        string sID = DataFormat.GetString(gridSKPD.Rows[e.RowIndex].Cells[1].Value);
                        int lID = DataFormat.GetInteger(sID);
                        frmSKPD fSKPD = new frmSKPD();
                        fSKPD.SetJenisForm(0);
                        fSKPD.SetID(lID);
                        fSKPD.ShowDialog();
                        LoadData();
                    }

                }
            }
        }

        private void chkSemuaUrusan_CheckedChanged(object sender, EventArgs e)
        {
            //
            LoadData();
            ShohHide();
        }
        private void ShohHide()
        {
            if (chkSemuaUrusan.Checked == true)
            {
                for (int row = 0; row < gridSKPD.Rows.Count; row++)
                {
                    if (gridSKPD.Rows[row].Cells[1].Value != null)
                        gridSKPD.Rows[row].Visible = true;
                }
            }
            else
            {
                for (int row = 0; row < gridSKPD.Rows.Count; row++)
                {
                    if (gridSKPD.Rows[row].Cells[1].Value != null)
                        gridSKPD.Rows[row].Visible = false;
                }

                int _idUrusan = ctrlUrusan1.GetID();
                if (_idUrusan > 0)
                {
                    for (int row = 0; row < gridSKPD.Rows.Count; row++)
                    {
                        if (gridSKPD.Rows[row].Cells[1].Value != null)
                        {
                            if (DataFormat.GetInteger(gridSKPD.Rows[row].Cells[1].Value.ToString().Substring(0, 3)) == _idUrusan)
                            {
                                gridSKPD.Rows[row].Visible = true;
                            }
                        }
                    }

                }
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            frmSKPD fSKPD = new frmSKPD();
            fSKPD.SetJenisForm(0);
            fSKPD.ShowDialog();
            LoadData();
        }

        private void ctrlUrusan1_OnChanged(int pID)
        {
            LoadData();

        }

        private void ctrlUrusan1_Load(object sender, EventArgs e)
        {

        }

        private void cmdTampilkan_Click(object sender, EventArgs e)
        {
            LoadData();
            ShohHide();

        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }
    }
}
