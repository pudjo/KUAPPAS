using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BP;
using Formatting;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Threading;
using DTO.Bendahara;
using BP.Bendahara;

namespace KUAPPAS
{
    public partial class frmLoginBaru : Form
    {
        // instance member to keep a reference to main form
        private Form MainForm;

        // flag to indicate if the form has been closed
        private bool IsClosed = false;
        private bool m_bOK;
        public delegate void UpdateUIDelegate(bool IsDataLoaded);
        Thread threadSplash;
        public frmLoginBaru()
        {
            InitializeComponent();
        }
         /// <summary>
    /// Initializes a new instance of the <see cref="SplashForm" /> class.
    /// </summary>
        public frmLoginBaru(Form mainForm)
            : this()
        {
          //// Store frmLoginBaru reference to parent form
            MainForm = mainForm;
            m_bOK = false;

          //// Attach to parent form events
          //MainForm.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
          //MainForm.Activated += new System.EventHandler(this.MainForm_Activated);
          //MainForm.Move += new System.EventHandler(this.MainForm_Move);

          //// Adjust appearance
            this.ShowInTaskbar = false; // do not show form in task bar
            this.TopMost = true; // show splash form on top of main form
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Visible = false;

          //// Adjust location
          //AdjustLocation();
        }
        public async Task CheckInternetAsync()
        {
            Ping myPing = new Ping();
            try
            {
                var pingReply = await myPing.SendPingAsync("google.com", 3000, new byte[32], new PingOptions(64, true));
                if (pingReply.Status == IPStatus.Success)
                {
                    this.rbInternet.Enabled = true;
                    rbInternet.Text = "Jalur Internet";
                    cmdRefresh.Visible = false;

                }
            }
            catch (Exception e)
            {
                this.rbInternet.Enabled = false;

                this.rbJaringan.Checked = true;
                cmdRefresh.Visible = true;

                //this.iconeConnexion = WindowsFormsApplication1.Properties.Resources.red;
            }
        }
        private async  void frmLoginBaru_Load(object sender, EventArgs e)
        {
           #if DEBUG
                  button1.Visible = true;
            #else 
                  button1.Visible= false ;
                  Pelayanan.Visible = false;
                  cmdVerifikator.Visible = false;
                  cmdSP2DOnlie.Visible = false;
            cmdCetak.Visible=false;
#endif


            cmbTahunAnggaran.Items.Add("2023");
            cmbTahunAnggaran.Items.Add("2024");
            cmbTahunAnggaran.Items.Add("2025");

            await CheckInternetAsync();
        }
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (!this.IsClosed)
            {
                this.Visible = false;
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!this.IsClosed)
            {
                this.Visible = true;
            }
        }

      
        public bool IsOK
        {
            get { return m_bOK; }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void cmdLogin_Click(object sender, EventArgs e)
        {
           

            Ini ini = new Ini(AppDomain.CurrentDomain.BaseDirectory + "KUAPPAS.ini");

            m_bOK = false;

            GlobalVar.DataSource = ini.IniReadValue("XMAN", "Alamat");
            GlobalVar.NamaDatabase= ini.IniReadValue("XMAN", "Database");



#if DEBUG
            if (txtUserID.Text.Length == 0)
            {
                txtUserID.Text = "pudjo";//"pelayanan";//
                txtPassword.Text = "Puyi54321";
            }
#endif

           
                GlobalVar.Theme = 1;

            if (txtUserID.Text.Length == 0)
            {
                MessageBox.Show("Pengguna belum diisi..");
                return;
            }


            //_Server = ini.IniReadValue("KUA", "Server");
            //_Database = ini.IniReadValue("KUA", "DATABASE");

            if (MessageBox.Show("Pengguna memilih Tahun Anggaran " + cmbTahunAnggaran.Text + ". Benar? ", "Tahun Anggaran", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            GlobalVar.TahunAnggaran = DataFormat.GetInteger(cmbTahunAnggaran.Text);


            if (GlobalVar.TahunAnggaran == 0)
            {
                MessageBox.Show("Belum Memilih Tahun Anggaran...");
                return;

            }
            if (GlobalVar.TahunAnggaran < 2021)
                GlobalVar.PP90 = false;
            else
                GlobalVar.PP90 = true;
            if (rbJaringan.Checked == true)
            {
                GlobalVar.JENIS_KONEKSI = 1;
            }
            else
            {

                GlobalVar.JENIS_KONEKSI = 2;
            }
            UpdateUI(false);
            threadSplash = new Thread(new ThreadStart(StartForm));
            threadSplash.Start();
            PenggunaLogic oLOgic = new PenggunaLogic(GlobalVar.TahunAnggaran);
            
            GlobalVar.Pengguna = oLOgic.GetByUserID(txtUserID.Text);
           
            

            
            if (GlobalVar.Pengguna == null)
            {
                if (oLOgic.LastError().Contains("Connection") == true)
                {
                    MessageBox.Show("Login Gagal: Ada masalah koneksi ke server..");
                }
                else
                {
                    MessageBox.Show("Login Gagal " + oLOgic.LastError());
                }
                return;
            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_AUDIT)
                {
                if (GlobalVar.TahunAnggaran != 2024)
                {
                    MessageBox.Show("Tahun anggaran salah");
                    return;
                }

                }
              
            if (GlobalVar.Pengguna.Status == 0)
            {

                frmBuatPassword fGantiPassword = new frmBuatPassword();
                fGantiPassword.SetPengguna(GlobalVar.Pengguna);
                fGantiPassword.ShowDialog();
                if (fGantiPassword.IsOK() == false)
                {
                    return;
                }



            }
            else
            {

                if (GlobalVar.Pengguna.Status == 9)
                {
                    MessageBox.Show("Pengguna tidak aktiv");

                    return;

                }

                ////// jika encrypt 
                if (GlobalVar.Pengguna.Password.Length > 15)
                {

                  //  if (Decrypt(GlobalVar.Pengguna.Password) != txtPassword.Text)
                    if (GlobalVar.Pengguna.Password.Trim() != PasswordUtility.Generate(txtPassword.Text).Trim())
                    {

                        MessageBox.Show("Password Salah.");
                        return;

                    }

                }
              

            }
            TahapanAnggaran ta = new TahapanAnggaran();
            TahapanAnggaranLogic oTALogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            ta = oTALogic.GetByDinas(0, GlobalVar.TahunAnggaran);

            if (ta != null)
            {
                GlobalVar.TahapAnggaran = ta.Tahap;
            }
            else

                GlobalVar.TahapAnggaran = 1;



            GlobalVar.ProfileRekening = new ProfileRekening();
            GlobalVar.ProfileProgramKegiatan = new ProfileProgramKegiatan();
            ProfileRekeningLogic oProfileRekeningLogic = new ProfileRekeningLogic(GlobalVar.TahunAnggaran, 2);
            ProfileProgramKegiatanLogic oPrgKegLogic = new ProfileProgramKegiatanLogic(GlobalVar.TahunAnggaran);

            GlobalVar.ProfileProgramKegiatan = oPrgKegLogic.Get();
            if (GlobalVar.ProfileProgramKegiatan == null)
            {
                GlobalVar.ProfileProgramKegiatan = new ProfileProgramKegiatan();
                GlobalVar.ProfileProgramKegiatan.KodeProgram = 2;
                GlobalVar.ProfileProgramKegiatan.KodeKegiatan = 2;
                oPrgKegLogic.Simpan(GlobalVar.ProfileProgramKegiatan);
                GlobalVar.ProfileProgramKegiatan = oPrgKegLogic.Get();

            }
            GlobalVar.CONNECTION_ASET = new RemoteConnection();
            RemoteConnectionLogic rcLogic = new RemoteConnectionLogic(GlobalVar.TahunAnggaran, 3);

            GlobalVar.CONNECTION_ASET = rcLogic.GetByJenis(2, GlobalVar.JENIS_KONEKSI);
            if (GlobalVar.CONNECTION_ASET != null)
                GlobalVar.CONNECTION_ASET.Decrypt();
            else
            {
                //MessageBox.Show("Kondeksi ke data Aset gagal.");

            }
            GlobalVar.ProfileRekening = oProfileRekeningLogic.GetByID(1);
            if (GlobalVar.ProfileRekening == null)
            {
                MessageBox.Show("Kode Rekening akan menggunakan 7 digit. Jika berbeda silakan hubungi admin");
                GlobalVar.ProfileRekening = new ProfileRekening();
                GlobalVar.ProfileRekening.Kode1 = 1;
                GlobalVar.ProfileRekening.Kode2 = 1;
                GlobalVar.ProfileRekening.Kode3 = 1;
                GlobalVar.ProfileRekening.Kode4 = 2;
                GlobalVar.ProfileRekening.Kode5 = 2;
                oProfileRekeningLogic.Simpan(GlobalVar.ProfileRekening);
                GlobalVar.ProfileRekening = oProfileRekeningLogic.GetByID(1);

            }


         


            Thread t = new Thread(new ThreadStart(InitialLoadDataMaster));
            t.IsBackground = true;

            t.Start();










        }
        public void StartForm()
        {
            try
            {
                progressBar1.Visible = true;
            }
            catch (Exception)
            {

            }
        }
        private void InitialLoadDataMaster()
        {
            try
            {
                int skpdPengguna = 0;
                PemdaLogic oPemdaLogic = new PemdaLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gPemda = oPemdaLogic.Get();


                SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gListSKPD = oSKPDLogic.Get(GlobalVar.TahunAnggaran);

                if (GlobalVar.Pengguna.SKPD > 0)
                {
                    SKPD oSKPD = new SKPD();
                    oSKPD = GlobalVar.gListSKPD.FirstOrDefault(x => x.ID == GlobalVar.Pengguna.SKPD);

                    GlobalVar.gListSKPD.Clear();
                    GlobalVar.gListSKPD.Add(oSKPD);
                }

                // Urusan Dinas
                UrusanDinasLogic oDinasLogic = new UrusanDinasLogic(GlobalVar.TahunAnggaran, 2);
                GlobalVar.gListUrusanDinas = oDinasLogic.Get();


                //Program
                TProgramAPBDLogic oProgramLogic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gListProgram = new List<TProgramAPBD>();
                skpdPengguna = GlobalVar.Pengguna.SKPD;
                GlobalVar.gListProgram = oProgramLogic.Get(GlobalVar.TahunAnggaran);


                if (GlobalVar.Pengguna.SKPD > 0)
                {
                    GlobalVar.gListProgram = GlobalVar.gListProgram.FindAll(x => x.IDDinas == GlobalVar.Pengguna.SKPD);
                }
                // Kegiatan 
                GlobalVar.gListKegiatan = new List<TKegiatanAPBD>();
                TKegiatanAPBDLogic oTKegiatanLgic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gListKegiatan = oTKegiatanLgic.Get(GlobalVar.TahunAnggaran);

                //RekeningLogic oRekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran);
                //GlobalVar.gListRekening = new List<Rekening>();
                // GlobalVar.gListRekening = oRekeningLogic.Get();

                //Banks
                GlobalVar.gLstBanks = new List<DaftarBank>();
                DaftarBankLogic oLogic = new DaftarBankLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gLstBanks = oLogic.GetBanks();

                // Otoritas
                OtoritasLogic oOtoritasLogic = new OtoritasLogic();
                GlobalVar.gListOtoritas = oOtoritasLogic.GetListOtoritas(GlobalVar.Pengguna.Kelompok); 


                //AnggaranRekening;
                GlobalVar.gListRekeningAnggaran = new List<TAnggaranRekening>();
                TAnggaranRekeningLogic oAnggaranRekeningLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gListRekeningAnggaran = oAnggaranRekeningLogic.Get(GlobalVar.TahunAnggaran);

                GlobalVar.gListRefPajak = new List<RefPajak>();
                RefPajakLogic oRefPajakLogic = new RefPajakLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gListRefPajak = oRefPajakLogic.Get();
            
                Invoke(new UpdateUIDelegate(UpdateUI), new object[] { true });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Keslaahan Initial Data");
            }

        }
        private void UpdateUI(bool IsDataLoaded)
        {
            if (IsDataLoaded)
            {

                threadSplash.Abort();
                m_bOK = true;
                this.Hide();

                

            }
            else
            {
                progressBar1.Visible = true;
   

            }
        }
        private void cmdBatal_Click(object sender, EventArgs e)
        {
            m_bOK = false;
            this.Hide();
        }

        private void frmLoginBaru_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.IsClosed = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmTestPDF fTestPDF = new frmTestPDF();
            fTestPDF.ShowDialog();// Show();
        }

        private void cmbTahunAnggaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTahunAnggaran.Text != "2024"){
                MessageBox.Show("Only for 2024");
                return;
            }
            lblTahun.Text = cmbTahunAnggaran.Text.Trim();

        }

        private async void cmdRefresh_Click(object sender, EventArgs e)
        {
            await CheckInternetAsync();
        }

        private void Pelayanan_Click(object sender, EventArgs e)
        {
            txtUserID.Text="pl";
            txtPassword.Text = "Puyi54321";
        }

        private void cmdVerifikator_Click(object sender, EventArgs e)
        {
            txtUserID.Text = "v";
            txtPassword.Text = "Puyi54321";
        }

        private void cmdSP2DOnlie_Click(object sender, EventArgs e)
        {
         
        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            txtUserID.Text = "c";
            txtPassword.Text = "Puyi54321";
        }

    }
}
