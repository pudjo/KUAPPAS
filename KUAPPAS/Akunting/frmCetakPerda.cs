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
//using Rahul.Office.Token;
//using Rahul.Office.MS.Word;
using Microsoft.Office.Interop.Word;
using System.IO;
using DataAccess;
using KUAPPAS.Akunting;
using Microsoft.Office.Interop.Excel;



namespace KUAPPAS
{
    public partial class frmCetakPerda : Form
    {

        private int m_iPerda;
        private string[] bulan = new string[13] { "", "JANUARI", "FEBRUARI", "MARET", "APRIL" ,"MEI","JUNI","JULI", "AGUSTUS","SEPTEMBER","OKTOBER","NOVEMBER","DESEMBER"};

        public frmCetakPerda()
        {
            InitializeComponent();
            //initEx();
        }
        private void initEx(){


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

        





            //GlobalVar.TahunAnggaran = DataFormat.GetInteger(ini.IniReadValue("PERDA","Tahun"));
            //int jenisJarinan= DataFormat.GetInteger(ini.IniReadValue("PERDA","Jalur"));


            //GlobalVar.PP90 = true;
            //GlobalVar.JENIS_KONEKSI = jenisJarinan;
                                    
            //TahapanAnggaran ta = new TahapanAnggaran();
            //TahapanAnggaranLogic oTALogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            //ta = oTALogic.GetByDinas(0, GlobalVar.TahunAnggaran);

            //if (ta != null)
            //{
            //    GlobalVar.TahapAnggaran = ta.Tahap;
            //}
            //else

            //    GlobalVar.TahapAnggaran = 1;


            //GlobalVar.ProfileRekening = new ProfileRekening();
            //GlobalVar.ProfileProgramKegiatan = new ProfileProgramKegiatan();
            //ProfileRekeningLogic oProfileRekeningLogic = new ProfileRekeningLogic(GlobalVar.TahunAnggaran, 2);
            //ProfileProgramKegiatanLogic oPrgKegLogic = new ProfileProgramKegiatanLogic(GlobalVar.TahunAnggaran);

            //GlobalVar.ProfileProgramKegiatan = oPrgKegLogic.Get();
            //if (GlobalVar.ProfileProgramKegiatan == null)
            //{
            //    GlobalVar.ProfileProgramKegiatan = new ProfileProgramKegiatan();
            //    GlobalVar.ProfileProgramKegiatan.KodeProgram = 2;
            //    GlobalVar.ProfileProgramKegiatan.KodeKegiatan = 2;
            //    oPrgKegLogic.Simpan(GlobalVar.ProfileProgramKegiatan);
            //    GlobalVar.ProfileProgramKegiatan = oPrgKegLogic.Get();

            //}
            //GlobalVar.CONNECTION_ASET = new RemoteConnection();
            //RemoteConnectionLogic rcLogic = new RemoteConnectionLogic(GlobalVar.TahunAnggaran, 3);

            //GlobalVar.CONNECTION_ASET = rcLogic.GetByJenis(2, GlobalVar.JENIS_KONEKSI);
            //if (GlobalVar.CONNECTION_ASET != null)
            //    GlobalVar.CONNECTION_ASET.Decrypt();
            //else
            //{
            //    MessageBox.Show("Kondeksi ke data Aset gagal.");

            //}
            //GlobalVar.ProfileRekening = oProfileRekeningLogic.GetByID(1);
            //if (GlobalVar.ProfileRekening == null)
            //{
            //    MessageBox.Show("Kode Rekening akan menggunakan 7 digit. Jika berbeda silakan hubungi admin");
            //    GlobalVar.ProfileRekening = new ProfileRekening();
            //    GlobalVar.ProfileRekening.Kode1 = 1;
            //    GlobalVar.ProfileRekening.Kode2 = 1;
            //    GlobalVar.ProfileRekening.Kode3 = 1;
            //    GlobalVar.ProfileRekening.Kode4 = 2;
            //    GlobalVar.ProfileRekening.Kode5 = 2;
            //    oProfileRekeningLogic.Simpan(GlobalVar.ProfileRekening);
            //    GlobalVar.ProfileRekening = oProfileRekeningLogic.GetByID(1);

            //}
                        
        }
        
        private void cmdCetak_Click(object sender, EventArgs e)
        {

          //  frmReportViewer fV = new frmReportViewer();
          //  ParameterLaporan _p = new ParameterLaporan();
          //  fV.SetTanggal(dtPrealisasi.Value );
          //  _p.Tahun =  GlobalVar.TahunAnggaran;
          //  _p.dTanggal = dtPrealisasi.Value;
          //  _p.AwalHalaman = DataFormat.GetInteger(txtAwalNoHal.Text);
            
          //  _p.JenisRekening = rb13.Checked == true ? 1 : 2;


          //   if (chkKosongkanTanggalPerda.Checked == true ){
          //       _p.DenganTanggal = false ;
          //   } else
          //       _p.DenganTanggal = true ;

          //  _p.Tahap = 7;// GetTahap();
          //  _p.pakaiTandaTangan = chkPakaiTandaTangan.Checked;

          //  _p.Keternagan = txtNomor.Text; 
          //  _p.NamaUser = "";// GlobalVar.Pengguna.UserID;

          //  _p.bPPKD = ctrlDinas.PPKD() == 1 ? 1 : 0;
          //  _p.TanggalRealisasi = dtReport.Value.Date;
          //  RefreshNomorPerda();
          //  _p.NomorPerda = "";
          //  _p.NamaPerda = "";

          //  _p.IDDinas = 0;// ctrlDinas1.GetID();
          //  if (rbRingkasanPerda.Checked == true)
          //  {
          //      _p.NamaLaporan = "LAPORAN REALISASI ANGGARAN " ;//ANGGARAN PENDAPATAN DAN BELANJA";
          ////     _p.Title2 = "SEMESTER I TAHUN ANGGARAN " + GlobalVar.TahunAnggaran.ToString();
          //    _p.Title2 = "UNTUK TAHUN YANG BERAKHIR SAMPAI DENGAN 31 DESEMBER " + GlobalVar.TahunAnggaran.ToString();

          //    _p.Title1 = "LAPORAN REALISASI  ANGGARAN ";// +GlobalVar.TahunAnggaran.ToString();
              
          //      _p.Title2 = "TAHUN ANGGARAN" + GlobalVar.TahunAnggaran.ToString();
              
          //      //  if (rb3.Checked)
          //      _p.LastLevel = 3;
          //      //fV.RingkasanPerdaRealisasi(_p);
          //      fV.RingkasanAPBDMurni(_p);

          //  }
          //  if (rbPerdaII.Checked== true )
          //      fV.PerdaIIRealisasi(_p);
          //  if (rbLampiran11.Checked)
          //  {
                
          //       fV.PerdaIIRealisasiB(_p, 1,true);
                 
                
          //  }
          //  if (rbPerdaIII.Checked == true)
          //  {
          //      _p.IDDinas = ctrlDinas.GetID();
          //      _p.IDDInas = ctrlDinas.GetID();
          //      if (rb3.Checked)
          //          _p.LastLevel = 3;
          //      else
          //          _p.LastLevel = 5;

                
          //      fV.PerdaIIIReallisasi (_p);
          //  }

          //  if (rbPerdaIV.Checked== true )
          //      fV.PerdaIVRealisasi050(_p);
          //  if (rbPerdaV.Checked == true)
          //      fV.PerdaVRealisasi050(_p);

          //  if (rbLampiran11.Checked== false )
          //            fV.Show ();
        }
     
        private void RefreshNomorPerda()
        {
            //if (rbPerdaI.Checked == true)
            //    m_iPerda = 1;

            //if (rbPerdaII.Checked == true)
            //    m_iPerda = 2;
            //if (rbPerdaIII.Checked == true)
            //    m_iPerda = 3;
            //if (rbPerdaIV.Checked == true)
            //    m_iPerda = 4;
            //if (rbPerdaV.Checked == true)
            //    m_iPerda = 5;
           
        }

        private void CetakMinutePERBUB(ParameterLaporan _p)
        {
            // objWordApp.PrintPreview;
            //string namaFile = AppDomain.CurrentDomain.BaseDirectory + "PERBUB.docx";
            //string newFIle = AppDomain.CurrentDomain.BaseDirectory + System.DateTime.Now.ToString() + "PERBUB2017.docx";
            //List<TokenReplacementInfo> list = new List<TokenReplacementInfo>();

            //if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Temp"))
            //    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Temp");

            //string n = DateTime.Now.ToString("ddMMMYYYY_HH_mm_ss");

            //newFIle = AppDomain.CurrentDomain.BaseDirectory + "\\PERBUBBARU.docx";
            
            //System.IO.File.Copy(namaFile, newFIle);
            
            //TokenReplacement t = new TokenReplacement(newFIle, "[$", "$]");
            
            //rptPerdaLogic oLogic = new rptPerdaLogic(GlobalVar.TahunAnggaran);
            //List<RingkasanPerda> _lst = new List<RingkasanPerda>();

            //_lst = oLogic.GetRingkasanPerda(_p.Tahun, _p.Tahap, _p);

            //foreach (RingkasanPerda r in _lst)
            //{

            //    list.Add(new TokenReplacementInfo("[$" + (r.IDRekening+3000000).ToString().Trim() +"$]" , r.JumlahMurni ));
            //}

            //t.replaceTokens(list);
            //t.save();

            //Microsoft.Office.Interop.Word.Application objWordApp = new Microsoft.Office.Interop.Word.Application();
            //Microsoft.Office.Interop.Word.Document objDoc = new Microsoft.Office.Interop.Word.Document();


            //objDoc = objWordApp.Documents.Open(newFIle);
            //objWordApp.Documents.Open(newFIle);
            //// objWordApp.PrintPreview;



            //// objDoc.Open();

            //// Process.Start("winword.exe", "C:\\you path here \\filename.docx");


        }
        public Pejabat GetKADA(DateTime date)
        {
            try
            {
                PejabatLogic oLogicPejabat = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                Pejabat oKada = new Pejabat();
                oKada = oLogicPejabat.GetKepalaDaerah();
                return oKada;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void frmCetakPerda_Load(object sender, EventArgs e)
        {
            try
            {
               
                ctrlDinas1.Create();
                ctrlDinas3.Create();

                TabPilihan.TabPages.Remove(tabPage1);//.Hide();

                int Tahap = 7;
                int Tahun = (int)GlobalVar.TahunAnggaran;
                int Jenis = 1;
                PerdaDAL oDAL = new PerdaDAL(GlobalVar.TahunAnggaran);
                Perda oPerda = new Perda();
                
                    oPerda = oDAL.Get(Tahun , Jenis,Tahap);
                    if (oPerda != null)
                    {

                        txtNomor.Text = oPerda.Nomor;

                        if (oPerda.Tanggal != null && oPerda.Tanggal.Year > 2016)
                            dtPrealisasi.Value = oPerda.Tanggal;
                        else
                            dtPrealisasi.Value = DateTime.Now.Date;
                    }
                    else
                    {
                        txtNomor.Text = "";
                        dtPrealisasi.Value = DateTime.Now.Date;

                    }



                    dtReport.Value = new DateTime(GlobalVar.TahunAnggaran, 12, 31);
                   Perda oPerbub = new Perda();
                    oPerbub = oDAL.Get(Tahun , 2,7);
                    //if (DateTime.Now.Year == GlobalVar.TahunAnggaran)
                    //{
                    //    cmdSinergi.Visible = true;
                    //}
                    //else
                    //{
                    //    cmdSinergi.Visible = false;

                    //}
                    //if (oPerbub != null)
                    //{

                    //    txtNoPerbub.Text = oPerbub.Nomor;

                    //    if (oPerbub.Tanggal != null && oPerbub.Tanggal.Year > 2016)
                    //        dtPerbub.Value = oPerbub.Tanggal;
                          
                    //    else
                    //        dtPerbub.Value = DateTime.Now.Date;
                    //}
                    //else
                    //{
                    //    txtNoPerbub.Text = "";
                    //    dtPerbub.Value = DateTime.Now.Date;

                    //}

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void rbPerdaV_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbPerdaII_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdImportDataAPBD_Click(object sender, EventArgs e)
        {
            //frmImportAPBD fIMport = new frmImportAPBD();
            //fIMport.Show();
        }

        private void cmdPerbauikiRealisasi_Click(object sender, EventArgs e)
        {
            //frmInputPlafonPerRekening fINput = new frmInputPlafonPerRekening(2);
            //fINput.SetMode(2);
            //fINput.Show();
        }

        private void cmbPerbaikiBL_Click(object sender, EventArgs e)
        {
          


        }

        private void cmdCetakPenjabaran_Click(object sender, EventArgs e)
        {

            
            ParameterLaporan _p = new ParameterLaporan();
            _p.Tahun = GlobalVar.TahunAnggaran;
            _p.dTanggal = dtPrealisasi.Value;

            _p.JenisRekening = rb13.Checked == true ? 1 : 2;
            _p.AwalHalaman = DataFormat.GetInteger(txtAwalHalamanPenjabaran.Text);
            _p.JenisRekening = rb13.Checked == true ? 1 : 2;
            if (chkPakaiTandatanganPenjabaran.Checked == true)
            {
                _p.Penandatangan = GetKADA(dtPrealisasi.Value);


            }

            if (chkTanpaTanggalPenjabaran.Checked == true)
            {
                _p.Tanggal = "";
            }
            else
            {
                _p.Tanggal = dtPrealisasi.Value.ToTanggalIndonesiaLengkap();
            }
            _p.Nomor = txtNomor.Text;
            _p.Tahap = 7;// GetTahap();

            
            _p.Keterangan = txtNomor.Text;
            _p.NamaUser = "";// GlobalVar.Pengguna.UserID;
            _p.IDDinas = ctrlDinas1.GetID();

            _p.pakaiTandaTangan = chkPakaiTandaTangan2.Checked;

            _p.Rd.Spasi = DataFormat.GetInteger(txtSpasi.Text);
            if (rbPerbubRingkasan.Checked == true)
            {
                _p.NamaLaporan = "Lampiran I ";//ANGGARAN PENDAPATAN DAN BELANJA";
                _p.Rd.Judul1 = GlobalVar.gPemda.Nama;
                _p.Rd.Judul2 = "RINGKASAN LAPORAN REALISASI ANGGARAN";
                _p.Rd.Judul3 = "TAHUN ANGGARAN "+ GlobalVar.TahunAnggaran.ToString();
                _p.Keterangan = "Peraturan Kepala Daerah Kabupaten";

                LaporanPerkada_1 lPerda1 = new LaporanPerkada_1();
                if (lPerda1.CetakLaporanPerkada(_p) == false)
                {
                    MessageBox.Show(lPerda1.Error);

                }
            }

            if (rbPerbubPenjabaran.Checked == true)
            {
                _p.IDDinas = ctrlDinas1.GetID();

                if (_p.IDDinas == 0)
                {
                    MessageBox.Show("SKPD belum dipilih");
                    return;
                }
                _p.NamaLaporan = "Lampiran I.1";

                _p.Keterangan = "Peraturan Kepala Daerah Kabupaten";

                _p.Rd.Judul1 = "Pemerintah Kabupaten Ketapang";
                _p.Rd.Judul2 = "PENJABARAN LAPORAN REALISASI ANGGARAN TAHUN ANGGARAN " + GlobalVar.TahunAnggaran.ToString();
                _p.Rd.Judul3 = "";
                _p.NamaDinas = ctrlDinas1.GetNamaSKPD();

                _p.Jenis = 5;
                LaporanPerkada_1_1 lPerda1 = new LaporanPerkada_1_1();
                if (lPerda1.CetakLaporanPerkada(_p) == false)
                {
                    MessageBox.Show(lPerda1.Error);

                }
            }

        }
        private int GetJenisPenjabaran()
        {
            if (rbPerbubRingkasan.Checked == true)
            {
                return 1;
            } else 
                return 2;
            


        }

        private void cmdSImpan_Click(object sender, EventArgs e)
        {
            PerdaDAL oDAL = new PerdaDAL(GlobalVar.TahunAnggaran);
            Perda oPerda = new Perda();
            oPerda.Tahap = 7;
            oPerda.Tahun = GlobalVar.TahunAnggaran;
            oPerda.Nomor = txtNomor.Text;
            oPerda.Keterangan = "";
            oPerda.Jenis = 1;
            oPerda.Tanggal = dtPrealisasi.Value.Date;
            if (oDAL.Simpan(oPerda) == true)
                MessageBox.Show("Nomor Perda sudah disimpan.");
            else
                MessageBox.Show(oDAL.LastError());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PerdaDAL oDAL = new PerdaDAL(GlobalVar.TahunAnggaran);
            Perda oPerda = new Perda();
            oPerda.Tahap = 7;
            oPerda.Tahun = GlobalVar.TahunAnggaran;
            oPerda.Nomor = txtNoPerbub.Text;
            oPerda.Keterangan = "";
            oPerda.Jenis = 2;
            //oPerda.Tanggal =dtPerbub.Value.Date;
            oPerda.Tanggal = dtReport.Value.Date;

            if (oDAL.Simpan(oPerda) == true)
                MessageBox.Show("Nomor Perbub sudah disimpan.");
            else
                MessageBox.Show(oDAL.LastError());
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void cmdSiapkanData_Click(object sender, EventArgs e)
        {
            
            //rptPerdaLogic oLogic = new rptPerdaLogic((int) GlobalVar.TahunAnggaran);

            //oLogic.BersihkanNonKegiatanRealisasi();
            //oLogic.UpdateRealisasi(dtReport.Value);


            MessageBox.Show("Data sudah dipersiapkan");

        }



        private void rbPerdaIII_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void cmbPerbaikiBL_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //       frmReportViewer fV = new frmReportViewer();
            //ParameterLaporan _p = new ParameterLaporan();
            //fV.SetTanggal(dtPrealisasi.Value );
            //_p.Tahun =  GlobalVar.TahunAnggaran;
            //_p.dTanggal = dtPrealisasi.Value;
            //_p.AwalHalaman = DataFormat.GetInteger(txtAwalNoHal.Text);
            //_p.JenisRekening = rb13.Checked == true ? 1 : 2;

            // if (chkKosongkanTanggalPerda.Checked == true ){
            //     _p.DenganTanggal = false ;
            // } else
            //     _p.DenganTanggal = true ;

            //_p.Tahap = 7;// GetTahap();
            //_p.pakaiTandaTangan = chkPakaiTandaTangan.Checked;

            //_p.Keternagan = txtNomor.Text; 
            //_p.NamaUser = "";// GlobalVar.Pengguna.UserID;

            //_p.bPPKD = ctrlDinas.PPKD() == 1 ? 1 : 0;
            //_p.TanggalRealisasi = dtReport.Value.Date;
            //RefreshNomorPerda();

            //_p.IDDinas = 0;// ctrlDinas1.GetID();
          
                
            // fV.PerdaIIRealisasiB(_p, 1,true);
        }

        private void cmdCetak050_Click(object sender, EventArgs e)
        {

            ParameterLaporan _p = new ParameterLaporan();
            _p.Tahun = GlobalVar.TahunAnggaran;
            _p.AwalHalaman = DataFormat.GetInteger(txtawalHakamanPerda.Text);            
            _p.JenisRekening = rb13.Checked == true ? 1 : 2;
            if (chkPakaiTandaTangan2.Checked== true)
            {
                _p.Penandatangan = GetKADA(dtPrealisasi.Value);


            }

            if (chkKosongkanTanggalPerda2.Checked == true)
            {
                _p.Tanggal = "";
            }
            else
            {
                _p.Tanggal = dtPrealisasi.Value.ToTanggalIndonesiaLengkap();
            }
            _p.Nomor = txtNomor.Text;
            _p.Tahap = 7;// GetTahap();
           

            _p.Keterangan = txtNomor.Text;
            _p.NamaUser = "";// GlobalVar.Pengguna.UserID;
            _p.IDDinas = ctrlDinas3.GetID();
          
            _p.pakaiTandaTangan = chkPakaiTandaTangan2.Checked;
//PERDA

            if (rb050_perUrusan.Checked == true)
            {
                _p.NamaLaporan = "Lampiran I.1";//ANGGARAN PENDAPATAN DAN BELANJA";
                _p.Keterangan = "Peraturan Daerah Kabupaten";

                _p.Rd.Judul1 = GlobalVar.gPemda.Nama;
                _p.Rd.Judul2 = "RINGKASAN LAPORAN REALISASI ANGGARAN MENURUT URUSAN PEMERINTAHAN DAERAH DAN ORGANISASI";

                LaporanPerdaI lPerda1 = new LaporanPerdaI();
                if (lPerda1.CetakLaporanPerda(_p) == false)
                {
                    MessageBox.Show(lPerda1.Error);

                }
            }
            
            if (rb050_Ringkasan.Checked == true)
            {
                _p.NamaLaporan = "Lampiran I.2 ";//ANGGARAN PENDAPATAN DAN BELANJA";
                _p.Rd.Judul1 = GlobalVar.gPemda.Nama;
                _p.Rd.Judul2 = "RINGKASAN LAPORAN REALISASI ANGGARAN MENURUT URUSAN PEMERINTAHAN DAERAH DAN ORGANISASI";
                _p.Keterangan = "Peraturan Daerah Kabupaten";

                LaporanPerda_1_2 lPerda1 = new LaporanPerda_1_2();
                if (lPerda1.CetakLaporanPerda(_p) == false)
                {
                    MessageBox.Show(lPerda1.Error);

                }
            }
            //perda
            if (rbPerda_1_3.Checked == true)
            {
                _p.IDDinas = ctrlDinas3.GetID();

                if (_p.IDDinas == 0)
                {
                    MessageBox.Show("SKPD belum dipilih");
                    return;
                }
                _p.NamaDinas = ctrlDinas3.GetNamaSKPD();
                _p.NamaLaporan = "Lampiran I.3";
                
                _p.Keterangan = "Peraturan Daerah Kabupaten";

                _p.Rd.Judul1 = GlobalVar.gPemda.Nama;
                _p.Rd.Judul2 = "RINCIAN APBD MENURUT URUSAN PEMERINTAHAN DAERAH, ORGANISASI, PROGRAM, KEGIATAN, SUB KEGIATAN, KELOMPOK,";
                _p.Rd.Judul3 = "DAN JENIS PENDAPATAN, BELANJA, DAN PEMBIAYAAN";
                _p.Jenis = 3;
                LaporanPerda_1_3 lPerda1 = new LaporanPerda_1_3();
                if (lPerda1.CetakLaporanPerda(_p) == false)
                {
                    MessageBox.Show(lPerda1.Error);

                }
            }
            
            if (rbPerdaV90.Checked == true)
            {
                _p.NamaLaporan = "";
                _p.Keterangan = "Peraturan Daerah Kabupaten";
                _p.Rd.Judul1 = GlobalVar.gPemda.Nama;
                _p.Rd.Judul2 = "REKAPITULASI REALISASI BELANJA DAERAH UNTUK KESELARASAN DAN KETERPADUAN URUSAN PEMERINTAHAN DAERAH DAN";
                _p.Rd.Judul3 = "FUNGSI DALAM KERANGKA PENGELOLAAN KEUANGAN NEGARA TA " + GlobalVar.TahunAnggaran;

                LaporanPerda_V lPerda1 = new LaporanPerda_V();
                if (lPerda1.CetakLaporanPerda(_p) == false)
                {
                    MessageBox.Show(lPerda1.Error);

                }
            }
            if (rbPerda_1_4.Checked == true)
            {
                _p.NamaLaporan = "Lampiran I.4";
                _p.Keterangan = "Peraturan Daerah Kabupaten";
                _p.Rd.Judul1 = GlobalVar.gPemda.Nama;
                _p.Rd.Judul2 = "REKAPITULASI REALISASI BELANJA MENURUT URUSAN PEMERINTAHAN DAERAH, ORGANISASI, PROGRAM,";
                _p.Rd.Judul3 = "KEGIATAN, DAN SUB KEGIATAN";

                LaporanPerda_1_4 lPerda1 = new LaporanPerda_1_4();
                if (lPerda1.CetakLaporanPerda(_p) == false)
                {
                    MessageBox.Show(lPerda1.Error);

                }
            }
            if (rbSPM.Checked == true)
            {
                _p.NamaLaporan = "Lampiran I.4";
                _p.Keterangan = "Peraturan Daerah Kabupaten";
                _p.Rd.Judul1 = GlobalVar.gPemda.Nama;
                _p.Rd.Judul2 = "REKAPITULASI REALISASI BELANJA UNTUK PEMENUHAN STANDAR PELAYANAN MINIMAL (SPM) TA " + GlobalVar.TahunAnggaran.ToString();
                _p.Rd.Judul3 = "";

                LaporanPerda_SPM lPerda1 = new LaporanPerda_SPM();
                if (lPerda1.CetakLaporanPerda(_p) == false)
                {
                    MessageBox.Show(lPerda1.Error);

                }
            }
            if (rbRealisasiRinci.Checked == true)
            {
                _p.NamaLaporan = "Lampiran ";
                _p.Keterangan = "Peraturan Daerah Kabupaten";
                _p.Rd.Judul1 = GlobalVar.gPemda.Nama;
                _p.Rd.Judul2 = "REKAPITULASI REALISASI RINCI TA " + GlobalVar.TahunAnggaran.ToString();
                _p.Rd.Judul3 = "";

                LaporanLRARinci lPerda1 = new LaporanLRARinci();
                if (lPerda1.CetakLaporanPerda(_p) == false)
                {
                    MessageBox.Show(lPerda1.Error);

                }
            }
            


        }

        private void rb050_Ringkasan_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void cmdInputRealisasi_Click(object sender, EventArgs e)
        {
            frmInputRealisasiPerRekening fInputRealisasi = new frmInputRealisasiPerRekening();
            fInputRealisasi.Show();


        }

        private void chkKosongkanTanggalPerda2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void rbPerbubPenjabaran_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
