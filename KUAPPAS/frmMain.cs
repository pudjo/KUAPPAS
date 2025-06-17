using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using DTO;
using KUAPPAS.Bendahara;


namespace KUAPPAS
{
    public partial class frmMain : Form
    {
        private int childFormNumber = 0;
        private PemdaLogic oPemdaLogic ;//= new PemdaLogic(GlobalVar.TahunAnggaran);

        Form activeChild ;//= this.ParentForm.ActiveMdiChild;
        //frmBendahara fBendahara;
        public frmMain()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            
        }

        private void OpenFile(object sender, EventArgs e)
        {
            
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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

        private void menuSKPD_Click(object sender, EventArgs e)
        {
            
            frmListDinas fListDinas = new frmListDinas();
            fListDinas.MdiParent = this;

            fListDinas.Show();


            //frmSKPD fSKPD = new frmSKPD();

        }

        private void menuKEcamatan_Click(object sender, EventArgs e)
        {
            frmKecamatan fKecamatan = new frmKecamatan();
            fKecamatan.MdiParent = this;
            fKecamatan.Show(); ;

        }

        private void menuDesa_Click(object sender, EventArgs e)
        {
            frmListDesa fLDesa = new frmListDesa();
            fLDesa.MdiParent = this;
            fLDesa.Show();
        }

        private void menuDusun_Click(object sender, EventArgs e)
        {
            frmListDusun fLDusun = new frmListDusun();
            fLDusun.MdiParent = this;
            fLDusun.Show();

        }

        private void menuProgramKegiatan_Click(object sender, EventArgs e)
        {
            //frmProgramKegiatan fPK = new frmProgramKegiatan();
            //fPK.MdiParent = this;
            //fPK.Show();
        }

        private void menuSettingPlafon_Click(object sender, EventArgs e)
        {
           
            
        }

        private void menuSettimgPlafon_Click(object sender, EventArgs e)
        {
          
        }

        private void menuMusrenmbang_Click(object sender, EventArgs e)
        {
            //frmBacaMusrenmbang fMusrenmbang = new frmBacaMusrenmbang();
            //fMusrenmbang.MdiParent = this;
            //fMusrenmbang.Show();

            //frmImportRKABapeda fImport = new frmImportRKABapeda();
            //fImport.ShowDialog();
        }

        private void menuEvaliuasiProgramKegiatan_Click(object sender, EventArgs e)
        {
            //frmAnalisaProgramMusrenmbang f = new frmAnalisaProgramMusrenmbang();
            //f.MdiParent = this;
            //f.Show();

        }

        private void urusanPemerintahan_Click(object sender, EventArgs e)
        {
            frmUrusanPemerintahan f = new frmUrusanPemerintahan();
            f.MdiParent = this;
            f.Show();

        }
      
        private void frmMain_Load(object sender, EventArgs e)
        {

            try
            {

                ////GlobalVar.guserSKPD = new List<SKPD>();
                ////GlobalVar.gListKegiatan = new List<TKegiatanAPBD>();
                ////GlobalVar.gListRekening = new List<Rekening>();

                int _tahapSKPD = 0;
                TahapanAnggaranLogic oTALogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
                TahapanAnggaran taUser = oTALogic.GetByDinas(GlobalVar.Pengguna.SKPD, GlobalVar.TahunAnggaran);
                _tahapSKPD = taUser.Tahap;
                ////RekeningLogic oRekLogic = new RekeningLogic(GlobalVar.TahunAnggaran);
                ////GlobalVar.gListRekening = oRekLogic.Get();
                //    menuAnggaran1.Width = menuAnggaran1.MaxWidth;
                
                /*frmLogin fLogin = new frmLogin();
                fLogin.ShowDialog();
                if (GlobalVar.Pengguna == null)
                    return;

                if (fLogin.IsOK() == false)
                {
                    this.Close();


                }
                else
                {*/
                    menuStrip.Visible = true;
                    if (GlobalVar.Pengguna.Level < 3)
                    {
                        menuTahap.Visible = false;
                        menuPegguna.Visible = false;
                    }

                         if (GlobalVar.Pengguna.Level < 2)
                    {

                        //menuProgramKegiatan.Visible = false;
                        //programKegiatanToolStripMenuItem.Visible = false;

                    }

                    if (GlobalVar.TahunAnggaran == 2018)
                    {
                        menuRKAPerubahanAnggaran.Visible = true;
                        anggaranKasPerubahanToolStripMenuItem.Visible = true;
                        menuDPAPerubahanAnggaran.Visible = true;


                        menuInputRKA.Visible = false;
                        dPAToolStripMenuItem.Visible = false;
                        mnuAnggaranKas.Visible = false;

                        menuRKAPenyempurnaan.Visible = true;
                        menuAnggaranKasPergeseran.Visible = true;
                        menuDPAPenyempurnaan.Visible = true;


                    }


                    //if (GlobalVar.Pengguna.Kelompok != 1 || GlobalVar.Pengguna.UserID=="pudjo")
                    //    ctrlSKPD1.Visible = true;

                    if (GlobalVar.Pengguna.SKPD > 0)
                    {
                        mnuPerdaAPBD.Visible = false;

                        mnuPerdaAPBD.Visible = false;
                        mnuRekapInput.Visible = false;
                        menuDasarHukum.Visible = false;
                        menuDesa.Visible = false;
                        menuDusun.Visible = false;

                        menuInpuPlafon.Visible = false;
                        menuKEcamatan.Visible = false;
                        menuKodeRekening.Visible = false;

                        menuPemantauan.Visible = false;
                        menuPeraturanKepalaDaerah.Visible = false;

                        menuPlafonBL.Visible = false;
                        // menuProgramKegiatan.Visible =false;
                        menuSetakKUA.Visible = false;
                        menuSettingDinasPPKD.Visible = false;
                        menuSettingPejabatPPKD.Visible = false;
                        menuSettingPemda.Visible = false;
                        menuSPD.Visible = false;
                        menuStandardHarga.Visible = false;
                        menuTahap.Visible = false;
                  
                        menuSettingTahapInput.Visible = false;
                        menuPlafonBL.Visible = false;
                        masterMenu.Visible = false;
                        menuPegguna.Visible = false;
                        inputPlafonToolStripMenuItem.Visible = false;

                        sPDToolStripMenuItem.Visible = false;
                        // mnuImportDataMusrenbangBapeda.Visible = false;
                        menuBendahara.Visible = false;
                        if (GlobalVar.Pengguna.Kelompok == 2 || GlobalVar.Pengguna.Level > 2)
                        {
                            //      pemilihanMusrenmabngPadaKegiatanToolStripMenuItem.Visible = true;

                        }
                        else
                            // pemilihanMusrenmabngPadaKegiatanToolStripMenuItem.Visible = false;
                            //bappedaToolStripMenuItem.Visible = false;
                            if (GlobalVar.Pengguna.UserID == "duato")
                            {
                                masterMenu.Visible = true;
                                menuStandardHarga.Visible = true;
                            }

                    }
               

  

                    if (GlobalVar.Pengguna.Kelompok == 2 || GlobalVar.Pengguna.Level > 2)
                    {
                        // pemilihanMusrenmabngPadaKegiatanToolStripMenuItem.Visible = true;
                        //  bappedaToolStripMenuItem.Visible = true;
                        if (GlobalVar.Pengguna.Kelompok == 2)
                        {
                            menuPlafonBL.Visible = false;
                            inputPlafonToolStripMenuItem.Visible = false;
                            //    programKegiatanToolStripMenuItem.Visible = false;
                            menuSettingDinasPPKD.Visible = false;
                            inputPlafonToolStripMenuItem.Visible = false;
                            aPBDToolStripMenuItem.Visible = false;
                            sPDToolStripMenuItem.Visible = false;
                            menuSettingDinasPPKD.Visible = false;
                            menuSettingPejabatPPKD.Visible = false;
                            menuKodeRekening.Visible = false;
                            mnuDaftarSumberDana.Visible = false;
                            mnuDaftarSumberDana.Visible = false;
                            menuDasarHukum.Visible = false;
                            menuStandardHarga.Visible = false;
                            menuKategoriBAru.Visible = false;
                            menuUrusanBaru.Visible = false;
                            menuMappingUrusanBaru.Visible = false;
                            menuMapUrusanProgram.Visible = false;


                        }
                        else
                        {
                            if (GlobalVar.Pengguna.Kelompok > 3)
                            {
                                keamananToolStripMenuItem.Visible = false;
                                masterMenu.Visible = false;
                                inputPlafonToolStripMenuItem.Visible = false;
                                menuRKA.Visible = false;
                                aPBDToolStripMenuItem.Visible = false;


                            }
                            else
                            {

                                menuPlafonBL.Visible = true;
                                if (GlobalVar.TahunAnggaran < 2020)
                                {
                                    menuPlafonBL.Text = "Input KUA PPAS Belanja Langdung";
                                    menuInpuPlafon.Visible = true;
                                    menuSetakKUA.Visible = true;

                                    mnuImportDataPaket.Visible = false;
                                }
                                else
                                {
                                    menuPlafonBL.Text = "Input KUA PPAS";
                                    menuInpuPlafon.Visible = true;
                                    menuSetakKUA.Visible = false;
                                    menuInpuPlafon.Text = "Plafon Pendapatan/Pembiayaan";
                                    mnuImportDataPaket.Visible = true;


                                }

                            }
                        }

                    }

                    if (GlobalVar.Pengguna.Kelompok > 3)
                    {
                        keamananToolStripMenuItem.Visible = false;
                        masterMenu.Visible = false;
                        inputPlafonToolStripMenuItem.Visible = false;
                        menuRKA.Visible = false;
                        aPBDToolStripMenuItem.Visible = false;
                        //             ctrlSKPD1.Visible = true;
                        if (GlobalVar.PP90 == false)
                            mnuplafonkepmen050.Visible = false;
                    }
                    // ctrlHeaderMain1.SetTahun(GlobalVar.TahunAnggaran);

                    if (GlobalVar.TahunAnggaran == 2022)
                    {
                        InputRKADPAPenyempurnaanPerubahanTanpaUraianToolStripMenuItem.Enabled = true;
                    }
                    if ((int)GlobalVar.TahunAnggaran >=2021 ){
                        menuInpuPlafon.Visible = false;
                    }
                    ctrlHeaderMain1.SetTahun((int)GlobalVar.TahunAnggaran);
               
                    //{
                     toolStripMenuItem5.Text = "Input/Periksa Data Anggaran Perubahan ";

                    //}
                    //else
                    //{
                    //    toolStripMenuItem5.Text = "Input/Periksa Data Anggaran ";
                    //}

                     if (GlobalVar.Pengguna.SKPD>0)
                     {
                         switch (_tahapSKPD)
                         {
                             case 2:
                                 
                                    menuInputRKA.Visible= true ;
                                    mnuRKATanpaUraian.Visible= true ;
                                    mnuAnggaranKas.Visible= true ;
                                    dPAToolStripMenuItem.Visible= true ;
                                                                     menuInputRKA.Enabled= true ;
                                    mnuRKATanpaUraian.Enabled= true ;
                                    mnuAnggaranKas.Enabled= true ;
                                    dPAToolStripMenuItem.Enabled= true ;
                                   break ;

                        case 3:
                                 
                            menuRKAPenyempurnaan.Visible= true ;
                            menuAnggaranKasPergeseran.Visible= true ;
                            menuDPAPenyempurnaan.Visible= true ;

                            menuRKAPenyempurnaan.Enabled= true ;
                            menuAnggaranKasPergeseran.Enabled= true ;
                            menuDPAPenyempurnaan.Enabled= true ;
                            break;

                             case 4:

                            toolStripMenuItem5.Visible= true ;
                            menuRKAPerubahanAnggaran.Visible= true ;
                            anggaranKasPerubahanToolStripMenuItem.Visible= true ;
                            menuDPAPerubahanAnggaran.Visible= true ;
                            toolStripMenuItem5.Enabled = true ;
                            menuRKAPerubahanAnggaran.Enabled= true ;
                            anggaranKasPerubahanToolStripMenuItem.Enabled= true ;
                            menuDPAPerubahanAnggaran.Enabled= true ;
                                                             break;
                                                         case 5 :

                            InputRKADPAPenyempurnaanPerubahanTanpaUraianToolStripMenuItem.Visible= true ;
                            anggaranKasPenyempurnaanPerubahanAnggaranToolStripMenuItem.Visible = true;

                            InputRKADPAPenyempurnaanPerubahanTanpaUraianToolStripMenuItem.Enabled = true;
                            anggaranKasPenyempurnaanPerubahanAnggaranToolStripMenuItem.Enabled = true;

                            break;


                         }

                     }
                     else
                     {
                         menuInputRKA.Visible= true ;
                        mnuRKATanpaUraian.Visible= true ;
                        mnuAnggaranKas.Visible= true ;
                        dPAToolStripMenuItem.Visible= true ;
                                                                     menuInputRKA.Enabled= true ;
                                    mnuRKATanpaUraian.Enabled= true ;
                                    mnuAnggaranKas.Enabled= true ;
                                    dPAToolStripMenuItem.Enabled= true ;
                        
                                 
                            menuRKAPenyempurnaan.Visible= true ;
                            menuAnggaranKasPergeseran.Visible= true ;
                            menuDPAPenyempurnaan.Visible= true ;

                            menuRKAPenyempurnaan.Enabled= true ;
                            menuAnggaranKasPergeseran.Enabled= true ;
                            menuDPAPenyempurnaan.Enabled= true ;
                         
                            toolStripMenuItem5.Visible= true ;
                            menuRKAPerubahanAnggaran.Visible= true ;
                            anggaranKasPerubahanToolStripMenuItem.Visible= true ;
                            menuDPAPerubahanAnggaran.Visible= true ;
                            toolStripMenuItem5.Enabled = true ;
                            menuRKAPerubahanAnggaran.Enabled= true ;
                            anggaranKasPerubahanToolStripMenuItem.Enabled= true ;
                            menuDPAPerubahanAnggaran.Enabled= true ;
                         
                            InputRKADPAPenyempurnaanPerubahanTanpaUraianToolStripMenuItem.Visible= true ;
                            anggaranKasPenyempurnaanPerubahanAnggaranToolStripMenuItem.Visible = true;

                            InputRKADPAPenyempurnaanPerubahanTanpaUraianToolStripMenuItem.Enabled = true;
                            anggaranKasPenyempurnaanPerubahanAnggaranToolStripMenuItem.Enabled = true;


                     }

                    SetVersionLabel();


               // }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan loading form");

            }

        }
        private void SetVersionLabel()
        {
            


        }
        private void menuInpuPlafon_Click(object sender, EventArgs e)
        {
            //frmPlafon fPlafon = new frmPlafon();
            //fPlafon.MdiParent = this;
            //fPlafon.Show();

        }

        private void menuPegguna_Click(object sender, EventArgs e)
        {
            frmPengguna fPEngguna = new frmPengguna();
            fPEngguna.MdiParent = this;

            fPEngguna.Show();
        }

        private void menuPlafonBL_Click(object sender, EventArgs e)
        {
            
            if ((int)GlobalVar.TahunAnggaran < 2020)
            {
                //frmPlafonBL fPlafonBLX = new frmPlafonBL();
                //fPlafonBLX.MdiParent = this;

                //fPlafonBLX.Show();


            }
            else
            {
                //if (GlobalVar.PP90== true)
                //{
                //    frmPlafonBL90 fPlafonBL90 = new frmPlafonBL90();
                //    fPlafonBL90.MdiParent = this;

                //    fPlafonBL90.Show();
                //}
                //else
                //{
                    frmKUAPPASTerintegrasi fPlafonBL = new frmKUAPPASTerintegrasi();
                    fPlafonBL.MdiParent = this;
                if (GlobalVar.PP90== false )
                    fPlafonBL.Profile = 1;
                else 
                    fPlafonBL.Profile = 2;

                    fPlafonBL.Show();
              //  }
            }

        }

        private void menuTahap_Click(object sender, EventArgs e)
        {
         
          

        }

        private void menuSetakKUA_Click(object sender, EventArgs e)
        {
            //frmCetakKUA fKUA = new frmCetakKUA();
            //fKUA.Show();
        }

        private void mnuRubahPassword_Click(object sender, EventArgs e)
        {
            //frmGantiPassword fGantiPWD = new frmGantiPassword();
            //fGantiPWD.UntukGantiPassword();
            //fGantiPWD.SetID(GlobalVar.Pengguna.ID);
            //fGantiPWD.ShowDialog();

        }

        private void mnuRekapInput_Click(object sender, EventArgs e)
        {
            frmRekap fRekap = new frmRekap();
            fRekap.MdiParent = this;
            fRekap.Show();

        }

        private void menuInputRKA_Click(object sender, EventArgs e)
        {
            ////if (GlobalVar.TahunAnggaran == 2020)
            ////{
            ////    frmRKA fRKAP = new frmRKA();
            ////    fRKAP.MdiParent = this;               
            ////    fRKAP.SetModeAndTahap(0, 1);
              
            ////    fRKAP.Show();
            ////}
            ////else
            ////{
            ////    frmRKA fRKA = new frmRKA();
            ////    fRKA.MdiParent = this;

            ////    //if (GlobalVar.TahapAnggaran < 2)
            ////    //{
            ////    fRKA.SetModeAndTahap(0, 1);
            ////    //}
            ////    //else
            ////    //{
            ////    //   fRKA.SetModeAndTahap(0, 2);
            ////    //}
            ////    fRKA.Profile = 2;

            ////    fRKA.Show();
            ////}

        }

        private void mnuAnggaranKas_Click(object sender, EventArgs e)
        {
            frmAnggaranKas fAK = new frmAnggaranKas();
            fAK.SetTahap(2);
            fAK.MdiParent = this;
            fAK.Show();
        }

        private void menuSettingPejabatSKPD_Click(object sender, EventArgs e)
        {
            //frmPejabatDinas fSKPD = new frmPejabatDinas();
            //fSKPD.ShowDialog();
        }

        private void subMenuPerdaAPBD_Click(object sender, EventArgs e)
        {

            //frmCetakPerda2 f = new frmCetakPerda2();            
            //f.Show();

        }

        private void subMenuRingkasanAPBD_Click(object sender, EventArgs e)
        {
            
        }

        private void menuSettingPemda_Click(object sender, EventArgs e)
        {
            frmSettingPemda fSettingPemda = new frmSettingPemda();
            fSettingPemda.MdiParent = this;
            fSettingPemda.Show();
        }

        private void unitkerjaMenu_Click(object sender, EventArgs e)
        {
            frmListUK fListUK = new frmListUK();
            fListUK.MdiParent = this;
            fListUK.Show();

        }

        private void menuSettingDinasPPKD_Click(object sender, EventArgs e)
        {
            //frmSettingPPKD fSettingPPKD = new frmSettingPPKD();
            //fSettingPPKD.ShowDialog();

        }

        private void menuKodeRekening_Click(object sender, EventArgs e)
        {
            if (GlobalVar.PP90 == false)
            {
                frmRekening fRekening = new frmRekening();
                fRekening.MdiParent = this;
                fRekening.Show();
            }
            else
            {
                frmRekening90 fRekening = new frmRekening90();
                fRekening.MdiParent = this;
                fRekening.Show();
            }

        }

        private void menuDasarHukum_Click(object sender, EventArgs e)
        {
            frmDasarHukum fDH = new frmDasarHukum();
            fDH.MdiParent = this;
            fDH.Show();

        }

        private void menuStandardHarga_Click(object sender, EventArgs e)
        {
            //frmStandardHarga fSH = new frmStandardHarga();
            //fSH.MdiParent = this;
            //fSH.Show();

        }

        private void menuRaperdaII_Click(object sender, EventArgs e)
        {
            //frmRaperdaII fRaperdaII = new frmRaperdaII();
            //fRaperdaII.SetTahap((int)GlobalVar.TahapAnggaran);
            //fRaperdaII.Show();
            

        }

        private void menuPenjabaran_Click(object sender, EventArgs e)
        {
            frmPenjabaran fPenjabaran = new frmPenjabaran();
            fPenjabaran.SetTahap((int)GlobalVar.TahapAnggaran);
            fPenjabaran.Show();

        }

        private void raperdaVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void menuRaperrdaIII_Click(object sender, EventArgs e)
        {

        }

        private void raperdaIVToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuImportPenjabaran_Click(object sender, EventArgs e)
        {
            frmImportAPBD fImport = new frmImportAPBD();
            fImport.Show();
        }

        private void mnuPerdaAPBD_Click(object sender, EventArgs e)
        {
            //frmCetakPerda2 f = new frmCetakPerda2();
            //f.Show();

        }

        private void menuPeraturanBupati_Click(object sender, EventArgs e)
        {
          
        }

        private void menuPemantauan_Click(object sender, EventArgs e)
        {
            frmDashBoard fDashBoard = new frmDashBoard();
            fDashBoard.MdiParent = this;
            fDashBoard.Show();

        }

        private void dPAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (GlobalVar.TahunAnggaran == 2020)
            //{
            //    frmRKA fRKAP = new frmRKA();
            //    fRKAP.MdiParent = this;

            //    //if (GlobalVar.TahapAnggaran < 2)
            //    //{
            //    fRKAP.SetModeAndTahap(0, 2);
            //    //}
            //    //else
            //    //{
            //    //   fRKA.SetModeAndTahap(0, 2);
            //    //}

            //    fRKAP.Show();
            //}
            //else
            //{
            //    frmRKA fRKA = new frmRKA();
            //    fRKA.MdiParent = this;

            //    //if (GlobalVar.TahapAnggaran < 2)
            //    //{
            //    fRKA.SetModeAndTahap(0, 2);
            //    //}
            //    //else
            //    //{
            //    //   fRKA.SetModeAndTahap(0, 2);
            //    //}

            //    fRKA.Show();
            //}

            ////frmDPA fDPA = new frmDPA();

            //////frmRKA fRKA = new frmRKA();
            ////fDPA.MdiParent = this;
            ////    fDPA.SetModeAndTahap(1,2);
            ////    //fDPA.SetModeAndTahap(1, 2);
            
            ////fDPA.Show();
        }

        private void menuSPD_Click(object sender, EventArgs e)
        {
            //if (GlobalVar.TahunAnggaran < 2021)
            //{
            //    frmSPD fSPD = new frmSPD();
            //    fSPD.MdiParent = this;
            //    fSPD.Show();
            //}
            //else
            //{
            //    frmSPDKempem50 fSPD50 = new frmSPDKempem50();
            //    fSPD50.MdiParent = this;
            //    fSPD50.Show();

            //}
        }

        private void menuRekapPerJenisBelanja_Click(object sender, EventArgs e)
        {
            //frmRekapPerJenis fRekap = new frmRekapPerJenis();
            //fRekap.MdiParent = this;
            //fRekap.Show();

        }

        private void mnuSumberDana_Click(object sender, EventArgs e)
        {

            frmImportAPBD fImport = new frmImportAPBD();

            fImport.Show();

            //frmSumberDana fSumberDana = new frmSumberDana();
            //fSumberDana.MdiParent = this;
            //fSumberDana.Show();
        }

        private void mnuDaftarSumberDana_Click(object sender, EventArgs e)
        {
            frmMasterSumberDana fMsumberDana = new frmMasterSumberDana();
            fMsumberDana.MdiParent = this;
            fMsumberDana.Show();

        }

        private void menuSettingPejabatPPKD_Click(object sender, EventArgs e)
        {

            frmPejabatDetail fPejabat = new frmPejabatDetail();
            fPejabat.ShowDialog();


            //frmSettingPejabatPPKD fSettingPPKD = new frmSettingPejabatPPKD(global::DTO.clsJenisJabatan.JenisJabatan.PPKD);
            //fSettingPPKD.ShowDialog();
        }

        private void menuPeraturanKepalaDaerah_Click(object sender, EventArgs e)
        {
            //frmCetakPeraturanKepalaDaerah fPerkada = new frmCetakPeraturanKepalaDaerah();
            //fPerkada.Show();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmADK fADK = new frmADK();
            fADK.Show();

        }

        private void mnuRegisterSPD_Click(object sender, EventArgs e)
        {
            frmRegisterSPD fSPD = new frmRegisterSPD();
            fSPD.Show();

        }

        private void menuPermen93_Click(object sender, EventArgs e)
        {
            //frmPermen93 fPermen93 = new frmPermen93();
            //fPermen93.Show();

        }

        private void menuKategoriBAru_Click(object sender, EventArgs e)
        {
            frmKategoriBaru fKB = new frmKategoriBaru();
            fKB.Show();
        }

        private void menuUrusanBaru_Click(object sender, EventArgs e)
        {
            //frmUrusanBaru fUB = new frmUrusanBaru();
            //fUB.Show();
        }

        private void menuCekDPA_Click(object sender, EventArgs e)
        {
            //frmCekDPAPlafon fCek = new frmCekDPAPlafon();
            //fCek.MdiParent = this;
            //fCek.Show();

        }

        private void menuRKAPenyempurnaan_Click(object sender, EventArgs e)
        {
            //if (GlobalVar.PP90 == false)
            //{
            //    frmRKAPenyempurnaan fRKAP = new frmRKAPenyempurnaan();
            //    fRKAP.MdiParent = this;
            //    fRKAP.SetTahap(3);
            //    fRKAP.SetModeAndTahap(0, 3);
            //    fRKAP.Show();
            //} else
            ////{
            //    frmRKA fRKAPergeseran = new frmRKA();
            //    fRKAPergeseran.SetModeAndTahap(0, 3);
            //    fRKAPergeseran.MdiParent = this;
            //   // fRKAPergeseran.SetTahap(3);
            //    fRKAPergeseran.Show();
            ////}

                frmInputPlafonPerRekening fPlafon = new frmInputPlafonPerRekening(1);
                fPlafon.MdiParent = this;
                fPlafon.Tahap = 3;// (int)GlobalVar.TahapAnggaran;
                fPlafon.Show();

        }

        private void menuSettingTahapInput_Click(object sender, EventArgs e)
        {
            frmSetStatusInput fStatusInput = new frmSetStatusInput();
            fStatusInput.Show();
        }

        private void menuDPAPenyempurnaan_Click(object sender, EventArgs e)
        {
            frmDPAPenyempurnaan fRKAP = new frmDPAPenyempurnaan();
            fRKAP.SetTahap(3);
            fRKAP.MdiParent = this;
            fRKAP.Show();

        }

        private void menuMappingUrusanBaru_Click(object sender, EventArgs e)
        {
            frmMapUrusanBaru fMapUrusanBaru = new frmMapUrusanBaru();
            fMapUrusanBaru.Show();

        }

        private void menuMapUrusanProgram_Click(object sender, EventArgs e)
        {
            frmMapUrusanBAruProgram f = new frmMapUrusanBAruProgram();
            f.Show();

        }

        private void mnuPosisiAnggaranKas_Click(object sender, EventArgs e)
        {
            //frmStatusAnggaranKas fsangkas = new frmStatusAnggaranKas();
            //fsangkas.Show();
        }

        private void menuRKAPerubahanAnggaran_Click(object sender, EventArgs e)
        {
            //frmRKA fRKAPergeseran = new frmRKA();
            //fRKAPergeseran.SetModeAndTahap(0, 4);
            //fRKAPergeseran.MdiParent = this;
            //// fRKAPergeseran.SetTahap(3);
            //fRKAPergeseran.Show();

            ////frmRKAPenyempurnaan fRKAP = new frmRKAPenyempurnaan();
            
            //fRKAP.MdiParent = this;
            //fRKAP.SetTahap(4);
            //fRKAP.SetModeAndTahap(0, 4);
            //fRKAP.Show();

        }

        private void menuDPAPerubahanAnggaran_Click(object sender, EventArgs e)
        {
            
            ////frmDPAPenyempurnaan fRKAP = new frmDPAPenyempurnaan();
            ////fRKAP.SetTahap(4);
            ////fRKAP.MdiParent = this;
            ////fRKAP.Show();
            //frmRKA fRKAPergeseran = new frmRKA();
            //fRKAPergeseran.SetModeAndTahap(0, 4);
            //fRKAPergeseran.MdiParent = this;
            //// fRKAPergeseran.SetTahap(3);
            //fRKAPergeseran.Show();
        }

        private void anggaranKasPerubahanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAnggaranKas fAK = new frmAnggaranKas();
            fAK.SetTahap(4);

            fAK.MdiParent = this;
            fAK.Show();
        }

        private void importAPBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmImportRKABapeda fIMport = new frmImportRKABapeda();
            //fIMport.Show();

            frmImportAPBD fIMport = new frmImportAPBD();
            fIMport.Mode = 2;
            fIMport.Show();


        }

        private void inputPlafonAPBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInputPlafonPerRekening fPlafon = new frmInputPlafonPerRekening(1);
          //  fPlafon.SetMode(1);
            //if (GlobalVar.TahunAnggaran == 2021)
            //    fPlafon.Profile = 3;
            fPlafon.MdiParent = this;
            fPlafon.Show();
        }

        private void menuAnggaranKasPergeseran_Click(object sender, EventArgs e)
        {
            frmAnggaranKas fAK = new frmAnggaranKas();
            fAK.SetTahap(3);
            fAK.MdiParent = this;
            fAK.Show();
        }

        private void mnuPemilihanMusrenmbangKegiatan_Click(object sender, EventArgs e)
        {

            

            //frmPemilihanKegiatanMusrenmbang f = new frmPemilihanKegiatanMusrenmbang();
            //f.MdiParent = this;
     
            //f.Show();

            //f.ShowDialog();
        }

        private void pemilihanMusrenmabngPadaKegiatanToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //frmPemilihanKegiatanMusrenmbang f = new frmPemilihanKegiatanMusrenmbang();
            //f.MdiParent = this;

            //f.Show();

        }

        private void mnuTimTAPD_Click(object sender, EventArgs e)
        {
            frmIputTimTAPD f = new frmIputTimTAPD();
            f.Show();
        }

        private void mnuMasterProgramKegiatanBapeda_Click(object sender, EventArgs e)
        {
            //frmProgramKegiatan fPK = new frmProgramKegiatan();
            //fPK.MdiParent = this;
            //fPK.Show();
        }

        private void menuMusrenbangToExcell_Click(object sender, EventArgs e)
        {
            //frmExportMusrenbangToExcell frmEx = new frmExportMusrenbangToExcell();
            //frmEx.Show();
        }
        private void LoadVersion()
        {
            
                Ini ini = new Ini(AppDomain.CurrentDomain.BaseDirectory + "KUAPPAS.ini");
                statusStrip.Items["lblALamat"].Text = ini.IniReadValue("KUA", "CurrentVersion");

            //_Server = ini.IneadValue("KUA", "CurrentVersion");
            //        _Database = ini.IniReadValue("KUA", "DATABASE");
            //        _Password = ini.IniReadValue("KUA", "PASWORD");

        }
        private void LoadForm(Form frm)
        {
           // sideBarAnggaran1.Width = 50;
 
            frm.MdiParent = this;            
            frm.Show();



        } 

        private void menuPermaslaahanPembangunan_Click(object sender, EventArgs e)
        {
            
        }

        private void mnuIsuStrategis_Click(object sender, EventArgs e)
        {
           
        }

        private void mnuVisidanMisi_Click(object sender, EventArgs e)
        {
           
        }

        private void mnuProfileRPJMD_Click(object sender, EventArgs e)
        {
            
        }

        private void mnuImportDataMusrenbangBapeda_Click(object sender, EventArgs e)
        {
            //frmImportRKABapeda fImport = new frmImportRKABapeda();
            //fImport.ShowDialog();
        }

        private void singkronisasiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmSettingSingkronisasi fSIngkron = new frmSettingSingkronisasi();
            //fSIngkron.MdiParent = this;
            //fSIngkron.Show();
        }

        private void sPPToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //    //frmBendahara fPK = new frmBendahara();
        ////    fBendahara  = new frmBendahara();
        //    activeChild = fBendahara;
        //    //fBendahara.MdiParent = this;
        //    //fBendahara.Show();

        }

        private void mnuStandardHarga2_Click(object sender, EventArgs e)
        {
            //frmStandardHarga fSH = new frmStandardHarga();
            //fSH.MdiParent = this;
            //fSH.Show();
        }

        private void importDataPerencanaanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmImportPerencanaan fImportPerencanaan = new frmImportPerencanaan();
            //fImportPerencanaan.Show();

        }

        private void mnuImportDataPaket_Click(object sender, EventArgs e)
        {
            //frmImportPerencanaan fImportPerencanaan = new frmImportPerencanaan();
            //fImportPerencanaan.Show();


        }

        private void mnuSettingKoneksi_Click(object sender, EventArgs e)
        {
            frmKoneksiPerencanaan fImportPerencanaan = new frmKoneksiPerencanaan();
            fImportPerencanaan.Show();

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (GlobalVar.PP90 ==false )
            {
                //frmProgramKegiatan fpg = new frmProgramKegiatan();
                //fpg.MdiParent = this;
                //fpg.Show();
            }
            else
            {
                //frmProgramKegiatan90 fpg = new frmProgramKegiatan90();
                //fpg.MdiParent = this;
                //fpg.Show();
            }
        }

        private void mnuKontrak_Click(object sender, EventArgs e)
        {
            //frmListKontrak fpg = new frmListKontrak();
            //fpg.MdiParent = this;

            //fpg.Show();
        }

        private void mnuCetakRekapKUA_Click(object sender, EventArgs e)
        {
            frmRekapKUAPPAS fRekap = new frmRekapKUAPPAS();
            fRekap.Show();

        }

        private void mnuInputPaguSKPD_Click(object sender, EventArgs e)
        {
            //frmPaguSKPD fpagu = new frmPaguSKPD();
            //fpagu.Show();

        }

        private void mnuVerifikasiPaket_Click(object sender, EventArgs e)
        {
            //frmVerifikasiPaket frmver = new frmVerifikasiPaket();
            //frmver.Show();
        }

        private void menuPerbaikiProgram_Click(object sender, EventArgs e)
        {
            frmPerbaikiKodeProgramKegiatan f = new frmPerbaikiKodeProgramKegiatan();
            f.Show();
        }

        private void mappingKodeRekening13KodeRekening64ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMappingRekening fMap = new frmMappingRekening();
            fMap.MdiParent = this;
            fMap.Show();
        }

        private void mnuCekKUAANggaran_Click(object sender, EventArgs e)
        {
            //frmCekKUAKegiatan fCek = new frmCekKUAKegiatan();
            //fCek.Show();

        }

        private void importMasterExcellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmImportExcell fImportExcell = new frmImportExcell();
            //fImportExcell.Show();

        }

        private void mnuPerdaRealisasi_Click(object sender, EventArgs e)
        {
            
        }

        private void ctrlSKPD1_OnChanged(int pID)
        {
            // Determine the active child form.  
            

            // If there is an active child form, find the active control, which  
            // in this example should be a RichTextBox.  
            if (activeChild != null)
            {
                try
                {
                    //if (activeChild is frmBendahara)
                    //{
                    //    fBendahara.SetIDDInas(pID);
                    //}
                    
                }
                catch
                {
                    MessageBox.Show("You need to select a RichTextBox.");
                }
            }  
        }

        private void sPJFungsionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        //private void menuAnggaran1_OnEnter(int pID)
        //{
        //    if (menuAnggaran1.Width == pID)
        //    {
        //        return;
        //    }
        //    for (int x = menuAnggaran1.MinWidth; x < pID; x++)
        //    {
        //        //for (int counter = 0; counter < 5; counter++)
        //        //{  

        //        //}
        //        x = x + 103;
        //        menuAnggaran1.Width = x;
        //    }
               

        //}
        protected override CreateParams CreateParams
        {
            get
            {

                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;

            }
        }


        //protected override void OnResizeBegin(EventArgs e)
        //{
        //    SuspendLayout();
        //    base.OnResizeBegin(e);
        //}
        //protected override void OnResizeEnd(EventArgs e)
        //{
        //    ResumeLayout();
        //    base.OnResizeEnd(e);
        //}
        private void menuAnggaran1_OnLeave(int pID, int pos)
        {
            //if (pos > menuAnggaran1.MaxWidth)
            //{i
           // if (pos >= (menuAnggaran1.MaxWidth ))
             //  menuAnggaran1.Width = pID;
           // }
        }

        private void menuAnggaran1_OnMenuPemda(int pID)
        {
            frmSettingPemda fSettingPemda = new frmSettingPemda();
            LoadForm(fSettingPemda);

            
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
           // panel1.Width = 50;

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
           // pictureBox1.Width = 50;

        }

        private void frmMain_MouseLeave(object sender, EventArgs e)
        {
//            sideBarAnggaran1.Width = 50;

        }

        private void sideBarAnggaran1_MouseEnter(object sender, EventArgs e)
        {
  //          sideBarAnggaran1.Width = 250;
        }

        private void sideBarAnggaran1_MouseLeave(object sender, EventArgs e)
        {
            //if (sideBarAnggaran1.GetPosX () >200)
            //sideBarAnggaran1.Width = 50;
        }

        private void sideBarAnggaran1_OnMenuPemda(int pID)
        {
            frmSettingPemda fSettingPemda = new frmSettingPemda();
            LoadForm(fSettingPemda);


        }

        private void sideBarAnggaran1_OnMenuSKPD(int pID)
        {

            frmListDinas fListDinas = new frmListDinas();
            LoadForm(fListDinas);//.MdiParent = this;

            //fListDinas.Show();
        }

        private void sideBarAnggaran1_OnMenuPejabat(int pID)
        {
          
        }

        private void sideBarAnggaran1_OnMenuProgramKegiatan(int pID)
        {
            if (GlobalVar.PP90 == false)
            {
                //frmProgramKegiatan fpg = new frmProgramKegiatan();
                //LoadForm(fpg);
            }
            else
            {
                //frmProgramKegiatan90 fpg = new frmProgramKegiatan90();
                //LoadForm(fpg);
            }
        }

        private void sideBarAnggaran1_OnMenuKodeRekening(int pID)
        {
            if (GlobalVar.PP90 == false)
            {
                frmRekening fRekening = new frmRekening();
                LoadForm(fRekening);
                
            }
            else
            {
                frmRekening90 fRekening = new frmRekening90();
                LoadForm(fRekening);
                
                
            }
        }

        private void sideBarAnggaran1_OnMenuDasarHukum(int pID)
        {
            frmDasarHukum fDH = new frmDasarHukum();
            LoadForm(fDH);
                
        }

        private void sideBarAnggaran1_OnMenuSumberDana(int pID)
        {
            frmMasterSumberDana fMsumberDana = new frmMasterSumberDana();
            LoadForm(fMsumberDana); 
        }

        private void sideBarAnggaran1_OnMenuStandardHarga(int pID)
        {
            //frmStandardHarga fSH = new frmStandardHarga();
            //LoadForm(fSH);
            
        }

        private void sideBarAnggaran1_OnMenuSettingPejabatSKPD(int pID)
        {
            //frmPejabatDinas fSKPD = new frmPejabatDinas();
            //fSKPD.ShowDialog();
        }

        private void sideBarAnggaran1_OnMenuSettingTimTAPD(int pID)
        {
            frmIputTimTAPD f = new frmIputTimTAPD();
            LoadForm(f);
        }

        private void sideBarAnggaran1_OnMenuSettingRKA(int pID)
        {
            //if (GlobalVar.TahunAnggaran == 2020)
            //{
                //frmRKA fRKAP = new frmRKA();
                //fRKAP.MdiParent = this;

                //fRKAP.SetModeAndTahap(0, 1);
                //LoadForm(fRKAP);
                
            //}
            //else
            //{
            //    frmRKA fRKA = new frmRKA();
            //    fRKA.MdiParent = this;

            //    //if (GlobalVar.TahapAnggaran < 2)
            //    //{
            //    fRKA.SetModeAndTahap(0, 1);
            //    //}
            //    //else
            //    //{
            //    //   fRKA.SetModeAndTahap(0, 2);
            //    //}

            //    fRKA.Show();
            //}
        }

        private void sideBarAnggaran1_OnMenuSettingAnggaranKas(int pID)
        {
            frmAnggaranKas fAK = new frmAnggaranKas();
            fAK.SetTahap(2);
            LoadForm(fAK);

        }

        private void sideBarAnggaran1_OnMenuSettingDPA(int pID)
        {
            
                //frmRKA fRKAP = new frmRKA();
            
                //fRKAP.SetModeAndTahap(0, 2);
                //LoadForm(fRKAP);

            }

        private void sideBarAnggaran1_OnMenuSettingRKAPergeseran(int pID)
        {
             if (GlobalVar.PP90 == false)
            {
                //frmRKAPenyempurnaan fRKAP = new frmRKAPenyempurnaan();
                
                //fRKAP.SetTahap(3);
                //fRKAP.SetModeAndTahap(0, 3);
                //LoadForm(fRKAP);

            } else
            {
                //frmRKA fRKAPergeseran = new frmRKA();
                //fRKAPergeseran.SetModeAndTahap(0, 3);
                //LoadForm(fRKAPergeseran);
            }
        }

        private void sideBarAnggaran1_OnMenuSettingAnggaranKasPergeseran(int pID)
        {
            frmAnggaranKas fAK = new frmAnggaranKas();
            fAK.SetTahap(3);
            LoadForm(fAK);
        
            
        }

        private void sideBarAnggaran1_OnMenuSettingDPAPergeseran(int pID)
        {
            frmDPAPenyempurnaan fRKAP = new frmDPAPenyempurnaan();
            fRKAP.SetTahap(3);
            LoadForm(fRKAP);


        }

        private void sideBarAnggaran1_OnMenuSettingRKAPerubahan(int pID)
        {
            //frmRKAPenyempurnaan fRKAP = new frmRKAPenyempurnaan();

   
            //fRKAP.SetTahap(4);
            //fRKAP.SetModeAndTahap(0, 4);
            //LoadForm(fRKAP);

        }

        private void sideBarAnggaran1_OnMenuSettingAnggaranKasPerubahan(int pID)
        {
            frmAnggaranKas fAK = new frmAnggaranKas();
            fAK.SetTahap(4);

            LoadForm(fAK);
        }

        private void sideBarAnggaran1_OnMenuSettingDPAPerubahan(int pID)
        {
            frmDPAPenyempurnaan fRKAP = new frmDPAPenyempurnaan();
            fRKAP.SetTahap(4);
            LoadForm(fRKAP);

        }

        private void sideBarAnggaran1_OnMenuSettingAnggaranKasPergeseranPerubahan(int pID)
        {
            frmAnggaranKas fAK = new frmAnggaranKas();
            fAK.SetTahap(5);

            LoadForm(fAK);
        }

        private void sideBarAnggaran1_OnMenuSettingDPAPergeseranPerubahan(int pID)
        {
            frmDPAPenyempurnaan fRKAP = new frmDPAPenyempurnaan();
            fRKAP.SetTahap(5);
            LoadForm(fRKAP);
        }

        private void sideBarAnggaran1_OnMenuSettingRKAPergeseranPerubahan(int pID)
        {
            if (GlobalVar.PP90 == false)
            {
                //frmRKAPenyempurnaan fRKAP = new frmRKAPenyempurnaan();

                //fRKAP.SetTahap(5);
                //fRKAP.SetModeAndTahap(0, 5);
                //LoadForm(fRKAP);

            }
            else
            {
                ////frmRKA fRKAPergeseran = new frmRKA();
                ////fRKAPergeseran.SetModeAndTahap(0, 5);
                ////LoadForm(fRKAPergeseran);
            }
        }

        private void sideBarAnggaran1_OnPaguSKPD(int pID)
        {
            //frmPaguSKPD fpagu = new frmPaguSKPD();
            //fpagu.Show();

        }

        private void sideBarAnggaran1_OnMenuSettingPlafonBTL(int pID)
        {
            //frmPlafon fPlafon = new frmPlafon();
            //LoadForm(fPlafon);

            
        }

        private void sideBarAnggaran1_OnMenuSettingPlafonBL(int pID)
        {

            if ((int)GlobalVar.TahunAnggaran < 2020)
            {
                //frmPlafonBL fPlafonBLX = new frmPlafonBL();
                //LoadForm(fPlafonBLX);

                

            }
            else
            {
                if (GlobalVar.PP90 == true)
                {
                    //frmPlafonBL90 fPlafonBL90 = new frmPlafonBL90();
                    //LoadForm(fPlafonBL90);

                }
                else
                {
                    frmKUAPPASTerintegrasi fPlafonBL = new frmKUAPPASTerintegrasi();
                    LoadForm(fPlafonBL);

                }
            }
        }

        private void sideBarAnggaran1_Load(object sender, EventArgs e)
        {
     
        }

        private void mnuplafonkepmen050_Click(object sender, EventArgs e)
        {
            frmKUAPPASTerintegrasi fPlafonBL = new frmKUAPPASTerintegrasi();
            fPlafonBL.MdiParent = this;
            fPlafonBL.Profile = 3;

            fPlafonBL.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //frmRKA fRKA = new frmRKA();
            //fRKA.MdiParent = this;

            //if (GlobalVar.TahapAnggaran < 2)
            //{
            ////fRKA.SetModeAndTahap(0, 1);
            //////}
            //////else
            //////{
            //////   fRKA.SetModeAndTahap(0, 2);
            //////}
            ////fRKA.Profile = 3;

            ////fRKA.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmInputPlafonPerRekening fPlafon = new frmInputPlafonPerRekening(1);
            fPlafon.MdiParent = this;
            fPlafon.Tahap = 4;// (int)GlobalVar.TahapAnggaran;
            fPlafon.Show();
   
        }

        private void inputRealisasiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInputPlafonPerRekening fPlafon = new frmInputPlafonPerRekening(1);
            //  fPlafon.SetMode(1);
            //if (GlobalVar.TahunAnggaran == 2021)
                fPlafon.Profile = 3;
            fPlafon.MdiParent = this;
            fPlafon.Show();
        }

        private void ctrlHeaderMain1_Load(object sender, EventArgs e)
        {

        }

        private void InputRKADPAPenyempurnaanPerubahanTanpaUraianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInputPlafonPerRekening fPlafon = new frmInputPlafonPerRekening(1);
            fPlafon.MdiParent = this;
            fPlafon.Tahap = 5;// (int)GlobalVar.TahapAnggaran;
            fPlafon.Show();
        }

        private void anggaranKasPenyempurnaanPerubahanAnggaranToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAnggaranKas fAK = new frmAnggaranKas();
            fAK.SetTahap(5);

            fAK.MdiParent = this;
            fAK.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmInputPlafonPerRekening fPlafon = new frmInputPlafonPerRekening(1);
            fPlafon.MdiParent = this;
            fPlafon.Tahap = 2;// (int)GlobalVar.TahapAnggaran;
            fPlafon.Show();
   
        }

        private void toolStripMenuItem6_Click_1(object sender, EventArgs e)
        {

           // frmImportSIPD fPlafon = new frmImportSIPD();
           // fPlafon.MdiParent = this;
           //// fPlafon.Tahap = 2;// (int)GlobalVar.TahapAnggaran;
           // fPlafon.Show();

        }

        private void inputSumberDanaKegiatan_Click(object sender, EventArgs e)
        {
            frmInputSUmberDanaKegiatan fInputSumberDanaKegiatan = new frmInputSUmberDanaKegiatan();
            fInputSumberDanaKegiatan.MdiParent = this;

            fInputSumberDanaKegiatan.Show();
   
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
