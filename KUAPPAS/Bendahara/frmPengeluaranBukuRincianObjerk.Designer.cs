namespace KUAPPAS.Bendahara
{
    partial class frmPengeluaranBukuRincianObjerk
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.lblPencarian = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSpasi = new System.Windows.Forms.TextBox();
            this.chkLS = new System.Windows.Forms.CheckBox();
            this.chkUPGU = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gridRincianObjek = new System.Windows.Forms.DataGridView();
            this.IDProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDKegiatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaKegiatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDSubKegiatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaSubKegiatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdRekening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaRekening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Anggaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBKU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBukti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPGU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlTahapAnggaran1 = new KUAPPAS.Anggaran.ctrlTahapAnggaran();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.cmdPanggilData = new System.Windows.Forms.Button();
            this.ctrlProgramKegiatan1 = new KUAPPAS.ctrlProgramKegiatan();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlPilihanRekeningAnggaran1 = new KUAPPAS.Bendahara.ctrlPilihanRekeningAnggaran();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkPilihProgram = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dtCetak = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.gridRincianObjek)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Location = new System.Drawing.Point(1052, 249);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(75, 23);
            this.cmdCariLagi.TabIndex = 71;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            this.cmdCariLagi.Click += new System.EventHandler(this.cmdCariLagi_Click);
            // 
            // cmdCari
            // 
            this.cmdCari.Location = new System.Drawing.Point(984, 249);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(62, 23);
            this.cmdCari.TabIndex = 70;
            this.cmdCari.Text = "Cari...";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(500, 251);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(478, 20);
            this.txtCari.TabIndex = 69;
            // 
            // lblPencarian
            // 
            this.lblPencarian.AutoSize = true;
            this.lblPencarian.Location = new System.Drawing.Point(439, 258);
            this.lblPencarian.Name = "lblPencarian";
            this.lblPencarian.Size = new System.Drawing.Size(55, 13);
            this.lblPencarian.TabIndex = 68;
            this.lblPencarian.Text = "Pencarian";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(242, 238);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 67;
            this.label12.Text = "Spassi Cetak";
            // 
            // txtSpasi
            // 
            this.txtSpasi.BackColor = System.Drawing.Color.Bisque;
            this.txtSpasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpasi.Location = new System.Drawing.Point(245, 251);
            this.txtSpasi.Name = "txtSpasi";
            this.txtSpasi.Size = new System.Drawing.Size(44, 20);
            this.txtSpasi.TabIndex = 66;
            this.txtSpasi.Text = "2";
            // 
            // chkLS
            // 
            this.chkLS.AutoSize = true;
            this.chkLS.Location = new System.Drawing.Point(114, 218);
            this.chkLS.Name = "chkLS";
            this.chkLS.Size = new System.Drawing.Size(62, 17);
            this.chkLS.TabIndex = 65;
            this.chkLS.Text = "LS/Gaji";
            this.chkLS.UseVisualStyleBackColor = true;
            // 
            // chkUPGU
            // 
            this.chkUPGU.AutoSize = true;
            this.chkUPGU.Location = new System.Drawing.Point(114, 195);
            this.chkUPGU.Name = "chkUPGU";
            this.chkUPGU.Size = new System.Drawing.Size(120, 17);
            this.chkUPGU.TabIndex = 64;
            this.chkUPGU.Text = "Belanja UP/GU/TU";
            this.chkUPGU.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 14);
            this.label3.TabIndex = 63;
            this.label3.Text = "O P D";
            // 
            // gridRincianObjek
            // 
            this.gridRincianObjek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRincianObjek.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridRincianObjek.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRincianObjek.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDProgram,
            this.NamaProgram,
            this.IDKegiatan,
            this.NamaKegiatan,
            this.IDSubKegiatan,
            this.NamaSubKegiatan,
            this.IdRekening,
            this.NamaRekening,
            this.Anggaran,
            this.NoBKU,
            this.Tanggal,
            this.NoBukti,
            this.Keterangan,
            this.UPGU,
            this.TU,
            this.LS,
            this.Saldo});
            this.gridRincianObjek.Location = new System.Drawing.Point(0, 285);
            this.gridRincianObjek.Name = "gridRincianObjek";
            this.gridRincianObjek.Size = new System.Drawing.Size(1238, 297);
            this.gridRincianObjek.TabIndex = 62;
            // 
            // IDProgram
            // 
            this.IDProgram.HeaderText = "IDProgram";
            this.IDProgram.Name = "IDProgram";
            this.IDProgram.ReadOnly = true;
            this.IDProgram.Width = 50;
            // 
            // NamaProgram
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NamaProgram.DefaultCellStyle = dataGridViewCellStyle1;
            this.NamaProgram.HeaderText = "NamaProgram";
            this.NamaProgram.Name = "NamaProgram";
            this.NamaProgram.ReadOnly = true;
            this.NamaProgram.Width = 200;
            // 
            // IDKegiatan
            // 
            this.IDKegiatan.HeaderText = "IDKegiaatan";
            this.IDKegiatan.Name = "IDKegiatan";
            this.IDKegiatan.ReadOnly = true;
            this.IDKegiatan.Width = 50;
            // 
            // NamaKegiatan
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NamaKegiatan.DefaultCellStyle = dataGridViewCellStyle2;
            this.NamaKegiatan.HeaderText = "Nama Kegiatan";
            this.NamaKegiatan.Name = "NamaKegiatan";
            this.NamaKegiatan.ReadOnly = true;
            this.NamaKegiatan.Width = 150;
            // 
            // IDSubKegiatan
            // 
            this.IDSubKegiatan.HeaderText = "IdSubKegiatan";
            this.IDSubKegiatan.Name = "IDSubKegiatan";
            // 
            // NamaSubKegiatan
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NamaSubKegiatan.DefaultCellStyle = dataGridViewCellStyle3;
            this.NamaSubKegiatan.HeaderText = "NamaSub Kegiatan";
            this.NamaSubKegiatan.Name = "NamaSubKegiatan";
            this.NamaSubKegiatan.ReadOnly = true;
            this.NamaSubKegiatan.Width = 250;
            // 
            // IdRekening
            // 
            this.IdRekening.HeaderText = "IdRekening";
            this.IdRekening.Name = "IdRekening";
            this.IdRekening.ReadOnly = true;
            // 
            // NamaRekening
            // 
            this.NamaRekening.HeaderText = "Nama Rekening";
            this.NamaRekening.Name = "NamaRekening";
            // 
            // Anggaran
            // 
            this.Anggaran.HeaderText = "Anggaran";
            this.Anggaran.Name = "Anggaran";
            this.Anggaran.ReadOnly = true;
            // 
            // NoBKU
            // 
            this.NoBKU.HeaderText = "No BKU";
            this.NoBKU.Name = "NoBKU";
            this.NoBKU.ReadOnly = true;
            this.NoBKU.Width = 50;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            // 
            // NoBukti
            // 
            this.NoBukti.HeaderText = "No Bukti";
            this.NoBukti.Name = "NoBukti";
            this.NoBukti.ReadOnly = true;
            // 
            // Keterangan
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Keterangan.DefaultCellStyle = dataGridViewCellStyle4;
            this.Keterangan.HeaderText = "Keterangan";
            this.Keterangan.Name = "Keterangan";
            this.Keterangan.ReadOnly = true;
            this.Keterangan.Width = 300;
            // 
            // UPGU
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.UPGU.DefaultCellStyle = dataGridViewCellStyle5;
            this.UPGU.HeaderText = "LS";
            this.UPGU.Name = "UPGU";
            this.UPGU.ReadOnly = true;
            // 
            // TU
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TU.DefaultCellStyle = dataGridViewCellStyle6;
            this.TU.HeaderText = "UP/GU";
            this.TU.Name = "TU";
            this.TU.ReadOnly = true;
            // 
            // LS
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.LS.DefaultCellStyle = dataGridViewCellStyle7;
            this.LS.HeaderText = "TU";
            this.LS.Name = "LS";
            this.LS.ReadOnly = true;
            // 
            // Saldo
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Saldo.DefaultCellStyle = dataGridViewCellStyle8;
            this.Saldo.HeaderText = "Jumlah";
            this.Saldo.Name = "Saldo";
            this.Saldo.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 14);
            this.label1.TabIndex = 61;
            this.label1.Text = "Jenis Anggaran";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(578, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 14);
            this.label2.TabIndex = 60;
            this.label2.Text = "Program Kegiatan";
            // 
            // ctrlTahapAnggaran1
            // 
            this.ctrlTahapAnggaran1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlTahapAnggaran1.ID = 0;
            this.ctrlTahapAnggaran1.Location = new System.Drawing.Point(114, 165);
            this.ctrlTahapAnggaran1.Name = "ctrlTahapAnggaran1";
            this.ctrlTahapAnggaran1.Size = new System.Drawing.Size(289, 23);
            this.ctrlTahapAnggaran1.TabIndex = 59;
            // 
            // cmdCetak
            // 
            this.cmdCetak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetak.Image = global::KUAPPAS.Properties.Resources.print;
            this.cmdCetak.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCetak.Location = new System.Drawing.Point(138, 239);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(101, 40);
            this.cmdCetak.TabIndex = 57;
            this.cmdCetak.Text = "Cetak ";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // cmdPanggilData
            // 
            this.cmdPanggilData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPanggilData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPanggilData.Image = global::KUAPPAS.Properties.Resources.search;
            this.cmdPanggilData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPanggilData.Location = new System.Drawing.Point(22, 239);
            this.cmdPanggilData.Name = "cmdPanggilData";
            this.cmdPanggilData.Size = new System.Drawing.Size(111, 40);
            this.cmdPanggilData.TabIndex = 56;
            this.cmdPanggilData.Text = "Panggil Data";
            this.cmdPanggilData.UseVisualStyleBackColor = true;
            this.cmdPanggilData.Click += new System.EventHandler(this.cmdPanggilData_Click);
            // 
            // ctrlProgramKegiatan1
            // 
            this.ctrlProgramKegiatan1.Enabled = false;
            this.ctrlProgramKegiatan1.Location = new System.Drawing.Point(690, 90);
            this.ctrlProgramKegiatan1.Name = "ctrlProgramKegiatan1";
            this.ctrlProgramKegiatan1.Size = new System.Drawing.Size(536, 100);
            this.ctrlProgramKegiatan1.TabIndex = 54;
            this.ctrlProgramKegiatan1.OnChanged += new KUAPPAS.ctrlProgramKegiatan.ValueChangedEventHandler(this.ctrlProgramKegiatan1_OnChanged);
            this.ctrlProgramKegiatan1.Load += new System.EventHandler(this.ctrlProgramKegiatan1_Load);
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Bulan = 1;
            this.ctrlTanggalBulanVertikal1.JenisPeriode = 1;
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(17, 90);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(500, 79);
            this.ctrlTanggalBulanVertikal1.TabIndex = 53;
            this.ctrlTanggalBulanVertikal1.TanggalAkhir = new System.DateTime(2024, 3, 3, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.TanggalAwal = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.Load += new System.EventHandler(this.ctrlTanggalBulanVertikal1_Load);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(114, 44);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(403, 43);
            this.ctrlDinas1.TabIndex = 52;
            this.ctrlDinas1.UK = 0;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1238, 41);
            this.ctrlHeader1.TabIndex = 51;
            // 
            // ctrlPilihanRekeningAnggaran1
            // 
            this.ctrlPilihanRekeningAnggaran1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlPilihanRekeningAnggaran1.Enabled = false;
            this.ctrlPilihanRekeningAnggaran1.Location = new System.Drawing.Point(690, 195);
            this.ctrlPilihanRekeningAnggaran1.Name = "ctrlPilihanRekeningAnggaran1";
            this.ctrlPilihanRekeningAnggaran1.Size = new System.Drawing.Size(536, 22);
            this.ctrlPilihanRekeningAnggaran1.TabIndex = 72;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(578, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 73;
            this.label4.Text = "Urusan Pemerintahan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(578, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Program";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(578, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 75;
            this.label6.Text = "Kegaiatan";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(578, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 76;
            this.label7.Text = "Sub Kegiatan";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(578, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 77;
            this.label8.Text = "Rekening";
            // 
            // chkPilihProgram
            // 
            this.chkPilihProgram.AutoSize = true;
            this.chkPilihProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPilihProgram.Location = new System.Drawing.Point(553, 44);
            this.chkPilihProgram.Name = "chkPilihProgram";
            this.chkPilihProgram.Size = new System.Drawing.Size(333, 17);
            this.chkPilihProgram.TabIndex = 78;
            this.chkPilihProgram.Text = "Hanya Sub Kegiatan/Kode Rekening Belanja Tertentu";
            this.chkPilihProgram.UseVisualStyleBackColor = true;
            this.chkPilihProgram.CheckedChanged += new System.EventHandler(this.chkPilihProgram_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 14);
            this.label9.TabIndex = 79;
            this.label9.Text = "Jenis Belanja";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(326, 238);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 81;
            this.label10.Text = "Tanggal Cetak";
            // 
            // dtCetak
            // 
            this.dtCetak.CustomFormat = "dd MMM yyyy";
            this.dtCetak.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCetak.Location = new System.Drawing.Point(326, 251);
            this.dtCetak.Name = "dtCetak";
            this.dtCetak.Size = new System.Drawing.Size(108, 20);
            this.dtCetak.TabIndex = 80;
            // 
            // frmPengeluaranBukuRincianObjerk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 594);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtCetak);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkPilihProgram);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ctrlPilihanRekeningAnggaran1);
            this.Controls.Add(this.cmdCariLagi);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblPencarian);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtSpasi);
            this.Controls.Add(this.chkLS);
            this.Controls.Add(this.chkUPGU);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gridRincianObjek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlTahapAnggaran1);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.cmdPanggilData);
            this.Controls.Add(this.ctrlProgramKegiatan1);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmPengeluaranBukuRincianObjerk";
            this.Text = "frmPengeluaranBukuRincianObjerk";
            this.Load += new System.EventHandler(this.frmPengeluaranBukuRincianObjerk_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRincianObjek)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlProgramKegiatan ctrlProgramKegiatan1;
        private ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private ctrlDinas ctrlDinas1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdCetak;
        private System.Windows.Forms.Button cmdPanggilData;
        private System.Windows.Forms.Label label2;
        private Anggaran.ctrlTahapAnggaran ctrlTahapAnggaran1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridRincianObjek;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDProgram;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaProgram;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDKegiatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaKegiatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDSubKegiatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaSubKegiatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn Anggaran;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBKU;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPGU;
        private System.Windows.Forms.DataGridViewTextBoxColumn TU;
        private System.Windows.Forms.DataGridViewTextBoxColumn LS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Saldo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkUPGU;
        private System.Windows.Forms.CheckBox chkLS;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSpasi;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblPencarian;
        private ctrlPilihanRekeningAnggaran ctrlPilihanRekeningAnggaran1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkPilihProgram;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtCetak;
    }
}