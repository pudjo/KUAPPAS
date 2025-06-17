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
    public partial class frmRekening : Form
    {
        private bool m_bNew;

        public frmRekening()
        {
            InitializeComponent();
        }

        private void frmRekening_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ctrlHeader1.SetCaption("Setting Kode Rekening", "Setting Kode Rekening");
            ctrlProfileRekening1.Create();
            treeRekening1.Create();
            
        }

        private void treeRekening1_Changed(Rekening rek)
        {
            ctrlKodeRekening.Create(rek);
            long IDParent = rek.IDParent;
            RekeningLogic oLogic = new RekeningLogic(GlobalVar.TahunAnggaran,RekeningLogic.E_REKENING_TYPE.REKENING_13);
            Rekening oRekeningParent = new Rekening();
            oRekeningParent= oLogic.GetParentByID(rek.ID);
            ctrlKodeRekeningParent.Create(oRekeningParent);
            txtNamaRekening.Text = rek.Nama;
            txtRoot.Text = rek.Root.ToString();
            txtRoot.Enabled = false;
            chkPunyaAnak.Checked = rek.Leaf == 1 ? false : true;
            m_bNew = false;


        }

        private void treeRekening1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //menuAdd.Show();
            m_bNew = true;
            Rekening oRek = new Rekening();
            RekeningLogic oLogic = new RekeningLogic(GlobalVar.TahunAnggaran,RekeningLogic.E_REKENING_TYPE.REKENING_13);
            oRek= oLogic.GetByID(ctrlKodeRekeningParent.GetID());
            ctrlKodeRekening.SetNewWithParent(oRek);
            txtNamaRekening.Text = "";
            txtRoot.Text = (oRek.Root + 1).ToString();
 
            
        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            RekeningLogic oLogic = new RekeningLogic(GlobalVar.TahunAnggaran,RekeningLogic.E_REKENING_TYPE.REKENING_13);
            Rekening oRek = new Rekening();
            oRek.Baru = m_bNew;

            oRek.ID = ctrlKodeRekening.GetID();
            oRek.Nama = txtNamaRekening.Text;
            oRek.Root = DataFormat.GetSingle(txtRoot.Text);
            oRek.IDParent = ctrlKodeRekeningParent.GetID();
            oRek.Leaf = chkPunyaAnak.Checked ? 0 : 1;
            if (oLogic.Simpan(ref oRek))
            {
                m_bNew = false;
                MessageBox.Show("Penyimpanan berhasil");
            }
            else
            {
                MessageBox.Show(oLogic.LastError());
            }




                 
        }

        private void cmdHapus_Click(object sender, EventArgs e)
        {
            RekeningLogic oLogic = new RekeningLogic(GlobalVar.TahunAnggaran,RekeningLogic.E_REKENING_TYPE.REKENING_13);
            Rekening oRek = new Rekening();
            oRek.ID = ctrlKodeRekening.GetID();

            
            if (oLogic.Hapus(oRek.ID))
            {
                
                MessageBox.Show("Penghapusan berhasil");
            }
            else
            {
                MessageBox.Show(oLogic.LastError());
            }

        }

        private void cmdTambahAnak_Click(object sender, EventArgs e)
        {
            Rekening oRekeningParent = new Rekening();
            RekeningLogic oLogic = new RekeningLogic(GlobalVar.TahunAnggaran, RekeningLogic.E_REKENING_TYPE.REKENING_13);
            oRekeningParent = oLogic.GetByID(ctrlKodeRekening.GetID());
            ctrlKodeRekeningParent.Create(oRekeningParent);
            ctrlKodeRekening.SetNewWithParent(oRekeningParent);
            txtNamaRekening.Text = "";
            txtRoot.Text = (oRekeningParent.Root + 1).ToString();
 

        }
    }
}
