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
    public partial class frmKoneksiPerencanaan : Form
    {
        private int mprofile;
        public frmKoneksiPerencanaan()
        {
            InitializeComponent();
                mprofile=3;
        }

        public int Profile
        {
            set { mprofile = value; }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmKoneksiPerencanaan_Load(object sender, EventArgs e)
        {
            this.Text = "Setting Koneksi Data Perencanaan";

            ctrlHeader1.SetCaption("Setting Koneksi Data Perencanaan", "");

            RemoteConnectionLogic rcLogic = new RemoteConnectionLogic(GlobalVar.TahunAnggaran, mprofile);
            RemoteConnection rc = new RemoteConnection();
            
            rc = rcLogic.GetByJenis(1,1);
            if (rc != null)
            {
                txtServer.Text = rc.Server;
                txtDatabase.Text = rc.Database;
                txtPassword.Text = AesOperation.DecryptString(GlobalVar.Key, rc.Password);
                txtUserID.Text = AesOperation.DecryptString(GlobalVar.Key, rc.UserID);
                                
            }

            rc = rcLogic.GetByJenis(1, 2);
            if (rc != null)
            {
                txtServerInternet.Text = rc.Server;
                txtDatabaseInternet.Text = rc.Database;
                txtPasswordInternet.Text = AesOperation.DecryptString(GlobalVar.Key, rc.Password);
                txtUserIDInternet.Text = AesOperation.DecryptString(GlobalVar.Key, rc.UserID);

            }

            rc = rcLogic.GetByJenis(2, 1);
            if (rc != null)
            {
                txtServerAset.Text = rc.Server;
                txtDatabaseAset.Text = rc.Database;
                txtPasswordAset.Text = AesOperation.DecryptString(GlobalVar.Key, rc.Password);
                txtUserIDAset.Text = AesOperation.DecryptString(GlobalVar.Key, rc.UserID);

            }
            rc = rcLogic.GetByJenis(2, 2);
            if (rc != null)
            {
                txtServerAsetInternet.Text = rc.Server;
                txtDatabaseAsetInternet.Text = rc.Database;
                txtPasswordAsetInternet.Text = AesOperation.DecryptString(GlobalVar.Key, rc.Password);
                txtUserIDAsetInternet.Text = AesOperation.DecryptString(GlobalVar.Key, rc.UserID);

            }

            //txtServerAset.Text = rc.Server;
            //txtDatabaseAset.Text = rc.Database;
            //txtPasswordAset.Text = AesOperation.DecryptString(GlobalVar.Key, rc.Password);
            //txtUserIDAset.Text = AesOperation.DecryptString(GlobalVar.Key, rc.UserID);

            
        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            RemoteConnection rc = new RemoteConnection();
            rc.Server = txtServer.Text;
            rc.Database = txtDatabase.Text;
            rc.UserID = AesOperation.EncryptString(GlobalVar.Key, txtUserID.Text.Trim()); 
            rc.Password = AesOperation.EncryptString(GlobalVar.Key, txtPassword.Text.Trim());
            rc.Jenis = 1;
            rc.Jalur = 1;

            RemoteConnectionLogic rcLogic = new RemoteConnectionLogic(GlobalVar.TahunAnggaran, mprofile);
            if (rcLogic.Simpan(ref rc) == true)
            {
                MessageBox.Show("Data Jalur Jatingan tersimpan");
            }
            else
            {
                MessageBox.Show("Gagal menyimpan..\n\n" + rcLogic.LastError());
            }
            rc = new RemoteConnection();
            rc.Server = txtServerInternet.Text;
            rc.Database = txtDatabaseInternet.Text;
            rc.UserID = AesOperation.EncryptString(GlobalVar.Key, txtUserIDInternet.Text.Trim());
            rc.Password = AesOperation.EncryptString(GlobalVar.Key, txtPasswordInternet.Text.Trim());
            rc.Jenis = 1;
            rc.Jalur = 2;

            rcLogic = new RemoteConnectionLogic(GlobalVar.TahunAnggaran, mprofile);
            if (rcLogic.Simpan(ref rc) == true)
            {
                MessageBox.Show("Data Jalur Interbet tersimpan");
            }
            else
            {
                MessageBox.Show("Gagal menyimpan..\n\n" + rcLogic.LastError());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoteConnection rc = new RemoteConnection();
            rc.Server = txtServerAset.Text;
            rc.Database = txtDatabaseAset.Text;
            rc.UserID = AesOperation.EncryptString(GlobalVar.Key, txtUserIDAset.Text.Trim());
            rc.Password = AesOperation.EncryptString(GlobalVar.Key, txtPasswordAset.Text.Trim());
            rc.Jenis = 2;
            rc.Jalur = 1;
            RemoteConnectionLogic rcLogic = new RemoteConnectionLogic(GlobalVar.TahunAnggaran, mprofile);
            if (rcLogic.Simpan(ref rc) == true)
            {
                MessageBox.Show("Data kondeksi Aset tersimpan");
            }
            else
            {
                MessageBox.Show("Gagal menyimpan Data Kondeksi Jaringan Aset ..\n\n" + rcLogic.LastError());
            }

            rc = new RemoteConnection();
            rc.Server = txtServerAsetInternet.Text;
            rc.Database = txtDatabaseAsetInternet.Text;
            rc.UserID = AesOperation.EncryptString(GlobalVar.Key, txtUserIDAsetInternet.Text.Trim());
            rc.Password = AesOperation.EncryptString(GlobalVar.Key, txtPasswordAsetInternet.Text.Trim());
            rc.Jenis = 2;
            rc.Jalur = 2;


            rcLogic = new RemoteConnectionLogic(GlobalVar.TahunAnggaran, mprofile);
            if (rcLogic.Simpan(ref rc) == true)
            {
                MessageBox.Show("Data Kondeksi Aset Internet tersimpan");
            }
            else
            {
                MessageBox.Show("Gagal  menyimpan Kondeksi Aset Internet ..\n\n" + rcLogic.LastError());
            }


        }
    }
}
