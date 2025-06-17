namespace KUAPPAS.Akunting
{
    partial class frmLO
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
            this.label12 = new System.Windows.Forms.Label();
            this.txtSpasi = new System.Windows.Forms.TextBox();
            this.cmdExcell = new System.Windows.Forms.Button();
            this.cmbLevelRekening = new System.Windows.Forms.ComboBox();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.chkSemuaDinas = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridLRATrx = new System.Windows.Forms.DataGridView();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.cmdJadikanSaldoAwal = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.label2 = new System.Windows.Forms.Label();
            this.chkExcelkanBB = new System.Windows.Forms.CheckBox();
            this.cmdDirektori = new System.Windows.Forms.Button();
            this.lblFolderPath = new System.Windows.Forms.Label();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealisasiBB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Realisasi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Selisih = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDRekening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaldoNormal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BukuBesar = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridLRATrx)).BeginInit();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(740, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 61;
            this.label12.Text = "Spassi Cetak";
            // 
            // txtSpasi
            // 
            this.txtSpasi.BackColor = System.Drawing.Color.Bisque;
            this.txtSpasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpasi.Location = new System.Drawing.Point(743, 67);
            this.txtSpasi.Name = "txtSpasi";
            this.txtSpasi.Size = new System.Drawing.Size(44, 20);
            this.txtSpasi.TabIndex = 60;
            this.txtSpasi.Text = "0";
            // 
            // cmdExcell
            // 
            this.cmdExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExcell.Location = new System.Drawing.Point(563, 50);
            this.cmdExcell.Name = "cmdExcell";
            this.cmdExcell.Size = new System.Drawing.Size(83, 42);
            this.cmdExcell.TabIndex = 59;
            this.cmdExcell.Text = "Excell";
            this.cmdExcell.UseVisualStyleBackColor = true;
            this.cmdExcell.Click += new System.EventHandler(this.cmdExcell_Click);
            // 
            // cmbLevelRekening
            // 
            this.cmbLevelRekening.FormattingEnabled = true;
            this.cmbLevelRekening.Location = new System.Drawing.Point(109, 190);
            this.cmbLevelRekening.Name = "cmbLevelRekening";
            this.cmbLevelRekening.Size = new System.Drawing.Size(286, 21);
            this.cmbLevelRekening.TabIndex = 24;
            // 
            // cmdCetak
            // 
            this.cmdCetak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetak.Location = new System.Drawing.Point(650, 50);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(87, 42);
            this.cmdCetak.TabIndex = 23;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Bulan = 5;
            this.ctrlTanggalBulanVertikal1.JenisPeriode = 1;
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(10, 120);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(404, 75);
            this.ctrlTanggalBulanVertikal1.TabIndex = 22;
            this.ctrlTanggalBulanVertikal1.TanggalAkhir = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.TanggalAwal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.Load += new System.EventHandler(this.ctrlTanggalBulanVertikal1_Load);
            // 
            // chkSemuaDinas
            // 
            this.chkSemuaDinas.AutoSize = true;
            this.chkSemuaDinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSemuaDinas.Location = new System.Drawing.Point(79, 49);
            this.chkSemuaDinas.Name = "chkSemuaDinas";
            this.chkSemuaDinas.Size = new System.Drawing.Size(100, 17);
            this.chkSemuaDinas.TabIndex = 21;
            this.chkSemuaDinas.Text = "Semua Dinas";
            this.chkSemuaDinas.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Level";
            // 
            // gridLRATrx
            // 
            this.gridLRATrx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLRATrx.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridLRATrx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLRATrx.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kode,
            this.Nama,
            this.RealisasiBB,
            this.Realisasi,
            this.Level,
            this.Selisih,
            this.IDRekening,
            this.SaldoNormal,
            this.BukuBesar});
            this.gridLRATrx.Location = new System.Drawing.Point(420, 102);
            this.gridLRATrx.Name = "gridLRATrx";
            this.gridLRATrx.Size = new System.Drawing.Size(778, 331);
            this.gridLRATrx.TabIndex = 20;
            this.gridLRATrx.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLRATrx_CellContentClick);
            // 
            // cmdLoad
            // 
            this.cmdLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoad.Location = new System.Drawing.Point(408, 50);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(149, 42);
            this.cmdLoad.TabIndex = 19;
            this.cmdLoad.Text = "Panggil Data";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "O P D";
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.BackColor = System.Drawing.SystemColors.Control;
            this.ctrlDinas1.Location = new System.Drawing.Point(67, 72);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(331, 56);
            this.ctrlDinas1.TabIndex = 17;
            this.ctrlDinas1.UK = 0;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1198, 46);
            this.ctrlHeader1.TabIndex = 16;
            // 
            // cmdJadikanSaldoAwal
            // 
            this.cmdJadikanSaldoAwal.Location = new System.Drawing.Point(925, 50);
            this.cmdJadikanSaldoAwal.Name = "cmdJadikanSaldoAwal";
            this.cmdJadikanSaldoAwal.Size = new System.Drawing.Size(153, 16);
            this.cmdJadikanSaldoAwal.TabIndex = 62;
            this.cmdJadikanSaldoAwal.Text = "Jadikan saldo awal ";
            this.cmdJadikanSaldoAwal.UseVisualStyleBackColor = true;
            this.cmdJadikanSaldoAwal.Click += new System.EventHandler(this.cmdJadikanSaldoAwal_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(814, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 73;
            this.label10.Text = "Tanggal Cetak";
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Location = new System.Drawing.Point(817, 62);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(112, 25);
            this.ctrlTanggal1.TabIndex = 74;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2025, 1, 26, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 75;
            this.label2.Text = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // chkExcelkanBB
            // 
            this.chkExcelkanBB.AutoSize = true;
            this.chkExcelkanBB.Location = new System.Drawing.Point(948, 68);
            this.chkExcelkanBB.Name = "chkExcelkanBB";
            this.chkExcelkanBB.Size = new System.Drawing.Size(130, 17);
            this.chkExcelkanBB.TabIndex = 77;
            this.chkExcelkanBB.Text = "Excellkan Buku Besar";
            this.chkExcelkanBB.UseVisualStyleBackColor = true;
            // 
            // cmdDirektori
            // 
            this.cmdDirektori.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDirektori.Location = new System.Drawing.Point(1084, 52);
            this.cmdDirektori.Name = "cmdDirektori";
            this.cmdDirektori.Size = new System.Drawing.Size(75, 23);
            this.cmdDirektori.TabIndex = 78;
            this.cmdDirektori.Text = "Direktori";
            this.cmdDirektori.UseVisualStyleBackColor = true;
            this.cmdDirektori.Click += new System.EventHandler(this.cmdDirektori_Click);
            // 
            // lblFolderPath
            // 
            this.lblFolderPath.AutoSize = true;
            this.lblFolderPath.Location = new System.Drawing.Point(1091, 79);
            this.lblFolderPath.Name = "lblFolderPath";
            this.lblFolderPath.Size = new System.Drawing.Size(0, 13);
            this.lblFolderPath.TabIndex = 79;
            this.lblFolderPath.Click += new System.EventHandler(this.lblFolderPath_Click);
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.Width = 150;
            // 
            // Nama
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Nama.DefaultCellStyle = dataGridViewCellStyle1;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 400;
            // 
            // RealisasiBB
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RealisasiBB.DefaultCellStyle = dataGridViewCellStyle2;
            this.RealisasiBB.HeaderText = "LO Tahun Berjalan";
            this.RealisasiBB.Name = "RealisasiBB";
            this.RealisasiBB.Width = 150;
            // 
            // Realisasi
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Realisasi.DefaultCellStyle = dataGridViewCellStyle3;
            this.Realisasi.HeaderText = "LO Tahunn Sebelum";
            this.Realisasi.Name = "Realisasi";
            this.Realisasi.ReadOnly = true;
            this.Realisasi.Width = 150;
            // 
            // Level
            // 
            this.Level.HeaderText = "Level";
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            this.Level.Visible = false;
            // 
            // Selisih
            // 
            this.Selisih.HeaderText = "Selisih";
            this.Selisih.Name = "Selisih";
            this.Selisih.ReadOnly = true;
            this.Selisih.Visible = false;
            // 
            // IDRekening
            // 
            this.IDRekening.HeaderText = "IDRekening";
            this.IDRekening.Name = "IDRekening";
            this.IDRekening.ReadOnly = true;
            this.IDRekening.Visible = false;
            // 
            // SaldoNormal
            // 
            this.SaldoNormal.HeaderText = "SaldoNormal";
            this.SaldoNormal.Name = "SaldoNormal";
            this.SaldoNormal.ReadOnly = true;
            this.SaldoNormal.Visible = false;
            this.SaldoNormal.Width = 30;
            // 
            // BukuBesar
            // 
            this.BukuBesar.HeaderText = "Buku Besar";
            this.BukuBesar.Name = "BukuBesar";
            this.BukuBesar.Width = 80;
            // 
            // frmLO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 445);
            this.Controls.Add(this.lblFolderPath);
            this.Controls.Add(this.cmdDirektori);
            this.Controls.Add(this.chkExcelkanBB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmdJadikanSaldoAwal);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtSpasi);
            this.Controls.Add(this.cmdExcell);
            this.Controls.Add(this.cmbLevelRekening);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.chkSemuaDinas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridLRATrx);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmLO";
            this.Text = "frmLO";
            this.Load += new System.EventHandler(this.frmLO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridLRATrx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbLevelRekening;
        private System.Windows.Forms.Button cmdCetak;
        private Bendahara.ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private System.Windows.Forms.CheckBox chkSemuaDinas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridLRATrx;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.Label label3;
        private ctrlDinas ctrlDinas1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSpasi;
        private System.Windows.Forms.Button cmdExcell;
        private System.Windows.Forms.Button cmdJadikanSaldoAwal;
        private System.Windows.Forms.Label label10;
        private ctrlTanggal ctrlTanggal1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkExcelkanBB;
        private System.Windows.Forms.Button cmdDirektori;
        private System.Windows.Forms.Label lblFolderPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealisasiBB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Realisasi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn Selisih;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaldoNormal;
        private System.Windows.Forms.DataGridViewButtonColumn BukuBesar;
    }
}