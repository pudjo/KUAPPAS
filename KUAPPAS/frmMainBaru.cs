using System;
using System.Windows.Forms;
using KUAPPAS.Bendahara;
using KUAPPAS.Akunting;
using KUAPPAS.SP2DOnline;
using KUAPPAS.Anggaran;
using KUAPPAS.KasDaerah;
using System.Threading;
using BP;
using DTO;

using System.Diagnostics;
using System.Reflection;
using DTO.Akuntansi;
using KUAPPAS.External;
using Menu;
using Z80NavBarControl.Z80NavBar.Themes;
using System.Web.Services.Description;
using System.Windows;

namespace KUAPPAS
{
    public partial class frmMainBaru : Form
    {
        private int childFormNumber = 0;
        public delegate void UpdateUIDelegate(bool IsDataLoaded);
        frmLoginBaru fLogin;
        Thread threadSplash;
        public frmMainBaru()
        {
            InitializeComponent();
            z80_Navigation1.SelectedItem += z80_Navigation1_SelectedItem;
            z80_Navigation1.Initialize(new MenuItems().daftarMenu, new ThemeSelector(Theme.RoyalBlue).CurrentTheme);

            z80_Navigation1.ItemSelect(1); // Default item 
            /*
                        WebClient webClient = new WebClient();
                        var client = new WebClient();
                        if (!webClient.DownloadString("link to web host/Version.txt").Contains("1.0.0"))
                        {
                            if (MessageBox.Show("A new update is available! Do you want to download it?", "Demo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                try
                                {
                                    if (File.Exists(@".\MyAppSetup.msi")) { File.Delete(@".\MyAppSetup.msi"); }
                                    client.DownloadFile("link to web host/MyAppSetup.zip", @"MyAppSetup.zip");
                                    string zipPath = @".\MyAppSetup.zip";
                                    string extractPath = @".\";
                                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                                    Process process = new Process();
                                    process.StartInfo.FileName = "msiexec.exe";
                                    process.StartInfo.Arguments = string.Format("/i MyAppSetup.msi");
                                    this.Close();
                                    process.Start();
                                }
                                catch
                                {
                                }
                            }
                        }*/
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }



        private void mnuSP2DOnline_Click(object sender, EventArgs e)
        {
            frmSP2DOnline fSP2DOnline = new frmSP2DOnline();
            DisplayFormOnTab(fSP2DOnline, "SP2DOnline", "SP2D Online");
        }

        private void mnuPembuatanSPD_Click(object sender, EventArgs e)
        {
            frmSPDKepmen900 fSPD = new frmSPDKepmen900();
            DisplayFormOnTab(fSPD, "SPD", "SPD");

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ChildForm childForm in this.MdiChildren)
            {
                //Check for its corresponding MDI child form
                //if (childForm.TabPag.Equals(tabControl1.SelectedTab))
                //{
                //    //Activate the MDI child form
                //    childForm.Select();
                //}
            }
        }

        private void mnuSPP_Click(object sender, EventArgs e)
        {
            frmListSPP f = new frmListSPP(0);
            DisplayFormOnTab(f, "SPP", "SPP");
        }
        private void DisplayFormOnTab(ChildForm f, string pTag, string pText)
        {

            //bool bFound = false;
            //tabControl1.Visible = true;

            //foreach (ChildForm cForm in this.MdiChildren)
            //{
            //    if (cForm.Tag == pTag)
            //    {
            //        //Activate the MDI child form
            //        cForm.Select();
            //        bFound = true;
            //    }
            //}
            //if (bFound == false)
            //{
            //    f.Text = pText;
            //    f.MdiParent = this;
            //    f.Tag = pTag;
            //    f.TabCtrl = tabControl1;
            //    TabPage tp = new TabPage();
            //    tp.Parent = tabControl1;
            //    tp.Text = f.Text;
            //    tp.Show();

            //    f.TabPag = tp;
            //    f.Show();
            //    tabControl1.SelectedTab = tp;
            //}
        }

        private void mnuInputKontrak_Click(object sender, EventArgs e)
        {

            frmListKontrak f = new frmListKontrak();
            DisplayFormOnTab(f, "Kontrak", "Kontrak");


        }

        private void mnuSPJPanjar_Click(object sender, EventArgs e)
        {
            frmListPengeluaran fBPK = new frmListPengeluaran();
            DisplayFormOnTab(fBPK, "BPK", "Panjar/SPJ");

        }

        private void mnuTransaksiBank_Click(object sender, EventArgs e)
        {
            frmListTransaksiBank fTB = new frmListTransaksiBank();
            DisplayFormOnTab(fTB, "TB", "Transaksi Bank");
        }

        private void pengembalianBelanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPengembalianBelanja fPB = new frmListPengembalianBelanja();
            DisplayFormOnTab(fPB, "CP", "Pengembalian Belanja");
        }

        private void mnuSetorPajak_Click(object sender, EventArgs e)
        {
            frmListPenyetoranPajak fPP = new frmListPenyetoranPajak();
            DisplayFormOnTab(fPP, "Pajak", "Setor Pajak");


        }

        private void mnuKoreksiBelanja_Click(object sender, EventArgs e)
        {
            frmListKoreksi fPP = new frmListKoreksi();
            DisplayFormOnTab(fPP, "Koreksi", "Koreksi Belanja");

        }

        private void mnuPenerimaan_Click(object sender, EventArgs e)
        {
            frmListPenerimaan fPenerimaan = new frmListPenerimaan();
            DisplayFormOnTab(fPenerimaan, "Penerimaan", "Daftar Penerimaan");
        }

        private void mnuPenyetoranKeKasda_Click(object sender, EventArgs e)
        {

            frmListPenyetoranKasda fSetor = new frmListPenyetoranKasda();
            DisplayFormOnTab(fSetor, "Penyetoran", "Daftar Penyetoran ke Kasda");


        }

        private void mnuBKUPrngrluaran_Click(object sender, EventArgs e)
        {
            frmBKU fBKU = new frmBKU();
            fBKU.JenisBendahara = 2;
            DisplayFormOnTab(fBKU, "bkubk", "B K U ");
        }

        private void mnuSPJFUngsional_Click(object sender, EventArgs e)
        {
            //frmSPJFungsional fBKU = new frmSPJFungsional();
            //DisplayFormOnTab(fBKU, "spjbk", "SPJ Fungsional");
        }

        private void mnuSKPD_Click(object sender, EventArgs e)
        {
            frmListSKRSKPD fSKR = new frmListSKRSKPD();
            DisplayFormOnTab(fSKR, "skrskpd", "SKR/SKPD");
        }

        private void perbendaharaanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnuLRA_Click(object sender, EventArgs e)
        {
            frmLRA fLRA = new frmLRA();
            DisplayFormOnTab(fLRA, "LRA", "LRA");

        }

        private void mnuLaporanSP2DOnline_Click(object sender, EventArgs e)
        {

            frmReportSP2DOnline fReportSP2DOnline = new frmReportSP2DOnline();
            DisplayFormOnTab(fReportSP2DOnline, "ReportSP2DOnline", "Laporan SP2D Online");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListPejabat fListPejabat = new frmListPejabat();
            DisplayFormOnTab(fListPejabat, "ListPejabat", "Daftar Pejabat");


        }

        private void anggaranToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAnggaran fAnggaran = new frmAnggaran();
//            DisplayFormOnTab(fAnggaran, "Anggaran", "Anggaran");
        }

        private void mnuSetTahapInut_Click(object sender, EventArgs e)
        {
            frmSetStatusInput fStatusInput = new frmSetStatusInput();
            fStatusInput.Show();
        }
        private void InitialLoadDataMaster()
        {


        }

        private void frmMainBaru_Load(object sender, EventArgs e)
        {



            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.toolStripStatusLabel.Text = "Version: " + version.Major.ToString() + "." + version.Minor.ToString() + "." + version.Build.ToString();
            fLogin = new frmLoginBaru(this);
            fLogin.ShowDialog();

            if (fLogin.IsOK == false)
            {
                MessageBox.Show("Tidak melanjutkan menjalankan aplikasi. Aplikasi akan ditutup. ");
                this.Close();

                return;
            }
            //ctrlHeaderMain1.SetTahun(GlobalVar.TahunAnggaran);

           // this.menuStrip.Visible = true;

            if (GlobalVar.Pengguna.SKPD > 0)
            {
                mnuSetTahapInut.Visible = false;
            }

            menuSP2DOnline.Visible = false;
            
            menuMaster.Visible = false;
            menuAnggaran.Visible = false;
            menuSPD.Visible = false;
            menuBendaharaPengeluaran.Visible = false;
            menuPenerimaan.Visible = false;
            menuLaporanBendaharaPengeluaran.Visible = false;
            menuLaporanBendaharaPenerimaan.Visible = false;
            menuBUD.Visible = false;
            mnuKasda.Visible = false;
            menuLaporanKasda.Visible = false;

            menuPPK.Visible = false;
            menuLaporanBUD.Visible = false;
            menuSettingPengguna.Visible = false;
            menuProsesAkuntansi.Visible = false;
            menuPelaporanAkuntansi.Visible = false;
            menuAset.Visible = false;

            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_AUDIT)
            {
                menuMaster.Visible = false;
                menuAnggaran.Visible = false ;
                menuSPD.Visible = false;
                menuBendaharaPengeluaran.Visible = true ;
                menuPenerimaan.Visible = true ;
                menuLaporanBendaharaPengeluaran.Visible = true ;
                menuLaporanBendaharaPenerimaan.Visible = true;
                menuBUD.Visible = false;
                mnuKasda.Visible = false;
                menuLaporanKasda.Visible = false;

                menuPPK.Visible = true;
                //menuLaporanBUD.Visible = false;
                menuSettingPengguna.Visible = false;
                menuProsesAkuntansi.Visible = true ;
                menuPelaporanAkuntansi.Visible = true;
            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_SKPD ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_PEMBANTU_SKPD)
            {
                menuSPD.Visible = false;
                menuPenerimaan.Visible = false;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuBUD.Visible = false;
                mnuKasda.Visible = false;
                menuPelaporanAkuntansi.Visible = false;
                menuBendaharaPengeluaran.Visible = true;
                menuBendaharaPengeluaran.Visible = true;
                menuPPK.Visible = false;
                menuBUD.Visible = false;
                menuSP2DOnline.Visible = false;
                menuBendaharaPengeluaran.Visible = true;
                menuLaporanBendaharaPengeluaran.Visible = true;
                menuAnggaran.Visible = true;
                menuSettingPengguna.Visible = true;
                menuMaster.Visible = true;
                mnuSettingPengguna.Text = "Ubah Password";

                BukaDashboard();
            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENERIMAAN_SKPD ||
                GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENERIMAAN_PPKD
                )
            {
                menuSPD.Visible = false;
                menuPenerimaan.Visible = true;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuBUD.Visible = false;
                mnuKasda.Visible = false;
                menuPelaporanAkuntansi.Visible = false;
                menuBendaharaPengeluaran.Visible = false;
                menuBendaharaPengeluaran.Visible = false;
                menuPPK.Visible = false;
                menuBUD.Visible = false;
                menuSP2DOnline.Visible = false;
                menuPenerimaan.Visible = true;
                menuLaporanBendaharaPenerimaan.Visible = true;
                menuMaster.Visible = true;

                menuSettingPengguna.Visible = true;
                mnuSettingPengguna.Text = "Ubah Password";



            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_KASDA)
            {

                menuPenerimaan.Visible = false;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuBUD.Visible = false;

                menuPelaporanAkuntansi.Visible = false;
                menuBendaharaPengeluaran.Visible = false;
                menuMaster.Visible = false;
                menuAnggaran.Visible = false;
                menuSPD.Visible = false;
                menuLaporanBendaharaPengeluaran.Visible = false;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuBUD.Visible = false;
                menuPelaporanAkuntansi.Visible = false;
                menuPPK.Visible = false;
                menuPPK.Visible = false;
                menuBUD.Visible = false;
                menuSP2DOnline.Visible = false;
                mnuKasda.Visible = true;
                menuLaporanKasda.Visible = true;
                mnuSettingPengguna.Text = "Ubah Password";
                menuSettingPengguna.Visible = true;
                mnuKasda.Visible = true;

            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_PPK)
            {
                menuSPD.Visible = false;
                menuPenerimaan.Visible = false;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuBUD.Visible = false;
                mnuKasda.Visible = false;
                menuPelaporanAkuntansi.Visible = false;
                //menuBendaharaPEngeluaran.Visible = true;
                menuBendaharaPengeluaran.Visible = false;

                menuPPK.Visible = true;

                menuBUD.Visible = false;
                menuAnggaran.Visible = true;
                menuPenerimaan.Visible = true;
                menuPengeluaran.Visible = true;
                menuMaster.Visible = false;
                menuPengeluaran.Visible = false;
                menuSP2DOnline.Visible = false;
                mnuSettingPengguna.Text = "Ubah Password";
                menuSettingPengguna.Visible = true;
                menuProsesAkuntansi.Visible = true;
                menuLaporanBendaharaPengeluaran.Visible = true;
                menuLaporanBendaharaPengeluaran.Text = "Laporan Bendahara Pengeluaran ";
                menuLaporanBendaharaPenerimaan.Visible = true;
                menuLaporanBendaharaPenerimaan.Text = "Laporan Bendahara Penerimaan.";

                menuPelaporanAkuntansi.Visible = true;

            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUD)
            {
                menuSPD.Visible = true;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuLaporanBendaharaPengeluaran.Visible = false;

                menuAnggaran.Visible = false;

                menuPenerimaan.Visible = false;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuBUD.Visible = false;
                mnuKasda.Visible = false;
                menuPelaporanAkuntansi.Visible = false;
                menuBendaharaPengeluaran.Visible = false;
                menuPPK.Visible = false;
                menuMaster.Visible = false;
                menuPengeluaran.Visible = false;

                menuBUD.Visible = true;
                menuLaporanBUD.Visible = true;
                menuSP2DOnline.Visible = false;
                mnuSettingPengguna.Text = "Ubah Password";
                menuSettingPengguna.Visible = true;

            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_MANANG)
            {
                menuSPD.Visible = true;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuLaporanBendaharaPengeluaran.Visible = false;
                menuBendaharaPengeluaran.Visible = false;
                menuBUD.Visible = false;
                menuSPD.Visible = false;
                menuAnggaran.Visible = false;
                menuPenerimaan.Visible = false;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuBUD.Visible = false;
                mnuKasda.Visible = false;
                menuPelaporanAkuntansi.Visible = false;
                menuBendaharaPengeluaran.Visible = false;
                menuPPK.Visible = false;

                menuAnggaran.Visible = true;
                menuMaster.Visible = false;
                menuPengeluaran.Visible = false;
                menuSP2DOnline.Visible = false;
                mnuSettingPengguna.Text = "Ubah Password";
                menuSettingPengguna.Visible = true;
                if (GlobalVar.Pengguna.Kelompok != 0 || GlobalVar.Pengguna.Kelompok != 1000)
                {
                    mnuImportSIPD.Visible = false;
                }

            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_AKUNTANSI)
            {
                menuSPD.Visible = true;
                menuLaporanBendaharaPenerimaan.Visible = true;
                menuLaporanBendaharaPenerimaan.Text = "Laporan Bendahara Penerimaan.";
                menuLaporanBendaharaPengeluaran.Visible = true;
                menuLaporanBendaharaPengeluaran.Text = "Laporan Bendahara Pengeluaran ";
                menuBendaharaPengeluaran.Visible = false;
                menuBUD.Visible = false;
                menuSPD.Visible = false;
                menuAnggaran.Visible = false;
                menuPenerimaan.Visible = false;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuBUD.Visible = false;
                mnuKasda.Visible = false;
                menuPelaporanAkuntansi.Visible = true;
                menuBendaharaPengeluaran.Visible = false;
                menuPPK.Visible = false;

                menuAnggaran.Visible = true;

                menuMaster.Visible = false;
                menuPengeluaran.Visible = false;
                menuSP2DOnline.Visible = false;
                mnuSettingPengguna.Text = "Ubah Password";
                menuSettingPengguna.Visible = true;
                menuProsesAkuntansi.Visible = true;
                menuPelaporanAkuntansi.Visible = true;
                if (GlobalVar.Pengguna.Kelompok != 0 || GlobalVar.Pengguna.Kelompok != 1000)
                {
                    mnuImportSIPD.Visible = false;
                }

            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_SUPPORT)
            {
                menuSP2DOnline.Visible = true;
                menuMaster.Visible = true;
                menuAnggaran.Visible = true;
                menuSPD.Visible = true;
                menuBendaharaPengeluaran.Visible = true;
                menuPenerimaan.Visible = true;
                menuLaporanBendaharaPengeluaran.Visible = true;
                menuLaporanBendaharaPenerimaan.Visible = true;
                menuBUD.Visible = true;
                mnuKasda.Visible = true;
                menuLaporanKasda.Visible = true;
                menuPelaporanAkuntansi.Visible = true;
                menuPPK.Visible = true;
                menuLaporanBUD.Visible = true;
                menuSettingPengguna.Visible = true;
                menuProsesAkuntansi.Visible = true;
                BukaDashboard();
            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_ADMIN
                )
            {
                menuSettingPengguna.Visible = true;
                menuMaster.Visible = true;

            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUDSP2DONLINE)
            {
                menuSP2DOnline.Visible = true;
                menuSettingPengguna.Visible = true;
                mnuSettingPengguna.Text = "Ubah Password";
            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUDCETAKSP2D)
            {
                menuSPD.Visible = false;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuLaporanBendaharaPengeluaran.Visible = false;

                menuAnggaran.Visible = false;

                menuPenerimaan.Visible = false;
                menuLaporanBendaharaPenerimaan.Visible = false;
                menuBUD.Visible = false;
                mnuKasda.Visible = false;
                menuPelaporanAkuntansi.Visible = false;
                menuBendaharaPengeluaran.Visible = false;
                menuPPK.Visible = false;
                menuMaster.Visible = false;
                menuPengeluaran.Visible = false;

                menuBUD.Visible = true;
                // menuLaporanBUD.Visible = true;
                menuSP2DOnline.Visible = false;
                menuVerifikasi.Text = "Cetak SP2D";
                mnuVerifikasi.Text = "Cetak SP2D";
                menuVerifikasi.Visible = true;
                mnuSettingPengguna.Text = "Ubah Password";
                menuSettingPengguna.Visible = true;
            }

            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_ASET)
            {
                menuAset.Visible = true;
            }

            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUDTERIMASPM)
            {
                frmPenerimaanSPM fTerimaSPM = new frmPenerimaanSPM();
                fTerimaSPM.Jenis = 1;
                fTerimaSPM.ShowDialog();


            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUDVERIFIKASISPM)
            {
                menuVerifikasi.Visible = true;
                menuSPD.Visible = true;
                menuSettingPengguna.Visible = true;
                menuMaster.Visible = true;
                mnuSettingPengguna.Text = "Ubah Password";


            }



        }

        private void mnuAnggaranKas_Click(object sender, EventArgs e)
        {

            frmAnggaranKas fAnggaran = new frmAnggaranKas();
           // DisplayFormOnTab(fAnggaran, "Anggaran Kas", "Anggaran Kas");


        }

        private void mnuSPDVersiLama_Click(object sender, EventArgs e)
        {
            //frmSPDKempem50 fSPDLama = new frmSPDKempem50();
            //DisplayFormOnTab(fSPDLama, "SPD Lama", "SPD Lama");   
        }

        private void mnuBAST_Click(object sender, EventArgs e)
        {
            frmListBAST fListBAST = new frmListBAST();
            DisplayFormOnTab(fListBAST, "BAST", "BAST");
        }

        private void mnuPPK_Click(object sender, EventArgs e)
        {
            frmListSPP f = new frmListSPP(1);
            DisplayFormOnTab(f, "SPM", "SPM");
        }

        private void verifikasiDanPenerbitanSP2DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListSPP f = new frmListSPP(3);
            DisplayFormOnTab(f, "InputSP2D", "InputSP2D");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void mnuJurnalaTrx_Click(object sender, EventArgs e)
        {

        }

        private void menuPengeluaran_Click(object sender, EventArgs e)
        {
            frmListPengeluaran f = new frmListPengeluaran();
            DisplayFormOnTab(f, "Panjar/SPJ", "Panjar dan SPJ");
        }

        private void mnuCatatTanggalCair_Click(object sender, EventArgs e)
        {
            frmCatatTanggalCair f = new frmCatatTanggalCair();
            DisplayFormOnTab(f, "Catat Tanggal Cair", "Catat Tanggal Cair");
        }

        private void mnuPemerintahDaerah_Click(object sender, EventArgs e)
        {
            frmSettingPemda fSettingPemda = new frmSettingPemda();
            fSettingPemda.MdiParent = this;
            fSettingPemda.Show();
        }

        private void mnuRegisterSPP_Click(object sender, EventArgs e)
        {
            frmRegisterSPP f = new frmRegisterSPP(0);

            DisplayFormOnTab(f, "Register SPP", "Register SPP");
        }

        private void mnuRegisterSPPSPMSP2D_Click(object sender, EventArgs e)
        {
            frmRegisterSPP f = new frmRegisterSPP(2);

            DisplayFormOnTab(f, "Register SP2D(Bendahara)", "Register SP2D(Bendahara)");
        }

        private void mnuPenutupanKas_Click(object sender, EventArgs e)
        {
            frmPengeluaranLaporanPenutupanKas f = new frmPengeluaranLaporanPenutupanKas();

            DisplayFormOnTab(f, "Penutupan Kas", "Penutupan Kas");
        }

        private void mnuImportSIPD_Click(object sender, EventArgs e)
        {
            frmImportAPBD fIMport = new frmImportAPBD();
            fIMport.Mode = 2;
            fIMport.Show();

        }

        private void oPDToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmListSKRSKPD fListOPD = new frmListSKRSKPD();
            DisplayFormOnTab(fListOPD, "daftar OPD", "daftar OPD");

        }

        private void mnuRegisterSPD_Click(object sender, EventArgs e)
        {
            frmRegisterSPD fSPD = new frmRegisterSPD();
            fSPD.Show();
        }

        private void mnuPejabatDinas_Click(object sender, EventArgs e)
        {
            frmListPejabat fListPejabat = new frmListPejabat();
            DisplayFormOnTab(fListPejabat, "ListPejabat", "Daftar Pejabat");
        }

        private void mnuSettingPengguna_Click(object sender, EventArgs e)
        {
            if (GlobalVar.Pengguna.Kelompok == 0 || GlobalVar.Pengguna.Kelompok == 1000)
            {

                frmPengguna fListPenggun = new frmPengguna();
                DisplayFormOnTab(fListPenggun, "List Pengguna", "Daftar Pengguna");

            }
            else
            {
                frmBuatPassword fGantiPassword = new frmBuatPassword();
                fGantiPassword.SetPengguna(GlobalVar.Pengguna);
                fGantiPassword.ShowDialog();
                if (fGantiPassword.IsOK() == false)
                {
                    return;
                }

            }
        }

        private void mnuKOdeRekening_Click(object sender, EventArgs e)
        {
            frmRekening90 fRekening = new frmRekening90();

            DisplayFormOnTab(fRekening, "Kode Rekening", "Kode Rekening");

        }

        private void mnuPejabatBUD_Click(object sender, EventArgs e)
        {
            frmListPejabat fListPejabat = new frmListPejabat();
            DisplayFormOnTab(fListPejabat, "ListPejabat", "Daftar Pejabat");

        }

        private void mnuPejabatSPD_Click(object sender, EventArgs e)
        {
            frmListPejabat fListPejabat = new frmListPejabat();
            DisplayFormOnTab(fListPejabat, "ListPejabat", "Daftar Pejabat");
        }

        private void mnuCatatBKU_Click(object sender, EventArgs e)
        {

            frmListSPP f = new frmListSPP(4);
            DisplayFormOnTab(f, "Catat BKU SP2D", "Catat BKU SP2D");
        }

        private void mnuSettingPPTK_Click(object sender, EventArgs e)
        {
            frmListPPTK f = new frmListPPTK();
            DisplayFormOnTab(f, "Setting PPTK", "Setting PPTK");
        }

        private void mnuSPJFungsional_Click_1(object sender, EventArgs e)
        {
            frmSPJFungsional f = new frmSPJFungsional(1);
            DisplayFormOnTab(f, "SPJ Fungsional", "SPJ Fungsional");

        }

        private void nmnuRegisterSP2D_Click(object sender, EventArgs e)
        {

        }

        private void mnuRegisterSP2dBUD_Click(object sender, EventArgs e)
        {
            frmBUDRegisterSP2D f = new frmBUDRegisterSP2D();
            DisplayFormOnTab(f, "Register SP2D (BUD)", "Register SP2D (BUD)");
        }

        private void bukuBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBukuKas f = new frmBukuKas(1);
            f.JenisBendahara = 2;
            DisplayFormOnTab(f, "Buku Kas Bank", "Buku Kas Bank");

        }

        private void mnuBukuIkasBank_Click(object sender, EventArgs e)
        {
            frmBukuKas f = new frmBukuKas(2);
            f.JenisBendahara = 2;
            DisplayFormOnTab(f, "Buku Kas Tunai", "Buku Kas Tunai");
        }

        private void mnuBukuPajak_Click(object sender, EventArgs e)
        {
            frmBukuKas f = new frmBukuKas(3);
            f.JenisBendahara = 2;
            DisplayFormOnTab(f, "Buku Pajak", "Buku Pajak");
        }

        private void mnuBukuPanjar_Click(object sender, EventArgs e)
        {

            frmBukuKas f = new frmBukuKas(4);
            f.JenisBendahara = 2;
            DisplayFormOnTab(f, "Buku Panjar", "Buku Panjar");
        }

        private void mnuBatasUP_Click(object sender, EventArgs e)
        {
            frmbataUP f = new frmbataUP();
            DisplayFormOnTab(f, "Batas UP", "Batas UP");
        }

        private void mnuRekapAnggaranKas_Click(object sender, EventArgs e)
        {

            frmAnggaranKasRekap fRekapAK = new frmAnggaranKasRekap();
            DisplayFormOnTab(fRekapAK, "Rekap AK", "Rekap AK");
        }

        private void mnuRegisterSP2DTerbitCair_Click(object sender, EventArgs e)
        {
            frmBUDRegisterSP2D f = new frmBUDRegisterSP2D();
            DisplayFormOnTab(f, "Register SP2D Terbit/Cair", "Register SP2D Terbit/Cair");
        }

        private void mnuRegSPPSPMSP2DBUD_Click(object sender, EventArgs e)
        {
            frmRegisterSPP f = new frmRegisterSPP(2);

            DisplayFormOnTab(f, "Register SPP/SPM/SP2D", "Register SPP/SPM/SP2D");
        }

        private void sPJAdministratifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSPJFungsional f = new frmSPJFungsional(2);
            DisplayFormOnTab(f, "SPJ Administratif", "SPJ Administratif");
        }

        private void mnuKartKendali_Click(object sender, EventArgs e)
        {
            frmPengeluaranLapKartuKendali f = new frmPengeluaranLapKartuKendali();
            DisplayFormOnTab(f, "Kartu Kendali", "Kartu Kendali");
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //string selectedTag = "";
            //if (tabControl1.SelectedTab != null)
            //{
            //    selectedTag = tabControl1.SelectedTab.Text;
            //    foreach (ChildForm cForm in this.MdiChildren)
            //    {
            //        if (cForm.TabPag.Text == selectedTag)
            //        {
            //            cForm.Select();
            //        }
            //    }
            //}
        }

        private void mnuBKUPenerimaan_Click(object sender, EventArgs e)
        {
            frmBKU fBKU = new frmBKU();
            fBKU.JenisBendahara = 1;
            DisplayFormOnTab(fBKU, "bkubt", "BKU ");
        }

        private void mnuBKPP_Click(object sender, EventArgs e)
        {
            frmBKU fBKU = new frmBKU();
            fBKU.JenisBendahara = 0;
            DisplayFormOnTab(fBKU, "bkuKasda", "BKPP");
        }

        private void ctrlHeaderMain1_Load(object sender, EventArgs e)
        {

        }

        private void mnuRegisterSP2DKasda_Click(object sender, EventArgs e)
        {
            frmBUDRegisterSP2D f = new frmBUDRegisterSP2D();
            DisplayFormOnTab(f, "Register SP2D", "Register SP2D");
        }

        private void mnuSaldoAwal_Click(object sender, EventArgs e)
        {
            frmSaldoAwalBendahara fSaldoAwal = new frmSaldoAwalBendahara();
            fSaldoAwal.ShowDialog();

        }

        private void mnuSPJPenerimaan_Click(object sender, EventArgs e)
        {
            // https://www.facebook.com/reel/930891985305584
            frmSPJFungsionalPenerimaan fSPJPenerimaan = new frmSPJFungsionalPenerimaan();
            DisplayFormOnTab(fSPJPenerimaan, "SPJ Fungsional Penerimaan", "SPJ Fungsional Penerimaan");
        }

        private void mnuPertanggungJawabanAdministratif_Click(object sender, EventArgs e)
        {
            frmSPJAdministratifPenerimaan fSPJAdministratifPenerimaan = new frmSPJAdministratifPenerimaan();
            DisplayFormOnTab(fSPJAdministratifPenerimaan, "SPJ Administratif Penerimaan", "SPJ Administratif Penerimaan");
        }

        private void mnuPenerimaanPenyetoran_Click(object sender, EventArgs e)
        {
            frmPenerimaanPenerimaandanPenyetoran fPenerimaanPenyetoran = new frmPenerimaanPenerimaandanPenyetoran();
            DisplayFormOnTab(fPenerimaanPenyetoran, "Terima Setor", "Terima Setor");
        }

        private void rincianObjekToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmPengeluaranBukuRincianObjerk fRincianObjek = new frmPengeluaranBukuRincianObjerk();
            DisplayFormOnTab(fRincianObjek, "Rincian Objek", "Rincian Objek");


        }

        private void laporanOperasionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLO fLO = new frmLO();
            DisplayFormOnTab(fLO, "Laporan Operasional", "Laporan Operasional");
        }

        private void mnuJurnalTrx_Click(object sender, EventArgs e)
        {
            frmJurnalTrx fJurnalTrx = new frmJurnalTrx();
            DisplayFormOnTab(fJurnalTrx, "Jurnal Transaksi", "Jurnal Transaksi");
        }

        private void mnuNeracaAwal_Click(object sender, EventArgs e)
        {


        }

        private void mnuLogOut_Click(object sender, EventArgs e)
        {
            fLogin = new frmLoginBaru(this);
            fLogin.ShowDialog();

            if (fLogin.IsOK == false)
            {
                MessageBox.Show("Tidak melanjutkan menjalankan aplikasi. Aplikasi akan ditutup. ");
                this.Close();

                return;
            }
          //  ctrlHeaderMain1.SetTahun(GlobalVar.TahunAnggaran);
        }

        private void mnuLPJUP_Click(object sender, EventArgs e)
        {
            frmLPJUP fPJUP = new frmLPJUP();
            DisplayFormOnTab(fPJUP, "LPJ UP", "LPJ UP");
        }

        private void mnuUnitKerja_Click(object sender, EventArgs e)
        {
            frmListUK fListUK = new frmListUK();
            DisplayFormOnTab(fListUK, "Unit Kerja", "Unit Kerja");
        }

        private void mnuImportPendapatan_Click(object sender, EventArgs e)
        {
            frmImportPendapatan fImport = new frmImportPendapatan();
            fImport.Show();
        }

        private void mnuRegisteSTS_Click(object sender, EventArgs e)
        {
            frmRegisterSTS fListUK = new frmRegisterSTS();
            DisplayFormOnTab(fListUK, "Register STS", "Register STS");
        }

        private void mnuPerdaRealisasi_Click(object sender, EventArgs e)
        {
            frmCetakPerda fCetakPerda = new frmCetakPerda();
            fCetakPerda.Show();
        }

        private void mnuBKUBendaharaPengeluaran_Click(object sender, EventArgs e)
        {
            frmBKU fBKU = new frmBKU();
            fBKU.JenisBendahara = 2;
            DisplayFormOnTab(fBKU, "bkubk", "B K U ");
        }

        private void mnuInputAnggaran_Click(object sender, EventArgs e)
        {

            frmInputPlafonPerRekening fInputAnggaran = new frmInputPlafonPerRekening(1);
            DisplayFormOnTab(fInputAnggaran, "Input Anggaran", "InputAnggaran");
        }

        private void mnuRekapGaji_Click(object sender, EventArgs e)
        {
            frmRekapGaji fRekapGaji = new frmRekapGaji();
            DisplayFormOnTab(fRekapGaji, "Rekap Gaji", "Rekap Gaji");
        }

        private void mnuAsetLRA_Click(object sender, EventArgs e)
        {
            frmLRA fLRA = new frmLRA();
            DisplayFormOnTab(fLRA, "LRA", "LRA");
        }

        private void mnuAsetFungsional_Click(object sender, EventArgs e)
        {
            frmSPJFungsional f = new frmSPJFungsional(1);
            DisplayFormOnTab(f, "SPJ Fungsional", "SPJ Fungsional");
        }

        private void mnuPenyetoranPajakBaru_Click(object sender, EventArgs e)
        {
            frmPajakDanSetorannya fPP = new frmPajakDanSetorannya();
            DisplayFormOnTab(fPP, "Pajak", "Setor Pajak");
        }

        private void mnuPosting_Click(object sender, EventArgs e)
        {
            frmPosting fPosting = new frmPosting();
            fPosting.ShowDialog();

        }

        private void mnuBank_Click(object sender, EventArgs e)
        {
            frmBank fBank = new frmBank();
            fBank.ShowDialog();
        }
        private void BukaDashboard()
        {
            //if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_SKPD ||
            //    GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_PEMBANTU_SKPD ||
            //    GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_SUPPORT
            //    )
            //{

            //    frmDashBoardBendahara fPP = new frmDashBoardBendahara();
            //    DisplayFormOnTab(fPP, "DashBoard Pengeluaran", "DashPengeluaran");
            //}

        }

        private void mnuNeracaAwal_Click_1(object sender, EventArgs e)
        {
            frmInputNeracaAwal fNeracaAwal = new frmInputNeracaAwal();
            DisplayFormOnTab(fNeracaAwal, "Neraca Awal", "Neraca Awal");
        }

        private void neracaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNeraca fNeraca = new frmNeraca();
            DisplayFormOnTab(fNeraca, "Neraca", "Neraca");
        }

        private void bukuBesarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBukuBesar fBukuBesar = new frmBukuBesar();
            DisplayFormOnTab(fBukuBesar, "BukuBesar", "BukuBesar");
        }

        private void jurnalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLaporanJurnal fBukuBesar = new frmLaporanJurnal();
            DisplayFormOnTab(fBukuBesar, "Laporan Jurnal", "Laporan Jurnal");

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuProsesAkuntansi_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuJurnalUmum_Click(object sender, EventArgs e)
        {
            frmListJurnal fBukuBesar = new frmListJurnal();
            fBukuBesar.Jenis = JENIS_JURNAL.JENIS_JURNALUMUM;
            fBukuBesar.Sumber = JENIS_SUMBERJURNAL.E_SUMBER_MANUAL;
            DisplayFormOnTab(fBukuBesar, "JurnalUmum", "Jurnal Umum");
        }

        private void mnuJurnalPenyesuaian_Click(object sender, EventArgs e)
        {
            frmListJurnal fBukuBesar = new frmListJurnal();
            fBukuBesar.Jenis = JENIS_JURNAL.JENIS_JURNALPENYESUAIAN;
            fBukuBesar.Sumber = JENIS_SUMBERJURNAL.E_JURNAL_PENYESUAIAN;
            DisplayFormOnTab(fBukuBesar, "JurnalPenyesuaian", "JurnalPenyesuaian");
        }

        private void menuPelaporanAkuntansi_Click(object sender, EventArgs e)
        {

        }

        private void laporanPerubahanEquitasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmLaporanLPE fPE = new frmLaporanLPE();

            DisplayFormOnTab(fPE, "LPE", "LPE");
        }

        private void mnuVerifikasi_Click(object sender, EventArgs e)
        {
            frmListSPP fListSPPE = new frmListSPP(1);

            DisplayFormOnTab(fListSPPE, "Verifikasi", "Verifikasi");
        }

        private void mnuTerimaSP2D_Click(object sender, EventArgs e)
        {
            frmPenerimaanSPM fTerima = new frmPenerimaanSPM();
            fTerima.Jenis = 2;
            fTerima.ShowDialog();
        }

        private void menuVerifikasi_Click(object sender, EventArgs e)
        {

        }

        private void mnuLPJTU_Click(object sender, EventArgs e)
        {
            frmLPJTU fLPJTU = new frmLPJTU();

            DisplayFormOnTab(fLPJTU, "LPJ TU", "LPJ TU");
        }

        private void mnuSInergi_Click(object sender, EventArgs e)
        {
            frmSinergi f = new frmSinergi();
            f.Show();
        }

        private void testBuatIDBillingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmTambahPajak fTPajak = new frmTambahPajak();
            fTPajak.Test = true;
            fTPajak.Show();
        }

        private void mnuAKDanRealisasi_Click(object sender, EventArgs e)
        {
            frmLRAAnngaranKas fLRA = new frmLRAAnngaranKas();
            fLRA.Show();
        }

        private void mnuTerimaSetorSTS_Click(object sender, EventArgs e)
        {
            frmTerimaSetorKasda fTerimaSetor = new frmTerimaSetorKasda();

            DisplayFormOnTab(fTerimaSetor, "Terima Setor STS", "Terima Setor STS");
        }

        private void menuPPK_Click(object sender, EventArgs e)
        {

        }

        private void sPJFungsionalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bendaharaPengeluaranToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bendaharaPenerimaanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bemdaharaPengeluaranToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sPJFungsionalToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmSPJFungsional fPJ = new frmSPJFungsional();
            DisplayFormOnTab(fPJ, "SPJ Fungsional", "SPJ Fungsional");

        }

        private void bKUPengeluaranToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBKU fBKU = new frmBKU();
            fBKU.JenisBendahara = 2;
            DisplayFormOnTab(fBKU, "BKU", "BKU");
        }

        private void menuMaster_Click(object sender, EventArgs e)
        {

        }

        private void z80_Navigation1_SelectedItem(Z80NavBarControl.Z80NavBar.NavBarItem item)
        {
            switch (item.ID)
            {
                case 2001:
                    frmSettingPemda fSettingPemda = new frmSettingPemda();
                    //    fSettingPemda.MdiParent = this;
                   LoadForm(fSettingPemda);


                    break;

                case 2002:
                    frmListDinas fListDinas = new frmListDinas();
                    LoadForm(fListDinas);

                    break;

                case 2003:
                    frmListUK hUK  = new frmListUK();
                    LoadForm(hUK);

                    break;

                case 2004:

                    frmRekening90 fRekening = new frmRekening90();
                    LoadForm(fRekening);

                    break;
                
                case 2005:
                    frmListPejabat frmlPejavat = new frmListPejabat();
                    LoadForm(frmlPejavat);

                    break;
                case 3009:
                    frmSetStatusInput fStatusInput = new frmSetStatusInput();
                    LoadForm(fStatusInput);
                    
                    break;
                case 3010:

                    frmImportAPBD fIMport = new frmImportAPBD();
                    fIMport.Mode = 2;
                    fIMport.Show();
                    break;

                case 3020:

                    frmInputPlafonPerRekening fInputAnggaran = new frmInputPlafonPerRekening(1);
                    
                    LoadForm(fInputAnggaran);
                    break;
                case 3030:

                    frmAnggaran fAnggaran = new frmAnggaran();
                    LoadForm(fAnggaran);

                    
                    break;
                case 3040:

                                    

                    frmAnggaranKas fAnggaranKas = new frmAnggaranKas();
                    LoadForm(fAnggaranKas);
                    
                    break;

                case 4010:
                    frmSPDKepmen900 fSPD = new frmSPDKepmen900();
                    LoadForm(fSPD);
                    break;
                case 4020:
                    frmRegisterSPD fRegisterSPD = new frmRegisterSPD();
                    fRegisterSPD.Show();
                    break;
                case 5101:
                    frmSaldoAwalBendahara fSaldoAwal = new frmSaldoAwalBendahara();
                    LoadForm(fSaldoAwal);
                    break;
                case 5102:
                    frmListPPTK f = new frmListPPTK();
                        LoadForm(f);
                    break;
                case 5103:
                    frmListKontrak fk = new frmListKontrak();
                    LoadForm(fk);
                    break;
                case 5104:
                    frmListBAST fListBAST = new frmListBAST();
                    LoadForm(fListBAST);

                    break;
                case 5105:
                    frmListSPP flSPP = new frmListSPP(0);
                    LoadForm(flSPP);
                    break;
                case 5106:
                    frmListSPP fBUSP2D = new frmListSPP(4);
                    LoadForm(fBUSP2D);
                    break;
                case 5107:
                    frmListTransaksiBank fTB = new frmListTransaksiBank();
                    LoadForm(fTB);
                    break;
                case 5108:
                    frmListPengeluaran fPanjar = new frmListPengeluaran();
                    LoadForm(fPanjar);
                    break;

                case 5109:
                    frmListPenyetoranPajak fPP = new frmListPenyetoranPajak();
                    LoadForm(fPP);
                    break;
                case 5110:
                    frmListPengembalianBelanja fPB = new frmListPengembalianBelanja();
                    LoadForm(fPB);
                    break;
                case 5111:
                    frmListKoreksi fKoreksi = new frmListKoreksi();
                    LoadForm(fKoreksi);


                    break;



            }


        }
        private void LoadForm(Form _form)
        {
            try
            {
                Form activeChild = this.ActiveMdiChild;
                bool bMustDisplay = false;
                // Jika tidak null dan tidak sama dengan child active sekarang maka

                if (activeChild == null)
                {
                    bMustDisplay = true;
                }
                else
                {

                    if (activeChild.Name != _form.Name)
                    {
                        bMustDisplay = true;
                    }
                }
                if (bMustDisplay)
                {


                    this.IsMdiContainer = true;
                    _form.MdiParent = this;
                   // this.Show();
                    _form.ControlBox = false;

                    _form.Width = this.ClientRectangle.Width;
                    _form.Location = new System.Drawing.Point(this.ClientRectangle.Left, this.ClientRectangle.Top);

//                                        _form.Location = new Point(0, 500);
//     _form.FormBorderStyle = FormBorderStyle.None;


                    _form.ShowIcon = false;
                    _form.Dock = DockStyle.Fill;
                    _form.Text = String.Empty;

                    if (_form == null)
                    {
                        MessageBox.Show("Form Ilang");
                        return;
                    }

                    _form.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void tabControl1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void mnuProgramKegiatan_Click(object sender, EventArgs e)
        {

        }

        private void menuAnggaran_Click(object sender, EventArgs e)
        {

        }

        private void menuSPD_Click(object sender, EventArgs e)
        {

        }

        private void menuPenerimaan_Click(object sender, EventArgs e)
        {

        }
    }
}
