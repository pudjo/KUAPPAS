using BP;
using DTO;
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

namespace KUAPPAS
{
    public partial class frmPenggunaRinci : Form
    {
        private List<cOtoritas> mLstGroup;
        private List<SKPD> m_lstSKPD;
        private Pengguna m_oPengguna;

        private List<ListItemData> m_lstStatus;
        public frmPenggunaRinci()
        {
            InitializeComponent();
            mLstGroup = new List<cOtoritas>();
            m_lstSKPD = new List<SKPD>();
            m_oPengguna = new Pengguna();
            m_lstStatus = new List<ListItemData>();
        }

        private void frmPenggunaRinci_Load(object sender, EventArgs e)
        {

        }
        public void SetNew()
        {
            ctrlNavigation1.SetNew();

        }
        public void SetPengguna(Pengguna oPengguna)
        {
            m_oPengguna = oPengguna;
            if (oPengguna != null)
            {
                ctrlStatusPengguna1.Create();
                ctrlDinas1.Create();

                txtID.Text = oPengguna.ID.ToString();
                txtID.Enabled = false;
                txtNama.Text = oPengguna.Nama;
                txtNIK.Text = oPengguna.NIK;
                txtUserID.Text = oPengguna.UserID;
                if (oPengguna.SKPD > 0)
                {
                    ctrlDinas1.SetID(oPengguna.SKPD, oPengguna.KodeUK);
                }

                ctrlStatusPengguna1.Status = (int)oPengguna.Status;
                if (ctrlStatusPengguna1.Status==0)
                {
                   lblPeringatanStatus.Visible =true;
                } else {
                   lblPeringatanStatus.Visible =false;
               }
                ctrlOtoRitas1.Create();
                ctrlOtoRitas1.Otoritas = oPengguna.Kelompok;
              




               

               


            }
        }

  

        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            ctrlStatusPengguna1.Create();

            txtID.Text = "0";
            txtID.Enabled = false;
            txtNama.Text = "";
            txtNIK.Text = "";
            txtUserID.Text = "";
            ctrlStatusPengguna1.Create();
            ctrlStatusPengguna1.Status = 0;
            ctrlStatusPengguna1.Enabled = false;
            lblPeringatanStatus.Visible = true;
            ctrlDinas1.Create();
            ctrlOtoRitas1.Create();
         
            return lRet;
        }

        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            PenggunaLogic oLOgic = new PenggunaLogic(GlobalVar.TahunAnggaran);
            lRet.ResponseStatus = true;
            try
            {

                if (txtNama.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Belum mengisi Nama..");
                    lRet.ResponseStatus = false;
                    return lRet;
                }
                if (txtUserID.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Belum mengisi Nama untuk Login..");
                    lRet.ResponseStatus = false;
                    return lRet;
                }
                if (oLOgic.GetByUserID(txtUserID.Text.Trim()) != null && DataFormat.GetInteger(txtID.Text)==0)
                {
                    MessageBox.Show("User ID Sudah dipakai..");
                    lRet.ResponseStatus = false;
                    return lRet;
                }


                
                //if ( == 0)
                //{
                //    MessageBox.Show("Belum mengisi Nama untuk Login..");
                //    lRet.ResponseStatus = false;
                //    return lRet;
                //}


                Pengguna oPengguna = new Pengguna();
                
                oPengguna.ID = DataFormat.GetInteger(txtID.Text);
                oPengguna.Nama = txtNama.Text;
                oPengguna.NIK = txtNIK.Text;
                oPengguna.UserID = txtUserID.Text;
                oPengguna.Kelompok = ctrlOtoRitas1.Otoritas;
                oPengguna.SKPD = 0;
                oPengguna.KodeUK = 0;
                
                if (ctrlDinas1.GetID() > 0)
                {
                    oPengguna.SKPD = ctrlDinas1.GetIDSKPD();
                    if (ctrlDinas1.GetKodeUK() > 0) { 
                        oPengguna.KodeUK = ctrlDinas1.GetKodeUK();
                    }
                }

                if (oPengguna.Kelompok < 0)
                {
                    MessageBox.Show("Belum memilih Otoritas Pengguna...");
                    lRet.ResponseStatus = false;
                    return lRet;
                }

                oLOgic.Simpan(ref oPengguna);
                MessageBox.Show("Penyimpanan Selesai");
                txtID.Text = oPengguna.ID.ToString();
                txtID.Enabled = false;

                // LoadPengguna();

                return lRet;

            }
            catch (Exception ex)
            {
                MessageBox.Show(oLOgic.LastError());
                lRet.ResponseStatus = false;
                return lRet;

            }
        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            if (MessageBox.Show("Apakah benar akan menghapus user " + txtNama.Text + "?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PenggunaLogic oLogic = new PenggunaLogic(GlobalVar.TahunAnggaran);
                if (oLogic.Hapus(DataFormat.GetInteger(txtID.Text)))
                {
                    MessageBox.Show("Penghapusan selesai.");
                }
                else
                {
                    MessageBox.Show("Penghapusan gagal dilakukan." + oLogic.LastError());
                }
            }

            return default(EventResponseMessage);
        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }
    }
}
