namespace KUAPPAS.Akunting
{
    partial class frmNeraca
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblFolderPath = new System.Windows.Forms.Label();
            this.cmdDirektori = new System.Windows.Forms.Button();
            this.chkExcelkanBB = new System.Windows.Forms.CheckBox();
            this.chkELiminasiRK = new System.Windows.Forms.CheckBox();
            this.cmdCek = new System.Windows.Forms.Button();
            this.txtSaldoBKU = new System.Windows.Forms.TextBox();
            this.cmdSaldoBKU = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSpasi = new System.Windows.Forms.TextBox();
            this.cmdJadikanSaldoAwal = new System.Windows.Forms.Button();
            this.cmdExcel = new System.Windows.Forms.Button();
            this.chkPPKD = new System.Windows.Forms.CheckBox();
            this.chkGabunganPPKDDanBukan = new System.Windows.Forms.CheckBox();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLevelRekening = new System.Windows.Forms.ComboBox();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.chkSemuaDinas = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.gridNeraca = new System.Windows.Forms.DataGridView();
            this.idrekening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NeracaTahunIni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaldiAwal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BukuBesar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdExcellkanBukuBesar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridNeraca)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFolderPath
            // 
            this.lblFolderPath.AutoSize = true;
            this.lblFolderPath.Location = new System.Drawing.Point(1051, 68);
            this.lblFolderPath.Name = "lblFolderPath";
            this.lblFolderPath.Size = new System.Drawing.Size(0, 13);
            this.lblFolderPath.TabIndex = 81;
            // 
            // cmdDirektori
            // 
            this.cmdDirektori.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDirektori.Location = new System.Drawing.Point(1051, 46);
            this.cmdDirektori.Name = "cmdDirektori";
            this.cmdDirektori.Size = new System.Drawing.Size(75, 23);
            this.cmdDirektori.TabIndex = 80;
            this.cmdDirektori.Text = "Direktori";
            this.cmdDirektori.UseVisualStyleBackColor = true;
            this.cmdDirektori.Click += new System.EventHandler(this.cmdDirektori_Click);
            // 
            // chkExcelkanBB
            // 
            this.chkExcelkanBB.AutoSize = true;
            this.chkExcelkanBB.Location = new System.Drawing.Point(915, 66);
            this.chkExcelkanBB.Name = "chkExcelkanBB";
            this.chkExcelkanBB.Size = new System.Drawing.Size(130, 17);
            this.chkExcelkanBB.TabIndex = 79;
            this.chkExcelkanBB.Text = "Excellkan Buku Besar";
            this.chkExcelkanBB.UseVisualStyleBackColor = true;
            // 
            // chkELiminasiRK
            // 
            this.chkELiminasiRK.AutoSize = true;
            this.chkELiminasiRK.Checked = true;
            this.chkELiminasiRK.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkELiminasiRK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkELiminasiRK.Location = new System.Drawing.Point(279, 111);
            this.chkELiminasiRK.Name = "chkELiminasiRK";
            this.chkELiminasiRK.Size = new System.Drawing.Size(96, 17);
            this.chkELiminasiRK.TabIndex = 75;
            this.chkELiminasiRK.Text = "Eliminasi RK";
            this.chkELiminasiRK.UseVisualStyleBackColor = true;
            this.chkELiminasiRK.CheckedChanged += new System.EventHandler(this.chkELiminasiRK_CheckedChanged);
            // 
            // cmdCek
            // 
            this.cmdCek.Location = new System.Drawing.Point(132, 410);
            this.cmdCek.Name = "cmdCek";
            this.cmdCek.Size = new System.Drawing.Size(95, 30);
            this.cmdCek.TabIndex = 74;
            this.cmdCek.Text = "Cek";
            this.cmdCek.UseVisualStyleBackColor = true;
            this.cmdCek.Click += new System.EventHandler(this.cmdCek_Click);
            // 
            // txtSaldoBKU
            // 
            this.txtSaldoBKU.Location = new System.Drawing.Point(131, 374);
            this.txtSaldoBKU.Name = "txtSaldoBKU";
            this.txtSaldoBKU.Size = new System.Drawing.Size(203, 20);
            this.txtSaldoBKU.TabIndex = 73;
            // 
            // cmdSaldoBKU
            // 
            this.cmdSaldoBKU.Location = new System.Drawing.Point(126, 336);
            this.cmdSaldoBKU.Name = "cmdSaldoBKU";
            this.cmdSaldoBKU.Size = new System.Drawing.Size(82, 30);
            this.cmdSaldoBKU.TabIndex = 72;
            this.cmdSaldoBKU.Text = "Saldo BKU";
            this.cmdSaldoBKU.UseVisualStyleBackColor = true;
            this.cmdSaldoBKU.Click += new System.EventHandler(this.cmdSaldoBKU_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(799, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 71;
            this.label10.Text = "Tanggal Cetak";
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Location = new System.Drawing.Point(800, 61);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(113, 25);
            this.ctrlTanggal1.TabIndex = 70;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2024, 4, 3, 0, 0, 0, 0);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(720, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 69;
            this.label12.Text = "Spassi Cetak";
            // 
            // txtSpasi
            // 
            this.txtSpasi.BackColor = System.Drawing.Color.Bisque;
            this.txtSpasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpasi.Location = new System.Drawing.Point(723, 65);
            this.txtSpasi.Name = "txtSpasi";
            this.txtSpasi.Size = new System.Drawing.Size(44, 20);
            this.txtSpasi.TabIndex = 68;
            this.txtSpasi.Text = "0";
            // 
            // cmdJadikanSaldoAwal
            // 
            this.cmdJadikanSaldoAwal.Location = new System.Drawing.Point(919, 44);
            this.cmdJadikanSaldoAwal.Name = "cmdJadikanSaldoAwal";
            this.cmdJadikanSaldoAwal.Size = new System.Drawing.Size(98, 15);
            this.cmdJadikanSaldoAwal.TabIndex = 29;
            this.cmdJadikanSaldoAwal.Text = "Jadikan saldo awal ";
            this.cmdJadikanSaldoAwal.UseVisualStyleBackColor = true;
            this.cmdJadikanSaldoAwal.Click += new System.EventHandler(this.cmdJadikanSaldoAwal_Click);
            // 
            // cmdExcel
            // 
            this.cmdExcel.Location = new System.Drawing.Point(548, 52);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(96, 31);
            this.cmdExcel.TabIndex = 28;
            this.cmdExcel.Text = "Excel";
            this.cmdExcel.UseVisualStyleBackColor = true;
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // chkPPKD
            // 
            this.chkPPKD.AutoSize = true;
            this.chkPPKD.Location = new System.Drawing.Point(24, 134);
            this.chkPPKD.Name = "chkPPKD";
            this.chkPPKD.Size = new System.Drawing.Size(55, 17);
            this.chkPPKD.TabIndex = 27;
            this.chkPPKD.Text = "PPKD";
            this.chkPPKD.UseVisualStyleBackColor = true;
            // 
            // chkGabunganPPKDDanBukan
            // 
            this.chkGabunganPPKDDanBukan.AutoSize = true;
            this.chkGabunganPPKDDanBukan.Location = new System.Drawing.Point(24, 157);
            this.chkGabunganPPKDDanBukan.Name = "chkGabunganPPKDDanBukan";
            this.chkGabunganPPKDDanBukan.Size = new System.Drawing.Size(163, 17);
            this.chkGabunganPPKDDanBukan.TabIndex = 26;
            this.chkGabunganPPKDDanBukan.Text = "Gabungan PPKD dan Bukan";
            this.chkGabunganPPKDDanBukan.UseVisualStyleBackColor = true;
            // 
            // cmdCetak
            // 
            this.cmdCetak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetak.Location = new System.Drawing.Point(650, 55);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(73, 31);
            this.cmdCetak.TabIndex = 25;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // cmdLoad
            // 
            this.cmdLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoad.Location = new System.Drawing.Point(435, 50);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(107, 31);
            this.cmdLoad.TabIndex = 24;
            this.cmdLoad.Text = "Panggil Data";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Level";
            // 
            // cmbLevelRekening
            // 
            this.cmbLevelRekening.FormattingEnabled = true;
            this.cmbLevelRekening.Location = new System.Drawing.Point(131, 298);
            this.cmbLevelRekening.Name = "cmbLevelRekening";
            this.cmbLevelRekening.Size = new System.Drawing.Size(278, 21);
            this.cmbLevelRekening.TabIndex = 20;
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Bulan = 5;
            this.ctrlTanggalBulanVertikal1.JenisPeriode = 1;
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(24, 228);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(404, 75);
            this.ctrlTanggalBulanVertikal1.TabIndex = 19;
            this.ctrlTanggalBulanVertikal1.TanggalAkhir = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.TanggalAwal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.Load += new System.EventHandler(this.ctrlTanggalBulanVertikal1_Load);
            // 
            // chkSemuaDinas
            // 
            this.chkSemuaDinas.AutoSize = true;
            this.chkSemuaDinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSemuaDinas.Location = new System.Drawing.Point(24, 111);
            this.chkSemuaDinas.Name = "chkSemuaDinas";
            this.chkSemuaDinas.Size = new System.Drawing.Size(100, 17);
            this.chkSemuaDinas.TabIndex = 18;
            this.chkSemuaDinas.Text = "Semua Dinas";
            this.chkSemuaDinas.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "O P D";
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.BackColor = System.Drawing.SystemColors.Control;
            this.ctrlDinas1.Location = new System.Drawing.Point(81, 180);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(331, 56);
            this.ctrlDinas1.TabIndex = 16;
            this.ctrlDinas1.UK = 0;
            // 
            // gridNeraca
            // 
            this.gridNeraca.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridNeraca.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridNeraca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNeraca.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idrekening,
            this.Detail,
            this.Kode,
            this.Nama,
            this.NeracaTahunIni,
            this.SaldiAwal,
            this.Column1,
            this.BukuBesar});
            this.gridNeraca.Location = new System.Drawing.Point(437, 87);
            this.gridNeraca.Name = "gridNeraca";
            this.gridNeraca.Size = new System.Drawing.Size(755, 372);
            this.gridNeraca.TabIndex = 4;
            this.gridNeraca.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridNeraca_CellContentClick);
            // 
            // idrekening
            // 
            this.idrekening.HeaderText = "Column1";
            this.idrekening.Name = "idrekening";
            this.idrekening.ReadOnly = true;
            this.idrekening.Visible = false;
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Buku Besar";
            this.Detail.Name = "Detail";
            this.Detail.ReadOnly = true;
            this.Detail.Width = 50;
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Kode Rekening";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.Width = 150;
            // 
            // Nama
            // 
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Nama.DefaultCellStyle = dataGridViewCellStyle7;
            this.Nama.HeaderText = "Nama Rekening";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 300;
            // 
            // NeracaTahunIni
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NeracaTahunIni.DefaultCellStyle = dataGridViewCellStyle8;
            this.NeracaTahunIni.HeaderText = "Tahun Ini";
            this.NeracaTahunIni.Name = "NeracaTahunIni";
            this.NeracaTahunIni.ReadOnly = true;
            this.NeracaTahunIni.Width = 150;
            // 
            // SaldiAwal
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SaldiAwal.DefaultCellStyle = dataGridViewCellStyle9;
            this.SaldiAwal.HeaderText = "Neraca Tahun n-1";
            this.SaldiAwal.Name = "SaldiAwal";
            this.SaldiAwal.ReadOnly = true;
            this.SaldiAwal.Width = 150;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Bayangan";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // BukuBesar
            // 
            this.BukuBesar.HeaderText = "Buku Besar";
            this.BukuBesar.Name = "BukuBesar";
            this.BukuBesar.Width = 80;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1192, 46);
            this.ctrlHeader1.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Kode Rekening";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn3.HeaderText = "Nama Rekening";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 500;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn4.HeaderText = "Tahun Ini";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn5.HeaderText = "Neraca Tahun n-1";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // cmdExcellkanBukuBesar
            // 
            this.cmdExcellkanBukuBesar.Location = new System.Drawing.Point(1132, 46);
            this.cmdExcellkanBukuBesar.Name = "cmdExcellkanBukuBesar";
            this.cmdExcellkanBukuBesar.Size = new System.Drawing.Size(75, 23);
            this.cmdExcellkanBukuBesar.TabIndex = 82;
            this.cmdExcellkanBukuBesar.Text = "Excellkan";
            this.cmdExcellkanBukuBesar.UseVisualStyleBackColor = true;
            this.cmdExcellkanBukuBesar.Click += new System.EventHandler(this.cmdExcellkanBukuBesar_Click);
            // 
            // frmNeraca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 460);
            this.Controls.Add(this.cmdExcellkanBukuBesar);
            this.Controls.Add(this.lblFolderPath);
            this.Controls.Add(this.cmdDirektori);
            this.Controls.Add(this.chkExcelkanBB);
            this.Controls.Add(this.chkELiminasiRK);
            this.Controls.Add(this.cmdCek);
            this.Controls.Add(this.txtSaldoBKU);
            this.Controls.Add(this.cmdSaldoBKU);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtSpasi);
            this.Controls.Add(this.cmdJadikanSaldoAwal);
            this.Controls.Add(this.cmdExcel);
            this.Controls.Add(this.chkPPKD);
            this.Controls.Add(this.chkGabunganPPKDDanBukan);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbLevelRekening);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.chkSemuaDinas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.gridNeraca);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmNeraca";
            this.Text = "Neraca";
            this.Load += new System.EventHandler(this.frmNeraca_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridNeraca)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.DataGridView gridNeraca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLevelRekening;
        private Bendahara.ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private System.Windows.Forms.CheckBox chkSemuaDinas;
        private System.Windows.Forms.Label label3;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.Button cmdCetak;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.CheckBox chkGabunganPPKDDanBukan;
        private System.Windows.Forms.CheckBox chkPPKD;
        private System.Windows.Forms.Button cmdExcel;
        private System.Windows.Forms.Button cmdJadikanSaldoAwal;
        private System.Windows.Forms.Label label10;
        private ctrlTanggal ctrlTanggal1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSpasi;
        private System.Windows.Forms.Button cmdSaldoBKU;
        private System.Windows.Forms.TextBox txtSaldoBKU;
        private System.Windows.Forms.Button cmdCek;
        private System.Windows.Forms.CheckBox chkELiminasiRK;
        private System.Windows.Forms.Button cmdDirektori;
        private System.Windows.Forms.CheckBox chkExcelkanBB;
        private System.Windows.Forms.Label lblFolderPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn idrekening;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn NeracaTahunIni;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaldiAwal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn BukuBesar;
        private System.Windows.Forms.Button cmdExcellkanBukuBesar;
    }
}