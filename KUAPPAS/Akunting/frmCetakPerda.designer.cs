namespace KUAPPAS
{
    partial class frmCetakPerda
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCetakPerda));
            this.TabPilihan = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSpasi = new System.Windows.Forms.TextBox();
            this.chkTanpaTanggalPenjabaran = new System.Windows.Forms.CheckBox();
            this.chkPakaiTandatanganPenjabaran = new System.Windows.Forms.CheckBox();
            this.txtAwalHalamanPenjabaran = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdCetakPenjabaran = new System.Windows.Forms.Button();
            this.rbPerbubPenjabaran = new System.Windows.Forms.RadioButton();
            this.rbPerbubRingkasan = new System.Windows.Forms.RadioButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cmdSImpan = new System.Windows.Forms.Button();
            this.txtNomor = new System.Windows.Forms.TextBox();
            this.No = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtPrealisasi = new System.Windows.Forms.DateTimePicker();
            this.rbRealisasiRinci = new System.Windows.Forms.RadioButton();
            this.rbSPM = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkPakaiTandaTangan2 = new System.Windows.Forms.CheckBox();
            this.chkKosongkanTanggalPerda2 = new System.Windows.Forms.CheckBox();
            this.txtawalHakamanPerda = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdCetak050 = new System.Windows.Forms.Button();
            this.rbPerdaV90 = new System.Windows.Forms.RadioButton();
            this.rbPerda_1_4 = new System.Windows.Forms.RadioButton();
            this.rbPerda_1_3 = new System.Windows.Forms.RadioButton();
            this.rb050_Ringkasan = new System.Windows.Forms.RadioButton();
            this.rb050_perUrusan = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.txtNoPerbub = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtPerbub = new System.Windows.Forms.DateTimePicker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dtReport = new System.Windows.Forms.DateTimePicker();
            this.rb13 = new System.Windows.Forms.RadioButton();
            this.rb64 = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.cmdSinergi = new System.Windows.Forms.Button();
            this.cmdInputRealisasi = new System.Windows.Forms.Button();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlDinas3 = new KUAPPAS.ctrlDinas();
            this.TabPilihan.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabPilihan
            // 
            this.TabPilihan.Controls.Add(this.tabPage1);
            this.TabPilihan.Controls.Add(this.tabPage2);
            this.TabPilihan.Controls.Add(this.tabPage3);
            this.TabPilihan.Location = new System.Drawing.Point(0, 63);
            this.TabPilihan.Name = "TabPilihan";
            this.TabPilihan.SelectedIndex = 0;
            this.TabPilihan.Size = new System.Drawing.Size(996, 476);
            this.TabPilihan.TabIndex = 70;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(988, 450);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PERDA REALISASI";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txtSpasi);
            this.tabPage2.Controls.Add(this.chkTanpaTanggalPenjabaran);
            this.tabPage2.Controls.Add(this.chkPakaiTandatanganPenjabaran);
            this.tabPage2.Controls.Add(this.txtAwalHalamanPenjabaran);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cmdCetakPenjabaran);
            this.tabPage2.Controls.Add(this.rbPerbubPenjabaran);
            this.tabPage2.Controls.Add(this.rbPerbubRingkasan);
            this.tabPage2.Controls.Add(this.ctrlDinas1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(988, 450);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "PENJABARAN ";
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 94;
            this.label2.Text = "Spassi Cetak";
            // 
            // txtSpasi
            // 
            this.txtSpasi.BackColor = System.Drawing.Color.Bisque;
            this.txtSpasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpasi.Location = new System.Drawing.Point(231, 242);
            this.txtSpasi.Name = "txtSpasi";
            this.txtSpasi.Size = new System.Drawing.Size(44, 20);
            this.txtSpasi.TabIndex = 93;
            this.txtSpasi.Text = "0";
            // 
            // chkTanpaTanggalPenjabaran
            // 
            this.chkTanpaTanggalPenjabaran.AutoSize = true;
            this.chkTanpaTanggalPenjabaran.Location = new System.Drawing.Point(185, 178);
            this.chkTanpaTanggalPenjabaran.Name = "chkTanpaTanggalPenjabaran";
            this.chkTanpaTanggalPenjabaran.Size = new System.Drawing.Size(99, 17);
            this.chkTanpaTanggalPenjabaran.TabIndex = 92;
            this.chkTanpaTanggalPenjabaran.Text = "Tanpa Tanggal";
            this.chkTanpaTanggalPenjabaran.UseVisualStyleBackColor = true;
            // 
            // chkPakaiTandatanganPenjabaran
            // 
            this.chkPakaiTandatanganPenjabaran.AutoSize = true;
            this.chkPakaiTandatanganPenjabaran.Location = new System.Drawing.Point(185, 201);
            this.chkPakaiTandatanganPenjabaran.Name = "chkPakaiTandatanganPenjabaran";
            this.chkPakaiTandatanganPenjabaran.Size = new System.Drawing.Size(127, 17);
            this.chkPakaiTandatanganPenjabaran.TabIndex = 91;
            this.chkPakaiTandatanganPenjabaran.Text = "Pakai Tanda Tangan";
            this.chkPakaiTandatanganPenjabaran.UseVisualStyleBackColor = true;
            // 
            // txtAwalHalamanPenjabaran
            // 
            this.txtAwalHalamanPenjabaran.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAwalHalamanPenjabaran.Location = new System.Drawing.Point(346, 222);
            this.txtAwalHalamanPenjabaran.Name = "txtAwalHalamanPenjabaran";
            this.txtAwalHalamanPenjabaran.Size = new System.Drawing.Size(39, 23);
            this.txtAwalHalamanPenjabaran.TabIndex = 78;
            this.txtAwalHalamanPenjabaran.Text = "1";
            this.txtAwalHalamanPenjabaran.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 13);
            this.label3.TabIndex = 77;
            this.label3.Text = "Dimulai dengan nomor halaman:";
            // 
            // cmdCetakPenjabaran
            // 
            this.cmdCetakPenjabaran.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cmdCetakPenjabaran.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCetakPenjabaran.Location = new System.Drawing.Point(391, 178);
            this.cmdCetakPenjabaran.Name = "cmdCetakPenjabaran";
            this.cmdCetakPenjabaran.Size = new System.Drawing.Size(131, 54);
            this.cmdCetakPenjabaran.TabIndex = 2;
            this.cmdCetakPenjabaran.Text = "Cetak Penjabaran";
            this.cmdCetakPenjabaran.UseVisualStyleBackColor = false;
            this.cmdCetakPenjabaran.Click += new System.EventHandler(this.cmdCetakPenjabaran_Click);
            // 
            // rbPerbubPenjabaran
            // 
            this.rbPerbubPenjabaran.AutoSize = true;
            this.rbPerbubPenjabaran.Location = new System.Drawing.Point(110, 49);
            this.rbPerbubPenjabaran.Name = "rbPerbubPenjabaran";
            this.rbPerbubPenjabaran.Size = new System.Drawing.Size(208, 17);
            this.rbPerbubPenjabaran.TabIndex = 1;
            this.rbPerbubPenjabaran.Text = "PENJABARAN LAPORAN REALISASI";
            this.rbPerbubPenjabaran.UseVisualStyleBackColor = true;
            this.rbPerbubPenjabaran.CheckedChanged += new System.EventHandler(this.rbPerbubPenjabaran_CheckedChanged);
            // 
            // rbPerbubRingkasan
            // 
            this.rbPerbubRingkasan.AutoSize = true;
            this.rbPerbubRingkasan.Checked = true;
            this.rbPerbubRingkasan.Location = new System.Drawing.Point(110, 26);
            this.rbPerbubRingkasan.Name = "rbPerbubRingkasan";
            this.rbPerbubRingkasan.Size = new System.Drawing.Size(267, 17);
            this.rbPerbubRingkasan.TabIndex = 0;
            this.rbPerbubRingkasan.TabStop = true;
            this.rbPerbubRingkasan.Text = "RINGKASAN LAPORAN REALISASI ANGGARAN ";
            this.rbPerbubRingkasan.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.cmdSImpan);
            this.tabPage3.Controls.Add(this.txtNomor);
            this.tabPage3.Controls.Add(this.No);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.dtPrealisasi);
            this.tabPage3.Controls.Add(this.rbRealisasiRinci);
            this.tabPage3.Controls.Add(this.rbSPM);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.chkPakaiTandaTangan2);
            this.tabPage3.Controls.Add(this.chkKosongkanTanggalPerda2);
            this.tabPage3.Controls.Add(this.txtawalHakamanPerda);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.cmdCetak050);
            this.tabPage3.Controls.Add(this.rbPerdaV90);
            this.tabPage3.Controls.Add(this.rbPerda_1_4);
            this.tabPage3.Controls.Add(this.rbPerda_1_3);
            this.tabPage3.Controls.Add(this.rb050_Ringkasan);
            this.tabPage3.Controls.Add(this.rb050_perUrusan);
            this.tabPage3.Controls.Add(this.ctrlDinas3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(988, 450);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "PERDA REALISASI";
            // 
            // cmdSImpan
            // 
            this.cmdSImpan.Location = new System.Drawing.Point(259, 10);
            this.cmdSImpan.Name = "cmdSImpan";
            this.cmdSImpan.Size = new System.Drawing.Size(134, 31);
            this.cmdSImpan.TabIndex = 103;
            this.cmdSImpan.Text = "Simpan No Perda";
            this.cmdSImpan.UseVisualStyleBackColor = true;
            this.cmdSImpan.Click += new System.EventHandler(this.cmdSImpan_Click);
            // 
            // txtNomor
            // 
            this.txtNomor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNomor.Location = new System.Drawing.Point(83, 10);
            this.txtNomor.Name = "txtNomor";
            this.txtNomor.Size = new System.Drawing.Size(157, 20);
            this.txtNomor.TabIndex = 102;
            // 
            // No
            // 
            this.No.AutoSize = true;
            this.No.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.No.Location = new System.Drawing.Point(11, 15);
            this.No.Name = "No";
            this.No.Size = new System.Drawing.Size(65, 16);
            this.No.TabIndex = 101;
            this.No.Text = "No Perda";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Tanggal";
            // 
            // dtPrealisasi
            // 
            this.dtPrealisasi.CustomFormat = "dd MMM yyyy";
            this.dtPrealisasi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPrealisasi.Location = new System.Drawing.Point(83, 36);
            this.dtPrealisasi.Name = "dtPrealisasi";
            this.dtPrealisasi.Size = new System.Drawing.Size(157, 20);
            this.dtPrealisasi.TabIndex = 99;
            // 
            // rbRealisasiRinci
            // 
            this.rbRealisasiRinci.AutoSize = true;
            this.rbRealisasiRinci.Location = new System.Drawing.Point(27, 331);
            this.rbRealisasiRinci.Name = "rbRealisasiRinci";
            this.rbRealisasiRinci.Size = new System.Drawing.Size(554, 17);
            this.rbRealisasiRinci.TabIndex = 97;
            this.rbRealisasiRinci.TabStop = true;
            this.rbRealisasiRinci.Text = "REALISASI YANGDIRINCI MENURUT KELOMPOK, JENIS, OBJEK, RINCIAN OBJEK,, SUB RINCIAN" +
    " OBJEK,";
            this.rbRealisasiRinci.UseVisualStyleBackColor = true;
            // 
            // rbSPM
            // 
            this.rbSPM.AutoSize = true;
            this.rbSPM.Location = new System.Drawing.Point(27, 308);
            this.rbSPM.Name = "rbSPM";
            this.rbSPM.Size = new System.Drawing.Size(528, 17);
            this.rbSPM.TabIndex = 96;
            this.rbSPM.TabStop = true;
            this.rbSPM.Text = "REKAPITULASI REALISASI BELANJA UNTUK PEMENUHAN STANDAR PELAYANAN MINIMAL (SPM) ";
            this.rbSPM.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 292);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(342, 13);
            this.label10.TabIndex = 95;
            this.label10.Text = "FUNGSI DALAM KERANGKA PENGELOLAAN KEUANGAN NEGARA";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 237);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(189, 20);
            this.label9.TabIndex = 94;
            this.label9.Text = "INFORMASI LAINNYA";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(94, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(293, 13);
            this.label8.TabIndex = 93;
            this.label8.Text = "DAN JENIS PENDAPATAN, BELANJA, DAN PEMBIAYAAN";
            // 
            // chkPakaiTandaTangan2
            // 
            this.chkPakaiTandaTangan2.AutoSize = true;
            this.chkPakaiTandaTangan2.Location = new System.Drawing.Point(211, 405);
            this.chkPakaiTandaTangan2.Name = "chkPakaiTandaTangan2";
            this.chkPakaiTandaTangan2.Size = new System.Drawing.Size(127, 17);
            this.chkPakaiTandaTangan2.TabIndex = 92;
            this.chkPakaiTandaTangan2.Text = "Pakai Tanda Tangan";
            this.chkPakaiTandaTangan2.UseVisualStyleBackColor = true;
            // 
            // chkKosongkanTanggalPerda2
            // 
            this.chkKosongkanTanggalPerda2.AutoSize = true;
            this.chkKosongkanTanggalPerda2.Location = new System.Drawing.Point(211, 382);
            this.chkKosongkanTanggalPerda2.Name = "chkKosongkanTanggalPerda2";
            this.chkKosongkanTanggalPerda2.Size = new System.Drawing.Size(99, 17);
            this.chkKosongkanTanggalPerda2.TabIndex = 91;
            this.chkKosongkanTanggalPerda2.Text = "Tanpa Tanggal";
            this.chkKosongkanTanggalPerda2.UseVisualStyleBackColor = true;
            this.chkKosongkanTanggalPerda2.CheckedChanged += new System.EventHandler(this.chkKosongkanTanggalPerda2_CheckedChanged);
            // 
            // txtawalHakamanPerda
            // 
            this.txtawalHakamanPerda.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtawalHakamanPerda.Location = new System.Drawing.Point(372, 415);
            this.txtawalHakamanPerda.Name = "txtawalHakamanPerda";
            this.txtawalHakamanPerda.Size = new System.Drawing.Size(82, 23);
            this.txtawalHakamanPerda.TabIndex = 90;
            this.txtawalHakamanPerda.Text = "1";
            this.txtawalHakamanPerda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(208, 425);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 13);
            this.label6.TabIndex = 89;
            this.label6.Text = "Dimulai dengan nomor halaman:";
            // 
            // cmdCetak050
            // 
            this.cmdCetak050.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCetak050.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCetak050.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetak050.Image = ((System.Drawing.Image)(resources.GetObject("cmdCetak050.Image")));
            this.cmdCetak050.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCetak050.Location = new System.Drawing.Point(473, 382);
            this.cmdCetak050.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCetak050.Name = "cmdCetak050";
            this.cmdCetak050.Size = new System.Drawing.Size(82, 47);
            this.cmdCetak050.TabIndex = 88;
            this.cmdCetak050.Text = "Cetak";
            this.cmdCetak050.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCetak050.UseVisualStyleBackColor = true;
            this.cmdCetak050.Click += new System.EventHandler(this.cmdCetak050_Click);
            // 
            // rbPerdaV90
            // 
            this.rbPerdaV90.AutoSize = true;
            this.rbPerdaV90.Location = new System.Drawing.Point(27, 272);
            this.rbPerdaV90.Name = "rbPerdaV90";
            this.rbPerdaV90.Size = new System.Drawing.Size(702, 17);
            this.rbPerdaV90.TabIndex = 82;
            this.rbPerdaV90.Text = "REKAPITULASI REALISASI BELANJA DAERAH UNTUK KESELARASAN DAN KETERPADUAN URUSAN PE" +
    "MERINTAHAN DAERAH DAN";
            this.rbPerdaV90.UseVisualStyleBackColor = true;
            // 
            // rbPerda_1_4
            // 
            this.rbPerda_1_4.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.rbPerda_1_4.AutoSize = true;
            this.rbPerda_1_4.Location = new System.Drawing.Point(27, 205);
            this.rbPerda_1_4.Name = "rbPerda_1_4";
            this.rbPerda_1_4.Size = new System.Drawing.Size(811, 17);
            this.rbPerda_1_4.TabIndex = 81;
            this.rbPerda_1_4.Text = "Lamp 1.4: REKAPITULASI REALISASI BELANJA MENURUT URUSAN PEMERINTAHAN DAERAH, ORGA" +
    "NISASI, PROGRAM,  KEGIATAN, DAN SUB KEGIATAN";
            this.rbPerda_1_4.UseVisualStyleBackColor = true;
            // 
            // rbPerda_1_3
            // 
            this.rbPerda_1_3.AutoSize = true;
            this.rbPerda_1_3.Location = new System.Drawing.Point(27, 130);
            this.rbPerda_1_3.Name = "rbPerda_1_3";
            this.rbPerda_1_3.Size = new System.Drawing.Size(741, 17);
            this.rbPerda_1_3.TabIndex = 2;
            this.rbPerda_1_3.Text = "Lamp I.3: RINCIAN APBD MENURUT URUSAN PEMERINTAHAN DAERAH, ORGANISASI, PROGRAM, K" +
    "EGIATAN, SUB KEGIATAN, KELOMPOK, ";
            this.rbPerda_1_3.UseVisualStyleBackColor = true;
            // 
            // rb050_Ringkasan
            // 
            this.rb050_Ringkasan.AutoSize = true;
            this.rb050_Ringkasan.Location = new System.Drawing.Point(27, 98);
            this.rb050_Ringkasan.Name = "rb050_Ringkasan";
            this.rb050_Ringkasan.Size = new System.Drawing.Size(692, 17);
            this.rb050_Ringkasan.TabIndex = 1;
            this.rb050_Ringkasan.Text = "Lamp 1.2 : RINGKASAN APBD YANG DIKLASIFIKASI MENURUT KELOMPOK DAN JENIS PENDAPATA" +
    "N, BELANJA, DAN PEMBIAYAAN";
            this.rb050_Ringkasan.UseVisualStyleBackColor = true;
            this.rb050_Ringkasan.CheckedChanged += new System.EventHandler(this.rb050_Ringkasan_CheckedChanged);
            // 
            // rb050_perUrusan
            // 
            this.rb050_perUrusan.AutoSize = true;
            this.rb050_perUrusan.Checked = true;
            this.rb050_perUrusan.Location = new System.Drawing.Point(27, 73);
            this.rb050_perUrusan.Name = "rb050_perUrusan";
            this.rb050_perUrusan.Size = new System.Drawing.Size(653, 17);
            this.rb050_perUrusan.TabIndex = 0;
            this.rb050_perUrusan.TabStop = true;
            this.rb050_perUrusan.Text = "Lamp 1.1: RINGKASAN LAPORAN REALISASI ANGGARAN MENURUT URUSAN PEMERINTAHAN DAERAH" +
    " DAN ORGANISASI";
            this.rb050_perUrusan.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(755, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 26);
            this.button1.TabIndex = 90;
            this.button1.Text = "Simpan No Perda";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtNoPerbub
            // 
            this.txtNoPerbub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoPerbub.Location = new System.Drawing.Point(501, 37);
            this.txtNoPerbub.Name = "txtNoPerbub";
            this.txtNoPerbub.Size = new System.Drawing.Size(157, 20);
            this.txtNoPerbub.TabIndex = 89;
            this.txtNoPerbub.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(429, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 88;
            this.label4.Text = "No Perda";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(429, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 87;
            this.label5.Text = "Tanggal";
            this.label5.Visible = false;
            // 
            // dtPerbub
            // 
            this.dtPerbub.CustomFormat = "dd MMM yyyy";
            this.dtPerbub.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPerbub.Location = new System.Drawing.Point(501, 11);
            this.dtPerbub.Name = "dtPerbub";
            this.dtPerbub.Size = new System.Drawing.Size(107, 20);
            this.dtPerbub.TabIndex = 86;
            this.dtPerbub.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(996, 22);
            this.statusStrip1.TabIndex = 73;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dtReport
            // 
            this.dtReport.CustomFormat = "dd MMM yyyy";
            this.dtReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtReport.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtReport.Location = new System.Drawing.Point(119, 15);
            this.dtReport.Name = "dtReport";
            this.dtReport.Size = new System.Drawing.Size(171, 26);
            this.dtReport.TabIndex = 76;
            // 
            // rb13
            // 
            this.rb13.AutoSize = true;
            this.rb13.Checked = true;
            this.rb13.Location = new System.Drawing.Point(334, 24);
            this.rb13.Name = "rb13";
            this.rb13.Size = new System.Drawing.Size(99, 17);
            this.rb13.TabIndex = 78;
            this.rb13.TabStop = true;
            this.rb13.Text = "Permendagri 13";
            this.rb13.UseVisualStyleBackColor = true;
            this.rb13.Visible = false;
            // 
            // rb64
            // 
            this.rb64.AutoSize = true;
            this.rb64.Checked = true;
            this.rb64.Location = new System.Drawing.Point(347, 12);
            this.rb64.Name = "rb64";
            this.rb64.Size = new System.Drawing.Size(99, 17);
            this.rb64.TabIndex = 79;
            this.rb64.TabStop = true;
            this.rb64.Text = "Permendagri 64";
            this.rb64.UseVisualStyleBackColor = true;
            this.rb64.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(46, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 20);
            this.label12.TabIndex = 80;
            this.label12.Text = "Tanggal";
            // 
            // cmdSinergi
            // 
            this.cmdSinergi.Location = new System.Drawing.Point(742, 1);
            this.cmdSinergi.Name = "cmdSinergi";
            this.cmdSinergi.Size = new System.Drawing.Size(95, 34);
            this.cmdSinergi.TabIndex = 81;
            this.cmdSinergi.Text = "Siapkan Data";
            this.cmdSinergi.UseVisualStyleBackColor = true;
            this.cmdSinergi.Click += new System.EventHandler(this.cmdSiapkanData_Click);
            // 
            // cmdInputRealisasi
            // 
            this.cmdInputRealisasi.Location = new System.Drawing.Point(843, 1);
            this.cmdInputRealisasi.Name = "cmdInputRealisasi";
            this.cmdInputRealisasi.Size = new System.Drawing.Size(109, 34);
            this.cmdInputRealisasi.TabIndex = 91;
            this.cmdInputRealisasi.Text = "Input Realisasi";
            this.cmdInputRealisasi.UseVisualStyleBackColor = true;
            this.cmdInputRealisasi.Click += new System.EventHandler(this.cmdInputRealisasi_Click);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.BackColor = System.Drawing.SystemColors.Control;
            this.ctrlDinas1.Location = new System.Drawing.Point(146, 81);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(497, 20);
            this.ctrlDinas1.TabIndex = 3;
            this.ctrlDinas1.UK = 0;
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load);
            // 
            // ctrlDinas3
            // 
            this.ctrlDinas3.BackColor = System.Drawing.SystemColors.Control;
            this.ctrlDinas3.Location = new System.Drawing.Point(117, 168);
            this.ctrlDinas3.Name = "ctrlDinas3";
            this.ctrlDinas3.Size = new System.Drawing.Size(526, 22);
            this.ctrlDinas3.TabIndex = 80;
            this.ctrlDinas3.UK = 0;
            // 
            // frmCetakPerda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(996, 562);
            this.Controls.Add(this.cmdInputRealisasi);
            this.Controls.Add(this.cmdSinergi);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rb64);
            this.Controls.Add(this.txtNoPerbub);
            this.Controls.Add(this.rb13);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtReport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dtPerbub);
            this.Controls.Add(this.TabPilihan);
            this.Name = "frmCetakPerda";
            this.Text = "Cetak Perda Realisasi";
            this.Load += new System.EventHandler(this.frmCetakPerda_Load);
            this.TabPilihan.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        //    private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.TabControl TabPilihan;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdCetakPenjabaran;
        private System.Windows.Forms.RadioButton rbPerbubPenjabaran;
        private System.Windows.Forms.RadioButton rbPerbubRingkasan;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.TextBox txtAwalHalamanPenjabaran;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtNoPerbub;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtPerbub;
        private System.Windows.Forms.CheckBox chkPakaiTandatanganPenjabaran;
        private System.Windows.Forms.CheckBox chkTanpaTanggalPenjabaran;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DateTimePicker dtReport;
        private System.Windows.Forms.RadioButton rb13;
        private System.Windows.Forms.RadioButton rb64;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RadioButton rbPerdaV90;
        private System.Windows.Forms.RadioButton rbPerda_1_4;
        private ctrlDinas ctrlDinas3;
        private System.Windows.Forms.RadioButton rbPerda_1_3;
        private System.Windows.Forms.RadioButton rb050_Ringkasan;
        private System.Windows.Forms.RadioButton rb050_perUrusan;
        private System.Windows.Forms.CheckBox chkPakaiTandaTangan2;
        private System.Windows.Forms.CheckBox chkKosongkanTanggalPerda2;
        private System.Windows.Forms.TextBox txtawalHakamanPerda;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdCetak050;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbSPM;
        private System.Windows.Forms.RadioButton rbRealisasiRinci;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button cmdSImpan;
        private System.Windows.Forms.TextBox txtNomor;
        private System.Windows.Forms.Label No;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtPrealisasi;
        private System.Windows.Forms.Button cmdSinergi;
        private System.Windows.Forms.Button cmdInputRealisasi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSpasi;
    }
}