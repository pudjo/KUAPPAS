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
    public partial class frmDasarHukum : Form
    {
        public frmDasarHukum()
        {
            InitializeComponent();
        }

        private void frmDasarHukum_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ctrlHeader1.SetCaption("Setting Dasar Hukum", "");
            treeRekening1.Create();
            gridSasarHukum.FormatHeader();
            LoadDasarHukum();
        }
        private void LoadDasarHukum()
        {
            DasarHukumLogic oLogic = new DasarHukumLogic(GlobalVar.TahunAnggaran);
            List<DasarHukum> _lst = new List<DasarHukum>();
            Single snglOnPerda = rbPerda.Checked?1:0;

            _lst = oLogic.Get((int)GlobalVar.TahunAnggaran, snglOnPerda);


            gridSasarHukum.Rows.Clear();

            if (_lst != null)
            {
                foreach (DasarHukum dh in _lst)
                {
                    string[] row = { dh.IDRekening.ToString(), dh.IDRekening.ToKodeRekening(GlobalVar.ProfileRekening), dh.NamaRekening,dh.NoUrut.ToString(), dh.Keterangan };
                    gridSasarHukum.Rows.Add(row);

                }
            }

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeRekening1_Changed(Rekening rek)
        {
            rbPerda.Checked = false;
            rbPerkada.Checked = true;

            ctrlKodeRekeningTerpisah1.Create(rek);
            LoadDasarHukum();


        }

        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ctrlKodeRekeningTerpisah1.Clear();

            return default(EventResponseMessage);
        }

        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            DasarHukumLogic oLOgic = new DasarHukumLogic(GlobalVar.TahunAnggaran);
            DasarHukum oDasarHukum = new DasarHukum();
            oDasarHukum.IDRekening = ctrlKodeRekeningTerpisah1.GetID();
            oDasarHukum.Keterangan = txtKeterangan.Text;
            oDasarHukum.Tahun = GlobalVar.TahunAnggaran;
            oDasarHukum.IDDInas = 0;
            oDasarHukum.KodeKategori = 0;
            oDasarHukum.KodeUrusan = 0;
            oDasarHukum.KodeSKPD = 0;
            oDasarHukum.NoUrut = DataFormat.GetSingle(txtNo.Text);
            oDasarHukum.OnPerda = rbPerda.Checked ? 1 : 0;


            if (oLOgic.Simpan(oDasarHukum) == true)
            {
                LoadDasarHukum();
                return lRet;
            }
            else
            {
                MessageBox.Show(oLOgic.LastError());
                lRet.ResponseStatus = false;
                return lRet;

            }

            
        }

        private void gridSasarHukum_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridSasarHukum.Rows.Count)
            {
                long _idRekening = DataFormat.GetLong(gridSasarHukum.Rows[e.RowIndex].Cells[0].Value);
                RekeningLogic oLogic = new RekeningLogic(GlobalVar.TahunAnggaran,RekeningLogic.E_REKENING_TYPE.REKENING_13);
                Rekening oRekening = oLogic.GetByID(_idRekening);
                ctrlKodeRekeningTerpisah1.Create(oRekening);
                txtNo.Text = DataFormat.GetString(gridSasarHukum.Rows[e.RowIndex].Cells[3].Value);
                txtKeterangan.Text = DataFormat.GetString(gridSasarHukum.Rows[e.RowIndex].Cells[4].Value);


            }
        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            DasarHukumLogic oLOgic = new DasarHukumLogic(GlobalVar.TahunAnggaran);
            if (MessageBox.Show("Apakah benar akan menghapus Dasar Hukum ini?", "Konfirmasi", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                //DasarHukum oDasarHukum = new DasarHukum();
                //oDasarHukum.IDRekening = ctrlKodeRekeningTerpisah1.GetID();
                //oDasarHukum.Keterangan = txtKeterangan.Text;
                //oDasarHukum.Tahun = GlobalVar.TahunAnggaran;
                //oDasarHukum.IDDInas = 0;
                //oDasarHukum.KodeKategori = 0;
                //oDasarHukum.KodeUrusan = 0;
                //oDasarHukum.KodeSKPD = 0;
                if (oLOgic.Hapus(GlobalVar.TahunAnggaran, ctrlKodeRekeningTerpisah1.GetID(), DataFormat.GetInteger(txtNo.Text)) == true)
                {
                    MessageBox.Show("Penghapusan berhasil");

                    LoadDasarHukum();
                    
                    return lRet;

                }
                else
                {
                    MessageBox.Show(oLOgic.LastError());
                    lRet.ResponseStatus = false;
                    return lRet;

                }
            }
            else
            {
                return lRet;

            }
        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }

        private void rbPerda_Click(object sender, EventArgs e)
        {
            if (rbPerda.Checked == true)
            {
                ctrlKodeRekeningTerpisah1.Enabled = false;
               
                ctrlKodeRekeningTerpisah1.Clear();
                //treeRekening1.Enabled = false;
                LoadDasarHukum();

            }
        }

        private void rbPerkada_Click(object sender, EventArgs e)
        {
            if (rbPerkada.Checked == true)
            {
                ctrlKodeRekeningTerpisah1.Enabled = true;

                ctrlKodeRekeningTerpisah1.Clear();
                //treeRekening1.Enabled = false;
                LoadDasarHukum();

            }
        }
    }
}
